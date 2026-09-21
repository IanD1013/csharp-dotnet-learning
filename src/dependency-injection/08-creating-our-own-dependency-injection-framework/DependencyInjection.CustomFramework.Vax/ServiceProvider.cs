namespace Vax;

/// <summary>
/// The engine. Registrations are flattened into two dictionaries once, at build time:
/// singletons as <see cref="Lazy{T}"/> so construction is deferred until the first
/// request, transients as a factory that runs on every request.
/// </summary>
public class ServiceProvider
{
    private readonly Dictionary<Type, Func<object>> _transientTypes = [];
    private readonly Dictionary<Type, Lazy<object>> _singletonTypes = [];

    internal ServiceProvider(ServiceCollection serviceCollection)
    {
        GenerateServices(serviceCollection);
    }

    public T? GetService<T>()
    {
        return (T?)GetService(typeof(T));
    }

    public object? GetService(Type serviceType)
    {
        var singleton = _singletonTypes.GetValueOrDefault(serviceType);

        if (singleton is not null)
        {
            return singleton.Value;
        }

        var transient = _transientTypes.GetValueOrDefault(serviceType);

        return transient?.Invoke();
    }

    public T GetRequiredService<T>()
    {
        return (T)GetRequiredService(typeof(T));
    }

    public object GetRequiredService(Type serviceType)
    {
        return GetService(serviceType)
               ?? throw new InvalidOperationException(
                   $"No service for type '{serviceType}' has been registered.");
    }

    private void GenerateServices(ServiceCollection serviceCollection)
    {
        foreach (var serviceDescriptor in serviceCollection)
        {
            switch (serviceDescriptor.Lifetime)
            {
                case ServiceLifetime.Singleton:
                    if (serviceDescriptor.Implementation is not null)
                    {
                        _singletonTypes[serviceDescriptor.ServiceType] =
                            new Lazy<object>(serviceDescriptor.Implementation);
                        continue;
                    }

                    if (serviceDescriptor.ImplementationFactory is not null)
                    {
                        _singletonTypes[serviceDescriptor.ServiceType] =
                            new Lazy<object>(() =>
                                serviceDescriptor.ImplementationFactory(this));
                        continue;
                    }

                    _singletonTypes[serviceDescriptor.ServiceType] =
                        new Lazy<object>(() =>
                            Activator.CreateInstance(serviceDescriptor.ImplementationType,
                                GetConstructorParameters(serviceDescriptor))!);
                    continue;
                case ServiceLifetime.Transient:
                    if (serviceDescriptor.ImplementationFactory is not null)
                    {
                        _transientTypes[serviceDescriptor.ServiceType] =
                            () => serviceDescriptor.ImplementationFactory(this);
                        continue;
                    }

                    _transientTypes[serviceDescriptor.ServiceType] =
                        () => Activator.CreateInstance(serviceDescriptor.ImplementationType,
                            GetConstructorParameters(serviceDescriptor))!;
                    continue;
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(serviceCollection),
                        serviceDescriptor.Lifetime,
                        "Unknown service lifetime.");
            }
        }
    }

    private object?[] GetConstructorParameters(ServiceDescriptor descriptor)
    {
        var constructorInfo = descriptor.ImplementationType.GetConstructors().First();
        var parameters = constructorInfo.GetParameters()
            .Select(x => GetService(x.ParameterType)).ToArray();

        return parameters;
    }
}
