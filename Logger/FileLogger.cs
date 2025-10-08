using System;
using System.IO;

namespace Logger
{
    public class FileLogger : BaseLogger
    {
        public string FilePath { get; set; }
        public FileLogger(string filePath)
        {
            FilePath = filePath;
        }
        public override void Log(LogLevel logLevel, string message)
        {
            DateTime now = DateTime.Now;
            string logEntry = $"[{now:yyyy-MM-dd HH:mm:ss}] [{LoggerClassName}] [{logLevel}] {message}\n";
            File.AppendAllText(FilePath, logEntry);
        }
    }
}
