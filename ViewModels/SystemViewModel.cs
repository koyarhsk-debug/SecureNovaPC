using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SecureNovaPC.Models;
using SecureNovaPC.Services;

namespace SecureNovaPC.ViewModels {
    public class SystemViewModel : INotifyPropertyChanged {
        private SystemMonitoringService systemService;
        private PerformanceMetrics metrics;
        private SystemInfo systemInfo;
        private ObservableCollection<PerformanceMetrics> metricsHistory;

        public SystemViewModel() {
            systemService = new SystemMonitoringService();
            metricsHistory = new ObservableCollection<PerformanceMetrics>();
            LoadSystemData();
        }

        public PerformanceMetrics Metrics {
            get => metrics;
            set {
                metrics = value;
                OnPropertyChanged();
            }
        }

        public SystemInfo SystemInfo {
            get => systemInfo;
            set {
                systemInfo = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<PerformanceMetrics> MetricsHistory {
            get => metricsHistory;
            set {
                metricsHistory = value;
                OnPropertyChanged();
            }
        }

        private void LoadSystemData() {
            try {
                SystemInfo = systemService.GetSystemInfo();
                Metrics = systemService.GetSystemMetrics();
            }
            catch (Exception ex) {
                Console.WriteLine($"Error loading system data: {ex.Message}");
            }
        }

        public void RefreshMetrics() {
            LoadSystemData();
            metricsHistory.Add(Metrics);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}