using Microsoft.Extensions.Logging;

namespace DependencyInjection.Advanced.Demos.Api;

/// <summary>
/// A console logger that writes synchronously, so a log line written inside a request appears
/// where it happened rather than whenever a background queue gets flushed.
/// </summary>
internal sealed class InlineLoggerProvider : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName) => new InlineLogger(categoryName);

    public void Dispose()
    {
    }

    private sealed class InlineLogger : ILogger
    {
        private readonly string _category;

        public InlineLogger(string category) => _category = category.Split('.')[^1];

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

        public bool IsEnabled(LogLevel logLevel) => logLevel >= LogLevel.Information;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            Console.WriteLine($"  [log] {_category}: {formatter(state, exception)}");
        }
    }
}
