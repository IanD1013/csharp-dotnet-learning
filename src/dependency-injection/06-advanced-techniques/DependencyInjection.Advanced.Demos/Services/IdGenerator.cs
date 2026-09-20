namespace DependencyInjection.Advanced.Demos.Services;

/// <summary>
/// Lesson 5. Registered as a singleton, so two different ids prove two different containers.
/// </summary>
public sealed class IdGenerator
{
    public Guid Id { get; } = Guid.NewGuid();
}
