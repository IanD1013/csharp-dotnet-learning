using Microsoft.Extensions.DependencyInjection;
using ScrutorScanning.ConsoleApp.Services;

var services = new ServiceCollection();

services.Scan(selector =>
{
    selector
        .FromAssemblyOf<Program>()
        .AddClasses(f => f.InNamespaces("ScrutorScanning.ConsoleApp.Services"))
        .AsMatchingInterface()
        .WithSingletonLifetime()

        .AddClasses(f => f.InNamespaces("Services"))
        .AsMatchingInterface()
        .WithSingletonLifetime();
    // .FromAssemblyOf<ExampleAService>()
    //     .AddClasses()
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