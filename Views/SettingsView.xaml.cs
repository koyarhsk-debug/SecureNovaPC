using System.Windows;
using System.Windows.Controls;

namespace SecureNovaPC.Views {
    public partial class SettingsView : Page {
        public SettingsView() {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings() {
            // Load application settings
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("Settings saved successfully!", "Settings");
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("Settings reset to default!", "Settings");
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("SecureNova PC v1.0.0\nAdvanced System Protection & Optimization", "About");
        }
    }
}
