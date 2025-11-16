using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

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

    public IAsyncEnumerable<IPerson> People => throw new NotImplementedException();

    public IAsyncEnumerable<(string FirstName, string LastName)> FilterByEmailAddress(Predicate<string> filter)
    {
        throw new NotImplementedException();
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
