using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Tests;

[TestClass]
public class SampleDataAsyncTests
{
    [TestMethod]
    public async Task AsyncCsvRows_ConstructedProperly_EnumeratesProperly()
    {
        SampleDataAsync testSampleData = new();

        var csvRows = await testSampleData.CsvRows.ToListAsync();

        Assert.IsNotNull(csvRows);
        Assert.IsFalse(csvRows.Any(line => line.StartsWith("Id")));
        Assert.IsTrue(csvRows.All(line => line is string));
    }
}
