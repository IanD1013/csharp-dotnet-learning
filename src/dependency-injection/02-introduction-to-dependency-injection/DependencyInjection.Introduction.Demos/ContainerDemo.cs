using DependencyInjection.Introduction.Demos.Clock;
using DependencyInjection.Introduction.Demos.Data;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection.Introduction.Demos;

/// <summary>
/// Lesson 7 of the notes: "So do you have to do all that manually??".
/// Manual wiring first, then the container that ships with .NET doing the same job.
/// </summary>
public static class ContainerDemo
{
    public static async Task RunAsync()
    {
        Manual();
        Console.WriteLine();
        await ContainerAsync();
    }

    private static void Manual()
    {
        Console.WriteLine("-- Wiring the graph by hand --");

        var greeter = new Greeter(new SystemDateTimeProvider());
        Console.WriteLine($"  new Greeter(new SystemDateTimeProvider()) -> \"{greeter.CreateGreetMessage()}\"");
        Console.WriteLine("  Fine for two types. The data stack already needs three nested `new`s,");
        Console.WriteLine("  and nothing here says how long any of those objects should live.");
    }

    private static async Task ContainerAsync()
    {
        Console.WriteLine("-- The built-in container doing the wiring --");

        using var inMemoryFactory = new SqliteInMemoryDbConnectionFactory();

        var services = new ServiceCollection();
        services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();
        services.AddSingleton<IDbConnectionFactory>(inMemoryFactory);
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<UserService>();
        services.AddTransient<DatabaseInitializer>();
        services.AddTransient<Greeter>();

        await using var provider = services.BuildServiceProvider();

        var greeter = provider.GetRequiredService<Greeter>();
        Console.WriteLine($"  GetRequiredService<Greeter>() -> \"{greeter.CreateGreetMessage()}\"");

        await provider.GetRequiredService<DatabaseInitializer>().InitializeAsync();

        var userService = provider.GetRequiredService<UserService>();
        await userService.CreateAsync(new User { FullName = "Grace Hopper" });
        Console.WriteLine($"  GetRequiredService<UserService>() -> {(await userService.GetAllAsync()).Count()} row(s)");
        Console.WriteLine("  The whole UserService -> UserRepository -> IDbConnectionFactory chain");
        Console.WriteLine("  came out of one registration line each.");

        Console.WriteLine();
        Console.WriteLine("  Lifetimes, which manual wiring leaves entirely to you:");

        var clockA = provider.GetRequiredService<IDateTimeProvider>();
        var clockB = provider.GetRequiredService<IDateTimeProvider>();
        Console.WriteLine($"    singleton IDateTimeProvider, same instance twice -> {ReferenceEquals(clockA, clockB)}");

        var repositoryA = provider.GetRequiredService<IUserRepository>();
        var repositoryB = provider.GetRequiredService<IUserRepository>();
        Console.WriteLine($"    transient IUserRepository, same instance twice  -> {ReferenceEquals(repositoryA, repositoryB)}");
    }
}
