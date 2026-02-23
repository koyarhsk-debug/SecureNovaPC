using System;
using System.Diagnostics;
using SecureNovaPC.Models;

namespace SecureNovaPC.Services {
    public class SystemService {
        private PerformanceCounter cpuCounter;
        private PerformanceCounter ramCounter;

        public SystemService() {
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total", true);
            ramCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use", true);
        }

        public PerformanceMetrics GetSystemMetrics() {
            try {
                var metrics = new PerformanceMetrics {
                    CPUUsage = cpuCounter.NextValue(),
                    MemoryUsage = ramCounter.NextValue(),
                    DiskUsage = GetDiskUsage(),
                    SystemTemperature = GetSystemTemperature(),
                    AvailableMemory = GC.GetTotalMemory(false),
                    ProcessCount = Process.GetProcessCount(),
                    MeasurementTime = DateTime.Now
                };
                return metrics;
            }
            catch (Exception ex) {
                Console.WriteLine($"Error getting metrics: {ex.Message}");
                return null;
            }
        }

        private double GetDiskUsage() {
            try {
                var drives = DriveInfo.GetDrives();
                if (drives.Length > 0) {
                    var systemDrive = drives[0];
                    return (systemDrive.TotalSize - systemDrive.AvailableFreeSpace) * 100 / systemDrive.TotalSize;
                }
                return 0;
            }
            catch {
                return 0;
            }
        }

        private double GetSystemTemperature() {
            // Placeholder - requires WMI or hardware access
            return 52.0;
        }

        public SystemInfo GetSystemInfo() {
            try {
                return new SystemInfo {
                    OSName = Environment.OSVersion.VersionString,
                    ProcessorName = GetProcessorName(),
                    ProcessorCount = Environment.ProcessorCount,
                    ComputerName = Environment.MachineName,
                    UserName = Environment.UserName
                };
            }
            catch (Exception ex) {
                Console.WriteLine($"Error getting system info: {ex.Message}");
                return null;
            }
        }

        private string GetProcessorName() {
            try {
                var searcher = new System.Management.ManagementObjectSearcher("SELECT * FROM Win32_Processor");
                foreach (var obj in searcher.Get()) {
                    return obj["Name"].ToString();
                }
                return "Unknown";
            }
            catch {
                return "Unknown";
            }
        }
    }
}
