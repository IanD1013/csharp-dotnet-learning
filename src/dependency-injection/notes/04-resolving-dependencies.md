# Resolving dependencies

> 课程:[From Zero to Hero: Dependency Injection in .NET with C#](https://dometrain.com/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp/) · 第 4 章
> 共 15 课 · 约 25:09
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

---

## 课程索引

| # | 课程 | 时长 | 小节 |
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

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-different-project-types-53953161/) · 0:21

### 总结

本课介绍在不同 .NET 项目类型中解析依赖的各种模式,涵盖控制台应用、Web API、Minimal API、MVC、Razor Pages、Blazor 以及 gRPC。
底层的依赖注入容器和注册流程保持一致,但具体的解析入口点会有所不同,比如手动构建 service provider、构造函数注入或方法参数注入,以适配每种框架模板各自的需求和生命周期。

### 核心概念

*   **Manual Resolution(手动解析)**:在控制台应用这类没有宿主(host)的环境中必须使用,需要手动构建 service provider 并从中查询服务。
*   **Constructor Injection(构造函数注入)**:适用于由框架管理的类的标准模式,例如 Web API / MVC 控制器、Razor Page 模型和 gRPC 服务。
*   **Parameter Injection(参数注入)**:Minimal API 路由处理器专有的模式,依赖直接从方法签名中解析。
*   **Project-Specific Registrations(项目特有的注册方式)**:注册方式上的差异,例如 Blazor Server 中使用 `AddServerSideBlazor`,或在 Blazor WebAssembly 中配置一个 scoped 的 `HttpClient`。

### 课程笔记

在 .NET 中,依赖解析高度取决于项目类型以及需要该服务的具体类。
把服务注册到 `IServiceCollection` 是所有项目共通的基础,但获取机制会随着"该类的实例化是否由框架管理"而变化。

#### Console Applications(控制台应用)

在标准的控制台应用中,没有内置的宿主来自动管理服务的生命周期。
开发者必须手动创建 `ServiceCollection`、注册依赖,并调用 `BuildServiceProvider()` 来创建容器。
随后再通过 `GetRequiredService<T>` 或 `GetService<T>` 取出服务。

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

Minimal API 简化了依赖解析:服务可以直接作为路由处理委托的参数注入。
当端点被调用时,框架会自动从 DI 容器中解析这些参数。

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

#### Web API and MVC Controllers(Web API 与 MVC 控制器)

对于基于控制器的项目,构造函数注入是主要机制。
框架的控制器工厂会识别构造函数中声明的依赖,并在实例化控制器之前解析它们。

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

#### gRPC Services(gRPC 服务)

.NET 中的 gRPC 服务同样使用构造函数注入。
服务实现类继承自生成的基类,当一次 gRPC 调用被路由到该实现时,DI 容器会提供所需的服务。

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

#### Blazor Applications(Blazor 应用)

Blazor Server 和 Blazor WebAssembly 都在 `Program.cs` 中注册服务。
Blazor Server 通常把 `WeatherForecastService` 这类服务注册为 Singleton 或 Scoped,而 Blazor WebAssembly 则经常注册一个 scoped 的 `HttpClient`,并把它配置成指向宿主环境的基地址。

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

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-the-constructor-53953162/) · 0:59

### 总结

构造函数注入是 .NET 应用中解析依赖的首要方式,也是最直接的方式。
只要把所需的服务声明为类构造函数的参数,内置的依赖注入容器就会在运行时自动提供相应的实例,前提是这些服务已经注册到应用的 service collection 中。

### 核心概念

- **Constructor Injection(构造函数注入)**:通过把依赖声明为构造函数参数来请求依赖的标准模式。
- **Automatic Resolution(自动解析)**:.NET 依赖注入容器会在对象实例化过程中自动满足构造函数的要求。
- **Service Registration(服务注册)**:依赖必须注册到服务容器中(通常在 `Program.cs`)才能被解析。
- **Framework-Provided Services(框架提供的服务)**:某些服务(例如 `ILogger`)由 ASP.NET Core 自动注册,无需手动配置。

### 课程笔记

在 .NET 中,解析服务最常见也是默认的方式就是构造函数注入。
即便不使用正式的依赖注入框架,这种做法也是标准写法,而在 .NET 生态中它是完全自动化的。
要在某个类(比如一个 Web API 控制器)中解析服务,只需把该服务定义为这个类构造函数的一个参数。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-the-constructor-53953162/?t=25)

当一个类被实例化时,依赖注入框架会自动识别它所需的依赖,并提供合适的实例。
要让这一切生效,服务必须注册在应用的配置中,通常位于 `Program.cs` 文件(在更老的项目结构中则是 `Startup.cs`)。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-the-constructor-53953162/?t=40)

在 ASP.NET Core 中,许多必要的服务是由框架自动注册的。
例如,你在 `Program.cs` 里看不到对 `ILogger` 的显式注册,但它已经在幕后配置好了,因此无需任何额外设置就能立刻注入到控制器或其他服务中。

---

## 3. Resolving dependencies from the method

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-the-method-53953163/) · 1:27

### 总结

ASP.NET Core 允许使用 [FromServices] 特性直接在方法参数上解析依赖,这为标准的构造函数注入提供了另一种选择。
当某个依赖只被类中的单个 action 用到时,这种做法尤其有价值,因为它免去了在测试其他方法时还要为该依赖创建 mock 的麻烦,从而简化单元测试。
虽然它能减少测试样板代码,但这一技巧应当克制使用,以便继续遵守单一职责原则,并确保类不会变得过于复杂。

### 核心概念

* ASP.NET Core 中的方法级依赖解析
* 在 action 参数上使用 [FromServices] 特性
* 通过把依赖隔离到特定用例来提升可测试性
* 在方法注入与单一职责原则(SRP)之间取得平衡

### 课程笔记

在大多数 ASP.NET Core 应用中,依赖是通过构造函数注入来解析的。
这种模式保证了所需的服务对控制器或类中的所有方法都可用。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-the-method-53953163/?t=25)

当一个类包含多个方法,而某个依赖只有其中一个方法需要时,问题就出现了。
在构造函数注入下,这个类中每个方法的每个单元测试都必须为该依赖提供一个 mock,哪怕被测方法根本用不到它。

为了优化这一点,ASP.NET Core 提供了 `[FromServices]` 特性。
把它应用到方法参数上之后,框架只会在该方法被调用时才直接从 DI 容器中解析这个依赖。
这样就把依赖从类的构造函数中移除了,并把它的作用范围限制在真正需要它的那个 action 上。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-the-method-53953163/?t=40)

这个技巧虽然有助于简化测试,但通常被视为例外而非常规做法。
如果一个类有很多方法,却只有其中一个需要某个特定服务,这往往说明这个类承担了过多职责。
遵循单一职责原则通常会得到更小的类,而在更小的类里,构造函数注入仍然是最合适的选择。
不过,在重构时或面对特定架构约束时,方法级注入是一个值得拥有的工具。

---

## 4. Resolving dependencies in a console setup

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-a-console-setup-53953164/) · 1:17

### 总结

在 .NET 控制台应用中,依赖解析必须手动管理,因为它们没有 ASP.NET Core 那样的自动化基础设施。
这包括显式创建 ServiceCollection、注册依赖、构建 ServiceProvider,并手动解析作为入口点的服务。
一旦顶层服务被解析出来,DI 容器就会自动为依赖图中后续的所有依赖完成构造函数注入。

### 核心概念

- 手动实例化 `ServiceCollection`。
- 通过 `AddSingleton`、`AddScoped` 或 `AddTransient` 注册依赖。
- 用 `BuildServiceProvider` 把容器实体化。
- 使用 `GetRequiredService` 解析入口点。
- 构造函数注入作为主要的解析机制。

### 课程笔记

要在控制台应用中实现依赖注入,首先必须在项目文件中引入 `Microsoft.Extensions.DependencyInjection` NuGet 包。
这个包提供了 DI 容器所需的抽象和默认实现。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-a-console-setup-53953164/?t=30)

ASP.NET Core 会通过控制器自动处理服务解析,控制台应用则不同,需要手动配置服务容器。
你先实例化一个 `ServiceCollection`,并注册应用的各个服务。
注册完成后,调用 `BuildServiceProvider()` 创建 `IServiceProvider`,然后手动解析入口点服务。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-a-console-setup-53953164/?t=20)

一旦根服务被解析出来,DI 容器就会沿着依赖树自动级联解析。
`Application` 类所需的任何依赖都会通过它的构造函数注入进来。
这种自动解析会对容器中注册的所有嵌套依赖持续进行下去。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-a-console-setup-53953164/?t=70)

需要特别注意的是,在 ASP.NET Core Minimal API 或控制器中常用的 `[FromServices]` 特性,在控制台应用中是不受支持的。
原因在于 `[FromServices]` 所需的装配逻辑是 ASP.NET Core 框架特有的。
在控制台环境中,所有依赖解析都必须通过构造函数注入完成。

---

## 5. Resolving dependencies from the HttpContext

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-the-httpcontext-53953165/) · 1:09

### 总结

在 ASP.NET Core Web API 中,HttpContext 对象通过 RequestServices 属性提供了对当前请求所用服务的访问。
这个属性暴露出一个可以解析任何已注册依赖的 IServiceProvider,但直接在控制器 action 中使用它被认为是一种糟糕的做法。
它实现的是 Service Locator 反模式,会隐藏方法真正的依赖,并让单元测试变得复杂。
开发者应当优先使用构造函数注入或 [FromServices] 特性,以获得更清晰、更透明的依赖管理。

### 核心概念

- HttpContext.RequestServices:用于访问当前请求的 scoped service provider 的属性。
- IServiceProvider:RequestServices 返回的接口,允许手动解析服务。
- Service Locator 反模式:在方法内部手动解析依赖,而不是显式声明它们的做法。
- 可测试性:手动解析会隐藏依赖,使得在单元测试中难以对它们进行 mock。

### 课程笔记

在 ASP.NET Core Web API 控制器中,每个 action 都能访问 HttpContext。
这个对象封装了与具体这一次 HTTP 请求和响应相关的全部信息。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-the-httpcontext-53953165/?t=10)

在 HttpContext 内部,有一个名为 RequestServices 的属性。
这个属性是 IServiceProvider 的一个实现,它让你可以直接从依赖注入容器中解析服务。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-the-httpcontext-53953165/?t=35)

拿到 IServiceProvider 之后,你就可以调用 GetService 或 GetRequiredService 之类的方法,取出任何已注册的依赖。

#### 为什么这是糟糕的做法

从 HttpContext.RequestServices 解析依赖虽然可行,但基于以下几点原因,它通常被认为是糟糕的做法:

1. **隐藏依赖**:在方法体内部解析服务,会让方法签名不再反映它运行时真正需要什么。这使代码更难理解、更难维护。
2. **降低可测试性**:对这个控制器 action 做单元测试会变得困难得多,因为你必须 mock 整个 HttpContext 及其 service provider,而不是直接传入某个具体服务的 mock。
3. **Service Locator 模式**:这种做法是 Service Locator 反模式的一种形式,组件"主动去找"自己的依赖,而不是由外部提供给它。

#### 推荐的替代方案

与其使用 RequestServices,你应当选择下面这些标准的依赖注入技巧之一:
- **构造函数注入**:把所需的服务注入到控制器的构造函数中,并保存在一个私有字段里。
- **[FromServices] 特性**:如果某个服务只有特定的 action 需要,就在该 action 的参数上使用 [FromServices] 特性。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-the-httpcontext-53953165/?t=45)

---

## 6. Resolving dependencies from Action Filters as Attributes

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-action-filters-as-attributes-53953167/) · 2:42

### 总结

本课讲解当 ASP.NET Core 的 Action Filter 以特性(attribute)形式使用时,该如何处理其中的依赖。
由于特性只支持常量参数,标准的构造函数注入在这里行不通。
本课演示了一种使用 Service Locator 模式从 `HttpContext.RequestServices` 解析服务的变通做法,同时提醒这通常被视为反模式。

### 核心概念

* **IAsyncActionFilter**:用于创建包裹 action 执行过程的过滤器的接口,可以在 action 前后运行代码。
* **Attribute Constraints(特性的限制)**:C# 中的特性只允许常量参数,这使得把特性应用到控制器或 action 上时,无法通过构造函数直接注入服务。
* **Service Locator Pattern(服务定位器模式)**:一种手动从 service provider(这里是 `HttpContext.RequestServices`)请求服务,而不是由外部注入的技巧。
* **反模式警告**:使用 Service Locator 模式通常是不被推荐的,只有在没有其他替代方案时才应使用。

### 课程笔记

可以把 action filter 实现为一个特性,用来测量请求的执行时间。
通过实现 `IAsyncActionFilter`,过滤器就能借助 `try...finally` 块,在请求开始之前和完成之后分别执行逻辑。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-action-filters-as-attributes-53953167/?t=10)

`Console.WriteLine` 用于简单演示还行,但生产代码应该使用 `ILogger`。
然而,试图在特性中使用标准的构造函数注入会遇到很大的限制。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-action-filters-as-attributes-53953167/?t=70)

特性只能接受常量参数。
由于 `ILogger` 这样的服务不是常量,在把特性应用到控制器 action 上时,它无法被传入特性的构造函数。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-action-filters-as-attributes-53953167/?t=85)

要解决这个问题,可以使用 Service Locator 模式。
通过从 `ActionExecutingContext` 访问 `HttpContext`,你就能取到 `RequestServices`(当前作用域的 `IServiceProvider`),然后手动解析所需的服务。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-action-filters-as-attributes-53953167/?t=140)

这种做法虽然可行,也能实现结构化日志,但它被认为是一种反模式。
手动解析服务绕开了依赖注入带来的好处,如果存在更好的替代方案就应当避免。

---

## 7. Resolving dependencies from Service Filters

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-service-filters-53953168/) · 2:42

本课演示如何从 action filter 中的 Service Locator 模式,转向使用 Service Filter 这种更健壮的依赖注入方式。
通过实现 IAsyncActionFilter 接口并把过滤器注册到 DI 容器,开发者就能在过滤器中使用构造函数注入,相比从 HttpContext 手动解析服务,代码的可维护性和可测试性都大幅提升。

### 核心概念

- 标准特性在构造函数注入方面的限制。
- 过滤器中使用 `HttpContext.RequestServices` 的 Service Locator 反模式。
- 实现 `IAsyncActionFilter` 接口来编写自定义过滤逻辑。
- 把过滤器作为服务注册到 IServiceCollection 中。
- 通过 `[ServiceFilter]` 特性应用过滤器。

### 课程笔记

当把 action filter 实现为特性时,开发者常常会碰到一个限制:特性属于元数据,不支持从依赖注入(DI)容器进行构造函数注入。
一个常见但并不理想的变通做法,是在过滤器的执行上下文中通过 `HttpContext` 访问 service provider,也就是使用 Service Locator 模式。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-service-filters-53953168/?t=10)

这种做法之所以有问题,是因为它把依赖藏在了方法体内部,让代码更难推理,单元测试的难度也大幅上升。
测试这样一个过滤器需要 mock `HttpContext`、`ActionExecutingContext` 以及 service provider 本身。

#### 实现 Service Filter

更好的替代方案是使用 Service Filter。
不再创建一个包含逻辑的特性,而是创建一个实现 `IAsyncActionFilter` 接口的类。
这样过滤器就能完整参与 DI 生命周期,从而可以通过构造函数注入取得像 logger 这样的必需服务。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-service-filters-53953168/?t=55)

实现逻辑和基于特性的做法基本相同,但依赖现在是在构造函数中显式声明、并由容器提供的。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-service-filters-53953168/?t=70)

#### 注册与应用

由于过滤器现在依赖构造函数注入,它必须注册到 DI 容器中。
通常这类过滤器会在 `Program.cs` 中以 scoped 生命周期注册。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-service-filters-53953168/?t=85)

要把这个过滤器应用到某个控制器或某个具体 action 上,可以使用 `[ServiceFilter]` 特性并指定过滤器类的类型。
这会告诉 ASP.NET Core 在运行时从 DI 容器中解析该过滤器实例,从而确保它的所有依赖都被正确注入。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-service-filters-53953168/?t=100)

在上面的例子中,原先的 `[DurationLogger]` 特性被替换成了 `[ServiceFilter(typeof(DurationLoggerFilter))]`。
这种做法保证了过滤器完全可测试,因为在单元测试时你只需把一个 mock 的 logger 传入过滤器的构造函数即可。

---

## 8. Resolving dependencies from Middleware

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-middleware-53953169/) · 2:32

**总结**

本课演示如何在 ASP.NET Core 中实现自定义中间件,以处理请求计时和日志这类横切关注点。
它重点说明了中间件构造的标准模式、通过构造函数注入依赖(而不是从 HttpContext 手动解析服务)的正确方式,以及中间件在请求管道中顺序的重要性。

**核心概念**

* 中间件的结构与 `RequestDelegate`。
* `InvokeAsync` 方法与 `HttpContext`。
* 在中间件中使用构造函数注入服务。
* 使用 `app.UseMiddleware<T>` 注册中间件。
* 中间件在管道中顺序的重要意义。

**课程笔记**

ASP.NET Core 中的中间件提供了一种在请求管道中执行逻辑的方式。
过滤器通常只应用于特定的 action 或控制器,中间件则不同,它可以全局地作用于所有请求。
一个标准的中间件类需要在构造函数中接收一个 `RequestDelegate`,并提供一个接受 `HttpContext` 的 `InvokeAsync` 方法。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-middleware-53953169/?t=25)

要实现包裹整个请求的逻辑(比如给请求计时),你需要把对下一个委托的调用放进 try-finally 块中。
这样可以确保逻辑在管道中后续组件执行之前和之后都会运行。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-middleware-53953169/?t=40)

虽然从 `HttpContext.RequestServices` 手动解析服务是可行的,但更推荐、也更地道的做法是使用构造函数注入。
你可以把任何已注册的服务(例如 `ILogger`)和 `RequestDelegate` 一起直接注入到中间件的构造函数中。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-middleware-53953169/?t=145)

中间件必须在 `Program.cs` 文件中通过 `WebApplication` 实例上的 `UseMiddleware<T>` 扩展方法注册。
服务注册的顺序通常无关紧要,中间件注册的顺序却至关重要,因为它决定了请求管道的执行次序。
把计时中间件放在最顶端,可以确保它测量的是整个请求的耗时,涵盖后续所有中间件以及最终的 action 执行。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-from-middleware-53953169/?t=115)

---

## 9. Resolving dependencies in Minimal APIs

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-minimal-apis-53953170/) · 3:13

### 总结

.NET 中的 Minimal API 提供了一种精简的依赖解析方式:把依赖直接注入到路由处理委托中。
与传统的基于控制器的做法不同,Minimal API 会自动从依赖注入容器中识别并解析服务,通常无需显式使用 [FromServices] 这样的特性。
本课探讨如何把 ILogger 这类服务注入到端点中,并强调在应用级别与请求级别解析依赖时,理解服务生命周期和作用域的重要性。

### 核心概念

* **Delegate Parameter Injection(委托参数注入)**:服务直接传入映射到路由的 lambda 或方法。
* **Implicit Resolution(隐式解析)**:框架会自动识别容器中的服务,无需 `[FromServices]` 特性。
* **Request Scoping(请求作用域)**:在路由委托中解析出的依赖,其作用域是这一次具体的 HTTP 请求。
* **应用作用域 vs. 请求作用域**:通过 `app.Services`(应用作用域)解析服务,如果这些服务本应限定在请求作用域内,就可能引发问题。
* **Middleware Consistency(中间件一致性)**:中间件注册仍然沿用标准 ASP.NET Core 的做法,即 `app.UseMiddleware<T>()`。

### 课程笔记

.NET 6 引入的 Minimal API 让开发者能以更简洁的方式定义端点及其逻辑,从而简化了构建 API 的过程。
Minimal API 的一个关键特性,就是它处理依赖注入的方式。
标准的中间件注册与其他 ASP.NET Core 模板保持一致;你依然可以使用 `app.UseMiddleware<T>()` 来拦截所有已映射端点上的请求。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-minimal-apis-53953170/?t=25)

在 Minimal API 中,你可以把依赖作为参数加到路由处理器的委托(lambda 函数)上来解析它们。
例如,如果你想在某个 `MapGet` 端点里使用日志,只需把 `ILogger<Program>` 放进参数列表即可。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-minimal-apis-53953170/?t=70)

Minimal API 框架足够"聪明",它会去查看依赖注入容器(该容器在 `WebApplication.CreateBuilder(args)` 阶段被填充),并自动提供所请求的服务。
早期版本或其他模式可能要求显式加上 `[FromServices]` 特性,但在这里并非必需,因为框架会自动识别服务类型。

从技术上讲,你也可以通过 `app.Services` 属性手动解析服务,它提供了对 `IServiceProvider` 的访问。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-minimal-apis-53953170/?t=145)

不过,在应用级别手动解析是有风险的。
当你在请求委托之外使用 `app.Services.GetRequiredService<T>()` 解析服务时,你是在应用作用域中解析它。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-minimal-apis-53953170/?t=160)

这是一个常见错误。
在应用级别解析出来的服务会在整个应用生命周期内一直存在,而注入到 `MapGet` 委托中的服务是在请求作用域内解析的。
在本应使用请求作用域服务的地方使用了应用作用域的服务,可能导致意料之外的行为或资源泄漏。
正确且最安全的做法,是把依赖直接注入到路由处理器的参数中。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-minimal-apis-53953170/?t=175)

---

## 10. Resolving dependencies in Razor Views & Pages

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-razor-views-pages-53953172/) · 1:28

### 总结

本课演示如何使用 @inject 指令,直接在 Razor 视图和 Razor Pages 中解析依赖。
对控制器或 PageModel 这类类而言,构造函数注入是标准做法;而当 UI 渲染本身需要某些数据或逻辑时,@inject 指令可以让你直接在标记中访问服务。

### 核心概念

* @inject 指令的语法:@inject <Type> <Name>。
* 把服务注入到 MVC 视图(.cshtml)中。
* 把服务注入到 Razor Pages 中。
* 在 Razor 标记中通过 @ 符号访问服务成员。
* 配套类仍然保持标准的构造函数注入。

### 课程笔记

在 ASP.NET Core MVC 和 Razor Pages 中,你可以直接在标记文件里解析服务。
当某个视图需要一个既不由控制器也不由 PageModel 提供的服务时,这一点尤其有用。

来看一个用于提供消息的服务类:

```csharp
namespace ResolvingDeps.Mvc;

public class ServiceToInject
{
    public string Message => "I was injected!";
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-razor-views-pages-53953172/?t=25)

要在 MVC 视图(例如 Index.cshtml)中解析这个服务,在文件顶部使用 @inject 指令。
你必须指定服务类型,并为该实例提供一个名称。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-razor-views-pages-53953172/?t=55)

服务注入之后,就可以在 Razor 文件的任何位置,通过 @ 符号加上实例名来访问它的属性和方法。

这种做法在 Razor Pages 中完全相同。
虽然 Razor Pages 通常会使用 PageModel,并在其中通过构造函数注入解析依赖,但 @inject 指令依然可以直接在 .cshtml 文件中使用。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-razor-views-pages-53953172/?t=80)

在视图中使用 @inject 指令不会影响项目其余部分的架构;控制器和其他类应当继续通过它们的构造函数解析依赖。

---

## 11. Resolving dependencies in Blazor

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-blazor-53953173/) · 1:00

Blazor 在 Server 和 WebAssembly 两种模型下采用一致的依赖注入方式,与 ASP.NET Core MVC 和 Razor Pages 中的模式如出一辙。
通过在 Razor 组件中使用 @inject 指令,开发者可以轻松解析 HttpClient 或自定义业务逻辑服务。
Blazor 应用中的普通 C# 类则继续使用传统的构造函数注入来解析依赖,从而在整个 .NET 生态中保持统一的开发体验。

### 核心概念

*   **@inject 指令**:在 Razor 组件中解析依赖的主要机制。
*   **Model Consistency(模型一致性)**:Blazor Server 与 Blazor WebAssembly 共用相同的依赖注入语法。
*   **Constructor Injection(构造函数注入)**:Blazor 项目中的普通 C# 类仍然使用构造函数注入来解析服务。
*   **与 ASP.NET Core 对齐**:Blazor 中的 DI 模式在设计上就让来自 MVC 或 Razor Pages 的开发者感到熟悉。

### 课程笔记

Blazor 采用的依赖注入机制与 ASP.NET Core MVC 和 Razor Pages 高度一致。
这种一致性让开发者在 .NET 生态中不同项目类型之间的切换变得更简单。
在 Razor 组件中,依赖通过 `@inject` 指令解析。
该指令会指定服务类型,以及用于访问该服务的属性名称。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-blazor-53953173/?t=55)

这种做法在 Blazor Server 和 Blazor WebAssembly 中是完全一样的。
例如,在 Blazor WebAssembly 应用中,通常会注入 `HttpClient`,以便在组件生命周期方法(比如 `OnInitializedAsync`)中执行数据获取操作。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-blazor-53953173/?t=40)

在 Blazor Server 中,你注入的可能是某个具体的服务类(比如 `WeatherForecastService`)而不是 `HttpClient`。
在 `@code` 块中的用法保持不变,依然是用注入进来的属性去调用服务方法来获取数据。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-blazor-53953173/?t=25)

Razor 组件使用 `@inject` 指令,而 Blazor 项目中的普通 C# 类仍然使用构造函数注入。
这确保了依赖注入的核心原则在整个应用架构中都得到贯彻。

---

## 12. Resolving dependencies in gRPC Services

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-grpc-services-53953174/) · 1:16

**总结**

.NET 中的 gRPC 服务通过标准的构造函数注入来使用内置的依赖注入容器。
只要在 Protobuf 文件中定义服务契约,并继承生成的基类,开发者就能轻松注入 logger 或业务服务这类依赖。
整个集成在应用启动时收尾:把 gRPC 服务添加到容器并映射服务端点,以确保运行时所有级联的依赖都能被正确解析。

**核心概念**

* 用 Protobuf(.proto)文件作为服务契约的模板。
* 继承自生成的基类(例如 GreeterBase)。
* 使用标准的构造函数注入解析服务依赖。
* 在 service collection 中通过 AddGrpc() 注册。
* 通过 MapGrpcService<T>() 映射端点,以启用 DI 解析。

**课程笔记**

gRPC 是一个日益流行的框架,适用于高性能的内部服务通信,尤其是在偏好基于契约通信的场景中。
在 .NET 的 gRPC 项目里,服务契约使用 Protocol Buffers(protobuf)文件来定义。
这个文件充当模板,在幕后生成所需的 C# 基类和消息类型。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-grpc-services-53953174/?t=40)

服务实现类继承自生成的基类(例如 `Greeter.GreeterBase`)。
gRPC 服务中的依赖注入非常直接,与其他 .NET 组件遵循同样的模式:依赖通过类的构造函数来请求。
微软的实现确保你无需手动解析服务;标准的构造函数模式保持不变。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-grpc-services-53953174/?t=55)

要启用依赖解析,gRPC 服务必须在 `Program.cs` 中完成注册和映射。
`AddGrpc` 方法把所需的基础设施添加到 service collection 中。
随后 `MapGrpcService<T>` 方法初始化服务端点。
这个映射确保了当服务被调用时,它所需的任何依赖、以及由这些依赖级联出来的依赖,都会自动从 DI 容器中解析出来。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-grpc-services-53953174/?t=70)

---

## 13. Resolving dependencies in Hosted Services

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-hosted-services-53953175/) · 2:38

### 总结

在 ASP.NET Core 中,hosted service 用于执行后台操作,例如处理消息队列或运行定时任务。
通过继承 BackgroundService 基类,开发者可以实现 ExecuteAsync 方法来定义长时间运行的逻辑,并借助 CancellationToken 遵循应用的生命周期。
这些服务使用 AddHostedService 注册到 IServiceCollection 中,并且完整支持标准的依赖注入,可以通过构造函数解析 ILogger 之类的服务。

### 核心概念

- 使用 IHostedService 和 BackgroundService 执行后台任务。
- 重写 ExecuteAsync 以实现持续执行的逻辑。
- 用 CancellationToken 管理服务生命周期。
- 通过 AddHostedService<T>() 注册 hosted service。
- 在后台服务中通过构造函数注入解析依赖。

### 课程笔记

Hosted service(后台服务)是 ASP.NET Core 中一项强大的功能,用于在后台运行任务,比如队列消费者或周期性定时器。
你可以直接实现 IHostedService 接口,但对于持续运行的后台任务,通常更推荐使用 BackgroundService 基类,因为它提供了更简化的实现模式。

要创建一个后台服务,定义一个继承自 BackgroundService 的类,并重写 ExecuteAsync 方法。

```csharp
namespace ResolvingDeps.WebApi.HostedServices;

public class BackgroundTicker : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-hosted-services-53953175/?t=70)

ExecuteAsync 方法会收到一个 CancellationToken,应当用它来监测应用何时正在关闭。
一种常见模式是使用一个 while 循环来检查 IsCancellationRequested。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-hosted-services-53953175/?t=85)

在循环内部,你可以执行后台逻辑。
重要的是要把 stoppingToken 传给任何异步方法(例如 Task.Delay),以确保服务能及时响应关闭请求。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-hosted-services-53953175/?t=100)

要让 hosted service 生效,必须使用 IServiceCollection 上的 AddHostedService 扩展方法把它注册到依赖注入容器中。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-hosted-services-53953175/?t=115)

Hosted service 完整支持依赖注入。
与其使用 Console.WriteLine,你应该通过构造函数注入 ILogger<T> 这类依赖。
当 hosted service 在应用启动时被实例化,DI 容器会解析这些依赖。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-hosted-services-53953175/?t=145)

---

## 14. Resolving dependencies in service registration

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-service-registration-53953176/) · 1:54

### 总结

在 .NET 中,依赖注入容器通常会自动处理服务的实例化,但开发者也可以在注册过程中手动解析依赖。
借助 AddScoped 等方法的工厂重载,你可以拿到一个 IServiceProvider 实例,显式取出所需的服务,并把它们传入某个类的构造函数。
当初始化逻辑较为复杂,或者服务加入容器前需要特定的手动配置时,这种做法提供了必要的灵活性。

### 核心概念

- 服务注册的工厂重载。
- 使用 IServiceProvider 手动解析依赖。
- GetRequiredService<T> 方法。
- 注册工厂内部的作用域执行。
- 用已有依赖自定义服务的实例化过程。

### 课程笔记

在标准的 .NET 依赖注入中,服务往往只需提供实现类型即可完成注册。
容器随后会使用反射识别构造函数,并自动解析依赖。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-service-registration-53953176/?t=25)

不过,有些情况下你需要以特定方式解析服务,或者需要手动完成实例化。
这时可以使用一个提供 `IServiceProvider`(通常命名为 `provider`)的工厂委托。
这个 provider 的作用域绑定到该次注册,代表服务被请求时将被实体化的那个 service provider。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-service-registration-53953176/?t=40)

在这个工厂内部,你可以用 `provider` 通过 `GetRequiredService<T>()` 解析已有的服务。
这让你能手动取出依赖(比如一个 `ILogger`),再把它传入你正在注册的那个服务的构造函数。
对于需要在服务实例化过程中执行自定义逻辑的高级场景,这一技巧是必不可少的。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/resolving-dependencies-in-service-registration-53953176/?t=100)

---

## 15. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953180/) · 0:31

### 总结

本课总结了在 .NET 生态中解析依赖的各种方式,覆盖控制台应用、Web API、MVC、Razor Pages、Blazor、gRPC 和 Hosted Service 等项目类型。
它强调服务解析可以发生在 UI 组件中、后台任务中,甚至发生在服务注册阶段本身。

### 核心概念

* 控制台应用与 Web API 中的依赖解析。
* 与 MVC、Razor Pages 和 Razor 视图的集成。
* Blazor 组件与 gRPC 服务中的服务注入。
* 在 Hosted Service 中利用依赖完成后台处理。
* 在注册过程中解析服务。

### 课程笔记

本章全面介绍了如何在不同的 .NET 项目模板中解析服务。
内置依赖注入(DI)框架的灵活性,使它能够无缝融入各种应用架构。

覆盖的关键领域包括:
* **Web 与 UI 框架**:在 Web API、MVC 控制器和 Razor Pages 中解析依赖的实现细节。这也包括在 Razor 视图和 Blazor 组件中使用 `@inject` 指令,把服务直接带入 UI 层。
* **通信与后台任务**:在 gRPC 服务和 Hosted Service 中解析依赖的技巧,确保后台逻辑能够访问所需的应用服务。
* **控制台与注册逻辑**:控制台应用中的手动解析,以及在服务注册逻辑内部就能解析依赖的能力,从而可以基于其他已注册的服务进行动态配置。
