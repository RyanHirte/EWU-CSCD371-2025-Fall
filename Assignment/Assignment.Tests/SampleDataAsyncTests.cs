using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
        CollectionAssert.AreEqual(await expected, actual);
    }

    [TestMethod]
    public async Task GetAggregateListOfStatesGivenPeopleCollection_PeopleFromCsv_ReturnsCorrectAggregateString()
    {
        SampleDataAsync testSampleData = new();
        var people = testSampleData.People;
        string aggregateStates = testSampleData.GetAggregateListOfStatesGivenPeopleCollection(people);
        var expectedStates = await people
            .Select(person => person.Address.State)
            .Where(state => !string.IsNullOrWhiteSpace(state))
            .Distinct(System.StringComparer.OrdinalIgnoreCase)
            .OrderBy(state => state, System.StringComparer.OrdinalIgnoreCase)
            .ToListAsync();
        var expectedAggregate = string.Join(", ", expectedStates);
        Assert.AreEqual<string>(expectedAggregate, aggregateStates);
    }

    [TestMethod]
    public async Task GetAggregateSortedListOfStatesUsingCsvRows_UsingCsvRows_ReturnsCorrectAggregateString()
    {
        SampleDataAsync testSampleData = new();
        string aggregateStates = testSampleData.GetAggregateSortedListOfStatesUsingCsvRows();
        var expectedStates = await testSampleData.CsvRows
            .Select(row => row.Split(',', StringSplitOptions.TrimEntries)[6])
            .Where(state => !string.IsNullOrWhiteSpace(state))
            .Distinct(System.StringComparer.OrdinalIgnoreCase)
            .OrderBy(state => state, System.StringComparer.OrdinalIgnoreCase)
            .ToListAsync();
        var expectedAggregate = string.Join(", ", expectedStates);
        Assert.AreEqual<string>(expectedAggregate, aggregateStates);
    }

    [TestMethod]
    public async Task GetUniqueSortedListOfStatesGivenCsvRows_UsingCsvRows_ReturnsCorrectStateList()
    {
        SampleDataAsync testSampleData = new();
        var stateList = testSampleData.GetUniqueSortedListOfStatesGivenCsvRows();
        var expectedStates = await testSampleData.CsvRows
            .Select(row => row.Split(',', StringSplitOptions.TrimEntries)[6])
            .Where(state => !string.IsNullOrWhiteSpace(state))
            .Distinct(System.StringComparer.OrdinalIgnoreCase)
            .OrderBy(state => state, System.StringComparer.OrdinalIgnoreCase)
            .ToListAsync();
        CollectionAssert.AreEqual(expectedStates, await stateList.ToListAsync());
    }

}
