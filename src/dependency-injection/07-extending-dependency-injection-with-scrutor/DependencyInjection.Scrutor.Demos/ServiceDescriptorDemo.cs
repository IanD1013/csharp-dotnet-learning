using DependencyInjection.Scrutor.Demos.Output;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection.Scrutor.Demos;

/// <summary>
/// Lesson 8: Using the ServiceDescriptor attribute.
/// </summary>
public static class ServiceDescriptorDemo
{
    private const string Namespace = "DependencyInjection.Scrutor.Demos.Descriptors";

    /// <summary>Runs the section.</summary>
    public static void Run()
    {
        Console.WriteLine("One AddClasses with no filter at all, closed by UsingAttributes():");
        var services = new ServiceCollection();
        services.Scan(selector => selector
            .FromAssemblyOf<Program>()
                .AddClasses(f => f.InNamespaces(Namespace))
                .UsingAttributes());
        Registrations.Print(services, Namespace);
        Console.WriteLine();

        Console.WriteLine("What each attribute asked for:");
        Console.WriteLine("  [ServiceDescriptor]                                       -> self and interface, transient");
        Console.WriteLine("  [ServiceDescriptor(typeof(IInterfaceOnlyService))]        -> that interface only, transient");
        Console.WriteLine("  [ServiceDescriptor(typeof(ISingletonOnlyService), Singleton)] -> that interface only, singleton");
        Console.WriteLine("  [ServiceDescriptor(null, Singleton)]                      -> self and interface, singleton");
        Console.WriteLine("  two attributes on MultiService                            -> both rows, in attribute order");
        Console.WriteLine();
        Console.WriteLine("  -> no custom attribute classes were written for any of this, and the lifetime");
        Console.WriteLine("     lives on the class rather than in the Scan call");
    }
}
