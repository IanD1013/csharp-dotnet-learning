namespace DependencyInjection.Advanced.Demos.Services;

/// <summary>
/// Lesson 1. Carries nothing but an identity, so two resolutions can be compared.
/// </summary>
public sealed class ExampleService
{
    public Guid Id { get; } = Guid.NewGuid();
}
