using System;
using System.IO;

namespace SecureNovaPC.Services
{
    public class LicenseService
    {
        private const string LicenseFilePath = "license.key";
        
        public bool ValidateLicense(string licenseKey)
        {
            // Simulate license validation
            return !string.IsNullOrEmpty(licenseKey) && licenseKey == GetStoredLicenseKey();
        }
        
        private string GetStoredLicenseKey()
        {
            if (File.Exists(LicenseFilePath))
            {
                return File.ReadAllText(LicenseFilePath);
            }
            return null;
        }
        
        public void StoreLicenseKey(string licenseKey)
        {
            File.WriteAllText(LicenseFilePath, licenseKey);
        }
    }
}