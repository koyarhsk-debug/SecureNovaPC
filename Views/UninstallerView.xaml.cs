using System.Windows;
using System.Windows.Controls;

namespace SecureNovaPC.Views {
    public partial class UninstallerView : Page {
        public UninstallerView() {
            InitializeComponent();
            LoadInstalledApps();
        }

        private void LoadInstalledApps() {
            // Load installed applications list
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("Refreshing application list...", "Uninstaller");
            LoadInstalledApps();
        }

        private void UninstallButton_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("Uninstalling selected application...", "Uninstaller");
        }
    }
}
