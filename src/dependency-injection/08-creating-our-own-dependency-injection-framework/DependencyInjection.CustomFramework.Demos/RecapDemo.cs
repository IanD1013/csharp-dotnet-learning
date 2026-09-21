using DependencyInjection.CustomFramework.Demos.Services;

using Vax;

namespace DependencyInjection.CustomFramework.Demos;

/// <summary>
/// Lesson 5 - Section recap. The program the course ends on: an instance registration
/// and a factory that resolves it, both resolved through the finished Vax container.
/// </summary>
public static class RecapDemo
{
    public static void Run()
    {
        var services = new ServiceCollection();

        // services.AddSingleton<IConsoleWriter, ConsoleWriter>();
        // services.AddSingleton<IIdGenerator, IdGenerator>();

        // services.AddSingleton<ConsoleWriter>();
        services.AddSingleton(new ConsoleWriter());

        services.AddSingleton(
            provider => new IdGenerator(provider.GetService<ConsoleWriter>()!));

        var serviceProvider = services.BuildServiceProvider();

        var service1 = serviceProvider.GetService<IdGenerator>();
        var service2 = serviceProvider.GetService<IdGenerator>();

        service1!.PrintId();

        Console.WriteLine($"  ReferenceEquals(service1, service2): {ReferenceEquals(service1, service2)}");
        Console.WriteLine("  -> registration by instance, registration by factory, nested resolution and");
        Console.WriteLine("     singleton lifetime, on a container with zero external dependencies");
    }
}
