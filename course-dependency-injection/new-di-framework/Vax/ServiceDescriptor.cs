namespace Vax;

public class ServiceDescriptor
{
    public Type ServiceType { get; init; } = null!;
    public Type ImplementationType { get; set; }
    public object? Implementation { get; set; }
    public Func<ServiceProvider, object>? ImplementationFactory { get; set; }
    public ServiceLifetime Lifetime { get; set; }
}