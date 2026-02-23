using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SecureNovaPC.Models;
using SecureNovaPC.Services;

namespace SecureNovaPC.ViewModels {
    public class DashboardViewModel : INotifyPropertyChanged {
        private SystemMonitoringService systemService;
        private PerformanceMetrics currentMetrics;
        private int totalThreats;
        private string systemStatus;
        private string lastScanTime;

        public DashboardViewModel() {
            systemService = new SystemMonitoringService();
            LoadDashboardData();
        }

        public PerformanceMetrics CurrentMetrics {
            get => currentMetrics;
            set {
                currentMetrics = value;
                OnPropertyChanged();
            }
        }

        public int TotalThreats {
            get => totalThreats;
            set {
                totalThreats = value;
                OnPropertyChanged();
            }
        }

        public string SystemStatus {
            get => systemStatus;
            set {
                systemStatus = value;
                OnPropertyChanged();
            }
        }

        public string LastScanTime {
            get => lastScanTime;
            set {
                lastScanTime = value;
                OnPropertyChanged();
            }
        }

        private void LoadDashboardData() {
            try {
                CurrentMetrics = systemService.GetSystemMetrics();
                SystemStatus = TotalThreats == 0 ? "SAFE" : "AT RISK";
                LastScanTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            }
            catch (Exception ex) {
                Console.WriteLine($"Error loading dashboard: {ex.Message}");
            }
        }

        public void RefreshDashboard() {
            LoadDashboardData();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}