using System;

namespace Logger;

public static class BaseLoggerMixins
{
    public static void Error(this BaseLogger logger, string message)
    {
        ArgumentNullException.ThrowIfNull(logger);

        logger.Log(LogLevel.Error, message);
    }
    public static void Warning(this BaseLogger logger, string message)
    {
        ArgumentNullException.ThrowIfNull(logger);

        logger.Log(LogLevel.Warning, message);
    }
    public static void Information(this BaseLogger logger, string message)
    {
        ArgumentNullException.ThrowIfNull(logger);

        logger.Log(LogLevel.Information, message);
    }
    public static void Debug(this BaseLogger logger, string message)
    {
        ArgumentNullException.ThrowIfNull(logger);

        logger.Log(LogLevel.Debug, message);
    }
}
