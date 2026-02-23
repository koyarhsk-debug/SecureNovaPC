using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SecureNovaPC.Services;

namespace SecureNovaPC.ViewModels {
    public class NetworkViewModel : INotifyPropertyChanged {
        private NetworkService networkService;
        private string publicIP;
        private string localIP;
        private double networkLatency;
        private bool isConnected;
        private ObservableCollection<string> activeConnections;
        private ObservableCollection<string> blockedApps;

        public NetworkViewModel() {
            networkService = new NetworkService();
            activeConnections = new ObservableCollection<string>();
            blockedApps = new ObservableCollection<string>();
            LoadNetworkData();
        }

        public string PublicIP {
            get => publicIP;
            set {
                publicIP = value;
                OnPropertyChanged();
            }
        }

        public string LocalIP {
            get => localIP;
            set {
                localIP = value;
                OnPropertyChanged();
            }
        }

        public double NetworkLatency {
            get => networkLatency;
            set {
                networkLatency = value;
                OnPropertyChanged();
            }
        }

        public bool IsConnected {
            get => isConnected;
            set {
                isConnected = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> ActiveConnections {
            get => activeConnections;
            set {
                activeConnections = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> BlockedApps {
            get => blockedApps;
            set {
                blockedApps = value;
                OnPropertyChanged();
            }
        }

        private void LoadNetworkData() {
            try {
                IsConnected = networkService.IsNetworkConnected();
                PublicIP = networkService.GetPublicIPAddress();
                LocalIP = networkService.GetLocalIPAddress();
                NetworkLatency = networkService.CheckNetworkLatency();
            }
            catch (Exception ex) {
                Console.WriteLine($"Error loading network data: {ex.Message}");
            }
        }

        public void RefreshNetworkData() {
            LoadNetworkData();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}