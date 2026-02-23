using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SecureNovaPC.Models;
using SecureNovaPC.Services;

namespace SecureNovaPC.ViewModels {
    public class UninstallerViewModel : INotifyPropertyChanged {
        private UninstallerService uninstallerService;
        private ObservableCollection<InstalledApp> installedApps;
        private InstalledApp selectedApp;
        private string searchTerm;

        public UninstallerViewModel() {
            uninstallerService = new UninstallerService();
            installedApps = new ObservableCollection<InstalledApp>();
            LoadInstalledApps();
        }

        public ObservableCollection<InstalledApp> InstalledApps {
            get => installedApps;
            set {
                installedApps = value;
                OnPropertyChanged();
            }
        }

        public InstalledApp SelectedApp {
            get => selectedApp;
            set {
                selectedApp = value;
                OnPropertyChanged();
            }
        }

        public string SearchTerm {
            get => searchTerm;
            set {
                searchTerm = value;
                SearchApplications();
                OnPropertyChanged();
            }
        }

        private void LoadInstalledApps() {
            try {
                var apps = uninstallerService.GetInstalledApplications();
                foreach (var app in apps) {
                    installedApps.Add(app);
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Error loading apps: {ex.Message}");
            }
        }

        private void SearchApplications() {
            if (string.IsNullOrEmpty(searchTerm)) {
                LoadInstalledApps();
            }
            else {
                var results = uninstallerService.SearchApplications(searchTerm);
                installedApps.Clear();
                foreach (var app in results) {
                    installedApps.Add(app);
                }
            }
        }

        public void UninstallApplication() {
            if (SelectedApp != null) {
                uninstallerService.UninstallApplication(SelectedApp.AppId);
                installedApps.Remove(SelectedApp);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}