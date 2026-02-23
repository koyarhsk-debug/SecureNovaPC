using System;

namespace LicenseDataModels
{
    public class LicenseInfo
    {
        public string LicenseKey { get; set; }
        public string Email { get; set; }
        public string SubscriptionType { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int DaysRemaining { get; set; }
    }

    public class LicenseValidationRequest
    {
        public string LicenseKey { get; set; }
        public string Email { get; set; }
    }

    public class LicenseValidationResponse
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
        public LicenseInfo LicenseInfo { get; set; }
    }
}