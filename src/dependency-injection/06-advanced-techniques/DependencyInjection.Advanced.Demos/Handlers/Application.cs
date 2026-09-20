using DependencyInjection.Advanced.Demos.Output;

namespace DependencyInjection.Advanced.Demos.Handlers;

/// <summary>
/// The multi-function console app from lesson 3. It injects one orchestrator instead of every
/// handler it might ever dispatch to.
/// </summary>
public sealed class Application
{
    private readonly IConsoleWriter _consoleWriter;
    private readonly HandlerOrchestrator _handlerOrchestrator;

    public Application(IConsoleWriter consoleWriter, HandlerOrchestrator handlerOrchestrator)
    {
        _consoleWriter = consoleWriter;
        _handlerOrchestrator = handlerOrchestrator;
    }

    public async Task RunAsync(string[] args)
    {
        var command = args[0];

        var handler = _handlerOrchestrator.GetHandlerForCommandName(command);
        if (handler is null)
        {
            _consoleWriter.WriteLine($"No handler found for command name {command}");
            return;
        }

        await handler.HandleAsync();
    }
}
