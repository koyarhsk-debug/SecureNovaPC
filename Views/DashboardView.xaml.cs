using System.Windows;
using System.Windows.Controls;

namespace SecureNovaPC.Views {
    public partial class DashboardView : Page {
        public DashboardView() {
            InitializeComponent();
            LoadDashboardData();
        }

        private void LoadDashboardData() {
            // Load dashboard data
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("Refreshing dashboard...", "Dashboard");
            LoadDashboardData();
        }

        private void OptimizeButton_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("Starting optimization...", "Optimize");
        }
    }
}