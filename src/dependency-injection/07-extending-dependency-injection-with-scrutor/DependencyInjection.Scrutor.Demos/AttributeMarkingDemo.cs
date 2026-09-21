using DependencyInjection.Scrutor.Demos.AttributeMarking;
using DependencyInjection.Scrutor.Demos.Output;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection.Scrutor.Demos;

/// <summary>
/// Lesson 6: Attribute marking.
/// </summary>
public static class AttributeMarkingDemo
{
    private const string Namespace = "DependencyInjection.Scrutor.Demos.AttributeMarking";

    /// <summary>Runs the section.</summary>
    public static void Run()
    {
        Console.WriteLine("WithAttribute<T> in place of AssignableTo<T>, one AddClasses per lifetime:");
        var services = new ServiceCollection();
        services.Scan(selector => selector
            .FromAssemblyOf<Program>()
                .AddClasses(f => f.InNamespaces(Namespace).WithAttribute<SingletonAttribute>())
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime()
                .AddClasses(f => f.InNamespaces(Namespace).WithAttribute<TransientAttribute>())
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
                .AddClasses(f => f.InNamespaces(Namespace).WithAttribute<ScopedAttribute>())
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());
        Registrations.Print(services, Namespace);
        Console.WriteLine();

        Console.WriteLine("  -> compare with lesson 5: no ISingletonService or ITransientService rows,");
        Console.WriteLine("     because the marker is metadata now and not part of the type's interface list");
        Console.WriteLine();

        Console.WriteLine("Where the lifetime is declared, per class:");
        foreach (var type in new[] { typeof(ExampleAService), typeof(ExampleBService), typeof(ExampleCService) })
        {
            var marker = type.GetCustomAttributes(inherit: false)
                .Select(a => a.GetType().Name.Replace("Attribute", string.Empty, StringComparison.Ordinal))
                .FirstOrDefault() ?? "(none)";
            Console.WriteLine($"  {type.Name,-16} [{marker}]");
        }

        Console.WriteLine("  -> readable from the class itself, without opening Program.cs");
    }
}
