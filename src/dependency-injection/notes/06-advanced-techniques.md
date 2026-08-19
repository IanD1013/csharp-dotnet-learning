# Advanced techniques

> Course: [From Zero to Hero: Dependency Injection in .NET with C#](https://dometrain.com/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp/) · Chapter 6
> 8 lessons · ~47:32
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Creating a custom scope](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/creating-a-custom-scope-53953210/) | 5:42 | [↓](#1-creating-a-custom-scope) |
| 2 | [The service locator anti-pattern](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-service-locator-anti-pattern-53953211/) | 2:48 | [↓](#2-the-service-locator-anti-pattern) |
| 3 | [When service locator makes sense](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/when-service-locator-makes-sense-53953212/) | 20:37 | [↓](#3-when-service-locator-makes-sense) |
| 4 | [Avoiding capturing dependencies](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/avoiding-capturing-dependencies-53953213/) | 3:14 | [↓](#4-avoiding-capturing-dependencies) |
| 5 | [Avoiding multiple service providers](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/avoiding-multiple-service-providers-53953214/) | 3:18 | [↓](#5-avoiding-multiple-service-providers) |
| 6 | [Creating decorators](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/creating-decorators-53953215/) | 7:11 | [↓](#6-creating-decorators) |
| 7 | [The future of dependency injection](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-future-of-dependency-injection-53953216/) | 3:55 | [↓](#7-the-future-of-dependency-injection) |
| 8 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953217/) | 0:47 | [↓](#8-section-recap) |

---

## 1. Creating a custom scope

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/creating-a-custom-scope-53953210/) · 5:42

### Summary

In console applications or background services, the DI container does not automatically manage scopes like it does in ASP.NET Core.
To use scoped services correctly in these environments—such as when processing individual messages from a bus—developers must manually create and manage custom scopes.
This lesson demonstrates how to use IServiceProvider.CreateScope() and IServiceScopeFactory to define boundaries for scoped lifetimes, ensuring that services are resolved and disposed of correctly within a specific unit of work.

### Key concepts

*   **Implicit vs. Explicit Scopes**: In ASP.NET Core, scopes are tied to the lifetime of an HTTP request; in console apps, scopes must be defined manually.
*   **Scoped Service Behavior**: Without an active scope, services registered as scoped are treated as singletons by the root service provider.
*   **IServiceScope**: An interface representing a temporary boundary where scoped services live.
*   **IServiceScopeFactory**: The preferred architectural approach for creating scopes, providing a more narrow and controlled interface than the full service provider.

### Lesson notes

In a standard Web API, the DI container manages scopes automatically.
However, in a console application or a service consuming messages from an event bus (like RabbitMQ or Azure Service Bus), you often want a scope to represent the processing of a single individual message.
Without a manually defined scope, the container has no way of knowing when a scoped lifetime should begin or end.

Consider a service used to demonstrate identity across resolutions:

```csharp
namespace CustomScope.ConsoleApp;

public class ExampleService
{
    public Guid Id { get; } = Guid.NewGuid();
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/creating-a-custom-scope-53953210/?t=85)

If you register this service as scoped in a console application and resolve it directly from the root `IServiceProvider`, it will behave like a singleton.
Every resolution will return the same instance because no scope boundary has been established.

```csharp
using Microsoft.Extensions.DependencyInjection;
using CustomScope.ConsoleApp;

var services = new ServiceCollection();

services.AddScoped<ExampleService>();

var serviceProvider = services.BuildServiceProvider();

var exampleService1 = serviceProvider.GetRequiredService<ExampleService>();
var exampleService2 = serviceProvider.GetRequiredService<ExampleService>();

Console.WriteLine(exampleService1.Id);
Console.WriteLine(exampleService2.Id);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/creating-a-custom-scope-53953210/?t=100)

To create a custom scope, use the `CreateScope()` method.
This returns an `IServiceScope` which contains its own `ServiceProvider`.
Services resolved from this scoped provider are unique to that scope.
By wrapping the scope in a `using` block, you ensure that all services created within that scope are correctly disposed of when the block ends.

```csharp
services.AddScoped<ExampleService>();

var serviceProvider = services.BuildServiceProvider();

using (var serviceScope = serviceProvider.CreateScope())
{
    var exampleService1 = serviceScope.ServiceProvider.GetRequiredService<ExampleService>();
    Console.WriteLine(exampleService1.Id);
}

using (var serviceScope = serviceProvider.CreateScope())
{
    var exampleService2 = serviceScope.ServiceProvider.GetRequiredService<ExampleService>();
    Console.WriteLine(exampleService2.Id);
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/creating-a-custom-scope-53953210/?t=235)

While calling `CreateScope()` on the `IServiceProvider` is functional, it can be seen as a violation of the Service Locator pattern because it gives the consumer access to the full-fledged provider.
A more architecturally sound approach is to use `IServiceScopeFactory`.
This factory is automatically registered by the DI container and its sole responsibility is creating scopes.
This makes the intent of the code clearer and restricts the power given to the component injecting the factory.

```csharp
services.AddScoped<ExampleService>();

var serviceProvider = services.BuildServiceProvider();

var serviceScopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

using (var serviceScope = serviceScopeFactory.CreateScope())
{
    var exampleService1 = serviceScope.ServiceProvider.GetRequiredService<ExampleService>();
    Console.WriteLine(exampleService1.Id);
}

using (var serviceScope = serviceScopeFactory.CreateScope())
{
    var exampleService2 = serviceScope.ServiceProvider.GetRequiredService<ExampleService>();
    Console.WriteLine(exampleService2.Id);
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/creating-a-custom-scope-53953210/?t=310)

---

## 2. The service locator anti-pattern

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-service-locator-anti-pattern-53953211/) · 2:48

### Summary

The service locator pattern is considered an anti-pattern in modern .NET development because it obscures a class's dependencies and complicates unit testing.
By resolving services directly from an IServiceProvider or HttpContext within a method body rather than injecting them via a constructor, developers hide the true requirements of a component, making the code harder to maintain and mock effectively.

### Key concepts

* **Service Locator Definition**: A pattern where a central registry or provider (like `IServiceProvider`) is used to resolve dependencies on demand within a class, rather than having them injected.
* **Hidden Dependencies**: Using a service locator hides what a class actually needs to function, making the API surface dishonest about its requirements.
* **Testing Friction**: Mocking dependencies becomes significantly more difficult because the tester must set up the entire service provider infrastructure rather than just passing a mock into a constructor.
* **Intent**: Explicit constructor injection shows the intent of the class, whereas service location requires reading the implementation details to understand dependencies.

### Lesson notes

The service locator is a pattern that allows a component to resolve its dependencies by querying a central container or provider.
In modern .NET, this is typically done by using `IServiceProvider` directly.
While it was once common, it is now widely regarded as an anti-pattern because it circumvents the primary benefits of Dependency Injection.

A common example of this anti-pattern occurs in ASP.NET Core filters or attributes, where a developer might use the `HttpContext` to resolve a service manually:

```csharp
public class DurationLoggerAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var sw = Stopwatch.StartNew();
        try
        {
            await next();
        }
        finally
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<DurationLoggerAttribute>>();
            logger.LogInformation("Request with name {0} completed in {1}ms",
                context.ActionDescriptor.DisplayName,
                sw.ElapsedMilliseconds);
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-service-locator-anti-pattern-53953211/?t=25)

#### Why it is an anti-pattern

**1. Lack of Intent**
Intent is a critical aspect of software design.
When a class defines its dependencies in its constructor (e.g., `public MyController(IDatabase db)`), it explicitly communicates that it requires a database to function.
If you use a service locator inside a method, a consumer of that class cannot know what dependencies are required without reading the internal implementation.
If the source code is not available, the class becomes a "black box" with hidden requirements.

**2. Testing and Mocking Difficulties**
Service location makes unit testing significantly more complex.
If a method hides a dependency resolution inside its body, a test will fail at runtime with a `NullReferenceException` or a resolution error unless the developer knows exactly which service to mock and how to register it in a mock container.
In the example above, to test the `DurationLoggerAttribute`, a developer would have to mock the `ActionExecutingContext`, which contains an `HttpContext`, which contains a `RequestServices` provider, which finally returns the `ILogger`.
This creates a "messy" testing setup compared to simply passing a mock logger into a constructor.

**3. Maintenance Overhead**
Injecting `IServiceProvider` instead of the specific services required is generally discouraged.
You should always prefer injecting the specific services needed.
If you find yourself in a situation where you feel forced to use a service locator, it should be treated as an edge case that requires extensive documentation to explain how the code should be tested and why the pattern was necessary.

While there are very specific, legitimate use cases where the benefits of service location outweigh the drawbacks (such as certain framework-level constraints), these are rare and should be communicated clearly to the development team.

---

## 3. When service locator makes sense

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/when-service-locator-makes-sense-53953212/) · 20:37

### Summary

While the Service Locator pattern is typically considered an anti-pattern, it is a valid choice for infrastructure-level components that must dynamically resolve services based on runtime data, such as command-line arguments or message types.
This lesson demonstrates how to implement a 'Handler Orchestrator' that uses assembly scanning and custom attributes to map string commands to specific handler implementations.
By injecting IServiceScopeFactory and creating a localized service provider scope, the orchestrator can resolve dependencies for specific handlers without polluting the main application's constructor with every possible service implementation, effectively mimicking the internal behavior of libraries like MediatR.

### Key concepts

* **Infrastructure-level Service Location**: Using service location within a specialized orchestrator rather than as a global dependency.
* **Dynamic Command Dispatching**: Mapping runtime strings (e.g., CLI arguments) to specific service types.
* **Assembly Scanning**: Automatically discovering types that implement a specific interface within an assembly.
* **Metadata-driven Resolution**: Using custom attributes to associate metadata (like command names) with implementation types.
* **Scoped Resolution in Singletons**: Using `IServiceScopeFactory` to resolve scoped or transient services from within a singleton orchestrator.

### Lesson notes

In complex applications, such as a multi-function console app, you may need to execute different logic based on runtime input.
A common example is a CLI that accepts commands like `weather` or `time`.
Hardcoding these mappings into the main application class leads to bloated constructors and rigid code.

```csharp
using MultiFunction.ConsoleApp.Time;
using MultiFunction.ConsoleApp.Weather;

var services = new ServiceCollection();

services.AddSingleton<IConsoleWriter, ConsoleWriter>();
services.AddHttpClient();
services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
services.AddSingleton<IWeatherService, OpenWeatherService>();

services.AddSingleton<Application>();

var serviceProvider = services.BuildServiceProvider();

var application = serviceProvider.GetRequiredService<Application>();
if (args.Length == 0)
{
    args = new[] { "weather" };
}

await application.RunAsync(args);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/when-service-locator-makes-sense-53953212/?t=25)

To handle this dynamically, we define a common interface, `IHandler`, which all command implementations must fulfill.

```csharp
namespace MultiFunction.ConsoleApp.Handlers;

public interface IHandler
{
    Task HandleAsync();
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/when-service-locator-makes-sense-53953212/?t=175)

Individual handlers, such as the `GetCurrentLondonWeatherHandler`, implement this interface and receive their own specific dependencies via standard constructor injection.

```csharp
private readonly IConsoleWriter _consoleWriter;
    private readonly IWeatherService _weatherService;

    public GetCurrentLondonWeatherHandler(IConsoleWriter consoleWriter,
        IWeatherService weatherService)
    {
        _consoleWriter = consoleWriter;
        _weatherService = weatherService;
    }

    public async Task HandleAsync()
    {
        var weather = await _weatherService.GetCurrentWeatherAsync("London");
        _consoleWriter.WriteLine($"The temperature in London is {weather.Main.Temp}C");
    }
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/when-service-locator-makes-sense-53953212/?t=250)

To resolve these handlers dynamically, we implement a `HandlerOrchestrator`.
This class is a specialized service locator.
Instead of injecting every possible handler into the `Application` class, we inject the orchestrator.
The orchestrator uses `IServiceScopeFactory` to create a scope and resolve the required handler type at runtime.

```csharp
using Microsoft.Extensions.DependencyInjection;

namespace MultiFunction.ConsoleApp.Handlers;

public class HandlerOrchestrator
{
    private readonly Dictionary<string, Type> _handlerTypes = new();
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public HandlerOrchestrator(IServiceScopeFactory serviceScopeFactory)
    { 
        _serviceScopeFactory = serviceScopeFactory;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/when-service-locator-makes-sense-53953212/?t=370)

The orchestrator resolves the handler by looking up the type in a dictionary and using the `IServiceProvider` from a new scope to instantiate it.
This ensures that the handler's dependencies are correctly managed according to their registered lifetimes.

```csharp
    public IHandler? GetHandlerForCommandName(string command)
    {
        var handlerType = _handlerTypes.GetValueOrDefault(command);

        if (handlerType is null)
        {
            return null;
        }

        using var serviceScope = _serviceScopeFactory.CreateScope();
        return (IHandler)serviceScope.ServiceProvider.GetRequiredService(handlerType);
    }
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/when-service-locator-makes-sense-53953212/?t=475)

To avoid manual registration of every command-to-type mapping, we use a custom attribute, `CommandNameAttribute`, to decorate handler classes.

```csharp
namespace MultiFunction.ConsoleApp.Handlers;

[AttributeUsage(AttributeTargets.Class)]
public class CommandNameAttribute : Attribute
{
    public string CommandName { get; set; }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/when-service-locator-makes-sense-53953212/?t=535)

Using reflection and assembly scanning, the orchestrator can automatically discover all classes implementing `IHandler` that are decorated with the `CommandNameAttribute` and populate its internal mapping dictionary.

```csharp
private void RegisterCommandHandler()
{
    var handlerTypes = typeof(IHandler).Assembly.DefinedTypes
        .Where(x => !x.IsInterface && !x.IsAbstract && typeof(IHandler).IsAssignableFrom(x));

    foreach (var handlerType in handlerTypes)
    {
        var commandNameAttribute = handlerType.GetCustomAttribute<CommandNameAttribute>();
        if(commandNameAttribute is null) continue;
        
        var commandName = commandNameAttribute.CommandName;
        _handlerTypes[commandName] = handlerType;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/when-service-locator-makes-sense-53953212/?t=655)

Finally, to make the system truly plug-and-play, we create extension methods for `IServiceCollection`.
These methods handle both the registration of the orchestrator and the automatic registration of all discovered handler types into the DI container.

```csharp
namespace MultiFunction.ConsoleApp.Handlers;

public static class HandlerExtensions
{
    public static void AddCommandHandlers(this IServiceCollection services, Assembly assembly)
    {
        services.TryAddSingleton<HandlerOrchestrator>();

        var handlerTypes = GetHandlerTypesForAssembly(assembly);

        foreach (var handlerType in handlerTypes)
        {
            services.TryAddTransient(handlerType);
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/when-service-locator-makes-sense-53953212/?t=1135)

This approach provides a clean, extensible architecture where adding a new command only requires creating a new class that implements `IHandler` and adding the `CommandName` attribute.
The infrastructure handles the rest through controlled service location.

---

## 4. Avoiding capturing dependencies

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/avoiding-capturing-dependencies-53953213/) · 3:14

### Summary

Capturing dependencies in closures is a common mistake in .NET applications, particularly when using Minimal APIs.
When a service is resolved from the root service provider outside of a request handler and then used inside that handler's lambda expression, it becomes captured as a closure.
This effectively turns the service into a singleton for the lifetime of the application, regardless of its intended lifetime (e.g., transient or scoped), which can lead to severe concurrency issues and state corruption.

### Key concepts

- **Closure Capture**: Accessing variables from an outer scope within a lambda expression, causing the variable to be held in memory as long as the lambda exists.
- **Root Provider Resolution**: Resolving services directly from `app.Services` (the root container) rather than the request-specific scope.
- **Lifetime Distortion**: The phenomenon where a transient or scoped service behaves like a singleton because it is captured by a long-lived delegate.
- **Minimal API Parameter Injection**: The practice of declaring dependencies as parameters in the endpoint handler to ensure they are resolved correctly from the request scope.

### Lesson notes

In a standard Minimal API implementation, dependencies are typically injected directly into the route handler delegate.
This ensures that the Model Context Protocol or the underlying Dependency Injection (DI) container can manage the service's lifetime correctly.
For example, if a service is registered as transient, it will be instantiated every time the endpoint is called.

```csharp
using Microsoft.AspNetCore.Mvc;
using Weather.Minimal.Api.Weather;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddTransient<IWeatherService, OpenWeatherService>();

var app = builder.Build();

app.MapGet("weather/{city}",
    async ([FromRoute] string city, IWeatherService weatherService) =>
{
    var weather = await weatherService.GetCurrentWeatherAsync(city);
    return weather == null ? Results.NotFound() : Results.Ok(weather);
});

app.Run();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/avoiding-capturing-dependencies-53953213/?t=20)

In the implementation above, the `OpenWeatherService` is resolved through the method parameters.
If you place a breakpoint in the constructor of `OpenWeatherService`, you will see it triggered on every request because it is registered with a transient lifetime.

```csharp
namespace Weather.Minimal.Api.Weather;

public class OpenWeatherService : IWeatherService
{
    private const string OpenWeatherApiKey = "f539ebbe9ad5228403f6c267b7b7743c";
    private readonly IHttpClientFactory _httpClientFactory;

    public OpenWeatherService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<WeatherResponse?> GetCurrentWeatherAsync(string city)
    {
        var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}";
        var httpClient = _httpClientFactory.CreateClient();

        var weatherResponse = await httpClient.GetAsync(url);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/avoiding-capturing-dependencies-53953213/?t=50)

A common but fatal flaw occurs when developers resolve the service from the root service provider (`app.Services`) before the request is even created.
By resolving the service outside the scope of the handler and then using it inside the lambda, the service is captured as a closure.

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddTransient<IWeatherService, OpenWeatherService>();

var app = builder.Build();

var weatherService = app.Services.GetRequiredService<IWeatherService>();

app.MapGet("weather/{city}",
    async ([FromRoute] string city) =>
{
    var weather = await weatherService.GetCurrentWeatherAsync(city);
    return weather == null ? Results.NotFound() : Results.Ok(weather);
});

app.Run();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/avoiding-capturing-dependencies-53953213/?t=100)

In this scenario, the `weatherService` is resolved once when the application starts.
Even though it is registered as `Transient`, it is now treated as an out-of-scope singleton because the same instance is reused for every single request.
This bypasses the intended DI lifecycle and can cause significant concurrency issues, especially if the service is not thread-safe.

To fix this, ensure that services are resolved within their proper scope by passing them as arguments to the handler delegate.
This allows the framework to properly resolve the dependency from the request-specific scope for every execution.

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddTransient<IWeatherService, OpenWeatherService>();

var app = builder.Build();

//var weatherService = app.Services.GetRequiredService<IWeatherService>();

app.MapGet("weather/{city}",
    async ([FromRoute] string city, IWeatherService weatherService) =>
{
    var weather = await weatherService.GetCurrentWeatherAsync(city);
    return weather == null ? Results.NotFound() : Results.Ok(weather);
});

app.Run();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/avoiding-capturing-dependencies-53953213/?t=175)

---

## 5. Avoiding multiple service providers

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/avoiding-multiple-service-providers-53953214/) · 3:18

Creating multiple service providers is a common mistake that can lead to subtle and difficult-to-debug issues in .NET applications.
When an ASP.NET Core application is built, it typically manages a single internal service provider to resolve dependencies for the lifetime of the application.

### Key concepts

- **Independent Containers**: Calling `BuildServiceProvider()` creates a new, isolated dependency injection container.
- **Singleton Violation**: Singletons are only unique within the scope of a single service provider. Multiple providers result in multiple "singleton" instances.
- **Startup Pitfalls**: Manually building a provider during startup (e.g., to run migrations or access configuration) is the primary cause of this issue.
- **Best Practices**: In Web APIs, rely on the framework's `builder.Build()`; in console applications, ensure `BuildServiceProvider()` is called only once.

### Lesson notes

In a standard Minimal API setup, services are registered to the `IServiceCollection` and resolved automatically by the framework.

```csharp
using Microsoft.AspNetCore.Mvc;
using Weather.Minimal.Api.Weather;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddTransient<IWeatherService, OpenWeatherService>();

var app = builder.Build();

app.MapGet("weather/{city}",
    async ([FromRoute] string city, IWeatherService weatherService) =>
{
    var weather = await weatherService.GetCurrentWeatherAsync(city);
    return weather == null ? Results.NotFound() : Results.Ok(weather);
});

app.Run();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/avoiding-multiple-service-providers-53953214/?t=10)

Problems arise when a developer needs to access a service before the application has fully started.
For example, consider an `IdGenerator` registered as a singleton:

```csharp
using Microsoft.AspNetCore.Mvc;
using Weather.Minimal.Api;
using Weather.Minimal.Api.Weather;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddSingleton<IdGenerator>();
builder.Services.AddTransient<IWeatherService, OpenWeatherService>();



var app = builder.Build();

app.MapGet("weather/{city}",
    async ([FromRoute] string city, IWeatherService weatherService) =>
{
    var weather = await weatherService.GetCurrentWeatherAsync(city);
    return weather == null ? Results.NotFound() : Results.Ok(weather);
});

app.Run();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/avoiding-multiple-service-providers-53953214/?t=55)

If you attempt to resolve this `IdGenerator` during the configuration phase by calling `BuildServiceProvider()`, you create a second container:

```csharp
using Microsoft.AspNetCore.Mvc;
using Weather.Minimal.Api;
using Weather.Minimal.Api.Weather;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddSingleton<IdGenerator>();

var idGenerator = builder.Services.BuildServiceProvider().GetRequiredService<IdGenerator>();

builder.Services.AddTransient<IWeatherService, OpenWeatherService>();

var app = builder.Build();

app.MapGet("weather/{city}",
    async ([FromRoute] string city, IWeatherService weatherService) =>
{
    var weather = await weatherService.GetCurrentWeatherAsync(city);
    return weather == null ? Results.NotFound() : Results.Ok(weather);
});

app.Run();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/avoiding-multiple-service-providers-53953214/?t=85)

When the application eventually runs and handles a request, it uses its own internal service provider.
If the endpoint also injects the `IdGenerator`, the instance provided to the endpoint will be different from the instance resolved during startup.

```csharp
using Microsoft.AspNetCore.Mvc;
using Weather.Minimal.Api;
using Weather.Minimal.Api.Weather;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddSingleton<IdGenerator>();

var idGenerator = builder.Services.BuildServiceProvider().GetRequiredService<IdGenerator>();
Console.WriteLine(idGenerator.Id);

builder.Services.AddTransient<IWeatherService, OpenWeatherService>();

var app = builder.Build();

app.MapGet("weather/{city}",
    async ([FromRoute] string city, IWeatherService weatherService,
        IdGenerator idGen) =>
{
    Console.WriteLine(idGen.Id);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/avoiding-multiple-service-providers-53953214/?t=130)

In this scenario, the console will output two different GUIDs, even though the service is registered as a singleton.
This happens because each service provider maintains its own internal dictionary of singleton instances.
This can lead to critical failures if the singleton is intended to manage shared state, such as a cache or a database connection pool.

To resolve this, avoid calling `BuildServiceProvider()` in the `Program.cs` of a Web API.
Instead, perform service resolution after the application has been built using `app.Services` or within the scope of a request.

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddSingleton<IdGenerator>();

builder.Services.AddTransient<IWeatherService, OpenWeatherService>();

var app = builder.Build();

app.MapGet("weather/{city}",
    async ([FromRoute] string city, IWeatherService weatherService,
        IdGenerator idGen) =>
{
    Console.WriteLine(idGen.Id);
    var weather = await weatherService.GetCurrentWeatherAsync(city);
    return weather == null ? Results.NotFound() : Results.Ok(weather);
});

app.Run();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/avoiding-multiple-service-providers-53953214/?t=190)

---

## 6. Creating decorators

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/creating-decorators-53953215/) · 7:11

### Summary

Decorators allow you to add behavior to a service without modifying its original implementation, adhering to the Single Responsibility Principle.
By wrapping a core service with a decorator that implements the same interface, you can inject cross-cutting concerns like logging or performance monitoring.
In .NET, this is achieved by registering the base implementation as a concrete type and then registering the interface using a factory function that resolves the base type and wraps it in the decorator.

### Key concepts

- **Decorator Pattern**: A structural pattern that allows behavior to be added to an individual object, dynamically, without affecting the behavior of other objects from the same class.
- **Separation of Concerns**: Keeping business logic (like weather retrieval) separate from infrastructure concerns (like logging and timing).
- **Dependency Redirection**: Configuring the DI container to resolve an interface to a decorator, which in turn resolves the underlying implementation.
- **Factory Registration**: Using the `IServiceCollection` factory delegate to manually instantiate a decorator with its required dependencies.

### Lesson notes

In a standard .NET API, a controller typically depends on an interface for its business logic.
For example, a `WeatherForecastController` might depend on `IWeatherService` to retrieve data for a specific city.

```csharp
using ...

namespace Weather.Api.Controllers;

[ApiController]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherForecastController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [DurationLogger]
    [HttpGet("weather/{city}")]
    public async Task<IActionResult> GetCurrentWeather([FromRoute] string city)
    {
        var weather = await _weatherService.GetCurrentWeatherAsync(city);
        if (weather == null)
        {
            return NotFound();
        }
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/creating-decorators-53953215/?t=25)

While you could implement timing and logging logic directly inside the `OpenWeatherService` implementation using a `Stopwatch` and `try/finally` block, this violates the Single Responsibility Principle.
The service should only be concerned with communicating with the weather API.

```csharp
$"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={OpenWeatherA
            var httpClient = _httpClientFactory.CreateClient();

            var weatherResponse = await httpClient.GetAsync(url);
            if (weatherResponse.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            var weather = await weatherResponse.Content.ReadFromJsonAsync<WeatherResponse>();
            return weather;
        }
        finally
        {
            sw.Stop();
            _logger.LogInformation("Weather retrieval for city: {0}, took {1}ms",
                city, sw.ElapsedMilliseconds);
        }
    }
}
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/creating-decorators-53953215/?t=140)

A more maintainable approach is to create a decorator.
The `LoggedWeatherService` class implements the same `IWeatherService` interface and accepts an instance of `IWeatherService` in its constructor.
This allows it to act as a wrapper around the actual implementation.

```csharp
using System.Diagnostics;

namespace Weather.Api.Weather;

public class LoggedWeatherService : IWeatherService
{
    private readonly IWeatherService _weatherService; //<-- OpenWeatherService
    private readonly ILogger<IWeatherService> _logger;

    public LoggedWeatherService(IWeatherService weatherService,
        ILogger<IWeatherService> logger)
    {
        _weatherService = weatherService;
        _logger = logger;
    }

    public async Task<WeatherResponse?> GetCurrentWeatherAsync(string city)
    {
        var sw = Stopwatch.StartNew();
        try
        {
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/creating-decorators-53953215/?t=250)

The decorator delegates the actual work to the inner service while handling the timing and logging logic independently.

```csharp
public async Task<WeatherResponse?> GetCurrentWeatherAsync(string city)
{
    var sw = Stopwatch.StartNew();
    try
    {
        return await _weatherService.GetCurrentWeatherAsync(city);
    }
    finally
    {
        sw.Stop();
        _logger.LogInformation("Weather retrieval for city: {0}, took {1}ms",
            city, sw.ElapsedMilliseconds);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/creating-decorators-53953215/?t=405)

To configure this in the Dependency Injection container, you must register the concrete implementation (`OpenWeatherService`) as itself.
Then, register the interface (`IWeatherService`) using a factory delegate that resolves the concrete service and the logger to instantiate the decorator.

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddTransient<OpenWeatherService>();
builder.Services.AddTransient<IWeatherService>(provider =>
    new LoggedWeatherService(provider.GetRequiredService<OpenWeatherService>(),
        provider.GetRequiredService<ILogger<IWeatherService>>()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/creating-decorators-53953215/?t=415)

When the application resolves `IWeatherService`, it receives the `LoggedWeatherService`.
Internally, the decorator uses the resolved `OpenWeatherService` to perform the actual API calls, effectively decoupling the metric collection from the business logic.

---

## 7. The future of dependency injection

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-future-of-dependency-injection-53953216/) · 3:55

Source generators, introduced in C# 9 and .NET 5, enable a form of meta-programming where code is inspected and additional code is generated during compile time.
In the context of dependency injection, this allows for the creation of service providers that do not rely on runtime reflection.

### Key concepts

- **Source Generators**: Meta-programming introduced in C# 9 that allows inspecting code and generating additional source files during compilation.
- **Compile-time Safety**: Errors in DI configuration (e.g., missing dependencies) are caught during the build process rather than at runtime.
- **Performance**: Eliminates the overhead of reflection, resulting in faster startup times and near-instant service resolution.
- **Jab Library**: A third-party library that implements source-generated DI in a way that feels familiar to standard .NET DI.

### Lesson notes

The primary advantages of source-generated DI are compile-time checks and performance.
If a service is missing or incorrectly registered, the compiler will issue an error, preventing runtime failures.
Additionally, because the resolution logic is generated as standard C# code, application startup and service resolution are significantly faster because the application is simply calling code as if it were manually written.

To implement source-generated DI, libraries like `Jab` can be used.
First, define the interfaces and implementations:

```csharp
namespace DependencyInjectionFuture.ConsoleApp;

public class ConsoleWriter : IConsoleWriter
{
    public void WriteLine(string text)
    {
        Console.WriteLine(text);
    }
}

public interface IConsoleWriter
{
    void WriteLine(string text);
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-future-of-dependency-injection-53953216/?t=100)

Next, create a `partial` class to act as the service provider.
This class must be decorated with the `[ServiceProvider]` attribute.
Services are registered using attributes like `[Transient]`, `[Scoped]`, or `[Singleton]` directly on the class definition.

```csharp
using Jab;

namespace DependencyInjectionFuture.ConsoleApp;

[ServiceProvider]
[Transient(typeof(IConsoleWriter), typeof(ConsoleWriter))]
public partial class MyServiceProvider
{

}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-future-of-dependency-injection-53953216/?t=130)

Upon saving, the source generator automatically produces the implementation details for the partial class.
This generated code handles the instantiation and lifetime management of the services, including the creation of scopes and disposal of services.

```csharp
namespace DependencyInjectionFuture.ConsoleApp{

    public partial class MyServiceProvider : global::System.IDisposable,
        global::System.IServiceProvider,
        IServiceProvider<DependencyInjectionFuture.ConsoleApp.IConsoleWriter>
    {

        DependencyInjectionFuture.ConsoleApp.IConsoleWriter IServiceProvider<DependencyInjection.IConsoleWriter>.GetService()
        {
            DependencyInjectionFuture.ConsoleApp.ConsoleWriter service = new DependencyInjectionFuture.ConsoleApp.ConsoleWriter();
            TryAddDisposable(service);
            return service;
        }

        object global::System.IServiceProvider.GetService(global::System.Type type){
            if (type == typeof(DependencyInjectionFuture.ConsoleApp.IConsoleWriter))
                return ((IServiceProvider<DependencyInjectionFuture.ConsoleApp.IConsoleWriter>)this).GetService();
            return null;
        }

        private global::System.Collections.Generic.List<object> disposables;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-future-of-dependency-injection-53953216/?t=160)

Finally, the generated service provider can be used in the application.
It provides a type-safe `GetService<T>` method that resolves dependencies without the overhead of runtime reflection.

```csharp
using DependencyInjectionFuture.ConsoleApp;


var serviceProvider = new MyServiceProvider();

var consoleWriter = serviceProvider.GetService<IConsoleWriter>();

consoleWriter.WriteLine("Hi From Source Generated DI");
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-future-of-dependency-injection-53953216/?t=205)

While ASP.NET Core currently relies heavily on reflection-based DI, source-generated alternatives are likely to become more prominent in the future of .NET development due to their efficiency and safety.

---

## 8. Section recap

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953217/) · 0:47

### Summary

This lesson provides a comprehensive summary of advanced dependency injection techniques in .NET, including manual scope management, the nuances of the service locator pattern, and architectural patterns like decorators.
It also reviews best practices for avoiding common pitfalls such as captured dependencies and redundant service providers, while looking forward to the role of source generation in the future of DI.

### Key concepts

*   **Manual Scope Creation**: Understanding how to explicitly create and manage service scopes.
*   **Service Locator Pattern**: Recognizing why it is generally considered an anti-pattern, while identifying specific scenarios where specifying intent makes it acceptable.
*   **Dependency Integrity**: Strategies for avoiding captured dependencies and preventing the proliferation of multiple service providers.
*   **Decorator Pattern**: Implementing decorators elegantly using the built-in DI container.
*   **Source Generation**: The evolution of DI towards source-generated service providers for improved performance and compile-time safety.

### Lesson notes

This section explored several advanced techniques for mastering dependency injection in .NET applications.
The journey began with manual scope management, demonstrating how to create custom scopes to control the lifetime of services beyond the standard request-response cycle.

A significant portion of the section was dedicated to the Service Locator pattern.
While often labeled an anti-pattern because it hides dependencies and complicates testing, there are specific contexts where it can be used effectively, particularly when the implementation allows for explicit intent.

To ensure architectural integrity, the section covered how to avoid common mistakes such as captured dependencies—where a service with a longer lifetime (like a Singleton) holds onto a service with a shorter lifetime (like a Scoped service).
Additionally, the lessons emphasized the importance of maintaining a single source of truth by avoiding the creation of multiple service providers within the same application context.

For structural patterns, the section demonstrated how to implement decorators elegantly.
This allows for the addition of behavior to services without modifying their underlying implementation, all while leveraging the DI container to resolve the decorated chain.

Finally, the section looked toward the future of dependency injection in the .NET ecosystem.
Source-generated service providers represent a significant shift, moving resolution logic to compile-time to reduce overhead and improve startup performance.
The next section will build upon these foundations by introducing Scrutor, a library designed to enhance the capabilities of the built-in .NET dependency injection framework.
