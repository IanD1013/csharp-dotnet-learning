namespace DependencyInjection.Advanced.Demos.Handlers;

[AttributeUsage(AttributeTargets.Class)]
public sealed class CommandNameAttribute : Attribute
{
    public CommandNameAttribute(string commandName) => CommandName = commandName;

    public string CommandName { get; }
}
