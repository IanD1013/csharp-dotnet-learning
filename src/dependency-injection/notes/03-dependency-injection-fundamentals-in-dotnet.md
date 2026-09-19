# Dependency Injection fundamentals in .NET

> 课程:[From Zero to Hero: Dependency Injection in .NET with C#](https://dometrain.com/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp/) · 第 3 章
> 共 15 课 · 约 46:02
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

---

## 课程索引

| # | 课程 | 时长 | 小节 |
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

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/introduction-53953101/) · 0:38

### 总结

本课介绍 .NET 中依赖注入(DI)的基础概念,为深入探究这套系统的工作方式做好铺垫。
它聚焦于拆解 DI 容器以建立扎实的技术基础,并确立在本课程余下部分中使用的术语。

### 核心概念

*   **Foundational DI Concepts(DI 基础概念)**:理解 .NET 中依赖是如何被管理的基本机制。
*   **System Deconstruction(系统拆解)**:把 DI 框架拆开,理解它的内部组件。
*   **Terminology Standardization(术语统一)**:为讨论服务、生命周期和容器建立一套共同的语言。
*   **Architectural Foundation(架构基础)**:为后续章节中的高级实现策略打下基础。

### 课程笔记

"Dependency Injection fundamentals in .NET" 这一章全面介绍了 DI 在这个生态系统中是如何实现和管理的。
这节导论课强调,在进入高级用法之前先理解核心机制的重要性。

本章采取的方式是拆解整个 DI 系统。
通过分析构成这个框架的各个部分,开发者可以更好地理解如何在这一基础之上构建。
这种拆解对于超越简单使用、走向更专业的模式实现是必不可少的。

即便对于已经熟悉依赖注入的人来说,这节导论课也很关键,因为它确立了后续所有课程都会引用的具体术语和概念框架。
跳过这些基础内容,可能会在课程后面引入更复杂的场景时造成困惑。

---

## 2. The simplest setup with Dependency Injection

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-simplest-setup-with-dependency-injection-53953102/) · 6:34

### 总结

本课使用一个控制台应用演示 .NET 中依赖注入(DI)的基本机制,把核心组件从 ASP.NET Core 的复杂性中隔离出来。
它介绍了用于注册依赖的 ServiceCollection、作为实际 DI 容器的 ServiceProvider,以及自动构造函数解析的过程。
通过定义接口与具体实现之间的映射,容器可以递归地解析出一整张依赖图,确保所需的服务在运行时被提供给各个类,而无需手动实例化。

### 核心概念

* **ServiceCollection**:一个充当指令清单的类,定义各个服务应当如何初始化和注册。
* **ServiceProvider**:DI 容器的具体实现,由 ServiceCollection 构建而来,负责管理服务的解析。
* **Service Registration(服务注册)**:把抽象(接口)映射到具体实现的过程(例如把 `IWeatherService` 映射到 `OpenWeatherService`)。
* **Constructor Injection(构造函数注入)**:容器自动识别并通过类的构造函数提供所需依赖的机制。
* **Dependency Graph(依赖图)**:级联式的解析过程,容器会解析根类,以及该类和它的依赖所需要的全部后续服务。

### 课程笔记

要在最基础的层面理解依赖注入,把它放在一个标准控制台应用中来看会很有帮助。
这种环境会暴露出那些通常被 ASP.NET Core 的抽象所隐藏的底层组件。
过程从一个 `ServiceCollection` 开始,服务及其实现在这里被注册。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-simplest-setup-with-dependency-injection-53953102/?t=35)

在这套配置中,`AddSingleton` 被用来注册 `IWeatherService`(映射到 `OpenWeatherService`)以及 `Application` 类本身。
注册完成后,调用 `BuildServiceProvider()` 来创建容器。
这个容器持有每个已注册服务应当如何解析的逻辑。

`Application` 类并不手动实例化自己的依赖。
相反,它在构造函数中声明这些依赖:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-simplest-setup-with-dependency-injection-53953102/?t=85)

当 `serviceProvider.GetRequiredService<Application>()` 被调用时,DI 容器会检查 `Application` 的构造函数。
它识别出这里需要一个 `IWeatherService`。
因为 `IWeatherService` 被注册为 `OpenWeatherService`,容器会自动实例化 `OpenWeatherService` 并把它注入到 `Application` 实例中。
这样开发者就不必再写 `new Application(new OpenWeatherService())` 这样的代码了。

DI 容器实际上就像一个注册表,或者说一个包含解析指令的"大类"。
它会处理整张依赖图:如果 `OpenWeatherService` 自身的构造函数还需要其他服务,只要那些服务已经注册,容器同样会把它们解析出来。
如果某个必需的服务没有出现在 `ServiceCollection` 中,应用会在解析的那一刻抛出异常,提示该服务未被注册。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-simplest-setup-with-dependency-injection-53953102/?t=340)

---

## 3. The ServiceCollection

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-servicecollection-53953103/) · 2:00

### 总结

ServiceCollection 是 .NET 依赖注入中的基础组件,它充当服务描述符的容器,定义框架应当如何解析依赖。
它实现了标准的集合接口,用来存放诸如单例生命周期之类的指令,指定某个服务应当通过接口解析,还是作为自注册的类来解析。

### 核心概念

*   **ServiceCollection**:在 .NET 应用中用于注册依赖的初始容器。
*   **ServiceDescriptor**:描述某个服务应当如何被依赖注入框架解析的对象。
*   **Singleton Lifetime(单例生命周期)**:一种注册范围,整个应用生命周期内只创建该类的一个实例。
*   **Interface Mapping(接口映射)**:通过把接口映射到某个具体实现来注册服务。
*   **Self-Registration(自注册)**:把一个具体类注册为解析到它自身,而不是通过接口来解析。

### 课程笔记

`ServiceCollection` 是在 .NET 中配置依赖注入时的主要入口。
它是构建服务提供者之前用于注册服务的初始容器。

```csharp
if (args.Length == 0)
{
    args = new string[1];
    args[0] = "London";
}

var services = new ServiceCollection();

await application.RunAsync(args);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-servicecollection-53953103/?t=10)

在内部,`ServiceCollection` 是一个特殊的列表。
它实现了若干标准的 .NET 集合接口,专门设计用来存放 `ServiceDescriptor` 对象。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-servicecollection-53953103/?t=25)

`ServiceDescriptor` 为依赖注入框架提供了必要的元数据,让它理解某个服务应当如何被解析。
`ServiceCollection` 本质上就是这些指令的一个列表。

在注册服务时,`AddSingleton` 之类的方法被用来定义服务的生命周期和实现。
单例注册确保该类在应用执行期间只存在一个实例,防止框架反复创建新实例。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-servicecollection-53953103/?t=55)

服务既可以通过把接口映射到具体实现来注册(例如把 `IWeatherService` 映射到 `OpenWeatherService`),也可以直接注册一个具体类型(例如 `Application`)。
当一个具体类型在没有接口的情况下被注册时,依赖注入框架会把这个类解析为它自身。

---

## 4. The ServiceProvider

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-serviceprovider-53953104/) · 1:46

### 总结

ServiceProvider 是 .NET 依赖注入系统的引擎,它作为容器负责解析服务并管理其生命周期。
如果说 ServiceCollection 是一份描述服务的注册表或者"食谱书",那么 ServiceProvider 就负责解读这些指令,实例化对象及其依赖。
借助 ServiceProvider,开发者可以避免在应用根部手动构造对象,从而实现自动的依赖解析和更易维护的代码结构。

### 核心概念

- **The Container(容器)**:`ServiceProvider` 才是真正解析服务的 DI 容器。
- **Resolution vs. Description(解析与描述)**:`ServiceCollection` 描述服务;`ServiceProvider` 构建服务。
- **Automatic Resolution(自动解析)**:它省去了手动实例化复杂依赖树的必要。
- **Internal Complexity(内部复杂度)**:它使用并发字典和调用点工厂来管理内部状态,以优化服务的创建。

### 课程笔记

`ServiceProvider` 是真正执行服务解析工作的核心组件。
如果把 `ServiceCollection` 看作一本菜谱或者一份指令清单,那么 `ServiceProvider` 就是按照这些指令做出最终成品的厨师。

要创建一个 `ServiceProvider`,你需要在 `IServiceCollection` 的实例上调用 `BuildServiceProvider()` 方法。
构建完成后,你可以使用 `GetRequiredService<T>()` 之类的方法来取得已注册类的实例。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-serviceprovider-53953104/?t=10)

在内部,`ServiceProvider` 是一个复杂的类。
反编译它可以看到用于管理服务生命周期和性能的精巧机制,包括用来缓存已解析服务和作用域的调用点工厂与并发字典。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-serviceprovider-53953104/?t=40)

使用 `ServiceProvider` 的首要好处是依赖的自动解析。
如果没有 DI 容器,你将不得不在应用的根部手动实例化每一个依赖。
例如,如果 `Application` 类需要一个 `IWeatherService`,你就必须手动创建 `OpenWeatherService` 并把它传进构造函数。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-serviceprovider-53953104/?t=85)

通过使用容器,你把这份职责委托了出去,让系统自动处理复杂的依赖树。
这项能力正是整个 .NET 开发中所使用的各种高级依赖注入模式的基础。

---

## 5. All of the above, in an API

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/all-of-the-above-in-an-api-53953105/) · 5:27

### 总结

本课演示如何在 .NET Web API 中实现依赖注入,并把传统的 Startup.cs 方式与 .NET 6 引入的现代 Program.cs 结构做了对比。
它涵盖了使用 IServiceCollection 进行服务注册、使用扩展方法做逻辑分组,以及框架如何通过构造函数注入自动为 API 控制器解析依赖。

### 核心概念

*   **Transition to Program.cs(迁移到 Program.cs)**:理解 .NET 6+ 如何把 `Startup.cs` 的逻辑整合进单个文件。
*   **Service Registration(服务注册)**:使用 `builder.Services`(一个 `IServiceCollection`)来注册 `IWeatherService` 之类的依赖。
*   **Middleware Pipeline(中间件管道)**:把传统的 `Configure` 方法映射到现代的应用构建流程。
*   **Extension Methods(扩展方法)**:使用 `Add{Feature}` 模式来封装多条服务注册,保持配置整洁。
*   **Automatic Resolution(自动解析)**:Web API 框架如何通过构造函数自动把依赖提供给控制器。

### 课程笔记

在 Web API 场景中,服务逻辑通常被封装在实现了特定接口的实现类里。
例如,一个 `OpenWeatherService` 可能实现 `IWeatherService`,并使用 `IHttpClientFactory` 从外部 API 获取数据。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/all-of-the-above-in-an-api-53953105/?t=25)

#### Legacy vs. Modern Configuration

在 .NET 6 之前,依赖注入是在一个 `Startup.cs` 类中配置的。
这个类把服务注册放在 `ConfigureServices` 方法里,把中间件配置放在 `Configure` 方法里。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/all-of-the-above-in-an-api-53953105/?t=75)

在现代 .NET(Minimal APIs)中,这些部分被直接映射进了 `Program.cs`。
`builder.Services` 属性代表 `IServiceCollection`(即原来的 `ConfigureServices` 区域),而 `builder.Build()` 之后的代码则代表中间件管道(即原来的 `Configure` 区域)。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/all-of-the-above-in-an-api-53953105/?t=295)

#### Extension Methods for Grouping

为了避免配置文件被底层的注册细节淹没,.NET 使用了扩展方法。
`AddEndpointsApiExplorer` 这样的方法封装了多次 `TryAddSingleton` 或 `TryAddEnumerable` 调用。
这让开发者可以把相关的服务归拢到一次描述性的方法调用之下。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/all-of-the-above-in-an-api-53953105/?t=145)

#### Controller Injection

服务一旦注册进容器,就会通过构造函数注入被控制器使用。
框架会在运行时自动解析所需的接口并提供具体实现。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/all-of-the-above-in-an-api-53953105/?t=310)

---

## 6. The different types of dependency lifetimes

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-different-types-of-dependency-lifetimes-53953106/) · 4:30

### 总结

.NET 中的依赖注入支持三种主要的生命周期:Transient、Scoped 和 Singleton。
这些生命周期决定了一个服务实例保持存活多久,以及它何时被释放或复用。
本课通过搭建一个 ID 生成器服务、一个 API 控制器和一个 action filter,来追踪实例在请求生命周期中的持久性,从而演示它们之间的差异。

### 核心概念

* **Transient**:每次从容器中请求该服务时都会创建一个新实例。
* **Scoped**:每个客户端请求(逻辑作用域)创建一个实例,例如一次 HTTP 请求。
* **Singleton**:第一次被请求时创建一个实例,此后在应用的整个生命周期内,每次请求都使用同一个实例。
* **Registration(注册)**:服务通过 `IServiceCollection` 上的 `AddTransient`、`AddScoped` 或 `AddSingleton` 方法注册进 DI 容器。
* **Observation(观察)**:使用 `IActionFilter` 和 `ServiceFilter` 可以让开发者在请求管道的不同阶段检查服务实例。

### 课程笔记

在 .NET 依赖注入中,生命周期定义了一个服务在被释放或复用之前保持存活多久。
三种主要的生命周期是 Transient、Scoped 和 Singleton。
它们在应用启动时的服务注册阶段进行配置。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-different-types-of-dependency-lifetimes-53953106/?t=25)

为了演示这些生命周期在真实应用中的行为,我们创建一个 `IdGenerator` 服务。
这个服务在被实例化时生成一个唯一的 `Guid`(全局唯一标识符),让我们能够追踪 DI 容器给出的是一个新实例还是一个缓存的实例。

```csharp
namespace Weather.Api.Service;

public class IdGenerator
{
    public Guid Id { get; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-different-types-of-dependency-lifetimes-53953106/?t=85)

这个服务被注入到一个 `LifetimeController` 中。
该控制器暴露了一个端点,返回注入进来的 `IdGenerator` 实例当前的 ID。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-different-types-of-dependency-lifetimes-53953106/?t=130)

为了专门观察 `Scoped` 生命周期的行为,我们实现一个 `LifetimeIndicatorFilter`。
这个过滤器实现了 `IActionFilter`,让我们可以在控制器 action 执行前后运行逻辑。
通过把 `IdGenerator` 注入这个过滤器并记录 ID,我们就能看出在一次 HTTP 请求中,过滤器和控制器之间是否共享同一个实例。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-different-types-of-dependency-lifetimes-53953106/?t=220)

这个过滤器必须注册进 DI 容器才能被解析。
在这个场景中,它以 scoped 生命周期注册。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-different-types-of-dependency-lifetimes-53953106/?t=250)

最后,通过 `ServiceFilter` 特性把这个过滤器应用到控制器 action 上。
这个特性告诉 ASP.NET Core 从 DI 容器中解析过滤器实例,从而确保过滤器所需的任何依赖(比如 `IdGenerator`)也能被正确注入。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-different-types-of-dependency-lifetimes-53953106/?t=265)

---

## 7. The Transient lifetime

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-transient-lifetime-53953107/) · 2:08

transient 生命周期是一种依赖注入范围,容器在每次解析被请求的服务时都会创建一个新实例。
这确保了依赖永远不会在不同组件之间共享,甚至在同一次请求中被不同的消费者多次请求时也不会共享。

### 核心概念

- **New Instance per Request(每次请求新实例)**:每次从容器解析该服务时都会创建一个新实例。
- **No State Sharing(不共享状态)**:因为实例对每个消费者都是唯一的,所以状态不会在不同的类或组件之间共享。
- **Registration(注册)**:服务通过 `IServiceCollection` 上的 `AddTransient` 方法注册。
- **Statelessness(无状态)**:最适合轻量、无状态的服务。

### 课程笔记

在 .NET 中,transient 服务是指每次被容器需要时都会被实例化的服务。
无论该服务是被控制器、过滤器还是其他任何组件请求,依赖注入系统都会为每一次具体的请求提供一个全新的实例。

为了说明这一点,来看一个 `IdGenerator` 类。
这个类在初始化时生成一个新的 `Guid`。
因为这个 ID 是在构造期间作为默认值设置的,所以它在该具体实例的生命周期内保持不变。
如果 ID 在两个访问点之间发生了变化,就说明正在使用的是这个类的两个不同实例。

```csharp
namespace Weather.Api.Service;

public class IdGenerator
{
    public Guid Id { get; } = Guid.NewGuid();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-transient-lifetime-53953107/?t=55)

要在应用中使用这个类,必须在 `Program.cs` 文件中用 `AddTransient` 方法注册它。
这告诉 .NET 容器,每次有人请求 `IdGenerator` 时都创建一个新的。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-transient-lifetime-53953107/?t=40)

当应用运行并有请求打到某个端点时,容器会解析各项依赖。
如果我们有一个需要 `IdGenerator` 的 `LifetimeController`,它会收到一个新实例。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-transient-lifetime-53953107/?t=10)

如果同一次请求还用到了像 `LifetimeIndicatorFilter` 这样的过滤器,而该过滤器同样需要一个 `IdGenerator`,那么容器会为这个过滤器创建第二个、不同的实例。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-transient-lifetime-53953107/?t=115)

在这个场景中,`LifetimeIndicatorFilter` 被注册为 scoped 服务,意味着这个过滤器实例本身会在整个 HTTP 请求期间被复用。
因此,注入到该过滤器构造函数中的 `IdGenerator` 实例在 `OnActionExecuting` 和 `OnActionExecuted` 两处也是同一个,结果就是同一个 ID 被记录了两次。
然而,因为 `IdGenerator` 是 transient 的,过滤器里的实例与 `LifetimeController` 里的实例完全无关,它们的 ID 值会不同。

---

## 8. The Singleton lifetime

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-singleton-lifetime-53953108/) · 2:08

Singleton 生命周期是依赖注入框架中的一种基础模式,一个类的单个实例会在应用执行的整个期间被保持。
当一个服务被注册为 Singleton 时,DI 容器只会实例化它一次,并把这同一个实例提供给每一个需要它的组件。

### 核心概念

- **Single Instance(单一实例)**:在应用的整个生命周期内该服务只存在一个实例。
- **Shared State(共享状态)**:同一个实例在所有请求和所有消费组件之间共享。
- **Performance(性能)**:通过避免重复实例化来减少开销,并降低垃圾回收压力。
- **Thread Safety(线程安全)**:当服务包含可变状态时需要格外小心,因为它会被多个线程同时访问。
- **Registration(注册)**:在服务集合中使用 `AddSingleton` 方法来定义。

### 课程笔记

在一个典型的实现中,控制器可能依赖 `IdGenerator` 这样的服务。
当使用 Singleton 生命周期时,这个生成器会在所有消费者之间共享。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-singleton-lifetime-53953108/?t=10)

要把一个服务配置为 Singleton 生命周期,在 `Program.cs` 文件的服务配置阶段使用 `AddSingleton<T>` 方法。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-singleton-lifetime-53953108/?t=55)

当应用运行时,同一个 `IdGenerator` 实例会被反复使用。
这意味着如果 `IdGenerator` 同时被注入到 `LifetimeController` 和 `LifetimeIndicatorFilter` 中,两者拿到的会是完全相同的实例。
更进一步,后续对该端点的 HTTP 请求也会继续使用最初那个实例。
这一点可以在应用日志中观察到:生成的 ID 在不同的执行上下文和不同的请求之间保持不变。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-singleton-lifetime-53953108/?t=70)

#### Performance and State

使用 Singleton 可以提升应用性能。
因为实例不会在每次请求时被重新创建,应用节省了处理能力和内存。
它还减轻了垃圾回收器的工作量,因为对象不会被不断地创建和丢弃。

不过,如果 Singleton 持有状态,就应当谨慎使用。
由于该实例在整个应用范围内共享,任何对其内部状态的修改都必须是线程安全的。
如果一个服务既不共享也不修改状态,那它就是 Singleton 生命周期的理想候选者,可以最大化效率。

---

## 9. The Scoped lifetime

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-scoped-lifetime-53953109/) · 2:31

### 总结

.NET 依赖注入中的 Scoped 生命周期确保一个服务实例在每个作用域内只创建一次。
在 ASP.NET Core 中,这个作用域通常对应一次 HTTP 请求的持续时间,从请求到达 Web 服务器的那一刻起,直到响应被发出为止。
这使得同一次请求中的多个组件(比如控制器、过滤器和中间件)可以共享同一个服务实例,同时确保不同的请求拿到各自独立的实例。
这种生命周期非常适合那些需要在一次请求中维持状态的服务,或者那些在不同并发请求之间不是线程安全、但在一次请求内可以安全复用的服务。

### 核心概念

- **Scope Definition(作用域定义)**:在 Web 应用中,每一个进来的 HTTP 请求都会自动创建一个作用域,并在响应发出时被释放。
- **Instance Sharing(实例共享)**:在单个作用域内,DI 容器总是返回同一个 scoped 服务实例。
- **Isolation(隔离)**:每个请求都有自己的作用域,意味着服务在不同用户和不同请求之间是隔离的。
- **Registration(注册)**:服务通过 `AddScoped<TService, TImplementation>()` 方法注册。
- **Efficiency(效率)**:在一次请求内复用实例比每次都新建(Transient)更高效,同时又比全局共享一个实例(Singleton)更安全。

### 课程笔记

Scoped 生命周期介于 Singleton 和 Transient 之间。
Singleton 存活于整个应用生命周期,Transient 服务每次被请求时都会创建,而 Scoped 服务则在每个特定作用域内创建一次。

在 ASP.NET Core 中,框架把作用域定义为一次 HTTP 请求的持续时间。
从请求到达 Web 服务器的那一刻起,直到响应返回为止,任何标记为 Scoped 的服务都会存活并被复用。
为了演示这种差异,先来看一个注册为 Singleton 的服务:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-scoped-lifetime-53953109/?t=10)

在 Singleton 注册下,每次调用该端点都会在所有请求中返回相同的标识符:

```json
"1c48883f-e85d-4ed8-b9a8-e08fecc6aa66"
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-scoped-lifetime-53953109/?t=25)

但当注册改为 `AddScoped` 后,每一个新请求都会得到一个新实例,因此也会得到一个新的 ID:

```json
"9fe14f48-9687-465f-97fd-47343e16faf3"
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-scoped-lifetime-53953109/?t=40)

Scoped 生命周期的威力,在同一请求管道中的多个组件需要与同一个服务交互时体现得最明显。
例如,如果 `IdGenerator` 注册为 Scoped,那么 `LifetimeIndicatorFilter` 和 `LifetimeController` 就可以使用同一个 `IdGenerator` 实例。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-scoped-lifetime-53953109/?t=70)

当应用以 scoped 服务运行时,日志显示:在单次请求内,ID 在不同执行阶段(例如 `OnActionExecuting` 和 `OnActionExecuted`)保持一致,但在发起第二次请求时就会改变:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-scoped-lifetime-53953109/?t=85)

控制器通过它的构造函数接收 scoped 服务。
因为控制器是在请求作用域内被解析的,它会与该请求中其他 scoped 组件共享同样的服务实例。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-scoped-lifetime-53953109/?t=115)

这套机制既让服务解析变得高效,也带来了在请求开始时收集状态、并在请求结束时复用这些状态的能力。
虽然 ASP.NET Core 默认为每个请求提供一个作用域,但在需要时也可以手动定义自定义作用域,这在没有内置请求作用域的控制台应用中尤其有用。

---

## 10. GetService vs GetRequiredService

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/getservice-vs-getrequiredservice-53953110/) · 3:38

### 总结

本课探讨在 .NET 中从 IServiceProvider 解析服务的两个主要方法:GetService 和 GetRequiredService。
它详细说明了当请求的服务不在容器中时两者不同的行为,具体来说就是 GetService 返回 null 而 GetRequiredService 抛出异常,并给出了在大多数应用场景下为何应优先选择 GetRequiredService 的指导,以获得快速失败的行为和更清晰的调试体验。

### 核心概念

* **GetService<T>**:返回服务实例,如果该服务未在容器中注册则返回 null。
* **GetRequiredService<T>**:返回服务实例,如果该服务未注册则抛出 InvalidOperationException。
* **Generic vs. Non-Generic(泛型与非泛型)**:泛型方法优于非泛型版本,因为它们直接返回具体类型,免去了从 object 手动强制转换的麻烦。
* **Fail-Fast Principle(快速失败原则)**:GetRequiredService 是推荐的默认选择,因为它会在解析的那一刻立即暴露配置错误。
* **Framework Defaults(框架默认行为)**:ASP.NET Core 在内部对控制器和其他框架组件的构造函数注入使用的就是 required-service 逻辑。

### 课程笔记

在 .NET 依赖注入中,有两种主要的方式可以从 `IServiceProvider` 解析服务。
虽然你可以使用需要传入 `typeof` 参数并手动强制转换的非泛型方法,但更推荐使用泛型扩展方法,因为它们直接返回具体类型。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/getservice-vs-getrequiredservice-53953110/?t=10)

`GetService` 和 `GetRequiredService` 之间的根本区别在于它们如何处理缺失的注册。
如果找不到服务,`GetService` 返回 `null`,而 `GetRequiredService` 则抛出异常。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/getservice-vs-getrequiredservice-53953110/?t=25)

如果漏掉了某个服务注册,`GetService` 会返回 `null`,这可能导致后续应用逻辑中出现 `NullReferenceException`。
`GetRequiredService` 则提供了"快速失败"的机制,立即抛出 `InvalidOperationException`,指出没有为指定类型注册过服务。
这让开发阶段识别和修复配置问题变得容易得多。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/getservice-vs-getrequiredservice-53953110/?t=70)

在大多数情况下,`GetRequiredService` 是更好的选择,因为当你请求一个服务时,你的意图通常就是它必须存在。
ASP.NET Core 在内部用于控制器激活之类的工作时用的也是这套逻辑。
如果某个控制器依赖了没有注册进容器的服务,框架就会抛出异常,因为它需要这个依赖才能正常工作。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/getservice-vs-getrequiredservice-53953110/?t=190)

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/getservice-vs-getrequiredservice-53953110/?t=205)

---

## 11. Generic-based registration vs implementation-based registration

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/generic-based-registration-vs-implementation-based-registration-53953111/) · 3:31

### 总结

本课探讨 .NET 依赖注入(DI)容器中自动的泛型注册与手动的实现注册之间的区别。
它演示了 DI 框架如何通过图遍历自动解析构造函数依赖,并解释了注册缺失会带来的后果,也就是启动失败。
本课还涵盖了其他注册模式,比如在自动解析不可行的复杂场景中使用工厂 lambda 和直接实例化。

### 核心概念

* **Automatic Resolution(自动解析)**:使用泛型注册时,DI 容器自动解析构造函数依赖的能力。
* **Extension Methods(扩展方法)**:使用 `AddHttpClient` 之类的方法来注册复杂的内部服务及其依赖。
* **Startup Failures(启动失败)**:如果某个已注册服务所需的依赖缺失,应用会在 `Build()` 阶段失败。
* **Factory Lambdas(工厂 lambda)**:使用 `IServiceProvider` 进行手动注册,以控制服务如何被实例化。
* **Instance Registration(实例注册)**:直接注册一个已经构造好的对象,通常用于单例。
* **Graph Traversal(图遍历)**:级联效应,DI 容器解析一个服务时会先解析它的所有依赖,以及这些依赖的依赖,层层递归。

### 课程笔记

在标准的 .NET 开发中,服务通常使用泛型方法来注册。
这种方式假定服务构造函数所需的任何依赖都已经注册,并且能被 DI 框架解析出来。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/generic-based-registration-vs-implementation-based-registration-53953111/?t=10)

例如,`OpenWeatherService` 这个实现在构造函数中需要一个 `IHttpClientFactory`。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/generic-based-registration-vs-implementation-based-registration-53953111/?t=25)

虽然 `IHttpClientFactory` 并没有在主 `Program.cs` 文件里显式注册,但它是通过 `AddHttpClient` 扩展方法加进来的。
反编译这个方法可以看到,它注册了若干内部服务,包括 `DefaultHttpClientFactory` 以及 `IHttpClientFactory` 本身。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/generic-based-registration-vs-implementation-based-registration-53953111/?t=40)

如果某个必需的依赖缺失,比如把 `AddHttpClient` 注释掉,应用就会在启动时失败。
DI 容器会在 `builder.Build()` 阶段做一次检查,如果它无法满足某个已注册服务的构造函数要求,就会抛出异常。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/generic-based-registration-vs-implementation-based-registration-53953111/?t=80)

由此产生的错误表明,框架无法为该实现找到所需的服务。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/generic-based-registration-vs-implementation-based-registration-53953111/?t=65)

为了应对自动解析不可行的场景(例如实现是 internal 的,或者需要特定的手动设置),你可以使用基于实现的注册。
这种方式是提供一个工厂 lambda 来手动实例化该服务。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/generic-based-registration-vs-implementation-based-registration-53953111/?t=115)

另外,对于单例,你可以直接注册该类的一个特定实例。
既然它是单例,容器每次都会返回同一个实例,而不需要执行 lambda。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/generic-based-registration-vs-implementation-based-registration-53953111/?t=160)

只要服务构造函数中的每一个依赖都已经存在于 `IServiceCollection` 中,泛型注册就能正常工作。
DI 框架会执行一次图遍历:如果服务 A 依赖服务 B,而服务 B 又依赖服务 C,只要所有组件都已注册,框架就会把整条链路解析出来。
如果这条链中缺了任何一环,DI 容器就会在初始化期间抛出异常。

---

## 12. Registration approaches

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registration-approaches-53953112/) · 6:04

### 总结

本课探讨在 .NET 依赖注入(DI)容器中注册服务的各种方式,从自动的泛型注册到手动的工厂式注册。
它演示了在需要自定义初始化逻辑时如何使用 IServiceProvider 手动解析依赖,并强调了 Transient、Scoped 和 Singleton 各自的具体要求,包括为 Singleton 注册直接实例的能力。

### 核心概念

- **Automatic Registration(自动注册)**:使用 `AddScoped<T>()` 之类的泛型方法,由 DI 容器自动解析构造函数依赖。
- **Factory Registration(工厂注册)**:使用 lambda 表达式 `(provider => ...)` 手动实例化服务,从而获得对 `IServiceProvider` 的访问。
- **Manual Dependency Resolution(手动依赖解析)**:在工厂中使用 `provider.GetRequiredService<T>()` 为构造函数解析具体依赖。
- **Singleton Instance Registration(单例实例注册)**:可以把一个已存在的类实例注册为 Singleton。
- **Lifetime Constraints(生命周期约束)**:理解为什么 Transient 和 Scoped 注册需要的是工厂逻辑或类型,而不是直接的实例,才能正确管理对象生命周期。

### 课程笔记

在 .NET 中最常见的服务注册方式是泛型注册。
只要类构造函数中的所有依赖也都注册在容器里,这种方式就能让 DI 容器自动处理服务解析的级联效应。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registration-approaches-53953112/?t=10)

#### Manual Factory Registration

虽然自动注册是标准做法,但你也可以用工厂模式手动达成同样的效果。
对于像 `IdGenerator` 这样没有构造函数依赖的类,下面这两种注册在功能上是完全等价的:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registration-approaches-53953112/?t=40)

当一个类有依赖时,手动注册会变得更复杂。
例如,`LifetimeIndicatorFilter` 需要一个 `IdGenerator` 和一个 `ILogger`:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registration-approaches-53953112/?t=85)

要手动注册它,你需要用到传入 lambda 的 `IServiceProvider`(通常命名为 `provider`)。
这个 provider 让你可以调用 `GetRequiredService<T>()`,在把依赖传进被注册服务的构造函数之前先把它们解析出来。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registration-approaches-53953112/?t=175)

#### Lifetime-Specific Registration Rules

不同的生命周期在实例注册方面有不同的规则:

1.  **Singleton**:因为整个应用生命周期内只存在一个实例,你可以直接注册一个已经实例化好的对象。
每次请求该服务时,容器都会返回这个特定的实例。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registration-approaches-53953112/?t=265)

2.  **Transient and Scoped**:这两种生命周期不允许直接注册实例(例如 `AddTransient(new IdGenerator())`)。
这是因为容器必须有能力创建新实例,要么在每次请求该服务时创建(Transient),要么每个作用域创建一次(Scoped)。
要手动注册它们,你必须提供一个工厂函数,好让容器知道在需要时如何实例化这个对象。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registration-approaches-53953112/?t=310)

手动注册并不常见,通常只保留给那些需要"花式"初始化,或者必须在服务注册前执行某些逻辑的场景。
在大多数情况下,泛型方式因其简洁和可维护性而更受青睐。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registration-approaches-53953112/?t=355)

---

## 13. The Startup.cs and changes after .NET 6

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-startupcs-and-changes-after-net-6-53953113/) · 2:09

### 总结

本课讲解从传统的 Startup.cs 文件到 .NET 6 引入的整合式 Program.cs 模型的转变。
它详细说明了 ConfigureServices 和 Configure 方法如何映射到 WebApplicationBuilder 和 WebApplication 实例上,为在现代 .NET 应用中实现依赖注入和中间件提供了一条清晰的路径。

### 核心概念

* 把传统的 ConfigureServices 方法映射到 Program.cs 中的 builder.Services 属性。
* 把传统的 Configure 方法映射到 builder.Build() 之后的中间件管道配置区域。
* 理解 WebApplicationBuilder 从初始化到应用具现化的生命周期。
* 在 Startup.cs 方式与 .NET 6+ Minimal Hosting 模型之间保持功能上的一致。

### 课程笔记

在 .NET 6 及后续版本中,默认项目模板不再包含 Startup.cs 文件,转而采用基于 Minimal Hosting 模型的整合式 Program.cs。
这种方式使用 WebApplication.CreateBuilder(args) 来初始化应用及其服务容器。

以前位于 Startup.cs 的 ConfigureServices 方法中的逻辑,现在位于创建 builder 与调用 builder.Build() 之间。
在这一段中,你通过 builder.Services 属性把依赖注册进 IoC 容器。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-startupcs-and-changes-after-net-6-53953113/?t=10)

在传统的 Startup.cs 模型中,同样的配置会在一个直接接收 IServiceCollection 的 ConfigureServices 方法内完成。
主要区别仅仅在于代码所处的位置,以及用来访问服务集合的对象。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-startupcs-and-changes-after-net-6-53953113/?t=70)

以前在 Startup.cs 的 Configure 方法中定义的中间件管道,现在则是在 builder 通过 builder.Build() 把应用具现化之后,使用 app 实例(类型为 WebApplication)来配置。
这一段在 app.Run() 被调用时结束。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-startupcs-and-changes-after-net-6-53953113/?t=85)

虽然映射端点的语法在 .NET 6 中略有变化(改为在 app 对象上做顶层映射),但底层行为保持不变。
仍在使用旧式 Program.cs 与 Startup.cs 拆分的开发者不需要迁移现有项目,因为两种模式都得到完整支持,只不过 .NET 6 的方式是当前的标准。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-startupcs-and-changes-after-net-6-53953113/?t=115)

---

## 14. Third party libraries

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/third-party-libraries-53953114/) · 1:49

### 总结

本课评估了在 .NET 开发中使用第三方依赖注入(DI)容器的必要性。
在承认 Autofac、Lamar 和 SimpleInjector 等流行替代方案的同时,它强调内置的 Microsoft DI 框架对绝大多数应用来说通常已经足够。
原生容器提供了更好的性能,由 Microsoft 持续优化,并且可以借助 Scrutor 这类专门的库来增强,从而在不切换到完全不同框架的前提下获得高级特性。

### 核心概念

* **Third-party IoC Containers(第三方 IoC 容器)**:流行的替代方案包括 Autofac、Lamar(StructureMap 的继任者)和 SimpleInjector。
* **Performance(性能)**:内置的 Microsoft DI 容器通常比第三方替代方案更快,并且每个 .NET 版本都会带来性能提升。
* **Extensibility(可扩展性)**:Scrutor 这样的库可以为内置容器添加程序集扫描之类的高级特性,而不必替换它。
* **Standardization(标准化)**:坚持使用内置框架可以确保长期支持和兼容性,因为 Microsoft 会持续维护和优化这个库。

### 课程笔记

虽然 .NET 生态中有若干成熟的第三方依赖注入和控制反转(IoC)容器,但现代开发的主要关注点仍是内置的 Microsoft 框架。
通过 NuGet 可以获得的常见第三方选项包括:

* **Autofac**:一个被广泛使用、功能丰富的 IoC 容器。
* **Lamar**:StructureMap 的继任者,设计目标是成为一个快速而现代的替代方案。
* **SimpleInjector**:以高级特性和严格校验著称的容器。

.NET 应用中的标准做法是在应用启动阶段使用 `IServiceCollection` 来注册依赖。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/third-party-libraries-53953114/?t=10)

尽管有这些第三方工具可用,内置框架在大多数场景下仍是推荐选择。
它经过高度优化,性能往往优于第三方容器。
因为内置容器由 Microsoft 维护,它会随着每个 .NET 大版本获得显著的性能改进(例如从 .NET 5 到 .NET 6 的演进)。

对于需要基础框架中没有的高级特性的开发者,可以引入 **Scrutor** 之类的库。
Scrutor 构建在现有的 Microsoft DI 框架之上而不是取代它,在提供程序集扫描和装饰等能力的同时,保留了原生容器的性能优势。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/third-party-libraries-53953114/?t=55)

坚持使用内置提供者,开发者可以确保应用保持轻量,并持续受益于 .NET 团队带来的优化。

---

## 15. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953115/) · 1:09

### 总结

本课对 .NET 中依赖注入(DI)的基础做了一次全面回顾,涵盖从基本的控制台应用配置到在 ASP.NET Core API 中的集成。
它复习了 ServiceCollection 与 ServiceProvider 的关键角色、Transient、Scoped 和 Singleton 在行为上的差异,以及 GetService 与 GetRequiredService 之间的技术区别。
此外,本课还谈到了服务注册从传统 Startup.cs 模型到现代 .NET 6+ 模式的演进,并评估了第三方 IoC 容器的必要性。

### 核心概念

- **ServiceCollection vs. ServiceProvider**:用于注册的容器与用于解析的引擎之间的区别。
- **Service Lifetimes(服务生命周期)**:Transient、Scoped 和 Singleton 服务的行为与存活时长。
- **GetService vs. GetRequiredService**:从容器解析服务时正确的错误处理与可空性处理。
- **Framework Evolution(框架演进)**:从传统的 `Startup.cs` 模型到现代 .NET 6+ `Program.cs` 结构的转变。
- **Built-in DI Framework(内置 DI 框架)**:原生 .NET DI 实现相比第三方方案的效率与充分性。

### 课程笔记

.NET 中依赖注入的实现围绕两个主要组件展开:`IServiceCollection` 和 `IServiceProvider`。
`IServiceCollection` 在应用启动阶段用于注册服务并定义它们的生命周期。
应用构建完成后,`IServiceProvider` 用于解析这些已注册的服务。

在现代 ASP.NET Core 应用中,这套配置通常在 `Program.cs` 文件中完成。
`WebApplicationBuilder` 暴露出 `Services` 属性用于注册,随后应用才被构建成一个 `WebApplication` 实例。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953115/?t=10)

#### Service Lifetimes

DI 的一个核心方面是管理对象存活多久。
.NET 提供三种主要的生命周期:

- **Transient**:每次从容器请求该服务时都会创建一个新实例。
- **Scoped**:每个作用域创建一个实例(在 Web 应用中通常是一次 HTTP 请求)。
- **Singleton**:第一次被请求时创建一个实例,并在应用的整个生命周期内持续存在。

#### Service Resolution

一个常见的困惑点是在 `GetService` 与 `GetRequiredService` 之间做选择。
如果服务没有被注册,`GetService` 会返回 `null`,这需要手动做空值检查。
相比之下,如果服务缺失,`GetRequiredService` 会抛出 `InvalidOperationException`。
对于应用执行所必需的依赖,通常更推荐使用 `GetRequiredService`,以确保失败尽早且显式地发生。

#### Framework Evolution

本课还强调了 .NET 6 引入的架构性转变。
以前,服务注册是在一个独立的 `Startup.cs` 文件的 `ConfigureServices` 方法中处理的,而中间件管道则在 `Configure` 方法中配置。
现代 .NET 把这两部分整合进 `Program.cs` 中的单一流程,不过服务集合底层的逻辑保持不变。

最后,虽然也有第三方的控制反转(IoC)容器和 DI 库可供选择(比如 Autofac 或 Ninject),但内置的 .NET DI 框架对绝大多数软件需求来说都足够高效且功能完整,这往往使得外部库变得没有必要。
