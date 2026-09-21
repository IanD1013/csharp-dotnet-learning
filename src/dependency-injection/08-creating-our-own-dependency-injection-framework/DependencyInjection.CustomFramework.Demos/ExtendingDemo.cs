using DependencyInjection.CustomFramework.Demos.Services;

using Vax;

namespace DependencyInjection.CustomFramework.Demos;

/// <summary>
/// Lesson 4 - Extending the main implementation. Generic constraints, self-registration,
/// a hand-built descriptor, a pre-built instance, and factory registration.
/// </summary>
public static class ExtendingDemo
{
    public static void Run()
    {
        Console.WriteLine("Type safety, enforced by the compiler rather than at runtime:");
        Console.WriteLine("  AddSingleton<TService, TImplementation>() is constrained to");
        Console.WriteLine("  where TImplementation : class, TService");
        Console.WriteLine("  -> services.AddSingleton<IIdGenerator, ConsoleWriter>() is CS0311 at build time,");
        Console.WriteLine("     so it can never reach Activator.CreateInstance and fail there");

        Console.WriteLine();
        Console.WriteLine("Self-registration, for a concrete type with no interface:");
        var selfRegistered = new ServiceCollection();
        selfRegistered.AddSingleton<ConsoleWriter>();

        var selfProvider = selfRegistered.BuildServiceProvider();
        selfProvider.GetService<ConsoleWriter>()!.WriteLine("  written by the self-registered ConsoleWriter");
        Console.WriteLine($"  the descriptor maps {selfRegistered[0].ServiceType.Name} -> {selfRegistered[0].ImplementationType.Name}");

        Console.WriteLine();
        Console.WriteLine("A descriptor built by hand and handed to AddService:");
        var manual = new ServiceCollection();
        manual.AddService(new ServiceDescriptor
        {
            ServiceType = typeof(IConsoleWriter),
            ImplementationType = typeof(ConsoleWriter),
            Lifetime = ServiceLifetime.Transient
        });

        manual.BuildServiceProvider().GetService<IConsoleWriter>()!
            .WriteLine("  written by a service registered through a hand-built descriptor");

        Console.WriteLine();
        Console.WriteLine("An instance created outside the container:");
        var existing = new ConsoleWriter();
        var instanceServices = new ServiceCollection();
        instanceServices.AddSingleton(existing);

        var resolved = instanceServices.BuildServiceProvider().GetService<ConsoleWriter>();
        Console.WriteLine($"  ReferenceEquals(existing, resolved): {ReferenceEquals(existing, resolved)}");
        Console.WriteLine("  -> AddSingleton(object) reads the runtime type as the service type and stores");
        Console.WriteLine("     the object itself, so nothing is ever constructed for it");

        Console.WriteLine();
        Console.WriteLine("A factory, which gets the provider and can resolve nested dependencies:");
        var factoryServices = new ServiceCollection();
        factoryServices.AddSingleton<ConsoleWriter>();
        factoryServices.AddSingleton<IIdGenerator>(
            provider => new IdGenerator(provider.GetService<ConsoleWriter>()!));

        var factoryProvider = factoryServices.BuildServiceProvider();
        var first = factoryProvider.GetService<IIdGenerator>();
        var second = factoryProvider.GetService<IIdGenerator>();
        first!.PrintId();
        second!.PrintId();
        Console.WriteLine($"  ReferenceEquals: {ReferenceEquals(first, second)}");
        Console.WriteLine("  -> the factory is wrapped in the same Lazy, so a singleton factory runs once");

        Console.WriteLine();
        Console.WriteLine("The same factory registered as Transient:");
        var transientFactory = new ServiceCollection();
        transientFactory.AddSingleton<ConsoleWriter>();
        transientFactory.AddTransient<IIdGenerator>(
            provider => new IdGenerator(provider.GetService<ConsoleWriter>()!));

        var transientProvider = transientFactory.BuildServiceProvider();
        transientProvider.GetService<IIdGenerator>()!.PrintId();
        transientProvider.GetService<IIdGenerator>()!.PrintId();
        Console.WriteLine("  -> two ids: the transient branch invokes the factory on every request");
    }
}
