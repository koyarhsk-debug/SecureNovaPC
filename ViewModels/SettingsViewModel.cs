using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SecureNovaPC.ViewModels {
    public class SettingsViewModel : INotifyPropertyChanged {
        private bool autoStartEnabled;
        private bool notificationsEnabled;
        private bool darkModeEnabled;
        private string scanSchedule;
        private string language;
        private bool updateCheckEnabled;

        public SettingsViewModel() {
            LoadSettings();
        }

        public bool AutoStartEnabled {
            get => autoStartEnabled;
            set {
                autoStartEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool NotificationsEnabled {
            get => notificationsEnabled;
            set {
                notificationsEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool DarkModeEnabled {
            get => darkModeEnabled;
            set {
                darkModeEnabled = value;
                OnPropertyChanged();
            }
        }

        public string ScanSchedule {
            get => scanSchedule;
            set {
                scanSchedule = value;
                OnPropertyChanged();
            }
        }

        public string Language {
            get => language;
            set {
                language = value;
                OnPropertyChanged();
            }
        }

        public bool UpdateCheckEnabled {
            get => updateCheckEnabled;
            set {
                updateCheckEnabled = value;
                OnPropertyChanged();
            }
        }

        private void LoadSettings() {
            AutoStartEnabled = true;
            NotificationsEnabled = true;
            DarkModeEnabled = false;
            ScanSchedule = "Daily";
            Language = "English";
            UpdateCheckEnabled = true;
        }

        public void SaveSettings() {
            // Save settings logic here
        }

        public void ResetSettings() {
            LoadSettings();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}