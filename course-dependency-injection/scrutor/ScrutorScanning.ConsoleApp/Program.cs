using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using ScrutorScanning.ConsoleApp.Attributes;
using ScrutorScanning.ConsoleApp.Services;

var services = new ServiceCollection();

services.Scan(selector =>
{
    selector
        .FromAssemblyOf<Program>()
        .AddClasses(f => f.WithAttribute<SingletonAttribute>())
        .AsImplementedInterfaces()
        .WithSingletonLifetime()
        
        .AddClasses(f => f.WithAttribute<TransientAttribute>())
        .UsingRegistrationStrategy(RegistrationStrategy.Append)
        .AsImplementedInterfaces()
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