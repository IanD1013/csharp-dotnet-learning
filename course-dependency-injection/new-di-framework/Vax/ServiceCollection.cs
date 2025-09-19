namespace Vax;

public class ServiceCollection : List<ServiceDescriptor>
{
    /*
     * 5. Use provider
     * services.AddSingleton<ConsoleWriter>(); 
     * services.AddSingleton(provider => new IdGenerator(provider.GetService<ConsoleWriter>()))
     */
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
    
    /*
     * 4. Add an already-made implementation
     * services.AddSingleton(new ConsoleWriter())
     */
    public ServiceCollection AddSingleton(object implementation)
    {
        var serviceType = implementation.GetType();
        var serviceDescriptor = new ServiceDescriptor
        {
            ServiceType = serviceType,
            ImplementationType = serviceType,
            Implementation = implementation,
            Lifetime = ServiceLifetime.Singleton // singleton only in this case because it's an instance already
        };
        Add(serviceDescriptor);
        return this;
    }
    
    /*
     * 3. User wants to create their own service descriptor
     */
    public ServiceCollection AddService(ServiceDescriptor descriptor)
    {
        Add(descriptor);
        return this;
    }
    
    /* 2. Register a single thing on its own without depending on the interface:
     * services.AddTransient<ConsoleWriter>()
     */
    public ServiceCollection AddSingleton<TService>()  
        where TService : class
    {
        var serviceDescriptor = AddServiceDescriptorWithLifetime<TService, TService>(ServiceLifetime.Singleton);
        Add(serviceDescriptor);
        return this;
    }        
    
    public ServiceCollection AddTransient<TService>()  
        where TService : class
    {
        var serviceDescriptor = AddServiceDescriptorWithLifetime<TService, TService>(ServiceLifetime.Transient);
        Add(serviceDescriptor);
        return this;
    }    
    
    /*
     * 1. Add constraint so that services.AddSingleton<IConsoleWriter, IdGenerator>() will error out
     */
    public ServiceCollection AddSingleton<TService, TImplementation>()  
        where TService : class
        where TImplementation : class, TService
    {
        var serviceDescriptor = AddServiceDescriptorWithLifetime<TService, TImplementation>(ServiceLifetime.Singleton);
        Add(serviceDescriptor);
        return this;
    }    
    
    public ServiceCollection AddTransient<TService, TImplementation>()
        where TService : class
        where TImplementation : class, TService
    {
        var serviceDescriptor = AddServiceDescriptorWithLifetime<TService, TImplementation>(ServiceLifetime.Transient);
        Add(serviceDescriptor);
        return this;
    }
    
    public ServiceProvider BuildServiceProvider()
    {
        return new ServiceProvider(this);
    }
    
    private static ServiceDescriptor AddServiceDescriptorWithLifetime<TService, TImplementation>(ServiceLifetime lifetime)
    {
        var serviceDescriptor = new ServiceDescriptor
        {
            ServiceType = typeof(TService),
            ImplementationType = typeof(TImplementation),
            Lifetime = lifetime
        };
        return serviceDescriptor;
    }
}