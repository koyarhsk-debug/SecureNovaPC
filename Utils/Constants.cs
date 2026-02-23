using System;

namespace SecureNovaPC.Utils
{
    public static class Constants
    {
        public const string ApplicationName = "SecureNovaPC";
        public const string Version = "1.0.0";
        public const string Developer = "Koyarhsk Debug Team";
    }

    public enum UserRoles
    {
        Admin,
        User,
        Guest
    }

    public enum LogLevel
    {
        Info,
        Warning,
        Error,
        Debug
    }
}