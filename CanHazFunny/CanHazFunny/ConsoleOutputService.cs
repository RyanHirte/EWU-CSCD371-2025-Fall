using System;

namespace CanHazFunny;

public class ConsoleOutputService : InterfaceOutputService
{
    public void WriteJoke(string message)
    {
        Console.WriteLine(message);
    }
}