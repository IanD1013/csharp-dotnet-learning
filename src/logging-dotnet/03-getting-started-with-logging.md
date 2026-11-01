# Getting started with Logging

> 课程:[From Zero to Hero: Logging in .NET](https://dometrain.com/course/from-zero-to-hero-logging-in-dotnet/) · 第 3 章
> 共 10 课 · 约 24:58
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [The modern .NET Logger](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-modern-net-logger-54121605/) | 3:03 | [↓](#1-the-modern-net-logger) |
| 2 | [The log methods](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-log-methods-54121607/) | 2:55 | [↓](#2-the-log-methods) |
| 3 | [Why simple logging isn't good enough](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/why-simple-logging-isnt-good-enough-54121608/) | 2:11 | [↓](#3-why-simple-logging-isnt-good-enough) |
| 4 | [What is Structured Logging?](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/what-is-structured-logging-54121609/) | 1:27 | [↓](#4-what-is-structured-logging) |
| 5 | [The message template](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-message-template-54121610/) | 1:38 | [↓](#5-the-message-template) |
| 6 | [Log entry parameters](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-entry-parameters-54121611/) | 2:31 | [↓](#6-log-entry-parameters) |
| 7 | [Understanding the Log Category](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/understanding-the-log-category-54121612/) | 2:10 | [↓](#7-understanding-the-log-category) |
| 8 | [Working with the Log Level](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/working-with-the-log-level-54121614/) | 5:20 | [↓](#8-working-with-the-log-level) |
| 9 | [The log event id](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-log-event-id-54121615/) | 2:49 | [↓](#9-the-log-event-id) |
| 10 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121616/) | 0:54 | [↓](#10-section-recap) |

## 1. The modern .NET Logger

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-modern-net-logger-54121605/) · 3:03

### 总结

现代 .NET 日志记录在所有应用类型中都是标准化的,包括 Console 应用、Minimal API、Web API 和 Blazor。
这种统一性确保了无论使用哪种项目模板,日志记录的实现都保持一致,都使用 ILogger 接口和内置的依赖注入容器。
无论是在控制台应用中手动配置 LoggerFactory,还是把 ILogger<T> 注入到控制器和 Razor 组件中,记录信息的底层概念和方法在整个 .NET 生态中都是相同的。

### 核心概念

*   **ILogger 和 ILogger<T>**:用于执行日志记录操作的主要接口。
*   **LoggerFactory**:用于手动创建 logger 实例的工厂,主要用于没有预先配置好宿主(host)的应用。
*   **日志类别(Logger Categories)**:对日志输出进行分类的标识符(通常基于类型或字符串)。
*   **依赖注入**:在 ASP.NET Core 和 Blazor 应用中获取 logger 的标准方式。
*   **提供程序配置(Provider Configuration)**:定义日志发送到哪里,例如控制台或 Windows 事件日志。

### 课程笔记

现代 .NET 日志记录被设计为无处不在且保持一致。
虽然不同的项目模板有不同的设置要求,但它们都依赖于同一套日志抽象。

#### Console Applications

在一个简单的控制台应用中,日志记录通常通过 `LoggerFactory` 手动设置。
这个工厂会配置特定的提供程序,例如 Console 提供程序,以决定日志输出到哪里。

```csharp
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
});

ILogger logger = loggerFactory.CreateLogger<Program>();

logger.LogInformation("Hello world!");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-modern-net-logger-54121605/?t=40)

`CreateLogger` 方法定义了 logger 的类别。
使用泛型形式 `CreateLogger<Program>()` 是标准做法,因为它会把类的完全限定名用作类别。
不过,你也可以指定一个自定义的字符串类别。

```csharp
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
});

ILogger logger = loggerFactory.CreateLogger("Course");

logger.LogInformation("Hello world!");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-modern-net-logger-54121605/?t=70)

#### Minimal APIs

在 ASP.NET Core Minimal API 中,日志基础设施由 `WebApplication` builder 自动注册。
你可以把 `ILogger<T>` 直接注入到路由处理程序(lambda)中,框架会通过依赖注入提供该实例。

```csharp
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", (ILogger<Program> logger) => 
{
    logger.LogInformation("Handling request");
    return "Hello world!";
});

app.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-modern-net-logger-54121605/?t=100)

#### Web API Controllers

对于使用控制器的传统 Web API,logger 通常通过构造函数注入。
框架会在实例化控制器时解析 `ILogger<T>` 依赖。

```csharp
using Microsoft.AspNetCore.Mvc;

namespace BasicWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.LogInformation("Fetching weather forecast data");
        // ... logic to return forecast
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-modern-net-logger-54121605/?t=125)

#### Blazor Applications

在 Blazor 中,你可以使用 `@inject` 指令把一个 `ILogger<T>` 实例引入 Razor 组件。
这样你就可以直接在 UI 逻辑中记录事件,例如按钮点击或生命周期变化。

```razor
@page "/counter"
@inject ILogger<Counter> Logger

<PageTitle>Counter</PageTitle>

<h1>Counter</h1>

<p role="status">Current count: @currentCount</p>

<button class="btn btn-primary" @onclick="IncrementCount">Click me</button>

@code {
    private int currentCount = 0;

    private void IncrementCount()
    {
        currentCount++;
        Logger.LogInformation("Current count is now: {Count}", currentCount);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-modern-net-logger-54121605/?t=160)

## 2. The log methods

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-log-methods-54121607/) · 2:55

### 总结

本课探讨 .NET 中 ILogger 接口的核心方法,区分基础的 Log 方法与 LogInformation 这类常用的扩展方法。
它拆解了 Log 方法的参数,例如 LogLevel 和 EventId,并通过强调为什么应避免在日志消息中使用传统的字符串操作(包括插值、拼接和 string.Format)来引入结构化日志的概念。

### 核心概念

- **ILogger 接口的核心方法**:`Log`、`BeginScope` 和 `IsEnabled`。
- **扩展方法**:像 `LogInformation` 和 `LogCritical` 这样简化日志调用的便捷包装。
- **LogLevel 枚举**:定义日志条目的严重级别(例如 Information、Warning、Error、Critical)。
- **EventId**:一个可选的标识符,用于对特定类型的日志事件进行分类。
- **日志反模式**:避免字符串插值、拼接和 `string.Format`,以保留结构化日志的能力。

### 课程笔记

`ILogger` 接口提供了 .NET 中日志记录的主要机制。
虽然开发者经常使用 `LogInformation` 这样的方法,但它们实际上是扩展方法。
`ILogger` 接口本身只包含三个核心方法:`Log`、`BeginScope` 和 `IsEnabled`。

```csharp
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
});

ILogger logger = loggerFactory.CreateLogger<Program>();

logger.LogInformation("Hello world!");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-log-methods-54121607/?t=15)

`Log` 方法是所有日志扩展方法背后的底层引擎。
要直接使用它,你必须指定 `LogLevel`、一个 `EventId`(目前可以设为 `0`)以及消息。

```csharp
logger.Log(LogLevel.Information, 0, "Hello world!");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-log-methods-54121607/?t=70)

```text
  LogInformation / LogError / LogCritical  (extension methods)
                      |
                      v
  ILogger.Log(LogLevel, EventId, message)
```

通过修改 `LogLevel` 参数,你可以调整日志的严重级别。
级别包括 `Information`、`Warning`、`Error` 和 `Critical`。

```csharp
logger.Log(LogLevel.Critical, 0, "Hello world!");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-log-methods-54121607/?t=85)

在实践中,除非要实现复杂的自定义逻辑,你很少会使用原始的 `Log` 方法。
相反,你应该使用预先配置好的扩展方法,例如 `LogInformation`、`LogError` 或 `LogCritical`。

在记录变量时,人们很容易使用标准的 C# 字符串操作技巧。
例如,你可能会使用字符串插值把 `age` 这样的变量放进消息中。

```csharp
var age = 30;

logger.LogInformation($"Nick just turned: {age}");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-log-methods-54121607/?t=145)

或者,你可能会考虑字符串拼接或 `string.Format`。

```csharp
logger.LogInformation("Nick just turned: " + age);

logger.LogInformation(string.Format("Nick just turned: {0}", age));
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-log-methods-54121607/?t=160)

然而,在日志记录的语境下,所有这些做法(插值、拼接和 `string.Format`)从根本上都是错误的。
它们会阻止日志系统利用结构化日志,这个概念将在后续课程中详细探讨。

## 3. Why simple logging isn't good enough

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/why-simple-logging-isnt-good-enough-54121608/) · 2:11

### 总结

在生产系统中,使用字符串插值或拼接的简单日志记录是不够的,因为它把数据直接"烘焙"进了消息字符串。
这种做法让日志分析工具无法有效地按特定参数搜索或筛选,因为各个数据点都淹没在扁平的文本中。
通过查看 JSON Console 提供程序的输出可以清楚地看到,插值进去的值被当作不可变消息的一部分,而不是独立的元数据,因此必须转向结构化日志或语义化日志。

### 核心概念

- **数据烘焙(Data Baking)**:字符串插值或拼接把变量合并成单个字符串、从而丢失原始数据结构的过程。
- **可搜索性与分析**:当某些值没有作为独立属性存储时,很难针对这些特定值查询日志(例如查找某个特定用户 ID 的所有日志)。
- **JSON Console 提供程序**:一种把日志输出为 JSON 对象的日志提供程序,使日志条目的内部结构(或结构的缺失)变得可见。
- **结构化/语义化日志**:提供消息模板和独立参数的做法,使日志提供程序能够把数据保留为独立的属性。

### 课程笔记

对于专业的应用来说,过于简单的日志记录方式在技术上往往是不正确的。
虽然字符串插值、字符串拼接或 `string.Format` 这样的方法在标准控制台输出中看起来能用,但它们无法提供现代日志分析所需的元数据。

```csharp
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
});

ILogger logger = loggerFactory.CreateLogger<Program>();

var age = 30;

logger.LogInformation($"Nick just turned: {age}");
logger.LogInformation("Nick just turned: " + age);
logger.LogInformation(string.Format("Nick just turned: {0}", age));
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/why-simple-logging-isnt-good-enough-54121608/?t=10)

即使使用变量来创建更通用的日志条目,根本问题依然存在。
如果把日志提供程序换成一个为复杂分析而捕获数据的提供程序,基于简单字符串的日志记录的局限就会显现出来。

```csharp
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
});

ILogger logger = loggerFactory.CreateLogger<Program>();

var name = "Nick";
var age = 30;

logger.LogInformation($"{name} just turned: {age}");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/why-simple-logging-isnt-good-enough-54121608/?t=35)

为了演示这一点,可以把标准的 Console 提供程序替换为 JSON Console 提供程序。
这一改动改变了日志信息被转换成适合分析的格式的方式,而无需改动日志调用本身。

```csharp
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddJsonConsole();
    // .AddConsole();
});

ILogger logger = loggerFactory.CreateLogger<Program>();

var name = "Nick";
var age = 30;

logger.LogInformation($"{name} just turned: {age}");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/why-simple-logging-isnt-good-enough-54121608/?t=60)

查看生成的 JSON 输出时,一个关键缺陷就暴露出来了。
`name` 和 `age` 的值(例如 "Nick" 和 "30")被烘焙进了 `Message` 和 `{OriginalFormat}` 字段。

```json
{
  "EventId": 0,
  "LogLevel": "Information",
  "Category": "Program",
  "Message": "Nick just turned: 30",
  "State": {
    "Message": "Nick just turned: 30",
    "{OriginalFormat}": "Nick just turned: 30"
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/why-simple-logging-isnt-good-enough-54121608/?t=85)

由于这些值只是作为一个更大字符串的一部分被捕获,要在成千上万条日志中可靠地搜索或分析它们几乎是不可能的。
如果你想找出每一个用户年满 30 岁的实例,就不得不进行代价高昂且容易出错的字符串解析。
这个问题的解决方案是结构化日志(也称为语义化日志),它会把这些值保留为可搜索的元数据。

## 4. What is Structured Logging?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/what-is-structured-logging-54121609/) · 1:27

### 总结

结构化日志,也称为语义化日志,是现代 .NET 开发中的一个基础概念,它优先捕获数据上下文,而不是简单的消息字符串。
结构化日志不是用字符串插值或拼接把值直接烘焙进消息,而是把日志消息视为带有关联参数的模板。
这种做法让日志提供程序能够保留原始格式和各个数据点,从而在日志管理系统中实现高级的筛选、搜索和分析。

### 核心概念

- 结构化日志与语义化日志
- 消息模板与字符串插值
- 保留上下文和元数据
- 参数化日志
- 提供程序层面的日志处理

### 课程笔记

无论你使用的是内置的 `ILogger` 还是 Serilog 这样的第三方库,结构化日志都是使用现代 .NET 日志时最需要掌握的关键概念。
其首要目标是避免记录单独的、扁平的字符串,而是捕获丰富的上下文和元数据,供日后调试和解决问题时使用。

当日志被作为结构化数据处理时,输出中不仅包含渲染后的消息,还包含状态和原始格式。
如果使用字符串插值,日志提供程序收到的就是一个已求值的单一字符串,从而失去了区分模板与数据的能力。

```json
{"EventId":0,"LogLevel":"Information","Category":"Program","Message":"Nick just turned: 30","State":{"Message":"Nick just turned: 30","{OriginalFormat}":"Nick just turned: 30"}}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/what-is-structured-logging-54121609/?t=10)

产生这种扁平输出的代码通常使用字符串插值或拼接,它们会在变量到达 logger 之前就对其求值。
这在结构化日志中被视为一种反模式。

```csharp
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddJsonConsole();
    //.AddConsole();
});

ILogger logger = loggerFactory.CreateLogger<Program>();

var name = "Nick";
var age = 30;

logger.LogInformation($"{name} just turned: {age}");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/what-is-structured-logging-54121609/?t=25)

在现代 IDE 中,以这种方式使用字符串插值常常会触发一条警告,提示"日志消息模板不应在多次调用之间变化"(logging message template should not vary between calls)。
这个提示指向了结构化日志的核心原则:消息应当是一个静态模板。

结构化日志把消息视为带有关联参数的模板。
你不是把参数烘焙进字符串,而是把模板和参数分别传给提供程序。
这让提供程序能够恰当地处理数据,例如把参数存储为可搜索的元数据。
正确的实现方式是使用带命名占位符的消息模板:

```csharp
// The correct way to implement a message template with parameters
logger.LogInformation(LogEvents.UserBirthday, "{Name} just turned: {Age}", name, age);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/what-is-structured-logging-54121609/?t=84)

## 5. The message template

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-message-template-54121610/) · 1:38

### 总结

本课讲解在 .NET 日志中如何从 C# 字符串插值过渡到结构化消息模板。
它详细说明了如何在日志消息中用花括号定义占位符,并强调这些占位符名称与局部变量名称无关,而是作为结构化数据的键。

### 核心概念

* 用消息模板取代字符串插值
* 使用花括号 `{}` 定义结构化占位符
* 让日志属性名与局部变量名解耦
* 把 `message` 参数理解为格式字符串

### 课程笔记

在 .NET 中实现日志记录时,最佳实践是使用消息模板,而不是标准的 C# 字符串插值(`$` 前缀)。
虽然 `LogInformation` 这类方法的第一个参数通常名为 `message`,但从技术上讲,它是一个结构化消息模板的格式字符串。
这个名称在很大程度上是框架早期版本遗留下来的产物。

采用消息模板的第一步是去掉字符串插值符号,以确保日志框架能够分别解析模板及其参数。

```csharp
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddJsonConsole();
    //.AddConsole();
});

ILogger logger = loggerFactory.CreateLogger<Program>();

var name = "Nick";
var age = 30;

logger.LogInformation("{name} just turned: {age}");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-message-template-54121610/?t=10)

模板中的占位符使用花括号 `{}` 定义。
这些占位符并不直接绑定到应用代码中的变量名;它们是被捕获的结构化数据的标识符。
无论局部变量叫什么名字,你都可以为这些占位符选择任意名称。

```csharp
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddJsonConsole();
    //.AddConsole();
});

ILogger logger = loggerFactory.CreateLogger<Program>();

var name = "Nick";
var age = 30;

logger.LogInformation("{Firs} just turned: ");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-message-template-54121610/?t=55)

这种解耦让日志的结构即使在内部变量名改变时也能保持一致。
例如,使用 `{Name}` 和 `{Age}` 作为占位符,可以为 JSON console logger 这类结构化日志提供程序提供清晰、一致的键。

```csharp
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddJsonConsole();
    //.AddConsole();
});

ILogger logger = loggerFactory.CreateLogger<Program>();

var name = "Nick";
var age = 30;

logger.LogInformation("{Name} just turned: {Age}");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-message-template-54121610/?t=85)

如果模板中定义了占位符,但没有向日志方法传入任何参数,输出将把原始模板字符串显示为消息。

## 6. Log entry parameters

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-entry-parameters-54121611/) · 2:31

### 总结

本课演示如何在 .NET 中通过使用带命名占位符的消息模板(而不是字符串插值或拼接)来实现结构化日志。
通过把变量作为独立参数传给日志方法,.NET 会把数据保留为一个结构化的状态对象,使日志提供程序能够把名字和整数这类参数捕获为独立的 JSON 属性。
这种做法对现代应用开发至关重要,因为它支持基于特定日志元数据进行高级筛选、查询和告警,而不必解析原始文本字符串。

### 核心概念

*   **消息模板**:在日志消息字符串中使用花括号中的命名占位符(例如 `{Name}`)。
*   **结构化日志**:避免字符串插值(`$`)和拼接(`+`),以确保参数被作为数据保留下来。
*   **状态对象(State Object)**:存储参数和日志消息原始格式的底层机制。
*   **参数保留**:日志提供程序(如 JSON Console)会保持参数的原始数据类型,例如字符串和整数。
*   **提供程序的灵活性**:不同的提供程序可以以不同方式处理结构化数据,从简单的文本替换到复杂的 JSON 序列化。

### 课程笔记

在 .NET 中记录日志时,最佳实践是避免字符串插值和拼接。
相反,你应该使用消息模板。
要把参数传递下去,你需要在消息字符串中定义占位符,并把对应的变量作为后续参数提供。

```csharp
logger.LogInformation("{Name} just turned: {Age}", );
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-entry-parameters-54121611/?t=10)

在可用的实现中,变量按照它们在模板中出现的顺序传入。
现代 IDE 通常会高亮显示哪个变量对应模板字符串中的哪个命名占位符。

```csharp
ILogger logger = loggerFactory.CreateLogger<Program>();

var name = "Nick";
var age = 30;

logger.LogInformation("{Name} just turned: {Age}", name, age);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-entry-parameters-54121611/?t=25)

```text
  "{Name} just turned: {Age}",  name,  age
    |                   |        |      |
    |                   +--------|------+
    +----------------------------+
```

当这条日志条目被处理时,模板的原始格式会被保留,但生成的输出中包含一个 "State" 对象。
这个对象极其重要,因为它在包含消息的同时,还包含 `Name` 和 `Age` 这两个独立参数。
它们以其原始值被捕获为 JSON 对象。

```json
{"EventId":0,"LogLevel":"Information","Category":"Program","Message":"Nick just turned: 30","State":{"Message":"Nick just turned: 30","Name":"Nick","Age":30,"{OriginalFormat}":"{Name} just turned: {Age}"}}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-entry-parameters-54121611/?t=40)

这种结构是结构化日志的核心。
它让日志提供程序能够执行扁平文本无法实现的操作,例如设置条件、触发告警,或根据特定参数值筛选日志。

标准的 console logger 可能会把这些数据压平成一个简单的字符串,而 JSON console logger 则会保留元数据,包括数据类型(例如 `Nick` 是字符串,`30` 是整数)。
在整个应用中一致地使用这种模式,会让调试变得容易得多。

```csharp
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddJsonConsole();
    //.AddConsole();
});

ILogger logger = loggerFactory.CreateLogger<Program>();

var name = "Nick";
var age = 30;

logger.LogInformation("{Name} just turned: {Age}", name, age);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-entry-parameters-54121611/?t=130)

## 7. Understanding the Log Category

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/understanding-the-log-category-54121612/) · 2:10

### 总结

.NET 中的日志类别是与日志消息关联的一个字符串标识符,通常代表生成这些日志的类或组件。
虽然从技术上讲它是一个任意字符串,但标准约定是使用 logger 所在类的完全限定名,这通常通过注入 ILogger<T> 来实现。
这种分类对于筛选日志和在调试时隔离问题至关重要,因为它让开发者能够通过配置控制应用中特定部分的详细程度。

### 核心概念

* **任意字符串**:日志类别可以是任意字符串,不过在使用泛型 logger 时它默认为类型名。
* **命名约定**:标准做法是使用实例化或注入 logger 的那个类的类名。
* **筛选**:在配置中,类别被用作日志筛选和级别覆盖的主键。
* **依赖注入**:从 .NET 6 起,`ILogger<T>` 会被自动注册到 DI 容器中,但非泛型的 `ILogger` 不会。

### 课程笔记

日志类别本质上是附加在每条日志消息上的一个任意字符串。
使用 `LoggerFactory` 创建 logger 时,类别在创建的那一刻就被确定了。
如果使用泛型的 `CreateLogger<T>` 方法,类别默认为类型 `T` 的名称(例如 "Program")。

```csharp
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddJsonConsole();
});

ILogger logger = loggerFactory.CreateLogger<Program>();

var name = "Nick";
var age = 30;

logger.LogInformation("{Name} just turned: {Age}", name, age);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/understanding-the-log-category-54121612/?t=10)

当这段代码使用 JSON console formatter 执行时,类别会在输出中明确可见。
这让外部日志聚合器或阅读控制台的开发者能够准确识别是哪个组件产生了这条记录。

```json
{"EventId":0,"LogLevel":"Information","Category":"Program","Message":"Nick just turned: 30","State":{"Message":"Nick just turned: 30","Name":"Nick","Age":30,"{OriginalFormat}":"{Name} just turned: {Age}"}}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/understanding-the-log-category-54121612/?t=40)

虽然你可以向 `CreateLogger("CustomCategory")` 方法提供任意字符串,但一般约定是使用类名。
这种一致性让调试和隔离日志更容易,因为类别可以充当筛选条件。

在现代 .NET 应用中(从 .NET 6 起),泛型的 `ILogger<T>` 接口会被自动注册到依赖注入容器中。
在 Minimal API 中,你可以把 `ILogger<Program>` 直接注入到路由处理程序中:

```csharp
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", (ILogger<Program> logger) =>
{
    return "Hello World!";
});

app.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/understanding-the-log-category-54121612/?t=75)

需要注意的是,原始的、非泛型的 `ILogger` 接口默认并未注册。
如果你尝试在控制器或 Minimal API 端点中注入不带泛型类型参数的 `ILogger`,除非你手动注册过它,否则应用将无法解析该服务。
标准模式是始终使用泛型版本,以确保类别被正确映射到类。

```csharp
using Microsoft.AspNetCore.Mvc;

namespace BasicWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.LogInformation("Retrieving weather");
        // ... logic
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/understanding-the-log-category-54121612/?t=100)

这种行为在 Web API、Blazor 以及其他 .NET 应用模板中是一致的。
当类别与日志级别和筛选器结合使用时,它会变得强大得多,可以根据日志来源对捕获哪些日志进行细粒度控制。

## 8. Working with the Log Level

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/working-with-the-log-level-54121614/) · 5:20

### 总结

本课探讨 .NET 中日志级别的层级结构,以及最低日志级别阈值的概念。
它演示了默认配置通常如何屏蔽 Debug 和 Trace 这类较低级别的日志,并讲解如何在控制台应用中以编程方式、在 ASP.NET Core 中通过配置文件来调整这些阈值。

### 核心概念

* **日志级别层级**:日志按严重程度分类:Trace < Debug < Information < Warning < Error < Critical < None。
* **最低日志级别(Minimum Log Level)**:决定哪些日志会被处理的阈值;任何级别低于该阈值的日志都会被忽略。
* **默认阈值**:大多数日志提供程序,包括 Console 和 JSON Console,默认为 `LogLevel.Information`。
* **编程式配置**:在控制台应用中,通过 `ILoggingBuilder` 上的 `SetMinimumLevel` 方法设置最低级别。
* **基于配置的筛选**:在 ASP.NET Core 中,日志级别通常通过 `appsettings.json` 管理,可以按类别进行细粒度控制(例如 `Default` 与 `Microsoft.AspNetCore`)。
* **LogLevel.None**:一个特殊级别,当被设为最低级别时,会屏蔽所有日志输出。

```text
  Trace < Debug < Information < Warning < Error < Critical < None
  \___________/   ^
     ignored      default minimum (Console / JSON Console)
```

### 课程笔记

默认情况下,在 .NET 中使用标准日志提供程序时,以 `Information` 级别发出的日志会在输出中可见。
例如,一条 `Information` 级别的 JSON console 日志包含级别、类别和状态:

```json
{"EventId":0,"LogLevel":"Information","Category":"Program","Message":"Nick just turned: 30","State":{"Message":"Nick just turned: 30","Name":"Nick","Age":30,"{OriginalFormat}":"{Name} just turned: {Age}"}}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/working-with-the-log-level-54121614/?t=10)

然而,如果你尝试以低于 `Information` 的级别记录日志,例如 `Debug` 或 `Trace`,这些日志默认不会出现在控制台中。

```csharp
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddJsonConsole();
    //.AddConsole();
});

ILogger logger = loggerFactory.CreateLogger<Program>();

var name = "Nick";
var age = 30;

logger.LogDebug("{Name} just turned: {Age}", name, age);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/working-with-the-log-level-54121614/?t=25)

这是由"最低日志级别"阈值造成的。
标准 console 和 JSON console 提供程序的默认最低级别都是 `Information`,这意味着它们会忽略 `Debug` 或 `Trace` 级别的任何内容。

```csharp
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddJsonConsole();
    //.AddConsole();
});

ILogger logger = loggerFactory.CreateLogger<Program>();

var name = "Nick";
var age = 30;

logger.LogTrace("{Name} just turned: {Age}", name, age);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/working-with-the-log-level-54121614/?t=40)

这种行为在 ASP.NET Core Web API 中也是一致的。
如果控制器使用 `_logger.LogDebug("Retrieving weather")`,除非修改配置,否则这条消息不会出现在日志中。
把调用改为 `_logger.LogInformation("Retrieving weather")`,由于默认阈值的缘故,日志会立即变得可见。

```csharp
[HttpGet(Name = "GetWeatherForecast")]
public IEnumerable<WeatherForecast> Get()
{
    _logger.LogInformation("Retrieving weather");
    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    {
        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    })
    .ToArray();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/working-with-the-log-level-54121614/?t=100)

#### 配置最低级别

在控制台应用中,你可以在 `LoggerFactory.Create` 的 builder 中使用 `SetMinimumLevel` 以编程方式调整阈值。
把级别设为 `LogLevel.Debug` 会让 `Debug` 日志通过,而 `Trace` 日志(低于 `Debug`)仍然被屏蔽。

```csharp
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder
        .SetMinimumLevel(LogLevel.Debug)
        .AddJsonConsole();
    //.AddConsole();
});

ILogger logger = loggerFactory.CreateLogger<Program>();

var name = "Nick";
var age = 30;

logger.LogDebug("{Name} just turned: {Age}", name, age);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/working-with-the-log-level-54121614/?t=145)

把最低级别设为 `LogLevel.None` 实际上会禁用所有日志,因为 `None` 是层级中可能的最高值,没有任何日志级别能超过它。

```csharp
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder
        .SetMinimumLevel(LogLevel.None)
        .AddJsonConsole();
    //.AddConsole();
});
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/working-with-the-log-level-54121614/?t=175)

#### 在 ASP.NET Core 中配置

在现代 ASP.NET Core 应用中,日志级别通常通过 `appsettings.json` 管理。
这种配置让你可以为不同的类别定义级别。
`Default` 类别充当兜底项,适用于任何没有显式定义的类别。

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/working-with-the-log-level-54121614/?t=220)

在上面的示例中,`Microsoft.AspNetCore`(涵盖 HTTP 请求处理这类框架层面的事件)被设为 `Warning`。
如果把它改为 `Information`,日志中就会出现大量来自内部管道中间件、路由和宿主诊断的"冗余"。

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Information"
    }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/working-with-the-log-level-54121614/?t=260)

通常,框架类别会保持在 `Warning`,以避免"过度记录",而应用自身的日志则通过 `Default` 或特定的命名空间条目来管理。

## 9. The log event id

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-log-event-id-54121615/) · 2:49

### 总结

日志事件 ID 是 .NET 中一种用于标记和分类日志条目的确定性方式。
虽然它默认为零,但开发者可以提供自定义 ID,把特定的日志事件与唯一的数字标识符关联起来,便于筛选和分析。
尽管结构化日志提供了其他捕获上下文的方式,事件 ID 仍然是在整个应用中一致地标记操作的可靠工具。

### 核心概念

* **默认事件 ID**:默认情况下,日志条目被分配的事件 ID 为 0。
* **自定义事件 ID**:日志方法的重载允许传入一个特定的整数来标识事件。
* **EventId 结构体**:从技术上讲,该参数是一个 `EventId` 结构体,它支持从 `int` 隐式转换。
* **集中管理**:使用一个包含常量整数的静态类,可以以可维护的方式管理事件 ID。
* **确定性标记**:事件 ID 提供了一种一致的日志分类方式,与现代结构化日志相辅相成。

### 课程笔记

事件 ID 是日志消息中包含的一个元数据字段,在格式化输出中通常作为第一个参数出现。
在标准 console 或 JSON console 提供程序中,如果没有显式提供,这个 ID 默认为零。

```csharp
var name = "Nick";
var age = 30;

logger.LogInformation("{Name} just turned: {Age}", name, age);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-log-event-id-54121615/?t=10)

要自定义这个值,你可以使用接受整数作为第一个参数的方法重载。
例如,为某个特定的日志事件分配 ID `5001`:

```csharp
logger.LogInformation(5001, "{Name} just turned: {Age}", name, age);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-log-event-id-54121615/?t=55)

虽然方法签名接受一个 `int`,但该参数实际上是一个 `EventId` 结构体。
.NET 日志框架提供了一个隐式运算符,会自动把整数转换为 `EventId`。
这个结构体让你可以用数字标识符标记操作,使它们更容易追踪和筛选。

管理这些 ID 的一个常见最佳实践是创建一个集中的类,例如 `LogEvents`,把它们存储为常量。
这能提供上下文,并确保整个应用中的一致性。

```csharp
namespace BasicConsoleApp;

public class LogEvents
{
    public const int UserBirthday = 5001;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-log-event-id-54121615/?t=115)

通过使用这些常量,日志调用会变得更易读、更易维护:

```csharp
logger.LogInformation(LogEvents.UserBirthday, "{Name} just turned: {Age}", name, age);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-log-event-id-54121615/?t=145)

尽管结构化日志引入了其他为日志附加属性的方式,事件 ID 仍然是一种强大的、确定性的方法,用于标记和分类应用事件。

## 10. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121616/) · 0:54

### 总结

本课全面回顾了 .NET 日志的基础概念,涵盖现代 .NET logger 的配置与注入、结构化日志相对于简单字符串插值的优势,以及如何管理日志类别和级别来控制输出。
此外,它还强调了事件 ID 在识别应用中特定类型日志事件方面的作用。

### 核心概念

*   现代 .NET logger 的配置与依赖注入。
*   从字符串插值过渡到结构化日志,以更好地捕获数据。
*   把日志类别和日志级别作为筛选阈值。
*   使用事件 ID 对特定日志事件进行分类和识别。

### 课程笔记

现代 .NET 日志系统被设计为高度可配置,并且可以轻松注入到大多数现代 .NET 和 ASP.NET 应用中。
借助 `ILogger<T>` 接口,开发者可以利用依赖注入获得一个自动与特定类别关联的 logger 实例,这个类别通常就是注入它的那个类。

```csharp
using Microsoft.AspNetCore.Mvc;

namespace BasicWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.LogInformation("Retrieving weather");
        // ...
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121616/?t=1)

现代日志记录中的一个关键区别是摒弃简单的字符串拼接或插值。
虽然插值很方便,但对现代应用来说并不够用,因为它把日志数据当作扁平字符串处理,丢失了底层结构。
结构化日志允许把参数捕获为独立的元数据,从而在日志管理系统中实现更强大的查询和分析。

```csharp
var name = "Nick";
var age = 30;

logger.LogInformation("{Name} just turned: {Age}", name, age);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121616/?t=11)

日志类别和日志级别共同定义了应当记录内容的最低阈值。
类别标识日志的来源(通常是命名空间和类),而日志级别(例如 Information、Warning、Error)表示严重程度。
它们通常在 `appsettings.json` 中配置,让应用的不同部分可以拥有不同的日志详细程度。

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121616/?t=32)

最后,事件 ID 提供了一种把唯一数字标识符与特定日志事件关联起来的方式。
这让开发者能够在文本消息或类别之外对日志进行分类,更容易在系统的不同部分之间筛选或归组相关的事件。

```csharp
namespace BasicConsoleApp;

public class LogEvents
{
    public const int UserBirthday = 5001;
}

// Usage in Program.cs
logger.LogInformation(LogEvents.UserBirthday, "{Name} just turned: {Age}", name, age);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121616/?t=42)
