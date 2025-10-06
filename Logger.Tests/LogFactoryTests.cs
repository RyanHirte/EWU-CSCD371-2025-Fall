using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logger.Tests;

[TestClass]
public class LogFactoryTests
{
    [TestMethod]
    public void CreateLogger_SetsLoggerClassName_Success()
    {
        // Arrange
        LogFactory logFactory = new();
        // string className = "TestClass";
        // string filePath = "Testpath";

        // Act
        // var logger = logFactory.CreateLogger(className, filePath);

        // Assert
        // Assert.AreEqual(className, logger.LoggerClassName);
    }
}
