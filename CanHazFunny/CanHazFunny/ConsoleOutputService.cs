using System;

namespace CanHazFunny;

public class ConsoleOutputService : IOutputService
{
    public void WriteJoke(string joke)
    {
        if (joke is null) throw new ArgumentNullException(nameof(joke));

        Console.WriteLine(joke);
    }
}