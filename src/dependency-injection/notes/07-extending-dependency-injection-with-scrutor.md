# Extending Dependency Injection with Scrutor

> Course: [From Zero to Hero: Dependency Injection in .NET with C#](https://dometrain.com/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp/) · Chapter 7
> 11 lessons · ~43:56
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [What is Scrutor?](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/what-is-scrutor-53953303/) | 1:34 | [↓](#1-what-is-scrutor) |
| 2 | [Registering service decorators](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-service-decorators-53953304/) | 3:58 | [↓](#2-registering-service-decorators) |
| 3 | [Surprise optional refactoring lecture](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/surprise-optional-refactoring-lecture-53953305/) | 8:26 | [↓](#3-surprise-optional-refactoring-lecture) |
| 4 | [Service registration by scanning](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/service-registration-by-scanning-53953306/) | 13:09 | [↓](#4-service-registration-by-scanning) |
| 5 | [Interface marking](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/interface-marking-53953307/) | 3:28 | [↓](#5-interface-marking) |
| 6 | [Attribute marking](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/attribute-marking-53953308/) | 3:13 | [↓](#6-attribute-marking) |
| 7 | [Namespace filtering](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/namespace-filtering-53953309/) | 2:19 | [↓](#7-namespace-filtering) |
| 8 | [Using the ServiceDescriptor attribute](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-the-servicedescriptor-attribute-53953310/) | 2:47 | [↓](#8-using-the-servicedescriptor-attribute) |
| 9 | [Using RegistrationStrategies](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-registrationstrategies-53953311/) | 2:24 | [↓](#9-using-registrationstrategies) |
| 10 | [Potential pitfalls](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/potential-pitfalls-53953312/) | 1:52 | [↓](#10-potential-pitfalls) |
| 11 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953313/) | 0:46 | [↓](#11-section-recap) |

---

## 1. What is Scrutor?

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/what-is-scrutor-53953303/) · 1:34

### Summary

Scrutor is an open-source library created by Kristian Hellang that extends the built-in .NET Dependency Injection container with advanced features like assembly scanning and service decoration.
By providing a fluent API and efficient extension methods for IServiceCollection, Scrutor allows developers to automate service registration and implement the decorator pattern without the overhead of manual configuration or switching to a full third-party DI container.
It effectively elevates the minimalist Microsoft.Extensions.DependencyInjection framework to an industry-standard level, offering the power of more complex frameworks while maintaining the simplicity of the native .NET ecosystem.

### Key concepts

* **Open-source extension**: Enhances the standard `Microsoft.Extensions.DependencyInjection` library.
* **Assembly Scanning**: Automates the registration of multiple services based on conventions, interfaces, or attributes.
* **Decoration**: Provides a clean, fluent way to implement the Decorator pattern for services.
* **Standardization**: Bridges the feature gap between the built-in container and third-party alternatives like Autofac or Ninject.

### Lesson notes

Scrutor is a library designed to add missing functionality to the standard .NET dependency injection container.
While the built-in container is lightweight and sufficient for many tasks, it lacks native support for advanced registration techniques.
Scrutor addresses this by adding extension methods that enable two primary features: registration by scanning and service decoration.

To use Scrutor, the package must be added as a dependency in the project file.
The following example demonstrates a project configuration including the Scrutor library:

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
      <PackageReference Include="Scrutor" Version="3.3.0" />
    </ItemGroup>

</Project>
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/what-is-scrutor-53953303/?t=3)

In standard .NET development, scanning and decoration often require manual implementation.
Manual scanning involves iterating through types in an assembly and registering them individually, while manual decoration typically requires complex factory registrations.
Scrutor provides a more efficient and readable alternative through its fluent API.

The library is particularly valuable when developers face complaints that the built-in container is too "bare-bones".
By adding Scrutor, the container gains the vast majority of features found in larger, more complex DI packages.
The following code illustrates a basic setup using a `ServiceCollection` where Scrutor's extension methods would be applied to manage service registrations:

```csharp
using Microsoft.Extensions.DependencyInjection;
using ScrutorScanning.ConsoleApp.Services;

var services = new ServiceCollection();

services.AddTransient<IExampleAService, ExampleAService>();

PrintRegisteredService(services);
var serviceProvider = services.BuildServiceProvider();

void PrintRegisteredService(IServiceCollection serviceCollection)
{
    foreach (var service in serviceCollection)
    {
        Console.WriteLine($"{service.ServiceType.Name} -> {service.ImplementationType?.Name} as {service.Lifetime.ToString()}");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/what-is-scrutor-53953303/?t=26)

Scrutor's scanning capabilities are significantly more elaborate than simple name-based searches.
It allows for complex filtering and registration strategies, making it a highly flexible tool for managing large numbers of dependencies.
The implementation of these features typically begins with scanning, as it is a common requirement for modern .NET applications, followed by decoration to handle cross-cutting concerns.

---

## 2. Registering service decorators

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-service-decorators-53953304/) · 3:58

### Summary

This lesson demonstrates how to use the Scrutor library to implement the Decorator pattern in .NET Dependency Injection.
It contrasts the manual, verbose method of wrapping services with Scrutor's streamlined .Decorate<TService, TDecorator>() extension method, which handles the complexity of service replacement and dependency wrapping automatically.

### Key concepts

- **Decorator Pattern**: A structural pattern that allows behavior to be added to an individual object, dynamically, without affecting the behavior of other objects from the same class.
- **Scrutor**: A third-party library that extends the native .NET `IServiceCollection` with advanced registration capabilities.
- **Service Decoration**: The process of replacing a registered service with a new implementation that wraps the original implementation.
- **`Decorate` Method**: A Scrutor extension that simplifies the registration of decorators by automatically handling the injection of the inner service.
- **`TryDecorate` Method**: A variation that only applies the decorator if the service has not already been decorated, following standard .NET "Try" conventions.

### Lesson notes

Implementing the decorator pattern using the native .NET Dependency Injection container can be verbose and "clunky."
It typically requires registering the base implementation and then using a factory delegate to manually resolve dependencies and wrap the service.

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

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-service-decorators-53953304/?t=40)

In the example above, `LoggedWeatherService` acts as a decorator for `IWeatherService`.
It takes the inner service and a logger as dependencies to add timing and logging behavior around the weather retrieval logic.

```csharp
using System.Diagnostics;

namespace Weather.Api.Weather;

public class LoggedWeatherService : IWeatherService
{
    private readonly IWeatherService _weatherService;
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
            return await _weatherService.GetCurrentWeatherAsync(city);
        }
        finally
        {
            sw.Stop();
            _logger.LogInformation("Weather retrieval for city: {0}, took {1}ms",
                city, sw.ElapsedMilliseconds);
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-service-decorators-53953304/?t=85)

By installing the Scrutor library, we can replace the manual factory registration with the `Decorate` extension method.
This makes the code significantly cleaner and more concise.
Scrutor automatically handles removing the existing dependency registration, adding the new implementation, and re-injecting the original implementation into the decorator.

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();
builder.Services.AddTransient<IWeatherService, OpenWeatherService>();
builder.Services.Decorate<IWeatherService, LoggedWeatherService>();

builder.Services.AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));
builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-service-decorators-53953304/?t=145)

Scrutor also provides a `TryDecorate` method.
This follows the standard .NET convention where the decoration is only applied if the service has not already been decorated, preventing redundant layers of behavior if the registration code is executed multiple times.

---

## 3. Surprise optional refactoring lecture

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/surprise-optional-refactoring-lecture-53953305/) · 8:26

**Summary**

This lesson demonstrates a refactoring technique to replace repetitive manual profiling code—consisting of Stopwatch instances and try-finally blocks—with a clean, reusable IDisposable pattern.
By creating a TimedLogOperation class and extending the ILoggerAdapter, developers can time operations using a simple using statement, which automatically handles starting the timer, stopping it, and logging the elapsed time upon disposal.

**Key concepts**

*   Refactoring manual profiling logic into a reusable component.
*   Leveraging the `IDisposable` interface and `using` statements for automatic cleanup.
*   Extending `ILoggerAdapter` to provide a fluent `TimedOperation` method.
*   Using `Stopwatch.StartNew()` for precise execution timing.
*   Maintaining testability by wrapping logging and timing logic.

**Lesson notes**

Manual profiling often involves repetitive boilerplate code.
A common pattern is to initialize a `Stopwatch`, start it, and use a `try-finally` block to ensure the elapsed time is logged even if an exception occurs.
While functional, this approach introduces significant nesting and clutter.

```csharp
}

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
            _logger.LogInformation("Weather retrieval for city: {0}, completed in {1}ms",
                city, sw.ElapsedMilliseconds);
        }
    }

}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/surprise-optional-refactoring-lecture-53953305/?t=10)

To clean this up, we can leverage the C# `using` statement.
The `using` keyword is syntactic sugar for a `try-finally` block that calls the `Dispose()` method on an `IDisposable` object when it goes out of scope.
For example, when using an `HttpClient`, the compiler ensures the client is disposed correctly.

```csharp
}

    public async Task<WeatherResponse?> GetCurrentWeatherAsync(string city)
    {
        using (var client = new HttpClient())
        {
        }

        var sw = Stopwatch.StartNew();
        try
        {
            return await _weatherService.GetCurrentWeatherAsync(city);
        }
        finally
        {
            sw.Stop();
            _logger.LogInformation("Weather retrieval for city: {0}, completed in {1}ms",
                city, sw.ElapsedMilliseconds);
        }
    }

}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/surprise-optional-refactoring-lecture-53953305/?t=85)

### Implementing TimedLogOperation

We can encapsulate the timing and logging logic into a generic class called `TimedLogOperation<T>`.
This class implements `IDisposable`.
In its constructor, it starts a `Stopwatch` and stores the logger, log level, message template, and arguments.
In the `Dispose` method, it stops the timer and logs the result.

```csharp
private readonly object?[] _args;
    private readonly Stopwatch _stopwatch;

    public TimedLogOperation(ILoggerAdapter<T> logger,
        LogLevel logLevel, string message, object?[] args)
    {
        _logger = logger;
        _logLevel = logLevel;
        _message = message;
        _args = args;
        _stopwatch = Stopwatch.StartNew();
    }

    public void Dispose()
    {
        _stopwatch.Stop();
        _logger.Log(_logLevel, "{_message} completed in {_stopwatch.ElapsedMilliseconds}ms", _args);
    }
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/surprise-optional-refactoring-lecture-53953305/?t=235)

### Integrating with ILoggerAdapter

To make this operation easily accessible, we add a `TimedOperation` method to the `ILoggerAdapter<TType>` interface and its implementation.
This method returns an `IDisposable` (the `TimedLogOperation`), allowing it to be used directly within a `using` statement.

```csharp
namespace Weather.Api.Logging;

public class LoggerAdapter<TType> : ILoggerAdapter<TType>
{
    private readonly ILogger<LoggerAdapter<TType>> _logger;

    public LoggerAdapter(ILogger<LoggerAdapter<TType>> logger)
    {
        _logger = logger;
    }

    public void Log(LogLevel logLevel, string template, params object[] args)
    {
        _logger.Log(logLevel, template, args);
    }

    public void LogInformation(string template, params object[] args)
    {
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/surprise-optional-refactoring-lecture-53953305/?t=340)

### Final Usage

With the refactoring complete, the complex `try-finally` block in the service is replaced by a single `using var` declaration.
This significantly improves readability while maintaining the same profiling functionality.

```csharp
public async Task<WeatherResponse?> GetCurrentWeatherAsync(string city)
    {
        using var _ = _logger.TimedOperation("Weather retrieval for city: {0},", city);
        return await _weatherService.GetCurrentWeatherAsync(city);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/surprise-optional-refactoring-lecture-53953305/?t=385)

This pattern is highly flexible and can be applied anywhere a logger is available, such as within a controller to time the generation of a response.

```csharp
[HttpGet("weather/{city}")]
public async Task<IActionResult> GetCurrentWeather([FromRoute] string city)
{
    var weather = await _weatherService.GetCurrentWeatherAsync(city);

    using(var _ = _logger.TimedOperation("{0} response", nameof(GetCurrentWeather)))
    {
        if (weather == null)
        {
            return NotFound();
        }

        return Ok(weather);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/surprise-optional-refactoring-lecture-53953305/?t=475)

---

## 4. Service registration by scanning

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/service-registration-by-scanning-53953306/) · 13:09

### Summary

Service scanning in Scrutor allows for the automatic registration of services into the .NET Dependency Injection container based on conventions and rules rather than explicit manual registration.
By scanning assemblies, developers can apply filters—such as namespace matching or naming patterns—and define registration strategies like matching interfaces or self-registration.
This approach reduces boilerplate code and ensures that new services following established patterns are automatically included in the container with the appropriate lifetime.

### Key concepts

* **Assembly Scanning**: Identifying types within specific assemblies for registration automatically at startup.
* **Filtering**: Narrowing down types using namespaces, naming conventions (e.g., `EndsWith`), or interface implementation (`AssignableTo`).
* **Registration Strategies**: Defining how a class is registered, such as `AsMatchingInterface` (convention-based), `AsSelf`, or `AsImplementedInterfaces`.
* **Lifetime Management**: Overriding the default transient lifetime using methods like `WithSingletonLifetime` or `WithScopedLifetime`.
* **Hierarchical Configuration**: Nesting multiple scanning rules within a single `Scan` call to handle different registration requirements.

### Lesson notes

To begin using Scrutor's scanning capabilities, you must install the `Scrutor` and `Microsoft.Extensions.DependencyInjection` packages.
Scanning allows you to replace explicit service registrations with convention-based rules.

Consider a project with multiple services, such as `ExampleAService` and `ExampleBService`, each implementing their respective interfaces:

```csharp
namespace ScrutorScanning.ConsoleApp.Services;

public class ExampleBService : IExampleBService
{

}

public interface IExampleBService
{

}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/service-registration-by-scanning-53953306/?t=40)

To visualize what is registered in the container, a helper method can iterate through the `IServiceCollection` and print the service type, implementation type, and lifetime.

```csharp
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

PrintRegisteredService(services);
var serviceProvider = services.BuildServiceProvider();

void PrintRegisteredService(IServiceCollection serviceCollection)
{
    foreach (var service in serviceCollection)
    {
        Console.WriteLine($"{service.ServiceType.Name} -> {service.ImplementationType?.Name}");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/service-registration-by-scanning-53953306/?t=160)

#### Basic Scanning with AsMatchingInterface

The entry point for scanning is the `Scan` extension method on `IServiceCollection`.
The process follows a hierarchy: first, you select the assembly; second, you filter the classes; and third, you define the registration strategy.

```csharp
var services = new ServiceCollection();

//services.AddSingleton<IExampleAService, ExampleAService>();

services.Scan(selector =>
{
    selector.FromAssemblyOf<Program>()
        .AddClasses(f => f.InNamespaces("ScrutorScanning.ConsoleApp.Services"))
        .AsMatchingInterface();
});
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/service-registration-by-scanning-53953306/?t=280)

In this example, `AsMatchingInterface` automatically registers `ExampleAService` as `IExampleAService`.
Scrutor identifies these matches by looking for interfaces where the name matches the class name prefixed with "I".
If the names do not match (e.g., `ExampleABService` implementing `IExampleAService`), Scrutor will not register them using this specific strategy.

#### Registration Strategies and Lifetimes

Scrutor provides several strategies for registering discovered types:
* **AsSelf()**: Registers the class as its own type.
* **AsMatchingInterface()**: Registers the class against an interface with a matching name.
* **AsImplementedInterfaces()**: Registers the class against every interface it implements.
* **AsSelfWithInterfaces()**: Registers the class as itself and its interfaces, ensuring they all point to the same instance via an internal factory.

By default, Scrutor registers services with a **Transient** lifetime.
This can be overridden using lifetime methods:

```csharp
{
    selector.FromAssemblyOf<Program>()
        .AddClasses(f => f.InNamespaces("ScrutorScanning.ConsoleApp.Services"))
        .AsImplementedInterfaces()
        .WithSingletonLifetime();
});

PrintRegisteredService(services);
var serviceProvider = services.BuildServiceProvider();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/service-registration-by-scanning-53953306/?t=435)

When using `AsSelfWithInterfaces`, Scrutor effectively performs the following manual registration logic using a factory to ensure the singleton instance is shared across all registered types:

```csharp
// Manual equivalent of AsSelfWithInterfaces
services.AddSingleton<ExampleABService>();
services.AddSingleton<IExampleAService>(provider => provider.GetRequiredService<ExampleABService>());
services.AddSingleton<IExampleBService>(provider => provider.GetRequiredService<ExampleABService>());
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/service-registration-by-scanning-53953306/?t=535)

#### Advanced Filtering

Filters can be more dynamic than simple namespace matching.
You can use the `Where` method to filter by type name or other properties, or use `AssignableTo` to find types implementing a specific interface.

```csharp
services.Scan(selector =>
{
    selector.FromAssemblyOf<Program>()
        .AddClasses(f => f.Where(t => t.Name.EndsWith("Service")))
        .AsMatchingInterface()
        .WithSingletonLifetime();
});
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/service-registration-by-scanning-53953306/?t=585)

This is particularly useful for patterns like the Repository pattern, where you can register all repositories in one declaration:

```csharp
services.Scan(selector =>
{
    selector
        .FromAssemblyOf<Program>()
            .AddClasses(f => f.Where(t => t.Name.EndsWith("Repository")))
                .AsMatchingInterface()
                .WithScopedLifetime();
});
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/service-registration-by-scanning-53953306/?t=775)

#### Hierarchical Scanning

You can include multiple `AddClasses` calls or even multiple `FromAssemblyOf` calls within a single `Scan` block.
Each call to `AddClasses` resets the context for the next set of registration rules, allowing for complex, multi-layered registration logic within a single assembly or across multiple assemblies.

```csharp
services.Scan(selector =>
{
    selector
        .FromAssemblyOf<Program>()
            .AddClasses(f => f.InNamespaces("ScrutorScanning.ConsoleApp.Services"))
                .AsMatchingInterface()
                .WithSingletonLifetime()

            .AddClasses(f => f.Where(t => t.Name.EndsWith("Service")))
                .AsMatchingInterface()
                .WithSingletonLifetime();
});
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/service-registration-by-scanning-53953306/?t=685)

---

## 5. Interface marking

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/interface-marking-53953307/) · 3:28

Interface marking is a technique in Scrutor where empty "marker" interfaces are used to explicitly define the Dependency Injection lifetime of a class.
Instead of relying on naming conventions, which can lead to accidental registrations, developers implement specific interfaces (e.g., ISingletonService) on their classes.
Scrutor then scans for these types using the AssignableTo filter, allowing for precise control over registration and lifetime management within the service collection.

### Key concepts

* Marker Interfaces: Empty interfaces used to tag classes for specific registration behavior.
* AssignableTo<T>: A Scrutor filter that selects classes implementing a specific interface or inheriting from a specific type.
* Lifetime Mapping: Explicitly associating a marker interface with a DI lifetime (Singleton, Scoped, or Transient).
* Registration Strategies: Choosing between AsImplementedInterfaces and AsMatchingInterface to control how services are added to the container.

### Lesson notes

Interface marking provides a more robust alternative to name-based registration.
When using name-based filtering, such as checking if a class name ends with "Repository", there is a risk of accidental registrations if a class is named incorrectly or if the desired lifetime does not match the naming convention.

```csharp
services.Scan(selector =>
{
    selector
        .FromAssemblyOf<Program>()
        .AddClasses(f => f.Where(t => t.Name.EndsWith("Repository")))
        .AsMatchingInterface()
        .WithScopedLifetime();
});

PrintRegisteredService(services);
var serviceProvider = services.BuildServiceProvider();

void PrintRegisteredService(IServiceCollection serviceCollection)
{
    foreach (var service in serviceCollection)
    {
        Console.WriteLine($"{service.ServiceType.Name} -> {service.Implemer
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/interface-marking-53953307/?t=10)

Instead, developers can define empty marker interfaces to represent different lifetimes.
These interfaces serve no functional purpose other than to categorize types during assembly scanning.

```csharp
namespace ScrutorScanning.ConsoleApp.Services;

public interface IScopedService
{

}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/interface-marking-53953307/?t=40)

Services then implement these markers alongside their functional interfaces.
For example, a service intended to be transient would implement both its specific service interface and the `ITransientService` marker.

```csharp
namespace ScrutorScanning.ConsoleApp.Services;

public class ExampleBService : IExampleBService, ITransientService
{

}

public interface IExampleBService
{

}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/interface-marking-53953307/?t=55)

In the Scrutor configuration, the `AssignableTo<T>` method is used to filter classes by these markers.
This allows for chaining multiple registration rules within a single `Scan` call, with each rule targeting a different lifetime based on the implemented marker.

```csharp
.AddClasses(f => f.AssignableTo<ISingletonService>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime()
        .AddClasses(f => f.AssignableTo<ITransientService>())
            .AsImplementedInterfaces()
            .WithTransientLifetime()
        .AddClasses(f => f.AssignableTo<IScopedService>())
            .AsImplementedInterfaces()
            .WithScopedLifetime();
});

PrintRegisteredService(services);
var serviceProvider = services.BuildServiceProvider();

void PrintRegisteredService(IServiceCollection serviceCollection)
{
    foreach (var service in serviceCollection)
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/interface-marking-53953307/?t=100)

When using `AsImplementedInterfaces`, Scrutor registers the class under every interface it implements.
This means the service will be registered under its functional interface (e.g., `IExampleAService`) and the marker interface (e.g., `ISingletonService`).

```csharp
.AddClasses(f => f.AssignableTo<ISingletonService>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime()

        .AddClasses(f => f.AssignableTo<ITransientService>())
            .AsImplementedInterfaces()
            .WithTransientLifetime()

        .AddClasses(f => f.AssignableTo<IScopedService>())
            .AsImplementedInterfaces()

IExampleAService -> ExampleAService as Singleton
ISingletonService -> ExampleAService as Singleton
IExampleBService -> ExampleBService as Transient
ITransientService -> ExampleBService as Transient
IExampleCService -> ExampleCService as Scoped
IScopedService -> ExampleCService as Scoped
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/interface-marking-53953307/?t=115)

To prevent the marker interface itself from being registered as a service type, `AsMatchingInterface` can be used.
This strategy only registers the interface that matches the class name (e.g., `IExampleService` for `ExampleService`), effectively ignoring the marker interface for resolution purposes.
If naming conventions are not strictly followed, `AsImplementedInterfaces` remains the safer choice, even if it results in extra registrations that are never resolved.

```csharp
.AddClasses(f => f.AssignableTo<ISingletonService>())
                .AsMatchingInterface()
                .WithSingletonLifetime()

            .AddClasses(f => f.AssignableTo<ITransientService>())
                .AsMatchingInterface()
                .WithTransientLifetime()

            .AddClasses(f => f.AssignableTo<IScopedService>())
                .AsMatchingInterface()
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/interface-marking-53953307/?t=135)

While interface marking is a valid technique, it is sometimes criticized as a "leaky abstraction" because it requires the implementation class to have knowledge of its intended registration lifetime within the dependency injection container.
However, it provides a high degree of control and clarity for developers who prefer explicit marking over implicit conventions.

---

## 6. Attribute marking

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/attribute-marking-53953308/) · 3:13

### Summary

This lesson demonstrates how to use custom attributes to manage service lifetimes in Scrutor as an alternative to marker interfaces.
By defining and applying attributes like [Transient], [Scoped], and [Singleton] directly to service classes, developers can make dependency injection registration explicit and localized.
This approach avoids polluting the interface hierarchy with marker types while allowing Scrutor's scanning engine to filter and register services based on these custom metadata markers.

### Key concepts

*   **Custom Lifetime Attributes**: Creating specific attribute classes (e.g., `TransientAttribute`) to represent DI lifetimes.
*   **Attribute Constraints**: Using `[AttributeUsage(AttributeTargets.Class)]` to ensure lifetime markers are only applied to implementation classes.
*   **Scrutor Attribute Filtering**: Utilizing the `WithAttribute<T>` selector to identify classes for registration.
*   **Explicit Registration**: Improving code readability by making the DI lifetime visible directly on the class definition rather than hidden in a central configuration file.

### Lesson notes

While marker interfaces are a common way to categorize services for scanning, they can lead to unnecessary interface pollution.
An alternative approach is to use custom attributes to mark classes with their intended lifetime.
This makes the registration intent explicit on the class itself.

Initially, a project might use interface-based scanning as shown here:

```csharp
{
    selector
        .FromAssemblyOf<Program>()
        .AddClasses(f => f.AssignableTo<ISingletonService>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime()

        .AddClasses(f => f.AssignableTo<ITransientService>())
            .AsImplementedInterfaces()
            .WithTransientLifetime()

        .AddClasses(f => f.AssignableTo<IScopedService>())
            .AsImplementedInterfaces()
            .WithScopedLifetime();
});

PrintRegisteredService(services);
var serviceProvider = services.BuildServiceProvider();

void PrintRegisteredService(IServiceCollection serviceCollection)
{
    foreach (var service in serviceCollection)
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/attribute-marking-53953308/?t=10)

To move to an attribute-based model, you must first define the attributes.
These classes should extend the base `Attribute` class and use the `AttributeUsage` attribute to restrict their application to classes only.

```csharp
namespace ScrutorScanning.ConsoleApp.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class TransientAttribute : Attribute
{
    
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/attribute-marking-53953308/?t=40)

Similar attributes should be created for other lifetimes, such as Scoped:

```csharp
namespace ScrutorScanning.ConsoleApp.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class ScopedAttribute : Attribute
{
    
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/attribute-marking-53953308/?t=55)

Once the attributes are defined, they can be applied directly to the implementation classes.
This removes the need for the class to implement specific marker interfaces like `IScopedService` or `ITransientService` just for the sake of the DI container.

```csharp
using ScrutorScanning.ConsoleApp.Attributes;
using ScrutorScanning.ConsoleApp.ServiceMarkers;

namespace ScrutorScanning.ConsoleApp.Services;

[Scoped]
public class ExampleCService : IExampleCService
{
    
}

public interface IExampleCService
{
    
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/attribute-marking-53953308/?t=85)

After marking the classes, the Scrutor scanning configuration must be updated.
Instead of using `AssignableTo<T>`, use the `WithAttribute<T>` filter within the `AddClasses` method to target classes decorated with your custom attributes.

```csharp
.FromAssemblyOf<Program>()
    .AddClasses(f => f.WithAttribute<SingletonAttribute>())
        .AsImplementedInterfaces()
        .WithSingletonLifetime()

    .AddClasses(f => f.WithAttribute<TransientAttribute>())
        .AsImplementedInterfaces()
        .WithTransientLifetime()

    .AddClasses(f => f.WithAttribute<ScopedAttribute>())
        .AsImplementedInterfaces()
        .WithScopedLifetime();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/attribute-marking-53953308/?t=130)

This pattern is common in other ecosystems, such as Java.
While it does mean the class has knowledge of how it is registered in the DI container, it provides high visibility.
Anyone looking at the class can immediately see its intended lifecycle without searching through `Startup.cs` or various extension methods.

```csharp
using ScrutorScanning.ConsoleApp.Attributes;
using ScrutorScanning.ConsoleApp.ServiceMarkers;

namespace ScrutorScanning.ConsoleApp.Services;

[Transient]
public class ExampleBService : IExampleBService
{

}

public interface IExampleBService
{

}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/attribute-marking-53953308/?t=145)

---

## 7. Namespace filtering

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/namespace-filtering-53953309/) · 2:19

### Summary

Scrutor allows for scanning and registering classes based on their namespaces, which is effective when projects adhere to strict naming conventions for layers like repositories or services.
This approach eliminates the need for manual attribute decoration but imposes a uniform lifetime on all classes within a filtered namespace.
Because namespace strings are not type-safe and can change during refactoring, this method carries a risk of runtime failures unless supported by comprehensive unit testing.

### Key concepts

- **Namespace Matching**: Using `InNamespaces` to include classes from specific string-defined namespaces.
- **Lifetime Uniformity**: All classes registered within a single namespace filter must share the same lifetime (e.g., Singleton, Scoped, or Transient).
- **Attribute Independence**: Namespace-based registration can ignore or override attributes like `[Singleton]` if the scanner is configured to do so.
- **Exclusion Filters**: The `NotInNamespaces` method allows for excluding specific namespaces from a broader scan.
- **Maintenance Risk**: Namespace-based scanning is sensitive to refactoring; changes to namespace names will break the registration if the strings in the DI configuration are not updated.

### Lesson notes

When you have a consistent naming convention for your project, such as `Project.Repositories` or `Project.Services`, you can use namespace matching to register your dependencies.
This avoids the need to decorate every class with attributes.

Previously, registration might have relied heavily on attributes to define lifetimes:

```csharp
.FromAssemblyOf<Program>()
    .AddClasses(f => f.WithAttribute<SingletonAttribute>())
    .AsImplementedInterfaces()
    .WithSingletonLifetime()

    .AddClasses(f => f.WithAttribute<TransientAttribute>())
    .AsImplementedInterfaces()
    .WithTransientLifetime()

    .AddClasses(f => f.WithAttribute<ScopedAttribute>())
    .AsImplementedInterfaces()
    .WithScopedLifetime();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/namespace-filtering-53953309/?t=10)

To filter by namespace, use the `InNamespaces` method.
This method accepts an array of strings representing the namespaces to scan.

```csharp
.FromAssemblyOf<Program>()
    .AddClasses(f => f.InNamespaces(""))
    .AsImplementedInterfaces()
    .WithSingletonLifetime()

    .AddClasses(f => f.WithAttribute<TransientAttribute>())
    .AsImplementedInterfaces()
    .WithTransientLifetime()

    .AddClasses(f => f.WithAttribute<ScopedAttribute>())
    .AsImplementedInterfaces()
    .WithScopedLifetime();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/namespace-filtering-53953309/?t=25)

A significant limitation of namespace filtering is the lack of fine-grained control over lifetimes.
Every class identified within the specified namespace must be registered with the same lifetime.
If different lifetimes are required for classes within the same namespace, you must use more specific filters or return to attribute-based registration.

When using namespace scanning, existing attributes on classes are ignored unless specifically included in the filter logic.
For example, a class decorated with a `[Singleton]` attribute will still be registered according to the Scrutor configuration, even if that configuration specifies a different lifetime.

```csharp
using ScrutorScanning.ConsoleApp.Attributes;
using ScrutorScanning.ConsoleApp.ServiceMarkers;

namespace ScrutorScanning.ConsoleApp.Services;

[Singleton]
public class ExampleAService : IExampleAService
{
}

public interface IExampleAService
{
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/namespace-filtering-53953309/?t=55)

By specifying the exact namespace string, Scrutor will register all matching classes as the implemented interface.
In the following example, all services in the specified namespace are registered as singletons.

```csharp
.FromAssemblyOf<Program>()
    .AddClasses(f => f.InNamespaces("ScrutorScanning.ConsoleApp.Services"))
    .AsImplementedInterfaces()
    .WithSingletonLifetime();

});

PrintRegisteredService(services);
var serviceProvider = services.BuildServiceProvider();

void PrintRegisteredService(IServiceCollection serviceCollection)

// Terminal output
IExampleAService -> ExampleAService as Singleton
IExampleBService -> ExampleBService as Singleton
IExampleCService -> ExampleCService as Singleton
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/namespace-filtering-53953309/?t=85)

Scrutor also provides methods for exclusion.
You can use `NotInNamespaces` to register everything except classes within a specific namespace.
Similarly, you can use `WithoutAttribute` to exclude classes decorated with specific markers, providing control over edge cases.

```csharp
.FromAssemblyOf<Program>()
    .AddClasses(f => f.NotIn)
    .AsImplementedInterfaces()
    .WithSingletonLifetime();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/namespace-filtering-53953309/?t=110)

While powerful, namespace-based registration is considered risky because namespaces are subject to change during refactoring.
Since these strings are not verified at compile-time, changes can lead to runtime failures.
It is recommended to use unit tests to ensure that the expected services are correctly registered.

---

## 8. Using the ServiceDescriptor attribute

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-the-servicedescriptor-attribute-53953310/) · 2:47

### Summary

The ServiceDescriptor attribute in Scrutor provides a unified, attribute-based approach to defining service registration directly on implementation classes.
By using the .UsingAttributes() method during assembly scanning, developers can control service types and lifetimes without creating custom attribute classes.
This approach offers high flexibility, including the ability to register a single class under multiple service types or lifetimes using multiple attributes, though it introduces a direct dependency between the service implementation and its registration configuration.

### Key concepts

- **ServiceDescriptor Attribute**: A Scrutor-provided attribute used to decorate classes for automatic registration.
- **UsingAttributes()**: The scanning method required to process `ServiceDescriptor` and other registration attributes.
- **Default Registration**: Without parameters, the attribute registers the class as itself and its implemented interfaces with a Transient lifetime.
- **Explicit Configuration**: Parameters allow for defining specific service types and `ServiceLifetime` values.
- **Multiple Registrations**: Support for applying multiple `ServiceDescriptor` attributes to a single class for complex registration scenarios.

### Lesson notes

Scrutor provides a built-in `ServiceDescriptor` attribute that allows for fine-grained control over how a service is registered during assembly scanning.
When a class is decorated with this attribute, it can be automatically registered by calling `.UsingAttributes()` within the Scrutor scanning configuration.

```csharp
using Scrutor;
using ScrutorScanning.ConsoleApp.Attributes;
using ScrutorScanning.ConsoleApp.ServiceMarkers;

namespace ScrutorScanning.ConsoleApp.Services;

//[Singleton]
[ServiceDescriptor]
public class ExampleAService : IExampleAService
{

}

public interface IExampleAService
{

}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-the-servicedescriptor-attribute-53953310/?t=10)

To enable this, the scanning logic must include the `.UsingAttributes()` method.
This instructs Scrutor to look for the `ServiceDescriptor` attribute (or other custom attributes) on the classes it finds within the specified assembly.

```csharp
.WithTransientLifetime()

                .AddClasses(f => f.WithAttribute<ScopedAttribute>())
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()

            .FromAssemblyOf<Program>()
                .AddClasses()
                    .UsingAttributes();
});

PrintRegisteredService(services);
var serviceProvider = services.BuildServiceProvider();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-the-servicedescriptor-attribute-53953310/?t=40)

By default, if no parameters are passed to `[ServiceDescriptor]`, Scrutor registers the class as its own type and also as its implemented interface, both with a **Transient** lifetime.

You can explicitly define which interface or type the service should be registered as by passing the type to the attribute constructor.
When a type is specified, only that type is registered.

```csharp
using Scrutor;
using ScrutorScanning.ConsoleApp.Attributes;
using ScrutorScanning.ConsoleApp.ServiceMarkers;

namespace ScrutorScanning.ConsoleApp.Services;

//[Singleton]
[ServiceDescriptor(typeof(IExampleAService))]
public class ExampleAService : IExampleAService
{

}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-the-servicedescriptor-attribute-53953310/?t=65)

The attribute also supports specifying the `ServiceLifetime`.
For example, to register a service as a Singleton:

```csharp
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using ScrutorScanning.ConsoleApp.Attributes;
using ScrutorScanning.ConsoleApp.ServiceMarkers;

namespace ScrutorScanning.ConsoleApp.Services;

//[Singleton]
[ServiceDescriptor(typeof(IExampleAService), ServiceLifetime.Singleton)]
public class ExampleAService : IExampleAService
{

}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-the-servicedescriptor-attribute-53953310/?t=75)

If you want to register both the implementation class and its interface with a specific lifetime, you can pass `null` for the service type parameter.
This reverts to the default behavior of registering both self and interfaces but applies the specified lifetime to both.

```csharp
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using ScrutorScanning.ConsoleApp.Attributes;
using ScrutorScanning.ConsoleApp.ServiceMarkers;

namespace ScrutorScanning.ConsoleApp.Services;

//[Singleton]
[ServiceDescriptor(null, ServiceLifetime.Singleton)]
public class ExampleAService : IExampleAService
{

}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-the-servicedescriptor-attribute-53953310/?t=90)

For more complex scenarios, multiple `ServiceDescriptor` attributes can be applied to a single class.
This allows you to register the same implementation under different types with different lifetimes, and they will be registered in the order they appear on the class.

```csharp
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using ScrutorScanning.ConsoleApp.Attributes;
using ScrutorScanning.ConsoleApp.ServiceMarkers;

namespace ScrutorScanning.ConsoleApp.Services;

//[Singleton]
[ServiceDescriptor(typeof(ExampleAService), ServiceLifetime.Singleton)]
[ServiceDescriptor(typeof(IExampleAService), ServiceLifetime.Singleton)]
public class ExampleAService : IExampleAService
{
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-the-servicedescriptor-attribute-53953310/?t=115)

While this approach couples the class to its registration logic, it makes the registration details highly visible and provides a unified way to handle dependency injection without creating many custom attributes.

---

## 9. Using RegistrationStrategies

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-registrationstrategies-53953311/) · 2:24

**Summary**
This lesson explains how to control Scrutor's behavior when encountering duplicate service registrations using the UsingRegistrationStrategy method.
It covers the four primary strategies—Append, Skip, Replace, and Throw—detailing how each affects the service collection and providing recommendations for maintaining application stability during assembly scanning.

**Key concepts**
- **RegistrationStrategy.Append**: The default behavior; adds the service registration to the collection even if it already exists.
- **RegistrationStrategy.Skip**: If the service is already registered in the container, the new registration attempt is ignored.
- **RegistrationStrategy.Replace**: Overwrites any existing registration for the service with the new implementation and lifetime.
- **RegistrationStrategy.Throw**: Triggers an exception at startup if a duplicate registration is detected, ensuring configuration errors are caught immediately.

**Lesson notes**
When using Scrutor to scan assemblies, it is possible for a single class to match multiple registration criteria.
For example, a service might be defined as a singleton but also inadvertently included in a scan for transient services.

```csharp
using ScrutorScanning.ConsoleApp.ServiceMarkers;

namespace ScrutorScanning.ConsoleApp.Services;

//[Singleton]
public class ExampleAService : IExampleAService
{
}

public interface IExampleAService
{
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-registrationstrategies-53953311/?t=10)

```csharp
using ScrutorScanning.ConsoleApp.Attributes;
using ScrutorScanning.ConsoleApp.ServiceMarkers;

namespace ScrutorScanning.ConsoleApp.Services;

[Singl]
public class ExampleBService : IExampleBService
{
}

public interface IExampleBService
{
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-registrationstrategies-53953311/?t=20)

By default, if multiple `AddClasses` blocks match the same service, Scrutor will register the service multiple times.
This is equivalent to calling the standard `.Add()` method on the `IServiceCollection` multiple times.

```csharp
//          .AddClasses(f => f.AssignableTo<IScopedService>())
            //              .AsImplementedInterfaces()
            //              .WithScopedLifetime();

            .FromAssemblyOf<Program>()
                .AddClasses(f => f.WithAttribute<SingletonAttribute>())
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime()

                .AddClasses(f => f.WithAttribute<TransientAttribute>())
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()

                .AddClasses(f => f.WithAttribute<ScopedAttribute>())
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();

            //  .FromAssemblyOf<Program>()
            //      .AddClasses()
            //          .UsingAttributes();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-registrationstrategies-53953311/?t=30)

In the following output, `ExampleBService` is registered twice: once as a Singleton and once as a Transient.
This occurs because the default strategy is `Append`.

```csharp
.FromAssemblyOf<Program>()
                .AddClasses(f => f.WithAttribute<SingletonAttribute>())
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime()

IExampleAService -> ExampleAService as Singleton
IExampleBService -> ExampleBService as Singleton
IExampleBService -> ExampleBService as Transient
Process finished with exit code 0.
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-registrationstrategies-53953311/?t=40)

To change this behavior, use the `UsingRegistrationStrategy()` method.
This method is placed after `AddClasses()` but before the registration details like `AsImplementedInterfaces()`.

### Skip Strategy

Using `RegistrationStrategy.Skip` ensures that if a service is already present in the `IServiceCollection`, the current registration attempt is ignored.

```csharp
.AddClasses(f => f.WithAttribute<TransientAttribute>())
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()

                .AddClasses(f => f.WithAttribute<ScopedAttribute>())
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();

            //  .FromAssemblyOf<Program>()
            //      .AddClasses()
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-registrationstrategies-53953311/?t=85)

### Replace Strategy

Using `RegistrationStrategy.Replace()` will look for an existing registration and replace it with the new configuration.
In the example below, the Transient registration replaces the previous Singleton registration for `IExampleBService`.

```csharp
.AddClasses(f => f.WithAttribute<TransientAttribute>())
                    .UsingRegistrationStrategy(RegistrationStrategy.Replace())
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()

                .AddClasses(f => f.WithAttribute<ScopedAttribute>())
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();

IExampleAService -> ExampleAService as Singleton
IExampleBService -> ExampleBService as Transient
Process finished with exit code 0.
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-registrationstrategies-53953311/?t=100)

### Throw Strategy

`RegistrationStrategy.Throw` is the recommended approach for most applications.
It causes the application to fail immediately upon startup if a duplicate registration is detected, preventing accidental misconfigurations from reaching production.

```csharp
.WithSingletonLifetime()

            .AddClasses(f => f.WithAttribute<TransientAttribute>())
                .UsingRegistrationStrategy(RegistrationStrategy.Throw)
                .AsImplementedInterfaces()
                .WithTransientLifetime()

            .AddClasses(f => f.WithAttribute<ScopedAttribute>())
                .AsImplementedInterfaces()
                .WithScopedLifetime();

        // .FromAssemblyOf<Program>()
        //     .AddClasses()
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-registrationstrategies-53953311/?t=115)

---

## 10. Potential pitfalls

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/potential-pitfalls-53953312/) · 1:52

### Summary

Scrutor's assembly scanning capabilities provide a concise way to handle dependency injection but introduce risks such as reduced code discoverability and potential lifetime mismatches.
Over-reliance on scanning can hide registration logic, making it difficult for new developers to understand the system's configuration.
To mitigate these issues, developers should use specific filters and consider whether the responsibility of registration belongs within the class or the DI container configuration.

### Key concepts

- Hidden registration logic and reduced discoverability.
- Onboarding challenges for new developers due to implicit registrations.
- Architectural concerns regarding the placement of registration metadata (attributes vs. container configuration).
- Risk of incorrect service lifetimes (e.g., registering a singleton as transient).
- The importance of using specific filters in the `AddClasses` method.

### Lesson notes

Scrutor's scanning feature is a powerful tool for automating service registration, but it should be used sparingly and with highly specific filters.
While scanning reduces boilerplate, it can lead to maintenance challenges where registration logic becomes hidden.
This lack of transparency can increase the time required for new engineers to understand the application's dependency graph and how specific services are being resolved.

A significant architectural consideration is the location of registration metadata.
Using marker interfaces or attributes to drive scanning shifts the responsibility of Dependency Injection (DI) configuration from the container to the individual classes.
While attributes provide a clear link between a class and its intended lifetime, some architectural patterns suggest that classes should not be aware of how they are registered.

When implementing scanning, it is critical to use precise filters within the `AddClasses` method to prevent incorrect lifetime assignments.
For example, a service intended to be a Singleton might accidentally be registered as Transient if the scanning criteria are too broad.
Developers can use registration strategies, such as `RegistrationStrategy.Throw`, to handle conflicts and ensure that scanning behaves predictably.

```csharp
//          .AsImplementedInterfaces()
//          .WithScopedLifetime();

    .FromAssemblyOf<Program>()
        .AddClasses(f => f.WithAttribute<SingletonAttribute>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime()

        .AddClasses(f => f.WithAttribute<TransientAttribute>())
            .UsingRegistrationStrategy(RegistrationStrategy.Throw)
            .AsImplementedInterfaces()
            .WithTransientLifetime()

        .AddClasses(f => f.WithAttribute<ScopedAttribute>())
            .AsImplementedInterfaces()
            .WithScopedLifetime();

//  .FromAssemblyOf<Program>()
//      .AddClasses()
//          .UsingAttributes();
});
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/potential-pitfalls-53953312/?t=10)

Ultimately, while scanning is a useful feature of Scrutor, its decoration capabilities often provide more value with fewer architectural risks.
Developers should approach scanning with caution, ensuring that the convenience of concise code does not outweigh the need for explicit and maintainable service registrations.

---

## 11. Section recap

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953313/) · 0:46

### Summary

This lesson summarizes the benefits of using Scrutor to extend .NET's built-in dependency injection.
It highlights the decorator pattern implementation and various assembly scanning strategies, such as attribute-based registration, while cautioning about the potential side effects of automated dependency discovery.

### Key concepts

- **Scrutor Decoration**: Simplifies the registration of decorators compared to standard .NET DI.
- **Assembly Scanning**: Automatically discovers and registers types based on criteria like interfaces, namespaces, or attributes.
- **Registration Strategies**: Control how Scrutor handles duplicate or conflicting registrations (e.g., `RegistrationStrategy.Throw`).
- **Attribute-Based Registration**: Using custom attributes to define service lifetimes during scanning.

### Lesson notes

Scrutor provides a powerful extension to the standard .NET Dependency Injection (DI) container, specifically improving how decorators are handled.
Instead of manual, nested registrations that can become complex and difficult to maintain, Scrutor's decoration feature allows for a much cleaner and more readable implementation of the decorator pattern.

Beyond decoration, Scrutor introduces advanced assembly scanning capabilities.
This allows developers to automatically register dependencies based on various criteria, including interfaces, specific types, namespaces, or custom attributes.
This automation reduces the boilerplate code required in the `Program.cs` or startup configuration.

The following example demonstrates how to use assembly scanning with attribute marking to define lifetimes for different services within an assembly:

```csharp
//          .AsImplementedInterfaces()
    //          .WithScopedLifetime();

    .FromAssemblyOf<Program>()
        .AddClasses(f => f.WithAttribute<SingletonAttribute>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime()

        .AddClasses(f => f.WithAttribute<TransientAttribute>())
            .UsingRegistrationStrategy(RegistrationStrategy.Throw)
            .AsImplementedInterfaces()
            .WithTransientLifetime()

        .AddClasses(f => f.WithAttribute<ScopedAttribute>())
            .AsImplementedInterfaces()
            .WithScopedLifetime();

    //  .FromAssemblyOf<Program>()
    //      .AddClasses()
    //          .UsingAttributes();
});
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953313/?t=10)

While scanning simplifies registration, it requires careful implementation.
Developers must be mindful of potential side effects, such as accidentally registering types that should not be in the container or creating conflicting registrations.
Using specific registration strategies, such as `RegistrationStrategy.Throw`, can help manage these risks by ensuring the application fails explicitly if a registration conflict occurs.
