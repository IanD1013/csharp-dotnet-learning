# Exploring the Options Pattern

> 课程:[From Zero to Hero: Configuration and Options in .NET](https://dometrain.com/course/from-zero-to-hero-configuration-and-options-in-dotnet/) · 第 6 章
> 共 16 课 · 约 28:57
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction to the Options Pattern](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/introduction-to-the-options-pattern-58112413/) | 2:09 | [↓](#1-introduction-to-the-options-pattern) |
| 2 | [Modeling the Logging Options](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/modeling-the-logging-options-58112414/) | 2:09 | [↓](#2-modeling-the-logging-options) |
| 3 | [Bind Logging Options to Configuration](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/bind-logging-options-to-configuration-58112416/) | 1:50 | [↓](#3-bind-logging-options-to-configuration) |
| 4 | [Consuming Logging Options through DI](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/consuming-logging-options-through-di-58112417/) | 2:41 | [↓](#4-consuming-logging-options-through-di) |
| 5 | [Adding another options-modeled object](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/adding-another-options-modeled-object-58112418/) | 1:02 | [↓](#5-adding-another-options-modeled-object) |
| 6 | [Binding additional named-options](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/binding-additional-named-options-58112420/) | 1:48 | [↓](#6-binding-additional-named-options) |
| 7 | [Configuring and editing named-options](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/configuring-and-editing-named-options-58112421/) | 1:30 | [↓](#7-configuring-and-editing-named-options) |
| 8 | [Resolving named-options by name](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/resolving-named-options-by-name-58112422/) | 1:30 | [↓](#8-resolving-named-options-by-name) |
| 9 | [Verifying configuration bindings and DI hydration](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/verifying-configuration-bindings-and-di-hydration-58112423/) | 1:13 | [↓](#9-verifying-configuration-bindings-and-di-hydration) |
| 10 | [Considering alternative APIs for registering options](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/considering-alternative-apis-for-registering-options-58112424/) | 1:40 | [↓](#10-considering-alternative-apis-for-registering-options) |
| 11 | [Overriding configurations with post configure APIs](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/overriding-configurations-with-post-configure-apis-58112425/) | 2:15 | [↓](#11-overriding-configurations-with-post-configure-apis) |
| 12 | [Overriding all config-bound instances](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/overriding-all-config-bound-instances-58112426/) | 1:52 | [↓](#12-overriding-all-config-bound-instances) |
| 13 | [Avoid implementing IOptions](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/avoid-implementing-ioptions-58112427/) | 1:58 | [↓](#13-avoid-implementing-ioptions) |
| 14 | [Understanding reflection-based binding](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/understanding-reflection-based-binding-58112428/) | 2:14 | [↓](#14-understanding-reflection-based-binding) |
| 15 | [Enabling source-generated configuration binding](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/enabling-source-generated-configuration-binding-58112429/) | 2:05 | [↓](#15-enabling-source-generated-configuration-binding) |
| 16 | [Section Recap](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/section-recap-58112430/) | 1:01 | [↓](#16-section-recap) |

## 1. Introduction to the Options Pattern

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/introduction-to-the-options-pattern-58112413/) · 2:09

### 总结

Options Pattern 是现代 .NET 中的一种基础设计模式,它提供了一种以强类型方式访问配置设置的途径。
通过把来自各种来源(例如 JSON 文件、环境变量和命令行参数)的配置数据映射到自定义类中,它让我们能够借助依赖注入把这些设置分发到应用程序生态的各个角落。

### 核心概念

* **强类型配置**:把原始配置键映射到 C# 类,以确保类型安全并便于使用。
* **依赖注入集成**:通过内置的 DI 容器向应用程序提供配置对象。
* **多来源支持**:把来自 JSON、XML、环境变量、命令行参数和云端机密的设置整合到同一个模型中。
* **适用于整个生态**:一种统一的方式,适用于 ASP.NET Core、Worker Services、.NET Aspire 和 Azure Functions。

### 课程笔记

Options Pattern 是由 Microsoft .NET 团队引入的一种设计模式,也是现代 .NET 开发的核心组成部分。
它被视为一种最佳实践,与 Generic Host、内置日志以及原生依赖注入(DI)框架等其他基础特性协同工作。

这一模式充当底层配置系统之上的一个抽象层。
它会消费来自各种配置提供程序的数据(包括 JSON 文件、XML 文件、命令行参数、环境变量以及基于云的机密),并把这些数据转换为强类型对象。
这让开发者得以摆脱基于字符串的配置查找,这种查找容易出错,而且难以维护。

要实现这一模式,开发者需要定义自定义的 options 类,用来表示配置中的特定节。
随后把这些类注册到 DI 容器中。
注册之后,配置就会以强类型类的形式提供给 .NET 开发工作负载的整个技术栈,包括 .NET Aspire、ASP.NET Core minimal API、worker service 和 Azure Functions。

```text
JSON / XML / 命令行参数 / 环境变量 / 云端机密
                    |
                    v
          配置提供程序 (configuration)
                    |
                    v
      自定义 options 类 (强类型对象)
                    |
                    v
              DI 容器 -> 应用程序
```

## 2. Modeling the Logging Options

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/modeling-the-logging-options-58112414/) · 2:09

本课演示如何使用 Options pattern 为标准的 .NET 日志配置建模。
内容涵盖创建一个与 JSON 配置相对应的强类型类结构,重点讲解如何通过继承字典类型来处理嵌套对象和动态键(例如命名空间类别)。

### 核心概念

- 把 JSON 配置节映射到 C# 类
- 使用常量表示配置节名称,以提升可维护性
- 使用 `Dictionary<string, TValue>` 处理配置中的动态键
- 用嵌套对象为层级化的配置结构建模

### 课程笔记

在一个标准的 ASP.NET Core minimal API 项目中,`appsettings.json` 文件包含一个默认的 `Logging` 节。
这一节为各个类别定义了日志级别,例如默认级别以及针对 Microsoft 命名空间的特定覆盖。

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/modeling-the-logging-options-58112414/?t=10)

为了应用 options pattern,我们创建一个类来表示这个顶层配置对象。
按照惯例,这个类命名为 `LoggingOptions`。
最佳实践是包含一个 `public const string`,用来定义该节在配置文件中的名称。
这样消费者就能轻松地把该类绑定到正确的节,而无需在应用程序的其他地方硬编码字符串。

```csharp
namespace Options.Api.Logging;

/// <summary>
/// Represents an options "logging" object, containing various
/// log level configuration values mapped from namespaces.
/// </summary>
public sealed class LoggingOptions
{
    /// <summary>
    /// The logging configuration section name. This is exposed by convention to
    /// allow consumers of these options to bind configuration to their named-section.
    /// </summary>
    public const string LoggingConfigurationSectionName = "Logging";

    /// <summary>
    /// Gets or sets the LogLevelOptions value as represented in configuration.
    /// </summary>
    public LogLevelOptions? LogLevel { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/modeling-the-logging-options-58112414/?t=55)

JSON 中的 `LogLevel` 属性包含 `Default` 和 `Microsoft.AspNetCore` 之类的键。
由于这些键可以是任意字符串(代表不同的日志类别或命名空间),它们无法在普通类中被建模为固定属性。
因此,我们改用一个配置绑定器能够填充的集合。

为了表示这种映射,我们创建一个继承自 `Dictionary<string, LogLevel>` 的 `LogLevelOptions` 类。
这让底层配置绑定器能够把 JSON 键映射到字典的键,把 JSON 值映射到 `LogLevel` 枚举值。
使用 `StringComparer.OrdinalIgnoreCase` 可以确保配置键的匹配不区分大小写。

```csharp
/// <summary>
/// A Dictionary{TKey, TValue} where 'TKey' is the logging category or namespace.
/// </summary>
public sealed class LogLevelOptions
    : Dictionary<string, LogLevel>
{
    /// <inheritdoc cref="Dictionary{TKey, TValue}" />
    public LogLevelOptions()
        : base(comparer: StringComparer.OrdinalIgnoreCase)
    {
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/modeling-the-logging-options-58112414/?t=115)

```text
appsettings.json                        C#
"Logging"                    ------>  LoggingOptions
  "LogLevel"                 ------>    LogLevel : LogLevelOptions
    "Default": "Information" ------>      ["Default"] = LogLevel.Information
    "Microsoft.AspNetCore":  ------>      ["Microsoft.AspNetCore"]
        "Warning"                            = LogLevel.Warning
```

定义好这些模型之后,下一步就是在 `Program.cs` 文件中把 `LoggingOptions` 对象绑定到实际的配置上。

## 3. Bind Logging Options to Configuration

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/bind-logging-options-to-configuration-58112416/) · 1:50

### 总结

本课演示如何在 ASP.NET Core 应用程序中把一个配置节绑定到强类型的 options 类。
通过在 service collection 上使用 AddOptions 和 Bind 方法,开发者可以把来自 appsettings.json 或其他来源的配置值直接映射到 C# 类,从而能够通过依赖注入使用 Options pattern。

### 核心概念

- 使用 AddOptions<T>() 注册 options。
- 使用 .Bind() 把配置节绑定到 options 类。
- 在 options 类中利用静态常量来定义配置节名称。
- 把 IOptions<T> 注入 minimal API 处理程序以消费配置。

### 课程笔记

在 ASP.NET Core minimal API 应用程序中,Program.cs 文件是入口点,WebApplicationBuilder 就是在这里创建的。
这个 builder 包含 IServiceCollection,用于为依赖注入注册服务。
应用程序构建完成后,service provider 会按需解析这些依赖。

要实现 Options pattern,你需要使用 AddOptions<T>() 把你的 options 类注册到 service collection 中。
这个方法返回一个 OptionsBuilder<T>,它允许进行进一步的配置,例如绑定到某个特定的配置节。

Bind 方法用于把一个配置节连接到 options 对象。
它需要一个 IConfigurationSection,可以通过一个键从 builder.Configuration 中获取。
在这个实现中,键由 LoggingOptions 类上的一个静态属性(LoggingConfigurationSectionName)提供,从而确保类与配置结构之间保持一致。
这一绑定过程实际上把来自配置源的数据合并到 options 类的属性中,充当配置快照与应用程序内部类型之间的粘合剂。

```csharp
WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<LoggingOptions>()
    .Bind(config: builder.Configuration.GetSection(
        key: LoggingOptions.LoggingConfigurationSectionName));

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet(
    pattern: "/logging/options",
    handler: static (IOptions<LoggingOptions> options) =>
    {
        return Results.Ok(options.Value);
    });
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/bind-logging-options-to-configuration-58112416/?t=10)

```text
builder.Services.AddOptions<LoggingOptions>()   --> OptionsBuilder<LoggingOptions>
    .Bind(config: ...)                              |
                                                    v
builder.Configuration.GetSection(               IConfigurationSection
    key: LoggingOptions.LoggingConfigurationSectionName)   ("Logging")
                                                    |
                                                    v
handler: (IOptions<LoggingOptions> options) --> options.Value
```

options 绑定并注册之后,就可以在整个应用程序中被消费。
在 minimal API 中,这通常是通过把 IOptions<T> 注入路由处理程序来完成的。
框架会自动解析这些 options,并通过 Value 属性提供对已绑定值的访问。

## 4. Consuming Logging Options through DI

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/consuming-logging-options-through-di-58112417/) · 2:41

### 总结

本课演示如何在 .NET 应用程序中通过依赖注入(DI)使用 `IOptions<T>` 接口来消费强类型 options。
内容涵盖在服务容器中注册 `LoggingOptions`,并展示如何把 `IOptions` 包装器注入 Minimal API 端点,以访问底层配置值并将其作为 JSON 响应返回。

### 核心概念

*   **Options 注册**:使用 `builder.Services.AddOptions<T>().Bind()` 把一个配置节关联到一个类。
*   **IOptions<T> 接口**:通过依赖注入来注入和访问已配置 options 的标准包装器。
*   **.Value 属性**:`IOptions<T>` 上的属性,用于暴露强类型 options 类的实际实例。
*   **Minimal API 集成**:把 options 直接注入端点处理程序,以暴露配置状态。

### 课程笔记

要通过 options pattern 消费配置,应用程序必须先在依赖注入容器中注册 options 类。
这是通过在 `IServiceCollection` 上调用 `AddOptions<T>()` 并使用 `Bind` 方法将其绑定到相应的配置节来实现的。
在这个示例中,`LoggingOptions` 被绑定到由 `LoggingOptions.LoggingConfigurationSectionName` 定义的节。

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<LoggingOptions>()
    .Bind(config: builder.Configuration.GetSection(
        key: LoggingOptions.LoggingConfigurationSectionName));

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet(
    pattern: "/logging/options",
    handler: static (IOptions<LoggingOptions> options) =>
    {
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/consuming-logging-options-through-di-58112417/?t=10)

注册之后,就可以在端点处理程序中通过定义一个 `IOptions<LoggingOptions>` 类型的参数来消费这些 options。
.NET 依赖注入系统会自动满足这个参数,提供一个包裹着强类型对象的 `IOptions` 包装器。
`IOptions` 包装器暴露一个 `Value` 属性,它包含在幕后已被实例化并绑定到配置值的那个实例。

```csharp
app.MapGet( //   GET /logging/options HTTP/1.1
            //   Content-Type: application/json
    pattern: "/logging/options",
    handler: static (IOptions<LoggingOptions> options) =>
    {
        var loggingOptions = options.Value;

        return Results.Json(
            data: loggingOptions);
    })
    .WithName(endpointName: "GetLoggingOptions")
    .WithOpenApi();

app.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/consuming-logging-options-through-di-58112417/?t=25)

当端点被执行时(例如通过 Swagger),应用程序会解析 `IOptions<LoggingOptions>` 并返回日志配置的当前状态。
得到的 JSON 负载反映了从配置源(例如 `appsettings.json`)映射而来的值。

```json
{
  "logLevel": {
    "Default": 2,
    "Microsoft.AspNetCore": 3
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/consuming-logging-options-through-di-58112417/?t=145)

这证实了 `IOptions<T>` 包装器能够通过依赖注入成功解析,并把配置值正确映射到强类型对象中。

## 5. Adding another options-modeled object

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/adding-another-options-modeled-object-58112418/) · 1:02

### 总结

本课演示如何使用一个专门的 C# 类为配置节建模,重点关注 FeatureOptions 对象。
它通过包含布尔值、字符串、版本、URI 和数组等多种属性类型,突出了 Options pattern 的灵活性,并说明这些类型如何从配置源自动映射。

### 核心概念

- 把配置节建模为 POCO(Plain Old CLR Object)类。
- 支持多种属性类型,包括 System.Version、System.Uri 和数组。
- 为 options 模型使用 sealed 类,以确保配置结构干净、不可扩展。
- 为在应用程序入口点中进行绑定准备好 options 对象。

### 课程笔记

本课的重点是通过添加一个新的 options 对象来扩展配置模型。
这个对象 `FeatureOptions` 旨在展示 .NET 配置绑定器如何处理不同的数据类型。
这个类是对功能开关或一组配置的简单表示,它是用来演示类型映射的载体,而不是一个可用于生产环境的功能管理系统。

`FeatureOptions` 类在 `Options.Api.Features` 命名空间中被定义为一个 `sealed class`。
它是一个扁平结构,也就是说它不包含嵌套对象,这简化了对属性映射的初步演示。

```csharp
namespace Options.Api.Features;

/// <summary>
/// A representation of a "feature" options object.
/// </summary>
public sealed class FeatureOptions
{
    /// <summary>
    /// Gets or sets whether the feature is enabled.
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// Gets or sets the name of the feature.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the version of the feature.
    /// </summary>
    public Version? Version { get; set; }

    /// <summary>
    /// Gets or sets the endpoint (<see cref="Uri"/>) of the feature.
    /// </summary>
    public Uri? Endpoint { get; set; }

    /// <summary>
    /// Gets or sets the API key of the feature.
    /// </summary>
    public string? ApiKey { get; set; }

    /// <summary>
    /// Gets or sets any tags for the feature.
    /// </summary>
    public string[] Tags { get; set; } = [];
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/adding-another-options-modeled-object-58112418/?t=40)

该模型包含若干配置系统能够自动解析的属性类型:

- **Boolean(`bool`)**:用于 `Enabled` 属性,表示一个简单的开关。
- **String(`string?`)**:用于 `Name` 和 `ApiKey`,存储文本配置。
- **Version(`Version?`)**:展示绑定器能够处理 `System.Version` 之类的标准 .NET 类型,用于语义化版本。
- **URI(`Uri?`)**:展示到 `System.Uri` 的映射,用于服务端点。
- **Array(`string[]`)**:展示一组值(例如标签)如何从配置源映射而来。

通过定义这个模型,应用程序就做好了在 `Program.cs` 文件中把这些属性绑定到配置数据的准备,从而能够以强类型方式访问功能设置。

## 6. Binding additional named-options

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/binding-additional-named-options-58112420/) · 1:48

### 总结

本课讲解如何在 .NET 中使用命名 options(named options),把同一个 options 模型的多个实例绑定到不同的配置节。
通过向 AddOptions 方法提供一个唯一的名称,开发者可以区分同一类型的不同设置集合,从而在多个应用程序组件之间复用同一个 options 类。

### 核心概念

* **Named Options**:一项允许在依赖注入容器中对同一 options 类型进行多次注册的特性。
* **AddOptions<TOptions>(string name)**:用于为 options 实例分配唯一标识符的方法重载。
* **节绑定**:把特定的配置键(例如 "Features:TodoApi")映射到一个命名 options 实例。
* **模型复用**:使用单个 options 类(例如 FeatureOptions)来表示多个不同功能的配置。

### 课程笔记

在 .NET 中,Options pattern 通常把一个类绑定到某个特定的配置节。
例如,LoggingOptions 通常被绑定到由该类自身内部的一个静态字符串所定义的节。

```csharp
var builder = WebApplication.CreateBuilder(args);

// Bind IOptions<LoggingOptions> to the
// "Logging" config section for DI.
builder.Services.AddOptions<LoggingOptions>()
    .Bind(config: builder.Configuration.GetSection(
        key: LoggingOptions.LoggingConfigurationSectionName));
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/binding-additional-named-options-58112420/?t=10)

当你需要为应用程序的不同部分复用同一个 options 模型时,可以利用命名 options。
当你有一个通用模型(例如 FeatureOptions),并且它应当根据上下文填充不同的值时(例如 Todo API 的设置与 Weather Station 的设置),这就很有用。

要实现这一点,向 AddOptions<T> 传入一个字符串名称作为参数。
这个名称把各个注册彼此区分开来。
随后,每个命名注册都可以使用 Bind 方法绑定到各自的配置节。

```csharp
var builder = WebApplication.CreateBuilder(args);

// Bind IOptions<LoggingOptions> to the
// "Logging" config section for DI.
builder.Services.AddOptions<LoggingOptions>()
    .Bind(config: builder.Configuration.GetSection(
        key: LoggingOptions.LoggingConfigurationSectionName));

// Add named configuration options.
builder.Services.AddOptions<FeatureOptions>(
    name: "TodoApi"
    )
    .Bind(config: builder.Configuration.GetSection(
        key: "Features:TodoApi"));

builder.Services.AddOptions<FeatureOptions>(
    name: "WeatherStation"
    )
    .Bind(config: builder.Configuration.GetSection(
        key: "Features:WeatherStation"));

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/binding-additional-named-options-58112420/?t=85)

```text
"Features:TodoApi"        --> AddOptions<FeatureOptions>(name: "TodoApi")
"Features:WeatherStation" --> AddOptions<FeatureOptions>(name: "WeatherStation")
                                         |
                                         v
                       同一个类 FeatureOptions,两个命名实例
```

通过显式命名这些 options,它们会在 DI 容器中分开存储。
之后在注入 IOptionsSnapshot<T> 或 IOptionsMonitor<T> 时,可以通过指定名称来获取它们。
这让应用程序能够把 appsettings.json(或其他配置源)的不同节映射到同一个 C# 类结构上而不会发生冲突。

## 7. Configuring and editing named-options

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/configuring-and-editing-named-options-58112421/) · 1:30

### 总结

本课演示如何在 JSON 文件中组织配置数据的结构,以支持 .NET 中的 Options pattern。
通过把设置组织成逻辑层级(例如一个顶层 Features 节点,其中包含针对 TodoApi 和 WeatherStation 等不同服务的具体配置),开发者可以在配置提供程序与强类型 options 对象之间建立清晰的映射。

### 核心概念

- 使用 JSON 节点构建层级化的配置结构。
- 把配置节映射到具体的功能 options。
- 在配置中支持多种数据类型,包括布尔值、字符串和数组。
- 与提供程序无关的配置:值可以来自 JSON、环境变量或基于云的配置服务。

### 课程笔记

在 `appsettings.Development.json` 文件中,配置被组织成一个层级结构。
一个顶层的 `Features` 节点充当各种应用程序功能的容器,每个功能都有自己的一组配置值。
这种结构支持逻辑分组,例如把 `TodoApi` 的配置与 `WeatherStation` 的配置分开。

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "Features": {
    "TodoApi": { ... },
    "WeatherStation": { ... }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/configuring-and-editing-named-options-58112421/?t=10)

`TodoApi` 功能是一个详细的配置对象。
它包含一个布尔类型的 `Enabled` 标志、`Name` 和 `Version` 之类的元数据、`Endpoint` 和 `ApiKey` 之类的连接信息,以及一个以 JSON 数组表示的 `Tags` 集合。

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "Features": {
    "TodoApi": {
      "Enabled": true,
      "Name": "TODOs API",
      "Version": "0.4.20",
      "Endpoint": "https://jsonplaceholder.typicode.com/todos",
      "ApiKey": "RGF2aWQgUGluZSB3YXMgaGVyZSDwn6STIQ==",
      "Tags": [
        "self-improvement",
        "organization"
      ]
    },
    "WeatherStation": { ... }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/configuring-and-editing-named-options-58112421/?t=25)

相比之下,`WeatherStation` 功能展示的是一个更简单的配置。
它当前处于禁用状态,并且只包含一个名称,代表一个可能还处于规划阶段、尚无完整配置需求的功能。

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "Features": {
    "TodoApi": { ... },
    "WeatherStation": {
      "Enabled": false,
      "Name": "Weather Station"
    }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/configuring-and-editing-named-options-58112421/?t=40)

虽然这些示例使用的是 JSON 文件,但 .NET 配置系统与提供程序无关。
这些值也可以通过环境变量、命令行参数、Azure App Configuration 或 AWS Secrets Manager 来定义。
无论来源是什么,配置系统都会确保这些值流经系统,并正确映射到应用程序中对应的 options 对象。

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "Features": {
    "TodoApi": {
      "Enabled": true,
      "Name": "TODOs API",
      "Version": "0.4.20",
      "Endpoint": "https://jsonplaceholder.typicode.com/todos",
      "ApiKey": "RGF2aWQgUGluZSB3YXMgaGVyZSDwn6STIQ==",
      "Tags": [
        "self-improvement",
        "organization"
      ]
    },
    "WeatherStation": {
      "Enabled": false,
      "Name": "Weather Station"
    }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/configuring-and-editing-named-options-58112421/?t=55)

## 8. Resolving named-options by name

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/resolving-named-options-by-name-58112422/) · 1:30

### 总结

本课演示如何使用 `IOptionsSnapshot<T>` 获取特定的命名配置实例。
标准的 `IOptions<T>` 接口仅限于单个默认配置实例,而 `IOptionsSnapshot<T>` 提供了一个 `Get` 方法,让开发者能够在单个请求作用域内按注册名称解析同一类型的多个不同配置。

### 核心概念

* **IOptionsSnapshot<T>**:一个 scoped 服务,用于在请求的生命周期内访问命名 options。
* **Named Options**:同一个 options 类的多个实例,分别绑定到不同的配置节。
* **Get(string name)**:用于获取 options 类的某个特定命名实例的方法。
* **请求生命周期**:`IOptionsSnapshot` 的值每个请求只计算一次,确保整个操作过程中的一致性。

### 课程笔记

为了评估多个命名配置实例,可以使用 `IOptionsSnapshot<T>` 实现一个 API 端点。
`IOptions<T>` 被限制为与一个配置类型一对一映射(使用空字符串作为默认名称),而 `IOptionsSnapshot<T>` 则允许访问对应同一 options 类型的不同命名节。

在下面的示例中,定义了一个 `/features` 端点。
处理程序接受 `IOptionsSnapshot<FeatureOptions>` 作为依赖。
通过调用 `Get` 方法并提供具体的注册名称(例如 "TodoApi" 或 "WeatherStation"),处理程序可以解析出 `FeatureOptions` 的不同实例。
随后这些实例会作为 JSON 响应返回。

```csharp
app.MapGet( // GET /features HTTP/1.1
            // Content-Type: application/json
    pattern: "/features",
    handler: static (IOptionsSnapshot<FeatureOptions> options) =>
    {
        FeatureOptions? todo = options.Get(name: "TodoApi");
        FeatureOptions? weatherStation = options.Get(name: "WeatherStation");

        return Results.Json(
            data: new {
                TodoApis = todo,
                WeatherStation = weatherStation
            });
    })
.WithName(endpointName: "GetFeatureOptions")
.WithOpenApi();

app.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/resolving-named-options-by-name-58112422/?t=10)

```text
IOptions<FeatureOptions>          --> 仅默认名称 ""
IOptionsSnapshot<FeatureOptions>
    .Get(name: "TodoApi")         --> FeatureOptions (TodoApi)
    .Get(name: "WeatherStation")  --> FeatureOptions (WeatherStation)
```

当应用程序需要区分共享相同数据结构的不同设置集合时,这种方式必不可少。
通过使用 `IOptionsSnapshot<T>`,应用程序可以按名称获取所需的确切配置,而不必局限于单个全局实例。

## 9. Verifying configuration bindings and DI hydration

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/verifying-configuration-bindings-and-di-hydration-58112423/) · 1:13

### 总结

本课演示如何验证来自 appsettings.json 的配置值已被正确绑定到强类型类,并通过 .NET 中的 Options pattern 注入。
它特别关注使用 IOptionsSnapshot 获取命名 options,并确认依赖注入(DI)容器成功地填充(hydrate)了这些对象,包括数组和嵌套属性之类的复杂类型,同时会用默认值或 null 值来处理缺失的配置键。

### 核心概念

*   **DI Hydration**:.NET 依赖注入容器使用来自配置提供程序的值填充 options 类属性的过程。
*   **命名 Options 获取**:使用 `IOptionsSnapshot<T>.Get(name)` 访问某个共享配置类型的特定实例。
*   **绑定验证**:检查 options 对象的运行时状态,以确保数组、布尔值和字符串等类型被正确映射。
*   **默认值处理**:观察绑定器如何通过分配默认值来处理缺失的配置键(例如字符串为 `null`,集合为空数组)。

### 课程笔记

为了验证配置节已被正确绑定到 `FeatureOptions` 类,定义了一个 Minimal API 端点。
这个端点利用 `IOptionsSnapshot<FeatureOptions>` 获取两个不同的命名配置:"TodoApi" 和 "WeatherStation"。
这两个节都映射到同一个底层 `FeatureOptions` 类型,展示了 Options pattern 在共享 schema 方面的灵活性。

```csharp
app.MapGet( // GET /features HTTP/1.1
            // Content-Type: application/json
    "/features",
    static (IOptionsSnapshot<FeatureOptions> options) =>
    {        var todo = options.Get("TodoApi");
        var weatherStation = options.Get("WeatherStation");

        return Results.Json(
            new {
                TodoApis = todo,
                WeatherStation = weatherStation
            });
    })
.WithName("GetFeatureOptions")
.WithOpenApi();

app.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/verifying-configuration-bindings-and-di-hydration-58112423/?t=25)

当应用程序运行时,可以通过一个标准的 HTTP GET 请求触发该端点。
这样就可以借助调试器或通过检查 JSON 响应,在运行时检查已填充的对象。

```bash
curl -X 'GET' \
  'https://localhost:7108/features' \
  -H 'accept: */*'
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/verifying-configuration-bindings-and-di-hydration-58112423/?t=40)

执行后,得到的 JSON 证实 `TodoApi` 节已用配置源中对应的值完全填充,包括 API key、版本和 tags 数组。
相反,`WeatherStation` 节在配置源中缺少若干键,结果显示绑定器正确地为缺失的字符串分配了 `null`,并为 tags 初始化了一个空数组。
这证实了依赖注入系统正在成功提供绑定到所定义类的强类型配置 options。

```json
{
  "todoApis": {
    "enabled": true,
    "name": "TODOs API",
    "version": "0.4.20",
    "endpoint": "https://jsonplaceholder.typicode.com/todos",
    "apiKey": "RGF2aWQgUGluZSB3YXMgaGVyZSDwn6STIQ==",
    "tags": [
      "self-improvement",
      "organization"
    ]
  },
  "weatherStation": {
    "enabled": false,
    "name": "Weather Station",
    "version": null,
    "endpoint": null,
    "apiKey": null,
    "tags": []
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/verifying-configuration-bindings-and-di-hydration-58112423/?t=55)

## 10. Considering alternative APIs for registering options

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/considering-alternative-apis-for-registering-options-58112424/) · 1:40

### 总结

本课探讨在 .NET 中注册和绑定 options 的替代 API,具体比较了 AddOptions fluent API 与 Configure 方法。
它演示了 Configure API 如何通过 BinderOptions 委托对绑定过程提供更细粒度的控制,让开发者能够指定诸如是否绑定非公共属性、遇到未知配置值时是否抛出错误等行为。

### 核心概念

- **AddOptions 与 Configure**:`AddOptions<T>().Bind()` 是一种常见的 fluent 方式,而 `Configure<T>()` 为命名 options 提供了一种更简洁的替代方案。
- **自动注册**:`Configure<T>` 方法会自动处理把 options 添加到 service collection 并以指定名称注册的工作。
- **BinderOptions**:`Configure` 方法中的一个可选委托,允许对配置值如何映射到 options 对象进行精细控制。
- **校验控制**:借助 `BinderOptions`,开发者可以通过启用 `ErrorOnUnknownConfiguration` 来强制执行严格映射。

### 课程笔记

注册命名 options 时,有多种方式可以实现同样的结果。
第一种方式是使用 `AddOptions<T>` 方法,指定一个名称,然后手动用相应的配置节调用 `Bind`。

或者,你也可以直接使用 `Configure<T>` 方法。
这个 API 接受 options 实例的名称和配置节作为参数。
在底层,这个方法会处理泛型 options 类型的注册,并自动把它绑定到所提供的节。
当你在绑定过程中需要更多灵活性时,通常会优先选择这个替代 API。

```csharp
// Add named configuration options.
builder.Services.AddOptions<FeatureOptions>(
    name: "TodoApi"
    )
    .Bind(config: builder.Configuration.GetSection(
        key: "Features:TodoApi"));

builder.Services.Configure<FeatureOptions>(
    name: "WeatherStation",
    config: builder.Configuration.GetSection(
        key: "Features:WeatherStation"));

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/considering-alternative-apis-for-registering-options-58112424/?t=25)

使用 `Configure` API 的一个主要原因是,它有一个接受 `BinderOptions` 委托的重载。
这对绑定器的行为提供了精细的控制。
例如,你可以显式定义绑定器是否应尝试设置非公共属性,或者在遇到 options 类上没有对应属性的配置值时是否应抛出异常。

```csharp
builder.Services.Configure<FeatureOptions>(
    name: "WeatherStation",
    config: builder.Configuration.GetSection(
        key: "Features:WeatherStation"),
    configureBinder: static (BinderOptions o) =>
    { 
        o.BindNonPublicProperties = false;
        o.ErrorOnUnknownConfiguration = true;
    });

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/considering-alternative-apis-for-registering-options-58112424/?t=55)

`appsettings.json`(或其他配置源)中的配置结构必须与注册时指定的键保持一致,才能确保绑定成功。

```json
{
  "todoApis": {
    "enabled": true,
    "name": "TODOs API",
    "version": "0.4.20",
    "endpoint": "https://jsonplaceholder.typicode.com/todos",
    "apiKey": "RGF2aWQgUGluZSB3YXMgaGVyZS4=",
    "tags": [
      "self-improvement",
      "organization"
    ]
  },
  "weatherStation": {
    "enabled": false,
    "name": "Weather Station",
    "version": null,
    "endpoint": null,
    "apiKey": null,
    "tags": []
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/considering-alternative-apis-for-registering-options-58112424/?t=90)

当应用程序执行并查询 features 端点时,返回的值符合预期,这证实替代的 `Configure` API 正确地把配置数据绑定到了命名 options 实例上。

## 11. Overriding configurations with post configure APIs

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/overriding-configurations-with-post-configure-apis-58112425/) · 2:15

### 总结

.NET 中的 PostConfigure API 提供了一种机制,可以在所有标准配置和绑定步骤完成之后,对 options 对象进行后期修改。
这让开发者能够介入填充(hydration)过程,从而合并硬编码的值、执行手动覆盖,或者计算依赖于已从配置源绑定的值的属性。

### 核心概念

* **Post-Configuration**:options pattern 中的一个阶段,发生在所有标准 `Configure` 调用都处理完毕之后。
* **`PostConfigure<TOptions>`**:一个 API,用于注册一个修改特定命名 options 实例的操作。
* **执行顺序**:无论 `PostConfigure` 在服务注册代码中的什么位置被调用,它都会在 options 对象被标准配置提供程序部分填充之后才被评估。
* **合并与覆盖**:能够把来自外部配置(例如 `appsettings.json`)的值与程序化覆盖组合起来,以确保特定设置优先生效。

### 课程笔记

在 .NET 中,options pattern 支持多层配置。
`Configure` 方法通常用于把 options 绑定到配置节,而当你需要对 options 对象如何被填充拥有最终决定权时,就会使用 `PostConfigure` API。

起初,一个命名 options 实例通过绑定到配置中的某个特定节来完成配置:

```csharp
builder.Services.Configure<FeatureOptions>(
    name: "WeatherStation",
    config: builder.Configuration.GetSection(
        key: "Features:WeatherStation"));

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var WebApplication? app = builder.Build();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/overriding-configurations-with-post-configure-apis-58112425/?t=10)

为了覆盖或补充这些值,会使用 `PostConfigure`。
这个方法注册一个回调,该回调会在针对该特定类型和名称的所有其他配置调用都发生之后才被评估。
这确保了传入委托的 `FeatureOptions` 对象已经用来自配置提供程序的值(例如 `Name` 和 `Enabled` 属性)完成了部分填充。

```csharp
// Add named configuration options.
builder.Services.AddOptions<FeatureOptions>()
    .Bind(config: builder.Configuration.GetSection(
        key: "Features:TodoApi"));

// Overrides (and/or merges) with existing configured bindings.
builder.Services.PostConfigure<FeatureOptions>(
    name: "WeatherStation",
    configureOptions: static (FeatureOptions options) =>
    {
        options.Version = new Version(major: 1, minor: 0);
        options.Endpoint = new Uri(
            uristring: "https://freetestapi.com/api/v1/weathers");
        options.Tags =
            string[] [
                "fake-weather",
                "test-api"
            ];
    });

builder.Services.Configure<FeatureOptions>(
    name: "WeatherStation",
    config: builder.Configuration.GetSection(
        key: "Features:WeatherStation"));
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/overriding-configurations-with-post-configure-apis-58112425/?t=40)

即使在源代码中 `PostConfigure` 被放在 `Configure` 调用之前,.NET options 系统也会确保 post-configure 操作最后运行。
这让开发者能够以内联方式手动提供配置值,这些值会与现有绑定合并或覆盖它们。

```text
源代码顺序                          实际执行顺序("WeatherStation")
1. PostConfigure<FeatureOptions>    1. Configure<FeatureOptions>
2. Configure<FeatureOptions>           (Features:WeatherStation -> Enabled, Name)
                                    2. PostConfigure<FeatureOptions>
                                       (Version, Endpoint, Tags)
```

当应用程序运行并解析 options 时,得到的对象反映了配置源与 `PostConfigure` 委托中提供的手动覆盖二者的组合。
在下面的示例中,`WeatherStation` options 的 `Enabled` 和 `Name` 属性来自配置文件,而 `Version`、`Endpoint` 和 `Tags` 则通过 post-configure API 被成功覆盖。

```json
{
  "todoApis": {
    "enabled": true,
    "name": "TODOs API",
    "version": "0.4.20",
    "endpoint": "https://jsonplaceholder.typicode.com/todos",
    "apiKey": "RGF2aWQgUGluZSB3YXMgaGVyZStiOQ==",
    "tags": [
      "self-improvement",
      "organization"
    ]
  },
  "weatherStation": {
    "enabled": false,
    "name": "Weather Station",
    "version": "1.0",
    "endpoint": "https://freetestapi.com/api/v1/weathers",
    "apiKey": null,
    "tags": [
      "fake-weather",
      "test-api"
    ]
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/overriding-configurations-with-post-configure-apis-58112425/?t=120)

## 12. Overriding all config-bound instances

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/overriding-all-config-bound-instances-58112426/) · 1:52

### 总结

.NET 中的 `PostConfigureAll` API 提供了一种机制,可以把配置逻辑应用到某个特定 options 类型的每一个实例上,无论它们是命名实例还是默认实例。
本课演示如何使用这个 API 来确保全局默认值(例如把集合初始化为空数组)在更具体的 post-configuration 逻辑执行之前,被一致地应用到所有已配置的实例上。

### 核心概念

*   **PostConfigureAll<TOptions>**:一个注册方法,把一个配置回调应用到指定 options 类型的每一个实例上,包括命名实例。
*   **全局默认值**:`PostConfigureAll` 非常适合设置基线值或初始化属性(例如对数组做 null 检查),这些设置应当适用于该类型的所有实例。
*   **执行顺序**:`PostConfigureAll` 回调在针对具体实例的 `PostConfigure` 回调之前执行,从而能够在应用更具体的覆盖之前先建立全局默认值。
*   **与名称无关**:`Configure` 或 `PostConfigure` 通常针对某个特定的命名实例或默认实例,而 `PostConfigureAll` 则会针对该类型的每一个已注册实例被调用。

### 课程笔记

除了标准的 `AddOptions` 和 `Configure` API 之外,Options pattern 还包含 `PostConfigureAll<TOptions>` 方法。
这个 API 的独特之处在于,所提供的回调会针对该 options 类型的每一个实例被调用,无论配置节或实例被赋予了什么名称。

`PostConfigureAll` 的一个常见用例是确保所有实例的属性都被正确初始化。
例如,如果一个 options 类包含一个 `Tags` 数组,而它在绑定到配置源之后可能为 null,就可以使用 `PostConfigureAll` 有条件地把它初始化为空数组。

```csharp
builder.Services.AddOptions<FeatureOptions>()
    .Bind(config: builder.Configuration.GetSection(
        key: "Features:TodoApi"));

// Overrides (and/or merges) with existing configured bindings.
builder.Services.PostConfigure<FeatureOptions>(
    name: "WeatherStation",
    configureOptions: static (FeatureOptions options) =>
    {
        options.Version = new Version(major: 1, minor: 0);
        options.Endpoint = new Uri(
            uriString: "https://freetestapi.com/api/v1/weathers");
        options.Tags = new[]
        {
            "fake-weather",
            "test-api"
        };
    });

// Override all config-bound instances of FeatureOptions.
builder.Services.PostConfigureAll<FeatureOptions>(
    configureOptions:
        static (FeatureOptions options) =>
            options.Tags ??= [];

builder.Services.Configure<FeatureOptions>(
    name: "WeatherStation",
    config: builder.Configuration.GetSection(
        key: "Features:WeatherStation"));
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/overriding-all-config-bound-instances-58112426/?t=10)

#### 执行顺序与合并

在执行管道中,`PostConfigureAll` 在针对具体实例的 `PostConfigure` 方法之前运行。
在上面的示例中,当解析 "WeatherStation" 实例时:

1.  配置从 `Features:WeatherStation` 节绑定。
2.  `PostConfigureAll` 运行,检查 `Tags` 是否为 null。
    如果是,就把它初始化为空数组。
3.  针对 "WeatherStation" 的特定 `PostConfigure` 运行,用具体的值覆盖 `Version`、`Endpoint` 和 `Tags`。

```text
"WeatherStation" 解析时:
  Configure (Features:WeatherStation)
        |
        v
  PostConfigureAll<FeatureOptions>     options.Tags ??= []
        |
        v
  PostConfigure<FeatureOptions>("WeatherStation")
                                       Version, Endpoint, Tags
```

这确保了即使某个配置节(例如 `TodoApi`)没有定义任何 tags,得到的对象也会是一个空数组,而不是一个 null 引用。
最终得到的 JSON 代表了所有配置源、手动覆盖以及这些全局默认值合并后的版本。

```json
{
  "todoApis": {
    "enabled": true,
    "name": "TODOs API",
    "version": "0.4.20",
    "endpoint": "https://jsonplaceholder.typicode.com/todos",
    "apiKey": "RGF2aWQgUGluZSB3YXMgaGVyZSTDWn6STIQ==",
    "tags": [
      "self-improvement",
      "organization"
    ]
  },
  "weatherStation": {
    "enabled": false,
    "name": "Weather Station",
    "version": "1.0",
    "endpoint": "https://freetestapi.com/api/v1/weathers",
    "apiKey": null,
    "tags": [
      "fake-weather",
      "test-api"
    ]
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/overriding-all-config-bound-instances-58112426/?t=100)

## 13. Avoid implementing IOptions

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/avoid-implementing-ioptions-58112427/) · 1:58

### 总结

本课针对 .NET 开发中关于 IOptions 接口的一个常见误解。
它解释了 options 类应当保持为简单的 POCO,不应直接实现 IOptions,因为框架提供这个接口是作为依赖注入的包装器,用来交付强类型的配置对象。

### 核心概念

- IOptions<T> 的用途是作为依赖注入的服务包装器,而不是供领域模型或配置模型去实现的接口。
- .NET 运行时把 IOptions<T> 注册为一个开放泛型,使其能够自动解析并提供底层的配置值。
- 在类上手动实现 IOptions<T>(例如让 Value 属性返回 this)是多余且不必要的。
- IOptionsSnapshot<T> 和 IOptionsMonitor<T> 等相关接口为 scoped 或 singleton 生命周期提供了额外功能,但遵循同样的包装原则。

### 课程笔记

在 .NET 中使用 Options pattern 时,一个常见的错误是尝试在 options 类本身上直接实现 IOptions 接口。
虽然还存在 IOptionsSnapshot 和 IOptionsMonitor 等其他接口,用于不同的生命周期和更新场景,但核心原则是一样的:你的 options 对象应当是简单的类。

一个标准的、定义良好的 options 类应当看起来像一个简单的 POCO:

```csharp
namespace Options.Api.Features;

/// <summary>
/// A representation of a "feature" options object.
/// </summary>
public sealed class FeatureOptions
{
    /// <summary>
    /// Gets or sets whether the feature is enabled.
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// Gets or sets the name of the feature.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the version of the feature.
    /// </summary>
    public Version? Version { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/avoid-implementing-ioptions-58112427/?t=10)

在开源仓库中搜索,经常会发现开发者像这样错误地实现该接口:

```csharp
namespace Options.Api.Features;

/// <summary>
/// A representation of a "feature" options object.
/// </summary>
public sealed class FeatureOptions : IOptions<FeatureOptions>
{
    /// <inheritdoc cref="IOptions{TOptions}.Value" />
    public FeatureOptions Value => this;

    /// <summary>
    /// Gets or sets whether the feature is enabled.
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// Gets or sets the name of the feature.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the version of the feature.
    /// </summary>
    public Version? Version { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/avoid-implementing-ioptions-58112427/?t=100)

这种手动实现是不必要的。
因为 IOptions 在 .NET 依赖注入容器中被注册为一个开放泛型,框架已经知道如何提供底层的值。
这个接口充当一个包装器,用来把强类型类提供给你的服务;类本身不应该关心这个包装器的实现。

## 14. Understanding reflection-based binding

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/understanding-reflection-based-binding-58112428/) · 2:14

### 总结

本课考察 .NET Options Pattern 的内部实现,重点关注 Bind 方法如何把配置数据映射到强类型对象。
它揭示了默认的绑定机制依赖于反射,这会带来性能开销,并给 Native AOT(Ahead-of-Time)编译和裁剪(trimming)带来挑战。
本课介绍了 source generator,作为现代 .NET 应用程序中实现高性能、兼容 AOT 的配置绑定的推荐替代方案。

### 核心概念

* `Bind` 方法充当配置节与通过依赖注入填充 options 类型之间的纽带。
* 在内部,配置系统依赖 `ConfigurationBinder.BindInstance` 来执行映射。
* 默认绑定在运行时使用反射来评估类型并赋值。
* 由于其动态特性,基于反射的绑定与 Native AOT 和裁剪不兼容。
* 可以使用 source generator 以编译时代码生成取代反射,从而获得更好的性能和 AOT 支持。

### 课程笔记

在一个标准的 .NET API 中,`AddOptions` 方法后面通常跟着对 `Bind` 的调用。
这个方法充当"神奇的胶水",它接收一个配置节,并把它黏合到依赖注入期间指定 options 类型的填充过程上。

```csharp
var builder = WebApplication.CreateBuilder(args);

// Bind IOptions<LoggingOptions> to the
// "Logging" config section for DI.
builder.Services.AddOptions<LoggingOptions>()
    .Bind(config: builder.Configuration.GetSection(
        key: LoggingOptions.LoggingConfigurationSectionName));

// Add named configuration options.
builder.Services.AddOptions<FeatureOptions>()
    .Bind(config: builder.Configuration.GetSection(
        key: "Features:TodoApi"));

// Overrides (and/or merges) with existing configured bindings.
builder.Services.PostConfigure<FeatureOptions>(options => { });

// Override all config-bound instances of FeatureOptions.
builder.Services.PostConfigureAll<FeatureOptions>(options => { });

builder.Services.Configure<FeatureOptions>(
    name: "WeatherStation",
    config: builder.Configuration.GetSection(
        key: "Features:WeatherStation"));

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/understanding-reflection-based-binding-58112428/?t=10)

虽然 `PostConfigure` 和 `PostConfigureAll` 之类的 API 允许对配置进行后处理,但核心的 `Configure` 和 `Bind` API 最终会沿着依赖层级向下走到 `ConfigurationBinder`。
具体来说,它们会调用一个名为 `BindInstance` 的内部方法来处理实际的对象填充。

```csharp
namespace Microsoft.Extensions.Configuration
{
    public static class ConfigurationBinder
    {
        [RequiresDynamicCode(message: DynamicCodeWarningMessage)]
        [RequiresUnreferencedCode(message: TrimmingWarningMessage)]
        private static void BindInstance(
            [DynamicallyAccessedMembers(memberTypes: DynamicallyAccessedMemberTypes.All)] Type type,
            BindingPoint bindingPoint,
            IConfiguration config,
            BinderOptions options,
            bool isParentCollection)
        {
            // if binding IConfigurationSection, break early
            if (type == typeof(IConfigurationSection))
            { 
                bindingPoint.TrySetValue(config);
                return;
            }

            if (config is null)
            { 
                return;
            }

            IConfigurationSection? section = config as IConfigurationSection;
            string? configValue = section?.Value;
            if (configValue != null && TryConvertValue(type, configValue, out var result, out var error))
            {
                if (error == null)
                {
                    // ...
                }
            }
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/understanding-reflection-based-binding-58112428/?t=50)

```text
AddOptions<T>().Bind(...)  /  Configure<T>(...)
                 |
                 v
        ConfigurationBinder
                 |
                 v
        BindInstance (反射)
        [RequiresDynamicCode]
        [RequiresUnreferencedCode]
```

`BindInstance` 方法使用反射来评估如何把配置值绑定到目标 options 对象的属性上。
因为它依赖反射,所以它被标记了 `RequiresDynamicCode` 和 `RequiresUnreferencedCode` 之类的特性。
这表明基于反射的绑定并不原生兼容 Native AOT(Ahead-of-Time)编译或激进裁剪,因为编译器无法静态确定在运行时会访问哪些成员。

除了兼容性问题之外,基于反射的绑定还可能有些低效。
为了解决这一点,.NET 提供了一个内置的 source generator,可以通过项目配置启用。
这个 source generator 在编译时生成所需的绑定代码,从而消除对反射的需要,并使该 API 能够以 Native AOT 为目标进行编译。
只要有可能,都鼓励利用这些 source generator 来提升应用程序性能以及与现代部署方式的兼容性。

## 15. Enabling source-generated configuration binding

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/enabling-source-generated-configuration-binding-58112429/) · 2:05

.NET 配置绑定 source generator 通过用生成的代码取代基于反射的配置绑定,提升了应用程序性能以及与 Ahead-of-Time(AOT)编译的兼容性。
通过在项目文件中启用这一特性,编译器会生成专门的方法,拦截对 Bind 和 Configure 等标准配置 API 的调用,在编译时把配置节直接映射到 options 类。

### 核心概念

- **EnableConfigurationBindingGenerator**:用于开关该 source generator 的 MSBuild 属性。
- **Interceptors**:一项 C# 编译器特性,允许 source generator 用生成的替代实现来替换对特定方法(例如 `.Bind()`)的调用。
- **无反射绑定**:通过在编译时生成代码,应用程序避免了使用反射把配置键映射到对象属性所带来的性能开销。
- **Analyzer 集成**:生成的源代码可以在 IDE 中项目的 analyzer 依赖项下查看。

### 课程笔记

要启用配置绑定生成器,你必须修改项目的 `.csproj` 文件。
添加 `<EnableConfigurationBindingGenerator>` 属性并将其设置为 `true`,就会指示编译器生成优化后的绑定逻辑。

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <RootNamespace>Options.Api</RootNamespace>
    <RuntimeIdentifiers>linux-x64</RuntimeIdentifiers>
    <EnableSdkContainerDebugging>True</EnableSdkContainerDebugging>
    <ContainerBaseImage>mcr.microsoft.com/dotnet/aspnet:8.0</ContainerBaseImage>
    <UserSecretsId>6e4982ad-5c23-4bf6-8280-865507967173</UserSecretsId>
    <DockerDefaultTargetOS>Linux</DockerDefaultTargetOS>
    <EnableConfigurationBindingGenerator>true</EnableConfigurationBindingGenerator>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="8.0.7" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.4.0" />
  </ItemGroup>

  <ItemGroup>
    <ContainerPort Include="8081" />
  </ItemGroup>

</Project>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/enabling-source-generated-configuration-binding-58112429/?t=10)

从宏观上看,这个 source generator 利用了一项名为 interceptors 的 C# 特性。
它会识别对标准配置方法(具体来说是 `Get`、`Bind` 和 `Configure`)的调用,并用 source generator 生成的替代实现来覆盖它们。
这一过程消除了运行时使用反射来发现属性类型和名称的需要。

```csharp
var builder = WebApplication.CreateBuilder(args);

// Bind IOptions<LoggingOptions> to the
// "Logging" config section for DI.
builder.Services.AddOptions<LoggingOptions>()
    .Bind(config: builder.Configuration.GetSection(
        key: LoggingOptions.LoggingConfigurationSectionName));

// Add named configuration options.
builder.Services.AddOptions<FeatureOptions>()
    .Bind(config: builder.Configuration.GetSection(
        key: "Features:TodoApi"));

// Overrides (and/or merges) with existing
builder.Services.PostConfigure<FeatureOptions>(options => { });

builder.Services.Configure<FeatureOptions>(
    name: "WeatherStation",
    config: builder.Configuration.GetSection(
        key: "Features:WeatherStation"));

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/enabling-source-generated-configuration-binding-58112429/?t=40)

启用之后,可以在 Solution Explorer 中的 **Dependencies > Analyzers > Microsoft.Extensions.Configuration.Binder.SourceGeneration** 下查看生成的代码。
生成器会产生一个文件作用域的 `InterceptsLocationAttribute`,编译器用它把原始调用点映射到新生成的实现上。

```csharp
#nullable disable warnings

// Suppress warnings about [Obsolete] member usage in generated code.
#pragma warning disable CS0612, CS0618

namespace System.Runtime.CompilerServices
{
    using System;
    using System.CodeDom.Compiler;

    [GeneratedCode("Microsoft.Extensions.Configuration.Binder.SourceGeneration", "8.0.7")]
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    file sealed class InterceptsLocationAttribute : Attribute
    {
        public InterceptsLocationAttribute(string filePath, int line, int column)
        {
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/enabling-source-generated-configuration-binding-58112429/?t=100)

生成的绑定器逻辑包含用 `InterceptsLocation` 特性修饰的方法。
这些特性指定了你的源代码(例如 `Program.cs`)中被生成器替换的那些调用的确切文件路径、行号和列号。

```csharp
namespace Microsoft.Extensions.Configuration.Binder.SourceGeneration
{
        /// <summary>
        /// </summary>
        [InterceptsLocation(filePath: @"C:\...\Program.cs", line: 6, column: 6)]
        [InterceptsLocation(filePath: @"C:\...\Program.cs", line: 13, column: 6)]
        public static IOptionsBuilder<TOptions> Bind<TOptions>(this IOptionsBuilder<TOptions> optionsBuilder, IConfiguration config) where TOptions : class

        /// <summary>
        /// </summary>
        [InterceptsLocation(filePath: @"C:\...\Program.cs", line: 38, column: 6)]
        public static IOptionsBuilder<TOptions> Configure<TOptions>(this IOptionsBuilder<TOptions> optionsBuilder, IConfiguration config, Action<BinderOptions>? configureOptions = null) where TOptions : class
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/enabling-source-generated-configuration-binding-58112429/?t=115)

```text
Program.cs                               生成的代码
.Bind(...)       line: 6,  column: 6 --> Bind<TOptions>(...)
.Bind(...)       line: 13, column: 6 --> Bind<TOptions>(...)
.Configure(...)  line: 38, column: 6 --> Configure<TOptions>(...)
                     (由 [InterceptsLocation] 关联)
```

## 16. Section Recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/section-recap-58112430/) · 1:01

### 总结

本课对 .NET 中的 Options pattern 做了全面回顾,重点关注配置数据如何被绑定到强类型类并集成到依赖注入容器中。
内容涵盖核心接口、配置方法,以及基于反射的绑定与 source generator 生成的绑定之间的区别。

### 核心概念

- 使用 Options pattern 进行强类型配置绑定。
- IOptions<T>、IOptionsSnapshot<T> 和 IOptionsMonitor<T> 接口的用法。
- 在 Program.cs 中注册和绑定 options。
- 为不同的配置实例实现命名 options。
- 使用 PostConfigure 和 ConfigureAll 进行后期 options 修改的 post-configuration 技术。
- 基于反射的绑定与 source generator 生成的绑定在语义上的差异。

### 课程笔记

Options pattern 是 .NET 中管理配置的一个基础概念。
它涉及获取各种配置数据源,并把它们绑定到强类型类上。
随后这些类会被注册到依赖注入(DI)容器中,从而可以被注入到整个应用程序的服务里。

访问这些配置值通常通过以下三个主要接口之一来完成:

- **IOptions<T>**:一个 singleton 服务,在启动时读取一次配置。
- **IOptionsSnapshot<T>**:一个 scoped 服务,在每个请求时重新评估配置,适用于在不重启应用程序的情况下处理配置变更。
- **IOptionsMonitor<T>**:一个 singleton 服务,支持实时通知以及对当前配置状态的访问。

在 Program.cs 文件中,options 通过访问 IServiceCollection 来配置。
使用 AddOptions<T> 方法注册 options 类型,随后使用 Bind 方法把它关联到配置提供程序的某个特定节。

```csharp
var builder = WebApplication.CreateBuilder(args);

// Bind IOptions<LoggingOptions> to the
// "Logging" config section for DI.
builder.Services.AddOptions<LoggingOptions>()
    .Bind(config: builder.Configuration.GetSection(
        key: LoggingOptions.LoggingConfigurationSectionName));

// Add named configuration options.
builder.Services.AddOptions<FeatureOptions>(
        name: "TodoApi"
    )
    .Bind(config: builder.Configuration.GetSection(
        key: "Features:TodoApi"));

// Overrides (and/or merges) with existing configured bindings.
builder.Services.PostConfigure<FeatureOptions>(
    name: "WeatherStation",
    configureOptions: static (FeatureOptions options) =>
    {
        options.Version = new Version(major: 1, minor: 0);
        options.Endpoint = new Uri(
            uriString: "https://freetestapi.com/api/v1/weathers");
        options.Tags = ["fake-weather"];
    });
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/section-recap-58112430/?t=10)

框架还支持更高级的配置场景,例如 PostConfigure 和 ConfigureAll,它们允许在 options 初次绑定之后对其进行修改或合并。
这对于设置默认值或依赖其他配置设置的计算值尤其有用。
此外,开发者可以在基于反射的绑定(传统方式)与 source generator 生成的绑定之间做出选择,后者带来性能优势,并且是 Native AOT 场景所必需的。
掌握 options pattern 的下一步,是实现校验以确保配置数据正确无误。
