using DependencyInjection.CustomFramework.Demos.Services;

using Vax;

namespace DependencyInjection.CustomFramework.Demos;

/// <summary>
/// Lesson 3 - The implementation. The descriptors, the two dictionaries, recursive
/// constructor resolution, and the Lazy that makes registration order irrelevant.
/// </summary>
public static class ImplementationDemo
{
    public static void Run()
    {
        Console.WriteLine("What a registration actually is:");
        var services = new ServiceCollection();

        services.AddSingleton<IConsoleWriter, ConsoleWriter>();
        services.AddSingleton<IIdGenerator, IdGenerator>();

        foreach (var descriptor in services)
        {
            Console.WriteLine(
                $"  {descriptor.ServiceType.Name} -> {descriptor.ImplementationType.Name} as {descriptor.Lifetime}");
        }

        Console.WriteLine($"  -> ServiceCollection is a List<ServiceDescriptor>, so Count is {services.Count}");

        Console.WriteLine();
        Console.WriteLine("Resolving IIdGenerator twice, registered as Singleton:");
        var serviceProvider = services.BuildServiceProvider();

        var service1 = serviceProvider.GetService<IIdGenerator>();
        var service2 = serviceProvider.GetService<IIdGenerator>();

        service1!.PrintId();
        service2!.PrintId();
        Console.WriteLine($"  ReferenceEquals: {ReferenceEquals(service1, service2)}");
        Console.WriteLine("  -> IdGenerator never asked for an IConsoleWriter; GetConstructorParameters");
        Console.WriteLine("     read its constructor and resolved one out of the same container");

        Console.WriteLine();
        Console.WriteLine("The same two services as Transient:");
        var transientProvider = new ServiceCollection()
            .AddTransient<IConsoleWriter, ConsoleWriter>()
            .AddTransient<IIdGenerator, IdGenerator>()
            .BuildServiceProvider();

        transientProvider.GetService<IIdGenerator>()!.PrintId();
        transientProvider.GetService<IIdGenerator>()!.PrintId();
        Console.WriteLine("  -> two ids: the transient dictionary holds a factory, and it runs per request");

        Console.WriteLine();
        Console.WriteLine("Registering the dependency after the thing that needs it:");
        var outOfOrder = new ServiceCollection();
        outOfOrder.AddSingleton<IIdGenerator, IdGenerator>();
        outOfOrder.AddSingleton<IConsoleWriter, ConsoleWriter>();

        outOfOrder.BuildServiceProvider().GetService<IIdGenerator>()!.PrintId();
        Console.WriteLine("  -> works, because the singleton is a Lazy<object>: BuildServiceProvider only");
        Console.WriteLine("     records how to build it, and the constructor runs on the first request");
    }
}
