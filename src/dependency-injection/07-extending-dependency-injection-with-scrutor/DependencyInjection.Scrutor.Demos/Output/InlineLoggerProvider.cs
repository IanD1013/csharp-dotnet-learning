using Microsoft.Extensions.Logging;

namespace DependencyInjection.Scrutor.Demos.Output;

/// <summary>
/// Writes log lines straight to the console on the calling thread. The console logger that
/// ships with Microsoft.Extensions.Logging buffers on a background thread, which would let the
/// decorator's timing line land after the demo had moved on. Not part of the course code.
/// </summary>
public sealed class InlineLoggerProvider : ILoggerProvider
{
    /// <inheritdoc />
    public ILogger CreateLogger(string categoryName) => new InlineLogger();

    /// <inheritdoc />
    public void Dispose()
    {
    }

    private sealed class InlineLogger : ILogger
    {
        public IDisposable? BeginScope<TState>(TState state)
            where TState : notnull => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            ArgumentNullException.ThrowIfNull(formatter);
            Console.WriteLine($"  [log] {formatter(state, exception)}");
        }
    }
}
