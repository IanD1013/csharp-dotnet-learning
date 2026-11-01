# Deep diving into the Logger

> 课程:[From Zero to Hero: Logging in .NET](https://dometrain.com/course/from-zero-to-hero-logging-in-dotnet/) · 第 4 章
> 共 7 课 · 约 23:45
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [The logging packages](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-logging-packages-54121627/) | 5:27 | [↓](#1-the-logging-packages) |
| 2 | [Host vs non-host console app](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/host-vs-non-host-console-app-54121628/) | 3:52 | [↓](#2-host-vs-non-host-console-app) |
| 3 | [Customizing the loggers and providers](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/customizing-the-loggers-and-providers-54121629/) | 3:09 | [↓](#3-customizing-the-loggers-and-providers) |
| 4 | [Log category configuration](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-category-configuration-54121630/) | 4:32 | [↓](#4-log-category-configuration) |
| 5 | [Dealing with exceptions](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/dealing-with-exceptions-54121631/) | 1:52 | [↓](#5-dealing-with-exceptions) |
| 6 | [What are Log filters?](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/what-are-log-filters-54121632/) | 4:03 | [↓](#6-what-are-log-filters) |
| 7 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121633/) | 0:50 | [↓](#7-section-recap) |

## 1. The logging packages

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-logging-packages-54121627/) · 5:27

本课探讨 .NET 日志生态的 NuGet 包结构,区分抽象层、核心实现和具体的日志提供程序(logging provider)。
它解释了 Web 项目如何通过 Web SDK 隐式引用这些依赖,而控制台应用则需要显式配置 Microsoft.Extensions.Logging 及其相关提供程序等包。
本课强调了抽象包对于库作者保持解耦架构的重要性,并演示了把 NullLoggerProvider 用作安全默认值。

### 核心概念

*   **隐式依赖与显式依赖(Implicit vs. Explicit Dependencies)**:使用 Web SDK 的 Web 项目会自动包含日志程序集,而标准控制台项目则不会。
*   **Microsoft.Extensions.Logging.Abstractions**:包含 `ILogger` 和 `ILoggerFactory` 等核心接口。非常适合库作者使用,以免把特定实现强加给使用者。
*   **Microsoft.Extensions.Logging**:包含核心实现逻辑,其中包括 `LoggerFactory` 类。
*   **日志提供程序(Logging Providers)**:具体的实现(Console、Debug、EventLog 等),各自位于专属的 NuGet 包中。
*   **NullLoggerProvider**:一个不执行任何操作的提供程序,在没有提供其他日志实现时用作安全默认值。

### 课程笔记

#### Web 项目中的隐式依赖

在 .NET Web 项目中,无需显式添加 NuGet 包就可以使用 `ILogger` 接口。
这是项目 SDK 的缘故。
当项目使用 `Microsoft.NET.Sdk.Web` 时,它会隐式引用日志、配置和依赖注入所需的程序集。

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

    <PropertyGroup>
        <TargetFramework>net8.0</TargetFramework>
        <Nullable>enable</Nullable>
        <ImplicitUsings>enable</ImplicitUsings>
    </PropertyGroup>

</Project>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-logging-packages-54121627/?t=40)

这些隐式引用包括 `Microsoft.Extensions.Logging`、`Microsoft.Extensions.Logging.Abstractions`,以及 Console、Debug、EventLog、EventSource 和 TraceSource 等若干提供程序。

#### 控制台应用中的显式依赖

与 Web 项目不同,使用 `Microsoft.NET.Sdk` 的标准控制台应用需要显式的 NuGet 包引用才能启用日志。
要实现与 Web 项目相同的功能,你必须手动添加核心日志包以及打算使用的具体提供程序。

```xml
<Project Sdk="Microsoft.NET.Sdk">

    <PropertyGroup>
        <OutputType>Exe</OutputType>
        <TargetFramework>net8.0</TargetFramework>
        <ImplicitUsings>enable</ImplicitUsings>
        <Nullable>enable</Nullable>
    </PropertyGroup>

    <ItemGroup>
      <PackageReference Include="Microsoft.Extensions.Logging" Version="7.0.0" />
      <PackageReference Include="Microsoft.Extensions.Logging.Console" Version="7.0.0" />
      <PackageReference Include="Microsoft.Extensions.Logging.EventLog" Version="7.0.0" />
    </ItemGroup>

</Project>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-logging-packages-54121627/?t=130)

#### 抽象包

`Microsoft.Extensions.Logging.Abstractions` 包是一个轻量级依赖,只包含接口(如 `ILogger`),不包含实现。
这对库作者尤其有用。
通过只依赖抽象,库可以要求一个 `ILogger`,而不必把特定的日志实现或提供程序强加给最终用户。
这与 .NET 依赖注入中 `IServiceProvider` 和 `IServiceCollection` 抽象所采用的做法如出一辙。

如果只安装抽象包,你可以使用 `ILogger` 接口,但不会有 `LoggerFactory`,也无法直接创建 logger。

#### 核心实现与提供程序

要真正创建和使用 logger,你需要 `Microsoft.Extensions.Logging` 包,它提供了 `LoggerFactory`。
然而,即使有了核心包,你仍然缺少具体的输出目标(提供程序)。
例如,在安装 `Microsoft.Extensions.Logging.Console` 包之前,`AddConsole()` 扩展方法是不可用的。

如果没有配置任何提供程序,你可以使用抽象包中的 `NullLoggerProvider`。
它是一个单例,作为安全的空实现,在调用日志方法时不执行任何操作。

```csharp
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddProvider(NullLoggerProvider.Instance);
});

ILogger logger = loggerFactory.CreateLogger<Program>();

var name = "Nick";
var age = 30;

logger.LogInformation("{Name} just turned: {Age}", name, age);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-logging-packages-54121627/?t=280)

一旦添加了相应的提供程序包(例如 `.Console`),对应的扩展方法就会出现在 `ILoggingBuilder` 上。

```csharp
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
});

ILogger logger = loggerFactory.CreateLogger<Program>();

var name = "Nick";
var age = 30;

logger.LogInformation("{Name} just turned: {Age}", name, age);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-logging-packages-54121627/?t=310)

```text
Microsoft.Extensions.Logging.Abstractions   ILogger, ILoggerFactory,
                                            NullLoggerProvider
              ^
              |
Microsoft.Extensions.Logging                LoggerFactory
              ^
              |
Microsoft.Extensions.Logging.Console        AddConsole()
Microsoft.Extensions.Logging.EventLog
```

## 2. Host vs non-host console app

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/host-vs-non-host-console-app-54121628/) · 3:52

### 总结

本课探讨非宿主(non-host)控制台应用与使用 .NET Generic Host 或 Web Application builder 的宿主应用之间的区别:在前者中,开发者手动管理 LoggerFactory。
手动方式提供最直接的控制,被视为日志的"原始"版本,而宿主抽象则提供内置的依赖注入和预先配置好的日志服务,适用于 Web API 和长时间运行的后台服务。
理解这两种方式,可以确保在任何 .NET 项目类型中都能有效地实现日志,从简单脚本到复杂的微服务皆是如此。

### 核心概念

*   **非宿主方式(Non-host approach)**:在简单的控制台应用中手动创建和管理 `LoggerFactory`,以获得完全的控制。
*   **Web Application Host**:使用 `WebApplication.CreateBuilder(args)`,它会为 ASP.NET Core 项目隐式配置日志及其他服务。
*   **Generic Host**:为需要依赖注入但不一定需要 Web 服务器的长时间运行服务或后台 worker 使用 `IHost` 和 `Host.CreateDefaultBuilder(args)`。
*   **ConfigureLogging**:宿主 builder 上的一个扩展方法,用于在宿主环境中添加日志提供程序和过滤器。
*   **服务解析(Service Resolution)**:从宿主内置的依赖注入(DI)容器中获取 `ILogger` 实例。

### 课程笔记

在 .NET 中,日志既可以通过手动管理日志基础设施来实现,也可以借助内置的宿主抽象来实现。
最直接的,或者说"原始"的方法是直接使用 `LoggerFactory`。
这在非宿主控制台应用中很典型,因为你希望完全控制 factory 的生命周期。

```csharp
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
});

ILogger logger = loggerFactory.CreateLogger<Program>();

var name = "Nick";
var age = 30;

logger.LogInformation("{Name} just turned: {Age}", name, age);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/host-vs-non-host-console-app-54121628/?t=10)

另一种方式是,现代 .NET Web 应用使用 `WebApplication` 类。
使用 `Microsoft.NET.Sdk.Web` SDK 时,项目会隐式包含大量 NuGet 包和配置。
`WebApplicationBuilder` 会在幕后自动设置好日志。

```csharp
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/host-vs-non-host-console-app-54121628/?t=30)

对于不基于 Web、但仍需要健壮宿主环境的应用(例如后台 worker 或长时间运行的服务),Generic Host 是合适的选择。
这需要 `Microsoft.Extensions.Hosting` 包。
Generic Host 提供了一种结构化的方式来管理应用生命周期和服务,而无需暴露 Web 端点。

```csharp
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(x =>
    {
        x.AddJsonConsole();
    })
    .Build();

host.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/host-vs-non-host-console-app-54121628/?t=155)

在宿主环境中,你不会手动从 factory 创建 logger。
相反,你从宿主的服务提供程序(service provider)中解析它。
这让应用可以使用与 ASP.NET Core 中相同的 `ILogger` 抽象和技巧。

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

// ... host building code ...

var logger = host.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Log something");

host.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/host-vs-non-host-console-app-54121628/?t=205)

虽然宿主抽象提供了便利并与依赖注入集成,但底层原理是一样的。
手动的 `LoggerFactory` 方式代表了 .NET 日志的核心,在这一层学到的技能可以直接应用到宿主场景中。

```text
非宿主:  LoggerFactory.Create(...) --> loggerFactory.CreateLogger<Program>()

宿主:    Host.CreateDefaultBuilder(args)
           .ConfigureLogging(...)
           .Build() --> host.Services.GetRequiredService<ILogger<Program>>()
```

## 3. Customizing the loggers and providers

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/customizing-the-loggers-and-providers-54121629/) · 3:09

### 总结

本课演示如何在基本的提供程序注册之外,对 .NET 日志基础设施进行深入定制。
内容涵盖设置全局最低日志级别、配置特定提供程序的选项(例如 JSON 格式和时间戳样式),以及使用 ClearProviders 管理提供程序集合。
此外,本课还解释了这些配置模式如何适用于不同的项目类型,包括控制台应用、Minimal API 和 Generic Host。

### 核心概念

* 在 `LoggerFactory.Create` 中定制 `ILoggingBuilder`。
* 使用 `SetMinimumLevel` 进行全局日志级别过滤。
* 使用选项委托进行特定提供程序的配置(例如 `JsonConsoleFormatterOptions`)。
* 使用 `ClearProviders` 重置日志管道。
* 日志配置在控制台、Web 和 Generic Host builder 之间保持一致。

### 课程笔记

`ILoggingBuilder` 提供了若干方法,用来控制日志如何被处理以及被发送到哪里。
最常见的定制之一,是为 factory 全局设置最低日志级别。
默认情况下,许多提供程序会过滤掉 `Debug` 和 `Trace` 日志。
使用 `SetMinimumLevel(LogLevel.Debug)` 可以确保这些较低级别的日志被已注册的提供程序捕获和处理。

```csharp
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddJsonConsole();
    builder.SetMinimumLevel(LogLevel.Debug);
});

ILogger logger = loggerFactory.CreateLogger<Program>();

var name = "Nick";
var age = 30;

logger.LogDebug("{Name} just turned: {Age}", name, age);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/customizing-the-loggers-and-providers-54121629/?t=25)

提供程序本身通常也提供特定的配置选项。
例如,JSON 控制台提供程序允许通过 `JsonConsoleFormatterOptions` 对格式进行细致的控制。
你可以禁用 scope,定义自定义的时间戳格式,并配置底层的 `JsonWriterOptions` 以启用缩进,提高可读性。

```csharp
using System.Text.Json;
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddJsonConsole(options =>
    {
        options.IncludeScopes = false;
        options.TimestampFormat = "HH:mm:ss";
        options.JsonWriterOptions = new JsonWriterOptions
        {
            Indented = true
        };
    });
    builder.SetMinimumLevel(LogLevel.Debug);
});

ILogger logger = loggerFactory.CreateLogger<Program>();

var name = "Nick";
var age = 30;

logger.LogDebug("{Name} just turned: {Age}", name, age);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/customizing-the-loggers-and-providers-54121629/?t=70)

配置了缩进后,输出会为每条日志生成一个结构化的 JSON 对象,记录消息、状态以及自定义的时间戳格式。

```json
{
  "Timestamp": "10:40:19",
  "EventId": 0,
  "LogLevel": "Debug",
  "Category": "Program",
  "Message": "Nick just turned: 30",
  "State": {
    "Message": "Nick just turned: 30",
    "Name": "Nick",
    "Age": 30,
    "{OriginalFormat}": "{Name} just turned: {Age}"
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/customizing-the-loggers-and-providers-54121629/?t=80)

在某些场景下,你可能希望先移除所有默认提供程序,再添加特定的提供程序。
`ClearProviders()` 方法会从 builder 中移除所有已注册的 `ILoggerProvider` 实例。
如果 `ClearProviders()` 在某些提供程序之后调用,那么在它之前添加的任何提供程序都会被忽略。
这让你可以用另一个提供程序(例如 Systemd 控制台提供程序)完全替换默认的日志栈。

```csharp
using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddJsonConsole(options =>
    {
        options.IncludeScopes = false;
        options.TimestampFormat = "HH:mm:ss";
        options.JsonWriterOptions = new JsonWriterOptions
        {
            Indented = true
        };
    });

    builder.ClearProviders();
    builder.AddSystemdConsole();
    builder.SetMinimumLevel(LogLevel.Debug);
});

ILogger logger = loggerFactory.CreateLogger<Program>();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/customizing-the-loggers-and-providers-54121629/?t=100)

```text
AddJsonConsole(...)    -->  [JsonConsole]
ClearProviders()       -->  [ ]               (JsonConsole 被移除)
AddSystemdConsole()    -->  [SystemdConsole]
```

这些定制模式在不同的 .NET 宿主类型之间是一致的。
在 ASP.NET Core Minimal API 或 Web 应用中,你可以通过 `WebApplicationBuilder` 上的 `Logging` 属性访问日志 builder。

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
bbuilder.Logging.AddJsonConsole();

var app = builder.Build();
app.MapGet("/", () => "Hello World!");
app.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/customizing-the-loggers-and-providers-54121629/?t=140)

同样,使用 Generic Host 时,你可以使用 `ConfigureLogging` 方法访问同一个 `ILoggingBuilder` 接口。
这确保了无论应用的入口点是什么,日志配置都保持统一。

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

var app = builder.Build();
app.MapGet("/", () => "Hello World!");
app.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/customizing-the-loggers-and-providers-54121629/?t=175)

虽然这些示例侧重于基于代码的配置,但 .NET 日志也支持通过 `appsettings.json` 进行全面的配置,这样无需重新编译应用就能更改日志级别和提供程序设置。

## 4. Log category configuration

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-category-configuration-54121630/) · 4:32

### 总结

.NET 中的日志主要通过 appsettings.json 进行配置,开发者可以在其中全局地或针对特定类别(category)定义日志级别。
这种配置支持分层覆盖,更具体的命名空间优先于更通用的命名空间。
此外,.NET 还支持特定提供程序的设置,可以为控制台等各个日志输出目标分别设定不同的日志级别和格式选项(例如带自定义时间戳和缩进的 JSON 输出),而无需修改代码或重新部署应用。

### 核心概念

*   **分层日志级别(Hierarchical Log Levels)**:具体的类别命名空间会覆盖配置中定义的 `Default` 级别。
*   **类别粒度(Category Granularity)**:过滤可以应用于命名空间的不同层级(例如 `Microsoft.Hosting.Lifetime` 与 `Microsoft.Extensions.Hosting`)。
*   **提供程序覆盖(Provider Overrides)**:`Logging` 节可以包含特定提供程序的块(例如 `Console`),为该特定输出覆盖顶层设置。
*   **控制台格式化器(Console Formatters)**:内置支持 `simple` 和 `json` 格式化器,可通过 `FormatterName` 和 `FormatterOptions` 进行配置。
*   **配置灵活性(Configuration Flexibility)**:使用 `appsettings.json` 可以通过环境变量或外部配置提供程序更改日志行为,而无需修改源代码。

### 课程笔记

在 .NET Core 2.0 及更高版本中,`appsettings.json` 是配置应用行为(包括日志)的标准位置。
大多数项目模板在该文件的特定环境版本(例如 `appsettings.Development.json`)和与环境无关的版本中都包含一个默认的 `Logging` 节。

#### 基本的类别配置

最基本的配置定义一个 `Default` 日志级别,以及针对 `Microsoft.AspNetCore` 等命名空间的特定覆盖。
在下面的示例中,默认级别是 `Information`,但来自 ASP.NET Core 框架的日志被限制为 `Warning` 及以上。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-category-configuration-54121630/?t=10)

如果把 `Microsoft.AspNetCore` 的级别降低到 `Information`,应用会输出多得多的关于请求执行和框架内部操作的遥测信息。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-category-configuration-54121630/?t=55)

#### 细粒度过滤

你可以针对特定的子类别来减少噪音。
例如,要在保留其他 `Information` 日志的同时屏蔽启动和生命周期相关的信息日志,可以专门把 `Microsoft.Hosting.Lifetime` 设置为 `Warning`。

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Information",
      "Microsoft.Hosting.Lifetime": "Warning"
    }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-category-configuration-54121630/?t=90)

反过来,你也可以把特定命名空间设置为 `Trace` 或 `Debug`,提高其详细程度,以调试宿主的内部逻辑。

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Information",
      "Microsoft.Hosting.Lifetime": "Trace",
      "Microsoft.Extensions.Hosting": "Trace"
    }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-category-configuration-54121630/?t=130)

#### 特定提供程序的覆盖

配置可以按日志提供程序进行专门化。
例如,你可能希望应用的总体日志级别是 `Warning`,但在开发期间专门让 `Console` 提供程序输出 `Information` 日志。

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    },
    "Console": {
      "LogLevel": {
        "Default": "Information",
        "Microsoft.AspNetCore": "Information"
      }
    }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-category-configuration-54121630/?t=175)

```text
Logging
+-- LogLevel                      (所有提供程序)
|     Default              = Information
|     Microsoft.AspNetCore = Warning
+-- Console
      LogLevel                    (仅 Console,覆盖顶层设置)
        Default              = Information
        Microsoft.AspNetCore = Information
```

#### 控制台格式化器配置

像 Console logger 这样的提供程序支持对格式化器进行高级配置。
默认情况下,控制台使用 `simple` 格式化器,但可以改为 `json`。
在 `FormatterOptions` 中,你可以定制诸如 JSON 是否缩进、是否包含 scope 以及具体的时间戳格式等属性。

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    },
    "Console": {
      "LogLevel": {
        "Default": "Information",
        "Microsoft.AspNetCore": "Information"
      },
      "FormatterName": "json",
      "FormatterOptions": {
        "SingleLine": true,
        "IncludeScopes": true,
        "TimestampFormat": "HH:mm:ss",
        "UseUtcTimestamp": true,
        "JsonWriterOptions": {
          "Indented": true
        }
      }
    }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-category-configuration-54121630/?t=215)

强烈推荐这种配置方式,因为它允许通过热重载或简单地重新部署配置文件来应用更改,而无需重新编译应用代码。

```csharp
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-category-configuration-54121630/?t=250)

## 5. Dealing with exceptions

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/dealing-with-exceptions-54121631/) · 1:52

### 总结

本课演示如何使用 ILogger 接口在 .NET 中有效地记录异常。
内容涵盖专门用于捕获异常对象的方法重载,确保关键的诊断信息(包括异常类型、消息和堆栈跟踪)保留在日志输出中。
本课还强调在记录应用故障时选择合适的日志级别,例如 Warning、Error 或 Critical。

### 核心概念

*   **异常重载(Exception Overloads)**:`ILogger` 中的每个日志方法(例如 `LogInformation`、`LogError`)都有一个接受 `Exception` 作为参数的重载。
*   **参数顺序(Parameter Ordering)**:记录异常时,`Exception` 对象通常作为第一个参数,放在消息模板之前传入。
*   **结构化异常数据(Structured Exception Data)**:传入异常对象可以让 logger 把完整的堆栈跟踪和异常类型作为结构化数据捕获,而不仅仅是一个字符串。
*   **日志严重性(Log Severity)**:应根据故障的影响,用合适的 `LogLevel`(Warning、Error 或 Critical)记录异常。

### 课程笔记

日志在出问题时最为关键。
为了有效地处理和记录错误,你应该把可能失败的代码包在 `try-catch` 块中。

首先,来看一个跟踪应用状态的基本日志设置:

```csharp
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
    builder.SetMinimumLevel(LogLevel.Debug);
});

ILogger logger = loggerFactory.CreateLogger<Program>();

var name = "Nick";
var age = 30;

logger.LogDebug("{Name} just turned: {Age}", name, age);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/dealing-with-exceptions-54121631/?t=10)

为了模拟错误,我们可以在 `try` 块中抛出一个异常。
在 `catch` 块中,我们使用 logger 记录这次失败。
一开始,你可能只是记录一条消息,而没有传入实际的异常对象:

```csharp
ILogger logger = loggerFactory.CreateLogger<Program>();

var name = "Nick";
var age = 30;

try
{
    throw new Exception("Something went wrong");
}
catch (Exception ex)
{
    logger.LogInformation("{Name} just turned: {Age}", name, age);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/dealing-with-exceptions-54121631/?t=40)

然而,`ILogger` 接口提供了专门处理异常的重载。
无论你使用哪个日志方法,都有一个重载把 `Exception` 作为第一个或第二个参数(取决于是否使用了 Event ID)。
把异常对象 `ex` 直接传给 logger,系统就能捕获错误的完整上下文。

```csharp
ILogger logger = loggerFactory.CreateLogger<Program>();

var name = "Nick";
var age = 30;

try
{
    throw new Exception("Something went wrong");
}
catch (Exception ex)
{
    logger.LogInformation(ex, "Failure during birthday of {Name} who is {Age}", name, age);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/dealing-with-exceptions-54121631/?t=70)

当异常作为对象传入时,logger 会把它当作一个额外的结构化对象。
生成的日志输出(尤其是使用 JSON 格式化器时)会包含异常类型、消息以及完整的堆栈跟踪,其中包括错误发生的确切文件路径和行号。

```json
{
  "Timestamp": "11:00:52",
  "EventId": 0,
  "LogLevel": "Information",
  "Category": "Program",
  "Message": "Failure during birthday of Nick who is 30",
  "Exception": "System.Exception: Something went wrong\r\n   at Program.\u003CMain\u003E$(String[] args) in I:\\lab\\courses\\logging-course-final\\3.DeepDive\\BasicConsoleApp\\Program.cs:line 26",
  "State": {
    "Message": "Failure during birthday of Nick who is 30",
    "Name": "Nick",
    "Age": 30,
    "{OriginalFormat}": "Failure during birthday of {Name} who is {Age}"
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/dealing-with-exceptions-54121631/?t=85)

实际上,异常不应该作为普通的 Information 来记录。
根据严重程度,你应该使用 `LogWarning`、`LogError` 或 `LogCritical`。
这些方法都支持同样的异常重载,而且其行为在不同的日志级别之间保持一致。

```csharp
try
{
    throw new Exception("Something went wrong");
}
catch (Exception ex)
{
    logger.LogError(ex, "Failure during birthday of {Name} who is {Age}", name, age);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/dealing-with-exceptions-54121631/?t=100)

## 6. What are Log filters?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/what-are-log-filters-54121632/) · 4:03

日志过滤器(log filter)是决定一条日志消息应被处理还是被忽略的底层机制。
虽然开发者通常是通过配置文件间接地与过滤器打交道,但也可以在代码中显式定义过滤器,以便对日志行为进行更细粒度的控制。

### 核心概念

- **配置与代码(Configuration vs. Code)**:在 `appsettings.json` 中设置最低级别,实际上就是在幕后实现了一个日志过滤器。
- **编程式过滤(Programmatic Filtering)**:`AddFilter` 方法支持复杂的逻辑,可以同时评估日志级别、提供程序名称和类别名称。
- **特定提供程序的过滤器(Provider-Specific Filters)**:可以使用泛型重载把过滤器限定在特定的日志提供程序上(例如 Console、Debug)。
- **规则优先级(Rule Precedence)**:过滤器可以组合起来构成精细的规则,例如为系统级日志和应用级日志设置不同的阈值。

### 课程笔记

大多数开发者都是通过 `appsettings.json` 文件的 `Logging` 节来熟悉日志过滤的。
这一配置根据默认级别或特定类别定义日志的最低阈值。

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    },
    "Console": {
      "LogLevel": {
        "Default": "Information",
        "Microsoft.AspNetCore": "Information"
      },
      "FormatterName": "json",
      "FormatterOptions": {
        "SingleLine": true,
        "IncludeScopes": true,
        "TimestampFormat": "HH:mm:ss",
        "UseUtcTimestamp": true,
        "JsonWriterOptions": {
          "Indented": true
        }
      }
    }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/what-are-log-filters-54121632/?t=10)

在代码中,使用 `SetMinimumLevel` 设置最低级别是一种简单的过滤形式。
不过,`AddFilter` 方法提供了一种更可定制的方式。
例如,你可以定义一个过滤器,只允许级别恰好匹配某个特定级别的日志通过,或者只允许达到某个阈值的日志通过。

```csharp
using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddJsonConsole(options =>
    {
        options.IncludeScopes = false;
        options.TimestampFormat = "HH:mm:ss";
        options.JsonWriterOptions = new JsonWriterOptions
        {            Indented = true
        };
    });

    builder.AddFilter(x => x >= LogLevel.Debug);
});

ILogger<Program> logger = loggerFactory.CreateLogger<Program>();

var name = "Nick";
var age = 30;

logger.LogDebug("{Name} just turned: {Age}", name, age);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/what-are-log-filters-54121632/?t=60)

除了 `LogLevel` 之外,`AddFilter` 还可以接受一个谓词,让你同时访问提供程序名称、类别名称和日志级别。
这样就可以写出复杂的表达式,例如仅在使用 Console 提供程序时才针对某个特定类别进行过滤。

```csharp
builder.AddFilter((provider, category, logLevel) =>
{
    return provider!.Contains("Console") &&
           category!.Contains("Microsoft.Extensions.Hosting.Internal.Host") &&
           logLevel >= LogLevel.Debug;
});
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/what-are-log-filters-54121632/?t=115)

还可以使用泛型的 `AddFilter<T>` 方法把过滤器应用到特定的提供程序上。
当你想在不同的输出之间混合搭配不同行为时,这尤其有用。
例如,你可能希望 `System` 日志在全局范围内为 `Debug` 级别,但专门针对 Console 提供程序把 `Microsoft` 日志限制为 `Information`。

```csharp
builder
    .AddFilter("System", LogLevel.Debug)
    .AddFilter<ConsoleLoggerProvider>("Microsoft", LogLevel.Information);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/what-are-log-filters-54121632/?t=175)

使用 Generic Host 时,这些过滤器通常在 `ConfigureLogging` 块中配置。
这种方式可以构建一个高度灵活的日志系统,应用的不同组件可以根据所用的提供程序拥有各自不同的详细程度。

```csharp
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

await Host.CreateDefaultBuilder(args)
    .ConfigureLogging(logging =>
        logging
            .AddFilter("System", LogLevel.Debug)
            .AddFilter<ConsoleLoggerProvider>("Microsoft", LogLevel.Information)
            .AddFilter<ConsoleLoggerProvider>("Microsoft.Extensions.Hosting.Internal.Host", LogLevel.Debug))
    .Build()
    .RunAsync();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/what-are-log-filters-54121632/?t=205)

当应用带着这些过滤器运行时,输出只会包含满足所定义条件的日志,例如来自内部 host 的 `Debug` 日志,以及来自其他 Microsoft 类别的 `Information` 日志。

```text
dbug: Microsoft.Extensions.Hosting.Internal.Host[1]
      Hosting starting
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Production
dbug: Microsoft.Extensions.Hosting.Internal.Host[2]
      Hosting started
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/what-are-log-filters-54121632/?t=220)

虽然编程式过滤器很强大,但它们最常被库作者使用。
对于标准的微服务或应用,这类配置大多更适合通过 `appsettings.json` 处理,以便无需重新编译代码就能针对不同环境做出更改。

## 7. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121633/) · 0:50

### 总结

本课全面回顾了本节所涵盖的基础日志概念,包括日志抽象与提供程序之间的关系、宿主环境与非宿主环境中 logger 注册方式的差异,以及日志过滤和配置的机制。
它强调异常在日志重载中被当作普通对象处理,并为更详细地探讨日志输出目标和提供程序做好铺垫。

### 核心概念

- 日志包生态(抽象与实现)。
- 基于宿主与非基于宿主的应用注册。
- 配置驱动的日志与提供程序定制。
- 通过特定方法重载记录异常。
- 日志过滤机制。

### 课程笔记

#### 日志基础设施与包

.NET 日志系统建立在分层架构之上。
它从抽象开始(位于 `Microsoft.Extensions.Logging.Abstractions` 中),这些抽象定义了整个应用中使用的接口。
这些抽象随后由核心日志包实现,核心日志包协调各种日志提供程序(例如 Console、Debug 或 EventLog),把日志输出导向特定的目标。

#### 注册模型

基于宿主和非基于宿主的应用在注册日志的方式上有明显区别。
在非基于宿主的应用(例如一个简单的控制台工具)中,`LoggerFactory` 是使用 `LoggerFactory.Create` 手动创建的。

```csharp
using Microsoft.Extensions.Logging;

#region secret 🤭

using var loggerFactory = LoggerFactory.Create(builder =>
{
#pragma warning disable CA1416
    builder
        .AddConsole()
        .AddEventLog();
#pragma warning restore CA1416
});

ILogger<Program> CreateLogger()
{
    return loggerFactory.CreateLogger<Program>();
}
#endregion

ILogger<Program> logger = CreateLogger();

logger.LogInformation("Hello world!");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121633/?t=11)

相比之下,基于宿主的应用(例如 ASP.NET Core 或 Worker Service)使用 `CreateDefaultBuilder` 或 `WebApplication.CreateBuilder` 模式,它们会自动处理大部分日志注册和配置。

#### 配置与定制

日志行为可以通过代码或 `appsettings.json` 等外部配置文件进行定制。
这让开发者无需重新编译应用就能定义日志级别和特定提供程序的设置。
例如,可以把 `Console` 提供程序配置为使用 JSON 等特定格式化器,并包含 scope 或时间戳。

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    },
    "Console": {
      "LogLevel": {
        "Default": "Information",
        "Microsoft.AspNetCore": "Information"
      },
      "FormatterName": "json",
      "FormatterOptions": {
        "SingleLine": true,
        "IncludeScopes": true,
        "TimestampFormat": "HH:mm:ss",
        "UseUtcTimestamp": true,
        "JsonWriterOptions": {
          "Indented": true
        }
      }
    }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121633/?t=19)

#### 异常处理

在 .NET 日志中,异常并不被当作一种需要单独日志管道的独特基础类型。
相反,它们是通过 `ILogger` 接口中的方法重载来处理的。
`Exception` 只是传入日志方法的另一个对象,这让提供程序可以把堆栈跟踪和消息格式化后与日志条目一并输出。

#### 日志过滤

日志过滤器提供了一种根据特定条件(例如日志类别或最低日志级别)包含或排除日志的机制。
这些过滤器可以全局应用,也可以在日志配置期间使用 `AddFilter` 方法限定在特定的提供程序上。

```csharp
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

await Host.CreateDefaultBuilder(args)
    .ConfigureLogging(logging =>
        logging
            .AddFilter("System", LogLevel.Debug)
            .AddFilter<ConsoleLoggerProvider>("Microsoft", LogLevel.Information)
            .AddFilter<ConsoleLoggerProvider>("Microsoft.Extensions.Hosting.Internal.Host", LogLevel.Debug))
    .Build()
    .RunAsync();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121633/?t=38)
