# Options Monitoring

> 课程:[From Zero to Hero: Configuration and Options in .NET](https://dometrain.com/course/from-zero-to-hero-configuration-and-options-in-dotnet/) · 第 8 章
> 共 8 课 · 约 20:14
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction to Options Monitoring](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/introduction-to-options-monitoring-58112730/) | 1:13 | [↓](#1-introduction-to-options-monitoring) |
| 2 | [Differentiating Options interfaces](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/differentiating-options-interfaces-58112732/) | 2:10 | [↓](#2-differentiating-options-interfaces) |
| 3 | [Visualizing an Alerting App Scenario](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/visualizing-an-alerting-app-scenario-58112733/) | 1:07 | [↓](#3-visualizing-an-alerting-app-scenario) |
| 4 | [Exploring the Alerting Implementation](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/exploring-the-alerting-implementation-58112734/) | 7:47 | [↓](#4-exploring-the-alerting-implementation) |
| 5 | [Post Code-walk: Options and Settings](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/post-code-walk-options-and-settings-58112735/) | 3:08 | [↓](#5-post-code-walk-options-and-settings) |
| 6 | [Changing Config Data & Monitoring Alerts](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/changing-config-data-monitoring-alerts-58112737/) | 2:04 | [↓](#6-changing-config-data--monitoring-alerts) |
| 7 | [Being Mindful of Monitoring Limitations](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/being-mindful-of-monitoring-limitations-58112738/) | 1:32 | [↓](#7-being-mindful-of-monitoring-limitations) |
| 8 | [Section Recap](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/section-recap-58112739/) | 1:13 | [↓](#8-section-recap) |

## 1. Introduction to Options Monitoring

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/introduction-to-options-monitoring-58112730/) · 1:13

### 总结

选项监视(options monitoring)使 .NET 应用能够在运行时检测并响应配置源中的变化。
虽然环境变量和命令行参数等某些配置源不支持变更通知,但基于文件的配置源(JSON、XML、INI)以及云配置提供程序允许选项模式动态更新,而无需重启应用。

### 核心概念

* **配置变更检测(Configuration Change Detection)**:选项模式监听底层配置提供程序中更新的能力。
* **运行时通知(Runtime Notifications)**:允许应用在某个配置值被修改后立即采取行动的机制。
* **提供程序限制(Provider Limitations)**:并非所有配置源都支持监视;具体来说,环境变量和命令行参数是静态的。
* **受支持的配置源(Supported Sources)**:监视主要用于基于文件的提供程序(JSON、XML、INI)、User Secrets 以及基于云的配置服务。

### 课程笔记

选项监视让选项模式的各种实现能够利用配置源中的变更检测。
这确保了当底层配置发生变化时,应用可以收到通知并做出相应响应,通常可以避免重启进程。

#### 受支持与不受支持的提供程序

区分支持变更跟踪的配置提供程序和不支持的配置提供程序很重要:

* **环境变量**:进程启动后,目前没有任何机制可以监视环境变量的变化。
* **命令行参数**:它们只在启动时提供一次,因此变更检测对这个提供程序没有意义。
* **基于文件的提供程序**:存储在 XML、JSON 或 INI 文件中的配置可以在文件系统上被监视变化。
* **开发与云提供程序**:User Secrets、容器专用的配置配置文件(profile),以及各种云配置提供程序(例如 Azure 或 AWS 的提供程序)通常都支持变更通知。

借助这些通知,开发者可以确保应用与其环境保持同步。
这一功能构成了在不同选项接口(例如 `IOptionsSnapshot` 和 `IOptionsMonitor`)之间做选择的基础,具体取决于设置所需的生命周期和更新频率。

## 2. Differentiating Options interfaces

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/differentiating-options-interfaces-58112732/) · 2:10

### 总结

本课解释了 .NET 中三个主要选项接口之间的区别:IOptions<T>、IOptionsSnapshot<T> 和 IOptionsMonitor<T>。
它详细说明了这些接口的服务生命周期(Singleton 或 Scoped),以及这些生命周期如何决定配置值何时被读取或重新计算,这对于根据动态重新加载或性能等应用需求选择合适的接口至关重要。

### 核心概念

- **服务生命周期(Service Lifetimes)**:理解 Transient、Scoped 和 Singleton 生命周期是正确使用选项模式的基础。
- **IOptions<T>**:一个 Singleton 接口,在启动时读取一次配置。
- **IOptionsSnapshot<T>**:一个 Scoped 接口,为每个新作用域重新计算值,非常适合按请求更新。
- **IOptionsMonitor<T>**:一个 Singleton 接口,支持变更通知和配置值的动态重新加载。

### 课程笔记

理解可用选项接口之间的区别,对于正确实现选项模式至关重要。
由于选项模式依赖于依赖注入,因此首先回顾一下 .NET 中的标准服务生命周期很重要:

- **Transient**:每次从服务容器请求服务时都会新建服务。
- **Scoped**:每个作用域创建一次服务(例如在 ASP.NET Core 中每个 HTTP 请求一次)。
- **Singleton**:服务只创建一次,并在应用的整个生命周期内存续。

下面的摘要概述了主要的选项接口及其关联的生命周期:

```markdown
# Service Lifetime Reminder

- **Transient**: Created anew each time they're requested.
- **Scoped**: Created anew for a single scope as needed.
- **Singleton**: Created only once for the life of the app.

# Options Interfaces

- `IOptions<TOptions>`:

  - Singleton service lifetime.
  - Only read once, at app startup.

- `IOptionsSnapshot<TOptions>`:

  - Scoped service lifetime.
  - Values are recomputed for each new scope.
  - Designed for transient and scoped dependencies.

- `IOptionsMonitor<TOptions>`:

  - Singleton service lifetime.
  - Enables change detection
  - Supports dynamic reloading of values.
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/differentiating-options-interfaces-58112732/?t=10)

```text
+-----------------------------+-----------+------------------------------+
| Interface                   | Lifetime  | Values                       |
+-----------------------------+-----------+------------------------------+
| IOptions<TOptions>          | Singleton | read once, at app startup    |
| IOptionsSnapshot<TOptions>  | Scoped    | recomputed for each scope    |
| IOptionsMonitor<TOptions>   | Singleton | change detection, reloading  |
+-----------------------------+-----------+------------------------------+
```

#### IOptions<T>

`IOptions<T>` 接口被注册为 Singleton 服务。
它只在应用启动期间读取一次配置值。
因为它是 Singleton,所以可以安全地注入到任何生命周期(Transient、Scoped 或 Singleton)的服务中。
不过,它不支持在应用启动后读取更新过的配置值;这些值在进程存续期间保持静态。

#### IOptionsSnapshot<T>

`IOptionsSnapshot<T>` 是一个 Scoped 服务。
这意味着配置值会为每个新作用域重新计算。
在 ASP.NET Core 应用的上下文中,这通常意味着选项在每个 HTTP 请求中被求值一次。
这个接口是为在 transient 或 scoped 依赖中使用而设计的,适用于你需要配置反映底层源(例如 `appsettings.json` 文件)中所做的更改、又不想重启应用的场景。

#### IOptionsMonitor<T>

`IOptionsMonitor<T>` 是一个 Singleton 服务,它通过通知实现变更检测。
它支持值的动态重新加载,使应用能够立即对配置变化做出反应。
因为它是 Singleton,所以无论其他服务的生命周期如何,它都可以被注入其中,这使它成为在 Singleton 服务中需要实时更新的场景下的首选。

## 3. Visualizing an Alerting App Scenario

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/visualizing-an-alerting-app-scenario-58112733/) · 1:07

### 总结

本课介绍了一个告警应用的实际场景,该应用监视来自多个气象站的温度读数。
该系统被设计为高度可配置,允许调整气象站数量、轮询间隔、气象站名称和温度阈值。
核心逻辑包括定期检查传感器数据,并在读数超出定义的安全范围时触发告警,这为演示 .NET 中的高级选项监视打下基础。

### 核心概念

- **可配置的传感器监视**:系统支持通过配置定义任意数量的气象站。
- **动态轮询间隔**:两次传感器读数周期之间的时长是一个可配置参数。
- **基于阈值的告警**:实现了将实时数据与预定义安全阈值进行比较的逻辑。
- **循环工作流**:一个反复执行的过程,包括获取数据、根据配置进行校验,以及记录日志或告警。

### 课程笔记

该应用场景涉及一个处理来自任意数量气象站数据的监视系统。
为了确保灵活性,若干参数通过配置外部化:

- **气象站数量与标识**:气象站的具体数量及其各自的名称是可配置的。
- **轮询频率**:每个传感器读数周期之间的时长是可调整的。
- **告警阈值**:每个气象站或读数类型都有定义好的阈值,用于确定某个值何时被视为不安全。

应用的工作流遵循一个循环模式:

1.  **初始化**:启动时,应用获取所有传感器的当前配置。
2.  **数据采集**:系统从已配置的气象站拉取温度读数。
3.  **阈值校验**:每个读数都与其特定阈值进行比较。
4.  **执行动作**:
    - 如果读数在阈值范围内,系统记录该值,并进入等待状态,直到下一个轮询周期。
    - 如果读数超过或低于阈值,系统触发告警。

```text
+------------------+
| Initialization   |  retrieve current configuration for all sensors
+------------------+
         |
         v
+------------------+
| Data Acquisition |<----------------------------+
+------------------+                             |
         |                                       |
         v                                       |
+----------------------+                         |
| Threshold Validation |                         |
+----------------------+                         |
     |            |                              |
  within       exceeds /                         |
  threshold    falls below                       |
     |            |                              |
     v            v                              |
 log value    trigger alert                      |
     |                                           |
     v                                           |
 wait until next polling cycle ------------------+
```

这个场景为在 .NET 中实现和监视选项提供了一个具体用例,尤其适用于配置值可能需要在运行时更改而不重启应用的情况。

## 4. Exploring the Alerting Implementation

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/exploring-the-alerting-implementation-58112734/) · 7:47

本课探讨了在 .NET worker service 中使用 `IOptionsMonitor<T>` 监视传感器数据的一个实际实现。
它演示了如何使用 `OnChange` 方法对动态配置更新做出反应,以及如何在后台执行循环中访问 `CurrentValue` 属性。
该实现包括一个用于创建传感器服务的工厂模式,以及一个基于逻辑的告警系统,该系统将实时(模拟)数据与应用配置中定义的阈值进行比较,突出了在运行时更新监视参数而无需重启应用的能力。

### 核心概念

*   **IOptionsMonitor<T>**:用于动态配置跟踪和实时更新。
*   **OnChange 回调**:一种注册监听器的机制,每当底层配置源发生变化时该监听器就会触发。
*   **订阅管理**:`OnChange` 方法返回一个 `IDisposable`,必须对其进行管理,以防止内存泄漏或不需要的回调。
*   **CurrentValue**:提供对最近缓存的配置值的即时访问。
*   **BackgroundService 集成**:利用托管服务,按照动态选项中定义的间隔轮询数据。
*   **传感器工厂模式**:将具名传感器服务的创建与监视逻辑解耦。

### 课程笔记

#### 应用入口点与配置

该应用使用 .NET Worker Service 模板构建。
配置被设置为把一个特定的节绑定到 `SensorStationOptions`,并在启动时对其进行校验。
`SensorFactory` 和 `SensorMonitorService` 分别被注册为 singleton 和托管服务。

```csharp
using Options.Monitoring.SensorStation;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<SensorFactory>();
builder.Services.AddHostedService<SensorMonitorService>();

builder.Services.AddOptionsWithValidateOnStart<SensorStationOptions>()
    .BindConfiguration(configSectionPath:
        SensorStationOptions.SensorStationOptionsSectionName)
    .ValidateDataAnnotations();

var host = builder.Build();
host.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/exploring-the-alerting-implementation-58112734/?t=10)

#### 传感器工厂与服务实现

`SensorFactory` 是一个 internal、sealed 的类,负责创建和管理具名传感器服务。
它维护一个传感器字典,以确保对于给定名称总是返回同一个实例,从而有效地缓存这些服务。

```csharp
namespace Options.Monitoring.SensorStation;

internal sealed class SensorFactory
{
    private readonly Dictionary<string, ISensorService> _sensors =
        new Dictionary<string, ISensorService>(
            comparer: StringComparer.OrdinalIgnoreCase);

    public ISensorService Create(string name)
    {
        if (_sensors.TryGetValue(name, out var service))
        { 
            return service;
        }

        return _sensors[name] = name switch
        {
            // TODO: Add real sensors ...
            _ => new FakeSensorService()
        };
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/exploring-the-alerting-implementation-58112734/?t=20)

为了模拟真实世界的行为,`FakeSensorService` 会生成逐渐变化的温度读数。
它使用主构造函数设置初始值,并使用 `ReadTemperature` 方法根据一个随机增量计算下一个读数。

```csharp
namespace Options.Monitoring.SensorStation;

internal sealed class FakeSensorService(
    double initialTemperature = 65,
    double minChange = -0.05,
    double maxChange = 0.5) : ISensorService
{
    private double _currentTemperature = initialTemperature;

    public double ReadTemperature()
    {
        // Help to emulate a somewhat realistic/gradual change
        var change = Random.Shared.NextDouble() *
            (maxChange - minChange) + minChange;

        _currentTemperature += change;

        return _currentTemperature;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/exploring-the-alerting-implementation-58112734/?t=115)

#### 使用 IOptionsMonitor 进行监视

`SensorMonitorService` 实现了 `BackgroundService`,并利用 `IOptionsMonitor<SensorStationOptions>`。
在构造函数中,使用 `OnChange` 方法订阅配置变更。
该方法返回一个代表该订阅的 `IDisposable`。

```csharp
using Microsoft.Extensions.Options;

namespace Options.Monitoring.SensorStation;

internal sealed class SensorMonitorService : BackgroundService
{
    private readonly SensorFactory _sensorFactory;
    private readonly IOptionsMonitor<SensorStationOptions> _options;
    private readonly ILogger<SensorMonitorService> _logger;
    private readonly IDisposable? _onChangedDisposable;

    public SensorMonitorService(
        SensorFactory sensorFactory,
        IOptionsMonitor<SensorStationOptions> options,
        ILogger<SensorMonitorService> logger)
    {
        _sensorFactory = sensorFactory;
        _options = options;
        _logger = logger;
        _onChangedDisposable = options.OnChange(listener: OnOptionsChanged);
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/exploring-the-alerting-implementation-58112734/?t=175)

当底层配置(例如 `appsettings.json`)被更新时,`OnOptionsChanged` 回调会携带最新的选项被触发。
为了防止内存泄漏,订阅会在服务被释放时一并释放。

```csharp
    private void OnOptionsChanged(SensorStationOptions latestOptions)
    {
        _logger.LogInformation(message: """
            Threshold's changed:
                {Options}
            """
            ,
            args: latestOptions);
    }

    public override void Dispose()
    {
        _onChangedDisposable?.Dispose();
        base.Dispose();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/exploring-the-alerting-implementation-58112734/?t=250)

```text
appsettings.json updated
        |
        v
IOptionsMonitor<SensorStationOptions>
        |  OnChange(listener: OnOptionsChanged)
        v
OnOptionsChanged(latestOptions)  -->  _logger.LogInformation(...)

SensorMonitorService.Dispose()
        |
        v
_onChangedDisposable?.Dispose()   (subscription disposed)
```

#### 执行循环与告警逻辑

`ExecuteAsync` 方法包含主轮询循环。
它在每次迭代开始时从选项监视器中获取 `CurrentValue`。
这确保了即使配置在循环中途发生变化,下一次迭代也会使用更新后的值,例如新的 `PollingInterval` 或更新后的传感器阈值。

```csharp
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var options = _options.CurrentValue;

            foreach (var (sensor, thresholds) in options.Sensors ?? [])
            {
                var service = _sensorFactory.Create(sensor);
                var temp = service.ReadTemperature();

                AlertSensorReadings(sensor, temp, thresholds);
            }

            await Task.Delay(options.PollingInterval, stoppingToken);
        }
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/exploring-the-alerting-implementation-58112734/?t=355)

`AlertSensorReadings` 方法执行实际的阈值校验。
如果某个温度读数超出选项中定义的 `High` 或 `Low` 阈值,就会生成一条 critical 级别的日志条目来模拟告警。

```csharp
    private void AlertSensorReadings(
        string sensor, double temp, ThresholdOptions thresholds)
    {
        var isLowerThanOrAtMax = temp <= thresholds.High;
        var isGreaterThanOrAtMin = temp >= thresholds.Low;

        if (isLowerThanOrAtMax && isGreaterThanOrAtMin)
        {
            _logger.LogInformation(
                "Normal '{Sensor}' reading: {Temp:F2}°F",
                sensor, temp);
        }
        else
        {
            _logger.LogCritical("""
                Wow! The '{Sensor}' reading is out of range: {Temp:F2}°F
                """,
                sensor, temp);
        }
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/exploring-the-alerting-implementation-58112734/?t=415)

## 5. Post Code-walk: Options and Settings

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/post-code-walk-options-and-settings-58112735/) · 3:08

### 总结

本课探讨了在 .NET 应用中进行选项监视所需的选项模型和配置结构。
它详细介绍了如何使用 IValidateOptions 模式和源生成的校验器来实现 SensorStationOptions 和 ThresholdOptions,以确保配置的完整性。
本课还演示了这些类如何直接映射到 appsettings.json 文件,从而便于在应用运行期间进行实时配置更新。

### 核心概念

- 使用 [OptionsValidator] 和 IValidateOptions<T> 进行选项校验。
- 通过 partial 类实现源生成的校验。
- 将 JSON 配置节映射到强类型选项。
- 使用 Data Annotations 校验 TimeSpan 和数值范围。
- 用基于字典的配置实现动态的具名设置。

### 课程笔记

该应用使用强类型选项模型来管理传感器站配置。
主配置类 `SensorStationOptions` 实现了 `IValidateOptions<T>` 接口,并带有 `[OptionsValidator]` 特性。
这种设置启用了源生成的校验,用更高效的生成代码取代了运行时基于反射的特性检查。

```csharp
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace Options.Monitoring.SensorStation;

[OptionsValidator]
public sealed partial class SensorStationOptions
    : IValidateOptions<SensorStationOptions>
{
    /// <summary>
    /// Gets the name of the configuration section that
    /// corresponds to the <see cref="SensorStationOptions"/>.
    /// </summary>
    public const string SensorStationOptionsSectionName = "SensorOptions";

    /// <summary>
    /// Gets or sets the polling interval between sensor temperature readings.
    /// </summary>
    [Range(
        type: typeof(TimeSpan),
        minimum: "00:00:00",
        maximum: "23:59:59",
        ErrorMessage = "Time must be between 00:00:00 and 23:59:59")]
    [RegularExpression(
        pattern: @"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]$",
        ErrorMessage = "Time must be in the format HH:mm:ss and between 00:00:00 and 23:59:59")]
    public TimeSpan PollingInterval { get; set; }

    /// <summary>
    /// Gets or sets the map of named thresholds settings.
    /// </summary>
    [Required(ErrorMessage = "A mapping of station names to thresholds is required")]
    public Dictionary<string, ThresholdOptions>? Sensors { get; set; } = new();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/post-code-walk-options-and-settings-58112735/?t=10)

`SensorStationOptions` 类定义了一个常量 `SensorStationOptionsSectionName`,其值为 "SensorOptions",用于标识配置中的顶层节点。
`PollingInterval` 属性是一个 `TimeSpan`,决定收集传感器读数的频率。
它会被校验,以确保其落在 24 小时范围内并遵循 `HH:mm:ss` 格式。
`Sensors` 属性是一个字典,其中每个键是一个传感器名称,每个值是一个 `ThresholdOptions` 对象。

`ThresholdOptions` record 类同样实现了 `IValidateOptions` 模式,并使用源生成进行校验。
它包含 `Low` 和 `High` 两个阈值属性,这两个属性使用 `[Range]` 和 `[Required]` 特性进行校验。

```csharp
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;

namespace Options.Monitoring.SensorStation;

[OptionsValidator]
public sealed partial record class ThresholdOptions
    : IValidateOptions<ThresholdOptions>
{
    /// <summary>
    /// Gets or sets the low threshold.
    /// </summary>
    [Range(
        minimum: -0.001d,
        maximum: +1.000d)]
    [Required(ErrorMessage = "A low threshold value is required")]
    public double Low { get; set; }

    /// <summary>
    /// Gets or sets the high threshold.
    /// </summary>
    [Range(
        minimum: +1d,
        maximum: +5d)]
    [Required(ErrorMessage = "A high threshold value is required")]
    public double High { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/post-code-walk-options-and-settings-58112735/?t=115)

配置值定义在 `appsettings.json` 中。
`SensorOptions` 节直接映射到 `SensorStationOptions` 类。
它包含 `PollingInterval` 和一个 `Sensors` 对象,其中每个条目对应一个特定的传感器位置及其关联的阈值。

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "SensorOptions": {
    "PollingInterval": "00:00:10",
    "Sensors": {
      "Roof": {
        "Low": 50,
        "High": 90
      },
      "Roof 2": {
        "Low": 55,
        "High": 70
      },
      "Garage": {
        "Low": 30,
        "High": 50
      },
      "Patio": {
        "Low": 45,
        "High": 82.5
      }
    }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/post-code-walk-options-and-settings-58112735/?t=145)

```text
appsettings.json                    Options model
------------------------------      -------------------------------------
"SensorOptions"               -->   SensorStationOptions
  "PollingInterval"           -->     TimeSpan PollingInterval
  "Sensors"                   -->     Dictionary<string, ThresholdOptions>
    "Roof", "Garage", ...     -->       key -> ThresholdOptions
      "Low"                   -->         double Low
      "High"                  -->         double High
```

通过使用这个选项模型,应用可以监视 `appsettings.json` 文件的变化。
当文件被更新时,选项会被自动重新绑定并重新校验,从而允许在不重启的情况下实时更新应用的行为。

## 6. Changing Config Data & Monitoring Alerts

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/changing-config-data-monitoring-alerts-58112737/) · 2:04

### 总结

本课使用 IOptionsMonitor<T> 演示了 .NET 中配置监视的实时行为。
通过在应用运行时修改底层的 appsettings.json 文件,本课展示了系统如何自动检测变化、将更新后的值重新绑定到选项实例,并触发变更通知。
这种动态行为通过更新传感器阈值和完全移除配置节来展示,使应用无需重启即可调整其监视逻辑。

### 核心概念

* 通过 IOptionsMonitor<T> 实现实时配置更新。
* 保存源文件时自动重新绑定选项。
* 处理变更事件,以记录配置的变化或对其做出反应。
* 根据配置状态动态管理集合(例如传感器字典)。

### 课程笔记

应用启动时,会加载传感器站的初始配置。
IOptionsMonitor<T> 服务用于跟踪这些设置,其中包括一个轮询间隔和一个传感器字典,每个传感器都有自己的范围阈值。

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "PollingInterval": "00:00:30",
  "Sensors": {
    "Roof": {
      "Low": 50,
      "High": 90
    },
    "Roof 2": {
      "Low": 55,
      "High": 70
    },
    "Garage": {
      "Low": 30,
      "High": 50
    },
    "Patio": {
      "Low": 45,
      "High": 82.5
    }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/changing-config-data-monitoring-alerts-58112737/?t=40)

应用会主动监视这些传感器。
例如,如果 "Garage" 传感器报告的值超出了其定义的范围(最初为 30 到 50),应用就会将其识别为超出范围。
为了在不重启应用的情况下调整这一行为,可以修改底层的 appsettings.json。
将 "Garage" 传感器的 "High" 阈值提高到 80 并保存文件后,变更事件会被立即触发。
应用会记录阈值已更改,并开始对后续读数使用新的值。

此外,IOptionsMonitor<T> 还能处理配置的结构性变化,例如移除整个配置节。
如果从配置文件中删除某个传感器(例如 "Roof 2"),应用会收到变更通知并更新其内部状态。

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "SensorOptions": {
    "PollingInterval": "00:00:30",
    "Sensors": {
      "Roof": {
        "Low": 50,
        "High": 90
      },
      "Garage": {
        "Low": 30,
        "High": 80
      },
      "Patio": {
        "Low": 45,
        "High": 82.5
      }
    }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/changing-config-data-monitoring-alerts-58112737/?t=100)

更新之后,应用只监视剩余的传感器(Roof、Garage 和 Patio)。
任何来自已移除传感器的传入数据都会被忽略,因为它已不再是当前生效配置的一部分。
这表明 IOptionsMonitor<T> 提供了一种实时动态重新加载并绑定选项的有力方式,确保应用始终使用最新的配置值运行。

## 7. Being Mindful of Monitoring Limitations

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/being-mindful-of-monitoring-limitations-58112738/) · 1:32

### 总结

虽然 .NET 选项监视提供了强大的实时配置更新能力,但它仅限于基于文件系统的配置提供程序,并且在 Docker 或网络文件共享等特定环境中可能遇到可靠性问题。
开发者必须了解这些约束以及可用的基于轮询的变通方案,以确保在不同部署场景中行为一致。

### 核心概念

- 选项监视只受基于文件系统的配置提供程序支持。
- 受支持的提供程序包括 JSON、XML、INI、User Secrets 和 Key-per-file。
- Docker 和网络文件共享等环境可能无法触发变更通知。
- `DOTNET_USE_POLLING_FILE_WATCHER` 环境变量会启用一种轮询机制。
- 轮询以固定且不可配置的 4 秒间隔进行。

### 课程笔记

选项监视是 .NET 中一项强大的功能,但它并不适用于所有配置源。
它的功能严格限于基于文件系统的配置提供程序。
这意味着只有来自物理文件的配置数据才能触发监视所需的变更通知。

为这一功能提供一等支持的包包括:
- `Microsoft.Extensions.Configuration.Ini`
- `Microsoft.Extensions.Configuration.Json`
- `Microsoft.Extensions.Configuration.KeyPerFile`
- `Microsoft.Extensions.Configuration.UserSecrets`
- `Microsoft.Extensions.Configuration.Xml`

```markdown
# Monitoring Limitations

1. Limited to File-System Configuration Providers:

   - Microsoft.Extensions.Configuration.Ini
   - Microsoft.Extensions.Configuration.Json
   - Microsoft.Extensions.Configuration.KeyPerFile
   - Microsoft.Extensions.Configuration.UserSecrets
   - Microsoft.Extensions.Configuration.Xml

2. Some environments, such as File Shares or Docker Containers
   are unreliable for change notifications.

   - Set `DOTNET_USE_POLLING_FILE_WATCHER` to `true` for polling
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/being-mindful-of-monitoring-limitations-58112738/?t=10)

除了提供程序方面的限制之外,某些部署环境也会给文件系统监视器带来挑战。
在 Docker 容器或网络文件共享等环境中,底层操作系统或文件系统可能无法可靠地向 .NET 运行时发送变更通知。

为了解决这个问题,.NET 通过 `DOTNET_USE_POLLING_FILE_WATCHER` 环境变量提供了一种变通方案。
当该变量设置为 `true` 时,框架会从依赖操作系统级别的通知切换为一种轮询机制。
在这种模式下,框架会代表应用手动重新读取配置文件以检查变化。

需要注意的是,这个轮询间隔不可配置,固定为四秒。
虽然这确保了即使在受限环境中最终也能检测到变化,但与即时的文件系统通知相比,它会引入轻微的延迟。

## 8. Section Recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/section-recap-58112739/) · 1:13

### 总结

本课总结了对 .NET 中选项监视的探索,回顾了如何选择合适的 IOptions 接口,以及动态配置更新的实际应用。
它重点介绍了在轮询场景中使用后台服务,并讨论了与文件系统提供程序以及特定环境下通知可靠性相关的关键限制。

### 核心概念

- 区分 `IOptions`、`IOptionsSnapshot` 和 `IOptionsMonitor`,以确定适合特定用例的接口。
- 使用轮询机制在后台服务中实现动态配置更新。
- 校验运行时的配置变更,以确保应用行为与更新后的设置保持一致。
- 识别支持变更通知的具体配置提供程序。
- 使用轮询文件监视器解决 Docker 或文件共享中特定于环境的挑战。

### 课程笔记

选项监视让应用能够在运行时对配置变化做出反应,而无需重启。
这一实现的关键部分是根据服务是 singleton、scoped 还是需要实时变更通知,来选择正确的接口(`IOptions`、`IOptionsSnapshot` 或 `IOptionsMonitor`)。

在实际场景中,例如告警应用,后台服务可能会利用一个轮询系统来监视外部传感器。
通过使用 `IOptionsMonitor`,该服务可以随着底层配置数据的变化动态调整其阈值或行为。
这种方法已经通过在应用执行期间修改配置文件、并观察其对后台服务逻辑的即时影响得到了验证。

不过,开发者在实现监视时必须注意一些具体的限制。
变更通知主要由基于文件系统的配置提供程序支持。
此外,Docker 容器或网络文件共享等某些环境可能无法可靠地触发文件系统事件。

```markdown
## Monitoring Limitations

1. Limited to File-System Configuration Providers:

    - Microsoft.Extensions.Configuration.Ini
    - Microsoft.Extensions.Configuration.Json
    - Microsoft.Extensions.Configuration.KeyPerFile
    - Microsoft.Extensions.Configuration.UserSecrets
    - Microsoft.Extensions.Configuration.Xml

2. Some environments, such as File Shares or Docker Containers
   are unreliable for change notifications.

    - Set `DOTNET_USE_POLLING_FILE_WATCHER` to `true` for polling
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/section-recap-58112739/?t=10)

当变更通知不可靠时,将环境变量 `DOTNET_USE_POLLING_FILE_WATCHER` 设置为 `true` 会强制系统使用轮询机制来检测文件变化,从而确保配置更新仍然能够被处理。
