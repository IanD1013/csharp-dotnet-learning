using Microsoft.Extensions.Logging;

namespace BasicConsoleApp;

public class IanLoggerProvider : ILoggerProvider
{
    public void Dispose()
    {
        
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new IanLogger();
    }
}

public class IanLogger : ILogger
{
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }
        
        Console.WriteLine($"[{logLevel}({eventId})]: {state}");
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel != LogLevel.None;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return NullScope.Instance;
    }
    
    internal sealed class NullScope : IDisposable
    {
        public static NullScope Instance { get; } = new();
        
        private NullScope() {}
        public void Dispose() {}
    }
}