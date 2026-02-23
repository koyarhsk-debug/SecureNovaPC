using System;
using System.Windows;

namespace SecureNovaPC
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // Add application startup logic here
            InitializeServices();
        }

        private void InitializeServices()
        {
            // Add service initialization logic here
        }
    }
}