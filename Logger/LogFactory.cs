namespace Logger;

public class LogFactory
{
    private string? _configedFilePath; // ? for nullable, _ for private

    public BaseLogger? CreateLogger(string className)
    {
        if (_configuredFilePath == null) // return null if not configured
            return null;
        
        return new FileLogger(_configuredFilePath)
        {
            LoggerClassName = className
        };
    }

    public void ConfigureFileLogger(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("File path cannot be null or whitespace: ", nameof(path));

        _configuredFilePath = path;
    }
}
