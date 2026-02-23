using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Win32;
using SecureNovaPC.Models;

namespace SecureNovaPC.Services {
    public class UninstallerService {
        private List<InstalledApp> installedApps;

        public UninstallerService() {
            installedApps = new List<InstalledApp>();
        }

        public List<InstalledApp> GetInstalledApplications() {
            try {
                installedApps.Clear();
                GetAppsFromRegistry(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
                GetAppsFromRegistry(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
                return installedApps.OrderBy(a => a.AppName).ToList();
            }
            catch (Exception ex) {
                Console.WriteLine($"Error getting installed applications: {ex.Message}");
                return new List<InstalledApp>();
            }
        }

        private void GetAppsFromRegistry(string registryPath) {
            try {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registryPath)) {
                    if (key != null) {
                        foreach (string subKeyName in key.GetSubKeyNames()) {
                            try {
                                using (RegistryKey subKey = key.OpenSubKey(subKeyName)) {
                                    object displayName = subKey.GetValue("DisplayName");
                                    object displayVersion = subKey.GetValue("DisplayVersion");
                                    object publisher = subKey.GetValue("Publisher");
                                    object installLocation = subKey.GetValue("InstallLocation");
                                    object estimatedSize = subKey.GetValue("EstimatedSize");

                                    if (displayName != null) {
                                        var app = new InstalledApp {
                                            AppId = subKeyName,
                                            AppName = displayName.ToString(),
                                            Version = displayVersion?.ToString() ?? "Unknown",
                                            Publisher = publisher?.ToString() ?? "Unknown",
                                            AppSize = ConvertSizeToBytes(estimatedSize),
                                            InstallLocation = installLocation?.ToString() ?? "Unknown"
                                        };
                                        installedApps.Add(app);
                                    }
                                }
                            }
                            catch {
                                // Skip apps with errors
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Error reading registry: {ex.Message}");
            }
        }

        private long ConvertSizeToBytes(object sizeObject) {
            if (sizeObject != null && long.TryParse(sizeObject.ToString(), out long size)) {
                return size * 1024; // Convert KB to bytes
            }
            return 0;
        }

        public List<InstalledApp> SearchApplications(string searchTerm) {
            return installedApps
                .Where(a => a.AppName.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
        }

        public bool UninstallApplication(string appId) {
            try {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey($@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{appId}", true)) {
                    if (key != null) {
                        object uninstallString = key.GetValue("UninstallString");
                        if (uninstallString != null) {
                            ExecuteUninstaller(uninstallString.ToString());
                            return true;
                        }
                    }
                }

                using (RegistryKey key = Registry.LocalMachine.OpenSubKey($@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\{appId}", true)) {
                    if (key != null) {
                        object uninstallString = key.GetValue("UninstallString");
                        if (uninstallString != null) {
                            ExecuteUninstaller(uninstallString.ToString());
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex) {
                Console.WriteLine($"Error uninstalling application: {ex.Message}");
                return false;
            }
        }

        private void ExecuteUninstaller(string uninstallString) {
            try {
                if (uninstallString.StartsWith("\"")) {
                    // Handle quoted paths
                    int endQuote = uninstallString.IndexOf("\"", 1);
                    string executablePath = uninstallString.Substring(1, endQuote - 1);
                    string arguments = uninstallString.Substring(endQuote + 1).Trim();

                    ProcessStartInfo psi = new ProcessStartInfo {
                        FileName = executablePath,
                        Arguments = arguments,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    using (Process process = Process.Start(psi)) {
                        process.WaitForExit();
                    }
                }
                else {
                    Process.Start(uninstallString);
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Error executing uninstaller: {ex.Message}");
            }
        }

        public long GetTotalInstalledAppsSize() {
            return installedApps.Sum(a => a.AppSize);
        }

        public int GetTotalInstalledAppsCount() {
            return installedApps.Count;
        }

        public List<InstalledApp> GetLargestApplications(int topCount = 10) {
            return installedApps
                .OrderByDescending(a => a.AppSize)
                .Take(topCount)
                .ToList();
        }
    }
}
