using DependencyInjection.Scrutor.Demos.Output;
using DependencyInjection.Scrutor.Demos.Pitfalls;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace DependencyInjection.Scrutor.Demos;

/// <summary>
/// Lesson 10: Potential pitfalls.
/// </summary>
public static class PitfallsDemo
{
    private const string Namespace = "DependencyInjection.Scrutor.Demos.Pitfalls";

    /// <summary>Runs the section.</summary>
    public static void Run()
    {
        Console.WriteLine("A filter broad enough to be convenient: everything named *Service, transient.");
        var broad = new ServiceCollection();
        broad.Scan(selector => selector
            .FromAssemblyOf<Program>()
                .AddClasses(f => f.InNamespaces(Namespace).Where(t => t.Name.EndsWith("Service", StringComparison.Ordinal)))
                .AsMatchingInterface()
                .WithTransientLifetime());
        Registrations.Print(broad, Namespace);

        using (var provider = broad.BuildServiceProvider())
        {
            var first = provider.GetRequiredService<IConnectionPoolService>();
            var second = provider.GetRequiredService<IConnectionPoolService>();
            Console.WriteLine($"  pool on first resolve:  {first.Id}");
            Console.WriteLine($"  pool on second resolve: {second.Id}");
            Console.WriteLine($"  same instance: {ReferenceEquals(first, second)}  <- a connection pool per caller");
        }

        Console.WriteLine("  -> DatabaseMigrationService is resolvable too, and it was never meant to be");
        Console.WriteLine("  -> nothing failed; the build was clean and the app started");
        Console.WriteLine();

        Console.WriteLine("The same two types, registered explicitly:");
        var explicitly = new ServiceCollection();
        explicitly.AddSingleton<IConnectionPoolService, ConnectionPoolService>();
        Registrations.Print(explicitly, Namespace);

        using (var provider = explicitly.BuildServiceProvider())
        {
            var first = provider.GetRequiredService<IConnectionPoolService>();
            var second = provider.GetRequiredService<IConnectionPoolService>();
            Console.WriteLine($"  same instance: {ReferenceEquals(first, second)}");
        }

        Console.WriteLine("  -> two lines of registration, and both problems are gone");
        Console.WriteLine();

        Console.WriteLine("Where scanning is worth it, RegistrationStrategy.Throw keeps it honest:");
        try
        {
            var guarded = new ServiceCollection();
            guarded.AddSingleton<IConnectionPoolService, ConnectionPoolService>();
            guarded.Scan(selector => selector
                .FromAssemblyOf<Program>()
                    .AddClasses(f => f.InNamespaces(Namespace).Where(t => t.Name.EndsWith("Service", StringComparison.Ordinal)))
                        .UsingRegistrationStrategy(RegistrationStrategy.Throw)
                        .AsMatchingInterface()
                        .WithTransientLifetime());
        }
        catch (InvalidOperationException exception)
        {
            Console.WriteLine($"  {exception.GetType().Name}: {exception.Message}");
        }

        Console.WriteLine("  -> the broad filter is still broad, but it can no longer overwrite a deliberate");
        Console.WriteLine("     registration in silence");
    }
}
