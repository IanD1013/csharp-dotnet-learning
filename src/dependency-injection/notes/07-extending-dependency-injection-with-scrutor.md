# Extending Dependency Injection with Scrutor

> 课程:[From Zero to Hero: Dependency Injection in .NET with C#](https://dometrain.com/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp/) · 第 7 章
> 共 11 课 · 约 43:56
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

---

## 课程索引

| #   | 课程                                                                                                                                                                                                       | 时长  | 小节                                             |
| --- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ----- | ------------------------------------------------ |
| 1   | [What is Scrutor?](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/what-is-scrutor-53953303/)                                                       | 1:34  | [↓](#1-what-is-scrutor)                          |
| 2   | [Registering service decorators](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-service-decorators-53953304/)                          | 3:58  | [↓](#2-registering-service-decorators)           |
| 3   | [Surprise optional refactoring lecture](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/surprise-optional-refactoring-lecture-53953305/)            | 8:26  | [↓](#3-surprise-optional-refactoring-lecture)    |
| 4   | [Service registration by scanning](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/service-registration-by-scanning-53953306/)                      | 13:09 | [↓](#4-service-registration-by-scanning)         |
| 5   | [Interface marking](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/interface-marking-53953307/)                                                    | 3:28  | [↓](#5-interface-marking)                        |
| 6   | [Attribute marking](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/attribute-marking-53953308/)                                                    | 3:13  | [↓](#6-attribute-marking)                        |
| 7   | [Namespace filtering](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/namespace-filtering-53953309/)                                                | 2:19  | [↓](#7-namespace-filtering)                      |
| 8   | [Using the ServiceDescriptor attribute](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-the-servicedescriptor-attribute-53953310/)            | 2:47  | [↓](#8-using-the-servicedescriptor-attribute)    |
| 9   | [Using RegistrationStrategies](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-registrationstrategies-53953311/)                              | 2:24  | [↓](#9-using-registrationstrategies)             |
| 10  | [Potential pitfalls](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/potential-pitfalls-53953312/)                                                  | 1:52  | [↓](#10-potential-pitfalls)                      |
| 11  | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953313/)                                                            | 0:46  | [↓](#11-section-recap)                           |

---

## 1. What is Scrutor?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/what-is-scrutor-53953303/) · 1:34

### 总结

Scrutor 是由 Kristian Hellang 创建的一个开源库,它为 .NET 内置的依赖注入容器扩展了程序集扫描、服务装饰之类的高级特性。
通过为 IServiceCollection 提供流式 API 和高效的扩展方法,Scrutor 让开发者可以自动化服务注册、实现装饰器模式,而不必承担手动配置的开销,也不必换成一个完整的第三方 DI 容器。
它实际上把极简的 Microsoft.Extensions.DependencyInjection 框架提升到了业界标准的水平,在保持原生 .NET 生态简洁性的同时,提供了那些更复杂框架才有的能力。

### 核心概念

* **Open-source extension(开源扩展)**:增强标准的 `Microsoft.Extensions.DependencyInjection` 库。
* **Assembly Scanning(程序集扫描)**:基于约定、接口或特性自动注册多个服务。
* **Decoration(装饰)**:提供一种干净、流式的方式来为服务实现装饰器模式。
* **Standardization(标准化)**:弥合内置容器与 Autofac、Ninject 等第三方替代方案之间的功能差距。

### 课程笔记

Scrutor 是一个旨在为标准 .NET 依赖注入容器补齐缺失功能的库。
内置容器虽然轻量,对许多任务来说也足够用,但它缺少对高级注册技术的原生支持。
Scrutor 通过添加扩展方法解决了这个问题,这些扩展方法带来了两项主要特性:按扫描注册和服务装饰。

要使用 Scrutor,必须把这个包作为依赖添加到项目文件中。
下面的示例展示了一个包含 Scrutor 库的项目配置:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/what-is-scrutor-53953303/?t=3)

在标准的 .NET 开发中,扫描和装饰往往需要手动实现。
手动扫描意味着遍历某个程序集中的类型并逐个注册,而手动装饰通常需要复杂的工厂注册。
Scrutor 通过它的流式 API 提供了一种更高效、更易读的替代方案。

当开发者面对“内置容器太简陋”这类抱怨时,这个库尤其有价值。
加上 Scrutor 之后,容器就获得了那些更大、更复杂的 DI 包中绝大多数的特性。
下面的代码展示了一个使用 `ServiceCollection` 的基础配置,Scrutor 的扩展方法会在这里被用来管理服务注册:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/what-is-scrutor-53953303/?t=26)

Scrutor 的扫描能力远不止基于名称的简单搜索那么简单。
它支持复杂的过滤和注册策略,这让它成为管理大量依赖时非常灵活的工具。
这些特性的实现通常从扫描开始,因为这是现代 .NET 应用的常见需求,然后再用装饰来处理横切关注点。

---

## 2. Registering service decorators

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-service-decorators-53953304/) · 3:58

### 总结

本课演示如何使用 Scrutor 库在 .NET 依赖注入中实现装饰器模式。
它把手动包装服务的那种冗长做法,与 Scrutor 精简的 .Decorate<TService, TDecorator>() 扩展方法作对比,后者会自动处理服务替换和依赖包装的复杂性。

### 核心概念

- **Decorator Pattern(装饰器模式)**:一种结构型模式,允许动态地给单个对象添加行为,而不影响同一个类的其他对象的行为。
- **Scrutor**:一个第三方库,为原生的 .NET `IServiceCollection` 扩展了高级注册能力。
- **Service Decoration(服务装饰)**:用一个包装了原实现的新实现,来替换已注册服务的过程。
- **`Decorate` Method(`Decorate` 方法)**:Scrutor 的一个扩展方法,它自动处理内层服务的注入,从而简化装饰器的注册。
- **`TryDecorate` Method(`TryDecorate` 方法)**:一个变体,只有当服务尚未被装饰时才应用装饰器,遵循标准的 .NET “Try” 约定。

### 课程笔记

用原生的 .NET 依赖注入容器实现装饰器模式,写起来既冗长又“笨拙”。
它通常需要先注册基础实现,然后用一个工厂委托来手动解析依赖并包装服务。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-service-decorators-53953304/?t=40)

在上面的例子中,`LoggedWeatherService` 充当 `IWeatherService` 的装饰器。
它把内层服务和一个 logger 作为依赖接收进来,从而在天气获取逻辑的外面加上计时和日志行为。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-service-decorators-53953304/?t=85)

安装了 Scrutor 库之后,我们可以用 `Decorate` 扩展方法替换掉手动的工厂注册。
这让代码明显更干净、更简洁。
Scrutor 会自动处理移除已有的依赖注册、添加新的实现,以及把原实现重新注入到装饰器中。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-service-decorators-53953304/?t=145)

Scrutor 还提供了一个 `TryDecorate` 方法。
它遵循标准的 .NET 约定:只有当服务尚未被装饰时才应用装饰,这样即便注册代码被执行多次,也不会叠加出多余的行为层。

---

## 3. Surprise optional refactoring lecture

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/surprise-optional-refactoring-lecture-53953305/) · 8:26

### 总结

本课演示一种重构技巧,用一个干净、可复用的 IDisposable 模式,替换掉那些重复的手动性能剖析代码,也就是由 Stopwatch 实例和 try-finally 块组成的代码。
通过创建一个 TimedLogOperation 类并扩展 ILoggerAdapter,开发者可以用一个简单的 using 语句为操作计时,它会自动处理启动计时器、停止计时器,并在释放时记录已耗费的时间。

### 核心概念

*   把手动的性能剖析逻辑重构成一个可复用的组件。
*   借助 `IDisposable` 接口和 `using` 语句实现自动清理。
*   扩展 `ILoggerAdapter`,提供一个流式的 `TimedOperation` 方法。
*   使用 `Stopwatch.StartNew()` 进行精确的执行计时。
*   通过包装日志和计时逻辑来保持可测试性。

### 课程笔记

手动做性能剖析往往牵涉到重复的样板代码。
一个常见的模式是:初始化一个 `Stopwatch`、启动它,然后用一个 `try-finally` 块来确保即使发生异常也会记录耗时。
这种做法虽然能工作,却带来了明显的嵌套和杂乱。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/surprise-optional-refactoring-lecture-53953305/?t=10)

为了把它清理干净,我们可以利用 C# 的 `using` 语句。
`using` 关键字是 `try-finally` 块的语法糖,当一个 `IDisposable` 对象离开作用域时,它会调用该对象的 `Dispose()` 方法。
举例来说,当使用 `HttpClient` 时,编译器会确保这个 client 被正确释放。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/surprise-optional-refactoring-lecture-53953305/?t=85)

#### 实现 TimedLogOperation

我们可以把计时和日志逻辑封装进一个名为 `TimedLogOperation<T>` 的泛型类。
这个类实现了 `IDisposable`。
在它的构造函数中,它启动一个 `Stopwatch`,并保存 logger、日志级别、消息模板和参数。
在 `Dispose` 方法中,它停止计时器并记录结果。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/surprise-optional-refactoring-lecture-53953305/?t=235)

#### 与 ILoggerAdapter 集成

为了让这个操作更易于使用,我们在 `ILoggerAdapter<TType>` 接口及其实现中加入一个 `TimedOperation` 方法。
这个方法返回一个 `IDisposable`(即 `TimedLogOperation`),从而可以直接用在 `using` 语句中。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/surprise-optional-refactoring-lecture-53953305/?t=340)

#### 最终用法

重构完成后,服务里那个复杂的 `try-finally` 块被一行 `using var` 声明取代。
这在保持同样的性能剖析功能的同时,显著提升了可读性。

```csharp
public async Task<WeatherResponse?> GetCurrentWeatherAsync(string city)
    {
        using var _ = _logger.TimedOperation("Weather retrieval for city: {0},", city);
        return await _weatherService.GetCurrentWeatherAsync(city);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/surprise-optional-refactoring-lecture-53953305/?t=385)

这个模式非常灵活,凡是有 logger 可用的地方都能用,比如在 controller 中为生成响应的过程计时。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/surprise-optional-refactoring-lecture-53953305/?t=475)

---

## 4. Service registration by scanning

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/service-registration-by-scanning-53953306/) · 13:09

### 总结

Scrutor 中的服务扫描允许基于约定和规则,而不是显式的手动注册,把服务自动注册进 .NET 依赖注入容器。
通过扫描程序集,开发者可以应用诸如命名空间匹配或命名模式之类的过滤器,并定义像匹配接口或自我注册这样的注册策略。
这种做法减少了样板代码,并确保遵循既有模式的新服务会自动以恰当的生命周期被纳入容器。

### 核心概念

* **Assembly Scanning(程序集扫描)**:在启动时自动识别特定程序集中可注册的类型。
* **Filtering(过滤)**:使用命名空间、命名约定(例如 `EndsWith`)或接口实现关系(`AssignableTo`)来收窄类型范围。
* **Registration Strategies(注册策略)**:定义一个类如何被注册,例如 `AsMatchingInterface`(基于约定)、`AsSelf` 或 `AsImplementedInterfaces`。
* **Lifetime Management(生命周期管理)**:用 `WithSingletonLifetime` 或 `WithScopedLifetime` 之类的方法覆盖默认的 transient 生命周期。
* **Hierarchical Configuration(层级化配置)**:在单次 `Scan` 调用中嵌套多条扫描规则,以应对不同的注册需求。

### 课程笔记

要开始使用 Scrutor 的扫描能力,你必须安装 `Scrutor` 和 `Microsoft.Extensions.DependencyInjection` 这两个包。
扫描让你可以用基于约定的规则取代显式的服务注册。

设想一个项目里有多个服务,比如 `ExampleAService` 和 `ExampleBService`,它们各自实现了对应的接口:

```csharp
namespace ScrutorScanning.ConsoleApp.Services;

public class ExampleBService : IExampleBService
{

}

public interface IExampleBService
{

}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/service-registration-by-scanning-53953306/?t=40)

为了看清容器里注册了什么,可以写一个辅助方法遍历 `IServiceCollection`,打印出服务类型、实现类型和生命周期。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/service-registration-by-scanning-53953306/?t=160)

#### 用 AsMatchingInterface 做基础扫描

扫描的入口是 `IServiceCollection` 上的 `Scan` 扩展方法。
整个过程遵循一个层级:首先选择程序集,其次过滤类,第三定义注册策略。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/service-registration-by-scanning-53953306/?t=280)

在这个例子中,`AsMatchingInterface` 会自动把 `ExampleAService` 注册为 `IExampleAService`。
Scrutor 识别这类匹配的方式,是寻找名称等于类名加上前缀 “I” 的接口。
如果名称对不上(例如 `ExampleABService` 实现了 `IExampleAService`),Scrutor 就不会用这种特定策略注册它们。

#### 注册策略与生命周期

Scrutor 为已发现的类型提供了几种注册策略:
* **AsSelf()**:把类注册为它自己的类型。
* **AsMatchingInterface()**:把类注册到一个名称相匹配的接口上。
* **AsImplementedInterfaces()**:把类注册到它实现的每一个接口上。
* **AsSelfWithInterfaces()**:把类同时注册为它自己和它的各个接口,并通过内部的工厂确保它们都指向同一个实例。

默认情况下,Scrutor 以 **Transient** 生命周期注册服务。
这可以用生命周期方法来覆盖:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/service-registration-by-scanning-53953306/?t=435)

使用 `AsSelfWithInterfaces` 时,Scrutor 实际上执行的是下面这段手动注册逻辑,用一个工厂来保证单例实例在所有注册类型之间共享:

```csharp
// Manual equivalent of AsSelfWithInterfaces
services.AddSingleton<ExampleABService>();
services.AddSingleton<IExampleAService>(provider => provider.GetRequiredService<ExampleABService>());
services.AddSingleton<IExampleBService>(provider => provider.GetRequiredService<ExampleABService>());
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/service-registration-by-scanning-53953306/?t=535)

#### 高级过滤

过滤器可以比简单的命名空间匹配更灵活。
你可以用 `Where` 方法按类型名称或其他属性过滤,或者用 `AssignableTo` 找出实现了某个特定接口的类型。

```csharp
services.Scan(selector =>
{
    selector.FromAssemblyOf<Program>()
        .AddClasses(f => f.Where(t => t.Name.EndsWith("Service")))
        .AsMatchingInterface()
        .WithSingletonLifetime();
});
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/service-registration-by-scanning-53953306/?t=585)

这对于像 Repository 模式这样的场景特别有用,你可以用一条声明注册所有 repository:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/service-registration-by-scanning-53953306/?t=775)

#### 层级化扫描

你可以在单个 `Scan` 块中包含多次 `AddClasses` 调用,甚至多次 `FromAssemblyOf` 调用。
每次调用 `AddClasses` 都会为下一组注册规则重置上下文,这样就能在单个程序集内部或跨多个程序集实现复杂的、多层次的注册逻辑。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/service-registration-by-scanning-53953306/?t=685)

---

## 5. Interface marking

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/interface-marking-53953307/) · 3:28

### 总结

接口标记(interface marking)是 Scrutor 中的一种技巧,用空的“标记”接口来显式定义一个类的依赖注入生命周期。
开发者不再依赖可能导致误注册的命名约定,而是在类上实现特定的接口(例如 ISingletonService)。
随后 Scrutor 用 AssignableTo 过滤器扫描这些类型,从而对服务集合中的注册和生命周期管理实现精确控制。

### 核心概念

* Marker Interfaces(标记接口):用于给类打标签以触发特定注册行为的空接口。
* AssignableTo<T>:Scrutor 的一个过滤器,用于选出实现了某个特定接口或继承自某个特定类型的类。
* Lifetime Mapping(生命周期映射):把标记接口显式地关联到某个 DI 生命周期(Singleton、Scoped 或 Transient)。
* Registration Strategies(注册策略):在 AsImplementedInterfaces 和 AsMatchingInterface 之间选择,以控制服务如何被加入容器。

### 课程笔记

接口标记为基于名称的注册提供了一种更稳健的替代方案。
使用基于名称的过滤时,比如检查类名是否以 “Repository” 结尾,一旦某个类的命名有误,或者期望的生命周期与命名约定不匹配,就有误注册的风险。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/interface-marking-53953307/?t=10)

作为替代,开发者可以定义空的标记接口来代表不同的生命周期。
这些接口除了在程序集扫描时给类型分类之外,没有任何其他功能作用。

```csharp
namespace ScrutorScanning.ConsoleApp.Services;

public interface IScopedService
{

}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/interface-marking-53953307/?t=40)

服务随后在实现自己功能性接口的同时也实现这些标记接口。
例如,一个打算作为 transient 的服务,会同时实现它自己的服务接口和 `ITransientService` 标记接口。

```csharp
namespace ScrutorScanning.ConsoleApp.Services;

public class ExampleBService : IExampleBService, ITransientService
{

}

public interface IExampleBService
{

}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/interface-marking-53953307/?t=55)

在 Scrutor 的配置里,用 `AssignableTo<T>` 方法按这些标记过滤类。
这让你可以在单次 `Scan` 调用中串联多条注册规则,每条规则根据所实现的标记对应一种不同的生命周期。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/interface-marking-53953307/?t=100)

使用 `AsImplementedInterfaces` 时,Scrutor 会把这个类注册到它实现的每一个接口下。
这意味着该服务既会注册在它的功能性接口下(例如 `IExampleAService`),也会注册在标记接口下(例如 `ISingletonService`)。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/interface-marking-53953307/?t=115)

为了避免标记接口本身被注册成服务类型,可以改用 `AsMatchingInterface`。
这种策略只注册与类名相匹配的那个接口(例如 `ExampleService` 对应 `IExampleService`),就解析而言实际上忽略了标记接口。
如果命名约定没有被严格遵守,`AsImplementedInterfaces` 仍然是更安全的选择,即使它会产生一些永远不会被解析的多余注册。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/interface-marking-53953307/?t=135)

接口标记虽然是一种有效的技巧,但它有时会被批评为“泄漏的抽象”,因为它要求实现类知道自己在依赖注入容器中打算采用的注册生命周期。
不过,对于那些更偏好显式标记而非隐式约定的开发者来说,它提供了高度的控制力和清晰度。

---

## 6. Attribute marking

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/attribute-marking-53953308/) · 3:13

### 总结

本课演示如何用自定义特性(attribute)来管理 Scrutor 中的服务生命周期,作为标记接口的替代方案。
通过定义并在服务类上直接应用 [Transient]、[Scoped]、[Singleton] 之类的特性,开发者可以让依赖注入的注册变得显式且就地可见。
这种做法避免了用标记类型污染接口层级,同时又让 Scrutor 的扫描引擎能够基于这些自定义元数据标记来过滤并注册服务。

### 核心概念

*   **Custom Lifetime Attributes(自定义生命周期特性)**:创建特定的特性类(例如 `TransientAttribute`)来代表各种 DI 生命周期。
*   **Attribute Constraints(特性约束)**:使用 `[AttributeUsage(AttributeTargets.Class)]` 确保生命周期标记只能应用在实现类上。
*   **Scrutor Attribute Filtering(Scrutor 特性过滤)**:利用 `WithAttribute<T>` 选择器来识别需要注册的类。
*   **Explicit Registration(显式注册)**:把 DI 生命周期直接展现在类定义上,而不是藏在某个集中的配置文件里,从而提升代码可读性。

### 课程笔记

标记接口虽然是给服务分类以供扫描的常见做法,但它可能导致不必要的接口污染。
另一种做法是使用自定义特性,给类打上它所期望的生命周期标记。
这让注册意图在类本身上就一目了然。

一开始,项目可能像这样使用基于接口的扫描:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/attribute-marking-53953308/?t=10)

要转向基于特性的模型,你必须先定义这些特性。
这些类应当继承自基类 `Attribute`,并使用 `AttributeUsage` 特性把它们的应用范围限制为仅限类。

```csharp
namespace ScrutorScanning.ConsoleApp.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class TransientAttribute : Attribute
{
    
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/attribute-marking-53953308/?t=40)

其他生命周期也应当创建类似的特性,比如 Scoped:

```csharp
namespace ScrutorScanning.ConsoleApp.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class ScopedAttribute : Attribute
{
    
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/attribute-marking-53953308/?t=55)

特性定义好之后,就可以直接应用在实现类上。
这样一来,类就不必仅仅为了 DI 容器而去实现 `IScopedService` 或 `ITransientService` 这类特定的标记接口了。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/attribute-marking-53953308/?t=85)

给类打上标记之后,还必须更新 Scrutor 的扫描配置。
不再使用 `AssignableTo<T>`,而是在 `AddClasses` 方法中使用 `WithAttribute<T>` 过滤器,来定位那些被你的自定义特性修饰的类。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/attribute-marking-53953308/?t=130)

这种模式在其他生态中很常见,比如 Java。
它确实意味着类知道自己是如何被注册进 DI 容器的,但它带来了很高的可见性。
任何人看到这个类,都能立刻知道它期望的生命周期,而不必去翻 `Startup.cs` 或各种扩展方法。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/attribute-marking-53953308/?t=145)

---

## 7. Namespace filtering

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/namespace-filtering-53953309/) · 2:19

### 总结

Scrutor 允许基于命名空间扫描并注册类,这在项目对 repository、service 这类分层严格遵循命名约定时很有效。
这种做法省去了手动给类加特性的必要,但它会给某个被过滤出来的命名空间下的所有类强加统一的生命周期。
由于命名空间字符串不是类型安全的,而且在重构时可能改变,这种方法带有运行时失败的风险,除非有全面的单元测试作为支撑。

### 核心概念

- **Namespace Matching(命名空间匹配)**:使用 `InNamespaces` 纳入指定字符串命名空间下的类。
- **Lifetime Uniformity(生命周期一致性)**:在单个命名空间过滤器下注册的所有类,必须共用同一种生命周期(例如 Singleton、Scoped 或 Transient)。
- **Attribute Independence(与特性无关)**:如果扫描器被这样配置,基于命名空间的注册可以忽略或覆盖像 `[Singleton]` 这样的特性。
- **Exclusion Filters(排除过滤器)**:`NotInNamespaces` 方法允许在一次更宽泛的扫描中排除特定的命名空间。
- **Maintenance Risk(维护风险)**:基于命名空间的扫描对重构很敏感;如果 DI 配置中的字符串没有同步更新,命名空间改名就会让注册失效。

### 课程笔记

当你的项目有一致的命名约定时,比如 `Project.Repositories` 或 `Project.Services`,你可以用命名空间匹配来注册依赖。
这样就不必给每个类都加上特性。

在此之前,注册可能严重依赖特性来定义生命周期:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/namespace-filtering-53953309/?t=10)

要按命名空间过滤,使用 `InNamespaces` 方法。
这个方法接收一个字符串数组,表示要扫描的命名空间。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/namespace-filtering-53953309/?t=25)

命名空间过滤的一个显著局限,是对生命周期缺乏细粒度的控制。
在指定命名空间内被识别出来的每一个类,都必须以相同的生命周期注册。
如果同一命名空间下的类需要不同的生命周期,你就必须使用更具体的过滤器,或者回到基于特性的注册。

使用命名空间扫描时,类上已有的特性会被忽略,除非过滤逻辑里专门把它们包含进来。
例如,一个被 `[Singleton]` 特性修饰的类,仍然会按照 Scrutor 的配置来注册,哪怕那份配置指定的是另一种生命周期。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/namespace-filtering-53953309/?t=55)

只要指定了确切的命名空间字符串,Scrutor 就会把所有匹配的类注册为它们所实现的接口。
在下面的例子中,指定命名空间下的所有服务都被注册为单例。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/namespace-filtering-53953309/?t=85)

Scrutor 还提供了用于排除的方法。
你可以用 `NotInNamespaces` 注册除某个特定命名空间下的类之外的所有内容。
类似地,你可以用 `WithoutAttribute` 排除被特定标记修饰的类,从而控制各种边缘情况。

```csharp
.FromAssemblyOf<Program>()
    .AddClasses(f => f.NotIn)
    .AsImplementedInterfaces()
    .WithSingletonLifetime();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/namespace-filtering-53953309/?t=110)

基于命名空间的注册虽然强大,却被认为有风险,因为命名空间在重构过程中是会变的。
由于这些字符串在编译期得不到校验,改动可能导致运行时失败。
建议使用单元测试来确保期望的服务都被正确注册。

---

## 8. Using the ServiceDescriptor attribute

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-the-servicedescriptor-attribute-53953310/) · 2:47

### 总结

Scrutor 中的 ServiceDescriptor 特性提供了一种统一的、基于特性的方式,直接在实现类上定义服务注册。
通过在程序集扫描时使用 .UsingAttributes() 方法,开发者无需创建自定义特性类就能控制服务类型和生命周期。
这种做法提供了很高的灵活性,包括用多个特性把同一个类注册为多种服务类型或多种生命周期,不过它也在服务实现与其注册配置之间引入了直接的依赖。

### 核心概念

- **ServiceDescriptor Attribute(ServiceDescriptor 特性)**:Scrutor 提供的一个特性,用于修饰类以便自动注册。
- **UsingAttributes()**:处理 `ServiceDescriptor` 及其他注册特性所需的扫描方法。
- **Default Registration(默认注册)**:不带参数时,该特性会把类注册为它自己以及它实现的接口,生命周期为 Transient。
- **Explicit Configuration(显式配置)**:参数允许定义具体的服务类型和 `ServiceLifetime` 值。
- **Multiple Registrations(多重注册)**:支持在同一个类上应用多个 `ServiceDescriptor` 特性,以应对复杂的注册场景。

### 课程笔记

Scrutor 提供了一个内置的 `ServiceDescriptor` 特性,可以在程序集扫描期间对服务如何注册进行细粒度控制。
当一个类被这个特性修饰时,只要在 Scrutor 的扫描配置中调用 `.UsingAttributes()`,它就能被自动注册。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-the-servicedescriptor-attribute-53953310/?t=10)

要启用这个功能,扫描逻辑中必须包含 `.UsingAttributes()` 方法。
它指示 Scrutor 在指定程序集内找到的类上,去查找 `ServiceDescriptor` 特性(或其他自定义特性)。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-the-servicedescriptor-attribute-53953310/?t=40)

默认情况下,如果不给 `[ServiceDescriptor]` 传任何参数,Scrutor 会把这个类注册为它自己的类型,同时也注册为它实现的接口,两者都是 **Transient** 生命周期。

你可以通过把类型传给特性的构造函数,显式地定义该服务应当被注册成哪个接口或类型。
当指定了类型时,只有那个类型会被注册。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-the-servicedescriptor-attribute-53953310/?t=65)

这个特性也支持指定 `ServiceLifetime`。
例如,把一个服务注册为 Singleton:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-the-servicedescriptor-attribute-53953310/?t=75)

如果你既想注册实现类,也想以某个特定生命周期注册它的接口,可以给服务类型参数传 `null`。
这会回到既注册自身又注册各接口的默认行为,但对两者都应用所指定的生命周期。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-the-servicedescriptor-attribute-53953310/?t=90)

对于更复杂的场景,可以在同一个类上应用多个 `ServiceDescriptor` 特性。
这让你能把同一个实现注册到不同的类型、配上不同的生命周期,而它们会按照在类上出现的顺序被注册。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-the-servicedescriptor-attribute-53953310/?t=115)

这种做法虽然把类和它的注册逻辑耦合在了一起,却让注册细节高度可见,并提供了一种统一的方式来处理依赖注入,而不必创建许多自定义特性。

---

## 9. Using RegistrationStrategies

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-registrationstrategies-53953311/) · 2:24

### 总结

本课讲解如何用 UsingRegistrationStrategy 方法控制 Scrutor 在遇到重复服务注册时的行为。
它覆盖了四种主要策略 Append、Skip、Replace 和 Throw,详述每一种如何影响服务集合,并就如何在程序集扫描期间保持应用稳定性给出建议。

### 核心概念

- **RegistrationStrategy.Append**:默认行为;即使服务已经存在,也把这条服务注册加入集合。
- **RegistrationStrategy.Skip**:如果服务已经注册在容器中,本次注册尝试会被忽略。
- **RegistrationStrategy.Replace**:用新的实现和生命周期覆盖该服务已有的任何注册。
- **RegistrationStrategy.Throw**:一旦检测到重复注册,就在启动时抛出异常,确保配置错误被立刻发现。

### 课程笔记

使用 Scrutor 扫描程序集时,同一个类有可能匹配到多条注册条件。
例如,某个服务被定义为单例,却又在扫描 transient 服务时被无意中包含了进来。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-registrationstrategies-53953311/?t=10)

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-registrationstrategies-53953311/?t=20)

默认情况下,如果多个 `AddClasses` 块匹配到同一个服务,Scrutor 会把这个服务注册多次。
这等价于在 `IServiceCollection` 上多次调用标准的 `.Add()` 方法。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-registrationstrategies-53953311/?t=30)

在下面的输出里,`ExampleBService` 被注册了两次:一次作为 Singleton,一次作为 Transient。
这是因为默认策略是 `Append`。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-registrationstrategies-53953311/?t=40)

要改变这种行为,使用 `UsingRegistrationStrategy()` 方法。
这个方法放在 `AddClasses()` 之后,但在 `AsImplementedInterfaces()` 这类注册细节之前。

#### Skip 策略

使用 `RegistrationStrategy.Skip` 可以确保:如果服务已经存在于 `IServiceCollection` 中,当前这次注册尝试就会被忽略。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-registrationstrategies-53953311/?t=85)

#### Replace 策略

使用 `RegistrationStrategy.Replace()` 会查找已有的注册,并用新的配置替换它。
在下面的例子中,Transient 注册替换掉了 `IExampleBService` 先前的 Singleton 注册。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-registrationstrategies-53953311/?t=100)

#### Throw 策略

`RegistrationStrategy.Throw` 是大多数应用的推荐做法。
一旦检测到重复注册,它会让应用在启动时立即失败,从而防止意外的错误配置流入生产环境。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/using-registrationstrategies-53953311/?t=115)

---

## 10. Potential pitfalls

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/potential-pitfalls-53953312/) · 1:52

### 总结

Scrutor 的程序集扫描能力提供了一种简洁的依赖注入处理方式,但也带来了诸如代码可发现性下降和生命周期错配之类的风险。
过度依赖扫描会隐藏注册逻辑,让新来的开发者难以理解系统的配置。
为了缓解这些问题,开发者应当使用具体的过滤器,并思考注册这项职责究竟应该属于类本身还是 DI 容器的配置。

### 核心概念

- 被隐藏的注册逻辑和下降的可发现性。
- 隐式注册给新开发者带来的上手难题。
- 关于注册元数据放在哪里的架构考量(特性 vs. 容器配置)。
- 服务生命周期出错的风险(例如把一个单例注册成了 transient)。
- 在 `AddClasses` 方法中使用具体过滤器的重要性。

### 课程笔记

Scrutor 的扫描特性是自动化服务注册的强大工具,但应当谨慎使用,并配以高度具体的过滤器。
扫描虽然减少了样板代码,却可能带来注册逻辑被隐藏起来的维护难题。
这种透明度的缺失,会拉长新工程师理解应用依赖关系图以及具体服务如何被解析所需要的时间。

一个重要的架构考量是注册元数据的位置。
用标记接口或特性来驱动扫描,会把依赖注入(DI)配置的职责从容器转移到各个类上。
特性虽然在类与其期望的生命周期之间建立了清晰的联系,但某些架构范式认为类不应当知道自己是如何被注册的。

在实现扫描时,关键是在 `AddClasses` 方法中使用精确的过滤器,以防止生命周期被错误地分配。
例如,如果扫描条件过于宽泛,一个本应是 Singleton 的服务可能被意外注册成 Transient。
开发者可以使用像 `RegistrationStrategy.Throw` 这样的注册策略来处理冲突,确保扫描行为可预测。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/potential-pitfalls-53953312/?t=10)

归根结底,扫描虽是 Scrutor 的一个有用特性,但它的装饰能力往往能以更少的架构风险带来更多价值。
开发者应当谨慎对待扫描,确保代码简洁带来的便利不会盖过对显式且可维护的服务注册的需求。

---

## 11. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953313/) · 0:46

### 总结

本课总结使用 Scrutor 扩展 .NET 内置依赖注入所带来的好处。
它着重回顾了装饰器模式的实现以及各种程序集扫描策略,比如基于特性的注册,同时也提醒注意自动依赖发现可能带来的副作用。

### 核心概念

- **Scrutor Decoration(Scrutor 装饰)**:相比标准的 .NET DI,简化了装饰器的注册。
- **Assembly Scanning(程序集扫描)**:基于接口、命名空间或特性等条件自动发现并注册类型。
- **Registration Strategies(注册策略)**:控制 Scrutor 如何处理重复或冲突的注册(例如 `RegistrationStrategy.Throw`)。
- **Attribute-Based Registration(基于特性的注册)**:在扫描时使用自定义特性来定义服务生命周期。

### 课程笔记

Scrutor 为标准的 .NET 依赖注入(DI)容器提供了强有力的扩展,尤其改善了装饰器的处理方式。
相比那些会变得复杂又难以维护的手动嵌套注册,Scrutor 的装饰特性让装饰器模式的实现更干净、更易读。

除了装饰之外,Scrutor 还引入了高级的程序集扫描能力。
这让开发者可以基于接口、特定类型、命名空间或自定义特性等各种条件自动注册依赖。
这种自动化减少了 `Program.cs` 或启动配置中所需的样板代码。

下面的示例演示如何用程序集扫描配合特性标记,为一个程序集内不同的服务定义生命周期:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953313/?t=10)

扫描虽然简化了注册,却需要谨慎实现。
开发者必须留意潜在的副作用,比如意外注册了本不该进入容器的类型,或者制造出相互冲突的注册。
使用像 `RegistrationStrategy.Throw` 这样具体的注册策略,可以在发生注册冲突时让应用显式失败,从而帮助管理这些风险。

---

## 运行 Demo

本章的代码取自课程的 `6.Scrutor` 目录。
课程那两个项目(`ScrutorScanning.ConsoleApp` 和 `Weather.Api`)被合并成一个可以从头跑到尾的 demo,每节课一个小节。
所有小节共用一个程序集,所以每次扫描都额外用 `InNamespaces` 限定在本节自己的命名空间里;课程里每个项目只装它自己的示例,不需要这层限定。
第 11 课是本章回顾,没有对应的可运行小节。

```
src/dependency-injection/07-extending-dependency-injection-with-scrutor/DependencyInjection.Scrutor.Demos/
  IntroDemo.cs                          第 1 课:手写注册与等价的一次扫描,产出同样的描述符
  Scanning/                             第 1、4 课的示例服务:ExampleA/B/AB、UserRepository、OrderRepository
  Weather/LoggedWeatherService.cs       第 2 课:Stopwatch + try-finally 版装饰器
  DecoratorDemo.cs                      第 2 课:工厂手写装饰 vs Decorate,以及 TryDecorate 的真实语义
  Logging/                              第 3 课:ILoggerAdapter、LoggerAdapter、TimedLogOperation
  Weather/TimedLoggedWeatherService.cs  第 3 课:重构后的装饰器,只剩一行 using
  TimedOperationDemo.cs                 第 3 课
  ScanningDemo.cs                       第 4 课:AsMatchingInterface / AsSelf / AsImplementedInterfaces /
                                        AsSelfWithInterfaces、生命周期覆盖、Where 过滤、层级化扫描
  InterfaceMarking/                     第 5 课:三个空标记接口和三个示例服务
  InterfaceMarkingDemo.cs               第 5 课
  AttributeMarking/                     第 6 课:Singleton/Transient/Scoped 三个自定义特性和三个示例服务
  AttributeMarkingDemo.cs               第 6 课
  NamespaceFiltering/                   第 7 课:Services 和 Internal 两个命名空间
  NamespaceFilteringDemo.cs             第 7 课:InNamespaces、NotInNamespaces、WithoutAttribute
  Descriptors/                          第 8 课:[ServiceDescriptor] 的五种写法各一个类
  ServiceDescriptorDemo.cs              第 8 课
  Strategies/                           第 9 课:同时带 [Singleton] 和 [Transient] 的服务
  RegistrationStrategyDemo.cs           第 9 课:Append、Skip、Replace、Throw
  Pitfalls/                             第 10 课:被宽泛过滤器误伤的连接池和迁移服务
  PitfallsDemo.cs                       第 10 课
  Output/Registrations.cs               课程的 PrintRegisteredService,额外打印 keyed 注册的 key
  Output/InlineLoggerProvider.cs        同步写控制台的 logger,保证日志行不乱序,课程代码里没有
  Weather/LocalWeatherService.cs        返回本地假数据的 IWeatherService,替掉课程里调 OpenWeather 的实现
```

```bash
cd src/dependency-injection/07-extending-dependency-injection-with-scrutor/DependencyInjection.Scrutor.Demos
dotnet run -c Release                   # 十个小节全跑
dotnet run -c Release -- intro          # 第 1 课
dotnet run -c Release -- decorate       # 第 2 课
dotnet run -c Release -- timed          # 第 3 课
dotnet run -c Release -- scanning       # 第 4 课
dotnet run -c Release -- interfaces     # 第 5 课
dotnet run -c Release -- attributes     # 第 6 课
dotnet run -c Release -- namespaces     # 第 7 课
dotnet run -c Release -- descriptor     # 第 8 课
dotnet run -c Release -- strategies     # 第 9 课
dotnet run -c Release -- pitfalls       # 第 10 课
```

Demo 是瞬间跑完的,而且不需要联网。
用的是 Scrutor 7.0.0,课程录制时是 3.3.0。

实际输出:

```text
======================================================================
  What is Scrutor?
======================================================================

Scrutor version in use: 7.0.0.0

Registered by hand, one line per service:
  IExampleAService -> ExampleAService as Transient
  IExampleBService -> ExampleBService as Transient
  IUserRepository -> UserRepository as Transient
  IOrderRepository -> OrderRepository as Transient

The same four descriptors, produced by one scan instead:
  IExampleAService -> ExampleAService as Transient
  IExampleBService -> ExampleBService as Transient
  IUserRepository -> UserRepository as Transient
  IOrderRepository -> OrderRepository as Transient

  -> the container cannot tell the two apart, and the second one keeps working
     as services are added, because it describes a rule rather than a list


======================================================================
  Registering service decorators
======================================================================

The native way: register the concrete type, then hand-build the decorator in a factory.
  LocalWeatherService -> LocalWeatherService as Transient
  IWeatherService -> (factory) as Transient
  resolved LoggedWeatherService, calling it for Athens:
  [log] Weather retrieval for city: Athens, took 38ms
  it is 27.4C in Athens

The Scrutor way: register the service normally, then decorate it.
  IWeatherService -> (factory) as Transient
  IWeatherService [key: IWeatherService+56ef35cdfb4d4db5b2054bbcfc3635d4+Decorated] -> LocalWeatherService as Transient
  resolved LoggedWeatherService, calling it for London:
  [log] Weather retrieval for city: London, took 39ms
  it is 14.1C in London

  -> Decorate moved the original registration onto a generated key and put the
     decorator's factory at IWeatherService, which is how the decorator receives
     the inner service without asking for IWeatherService and recursing

Decorate on a service nobody registered:
  DecorationException: Could not find any registered services for type 'DependencyInjection.Scrutor.Demos.Weather.IWeatherService'.
  TryDecorate on the same empty collection returned False
  -> that is the Try the name refers to: "decorate if the service is there",
     not "decorate unless it is already decorated"

Which means calling it twice does stack two decorators:
  first TryDecorate returned True
  second TryDecorate returned True
  resolved LoggedWeatherService, calling it for Auckland:
  [log] Weather retrieval for city: Auckland, took 25ms
  [log] Weather retrieval for city: Auckland, took 25ms
  it is 18.9C in Auckland
  -> two timing lines, one per layer: a registration helper run twice double-wraps


======================================================================
  Surprise optional refactoring lecture
======================================================================

Before: LoggedWeatherService held a Stopwatch, a try and a finally around one await.
After:  TimedLoggedWeatherService is a using statement and a return.

Calling the decorated service:
  [log] Weather retrieval for city: Athens, completed in 33ms
  it is 27.4C in Athens
  -> the timing line above was written by Dispose, not by the service

The same operation used inline, the way the course uses it in a controller:
  ...building the response...
  [log] WeatherEndpoint response completed in 52ms
  -> the block above timed itself, with no Stopwatch in sight


======================================================================
  Service registration by scanning
======================================================================

AsMatchingInterface: register ExampleAService as IExampleAService, by name.
  IExampleAService -> ExampleAService as Transient
  IExampleBService -> ExampleBService as Transient
  IUserRepository -> UserRepository as Transient
  IOrderRepository -> OrderRepository as Transient
  -> ExampleABService is missing: no IExampleABService exists to match its name

AsSelf: register every class as its own type.
  ExampleAService -> ExampleAService as Transient
  ExampleBService -> ExampleBService as Transient
  ExampleABService -> ExampleABService as Transient
  UserRepository -> UserRepository as Transient
  OrderRepository -> OrderRepository as Transient

AsImplementedInterfaces, with the default transient lifetime replaced by singleton.
  IExampleAService -> ExampleAService as Singleton
  IExampleBService -> ExampleBService as Singleton
  IExampleAService -> ExampleABService as Singleton
  IExampleBService -> ExampleABService as Singleton
  IUserRepository -> UserRepository as Singleton
  IOrderRepository -> OrderRepository as Singleton
  -> ExampleABService appears twice, once per interface it implements

AsSelfWithInterfaces: self plus interfaces, wired through a factory.
  ExampleABService -> ExampleABService as Singleton
  IExampleAService -> (factory) as Singleton
  IExampleBService -> (factory) as Singleton
  all three resolve to one instance: True
  -> the two interfaces are factories that forward to the self registration,
     which is why the singleton is not duplicated three times over

Where(...EndsWith("Repository")): the whole repository layer in one declaration.
  IUserRepository -> UserRepository as Scoped
  IOrderRepository -> OrderRepository as Scoped

Two AddClasses calls in one Scan: services singleton, repositories scoped.
  IExampleAService -> ExampleAService as Singleton
  IExampleBService -> ExampleBService as Singleton
  IUserRepository -> UserRepository as Scoped
  IOrderRepository -> OrderRepository as Scoped
  -> each AddClasses resets the context, so the two rules do not bleed into each other


======================================================================
  Interface marking
======================================================================

Name-based filtering gives one lifetime to everything it catches:
  IExampleAService -> ExampleAService as Scoped
  IExampleBService -> ExampleBService as Scoped
  IExampleCService -> ExampleCService as Scoped
  -> three services, three scoped registrations, whether that was wanted or not

AssignableTo<T> over marker interfaces, one AddClasses per lifetime:
  IExampleAService -> ExampleAService as Singleton
  ISingletonService -> ExampleAService as Singleton
  IExampleBService -> ExampleBService as Transient
  ITransientService -> ExampleBService as Transient
  IExampleCService -> ExampleCService as Scoped
  IScopedService -> ExampleCService as Scoped
  -> each class got the lifetime it asked for, and the marker interfaces
     were registered as service types too, because they are implemented interfaces

The same scan with AsMatchingInterface instead:
  IExampleAService -> ExampleAService as Singleton
  IExampleBService -> ExampleBService as Transient
  IExampleCService -> ExampleCService as Scoped
  -> the markers are gone, but only because every class is named after its interface


======================================================================
  Attribute marking
======================================================================

WithAttribute<T> in place of AssignableTo<T>, one AddClasses per lifetime:
  IExampleAService -> ExampleAService as Singleton
  IExampleBService -> ExampleBService as Transient
  IExampleCService -> ExampleCService as Scoped

  -> compare with lesson 5: no ISingletonService or ITransientService rows,
     because the marker is metadata now and not part of the type's interface list

Where the lifetime is declared, per class:
  ExampleAService  [Singleton]
  ExampleBService  [Transient]
  ExampleCService  [Scoped]
  -> readable from the class itself, without opening Program.cs


======================================================================
  Namespace filtering
======================================================================

InNamespaces("DependencyInjection.Scrutor.Demos.NamespaceFiltering.Services"), everything singleton:
  IExampleAService -> ExampleAService as Singleton
  IExampleBService -> ExampleBService as Singleton
  IExampleCService -> ExampleCService as Singleton
  -> ExampleAService still carries [Singleton], but the scan never looked:
     the lifetime came from the Scan call, and the attribute was ignored

Every class under the chapter's namespace gets the same lifetime, wanted or not:
  IExampleAService -> ExampleAService as Singleton
  IExampleBService -> ExampleBService as Singleton
  IExampleCService -> ExampleCService as Singleton
  ICacheWarmerService -> CacheWarmerService as Singleton
  IDiagnosticsService -> DiagnosticsService as Singleton
  -> the two internal types came along for the ride, as singletons

NotInNamespaces("DependencyInjection.Scrutor.Demos.NamespaceFiltering.Internal") puts them back out:
  IExampleAService -> ExampleAService as Singleton
  IExampleBService -> ExampleBService as Singleton
  IExampleCService -> ExampleCService as Singleton

WithoutAttribute<ScopedAttribute> excludes the one edge case instead:
  IExampleAService -> ExampleAService as Singleton
  IExampleBService -> ExampleBService as Singleton
  IExampleCService -> ExampleCService as Singleton
  ICacheWarmerService -> CacheWarmerService as Singleton
  -> DiagnosticsService is gone, CacheWarmerService stayed

And the whole thing hinges on a string: "DependencyInjection.Scrutor.Demos.NamespaceFiltering.Services"
  -> rename the folder in a refactor and the compiler says nothing; the app
     starts and fails on the first resolve, which is what the unit tests are for


======================================================================
  Using the ServiceDescriptor attribute
======================================================================

One AddClasses with no filter at all, closed by UsingAttributes():
  DefaultService -> DefaultService as Transient
  IDefaultService -> DefaultService as Transient
  IInterfaceOnlyService -> InterfaceOnlyService as Transient
  ISingletonOnlyService -> SingletonOnlyService as Singleton
  SelfAndInterfaceService -> SelfAndInterfaceService as Singleton
  ISelfAndInterfaceService -> SelfAndInterfaceService as Singleton
  MultiService -> MultiService as Singleton
  IMultiService -> MultiService as Singleton

What each attribute asked for:
  [ServiceDescriptor]                                       -> self and interface, transient
  [ServiceDescriptor(typeof(IInterfaceOnlyService))]        -> that interface only, transient
  [ServiceDescriptor(typeof(ISingletonOnlyService), Singleton)] -> that interface only, singleton
  [ServiceDescriptor(null, Singleton)]                      -> self and interface, singleton
  two attributes on MultiService                            -> both rows, in attribute order

  -> no custom attribute classes were written for any of this, and the lifetime
     lives on the class rather than in the Scan call


======================================================================
  Using RegistrationStrategies
======================================================================

ExampleBService carries both [Singleton] and [Transient], so two passes claim it.

Default (Append):
  IExampleAService -> ExampleAService as Singleton
  IExampleBService -> ExampleBService as Singleton
  IExampleBService -> ExampleBService as Transient
  -> registered twice, the same as calling Add twice

RegistrationStrategy.Skip:
  IExampleAService -> ExampleAService as Singleton
  IExampleBService -> ExampleBService as Singleton
  -> the transient pass found IExampleBService already there and left it alone

RegistrationStrategy.Replace():
  IExampleAService -> ExampleAService as Singleton
  IExampleBService -> ExampleBService as Transient
  -> the singleton row is gone; the transient pass overwrote it

RegistrationStrategy.Throw:
  DuplicateTypeRegistrationException: A service of type 'DependencyInjection.Scrutor.Demos.Strategies.IExampleBService' has already been registered.
  -> the app refuses to start, which is the point: the duplicate was a mistake


======================================================================
  Potential pitfalls
======================================================================

A filter broad enough to be convenient: everything named *Service, transient.
  IConnectionPoolService -> ConnectionPoolService as Transient
  IDatabaseMigrationService -> DatabaseMigrationService as Transient
  pool on first resolve:  5125e833-4ae9-4dcb-8fdf-498c4ad7612a
  pool on second resolve: 9c358166-95fa-4887-9de7-4274e01b4c45
  same instance: False  <- a connection pool per caller
  -> DatabaseMigrationService is resolvable too, and it was never meant to be
  -> nothing failed; the build was clean and the app started

The same two types, registered explicitly:
  IConnectionPoolService -> ConnectionPoolService as Singleton
  same instance: True
  -> two lines of registration, and both problems are gone

Where scanning is worth it, RegistrationStrategy.Throw keeps it honest:
  DuplicateTypeRegistrationException: A service of type 'DependencyInjection.Scrutor.Demos.Pitfalls.IConnectionPoolService' has already been registered.
  -> the broad filter is still broad, but it can no longer overwrite a deliberate
     registration in silence
```
