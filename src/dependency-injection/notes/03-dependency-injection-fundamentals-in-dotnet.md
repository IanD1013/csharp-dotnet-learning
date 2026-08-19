# Dependency Injection fundamentals in .NET

> Course: [From Zero to Hero: Dependency Injection in .NET with C#](https://dometrain.com/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp/) · Chapter 3
> 15 lessons · ~46:02
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Introduction](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/introduction-53953101/) | 0:38 | [↓](#1-introduction) |
| 2 | [The simplest setup with Dependency Injection](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-simplest-setup-with-dependency-injection-53953102/) | 6:34 | [↓](#2-the-simplest-setup-with-dependency-injection) |
| 3 | [The ServiceCollection](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-servicecollection-53953103/) | 2:00 | [↓](#3-the-servicecollection) |
| 4 | [The ServiceProvider](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-serviceprovider-53953104/) | 1:46 | [↓](#4-the-serviceprovider) |
| 5 | [All of the above, in an API](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/all-of-the-above-in-an-api-53953105/) | 5:27 | [↓](#5-all-of-the-above-in-an-api) |
| 6 | [The different types of dependency lifetimes](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-different-types-of-dependency-lifetimes-53953106/) | 4:30 | [↓](#6-the-different-types-of-dependency-lifetimes) |
| 7 | [The Transient lifetime](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-transient-lifetime-53953107/) | 2:08 | [↓](#7-the-transient-lifetime) |
| 8 | [The Singleton lifetime](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-singleton-lifetime-53953108/) | 2:08 | [↓](#8-the-singleton-lifetime) |
| 9 | [The Scoped lifetime](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-scoped-lifetime-53953109/) | 2:31 | [↓](#9-the-scoped-lifetime) |
| 10 | [GetService vs GetRequiredService](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/getservice-vs-getrequiredservice-53953110/) | 3:38 | [↓](#10-getservice-vs-getrequiredservice) |
| 11 | [Generic-based registration vs implementation-based registration](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/generic-based-registration-vs-implementation-based-registration-53953111/) | 3:31 | [↓](#11-generic-based-registration-vs-implementation-based-registration) |
| 12 | [Registration approaches](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registration-approaches-53953112/) | 6:04 | [↓](#12-registration-approaches) |
| 13 | [The Startup.cs and changes after .NET 6](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-startupcs-and-changes-after-net-6-53953113/) | 2:09 | [↓](#13-the-startupcs-and-changes-after-net-6) |
| 14 | [Third party libraries](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/third-party-libraries-53953114/) | 1:49 | [↓](#14-third-party-libraries) |
| 15 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953115/) | 1:09 | [↓](#15-section-recap) |

---

## 1. Introduction

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/introduction-53953101/) · 0:38

### Summary

This lesson introduces the fundamental concepts of Dependency Injection (DI) in .NET, setting the stage for a deep dive into how the system works.
It focuses on deconstructing the DI container to build a solid technical foundation and establishes the terminology used throughout the remainder of the course.

### Key concepts

*   **Foundational DI Concepts**: Understanding the basic mechanics of how dependencies are managed in .NET.
*   **System Deconstruction**: Breaking down the DI framework to understand its internal components.
*   **Terminology Standardization**: Establishing a common language for discussing services, lifetimes, and containers.
*   **Architectural Foundation**: Creating a basis for advanced implementation strategies in subsequent sections.

### Lesson notes

The "Dependency Injection fundamentals in .NET" section provides a comprehensive overview of how DI is implemented and managed within the ecosystem.
This introductory lesson emphasizes the importance of understanding the core mechanics before moving on to advanced usage.

The approach taken in this section involves deconstructing the entire DI system.
By analyzing the individual parts that make up the framework, developers can better understand how to build on top of this foundation.
This deconstruction is essential for moving beyond simple usage to a more professional implementation of the pattern.

Even for those already familiar with Dependency Injection, this introduction is vital because it establishes the specific terminology and conceptual framework that will be referenced in all following lessons.
Skipping these fundamentals may lead to confusion when more complex scenarios are introduced later in the course.

---

## 2. The simplest setup with Dependency Injection

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-simplest-setup-with-dependency-injection-53953102/) · 6:34

### Summary

This lesson demonstrates the fundamental mechanics of Dependency Injection (DI) in .NET using a console application to isolate the core components from the complexities of ASP.NET Core.
It introduces the ServiceCollection for registering dependencies, the ServiceProvider as the functional DI container, and the process of automatic constructor resolution.
By defining mappings between interfaces and concrete implementations, the container can recursively resolve a graph of dependencies, ensuring that required services are provided to classes at runtime without manual instantiation.

### Key concepts

* **ServiceCollection**: A class that acts as a list of instructions, defining how services should be initialized and registered.
* **ServiceProvider**: The concrete implementation of the DI container, built from the ServiceCollection, which manages the resolution of services.
* **Service Registration**: The process of mapping an abstraction (interface) to a concrete implementation (e.g., mapping `IWeatherService` to `OpenWeatherService`).
* **Constructor Injection**: The mechanism where the container automatically identifies and provides required dependencies through a class's constructor.
* **Dependency Graph**: The cascading resolution process where the container resolves a root class and all subsequent services required by that class and its dependencies.

### Lesson notes

To understand Dependency Injection at its most basic level, it is helpful to view it within a standard console application.
This environment exposes the underlying components that are typically hidden behind the abstractions of ASP.NET Core.
The process begins with a `ServiceCollection`, where services and their implementations are registered.

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

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-simplest-setup-with-dependency-injection-53953102/?t=35)

In this setup, `AddSingleton` is used to register both the `IWeatherService` (mapped to `OpenWeatherService`) and the `Application` class itself.
Once the registrations are complete, `BuildServiceProvider()` is called to create the container.
This container holds the logic for how every registered service should be resolved.

The `Application` class does not manually instantiate its dependencies.
Instead, it defines them in its constructor:

```csharp
namespace Weather.ConsoleApp;

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

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-simplest-setup-with-dependency-injection-53953102/?t=85)

When `serviceProvider.GetRequiredService<Application>()` is invoked, the DI container inspects the `Application` constructor.
It recognizes that an `IWeatherService` is required.
Because `IWeatherService` was registered as `OpenWeatherService`, the container automatically instantiates `OpenWeatherService` and injects it into the `Application` instance.
This removes the need for the developer to write code like `new Application(new OpenWeatherService())`.

The DI container effectively functions as a registry or a "big class" containing instructions for resolution.
It handles the entire dependency graph: if the `OpenWeatherService` itself required other services in its constructor, the container would resolve those as well, provided they were registered.
If a required service is missing from the `ServiceCollection`, the application will throw an exception at the point of resolution, indicating that the service is not registered.

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

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-simplest-setup-with-dependency-injection-53953102/?t=340)

---

## 3. The ServiceCollection

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-servicecollection-53953103/) · 2:00

### Summary

The ServiceCollection is the foundational component in .NET dependency injection, acting as a container for service descriptors that define how the framework should resolve dependencies.
It implements standard collection interfaces to store instructions—such as singleton lifetimes—specifying whether a service should be resolved via an interface or as a self-registered class.

### Key concepts

*   **ServiceCollection**: The initial container used to register dependencies in a .NET application.
*   **ServiceDescriptor**: An object that describes how a service should be resolved by the dependency injection framework.
*   **Singleton Lifetime**: A registration scope where only one instance of a class is created for the entire lifetime of the application.
*   **Interface Mapping**: Registering a service by mapping an interface to a specific concrete implementation.
*   **Self-Registration**: Registering a concrete class to resolve to itself rather than through an interface.

### Lesson notes

The `ServiceCollection` is the primary entry point when configuring dependency injection in .NET.
It serves as the initial container where services are registered before the service provider is built.

```csharp
if (args.Length == 0)
{
    args = new string[1];
    args[0] = "London";
}

var services = new ServiceCollection();

await application.RunAsync(args);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-servicecollection-53953103/?t=10)

Internally, the `ServiceCollection` is a specialized list.
It implements several standard .NET collection interfaces, specifically designed to hold `ServiceDescriptor` objects.

```csharp
public class ServiceCollection :
        IServiceCollection,
        IList<ServiceDescriptor>,
        ICollection<ServiceDescriptor>,
        IEnumerable<ServiceDescriptor>,
        IEnumerable
    {

        #nullable disable
        private readonly List<ServiceDescriptor> _descriptors = new List<ServiceDescriptor>();

        /// <inheritdoc />
        public int Count => this._descriptors.Count;

        /// <inheritdoc />
        public bool IsReadOnly => false;
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-servicecollection-53953103/?t=25)

A `ServiceDescriptor` provides the necessary metadata for the dependency injection framework to understand how a service should be resolved.
The `ServiceCollection` is essentially a list of these instructions.

When registering services, methods like `AddSingleton` are used to define the service's lifetime and implementation.
A singleton registration ensures that only one instance of the class exists throughout the application's execution, preventing the framework from creating new instances repeatedly.

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

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-servicecollection-53953103/?t=55)

Services can be registered by mapping an interface to a concrete implementation (e.g., mapping `IWeatherService` to `OpenWeatherService`) or by registering a concrete type directly (e.g., `Application`).
When a concrete type is registered without an interface, the dependency injection framework resolves the class to itself.

---

## 4. The ServiceProvider

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-serviceprovider-53953104/) · 1:46

### Summary

The ServiceProvider is the engine of the .NET Dependency Injection system, acting as the container that resolves and manages the lifecycle of services.
While the ServiceCollection serves as a registry or "recipe book" where services are described, the ServiceProvider is responsible for interpreting these instructions to instantiate objects and their dependencies.
By leveraging the ServiceProvider, developers can avoid manual object construction at the application root, enabling automatic dependency resolution and more maintainable code structures.

### Key concepts

- **The Container**: The `ServiceProvider` is the actual DI container that resolves services.
- **Resolution vs. Description**: `ServiceCollection` describes the services; `ServiceProvider` builds them.
- **Automatic Resolution**: It eliminates the need to manually instantiate complex dependency trees.
- **Internal Complexity**: It manages internal state using concurrent dictionaries and call site factories to optimize service creation.

### Lesson notes

The `ServiceProvider` is the core component that performs the actual work of resolving services.
If the `ServiceCollection` is viewed as a cookbook or a list of instructions, the `ServiceProvider` is the chef that follows those instructions to produce the final objects.

To create a `ServiceProvider`, you call the `BuildServiceProvider()` method on an instance of `IServiceCollection`.
Once built, you can use methods like `GetRequiredService<T>()` to retrieve instances of your registered classes.

```csharp
if (args.Length == 0)
{
    args = new string[1];
    args[0] = "London";
}

var services = new ServiceCollection();

services.AddSingleton<IWeatherService, OpenWeatherService>();
services.AddSingleton<Application>();

ServiceProvider serviceProvider = services.BuildServiceProvider();

var application = serviceProvider.GetRequiredService<Application>();

await application.RunAsync(args);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-serviceprovider-53953104/?t=10)

Internally, the `ServiceProvider` is a complex class.
Decompiling it reveals sophisticated mechanisms for managing service lifetimes and performance, including call site factories and concurrent dictionaries used to cache resolved services and scopes.

```csharp
private readonly CallSiteValidator _callSiteValidator;

    private readonly Func<Type, Func<ServiceProviderEngineScope, object>> _createServiceAc

    // Internal for testing
    internal ServiceProviderEngine _engine;

    private bool _disposed;

    private ConcurrentDictionary<Type, Func<ServiceProviderEngineScope, object>> _realizec

    internal CallSiteFactory CallSiteFactory { get; }

    internal ServiceProviderEngineScope Root { get; }
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-serviceprovider-53953104/?t=40)

The primary advantage of using the `ServiceProvider` is the automatic resolution of dependencies.
Without a DI container, you would be forced to manually instantiate every dependency at the root of your application.
For example, if the `Application` class requires an `IWeatherService`, you would have to manually create the `OpenWeatherService` and pass it into the constructor.

```csharp
if (args.Length == 0)
{
    args = new string[1];
    args[0] = "London";
}

var services = new ServiceCollection();

services.AddSingleton<IWeatherService, OpenWeatherService>();
services.AddSingleton<Application>();

ServiceProvider serviceProvider = services.BuildServiceProvider();

new Application(new OpenWeatherService());
var application = serviceProvider.GetRequiredService<Application>();

await application.RunAsync(args);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-serviceprovider-53953104/?t=85)

By using the container, you delegate this responsibility, allowing the system to handle complex dependency trees automatically.
This capability is the foundation for the advanced dependency injection patterns used throughout .NET development.

---

## 5. All of the above, in an API

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/all-of-the-above-in-an-api-53953105/) · 5:27

### Summary

This lesson demonstrates how to implement Dependency Injection in a .NET Web API, comparing the legacy Startup.cs approach with the modern Program.cs structure introduced in .NET 6.
It covers service registration using IServiceCollection, the use of extension methods for logical grouping, and how the framework automatically resolves dependencies in API controllers via constructor injection.

### Key concepts

*   **Transition to Program.cs**: Understanding how .NET 6+ consolidated the `Startup.cs` logic into a single file.
*   **Service Registration**: Using `builder.Services` (an `IServiceCollection`) to register dependencies like `IWeatherService`.
*   **Middleware Pipeline**: Mapping the legacy `Configure` method to the modern application builder flow.
*   **Extension Methods**: Using the `Add{Feature}` pattern to encapsulate multiple service registrations and keep configuration clean.
*   **Automatic Resolution**: How the Web API framework automatically provides dependencies to controllers through their constructors.

### Lesson notes

In a Web API context, service logic is typically encapsulated in implementation classes that fulfill specific interfaces.
For example, an `OpenWeatherService` might implement an `IWeatherService` to fetch data from an external API using an `IHttpClientFactory`.

```csharp
namespace Weather.Api.Weather;

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

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/all-of-the-above-in-an-api-53953105/?t=25)

#### Legacy vs. Modern Configuration

Prior to .NET 6, dependency injection was configured in a `Startup.cs` class.
This class separated service registration into a `ConfigureServices` method and middleware configuration into a `Configure` method.

```csharp
public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddHttpClient();
        services.AddTransient<IWeatherService, OpenWeatherService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/all-of-the-above-in-an-api-53953105/?t=75)

In modern .NET (Minimal APIs), these sections are mapped directly into `Program.cs`.
The `builder.Services` property represents the `IServiceCollection` (the `ConfigureServices` area), while the code following `builder.Build()` represents the middleware pipeline (the `Configure` area).

```csharp
using Weather.Api.Weather;

var builder = WebApplication.CreateBuilder(args);

// ConfigureServices Starts
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();
builder.Services.AddTransient<IWeatherService, OpenWeatherService>();
// ConfigureService Ends

var app = builder.Build();

// Configure Starts
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/all-of-the-above-in-an-api-53953105/?t=295)

#### Extension Methods for Grouping

To avoid polluting the configuration file with low-level registration details, .NET uses extension methods.
Methods like `AddEndpointsApiExplorer` encapsulate multiple `TryAddSingleton` or `TryAddEnumerable` calls.
This allows developers to group related services under a single, descriptive method call.

```csharp
#nullable enable
namespace Microsoft.Extensions.DependencyInjection
{
    public static class EndpointMetadataApiExplorerServiceCollectionExtensions
    {
        public static IServiceCollection AddEndpointsApiExplorer(
            this IServiceCollection services)
        {
            services.TryAddSingleton<IActionDescriptorCollectionProvider, DefaultActionDescriptorCol]
            services.TryAddSingleton<IApiDescriptionGroupCollectionProvider, ApiDescriptionGroupColle
            services.TryAddEnumerable(ServiceDescriptor.Transient<IApiDescriptionProvider, EndpointMe
            return services;
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/all-of-the-above-in-an-api-53953105/?t=145)

#### Controller Injection

Once services are registered in the container, they are consumed by controllers via constructor injection.
The framework automatically resolves the required interface and provides the concrete implementation at runtime.

```csharp
[ApiController]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherService _weatherService;


    public WeatherForecastController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet("weather/{city}")]
    public async Task<IActionResult> GetCurrentWeather([FromRoute] string city)
    {
        var weather = await _weatherService.GetCurrentWeatherAsync(city);
        if (weather == null)
        {
            return NotFound();
        }
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/all-of-the-above-in-an-api-53953105/?t=310)

---

## 6. The different types of dependency lifetimes

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-different-types-of-dependency-lifetimes-53953106/) · 4:30

### Summary

Dependency injection in .NET supports three primary lifetimes: Transient, Scoped, and Singleton.
These lifetimes determine how long a service instance remains active and when it is disposed of or reused.
This lesson demonstrates these differences by setting up an ID generator service, an API controller, and an action filter to track instance persistence across the request lifecycle.

### Key concepts

* **Transient**: A new instance is created every time the service is requested from the container.
* **Scoped**: A single instance is created for each client request (logical scope), such as an HTTP request.
* **Singleton**: A single instance is created the first time it is requested, and that same instance is used by every subsequent request for the lifetime of the application.
* **Registration**: Services are registered in the DI container using `AddTransient`, `AddScoped`, or `AddSingleton` methods on `IServiceCollection`.
* **Observation**: Using `IActionFilter` and `ServiceFilter` allows developers to inspect service instances at different stages of the request pipeline.

### Lesson notes

In .NET Dependency Injection, lifetimes define how long a service stays active before it is disposed of or reused.
The three primary lifetimes are Transient, Scoped, and Singleton.
These are configured during service registration in the application startup.

```csharp
using Weather.Api.Weather;

var builder = WebApplication.CreateBuilder(args);

// ConfigureServices Starts
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();
builder.Services.AddTransient<IWeatherService, OpenWeatherService>();
// ConfigureService Ends

var app = builder.Build();

// Configure Starts
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-different-types-of-dependency-lifetimes-53953106/?t=25)

To demonstrate how these lifetimes behave in a real application, we create an `IdGenerator` service.
This service generates a unique `Guid` (Globally Unique Identifier) when it is instantiated, allowing us to track whether the DI container is providing a new instance or a cached one.

```csharp
namespace Weather.Api.Service;

public class IdGenerator
{
    public Guid Id { get; }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-different-types-of-dependency-lifetimes-53953106/?t=85)

This service is injected into a `LifetimeController`.
The controller exposes an endpoint that returns the current ID from the injected `IdGenerator` instance.

```csharp
public class LifetimeController : ControllerBase
{
    private readonly IdGenerator _idGenerator;

    public LifetimeController(IdGenerator idGenerator)
    {
        _idGenerator = idGenerator;
    }

    [HttpGet("lifetime")]
    public IActionResult GetId()
    {
        var id = _idGenerator.Id;
        return Ok(id);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-different-types-of-dependency-lifetimes-53953106/?t=130)

To specifically observe the behavior of the `Scoped` lifetime, we implement a `LifetimeIndicatorFilter`.
This filter implements `IActionFilter`, which allows us to execute logic before and after the controller action.
By injecting the `IdGenerator` into this filter and logging the ID, we can see if the same instance is shared between the filter and the controller during a single HTTP request.

```csharp
public void OnActionExecuting(ActionExecutingContext context)
    {
        var id = _idGenerator.Id;
        _logger.LogInformation($"{nameof(OnActionExecuting)} id was: {id}");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var id = _idGenerator.Id;
        _logger.LogInformation($"{nameof(OnActionExecuted)} id was: {id}");
    }

}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-different-types-of-dependency-lifetimes-53953106/?t=220)

The filter must be registered in the DI container to be resolvable.
In this scenario, it is registered with a scoped lifetime.

```csharp
using Weather.Api.Filter;
using Weather.Api.Weather;

var builder = WebApplication.CreateBuilder(args);

// ConfigureServices Starts
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();
builder.Services.AddTransient<IWeatherService, OpenWeatherService>();
builder.Services.AddScoped<LifetimeIndicatorFilter>();


// ConfigureService Ends

var app = builder.Build();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-different-types-of-dependency-lifetimes-53953106/?t=250)

Finally, the filter is applied to the controller action using the `ServiceFilter` attribute.
This attribute tells ASP.NET Core to resolve the filter instance from the DI container, ensuring that any dependencies required by the filter (like `IdGenerator`) are also correctly injected.

```csharp
[ApiController]
public class LifetimeController : ControllerBase
{
    private readonly IdGenerator _idGenerator;

    public LifetimeController(IdGenerator idGenerator)
    {
        _idGenerator = idGenerator;
    }

    [HttpGet("lifetime")]
    [ServiceFilter(typeof(LifetimeIndicatorFilter))]
    public IActionResult GetId()
    {
        var id = _idGenerator.Id;
        return Ok(id);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-different-types-of-dependency-lifetimes-53953106/?t=265)

---

## 7. The Transient lifetime

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-transient-lifetime-53953107/) · 2:08

The transient lifetime is a dependency injection scope where the container creates a new instance of the requested service every time it is resolved.
This ensures that dependencies are never shared between different components or even within the same request if requested multiple times by different consumers.

### Key concepts

- **New Instance per Request**: A new instance is created every time the service is resolved from the container.
- **No State Sharing**: Because instances are unique to each consumer, state is not shared between different classes or components.
- **Registration**: Services are registered using the `AddTransient` method on the `IServiceCollection`.
- **Statelessness**: Best suited for lightweight, stateless services.

### Lesson notes

In .NET, a transient service is one that is instantiated every time it is required by the container.
Whether the service is requested by a controller, a filter, or any other component, the dependency injection system will provide a fresh instance for each specific request.

To illustrate this, consider an `IdGenerator` class.
This class generates a new `Guid` when it is initialized.
Because the ID is set as a default value during construction, it remains constant for the life of that specific instance.
If the ID changes between two points of access, it indicates that two different instances of the class are being used.

```csharp
namespace Weather.Api.Service;

public class IdGenerator
{
    public Guid Id { get; } = Guid.NewGuid();
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-transient-lifetime-53953107/?t=55)

To use this class within the application, it must be registered in the `Program.cs` file using the `AddTransient` method.
This tells the .NET container to create a new `IdGenerator` every time one is requested.

```csharp
var builder = WebApplication.CreateBuilder(args);

// ConfigureServices Starts
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();
builder.Services.AddTransient<IWeatherService, OpenWeatherService>();
builder.Services.AddScoped<LifetimeIndicatorFilter>();
builder.Services.AddTransient<IdGenerator>();

// ConfigureService Ends

var app = builder.Build();

// Configure Starts
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-transient-lifetime-53953107/?t=40)

When the application runs and a request is made to an endpoint, the container resolves the dependencies.
If we have a `LifetimeController` that requires an `IdGenerator`, it receives a new instance.

```csharp
[ApiController]
public class LifetimeController : ControllerBase
{
    private readonly IdGenerator _idGenerator;

    public LifetimeController(IdGenerator idGenerator)
    {
        _idGenerator = idGenerator;
    }

    [HttpGet("lifetime")]
    [ServiceFilter(typeof(LifetimeIndicatorFilter))]
    public IActionResult GetId()
    {
        var id = _idGenerator.Id;
        return Ok(id);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-transient-lifetime-53953107/?t=10)

If the same request also uses a filter like `LifetimeIndicatorFilter`, and that filter also requires an `IdGenerator`, the container will create a second, different instance for the filter.

```csharp
using Microsoft.AspNetCore.Mvc.Filters;
using Weather.Api.Service;

namespace Weather.Api.Filter;

public class LifetimeIndicatorFilter : IActionFilter
{
    private readonly IdGenerator _idGenerator;
    private readonly ILogger<LifetimeIndicatorFilter> _logger;

    public LifetimeIndicatorFilter(IdGenerator idGenerator,
        ILogger<LifetimeIndicatorFilter> logger)
    {
        _idGenerator = idGenerator;
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var id = _idGenerator.Id;
        _logger.LogInformation($"{nameof(OnActionExecuting)} id was: {id}");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var id = _idGenerator.Id;
        _logger.LogInformation($"{nameof(OnActionExecuted)} id was: {id}");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-transient-lifetime-53953107/?t=115)

In this scenario, the `LifetimeIndicatorFilter` is registered as a scoped service, meaning the filter instance itself is reused throughout the duration of the HTTP request.
Consequently, the `IdGenerator` instance injected into the filter's constructor is also reused for both `OnActionExecuting` and `OnActionExecuted`, resulting in the same ID being logged twice.
However, because `IdGenerator` is transient, the instance in the filter is entirely separate from the instance in the `LifetimeController`, and they will have different ID values.

---

## 8. The Singleton lifetime

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-singleton-lifetime-53953108/) · 2:08

The Singleton lifetime is a fundamental pattern in dependency injection frameworks where a single instance of a class is maintained for the entire duration of the application's execution.
When a service is registered as a Singleton, the DI container instantiates it once, and that same instance is provided to every component that requires it.

### Key concepts

- **Single Instance**: Only one instance of the service exists throughout the application's lifetime.
- **Shared State**: The same instance is shared across all requests and all consuming components.
- **Performance**: Reduces overhead by avoiding repeated instantiation and reducing garbage collection pressure.
- **Thread Safety**: Requires caution when the service contains mutable state, as it will be accessed by multiple threads simultaneously.
- **Registration**: Defined in the service collection using the `AddSingleton` method.

### Lesson notes

In a typical implementation, a controller might depend on a service like an `IdGenerator`.
When using the Singleton lifetime, this generator is shared across all consumers.

```csharp
[ApiController]
public class LifetimeController : ControllerBase
{
    private readonly IdGenerator _idGenerator;

    public LifetimeController(IdGenerator idGenerator)
    {
        _idGenerator = idGenerator;
    }

    [HttpGet("lifetime")]
    [ServiceFilter(typeof(LifetimeIndicatorFilter))]
    public IActionResult GetId()
    {
        var id = _idGenerator.Id;
        return Ok(id);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-singleton-lifetime-53953108/?t=10)

To configure a service with a Singleton lifetime, use the `AddSingleton<T>` method within the `Program.cs` file during service configuration.

```csharp
var builder = WebApplication.CreateBuilder(args);

// ConfigureServices Starts
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddHttpClient();
builder.Services.AddTransient<IWeatherService, OpenWeatherService>();
builder.Services.AddScoped<LifetimeIndicatorFilter>();
builder.Services.AddSingleton<IdGenerator>();

// ConfigureService Ends

var app = builder.Build();

// Configure Starts
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-singleton-lifetime-53953108/?t=55)

When the application runs, the same instance of the `IdGenerator` is reused.
This means that if the `IdGenerator` is injected into both a `LifetimeController` and a `LifetimeIndicatorFilter`, both will receive the exact same instance.
Furthermore, subsequent HTTP requests to the endpoint will continue to use that same initial instance.
This is observable in the application logs, where the generated ID remains constant across different execution contexts and requests.

```plaintext
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5001
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: I:\lab\courses\dep-injection-course-final\2.Fundamentals\Weather.Api
info: Weather.Api.Filter.LifetimeIndicatorFilter[0]
      OnActionExecuting id was: 6a201d5c-80a5-40a3-a218-05423a6c1120
info: Weather.Api.Filter.LifetimeIndicatorFilter[0]
      OnActionExecuted id was: 6a201d5c-80a5-40a3-a218-05423a6c1120
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-singleton-lifetime-53953108/?t=70)

#### Performance and State

Using Singletons can improve application performance.
Because the instance is not recreated for every request, the application saves processing power and memory.
It also reduces the workload of the garbage collector, as objects are not being constantly created and discarded.

However, Singletons should be used with caution if they maintain state.
Since the instance is shared across the entire application, any mutation of its internal state must be thread-safe.
If a service does not share or mutate state, it is an ideal candidate for the Singleton lifetime to maximize efficiency.

---

## 9. The Scoped lifetime

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-scoped-lifetime-53953109/) · 2:31

### Summary

The Scoped lifetime in .NET Dependency Injection ensures that a service instance is created once per scope.
In ASP.NET Core, this scope typically corresponds to the duration of a single HTTP request, from the moment it hits the web server until the response is sent.
This allows multiple components within the same request—such as controllers, filters, and middleware—to share the same instance of a service, while ensuring that different requests receive their own unique instances.
This lifetime is ideal for services that need to maintain state throughout a request or for services that are not thread-safe across different concurrent requests but can be safely reused within one.

### Key concepts

- **Scope Definition**: In web applications, a scope is automatically created for every incoming HTTP request and disposed of when the response is sent.
- **Instance Sharing**: Within a single scope, the DI container will always return the same instance of a scoped service.
- **Isolation**: Each request has its own scope, meaning services are isolated between different users and requests.
- **Registration**: Services are registered using the `AddScoped<TService, TImplementation>()` method.
- **Efficiency**: Reusing instances within a request can be more efficient than creating new ones (Transient) while being safer than sharing one instance globally (Singleton).

### Lesson notes

The Scoped lifetime is a middle ground between Singleton and Transient.
While a Singleton lives for the entire application lifetime and a Transient service is created every time it is requested, a Scoped service is created once per specific scope.

In ASP.NET Core, the framework defines the scope as the duration of an HTTP request.
From the moment the request hits the web server until the response is returned, any service marked as Scoped will live and be reused.
To demonstrate the difference, consider a service registered as a Singleton:

```csharp
builder.Services.AddSingleton<IdGenerator>();

// ConfigureService Ends

var app = builder.Build();

// Configure Starts
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
//Configure End

app.Run();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-scoped-lifetime-53953109/?t=10)

In a Singleton registration, every call to the endpoint returns the same identifier across all requests:

```json
"1c48883f-e85d-4ed8-b9a8-e08fecc6aa66"
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-scoped-lifetime-53953109/?t=25)

However, when the registration is changed to `AddScoped`, each new request results in a new instance and therefore a new ID:

```json
"9fe14f48-9687-465f-97fd-47343e16faf3"
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-scoped-lifetime-53953109/?t=40)

The power of the Scoped lifetime is most evident when multiple components in the same request pipeline need to interact with the same service.
For example, a `LifetimeIndicatorFilter` and a `LifetimeController` can both use the same `IdGenerator` instance if it is registered as Scoped.

```csharp
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();
builder.Services.AddTransient<IWeatherService, OpenWeatherService>();
builder.Services.AddScoped<LifetimeIndicatorFilter>();
builder.Services.AddSingleton<IdGenerator>();

// ConfigureService Ends

var app = builder.Build();

// Configure Starts
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-scoped-lifetime-53953109/?t=70)

When running the application with a scoped service, logs show that within a single request, the ID remains identical across different execution stages (e.g., `OnActionExecuting` and `OnActionExecuted`), but changes when a second request is initiated:

```text
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5001
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: I:\lab\courses\dep-injection-course-final\2.Fundamentals\Weather.Api
info: Weather.Api.Filter.LifetimeIndicatorFilter[0]
      OnActionExecuting id was: 4d744a5a-4a38-49e2-926a-301fb3b4ec56
info: Weather.Api.Filter.LifetimeIndicatorFilter[0]
      OnActionExecuted id was: 4d744a5a-4a38-49e2-926a-301fb3b4ec56
info: Weather.Api.Filter.LifetimeIndicatorFilter[0]
      OnActionExecuting id was: 9f689a58-bb4e-41a3-b0b1-0216501d286a
info: Weather.Api.Filter.LifetimeIndicatorFilter[0]
      OnActionExecuted id was: 9f689a58-bb4e-41a3-b0b1-0216501d286a
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-scoped-lifetime-53953109/?t=85)

The controller receives the scoped service through its constructor.
Because the controller is resolved within the request scope, it shares the same service instances as other scoped components in that request.

```csharp
[ApiController]
public class LifetimeController : ControllerBase
{
    private readonly IdGenerator _idgenerator;

    public LifetimeController(IdGenerator idGenerator)
    {
        _idgenerator = idGenerator;
    }

    [HttpGet("lifetime")]
    [ServiceFilter(typeof(LifetimeIndicatorFilter))]
    public IActionResult GetId()
    {
        var id = _idgenerator.Id;
        return Ok(id);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-scoped-lifetime-53953109/?t=115)

This mechanism allows for efficient service resolution and the ability to collect state at the start of a request and reuse it at the end.
While ASP.NET Core provides a default scope per request, it is also possible to define custom scopes manually if needed, which is particularly useful in console applications that do not have a built-in request scope.

---

## 10. GetService vs GetRequiredService

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/getservice-vs-getrequiredservice-53953110/) · 3:38

### Summary

This lesson explores the two primary methods for resolving services from an IServiceProvider in .NET: GetService and GetRequiredService.
It details their differing behaviors when a requested service is missing from the container—specifically, how GetService returns null while GetRequiredService throws an exception—and provides guidance on why GetRequiredService is the preferred choice for most application scenarios to ensure fail-fast behavior and clearer debugging.

### Key concepts

* **GetService<T>**: Returns the service instance or null if the service is not registered in the container.
* **GetRequiredService<T>**: Returns the service instance or throws an InvalidOperationException if the service is not registered.
* **Generic vs. Non-Generic**: Generic methods are preferred over non-generic versions because they return the specific type directly, avoiding the need for manual casting from an object.
* **Fail-Fast Principle**: GetRequiredService is the recommended default because it identifies configuration errors immediately at the point of resolution.
* **Framework Defaults**: ASP.NET Core uses required-service logic internally for constructor injection in controllers and other framework components.

### Lesson notes

In .NET Dependency Injection, there are two primary ways to resolve services from an `IServiceProvider`.
While you can use non-generic methods that require a `typeof` argument and manual casting, it is preferred to use the generic extensions as they return the specific type directly.

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

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/getservice-vs-getrequiredservice-53953110/?t=10)

The fundamental difference between `GetService` and `GetRequiredService` lies in how they handle missing registrations.
`GetService` returns `null` if the service cannot be found, whereas `GetRequiredService` throws an exception.

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

var app = serviceProvider.GetService<Application>();
var application = serviceProvider.GetRequiredService<Application>();

await application.RunAsync(args);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/getservice-vs-getrequiredservice-53953110/?t=25)

If a service registration is omitted, `GetService` will return `null`, which can lead to `NullReferenceException` errors later in the application logic.
`GetRequiredService` provides a "fail-fast" mechanism by throwing an `InvalidOperationException` immediately, stating that no service for the specified type has been registered.
This makes it much easier to identify and fix configuration issues during development.

```csharp
args = new string[1];
    args[0] = "London";
}

var services = new ServiceCollection();

services.AddSingleton<IWeatherService, OpenWeatherService>();
//services.AddSingleton<Application>();

var serviceProvider = services.BuildServiceProvider();

var app = serviceProvider.GetService<Application>();
var application = serviceProvider.GetRequiredService<Application>();

await application.RunAsync(args);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/getservice-vs-getrequiredservice-53953110/?t=70)

In most cases, `GetRequiredService` is the better choice because if you are requesting a service, your intention is typically that it must exist.
This logic is also what ASP.NET Core uses internally for things like controller activation.
If a controller depends on a service that hasn't been registered in the container, the framework will throw an exception because it requires that dependency to function.

```csharp
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();
builder.Services.AddTransient<IWeatherService, OpenWeatherService>();
builder.Services.AddScoped<LifetimeIndicatorFilter>();
builder.Services.AddScoped<IdGenerator>();

// ConfigureService Ends

var app = builder.Build();

// Configure Starts
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/getservice-vs-getrequiredservice-53953110/?t=190)

```csharp
[ApiController]
public class LifetimeController : ControllerBase
{
    private readonly IdGenerator _idGenerator;

    public LifetimeController(IdGenerator idGenerator)
    {
        _idGenerator = idGenerator;
    }

    [HttpGet("lifetime")]
    [ServiceFilter(typeof(LifetimeIndicatorFilter))]
    public IActionResult GetId()
    {
        var id = _idGenerator.Id;
        return Ok(id);
    }

}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/getservice-vs-getrequiredservice-53953110/?t=205)

---

## 11. Generic-based registration vs implementation-based registration

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/generic-based-registration-vs-implementation-based-registration-53953111/) · 3:31

### Summary

This lesson explores the differences between automatic generic-based registration and manual implementation-based registration in the .NET Dependency Injection (DI) container.
It demonstrates how the DI framework automatically resolves constructor dependencies through graph traversal and explains the consequences of missing registrations, which result in startup failures.
The lesson also covers alternative registration patterns, such as using factory lambdas and direct instance instantiation for complex scenarios where automatic resolution is not feasible.

### Key concepts

* **Automatic Resolution**: The DI container's ability to resolve constructor dependencies automatically when using generic registration.
* **Extension Methods**: Using methods like `AddHttpClient` to register complex internal services and their dependencies.
* **Startup Failures**: The application will fail during the `Build()` phase if a required dependency for a registered service is missing.
* **Factory Lambdas**: Manual registration using `IServiceProvider` to control how a service is instantiated.
* **Instance Registration**: Direct registration of a pre-constructed object, typically used for singletons.
* **Graph Traversal**: The cascading effect where the DI container resolves a service by first resolving all its dependencies, and their dependencies, recursively.

### Lesson notes

In standard .NET development, services are typically registered using generic methods.
This approach assumes that any dependency required by a service's constructor is already registered and resolvable by the DI framework.

```csharp
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddTransient<IWeatherService, OpenWeatherService>();
builder.Services.AddScoped<LifetimeIndicatorFilter>();
builder.Services.AddScoped<IdGenerator>();

// ConfigureService Ends

var app = builder.Build();

// Configure Starts
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/generic-based-registration-vs-implementation-based-registration-53953111/?t=10)

For example, the `OpenWeatherService` implementation requires an `IHttpClientFactory` in its constructor.

```csharp
namespace Weather.Api.Weather;

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

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/generic-based-registration-vs-implementation-based-registration-53953111/?t=25)

While `IHttpClientFactory` is not explicitly registered in the main `Program.cs` file, it is added via the `AddHttpClient` extension method.
Decompiling this method reveals that it registers several internal services, including the `DefaultHttpClientFactory` and the `IHttpClientFactory` itself.

```csharp
if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddLogging();
        services.AddOptions();

        //
        // Core abstractions
        //
        services.TryAddTransient<HttpMessageHandlerBuilder, DefaultHttpMessageHandlerBuild
        services.TryAddSingleton<DefaultHttpClientFactory>();
        services.TryAddSingleton<IHttpClientFactory>(serviceProvider => serviceProvider.Ge
        services.TryAddSingleton<IHttpMessageHandlerFactory>(serviceProvider => servicePro

        //
        // Typed Clients
        //
        services.TryAdd(ServiceDescriptor.Transient(typeof(ITypedHtt
        services.TryAdd(ServiceDescriptor.Singleton(typeof(DefaultTy
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/generic-based-registration-vs-implementation-based-registration-53953111/?t=40)

If a required dependency is missing—for instance, if `AddHttpClient` is commented out—the application will fail at startup.
The DI container performs a check during the `builder.Build()` phase and throws an exception if it cannot satisfy the constructor requirements of a registered service.

```csharp
// ConfigureServices Starts
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddHttpClient();

builder.Services.AddTransient<IWeatherService, OpenWeatherService>();
builder.Services.AddScoped<LifetimeIndicatorFilter>();
builder.Services.AddScoped<IdGenerator>();

// ConfigureService Ends

var app = builder.Build();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/generic-based-registration-vs-implementation-based-registration-53953111/?t=80)

The resulting error indicates that the framework was unable to find the required service for the implementation.

```text
at Microsoft.Extensions.DependencyInjection.ServiceProvider..ctor(ICollection
`1 serviceDescriptors, ServiceProviderOptions options)
   --- End of inner exception stack trace ---
   at Microsoft.Extensions.DependencyInjection.ServiceProvider..ctor(ICollection
`1 serviceDescriptors, ServiceProviderOptions options)
   at Microsoft.Extensions.DependencyInjection.ServiceCollectionContainerBuilder
Extensions.BuildServiceProvider(IServiceCollection services, ServiceProviderOpti
ons options)
   at Microsoft.Extensions.DependencyInjection.DefaultServiceProviderFactory.Cre
ateServiceProvider(IServiceCollection containerBuilder)
   at Microsoft.Extensions.Hosting.Internal.ServiceFactoryAdapter`1.CreateServic
eProvider(Object containerBuilder)
   at Microsoft.Extensions.Hosting.HostBuilder.CreateServiceProvider()
   at Microsoft.Extensions.Hosting.HostBuilder.Build()
   at Microsoft.AspNetCore.Builder.WebApplicationBuilder.Build()
   at Program.<Main>$(String[] args) in I:\lab\courses\dep-injection-course-fina
l\2.Fundamentals\Weather.Api\Program.cs:line 20
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/generic-based-registration-vs-implementation-based-registration-53953111/?t=65)

To handle scenarios where automatic resolution is not possible (e.g., when an implementation is internal or requires specific manual setup), you can use implementation-based registration.
This involves providing a factory lambda that manually instantiates the service.

```csharp
// ConfigureServices Starts
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddHttpClient();

builder.Services.AddTransient<IWeatherService>(_ =>
    new OpenWeatherService(new ));
builder.Services.AddScoped<LifetimeIndicatorFilter>();
builder.Services.AddScoped<IdGenerator>();

// ConfigureService Ends
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/generic-based-registration-vs-implementation-based-registration-53953111/?t=115)

Alternatively, for singletons, you can register a specific instance of the class directly.
Since it is a singleton, the container will return the same instance every time without needing to execute a lambda.

```csharp
// ConfigureServices Starts
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddHttpClient();

builder.Services.AddSingleton<IWeatherService>(new OpenWeatherService());
builder.Services.AddScoped<LifetimeIndicatorFilter>();
builder.Services.AddScoped<IdGenerator>();

// ConfigureService Ends
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/generic-based-registration-vs-implementation-based-registration-53953111/?t=160)

Generic registration works as long as every dependency in the service's constructor is already present in the `IServiceCollection`.
The DI framework performs a graph traversal: if Service A depends on Service B, and Service B depends on Service C, the framework will resolve the entire chain as long as all components are registered.
If any link in this chain is missing, the DI container will throw an exception during initialization.

---

## 12. Registration approaches

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registration-approaches-53953112/) · 6:04

### Summary

This lesson explores the various methods for registering services in the .NET Dependency Injection (DI) container, ranging from automatic generic registrations to manual factory-based approaches.
It demonstrates how to use the IServiceProvider to resolve dependencies manually when custom initialization logic is required and highlights the specific requirements for Transient, Scoped, and Singleton lifetimes, including the ability to register direct instances for Singletons.

### Key concepts

- **Automatic Registration**: Using generic methods like `AddScoped<T>()` where the DI container automatically resolves constructor dependencies.
- **Factory Registration**: Using a lambda expression `(provider => ...)` to manually instantiate a service, providing access to the `IServiceProvider`.
- **Manual Dependency Resolution**: Utilizing `provider.GetRequiredService<T>()` within a factory to resolve specific dependencies for a constructor.
- **Singleton Instance Registration**: The ability to register a pre-existing instance of a class as a Singleton.
- **Lifetime Constraints**: Understanding why Transient and Scoped registrations require factory logic or types rather than direct instances to manage object lifecycles correctly.

### Lesson notes

The most common approach to service registration in .NET is the generic registration.
This method allows the DI container to automatically handle the cascading effect of resolving services, provided all dependencies in a class's constructor are also registered in the container.

```csharp
// ConfigureServices Starts
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddTransient<IWeatherService, OpenWeatherService>();
builder.Services.AddScoped<LifetimeIndicatorFilter>();
builder.Services.AddScoped<IdGenerator>();

// ConfigureService Ends

var app = builder.Build();

// Configure Starts
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registration-approaches-53953112/?t=10)

#### Manual Factory Registration

While automatic registration is standard, you can achieve the same result manually using a factory pattern.
For a class like `IdGenerator` which has no constructor dependencies, the following two registrations are functionally identical:

```csharp
builder.Services.AddHttpClient();

builder.Services.AddTransient<IWeatherService, OpenWeatherService>();
builder.Services.AddScoped<LifetimeIndicatorFilter>();
//builder.Services.AddScoped<IdGenerator>();
builder.Services.AddScoped(_ => new IdGenerator());

// ConfigureService Ends

var app = builder.Build();

// Configure Starts
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registration-approaches-53953112/?t=40)

When a class has dependencies, manual registration becomes more complex.
For example, the `LifetimeIndicatorFilter` requires an `IdGenerator` and an `ILogger`:

```csharp
using Weather.Api.Service;

namespace Weather.Api.Filter;

public class LifetimeIndicatorFilter : IActionFilter
{
    private readonly IdGenerator _idGenerator;
    private readonly ILogger<LifetimeIndicatorFilter> _logger;

    public LifetimeIndicatorFilter(IdGenerator idGenerator,
        ILogger<LifetimeIndicatorFilter> logger)
    {
        _idGenerator = idGenerator;
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var id = _idGenerator.Id;
        _logger.LogInformation($"{nameof(OnActionExecuting)} id wa {j
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registration-approaches-53953112/?t=85)

To register this manually, you use the `IServiceProvider` (often named `provider`) passed into the lambda.
This provider allows you to call `GetRequiredService<T>()` to resolve the necessary dependencies before passing them into the constructor of the service being registered.

```csharp
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddTransient<IWeatherService, OpenWeatherService>();

builder.Services.AddScoped<LifetimeIndicatorFilter>();
builder.Services.AddScoped(provider =>
{
    var idGenerator = provider.GetRequiredService<IdGenerator>();
    var logger = provider.GetRequiredService<ILogger<LifetimeIndicatorFilter>>();

    return new LifetimeIndicatorFilter(idGenerator, logger);
});

//builder.Services.AddScoped<IdGenerator>();
builder.Services.AddScoped(_ => new IdGenerator());

// ConfigureService Ends

var app = builder.Build();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registration-approaches-53953112/?t=175)

#### Lifetime-Specific Registration Rules

Different lifetimes have different rules regarding instance registration:

1.  **Singleton**: Since only one instance exists for the application's lifetime, you can register a pre-instantiated object directly. The container will return this specific instance every time the service is requested.

```csharp
{
    var idGenerator = provider.GetRequiredService<IdGenerator>();
    var logger = provider.GetRequiredService<ILogger<LifetimeIndicatorFilter>>();

    return new LifetimeIndicatorFilter(idGenerator, logger);
});

//builder.Services.AddScoped<IdGenerator>();
//builder.Services.AddScoped(_ => new IdGenerator());

builder.Services.AddSingleton(new IdGenerator());

// ConfigureService Ends

var app = builder.Build();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registration-approaches-53953112/?t=265)

2.  **Transient and Scoped**: These lifetimes do not allow direct instance registration (e.g., `AddTransient(new IdGenerator())`). This is because the container must be able to create new instances—either every time the service is requested (Transient) or once per scope (Scoped). To register these manually, you must provide a factory function so the container knows how to instantiate the object when needed.

```csharp
{
    var idGenerator = provider.GetRequiredService<IdGenerator>();
    var logger = provider.GetRequiredService<ILogger<LifetimeIndicatorFilter>>();

    return new LifetimeIndicatorFilter(idGenerator, logger);
});

//builder.Services.AddScoped<IdGenerator>();
//builder.Services.AddScoped(_ => new IdGenerator());

builder.Services.AddTransient(_ => new IdGenerator());

// ConfigureService Ends

var app = builder.Build();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registration-approaches-53953112/?t=310)

Manual registration is less common and typically reserved for scenarios requiring "fancy" initialization or logic that must execute before the service is registered.
In most cases, the generic approach is preferred for its simplicity and maintainability.

```csharp
var builder = WebApplication.CreateBuilder(args);


// ConfigureServices Starts
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddTransient<IWeatherService, OpenWeatherService>();
// builder.Services.AddTransient<IWeatherService>(provider =>
//     new OpenWeatherService(provider.GetRequiredService<IHttpClientFactory>()));

builder.Services.AddScoped<LifetimeIndicatorFilter>();
// builder.Services.AddScoped(provider =>
// {
//     var idGenerator = provider.GetRequiredService<IdGenerator>();
//     var logger = provider.GetRequiredService<ILogger<LifetimeIndicatorFilter>>();
//
//     return new LifetimeIndicatorFilter(idGenerator, logger);
// });
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registration-approaches-53953112/?t=355)

---

## 13. The Startup.cs and changes after .NET 6

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-startupcs-and-changes-after-net-6-53953113/) · 2:09

### Summary

This lesson explains the transition from the traditional Startup.cs file to the consolidated Program.cs model introduced in .NET 6.
It details how the ConfigureServices and Configure methods map to the WebApplicationBuilder and WebApplication instances, providing a clear path for implementing dependency injection and middleware in modern .NET applications.

### Key concepts

* Mapping the legacy ConfigureServices method to the builder.Services property in Program.cs.
* Mapping the legacy Configure method to the middleware pipeline configuration section after builder.Build().
* Understanding the lifecycle of the WebApplicationBuilder from initialization to application materialization.
* Maintaining functional parity between the Startup.cs approach and the .NET 6+ Minimal Hosting model.

### Lesson notes

In .NET 6 and subsequent versions, the default project template omits the Startup.cs file in favor of a consolidated Program.cs using the Minimal Hosting model.
This approach uses WebApplication.CreateBuilder(args) to initialize the application and its service container.

The logic previously found in the ConfigureServices method of Startup.cs now resides between the creation of the builder and the call to builder.Build().
In this section, you register dependencies with the IoC container using the builder.Services property.

```csharp
using Weather.Api.Filter;
using Weather.Api.Service;
using Weather.Api.Weather;

var builder = WebApplication.CreateBuilder(args);

// ConfigureServices Starts
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddTransient<IWeatherService, OpenWeatherService>();
builder.Services.AddScoped<LifetimeIndicatorFilter>();
builder.Services.AddScoped<IdGenerator>();
// ConfigureService Ends

var app = builder.Build();

// Configure Starts
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-startupcs-and-changes-after-net-6-53953113/?t=10)

In the legacy Startup.cs model, this same configuration would be handled within a ConfigureServices method that receives an IServiceCollection directly.
The primary difference is simply the location of the code and the object used to access the service collection.

```csharp
public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddHttpClient();

        services.AddTransient<IWeatherService, OpenWeatherService>();
        services.AddScoped<LifetimeIndicatorFilter>();
        services.AddScoped<IdGenerator>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-startupcs-and-changes-after-net-6-53953113/?t=70)

The middleware pipeline, which was previously defined in the Configure method of Startup.cs, is now configured using the app instance (of type WebApplication) after the builder has materialized the application via builder.Build().
This section ends when app.Run() is called.

```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseEndpoints(x => x.MapControllers());
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-startupcs-and-changes-after-net-6-53953113/?t=85)

While the syntax for mapping endpoints has changed slightly in .NET 6 (using top-level mapping on the app object), the underlying behavior remains the same.
Developers using the older Program.cs and Startup.cs split do not need to migrate existing projects, as both patterns are fully supported, though the .NET 6 approach is the current standard.

```csharp
var builder = WebApplication.CreateBuilder(args);

// ConfigureServices Starts
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddTransient<IWeatherService, OpenWeatherService>();
builder.Services.AddScoped<LifetimeIndicatorFilter>();
builder.Services.AddScoped<IdGenerator>();
// ConfigureService Ends

var app = builder.Build();

// Configure Starts
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-startupcs-and-changes-after-net-6-53953113/?t=115)

---

## 14. Third party libraries

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/third-party-libraries-53953114/) · 1:49

### Summary

This lesson evaluates the necessity of third-party Dependency Injection (DI) containers in .NET development.
While acknowledging popular alternatives like Autofac, Lamar, and SimpleInjector, it emphasizes that the built-in Microsoft DI framework is typically sufficient for most applications.
The native container offers superior performance, continuous optimization by Microsoft, and can be enhanced with specialized libraries like Scrutor to provide advanced features without switching to a completely different framework.

### Key concepts

* **Third-party IoC Containers**: Popular alternatives include Autofac, Lamar (the successor to StructureMap), and SimpleInjector.
* **Performance**: The built-in Microsoft DI container is generally faster than third-party alternatives and receives performance boosts with each .NET release.
* **Extensibility**: Libraries like Scrutor can be used to add advanced features, such as assembly scanning, to the built-in container without replacing it.
* **Standardization**: Sticking to the built-in framework ensures long-term support and compatibility as Microsoft maintains and optimizes the library.

### Lesson notes

While the .NET ecosystem includes several mature third-party Dependency Injection and Inversion of Control (IoC) containers, the built-in Microsoft framework is the primary focus for modern development.
Common third-party options available via NuGet include:

* **Autofac**: A widely used, feature-rich IoC container.
* **Lamar**: The successor to StructureMap, designed to be a fast and modern alternative.
* **SimpleInjector**: A container known for its advanced features and strict verification.

The standard approach in .NET applications involves using the `IServiceCollection` to register dependencies during the application startup phase.

```csharp
var builder = WebApplication.CreateBuilder(args);

// ConfigureServices Starts
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddTransient<IWeatherService, OpenWeatherService>();
builder.Services.AddScoped<LifetimeIndicatorFilter>();
builder.Services.AddScoped<IdGenerator>();
// ConfigureService Ends

var app = builder.Build();

// Configure Starts
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/third-party-libraries-53953114/?t=10)

Despite the availability of these third-party tools, the built-in framework is recommended for most scenarios.
It is highly optimized and often outperforms third-party containers.
Because Microsoft maintains the built-in container, it receives significant performance improvements with each major version of .NET (for example, the transition from .NET 5 to .NET 6).

For developers who require advanced features not present in the base framework, libraries like **Scrutor** can be added.
Scrutor builds on top of the existing Microsoft DI framework rather than replacing it, providing capabilities like assembly scanning and decoration while maintaining the performance benefits of the native container.

```csharp
builder.Services.AddScoped<IdGenerator>();
// ConfigureService Ends

var app = builder.Build();

// Configure Starts
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
//Configure End

app.Run();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/third-party-libraries-53953114/?t=55)

By sticking to the built-in provider, developers ensure their applications remain lightweight and benefit from the ongoing optimizations provided by the .NET team.

---

## 15. Section recap

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953115/) · 1:09

### Summary

This lesson provides a comprehensive recap of Dependency Injection (DI) fundamentals in .NET, ranging from basic console application setups to integration within ASP.NET Core APIs.
It reviews the critical roles of the ServiceCollection and ServiceProvider, the behavioral differences between Transient, Scoped, and Singleton lifetimes, and the technical distinction between GetService and GetRequiredService.
Additionally, the lesson touches upon the evolution of service registration from the legacy Startup.cs model to modern .NET 6+ patterns and evaluates the necessity of third-party IoC containers.

### Key concepts

- **ServiceCollection vs. ServiceProvider**: The distinction between the container used for registration and the engine used for resolution.
- **Service Lifetimes**: The behavior and duration of Transient, Scoped, and Singleton services.
- **GetService vs. GetRequiredService**: Proper error handling and nullability when resolving services from the container.
- **Framework Evolution**: The transition from the legacy `Startup.cs` model to the modern .NET 6+ `Program.cs` structure.
- **Built-in DI Framework**: The efficiency and sufficiency of the native .NET DI implementation compared to third-party alternatives.

### Lesson notes

The implementation of dependency injection in .NET is centered around two main components: the `IServiceCollection` and the `IServiceProvider`.
The `IServiceCollection` is used during the application's startup phase to register services and define their lifetimes.
Once the application is built, the `IServiceProvider` is used to resolve those registered services.

In modern ASP.NET Core applications, this setup is typically handled in the `Program.cs` file.
The `WebApplicationBuilder` exposes the `Services` property for registration before the application is built into a `WebApplication` instance.

```csharp
builder.Services.AddScoped<IdGenerator>();
// ConfigureService Ends

var app = builder.Build();

// Configure Starts
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
//Configure End

app.Run();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953115/?t=10)

#### Service Lifetimes

A core aspect of DI is managing how long an object lives.
.NET provides three primary lifetimes:
- **Transient**: A new instance is created every time the service is requested from the container.
- **Scoped**: A single instance is created per scope (usually a single HTTP request in web applications).
- **Singleton**: A single instance is created the first time it is requested and persists for the entire lifetime of the application.

#### Service Resolution

A common point of confusion is the choice between `GetService` and `GetRequiredService`.
`GetService` returns `null` if the service has not been registered, which requires manual null checking.
In contrast, `GetRequiredService` throws an `InvalidOperationException` if the service is missing.
Using `GetRequiredService` is generally preferred for dependencies that are essential for the application's execution to ensure failures happen early and explicitly.

#### Framework Evolution

The lesson also highlights the architectural shift introduced in .NET 6.
Previously, service registration was handled in a separate `Startup.cs` file within a `ConfigureServices` method, while the middleware pipeline was configured in a `Configure` method.
Modern .NET consolidates these into a single flow in `Program.cs`, though the underlying logic of the service collection remains the same.

Finally, while third-party Inversion of Control (IoC) containers and DI libraries (such as Autofac or Ninject) are available, the built-in .NET DI framework is highly efficient and feature-complete for the vast majority of software requirements, often making external libraries unnecessary.
