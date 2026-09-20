using DependencyInjection.Advanced.Demos.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection.Advanced.Demos;

/// <summary>
/// Lesson 1: Creating a custom scope.
/// </summary>
public static class CustomScopeDemo
{
    public static void Run()
    {
        RootProviderTreatsScopedAsSingleton();
        ScopesFromTheServiceProvider();
        ScopesFromTheServiceScopeFactory();
    }

    private static void RootProviderTreatsScopedAsSingleton()
    {
        var services = new ServiceCollection();
        services.AddScoped<ExampleService>();
        var serviceProvider = services.BuildServiceProvider();

        var first = serviceProvider.GetRequiredService<ExampleService>();
        var second = serviceProvider.GetRequiredService<ExampleService>();

        Console.WriteLine("Resolved straight from the root provider, with no scope in sight:");
        Console.WriteLine($"  1st: {first.Id}");
        Console.WriteLine($"  2nd: {second.Id}");
        Console.WriteLine($"  -> same instance: {ReferenceEquals(first, second)}  <- scoped behaves like a singleton here");
        Console.WriteLine();
    }

    private static void ScopesFromTheServiceProvider()
    {
        var services = new ServiceCollection();
        services.AddScoped<ExampleService>();
        var serviceProvider = services.BuildServiceProvider();

        Console.WriteLine("Two scopes created with IServiceProvider.CreateScope():");

        using (var serviceScope = serviceProvider.CreateScope())
        {
            var exampleService1 = serviceScope.ServiceProvider.GetRequiredService<ExampleService>();
            Console.WriteLine($"  scope 1: {exampleService1.Id}");
        }

        using (var serviceScope = serviceProvider.CreateScope())
        {
            var exampleService2 = serviceScope.ServiceProvider.GetRequiredService<ExampleService>();
            Console.WriteLine($"  scope 2: {exampleService2.Id}");
        }

        Console.WriteLine("  -> one instance per scope, disposed when the using block ends");
        Console.WriteLine();
    }

    private static void ScopesFromTheServiceScopeFactory()
    {
        var services = new ServiceCollection();
        services.AddScoped<ExampleService>();
        var serviceProvider = services.BuildServiceProvider();

        // IServiceScopeFactory is registered by the container itself, and creating scopes is all
        // it can do. Injecting it hands out far less power than the full IServiceProvider does.
        var serviceScopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

        Console.WriteLine("The same two scopes, created with IServiceScopeFactory instead:");

        using (var serviceScope = serviceScopeFactory.CreateScope())
        {
            var exampleService1 = serviceScope.ServiceProvider.GetRequiredService<ExampleService>();
            Console.WriteLine($"  scope 1: {exampleService1.Id}");
        }

        using (var serviceScope = serviceScopeFactory.CreateScope())
        {
            var exampleService2 = serviceScope.ServiceProvider.GetRequiredService<ExampleService>();
            Console.WriteLine($"  scope 2: {exampleService2.Id}");
        }

        Console.WriteLine("  -> same behaviour, narrower interface");
    }
}
