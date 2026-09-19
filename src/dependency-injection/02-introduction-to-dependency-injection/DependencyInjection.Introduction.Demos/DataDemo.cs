using DependencyInjection.Introduction.Demos.Data;

namespace DependencyInjection.Introduction.Demos;

/// <summary>
/// Lesson 3 of the notes: "A practical example of the dependency problem".
/// The same UserService runs against a file database and an in-memory one without a single
/// edit, because the provider is chosen at the composition root.
/// </summary>
public static class DataDemo
{
    public static async Task RunAsync()
    {
        await HardWiredAsync();

        Console.WriteLine();
        var fileFactory = new SqliteFileDbConnectionFactory(new DbConnectionOptions
        {
            ConnectionString = $"Data Source={DemoDatabase.HardWiredPath}"
        });
        await InjectedAsync(fileFactory, "SQLite file");

        Console.WriteLine();
        using var inMemoryFactory = new SqliteInMemoryDbConnectionFactory();
        await InjectedAsync(inMemoryFactory, "SQLite in-memory");

        Console.WriteLine();
        Console.WriteLine("  Same UserService, same UserRepository, two providers.");
        Console.WriteLine("  Only the composition root changed.");
    }

    private static async Task HardWiredAsync()
    {
        Console.WriteLine("-- Service news up repository news up factory --");
        Console.WriteLine($"  Database file: {DemoDatabase.HardWiredPath}");

        // Seeded through the injected stack, so the hard-wired chain has a row to read.
        var seedFactory = new SqliteFileDbConnectionFactory(new DbConnectionOptions
        {
            ConnectionString = $"Data Source={DemoDatabase.HardWiredPath}"
        });
        await new DatabaseInitializer(seedFactory).InitializeAsync();
        await new UserRepository(seedFactory).CreateAsync(new User { FullName = "Nick Chapsas" });

        var service = new TightlyCoupledUserService();
        var users = await service.GetAllAsync();

        Console.WriteLine($"  Read {users.Count()} user(s) through the hard-wired chain.");
        Console.WriteLine("  It works, but a unit test of this service needs that exact file on disk,");
        Console.WriteLine("  and moving to another provider means editing the repository.");
    }

    private static async Task InjectedAsync(IDbConnectionFactory connectionFactory, string label)
    {
        Console.WriteLine($"-- IDbConnectionFactory injected: {label} --");

        await new DatabaseInitializer(connectionFactory).InitializeAsync();

        var userService = new UserService(new UserRepository(connectionFactory));

        var user = new User { FullName = "Ada Lovelace" };
        Console.WriteLine($"  CreateAsync     -> {await userService.CreateAsync(user)}");
        Console.WriteLine($"  GetByIdAsync    -> {(await userService.GetByIdAsync(user.Id))?.FullName ?? "<null>"}");
        Console.WriteLine($"  GetAllAsync     -> {(await userService.GetAllAsync()).Count()} row(s)");
        Console.WriteLine($"  DeleteByIdAsync -> {await userService.DeleteByIdAsync(user.Id)}");
        Console.WriteLine($"  GetAllAsync     -> {(await userService.GetAllAsync()).Count()} row(s)");
    }
}
