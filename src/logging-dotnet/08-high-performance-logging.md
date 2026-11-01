# High-performance logging

> 课程:[From Zero to Hero: Logging in .NET](https://dometrain.com/course/from-zero-to-hero-logging-in-dotnet/) · 第 8 章
> 共 4 课 · 约 12:32
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Why does it matter?](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/why-does-it-matter-54121709/) | 1:41 | [↓](#1-why-does-it-matter) |
| 2 | [The LoggerMessage.Define method](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-loggermessagedefine-method-54121710/) | 6:06 | [↓](#2-the-loggermessagedefine-method) |
| 3 | [The logging source generator](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-logging-source-generator-54121711/) | 4:07 | [↓](#3-the-logging-source-generator) |
| 4 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121712/) | 0:38 | [↓](#4-section-recap) |

## 1. Why does it matter?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/why-does-it-matter-54121709/) · 1:41

### 总结

高性能日志之所以关键,是因为日志是一个横切关注点,遍布整个应用程序。
"热路径"(hot path)中低效的日志会显著降低系统性能。
本节介绍一些高级 .NET 技术,让性能敏感代码中的日志尽可能廉价、高效。

### 核心概念

- **横切关注点(Cross-cutting concerns)**:日志在整个应用程序中都有实现;日志中的低效会在全局范围内叠加。
- **性能影响(Performance impact)**:在代码库中反复出现的低效日志模式,会导致显著的全系统性能下降。
- **热路径(Hot paths)**:高频执行的路径,必须把其中的日志开销降到最低,以维持吞吐量。
- **优化技术(Optimization techniques)**:介绍一些专门的 .NET 方法,它们把日志效率置于实现简洁性之上。

### 课程笔记

日志是 .NET 应用程序中一个基础的横切关注点。
由于它几乎在每个模块和服务中都有实现,它的性能影响是累积的。
如果日志实现得低效,这种低效就会在整个应用程序中一致地重复出现,从而导致可测量的性能问题。
"错误"的日志不仅仅会产生质量差的日志;它会直接影响应用程序的性能表现。

虽然在 .NET 中遵循标准的日志最佳实践通常就能得到高性能的日志,但某些"热路径",也就是那些执行频繁、要求最高效率的代码段,能从更深层的优化中获益。
.NET 提供了两种专门的技术,用来把日志性能再往前推进一步。
尽管这些方法实现起来可能更繁琐,也比标准的日志调用需要更加小心,但它们对于性能关键的场景是必不可少的。
本节探讨这些优化的核心,让开发者在不牺牲应用程序响应能力的前提下保持诊断可见性。

## 2. The LoggerMessage.Define method

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-loggermessagedefine-method-54121710/) · 6:06

### 总结

在 .NET 中,可以通过不再使用 LogInformation 这类标准扩展方法来实现高性能日志,这些方法会因值类型装箱以及为参数分配对象数组而产生开销。
LoggerMessage.Define 方法提供了一种创建缓存委托的方式,这些委托使用泛型类型参数来消除这些开销。
通过定义一个静态字段来保存这个委托,开发者可以以最少的分配执行日志操作,并利用经过优化的格式化器和自动的日志级别检查。

### 核心概念

- **性能开销(Performance Overhead)**:像 `LogInformation` 这样的标准日志方法会把值类型(例如 `int`、`decimal`)装箱为对象,并在每次日志调用时分配一个新的 `object[]`。
- **LoggerMessage.Define**:一个工具方法,为特定的日志模板和一组参数创建一个缓存的 `Action` 委托。
- **泛型类型参数(Generic Type Parameters)**:通过使用泛型(最多 6 个参数),`LoggerMessage.Define` 避免了值类型装箱。
- **静态初始化(Static Initialization)**:把日志委托定义为 `static readonly` 字段,确保模板只在编译/初始化期间被解析一次,格式化器也只被创建一次。
- **内部优化(Internal Optimizations)**:该方法会自动处理 `IsEnabled` 检查,并使用专门的格式化器来尽量减少运行时的工作。

### 课程笔记

在一个典型的 .NET 应用程序中,例如一个 `PaymentService`,日志通常是用标准扩展方法实现的。
这些方法虽然方便,却有隐藏的性能开销。

```csharp
public class PaymentService
{
    private readonly ILogger<PaymentService> _logger;

    public PaymentService(ILogger<PaymentService> logger)
    {
        _logger = logger;
    }

    public void CreatePayment(string email, decimal amount, int productId)
    {
        // Do some work
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-loggermessagedefine-method-54121710/?t=25)

在 `CreatePayment` 方法中,一种常见的日志实现如下:

```csharp
public void CreatePayment(string email, decimal amount, int productId)
{
    // Do some work
    _logger.LogInformation("Customer {Email} purchased product {ProductId} at {Amount}",
        email, amount, productId);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-loggermessagedefine-method-54121710/?t=100)

即使方法中做的工作很少,这个日志调用的开销也很大。
`decimal` 和 `int` 参数会被装箱为对象,并且会分配一个新数组来把这些参数传给日志基础设施。
为了优化这一点,可以使用 `LoggerMessage.Define` 方法。

要实现这项优化,需要创建一个 `private static readonly Action` 字段。
这个委托将保存缓存的日志操作。
`Action` 的签名必须包含 `ILogger`,接着是各参数的类型,最后是一个可空的 `Exception`。

```csharp
public class PaymentService
{
    private readonly ILogger<PaymentService> _logger;

    private static readonly Action<ILogger, string, decimal, int, Exception?> _logPayment =
        LoggerMessage.Define<string, decimal, int>(
            LogLevel.Information,
            new EventId(5001, nameof(CreatePayment)),
            "Customer {Email} purchased product {ProductId} at {Amount}"
        );

    public PaymentService(ILogger<PaymentService> logger)
    {
        _logger = logger;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-loggermessagedefine-method-54121710/?t=235)

在这个定义中:

1. **泛型**:`<string, decimal, int>` 确保参数在处理时不会被装箱。
2. **日志级别**:在定义时指定一次。
3. **EventId**:使用的是结构体,因此不会分配对象。
   使用 `nameof(CreatePayment)` 为事件提供了一个清晰的名称。
4. **模板**:该字符串是编译期字面量,并且会被驻留(interned)。

定义完成后,在方法中直接调用该委托:

```csharp
public void CreatePayment(string email, decimal amount, int productId)
{
    // Do some work
    _logPayment(_logger, email, amount, productId, null);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-loggermessagedefine-method-54121710/?t=255)

在内部,`LoggerMessage.Define` 会根据参数数量创建一个专门的 `LogValuesFormatter`。
它还包含在执行日志之前检查 `logger.IsEnabled(logLevel)` 的逻辑,从而在日志级别被抑制时避免不必要的工作。

```csharp
public static Action<ILogger, T1, T2, T3, Exception?> Define<T1, T2, T3>(LogLevel logLevel, EventId eventId, string formatString)
{
    LogValuesFormatter formatter = CreateLogValuesFormatter(formatString, expectedParameterCount: 3);

    void Log(ILogger logger, T1 arg1, T2 arg2, T3 arg3, Exception? exception)
    {
        logger.Log(logLevel, eventId, new LogValues<T1, T2, T3>(formatter, arg1, arg2, arg3), exception, (s, e) => s.ToString());
    }

    return (logger, arg1, arg2, arg3, exception) =>
    {
        if (logger.IsEnabled(logLevel))
        {
            Log(logger, arg1, arg2, arg3, exception);
        }
    };
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-loggermessagedefine-method-54121710/?t=305)

```text
static init (once)
  LoggerMessage.Define<T1, T2, T3>(logLevel, eventId, formatString)
    -> CreateLogValuesFormatter(formatString, 3) -> formatter
    -> returns Action<ILogger, T1, T2, T3, Exception?>

each call
  _logPayment(_logger, email, amount, productId, null)
    -> logger.IsEnabled(logLevel) ?
         false -> return
         true  -> Log(...)
                    -> logger.Log(logLevel, eventId,
                         new LogValues<T1, T2, T3>(formatter, ...), ...)
```

虽然这种方式性能很高,但如果一个类需要许多不同的日志消息,它就会变得繁杂,因为每条消息都需要自己的静态字段和手动的参数管理。
在现代 .NET 版本中,这种复杂性由 Logger Source Generator 来解决。

## 3. The logging source generator

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-logging-source-generator-54121711/) · 4:07

### 总结

.NET 中的日志源生成器利用编译期代码生成,为手动实现 LoggerMessage.Define 提供了一种高性能的替代方案。
通过用 [LoggerMessage] 特性修饰分部类中的分部方法,开发者能以最少的样板代码获得尽可能高的日志性能。
生成器会自动处理日志级别检查,并使用内部结构体优化参数存储,在无需管理私有静态委托这种复杂性的情况下确保高效执行。

### 核心概念

- **LoggerMessage 源生成器(LoggerMessage Source Generator)**:一个编译期工具,用于生成高度优化的日志代码。
- **分部类和分部方法(Partial Classes and Methods)**:必需的,以便源生成器为你的日志方法提供实现。
- **[LoggerMessage] 特性([LoggerMessage] Attribute)**:用于在编译期定义日志级别、事件 ID 和消息模板。
- **性能优化(Performance Optimization)**:自动避免值类型装箱,并包含内置的日志级别检查(`IsEnabled`),以防止不必要的分配。
- **扩展方法(Extension Methods)**:生成的方法通常实现为 `ILogger` 上的扩展方法,以获得简洁的开发体验。

### 课程笔记

虽然 `LoggerMessage.Define` 提供了高性能,但它需要管理私有静态委托和复杂的参数传递,这可能导致不够理想的开发体验。
日志源生成器解决了这个问题,它允许开发者定义简单的方法签名,由编译器自动实现。

要使用源生成器,首先创建一个静态分部类来存放日志扩展方法。

```csharp
namespace BasicConsoleApp;

public static partial class LoggerExtensions
{
    
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-logging-source-generator-54121711/?t=40)

接下来,定义一个分部方法签名。
这个方法应当是 `ILogger` 上的扩展方法,并包含日志消息所需的参数。

```csharp
using Microsoft.Extensions.Logging;

namespace BasicConsoleApp;

public static partial class LoggerExtensions
{
    public static void LogPaymentCreation(
        this ILogger logger, string email, decimal amount, int productId)
    {
        
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-logging-source-generator-54121711/?t=85)

要触发源生成器,需用 `[LoggerMessage]` 特性修饰该方法,并把方法标记为 `partial`。
你必须在特性中提供 `LogLevel`、一个 `EventId` 以及 `Message` 模板。

```csharp
using Microsoft.Extensions.Logging;

namespace BasicConsoleApp;

public static partial class LoggerExtensions
{
    [LoggerMessage(Level = LogLevel.Information,
        EventId = 5001,
        Message = "Customer {Email} purchased product {ProductId} at {Amount}")]
    public static partial void LogPaymentCreation(
        this ILogger logger, string email, decimal amount, int productId);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-logging-source-generator-54121711/?t=130)

定义完成后,就可以在任何能访问 `ILogger` 实例的类中直接调用该方法。
这取代了手动调用静态委托的做法。

```csharp
public void CreatePayment(string email, decimal amount, int productId)
{
    // Do some work
    _logger.LogPaymentCreation(email, amount, productId);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-logging-source-generator-54121711/?t=160)

#### 底层原理

源生成器会产生高度优化的代码,这些代码在 IDE 的生成文件中可见。
关键的优化包括:

1. **自动的日志级别检查**:生成的实现会在执行任何日志逻辑之前自动调用 `_logger.IsEnabled(LogLevel)`,从而无需在应用程序代码中手动检查。
2. **高效的参数存储**:生成器会创建一个 `readonly struct` 来保存日志参数。
   这比以对象数组的形式传递参数更高效,因为它避免了值类型(例如 `decimal` 或 `int`)的装箱。
3. **自定义格式化器**:它使用专为该操作生成的特定格式,确保内置日志提供程序获得尽可能高的性能。

```csharp
private readonly struct __LogPaymentCreationStruct : global::System.Collections.Generic.IReadOnlyList<global::System.Collections.Generic.KeyValuePair<string, object?>>
{
    private readonly global::System.String _email;
    private readonly global::System.Decimal _amount;
    private readonly global::System.Int32 _productId;

    public __LogPaymentCreationStruct(global::System.String email, global::System.Decimal amount, global::System.Int32 productId)
    {
        this._email = email;
        this._amount = amount;
        this._productId = productId;
    }
    // ... implementation of IReadOnlyList
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/the-logging-source-generator-54121711/?t=180)

## 4. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121712/) · 0:38

### 总结

本课总结了 .NET 中的高性能日志技术,重点是使用 LoggerMessage.Define 和 LoggerMessage 源生成器。
这些工具通过使用预编译的 action 和源生成的代码,来降低标准日志带来的性能开销和内存分配。

### 核心概念

- **LoggerMessage.Define**:创建一个用于日志的缓存委托,以避免模板解析开销。
- **LoggerMessage 源生成器(LoggerMessage Source Generator)**:使用 C# 源生成器自动实现日志方法。
- **性能优化(Performance Optimization)**:通过专门的结构体和方法,降低内存压力并提升执行速度。
- **编译期 action(Compile-time Actions)**:把日志逻辑从运行时解析转移到编译期生成。

### 课程笔记

高性能日志这一节聚焦于在 .NET 中优化日志执行的两种主要策略。

第一种策略涉及 `LoggerMessage.Define` 方法。
这个工具允许创建一个专门为特定日志场景构建的编译期 action。
通过预先定义参数和消息模板,应用程序避免了每次生成日志条目时解析字符串模板的运行时开销。

```csharp
private static readonly Action<ILogger, string, decimal, int, Exception?> _logPayment =
    LoggerMessage.Define<string, decimal, int>(
        LogLevel.Information,
        new EventId(5001, nameof(CreatePayment)),
        "Customer {Email} purchased product {ProductId} at {Amount}"
    );
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121712/?t=5)

第二种策略是 `LoggerMessage` 源生成器。
这种现代方法利用 C# 源生成器自动构建整个日志实现。
通过用 `[LoggerMessage]` 特性修饰一个分部方法,生成器会产生高效的代码,包括使用结构体来尽量减少堆分配。

```csharp
using Microsoft.Extensions.Logging;

namespace BasicConsoleApp;

public static partial class LoggerExtensions
{
    [LoggerMessage(Level = LogLevel.Information,
        EventId = 5001,
        Message = "Customer {Email} purchased product {ProductId} at {Amount}")]
    public static partial void LogPaymentCreation(
        this ILogger logger, string email, decimal amount, int productId);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121712/?t=17)

随后可以在服务中直接调用这个源生成的方法,从而提供一个简洁且高性能的日志 API。

```csharp
public void CreatePayment(string email, decimal amount, int productId)
{
    // Do some work
    _logger.LogPaymentCreation(email, amount, productId);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121712/?t=17)

本课最后过渡到课程的最后一节,该节聚焦于监控日志。
