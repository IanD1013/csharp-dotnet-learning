using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection.Scrutor.Demos.Output;

/// <summary>
/// The course's PrintRegisteredService helper, kept in one place because every lesson in this
/// chapter reads the container back out to see what scanning actually produced.
/// </summary>
public static class Registrations
{
    /// <summary>Prints every descriptor in the collection.</summary>
    public static void Print(IServiceCollection services) => Print(services, null);

    /// <summary>
    /// Prints the descriptors whose service type lives under <paramref name="namespacePrefix"/>.
    /// All ten sections share one assembly, so the console would be unreadable without it.
    /// </summary>
    public static void Print(IServiceCollection services, string? namespacePrefix)
    {
        var printed = 0;
        foreach (var descriptor in services)
        {
            if (namespacePrefix is not null &&
                descriptor.ServiceType.Namespace?.StartsWith(namespacePrefix, StringComparison.Ordinal) != true)
            {
                continue;
            }

            var key = descriptor.IsKeyedService ? $" [key: {descriptor.ServiceKey}]" : string.Empty;
            Console.WriteLine($"  {Name(descriptor.ServiceType)}{key} -> {Implementation(descriptor)} as {descriptor.Lifetime}");
            printed++;
        }

        if (printed == 0)
        {
            Console.WriteLine("  (nothing registered)");
        }
    }

    private static string Implementation(ServiceDescriptor descriptor)
    {
        if (descriptor.IsKeyedService)
        {
            if (descriptor.KeyedImplementationType is not null)
            {
                return Name(descriptor.KeyedImplementationType);
            }

            return descriptor.KeyedImplementationInstance is not null
                ? Name(descriptor.KeyedImplementationInstance.GetType())
                : "(factory)";
        }

        if (descriptor.ImplementationType is not null)
        {
            return Name(descriptor.ImplementationType);
        }

        if (descriptor.ImplementationInstance is not null)
        {
            return Name(descriptor.ImplementationInstance.GetType());
        }

        // AsSelfWithInterfaces and Decorate both register a factory, so there is no
        // ImplementationType to report. That absence is itself part of lessons 2 and 4.
        return "(factory)";
    }

    private static string Name(Type type)
    {
        if (!type.IsGenericType)
        {
            return type.Name;
        }

        var name = type.Name[..type.Name.IndexOf('`', StringComparison.Ordinal)];
        var arguments = string.Join(", ", type.GetGenericArguments().Select(Name));
        return $"{name}<{arguments}>";
    }
}
