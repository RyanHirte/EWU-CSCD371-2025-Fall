using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Logger.Tests;

[TestClass]
public class FileLoggerTests
{
    [TestMethod]
    public void FileLogger_Initializes_Success()
    {
        // arrange
        string testFilePath = "testlog.txt";
        string LoggerClassName = "TestFileLogger";

        // act
        FileLogger fileLogger = new FileLogger(testFilePath) { LoggerClassName = LoggerClassName };

        // assert
        Assert.AreEqual(testFilePath, fileLogger.FilePath);
        Assert.AreEqual(LoggerClassName, fileLogger.LoggerClassName);
    }

    [TestMethod]
    public void Log_CreatesFileIfNotExisting_Success()
    {
        // arrange
        string testFilePath = "./testlog.txt";
        string LoggerClassName = "TestFileLogger";
        string testMessage = "This is a test log message.";
        if (File.Exists(testFilePath))
        {
            File.Delete(testFilePath);
        }
        FileLogger fileLogger = new FileLogger(testFilePath) { LoggerClassName = LoggerClassName };
        // act
        fileLogger.Log(LogLevel.Information, testMessage);
        // assert
        Assert.IsTrue(File.Exists(testFilePath), "File was not created by FileLogger");
        // cleanup
        if (File.Exists(testFilePath))
        {
            File.Delete(testFilePath);
        }
    }

    [TestMethod]
    public void Log_AppendsMessageToFile_Success()
    {
        // arrange
        string testFilePath = "./testlog.txt";
        string LoggerClassName = "TestFileLogger";
        string testMessage1 = "This is the first test log message.";
        string testMessage2 = "This is the second test log message.";
        if (File.Exists(testFilePath))
        {
            File.Delete(testFilePath);
        }
        FileLogger fileLogger = new FileLogger(testFilePath) { LoggerClassName = LoggerClassName };
        // act
        fileLogger.Log(LogLevel.Information, testMessage1);
        fileLogger.Log(LogLevel.Information, testMessage2);
        // assert
        string[] logContents = File.ReadAllLines(testFilePath);
        Assert.AreEqual(2, logContents.Length, "Log file does not contain expected number of entries");
        Assert.AreEqual(testMessage1, logContents[0], "First log message does not match");
        Assert.AreEqual(testMessage2, logContents[1], "Second log message does not match");
        // cleanup
        if (File.Exists(testFilePath))
        {
            File.Delete(testFilePath);
        }
    }
}
