# Introduction to Logging

> 课程:[From Zero to Hero: Logging in .NET](https://dometrain.com/course/from-zero-to-hero-logging-in-dotnet/) · 第 2 章
> 共 6 课 · 约 13:10
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [What is Logging?](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/what-is-logging-54121589/) | 1:58 | [↓](#1-what-is-logging) |
| 2 | [The simplest log entry](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-simplest-log-entry-54121590/) | 1:48 | [↓](#2-the-simplest-log-entry) |
| 3 | [Log categories](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-categories-54121591/) | 1:39 | [↓](#3-log-categories) |
| 4 | [Log Levels](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-levels-54121592/) | 4:46 | [↓](#4-log-levels) |
| 5 | [Log providers (destinations)](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-providers-destinations-54121593/) | 2:25 | [↓](#5-log-providers-destinations) |
| 6 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121594/) | 0:34 | [↓](#6-section-recap) |

## 1. What is Logging?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/what-is-logging-54121589/) · 1:58

### 总结

日志记录是在应用程序中记录事件的时间顺序历史或事件流的做法,用于辅助调试和系统监控。
通过捕获时间戳、事件来源和严重级别(Information、Warning 和 Error)等关键数据,开发者可以在事后排查问题并理解系统行为。
本课先以 Windows 事件查看器(Event Viewer)作为现实中的类比来介绍日志记录的概念,然后再过渡到 .NET 中的实现。

### 核心概念

- **日志记录的定义**:维护系统或应用程序中所发生事件的历史或事件流的行为。
- **主要用途**:故障排查、跟踪问题/错误,以及收集用于调试的系统信息。
- **日志条目的构成**:包括来源、时间戳、消息和严重级别等详细信息。
- **日志级别**:将事件归类为 Information、Warning 和 Error 等级别。

### 课程笔记

日志记录是对应用程序内事件的系统化记录,它构成了一份按时间顺序排列的操作历史。
这个事件流让开发者能够审查系统行为,并通过回顾特定时间点发生的事情来调查问题。
日志记录的主要目的是协助故障排查和调试。
通过跟踪错误、警告和一般信息,开发者可以找出在生产环境或开发环境中发生的问题的根源。

日志记录的一个实际例子是 Windows 事件查看器。
这个应用程序显示一个事件列表,其中每个条目都包含关键的元数据:
- **来源(Source)**:日志条目的来源。
- **时间戳(Timestamp)**:事件被记录的精确时间。
- **消息(Message)**:事件的描述性内容。
- **级别(Level)**:严重程度,例如 Information、Warning 或 Error。

在 .NET 应用程序中,日志记录是通过结构化的框架来实现的。
下面的项目配置展示了一个使用 `Microsoft.Extensions.Logging` 套件的基础控制台应用程序所需的依赖项:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/what-is-logging-54121589/?t=114)

应用程序代码使用 `LoggerFactory` 初始化日志记录基础设施。
这个工厂被配置为添加控制台输出,从而可以在终端中查看日志消息:

```csharp
using Microsoft.Extensions.Logging;

#region secret 🤭

using var loggerFactory = LoggerFactory.Create(builder =>
{
#pragma warning disable CA1416
    builder
        .AddConsole();
#pragma warning restore CA1416
});

ILogger CreateLogger()
{
    return loggerFactory.CreateLogger("Course");
}
#endregion

ILogger logger = CreateLogger();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/what-is-logging-54121589/?t=114)

## 2. The simplest log entry

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-simplest-log-entry-54121590/) · 1:48

### 总结

本课介绍在 .NET 中使用标准 `ILogger` 接口创建日志条目的基本方式。
它演示了如何向控制台应用程序记录一条信息级消息,并着重介绍了输出中包含的元数据,例如严重级别和日志类别。

### 核心概念

* **ILogger 接口**:Microsoft 提供的 .NET 标准日志接口,位于 `Microsoft.Extensions.Logging` 命名空间中。
* **日志级别**:`LogInformation` 和 `LogWarning` 等方法,用于定义日志条目的严重程度。
* **控制台输出**:控制台日志记录器的默认行为,包括按颜色区分的严重级别和类别信息。
* **日志类别**:一个标识日志消息来源的标签(通常是类名)。

### 课程笔记

在 .NET 中,日志记录的主要接口是 `ILogger`。
这个接口由 Microsoft 提供,是在不依赖第三方库的情况下实现日志记录的标准方式。

要创建一条基本的日志条目,首先要获取一个日志记录器实例。
在这个例子中,创建的是一个用于控制台应用程序的日志记录器。
有了日志记录器之后,就可以调用特定的方法,以不同的严重级别记录消息。
最基本的条目是一条信息级日志。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-simplest-log-entry-54121590/?t=10)

执行这段代码时,控制台会显示消息"Hello world!"以及额外的元数据。
这些元数据包括:

1. **严重级别**:输出表明这是一条信息级日志。
   如果把严重级别改为警告(使用 `LogWarning`),控制台输出通常会改变颜色(例如变为橙色),以表示更高的严重程度。
2. **日志类别**:输出中包含一个类别名称(例如项目名或类名)。
   这有助于识别日志的来源。

## 3. Log categories

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-categories-54121591/) · 1:39

### 总结

.NET 中的日志类别提供了一种逻辑机制,用于把相关的日志条目归入同一个标识符之下,这对于筛选和配置日志行为至关重要。
通过从非泛型的 ILogger 过渡到泛型的 ILogger<T> 接口,开发者可以自动把类的完全限定名指定为日志类别。
这种分类方式支持细粒度的控制,例如为应用程序中特定的命名空间或类设置不同的日志级别。

### 核心概念

*   **日志类别**:用于对日志条目分组的逻辑名称,通常代表日志产生所在的类或组件。
*   **ILogger<T>**:泛型日志接口,使用 `T` 的类型名称自动定义日志类别。
*   **逻辑分组**:类别让开发者能够独立管理来自不同来源的日志(例如应用程序内部代码与 ASP.NET Core 等第三方库)。
*   **配置**:类别与日志级别结合使用,用来筛选哪些消息会被特定的日志提供程序捕获。

### 课程笔记

在控制台中查看日志输出时,每个条目通常都包含一个类别字符串。
这个类别不一定是应用程序或项目的名称,而是一种逻辑分组,把日志条目放在某个特定的"伞"之下。
例如,Microsoft 把框架内部的日志归入 `Microsoft.AspNetCore` 类别之下。

开发者对这些类别拥有完全的控制权。
在一个基础实现中,`ILogger` 可能是在没有特定类型上下文的情况下创建的,这可能导致一个默认的或手动指定的类别,而它无法清楚地说明日志的来源。

```csharp
using Microsoft.Extensions.Logging;

// ... setup code ...

ILogger logger = CreateLogger();

logger.LogInformation("Hello world!");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-categories-54121591/?t=10)

为了提供更好的上下文和更易维护的代码,.NET 使用 `ILogger<T>` 接口。
通过指定一个类型参数(例如当前类),日志记录基础设施会自动获取该类型的名称,并把它用作日志类别。
这确保了每条日志条目都明确地与产生它的类关联起来。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-categories-54121591/?t=65)

把日志与特定类别关联起来是配置的基础。
它让开发者能够定义把类别和日志级别结合起来的规则,例如为自己的应用程序代码启用 `Information` 级别的日志,同时把 `Microsoft` 框架日志限制为只记录 `Warning` 或 `Error` 级别。

## 4. Log Levels

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-levels-54121592/) · 4:46

### 总结

.NET 中的日志级别定义了日志消息的严重程度和重要性,让开发者能够对诊断信息进行分类,并根据环境筛选输出。
内置的日志提供程序使用 LogLevel 枚举,其范围从用于最细粒度细节的 Trace,一直到用于不可恢复系统故障的 Critical。
理解这些级别对于在开发环境和生产环境中有效地监控、调试和维护应用程序健康状况至关重要。

### 核心概念

- **严重程度层级**:日志的范围从 Trace(0)到 Critical(5),None(6)用于配置。
- **Trace**:非常详细、可能包含敏感数据;默认禁用,且绝不在生产环境中使用。
- **Debug**:用于开发阶段的排查;很少在生产环境中启用。
- **Information**:标准的默认级别,用于跟踪应用程序的一般流程和长期趋势。
- **Warning**:突出显示那些不会让应用程序停止、但需要关注的异常事件。
- **Error**:表示某个特定活动或请求失败,导致其无法成功执行。
- **Critical**:表示需要立即干预的系统级或不可恢复的崩溃。

### 课程笔记

.NET 中的日志记录并不是一刀切的操作。
消息通过日志级别按严重程度加以区分。
使用 `ILogger` 时,通常会调用与这些级别对应的方法,例如 `LogInformation` 或 `LogWarning`。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-levels-54121592/?t=10)

#### LogLevel 枚举

严重级别定义在 `Microsoft.Extensions.Logging.LogLevel` 枚举中。
根据 Microsoft 的指导原则,每个级别都有特定的数值和明确的用途。

```csharp
public enum LogLevel
{
    /// <summary>
    /// Logs that contain the most detailed messages. These messages may contain sensitive application data.
    /// These messages are disabled by default and should never be enabled in a production environment.
    /// </summary>
    Trace = 0,

    /// <summary>
    /// Logs that are used for interactive investigation during development. These logs should contain 
    /// information useful for debugging and have no long-term value.
    /// </summary>
    Debug = 1,

    /// <summary>
    /// Logs that track the general flow of the application. These logs should have long-term value.
    /// </summary>
    Information = 2,

    /// <summary>
    /// Logs that highlight an abnormal or unexpected event in the application flow, but do not otherwise 
    /// cause the application execution to stop.
    /// </summary>
    Warning = 3,

    /// <summary>
    /// Logs that highlight when the current flow of execution is stopped due to a failure. 
    /// These should indicate a failure in the current activity, not an application-wide failure.
    /// </summary>
    Error = 4,

    /// <summary>
    /// Logs that describe an unrecoverable application or system crash, or a catastrophic failure that requires 
    /// immediate attention.
    /// </summary>
    Critical = 5,

    /// <summary>
    /// Not used for writing log messages. Specifies that a logging category should not write any messages.
    /// </summary>
    None = 6,
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-levels-54121592/?t=70)

#### 各级别详细说明

##### Trace (0)

Trace 日志包含最细粒度的细节。
由于它们可能包含敏感数据,因此默认是禁用的,并且绝不应在生产环境中启用,以避免监管或安全问题。

##### Debug (1)

Debug 日志主要用于开发和故障排查。
它们提供额外的上下文,帮助理解应用程序是如何进入某个特定状态的。
虽然有时会在生产环境中临时启用它们来诊断棘手的问题,但它们通常不会出现在生产日志中。

##### Information (2)

这是大多数 .NET 模板中的默认日志级别。
它跟踪应用程序的一般流程,并为分析客户长期以来如何使用应用程序提供长期价值。

##### Warning (3)

警告表示异常或意外的事件。
警告并不意味着应用程序已经崩溃,但它起到"大声"通知的作用,表明发生了某些可能需要调查的不寻常情况。

##### Error (4)

错误表示某个特定活动或请求失败了。
例如,如果因为某个局部问题导致支付无法处理或订单失败,就适合记录一条 Error 日志。
它表示当前的执行流程已经停止,但应用程序本身很可能仍在运行。

##### Critical (5)

Critical 日志代表不可恢复的状态,例如系统级崩溃或灾难性故障。
这些日志需要运维或开发团队立即关注。

##### None (6)

`None` 级别比较特殊,因为它不用于写入日志消息。
相反,它在配置中用于指定让某个特定的日志类别完全静默。

## 5. Log providers (destinations)

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-providers-destinations-54121593/) · 2:25

### 总结

.NET 中的日志提供程序充当日志消息的目的地,让开发者能够通过统一的接口把日志路由到控制台或 Windows 事件日志等多个输出。
这种解耦确保了无论日志是在终端、系统日志还是第三方服务中查看,日志记录逻辑都保持一致。

### 核心概念

- **日志提供程序**:定义日志数据发送到何处的抽象(例如 Console、File、Event Log)。
- **通用接口**:`ILogger` 接口让代码在生成日志时无需知道当前启用了哪些提供程序。
- **多个目的地**:可以同时注册多个提供程序,把同一条日志消息广播给所有提供程序。
- **平台限制**:某些提供程序只适用于特定的操作系统,例如 Windows 事件日志提供程序。

### 课程笔记

日志提供程序是负责接收日志消息并将其转换为适合特定目的地的组件。
运行一个标准的 .NET 应用程序时,控制台通常是默认的提供程序,但这个系统被设计为可以同时支持任意数量的目的地。

要定义这些目的地,需要把提供程序添加到日志配置中。
在下面的例子中,应用程序被配置为同时使用 Console 提供程序和 Windows Event Log 提供程序。
这是通过在日志构建器上调用 `AddConsole()` 和 `AddEventLog()` 实现的。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-providers-destinations-54121593/?t=10)

调用 `logger.LogInformation` 时,消息会被分发给所有已注册的提供程序。
在 Windows 环境中,这会导致消息既出现在控制台输出中,又被记录到 Windows 事件查看器中。
在事件查看器中,日志条目通常会显示类别(例如类名 `Program`)、消息文本以及来源(例如 `.NET Runtime`)。

```text
logger.LogInformation("Hello world!")
                |
                v
     +----------+-----------+
     |                      |
     v                      v
AddConsole()           AddEventLog()
控制台输出             Windows 事件查看器
"Hello world!"         类别: Program
                       消息: Hello world!
                       来源: .NET Runtime
```

需要注意的是,像 Event Log 这样的平台特定提供程序在 Linux 或 macOS 上无法工作。
虽然 Event Log 很适合用来演示提供程序的工作方式,但现代生产环境通常更青睐其他提供程序,例如数据库、文件或专门的第三方日志服务。

## 6. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121594/) · 0:34

### 总结

本课全面总结了 .NET 中日志记录的基础概念,包括日志记录的目的、日志条目的结构,以及日志类别、级别和提供程序的配置。
它巩固了日志记录器工厂的实际实现方式,用于在 .NET 应用程序中管理多个输出目的地。

### 核心概念

* **日志记录的重要性**:对于应用程序的可观测性和故障排查至关重要。
* **日志条目**:被记录信息的基本单位。
* **日志类别**:日志的逻辑分组,通常基于类名或组件名。
* **日志级别**:用于对日志输出进行分类和筛选的严重程度指示。
* **日志提供程序**:日志被发送到的目的地,例如控制台或系统事件日志。

### 课程笔记

日志记录是软件开发中的关键组成部分,它为了解应用程序的运行时行为提供了必要的可见性。
这个过程从创建一条日志条目开始,它捕获某个时间点上的特定事件或状态。

在 .NET 中,日志记录通常通过 `LoggerFactory` 来管理。
这个工厂负责创建 `ILogger` 实例,并管理定义日志存储或显示位置的各个提供程序。
下面的例子演示了如何初始化一个日志记录器工厂并写入一条基本的信息级日志:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121594/?t=7)

类别支持对日志进行逻辑分组,通常映射到生成日志的类或组件。
这通常通过泛型 `ILogger<T>` 接口来实现,其中 `T` 代表类名,正如上面 `ILogger<Program>` 的用法所示。

日志级别对于定义条目的严重程度至关重要,它让开发者能够根据环境或消息的紧急程度来筛选信息。
指定正确的日志级别(例如 Information、Warning 或 Error)是确保日志既有用又不至于泛滥的基础。

最后,日志提供程序定义了这些日志的目的地。
通过使用 `LoggerFactory`,可以同时配置 Console 或 Windows Event Log 等多个提供程序。
要使用特定的提供程序,必须在项目配置中引入相应的 NuGet 包:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121594/?t=22)
