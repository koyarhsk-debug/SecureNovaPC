using System.Windows;

namespace SecureNovaPC {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            this.Title = "SecureNova PC - System Protection & Optimization";
            LoadDashboard();
        }

        private void LoadDashboard() {
            if (MainContent != null) {
                MainContent.Content = new Views.DashboardView();
            }
        }

        private void DashboardButton_Click(object sender, RoutedEventArgs e) {
            MainContent.Content = new Views.DashboardView();
        }

        private void SystemButton_Click(object sender, RoutedEventArgs e) {
            MainContent.Content = new Views.SystemView();
        }

        private void SecurityButton_Click(object sender, RoutedEventArgs e) {
            MainContent.Content = new Views.SecurityView();
        }

        private void NetworkButton_Click(object sender, RoutedEventArgs e) {
            MainContent.Content = new Views.NetworkDefenderView();
        }

        private void UninstallerButton_Click(object sender, RoutedEventArgs e) {
            MainContent.Content = new Views.UninstallerView();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e) {
            MainContent.Content = new Views.SettingsView();
        }
    }
}