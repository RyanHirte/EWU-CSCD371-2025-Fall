using System;

namespace CanHazFunny;

public class ConsoleOutputService : InterfaceOutputService
{
    public void Write(string message)
    {
        Console.WriteLine(message);
    }
}