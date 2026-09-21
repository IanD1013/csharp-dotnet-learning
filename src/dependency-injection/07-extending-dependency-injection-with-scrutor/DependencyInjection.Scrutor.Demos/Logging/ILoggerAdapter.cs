using Microsoft.Extensions.Logging;

namespace DependencyInjection.Scrutor.Demos.Logging;

/// <summary>Lesson 3: the logging seam the course wraps ILogger in, so it stays fakeable in tests.</summary>
/// <typeparam name="TType">The type the log lines are attributed to.</typeparam>
public interface ILoggerAdapter<TType>
{
    /// <summary>Logs at the given level.</summary>
    void Log(LogLevel logLevel, string template, params object[] args);

    /// <summary>Logs at information level.</summary>
    void LogInformation(string template, params object[] args);

    /// <summary>Starts a timed operation that logs its duration when disposed.</summary>
    IDisposable TimedOperation(string template, params object[] args);
}
