# Dealing with providers

> 课程:[From Zero to Hero: Logging in .NET](https://dometrain.com/course/from-zero-to-hero-logging-in-dotnet/) · 第 5 章
> 共 7 课 · 约 18:31
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [What is a provider?](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/what-is-a-provider-54121644/) | 1:14 | [↓](#1-what-is-a-provider) |
| 2 | [Built-in providers](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/built-in-providers-54121645/) | 1:59 | [↓](#2-built-in-providers) |
| 3 | [Using the Application Insights provider in ASP.NET Core](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/using-the-application-insights-provider-in-aspnet-core-54121646/) | 5:01 | [↓](#3-using-the-application-insights-provider-in-aspnet-core) |
| 4 | [Using the Application Insights provider in a console app](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/using-the-application-insights-provider-in-a-console-app-54121647/) | 4:28 | [↓](#4-using-the-application-insights-provider-in-a-console-app) |
| 5 | [Editing registered providers](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/editing-registered-providers-54121648/) | 1:44 | [↓](#5-editing-registered-providers) |
| 6 | [Creating our own logger provider](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/creating-our-own-logger-provider-54121649/) | 3:18 | [↓](#6-creating-our-own-logger-provider) |
| 7 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121650/) | 0:47 | [↓](#7-section-recap) |

## 1. What is a provider?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/what-is-a-provider-54121644/) · 1:14

### 总结

在 .NET 日志中,提供程序(provider)是负责把日志消息投递到特定目的地的组件。
这些目的地涵盖从系统控制台到外部服务或本地文件系统的各种形式。
提供程序按其生命周期分类:临时性(ephemeral)提供程序(例如控制台)在应用程序停止后就会丢失数据,而持久性(persistent)提供程序(例如文件系统)会把日志保存下来以便将来检索。
.NET 框架包含若干内置提供程序,并支持创建自定义提供程序,以满足特定的架构需求。

### 核心概念

* **日志目的地(Log Destinations)**:提供程序定义日志被发送到哪里,可以是本地输出,也可以是外部服务。
* **临时性提供程序(Ephemeral Providers)**:像控制台这样的目的地,日志只在应用程序运行期间存在。
* **持久性提供程序(Persistent Providers)**:像文件系统这样的目的地,会永久保存日志。
* **可扩展性(Extensibility)**:.NET 日志系统既支持内置的提供程序实现,也支持自定义的提供程序实现。

### 课程笔记

提供程序实际上就是日志的一个目的地。
虽然人们通常把目的地想成外部服务,但它们也可以是本地输出。
例如,控制台就是开发期间常见的日志目的地。
在使用 Generic Host 的标准 .NET 控制台应用程序中,控制台会在幕后被配置为一个提供程序。

```csharp
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args).Build();

await host.RunAsync();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/what-is-a-provider-54121644/?t=25)

控制台是临时性提供程序的一个例子。
这种分类意味着,应用程序一旦停止运行,日志就不再存在于该目的地中;它们不是持久的。
在 Web 应用程序中也能看到这种行为,例如使用 Minimal API 模板的应用程序。

```csharp
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/what-is-a-provider-54121644/?t=40)

与临时性提供程序相反,持久性日志提供程序确保数据被保存下来以供长期访问。
文件系统是最常用的持久性提供程序之一。
.NET 为各种场景提供了若干内置提供程序,开发者也可以灵活地实现自定义提供程序,以面向特殊的目的地。

## 2. Built-in providers

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/built-in-providers-54121645/) · 1:59

### 总结

.NET 提供了若干内置的日志提供程序,但它们是否可用取决于项目类型和所引用的包。
使用 Web SDK 的 ASP.NET Core 应用程序会隐式包含 Console、Debug、EventLog、EventSource 和 TraceSource 这些提供程序。
相比之下,标准的控制台应用程序需要手动配置,并显式引用 NuGet 包。
在控制台应用中引用 Microsoft.Extensions.Hosting 包,会以传递依赖的形式自动引入若干日志提供程序,而只引用特定的日志包则能对日志栈进行更细粒度的控制。

### 核心概念

* **隐式提供程序注册(Implicit Provider Registration)**:ASP.NET Core(Web SDK)项目无需手动配置即自动包含一组日志提供程序。
* **默认的 Web 提供程序(Default Web Providers)**:Console、Debug、EventLog、EventSource 和 TraceSource。
* **SDK 差异(SDK Differences)**:`Microsoft.NET.Sdk.Web` 默认包含日志程序集,而 `Microsoft.NET.Sdk` 不包含。
* **传递依赖(Transitive Dependencies)**:像 `Microsoft.Extensions.Hosting` 这样的高层包会以间接依赖的形式包含多个日志提供程序。

### 课程笔记

.NET 中日志提供程序是否可用,很大程度上取决于项目的 SDK 及其依赖项。
在 ASP.NET Core 应用程序中,项目文件通常使用 `Microsoft.NET.Sdk.Web` SDK。

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

</Project>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/built-in-providers-54121645/?t=25)

由于这是一个 Web 项目,若干日志程序集会被隐式添加到应用程序中。
其中包括:
1. **Console**:输出到标准输出。
2. **Debug**:输出到 Visual Studio 等 IDE 的调试窗口。
3. **EventLog**:输出到 Windows 事件日志。
4. **EventSource**:为 Windows 事件跟踪(ETW)提供结构化日志。
5. **TraceSource**:支持旧版 .NET 跟踪。

相比之下,标准的控制台应用程序使用基础的 `Microsoft.NET.Sdk`。

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

</Project>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/built-in-providers-54121645/?t=55)

在这种环境下,不会隐式添加任何 Microsoft 专属的日志包。
要启用日志,你必须手动引用所需的 NuGet 包。
有意思的是,包的选择会影响哪些提供程序通过传递依赖变得可用。
例如,添加 `Microsoft.Extensions.Hosting` 包会隐式包含 Console、Debug、EventLog 和 EventSource 提供程序。

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.Extensions.Hosting" Version="7.0.1" />
    <PackageReference Include="Microsoft.Extensions.Logging" Version="7.0.0" />
    <PackageReference Include="Microsoft.Extensions.Logging.Console" Version="7.0.0" />
  </ItemGroup>

</Project>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/built-in-providers-54121645/?t=85)

如果移除 hosting 包,这些间接依赖就会消失。
如果你只引用 `Microsoft.Extensions.Logging.Console`,那么应用程序可用的就只有 Console 提供程序。

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
  </ItemGroup>

</Project>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/built-in-providers-54121645/?t=100)

在继续了解 Azure Application Insights 等外部日志提供程序之前,理解这些内置行为至关重要。

## 3. Using the Application Insights provider in ASP.NET Core

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/using-the-application-insights-provider-in-aspnet-core-54121646/) · 5:01

### 总结

本课演示如何在 ASP.NET Core 应用程序中把 Azure Application Insights 集成为外部日志提供程序。
内容包括安装所需的 NuGet 包、在应用程序构建器中配置遥测连接字符串,以及如何使用 Kusto 查询语言(KQL)在 Azure 门户中验证日志是否被正确摄取和查询。

### 核心概念

* **Microsoft.Extensions.Logging.ApplicationInsights**:把 Application Insights 日志集成到 .NET 应用程序中的标准 NuGet 包。
* **连接字符串(Connection Strings)**:对遥测数据进行身份验证并将其路由到特定 Azure 资源的主要机制。
* **异步批处理(Asynchronous Batching)**:Application Insights 以批次方式推送日志,以优化性能并减少 API 请求的数量。
* **Kusto 查询语言(KQL)**:Azure 门户中用于筛选和分析日志跟踪(trace)及自定义维度的查询语言。
* **遥测配置(Telemetry Configuration)**:把日志提供程序与 Azure 后端关联起来所需的编程式设置。

### 课程笔记

把 Application Insights 集成到 ASP.NET Core 应用程序中,可以在 Azure 云中实现集中式日志。
首先,你必须在 Azure 门户中创建一个 Application Insights 实例,并从中获取**连接字符串(Connection String)**。

#### 安装包

该集成需要 `Microsoft.Extensions.Logging.ApplicationInsights` NuGet 包。
这个提供程序遵循 .NET 日志扩展的标准命名约定。

#### 配置

为了确保在开发或测试期间能看到足够多的日志,你可能需要调整 `appsettings.json` 中的日志级别。
虽然 `Microsoft.AspNetCore` 通常设置为 `Warning`,但把它设置为 `Information` 会捕获更详细的框架活动。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/using-the-application-insights-provider-in-aspnet-core-54121646/?t=100)

在 `Program.cs` 文件中,通过 `builder.Logging` 属性上的 `AddApplicationInsights` 扩展方法注册该提供程序。
这一注册应当发生在创建 `WebApplicationBuilder` 与通过 `builder.Build()` 物化 `WebApplication` 之间。
你必须在 `configureTelemetryConfiguration` 委托中提供从 Azure 门户获取的连接字符串。

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddApplicationInsights(
    configureTelemetryConfiguration: teleConfig =>
        teleConfig.ConnectionString = "InstrumentationKey=3f0c4b53-89b6-4b84-811c-5659284c9071;IngestionEndpoint=https://northeurope-2.in.applicationinsights.azure.com/;LiveEndpoint=https://northeurope.livediagnostics.monitor.azure.com/",
    configureApplicationInsightsLoggerOptions: _ => { });

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/using-the-application-insights-provider-in-aspnet-core-54121646/?t=160)

```text
WebApplication.CreateBuilder(args)
        |
        v
builder.Logging.AddApplicationInsights(...)
        |
        v
builder.Build()  -->  WebApplication
```

#### 筛选与自定义

与其他日志提供程序一样,Application Insights 支持手动自定义筛选器。
你可以使用 `AddFilter` 指定哪些日志类别和级别应当发送到这个特定的提供程序。

```csharp
using Microsoft.Extensions.Logging.ApplicationInsights;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddApplicationInsights(
    configureTelemetryConfiguration: teleConfig =>
        teleConfig.ConnectionString = "InstrumentationKey=3f0c4b53-89b6-4b84-811c-5659284c9071;...",
    configureApplicationInsightsLoggerOptions: _ => { });

builder.Logging.AddFilter<ApplicationInsightsLoggerProvider>(null, LogLevel.Information);

var app = builder.Build();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/using-the-application-insights-provider-in-aspnet-core-54121646/?t=175)

#### 日志摄取与查询

日志以批次方式异步推送到 Application Insights。
这避免了应用程序为每一条日志消息都发起一次阻塞的 API 请求,那样的代价会高得无法接受。
因此,日志出现在 Azure 门户中之前可能会有轻微的延迟。

日志被摄取之后,可以在 Application Insights 资源的 **Logs** 部分使用 KQL 进行查询。
标准日志存储在 `traces` 表中。
Application Insights 还会捕获"自定义维度(Custom Dimensions)",即与日志消息相关联的参数(例如 `ContentRootPath`),可用于筛选。

```kusto
traces
| order by timestamp desc
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/using-the-application-insights-provider-in-aspnet-core-54121646/?t=230)

#### Generic Host 集成

对于使用 Generic Host 的非 Web 应用程序,配置在 `ConfigureLogging` 方法中遵循同样的模式。

```csharp
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(logging => 
    {
        logging.AddApplicationInsights(
            config => config.ConnectionString = "InstrumentationKey=...",
            options => { });
    })
    .Build();

await host.RunAsync();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/using-the-application-insights-provider-in-aspnet-core-54121646/?t=280)

## 4. Using the Application Insights provider in a console app

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/using-the-application-insights-provider-in-a-console-app-54121647/) · 4:28

### 总结

在本课中,我们探讨如何在没有传统 host 的独立控制台应用程序中实现 Application Insights 日志。
这个过程包括手动设置一个 InMemoryChannel 来处理遥测通信,并把应用程序逻辑包裹在 try-finally 块中,以保证所有缓冲的日志在应用程序退出前被刷新(flush)。
我们还利用 .NET 依赖注入(DI)容器来简化 TelemetryConfiguration 和 Application Insights 日志提供程序的配置,确保即使在最小化的环境中,日志也能被正确地路由到 Azure。

### 核心概念

- 为遥测手动实例化 `InMemoryChannel`。
- 使用 `try-finally` 确保在关闭时调用 `FlushAsync`。
- 在刷新之后加入一段延迟,以防进程过早终止。
- 利用 `IServiceCollection` 进行日志和遥测配置。
- 集成 `Microsoft.Extensions.Logging.ApplicationInsights` 提供程序。

### 课程笔记

要在没有 generic host 的控制台应用程序中使用 Application Insights,你必须先安装 `Microsoft.ApplicationInsights` 包。
这个核心包让你可以手动创建遥测通道。

```csharp
using Microsoft.ApplicationInsights.Channel;

using var channel = new InMemoryChannel();

try
{

}
finally
{

}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/using-the-application-insights-provider-in-a-console-app-54121647/?t=25)

由于你完全掌控这个通道,你必须确保所有缓冲在内存中的日志在应用程序关闭前被推送到 Application Insights。
这是通过在 `finally` 块中调用 `FlushAsync` 来实现的,以保证在意外关闭时也能投递。

```csharp
using Microsoft.ApplicationInsights.Channel;

using var channel = new InMemoryChannel();

try
{

}
finally
{
    await channel.FlushAsync(default);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/using-the-application-insights-provider-in-a-console-app-54121647/?t=70)

还建议在刷新之后加入一段短暂的延迟(例如一秒),以确保应用程序不会在网络传输完成之前退出。

```csharp
using Microsoft.ApplicationInsights.Channel;

using var channel = new InMemoryChannel();

try
{

}
finally
{
    await channel.FlushAsync(default);
    await Task.Delay(1000);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/using-the-application-insights-provider-in-a-console-app-54121647/?t=85)

为了简化日志和遥测的配置,你可以使用 .NET 依赖注入扩展。
这需要添加 `Microsoft.Extensions.DependencyInjection` 和 `Microsoft.Extensions.Options.ConfigurationExtensions` 包。

```csharp
using Microsoft.Extensions.DependencyInjection;

using var channel = new InMemoryChannel();

try
{
    IServiceCollection services = new ServiceCollection();
    services.Configure
}
finally
{
    await channel.FlushAsync(default);
    await Task.Delay(1000);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/using-the-application-insights-provider-in-a-console-app-54121647/?t=130)

接下来,配置 `TelemetryConfiguration`,让它使用手动创建的通道。
这是通过在 DI 配置中设置 `TelemetryChannel` 属性来完成的。
这需要 `Microsoft.ApplicationInsights.Extensibility` 命名空间。

```csharp
using var channel = new InMemoryChannel();

try
{
    IServiceCollection services = new ServiceCollection();
    services.Configure<TelemetryConfiguration>(x => x.TelemetryChannel = channel);
    services.AddLogging
}
finally
{
    await channel.FlushAsync(default);
    await Task.Delay(1000);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/using-the-application-insights-provider-in-a-console-app-54121647/?t=160)

最后,使用 `Microsoft.Extensions.Logging.ApplicationInsights` 包中的 `AddApplicationInsights` 扩展方法添加 Application Insights 日志提供程序。
你必须提供连接字符串并配置 logger 选项。
构建好 service provider 之后,你就可以解析一个 `ILogger<T>` 并像往常一样记录信息。

```csharp
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using var channel = new InMemoryChannel();

try
{
    IServiceCollection services = new ServiceCollection();
    services.Configure<TelemetryConfiguration>(x => x.TelemetryChannel = channel);
    services.AddLogging(x =>
    {
        x.AddApplicationInsights(
            configureTelemetryConfiguration: teleConfig =>
                teleConfig.ConnectionString =
                    "InstrumentationKey=3f0c4b53-89b6-4b84-811c-5659284c9071;IngestionEndpoint=https://northeurope-2.in.applicationinsights.azure.com/;LiveEndpoint=https://northeurope.livediagnostics.monitor.azure.com/",
            configureApplicationInsightsLoggerOptions: _ => { });
    });

    var serviceProvider = services.BuildServiceProvider();

    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
    
    logger.LogInformation("Hello from console!");

}
finally
{
    await channel.FlushAsync(default);
    await Task.Delay(1000);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/using-the-application-insights-provider-in-a-console-app-54121647/?t=235)

```text
ILogger<Program>.LogInformation(...)
        |
        v
ApplicationInsights provider
        |
        v
TelemetryConfiguration.TelemetryChannel = InMemoryChannel (buffer)
        |
        v  finally: channel.FlushAsync(default); Task.Delay(1000)
Application Insights
```

要验证日志是否被发送,你可以使用 Kusto 查询语言(KQL)查询 Application Insights 中的 `traces` 表。

```kql
traces
| order by timestamp desc
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/using-the-application-insights-provider-in-a-console-app-54121647/?t=250)

## 5. Editing registered providers

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/editing-registered-providers-54121648/) · 1:44

### 总结

本课演示如何根据执行环境,以编程方式管理 .NET 应用程序中的日志提供程序注册。
通过使用 builder.Logging.ClearProviders() 以及基于 builder.Environment 的条件逻辑,开发者可以确保像控制台这样开销大的 logger 只限于开发环境使用,而像 Application Insights 这样生产级的提供程序则用于已部署的环境。

### 核心概念

* 使用 builder.Environment 进行条件式的提供程序注册。
* 使用 builder.Logging.ClearProviders() 清除默认提供程序。
* 特定于环境的日志策略(例如开发环境用 Console,生产环境用 Application Insights)。
* 控制台日志对生产环境性能的影响。
* 通过 launchSettings.json 配置环境。

### 课程笔记

在 ASP.NET Core 中,你可以在应用程序启动期间操作 builder.Logging 集合,以控制哪些日志提供程序处于活动状态。
这样做通常是为了优化性能,并确保日志根据环境被发送到合适的目的地。

例如,你可能希望在除开发环境之外的所有环境中使用 Application Insights,或者只在本地调试期间使用控制台。

```csharp
using Microsoft.Extensions.Logging.ApplicationInsights;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddApplicationInsights(
    configureTelemetryConfiguration: teleConfig =>
        teleConfig.ConnectionString = "InstrumentationKey=3f0c4b53-89b6-4b84-811c-5659284c9071;IngestionEndpoint=https://northeurope-2.in.applicationinsights.azure.com/;LiveEndpoint=https://northeurope.livediagnostics.monitor.azure.com/",
    configureApplicationInsightsLoggerOptions: _ => { });

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/editing-registered-providers-54121648/?t=10)

要实现特定于环境的逻辑,请使用 builder.Environment 属性。
一种常见的模式是先清除所有默认提供程序,然后只添加当前上下文所需的那些。

```csharp
if (builder.Environment.IsDevelopment())
{
    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/editing-registered-providers-54121648/?t=40)

在生产环境或其他非开发环境中,你可以注册更健壮的提供程序。
先清除提供程序可以确保不会有多余的默认 logger(例如 Event Log 或 Console)仍处于活动状态,这一点至关重要,因为控制台日志可能会显著降低生产环境中应用程序的性能。

```csharp
if (builder.Environment.IsDevelopment())
{
    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();
}
else
{
    builder.Logging.ClearProviders();
    builder.Logging.AddApplicationInsights(
        configureTelemetryConfiguration: teleConfig =>
            teleConfig.ConnectionString = "InstrumentationKey=3f0c4b53-89b6-4b84-811c-5659284c9071;IngestionEndpoint=https://northeurope-2.in.applicationinsights.azure.com/;LiveEndpoint=https://northeurope.livediagnostics.monitor.azure.com/",
        configureApplicationInsightsLoggerOptions: _ => { });
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/editing-registered-providers-54121648/?t=55)

```text
builder.Environment.IsDevelopment()?
   |
   +-- yes --> ClearProviders() --> AddConsole()
   |
   +-- no  --> ClearProviders() --> AddApplicationInsights(...)
```

你可以通过修改 launchSettings.json 文件中的 ASPNETCORE_ENVIRONMENT 变量来测试这些配置。
把环境切换为 "Production" 后,按照上面的逻辑,控制台 logger 会被停用,Application Insights 提供程序会被启用。

```json
{
  "$schema": "http://json.schemastore.org/launchsettings.json",
  "profiles": {
    "https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": false,
      "applicationUrl": "https://localhost:7072;http://localhost:5257",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Production"
      }
    }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/editing-registered-providers-54121648/?t=70)

## 6. Creating our own logger provider

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/creating-our-own-logger-provider-54121649/) · 3:18

### 总结

本课演示如何在 .NET 中实现自定义日志提供程序:创建实现 `ILoggerProvider` 和 `ILogger` 接口的类。
内容涵盖每个接口所需的基本方法,包括 logger 的创建、日志级别过滤以及实际的日志记录逻辑,同时还介绍了用于处理日志作用域的 `NullScope` 模式。
最后,本课展示了如何在 .NET 应用程序的日志管道中注册这个自定义提供程序。

### 核心概念

- 实现 `ILoggerProvider` 来创建 logger 实例
- 实现 `ILogger` 来定义自定义日志行为
- 为 `BeginScope` 使用 `NullScope` 单例以避免内存分配
- 在记录日志前检查 `IsEnabled` 以优化性能
- 在日志构建器中通过 `AddProvider` 注册自定义提供程序

### 课程笔记

要创建自定义日志提供程序,你必须实现 `ILoggerProvider` 接口。
这个接口需要两个方法:`CreateLogger`,为特定类别返回你的自定义 logger 实例;以及 `Dispose`,用于清理提供程序维护的任何状态。
在最简单的实现中,`CreateLogger` 只是返回你的自定义 logger 类的一个新实例。

```csharp
namespace BasicMinimalApi;

public class NickLoggerProvider : ILoggerProvider
{
    public void Dispose() { }

    public ILogger CreateLogger(string categoryName)
    {
        return new NickLogger();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/creating-our-own-logger-provider-54121649/?t=40)

核心逻辑位于 `ILogger` 的实现中。
这个接口需要三个方法:`BeginScope`、`IsEnabled` 和 `Log`。
对于 `BeginScope`,如果你暂时还不支持作用域,可以返回一个单例的 `NullScope`,以避免不必要的内存分配。
当作用域支持计划在以后实现或者根本不需要时,这是一种常见的模式。

```csharp
public class NickLogger : ILogger
{
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return NullScope.Instance;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        throw new NotImplementedException();
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        throw new NotImplementedException();
    }
}

internal sealed class NullScope : IDisposable
{
    public static NullScope Instance { get; } = new();

    private NullScope() { }

    public void Dispose() { }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/creating-our-own-logger-provider-54121649/?t=100)

`IsEnabled` 方法根据消息的 `LogLevel` 决定是否应当记录它。
一种简单的实现是启用除 `LogLevel.None` 之外的所有级别。

```csharp
public bool IsEnabled(LogLevel logLevel)
{
    return logLevel != LogLevel.None;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/creating-our-own-logger-provider-54121649/?t=115)

在 `Log` 方法中,你可以访问记录一条消息所需的全部参数,包括日志级别、事件 ID、状态以及一个可选的异常。
`Log` 方法的第一步应当始终是检查 `IsEnabled`。
这能确保你不会把处理能力浪费在格式化或写入那些已被当前配置过滤掉的日志上。
如果已启用,你就可以把日志写入你想要的输出,例如控制台。

```csharp
public void Log<TState>(
    LogLevel logLevel, EventId eventId,
    TState state, Exception? exception,
    Func<TState, Exception?, string> formatter)
{
    if (!IsEnabled(logLevel))
    {
        return;
    }

    Console.WriteLine($"[{logLevel}({eventId})]: {state}");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/creating-our-own-logger-provider-54121649/?t=160)

实现好提供程序和 logger 之后,你就可以在应用程序的日志配置中注册这个提供程序。
这是通过在 `builder.Logging` 对象上调用 `AddProvider` 来完成的。
如果你希望自定义提供程序成为唯一的日志机制,也可以选择清除已有的提供程序。

```csharp
//builder.Logging.AddConsole();
builder.Logging.AddProvider(new NickLoggerProvider());
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/creating-our-own-logger-provider-54121649/?t=175)

```text
builder.Logging.AddProvider(new NickLoggerProvider())
        |
        v
NickLoggerProvider.CreateLogger(categoryName) --> new NickLogger()
        |
        v
NickLogger.Log(...)
   IsEnabled(logLevel)? -- no --> return
        | yes
        v
Console.WriteLine($"[{logLevel}({eventId})]: {state}")
```

## 7. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121650/) · 0:47

### 总结

本节全面回顾了 .NET 中的日志提供程序,详细说明了它们作为日志消息最终目的地的角色。
本节探讨了各种项目模板(包括 ASP.NET Core、Generic Host 和独立的控制台应用程序)中可用的内置提供程序,并演示了如何集成 Azure Application Insights 这样的外部提供程序。
此外,各课还涵盖了高级配置技巧,例如基于执行环境的条件式提供程序注册,以及使用 ILoggerProvider 和 ILogger 接口实现自定义日志提供程序。

### 核心概念

- 日志提供程序作为日志目的地的重要性。
- 不同 .NET 应用程序类型(ASP.NET Core、Generic Host、Console)的内置提供程序。
- 在有 host 和无 host 的环境中集成 Application Insights。
- 基于环境设置的动态提供程序注册。
- 自定义日志提供程序和 logger 的实现。

### 课程笔记

本节一开始定义了日志提供程序,以及它们在 .NET 日志生态中的关键作用。
提供程序是负责接收日志消息并把它们写入特定输出的组件,例如控制台、文件或基于云的监控服务。

内置提供程序因应用程序类型而异。
ASP.NET Core 和 Generic Host 应用程序自带一组预先配置好的提供程序(例如 Console 和 Debug),而基础的控制台应用程序则需要手动设置。

本节有相当一部分内容聚焦于集成 Azure Application Insights。
在 ASP.NET Core 环境中,这通常通过依赖注入来处理。
不过,它也可以在独立的控制台应用程序中实现,方法是手动配置 `ServiceCollection` 和 `TelemetryConfiguration`。

```csharp
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using var channel = new InMemoryChannel();

try
{
    IServiceCollection services = new ServiceCollection();
    services.Configure<TelemetryConfiguration>(x => x.TelemetryChannel = channel);
    services.AddLogging(x =>
    {
        x.AddApplicationInsights(
            configureTelemetryConfiguration: teleConfig =>
                teleConfig.ConnectionString =
                    "InstrumentationKey=3f0c4b53-89b6-4b84-811c-5659284c9071;IngestionEndpoint=https://northeurope-2.in.applicationinsights.azure.com/;LiveEndpoint=https://northeurope.livediagnostics.monitor.azure.com/",
            configureApplicationInsightsLoggerOptions: _ => { });
    });

    var serviceProvider = services.BuildServiceProvider();

    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
    
    logger.LogInformation("Hello from console!");
}
finally
{
    await channel.FlushAsync(default);
    await Task.Delay(1000);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121650/?t=17)

本节还讨论了如何动态管理提供程序的注册。
通过检查应用程序环境(例如 `builder.Environment.IsDevelopment()`),开发者可以清除默认提供程序,并注册适合该环境的特定提供程序,例如本地调试时使用自定义提供程序,生产环境使用 Application Insights。

```csharp
using BasicMinimalApi;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Logging.ClearProviders();
    builder.Logging.AddProvider(new NickLoggerProvider());
}
else
{
    builder.Logging.ClearProviders();
    builder.Logging.AddApplicationInsights(
        configureTelemetryConfiguration: teleConfig =>
            teleConfig.ConnectionString = "InstrumentationKey=3f0c4b53-89b6-4b84-811c-5659284c9071;IngestionEndpoint=https://northeurope-2.in.applicationinsights.azure.com/;LiveEndpoint=https://northeurope.livediagnostics.monitor.azure.com/",
        configureApplicationInsightsLoggerOptions: _ => { });
}

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121650/?t=29)

最后,本节以实现一个自定义日志提供程序作结。
这需要创建一个实现 `ILoggerProvider` 的类来管理 logger 的创建,以及一个实现 `ILogger` 的类来处理实际的日志写入逻辑。

```csharp
namespace BasicMinimalApi;

public class NickLoggerProvider : ILoggerProvider
{
    public void Dispose() { }

    public ILogger CreateLogger(string categoryName)
    {
        return new NickLogger();
    }
}

public class NickLogger : ILogger
{
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return NullScope.Instance;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel != LogLevel.None;
    }

    public void Log<TState>(
        LogLevel logLevel, EventId eventId, 
        TState state, Exception? exception, 
        Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }
        
        Console.WriteLine($"[{logLevel}({eventId})]: {state}");
    }
    
    internal sealed class NullScope : IDisposable
    {
        public static NullScope Instance { get; } = new();

        private NullScope() { }
        
        public void Dispose() { }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121650/?t=36)
