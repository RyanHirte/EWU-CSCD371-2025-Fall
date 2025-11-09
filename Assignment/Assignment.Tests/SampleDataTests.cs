using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assignment.Tests;

[TestClass]
public class SampleDataTests
{
    [TestMethod]
    public void CsvRows_ConstructedProperly_EnumeratesProperly()
    {
        // Arrange
        SampleData sampleData = new();
        // Act
        var csvRows = sampleData.CsvRows;
        // Assert
        Assert.IsNotNull(csvRows);
        foreach (string row in csvRows)
        {
            Assert.IsFalse(string.IsNullOrWhiteSpace(row));
            Assert.IsInstanceOfType<string>(row);
        }
    }
}
