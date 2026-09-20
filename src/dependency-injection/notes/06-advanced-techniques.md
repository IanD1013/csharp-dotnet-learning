# Advanced techniques

> 课程:[From Zero to Hero: Dependency Injection in .NET with C#](https://dometrain.com/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp/) · 第 6 章
> 共 8 课 · 约 47:32
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

---

## 课程索引

| #   | 课程                                                                                                                                                                                       | 时长  | 小节                                        |
| --- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ----- | ------------------------------------------- |
| 1   | [Creating a custom scope](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/creating-a-custom-scope-53953210/)                         | 5:42  | [↓](#1-creating-a-custom-scope)             |
| 2   | [The service locator anti-pattern](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-service-locator-anti-pattern-53953211/)       | 2:48  | [↓](#2-the-service-locator-anti-pattern)    |
| 3   | [When service locator makes sense](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/when-service-locator-makes-sense-53953212/)       | 20:37 | [↓](#3-when-service-locator-makes-sense)    |
| 4   | [Avoiding capturing dependencies](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/avoiding-capturing-dependencies-53953213/)         | 3:14  | [↓](#4-avoiding-capturing-dependencies)     |
| 5   | [Avoiding multiple service providers](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/avoiding-multiple-service-providers-53953214/) | 3:18  | [↓](#5-avoiding-multiple-service-providers) |
| 6   | [Creating decorators](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/creating-decorators-53953215/)                                 | 7:11  | [↓](#6-creating-decorators)                 |
| 7   | [The future of dependency injection](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-future-of-dependency-injection-53953216/)   | 3:55  | [↓](#7-the-future-of-dependency-injection)  |
| 8   | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953217/)                                             | 0:47  | [↓](#8-section-recap)                       |

---

## 1. Creating a custom scope

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/creating-a-custom-scope-53953210/) · 5:42

### 总结

在控制台应用或后台服务中,DI 容器不会像在 ASP.NET Core 中那样自动管理作用域。
要在这些环境中正确使用 scoped 服务,比如在处理来自消息总线的单条消息时,开发者必须手动创建并管理自定义作用域。
本课演示如何使用 IServiceProvider.CreateScope() 和 IServiceScopeFactory 为 scoped 生命周期划定边界,确保服务在某个特定的工作单元内被正确解析和释放。

### 核心概念

- **Implicit vs. Explicit Scopes(隐式作用域 vs. 显式作用域)**:在 ASP.NET Core 中,作用域与一次 HTTP 请求的生命周期绑定;在控制台应用中,作用域必须手动定义。
- **Scoped Service Behavior(Scoped 服务的行为)**:如果没有活动的作用域,注册为 scoped 的服务会被根 service provider 当作单例对待。
- **IServiceScope**:一个表示临时边界的接口,scoped 服务就存活在这个边界内。
- **IServiceScopeFactory**:创建作用域时在架构上更推荐的做法,它提供了一个比完整的 service provider 更窄、更受控的接口。

### 课程笔记

在标准的 Web API 中,DI 容器会自动管理作用域。
但在控制台应用中,或者在消费事件总线(比如 RabbitMQ 或 Azure Service Bus)消息的服务中,你往往希望用一个作用域来表示对单条消息的处理。
如果没有手动定义作用域,容器就无从知道一个 scoped 生命周期应该何时开始、何时结束。

考虑这样一个服务,用它来演示多次解析之间实例的身份:

```csharp
namespace CustomScope.ConsoleApp;

public class ExampleService
{
    public Guid Id { get; } = Guid.NewGuid();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/creating-a-custom-scope-53953210/?t=85)

如果你在控制台应用中把这个服务注册为 scoped,并直接从根 `IServiceProvider` 解析它,它的行为会和单例一样。
每次解析都会返回同一个实例,因为没有建立任何作用域边界。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/creating-a-custom-scope-53953210/?t=100)

要创建自定义作用域,使用 `CreateScope()` 方法。
它返回一个 `IServiceScope`,其中包含它自己的 `ServiceProvider`。
从这个作用域的 provider 解析出来的服务是该作用域独有的。
把作用域包在 `using` 块里,可以确保该作用域内创建的所有服务在块结束时被正确释放。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/creating-a-custom-scope-53953210/?t=235)

在 `IServiceProvider` 上调用 `CreateScope()` 虽然可以工作,但它可以被看作对 Service Locator 模式的一种违反,因为它让使用方拿到了功能完整的 provider。
在架构上更合理的做法是使用 `IServiceScopeFactory`。
这个工厂由 DI 容器自动注册,它唯一的职责就是创建作用域。
这让代码的意图更清晰,也限制了注入该工厂的组件所获得的能力。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/creating-a-custom-scope-53953210/?t=310)

---

## 2. The service locator anti-pattern

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-service-locator-anti-pattern-53953211/) · 2:48

### 总结

在现代 .NET 开发中,Service Locator 模式被认为是一种反模式,因为它会掩盖一个类的依赖,并让单元测试变得复杂。
如果在方法体内直接从 IServiceProvider 或 HttpContext 解析服务,而不是通过构造函数注入,开发者就隐藏了组件真正的需求,使代码更难维护、也更难有效地进行 mock。

### 核心概念

- **Service Locator Definition(Service Locator 的定义)**:一种在类内部通过中心化的注册表或 provider(比如 `IServiceProvider`)按需解析依赖,而不是让依赖被注入进来的模式。
- **Hidden Dependencies(隐藏的依赖)**:使用 Service Locator 会隐藏一个类实际需要什么才能工作,让它的 API 表面无法诚实地表达自身的需求。
- **Testing Friction(测试摩擦)**:mock 依赖会变得困难得多,因为测试者必须搭建整套 service provider 基础设施,而不是只把一个 mock 传进构造函数。
- **Intent(意图)**:显式的构造函数注入能展现类的意图,而 Service Locator 则要求阅读实现细节才能弄清依赖。

### 课程笔记

Service Locator 是一种让组件通过查询中心化的容器或 provider 来解析自身依赖的模式。
在现代 .NET 中,这通常是通过直接使用 `IServiceProvider` 来完成的。
它曾经很常见,但如今被广泛视为一种反模式,因为它绕开了依赖注入的主要好处。

这种反模式的一个常见例子出现在 ASP.NET Core 的 filter 或 attribute 中,开发者可能会用 `HttpContext` 手动解析服务:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-service-locator-anti-pattern-53953211/?t=25)

#### 为什么它是一种反模式

**1. Lack of Intent(缺乏意图表达)**
意图是软件设计中至关重要的一个方面。
当一个类在构造函数中声明它的依赖时(例如 `public MyController(IDatabase db)`),它就明确地表达了自己需要一个数据库才能工作。
如果你在方法内部使用 Service Locator,这个类的使用方不去读内部实现就无法知道需要哪些依赖。
如果拿不到源代码,这个类就变成了一个带有隐藏需求的“黑盒”。

**2. Testing and Mocking Difficulties(测试与 mock 的困难)**
Service Locator 会让单元测试复杂得多。
如果一个方法把依赖解析藏在方法体内,除非开发者确切知道该 mock 哪个服务、以及如何把它注册到 mock 容器里,否则测试会在运行时以 `NullReferenceException` 或解析错误失败。
在上面的例子中,要测试 `DurationLoggerAttribute`,开发者必须 mock `ActionExecutingContext`,它里面包含一个 `HttpContext`,后者又包含一个 `RequestServices` provider,最后才返回 `ILogger`。
与简单地把一个 mock logger 传进构造函数相比,这会造成一套“混乱”的测试搭建。

**3. Maintenance Overhead(维护开销)**
一般不建议注入 `IServiceProvider` 来代替所需的具体服务。
你应当始终优先注入真正需要的那些具体服务。
如果你发现自己处在不得不使用 Service Locator 的处境,那就应当把它当作一种边缘情况来对待,需要用详尽的文档说明这段代码应该怎么测试,以及为什么必须使用这种模式。

确实存在一些非常特定的、合理的使用场景,其中 Service Locator 的收益超过它的弊端(比如某些框架层面的约束),但这类场景很少见,而且应当向开发团队清楚地说明。

---

## 3. When service locator makes sense

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/when-service-locator-makes-sense-53953212/) · 20:37

### 总结

虽然 Service Locator 模式通常被认为是一种反模式,但对于那些必须根据运行时数据(例如命令行参数或消息类型)动态解析服务的基础设施级组件来说,它是一个合理的选择。
本课演示如何实现一个“Handler Orchestrator”,它利用程序集扫描和自定义 attribute 把字符串命令映射到具体的 handler 实现。
通过注入 IServiceScopeFactory 并创建一个局部的 service provider 作用域,orchestrator 可以为特定的 handler 解析依赖,而不必把所有可能的服务实现都塞进主应用的构造函数,这实际上模仿了 MediatR 这类库的内部行为。

### 核心概念

- **Infrastructure-level Service Location(基础设施级的 Service Locator)**:在专门的 orchestrator 内部使用 Service Locator,而不是把它当作全局依赖。
- **Dynamic Command Dispatching(动态命令分发)**:把运行时的字符串(例如 CLI 参数)映射到具体的服务类型。
- **Assembly Scanning(程序集扫描)**:自动发现程序集中实现了某个特定接口的类型。
- **Metadata-driven Resolution(元数据驱动的解析)**:使用自定义 attribute 把元数据(比如命令名)与实现类型关联起来。
- **Scoped Resolution in Singletons(在单例中解析 scoped 服务)**:使用 `IServiceScopeFactory` 从单例 orchestrator 内部解析 scoped 或 transient 服务。

### 课程笔记

在复杂的应用中,比如一个多功能的控制台应用,你可能需要根据运行时输入执行不同的逻辑。
一个常见的例子是接受 `weather` 或 `time` 这类命令的 CLI。
把这些映射硬编码进主应用类会导致构造函数臃肿、代码僵化。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/when-service-locator-makes-sense-53953212/?t=25)

为了动态处理这件事,我们定义一个公共接口 `IHandler`,所有命令实现都必须满足它。

```csharp
namespace MultiFunction.ConsoleApp.Handlers;

public interface IHandler
{
    Task HandleAsync();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/when-service-locator-makes-sense-53953212/?t=175)

各个 handler,比如 `GetCurrentLondonWeatherHandler`,实现这个接口,并通过标准的构造函数注入接收它们各自特定的依赖。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/when-service-locator-makes-sense-53953212/?t=250)

为了动态解析这些 handler,我们实现一个 `HandlerOrchestrator`。
这个类是一个专用的 Service Locator。
我们不把所有可能的 handler 都注入 `Application` 类,而是注入这个 orchestrator。
orchestrator 使用 `IServiceScopeFactory` 创建作用域,并在运行时解析所需的 handler 类型。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/when-service-locator-makes-sense-53953212/?t=370)

orchestrator 通过在字典中查找类型,并使用新作用域中的 `IServiceProvider` 来实例化它,从而解析出 handler。
这确保了 handler 的依赖会按照它们注册的生命周期被正确管理。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/when-service-locator-makes-sense-53953212/?t=475)

为了避免手动注册每一条命令到类型的映射,我们用一个自定义 attribute `CommandNameAttribute` 来标注 handler 类。

```csharp
namespace MultiFunction.ConsoleApp.Handlers;

[AttributeUsage(AttributeTargets.Class)]
public class CommandNameAttribute : Attribute
{
    public string CommandName { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/when-service-locator-makes-sense-53953212/?t=535)

借助反射和程序集扫描,orchestrator 可以自动发现所有实现了 `IHandler` 且被 `CommandNameAttribute` 标注的类,并填充它内部的映射字典。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/when-service-locator-makes-sense-53953212/?t=655)

最后,为了让这套系统真正做到即插即用,我们为 `IServiceCollection` 编写扩展方法。
这些方法既负责注册 orchestrator,也负责把所有发现的 handler 类型自动注册到 DI 容器中。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/when-service-locator-makes-sense-53953212/?t=1135)

这种做法提供了一个清晰、可扩展的架构:添加一条新命令只需要创建一个实现 `IHandler` 的新类,并加上 `CommandName` attribute。
其余的事情由基础设施通过受控的 Service Locator 来完成。

---

## 4. Avoiding capturing dependencies

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/avoiding-capturing-dependencies-53953213/) · 3:14

### 总结

在闭包中捕获依赖是 .NET 应用中的一个常见错误,在使用 Minimal API 时尤其如此。
当一个服务在请求处理器之外从根 service provider 解析出来,然后在该处理器的 lambda 表达式内部被使用时,它就被当作闭包捕获了。
这实际上让该服务在应用的整个生命周期内变成了单例,无论它原本预期的生命周期是什么(例如 transient 或 scoped),并可能导致严重的并发问题和状态损坏。

### 核心概念

- **Closure Capture(闭包捕获)**:在 lambda 表达式中访问外层作用域的变量,导致该变量只要 lambda 还存在就一直被保留在内存中。
- **Root Provider Resolution(从根 provider 解析)**:直接从 `app.Services`(根容器)解析服务,而不是从请求专属的作用域解析。
- **Lifetime Distortion(生命周期扭曲)**:transient 或 scoped 服务因为被一个长生命周期的委托捕获,从而表现得像单例的现象。
- **Minimal API Parameter Injection(Minimal API 的参数注入)**:把依赖声明为端点处理器的参数,以确保它们能从请求作用域中被正确解析。

### 课程笔记

在标准的 Minimal API 实现中,依赖通常直接注入到路由处理器委托中。
这确保 Model Context Protocol 或底层的依赖注入(DI)容器能够正确管理服务的生命周期。
例如,如果一个服务注册为 transient,那么每次调用该端点时它都会被实例化。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/avoiding-capturing-dependencies-53953213/?t=20)

在上面的实现中,`OpenWeatherService` 是通过方法参数解析的。
如果你在 `OpenWeatherService` 的构造函数里打一个断点,你会看到它在每次请求时都被触发,因为它注册的是 transient 生命周期。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/avoiding-capturing-dependencies-53953213/?t=50)

一个常见但致命的缺陷发生在:开发者在请求尚未被创建之前,就从根 service provider(`app.Services`)解析了服务。
在处理器作用域之外解析服务、然后在 lambda 内部使用它,会让这个服务被当作闭包捕获。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/avoiding-capturing-dependencies-53953213/?t=100)

在这种情况下,`weatherService` 在应用启动时只被解析一次。
尽管它注册的是 `Transient`,现在却被当成了一个脱离作用域的单例,因为每一次请求都复用同一个实例。
这绕过了原本设计的 DI 生命周期,并可能引发严重的并发问题,尤其当该服务不是线程安全的时候。

要修复这个问题,把服务作为参数传给处理器委托,确保它们在正确的作用域内被解析。
这样框架就能在每次执行时从请求专属的作用域中正确解析依赖。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/avoiding-capturing-dependencies-53953213/?t=175)

---

## 5. Avoiding multiple service providers

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/avoiding-multiple-service-providers-53953214/) · 3:18

创建多个 service provider 是一个常见错误,它会在 .NET 应用中引发难以察觉、难以调试的问题。
当一个 ASP.NET Core 应用被构建出来时,它通常会管理一个内部的 service provider,在应用的整个生命周期内用它来解析依赖。

### 核心概念

- **Independent Containers(相互独立的容器)**:调用 `BuildServiceProvider()` 会创建一个全新的、彼此隔离的依赖注入容器。
- **Singleton Violation(单例被破坏)**:单例只在单个 service provider 的范围内是唯一的。多个 provider 会导致出现多个“单例”实例。
- **Startup Pitfalls(启动阶段的陷阱)**:在启动过程中手动构建 provider(例如为了执行数据库迁移或读取配置)是造成这个问题的主要原因。
- **Best Practices(最佳实践)**:在 Web API 中依赖框架的 `builder.Build()`;在控制台应用中确保 `BuildServiceProvider()` 只被调用一次。

### 课程笔记

在标准的 Minimal API 配置中,服务被注册到 `IServiceCollection`,并由框架自动解析。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/avoiding-multiple-service-providers-53953214/?t=10)

当开发者需要在应用完全启动之前访问某个服务时,问题就出现了。
例如,考虑一个注册为单例的 `IdGenerator`:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/avoiding-multiple-service-providers-53953214/?t=55)

如果你在配置阶段调用 `BuildServiceProvider()` 来解析这个 `IdGenerator`,你就创建了第二个容器:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/avoiding-multiple-service-providers-53953214/?t=85)

当应用最终运行并处理请求时,它使用的是自己内部的 service provider。
如果端点也注入了 `IdGenerator`,那么提供给端点的实例会与启动期间解析出来的实例不同。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/avoiding-multiple-service-providers-53953214/?t=130)

在这种情况下,控制台会输出两个不同的 GUID,尽管该服务注册的是单例。
这是因为每个 service provider 都维护着自己内部的单例实例字典。
如果这个单例本来是用来管理共享状态的,比如缓存或数据库连接池,那就可能导致严重的故障。

要解决这个问题,避免在 Web API 的 `Program.cs` 中调用 `BuildServiceProvider()`。
取而代之的是,在应用构建完成之后使用 `app.Services` 进行服务解析,或者在某次请求的作用域内解析。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/avoiding-multiple-service-providers-53953214/?t=190)

---

## 6. Creating decorators

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/creating-decorators-53953215/) · 7:11

### 总结

装饰器让你能够在不修改服务原有实现的前提下为它添加行为,从而遵循单一职责原则。
用一个实现了相同接口的装饰器把核心服务包起来,你就可以注入日志记录或性能监控这类横切关注点。
在 .NET 中,这是通过把基础实现按具体类型注册,然后用一个工厂函数注册接口来实现的,该工厂函数解析出基础类型并把它包进装饰器。

### 核心概念

- **Decorator Pattern(装饰器模式)**:一种结构型模式,允许在不影响同一个类的其他对象行为的前提下,动态地为某个单独对象添加行为。
- **Separation of Concerns(关注点分离)**:把业务逻辑(比如获取天气)与基础设施关注点(比如日志和计时)分开。
- **Dependency Redirection(依赖重定向)**:配置 DI 容器,让接口解析到装饰器,而装饰器再去解析底层的实现。
- **Factory Registration(工厂式注册)**:使用 `IServiceCollection` 的工厂委托,手动实例化装饰器并传入它所需的依赖。

### 课程笔记

在标准的 .NET API 中,controller 通常依赖一个接口来完成业务逻辑。
例如,`WeatherForecastController` 可能依赖 `IWeatherService` 来获取某个特定城市的数据。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/creating-decorators-53953215/?t=25)

虽然你可以用 `Stopwatch` 和 `try/finally` 块直接在 `OpenWeatherService` 实现内部完成计时和日志逻辑,但这违反了单一职责原则。
这个服务应当只关心与天气 API 的通信。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/creating-decorators-53953215/?t=140)

更易维护的做法是创建一个装饰器。
`LoggedWeatherService` 类实现同一个 `IWeatherService` 接口,并在构造函数中接收一个 `IWeatherService` 实例。
这让它可以充当真实实现的包装器。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/creating-decorators-53953215/?t=250)

装饰器把真正的工作委托给内部的服务,同时独立地处理计时和日志逻辑。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/creating-decorators-53953215/?t=405)

要在依赖注入容器中配置这一点,你必须把具体实现(`OpenWeatherService`)按它自身的类型注册。
然后,用一个工厂委托来注册接口(`IWeatherService`),该委托解析出具体服务和 logger,用它们来实例化装饰器。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/creating-decorators-53953215/?t=415)

当应用解析 `IWeatherService` 时,它拿到的是 `LoggedWeatherService`。
在内部,装饰器使用解析出来的 `OpenWeatherService` 来执行真正的 API 调用,从而有效地把指标采集与业务逻辑解耦。

---

## 7. The future of dependency injection

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-future-of-dependency-injection-53953216/) · 3:55

源生成器在 C# 9 和 .NET 5 中被引入,它带来了一种元编程形式:在编译期检查代码并生成额外的代码。
在依赖注入的语境下,这使得创建不依赖运行时反射的 service provider 成为可能。

### 核心概念

- **Source Generators(源生成器)**:C# 9 引入的元编程能力,允许在编译期检查代码并生成额外的源文件。
- **Compile-time Safety(编译期安全)**:DI 配置中的错误(例如缺少依赖)会在构建过程中被发现,而不是在运行时。
- **Performance(性能)**:消除了反射的开销,带来更快的启动时间和近乎即时的服务解析。
- **Jab Library(Jab 库)**:一个第三方库,它以一种贴近标准 .NET DI 的方式实现了源生成式的 DI。

### 课程笔记

源生成式 DI 的主要优势是编译期检查和性能。
如果某个服务缺失或注册有误,编译器会报错,从而避免运行时故障。
此外,由于解析逻辑是以标准 C# 代码的形式生成的,应用启动和服务解析都会明显更快,因为应用只是在调用代码,就好像这些代码是手写的一样。

要实现源生成式的 DI,可以使用 `Jab` 这类库。
首先,定义接口和实现:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-future-of-dependency-injection-53953216/?t=100)

接下来,创建一个 `partial` 类来充当 service provider。
这个类必须标注 `[ServiceProvider]` attribute。
服务通过直接写在类定义上的 `[Transient]`、`[Scoped]` 或 `[Singleton]` 这类 attribute 来注册。

```csharp
using Jab;

namespace DependencyInjectionFuture.ConsoleApp;

[ServiceProvider]
[Transient(typeof(IConsoleWriter), typeof(ConsoleWriter))]
public partial class MyServiceProvider
{

}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-future-of-dependency-injection-53953216/?t=130)

保存之后,源生成器会自动为这个 partial 类产出实现细节。
生成的代码负责服务的实例化和生命周期管理,包括作用域的创建和服务的释放。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-future-of-dependency-injection-53953216/?t=160)

最后,生成的 service provider 就可以在应用中使用了。
它提供了类型安全的 `GetService<T>` 方法,在解析依赖时不带运行时反射的开销。

```csharp
using DependencyInjectionFuture.ConsoleApp;


var serviceProvider = new MyServiceProvider();

var consoleWriter = serviceProvider.GetService<IConsoleWriter>();

consoleWriter.WriteLine("Hi From Source Generated DI");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-future-of-dependency-injection-53953216/?t=205)

虽然 ASP.NET Core 目前仍然严重依赖基于反射的 DI,但源生成式的替代方案凭借其效率和安全性,很可能在未来的 .NET 开发中占据更重要的位置。

---

## 8. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953217/) · 0:47

### 总结

本课全面总结了 .NET 中依赖注入的高级技巧,包括手动的作用域管理、Service Locator 模式的种种微妙之处,以及装饰器这类架构模式。
它还回顾了避免常见陷阱的最佳实践,比如被捕获的依赖和多余的 service provider,同时展望了源生成在 DI 未来中的角色。

### 核心概念

- **Manual Scope Creation(手动创建作用域)**:理解如何显式地创建和管理服务作用域。
- **Service Locator Pattern(Service Locator 模式)**:认识到为什么它通常被视为反模式,同时识别出那些因为能明确表达意图而可以接受它的特定场景。
- **Dependency Integrity(依赖的完整性)**:避免被捕获的依赖、防止 service provider 泛滥的各种策略。
- **Decorator Pattern(装饰器模式)**:利用内置的 DI 容器优雅地实现装饰器。
- **Source Generation(源生成)**:DI 朝着源生成式 service provider 演进,以获得更好的性能和编译期安全。

### 课程笔记

本章探索了若干用于精通 .NET 应用依赖注入的高级技巧。
这段旅程从手动的作用域管理开始,演示了如何创建自定义作用域,以便在标准的请求-响应周期之外控制服务的生命周期。

本章有相当一部分内容专门讨论 Service Locator 模式。
它常常因为隐藏依赖、让测试复杂化而被贴上反模式的标签,但在某些特定场景下它可以被有效地使用,尤其是当实现方式允许表达明确意图的时候。

为了保证架构的完整性,本章讲解了如何避免一些常见错误,比如被捕获的依赖,也就是生命周期较长的服务(例如 Singleton)持有生命周期较短的服务(例如 Scoped 服务)。
此外,这些课程还强调了通过避免在同一个应用上下文中创建多个 service provider 来维持单一事实来源的重要性。

在结构型模式方面,本章演示了如何优雅地实现装饰器。
这让你可以在不修改底层实现的情况下为服务添加行为,同时借助 DI 容器来解析这条被装饰的调用链。

最后,本章展望了 .NET 生态中依赖注入的未来。
源生成式的 service provider 代表着一次重大转变,它把解析逻辑移到编译期,以减少开销并提升启动性能。
下一章将在这些基础之上引入 Scrutor,这是一个旨在增强 .NET 内置依赖注入框架能力的库。
