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
        Assert.IsFalse(csvRows.Any(line => line.StartsWith("Id", System.StringComparison.OrdinalIgnoreCase)));
        Assert.IsTrue(csvRows.All(line => line is string));
    }

    [TestMethod]
    public async Task People_FromCsv_ReturnsEnumerableOfPerson()
    {
        SampleDataAsync testSampleData = new();

        var people = await testSampleData.People.ToListAsync();

        Assert.IsNotNull(people);
        Assert.IsTrue(people.All(person => person is Person), "Not everyone in the list is a Person object.");
    }
}
