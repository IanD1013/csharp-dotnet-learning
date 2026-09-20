# Deep dive

> 课程:[From Zero to Hero: Dependency Injection in .NET with C#](https://dometrain.com/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp/) · 第 5 章
> 共 11 课 · 约 43:36
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

---

## 课程索引

| #   | 课程                                                                                                                                                                                                              | 时长 | 小节                                                   |
| --- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---- | ------------------------------------------------------ |
| 1   | [Do you need an interface for everything?](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/do-you-need-an-interface-for-everything-53953190/)              | 4:59 | [↓](#1-do-you-need-an-interface-for-everything)        |
| 2   | [Choosing the right dependency lifetime](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/choosing-the-right-dependency-lifetime-53953191/)                 | 2:31 | [↓](#2-choosing-the-right-dependency-lifetime)         |
| 3   | [Dependency resolving issues and limitations](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-resolving-issues-and-limitations-53953192/)       | 7:43 | [↓](#3-dependency-resolving-issues-and-limitations)    |
| 4   | [Registering open generics](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-open-generics-53953193/)                                           | 2:38 | [↓](#4-registering-open-generics)                      |
| 5   | [Registering multiple interface implementations](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-multiple-interface-implementations-53953194/) | 3:22 | [↓](#5-registering-multiple-interface-implementations) |
| 6   | [The ServiceDescriptor](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-servicedescriptor-53953195/)                                                   | 4:22 | [↓](#6-the-servicedescriptor)                          |
| 7   | [Add vs TryAdd](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/add-vs-tryadd-53953196/)                                                                   | 4:22 | [↓](#7-add-vs-tryadd)                                  |
| 8   | [TryAddEnumerable](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/tryaddenumerable-53953197/)                                                             | 3:31 | [↓](#8-tryaddenumerable)                               |
| 9   | [Replacing dependencies](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/replacing-dependencies-53953199/)                                                 | 5:47 | [↓](#9-replacing-dependencies)                         |
| 10  | [Cleaning up service registration](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/cleaning-up-service-registration-53953200/)                             | 3:22 | [↓](#10-cleaning-up-service-registration)              |
| 11  | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953201/)                                                                   | 0:59 | [↓](#11-section-recap)                                 |

---

## 1. Do you need an interface for everything?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/do-you-need-an-interface-for-everything-53953190/) · 4:59

### 总结

本课针对一个常见误解展开讨论:认为 .NET 应用中每个类都需要一个接口,并且都必须注册到依赖注入容器中。
接口对可测试性和可替换性固然至关重要,但当它被教条地套用在并不需要抽象的组件上(比如简单的 mapper)时,就会导致过度设计。
通过对比静态扩展方法与注入式 mapper 服务两种做法,本课说明 mapper 理想情况下应当保持为纯粹的属性到属性的转换器。
把服务注入到 mapper 中被认定为一种不良实践,它会隐藏业务逻辑并使测试复杂化,这表明开发者应当在实用主义和僵化的架构规则之间选择前者。

### 核心概念

- **Purpose of DI(依赖注入的目的)**:依赖注入的主要用途是提升组件的可测试性和可替换性。
- **Pragmatism vs. Dogmatism(实用主义 vs. 教条主义)**:并非每个类都需要接口;只有那些确实需要抽象或替换的依赖才值得注入。
- **Mapper Responsibilities(Mapper 的职责)**:Mapper 应当只专注于属性到属性的映射。不应该为了执行业务逻辑而往里注入服务。
- **Static Extensions(静态扩展方法)**:对于简单、不含逻辑的转换,静态扩展方法通常比注入式服务更高效、更易维护。

### 课程笔记

在 .NET 开发中,一个常见的争议点是:是否每个组件都需要一个接口。
前面的课程里大量使用接口来演示依赖注入(DI),但并不是所有东西都需要可注入。
DI 的主要好处在于能够替换依赖或在测试中对其进行 mock。
如果一个组件按定义就不需要被替换,那么把它包在接口里可能是多余的。

考虑 Weather API 中一个典型的映射场景。
应用内部使用 `WeatherModel`,但对 API 使用方返回的是 `WeatherResponse` 契约,以便维护版本并避免破坏性变更。
这个映射可以用一个静态扩展方法高效完成:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/do-you-need-an-interface-for-everything-53953190/?t=50)

在控制器中,这个扩展方法直接在 weather service 返回的 model 上调用:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/do-you-need-an-interface-for-everything-53953190/?t=65)

经常能看到开发者试图把这件事"标准化",做法是创建一个 `IMapper` 接口和一个具体的 `Mapper` 类,然后把它注册成 singleton:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/do-you-need-an-interface-for-everything-53953190/?t=145)

这种做法需要把服务注册到 DI 容器,并把它注入到控制器的构造函数中:

```csharp
builder.Services.AddSingleton<IMapper, Mapper>();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/do-you-need-an-interface-for-everything-53953190/?t=160)

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/do-you-need-an-interface-for-everything-53953190/?t=190)

虽然有人认为这是"好实践",但它往往源自另一个更早的坏实践:在 mapper 里包含了复杂逻辑。
如果一个 mapper 需要注入其他服务才能工作,那它多半是承担了过多职责。
Mapper 的唯一职责应该是用传入的参数组装对象,而不是去查询其他服务获取信息。
把业务逻辑藏在 mapper 内部是对职责划分的破坏。

对映射这类简单逻辑过度使用接口,会让测试更困难,也会让跨边界的错误有机可乘。
开发者应当保持实用主义:如果一个组件天生难以测试,或者需要可替换性,那就使用接口。
如果它只是一个简单的工具方法或转换,那么接口很可能是不必要的。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/do-you-need-an-interface-for-everything-53953190/?t=280)

---

## 2. Choosing the right dependency lifetime

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/choosing-the-right-dependency-lifetime-53953191/) · 2:31

### 总结

选择合适的依赖生命周期,需要在性能、内存占用和线程安全之间取得平衡。
Singleton 通过减少垃圾回收开销,对无状态服务而言效率极高;而 Transient 则为有状态或对线程敏感的逻辑提供了更安全的方案,代价是更高的资源消耗。

### 核心概念

- **Singleton**:只创建一次的单个实例,在整个应用生命周期内共享。
- **Transient**:每次从容器请求该服务时都会创建一个新实例。
- **Scoped**:在特定作用域内共享的单个实例,例如 ASP.NET Core 中的一次 HTTP 请求。
- **Statelessness(无状态)**:没有状态的服务是注册为 Singleton 的理想候选,可以优化内存和 GC 性能。
- **Thread Safety(线程安全)**:对 Singleton 来说这是关键考量,因为它会被多个线程同时访问。

### 课程笔记

在 .NET 依赖注入中,服务会以三种生命周期之一进行注册:Singleton、Transient 或 Scoped。
Singleton 在整个应用生命周期内维持单个实例。
Transient 服务每次被解析时都会创建一个新实例。
Scoped 服务在每个作用域内维持一个实例,在 ASP.NET Core 中这通常对应一次 HTTP 请求。

在这些生命周期之间的选择,取决于服务预期的行为和资源需求。
如果一个服务是无状态的且没有副作用,把它注册为 Singleton 是最理想的。
这种做法避免了重复分配从而节省内存,也减轻了垃圾回收器的负担,通过减少对象释放和回收所花的时间来提升整体应用性能。

如果一个服务需要在某个特定操作或请求内保持一致性,同时又不在不同请求之间产生副作用,那么 Scoped 生命周期是合适的。
不过,是否选择 Scoped 生命周期往往取决于该服务自身依赖的生命周期,这个话题会在后续课程中进一步展开。

一般来说,Transient 注册是最安全的选项,因为它避免了共享状态问题,但它在性能和内存方面代价最高。
Singleton 由于潜在的线程安全问题而不那么安全,但如果实现得当,效率会非常高。
开发者应当进行充分的测试和性能剖析,确保所选的生命周期与应用的逻辑和并发需求相匹配,使用 Singleton 时尤其如此,因为状态冲突会导致 bug。

---

## 3. Dependency resolving issues and limitations

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-resolving-issues-and-limitations-53953192/) · 7:43

### 总结

本课探讨在 .NET 依赖注入中解析不同生命周期的服务时,存在的关键限制和常见陷阱。
内容涵盖"captive dependency(俘获依赖)"问题,也就是较短生命周期的服务被提升为较长生命周期;涵盖把 scoped 服务注入 singleton 的严格禁令;还涵盖由循环依赖导致的解析失败。

### 核心概念

- **Captive Dependencies(俘获依赖)**:较短生命周期的服务(Transient)在被注入 Singleton 后,实际上变成了长生命周期。
- **Scope Validation(作用域校验)**:框架会阻止把 Scoped 服务注入 Singleton,以避免内存泄漏和逻辑错误。
- **Circular Dependencies(循环依赖)**:两个或多个服务互相依赖的场景,会导致 DI 容器无法构建对象图。
- **Lifetime Cascading(生命周期级联)**:理解依赖链中"最高"的生命周期如何决定它所消费的依赖的行为。

### 课程笔记

为了理解生命周期之间如何相互作用、限制在哪里,我们定义三个服务,分别代表一种生命周期:Transient、Scoped 和 Singleton。
每个服务在实例化时都会生成一个唯一的 `Guid`。

```csharp
namespace Limitations.Api.Services;

public class TransientService
{
    public Guid Id { get; } = Guid.NewGuid();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-resolving-issues-and-limitations-53953192/?t=25)

```csharp
namespace Limitations.Api.Services;

public class ScopedService
{
    public Guid Id { get; } = Guid.NewGuid();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-resolving-issues-and-limitations-53953192/?t=85)

这些服务以各自的生命周期注册到 DI 容器中:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-resolving-issues-and-limitations-53953192/?t=55)

在一个标准的控制器中(它在 ASP.NET Core 里具有 scoped 生命周期),我们可以注入全部三个服务。
在这种情况下,Singleton 的 ID 在所有请求之间保持不变,Scoped 的 ID 在单次请求内保持不变但在请求之间会变化,而 Transient 的 ID 每次解析都会变化。

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

`get_lesson` 返回的课程文档在此处被截断。
以下是通过 `search_code` 取得的、屏幕上显示的剩余代码:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-resolving-issues-and-limitations-53953192/?t=110)

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-resolving-issues-and-limitations-53953192/?t=125)

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-resolving-issues-and-limitations-53953192/?t=145)

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-resolving-issues-and-limitations-53953192/?t=160)

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-resolving-issues-and-limitations-53953192/?t=190)

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-resolving-issues-and-limitations-53953192/?t=205)

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-resolving-issues-and-limitations-53953192/?t=235)

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-resolving-issues-and-limitations-53953192/?t=280)

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-resolving-issues-and-limitations-53953192/?t=310)

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-resolving-issues-and-limitations-53953192/?t=340)

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-resolving-issues-and-limitations-53953192/?t=370)

---

## 4. Registering open generics

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-open-generics-53953193/) · 2:38

### 总结

在 .NET 依赖注入(DI)容器中注册开放泛型(open generics),可以在注册时无需显式定义每一种可能的类型参数,就能解析泛型类型。
这在实现 Logger Adapter 这类模式时尤其有用:它包装了标准的 ILogger,通过避开难以 mock 的内部组件和扩展方法来提升单元测试能力。
借助 typeof 运算符配合开放泛型语法,开发者可以把一个开放接口映射到一个开放实现,让 DI 容器在运行时动态创建对应的封闭泛型类型。

### 核心概念

- **Testability Challenges(可测试性挑战)**:默认的 .NET `ILogger<T>` 很难做单元测试,因为它严重依赖内部组件和扩展方法。
- **Logger Adapter Pattern(日志适配器模式)**:为日志创建一个包装接口和类,可以让 mock 和代理日志调用变得更容易。
- **Open Generics(开放泛型)**:尚未指定类型参数的泛型类型(例如 `ILoggerAdapter<T>`)。
- **Typeof Operator(typeof 运算符)**:在 .NET 中把开放泛型类型传入 DI 注册方法的机制。
- **Runtime Resolution(运行时解析)**:DI 容器能够从一个开放泛型注册中解析出具体的封闭泛型(例如 `ILoggerAdapter<WeatherForecastController>`)。

### 课程笔记

出于可测试性的考虑,常见做法是使用日志适配器,而不是直接注入 `ILogger<T>`。
标准的 .NET logger 很难做单元测试,因为它用到了内部组件和扩展方法。
使用适配器后,你可以注入一个 private readonly 的接口,通过代理来调用方法,从而让代码具备可单元测试性。

首先,定义适配器的开放泛型接口:

```csharp
namespace Weather.Api.Logging;

public interface ILoggerAdapter<TType>
{
    void LogInformation(string template, params object[] args);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-open-generics-53953193/?t=10)

接下来实现这个适配器。
实现类包装了标准的 `ILogger`,并把泛型类型透传进去:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-open-generics-53953193/?t=25)

难点出现在把这些类型注册到 DI 容器的时候。
你不能对开放泛型使用标准的泛型注册语法,因为注册时并没有指定类型参数 `T`。
下面这种写法不是合法的 C#:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-open-generics-53953193/?t=85)

虽然你可以手动注册这个泛型的每一个封闭版本(例如为 `WeatherForecastController` 注册),但随着应用增长,这种做法既不现实也无法扩展:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-open-generics-53953193/?t=100)

注册开放泛型的正确方式是使用 `typeof` 运算符。
它让你可以在不提供类型实参的情况下指定泛型类型。
这种方式没有编译期的类型约束,但它允许 DI 容器在运行时解析任何被请求的封闭泛型类型。

```csharp
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();
builder.Services.AddTransient<IWeatherService, OpenWeatherService>();
builder.Services.AddSingleton<IMapper, Mapper>();

builder.Services.AddTransient(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-open-generics-53953193/?t=130)

注册完成后,这个适配器就可以用标准的构造函数注入模式,以封闭泛型的形式注入到任何类中,例如控制器:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-open-generics-53953193/?t=145)

---

## 5. Registering multiple interface implementations

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-multiple-interface-implementations-53953194/) · 3:22

### 总结

.NET 依赖注入框架允许为同一个接口类型注册多个实现。
当注册了多个服务时,容器会把它们追加到内部的服务集合中,而不是覆盖之前的条目。
解析该接口的单个实例时返回的是最后注册的那个实现,而解析该接口的 IEnumerable 则可以按添加顺序访问到所有已注册的实现。

### 核心概念

- **Multiple Registrations(多重注册)**:DI 容器支持为同一个服务接口添加多个具体类型。
- **Service Collection Growth(服务集合的增长)**:每次注册都会向集合中添加一个新的 ServiceDescriptor,而不是替换已有的条目。
- **Last-In-First-Out (LIFO) Resolution(后进先出的解析)**:请求单个实例时,容器提供的是最近一次注册的实现。
- **Collection Resolution(集合解析)**:注入 `IEnumerable<T>` 可以让消费方拿到类型 T 的所有已注册实现。

### 课程笔记

.NET DI 容器允许为同一个接口注册多个实现。
例如,一个应用可能同时注册真实服务和一个内存版本,用于测试或作为回退方案。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-multiple-interface-implementations-53953194/?t=70)

在内部,`IServiceCollection` 会跟踪每一次注册。
为 `IWeatherService` 添加第二个实现会让服务数量增加,而不是替换掉第一次注册。
当应用尝试解析 `IWeatherService` 的单个实例时,DI 容器返回的是最后注册的那个实现(在这个例子中是 `InMemoryWeatherService`)。

要使用所有已注册的实现,消费方应当注入一个 `IEnumerable<T>`。
这样应用就能按照它们被添加到容器的顺序,访问到每一个实例。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-multiple-interface-implementations-53953194/?t=130)

集合注入之后,就可以用标准的 LINQ 方法来操作这些服务。
例如,你可能明确想要第一个注册的实现(前面例子中的 `OpenWeatherService`),而不是默认的最后注册的那个。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-multiple-interface-implementations-53953194/?t=185)

处理多个实现的控制器,其最终结构是在构造函数中定义这个集合,并在 action 方法内使用所需的特定服务。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/registering-multiple-interface-implementations-53953194/?t=195)

---

## 6. The ServiceDescriptor

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-servicedescriptor-53953195/) · 4:22

**总结**

ServiceDescriptor 是 .NET 依赖注入容器的基本构建块,它是定义服务如何注册和解析的底层对象。
每一个注册方法,比如 AddTransient 或 AddSingleton,最终都会创建一个 ServiceDescriptor 并把它加入 IServiceCollection。
理解这个类对高级的框架扩展至关重要,例如实现 decorator 或 interceptor,因为它提供了对服务类型、实现策略和生命周期的细粒度控制。

**核心概念**

- ServiceDescriptor 类描述了一个服务应当如何注册和解析。
- IServiceCollection 是一个由 ServiceDescriptor 对象组成的集合。
- 像 AddTransient 这样的标准扩展方法,只是对 ServiceDescriptor 实例化的包装。
- 描述符可以通过实现类型、已有实例或工厂委托来定义。
- 手动注册的做法,就是直接把一个描述符添加到 IServiceCollection 中。

**课程笔记**

ServiceDescriptor 类是 .NET 中服务注册背后的核心组件。
每当使用扩展方法注册服务时,容器实际上是在创建一个 ServiceDescriptor 并把它存入 IServiceCollection。

```csharp
builder.Services.AddHttpClient();

builder.Services.AddTransient<IWeatherService, OpenWeatherService>();

builder.Services.AddSingleton<IMapper, Mapper>();
builder.Services.AddTransient(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-servicedescriptor-53953195/?t=10)

在内部,IServiceCollection 就像一个由这些描述符组成的列表。
查看接口的实现就能看出这一点,其中包含了添加和移除 ServiceDescriptor 条目的方法。

```csharp
/// <inheritdoc />
public bool Remove(ServiceDescriptor item);

/// <inheritdoc />
public void RemoveAt(int index);

#nullable disable
void ICollection<ServiceDescriptor>.Add(ServiceDescriptor item);

IEnumerator IEnumerable.GetEnumerator();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-servicedescriptor-53953195/?t=40)

一个 ServiceDescriptor 明确定义了服务类型、实现类型(或实例/工厂)以及服务生命周期。
除了使用标准扩展方法之外,你也可以手动实例化一个描述符。
例如,把 IWeatherService 注册为 OpenWeatherService 实现并使用 transient 生命周期:

```csharp
var weatherServiceDescriptor =
    new ServiceDescriptor(typeof(IWeatherService), typeof(OpenWeatherService), ServiceLifetime.Transient);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-servicedescriptor-53953195/?t=120)

描述符创建好之后,使用 Add 方法把它添加到服务集合中。
这种方式在功能上与使用 AddTransient 扩展方法完全等价。

```csharp
builder.Services.Add(weatherServiceDescriptor);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-servicedescriptor-53953195/?t=165)

ServiceDescriptor 还支持更复杂的注册场景,比如使用工厂委托从 IServiceProvider 中手动解析依赖。
当你向标准注册方法提供自定义工厂时,底层用的就是同一套机制。

```csharp
var weatherServiceDescriptor =
    new ServiceDescriptor(typeof(IWeatherService), provider =>
    {
        return new OpenWeatherService(provider.GetRequiredService<IHttpClientFactory>());
    }, ServiceLifetime.Transient);

builder.Services.Add(weatherServiceDescriptor);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-servicedescriptor-53953195/?t=235)

这种描述式的做法在扩展框架或实现 interceptor 等高级模式时尤其有用,因为它提供了与控制反转(IoC)容器交互的最基础方式。

---

## 7. Add vs TryAdd

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/add-vs-tryadd-53953196/) · 4:22

### 总结

.NET 中 IServiceCollection 的 Add 方法允许对同一服务类型进行多次注册,默认情况下解析出来的通常是最后注册的实现。
相比之下,TryAdd 系列方法只有在容器中不存在该特定服务类型的注册时,才会真正注册这个服务。
这一机制可以防止意外的重复注册,并确保第一个注册的实现优先生效,在构建模块化或可扩展应用时尤其有用,因为默认服务可能需要被覆盖。

### 核心概念

- **Standard Add behavior(标准 Add 行为)**:同一服务类型允许多次注册;默认解析出来的是最后注册的那个实现。
- **TryAdd behavior(TryAdd 行为)**:只有在该服务类型尚未注册时,服务才会被添加到集合中。
- **ServiceDescriptor**:一种手动定义服务注册的方式,可以传给 Add 和 TryAdd 两类方法。
- **Typed variations(带类型的变体)**:TryAddTransient、TryAddScoped 和 TryAddSingleton 等扩展方法提供了与 TryAdd 相同的保护,同时语法更简洁。
- **First-in wins(先到先得)**:与标准的 Add 方法不同,TryAdd 会保留最初的注册,并忽略后续对同一服务类型的注册尝试。

### 课程笔记

使用标准注册方法,或者直接把 `ServiceDescriptor` 添加到 `IServiceCollection` 时,.NET 允许为同一服务类型注册多个实现。
在为单个接口注册了多个实现的场景下,当请求该接口的单个实例时,依赖注入容器会解析出最后注册的那个实现。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/add-vs-tryadd-53953196/?t=55)

为了防止意外的多次注册,或者确保只在没有提供其他实现时才使用默认实现,.NET 提供了 `TryAdd` 方法。
这个方法会检查 `IServiceCollection` 的当前状态。
如果它发现该服务类型(例如 `IWeatherService`)已经注册过,就不会添加新的描述符。
这就形成了一种"先到先得"的场景。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/add-vs-tryadd-53953196/?t=130)

除了手动构造 `ServiceDescriptor` 对象之外,你还可以使用包装好的扩展方法:`TryAddTransient`、`TryAddScoped` 和 `TryAddSingleton`。
这些方法具备与 `TryAdd` 相同的优势,同时保留了标准注册中简洁的泛型语法。
它们能有效防止应用不同部分对同一服务类型的重复注册。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/add-vs-tryadd-53953196/?t=220)

---

## 8. TryAddEnumerable

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/tryaddenumerable-53953197/) · 3:31

**总结**

TryAddEnumerable 是 .NET 依赖注入中一个专门的注册方法,用于确保某个服务类型的特定实现只被注册一次。
TryAdd 在该服务类型已存在任意实现时就会跳过注册,而 TryAddEnumerable 只在"服务类型与实现类型"这一对组合已经存在于容器中时才跳过注册。
这使它非常适合这样的场景:允许同一接口有多个不同实现(比如多个天气数据提供方或插件),同时防止同一个提供方被冗余地重复注册。

**核心概念**

- **Service Type vs. Implementation Type Matching(服务类型与实现类型的匹配)**:TryAddEnumerable 在决定是否添加服务之前,会同时检查服务类型(接口)和实现类型(类)。
- **ServiceDescriptor Requirement(对 ServiceDescriptor 的要求)**:与标准注册方法不同,TryAddEnumerable 要求使用 `ServiceDescriptor` 类。
- **Deduplication(去重)**:它防止同一个实现针对同一服务类型被注册多次,这在构建模块化或可扩展系统时非常有用。
- **Enumerable Support(集合支持)**:该方法可以接收单个 `ServiceDescriptor`,也可以接收 `IEnumerable<ServiceDescriptor>` 以一次性注册多个实现。

**课程笔记**

在标准的依赖注入注册中,`TryAdd` 方法会检查某个特定服务类型是否已经注册了任何实现。
如果已存在注册,`TryAdd` 就会忽略后续对该服务类型的注册尝试,无论实现类是否不同。
`TryAddEnumerable` 改变了这一行为,它检查的是特定的"服务类型与实现类型"组合是否已存在。

要使用 `TryAddEnumerable`,你必须采用 `ServiceDescriptor` 的方式。
这需要显式定义服务类型、实现类型和生命周期。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/tryaddenumerable-53953197/?t=55)

使用 `TryAddEnumerable` 时,你可以为同一个接口注册多个不同的实现。
例如,如果你先注册 `OpenWeatherService`、再注册 `InMemoryWeatherService`,两者都会被加入容器,因为它们的实现类型不同,尽管它们共享 `IWeatherService` 接口。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/tryaddenumerable-53953197/?t=70)

如果你尝试为同一服务类型多次注册完全相同的实现,`TryAddEnumerable` 会检测到已有的注册并跳过重复项。
这确保容器中不会出现同一个类的冗余条目。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/tryaddenumerable-53953197/?t=115)

该方法还支持一个接收 `IEnumerable<ServiceDescriptor>` 的重载。
这让你可以在一次调用中传入一个集合,比如描述符的数组或列表。
DI 容器会遍历这个集合并应用同样的逻辑:只添加那些在容器中尚不存在匹配的"服务类型与实现类型"组合的描述符。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/tryaddenumerable-53953197/?t=175)

---

## 9. Replacing dependencies

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/replacing-dependencies-53953199/) · 5:47

### 总结

本课讲解如何在 service provider 构建之前,通过移除或替换已有的服务注册来操作 IServiceCollection。
它涵盖了多种移除手段,包括按类型移除、按索引移除和按描述符移除,同时着重指出了一些关键陷阱,例如通过 ServiceDescriptor 移除服务时对引用相等性的要求。

### 核心概念

- **Service Collection Mutability(服务集合的可变性)**:在调用 `builder.Build()` 之前,`IServiceCollection` 都是可以被修改的;在那之后的注册会被忽略。
- **Type-based Removal(按类型移除)**:`RemoveAll(typeof(T))` 会移除与某个特定服务类型相关的所有注册。
- **Index-based Removal(按索引移除)**:`RemoveAt(index)` 允许移除特定位置上的服务,但这需要谨慎地管理索引。
- **Reference Equality in Removal(移除时的引用相等性)**:使用 `Remove(ServiceDescriptor)` 时,DI 容器查找的是内存中完全相同的对象引用,而不是值相同的描述符。
- **Use Cases(使用场景)**:这些技巧对实现 decorator、服务重定向,或把复杂注册逻辑隐藏在库的扩展方法中都至关重要。

### 课程笔记

在 .NET 中,`IServiceCollection` 在 `builder.Build()` 方法被调用之前都保持可修改状态。
无论使用现代的 `Program.cs` builder 模式还是旧式的 `Startup.cs` 做法,service provider 一旦创建就是不可变的。
在 builder 生成 provider 之后再尝试的任何服务注册,都会被应用忽略。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/replacing-dependencies-53953199/?t=10)

在 build 步骤之前,你可以修改这个集合来实现服务重定向或 decorator。
一种常见的方法是 `RemoveAll`,它会清除某个特定服务类型的所有注册。
当你想确保该接口此前的注册不会干扰你的新实现时,这非常有用。

```csharp
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();


builder.Services.AddTransient<IWeatherService, OpenWeatherService>();
builder.Services.AddTransient<IWeatherService, InMemoryWeatherService>();

builder.Services.RemoveAll(typeof(IWeatherService));
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/replacing-dependencies-53953199/?t=85)

你也可以使用 `RemoveAt` 按索引移除服务。
不过,硬编码索引通常并不可取,因为随着框架或其他库添加服务,集合的大小会发生变化。
如果要用这个方法,索引应当动态计算得出。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/replacing-dependencies-53953199/?t=145)

更细粒度的做法是使用 `ServiceDescriptor`。
一个常见的陷阱是:试图通过创建一个与已有描述符值完全相同的新 `ServiceDescriptor` 实例来移除服务。
由于 `ServiceDescriptor` 是一个类,`Remove` 方法检查的是引用相等性。
如果传给 `Remove` 的描述符在内存中与集合中当前那个不是同一个实例,移除就会失败,服务数量保持不变。

```csharp
builder.Services.AddHttpClient();

builder.Services.AddTransient<IWeatherService, OpenWeatherService>();
builder.Services.AddTransient<IWeatherService, InMemoryWeatherService>();

var openWeatherServiceDescriptor =
    new ServiceDescriptor(typeof(IWeatherService), typeof(OpenWeatherService), ServiceLifetime.

builder.Services.Remove(openWeatherServiceDescriptor);

// var openWeatherServiceDescriptor =
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/replacing-dependencies-53953199/?t=235)

要用描述符成功移除一个服务,你必须持有注册时所用的那个确切对象的引用。
把这个特定的描述符实例添加到集合中,再用同一个实例调用 `Remove`,DI 容器就能正确识别并删除该条目。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/replacing-dependencies-53953199/?t=295)

---

## 10. Cleaning up service registration

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/cleaning-up-service-registration-53953200/) · 3:22

把服务注册重构成 IServiceCollection 上的扩展方法,是保持 Program.cs 文件整洁可控的一项关键技巧。
通过把相关服务(比如某个特定领域如 weather 的服务)归入专门的静态类,开发者可以改善代码组织和可维护性。
此外,把这些扩展方法放在 Microsoft.Extensions.DependencyInjection 命名空间下,可以提升开发体验,因为使用方无需额外导入命名空间就能用到这些方法。

### 核心概念

- **Extension Methods(扩展方法)**:利用带 `this` 关键字的静态方法在 `IServiceCollection` 上进行扩展,以扩充内置的依赖注入容器。
- **Logical Grouping(逻辑分组)**:把相关的服务注册(例如 HTTP client、领域服务和描述符)合并到一个单一的、语义明确的方法中。
- **Fluent API Design(流式 API 设计)**:从注册方法中返回 `IServiceCollection` 实例,以支持方法链式调用。
- **Namespace Spoofing(命名空间"冒名")**:把扩展方法放进 `Microsoft.Extensions.DependencyInjection` 命名空间,以简化终端用户的使用。

### 课程笔记

在典型的 .NET 应用中,随着服务数量增长,`Program.cs` 或 `Startup.cs` 文件会变得臃肿。
手动注册多个服务描述符及其相关依赖,会导致启动流程杂乱且难以阅读。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/cleaning-up-service-registration-53953200/?t=10)

为了解决这个问题,你可以创建一个静态类,比如 `WeatherServiceRegistration`,并在 `IServiceCollection` 上定义一个静态扩展方法。
这让你可以把与某个特定功能集相关的所有逻辑都封装起来。

```csharp
namespace Weather.Api.Weather;

public static class WeatherServiceRegistration
{
    public static IServiceCollection AddWeatherServices(this IServiceCollection)
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/cleaning-up-service-registration-53953200/?t=70)

注册这些具体服务的逻辑被从主入口点移到了这个新方法里。
在方法末尾返回 `services` 对象是必要的,这样才能维持 .NET 服务注册所期望的流式 API 模式。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/cleaning-up-service-registration-53953200/?t=115)

重构之后,`Program.cs` 文件明显清爽了许多。
你只需在 `builder.Services` 上调用这个扩展方法,它把各个服务描述符的底层复杂性都隐藏了起来。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/cleaning-up-service-registration-53953200/?t=145)

业界的一个常见做法,尤其是对外部库而言,是把注册类的命名空间改成 `Microsoft.Extensions.DependencyInjection`。
这一技巧确保使用方只要拥有标准的依赖注入包,就能立刻用到这个扩展方法,而无需为该库特有的内部命名空间额外写一条 `using` 语句。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/cleaning-up-service-registration-53953200/?t=155)

---

## 11. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953201/) · 0:59

本课全面总结了 .NET 中依赖注入(DI)的进阶概念,重点放在实用的实现策略和架构最佳实践上。

### 核心概念

- **Pragmatic Interface Usage(务实地使用接口)**:摒弃"万物皆接口"的教条,转向务实的设计。
- **Service Lifetimes(服务生命周期)**:选择合适的生命周期(Singleton、Scoped、Transient)并理解解析方面的约束。
- **Circular Dependencies(循环依赖)**:识别并化解由服务之间相互依赖成环所引发的问题。
- **Advanced Registration(进阶注册)**:利用开放泛型和 `ServiceDescriptor` 对服务定义进行细粒度控制。
- **Registration Logic(注册逻辑)**:区分 `Add`、`TryAdd` 和 `TryAddEnumerable` 方法,以管理服务的重复注册。
- **Code Organization(代码组织)**:使用扩展方法重构并清理 `Program.cs` 或 `Startup.cs`。

### 课程笔记

#### Architectural Decisions and Lifetimes(架构决策与生命周期)

本次深入探讨中有相当大的篇幅聚焦于务实地使用接口。
接口对解耦和单元测试固然至关重要,但不应当被教条地套用到每一个类上。
开发者必须评估接口对某个具体组件是否真的带来实际价值。

选择正确的服务生命周期对应用的稳定性至关重要。
生命周期配置错误,比如试图从 singleton 中解析 scoped 服务,会导致运行时异常或内存泄漏。
此外,理解 DI 容器如何解析这些服务,有助于识别和修复循环依赖,也就是两个或多个服务互相依赖、导致无法成功实例化的情况。

#### Advanced Registration and Open Generics(进阶注册与开放泛型)

`ServiceDescriptor` 是 DI 容器的基本构建块,它包含了创建服务实例所需的全部信息。
除了标准注册之外,.NET 还支持开放泛型,允许用一次注册处理任意的泛型类型参数。

在管理同一服务类型的多个注册时,方法的选择很重要。
标准的 `Add` 方法会追加注册,而 `TryAdd` 确保只有在服务此前未被添加时才注册。
`TryAddEnumerable` 专门用于管理同一接口的多个实现,同时避免添加重复的实现类型。

#### Refactoring and Extension Methods(重构与扩展方法)

随着应用增长,`Program.cs` 文件会被服务注册塞得杂乱不堪。
为了保持可读性和组织性,这些注册应当被移到 `IServiceCollection` 上特定的扩展方法中。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953201/?t=10)

在上面的例子中,`AddWeatherServices()` 是一个封装了相关服务注册的扩展方法。
这段代码同时还演示了如何使用 `typeof(ILoggerAdapter<>)` 和 `typeof(LoggerAdapter<>)` 来注册开放泛型类型。
