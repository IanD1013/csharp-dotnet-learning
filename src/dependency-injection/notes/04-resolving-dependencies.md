# Resolving dependencies

> Course: [From Zero to Hero: Dependency Injection in .NET with C#](https://dometrain.com/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp/) · Chapter 4
> 15 lessons · ~25:09
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Resolving dependencies in different project types](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-different-project-types-53953161/) | 0:21 | [↓](#1-resolving-dependencies-in-different-project-types) |
| 2 | [Resolving dependencies from the constructor](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-the-constructor-53953162/) | 0:59 | [↓](#2-resolving-dependencies-from-the-constructor) |
| 3 | [Resolving dependencies from the method](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-the-method-53953163/) | 1:27 | [↓](#3-resolving-dependencies-from-the-method) |
| 4 | [Resolving dependencies in a console setup](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-a-console-setup-53953164/) | 1:17 | [↓](#4-resolving-dependencies-in-a-console-setup) |
| 5 | [Resolving dependencies from the HttpContext](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-the-httpcontext-53953165/) | 1:09 | [↓](#5-resolving-dependencies-from-the-httpcontext) |
| 6 | [Resolving dependencies from Action Filters as Attributes](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-action-filters-as-attributes-53953167/) | 2:42 | [↓](#6-resolving-dependencies-from-action-filters-as-attributes) |
| 7 | [Resolving dependencies from Service Filters](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-service-filters-53953168/) | 2:42 | [↓](#7-resolving-dependencies-from-service-filters) |
| 8 | [Resolving dependencies from Middleware](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-middleware-53953169/) | 2:32 | [↓](#8-resolving-dependencies-from-middleware) |
| 9 | [Resolving dependencies in Minimal APIs](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-minimal-apis-53953170/) | 3:13 | [↓](#9-resolving-dependencies-in-minimal-apis) |
| 10 | [Resolving dependencies in Razor Views & Pages](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-razor-views-pages-53953172/) | 1:28 | [↓](#10-resolving-dependencies-in-razor-views--pages) |
| 11 | [Resolving dependencies in Blazor](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-blazor-53953173/) | 1:00 | [↓](#11-resolving-dependencies-in-blazor) |
| 12 | [Resolving dependencies in gRPC Services](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-grpc-services-53953174/) | 1:16 | [↓](#12-resolving-dependencies-in-grpc-services) |
| 13 | [Resolving dependencies in Hosted Services](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-hosted-services-53953175/) | 2:38 | [↓](#13-resolving-dependencies-in-hosted-services) |
| 14 | [Resolving dependencies in service registration](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-service-registration-53953176/) | 1:54 | [↓](#14-resolving-dependencies-in-service-registration) |
| 15 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953180/) | 0:31 | [↓](#15-section-recap) |

---

## 1. Resolving dependencies in different project types

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-different-project-types-53953161/) · 0:21

### Summary

This lesson introduces the various patterns for resolving dependencies across different .NET project types, including Console applications, Web APIs, Minimal APIs, MVC, Razor Pages, Blazor, and gRPC.
While the underlying dependency injection container and registration process remain consistent, the specific entry points for resolution—such as manual service provider construction, constructor injection, or method parameter injection—vary to accommodate the specific requirements and lifecycles of each framework template.

### Key concepts

*   **Manual Resolution**: Required in non-hosted environments like Console applications where the service provider must be built and queried manually.
*   **Constructor Injection**: The standard pattern for framework-managed classes such as Web API/MVC Controllers, Razor Page models, and gRPC services.
*   **Parameter Injection**: A specialized pattern for Minimal API route handlers where dependencies are resolved directly from the method signature.
*   **Project-Specific Registrations**: Variations in how services are registered, such as the use of `AddServerSideBlazor` in Blazor Server or configuring a scoped `HttpClient` in Blazor WebAssembly.

### Lesson notes

Dependency resolution in .NET is highly dependent on the project type and the specific class requiring the service.
While the registration of services into the `IServiceCollection` is a shared fundamental, the retrieval mechanism changes based on whether the framework manages the instantiation of the class.

#### Console Applications

In a standard Console application, there is no built-in host to manage the service lifecycle automatically.
Developers must manually create a `ServiceCollection`, register dependencies, and call `BuildServiceProvider()` to create the container.
Services are then retrieved using `GetRequiredService<T>` or `GetService<T>`.

```csharp
using Microsoft.Extensions.DependencyInjection;
using ResolvingDeps.ConsoleApp;
using ResolvingDeps.ConsoleApp.Weather;

if (args.Length == 0)
{
    args = new string[1];
    args[0] = "London";
}

var services = new ServiceCollection();

services.AddSingleton<IWeatherService, OpenWeatherService>();
services.AddSingleton<Application>();

var serviceProvider = services.BuildServiceProvider();

var application = serviceProvider.GetRequiredService<Application>();

await application.RunAsync(args);
```

#### Minimal APIs

Minimal APIs simplify dependency resolution by allowing services to be injected directly as parameters in the route handler delegates.
The framework automatically resolves these parameters from the DI container when the endpoint is called.

```csharp
app.MapGet("weather", (ILogger<Program> logger) =>
{
    var weatherSummaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    var weather = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = weatherSummaries[Random.Shared.Next(weatherSummaries.Length)]
        }).ToArray();
    logger.LogInformation("Hi from logger.");
    return Results.Ok(weather);
});
```

#### Web API and MVC Controllers

For Controller-based projects, constructor injection is the primary mechanism.
The framework's controller factory identifies the dependencies defined in the constructor and resolves them before instantiating the controller.

```csharp
[ApiController]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet("weather")]
    public IEnumerable<WeatherForecast> Get()
    {
        // Implementation
    }
}
```

#### gRPC Services

gRPC services in .NET also utilize constructor injection.
The service implementation class inherits from a generated base class, and the DI container provides the required services when a gRPC call is routed to the implementation.

```csharp
public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;

    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }
}
```

#### Blazor Applications

Blazor Server and Blazor WebAssembly register services in `Program.cs`.
Blazor Server typically registers services like `WeatherForecastService` as Singletons or Scoped, while Blazor WebAssembly frequently registers a scoped `HttpClient` configured with the base address of the host environment.

```csharp
// Blazor WebAssembly example
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
```

---

## 2. Resolving dependencies from the constructor

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-the-constructor-53953162/) · 0:59

### Summary

Constructor injection is the primary and most straightforward method for resolving dependencies in .NET applications.
By defining required services as parameters in a class constructor, the built-in dependency injection container automatically provides the necessary instances at runtime, provided they have been registered in the application's service collection.

### Key concepts

- **Constructor Injection**: The standard pattern for requesting dependencies by declaring them as constructor parameters.
- **Automatic Resolution**: The .NET dependency injection container automatically fulfills constructor requirements during object instantiation.
- **Service Registration**: Dependencies must be registered in the service container (typically in `Program.cs`) to be resolvable.
- **Framework-Provided Services**: Certain services, such as `ILogger`, are automatically registered by ASP.NET Core and do not require manual configuration.

### Lesson notes

In .NET, the most common and default method for resolving services is through constructor injection.
This approach is standard even when not using a formal dependency injection framework, but it is fully automated within the .NET ecosystem.
To resolve a service within a class, such as a Web API controller, you simply define the service as a parameter in the class's constructor.

```csharp
private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering",
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet("weather")]
    [ServiceFilter(typeof(DurationLoggerFilter))]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-the-constructor-53953162/?t=25)

When a class is instantiated, the dependency injection framework automatically identifies the required dependencies and provides the appropriate instances.
For this to work, the service must be registered in the application's configuration, typically found in the `Program.cs` file (or `Startup.cs` in older project structures).

```csharp
using ResolvingDeps.WebApi.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DurationLoggerFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-the-constructor-53953162/?t=40)

In ASP.NET Core, many essential services are registered automatically by the framework.
For example, while you may not see an explicit registration for `ILogger` in `Program.cs`, it is configured behind the scenes, allowing it to be injected into controllers or other services immediately without additional setup.

---

## 3. Resolving dependencies from the method

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-the-method-53953163/) · 1:27

### Summary

ASP.NET Core allows for resolving dependencies directly within a method's parameters using the [FromServices] attribute, offering an alternative to standard constructor injection.
This approach is particularly beneficial when a dependency is only needed for a single action within a class, as it simplifies unit testing by removing the need to mock that dependency for other methods.
While useful for reducing test boilerplate, this technique should be used sparingly to maintain adherence to the Single Responsibility Principle and ensure that classes do not become overly complex.

### Key concepts

* Method-level dependency resolution in ASP.NET Core
* Using the [FromServices] attribute for action parameters
* Improving testability by isolating dependencies to specific use cases
* Balancing method injection with the Single Responsibility Principle (SRP)

### Lesson notes

In most ASP.NET Core applications, dependencies are resolved through constructor injection.
This pattern ensures that the required services are available to all methods within the controller or class.

```csharp
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering",
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet("weather")]
    [ServiceFilter(typeof(DurationLoggerFilter))]

    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-the-method-53953163/?t=25)

A challenge arises when a class contains multiple methods, but a specific dependency is only required by one of them.
With constructor injection, every unit test for every method in that class must provide a mock for that dependency, even if the method under test does not use it.

To optimize this, ASP.NET Core provides the `[FromServices]` attribute.
When applied to a method parameter, the framework resolves the dependency directly from the DI container only when that specific method is called.
This removes the dependency from the class constructor and limits its scope to the single action that requires it.

```csharp
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering",
    };

    [HttpGet("weather")]
    [ServiceFilter(typeof(DurationLoggerFilter))]

    public IEnumerable<WeatherForecast> Get(
        [FromServices] ILogger<WeatherForecastController> logger)
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-the-method-53953163/?t=40)

While this technique is useful for simplifying tests, it is generally considered an exception rather than the rule.
If a class has many methods and only one requires a specific service, it may be a sign that the class is handling too many responsibilities.
Following the Single Responsibility Principle often leads to smaller classes where constructor injection remains the most appropriate choice.
However, method-level injection is a valuable tool to have when refactoring or when dealing with specific architectural constraints.

---

## 4. Resolving dependencies in a console setup

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-a-console-setup-53953164/) · 1:17

### Summary

In .NET console applications, dependency resolution must be managed manually because they lack the automatic infrastructure found in ASP.NET Core.
This involves explicitly creating a ServiceCollection, registering dependencies, building a ServiceProvider, and manually resolving the entry-point service.
Once the top-level service is resolved, the DI container automatically handles constructor injection for all subsequent dependencies in the graph.

### Key concepts

- Manual `ServiceCollection` instantiation.
- Dependency registration via `AddSingleton`, `AddScoped`, or `AddTransient`.
- Materializing the container with `BuildServiceProvider`.
- Entry-point resolution using `GetRequiredService`.
- Constructor injection as the primary resolution mechanism.

### Lesson notes

To implement Dependency Injection in a console application, you must first include the `Microsoft.Extensions.DependencyInjection` NuGet package in your project file.
This package provides the necessary abstractions and default implementation for the DI container.

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net6.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="6.0.0" />
  </ItemGroup>

</Project>
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-a-console-setup-53953164/?t=30)

Unlike ASP.NET Core, which handles service resolution automatically through controllers, console applications require manual configuration of the service container.
You begin by instantiating a `ServiceCollection` and registering your application's services.
After registration, you call `BuildServiceProvider()` to create the `IServiceProvider` and then manually resolve the entry-point service.

```csharp
if (args.Length == 0)
{
    args = new string[1];
    args[0] = "London";
}

var services = new ServiceCollection();

services.AddSingleton<IWeatherService, OpenWeatherService>();
services.AddSingleton<Application>();

var serviceProvider = services.BuildServiceProvider();

var application = serviceProvider.GetRequiredService<Application>();

await application.RunAsync(args);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-a-console-setup-53953164/?t=20)

Once the root service is resolved, the DI container automatically cascades resolution down the dependency tree.
Any dependencies required by the `Application` class are injected through its constructor.
This automatic resolution continues for all nested dependencies registered in the container.

```csharp
namespace ResolvingDeps.ConsoleApp;

public class Application
{
    private readonly IWeatherService _weatherService;

    public Application(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    public async Task RunAsync(string[] args)
    {
        var city = args[0];
        var weather = await _weatherService.GetCurrentWeatherAsync(city);
        var serializedWeather = JsonSerializer.Serialize(weather, new Json
        {
            WriteIndented = true
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-a-console-setup-53953164/?t=70)

It is important to note that the `[FromServices]` attribute, commonly used in ASP.NET Core minimal APIs or controllers, is not supported in console applications.
This is because the wiring logic required for `[FromServices]` is specific to the ASP.NET Core framework.
In a console setup, all dependency resolution must occur via constructor injection.

---

## 5. Resolving dependencies from the HttpContext

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-the-httpcontext-53953165/) · 1:09

### Summary

In ASP.NET Core Web API, the HttpContext object provides access to the current request's services via the RequestServices property.
While this property exposes an IServiceProvider that can resolve any registered dependency, using it directly within a controller action is considered a bad practice.
It implements the Service Locator anti-pattern, which hides a method's true dependencies and complicates unit testing.
Developers should instead prefer constructor injection or the [FromServices] attribute for cleaner, more transparent dependency management.

### Key concepts

- HttpContext.RequestServices: The property used to access the scoped service provider for the current request.
- IServiceProvider: The interface returned by RequestServices that allows manual resolution of services.
- Service Locator Anti-pattern: The practice of resolving dependencies manually inside a method rather than declaring them explicitly.
- Testability: Manual resolution hides dependencies, making it difficult to mock them in unit tests.

### Lesson notes

In an ASP.NET Core Web API controller, every action has access to the HttpContext.
This object encapsulates all information regarding the specific HTTP request and response.

```csharp
_logger = logger;
    }

    [HttpGet("weather")]
    public IEnumerable<WeatherForecast> Get()
    {
        HttpContext

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-the-httpcontext-53953165/?t=10)

Within the HttpContext, there is a property called RequestServices.
This property is an implementation of IServiceProvider, which allows you to resolve services directly from the dependency injection container.

```csharp
_logger = logger;
    }

    [HttpGet("weather")]
    public IEnumerable<WeatherForecast> Get()
    {
        IServiceProvider serviceProvider = HttpContext.RequestServices;
        serviceProvider.G

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-the-httpcontext-53953165/?t=35)

Once you have access to the IServiceProvider, you can call methods like GetService or GetRequiredService to retrieve any registered dependency.

#### Why this is a bad practice

While resolving dependencies from HttpContext.RequestServices is possible, it is generally considered a bad practice for several reasons:

1. **Hides Dependencies**: By resolving services inside the method body, the method's signature no longer reflects what it actually needs to function. This makes the code harder to understand and maintain.
2. **Reduced Testability**: It becomes significantly harder to unit test the controller action because you must mock the entire HttpContext and its service provider, rather than simply passing in a mock of the specific service.
3. **Service Locator Pattern**: This approach is a form of the Service Locator anti-pattern, where the component "reaches out" to find its dependencies instead of having them provided.

#### Recommended Alternatives

Instead of using RequestServices, you should use one of the following standard dependency injection techniques:
- **Constructor Injection**: Inject the required service into the controller's constructor and store it in a private field.
- **[FromServices] Attribute**: If a service is only needed for a specific action, use the [FromServices] attribute on the action parameter.

```csharp
public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet("weather")]
    public IEnumerable<WeatherForecast> Get()
    {
        IServiceProvider serviceProvider = HttpContext.RequestServices;

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-the-httpcontext-53953165/?t=45)

---

## 6. Resolving dependencies from Action Filters as Attributes

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-action-filters-as-attributes-53953167/) · 2:42

### Summary

This lesson explains how to handle dependencies within ASP.NET Core Action Filters when used as attributes.
Since attributes only support constant parameters, standard constructor injection is not possible.
The lesson demonstrates a workaround using the Service Locator pattern to resolve services from `HttpContext.RequestServices` while cautioning that this is generally considered an anti-pattern.

### Key concepts

* **IAsyncActionFilter**: An interface used to create filters that surround action execution, allowing code to run before and after an action.
* **Attribute Constraints**: Attributes in C# only allow constant parameters, which prevents the direct injection of services via the constructor when the attribute is applied to a controller or action.
* **Service Locator Pattern**: A technique where services are manually requested from a service provider (in this case, `HttpContext.RequestServices`) rather than being injected.
* **Anti-pattern Warning**: Using the Service Locator pattern is generally discouraged and should only be used when no other alternatives are available.

### Lesson notes

An action filter can be implemented as an attribute to measure the execution time of a request.
By implementing `IAsyncActionFilter`, the filter can execute logic before the request starts and after it completes using a `try...finally` block.

```csharp
namespace ResolvingDeps.WebApi.Attributes;

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
            var text = $"Request completed in {sw.ElapsedMilliseconds}ms";
            Console.WriteLine(text);
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-action-filters-as-attributes-53953167/?t=10)

While `Console.WriteLine` works for simple demonstrations, production code should use `ILogger`.
However, attempting to use standard constructor injection within an attribute leads to significant limitations.

```csharp
namespace ResolvingDeps.WebApi.Attributes;

public class DurationLoggerAttribute : Attribute, IAsyncActionFilter
{
    private readonly ILogger<DurationLoggerAttribute> _logger;

    public DurationLoggerAttribute(ILogger<DurationLoggerAttribute> logger)
    {
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var sw = Stopwatch.StartNew();
        try
        {
            await next();
        }
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-action-filters-as-attributes-53953167/?t=70)

Attributes can only accept constant parameters.
Because a service like `ILogger` is not a constant, it cannot be passed into the attribute's constructor when applying it to a controller action.

```csharp
public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet("weather")]
    [DurationLogger(new Logger<DurationLoggerAttribute>(null))]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-action-filters-as-attributes-53953167/?t=85)

To resolve this, you can use the Service Locator pattern.
By accessing the `HttpContext` from the `ActionExecutingContext`, you can retrieve the `RequestServices` (the `IServiceProvider` for the current scope) and manually resolve the required service.

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
            var serviceProvider = context.HttpContext.RequestServices;
            var logger = serviceProvider.GetRequiredService<ILogger<DurationLoggerAttribute>>();
            var text = $"Request completed in {sw.ElapsedMilliseconds}ms";
            logger.LogInformation(text);
            //Console.WriteLine(text);
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-action-filters-as-attributes-53953167/?t=140)

Although this approach works and allows for structured logging, it is considered an anti-pattern.
Manual service resolution bypasses the benefits of dependency injection and should be avoided if better alternatives exist.

---

## 7. Resolving dependencies from Service Filters

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-service-filters-53953168/) · 2:42

This lesson demonstrates how to transition from using the Service Locator pattern within action filters to a more robust dependency injection approach using Service Filters.
By implementing the IAsyncActionFilter interface and registering the filter in the DI container, developers can utilize constructor injection within filters, significantly improving code maintainability and testability compared to resolving services manually from HttpContext.

### Key concepts

- Limitations of standard attributes regarding constructor injection.
- The Service Locator anti-pattern in filters using `HttpContext.RequestServices`.
- Implementing the `IAsyncActionFilter` interface for custom filter logic.
- Registering filters as services in the IServiceCollection.
- Applying filters via the `[ServiceFilter]` attribute.

### Lesson notes

When implementing action filters as attributes, developers often encounter a limitation: attributes are metadata and do not support constructor injection from the dependency injection (DI) container.
A common but suboptimal workaround is to use the Service Locator pattern by accessing the service provider through the `HttpContext` within the filter's execution context.

```csharp
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
            var serviceProvider = context.HttpContext.RequestServices;
            var logger = serviceProvider.GetRequiredService<ILogger<DurationLoggerAttribute>>();
            var text = $"Request completed in {sw.ElapsedMilliseconds}ms";
            logger.LogInformation(text);
            //Console.WriteLine(text);
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-service-filters-53953168/?t=10)

This approach is problematic because it hides dependencies within the method body, making the code harder to reason about and significantly more difficult to unit test.
Testing such a filter requires mocking the `HttpContext`, the `ActionExecutingContext`, and the service provider itself.

#### Implementing Service Filters

A better alternative is to use a Service Filter.
Instead of creating an attribute that contains the logic, you create a class that implements the `IAsyncActionFilter` interface.
This allows the filter to participate fully in the DI lifecycle, enabling constructor injection for required services like loggers.

```csharp
namespace ResolvingDeps.WebApi.Filters;

public class DurationLoggerFilter : IAsyncActionFilter
{
    private readonly ILogger<DurationLoggerFilter> _logger;

    public DurationLoggerFilter(ILogger<DurationLoggerFilter> logger)
    {
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var sw = Stopwatch.StartNew();
        try
        {
            await next();
        }
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-service-filters-53953168/?t=55)

The implementation logic remains similar to the attribute-based approach, but the dependencies are now explicitly declared in the constructor and provided by the container.

```csharp
public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var sw = Stopwatch.StartNew();
        try
        {
            await next();
        }
        finally
        {
            var text = $"Request completed in {sw.ElapsedMilliseconds}ms";
            _logger.LogInformation(text);
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-service-filters-53953168/?t=70)

#### Registration and Application

Because the filter now relies on constructor injection, it must be registered in the DI container.
Typically, these filters are registered with a scoped lifetime in `Program.cs`.

```csharp
using ResolvingDeps.WebApi.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DurationLoggerFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-service-filters-53953168/?t=85)

To apply this filter to a controller or a specific action, you use the `[ServiceFilter]` attribute and specify the type of the filter class.
This tells ASP.NET Core to resolve the filter instance from the DI container at runtime, ensuring all its dependencies are properly injected.

```csharp
public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet("weather")]
    
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-service-filters-53953168/?t=100)

In the example above, the previous `[DurationLogger]` attribute is replaced with `[ServiceFilter(typeof(DurationLoggerFilter))]`.
This approach ensures that the filter is fully testable, as you can now simply pass a mocked logger into the filter's constructor during unit testing.

---

## 8. Resolving dependencies from Middleware

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-middleware-53953169/) · 2:32

**Summary**

This lesson demonstrates how to implement custom middleware in ASP.NET Core to handle cross-cutting concerns like request timing and logging.
It highlights the standard pattern for middleware construction, the proper way to inject dependencies via the constructor rather than manual service resolution from HttpContext, and the importance of middleware ordering within the request pipeline.

**Key concepts**

* Middleware structure and the `RequestDelegate`.
* The `InvokeAsync` method and `HttpContext`.
* Constructor injection for services within middleware.
* Middleware registration using `app.UseMiddleware<T>`.
* The significance of middleware ordering in the pipeline.

**Lesson notes**

Middleware in ASP.NET Core provides a way to execute logic during the request pipeline.
Unlike filters which are typically applied to specific actions or controllers, middleware can be applied globally to all requests.
A standard middleware class requires a `RequestDelegate` in its constructor and an `InvokeAsync` method that accepts an `HttpContext`.

```csharp
namespace ResolvingDeps.WebApi.Middlewares;

public class DurationLoggerMiddleware
{
    private readonly RequestDelegate _next;

    public DurationLoggerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var sw = Stopwatch.StartNew();
        try
        {
            await _next(context);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-middleware-53953169/?t=25)

To implement logic that surrounds a request, such as timing its duration, you wrap the call to the next delegate in a try-finally block.
This ensures that the logic executes both before and after the subsequent components in the pipeline.

```csharp
public DurationLoggerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var sw = Stopwatch.StartNew();
        try
        {
            await _next(context);
        }
        finally
        {
            var text = $"Request completed in {sw.ElapsedMilliseconds}ms";
            // var logger = context.HttpContext.RequestServices.GetRequired
            // logger.LogInformation(text);
            Console.WriteLine(text);
        }
    }
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-middleware-53953169/?t=40)

While it is possible to resolve services manually from `HttpContext.RequestServices`, the preferred and most idiomatic approach is to use constructor injection.
You can inject any registered service, such as an `ILogger`, directly into the middleware's constructor alongside the `RequestDelegate`.

```csharp
public class DurationLoggerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<DurationLoggerMiddleware> _logger;

    public DurationLoggerMiddleware(RequestDelegate next,
        ILogger<DurationLoggerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var sw = Stopwatch.StartNew();
        try
        {
            await _next(context);
        }
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-middleware-53953169/?t=145)

Middleware must be registered in the `Program.cs` file using the `UseMiddleware<T>` extension method on the `WebApplication` instance.
Unlike service registration where order generally does not matter, the order of middleware registration is critical as it defines the execution sequence of the request pipeline.
Placing a timing middleware at the very top ensures it measures the duration of the entire request, including all subsequent middleware and the final action execution.

```csharp
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DurationLoggerFilter>();

var app = builder.Build();

app.UseMiddleware<DurationLoggerMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-middleware-53953169/?t=115)

---

## 9. Resolving dependencies in Minimal APIs

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-minimal-apis-53953170/) · 3:13

### Summary

Minimal APIs in .NET provide a streamlined way to resolve dependencies by injecting them directly into route handler delegates.
Unlike the traditional controller-based approach, Minimal APIs automatically detect and resolve services from the dependency injection container, often eliminating the need for explicit attributes like [FromServices].
This lesson explores how to inject services like ILogger into endpoints and highlights the importance of understanding service lifetimes and scopes when resolving dependencies at the application level versus the request level.

### Key concepts

* **Delegate Parameter Injection**: Services are passed directly into the lambda or method mapped to a route.
* **Implicit Resolution**: The framework automatically identifies services in the container without requiring the `[FromServices]` attribute.
* **Request Scoping**: Dependencies resolved within the route delegate are scoped to the specific HTTP request.
* **Application vs. Request Scope**: Resolving services via `app.Services` (Application scope) can lead to issues if those services are intended to be scoped to a request.
* **Middleware Consistency**: Middleware registration remains consistent with the standard ASP.NET Core approach using `app.UseMiddleware<T>()`.

### Lesson notes

Minimal APIs, introduced in .NET 6, simplify the process of building APIs by allowing developers to define endpoints and their logic in a more concise manner.
One of the key features of Minimal APIs is how they handle dependency injection.
The standard middleware registration remains consistent with other ASP.NET Core templates; you can still use `app.UseMiddleware<T>()` to capture requests across your mapped endpoints.

```csharp
using ResolvingDeps.MinimalApi;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("weather", () =>
{
    var weatherSummaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering",
    };

    var weather = Enumerable.Range(1, 5).Select(index => new WeatherForecast
    {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = weatherSummaries[Random.Shared.Next(weatherSummaries.Length)]
    }).ToArray();

    return Results.Ok(weather);
});
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-minimal-apis-53953170/?t=25)

In Minimal APIs, you can resolve dependencies by adding them as parameters to the delegate (the lambda function) of your route handler.
For example, if you want to use a logger within a `MapGet` endpoint, you can simply include `ILogger<Program>` in the parameter list.

```csharp
var app = builder.Build();

app.MapGet("weather", (ILogger<Program> _logger) =>
{
    var weatherSummaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering",
    };

    var weather = Enumerable.Range(1, 5).Select(index => new WeatherForecast
    {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = weatherSummaries[Random.Shared.Next(weatherSummaries.Length)]
    }).ToArray();

    _logger.LogInformation("Hi from logger.");
    return Results.Ok(weather);
});

app.Run();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-minimal-apis-53953170/?t=70)

The Minimal API framework is "smart" enough to look into the dependency injection container—which is populated during the `WebApplication.CreateBuilder(args)` phase—and automatically provide the requested service.
While earlier versions or other patterns might require the `[FromServices]` attribute to be explicit, it is not strictly necessary here as the framework detects the service type automatically.

It is technically possible to resolve services manually using the `app.Services` property, which provides access to the `IServiceProvider`.

```csharp
using Microsoft.AspNetCore.Mvc;
using ResolvingDeps.MinimalApi;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();

app.MapGet("weather", (ILogger<Program> logger) =>
{
    var weatherSummaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering",
    };

    var weather = Enumerable.Range(1, 5).Select(index => new WeatherForecast
    {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = weatherSummaries[Random.Shared.Next(weatherSummaries.Length)]
    }).ToArray();
    logger.LogInformation("Hi from logger.");
    return Results.Ok(weather);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-minimal-apis-53953170/?t=145)

However, manual resolution at the application level can be dangerous.
When you resolve a service using `app.Services.GetRequiredService<T>()` outside of a request delegate, you are resolving it at the application scope.

```csharp
var logger = app.Services.GetRequiredService<ILogger<Program>>();

app.MapGet("weather", () =>
{
    var weatherSummaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering",
    };

    var weather = Enumerable.Range(1, 5).Select(index => new WeatherForecast
    {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = weatherSummaries[Random.Shared.Next(weatherSummaries.Length)]
    }).ToArray();
    logger.LogInformation("Hi from logger.");
    return Results.Ok(weather);
});

app.Run();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-minimal-apis-53953170/?t=160)

This is a common mistake.
Services resolved at the application level persist for the lifetime of the application, whereas services injected into the `MapGet` delegate are resolved within a request scope.
Using an application-scoped service where a request-scoped service is expected can lead to unexpected behavior or resource leaks.
The correct and safest approach is to inject dependencies directly into the route handler parameters.

```csharp
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("weather", (ILogger<Program> logger) =>
{
    var weatherSummaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering",
    };

    var weather = Enumerable.Range(1, 5).Select(index => new WeatherForecast
    {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = weatherSummaries[Random.Shared.Next(weatherSummaries.Length)]
    }).ToArray();
    logger.LogInformation("Hi from logger.");
    return Results.Ok(weather);
});

app.Run();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-minimal-apis-53953170/?t=175)

---

## 10. Resolving dependencies in Razor Views & Pages

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-razor-views-pages-53953172/) · 1:28

### Summary

This lesson demonstrates how to resolve dependencies directly within Razor Views and Razor Pages using the @inject directive.
While constructor injection is the standard for classes like controllers or PageModels, the @inject directive allows services to be accessed directly in the markup for scenarios where data or logic is needed specifically for the UI rendering.

### Key concepts

* The @inject directive syntax: @inject <Type> <Name>.
* Injecting services into MVC Views (.cshtml).
* Injecting services into Razor Pages.
* Accessing service members within Razor markup using the @ symbol.
* Maintaining standard constructor injection for supporting classes.

### Lesson notes

In ASP.NET Core MVC and Razor Pages, you can resolve services directly within your markup files.
This is particularly useful when a view requires a service that is not provided by the controller or the PageModel.

Consider a service class designed to provide a message:

```csharp
namespace ResolvingDeps.Mvc;

public class ServiceToInject
{
    public string Message => "I was injected!";
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-razor-views-pages-53953172/?t=25)

To resolve this service in an MVC View (such as Index.cshtml), use the @inject directive at the top of the file.
You must specify the service type and provide a name for the instance.

```razor
@{
    ViewData["Title"] = "Home Page";
}
@inject ServiceToInject ServiceToInject

<div class="text-center">
    <h1 class="display-4">Welcome</h1>
    <p>Learn about <a href="https://docs.microsoft.com/aspnet/core">building Web apps with ASP.
    <p>@ServiceToInject.Message</p>
</div>
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-razor-views-pages-53953172/?t=55)

Once the service is injected, its properties and methods can be accessed anywhere in the Razor file using the @ symbol followed by the instance name.

This approach is identical for Razor Pages.
Although Razor Pages typically utilize a PageModel where dependencies are resolved via constructor injection, the @inject directive remains available for use directly within the .cshtml file.

```razor
@page
@model IndexModel
@{
    ViewData["Title"] = "Home Page";
}

<div class="text-center">
    <h1 class="display-4">Welcome</h1>
    <p>Learn about <a href="https://docs.microsoft.com/aspnet/core">building Web apps with ASP.
</div>
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-razor-views-pages-53953172/?t=80)

Using the @inject directive in views does not impact the rest of the project's architecture; controllers and other classes should continue to resolve their dependencies through their constructors.

---

## 11. Resolving dependencies in Blazor

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-blazor-53953173/) · 1:00

Blazor utilizes a consistent approach to dependency injection across both Server and WebAssembly models, mirroring the patterns found in ASP.NET Core MVC and Razor Pages.
By using the @inject directive within Razor components, developers can easily resolve services like HttpClient or custom business logic services.
Standard C# classes within a Blazor application continue to use traditional constructor injection for dependency resolution, ensuring a unified development experience across the .NET ecosystem.

### Key concepts

*   **@inject Directive**: The primary mechanism for resolving dependencies within Razor components.
*   **Model Consistency**: Blazor Server and Blazor WebAssembly share the same dependency injection syntax.
*   **Constructor Injection**: Standard C# classes within Blazor projects still utilize constructor injection for service resolution.
*   **Parity with ASP.NET Core**: The DI patterns in Blazor are designed to be familiar to developers coming from MVC or Razor Pages.

### Lesson notes

Blazor employs a dependency injection mechanism that is highly consistent with ASP.NET Core MVC and Razor Pages.
This consistency simplifies the transition between different project types within the .NET ecosystem.
In Razor components, dependencies are resolved using the `@inject` directive.
This directive specifies the service type and the name of the property through which the service will be accessed.

```razor
@page "/fetchdata"
@inject HttpClient Http

<PageTitle>Weather forecast</PageTitle>

<h1>Weather forecast</h1>

<p>This component demonstrates fetching data from the server.</p>

@if (forecasts == null)
{
    <p>
        <em>Loading...</em>
    </p>
}
else
{
    <table class="table">
        <thead>
            <tr>
                <th>Date</th>
                <th>Temp. (C)</th>
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-blazor-53953173/?t=55)

This approach is identical in both Blazor Server and Blazor WebAssembly.
For instance, in a Blazor WebAssembly application, the `HttpClient` is commonly injected to perform data fetching operations within the component's lifecycle methods, such as `OnInitializedAsync`.

```razor
<td>@forecast.Summary</td>
            </tr>
        }
        </tbody>
    </table>
}

@code {
    private WeatherForecast[]? forecasts;

    protected override async Task OnInitializedAsync()
    {
        forecasts = await Http.GetFromJsonAsync<WeatherForecast[]>("sample-data/weather.json");
    }

    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string? Summary { get; set; }
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-blazor-53953173/?t=40)

In Blazor Server, you might inject a specific service class (like a `WeatherForecastService`) instead of `HttpClient`.
The usage within the `@code` block remains the same, where the injected property is used to call service methods to retrieve data.

```razor
<td>@forecast.Summary</td>
            </tr>
        }
        </tbody>
    </table>
}

@code {
    private WeatherForecast[]? forecasts;

    protected override async Task OnInitializedAsync()
    {
        forecasts = await ForecastService.GetForecastAsync(DateTime.Now);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-blazor-53953173/?t=25)

While Razor components use the `@inject` directive, standard C# classes within a Blazor project continue to use constructor injection.
This ensures that the core principles of dependency injection are maintained throughout the entire application architecture.

---

## 12. Resolving dependencies in gRPC Services

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-grpc-services-53953174/) · 1:16

**Summary**

gRPC services in .NET leverage the built-in dependency injection container through standard constructor injection.
By defining service contracts in Protobuf files and inheriting from the generated base classes, developers can easily inject dependencies like loggers or business services.
The integration is finalized in the application startup by adding gRPC services to the container and mapping the service endpoints, which ensures that all cascaded dependencies are correctly resolved at runtime.

**Key concepts**

* Protobuf (.proto) files as templates for service contracts.
* Inheritance from generated base classes (e.g., GreeterBase).
* Standard constructor injection for service dependencies.
* Registration using AddGrpc() in the service collection.
* Endpoint mapping via MapGrpcService<T>() to enable DI resolution.

**Lesson notes**

gRPC is an increasingly popular framework for high-performance internal service communication, particularly where contract-based communication is preferred.
In a .NET gRPC project, the service contract is defined using a Protocol Buffers (protobuf) file.
This file acts as a template to generate the necessary C# base classes and message types behind the scenes.

```protobuf
syntax = "proto3";

option csharp_namespace = "ResolvingDeps.GrpcService";

package greet;

// The greeting service definition.
service Greeter {
  // Sends a greeting
  rpc SayHello (HelloRequest) returns (HelloReply);
}

// The request message containing the user's name.
message HelloRequest {
  string name = 1;
}

// The response message containing the greetings.
message HelloReply {
  string message = 1;
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-grpc-services-53953174/?t=40)

The service implementation inherits from the generated base class (e.g., `Greeter.GreeterBase`).
Dependency injection in gRPC services is straightforward and follows the same pattern as other .NET components: dependencies are requested via the class constructor.
Microsoft's implementation ensures that you do not need to manually resolve services; the standard constructor pattern remains unchanged.

```csharp
namespace ResolvingDeps.GrpcService.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;

    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-grpc-services-53953174/?t=55)

To enable dependency resolution, the gRPC services must be registered and mapped within `Program.cs`.
The `AddGrpc` method adds the required infrastructure to the service collection.
The `MapGrpcService<T>` method then initializes the service endpoint.
This mapping ensures that any dependencies required by the service, or any dependencies cascading from those, are automatically resolved from the DI container when the service is invoked.

```csharp
using ResolvingDeps.GrpcService.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.mic

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC cli");

app.Run();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-grpc-services-53953174/?t=70)

---

## 13. Resolving dependencies in Hosted Services

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-hosted-services-53953175/) · 2:38

### Summary

In ASP.NET Core, hosted services are used to perform background operations such as processing message queues or executing timed tasks.
By inheriting from the BackgroundService base class, developers can implement the ExecuteAsync method to define long-running logic that respects the application's lifecycle via a CancellationToken.
These services are registered in the IServiceCollection using AddHostedService, and they support standard dependency injection, allowing for the resolution of services like ILogger through the constructor.

### Key concepts

- Background tasks using IHostedService and BackgroundService.
- Implementing the ExecuteAsync override for continuous execution.
- Managing service lifecycles with CancellationToken.
- Registering hosted services with AddHostedService<T>().
- Resolving dependencies via constructor injection in background services.

### Lesson notes

Hosted services (or background services) are a powerful feature in ASP.NET Core for running tasks in the background, such as queue consumers or periodic timers.
While you can implement the IHostedService interface directly, the BackgroundService base class is typically preferred for continuous background tasks as it provides a simplified implementation pattern.

To create a background service, define a class that inherits from BackgroundService and override the ExecuteAsync method.

```csharp
namespace ResolvingDeps.WebApi.HostedServices;

public class BackgroundTicker : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-hosted-services-53953175/?t=70)

The ExecuteAsync method receives a CancellationToken which should be used to monitor when the application is shutting down.
A common pattern is to use a while loop that checks IsCancellationRequested.

```csharp
namespace ResolvingDeps.WebApi.HostedServices;

public class BackgroundTicker : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000);
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-hosted-services-53953175/?t=85)

Inside the loop, you can perform the background logic.
It is important to pass the stoppingToken to any asynchronous methods, such as Task.Delay, to ensure the service responds promptly to shutdown requests.

```csharp
namespace ResolvingDeps.WebApi.HostedServices;

public class BackgroundTicker : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine($"Hi from {}");
            await Task.Delay(1000, stoppingToken);
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-hosted-services-53953175/?t=100)

To activate the hosted service, it must be registered in the dependency injection container using the AddHostedService extension method on IServiceCollection.

```csharp
// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DurationLoggerFilter>();
builder.Services.AddHostedService<BackgroundTicker>();

var app = builder.Build();

app.UseMiddleware<DurationLoggerMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-hosted-services-53953175/?t=115)

Hosted services fully support dependency injection.
Instead of using Console.WriteLine, you should inject dependencies like ILogger<T> through the constructor.
The DI container will resolve these dependencies when the hosted service is instantiated at application startup.

```csharp
namespace ResolvingDeps.WebApi.HostedServices;

public class BackgroundTicker : BackgroundService
{
    private readonly ILogger<BackgroundTicker> _logger;

    public BackgroundTicker(ILogger<BackgroundTicker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation($"Hi from {nameof(BackgroundTicker)}");
            await Task.Delay(1000, stoppingToken);
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-hosted-services-53953175/?t=145)

---

## 14. Resolving dependencies in service registration

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-service-registration-53953176/) · 1:54

### Summary

In .NET, while the dependency injection container typically handles service instantiation automatically, developers can manually resolve dependencies during the registration process.
By utilizing factory overloads for methods such as AddScoped, you can access an IServiceProvider instance to explicitly retrieve required services and pass them into a class constructor.
This approach provides the flexibility needed for complex initialization logic or when specific manual configuration is required before a service is added to the container.

### Key concepts

- Factory overloads for service registration.
- Manual dependency resolution using IServiceProvider.
- The GetRequiredService<T> method.
- Scoped execution within the registration factory.
- Custom instantiation of services with existing dependencies.

### Lesson notes

In standard .NET dependency injection, services are often registered by simply providing the implementation type.
The container then uses reflection to identify the constructor and resolve dependencies automatically.

```csharp
// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<DurationLoggerFilter>();

builder.Services.AddHostedService<BackgroundTicker>();

var app = builder.Build();

app.UseMiddleware<DurationLoggerMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-service-registration-53953176/?t=25)

However, there are cases where you need to resolve services in a specific way or perform manual instantiation.
This is done by using a factory delegate that provides an `IServiceProvider` (often named `provider`).
This provider is scoped to the registration and represents the service provider that will be materialized when the service is requested.

```csharp
// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(provider =>
{

});

builder.Services.AddHostedService<BackgroundTicker>();

var app = builder.Build();

app.UseMiddleware<DurationLoggerMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-service-registration-53953176/?t=40)

Within this factory, you can use the `provider` to resolve existing services using `GetRequiredService<T>()`.
This allows you to manually fetch dependencies—such as an `ILogger`—and pass them into the constructor of the service you are registering.
This technique is essential for advanced scenarios where you need to perform custom logic during the instantiation of a service.

```csharp
// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(provider =>
{
    var logger = provider.GetRequiredService<ILogger<DurationLoggerFilter>>();
    return new DurationLoggerFilter(logger);
});

builder.Services.AddHostedService<BackgroundTicker>();

var app = builder.Build();

app.UseMiddleware<DurationLoggerMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-service-registration-53953176/?t=100)

---

## 15. Section recap

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953180/) · 0:31

### Summary

This lesson summarizes the various methods for resolving dependencies across the .NET ecosystem, covering project types such as Console applications, Web APIs, MVC, Razor Pages, Blazor, gRPC, and Hosted Services.
It emphasizes that service resolution can occur within UI components, background tasks, and even during the service registration phase itself.

### Key concepts

* Dependency resolution in Console applications and Web APIs.
* Integration with MVC, Razor Pages, and Razor Views.
* Service injection in Blazor components and gRPC services.
* Utilizing dependencies within Hosted Services for background processing.
* Resolving services during the registration process.

### Lesson notes

The section provided a broad overview of how to resolve services within different .NET project templates.
The flexibility of the built-in Dependency Injection (DI) framework allows for seamless integration across various application architectures.

Key areas covered include:
* **Web and UI Frameworks**: Implementation details for resolving dependencies in Web APIs, MVC controllers, and Razor Pages. This includes the use of the `@inject` directive within Razor Views and Blazor components to bring services directly into the UI layer.
* **Communication and Background Tasks**: Techniques for resolving dependencies in gRPC services and Hosted Services, ensuring that background logic has access to required application services.
* **Console and Registration Logic**: Manual resolution in Console applications and the ability to resolve dependencies even within the service registration logic itself, allowing for dynamic configuration based on other registered services.
