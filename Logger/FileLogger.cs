using System;
using System.IO;

namespace Logger
{
    public class FileLogger : BaseLogger
    {
        public string FilePath { get; }

        public FileLogger(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException($"File path cannot be null or whitespace: '{filePath}'", nameof(filePath));

            FilePath = filePath;
        }
        public override void Log(LogLevel logLevel, string message)
        {
            DateTime now = DateTime.Now;
            string logEntry = $"[{now:yyyy-MM-dd HH:mm:ss}] [{LoggerClassName}] [{logLevel}] {message}";
            // If the file does not exist, create it.
            if (!File.Exists(FilePath))
            {
                using (StreamWriter sw = File.CreateText(FilePath))
                {
                    sw.WriteLine(logEntry);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(FilePath))
                {
                    sw.WriteLine(logEntry);
                }
            }
        }
    }
}
