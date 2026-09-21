using Microsoft.Extensions.Logging;

namespace DependencyInjection.Scrutor.Demos.Logging;

/// <inheritdoc cref="ILoggerAdapter{TType}" />
public sealed class LoggerAdapter<TType> : ILoggerAdapter<TType>
{
    private readonly ILogger<LoggerAdapter<TType>> _logger;

    /// <summary>Wraps the real logger.</summary>
    public LoggerAdapter(ILogger<LoggerAdapter<TType>> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public void Log(LogLevel logLevel, string template, params object[] args)
    {
        // CA2254 wants a constant template. An adapter cannot have one: the template is the
        // caller's, and TimedLogOperation appends the elapsed time to it before it arrives here.
        // That indirection is exactly what lesson 3 builds.
#pragma warning disable CA2254
        _logger.Log(logLevel, template, args);
#pragma warning restore CA2254
    }

    /// <inheritdoc />
    public void LogInformation(string template, params object[] args)
    {
        Log(LogLevel.Information, template, args);
    }

    /// <inheritdoc />
    public IDisposable TimedOperation(string template, params object[] args)
    {
        return new TimedLogOperation<TType>(this, LogLevel.Information, template, args);
    }
}
