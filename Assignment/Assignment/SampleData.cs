using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assignment;

/// <summary>
/// Concrete implementation of ISampleData that reads from People.csv
/// </summary>
/// <remarks>
/// Goals:
/// -Keep each operation pure and composable by returning IEnumerable where practical.
/// -Prefer LINQ operators per assignment instructions.
/// -Make sorting/uniqueness case-insensitive and deterministic.
/// -Centralize CSV parsing to a single helper to avoid duplication.
/// </remarks>
public class SampleData : ISampleData
{
    // 1.
    public IEnumerable<string> CsvRows => File.ReadAllLines("People.csv").Skip(1).Select(line => line.Trim());

    /// <summary>
    /// Splits a comma-delimited row into columns.
    /// This centralizes parsing to keep other methods focused on their logic.
    /// This dataset does not contain quoted commas, so a simple split is sufficient.
    /// </summary>
    private static string[] ParseColumns(string row) => row.Split(',', StringSplitOptions.TrimEntries);

    /// <summary>
    /// Returns an alphabetical, case-insensitive, unique list of state abbreviations
    /// derived from CsvRows.
    /// Uses Distinct with StringComparer.OrdinalIgnoreCase so that "CA" and "ca" are
    /// treated as the same key.
    /// </summary>
    public IEnumerable<string> GetUniqueSortedListOfStatesGivenCsvRows()
        => CsvRows.Select(ParseColumns)
        .Select(cols => cols[6])
        .Where(s => !string.IsNullOrWhiteSpace(s))
        .Distinct(StringComparer.OrdinalIgnoreCase)
        .OrderBy(s => s, StringComparer.OrdinalIgnoreCase)
        .ToArray();

    /// <summary>
    /// Returns a comma-separated string containing the unique, alphabetically
    /// sorted list of states derived from CsvRows.
    /// Composes directly on GetUniqueSortedListOfStatesGivenCsvRows to avoid
    /// duplicating logic, and uses string.Join(string, string[]) for simple,
    /// efficient joining.
    /// </summary>
    public string GetAggregateSortedListOfStatesUsingCsvRows()
        => string.Join(", ", GetUniqueSortedListOfStatesGivenCsvRows());

    // 4.
    public IEnumerable<IPerson> People => throw new NotImplementedException();

    // 5.
    public IEnumerable<(string FirstName, string LastName)> FilterByEmailAddress(
        Predicate<string> filter) => throw new NotImplementedException();

    // 6.
    public string GetAggregateListOfStatesGivenPeopleCollection(
        IEnumerable<IPerson> people) => throw new NotImplementedException();
}
