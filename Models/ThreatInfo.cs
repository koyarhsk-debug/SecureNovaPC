using System;

namespace SecureNovaPC.Models {
    public class ThreatInfo {
        public int ThreatId { get; set; }
        public string ThreatName { get; set; }
        public string ThreatType { get; set; }
        public string Severity { get; set; }
        public string FilePath { get; set; }
        public DateTime DetectionTime { get; set; }
        public string Status { get; set; }
        public string Action { get; set; }
        public long FileSize { get; set; }
    }
}
