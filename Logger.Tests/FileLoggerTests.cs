using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.IO;

namespace Logger.Tests;


[TestClass]
public class FileLoggerTests
{
    private const string TestFilePath = "./testlog.txt";
    private const string LoggerClassName = "TestFileLogger";

    private void CleanUpFile()
    {
        if (File.Exists(TestFilePath))
        {
            File.Delete(TestFilePath);
        }
    }

    [TestInitialize]
    public void TestInitialize()
    {
        CleanUpFile();
    }

    [TestCleanup]
    public void TestCleanup()
    {
        CleanUpFile();
    }

    private static FileLogger CreateFileLogger()
    {
        return new FileLogger(TestFilePath) { LoggerClassName = LoggerClassName };
    }

    [TestMethod]
    public void FileLogger_Initializes_Success()
    {
        // act
        FileLogger fileLogger = CreateFileLogger();
        // assert
        Assert.AreEqual(TestFilePath, fileLogger.FilePath);
        Assert.AreEqual(LoggerClassName, fileLogger.LoggerClassName);
    }

    [TestMethod]
    public void Log_CreatesFileIfNotExisting_Success()
    {
        // arrange
        string testMessage = "This is a test log message.";
        FileLogger fileLogger = CreateFileLogger();
        // act
        fileLogger.Log(LogLevel.Information, testMessage);
        // assert
        Assert.IsTrue(File.Exists(TestFilePath), "File was not created by FileLogger");
    }

    [TestMethod]
    public void Log_AppendsMessageToFile_Success()
    {
        // arrange
        DateTime now = DateTime.Now;
        string testMessage1 = "This is the first test log message.";
        string testMessage2 = "This is the second test log message.";
        string expectedLogMessage1 = $"[{now:yyyy-MM-dd HH:mm:ss}] [{LoggerClassName}] [Information] {testMessage1}";
        string expectedLogMessage2 = $"[{now:yyyy-MM-dd HH:mm:ss}] [{LoggerClassName}] [Information] {testMessage2}";
        FileLogger fileLogger = CreateFileLogger();
        // act
        fileLogger.Log(LogLevel.Information, testMessage1);
        fileLogger.Log(LogLevel.Information, testMessage2);
        // assert
        string[] logContents = File.ReadAllLines(TestFilePath);
        Assert.AreEqual(2, logContents.Length, "Log file does not contain expected number of entries");
        Assert.AreEqual(expectedLogMessage1, logContents[0], "First log message does not match");
        Assert.AreEqual(expectedLogMessage2, logContents[1], "Second log message does not match");
    }

    [TestMethod]
    public void Log_MessageIsCorrect_Success()
    { 
        // arrange
        DateTime now = DateTime.Now;
        string testMessage = "This is a test log message.";
        string expectedLogMessage = $"[{now:yyyy-MM-dd HH:mm:ss}] [{LoggerClassName}] [Information] {testMessage}";
        FileLogger fileLogger = CreateFileLogger();
        // act
        fileLogger.Log(LogLevel.Information, testMessage);
        // assert
        string[] logContents = File.ReadAllLines(TestFilePath);
        Assert.AreEqual(expectedLogMessage, logContents[0]);
    }
}
