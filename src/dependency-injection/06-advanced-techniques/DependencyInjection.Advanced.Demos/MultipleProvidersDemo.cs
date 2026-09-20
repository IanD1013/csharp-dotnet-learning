using System.Net.Http.Json;
using DependencyInjection.Advanced.Demos.Api;
using DependencyInjection.Advanced.Demos.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection.Advanced.Demos;

/// <summary>
/// Lesson 5: Avoiding multiple service providers.
/// </summary>
public static class MultipleProvidersDemo
{
    public static async Task RunAsync()
    {
        await BuildingASecondProvider();
        Console.WriteLine();
        await ResolvingAfterTheAppIsBuilt();
    }

    private static async Task BuildingASecondProvider()
    {
        var builder = InProcessApi.CreateBuilder();
        builder.Services.AddSingleton<IdGenerator>();

        // ASP0000 is exactly the mistake this lesson is about: BuildServiceProvider() here makes a
        // second, completely separate container, and its singletons are not the app's singletons.
#pragma warning disable ASP0000
        var startupIdGenerator = builder.Services.BuildServiceProvider().GetRequiredService<IdGenerator>();
#pragma warning restore ASP0000

        var app = builder.Build();
        app.MapGet("id", (IdGenerator idGen) => idGen.Id);

        using var client = await InProcessApi.StartAsync(app);
        var requestId = await client.GetFromJsonAsync<Guid>(new Uri("id", UriKind.Relative));

        Console.WriteLine("Singleton resolved from builder.Services.BuildServiceProvider() at startup:");
        Console.WriteLine($"  startup: {startupIdGenerator.Id}");
        Console.WriteLine($"  request: {requestId}");
        Console.WriteLine($"  -> same instance: {startupIdGenerator.Id == requestId}  <- two containers, two \"singletons\"");

        await app.StopAsync();
    }

    private static async Task ResolvingAfterTheAppIsBuilt()
    {
        var builder = InProcessApi.CreateBuilder();
        builder.Services.AddSingleton<IdGenerator>();

        var app = builder.Build();
        app.MapGet("id", (IdGenerator idGen) => idGen.Id);

        // app.Services is the container the app itself will use, so there is only ever one.
        var startupIdGenerator = app.Services.GetRequiredService<IdGenerator>();

        using var client = await InProcessApi.StartAsync(app);
        var requestId = await client.GetFromJsonAsync<Guid>(new Uri("id", UriKind.Relative));

        Console.WriteLine("Singleton resolved from app.Services after the app was built:");
        Console.WriteLine($"  startup: {startupIdGenerator.Id}");
        Console.WriteLine($"  request: {requestId}");
        Console.WriteLine($"  -> same instance: {startupIdGenerator.Id == requestId}");

        await app.StopAsync();
    }
}
