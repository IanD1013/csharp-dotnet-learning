using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection.Advanced.Demos.Handlers;

/// <summary>
/// Lesson 3. A deliberate, infrastructure-level service locator: it maps a runtime string to a
/// handler type, so the application never has to inject every handler it might need.
/// </summary>
public sealed class HandlerOrchestrator
{
    private readonly Dictionary<string, Type> _handlerTypes = new();
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public HandlerOrchestrator(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
        RegisterCommandHandler();
    }

    public IReadOnlyDictionary<string, Type> KnownCommands => _handlerTypes;

    public IHandler? GetHandlerForCommandName(string command)
    {
        var handlerType = _handlerTypes.GetValueOrDefault(command);

        if (handlerType is null)
        {
            return null;
        }

        using var serviceScope = _serviceScopeFactory.CreateScope();
        return (IHandler)serviceScope.ServiceProvider.GetRequiredService(handlerType);
    }

    private void RegisterCommandHandler()
    {
        var handlerTypes = HandlerExtensions.GetHandlerTypesForAssembly(typeof(IHandler).Assembly);

        foreach (var handlerType in handlerTypes)
        {
            var commandNameAttribute = handlerType.GetCustomAttribute<CommandNameAttribute>();
            if (commandNameAttribute is null)
            {
                continue;
            }

            var commandName = commandNameAttribute.CommandName;
            _handlerTypes[commandName] = handlerType;
        }
    }
}
