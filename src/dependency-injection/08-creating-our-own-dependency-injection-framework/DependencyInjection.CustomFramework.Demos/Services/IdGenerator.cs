namespace DependencyInjection.CustomFramework.Demos.Services;

/// <summary>
/// Takes an <see cref="IConsoleWriter"/> through its constructor, which is what forces
/// the provider to resolve a dependency of a dependency. The id is created once per
/// instance, so it doubles as the lifetime probe: same id means same instance.
/// </summary>
public class IdGenerator : IIdGenerator
{
    private readonly IConsoleWriter _consoleWriter;

    public IdGenerator(IConsoleWriter consoleWriter)
    {
        _consoleWriter = consoleWriter;
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }

    public void PrintId()
    {
        _consoleWriter.WriteLine($"  id: {Id}");
    }
}
