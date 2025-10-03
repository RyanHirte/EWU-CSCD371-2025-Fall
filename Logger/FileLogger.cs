using System.Security.Cryptography.X509Certificates;

namespace Logger
{
    internal class FileLogger : BaseLogger
    {
        public string FilePath { get; set; }
        public FileLogger(string filePath)
        {
            FilePath = filePath;
        }
        public override void Log(LogLevel logLevel, string message)
        {
            // Implementation for logging to a file
        }
    }
}
