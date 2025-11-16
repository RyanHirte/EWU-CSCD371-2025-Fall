using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
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

    [TestMethod]
    public async Task FilterByEmailAddress_PeopleFromCsv_ReturnsFilteredTuples()
    {
        SampleDataAsync testSampleData = new();

        var expected = testSampleData.People
            .Where(person => person.EmailAddress.EndsWith(".edu", System.StringComparison.OrdinalIgnoreCase))
            .Select(person => (person.FirstName, person.LastName))
            .ToListAsync();

        var actual = await testSampleData.FilterByEmailAddress(email => email.EndsWith(".edu", System.StringComparison.OrdinalIgnoreCase)).ToListAsync();

        Assert.IsNotNull(actual);
        CollectionAssert.AreEqual(expected.Result, actual);
    }
}
