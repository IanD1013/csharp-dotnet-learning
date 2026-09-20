using DependencyInjection.Advanced.Demos.Output;
using DependencyInjection.Advanced.Demos.Time;

namespace DependencyInjection.Advanced.Demos.Handlers;

[CommandName("time")]
public sealed class GetCurrentTimeHandler : IHandler
{
    private readonly IConsoleWriter _consoleWriter;
    private readonly IDateTimeProvider _dateTimeProvider;

    public GetCurrentTimeHandler(IConsoleWriter consoleWriter,
        IDateTimeProvider dateTimeProvider)
    {
        _consoleWriter = consoleWriter;
        _dateTimeProvider = dateTimeProvider;
    }

    public Task HandleAsync()
    {
        var timeNow = _dateTimeProvider.DateTimeNow;
        _consoleWriter.WriteLine($"The current time is {timeNow:O}");
        return Task.CompletedTask;
    }
}
