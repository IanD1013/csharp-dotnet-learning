namespace Vax;

/// <summary>
/// One registration: what is asked for, what satisfies it, and for how long.
/// Exactly one of <see cref="Implementation"/>, <see cref="ImplementationFactory"/> or
/// <see cref="ImplementationType"/> is used when the provider builds the service.
/// </summary>
public class ServiceDescriptor
{
    public Type ServiceType { get; init; } = default!;

    public Type ImplementationType { get; set; } = default!;

    public object? Implementation { get; set; }

    public Func<ServiceProvider, object>? ImplementationFactory { get; set; }

    public ServiceLifetime Lifetime { get; set; }
}
