using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace DependencyInjection.Scrutor.Demos.Logging;

/// <summary>
/// Lesson 3: the try-finally turned into an IDisposable. The constructor starts the clock and
/// Dispose stops it and logs, so a using statement is all the caller has to write.
/// </summary>
/// <typeparam name="T">The type the log line is attributed to.</typeparam>
public sealed class TimedLogOperation<T> : IDisposable
{
    private readonly ILoggerAdapter<T> _logger;
    private readonly LogLevel _logLevel;
    private readonly string _message;
    private readonly object?[] _args;
    private readonly Stopwatch _stopwatch;

    /// <summary>Starts timing.</summary>
    public TimedLogOperation(ILoggerAdapter<T> logger,
        LogLevel logLevel, string message, object?[] args)
    {
        _logger = logger;
        _logLevel = logLevel;
        _message = message;
        _args = args;
        _stopwatch = Stopwatch.StartNew();
    }

    /// <summary>Stops timing and logs the elapsed milliseconds.</summary>
    public void Dispose()
    {
        _stopwatch.Stop();
        _logger.Log(_logLevel, $"{_message} completed in {_stopwatch.ElapsedMilliseconds}ms", _args!);
    }
}
