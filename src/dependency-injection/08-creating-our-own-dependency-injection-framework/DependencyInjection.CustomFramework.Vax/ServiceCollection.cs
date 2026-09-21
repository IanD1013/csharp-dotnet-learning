namespace Vax;

/// <summary>
/// The registry. A list of <see cref="ServiceDescriptor"/> with the Microsoft-shaped
/// registration methods on top of it.
/// </summary>
public class ServiceCollection : List<ServiceDescriptor>
{
    public ServiceCollection AddService(ServiceDescriptor descriptor)
    {
        Add(descriptor);
        return this;
    }

    public ServiceCollection AddSingleton<TService, TImplementation>()
        where TService : class
        where TImplementation : class, TService
    {
        var serviceDescriptor =
            CreateServiceDescriptorWithLifetime<TService, TImplementation>(ServiceLifetime.Singleton);
        Add(serviceDescriptor);
        return this;
    }

    public ServiceCollection AddTransient<TService, TImplementation>()
        where TService : class
        where TImplementation : class, TService
    {
        var serviceDescriptor =
            CreateServiceDescriptorWithLifetime<TService, TImplementation>(ServiceLifetime.Transient);
        Add(serviceDescriptor);
        return this;
    }

    public ServiceCollection AddSingleton<TService>()
        where TService : class
    {
        var serviceDescriptor =
            CreateServiceDescriptorWithLifetime<TService, TService>(ServiceLifetime.Singleton);
        Add(serviceDescriptor);
        return this;
    }

    public ServiceCollection AddTransient<TService>()
        where TService : class
    {
        var serviceDescriptor =
            CreateServiceDescriptorWithLifetime<TService, TService>(ServiceLifetime.Transient);
        Add(serviceDescriptor);
        return this;
    }

    public ServiceCollection AddSingleton(object implementation)
    {
        var serviceType = implementation.GetType();
        var serviceDescriptor = new ServiceDescriptor
        {
            ServiceType = serviceType,
            ImplementationType = serviceType,
            Implementation = implementation,
            Lifetime = ServiceLifetime.Singleton
        };

        Add(serviceDescriptor);
        return this;
    }

    public ServiceCollection AddSingleton<TService>(Func<ServiceProvider, TService> factory)
        where TService : class
    {
        var serviceDescriptor = new ServiceDescriptor
        {
            ServiceType = typeof(TService),
            ImplementationType = typeof(TService),
            ImplementationFactory = factory,
            Lifetime = ServiceLifetime.Singleton
        };

        Add(serviceDescriptor);
        return this;
    }

    public ServiceCollection AddTransient<TService>(Func<ServiceProvider, TService> factory)
        where TService : class
    {
        var serviceDescriptor = new ServiceDescriptor
        {
            ServiceType = typeof(TService),
            ImplementationType = typeof(TService),
            ImplementationFactory = factory,
            Lifetime = ServiceLifetime.Transient
        };

        Add(serviceDescriptor);
        return this;
    }

    public ServiceProvider BuildServiceProvider()
    {
        return new ServiceProvider(this);
    }

    private static ServiceDescriptor CreateServiceDescriptorWithLifetime<TService, TImplementation>(
        ServiceLifetime lifetime)
    {
        return new ServiceDescriptor
        {
            ServiceType = typeof(TService),
            ImplementationType = typeof(TImplementation),
            Lifetime = lifetime
        };
    }
}
