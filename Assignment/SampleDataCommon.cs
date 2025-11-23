using System;

namespace Assignment;

public class SampleDataCommon
{
    /// <summary>
    /// Splits a comma-delimited row into columns.
    /// This centralizes parsing to keep other methods focused on their logic.
    /// This dataset does not contain quoted commas, so a simple split is sufficient.
    /// </summary>
    public static string[] ParseColumns(string row) => row.Split(',', StringSplitOptions.TrimEntries);

    /// <summary>
    /// Creates a Person object from a string array of columns parsed from a CSV row.
    /// </summary>
    public static Person CreatePersonFromRow(string[] cols)
    {
        var addr = new Address(
                streetAddress: cols[4],
                city: cols[5],
                state: cols[6],
                zip: cols[7]
        );
        return new Person(
                firstName: cols[1],
                lastName: cols[2],
                address: addr,
                emailAddress: cols[3]
        );
    }
}
