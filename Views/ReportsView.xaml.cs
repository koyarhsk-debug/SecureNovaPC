using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace SecureNovaPC.Views {
    public partial class ReportsView : UserControl {
        public ObservableCollection<ScanReport> ScanHistory { get; set; }
        public ObservableCollection<ActivityLog> ActivityLogs { get; set; }

        public ReportsView() {
            InitializeComponent();
            LoadReportsData();
        }

        private void LoadReportsData() {
            // Initialize scan history
            ScanHistory = new ObservableCollection<ScanReport> {
                new ScanReport {
                    ScanType = "Full Scan",
                    ScanDate = "2026-02-23",
                    Duration = "45 minutes",
                    ThreatsFound = 3,
                    Status = "Completed"
                },
                new ScanReport {
                    ScanType = "Quick Scan",
                    ScanDate = "2026-02-22",
                    Duration = "15 minutes",
                    ThreatsFound = 1,
                    Status = "Completed"
                },
                new ScanReport {
                    ScanType = "Custom Scan",
                    ScanDate = "2026-02-21",
                    Duration = "30 minutes",
                    ThreatsFound = 0,
                    Status = "Completed"
                },
                new ScanReport {
                    ScanType = "Full Scan",
                    ScanDate = "2026-02-20",
                    Duration = "50 minutes",
                    ThreatsFound = 5,
                    Status = "Completed"
                }
            };

            // Initialize activity logs
            ActivityLogs = new ObservableCollection<ActivityLog> {
                new ActivityLog {
                    TimeStamp = "2026-02-23 14:30",
                    Activity = "Threat detected and quarantined",
                    Type = "Security"
                },
                new ActivityLog {
                    TimeStamp = "2026-02-23 13:15",
                    Activity = "Full system scan completed",
                    Type = "Scan"
                },
                new ActivityLog {
                    TimeStamp = "2026-02-23 12:00",
                    Activity = "Real-time protection enabled",
                    Type = "System"
                },
                new ActivityLog {
                    TimeStamp = "2026-02-23 11:45",
                    Activity = "Database updated successfully",
                    Type = "Update"
                },
                new ActivityLog {
                    TimeStamp = "2026-02-23 10:30",
                    Activity = "Application started",
                    Type = "System"
                }
            };

            this.DataContext = this;
        }

        public void ExportReport() {
            try {
                // Export logic here
                System.Windows.MessageBox.Show("Report exported successfully!", "Success", 
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            }
            catch (Exception ex) {
                System.Windows.MessageBox.Show($"Error exporting report: {ex.Message}", "Error",
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        public void PrintReport() {
            try {
                // Print logic here
                System.Windows.MessageBox.Show("Printing report...", "Information",
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            }
            catch (Exception ex) {
                System.Windows.MessageBox.Show($"Error printing report: {ex.Message}", "Error",
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }
    }

    // Helper class for scan reports
    public class ScanReport {
        public string ScanType { get; set; }
        public string ScanDate { get; set; }
        public string Duration { get; set; }
        public int ThreatsFound { get; set; }
        public string Status { get; set; }
    }

    // Helper class for activity logs
    public class ActivityLog {
        public string TimeStamp { get; set; }
        public string Activity { get; set; }
        public string Type { get; set; }
    }
}
