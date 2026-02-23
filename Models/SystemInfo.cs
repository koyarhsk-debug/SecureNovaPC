using System;

namespace SecureNovaPC.Models {
    public class SystemInfo {
        public string OSName { get; set; }
        public string OSVersion { get; set; }
        public string ProcessorName { get; set; }
        public int ProcessorCount { get; set; }
        public long TotalMemory { get; set; }
        public DateTime LastBootTime { get; set; }
        public string ComputerName { get; set; }
        public string UserName { get; set; }
    }
}
