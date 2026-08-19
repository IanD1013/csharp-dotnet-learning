# Deep dive

> Course: [From Zero to Hero: Dependency Injection in .NET with C#](https://dometrain.com/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp/) · Chapter 5
> 11 lessons · ~43:36
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Do you need an interface for everything?](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/do-you-need-an-interface-for-everything-53953190/) | 4:59 | [↓](#1-do-you-need-an-interface-for-everything) |
| 2 | [Choosing the right dependency lifetime](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/choosing-the-right-dependency-lifetime-53953191/) | 2:31 | [↓](#2-choosing-the-right-dependency-lifetime) |
| 3 | [Dependency resolving issues and limitations](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-resolving-issues-and-limitations-53953192/) | 7:43 | [↓](#3-dependency-resolving-issues-and-limitations) |
| 4 | [Registering open generics](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-open-generics-53953193/) | 2:38 | [↓](#4-registering-open-generics) |
| 5 | [Registering multiple interface implementations](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-multiple-interface-implementations-53953194/) | 3:22 | [↓](#5-registering-multiple-interface-implementations) |
| 6 | [The ServiceDescriptor](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-servicedescriptor-53953195/) | 4:22 | [↓](#6-the-servicedescriptor) |
| 7 | [Add vs TryAdd](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/add-vs-tryadd-53953196/) | 4:22 | [↓](#7-add-vs-tryadd) |
| 8 | [TryAddEnumerable](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/tryaddenumerable-53953197/) | 3:31 | [↓](#8-tryaddenumerable) |
| 9 | [Replacing dependencies](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/replacing-dependencies-53953199/) | 5:47 | [↓](#9-replacing-dependencies) |
| 10 | [Cleaning up service registration](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/cleaning-up-service-registration-53953200/) | 3:22 | [↓](#10-cleaning-up-service-registration) |
| 11 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953201/) | 0:59 | [↓](#11-section-recap) |

---

## 1. Do you need an interface for everything?

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/do-you-need-an-interface-for-everything-53953190/) · 4:59

### Summary

This lesson addresses the common misconception that every class in a .NET application requires an interface and must be registered for Dependency Injection.
While interfaces are essential for testability and replaceability, they can lead to over-engineering when applied dogmatically to components that do not require abstraction, such as simple mappers.
By comparing a static extension method approach to an injected mapper service, the lesson demonstrates that mappers should ideally remain pure property-to-property translators.
Injecting services into mappers is identified as a bad practice that hides business logic and complicates testing, suggesting that developers should favor pragmatism over rigid architectural rules.

### Key concepts

* **Purpose of DI**: Dependency Injection is primarily used to facilitate testability and the replaceability of components.
* **Pragmatism vs. Dogmatism**: Not every class needs an interface; only actual dependencies that require abstraction or replacement should be injected.
* **Mapper Responsibilities**: Mappers should focus exclusively on property-to-property mapping. They should not have services injected into them to perform business logic.
* **Static Extensions**: For simple, logic-less transformations, static extension methods are often more efficient and maintainable than injected services.

### Lesson notes

A common point of contention in .NET development is whether every component needs an interface.
While interfaces were used extensively in previous lessons to demonstrate Dependency Injection (DI), not everything needs to be injectable.
The main benefit of DI is the ability to replace or mock a dependency for testing.
If a component does not need to be replaced by definition, wrapping it in an interface may be unnecessary.

Consider a typical mapping scenario in a Weather API.
The application uses a `WeatherModel` internally but returns a `WeatherResponse` contract to the API consumer to maintain versioning and prevent breaking changes.
This mapping can be handled efficiently using a static extension method:

```csharp
using Weather.Api.Contracts;
using Weather.Api.Weather;

namespace Weather.Api.Mappers;

public static class MappingExtensions
{
    public static WeatherResponse MapToWeatherResponse(this WeatherModel weather)
    {
        return new WeatherResponse
        {
            City = weather.Name,
            Country = weather.Sys.Country,
            FeelsLike = weather.Main.FeelsLike,
            Humidity = weather.Main.Humidity,
            Temperature = weather.Main.Temp,
            Timezone = weather.Timezone
        };
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/do-you-need-an-interface-for-everything-53953190/?t=50)

In the controller, this extension method is called directly on the model returned by the weather service:

```csharp
using Microsoft.AspNetCore.Mvc;
using Weather.Api.Logging;
using Weather.Api.Mappers;
using Weather.Api.Weather;

namespace Weather.Api.Controllers;

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

        var weatherResponse = weather.MapToWeatherResponse();
        return Ok(weatherResponse);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/do-you-need-an-interface-for-everything-53953190/?t=65)

It is common to see developers attempt to "standardize" this by creating an `IMapper` interface and a concrete `Mapper` class, then registering it as a singleton:

```csharp
public class Mapper : IMapper
{
    public WeatherResponse MapToWeatherResponse(WeatherModel weather)
    {
        return new WeatherResponse
        {
            City = weather.Name,
            Country = weather.Sys.Country,
            FeelsLike = weather.Main.FeelsLike,
            Humidity = weather.Main.Humidity,
            Temperature = weather.Main.Temp,
            Timezone = weather.Timezone
        };
    }
}

public interface IMapper
{
    WeatherResponse MapToWeatherResponse(WeatherModel weather);
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/do-you-need-an-interface-for-everything-53953190/?t=145)

This approach requires registering the service in the DI container and injecting it into the controller constructor:

```csharp
builder.Services.AddSingleton<IMapper, Mapper>();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/do-you-need-an-interface-for-everything-53953190/?t=160)

```csharp
[ApiController]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherService _weatherService;
    private readonly IMapper _mapper;

    public WeatherForecastController(IWeatherService weatherService, IMapper mapper)
    {
        _weatherService = weatherService;
        _mapper = mapper;
    }
    // ...
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/do-you-need-an-interface-for-everything-53953190/?t=190)

While some consider this "good practice," it often stems from a prior bad practice: including complex logic in the mapper.
If a mapper requires other services to be injected into it to function, it is likely doing too much.
A mapper's sole job should be to assemble objects from provided parameters, not to query other services for information.
Hiding business logic inside a mapper is a violation of responsibilities.

Over-using interfaces for simple logic like mapping makes testing harder and allows for cross-boundary mistakes.
Developers should be pragmatic: if a component is untestable by nature or requires replaceability, use an interface.
If it is a simple utility or transformation, an interface is likely unnecessary.

```csharp
[HttpGet("weather/{city}")]
public async Task<IActionResult> GetCurrentWeather([FromRoute] string city)
{
    var weather = await _weatherService.GetCurrentWeatherAsync(city);
    if (weather == null)
    {
        return NotFound();
    }

    var weatherResponse = weather.MapToWeatherResponse();
    // var weatherResponse = _mapper.MapToWeatherResponse(weather);
    return Ok(weatherResponse);
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/do-you-need-an-interface-for-everything-53953190/?t=280)

---

## 2. Choosing the right dependency lifetime

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/choosing-the-right-dependency-lifetime-53953191/) · 2:31

### Summary

Choosing the appropriate dependency lifetime involves balancing performance, memory usage, and thread safety.
While Singletons are highly efficient for stateless services by reducing garbage collection overhead, Transients offer a safer approach for stateful or thread-sensitive logic at the cost of higher resource consumption.

### Key concepts

- **Singleton**: A single instance created once and shared throughout the application's lifetime.
- **Transient**: A new instance created every time the service is requested from the container.
- **Scoped**: A single instance shared within a specific scope, such as a single HTTP request in ASP.NET Core.
- **Statelessness**: Services without state are ideal candidates for Singleton registration to optimize memory and GC performance.
- **Thread Safety**: Critical consideration for Singletons, as they are accessed by multiple threads simultaneously.

### Lesson notes

In .NET Dependency Injection, services are registered with one of three lifetimes: Singleton, Transient, or Scoped.
A Singleton maintains a single instance throughout the entire application lifetime.
A Transient service creates a new instance every time it is resolved.
A Scoped service maintains one instance per scope, which in ASP.NET Core typically corresponds to a single HTTP request.

The choice between these lifetimes is driven by the service's intended behavior and resource requirements.
If a service is stateless and has no side effects, registering it as a Singleton is ideal.
This approach saves memory by avoiding repeated allocations and reduces the load on the garbage collector, which can improve overall application performance by minimizing the time spent on object disposal and collection.

If a service needs to maintain consistency within a specific operation or request without side effects across different requests, a Scoped lifetime is appropriate.
However, the choice of Scoped lifetime often depends on the lifetimes of the service's own dependencies, a topic explored in further detail in subsequent lessons.

As a general rule, Transient registration is the safest option because it avoids shared state issues, though it is the most expensive in terms of performance and memory.
Singletons are less safe due to potential thread-safety issues but are highly efficient if implemented correctly.
Developers should perform thorough testing and performance profiling to ensure the chosen lifetime aligns with the application's logic and concurrency requirements, especially when using Singletons where clashing state can lead to bugs.

---

## 3. Dependency resolving issues and limitations

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-resolving-issues-and-limitations-53953192/) · 7:43

### Summary

This lesson explores the critical limitations and common pitfalls when resolving services with different lifetimes in .NET Dependency Injection.
It covers the 'captive dependency' problem where shorter-lived services are promoted to longer lifetimes, the strict prohibition of injecting scoped services into singletons, and the resolution failure caused by circular dependencies.

### Key concepts

- **Captive Dependencies**: Shorter-lived services (Transient) becoming effectively long-lived when injected into Singletons.
- **Scope Validation**: The framework prevents injecting Scoped services into Singletons to avoid memory leaks and logic errors.
- **Circular Dependencies**: Scenarios where two or more services depend on each other, preventing the DI container from constructing the object graph.
- **Lifetime Cascading**: Understanding how the "highest" lifetime in a dependency chain dictates the behavior of the dependencies it consumes.

### Lesson notes

To understand how lifetimes interact and where limitations exist, we define three services, each representing a different lifetime: Transient, Scoped, and Singleton.
Each service generates a unique `Guid` upon instantiation.

```csharp
namespace Limitations.Api.Services;

public class TransientService
{
    public Guid Id { get; } = Guid.NewGuid();
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-resolving-issues-and-limitations-53953192/?t=25)

```csharp
namespace Limitations.Api.Services;

public class ScopedService
{
    public Guid Id { get; } = Guid.NewGuid();
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-resolving-issues-and-limitations-53953192/?t=85)

These services are registered in the DI container with their respective lifetimes:

```csharp
using Limitations.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<SingletonService>();
builder.Services.AddScoped<ScopedService>();
builder.Services.AddTransient<TransientService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-resolving-issues-and-limitations-53953192/?t=55)

In a standard controller, which has a scoped lifetime in ASP.NET Core, we can inject all three services.
In this scenario, the Singleton ID remains constant across all requests, the Scoped ID remains constant within a single request but changes between requests, and the Transient ID changes every time it is resolved.

```csharp
private readonly SingletonService _singletonService;
private readonly TransientService _transientService;
private readonly ScopedService _scopedService;

public LifetimeController(ScopedService scopedService,
    TransientService transientService,
    SingletonService singletonService)
{
    _scopedService = scopedService;
    _transientService = transientService;
    _singletonService = singletonService;
}

[HttpGet(
```

The lesson document returned by `get_lesson` is truncated at this point.
The remaining code shown on screen, from `search_code`:

```csharp
using Limitations.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Limitations.Api.Controllers;

[ApiController]
public class LifetimeController : ControllerBase
{
    private readonly SingletonService _singletonService;
    private readonly TransientService

    [HttpGet("life")]
    public IActionResult Get()
    {
        return Ok();
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-resolving-issues-and-limitations-53953192/?t=110)

```csharp
private readonly SingletonService _singletonService;
private readonly TransientService _transientService;
private readonly ScopedService _scopedService;

public LifetimeController(ScopedService scopedService,
    TransientService transientService,
    SingletonService singletonService)
{
    _scopedService = scopedService;
    _transientService = transientService;
    _singletonService = singletonService;
}

[HttpGet("lifetime")]
public IActionResult Get()
{
    return Ok();
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-resolving-issues-and-limitations-53953192/?t=125)

```csharp
public LifetimeController(ScopedService scopedService,
    TransientService transientService,
    SingletonService singletonService)
{
    _scopedService = scopedService;
    _transientService = transientService;
    _singletonService = singletonService;
}

[HttpGet("lifetime")]
public IActionResult Get()
{
    var ids = new
    {
        SingletonId = _singletonService.Id,
        _transientService
    };
    return Ok();
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-resolving-issues-and-limitations-53953192/?t=145)

```csharp
{
    _scopedService = scopedService;
    _transientService = transientService;
    _singletonService = singletonService;
}

[HttpGet("lifetime")]
public IActionResult Get()
{
    var ids = new
    {
        SingletonId = _singletonService.Id,
        TransientId = _transientService.Id
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-resolving-issues-and-limitations-53953192/?t=160)

```csharp
namespace Limitations.Api.Services;

public class SingletonService
{
    private readonly TransientService _

    public SingletonService()
    {

    }

    public Guid Id { get; } = Guid.NewGuid();
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-resolving-issues-and-limitations-53953192/?t=190)

```csharp
public class SingletonService
{
    private readonly TransientService _transientService;

    public SingletonService(TransientService transientService)
    {
        _transientService = transientService;
    }

    public Guid Id => _transientService.Id; //= Guid.NewGuid();
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-resolving-issues-and-limitations-53953192/?t=205)

```csharp
public class SingletonService
{
    private readonly TransientService _transientService;

    public SingletonService(TransientService transientService)
    {
        _transientService = transientService;
    }

    public Guid Id => _transientService.Id; //= Guid.NewGuid();
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-resolving-issues-and-limitations-53953192/?t=235)

```csharp
public class SingletonService
{
    private readonly ScopedService _scopedService;

    public SingletonService(ScopedService scopedService)
    {
        _scopedService = scopedService;
    }

    public Guid Id { get; } = Guid.NewGuid();
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-resolving-issues-and-limitations-53953192/?t=280)

```csharp
namespace Limitations.Api.Services;

public class SingletonService
{
    private readonly ScopedService _scopedService;

    public SingletonService(ScopedService scopedService)
    {
        _scopedService = scopedService;
    }

    public Guid Id => _scopedService.Id;// = Guid.NewGuid();
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-resolving-issues-and-limitations-53953192/?t=310)

```csharp
public class TransientService
{
    private readonly SingletonService _singletonService;

    public TransientService(SingletonService singletonService)
    {
        _singletonService = singletonService;
    }

    public Guid Id => _singletonService.Id;// = Guid.NewGuid();
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-resolving-issues-and-limitations-53953192/?t=340)

```csharp
namespace Limitations.Api.Services;

public class SingletonService
{
    private readonly ScopedService _scopedService;

    public SingletonService(ScopedService scopedService)
    {
        _scopedService = scopedService;
    }

    public Guid Id => _scopedService.Id;// = Guid.NewGuid();
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-resolving-issues-and-limitations-53953192/?t=370)

---

## 4. Registering open generics

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-open-generics-53953193/) · 2:38

### Summary

Registering open generics in the .NET Dependency Injection (DI) container allows for the resolution of generic types without explicitly defining every possible type parameter at registration time.
This is particularly useful when implementing patterns like the Logger Adapter, which wraps the standard ILogger to improve unit testability by avoiding internal components and extension methods that are difficult to mock.
By using the typeof operator with open generic syntax, developers can map an open interface to an open implementation, enabling the DI container to dynamically create the appropriate closed generic type at runtime.

### Key concepts

*   **Testability Challenges**: The default .NET `ILogger<T>` is difficult to unit test because it relies heavily on internal components and extension methods.
*   **Logger Adapter Pattern**: Creating a wrapper interface and class for logging allows for easier mocking and proxying of log calls.
*   **Open Generics**: Generic types where the type parameters have not yet been specified (e.g., `ILoggerAdapter<T>`).
*   **Typeof Operator**: The mechanism used in .NET to pass open generic types into DI registration methods.
*   **Runtime Resolution**: The DI container's ability to resolve a specific closed generic (like `ILoggerAdapter<WeatherForecastController>`) from an open generic registration.

### Lesson notes

For testability purposes, it is common to use a logger adapter rather than injecting `ILogger<T>` directly.
The standard .NET logger is difficult to unit test because it utilizes internal components and extension methods.
By using an adapter, you can inject a private readonly interface that makes the code unit-testable by calling methods through a proxy.

First, define the open generic interface for the adapter:

```csharp
namespace Weather.Api.Logging;

public interface ILoggerAdapter<TType>
{
    void LogInformation(string template, params object[] args);
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-open-generics-53953193/?t=10)

Next, implement the adapter.
The implementation wraps the standard `ILogger` and passes the generic type through:

```csharp
public class LoggerAdapter<TType> : ILoggerAdapter<TType>
{
    private readonly ILogger<LoggerAdapter<TType>> _logger;

    public LoggerAdapter(ILogger<LoggerAdapter<TType>> logger)
    {
        _logger = logger;
    }

    public void LogInformation(string template, params object[] args)
    {
        _logger.LogInformation(template, args);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-open-generics-53953193/?t=25)

The challenge arises when registering these types in the DI container.
You cannot use standard generic registration syntax for open generics because the type parameter `T` is not specified at the time of registration.
The following syntax is invalid C#:

```csharp
builder.Services.AddHttpClient();
builder.Services.AddTransient<IWeatherService, OpenWeatherService>();
builder.Services.AddSingleton<IMapper, Mapper>();

builder.Services.AddTransient<ILoggerAdapter<>, LoggerAdapter<>>();

var app = builder.Build();

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

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-open-generics-53953193/?t=85)

While you could manually register every closed version of the generic (e.g., for `WeatherForecastController`), this is impractical and unscalable as the application grows:

```csharp
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();
builder.Services.AddTransient<IWeatherService, OpenWeatherService>();
builder.Services.AddSingleton<IMapper, Mapper>();

builder.Services.AddTransient<ILoggerAdapter<WeatherForecastController>, LoggerAdapter<WeatherF

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-open-generics-53953193/?t=100)

The correct way to register open generics is to use the `typeof` operator.
This allows you to specify the generic type without its type arguments.
This method does not have compile-time type constraints, but it allows the DI container to resolve any requested closed generic type at runtime.

```csharp
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();
builder.Services.AddTransient<IWeatherService, OpenWeatherService>();
builder.Services.AddSingleton<IMapper, Mapper>();

builder.Services.AddTransient(typeof(ILoggerAdapter<>), typeof(lo));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-open-generics-53953193/?t=130)

Once registered, the adapter can be injected into any class, such as a controller, using the standard constructor injection pattern for closed generics:

```csharp
[ApiController]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherService _weatherService;
    //private readonly IMapper _mapper;
    private readonly ILoggerAdapter<WeatherForecastController> _logger;

    public WeatherForecastController(IWeatherService weatherService,
        ILoggerAdapter<WeatherForecastController> logger /*, IMapper mapper*/)
    {
        _weatherService = weatherService;
        _logger = logger;
        //_mapper = mapper;
    }

    [HttpGet("weather/{city}")]
    public async Task<IActionResult> GetCurrentWeather([FromRoute] string
    {
        var weather = await _weatherService.GetCurrentWeatherAsync
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-open-generics-53953193/?t=145)

---

## 5. Registering multiple interface implementations

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-multiple-interface-implementations-53953194/) · 3:22

### Summary

The .NET dependency injection framework allows multiple implementations to be registered for a single interface type.
When multiple services are registered, the container appends them to the internal service collection rather than overwriting previous entries.
Resolving a single instance of the interface returns the implementation that was registered last, while resolving an IEnumerable of the interface provides access to all registered implementations in the order they were added.

### Key concepts

- **Multiple Registrations**: The DI container supports adding multiple concrete types for the same service interface.
- **Service Collection Growth**: Each registration adds a new ServiceDescriptor to the collection; it does not replace existing ones.
- **Last-In-First-Out (LIFO) Resolution**: When a single instance is requested, the container provides the implementation from the most recent registration.
- **Collection Resolution**: Injecting `IEnumerable<T>` allows a consumer to receive all registered implementations of type T.

### Lesson notes

The .NET DI container allows for the registration of multiple implementations of the same interface.
For example, an application might register both a real service and an in-memory version for testing or fallback purposes.

```csharp
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddTransient<IWeatherService, OpenWeatherService>();
builder.Services.AddTransient<IWeatherService, InMemoryWeatherService>();

builder.Services.AddSingleton<IMapper, Mapper>();
builder.Services.AddTransient(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-multiple-interface-implementations-53953194/?t=70)

Internally, the `IServiceCollection` tracks every registration.
Adding a second implementation for `IWeatherService` increases the service count rather than replacing the first registration.
When the application attempts to resolve a single instance of `IWeatherService`, the DI container returns the implementation that was registered last (in this case, `InMemoryWeatherService`).

To work with all registered implementations, the consumer should inject an `IEnumerable<T>`.
This allows the application to access every registered instance in the order they were added to the container.

```csharp
public class WeatherForecastController : ControllerBase
{
    private readonly IEnumerable<IWeatherService> _weatherServices;
    //private readonly IMapper _mapper;
    private readonly ILoggerAdapter<WeatherForecastController> _logger;

    public WeatherForecastController(IEnumerable<IWeatherService> weatherServices,
        ILoggerAdapter<WeatherForecastController> logger /*, IMapper mapper*/)
    {
        _weatherServices = weatherServices;
        _logger = logger;
        //_mapper = mapper;
    }
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-multiple-interface-implementations-53953194/?t=130)

Once the collection is injected, standard LINQ methods can be used to interact with the services.
For instance, you might specifically want the first implementation registered (the `OpenWeatherService` in the previous example) rather than the default last-registered implementation.

```csharp
[HttpGet("weather/{city}")]
public async Task<IActionResult> GetCurrentWeather([FromRoute] string city)
{
    var first = _weatherServices.First();

    var weather = await first.GetCurrentWeatherAsync(city);
    if (weather == null)
    {
        return NotFound();
    }

    var weatherResponse = weather.MapToWeatherResponse();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-multiple-interface-implementations-53953194/?t=185)

The final structure of a controller handling multiple implementations involves defining the collection in the constructor and utilizing the specific service needed within the action methods.

```csharp
[ApiController]
public class WeatherForecastController : ControllerBase
{
    private readonly IEnumerable<IWeatherService> _weatherServices;
    //private readonly IMapper _mapper;
    private readonly ILoggerAdapter<WeatherForecastController> _logger;

    public WeatherForecastController(IEnumerable<IWeatherService> weatherServices,
        ILoggerAdapter<WeatherForecastController> logger /*, IMapper mapper*/)
    {
        _weatherServices = weatherServices;
        _logger = logger;
        //_mapper = mapper;
    }

    [HttpGet("weather/{city}")]
    public async Task<IActionResult> GetCurrentWeather([FromRoute] string city)
    {
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-multiple-interface-implementations-53953194/?t=195)

---

## 6. The ServiceDescriptor

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-servicedescriptor-53953195/) · 4:22

**Summary**

The ServiceDescriptor is the fundamental building block of the .NET Dependency Injection container, serving as the underlying object that defines how services are registered and resolved.
Every registration method, such as AddTransient or AddSingleton, ultimately creates and adds a ServiceDescriptor to the IServiceCollection.
Understanding this class is essential for advanced framework extensions, such as implementing decorators or interceptors, as it provides granular control over the service type, implementation strategy, and lifetime.

**Key concepts**

- The ServiceDescriptor class describes how a service should be registered and resolved.
- IServiceCollection is a collection of ServiceDescriptor objects.
- Standard extension methods like AddTransient are wrappers around ServiceDescriptor instantiation.
- Descriptors can be defined using implementation types, existing instances, or factory delegates.
- Manual registration is performed by adding a descriptor directly to the IServiceCollection.

**Lesson notes**

The ServiceDescriptor class is the core component behind service registration in .NET.
Whenever a service is registered using extension methods, the container is actually creating a ServiceDescriptor and storing it within the IServiceCollection.

```csharp
builder.Services.AddHttpClient();

builder.Services.AddTransient<IWeatherService, OpenWeatherService>();

builder.Services.AddSingleton<IMapper, Mapper>();
builder.Services.AddTransient(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-servicedescriptor-53953195/?t=10)

Internally, the IServiceCollection acts as a list of these descriptors.
This is evident when examining the interface implementation, which includes methods for adding and removing ServiceDescriptor items.

```csharp
/// <inheritdoc />
public bool Remove(ServiceDescriptor item);

/// <inheritdoc />
public void RemoveAt(int index);

#nullable disable
void ICollection<ServiceDescriptor>.Add(ServiceDescriptor item);

IEnumerator IEnumerable.GetEnumerator();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-servicedescriptor-53953195/?t=40)

A ServiceDescriptor explicitly defines the service type, the implementation type (or instance/factory), and the service lifetime.
Instead of using the standard extension methods, you can manually instantiate a descriptor.
For example, to register IWeatherService with an implementation of OpenWeatherService and a transient lifetime:

```csharp
var weatherServiceDescriptor =
    new ServiceDescriptor(typeof(IWeatherService), typeof(OpenWeatherService), ServiceLifetime.Transient);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-servicedescriptor-53953195/?t=120)

Once the descriptor is created, it is added to the service collection using the Add method.
This approach is functionally identical to using the AddTransient extension method.

```csharp
builder.Services.Add(weatherServiceDescriptor);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-servicedescriptor-53953195/?t=165)

The ServiceDescriptor also supports more complex registration scenarios, such as using a factory delegate to resolve dependencies manually from the IServiceProvider.
This is the same mechanism used behind the scenes when providing a custom factory to the standard registration methods.

```csharp
var weatherServiceDescriptor =
    new ServiceDescriptor(typeof(IWeatherService), provider =>
    {
        return new OpenWeatherService(provider.GetRequiredService<IHttpClientFactory>());
    }, ServiceLifetime.Transient);

builder.Services.Add(weatherServiceDescriptor);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-servicedescriptor-53953195/?t=235)

This descriptive approach is particularly useful when extending the framework or implementing advanced patterns like interceptors, as it provides the most fundamental way to interact with the Inversion of Control (IoC) container.

---

## 7. Add vs TryAdd

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/add-vs-tryadd-53953196/) · 4:22

### Summary

The Add method in .NET's IServiceCollection allows for multiple registrations of the same service type, where the last registered implementation is typically resolved by default.
In contrast, the TryAdd family of methods ensures that a service is only registered if no existing registration for that specific service type is present in the container.
This mechanism prevents accidental duplicate registrations and ensures that the first registered implementation takes precedence, which is particularly useful when building modular or extensible applications where default services might be overridden.

### Key concepts

- **Standard Add behavior**: Multiple registrations are allowed for the same service type; the last implementation registered is the one resolved by default.
- **TryAdd behavior**: A service is only added to the collection if the service type has not already been registered.
- **ServiceDescriptor**: A manual way to define service registrations that can be passed to both Add and TryAdd methods.
- **Typed variations**: Extension methods such as TryAddTransient, TryAddScoped, and TryAddSingleton provide the same protection as TryAdd with a more concise syntax.
- **First-in wins**: Unlike the standard Add methods, TryAdd preserves the initial registration and ignores subsequent attempts to register the same service type.

### Lesson notes

When using the standard registration methods or adding a `ServiceDescriptor` directly to the `IServiceCollection`, .NET allows multiple implementations to be registered for the same service type.
In scenarios where multiple implementations are registered for a single interface, the dependency injection container will resolve the last implementation registered when a single instance of that interface is requested.

```csharp
builder.Services.AddHttpClient();


//builder.Services.AddTransient<IWeatherService, OpenWeatherService>();
//builder.Services.AddTransient<IWeatherService, InMemoryWeatherService>();

var openWeatherServiceDescriptor =
    new ServiceDescriptor(typeof(IWeatherService), typeof(OpenWeatherService), ServiceLifetime.

var inMemWeatherServiceDescriptor =
    new ServiceDescriptor(typeof(IWeatherService), typeof(InMemoryWeatherService), ServiceLifet

builder.Services.Add(openWeatherServiceDescriptor);
builder.Services.Add(inMemWeatherServiceDescriptor);


builder.Services.AddSingleton<IMapper, Mapper>();
builder.Services.AddTransient(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));

var app = builder.Build();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/add-vs-tryadd-53953196/?t=55)

To prevent accidental multiple registrations or to ensure that a default implementation is only used if no other implementation has been provided, .NET provides the `TryAdd` method.
This method inspects the current state of the `IServiceCollection`.
If it finds that the service type (e.g., `IWeatherService`) is already registered, it will not add the new descriptor.
This creates a "first-in wins" scenario.

```csharp
var openWeatherServiceDescriptor =
    new ServiceDescriptor(typeof(IWeatherService), typeof(OpenWeatherService), ServiceLifetime.

var inMemWeatherServiceDescriptor =
    new ServiceDescriptor(typeof(IWeatherService), typeof(InMemoryWeatherService), ServiceLif

builder.Services.TryAdd(openWeatherServiceDescriptor);
builder.Services.TryAdd(inMemWeatherServiceDescriptor);

builder.Services.AddSingleton<IMapper, Mapper>();
builder.Services.AddTransient(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/add-vs-tryadd-53953196/?t=130)

Instead of manually constructing `ServiceDescriptor` objects, you can use the wrapped extension methods: `TryAddTransient`, `TryAddScoped`, and `TryAddSingleton`.
These methods offer the same benefits as `TryAdd` while maintaining the concise generic syntax used in standard registrations.
They are effective for preventing duplicate registrations of the same service type across different parts of an application.

```csharp
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();


builder.Services.TryAddTransient<IWeatherService, OpenWeatherService>();
builder.Services.TryAddTransient<IWeatherService, InMemoryWeatherService>();

// var openWeatherServiceDescriptor =
//     new ServiceDescriptor(typeof(IWeatherService), typeof(OpenWeatherService), ServiceLifeti
//
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/add-vs-tryadd-53953196/?t=220)

---

## 8. TryAddEnumerable

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/tryaddenumerable-53953197/) · 3:31

**Summary**

TryAddEnumerable is a specialized registration method in .NET Dependency Injection used to ensure that a specific implementation of a service type is only registered once.
Unlike TryAdd, which skips registration if any implementation for the service type exists, TryAddEnumerable only skips registration if the exact service type and implementation type pair is already present in the container.
This makes it ideal for scenarios where multiple different implementations of the same interface—such as multiple weather providers or plugins—should be allowed, while preventing redundant duplicate registrations of the same specific provider.

**Key concepts**

*   **Service Type vs. Implementation Type Matching**: TryAddEnumerable checks both the service type (interface) and the implementation type (class) before deciding whether to add the service.
*   **ServiceDescriptor Requirement**: Unlike standard registration methods, TryAddEnumerable requires the use of the `ServiceDescriptor` class.
*   **Deduplication**: It prevents the same implementation from being registered multiple times for the same service type, which is useful when building modular or extensible systems.
*   **Enumerable Support**: The method can accept a single `ServiceDescriptor` or an `IEnumerable<ServiceDescriptor>` to register multiple implementations at once.

**Lesson notes**

In standard dependency injection registration, `TryAdd` methods check if any implementation for a specific service type is already registered.
If a registration exists, `TryAdd` ignores subsequent registration attempts for that service type, regardless of whether the implementation class is different.
`TryAddEnumerable` changes this behavior by checking for the existence of the specific service type and implementation type pair.

To use `TryAddEnumerable`, you must utilize the `ServiceDescriptor` approach.
This involves explicitly defining the service type, the implementation type, and the lifetime.

```csharp
// builder.Services.TryAddTransient<IWeatherService, OpenWeatherService>();
// builder.Services.TryAddTransient<IWeatherService, InMemoryWeatherService>();

var openWeatherServiceDescriptor =
    new ServiceDescriptor(typeof(IWeatherService), typeof(OpenWeatherService), ServiceLifetime.

var inMemWeatherServiceDescriptor =
    new ServiceDescriptor(typeof(IWeatherService), typeof(InMemoryWeatherService), ServiceLifet

builder.Services.TryAddEnumerable(openWeatherServiceDescriptor);

// builder.Services.TryAdd(openWeatherServiceDescriptor);
// builder.Services.TryAdd(inMemWeatherServiceDescriptor);

builder.Services.AddSingleton<IMapper, Mapper>();
builder.Services.AddTransient(typeof(ILoggerAdapter<>), typeof(LoggerAdapte

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/tryaddenumerable-53953197/?t=55)

When using `TryAddEnumerable`, you can register multiple different implementations for the same interface.
For example, if you register `OpenWeatherService` and then `InMemoryWeatherService`, both will be added to the container because their implementation types differ, even though they share the `IWeatherService` interface.

```csharp
// builder.Services.TryAddTransient<IWeatherService, OpenWeatherService>();
// builder.Services.TryAddTransient<IWeatherService, InMemoryWeatherService>();

var openWeatherServiceDescriptor =
    new ServiceDescriptor(typeof(IWeatherService), typeof(OpenWeatherService), ServiceLifetime.

var inMemWeatherServiceDescriptor =
    new ServiceDescriptor(typeof(IWeatherService), typeof(InMemoryWeatherService), ServiceLifet

builder.Services.TryAddEnumerable(openWeatherServiceDescriptor);
builder.Services.TryAddEnumerable(inMemWeatherServiceDescriptor);

// builder.Services.TryAdd(openWeatherServiceDescriptor);
// builder.Services.TryAdd(inMemWeatherServiceDescriptor);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/tryaddenumerable-53953197/?t=70)

If you attempt to register the exact same implementation for the same service type multiple times, `TryAddEnumerable` will detect the existing registration and skip the duplicate.
This ensures that the container does not end up with redundant entries of the same class.

```csharp
var openWeatherServiceDescriptor =
    new ServiceDescriptor(typeof(IWeatherService), typeof(OpenWeatherService), ServiceLifetime.

var inMemWeatherServiceDescriptor =
    new ServiceDescriptor(typeof(IWeatherService), typeof(InMemoryWeatherService), ServiceLifet

builder.Services.TryAddEnumerable(openWeatherServiceDescriptor);
builder.Services.TryAddEnumerable(openWeatherServiceDescriptor);
builder.Services.TryAddEnumerable(inMemWeatherServiceDescriptor);

// builder.Services.TryAdd(openWeatherServiceDescriptor);
// builder.Services.TryAdd(inMemWeatherServiceDescriptor);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/tryaddenumerable-53953197/?t=115)

The method also supports an overload that accepts an `IEnumerable<ServiceDescriptor>`.
This allows you to pass a collection, such as an array or a list of descriptors, in a single call.
The DI container will iterate through the collection and apply the same logic: adding only those descriptors that do not already have a matching service type and implementation type pair in the container.

```csharp
var openWeatherServiceDescriptor =  openWeatherServiceDescriptor: Lifetime = Transien...
    new ServiceDescriptor(typeof(IWeatherService), typeof(OpenWeatherService), ServiceLifetime.

var inMemWeatherServiceDescriptor =  inMemWeatherServiceDescriptor: Lifetime = Transient, Serv
    new ServiceDescriptor(typeof(IWeatherService), typeof(InMemoryWeatherService), ServiceLifet

builder.Services.TryAddEnumerable(openWeatherServiceDescriptor);
builder.Services.TryAddEnumerable(openWeatherServiceDescriptor);
builder.Services.TryAddEnumerable(inMemWeatherServiceDescriptor);

builder.Services.TryAddEnumerable(new []{openWeatherServiceDescriptor, inMemWeatherServiceDescr

// builder.Services.TryAdd(openWeatherServiceDescriptor);
// builder.Services.TryAdd(inMemWeatherServiceDescriptor);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/tryaddenumerable-53953197/?t=175)

---

## 9. Replacing dependencies

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/replacing-dependencies-53953199/) · 5:47

### Summary

This lesson explains how to manipulate the IServiceCollection by removing or replacing existing service registrations before the service provider is built.
It covers various removal techniques—including type-based removal, index-based removal, and descriptor-based removal—while highlighting critical pitfalls such as the requirement for reference equality when removing services via ServiceDescriptor.

### Key concepts

- **Service Collection Mutability**: The `IServiceCollection` is open for manipulation until `builder.Build()` is called; registrations after this point are ignored.
- **Type-based Removal**: `RemoveAll(typeof(T))` removes all registrations associated with a specific service type.
- **Index-based Removal**: `RemoveAt(index)` allows for removing services at a specific position, though this requires careful index management.
- **Reference Equality in Removal**: When using `Remove(ServiceDescriptor)`, the DI container looks for the exact object reference in memory, not a descriptor with matching values.
- **Use Cases**: These techniques are essential for implementing decorators, service redirections, or hiding complex registration logic within library extension methods.

### Lesson notes

In .NET, the `IServiceCollection` remains open for manipulation until the `builder.Build()` method is called.
Whether using the modern `Program.cs` builder pattern or the older `Startup.cs` approach, the service provider becomes immutable once created.
Any service registrations attempted after the builder has produced the provider will be ignored by the application.

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
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/replacing-dependencies-53953199/?t=10)

Before the build step, you can mutate the collection to implement service redirections or decorators.
One common method is `RemoveAll`, which clears all registrations for a specific service type.
This is useful when you want to ensure that no previous registrations for an interface interfere with your new implementation.

```csharp
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();


builder.Services.AddTransient<IWeatherService, OpenWeatherService>();
builder.Services.AddTransient<IWeatherService, InMemoryWeatherService>();

builder.Services.RemoveAll(typeof(IWeatherService));
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/replacing-dependencies-53953199/?t=85)

You can also remove services by index using `RemoveAt`.
However, hardcoding indices is generally discouraged because the collection size changes as services are added by the framework or other libraries.
If you use this method, the index should be calculated dynamically.

```csharp
builder.Services.AddHttpClient();


builder.Services.AddTransient<IWeatherService, OpenWeatherService>();
builder.Services.AddTransient<IWeatherService, InMemoryWeatherService>();

builder.Services.RemoveAt(190);


// var openWeatherServiceDescriptor =
//     new ServiceDescriptor(typeof(IWeatherService), typeof(OpenWeatherService), ServiceLifeti
//
// var inMemWeatherServiceDescriptor =
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/replacing-dependencies-53953199/?t=145)

A more granular approach involves using `ServiceDescriptor`.
A common pitfall occurs when attempting to remove a service by creating a new `ServiceDescriptor` instance with identical values to an existing one.
Because `ServiceDescriptor` is a class, the `Remove` method checks for reference equality.
If the descriptor passed to `Remove` is a different instance in memory than the one currently in the collection, the removal will fail, and the service count will remain unchanged.

```csharp
builder.Services.AddHttpClient();

builder.Services.AddTransient<IWeatherService, OpenWeatherService>();
builder.Services.AddTransient<IWeatherService, InMemoryWeatherService>();

var openWeatherServiceDescriptor =
    new ServiceDescriptor(typeof(IWeatherService), typeof(OpenWeatherService), ServiceLifetime.

builder.Services.Remove(openWeatherServiceDescriptor);

// var openWeatherServiceDescriptor =
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/replacing-dependencies-53953199/?t=235)

To successfully remove a service using a descriptor, you must maintain a reference to the exact object used during registration.
By adding the specific descriptor instance to the collection and then calling `Remove` with that same instance, the DI container can correctly identify and excise the entry.

```csharp
var openWeatherServiceDescriptor =
    new ServiceDescriptor(typeof(IWeatherService), typeof(OpenWeatherService), ServiceLifetime.

var inMemWeatherServiceDescriptor =
    new ServiceDescriptor(typeof(IWeatherService), typeof(InMemoryWeatherService), ServiceLifet

builder.Services.Add(openWeatherServiceDescriptor);
builder.Services.Add(inMemWeatherServiceDescriptor);

builder.Services.Remove(openWeatherServiceDescriptor);


builder.Services.AddSingleton<IMapper, Mapper>();
builder.Services.AddTransient(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/replacing-dependencies-53953199/?t=295)

---

## 10. Cleaning up service registration

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/cleaning-up-service-registration-53953200/) · 3:22

Refactoring service registrations into extension methods on IServiceCollection is a key technique for maintaining a clean and manageable Program.cs file.
By grouping related services—such as those for a specific domain like weather—into dedicated static classes, developers can improve code organization and maintainability.
Furthermore, using the Microsoft.Extensions.DependencyInjection namespace for these extension methods enhances the developer experience by making the methods available without requiring additional namespace imports in the consuming code.

### Key concepts

- **Extension Methods**: Utilizing static methods with the `this` keyword on `IServiceCollection` to extend the built-in dependency injection container.
- **Logical Grouping**: Consolidating related service registrations (e.g., HTTP clients, domain services, and descriptors) into a single, descriptive method.
- **Fluent API Design**: Returning the `IServiceCollection` instance from registration methods to support method chaining.
- **Namespace Spoofing**: Placing extension methods within the `Microsoft.Extensions.DependencyInjection` namespace to simplify consumption for end-users.

### Lesson notes

In a typical .NET application, the `Program.cs` or `Startup.cs` file can become bloated as the number of services grows.
Manual registration of multiple service descriptors and related dependencies leads to a cluttered and difficult-to-read startup sequence.

```csharp
builder.Services.AddHttpClient();

// builder.Services.AddTransient<IWeatherService, OpenWeatherService>();
// builder.Services.AddTransient<IWeatherService, InMemoryWeatherService>();

var openWeatherServiceDescriptor =
    new ServiceDescriptor(typeof(IWeatherService), typeof(OpenWeatherService), ServiceLifetime.

var inMemWeatherServiceDescriptor =
    new ServiceDescriptor(typeof(IWeatherService), typeof(InMemoryWeatherService), ServiceLifet

builder.Services.Add(openWeatherServiceDescriptor);
builder.Services.Add(inMemWeatherServiceDescriptor);

builder.Services.AddSingleton<IMapper, Mapper>();
builder.Services.AddTransient(typeof(ILoggerAdapter<>), typeof(LoggerAdapt

var app = builder.Build();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/cleaning-up-service-registration-53953200/?t=10)

To resolve this, you can create a static class, such as `WeatherServiceRegistration`, and define a static extension method on `IServiceCollection`.
This allows you to encapsulate all logic related to a specific feature set.

```csharp
namespace Weather.Api.Weather;

public static class WeatherServiceRegistration
{
    public static IServiceCollection AddWeatherServices(this IServiceCollection)
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/cleaning-up-service-registration-53953200/?t=70)

The logic for registering the specific services is moved from the main entry point into this new method.
It is essential to return the `services` object at the end of the method to maintain the fluent API pattern expected in .NET service registration.

```csharp
public static class WeatherServiceRegistration
{
    public static IServiceCollection AddWeatherServices(this IServiceCollection services)
    {
        // services.AddTransient<IWeatherService, OpenWeatherService>();
        // services.AddTransient<IWeatherService, InMemoryWeatherService>();

        var openWeatherServiceDescriptor =
            new ServiceDescriptor(typeof(IWeatherService), typeof(OpenWeatherService), ServiceLifet

        var inMemWeatherServiceDescriptor =
            new ServiceDescriptor(typeof(IWeatherService), typeof(InMemoryWeatherService), Serv

        services.Add(openWeatherServiceDescriptor);
        services.Add(inMemWeatherServiceDescriptor);

        return services;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/cleaning-up-service-registration-53953200/?t=115)

After refactoring, the `Program.cs` file is significantly cleaner.
You simply call the extension method on `builder.Services`, which hides the underlying complexity of the individual service descriptors.

```csharp
using Microsoft.Extensions.DependencyInjection.Extensions;
using Weather.Api.Controllers;
using Weather.Api.Logging;
using Weather.Api.Mappers;
using Weather.Api.Weather;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddWeatherServices();

builder.Services.AddSingleton<IMapper, Mapper>();
builder.Services.AddTransient(typeof(ILoggerAdapter<>), typeof(LoggerAdapter

var app = builder.Build();

if (app.Environment.IsDevelopment())
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/cleaning-up-service-registration-53953200/?t=145)

A common industry practice, especially for external libraries, is to change the namespace of the registration class to `Microsoft.Extensions.DependencyInjection`.
This technique ensures that the extension method is immediately available to the consumer as soon as they have the standard dependency injection package, without requiring an additional `using` statement for the library's specific internal namespace.

```csharp
namespace Microsoft.Extensions.DependencyInjection;

public static class WeatherServiceRegistration
{
    public static IServiceCollection AddWeatherServices(this IServiceCollection services)
    {
        // services.AddTransient<IWeatherService, OpenWeatherService>();
        // services.AddTransient<IWeatherService, InMemoryWeatherService>();

        var openWeatherServiceDescriptor =
            new ServiceDescriptor(typeof(IWeatherService), typeof(OpenWeatherService), Servicel

        var inMemWeatherServiceDescriptor =
            new ServiceDescriptor(typeof(IWeatherService), typeof(InMemoryWeatherService), Ser

        services.Add(openWeatherServiceDescriptor);
        services.Add(inMemWeatherServiceDescriptor);
        return services;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/cleaning-up-service-registration-53953200/?t=155)

---

## 11. Section recap

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953201/) · 0:59

This lesson provides a comprehensive summary of advanced Dependency Injection (DI) concepts in .NET, focusing on practical implementation strategies and architectural best practices.

### Key concepts

*   **Pragmatic Interface Usage**: Avoiding the "interface for everything" dogma in favor of practical design.
*   **Service Lifetimes**: Selecting appropriate lifetimes (Singleton, Scoped, Transient) and understanding resolution constraints.
*   **Circular Dependencies**: Identifying and mitigating issues caused by services depending on one another in a loop.
*   **Advanced Registration**: Utilizing open generics and the `ServiceDescriptor` for granular control over service definitions.
*   **Registration Logic**: Differentiating between `Add`, `TryAdd`, and `TryAddEnumerable` methods to manage service duplicates.
*   **Code Organization**: Using extension methods to refactor and clean up `Program.cs` or `Startup.cs`.

### Lesson notes

#### Architectural Decisions and Lifetimes

A significant portion of the deep dive focused on the pragmatic use of interfaces.
While interfaces are essential for decoupling and unit testing, they should not be applied dogmatically to every class.
Developers must evaluate whether an interface provides actual value for a specific component.

Choosing the correct service lifetime is critical for application stability.
Misconfiguring lifetimes—such as attempting to resolve a scoped service from a singleton—can lead to runtime exceptions or memory leaks.
Furthermore, understanding how the DI container resolves these services helps in identifying and fixing circular dependencies, where two or more services depend on each other, preventing successful instantiation.

#### Advanced Registration and Open Generics

The `ServiceDescriptor` is the fundamental building block of the DI container, containing all the information needed to create a service instance.
Beyond standard registrations, .NET supports open generics, allowing a single registration to handle any generic type parameter.

When managing multiple registrations of the same service type, the choice of method is important.
Standard `Add` methods will append registrations, while `TryAdd` ensures that a service is only registered if it hasn't been added previously.
`TryAddEnumerable` is specifically designed to manage multiple implementations of the same interface without adding duplicate implementation types.

#### Refactoring and Extension Methods

As an application grows, the `Program.cs` file can become cluttered with service registrations.
To maintain readability and organization, these registrations should be moved into specific extension methods on `IServiceCollection`.

```csharp
using Weather.Api.Logging;
using Weather.Api.Mappers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddWeatherServices();

builder.Services.AddSingleton<IMapper, Mapper>();
builder.Services.AddTransient(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953201/?t=10)

In the example above, `AddWeatherServices()` is an extension method that encapsulates related service registrations.
This snippet also demonstrates the registration of an open generic type using `typeof(ILoggerAdapter<>)` and `typeof(LoggerAdapter<>)`.
