using System;

namespace SecureNovaPC.Models {
    public class PerformanceMetrics {
        public double CPUUsage { get; set; }
        public double MemoryUsage { get; set; }
        public double DiskUsage { get; set; }
        public double SystemTemperature { get; set; }
        public long AvailableMemory { get; set; }
        public long UsedMemory { get; set; }
        public DateTime MeasurementTime { get; set; }
        public int ProcessCount { get; set; }
        public double NetworkLatency { get; set; }
    }
}
