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

    private static string[] ParseColumns(string row) => row.Split(',', StringSplitOptions.TrimEntries);

    public IAsyncEnumerable<IPerson> People => GetPeople();

    private async IAsyncEnumerable<IPerson> GetPeople()
    {
        List<IPerson> people = [];
        await foreach (var row in CsvRows)
        {
            var cols = ParseColumns(row);
            var addr = new Address(
                streetAddress: cols[4],
                city: cols[5],
                state: cols[6],
                zip: cols[7]
            );

            people.Add((IPerson)new Person(
                firstName: cols[1],
                lastName: cols[2],
                address: addr,
                emailAddress: cols[3]
                ));
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
        throw new NotImplementedException();
    }

    public string GetAggregateSortedListOfStatesUsingCsvRows()
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<string> GetUniqueSortedListOfStatesGivenCsvRows()
    {
        throw new NotImplementedException();
    }
}
