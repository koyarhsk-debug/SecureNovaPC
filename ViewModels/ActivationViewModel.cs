using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SecureNovaPC.Models;
using SecureNovaPC.Services;

namespace SecureNovaPC.ViewModels {
    public class ActivationViewModel : INotifyPropertyChanged {
        private LicenseService licenseService;
        private LicenseInfo licenseInfo;
        private string licenseKey;
        private bool isActivated;
        private string activationStatus;
        private int daysRemaining;

        public ActivationViewModel() {
            licenseService = new LicenseService();
            LoadLicenseInfo();
        }

        public LicenseInfo LicenseInfo {
            get => licenseInfo;
            set {
                licenseInfo = value;
                OnPropertyChanged();
            }
        }

        public string LicenseKey {
            get => licenseKey;
            set {
                licenseKey = value;
                OnPropertyChanged();
            }
        }

        public bool IsActivated {
            get => isActivated;
            set {
                isActivated = value;
                OnPropertyChanged();
            }
        }

        public string ActivationStatus {
            get => activationStatus;
            set {
                activationStatus = value;
                OnPropertyChanged();
            }
        }

        public int DaysRemaining {
            get => daysRemaining;
            set {
                daysRemaining = value;
                OnPropertyChanged();
            }
        }

        private void LoadLicenseInfo() {
            try {
                LicenseInfo = licenseService.GetLicenseInfo();
                if (LicenseInfo != null) {
                    IsActivated = LicenseInfo.IsActivated;
                    ActivationStatus = IsActivated ? "Activated" : "Not Activated";
                    DaysRemaining = licenseService.GetRemainingDays();
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Error loading license info: {ex.Message}");
            }
        }

        public void ActivateLicense() {
            if (!string.IsNullOrEmpty(licenseKey)) {
                bool result = licenseService.ActivateLicense(licenseKey);
                if (result) {
                    IsActivated = true;
                    ActivationStatus = "Activated Successfully";
                    LoadLicenseInfo();
                }
                else {
                    ActivationStatus = "Activation Failed";
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}