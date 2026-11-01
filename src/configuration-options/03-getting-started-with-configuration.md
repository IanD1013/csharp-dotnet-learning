# Getting started with Configuration

> 课程:[From Zero to Hero: Configuration and Options in .NET](https://dometrain.com/course/from-zero-to-hero-configuration-and-options-in-dotnet/) · 第 3 章
> 共 10 课 · 约 18:11
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/introduction-58112011/) | 1:07 | [↓](#1-introduction) |
| 2 | [Standalone vs. Host](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/standalone-vs-host-58112012/) | 4:04 | [↓](#2-standalone-vs-host) |
| 3 | [What’s a ConfigurationManager?](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/whats-a-configurationmanager-58112013/) | 1:21 | [↓](#3-whats-a-configurationmanager) |
| 4 | [Exploring the Worker Service default configuration](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/exploring-the-worker-service-default-configuration-58112014/) | 1:30 | [↓](#4-exploring-the-worker-service-default-configuration) |
| 5 | [Running the Worker Service with template behavior](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/running-the-worker-service-with-template-behavior-58112015/) | 0:34 | [↓](#5-running-the-worker-service-with-template-behavior) |
| 6 | [Learning the JSON configuration provider capabilities](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/learning-the-json-configuration-provider-capabilities-58112016/) | 1:45 | [↓](#6-learning-the-json-configuration-provider-capabilities) |
| 7 | [Environment specific configuration](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/environment-specific-configuration-58112017/) | 2:56 | [↓](#7-environment-specific-configuration) |
| 8 | [Configuring and manually parsing custom values](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/configuring-and-manually-parsing-custom-values-58112018/) | 1:43 | [↓](#8-configuring-and-manually-parsing-custom-values) |
| 9 | [Benefiting from the Binder package](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/benefiting-from-the-binder-package-58112019/) | 1:40 | [↓](#9-benefiting-from-the-binder-package) |
| 10 | [Section Recap](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/section-recap-58112020/) | 1:31 | [↓](#10-section-recap) |

## 1. Introduction

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/introduction-58112011/) · 1:07

### 总结

本课介绍如何借助 Microsoft Extensions Configuration 生态,从基础的控制台应用配置过渡到更高级的场景。
它重点关注独立(standalone)应用与托管(hosted)应用之间的区别、如何使用 Configuration Binder 进行强类型数据映射,以及建立 Options 模式所需的基础心智模型。

### 核心概念

- 独立应用与托管应用
- `Microsoft.Extensions.Configuration.Binder` 包
- 强类型对象绑定
- Options 模式的基础 API
- 以 Worker Service 作为配置的使用场景

### 课程笔记

在基础的 `Microsoft.Extensions.Configuration` NuGet 包之上,本节探讨配置如何应用于不同的应用架构。
其中一个主要关注点是独立应用与托管应用之间的区别。

本节引入的一个关键组件是 `Microsoft.Extensions.Configuration.Binder` 包。
这个库构建在核心配置系统之上,提供把配置数据直接绑定到强类型 C# 对象的 API。
要摆脱基于字符串的键查找、转向更健壮且类型安全的配置模型,这项能力至关重要。

理解这些较底层的绑定 API,是为后续章节中讲到的 Options 模式打基础。
这些概念的实际应用将通过一个 Worker Service 模板来演示。

## 2. Standalone vs. Host

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/standalone-vs-host-58112012/) · 4:04

### 总结

本课讲解 .NET 中独立配置模型与托管配置模型的区别。
独立模型要求开发者手动管理 ConfigurationBuilder,包括添加配置源以及构建配置根。
相比之下,由 Microsoft.Extensions.Hosting 驱动的托管模型会把这一过程自动化,它提供一个预先配置好的 HostApplicationBuilder,默认就支持环境变量、命令行参数、JSON 文件和用户机密(user secrets)。

### 核心概念

- 独立应用模型与托管应用模型
- 使用 ConfigurationBuilder 手动管理配置
- 通过 Host.CreateApplicationBuilder 获得自动化的配置默认值
- Microsoft.Extensions.Hosting 包的传递依赖
- 默认配置源:appsettings.json、环境变量和命令行参数

### 课程笔记

在 .NET 中,配置可以通过两种主要模型来管理:独立模型和托管模型。
独立模型给予开发者有限的(finite)控制,但要求开发者手动负责整个配置管道。
在这种模型中,你必须手动实例化一个 ConfigurationBuilder,添加配置源(例如内存集合),再调用 Build() 生成一个 IConfigurationRoot。

```csharp
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder();

builder.AddInMemoryCollection(initialData: [
    new KeyValuePair<string, string?>(key: "Key", value: "123 That was easy ... "),
    new KeyValuePair<string, string?>(key: "Nested:Sub:Section", value: Math.PI.ToString()),
    new KeyValuePair<string, string?>(key: "ConnectionStrings:Example", value: "<example-database-cs>")
]);

var sources = builder.Sources;

var config = builder.Build();

var providers = config.Providers;

config.GetChildren();
config.GetSection("Nested:Sub:Section");
config.GetConnectionString("Example");
config.GetDebugView();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/standalone-vs-host-58112012/?t=10)

```text
ConfigurationBuilder
  + AddInMemoryCollection(...)   -> builder.Sources
  |
  v  Build()
IConfigurationRoot (config)      -> config.Providers
```

虽然独立方式在某些特定场景下很有用,但随着应用的增长,它可能会变得笨重。
更常见的做法是托管模型,它利用 Microsoft.Extensions.Hosting NuGet 包。
这个包是 .NET 的基础组成部分,提供依赖注入、日志记录以及一套健壮的配置默认值等好处。

```xml
<Project Sdk="Microsoft.NET.Sdk.Worker">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <UserSecretsId>dotnet-getting_started-b7896659-6a1d-4040-9f12-a06e426de233</UserSecretsId>
    <RootNamespace>getting_started</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.Extensions.Hosting" Version="8.0.0" />
  </ItemGroup>
</Project>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/standalone-vs-host-58112012/?t=115)

托管模型使用 Host 类型,它暴露一个静态的 CreateApplicationBuilder 方法。
该方法返回一个 HostApplicationBuilder,它预先配置了若干配置源:
- 以 DOTNET_ 为前缀的环境变量。
- 命令行参数。
- appsettings.json。
- 特定于环境的 JSON 文件(例如 appsettings.Development.json)。
- 用户机密。

Microsoft.Extensions.Hosting 包包含大量支撑这些功能的传递依赖,例如 Microsoft.Extensions.Configuration.Json、Microsoft.Extensions.Configuration.EnvironmentVariables 和 Microsoft.Extensions.Configuration.Binder。

```csharp
using getting_started;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>();

using IHost host = builder.Build();
host.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/standalone-vs-host-58112012/?t=175)

使用托管模型就好比驾驶一辆自动挡汽车;独立模型中需要的许多手动步骤("手动挡"方式)都由框架自动处理了。

## 3. What’s a ConfigurationManager?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/whats-a-configurationmanager-58112013/) · 1:21

### 总结

.NET 中的 ConfigurationManager 是一个专门的类,它通过同时实现 IConfigurationBuilder 和 IConfigurationRoot 接口来表示可变的配置。
这种设计使得管理器在添加新配置源时能够动态更新其内部的配置视图,确保即使应用仍在配置过程中,也能访问到最新的值。
这在托管场景中尤为关键,例如在 HostApplicationBuilder 中,经常需要读取配置值来影响服务注册和依赖注入的设置。

### 核心概念

- 双重职责:ConfigurationManager 既充当构建器(用于添加配置源),又充当构建结果的配置根(用于读取值)。
- 可变性:与生成静态根的传统构建器不同,管理器会在配置源被修改或添加时动态更新其视图。
- 托管集成:在应用启动的服务注册阶段访问配置值时必不可少。

### 课程笔记

在标准的 .NET Worker Service 模板中,应用通过 `Host.CreateApplicationBuilder(args)` 来初始化。
这个构建器实例提供了对应用基础设施核心组件的访问,其中包括它的配置系统。

```csharp
using GettingStarted;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/whats-a-configurationmanager-58112013/?t=10)

`builder.Configuration` 属性是管理应用设置的主要入口。
通过访问这个属性,开发者既可以添加新的配置源,也可以读取现有的值。

```csharp
using GettingStarted;

var builder = Host.CreateApplicationBuilder(args);

_ = builder.Configuration;

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/whats-a-configurationmanager-58112013/?t=70)

`Configuration` 属性的类型是 `Microsoft.Extensions.Configuration.ConfigurationManager`。
这个类的独特之处在于,它表示一个同时实现了 `IConfigurationBuilder` 和 `IConfigurationRoot` 的可变配置对象。

```csharp
namespace Microsoft.Extensions.Configuration
{
    /// <summary>
    /// Represents a mutable configuration object.
    /// </summary>
    /// <remarks>
    /// It is both an <see cref="IConfigurationBuilder"/> and an <see cref="IConfigurationRoot"/>.
    /// As sources are added, it updates its current view of configuration.
    /// </remarks>
    public sealed class ConfigurationManager : IConfigurationManager, IConfigurationRoot, IConfigurationBuilder
    {
        private readonly ConfigurationSources _sources;
        private readonly ConfigurationBuilderProperties _properties;
        private readonly ReferenceCountedProviderManager _providers;
        private readonly List<IDisposable> _changeTokenRegistrations;
        private ConfigurationReloadToken _changeToken = new ConfigurationReloadToken();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/whats-a-configurationmanager-58112013/?t=30)

```text
               ConfigurationManager
              /                    \
  IConfigurationBuilder        IConfigurationRoot
  (添加配置源)                 (读取值)
```

由于它既是构建器又是构建结果的根,`ConfigurationManager` 会在添加配置源时动态更新其配置视图。
这在托管场景中是一个强大的特性。
在传统的设置方式中,你可能必须先构建配置,然后才能从中读取。
有了 `ConfigurationManager`,即使在服务注册期间,值也已经可以直接使用,从而让配置能够影响服务在依赖注入容器中的注册方式。

## 4. Exploring the Worker Service default configuration

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/exploring-the-worker-service-default-configuration-58112014/) · 1:30

### 总结

本课审视 .NET Worker Service 模板的默认结构,重点关注 Worker 类及其 ExecuteAsync 方法。
它着重介绍了使用主构造函数进行依赖注入、日志检查的实现,并指出硬编码的值(具体来说是任务延迟)是迁移到外部配置的首要候选。

### 核心概念

* Worker Service 模板结构以及 BackgroundService 基类。
* 使用 C# 主构造函数进行依赖注入。
* ExecuteAsync 生命周期方法的实现。
* 使用 ILogger.IsEnabled 进行条件日志记录。
* 识别应当迁移到配置中的硬编码值。

### 课程笔记

标准的 .NET Worker Service 模板为长时间运行的后台任务提供了基础。
在这个实现中,Worker 类继承自 BackgroundService,并利用主构造函数注入一个 ILogger<Worker> 实例。

核心逻辑位于 ExecuteAsync 方法中,它会无限运行,直到应用通过 CancellationToken 收到停止请求。

```csharp
namespace GettingStarted;

public sealed class Worker(ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(logLevel: LogLevel.Information))
            {
                logger.LogInformation(
                    message: "Worker running at: {time}",
                    args: DateTimeOffset.Now);
            }

            await Task.Delay(
                millisecondsDelay: 1_000, cancellationToken: stoppingToken);
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/exploring-the-worker-service-default-configuration-58112014/?t=10)

在执行循环中,worker 执行两个主要动作:
1. 日志记录:它在记录当前时间之前,先用 logger.IsEnabled 检查 Information 日志级别是否启用。
   这是一种标准做法,用于在配置中禁用了特定日志级别时,避免不必要的字符串格式化或日志处理。
2. 延迟:循环使用 Task.Delay 暂停一段指定的时长。

在这个默认实现中,一个关键的观察是 1,000 毫秒的硬编码延迟。
像这样硬编码值会限制灵活性,因为改变 worker 的执行频率需要修改代码并重新编译。
这个具体的值正是 .NET 配置系统的首要候选,配置系统允许在不修改源代码的情况下从外部管理此类设置。

## 5. Running the Worker Service with template behavior

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/running-the-worker-service-with-template-behavior-58112015/) · 0:34

### 总结

本课演示标准 .NET Worker Service 模板的运行,重点介绍其默认的日志行为以及内置的 JSON 配置提供程序。

### 核心概念

- Worker Service 执行循环
- 默认日志行为
- 内置的 JSON 配置提供程序

### 课程笔记

运行应用后,worker 服务开始其执行循环。
控制台输出每秒显示一条信息级日志,确认服务处于活动状态并正在执行其计划任务。

worker 的实现定义在 `Worker` 类中,它继承自 `BackgroundService`。
`ExecuteAsync` 方法包含后台任务的逻辑,使用主构造函数注入一个 `ILogger`,并使用取消令牌来管理循环的生命周期。

```csharp
namespace GettingStarted;

public sealed class Worker(ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation(
                    "Worker running at: {time}",
                    DateTimeOffset.Now);
            }

            await Task.Delay(
                1_000, stoppingToken);
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/running-the-worker-service-with-template-behavior-58112015/?t=25)

除了执行逻辑之外,Worker Service 模板还预先配置了一个 JSON 配置提供程序。
这个提供程序是一项强大的功能,它让应用默认就能从 JSON 文件加载设置,后续课程将对此进行探讨。

## 6. Learning the JSON configuration provider capabilities

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/learning-the-json-configuration-provider-capabilities-58112016/) · 1:45

### 总结

本课探讨 .NET 中的 JSON 配置提供程序,它通过传递性 NuGet 包默认包含在托管模板中。
它演示了应用如何从 `appsettings.json` 读取日志配置,并解释了特定于环境的配置文件(例如 `appsettings.Development.json`)的优先级,这类文件可以在本地开发期间覆盖基础设置。

### 核心概念

- JSON 配置提供程序:自动包含在 .NET 托管模板中,使应用能够从 JSON 文件读取设置。
- appsettings.json:用于定义应用级设置的主配置文件。
- 日志配置:用于为默认类别以及 `Microsoft.Hosting.Lifetime` 等特定命名空间定义日志级别的结构。
- 特定于环境的配置:像 `appsettings.Development.json` 这样的文件,提供特定于环境的覆盖,优先级高于基础配置文件。

### 课程笔记

.NET Worker Service 模板通过传递依赖包含了 JSON 配置提供程序。
这个提供程序让应用能够从 JSON 文件获取配置值。
在典型的 Worker Service 中,`Worker` 类利用一个 `ILogger`,根据配置的日志级别输出信息。

```csharp
public sealed class Worker(ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation(
                    "Worker running at: {time}",
                    DateTimeOffset.Now);
            }

            await Task.Delay(
                1_000, stoppingToken);
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/learning-the-json-configuration-provider-capabilities-58112016/?t=10)

日志的默认配置定义在 `appsettings.json` 中。
它指定默认日志级别为 `Information`,并且 `Microsoft.Hosting.Lifetime` 命名空间也应以 `Information` 级别记录日志。

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/learning-the-json-configuration-provider-capabilities-58112016/?t=25)

当把基础的 `appsettings.json` 修改为更严格的级别时,例如默认为 `Warning`、托管生命周期为 `Error`,你可能会预期应用不再显示信息级日志。

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft.Hosting.Lifetime": "Error"
    }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/learning-the-json-configuration-provider-capabilities-58112016/?t=55)

然而,运行应用后可能会发现它仍然以 `Information` 级别记录日志。
这是因为 .NET 配置支持特定于环境的文件。
在开发环境中,`appsettings.Development.json` 文件会在 `appsettings.json` 之后加载。
由于它包含自己的日志配置,它会覆盖基础文件中设置的值。

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/learning-the-json-configuration-provider-capabilities-58112016/?t=100)

```text
  appsettings.json              Default: Warning
                                Microsoft.Hosting.Lifetime: Error
        |  (先加载)
        v
  appsettings.Development.json  Default: Information
                                Microsoft.Hosting.Lifetime: Information
        |  (后加载,覆盖)
        v
  最终生效                      Default: Information
                                Microsoft.Hosting.Lifetime: Information
```

## 7. Environment specific configuration

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/environment-specific-configuration-58112017/) · 2:56

### 总结

.NET 中特定于环境的配置让应用能够根据执行环境(例如 Development、Staging 或 Production)调整其行为。
通过使用 DOTNET_ENVIRONMENT 环境变量(本地开发时通常在 launchSettings.json 中定义),.NET 配置系统会自动加载 appsettings.Development.json 这样的特定于环境的文件。
这些文件会覆盖基础 appsettings.json 中的设置,从而在不修改核心应用代码的情况下,对日志级别等功能进行细粒度控制。

### 核心概念

- `DOTNET_ENVIRONMENT` 环境变量
- `launchSettings.json` 配置文件(profiles)
- 配置分层与特定于环境的覆盖
- `appsettings.{Environment}.json` 命名约定
- 通过配置进行日志级别过滤

### 课程笔记

.NET 应用的行为通常由其环境决定。
这主要通过 `DOTNET_ENVIRONMENT` 环境变量来管理。
在开发场景中,这个变量通常定义在 `launchSettings.json` 文件中,该文件指定了本地开发期间应用应如何启动。

```json
{
  "$schema": "http://json.schemastore.org/launchsettings.json",
  "profiles": {
    "getting_started": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "environmentVariables": {
        "DOTNET_ENVIRONMENT": "Development"
      }
    }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/environment-specific-configuration-58112017/?t=25)

当调用 `Host.CreateApplicationBuilder(args)` 时,`ConfigurationManager`(它既充当构建器,又充当配置实例)会自动把环境变量作为一个配置源包含进来。
这让应用能够检测其当前环境,并相应地调整其配置加载策略。

```csharp
using GettingStarted;

var builder = Host.CreateApplicationBuilder(args);

_ = builder.Configuration;

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/environment-specific-configuration-58112017/?t=40)

默认情况下,应用从基础的 `appsettings.json` 文件加载配置。
这个文件通常包含适用于所有环境的通用设置。

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/environment-specific-configuration-58112017/?t=10)

然而,由于环境被设置为 "Development",系统还会查找 `appsettings.Development.json`。
这个特定于环境的文件中的设置会覆盖基础文件中的设置。
例如,要把日志行为改为:应用默认只显示警告,但为托管生命周期提供详细的跟踪信息,就可以更新这个开发专用文件:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft.Hosting.Lifetime": "Trace"
    }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/environment-specific-configuration-58112017/?t=90)

```text
launchSettings.json
  DOTNET_ENVIRONMENT = "Development"
        |
        v
Host.CreateApplicationBuilder(args)  (ConfigurationManager)
        |
        +--> appsettings.json               (基础设置)
        |
        +--> appsettings.Development.json   (覆盖基础设置)
```

在这个配置下,`Microsoft.Hosting.Lifetime` 的 `Information` 级别日志仍然可见,因为 `Information` 高于 `Trace`。
反之,一般的应用日志会被抑制,除非它们达到 `Warning` 或更高级别。
下面的 `Worker` 实现演示了这一行为,它使用 `Random.Shared` 有条件地记录警告,以模拟间歇性问题。

```csharp
public sealed class Worker(ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(logLevel: LogLevel.Information))
            {
                logger.LogInformation(
                    message: "Worker running at: {time}",
                    args: DateTimeOffset.Now);
            }

            if (Random.Shared.Next(maxValue: 100) < 30 &&
                logger.IsEnabled(logLevel: LogLevel.Warning))
            {
                logger.LogWarning(message: """
                    Seeing this roughly 30% off the time ...
                    """);
            }

            await Task.Delay(
                millisecondsDelay: 1_000, cancellationToken: stoppingToken);
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/environment-specific-configuration-58112017/?t=145)

使用这些设置运行应用时,控制台会显示托管生命周期的信息级日志以及 worker 的警告日志,而 worker 的标准信息级日志则会根据特定于环境的配置被过滤掉。

## 8. Configuring and manually parsing custom values

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/configuring-and-manually-parsing-custom-values-58112018/) · 1:43

### 总结

本课演示如何通过把硬编码的值迁移到配置文件中,将应用行为外部化。
通过把 IConfiguration 注入到后台服务中,开发者可以读取自定义设置(例如任务延迟间隔),并手动将其解析为 TimeSpan 这样的强类型对象。
这种方式让应用的执行逻辑能够受特定于环境的设置影响,而无需修改代码。

### 核心概念

- 把设置外部化到 appsettings.json
- 通过主构造函数注入 IConfiguration
- 使用字符串键访问配置值
- 手动解析配置字符串(例如 TimeSpan.Parse)
- 为缺失的配置键实现回退值/默认值

### 课程笔记

为了让应用更灵活,应当把硬编码的值迁移到配置文件中。
在 .NET 应用中,这通常意味着向 appsettings.Development.json 这样的配置源添加键。
例如,可以添加一个 Delay 设置来控制后台 worker 的执行频率。

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "Delay": "0:0:05"
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/configuring-and-manually-parsing-custom-values-58112018/?t=30)

配置定义好之后,就可以在应用中通过注入 IConfiguration 接口来访问它。
在 BackgroundService 中,这通常通过主构造函数来完成。
随后就可以按键名读取配置值。
由于配置值以字符串形式存储,它们必须被解析为合适的类型,例如 TimeSpan。
另一个最佳实践是使用空合并运算符提供一个默认值,以防配置源中缺少该键。

```csharp
public sealed class Worker(
    ILogger<Worker> logger,
    IConfiguration config) : BackgroundService
{
    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation(
                    "Worker running at: {time}",
                    DateTimeOffset.Now);
            }

            var delay = TimeSpan.Parse(
                config["Delay"] ?? "00:00:05");

            await Task.Delay(
                delay, stoppingToken);
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/configuring-and-manually-parsing-custom-values-58112018/?t=55)

应用运行时,IConfiguration 系统会汇总来自所有提供程序的值。
现在 worker 会遵循配置的延迟。
如果 Delay 设置为五秒,日志就会反映这个间隔,每条 'Worker running' 记录之间的时间戳相差五秒。

```text
info: GettingStarted.Worker[0]
      Worker running at: 07/01/2024 22:01:13 -05:00
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: C:\Users\david\source\repos\zero-to-hero-configuration-and-options\getting-started
info: GettingStarted.Worker[0]
      Worker running at: 07/01/2024 22:01:18 -05:00
info: GettingStarted.Worker[0]
      Worker running at: 07/01/2024 22:01:23 -05:00
info: GettingStarted.Worker[0]
      Worker running at: 07/01/2024 22:01:28 -05:00
info: GettingStarted.Worker[0]
      Worker running at: 07/01/2024 22:01:33 -05:00
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/configuring-and-manually-parsing-custom-values-58112018/?t=80)

这种方式把应用逻辑与其设置解耦,使得在不同环境之间调整更加容易。

## 9. Benefiting from the Binder package

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/benefiting-from-the-binder-package-58112019/) · 1:40

本课演示如何使用 Microsoft.Extensions.Configuration.Binder 包来简化 .NET 中的配置读取。
开发者不必再通过索引器手动访问配置值并进行字符串解析,而是可以利用 `GetValue<T>` 扩展方法,把配置字符串自动转换为强类型对象,并可选地提供默认值。

### 核心概念

- 手动解析与自动绑定:从基于字符串的索引器访问过渡到类型安全的绑定。
- Microsoft.Extensions.Configuration.Binder:提供把配置绑定到对象的扩展方法的 NuGet 包。
- `GetValue<T>` 方法:一个同时处理类型转换和默认值赋值的泛型扩展方法。
- Options 模式的基础:绑定功能如何作为依赖注入中强类型选项的底层机制。

### 课程笔记

使用标准索引器(`config["Key"]`)访问配置值会返回一个字符串。
当值代表另一种类型(例如 `TimeSpan` 或整数)时,就需要手动解析。
手动解析通常既繁琐又容易出错,尤其是在处理空值检查和默认值的时候。

```csharp
public sealed class Worker(
    ILogger<Worker> logger,
    IConfiguration config) : BackgroundService
{
    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation(
                    "Worker running at: {time}",
                    DateTimeOffset.Now);
            }

            var delay = TimeSpan.Parse(
                config["Delay"] ?? "00:00:05");

            await Task.Delay(
                delay, stoppingToken);
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/benefiting-from-the-binder-package-58112019/?t=10)

为了改进这一点,.NET 提供了 `Microsoft.Extensions.Configuration.Binder` 包。
这个包引入了一些扩展方法,让你可以把配置节直接绑定到 .NET 类型上。
针对单个值,最常用的方法是 `GetValue<T>`。

通过使用 `GetValue<T>`,你可以把目标类型指定为泛型参数。
绑定器会负责把配置字符串转换为所请求的类型。
此外,它还允许你提供一个默认值,当配置键缺失或无法解析时就会使用该默认值。

```csharp
public sealed class Worker(
    ILogger<Worker> logger,
    IConfiguration config) : BackgroundService
{
    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation(
                    "Worker running at: {time}",
                    DateTimeOffset.Now);
            }

            var delay =
                config.GetValue<TimeSpan>(
                    "Delay",
                    TimeSpan.FromSeconds(5));

            await Task.Delay(
                delay, stoppingToken);
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/benefiting-from-the-binder-package-58112019/?t=55)

这种绑定功能是 .NET 中 Options 模式背后的核心机制,Options 模式把整个配置节映射到强类型类上,供依赖注入使用。
使用 binder 包可以减少硬编码的字符串和手动解析逻辑,从而带来更易维护、更健壮的配置管理。

## 10. Section Recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/section-recap-58112020/) · 1:31

### 总结

本课总结 .NET 配置的基础概念,涵盖从独立应用模型到托管应用模型的过渡。
它回顾了 IConfiguration API 在 Worker Service 模板中的使用、JSON 配置文件的层级特性,以及从手动解析配置到使用 binder 包便捷 API 进行类型安全取值的演进。

### 核心概念

- 独立配置模型与托管配置模型
- IConfiguration 和 ConfigurationManager API
- Worker Service 模板中的默认配置
- JSON 配置提供程序与特定于环境的覆盖
- 使用 Microsoft.Extensions.Configuration.Binder 包进行配置绑定

### 课程笔记

本课回顾 .NET 配置的基础概念,首先是独立应用模型与托管应用模型之间的区别。
ConfigurationManager 被介绍为管理配置源的核心 API。
本课使用标准的 Worker Service 模板,演示了配置如何被自动接入,以及默认设置如何被应用。

JSON 配置提供程序是一个重点,具体来说是 appsettings.json 和 appsettings.Development.json 的使用。
本课说明了特定于环境的文件如何覆盖基础配置,从而在开发与生产环境中实现不同的行为。

一个实践性的重构练习是移除硬编码的值(例如 worker 的执行延迟),并用由配置驱动的值来替换它们。
虽然可以手动解析这些值,但本课强调了 Microsoft.Extensions.Configuration.Binder 包的高效。
通过使用 GetValue<T> 扩展方法,开发者可以在读取配置值时获得自动类型转换和回退默认值。

```csharp
namespace GettingStarted;

public sealed class Worker(
    ILogger<Worker> logger,
    IConfiguration config) : BackgroundService
{
    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation(
                    "Worker running at: {time}",
                    DateTimeOffset.Now);
            }

            var delay =
                config.GetValue<TimeSpan>(
                    "Delay",
                    TimeSpan.FromSeconds(5));

            await Task.Delay(
                delay, stoppingToken);
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/section-recap-58112020/?t=10)

这些概念为接下来关于内置配置提供程序以及创建自定义提供程序的章节奠定了基础。
