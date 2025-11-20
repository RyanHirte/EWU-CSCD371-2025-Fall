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
}
