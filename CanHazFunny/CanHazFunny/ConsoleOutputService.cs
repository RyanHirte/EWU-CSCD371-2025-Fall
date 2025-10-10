using System;

namespace CanHazFunny;

public class ConsoleOutputService : IOutputService
{
    public void WriteJoke(string message)
    {
        Console.WriteLine(message);
    }
}