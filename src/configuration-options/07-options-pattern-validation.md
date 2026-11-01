# Options Pattern Validation

> 课程:[From Zero to Hero: Configuration and Options in .NET](https://dometrain.com/course/from-zero-to-hero-configuration-and-options-in-dotnet/) · 第 7 章
> 共 9 课 · 约 30:40
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction to Validating Options](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/introduction-to-validating-options-58112632/) | 1:42 | [↓](#1-introduction-to-validating-options) |
| 2 | [Implementing the IValidateOptions interface](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/implementing-the-ivalidateoptions-interface-58112633/) | 5:07 | [↓](#2-implementing-the-ivalidateoptions-interface) |
| 3 | [Registering options with validation](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/registering-options-with-validation-58112634/) | 3:02 | [↓](#3-registering-options-with-validation) |
| 4 | [Using data-annotations for declarative validation](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/using-data-annotations-for-declarative-validation-58112635/) | 4:45 | [↓](#4-using-data-annotations-for-declarative-validation) |
| 5 | [Handling validation errors](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/handling-validation-errors-58112637/) | 4:36 | [↓](#5-handling-validation-errors) |
| 6 | [Expressing simple delegate-based validation with fluent APIs](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/expressing-simple-delegate-based-validation-with-fluent-apis-58112638/) | 3:21 | [↓](#6-expressing-simple-delegate-based-validation-with-fluent-apis) |
| 7 | [Controlling when validation errors occur](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/controlling-when-validation-errors-occur-58112639/) | 2:24 | [↓](#7-controlling-when-validation-errors-occur) |
| 8 | [Compile-time validation with source-generators](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/compile-time-validation-with-source-generators-58112640/) | 4:07 | [↓](#8-compile-time-validation-with-source-generators) |
| 9 | [Section Recap](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/section-recap-58112642/) | 1:36 | [↓](#9-section-recap) |

## 1. Introduction to Validating Options

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/introduction-to-validating-options-58112632/) · 1:42

### 总结

本课介绍 .NET Options 模式中验证的重要性及其概念框架。
它强调了验证配置如何通过尽早捕获错误配置来提升应用程序的可靠性、安全性和可调试性。
本课还概述了执行流程:宿主(host)调用 IValidateOptions 的实现,这些实现可以返回成功、失败或跳过状态,以控制应用程序的行为。

### 核心概念

* **可靠性与安全性**:验证确保应用程序拥有正确的配置,并提供了一个清洗敏感数据项的机会。
* **尽早发现错误**:在启动时或运行早期捕获错误配置,可以简化调试。
* **验证生命周期**:宿主在应用程序启动过程中检测并执行 IValidateOptions 的实现。
* **验证状态**:验证逻辑的实现可以返回三种不同的状态:Success、Skipped 或 Failure。

### 课程笔记

验证选项是构建健壮 .NET 应用程序的关键一步。
它确保提供给应用程序的配置值是正确的,并满足所需的条件。
这个过程提升了整体可靠性,并通过允许开发者核验和清洗配置数据来增强安全性。
此外,它通过尽早(要么在应用程序启动时,要么在运行时)识别错误配置来简化调试过程,而不是任由无效状态在系统中传播。

从概念上讲,验证过程被集成在应用程序的生命周期中。
当应用程序启动时,宿主会检测到 options 模式的存在,并识别出所有已注册的 `IValidateOptions` 接口实现。
随后,宿主在这些实现上调用 `Validate` 方法。

每次验证执行都可能产生以下三种状态之一:
1. **Success**:配置有效,应用程序继续其正常流程。
2. **Skipped**:验证逻辑没有被应用(例如,它不适用于当前环境或特定实例)。
3. **Failure**:配置无效,从而让应用程序能够处理该错误(例如阻止启动)。

```text
应用程序启动
     |
     v
  host --- 检测 options 模式与已注册的 IValidateOptions 实现
     |
     v
  Validate(...)
     |
     +--> Success  --> 继续正常流程
     +--> Skipped  --> 验证逻辑未被应用
     +--> Failure  --> 处理错误(例如阻止启动)
```

这些状态决定了应用程序后续的流程,并确保系统其余部分只会使用有效的配置对象。

## 2. Implementing the IValidateOptions interface

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/implementing-the-ivalidateoptions-interface-58112633/) · 5:07

### 总结

IValidateOptions<TOptions> 接口提供了一种在 .NET options 模式中实现复杂验证逻辑的机制。
通过实现 Validate 方法,开发者可以基于选项实例的名称或其内部状态执行条件检查,并返回一个 ValidateOptionsResult,用来表示成功、失败(附带一条或多条汇总的错误消息),或者表示应对某个特定实例跳过验证。
这种方式对命名选项(named options)尤其有用,因为根据正在填充的配置节不同,可能需要应用不同的验证规则。

### 核心概念

*   **IValidateOptions<TOptions>**:用于为选项类定义自定义验证逻辑的接口。
*   **ValidateOptionsResult**:验证逻辑的返回类型,支持 `Success`、`Fail` 和 `Skip` 三种状态。
*   **命名选项验证**:能够根据正在验证的选项实例的名称应用不同的验证规则。
*   **失败汇总**:把多个验证错误收集到一个失败结果中。
*   **跳过验证**:使用 `ValidateOptionsResult.Skip` 表示某个验证器不适用于当前的选项实例。

### 课程笔记

要实现自定义验证逻辑,选项类(或一个独立的验证器类)必须实现 `IValidateOptions<TOptions>` 接口。
该接口要求实现一个 `Validate` 方法,该方法接收选项实例的名称以及已填充的选项对象本身。

```csharp
using Microsoft.Extensions.Options;

namespace Options.Validation.Features;

/// <summary>
/// A representation of a "feature" options object.
/// </summary>
public sealed record class FeatureOptions : IValidateOptions<FeatureOptions>
{
    public bool Enabled { get; set; }
    public string? Name { get; set; }
    public Version? Version { get; set; }
    public Uri? Endpoint { get; set; }
    public string? ApiKey { get; set; }
    public string[] Tags { get; set; } = [];
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/implementing-the-ivalidateoptions-interface-58112633/?t=10)

`Validate` 方法允许对不同命名配置节的验证方式进行细粒度控制。
在下面的实现中,使用了一个静态辅助方法 `IsNamed`,对选项实例名称执行不区分大小写的比较。

对于 "TodoApi" 配置节,逻辑首先检查该功能是否启用;如果它被禁用,验证立即成功。
如果已启用,它会检查是否有有效的端点以及是否为可用于生产的版本(Major > 0)。
多个失败可以收集到一个列表中,并通过 `ValidateOptionsResult.Fail(failures)` 一起返回。

对于 "WeatherStation" 配置节,如果该功能被启用,逻辑会有意地让验证失败,用来表示一个在当前环境中尚未实现的功能。
如果名称与任何已知的配置节都不匹配,该方法返回 `ValidateOptionsResult.Skip`。

```csharp
public ValidateOptionsResult Validate(string? name, FeatureOptions options)
{
    // Validate the "TodoApi" feature options.
    if (IsNamed(name, expectedName: "TodoApi"))
    {
        if (options.Enabled is false)
        {
            return ValidateOptionsResult.Success;
        }

        List<string> failures = [];

        if (options is { Endpoint: null })
        {
            failures.Add(item: "TODO API requires a valid endpoint.");
        }

        if (options is { Version.Major: 0 })
        {
            failures.Add(item: "TODO API running non-production version.");
        }

        if (failures.Count is > 0)
        {
            return ValidateOptionsResult.Fail(failures);
        }
    }

    // Validate the "WeatherStation" feature options.
    if (IsNamed(name, expectedName: "WeatherStation") && options is { Enabled: true })
    {
        return ValidateOptionsResult.Fail(failureMessage: """
            The weather station cannot be enabled.
            It hasn't been implemented yet ...
            """);
    }

    return ValidateOptionsResult.Skip;
}

static bool IsNamed(string? name, string expectedName)
{
    return string.Equals(
        a: name, b: expectedName,
        comparisonType: StringComparison.OrdinalIgnoreCase);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/implementing-the-ivalidateoptions-interface-58112633/?t=40)

```text
Validate(name, options)
  |
  +-- IsNamed(name, "TodoApi")?
  |     +-- Enabled is false            --> Success
  |     +-- Endpoint: null              --> failures.Add(...)
  |     +-- Version.Major: 0            --> failures.Add(...)
  |     +-- failures.Count > 0          --> Fail(failures)
  |
  +-- IsNamed(name, "WeatherStation") && Enabled: true
  |                                     --> Fail(failureMessage)
  |
  +-- 其他情况                          --> Skip
```

在 `Program.cs` 文件中,这些选项被注册为命名实例,并绑定到各自的配置节。

```csharp
using Options.Validation;
using Options.Validation.Features;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddOptions<FeatureOptions>(name: "TodoApi")
    .BindConfiguration(configSectionPath: "Features:TodoApi");

builder.Services.AddOptions<FeatureOptions>(name: "WeatherStation")
    .BindConfiguration(configSectionPath: "Features:WeatherStation");

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/implementing-the-ivalidateoptions-interface-58112633/?t=225)

`appsettings.json` 中的底层配置为这些配置节提供数据。
在这个示例中,"WeatherStation" 被显式启用,以触发自定义的验证失败。

```json
{
  "Features": {
    "TodoApi": {
      "Enabled": true,
      "Name": "TODOs API",
      "Version": "0.4.20",
      "Endpoint": "https://jsonplaceholder.typicode.com/todos",
      "ApiKey": "RGF2aWQgUGluZSB3YXMgaGVyZSDwn6STIQ==",
      "Tags": [ "self-improvement", "organization" ]
    },
    "WeatherStation": {
      "Enabled": true,
      "Name": "Weather Station"
    }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/implementing-the-ivalidateoptions-interface-58112633/?t=245)

一个后台 `Worker` 服务通过 `IOptionsMonitor<FeatureOptions>` 使用这些选项,并通过 `Get` 方法按名称获取它们。

```csharp
public sealed class Worker(
    ILogger<Worker> logger,
    IOptionsMonitor<FeatureOptions> options) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(logLevel: LogLevel.Information))
            {
                logger.LogInformation(
                    message: "TODO API feature options: {Options}",
                    args: options.Get(name: "TodoApi"));

                logger.LogInformation(
                    message: "Weather Station feature options: {Options}",
                    args: options.Get(name: "WeatherStation"));
            }

            await Task.Delay(
                millisecondsDelay: 10_000, cancellationToken: stoppingToken);
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/implementing-the-ivalidateoptions-interface-58112633/?t=260)

运行应用程序时,日志显示这些选项已被成功填充并记录下来,但验证逻辑还没有被执行。

```text
info: Options.Validation.Worker[0]
      TODO API feature options: FeatureOptions { Enabled = True, Name = TODOs API, Version = 0.4.20, ... }
info: Options.Validation.Worker[0]
      Weather Station feature options: FeatureOptions { Enabled = True, Name = Weather Station, ... }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/implementing-the-ivalidateoptions-interface-58112633/?t=290)

仅仅在选项类上实现 `IValidateOptions<T>`,并不足以让 .NET 运行时自动发现并运行验证。
验证必须在依赖注入容器中显式注册才会生效。

## 3. Registering options with validation

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/registering-options-with-validation-58112634/) · 3:02

### 总结

要强制执行 IValidateOptions<T> 实现中定义的验证逻辑,必须显式地注册这些选项,使其在启动时进行验证。
这可以通过链式调用 ValidateOnStart 方法,或使用 AddOptionsWithValidateOnStart 简写形式来实现。
关键在于,实现 IValidateOptions<T> 的类也必须作为该接口的服务描述符注册到依赖注入容器中。
这种配置确保应用程序在启动时执行"快速失败"(fail-fast)检查,在配置无效时抛出异常,而不是等到运行时首次访问这些选项时才发现。

### 核心概念

* **ValidateOnStart**:链式调用在选项注册之后的方法,强制验证在应用程序的启动阶段进行。
* **AddOptionsWithValidateOnStart**:一个简写 API,把向服务集合添加选项与启动时验证的要求合并在一起。
* **服务注册**:必须在 DI 容器中注册 `IValidateOptions<TOptions>` 接口,框架才能解析并执行验证逻辑。
* **快速失败验证**:在配置无效时阻止应用程序启动的做法,通过一个包含所有验证失败的聚合异常呈现出来。

### 课程笔记

仅仅在选项类上实现 `IValidateOptions<T>` 接口,并不足以触发验证。
你必须显式地指示 .NET 配置系统执行验证。
这通常在把选项添加到服务集合时完成。
通过在 `AddOptions` 调用之后链式调用 `.ValidateOnStart()`,可以确保验证逻辑在宿主构建时执行,而不是在运行时延迟执行。

```csharp
using Options.Validation;
using Options.Validation.Features;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddOptions<FeatureOptions>("TodoApi")
    .BindConfiguration("Features:TodoApi")
    .ValidateOnStart();

builder.Services.AddOptionsWithValidateOnStart<FeatureOptions>("WeatherStation")
    .BindConfiguration("Features:WeatherStation");

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/registering-options-with-validation-58112634/?t=10)

如上所示,还有一个简写方法 `AddOptionsWithValidateOnStart<T>(name)`,它在一次调用中同时完成注册和启动时验证的要求。

即使配置了 `ValidateOnStart`,服务容器也必须能够解析 `IValidateOptions<T>` 的实现。
你必须注册把该接口映射到实现的服务描述符(对于自我验证的选项来说,实现就是选项类本身)。
使用 `TryAddEnumerable` 是标准做法,它能确保验证器被正确注册,而不会产生重复的描述符。

```csharp
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Options.Validation;
using Options.Validation.Features;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddOptions<FeatureOptions>("TodoApi")
    .BindConfiguration("Features:TodoApi")
    .ValidateOnStart();

builder.Services.AddOptionsWithValidateOnStart<FeatureOptions>("WeatherStation")
    .BindConfiguration("Features:WeatherStation");

builder.Services.TryAddEnumerable(
    ServiceDescriptor.Singleton
        <IValidateOptions<FeatureOptions>, FeatureOptions>());

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/registering-options-with-validation-58112634/?t=85)

```text
IServiceCollection
  |
  +-- AddOptions<FeatureOptions>("TodoApi")
  |     .BindConfiguration(...) .ValidateOnStart()
  |
  +-- AddOptionsWithValidateOnStart<FeatureOptions>("WeatherStation")
  |     .BindConfiguration(...)
  |
  +-- TryAddEnumerable(
        ServiceDescriptor.Singleton
          IValidateOptions<FeatureOptions>  -->  FeatureOptions )
```

当应用程序启动时,框架会解析所有 `IValidateOptions<T>` 实现,并执行它们的 `Validate` 方法。
如果验证逻辑返回失败结果,应用程序会在启动序列中抛出一个未处理的异常。

```csharp
public sealed record class FeatureOptions : IValidateOptions<FeatureOptions>

/// <inheritdoc cref="IValidateOptions{TOptions}.Validate(string?, TOptions)" />
public ValidateOptionsResult Validate(string? name, FeatureOptions options)
{
    // Validate the "TodoApi" feature options.
    if (IsNamed(name, "TodoApi"))
    {
        if (options.Enabled is false)
        {
            return ValidateOptionsResult.Success;
        }
    }

    List<string> failures = [];

    if (options is { Endpoint: null })
    {
        failures.Add("TODO API requires a valid endpoint.");
    }

    if (options is { Version.Major: 0 })
    {
        failures.Add("TODO API running non-production version");
    }

    if (failures.Count is > 0)
    // ...
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/registering-options-with-validation-58112634/?t=115)

如果发生多个验证错误,它们会作为一组失败的聚合向上传递。
例如,如果配置指定了一个非生产版本,或者某个功能处于无效状态,应用程序就会停止,并显示 `ValidateOptionsResult` 中定义的具体失败消息。

## 4. Using data-annotations for declarative validation

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/using-data-annotations-for-declarative-validation-58112635/) · 4:45

### 总结

.NET Options 模式中的声明式验证允许开发者使用标准的数据注解(data annotation)特性来强制执行配置约束。
通过安装 Microsoft.Extensions.Options.DataAnnotations 包,并在服务注册时调用 .ValidateDataAnnotations(),选项类就可以用 [MaxLength]、[RegularExpression] 和 [DeniedValues] 等特性进行修饰,从而在应用程序启动时或运行时确保配置数据的完整性。

### 核心概念

* **NuGet 包**:需要 `Microsoft.Extensions.Options.DataAnnotations` 来启用验证逻辑。
* **注册**:在服务配置期间,把 `.ValidateDataAnnotations()` 方法链式调用在 `OptionsBuilder` 上。
* **声明式特性**:使用标准的 `System.ComponentModel.DataAnnotations` 特性,直接在选项类的属性上定义验证规则。
* **绑定与验证**:配置绑定(把原始字符串转换为 `System.Version` 之类的类型)发生在数据注解验证之前。如果绑定失败,会在验证逻辑执行之前就抛出 `FormatException`。
* **验证时机**:当与 `.ValidateOnStart()` 结合使用时,如果配置无效,验证错误会阻止应用程序启动。

### 课程笔记

要使用数据注解进行选项验证,你必须先安装 `Microsoft.Extensions.Options.DataAnnotations` NuGet 包。
这个包提供了必要的扩展方法,把标准的 .NET 验证特性集成到 Options 模式中。

```xml
<Project Sdk="Microsoft.NET.Sdk.Worker">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <UserSecretsId>dotnet-options_validation-0fe84c60-8265-4866-8372-865b1cd815a6</UserSecretsId>
    <RootNamespace>Options.Validation</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.Extensions.Hosting" Version="8.0.0" />
    <PackageReference Include="Microsoft.Extensions.Options.DataAnnotations" Version="8.0.0" />
  </ItemGroup>
</Project>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/using-data-annotations-for-declarative-validation-58112635/?t=10)

包安装完成后,通过在 `Program.cs` 中的选项注册上链式调用 `.ValidateDataAnnotations()` 来启用验证。
它既可以与 `AddOptions<T>()` 一起使用,也可以与 `AddOptionsWithValidateOnStart<T>()` 一起使用。

```csharp
using Options.Validation;
using Options.Validation.Features;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddOptions<FeatureOptions>(name: "TodoApi")
    .BindConfiguration(configSectionPath: "Features:TodoApi")
    .ValidateOnStart()
    .ValidateDataAnnotations();

builder.Services.AddOptionsWithValidateOnStart<FeatureOptions>(name: "WeatherStation")
    .BindConfiguration(configSectionPath: "Features:WeatherStation")
    .ValidateDataAnnotations();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/using-data-annotations-for-declarative-validation-58112635/?t=25)

基础设施就位之后,你就可以用 `System.ComponentModel.DataAnnotations` 命名空间中的特性来修饰选项类。
这样可以对属性值进行细粒度控制,例如为字符串设置最大长度,或定义允许/禁止的值。

```csharp
using System.ComponentModel.DataAnnotations;

namespace Options.Validation.Features;

public sealed record class FeatureOptions
{
    public bool Enabled { get; set; }

    [MaxLength(
        length: 100,
        ErrorMessage = "The name cannot be longer than 100 characters.")]
    public string? Name { get; set; }

    [RegularExpression(
        pattern: @"^\d+(\.\d+){1,3}$",
        ErrorMessage = "The version input doesn't match the regex.")]
    public string? Version { get; set; }

    public Uri? Endpoint { get; set; }

    [Key]
    public string? ApiKey { get; set; }

    [DeniedValues(
        values: [
            "deprecated",
            "out-of-date"
        ])]
    public string[] Tags { get; set; } = [];
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/using-data-annotations-for-declarative-validation-58112635/?t=85)

区分绑定错误和验证错误很重要。
如果某个属性的类型是 `System.Version`,而配置提供的是 `"v1"` 这样的字符串,配置绑定器将无法转换该值,从而在检查数据注解之前就导致 `System.InvalidOperationException`。

```text
fail: Microsoft.Extensions.Hosting.Internal.Host[11]
      Hosting failed to start
      System.InvalidOperationException: Failed to convert configuration value at 'Features:WeatherStation:Version' to type 'System.Version'.
      ---> System.FormatException: v1 is not a valid value for Version.
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/using-data-annotations-for-declarative-validation-58112635/?t=125)

```text
appsettings.json "Version": "v1"
        |
        v
  配置绑定(string -> 属性类型)
        |
        +-- Version? Version  --> 转换失败
        |                         System.InvalidOperationException
        |                         (FormatException)
        |
        +-- string? Version   --> 绑定成功
                |
                v
          数据注解验证 [RegularExpression]
                |
                +-- 不匹配 --> OptionsValidationException
```

为了让 `[RegularExpression]` 特性而不是绑定器来处理验证,可以把属性类型改为 `string`。
当配置不满足数据注解的约束时,应用程序会抛出 `Microsoft.Extensions.Options.OptionsValidationException`。

```text
fail: Microsoft.Extensions.Hosting.Internal.Host[11]
      Hosting failed to start
      Microsoft.Extensions.Options.OptionsValidationException: DataAnnotation validation failed for 'FeatureOptions' members : 'Version' with the error: 'The version input doesn't match the regex.'.
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/using-data-annotations-for-declarative-validation-58112635/?t=190)

一旦 `appsettings.json` 中的配置值被修正为满足数据注解约束(例如把版本 `"v1"` 改为 `"0.0"`),应用程序就会成功启动,选项也会被正确填充。

```json
{
  "Features": {
    "TodoApi": {
      "Enabled": true,
      "Name": "TODOs API",
      "Version": "0.4.20",
      "Endpoint": "https://jsonplaceholder.typicode.com/todos",
      "ApiKey": "RGF2aWQgUGluZSB3YXMgaGVyZSDwn6TIQw==",
      "Tags": [
        "self-improvement",
        "organization"
      ]
    },
    "WeatherStation": {
      "Enabled": true,
      "Name": "Weather Station - Pewaukee, WI",
      "Version": "0.0",
      "Tags": [
        "deprecated"
      ]
    }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/using-data-annotations-for-declarative-validation-58112635/?t=235)

## 5. Handling validation errors

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/handling-validation-errors-58112637/) · 4:36

### 总结

本课演示如何在 .NET Options 模式中处理验证错误:从一个会终止应用程序的全局 try-catch 块,转向在使用选项的服务内部进行隔离的错误处理。
通过在获取选项的位置捕获 OptionsValidationException,开发者可以记录具体的配置错误并提供回退行为,而不会让整个宿主崩溃。
本课还探讨了运行时验证与 AddOptionsWithValidateOnStart 所提供的"快速失败"方式之间的权衡。

### 核心概念

- **OptionsValidationException**:在获取选项期间,数据注解或自定义验证失败时,由选项系统抛出的特定异常。
- **隔离的错误处理**:在使用选项的服务内部捕获验证异常,以防止整个应用程序崩溃的做法。
- **运行时验证**:只在访问选项时(例如通过 `IOptionsMonitor.Get()`)才进行的验证。
- **启动时验证**:使用 `AddOptionsWithValidateOnStart` 强制在应用程序引导阶段进行验证,从而实现"快速失败"策略。

### 课程笔记

一开始,验证错误可能是通过把整个宿主的创建和执行包在一个全局 `try-catch` 块中来处理的。
虽然这能防止未处理的异常让进程悄无声息地崩溃,但它常常会因为单个配置错误就导致整个应用程序被拆除。

```csharp
using Options.Validation;
using Options.Validation.Features;

try
{
    var builder = Host.CreateApplicationBuilder(args);

    builder.Services.AddOptions<FeatureOptions>("TodoApi")
        .BindConfiguration("Features:TodoApi")
        .ValidateDataAnnotations();

    builder.Services.AddOptions<FeatureOptions>("WeatherStation")
        .BindConfiguration("Features:WeatherStation")
        .ValidateDataAnnotations();

    builder.Services.AddHostedService<Worker>();

    var host = builder.Build();
    host.Run();
}
catch (Exception ex)
{
    Console.OutputEncoding = System.Text.Encoding.UTF8;
    Console.Error.WriteLine($"{ex.Message} - 🙈 🙊 🙉");
    Console.Error.WriteLine(ex);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/handling-validation-errors-58112637/?t=10)

为了提升韧性,应当从 `Program.cs` 中移除全局 `try-catch`,从而允许在真正使用这些选项的服务内部进行更细粒度的错误处理。

```csharp
using Options.Validation;
using Options.Validation.Features;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddOptions<FeatureOptions>("TodoApi")
    .BindConfiguration("Features:TodoApi")
    .ValidateDataAnnotations();

builder.Services.AddOptions<FeatureOptions>("WeatherStation")
    .BindConfiguration("Features:WeatherStation")
    .ValidateDataAnnotations();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/handling-validation-errors-58112637/?t=40)

在使用选项的服务(例如一个 `BackgroundService`)中,选项通常通过 `IOptionsMonitor<T>` 来访问。
当调用 `options.Get(name)` 时,选项系统会执行验证。
如果配置无效,它会抛出一个 `OptionsValidationException`。

```csharp
public sealed class Worker(
    ILogger<Worker> logger,
    IOptionsMonitor<FeatureOptions> options) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation(
                    "TODO API feature options: {Options}",
                    GetNamedOptionsAsLogString("TodoApi"));

                logger.LogInformation(
                    "Weather Station feature options: {Options}",
                    GetNamedOptionsAsLogString("WeatherStation"));
            }

            await Task.Delay(10_000, stoppingToken);
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/handling-validation-errors-58112637/?t=55)

为了优雅地处理这些错误,你可以把获取逻辑封装到一个捕获 `OptionsValidationException` 的辅助方法中。
这让服务能够记录具体的验证失败(例如正则表达式不匹配),并返回一个默认值或空字符串,而不是崩溃。

```csharp
private string GetNamedOptionsAsLogString(string name)
{
    try
    {
        return options.Get(name)?.ToString() ?? "";
    }
    catch (OptionsValidationException ex)
    {
        logger.LogError(
            "{Name} ({Type}): {Errors}",
            ex.OptionsName, ex.OptionsType, ex.Message);

        return "";
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/handling-validation-errors-58112637/?t=70)

```text
Worker.ExecuteAsync
     |
     v
GetNamedOptionsAsLogString(name)
     |
     v
options.Get(name)  -- 选项系统执行验证
     |
     +-- 有效   --> ToString()            --> LogInformation
     |
     +-- 无效   --> OptionsValidationException
                       |
                       v
                  catch: LogError(OptionsName, OptionsType, Message)
                  return ""                --> 服务继续运行
```

例如,如果 `appsettings.json` 包含一个与 `FeatureOptions` 数据注解中定义的预期格式不匹配的版本字符串,catch 块就会拦截该错误。

```json
{
    "Features": {
        "TodoApi": {},
        "WeatherStation": {
            "Enabled": true,
            "Name": "Weather Station - Pewaukee, WI",
            "Version": "v1",
            "Tags": [
                "deprecated"
            ]
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/handling-validation-errors-58112637/?t=130)

#### 验证时机

默认情况下,`ValidateDataAnnotations()` 在运行时首次请求选项时执行验证。
这使得应用程序即使在配置无效的情况下也能启动,前提是在调用处对失败的选项进行了处理。

或者,你也可以使用 `AddOptionsWithValidateOnStart<T>()`。
这会强制应用程序在启动序列中验证选项。
如果验证失败,应用程序会抛出异常并无法启动。
这种"快速失败"的方式有助于在部署后立即发现环境配置错误。

```csharp
using Options.Validation;
using Options.Validation.Features;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddOptionsWithValidateOnStart<FeatureOptions>("TodoApi")
    .BindConfiguration("Features:TodoApi")
    .ValidateDataAnnotations();

builder.Services.AddOptionsWithValidateOnStart<FeatureOptions>("WeatherStation")
    .BindConfiguration("Features:WeatherStation")
    .ValidateDataAnnotations();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/handling-validation-errors-58112637/?t=250)

## 6. Expressing simple delegate-based validation with fluent APIs

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/expressing-simple-delegate-based-validation-with-fluent-apis-58112638/) · 3:21

### 总结

本课演示如何使用流式(fluent)Validate API 为 .NET Options 模式实现自定义验证逻辑。
它涵盖了从内联的基于 lambda 的验证,过渡到封装在静态类中的验证逻辑,这改善了代码组织,并使验证规则能够进行单元测试。
本课还强调了这些验证委托是如何限定在特定命名选项范围内的,从而确保自定义规则只会针对预期的配置节执行。

### 核心概念

- **流式验证**:在选项注册过程中使用 `.Validate<TOptions>` 方法定义自定义逻辑。
- **基于委托的逻辑**:通过返回布尔值的谓词来实现验证,该布尔值表示配置是否有效。
- **验证作用域**:流式验证只针对它所链接的那个选项实例;它不会自动应用到同一类型的其他实例上。
- **封装**:把验证逻辑从内联 lambda 移到静态类中,以便于单元测试和复用。
- **基于元组的验证器**:把验证谓词及其对应的失败消息组织成元组,使注册代码更简洁。

### 课程笔记

基于委托的验证是通过 Options 流式 API 中的 `Validate` 方法实现的。
该方法链式调用在 `BindConfiguration` 或 `AddOptions` 之后。
它提供对已填充选项对象的访问,从而允许编写评估多个属性的复杂逻辑。
例如,你可以强制要求:如果某个功能已启用,就必须提供特定的 URI。

```csharp
using Options.Validation;
using Options.Validation.Features;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddOptions<FeatureOptions>("TodoApi")
    .BindConfiguration("Features:TodoApi")
    .ValidateDataAnnotations();

builder.Services.AddOptions<FeatureOptions>("WeatherStation")
    .BindConfiguration("Features:WeatherStation")
    .Validate(options =>
    {
        if (options is { Enabled: true, Endpoint: null })
        {
            return false;
        }

        return true;
    },
    "The weather station cannot be enabled without a valid URI.");

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/expressing-simple-delegate-based-validation-with-fluent-apis-58112638/?t=10)

`Validate` API 的一个重要特性是,它的作用域限定在特定的命名选项实例上。
在上面的示例中,"WeatherStation" 实例的验证逻辑不会针对 "TodoApi" 实例执行,尽管它们共享同一个 `FeatureOptions` 类型。

```text
FeatureOptions
  |
  +-- "TodoApi"         --> .ValidateDataAnnotations()
  |
  +-- "WeatherStation"  --> .Validate(options => ..., "The weather station ...")
                            (只对 "WeatherStation" 执行)
```

虽然内联 lambda 对简单检查很有用,但把验证逻辑封装到一个独立的类中,可以让这些逻辑独立于依赖注入容器进行单元测试。
通过把验证规则定义为静态成员,就可以在服务注册时轻松地引用它们。

```csharp
namespace Options.Validation.Features;

internal static class FeatureOptionsValidators
{
    internal static (
        Func<FeatureOptions, bool> Validation,
        string FailureMessage
    ) EnabledWithMissingEndpoint => (
        Validation: static options =>
        {
            if (options is { Enabled: true, Endpoint: null })
            {
                return false;
            }

            return true;
        },
        FailureMessage: "The weather station cannot be enabled without a valid URI."
    );
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/expressing-simple-delegate-based-validation-with-fluent-apis-58112638/?t=160)

验证逻辑封装完成后,就可以借助 `using static` 指令来简化 `Program.cs` 中的注册代码。
这使你可以把验证谓词和失败消息直接从验证器类传入 `Validate` 方法。

```csharp
using Options.Validation;
using Options.Validation.Features;
using static Options.Validation.Features.FeatureOptionsValidators;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddOptions<FeatureOptions>(name: "TodoApi")
    .BindConfiguration(configSectionPath: "Features:TodoApi")
    .ValidateDataAnnotations();

builder.Services.AddOptions<FeatureOptions>(name: "WeatherStation")
    .BindConfiguration(configSectionPath: "Features:WeatherStation")
    .Validate(
        validation: EnabledWithMissingEndpoint.Validation,
        failureMessage: EnabledWithMissingEndpoint.FailureMessage);

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/expressing-simple-delegate-based-validation-with-fluent-apis-58112638/?t=130)

## 7. Controlling when validation errors occur

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/controlling-when-validation-errors-occur-58112639/) · 2:24

### 总结

在 .NET 应用程序中,验证错误发生的时机是一个关键的设计考量,它取决于具体的工作负载,例如 ASP.NET Core Web 应用或 worker 服务。
通过理解验证何时发生(无论是在启动时还是首次访问时),开发者可以在"快速失败"以立即捕获错误配置,与允许服务隔离错误之间做出选择。
本课探讨了启动时立即验证与运行时验证之间的权衡,并强调在应用程序开始处理请求之前发现环境配置错误的重要性。

### 核心概念

* **启动理念**:决定何时触发验证的策略,它影响错误的处理方式,以及错误是否会拆除整个应用程序。
* **特定于工作负载的需求**:不同的 .NET 应用程序类型(ASP.NET Core、Worker Services、控制台应用)需要不同的验证时机策略。
* **快速失败验证**:在启动时立即发现错误配置,以防止在最初的用户请求期间出现运行时异常。
* **验证方法**:无论验证是通过数据注解、流式 API 还是手动逻辑来表达,其执行时机都必须被显式管理。

### 课程笔记

在实现 options 模式时,验证错误发生的时机与验证逻辑本身同样重要。
根据应用程序的需求,你可能希望验证把错误隔离在各个服务内部,也可能希望它触发整个应用程序关闭,以避免在无效状态下运行。
这个决定在很大程度上受到正在开发的 .NET 工作负载类型的影响。

在 ASP.NET Core Web 应用程序中,通常希望验证在启动时立即发生。
这种"快速失败"的方式确保在服务器开始接收流量之前就发现配置错误的环境。
如果没有启动时验证,错误可能只会在运行时、当最初的请求尝试访问配置错误的选项时才暴露出来,从而在生产环境中导致意外故障。

相比之下,对于 worker 服务,托管服务(hosted service)通常作为应用程序初始启动序列的一部分启动。
因此,与该 worker 所使用的选项相关的任何验证错误,自然都会在启动阶段暴露出来。

下面的示例演示了在宿主构建器上下文中,同时使用数据注解和自定义流式验证来注册选项:

```csharp
using Options.Validation;
using Options.Validation.Features;
using static Options.Validation.Features.FeatureOptionsValidators;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddOptions<FeatureOptions>("TodoApi")
    .BindConfiguration("Features:TodoApi")
    .ValidateDataAnnotations();

builder.Services.AddOptions<FeatureOptions>("WeatherStation")
    .BindConfiguration("Features:WeatherStation")
    .Validate(
        validation: EnabledWithMissingEndpoint.Validation,
        failureMessage: EnabledWithMissingEndpoint.FailureMessage);

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/controlling-when-validation-errors-occur-58112639/?t=130)

虽然这些方法定义了数据*如何*被验证,开发者仍然必须考虑执行时机。
除了手动验证和声明式验证之外,.NET 还提供了编译时的替代方案,例如源生成器(source generator),以更高效地处理选项验证。

## 8. Compile-time validation with source-generators

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/compile-time-validation-with-source-generators-58112640/) · 4:07

.NET 中 Options 模式的编译时验证利用源生成器来自动实现验证逻辑。
通过把 `[OptionsValidator]` 特性应用到一个实现了 `IValidateOptions<TOptions>` 的 partial 类上,开发者可以使用标准的数据注解,而由编译器为 `Validate` 方法生成必要的样板代码。
这种方式把验证逻辑从运行时反射转移到编译时生成,从而提升性能,并且在缺少必需的类修饰符(例如 partial)时,通过编译器警告提供即时反馈。

### 核心概念

*   **源生成器验证**:使用 `[OptionsValidator]` 特性在编译时生成验证逻辑。
*   **Partial 类**:选项类必须标记为 `partial`,以便源生成器注入 `Validate` 方法的实现。
*   **IValidateOptions<T>**:源生成器为提供验证逻辑而实现的接口。
*   **数据注解支持**:`[MaxLength]`、`[RegularExpression]` 和 `[DeniedValues]` 等标准特性被用作生成代码的来源。
*   **性能**:消除了通常与数据注解验证相关的运行时反射开销。

### 课程笔记

要启用由编译时源生成器驱动的选项验证,应用程序仍然必须在服务集合中配置为使用数据注解验证。
这是在选项注册过程中通过 `ValidateDataAnnotations()` 方法完成的。

```csharp
using Options.Validation;
using Options.Validation.Features;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddOptions<FeatureOptions>(name: "TodoApi")
    .BindConfiguration(configSectionPath: "Features:TodoApi")
    .ValidateDataAnnotations();

builder.Services.AddOptions<FeatureOptions>(name: "WeatherStation")
    .BindConfiguration(configSectionPath: "Features:WeatherStation")
    .ValidateDataAnnotations();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/compile-time-validation-with-source-generators-58112640/?t=10)

选项类本身必须实现 `IValidateOptions<T>` 接口,并用 `[OptionsValidator]` 特性修饰。
这个特性会触发源生成器在编译时创建 `Validate` 方法的实现。

```csharp
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace Options.Validation.Features;

/// <summary>
/// A representation of a "feature" options object.
/// </summary>
[OptionsValidator]
public sealed partial record class FeatureOptions
    : IValidateOptions<FeatureOptions>
{
    /// <summary>
    /// Gets or sets whether the feature is enabled.
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// Gets or sets the name of the feature.
    /// </summary>
    [MaxLength(
        length: 100,
        ErrorMessage = "The name cannot be longer than 100 characters.")]
    public string? Name { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/compile-time-validation-with-source-generators-58112640/?t=25)

源生成器的一个关键要求是,类必须用 `partial` 关键字标记。
如果缺少 `partial` 修饰符,编译器会发出警告,指出生成器需要它才能工作。

```csharp
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace Options.Validation.Features;

/// <summary>
/// A representation of a "feature" options object.
/// </summary>
[OptionsValidator]
public sealed partial record class FeatureOptions
    : IValidateOptions<FeatureOptions>
{
    /// <summary>
    /// Gets or sets whether the feature is enabled.
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// Gets or sets the name of the feature.
    /// </summary>
    [MaxLength(
        length: 100,
        ErrorMessage = "The name cannot be longer than 100 characters.")]
    public string? Name { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/compile-time-validation-with-source-generators-58112640/?t=90)

类定义 `public sealed partial record class` 各部分有特定的用途:
*   **public**:允许它作为选项类在整个应用程序中被使用。
*   **sealed**:防止继承。
*   **partial**:源生成器注入代码所必需。
*   **record**:利用内置功能(例如 `ToString` 实现),让日志记录更容易。

随着添加更多验证特性(例如 `[RegularExpression]` 或 `[DeniedValues]`),源生成器会自动把它们纳入生成的验证逻辑中。

```csharp
public sealed partial record class FeatureOptions
{
    /// <summary>
    /// Gets or sets whether the feature is enabled.
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// Gets or sets the name of the feature.
    /// </summary>
    [MaxLength(
        100,
        ErrorMessage = "The name cannot be longer than 100 characters.")]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the version of the feature.
    /// </summary>
    [RegularExpression(
        @"^\d+(\.\d+){1,3}$",
        ErrorMessage = "The version input doesn't match the regex.")]
    public string? Version { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/compile-time-validation-with-source-generators-58112640/?t=115)

```csharp
public sealed partial record class FeatureOptions
{
    /// <summary>
    /// Gets or sets the endpoint (<see cref="Uri" />) of the feature.
    /// </summary>
    public Uri? Endpoint { get; set; }

    /// <summary>
    /// Gets or sets the API key of the feature.
    /// </summary>
    [Key]
    public string? ApiKey { get; set; }

    /// <summary>
    /// Gets or sets any tags for the feature.
    /// </summary>
    [DeniedValues(
        [
            "deprecated",
            "out-of-date"
        ])]
    public string[] Tags { get; set; } = [];
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/compile-time-validation-with-source-generators-58112640/?t=145)

生成的代码可以在项目依赖项下的 `Microsoft.Extensions.Options.SourceGeneration` 分析器中查看。
生成器会产生一个 `Validate` 方法,它使用 `ValidateOptionsResultBuilder` 和验证特性的静态实例,针对所定义的特性逐一手动检查每个属性,以避免运行时分配和反射。

```csharp
namespace Options.Validation.Features
{
    partial record class FeatureOptions
    {
        public global::Microsoft.Extensions.Options.ValidateOptionsResult Validate(string? name, global::Options.Validation.Features.FeatureOptions options)
        {
            global::Microsoft.Extensions.Options.ValidateOptionsResultBuilder? builder = null;
            
            // Example of generated validation for the Name property
            var context = new global::System.ComponentModel.DataAnnotations.ValidationContext(options);
            context.MemberName = "Name";
            
            if (!global::System.ComponentModel.DataAnnotations.Validator.TryValidateValue(options.Name, context, validationResults, validationAttributes))
            {
                (builder ??= new ValidateOptionsResultBuilder()).AddResults(validationResults);
            }

            return builder is null ? global::Microsoft.Extensions.Options.ValidateOptionsResult.Success : builder.Build();
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/compile-time-validation-with-source-generators-58112640/?t=175)

```text
[OptionsValidator]
public sealed partial record class FeatureOptions
    : IValidateOptions<FeatureOptions>
    [MaxLength] [RegularExpression] [DeniedValues]
        |
        v
Microsoft.Extensions.Options.SourceGeneration
        |
        v
partial record class FeatureOptions
    Validate(string? name, FeatureOptions options)
        |
        +-- builder is null --> ValidateOptionsResult.Success
        +-- 否则            --> builder.Build()
```

当应用程序以无效配置运行时(例如版本字符串不匹配所要求的正则表达式),生成的验证逻辑会正确识别出该失败,并记录特性中定义的错误消息。

```text
fail: Options.Validation.Worker[0]
      WeatherStation (Options.Validation.Features.FeatureOptions): DataAnnotation validation failed for 'FeatureOptions' members: 'Version' with the error: 'The version input doesn't match the regex.'.
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/compile-time-validation-with-source-generators-58112640/?t=225)

## 9. Section Recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/section-recap-58112642/) · 1:36

### 总结

本课全面回顾了 .NET 中 Options 模式的验证技术。
它涵盖了为自定义逻辑实现 IValidateOptions<T> 接口、使用数据注解进行声明式验证,以及启动时验证与延迟验证之间的权衡。
此外,它还重点介绍了源生成的编译时验证等高级特性,以及在整个应用程序中管理验证错误的策略。

### 核心概念

- 用于自定义验证逻辑的 IValidateOptions<T> 接口。
- 用于报告成功、失败或跳过的 ValidateOptionsResult。
- 使用数据注解的声明式验证。
- 在 Program.cs 中的注册与配置。
- 验证时机:急切(启动时)与延迟(按需)。
- 使用源生成器([OptionsValidator])的编译时验证。

### 课程笔记

.NET 中的 Options 模式支持多种验证机制,以确保配置的完整性。
自定义验证的基础是 IValidateOptions<T> 接口。
实现该接口需要定义一个 Validate 方法,它评估一个选项对象并返回一个 ValidateOptionsResult。
这个结果可以表示验证成功、附带具体错误消息的失败,或者对某个特定实例跳过验证的决定。

若想采用更声明式的方式,数据注解库允许开发者把验证特性直接应用到选项类的属性上。
常见的特性包括 [Required]、[Range] 和 [MaxLength]。

验证通常在 Program.cs 文件中、在服务注册期间进行配置。
在使用 Options 构建器(通过 AddOptions<T>())时,你可以链式调用 ValidateDataAnnotations() 来启用基于特性的检查,或者向 Validate 方法提供一个 lambda 来实现自定义逻辑。
默认情况下,验证是延迟的:它在首次从依赖注入容器中解析选项时才发生。
为了强制采用"快速失败"的方式,可以使用 ValidateOnStart() 在应用程序启动时运行所有验证逻辑。

现代 .NET 版本还支持源生成的验证。
通过把 [OptionsValidator] 特性应用到一个实现了 IValidateOptions<T> 的 partial 类上,编译器会在编译时生成验证代码。
这种方式比基于反射的验证更高效,并且能提供即时反馈。

```csharp
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace Options.Validation.Features;

/// <summary>
/// A representation of a "feature" options object.
/// </summary>
[OptionsValidator]
public sealed partial record class FeatureOptions
    : IValidateOptions<FeatureOptions>
{
    /// <summary>
    /// Gets or sets whether the feature is enabled.
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// Gets or sets the name of the feature.
    /// </summary>
    [MaxLength(
        length: 100,
        ErrorMessage = "The name cannot be longer than 100 characters.")]
    public string? Name { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/section-recap-58112642/?t=10)

本课最后指出,这些验证技术确保配置错误能被尽早捕获(要么在编译时,要么在启动时),从而防止因无效设置导致的运行时故障。
这为接下来探讨使用选项监视(options monitoring)实现实时配置更新做好了铺垫。
