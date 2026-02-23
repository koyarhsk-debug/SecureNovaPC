using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace SecureNovaPC.Utils {
    public class Helper {
        public static string FormatBytes(long bytes) {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;

            while (len >= 1024 && order < sizes.Length - 1) {
                order++;
                len = len / 1024;
            }

            return $"{len:0.##} {sizes[order]}";
        }

        public static long ConvertToBytes(double value, string unit) {
            return unit.ToUpper() switch {
                "B" => (long)value,
                "KB" => (long)(value * 1024),
                "MB" => (long)(value * 1024 * 1024),
                "GB" => (long)(value * 1024 * 1024 * 1024),
                "TB" => (long)(value * 1024 * 1024 * 1024 * 1024),
                _ => (long)value
            };
        }

        public static string FormatTime(TimeSpan timeSpan) {
            if (timeSpan.TotalDays >= 1) {
                return $"{timeSpan.Days}d {timeSpan.Hours}h";
            }
            else if (timeSpan.TotalHours >= 1) {
                return $"{timeSpan.Hours}h {timeSpan.Minutes}m";
            }
            else if (timeSpan.TotalMinutes >= 1) {
                return $"{timeSpan.Minutes}m {timeSpan.Seconds}s";
            }
            else {
                return $"{timeSpan.Seconds}s";
            }
        }

        public static string GetTimeAgo(DateTime dateTime) {
            TimeSpan timeSpan = DateTime.Now - dateTime;

            if (timeSpan.TotalSeconds < 60) {
                return "Just now";
            }
            else if (timeSpan.TotalMinutes < 60) {
                return $"{(int)timeSpan.TotalMinutes} minutes ago";
            }
            else if (timeSpan.TotalHours < 24) {
                return $"{(int)timeSpan.TotalHours} hours ago";
            }
            else if (timeSpan.TotalDays < 7) {
                return $"{(int)timeSpan.TotalDays} days ago";
            }
            else {
                return dateTime.ToString("dd/MM/yyyy");
            }
        }

        public static bool IsValidEmailAddress(string email) {
            try {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch {
                return false;
            }
        }

        public static bool IsAdministrator() {
            try {
                using (Process process = new Process()) {
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.Arguments = "/C net session";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                    process.WaitForExit();
                    return process.ExitCode == 0;
                }
            }
            catch {
                return false;
            }
        }

        public static void CreateBackup(string sourcePath, string backupPath) {
            try {
                if (File.Exists(sourcePath)) {
                    File.Copy(sourcePath, backupPath, true);
                }
                else if (Directory.Exists(sourcePath)) {
                    CopyDirectory(sourcePath, backupPath);
                }
            }
            catch (Exception ex) {
                Logger.GetInstance().LogError($"Error creating backup: {ex.Message}", ex);
            }
        }

        private static void CopyDirectory(string sourceDir, string destDir) {
            if (!Directory.Exists(destDir)) {
                Directory.CreateDirectory(destDir);
            }

            foreach (string file in Directory.GetFiles(sourceDir)) {
                string destFile = Path.Combine(destDir, Path.GetFileName(file));
                File.Copy(file, destFile, true);
            }

            foreach (string dir in Directory.GetDirectories(sourceDir)) {
                string destDirPath = Path.Combine(destDir, Path.GetDirectoryName(dir));
                CopyDirectory(dir, destDirPath);
            }
        }

        public static string EncodeToBase64(string plainText) {
            try {
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                return Convert.ToBase64String(plainTextBytes);
            }
            catch (Exception ex) {
                Logger.GetInstance().LogError($"Error encoding to base64: {ex.Message}", ex);
                return string.Empty;
            }
        }

        public static string DecodeFromBase64(string base64EncodedData) {
            try {
                byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
                return Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch (Exception ex) {
                Logger.GetInstance().LogError($"Error decoding from base64: {ex.Message}", ex);
                return string.Empty;
            }
        }

        public static bool DirectoryExists(string path) {
            return Directory.Exists(path);
        }

        public static bool FileExists(string path) {
            return File.Exists(path);
        }

        public static void DeleteFile(string filePath) {
            try {
                if (File.Exists(filePath)) {
                    File.Delete(filePath);
                }
            }
            catch (Exception ex) {
                Logger.GetInstance().LogError($"Error deleting file: {ex.Message}", ex);
            }
        }

        public static void DeleteDirectory(string directoryPath) {
            try {
                if (Directory.Exists(directoryPath)) {
                    Directory.Delete(directoryPath, true);
                }
            }
            catch (Exception ex) {
                Logger.GetInstance().LogError($"Error deleting directory: {ex.Message}", ex);
            }
        }

        public static string GetFileHash(string filePath) {
            try {
                using (var sha256 = System.Security.Cryptography.SHA256.Create()) {
                    using (var fileStream = File.OpenRead(filePath)) {
                        byte[] hashValue = sha256.ComputeHash(fileStream);
                        return Convert.ToHexString(hashValue);
                    }
                }
            }
            catch (Exception ex) {
                Logger.GetInstance().LogError($"Error computing file hash: {ex.Message}", ex);
                return string.Empty;
            }
        }

        public static void RestartApplication() {
            try {
                Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                Environment.Exit(0);
            }
            catch (Exception ex) {
                Logger.GetInstance().LogError($"Error restarting application: {ex.Message}", ex);
            }
        }
    }
}
