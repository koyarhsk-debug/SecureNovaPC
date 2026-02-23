using System.Windows;

namespace SecureNovaPC {
    public partial class ActivationWindow : Window {
        public ActivationWindow() {
            InitializeComponent();
        }

        private void ActivateButton_Click(object sender, RoutedEventArgs e) {
            string licenseKey = LicenseKeyTextBox.Text;
            string email = EmailTextBox.Text;

            if (string.IsNullOrWhiteSpace(licenseKey) || string.IsNullOrWhiteSpace(email)) {
                MessageBox.Show("Please enter both License Key and Email!", "Validation Error");
                return;
            }

            MessageBox.Show("License activated successfully!", "Activation");
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("Opening purchase page...", "Buy License");
        }

        private void TrialButton_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("Starting 30-day trial...", "Trial Activation");
            this.Close();
        }
    }
}