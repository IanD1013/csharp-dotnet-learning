using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;

namespace SerilogConsoleApp;

public class IanSink : ILogEventSink
{
    private readonly IFormatProvider? _formatProvider;

    public IanSink(IFormatProvider? formatProvider)
    {
        _formatProvider = formatProvider;
    }

    public IanSink() : this(null)
    {
        
    }

    public void Emit(LogEvent logEvent)
    {
        var message = logEvent.RenderMessage(_formatProvider);
        Console.WriteLine($"{DateTime.UtcNow} - {message}");
    }
}

public static class IanSinkExtensions
{
    public static LoggerConfiguration IanSink(this LoggerSinkConfiguration sinkConfiguration,
        IFormatProvider? formatProvider = null) =>
        sinkConfiguration.Sink(new IanSink());
}