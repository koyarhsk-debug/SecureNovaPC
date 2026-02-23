using System;
using System.IO;

namespace SecureNovaPC.Utils {
    public class Logger {
        private static Logger instance;
        private string logFilePath;
        private readonly object lockObject = new object();

        private Logger() {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string logDirectory = Path.Combine(appDataPath, "SecureNovaPC", "Logs");
            
            if (!Directory.Exists(logDirectory)) {
                Directory.CreateDirectory(logDirectory);
            }

            logFilePath = Path.Combine(logDirectory, $"Log_{DateTime.Now:yyyy-MM-dd}.txt");
        }

        public static Logger GetInstance() {
            if (instance == null) {
                instance = new Logger();
            }
            return instance;
        }

        public void LogInfo(string message) {
            Log("INFO", message);
        }

        public void LogWarning(string message) {
            Log("WARNING", message);
        }

        public void LogError(string message) {
            Log("ERROR", message);
        }

        public void LogError(string message, Exception ex) {
            Log("ERROR", $"{message} - Exception: {ex.Message}\n{ex.StackTrace}");
        }

        public void LogDebug(string message) {
            Log("DEBUG", message);
        }

        private void Log(string level, string message) {
            lock (lockObject) {
                try {
                    string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";
                    
                    using (StreamWriter writer = new StreamWriter(logFilePath, true)) {
                        writer.WriteLine(logEntry);
                    }

                    Console.WriteLine(logEntry);
                }
                catch (Exception ex) {
                    Console.WriteLine($"Error writing to log: {ex.Message}");
                }
            }
        }

        public string ReadLogFile() {
            try {
                if (File.Exists(logFilePath)) {
                    return File.ReadAllText(logFilePath);
                }
                return "Log file not found.";
            }
            catch (Exception ex) {
                return $"Error reading log file: {ex.Message}";
            }
        }

        public void ClearLogFile() {
            try {
                if (File.Exists(logFilePath)) {
                    File.Delete(logFilePath);
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Error clearing log file: {ex.Message}");
            }
        }

        public string GetLogFilePath() {
            return logFilePath;
        }
    }
}
