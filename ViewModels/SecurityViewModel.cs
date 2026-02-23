using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SecureNovaPC.Models;
using SecureNovaPC.Services;

namespace SecureNovaPC.ViewModels {
    public class SecurityViewModel : INotifyPropertyChanged {
        private ObservableCollection<ThreatInfo> detectedThreats;
        private ObservableCollection<ThreatInfo> quarantinedFiles;
        private bool isScanning;
        private int scanProgress;
        private string scanStatus;

        public SecurityViewModel() {
            detectedThreats = new ObservableCollection<ThreatInfo>();
            quarantinedFiles = new ObservableCollection<ThreatInfo>();
            scanStatus = "Ready";
        }

        public ObservableCollection<ThreatInfo> DetectedThreats {
            get => detectedThreats;
            set {
                detectedThreats = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ThreatInfo> QuarantinedFiles {
            get => quarantinedFiles;
            set {
                quarantinedFiles = value;
                OnPropertyChanged();
            }
        }

        public bool IsScanning {
            get => isScanning;
            set {
                isScanning = value;
                OnPropertyChanged();
            }
        }

        public int ScanProgress {
            get => scanProgress;
            set {
                scanProgress = value;
                OnPropertyChanged();
            }
        }

        public string ScanStatus {
            get => scanStatus;
            set {
                scanStatus = value;
                OnPropertyChanged();
            }
        }

        public void StartQuickScan() {
            IsScanning = true;
            ScanStatus = "Quick Scan in progress...";
            ScanProgress = 0;
        }

        public void StartFullScan() {
            IsScanning = true;
            ScanStatus = "Full Scan in progress...";
            ScanProgress = 0;
        }

        public void QuarantineThreat(ThreatInfo threat) {
            if (detectedThreats.Contains(threat)) {
                detectedThreats.Remove(threat);
                quarantinedFiles.Add(threat);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}