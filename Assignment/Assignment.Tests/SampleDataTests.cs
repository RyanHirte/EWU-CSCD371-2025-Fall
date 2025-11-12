using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

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

    [TestMethod]
    public void CsvRows_StreamsAnd_SkipsHeader()
    {
        // Arrange
        SampleData sampleData = new();
        // Act
        var csvRows = sampleData.CsvRows.ToArray();
        // Assert
        Assert.IsGreaterThan(0, csvRows.Length);
        Assert.DoesNotStartWith("Id,", csvRows[0]);
        Assert.IsTrue(csvRows.All(r => !string.IsNullOrWhiteSpace(r)));
    }

    [TestMethod]
    public void StatesFromCSV_AreUniqueAndSorted_UsingLINQChecks()
    {
        // Arrange
        SampleData sampleData = new();
        var states = sampleData.GetUniqueSortedListOfStatesGivenCsvRows().ToArray();
        // Act
        var nonDecreasing = states.Zip(states.Skip(1), (a, b) => string.Compare(a, b, true) <= 0);
        // Assert
        Assert.IsTrue(nonDecreasing.All(x => x));
        Assert.AreEqual(states.Length, states.Distinct(StringComparer.OrdinalIgnoreCase).Count());
    }
}
