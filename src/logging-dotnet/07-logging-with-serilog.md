# Logging with Serilog

> 课程:[From Zero to Hero: Logging in .NET](https://dometrain.com/course/from-zero-to-hero-logging-in-dotnet/) · 第 7 章
> 共 17 课 · 约 40:40
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [What is Serilog?](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/what-is-serilog-54121681/) | 2:19 | [↓](#1-what-is-serilog) |
| 2 | [Creating a Serilog Logger](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/creating-a-serilog-logger-54121682/) | 2:25 | [↓](#2-creating-a-serilog-logger) |
| 3 | [The concept of Sinks](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-concept-of-sinks-54121683/) | 2:46 | [↓](#3-the-concept-of-sinks) |
| 4 | [The static Serilog logger](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-static-serilog-logger-54121684/) | 1:35 | [↓](#4-the-static-serilog-logger) |
| 5 | [Simple Logger Dependency Injection](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/simple-logger-dependency-injection-54121685/) | 2:04 | [↓](#5-simple-logger-dependency-injection) |
| 6 | [Integrating with the Microsoft Logger](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/integrating-with-the-microsoft-logger-54121686/) | 2:17 | [↓](#6-integrating-with-the-microsoft-logger) |
| 7 | [File-based configuration](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/file-based-configuration-54121688/) | 2:37 | [↓](#7-file-based-configuration) |
| 8 | [Log enrichment](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-enrichment-54121689/) | 2:15 | [↓](#8-log-enrichment) |
| 9 | [Integrating with Application Insights](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/integrating-with-application-insights-54121690/) | 2:32 | [↓](#9-integrating-with-application-insights) |
| 10 | [Dealing with Structured Data](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/dealing-with-structured-data-54121691/) | 3:09 | [↓](#10-dealing-with-structured-data) |
| 11 | [Transforming Structured Data](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/transforming-structured-data-54121692/) | 1:28 | [↓](#11-transforming-structured-data) |
| 12 | [Manually enriching logs](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/manually-enriching-logs-54121693/) | 2:05 | [↓](#12-manually-enriching-logs) |
| 13 | [Timed Operations](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/timed-operations-54121694/) | 2:51 | [↓](#13-timed-operations) |
| 14 | [Masking sensitive logs](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/masking-sensitive-logs-54121695/) | 3:06 | [↓](#14-masking-sensitive-logs) |
| 15 | [Asynchronous processing](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/asynchronous-processing-54121696/) | 2:31 | [↓](#15-asynchronous-processing) |
| 16 | [Creating your own Sink](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/creating-your-own-sink-54121698/) | 3:29 | [↓](#16-creating-your-own-sink) |
| 17 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121699/) | 1:11 | [↓](#17-section-recap) |

## 1. What is Serilog?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/what-is-serilog-54121681/) · 2:19

### 总结

Serilog 是 .NET 中首屈一指的开源日志库,它开创并标准化了结构化日志、消息模板和语义化日志等现代日志实践。
虽然 Microsoft 内置的日志抽象被广泛使用,但 Serilog 仍然是一个功能更丰富的替代方案,它拥有由"sink"(输出提供程序)构成的庞大生态,并在 .NET 社区中获得了大量自发的采用。

### 核心概念

* **开源传统**:一个历史悠久、由社区维护的项目,自发下载量接近十亿次。
* **结构化日志的先驱**:Serilog 开创并标准化了消息模板和语义化日志等概念,Microsoft 后来在自己的日志抽象中采纳了这些概念。
* **Sinks**:Serilog 中与日志提供程序(logging provider)对等的概念。针对各种存储和监控系统,有数百个可用的 sink。
* **功能丰富**:相比默认的 Microsoft 提供程序,提供了更高级的参数解析和结构化日志能力。
* **集成**:经常被用作 `Microsoft.Extensions.Logging` 抽象背后的底层实现。

### 课程笔记

Serilog 是一个开源日志库,多年来一直是 .NET 生态中的重要组成部分。
它以自发的流行度而广为人知,在 NuGet 上的下载量接近十亿次。
虽然 Microsoft 自己的日志抽象(`Microsoft.Extensions.Logging`)下载量更高,但这些数字往往被默认项目模板中的隐式依赖所抬高。
相比之下,Serilog 的采用反映了各种规模组织中的开发者有意识的选择。

```csharp
#r "nuget: Serilog, 2.12.0"
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/what-is-serilog-54121681/?t=40)

Serilog 对生态的主要贡献,在于开创并标准化了结构化日志。
如今被视为 .NET 日志标准的那些概念,例如作用域(scope)、消息模板和语义化日志,最初都是由 Serilog 引入并推广的。
即使在今天,Serilog 的功能仍然比内置的 Microsoft 提供程序更丰富,对参数解析和复杂数据结构的支持也更出色。

Serilog 的一个关键特性是它使用的"sink",它们相当于 Microsoft 生态中的日志提供程序。
有数百个由社区维护的 sink 可用,提供的输出目标范围远比标准的 Microsoft 提供程序广泛。
在许多专业环境中,Microsoft 日志门面(facade)主要被用作一个入口,以便在幕后利用 Serilog 的能力。

```csharp
var position = new { Latitude = 25, Longitude = 134 };
var elapsedMs = 34;

log.Information("Processed {@Position} in {Elapsed:000} ms.", position, elapsedMs);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/what-is-serilog-54121681/?t=70)

## 2. Creating a Serilog Logger

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/creating-a-serilog-logger-54121682/) · 2:25

Serilog 是一个流行的 .NET 第三方日志框架,提供流式(fluent)配置 API 和结构化日志能力。
本课演示 Serilog 日志器的初始搭建,包括安装包、使用 LoggerConfiguration 构建 ILogger 实例,以及解决与标准 Microsoft 日志抽象之间的命名冲突。
本课还介绍了 Sink 的概念(即 Serilog 中与提供程序对等的东西),并把 Serilog 的日志级别与 Microsoft 的对应级别一一对照。

### 核心概念

*   **LoggerConfiguration**:用于配置并构建 Serilog 日志器实例的主要类。
*   **ILogger 歧义**:Serilog 使用了与 `Microsoft.Extensions.Logging` 相同的接口名(`ILogger`),因此在某些上下文中需要显式指定命名空间。
*   **日志级别对照**:Serilog 对某些级别使用了略有不同的术语,例如 `Verbose`(Trace)和 `Fatal`(Critical)。
*   **Sinks**:Serilog 对输出目的地的称呼,相当于 Microsoft 日志框架中的"Providers"。

### 课程笔记

要开始使用 Serilog,必须安装 `Serilog` NuGet 包。
包可用之后,就可以定义要在日志操作中使用的数据结构,例如一个 `Payment` 类。

```csharp
namespace BasicConsoleApp;

public class Payment
{
    public int PaymentId { get; set; }

    public string Email { get; set; }
    
    public Guid UserId { get; set; }

    public DateTime OccuredAt { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/creating-a-serilog-logger-54121682/?t=20)

#### 初始化日志器

与常常依赖 `LoggerFactory` 的 Microsoft 日志框架不同,Serilog 使用一个流式的 `LoggerConfiguration` 对象来构建日志器。
创建日志器最简单的方式,是实例化该配置并调用 `CreateLogger()`。

```csharp
using Serilog;

var logger = new LoggerConfiguration()
    .CreateLogger();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/creating-a-serilog-logger-54121682/?t=45)

#### 处理命名空间歧义

Serilog 的核心接口名为 `ILogger`。
由于 Microsoft 的日志抽象也使用 `ILogger` 这个名字,开发者可能会遇到命名冲突。
虽然两者用途相同,但它们是不同的接口。
如果两个命名空间同时存在,可以使用完全限定名 `Serilog.ILogger` 来消除歧义。

```csharp
using Serilog;
using Serilog.Core;

Serilog.ILogger logger = new LoggerConfiguration()
    .CreateLogger();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/creating-a-serilog-logger-54121682/?t=70)

#### 日志级别与方法

Serilog 提供了若干日志方法,它们与标准的 .NET 日志级别紧密对应:

*   `Verbose`:相当于 `Trace`。
*   `Debug`:相当于 `Debug`。
*   `Information`:相当于 `Information`。
*   `Warning`:相当于 `Warning`。
*   `Error`:相当于 `Error`。
*   `Fatal`:相当于 `Critical`。

此外,Serilog 还包含一个 `Write()` 方法,它在功能上等同于 Microsoft `ILogger` 接口中的 `Log()` 方法。
它还支持 `IsEnabled` 检查和 `ForContext`,后者提供与日志作用域类似的功能。

```csharp
using Serilog;
using Serilog.Core;

ILogger logger = new LoggerConfiguration()
    .CreateLogger();

logger.Information("Hello world from Serilogger");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/creating-a-serilog-logger-54121682/?t=85)

#### Sink 的概念

运行上面的代码时,控制台中不会出现任何输出。
这是因为 Serilog 需要配置"Sink"。
在 Microsoft 框架中,它们被称为提供程序(provider)。
Sink 定义了日志数据被发送到哪里(例如 Console、File 或某个云服务)。
如果没有配置至少一个 Sink,日志器会捕获信息,但没有可以输出的目的地。

## 3. The concept of Sinks

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-concept-of-sinks-54121683/) · 2:46

### 总结

Serilog 使用 Sink 的概念来定义日志目的地,提供了庞大的输出选项生态,包括控制台、文件、数据库和云服务。
Sink 通过 WriteTo 配置方法注册,使开发者可以轻松地把日志同时导向多个目的地,同时应用特定的配置,例如控制台输出的视觉主题或基于文件的日志的滚动策略。

### 核心概念

- **Sinks**:对日志目的地的抽象(例如 Console、File、SQL Server、Elasticsearch)。
- **WriteTo**:注册一个或多个 sink 的主要配置入口。
- **可扩展性**:Sink 通常作为独立的 NuGet 包分发(例如 `Serilog.Sinks.Console`)。
- **多 Sink 支持**:单个日志器只需一份配置,即可把事件广播到多个 sink。
- **Sink 配置**:各个 sink 会暴露特定的参数,例如控制台的主题或文件的滚动间隔。

### 课程笔记

Serilog 的 sink 遵循一致的命名和注册约定,因此非常容易被发现。
大多数 sink 都位于 `Serilog.Sinks.*` 命名空间下。
常见的选项包括 Console、File、Debug、MS SQL Server、Elasticsearch、Azure Blob Storage 和 AWS CloudWatch。

要注册一个 sink,请使用 `LoggerConfiguration` 对象上的 `WriteTo` 属性。
例如,要启用控制台日志,就调用控制台 sink 包提供的 `Console()` 扩展方法。

```csharp
using Serilog;
using Serilog.Core;

ILogger logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

logger.Information("Hello world!");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-concept-of-sinks-54121683/?t=10)

Sink 通常会提供额外的配置参数来增强输出。
例如,控制台 sink 支持主题,通过颜色编码提升日志的视觉清晰度。

```csharp
ILogger logger = new LoggerConfiguration()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateLogger();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-concept-of-sinks-54121683/?t=85)

Serilog 允许同时使用多个 sink。
可以链式调用多个 `WriteTo`,把日志发送到不同的位置。
使用 File sink 时,可以指定文件路径和管理策略,例如 `rollingInterval`(比如每天创建新文件)和 `rollOnFileSizeLimit`(防止单个日志文件变得太大而无法打开)。

```csharp
using Serilog;
using Serilog.Core;
using Serilog.Sinks.SystemConsole.Themes;

ILogger logger = new LoggerConfiguration()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
    .CreateLogger();

logger.Information("Hello world!");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-concept-of-sinks-54121683/?t=160)

```text
                        +--> WriteTo.Console(theme: AnsiConsoleTheme.Code)
logger.Information(...) |
                        +--> WriteTo.File("log.txt", rollingInterval: ...)
```

这种架构使得与各种监控工具(例如 Application Insights)集成变得轻而易举,只需添加相应的 sink 和一个额外的 `WriteTo` 注册即可。

## 4. The static Serilog logger

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-static-serilog-logger-54121684/) · 1:35

### 总结

Serilog 提供了一个静态的 `Log` 类,作为日志操作的全局单例。
虽然 `ILogger` 接口通常通过依赖注入来注入,但静态的 `Log.Logger` 属性对于管理日志器的生命周期至关重要,尤其是在应用程序终止之前把缓冲的日志刷新(flush)到 sink。
通过把配置好的日志器赋值给 `Log.Logger` 并调用 `Log.CloseAndFlush()`,开发者可以确保所有内存中的日志都被发送出去,从而防止在应用程序关闭或崩溃期间丢失数据。

### 核心概念

* **Log.Logger**:一个静态属性,保存全局共享的日志器实例。
* **生命周期管理**:使用静态日志器处理刷新和关闭等操作。
* **Log.CloseAndFlush()**:一个关键方法,用于确保缓冲的日志(例如发往 Application Insights 等云提供程序的内存中日志)在进程退出前被发送出去。
* **普遍可用**:静态日志器在 Serilog 内部被使用,并且在无法或不便使用依赖注入的上下文中也可以访问。

### 课程笔记

Serilog 提供了一个 `ILogger` 接口,它通常被注册到依赖注入(DI)容器中并从中注入。
不过,Serilog 还暴露了一个静态的 `Log.Logger` 属性。
这个静态日志器在整个应用程序中被全局使用,也被 Serilog 在内部用于各种后台操作。

静态日志器最关键的作用之一是管理日志的刷新。
许多日志提供程序(sink)会把日志消息缓冲在内存中以提升性能,而不是为每一条日志都发起一次 API 调用或写一次数据库。
在 DI 不可用的场景中,例如在应用程序启动、关闭期间,或者在特定的生命周期钩子中,很难确保这些缓冲的日志真的被写出。

通过创建一个日志器实例并把它赋值给静态的 `Log.Logger` 属性,你就能使用 `Log.CloseAndFlush()` 或 `Log.CloseAndFlushAsync()` 这样的生命周期方法。

```csharp
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

ILogger logger = new LoggerConfiguration()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    //.WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

Log.Logger = logger;

logger.Information("Hello world!");

Log.CloseAndFlush();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-static-serilog-logger-54121684/?t=25)

调用 `CloseAndFlush` 可以保证当前保存在内存中的任何消息(例如尚未推送的、发往 Application Insights 的日志)在应用程序关闭前被刷新出去。
在应用程序意外崩溃的情况下,这一机制确保日志器会在进程终止前尝试发送所有内存中的日志,从而防止丢失关键的诊断数据。
因此,使用 Serilog 时的一个标准模式是:配置好日志器后,立即把静态的 `Log.Logger` 设置为该实例,以简化生命周期管理并确保日志的完整性。

## 5. Simple Logger Dependency Injection

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/simple-logger-dependency-injection-54121685/) · 2:04

### 总结

本课演示如何在 Minimal API 中手动把 Serilog 日志器集成到 .NET 依赖注入(DI)容器中。
通过配置一个 Serilog ILogger 并把它注册为单例,开发者可以把它直接注入到 API 端点中。
然而,这种手动方式暴露出一个冲突:内置的 Microsoft 日志器和自定义的 Serilog 实例各自独立运行,导致控制台中的日志格式不一致且输出冗余。

### 核心概念

*   **手动 DI 注册**:在 `IServiceCollection` 中把 `Serilog.ILogger` 注册为单例,使其可以被注入。
*   **命名空间歧义**:通过使用完全限定名,解决 `Microsoft.Extensions.Logging.ILogger` 与 `Serilog.ILogger` 之间的冲突。
*   **端点注入**:在 Minimal API 路由处理程序中访问已注册的 Serilog 实例。
*   **日志共存**:理解手动注册会导致 Serilog 与默认的 .NET 日志提供程序并存运行,而不是取代它。

### 课程笔记

要把 Serilog 集成到 .NET Minimal API 中,首先必须配置日志器实例。
这一过程与控制台应用中的搭建类似,但需要处理命名空间歧义,因为 .NET 和 Serilog 都定义了一个 `ILogger` 接口。

在 `Program.cs` 文件中,初始化 `LoggerConfiguration` 并创建日志器。
为了确保 DI 容器使用正确的类型,请在注册时显式使用 `Serilog.ILogger` 接口。
创建之后,把该实例赋值给静态的 `Log.Logger` 属性,并以单例形式添加到服务集合中。

```csharp
var builder = WebApplication.CreateBuilder(args);

Serilog.ILogger logger = new LoggerConfiguration()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateLogger();

Log.Logger = logger;

builder.Services.AddSingleton(logger);

var app = builder.Build();

app.MapGet("/", () =>
{
    return "Hello World!";
});

app.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/simple-logger-dependency-injection-54121685/?t=40)

日志器注册到 DI 容器之后,就可以被注入到路由处理程序中。
通过在端点的 lambda 参数中指定 `Serilog.ILogger`,运行时会解析出 Serilog 实例,让你可以使用 `Information()` 等 Serilog 特有的方法。

```csharp
var builder = WebApplication.CreateBuilder(args);

Serilog.ILogger logger = new LoggerConfiguration()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateLogger();

Log.Logger = logger;

builder.Services.AddSingleton(logger);

var app = builder.Build();

app.MapGet("/", (Serilog.ILogger log) =>
{
    log.Information("Hello from the endpoint");
    return "Hello World!";
});

app.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/simple-logger-dependency-injection-54121685/?t=80)

虽然这种方式成功实现了注入,但它在应用程序的输出中造成了一种"冲突"的局面。
由于 Serilog 是作为独立的单例添加的,而不是作为日志提供程序集成进来的,内置的 Microsoft 日志器会继续用它自己的格式处理系统级事件(例如应用程序启动),而手动创建的 Serilog 实例只处理在端点中显式调用的日志。

```text
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:7072
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5257
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: I:\lab\courses\logging-course-final\6.Serilog\BasicMinimalApi
[15:12:25 INF] Hello from the endpoint
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/simple-logger-dependency-injection-54121685/?t=90)

在上面的输出中,以 `info:` 为前缀的行由默认的 Microsoft 日志器生成,而带有 `[15:12:25 INF]` 时间戳的那一行由 Serilog 生成。
要获得由 Serilog 处理所有应用程序日志的统一日志体验,就必须把它作为提供程序集成进来,从而有效地"劫持"内置的日志系统。

```text
 system-level events (startup) --> Microsoft logger --> "info: ..."
 endpoint log.Information()    --> Serilog.ILogger  --> "[15:12:25 INF] ..."
```

## 6. Integrating with the Microsoft Logger

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/integrating-with-the-microsoft-logger-54121686/) · 2:17

### 总结

本课演示如何把 Serilog 集成到 ASP.NET Core Web API 项目中,以取代默认的 Microsoft 日志提供程序。
通过使用 Serilog.AspNetCore 包以及宿主构建器上的 UseSerilog 扩展方法,开发者可以把所有应用程序日志和框架日志重定向到 Serilog 管道中。
这种方式让应用程序可以继续使用标准的 ILogger 接口,同时获得 Serilog 的高级功能,例如结构化日志和增强的控制台格式。

### 核心概念

*   通过 Serilog.AspNetCore NuGet 包进行集成。
*   使用 builder.Host.UseSerilog() 劫持内置的 ILogger 基础设施。
*   配置全局静态的 Log.Logger 属性。
*   保持与现有 ILogger<T> 注入的兼容性。
*   理解对标准 appsettings.json 日志配置的影响。

### 课程笔记

为了演示如何把 Serilog 集成到标准的 ASP.NET Core 环境中,我们使用一个 Web API 项目。
它提供了一个真实的场景,涉及框架级日志、依赖注入和基于控制器的日志记录。

在典型的控制器中,日志记录是通过标准的 Microsoft.Extensions.Logging.ILogger<T> 接口完成的。
这让应用程序可以与具体的日志实现保持解耦,同时在集成之后仍能受益于 Serilog 的功能。

```csharp
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.LogInformation("Retrieved weather");
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/integrating-with-the-microsoft-logger-54121686/?t=10)

要开始集成,必须安装 `Serilog.AspNetCore` 包。
这个包是专门为 ASP.NET Core 设计的,提供了替换默认日志提供程序所需的钩子。

在最初的 `Program.cs` 设置中,应用程序使用默认的构建器和服务,没有任何第三方日志提供程序:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/integrating-with-the-microsoft-logger-54121686/?t=40)

要把所有日志重定向到 Serilog,我们在 `builder.Host` 上使用 `UseSerilog()` 扩展方法。
这个方法实际上"劫持"了通用宿主(generic host)的日志系统。
现在,由框架或应用程序代码通过 `ILogger` 生成的所有日志都会流经 Serilog。

```csharp
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/integrating-with-the-microsoft-logger-54121686/?t=55)

```text
 framework ---------+
                    +--> ILogger --> builder.Host.UseSerilog() --> Serilog
 application code --+
```

Serilog 也可以在 Minimal API 中使用。
在这种情况下,如果在端点层面需要特定的 Serilog 功能,可以配置静态的 `Log.Logger`,并把 Serilog 的 `ILogger` 直接注册到依赖注入容器中。

```csharp
var builder = WebApplication.CreateBuilder(args);

Serilog.ILogger logger = new LoggerConfiguration()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateLogger();

Log.Logger = logger;

builder.Services.AddSingleton(logger);

var app = builder.Build();

app.MapGet("/", (Serilog.ILogger log) =>
{
    log.Information("Hello from the endpoint");
    return "Hello World!";
});

app.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/integrating-with-the-microsoft-logger-54121686/?t=70)

对于 Web API 项目,配置通常是在调用 `UseSerilog()` 之前先设置静态日志器来完成的。
这确保了日志管道在宿主启动时就已就绪。

```csharp
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateLogger();

builder.Host.UseSerilog();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/integrating-with-the-microsoft-logger-54121686/?t=85)

运行应用程序并访问某个端点之后,控制台输出会显示采用 Serilog 格式的结构化日志。
这既包括框架日志(例如路由匹配和端点执行),也包括应用程序日志(例如 "Retrieved weather")。
注意,Serilog 会捕获参数并应用主题,以提高可读性。

```text
[15:15:17 INF] Executing endpoint 'BasicWebApi.Controllers.WeatherForecastController.Get (BasicWebApi)'
[15:15:17 INF] Route matched with {action = "Get", controller = "WeatherForecast"}. Executing controller action System.Collections.Generic.IEnumerable`1[BasicWebApi.WeatherForecast] Get() on controller BasicWebApi.WeatherForecastController (BasicWebApi).
[15:15:17 INF] Retrieved weather
[15:15:17 INF] Executing ObjectResult, writing value of type 'BasicWebApi.WeatherForecast[]'.
[15:15:17 INF] Executed action BasicWebApi.Controllers.WeatherForecastController.Get (BasicWebApi)
[15:15:17 INF] Executed endpoint 'BasicWebApi.Controllers.WeatherForecastController.Get (BasicWebApi)'
[15:15:17 INF] Request finished HTTP/1.1 GET https://localhost:7250/weatherforecast - 200 null application/json; charset=utf-8 5.9702ms
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/integrating-with-the-microsoft-logger-54121686/?t=105)

一个需要重点考虑的问题是:一旦 Serilog 接管,`appsettings.json` 中的默认日志配置可能就不再被遵循。
Serilog 使用它自己的配置逻辑,如果你希望通过外部文件控制日志级别,就必须显式地进行设置。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/integrating-with-the-microsoft-logger-54121686/?t=115)

## 7. File-based configuration

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/file-based-configuration-54121688/) · 2:37

### 总结

Serilog 的配置既可以在代码中以流式方式管理,也可以通过 appsettings.json 等配置文件在外部管理。
虽然在代码中配置很直接,但基于文件的配置提供了更出色的灵活性,可以在运行时调整以及按环境设置,而无需修改代码。
通过使用 ReadFrom.Configuration 方法,Serilog 可以直接从 .NET 配置系统中读取设置,包括日志级别、类别覆盖(override)和 sink 定义。

### 核心概念

* 使用 `MinimumLevel` 和 `Override` 进行流式的代码内配置。
* 过渡到 `appsettings.json` 中的 `Serilog` 配置节。
* 在 JSON 中定义 `Default` 日志级别以及针对特定命名空间的覆盖。
* 在 `WriteTo` JSON 数组中配置 sink(例如 "Console")。
* 使用 `ReadFrom.Configuration(builder.Configuration)` 把 `IConfiguration` 绑定到 Serilog。

### 课程笔记

Serilog 提供了一套流式 API,可以直接在 C# 中配置日志。
基本的搭建包括:创建一个 `LoggerConfiguration`,定义一个 sink,并在调用 `builder.Host.UseSerilog()` 之前把结果赋值给 `Log.Logger`。

```csharp
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/file-based-configuration-54121688/?t=10)

为了控制日志量,可以在代码中指定最低级别和覆盖。
例如,你可能想设置一个总体级别,但把 `Microsoft.AspNetCore` 的日志限制在 `Warning` 级别,以减少噪音。

```csharp
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/file-based-configuration-54121688/?t=25)

虽然在代码中配置 Serilog 是可行的,但它缺乏外部配置的灵活性。
要把这些设置迁移到 `appsettings.json`,首先要把代码内的配置恢复到干净的状态。

```csharp
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/file-based-configuration-54121688/?t=50)

在 `appsettings.json` 文件中,你需要定义一个顶层的 `Serilog` 对象。
这个对象模仿了流式 API 的结构,让你可以指定 `MinimumLevel`(包含 `Default` 和 `Override` 属性)以及用于 sink 的 `WriteTo` 数组。
数组中的每个 sink 都由它的 `Name`(例如 "Console")来标识。

```json
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft.AspNetCore": "Warning"
      }
    },
    "WriteTo": [
      {
        "Name": "Console"
      }
    ]
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/file-based-configuration-54121688/?t=115)

```text
 Fluent API (C#)                      appsettings.json "Serilog"
 .MinimumLevel.Override(...)    <-->  "MinimumLevel": { "Default", "Override" }
 .WriteTo.Console(...)          <-->  "WriteTo": [ { "Name": "Console" } ]
```

最后,要让 Serilog 使用这些设置,请调用 `.ReadFrom.Configuration()` 并传入应用程序构建器中的配置对象。
这种方式让应用程序可以自动从 `appsettings.json`、`appsettings.Development.json` 或环境变量中加载设置。

```csharp
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/file-based-configuration-54121688/?t=130)

## 8. Log enrichment

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-enrichment-54121689/) · 2:15

Serilog 中的日志富化(enrichment)让开发者可以给每个日志事件附加额外的元数据,提供机器名、线程 ID 和应用程序特定标签等关键上下文。
这些元数据对于分布式环境中的调试和监控至关重要。
富化通过 appsettings.json 中的 Enrich 和 Properties 节进行配置,产生的结构化数据可以通过为控制台输出使用 JSON 格式化器来可视化。

### 核心概念

- **日志富化器(Log Enrichers)**:自动向日志事件添加上下文信息的组件。
- **标准富化器**:内置或由包提供的富化器,用于环境、线程和进程的详细信息。
- **静态属性**:应用于所有日志的固定元数据(例如应用程序名称)。
- **JSON 格式化**:使用 `RenderedCompactJsonFormatter` 等格式化器,在控制台中查看富化后的元数据。
- **配置驱动**:通过 `appsettings.json` 管理富化设置,以便在不同环境之间保持灵活。

### 课程笔记

Serilog 提供了一种强大的日志富化机制,让你可以给每个日志事件附加元数据。
这些元数据可以包括机器名、进程 ID 或线程 ID 等环境细节,也可以包括自定义的应用程序特定标签。
配置通常从 `appsettings.json` 中一个定义了最低级别和 sink 的标准 Serilog 设置开始。

```json
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft.AspNetCore": "Information"
      }
    },
    "WriteTo": [
      {
        "Name": "Console"
      }
    ]
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-enrichment-54121689/?t=10)

要添加富化,需要在 Serilog 配置节中使用 `Enrich` 属性。
这个数组接受富化器的名称。
常见的富化器包括 `FromLogContext`(从执行上下文中捕获属性)、`WithMachineName` 和 `WithThreadId`。

```json
"Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft.AspNetCore": "Information"
      }
    },
    "WriteTo": [
      {
        "Name": "Console"
      }
    ],
    "Enrich": ["FromLogContext", "WithMachineName", "WithThreadId"],
    "Properties":
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-enrichment-54121689/?t=40)

除了动态富化器之外,还可以指定会附加到每条日志上的静态属性。
这对于用应用程序名称或环境来标记日志很有用。

```json
"Enrich": ["FromLogContext", "WithMachineName", "WithThreadId"],
    "Properties": {
      "Application": "LoggingCourse"
    }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-enrichment-54121689/?t=70)

默认情况下,标准的控制台 sink 在其基于文本的输出中可能不会显示所有富化的元数据。
要可视化这些属性,可以为该 sink 配置一个 JSON 格式化器。
来自 `Serilog.Formatting.Compact` 程序集的 `Serilog.Formatting.Compact.RenderedCompactJsonFormatter` 是一个常见的选择,它可以生成包含所有富化属性的、结构化且机器可读的日志。

```json
"WriteTo": [
      {
        "Name": "Console",
        "Args": {
          "formatter": "Serilog.Formatting.Compact.RenderedCompactJsonFormatter, Serilog.Formatting.Compact"
        }
      }
    ],
    "Enrich": [
      "FromLogContext",
      "WithMachineName",
      "WithThreadId"
    ],
    "Properties": {
      "Application": "LoggingCourse"
    }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-enrichment-54121689/?t=80)

当应用程序使用此配置运行时,控制台输出会被转换为 JSON 对象。
现在每条日志记录都会包含富化的元数据,例如 `MachineName`、`ThreadId` 以及自定义的 `Application` 属性。

```json
{"@t":"2023-06-15T14:26:20.4410813Z","@m":"Now listening on: https://localhost:7250","@i":"d826f4b8","address":"https://localhost:7250","EventId":{"Id":14,"Name":"ListeningOnAddress"},"SourceContext":"Microsoft.Hosting.Lifetime","MachineName":"DESKTOP-LJL4Q5G","ThreadId":1,"Application":"LoggingCourse"}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-enrichment-54121689/?t=105)

## 9. Integrating with Application Insights

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/integrating-with-application-insights-54121690/) · 2:32

得益于 Serilog 基于 sink 的架构,把 Serilog 与 Azure Application Insights 集成非常简单。
这让开发者只需极少的配置改动(主要在 `appsettings.json` 文件中)就能把日志路由到 Azure。

### 核心概念

*   **Serilog Application Insights Sink**:`Serilog.Sinks.ApplicationInsights` 包让 Serilog 能够把日志事件发送到 Azure。
*   **基于配置的设置**:Sink 可以完全通过 `appsettings.json` 中的 `Serilog` 节来添加和配置。
*   **连接字符串**:从 Azure Portal 获取的唯一标识,用于把日志导向正确的 Application Insights 资源。
*   **遥测转换器(Telemetry Converter)**:Application Insights sink 所需的一个特定组件(例如 `TraceTelemetryConverter`),用于把 Serilog 日志事件转换为 Azure 遥测格式。
*   **自定义维度(Custom Dimensions)**:富化的属性(如 MachineName 或 ThreadId)会被自动映射为 Application Insights 中的自定义维度。

### 课程笔记

一开始,应用程序通常配置了一个基本的 Serilog 设置,例如写入 Console 并从日志上下文进行富化。

```json
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft.AspNetCore": "Information"
      }
    },
    "WriteTo": [
      {
        "Name": "Console"
      }
    ],
    "Enrich": [
      "FromLogContext",
      "WithMachineName",
      "WithThreadId"
    ],
    "Properties": {
      "Application": "LoggingCourse"
    }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/integrating-with-application-insights-54121690/?t=10)

要添加 Application Insights,需要在 `WriteTo` 数组中添加一个新条目。
这个条目需要一个值为 `"ApplicationInsights"` 的 `Name`,以及一个包含 `connectionString` 和 `telemetryConverter` 的 `Args` 对象。
`telemetryConverter` 是 Application Insights 特有的要求,它定义了转换如何进行。
对于标准的跟踪(trace)日志,使用的是位于 `Serilog.Sinks.ApplicationInsights` 程序集中的 `TraceTelemetryConverter`。

在下面的配置中,`Microsoft.AspNetCore` 的覆盖被设置为 `Information`,以确保捕获更多的日志,供在 Azure 门户中分析。

```json
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft.AspNetCore": "Information"
      }
    },
    "WriteTo": [
      {
        "Name": "Console"
      },
      {
        "Name": "ApplicationInsights",
        "Args": {
          "connectionString": "InstrumentationKey=3f0c4b53-89b6-4b84-811c-5659284c9071;IngestionEndpoint=https://northeurope-2.in.applicationinsights.azure.com/;LiveEndpoint=https://northeurope.livediagnostics.monitor.azure.com/",
          "telemetryConverter": "Serilog.Sinks.ApplicationInsights.TelemetryConverters.TraceTelemetryConverter, Serilog.Sinks.ApplicationInsights"
        }
      }
    ],
    "Enrich": [
      "FromLogContext",
      "WithMachineName",
      "WithThreadId"
    ],
    "Properties": {
      "Application": "LoggingCourse"
    }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/integrating-with-application-insights-54121690/?t=70)

```text
                     +--> "Console"             --> local console
 Serilog "WriteTo" --|
                     +--> "ApplicationInsights" --> Azure portal (traces)
                          Args: connectionString, telemetryConverter
```

配置完成后,运行 Web API 并产生请求,就会生成同时在本地控制台和 Azure 门户中可见的日志。

```text
[15:31:07 INF] Retrieved weather
[15:31:07 INF] Executing ObjectResult, writing value of type 'BasicWebApi.WeatherForecast[]'.
[15:31:07 INF] Executed action BasicWebApi.Controllers.WeatherForecastController.Get (BasicWebApi) in 1.4917ms
[15:31:07 INF] Executed endpoint 'BasicWebApi.Controllers.WeatherForecastController.Get (BasicWebApi)'
[15:31:07 INF] Request finished HTTP/1.1 GET https://localhost:7250/weatherforecast - 200 null application/json; charset=utf-8 7.4465ms
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/integrating-with-application-insights-54121690/?t=100)

在 Azure Portal 中,可以使用 Kusto 查询语言(KQL)查询这些日志。
富化的参数,例如 `MachineName`、`ThreadId` 以及 `Application` 等自定义属性,都会作为自定义维度保存在 Application Insights 的 `traces` 表中。

```kql
traces
| order by timestamp desc
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/integrating-with-application-insights-54121690/?t=120)

## 10. Dealing with Structured Data

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/dealing-with-structured-data-54121691/) · 3:09

Serilog 为处理结构化数据提供了成熟的机制。
来看一个表示支付的标准 C# 类:

```csharp
namespace BasicConsoleApp;

public class Payment
{
    public int PaymentId { get; set; }

    public Guid UserId { get; set; }

    public DateTime OccuredAt { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/dealing-with-structured-data-54121691/?t=10)

### 核心概念

- 用于对象序列化的解构运算符(@)。
- 用于强制输出 ToString() 的字符串化运算符($)。
- Dictionary 和集合的自动序列化。
- 在结构化日志中捕获类型信息。

### 课程笔记

在配置好 Serilog 日志器并实例化这个对象之后,记录该对象的默认行为可能并不是你所期望的。

```csharp
using BasicConsoleApp;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

ILogger logger = new LoggerConfiguration()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateLogger();

Log.Logger = logger;

var payment = new Payment
{
    PaymentId = 1,
    UserId = Guid.NewGuid(),
    OccuredAt = DateTime.UtcNow
};
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/dealing-with-structured-data-54121691/?t=25)

如果把对象传给消息模板而不使用任何特殊运算符,Serilog 会默认采用标准的 .NET 行为,即在对象上调用 `ToString()`,这通常会得到完全限定的类型名。

```csharp
logger.Information("New payment with data: {PaymentData}", payment);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/dealing-with-structured-data-54121691/?t=55)

为了捕获对象中的实际数据,Serilog 提供了解构运算符(`@`)。
通过在消息模板中的属性名前加上 `@` 前缀,Serilog 会把对象序列化为一个 JSON 对象,保留它的所有属性,甚至包括类型名。

反过来,如果你想显式地强制使用对象的字符串表示(即使 Serilog 原本可能会尝试解构它),可以使用字符串化运算符(`$`)。

```csharp
logger.Information("New payment with data: {$PaymentData}", payment);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/dealing-with-structured-data-54121691/?t=100)

```text
 {PaymentData}   --> ToString()      --> fully qualified type name
 {@PaymentData}  --> destructure (@) --> JSON object with properties + type
 {$PaymentData}  --> stringify ($)   --> string representation
```

Serilog 会自动处理某些类型,比如字典和数组。
如果把数据表示为一个字典,Serilog 会序列化其中的键值对,而不需要 `@` 运算符。

```csharp
var paymentData = new Dictionary<string, object>
{
    { "PaymentId", payment.PaymentId },
    { "UserId", payment.UserId },
    { "OccuredAt", payment.OccuredAt }
};

logger.Information("New payment with data: {PaymentData}", paymentData);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/dealing-with-structured-data-54121691/?t=130)

如果你想强制使用字典类型本身的字符串表示,可以使用字符串化运算符。

```csharp
logger.Information("New payment with data: {$PaymentData}", paymentData);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/dealing-with-structured-data-54121691/?t=145)

最后,对字典或集合使用解构运算符,会得到与自定义类型相同的类 JSON 输出。

```csharp
logger.Information("New payment with data: {@PaymentData}", paymentData);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/dealing-with-structured-data-54121691/?t=160)

## 11. Transforming Structured Data

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/transforming-structured-data-54121692/) · 1:28

### 总结

本课演示如何使用 Destructure.ByTransforming 配置来控制 Serilog 中复杂对象的序列化。
通过为特定类型定义一个转换,开发者可以确保结构化日志中只捕获相关的或安全的属性,防止意外记录敏感的或过于庞大的数据结构,例如 HttpContext。

### 核心概念

- 针对特定类型的解构
- 匿名对象投影
- 出于隐私和性能考虑的数据过滤
- 全局配置与逐次调用逻辑
- 其他解构限制(字符串长度、集合数量)

### 课程笔记

Serilog 的结构化日志能力允许把复杂对象直接序列化到日志事件中。
然而,记录 `HttpContext` 或大型领域实体这样的完整对象,可能会导致性能问题或意外暴露敏感数据。
Serilog 提供了一种机制,可以在这些对象被写入 sink 之前拦截并转换它们。

一开始,日志器配置了标准的控制台输出和一个主题。
`LoggerConfiguration` 上的 `Destructure` 属性是定义各类型在序列化过程中应如何处理的入口。

```csharp
using Serilog.Sinks.SystemConsole.Themes;

ILogger logger = new LoggerConfiguration()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .Destructure.ByTransforming()
    .CreateLogger();

Log.Logger = logger;

var payment = new Payment
{
    PaymentId = 1,
    UserId = Guid.NewGuid(),
    OccuredAt = DateTime.UtcNow
};

logger.Information("New payment with data: {@PaymentData}", payment);

Log.CloseAndFlush();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/transforming-structured-data-54121692/?t=10)

`Destructure.ByTransforming<T>` 方法让你可以为特定类型定义一个投影。
当日志器遇到一个带有 `@` 解构运算符标记的该类型对象时,它会使用所提供的转换逻辑,而不是默认的序列化。
在下面的配置中,`Payment` 对象被转换为一个只包含 `PaymentId` 和 `UserId` 属性的匿名对象。

```csharp
.Destructure.ByTransforming<Payment>(x => new
    {
        x.PaymentId, x.UserId
    })
    .CreateLogger();

Log.Logger = logger;

var payment = new Payment
{
    PaymentId = 1,
    UserId = Guid.NewGuid(),
    OccuredAt = DateTime.UtcNow
};

logger.Information("New payment with data: {@PaymentData}", payment);

Log.CloseAndFlush();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/transforming-structured-data-54121692/?t=40)

执行这条日志时,Serilog 会识别出为 `Payment` 类型注册的转换器并自动应用它。
产生的日志输出反映的是转换后的结构,而不是完整的 `Payment` 类,从而有效地过滤掉了 `OccuredAt` 属性:

```text
[15:40:12 INF] New payment with data: {"PaymentId": 1, "UserId": "99b7fb50-d0f4-461e-a70c-ac706f3bb6c3"}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/transforming-structured-data-54121692/?t=55)

```text
 Payment { PaymentId, UserId, OccuredAt }
   |
   |  .Destructure.ByTransforming<Payment>(x => new { x.PaymentId, x.UserId })
   v
 {"PaymentId": 1, "UserId": "..."}
```

Serilog 还支持其他解构约束。
例如,可以使用 `ToMaximumStringLength` 限制所记录字符串的最大长度,或者使用 `ToCollectionCount` 限制从列表中捕获的项数。
这些配置有助于在仍然提供必要上下文的同时,把日志大小保持在可控范围内。

```csharp
using BasicConsoleApp;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

ILogger logger = new LoggerConfiguration()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .Destructure.ByTransforming<Payment>(x => new
    {
        x.PaymentId, x.UserId
    })
    .CreateLogger();

Log.Logger = logger;

var payment = new Payment
{
    PaymentId = 1,
    UserId = Guid.NewGuid(),
    OccuredAt = DateTime.UtcNow
};
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/transforming-structured-data-54121692/?t=70)

## 12. Manually enriching logs

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/manually-enriching-logs-54121693/) · 2:05

Serilog 中的手动富化让开发者可以在特定作用域内给日志条目附加上下文属性,这与 Microsoft 的日志作用域功能相对应。
通过在 `using` 块中使用 `LogContext.PushProperty`,可以把属性添加到日志上下文中,并自动包含在该块内生成的所有日志里。
这种方式把元数据从消息字符串移到结构化日志属性中,使消息模板更简洁,这在使用 JSON 控制台格式化器等结构化 sink 时尤其有效。

### 核心概念

- **LogContext.PushProperty**:向当前日志上下文添加属性的主要方法。
- **Enrich.FromLogContext()**:让 Serilog 能够从 `LogContext` 捕获属性所必需的配置。
- **基于作用域的富化**:使用 `using` 块来定义富化属性的生命周期。
- **结构化日志重构**:把数据从消息模板移到上下文属性中,在保留元数据的同时简化日志消息。

### 课程笔记

Serilog 提供了一种手动日志富化机制,它在功能上等同于内置 Microsoft 日志器中的日志作用域。
首先,来看一个使用控制台 sink 并为 `Payment` 对象自定义解构的标准 Serilog 配置。

```csharp
using BasicConsoleApp;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

ILogger logger = new LoggerConfiguration()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .Destructure.ByTransforming<Payment>(x => new
    {
        x.PaymentId, x.UserId
    })
    .CreateLogger();

Log.Logger = logger;

var payment = new Payment
{
    PaymentId = 1,
    UserId = Guid.NewGuid(),
    OccuredAt = DateTime.UtcNow
};
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/manually-enriching-logs-54121693/?t=10)

手动富化是通过 `Serilog.Context` 命名空间中的 `LogContext.PushProperty` 方法实现的。
这个方法会在 `using` 块的持续期间,把一个属性附加到当前日志上下文上。
一开始,它可能会与一个仍然显式包含该属性的消息模板一起使用。

```csharp
var payment = new Payment
{
    PaymentId = 1,
    UserId = Guid.NewGuid(),
    OccuredAt = DateTime.UtcNow
};

using (LogContext.PushProperty("PaymentId", payment.PaymentId))
{
    
}

logger.Information(
    "Received payment with id {PaymentId} by user with id {UserId}",
    payment.PaymentId, payment.UserId);

Log.CloseAndFlush();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/manually-enriching-logs-54121693/?t=40)

这种方式的主要优点是可以重构日志消息。
由于该属性现在已经是上下文的一部分,就可以把它从消息模板中移除,而它仍会被捕获到结构化日志输出中。
这让日志消息保持简洁,同时确保元数据仍可用于过滤和分析。

```csharp
var payment = new Payment
{
    PaymentId = 1,
    UserId = Guid.NewGuid(),
    OccuredAt = DateTime.UtcNow
};

using (LogContext.PushProperty("PaymentId", payment.PaymentId))
{
    logger.Information(
        "Received payment by user with id {UserId}",
        payment.UserId);
}

Log.CloseAndFlush();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/manually-enriching-logs-54121693/?t=55)

```text
 using (LogContext.PushProperty("PaymentId", ...))  -- scope start
 |
 |   logger.Information("... {UserId}", ...)
 |      -> log event: UserId + PaymentId (from LogContext)
 |
 }                                                  -- scope end
```

要让 Serilog 在输出中包含这些属性,日志器必须配置 `FromLogContext` 富化器。
此外,使用 `JsonFormatter` 这样的结构化格式化器,可以把这些富化的参数作为最终日志条目中独立的字段展示出来。

```csharp
using BasicConsoleApp;
using Serilog;
using Serilog.Context;
using Serilog.Formatting.Json;
using Serilog.Sinks.SystemConsole.Themes;

ILogger logger = new LoggerConfiguration()
    .WriteTo.Console(new JsonFormatter())
    .Enrich.FromLogContext()
    .Destructure.ByTransforming<Payment>(x => new
    {
        x.PaymentId, x.UserId
    })
    .CreateLogger();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/manually-enriching-logs-54121693/?t=70)

虽然 `LogContext` 是一个静态类,但对日志逻辑做单元测试时,使用它通常不是问题。
如果需要严格的隔离,可以把这些静态调用包装在一个适配器中,并通过接口注入。
此外,`PushProperty` 支持一个可一次推送多个属性的重载,便于把复杂状态传入日志上下文。

Serilog 还为计时操作提供了专门的处理方式,它为与性能相关的日志提供了手动富化之外的另一种选择。

## 13. Timed Operations

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/timed-operations-54121694/) · 2:51

与标准 Microsoft 日志抽象所需的手动 stopwatch 方式相比,Serilog 提供了一种更集成的方式来处理计时操作。
这一功能由 `SerilogTimings` NuGet 包提供,它为 `ILogger` 接口添加了扩展方法,以自动捕获并记录操作耗时。

一开始,一条不带计时的标准日志条目可能是这样的:

```csharp
var payment = new Payment
{
    PaymentId = 1,
    UserId = Guid.NewGuid(),
    OccuredAt = DateTime.UtcNow
};

logger.Information(
    "Received payment with id {PaymentId} by user with id {UserId}",
    payment.PaymentId, payment.UserId);

Log.CloseAndFlush();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/timed-operations-54121694/?t=10)

### 使用 TimeOperation 自动计时

要测量一个操作花费了多长时间,可以在 `using` 块中使用 `TimeOperation` 扩展方法。
这为你想要测量的操作定义了一个作用域。

```csharp
var payment = new Payment
{
    PaymentId = 1,
    UserId = Guid.NewGuid(),
    OccuredAt = DateTime.UtcNow
};

using (logger.TimeOperation("Processing payment with id {PaymentId}", payment.PaymentId))
{
    await Task.Delay(50);
    logger.Information("Received payment by user with id {UserId}", payment.UserId);
}

Log.CloseAndFlush();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/timed-operations-54121694/?t=70)

当 `using` 块被释放(dispose)时,Serilog 会自动计算经过的时间并发出一条日志。
例如,上面的代码会产生一条这样的日志:"Processing payment with id 1 completed in 68.6 milliseconds."

### 使用 BeginOperation 手动控制

对于需要更细粒度地控制操作何时开始和结束,或者需要显式定义结果的场景,Serilog 提供了 `BeginOperation` 方法。
它返回一个可以手动完成的操作对象。

```csharp
var payment = new Payment
{
    PaymentId = 1,
    UserId = Guid.NewGuid(),
    OccuredAt = DateTime.UtcNow
};

var op = logger.BeginOperation("Processing payment with id {PaymentId}", payment.PaymentId);
await Task.Delay(50);
logger.Information("Received payment by user with id {UserId}", payment.UserId);
op.Complete();

Log.CloseAndFlush();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/timed-operations-54121694/?t=115)

调用 `op.Complete()` 会把该操作视为成功,并以 `Information` 级别记录它。

### 放弃操作与阈值

如果出了问题或操作被取消,可以调用 `op.Abandon()`。
这种方式让你可以在日志中区分成功的操作和失败的操作。

```csharp
var payment = new Payment
{
    PaymentId = 1,
    UserId = Guid.NewGuid(),
    OccuredAt = DateTime.UtcNow
};

var op = logger.BeginOperation("Processing payment with id {PaymentId}", payment.PaymentId);
await Task.Delay(50);
logger.Information("Received payment by user with id {UserId}", payment.UserId);
op.Abandon();

Log.CloseAndFlush();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/timed-operations-54121694/?t=130)

被放弃的操作会自动以 `Warning` 而不是 `Information` 级别记录。
此外,`SerilogTimings` 可以配置阈值;如果某个操作耗时超出预期,它可以被自动提升到警告级别,从而实现主动的性能监控。

```text
 var op = logger.BeginOperation(...)
          |
          +--> op.Complete()  --> Information (success)
          |
          +--> op.Abandon()   --> Warning
```

## 14. Masking sensitive logs

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/masking-sensitive-logs-54121695/) · 3:06

对电子邮件地址、密码或 IP 地址等敏感信息进行掩码处理,是日志记录中的一项关键要求,尤其是因为日志一旦存储到 Application Insights 或 Datadog 等外部系统中,往往就不可更改了。
Serilog 通过 Destructurama.Attributed 库为此提供了一个可靠的解决方案,它让你可以使用特性(attribute)在属性级别控制数据的解构。
通过用 [LogMasked] 或 [NotLogged] 等特性修饰属性,开发者可以在不改变原始对象结构的情况下,混淆敏感数据或把它从记录的输出中完全移除。

### 核心概念

*   **Destructurama.Attributed**:一个 Serilog 扩展,允许使用特性来控制对象如何被解构为日志属性。
*   **[LogMasked]**:用于混淆属性值的特性。它可以通过指定的掩码文本、保留长度,或显示原始数据的前缀来定制。
*   **[NotLogged]**:把某个属性从解构后的日志对象中完全排除的特性。
*   **不可变日志**:日志一旦写入某个提供程序(如 Application Insights),就无法轻易修改,因此在存储之前进行掩码处理至关重要。

### 课程笔记

使用 `@` 解构运算符记录复杂对象时,电子邮件地址等敏感数据可能会在无意中以明文形式被存储。

```csharp
namespace BasicConsoleApp;

public class Payment
{
    public int PaymentId { get; set; }

    public string Email { get; set; }

    public Guid UserId { get; set; }

    public DateTime OccuredAt { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/masking-sensitive-logs-54121695/?t=70)

为了解决这个问题,可以使用 `Destructurama.Attributed` 包。
安装该包之后,必须在 `LoggerConfiguration` 中使用 `.Destructure.UsingAttributes()` 方法注册它。
这会告诉 Serilog 在解构过程中查找对象上的特定特性。

```csharp
ILogger logger = new LoggerConfiguration()
    .WriteTo.NickSink()
    .Enrich.FromLogContext()
    .Destructure.UsingAttributes()
    .CreateLogger();

Log.Logger = logger;

var payment = new Payment
{
    PaymentId = 1,
    Email = "nick@dometrain.com",
    UserId = Guid.NewGuid(),
    OccuredAt = DateTime.UtcNow
};

logger.Information("Received payment with details {@PaymentData}", payment);

Log.CloseAndFlush();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/masking-sensitive-logs-54121695/?t=85)

#### 掩码选项

`[LogMasked]` 特性提供了几种混淆数据的方式。
默认情况下,它会把值替换为三个星号(`***`)。
可以使用 `Text` 属性自定义替换文本。

```csharp
using Destructurama.Attributed;

namespace BasicConsoleApp;

public class Payment
{
    public int PaymentId { get; set; }

    [LogMasked(Text = "_MASK")]
    public string Email { get; set; }

    public Guid UserId { get; set; }

    public DateTime OccuredAt { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/masking-sensitive-logs-54121695/?t=100)

如果出于调试目的需要保持原始数据的视觉长度,可以设置 `PreserveLength = true`。
这会把原始字符串中的每个字符都替换为一个星号。

```csharp
public class Payment
{
    public int PaymentId { get; set; }

    [LogMasked(PreserveLength = true)]
    public string Email { get; set; }

    public Guid UserId { get; set; }

    public DateTime OccuredAt { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/masking-sensitive-logs-54121695/?t=115)

#### 排除属性

如果某个属性根本不应该出现在日志中(例如密码),请使用 `[NotLogged]` 特性。
这会把该属性从日志输出中的解构对象里完全移除。

```csharp
public class Payment
{
    public int PaymentId { get; set; }

    [NotLogged]
    public string Email { get; set; }

    public Guid UserId { get; set; }

    public DateTime OccuredAt { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/masking-sensitive-logs-54121695/?t=135)

#### 部分掩码

还可以使用 `ShowFirst` 属性显示数据的一部分,同时对其余部分进行掩码。
这通常有助于识别记录(例如看到电子邮件的前几个字符),而不暴露完整的敏感值。
它可以与 `PreserveLength` 组合使用,同时提供上下文和长度信息。

```csharp
public class Payment
{
    public int PaymentId { get; set; }

    [LogMasked(ShowFirst = 3, PreserveLength = true)]
    public string Email { get; set; }

    public Guid UserId { get; set; }

    public DateTime OccuredAt { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/masking-sensitive-logs-54121695/?t=180)

产生的日志输出会显示前三个字符,后面跟着与原始字符串剩余长度相匹配的星号:

```csharp
[16:35:34 INF] Received payment with details {"PaymentId": 1, "Email": "nic****************", "UserId": "e9f9b287-9b6f-449d-ab95-5cf1dd8a4ce9", "OccuredAt": "2023-06-15T15:35:34.5987682Z", "$type": "Payment"}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/masking-sensitive-logs-54121695/?t=160)

## 15. Asynchronous processing

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/asynchronous-processing-54121696/) · 2:31

### 总结

Serilog 通过 Serilog.Sinks.Async 包提供异步日志能力,允许包装任何 sink(包括 Console 这样的同步 sink)以进行后台处理。
这种方式通过缓冲日志事件,并定期或在应用程序关闭时刷新它们,防止日志操作阻塞应用程序的主执行流程。
为了确保所有缓冲的日志在进程终止前被成功写出,开发者必须显式调用 Log.CloseAndFlush()。

### 核心概念

- **同步阻塞**:.NET 中的标准日志方法是同步的,这意味着调用线程可能需要等待日志提供程序处理该条目。
- **异步包装器**:`Serilog.Sinks.Async` 包允许包装任何现有的 sink,把 I/O 工作转移到后台线程。
- **日志缓冲**:异步 sink 使用一个内部缓冲区来存放日志事件,然后再把它们推送到最终目的地。
- **生命周期管理**:使用异步处理时,调用 `Log.CloseAndFlush()` 对于防止应用程序关闭期间丢失日志至关重要。
- **缓冲区配置**:开发者可以通过设置缓冲区上限以及定义缓冲区达到容量时的行为来调优性能。

### 课程笔记

在标准的 .NET 日志中,日志方法是同步的。
没有内置的 `LogInformationAsync`,因为日志基础设施通常期望提供程序自行处理性能问题。
然而,许多 sink 默认以同步方式执行工作。
例如,Console sink 直接写入标准输出流,这可能会在 I/O 操作完成期间阻塞应用程序的执行。

为了缓解这一问题,Serilog 通过 `Serilog.Sinks.Async` 包提供了一个包装器 sink。
它让你可以给任何 sink 添加异步能力,即使是那些原生不支持异步的 sink。
通过使用 `.Async()` 配置方法,可以包装一个 sink 的注册,使日志事件被移交给一个后台工作者。

```csharp
ILogger logger = new LoggerConfiguration()
    .WriteTo.Async(x => )
    .Enrich.FromLogContext()
    .Destructure.UsingAttributes()
    .CreateLogger();

Log.Logger = logger;

var payment = new Payment
{
    PaymentId = 1,
    Email = "nick@dometrain.com",
    UserId = Guid.NewGuid(),
    OccuredAt = DateTime.UtcNow
};

logger.Information("Received payment with details {@PaymentData}", payment);

//Log.CloseAndFlush();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/asynchronous-processing-54121696/?t=55)

当一个 sink 被包装在异步提供程序中时,日志会被缓冲。
这意味着,如果你运行应用程序,而它在一次日志调用之后立即终止,你可能在控制台中看不到任何输出。
日志被保存在内存中,并定期或在满足某些条件时被推送出去。

```csharp
using BasicConsoleApp;
using Destructurama;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using SerilogTimings.Extensions;

ILogger logger = new LoggerConfiguration()
    .WriteTo.Async(x => x.Console(theme:AnsiConsoleTheme.Code))
    .Enrich.FromLogContext()
    .Destructure.UsingAttributes()
    .CreateLogger();

Log.Logger = logger;

var payment = new Payment
{
    PaymentId = 1,
    Email = "nick@dometrain.com",
    UserId = Guid.NewGuid(),
    OccuredAt = DateTime.UtcNow
};
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/asynchronous-processing-54121696/?t=95)

```text
 logger.Information(...)
        |
        v
 WriteTo.Async(...)  -- buffer (in memory) --> background worker
                                                    |
                                                    v
                                             x.Console(...)
```

可以通过配置缓冲区来定制异步行为。
例如,可以设置特定的缓冲区大小,来控制在刷新之前有多少日志保存在内存中,或者决定缓冲区满时应用程序是否应该阻塞。
在长时间运行的应用程序中,Serilog 会确保日志被定期推送到 sink。

```csharp
using BasicConsoleApp;
using Destructurama;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using SerilogTimings.Extensions;

ILogger logger = new LoggerConfiguration()
    .WriteTo.Async(x => x.Console(theme:AnsiConsoleTheme.Code), 10)
    .Enrich.FromLogContext()
    .Destructure.UsingAttributes()
    .CreateLogger();

Log.Logger = logger;

var payment = new Payment
{
    PaymentId = 1,
    Email = "nick@dometrain.com",
    UserId = Guid.NewGuid(),
    OccuredAt = DateTime.UtcNow
};
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/asynchronous-processing-54121696/?t=130)

由于这种缓冲机制,在应用程序关闭过程中调用 `Log.CloseAndFlush()` 是必不可少的。
这个调用会通知所有已注册的 sink(尤其是异步 sink),在进程退出之前刷新它们缓冲区中剩余的日志事件。

```csharp
var payment = new Payment
{
    PaymentId = 1,
    Email = "nick@dometrain.com",
    UserId = Guid.NewGuid(),
    OccuredAt = DateTime.UtcNow
};

logger.Information("Received payment with details {@PaymentData}", payment);

Log.CloseAndFlush();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/asynchronous-processing-54121696/?t=10)

## 16. Creating your own Sink

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/creating-your-own-sink-54121698/) · 3:29

### 总结

在 Serilog 中创建自定义 sink,需要实现 ILogEventSink 接口,而该接口只要求实现一个 Emit 方法。
这个过程通常比为标准 .NET 日志抽象实现自定义提供程序更直接。
借助 LogEvent 对象的 RenderMessage 方法,并提供一个可选的 IFormatProvider,开发者可以轻松地格式化日志数据并把它路由到任何目的地。
为了提供与内置 sink 类似的无缝配置体验,开发者通常会把 sink 的注册包装在 LoggerSinkConfiguration 的一个扩展方法中,使该 sink 可以通过流式的 WriteTo API 添加。

### 核心概念

* **ILogEventSink**:创建自定义 Serilog sink 的核心接口。
* **Emit 方法**:该接口要求实现的唯一方法,用于处理日志事件。
* **LogEvent.RenderMessage**:一个内置方法,用于把日志事件的属性和模板转换为格式化后的字符串。
* **IFormatProvider**:一个可选的依赖项,用于在渲染消息时处理与区域性(culture)相关的格式化。
* **LoggerSinkConfiguration 扩展**:为自定义 sink 提供简洁、流式注册 API 的一种模式。

### 课程笔记

要在 Serilog 中创建自定义 sink,必须实现 `ILogEventSink` 接口。
这个接口比标准 .NET 日志抽象中基于提供程序的体系更简单,因为它只要求实现一个方法:`Emit`。

```csharp
using Serilog.Core;
using Serilog.Events;

namespace BasicConsoleApp;

public class NickSink : ILogEventSink
{
    public void Emit(LogEvent logEvent)
    {
        
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/creating-your-own-sink-54121698/?t=25)

实现 sink 时,支持 `IFormatProvider` 是标准做法。
这让 sink 在渲染日志消息时可以遵循与区域性相关的格式。
你应该提供一个接受该提供程序的构造函数,并可选地提供一个把提供程序默认设为 `null` 的无参构造函数。

```csharp
namespace BasicConsoleApp;

public class NickSink : ILogEventSink
{
    private readonly IFormatProvider? _formatProvider;

    public NickSink(IFormatProvider? formatProvider)
    {
        _formatProvider = formatProvider;
    }

    public void Emit(LogEvent logEvent)
    {
        
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/creating-your-own-sink-54121698/?t=40)

在 `Emit` 方法内部,`LogEvent` 对象提供了关于该日志条目的所有必要信息。
要获得人类可读的消息,请调用 `logEvent.RenderMessage(_formatProvider)`。
这个方法会自动把占位符替换为对应的属性值。
消息渲染完成后,就可以把它输出到你想要的目的地,例如控制台。

```csharp
public class NickSink : ILogEventSink
{
    private readonly IFormatProvider? _formatProvider;

    public NickSink(IFormatProvider? formatProvider)
    {
        _formatProvider = formatProvider;
    }
    
    public NickSink() : this(null)
    {
    }

    public void Emit(LogEvent logEvent)
    {
        var message = logEvent.RenderMessage(_formatProvider);
        Console.WriteLine($"{DateTime.UtcNow} - {message}");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/creating-your-own-sink-54121698/?t=70)

要使用这个 sink,可以在 `LoggerConfiguration` 中通过 `Sink` 方法手动注册它。
这需要直接实例化该 sink 类。

```csharp
//.WriteTo.Async(x => x.Console(theme:AnsiConsoleTheme.Code), 10)
.WriteTo.Sink(new NickSink())
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/creating-your-own-sink-54121698/?t=130)

为了提供与 Serilog 内置 sink 一致、更符合惯用法的体验,你应该为 `LoggerSinkConfiguration` 创建一个扩展方法。
这让使用者可以直接在 `WriteTo` 下用一个描述性的方法名来注册你的 sink。

```csharp
public static class NickSinkExtensions
{
    public static LoggerConfiguration NickSink(
        this LoggerSinkConfiguration sinkConfiguration,
        IFormatProvider? formatProvider = null)
    {
        return sinkConfiguration.Sink(new NickSink(formatProvider));
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/creating-your-own-sink-54121698/?t=175)

定义好扩展方法之后,就可以把自定义 sink 以流式方式集成到 `LoggerConfiguration` 中。

```csharp
ILogger logger = new LoggerConfiguration()
    //.WriteTo.Async(x => x.Console(theme:AnsiConsoleTheme.Code), 10)
    .WriteTo.NickSink()
    .Enrich.FromLogContext()
    .Destructure.UsingAttributes()
    .CreateLogger();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/creating-your-own-sink-54121698/?t=190)

```text
 .WriteTo.NickSink()
     |  NickSinkExtensions.NickSink(this LoggerSinkConfiguration, ...)
     v
 sinkConfiguration.Sink(new NickSink(formatProvider))
     |
     v
 NickSink.Emit(LogEvent logEvent)
     |  logEvent.RenderMessage(_formatProvider)
     v
 Console.WriteLine($"{DateTime.UtcNow} - {message}")
```

## 17. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121699/) · 1:11

### 总结

本课对 Serilog 这一节进行了全面回顾,突出了它作为 .NET 强大诊断日志库的角色。
它回顾了从基础搭建到高级功能的过渡,包括使用 sink 进行输出、使用静态日志器进行全局管理,以及与 ASP.NET Core 的集成。
回顾还涵盖了结构化数据处理、解构、元数据富化、计时操作、PII 掩码,以及自定义异步 sink 的实现。

### 核心概念

- Serilog Sinks 与静态日志器
- 通过 `UseSerilog` 集成 ASP.NET Core
- 基于文件的配置与富化器
- 结构化数据与解构
- 计时操作与 PII 掩码
- 异步日志与自定义 Sink 实现

### 课程笔记

Serilog 是一个以结构化数据为优先的诊断日志库。
与默认的 .NET 日志提供程序不同,Serilog 需要显式配置"Sink"(即控制台、文件或云提供程序等目标)才能输出日志数据。

该库使用一个静态日志器实例 `Log.Logger`,它可以在应用程序启动时进行配置。
必须使用 `Log.CloseAndFlush()`,以确保所有缓冲的日志事件在应用程序退出前被成功写入它们的 sink。

```csharp
using BasicConsoleApp;
using Destructurama;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using SerilogTimings.Extensions;

ILogger logger = new LoggerConfiguration()
    .WriteTo.NickSink()
    .Enrich.FromLogContext()
    .Destructure.UsingAttributes()
    .CreateLogger();

Log.Logger = logger;

var payment = new Payment
{
    PaymentId = 1,
    Email = "nick@dometrain.com",
    UserId = Guid.NewGuid(),
    OccuredAt = DateTime.UtcNow
};

logger.Information("Received payment with details {@PaymentData}", payment);

Log.CloseAndFlush();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121699/?t=18)

Serilog 通过替换内置的日志提供程序,与 ASP.NET Core 无缝集成。
通过调用 `builder.Host.UseSerilog()`,开发者可以把所有框架日志和应用程序日志路由到 Serilog,同时在整个应用程序中继续使用标准的 `ILogger<T>` 依赖注入。

```csharp
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// ...
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121699/?t=27)

配置可以外部化到 `appsettings.json`,从而支持按环境设置日志级别和 sink 设置。
为了给日志添加上下文,Serilog 使用"富化器(Enricher)",它会自动把机器名、进程 ID 或线程 ID 等元数据附加到每个日志事件上。

结构化日志是 Serilog 的核心优势。
借助解构(在消息模板中通常用 `@` 运算符表示),复杂对象可以作为可搜索的数据结构被记录下来,而不是简单的字符串。
掩码(用于保护敏感的个人身份信息,即 PII)以及用于跟踪执行时长的计时操作等功能,又进一步增强了这一点。

```csharp
using Destructurama.Attributed;

namespace BasicConsoleApp;

public class Payment
{
    public int PaymentId { get; set; }

    [LogMasked(ShowFirst = 3, PreserveLength = true)]
    public string Email { get; set; }
    
    public Guid UserId { get; set; }

    public DateTime OccuredAt { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121699/?t=56)

对于对性能敏感的环境,Serilog 支持异步处理,以尽量减少日志操作对性能的影响。
此外,开发者可以通过实现 `ILogEventSink` 接口来创建自定义 sink,从而扩展该库,以特定的方式处理日志。

```csharp
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;

namespace BasicConsoleApp;

public class NickSink : ILogEventSink
{
    private readonly IFormatProvider? _formatProvider;

    public NickSink(IFormatProvider? formatProvider)
    {
        _formatProvider = formatProvider;
    }
    
    public NickSink() : this(null)
    {
    }

    public void Emit(LogEvent logEvent)
    {
        var message = logEvent.RenderMessage(_formatProvider);
        Console.WriteLine($"{DateTime.UtcNow} - {message}");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121699/?t=61)
