// Logger.cs
using System;
using System.IO;

namespace I_fucking_hate_this_class.Handlers
{
    /// <summary>
    /// Custom logging para os meus projetos de PSI
    /// </summary>
    public static class Logger
    {
    
        private static readonly string logFilePath = @"C:\Users\jbmsi\Desktop\Projeto de PSi\I fucking hate this class\Logs\Logs.md";
        private static readonly object lockObj = new object();

        public static void Debug(string message) => LogWithLevel("DEBUG", message);
        public static void Info(string message) => LogWithLevel("INFO", message);
        public static void Warn(string message) => LogWithLevel("WARN", message);
        public static void Error(string message) => LogWithLevel("ERROR", message);
        public static void Fatal(string message) => LogWithLevel("FATAL", message);

        private static void LogWithLevel(string level, string message)
        {
            string timestampedMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";

            lock (lockObj)
            {
                try
                {
                    File.AppendAllText(logFilePath, timestampedMessage + Environment.NewLine);
                }
                catch (Exception ex)
                {
                   
                    Console.WriteLine($"[Logger Error] Failed to write log file: {ex.Message}");
                }

               
                Console.WriteLine(timestampedMessage);
            }
        }
    }
}