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
            // If the file does not exist, create it.
            if (!File.Exists(FilePath))
            {
                using (StreamWriter sw = File.CreateText(FilePath))
                {
                    sw.WriteLine(message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(FilePath))
                {
                    sw.WriteLine(message);
                }
            }
        }
    }
}
