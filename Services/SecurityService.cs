using System;
using System.Collections.Generic;
using System.IO;
using SecureNovaPC.Models;

namespace SecureNovaPC.Services {
    public class SecurityService {
        private List<ThreatInfo> detectedThreats;
        private List<string> quarantinedFiles;

        public SecurityService() {
            detectedThreats = new List<ThreatInfo>();
            quarantinedFiles = new List<string>();
        }

        public List<ThreatInfo> PerformQuickScan() {
            try {
                detectedThreats.Clear();
                ScanDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
                ScanDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                return detectedThreats;
            }
            catch (Exception ex) {
                Console.WriteLine($"Error during quick scan: {ex.Message}");
                return new List<ThreatInfo>();
            }
        }

        public List<ThreatInfo> PerformFullScan() {
            try {
                detectedThreats.Clear();
                ScanDirectory(Environment.GetEnvironmentVariable("SystemDrive"));
                return detectedThreats;
            }
            catch (Exception ex) {
                Console.WriteLine($"Error during full scan: {ex.Message}");
                return new List<ThreatInfo>();
            }
        }

        public List<ThreatInfo> PerformCustomScan(string folderPath) {
            try {
                detectedThreats.Clear();
                if (Directory.Exists(folderPath)) {
                    ScanDirectory(folderPath);
                }
                return detectedThreats;
            }
            catch (Exception ex) {
                Console.WriteLine($"Error during custom scan: {ex.Message}");
                return new List<ThreatInfo>();
            }
        }

        private void ScanDirectory(string directoryPath) {
            try {
                var files = Directory.GetFiles(directoryPath);
                foreach (var file in files) {
                    if (IsFileThreat(file)) {
                        detectedThreats.Add(new ThreatInfo {
                            ThreatId = detectedThreats.Count + 1,
                            ThreatName = Path.GetFileName(file),
                            ThreatType = "Malware",
                            Severity = "High",
                            FilePath = file,
                            DetectionTime = DateTime.Now,
                            Status = "Detected",
                            FileSize = new FileInfo(file).Length
                        });
                    }
                }

                var subdirectories = Directory.GetDirectories(directoryPath);
                foreach (var subdir in subdirectories) {
                    ScanDirectory(subdir);
                }
            }
            catch (UnauthorizedAccessException) {
                // Skip directories without access
            }
        }

        private bool IsFileThreat(string filePath) {
            string[] threatExtensions = { ".exe", ".bat", ".scr", ".vbs", ".js" };
            string fileExtension = Path.GetExtension(filePath).ToLower();
            return Array.Exists(threatExtensions, ext => ext == fileExtension);
        }

        public void QuarantineFile(string filePath) {
            try {
                string quarantineFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SecureNovaPC\\Quarantine");
                if (!Directory.Exists(quarantineFolder)) {
                    Directory.CreateDirectory(quarantineFolder);
                }

                string fileName = Path.GetFileName(filePath);
                string quarantinePath = Path.Combine(quarantineFolder, fileName);
                File.Move(filePath, quarantinePath);
                quarantinedFiles.Add(quarantinePath);
            }
            catch (Exception ex) {
                Console.WriteLine($"Error quarantining file: {ex.Message}");
            }
        }

        public List<string> GetQuarantinedFiles() {
            return quarantinedFiles;
        }

        public int GetTotalThreatsDetected() {
            return detectedThreats.Count;
        }
    }
}
