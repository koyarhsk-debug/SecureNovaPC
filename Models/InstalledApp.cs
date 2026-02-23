using System;

namespace SecureNovaPC.Models {
    public class InstalledApp {
        public string AppId { get; set; }
        public string AppName { get; set; }
        public string Version { get; set; }
        public string Publisher { get; set; }
        public long AppSize { get; set; }
        public DateTime InstallDate { get; set; }
        public string InstallLocation { get; set; }
        public bool IsSystemApp { get; set; }
        public string Category { get; set; }
    }
}
