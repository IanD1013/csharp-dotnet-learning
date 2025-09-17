using Microsoft.Extensions.DependencyInjection;
using ScrutorScanning.ConsoleApp.Attributes;
using ScrutorScanning.ConsoleApp.Services;

var services = new ServiceCollection();

services.Scan(selector =>
{
    selector
        .FromAssemblyOf<Program>()
        .AddClasses(f => f.WithAttribute<SingletonAttribute>())
        .AsMatchingInterface()
        .WithSingletonLifetime()
        
        .AddClasses(f => f.WithAttribute<TransientAttribute>())
        .AsMatchingInterface()
        .WithScopedLifetime()
        
        .AddClasses(f => f.WithAttribute<ScopedAttribute>())
        .AsMatchingInterface()
        .WithTransientLifetime();

});

PrintRegisteredServices(services);

var serviceProvider = services.BuildServiceProvider();

void PrintRegisteredServices(IServiceCollection serviceCollection)
{
    foreach (var service in serviceCollection)
    {
        Console.WriteLine($"{service.ServiceType.Name} -> {service.ImplementationType?.Name} as {service.Lifetime.ToString()}");
    }
}