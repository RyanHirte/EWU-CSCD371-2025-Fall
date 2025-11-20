using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assignment;

public class SampleDataAsync : IAsyncSampleData
{
    public IAsyncEnumerable<string> CsvRows => GetCsvRows();

    private static async IAsyncEnumerable<string> GetCsvRows()
    {
        bool isFirst = true;
        await foreach (var line in File.ReadLinesAsync("People.csv"))
        {
            if (isFirst)
            {
                isFirst = false;
                continue;
            }
            yield return line.Trim();
        }
    }

    public IAsyncEnumerable<IPerson> People => GetPeople();

    private async IAsyncEnumerable<IPerson> GetPeople()
    {
        List<IPerson> people = [];
        await foreach (var row in CsvRows)
        {
            var cols = SampleDataCommon.ParseColumns(row);
            people.Add(SampleDataCommon.CreatePersonFromRow(cols));
        }
        var sorted = people
            .OrderBy(person => person.Address.State, StringComparer.OrdinalIgnoreCase)
            .ThenBy(person => person.Address.City, StringComparer.OrdinalIgnoreCase)
            .ThenBy(person => person.Address.Zip, StringComparer.OrdinalIgnoreCase);
        foreach (IPerson person in people)
        {
            yield return person;
        }

    }

    public async IAsyncEnumerable<(string FirstName, string LastName)> FilterByEmailAddress(Predicate<string> filter)
    {
        await foreach (Person person in People)
        {
            if (filter(person.EmailAddress))
            {
                yield return (person.FirstName, person.LastName);
            }
        }
    }

    public string GetAggregateListOfStatesGivenPeopleCollection(IAsyncEnumerable<IPerson> people)
    {
        List<string> states = [];

        var enumerator = people.GetAsyncEnumerator();
        try
        {
            while (enumerator.MoveNextAsync().AsTask().Result)
            {
                var person = enumerator.Current;
                states.Add(person.Address.State);
            }
            states = states.Distinct().ToList();
            states.Sort(StringComparer.OrdinalIgnoreCase);
        }
        finally
        {
            enumerator.DisposeAsync().AsTask().Wait();
        }

        return string.Join(", ", states);
    }

    public string GetAggregateSortedListOfStatesUsingCsvRows()
    {
        List<string> states = [];
        IAsyncEnumerable<string> rows = CsvRows;

        var enumerator = rows.GetAsyncEnumerator();
        try
        {
            while (enumerator.MoveNextAsync().AsTask().Result)
            {
                var row = enumerator.Current;
                var cols = SampleDataCommon.ParseColumns(row);
                states.Add(cols[6]);
            }
            states = states
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(s => s, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }
        finally
        {
            enumerator.DisposeAsync().AsTask().Wait();
        }

        return string.Join(", ", states);
    }

    public async IAsyncEnumerable<string> GetUniqueSortedListOfStatesGivenCsvRows()
    {
        List<string> states = [];

        await foreach (var row in CsvRows)
        {
            var cols = SampleDataCommon.ParseColumns(row);
            var state = cols[6];
            if (!string.IsNullOrWhiteSpace(state) && !states.Contains(state, StringComparer.OrdinalIgnoreCase))
            {
                states.Add(state);
            }
        }

        var sortedStates = states.OrderBy(s => s, StringComparer.OrdinalIgnoreCase);
        foreach (var state in sortedStates)
        {
            yield return state;
        }
    }
}
