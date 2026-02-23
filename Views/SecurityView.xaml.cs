using System.Windows;
using System.Windows.Controls;

namespace SecureNovaPC.Views
{
    public partial class SecurityView : Page
    {
        public SecurityView()
        {
            InitializeComponent();
            LoadSecurityData();
        }

        private void LoadSecurityData()
        {
            // Load security data
        }

        private void QuickScanButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Starting Quick Scan...", "Security Scan");
        }

        private void FullScanButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Starting Full Scan...", "Security Scan");
        }

        private void CustomScanButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Starting Custom Scan...", "Security Scan");
        }

        private void QuarantineButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Opening Quarantine...", "Quarantine");
        }
    }
}