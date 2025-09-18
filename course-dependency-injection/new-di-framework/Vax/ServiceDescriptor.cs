namespace Vax;

public class ServiceDescriptor
{
    public Type ServiceType { get; init; } = null!;
    public Type ImplementationType { get; set; }
    public ServiceLifetime Lifetime { get; set; }
}