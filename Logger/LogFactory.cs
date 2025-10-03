namespace Logger;

public class LogFactory
{
    public BaseLogger CreateLogger(string className, string filePath)
    {
        return new FileLogger(filePath)
        { LoggerClassName = className };
    }
}
