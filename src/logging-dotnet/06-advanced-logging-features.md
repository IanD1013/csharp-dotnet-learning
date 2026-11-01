# Advanced Logging Features

> 课程:[From Zero to Hero: Logging in .NET](https://dometrain.com/course/from-zero-to-hero-logging-in-dotnet/) · 第 6 章
> 共 7 课 · 约 25:05
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Log message template formatting](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-message-template-formatting-54121665/) | 2:08 | [↓](#1-log-message-template-formatting) |
| 2 | [Dealing with complex objects](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/dealing-with-complex-objects-54121666/) | 2:55 | [↓](#2-dealing-with-complex-objects) |
| 3 | [Log scopes](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-scopes-54121668/) | 4:43 | [↓](#3-log-scopes) |
| 4 | [Checking for enabled log levels](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/checking-for-enabled-log-levels-54121669/) | 3:26 | [↓](#4-checking-for-enabled-log-levels) |
| 5 | [Timed log entries](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/timed-log-entries-54121670/) | 6:55 | [↓](#5-timed-log-entries) |
| 6 | [Changing the log level during runtime](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/changing-the-log-level-during-runtime-54121671/) | 3:51 | [↓](#6-changing-the-log-level-during-runtime) |
| 7 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121672/) | 1:07 | [↓](#7-section-recap) |

## 1. Log message template formatting

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-message-template-formatting-54121665/) · 2:08

### 总结

本课探讨 .NET 日志消息模板中一项高级功能:对参数进行格式化。
通过在占位符内使用冒号加格式说明符(例如 {Value:Format}),开发者可以借助标准的 .NET 格式字符串来控制数据在日志中的呈现方式。
本课演示了一些实际应用,例如把数字格式化为货币,以及以完整日期时间或通用可排序格式等不同模式显示日期,从而在无需手动处理字符串的情况下,确保日志输出既易读又在技术上精确。

### 核心概念

- **格式说明符(Format Specifiers)**:使用 `{ParameterName:Format}` 语法来应用 .NET 格式字符串。
- **货币格式化**:`:C` 说明符会根据系统的区域性(culture)自动把数字格式化为货币。
- **日期格式化**:支持 `:F`(完整日期/时间)和 `:u`(通用可排序)等标准模式。
- **区域性敏感**:记录的值会按照所配置的区域性进行格式化,这会影响符号和分隔符。

### 课程笔记

记录数据时,参数的默认字符串表示形式不一定总是够用。
例如,表示货币的数值或复杂的日期时间对象,往往需要特定的格式化才能更易读。
虽然可以手动进行字符串转换,但 .NET 日志框架提供了一种内置机制,可以直接在消息模板中处理这件事。

来看一个控制台应用程序中的标准日志场景:

```csharp
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
});

ILogger logger = loggerFactory.CreateLogger<Program>();

var paymentId = 1;
var amount = 15.99;

logger.LogInformation(
    "New Payment with id {PaymentId} for {Total}", paymentId, amount);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-message-template-formatting-54121665/?t=20)

要把 `amount` 格式化为货币,可以使用冒号参数语法。
在占位符中加上 `:C`,就是指示日志记录器把这个值当作货币类型处理。
注意,最终输出会包含与应用程序当前区域性相应的货币符号和格式。

```csharp
logger.LogInformation(
    "New Payment with id {PaymentId} for {Total:C}", paymentId, amount);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-message-template-formatting-54121665/?t=70)

这种语法用途非常广泛,支持各种 .NET 格式字符串,包括用于整数、百分比和 ISO 日期的格式字符串。
在处理 `DateTime` 对象时,它尤其强大。
例如,可以用 `:F` 指定完整日期时间格式,或用 `:u` 指定通用可排序格式。

```csharp
var date = DateTime.Now;

logger.LogInformation(
    "New Payment with id {PaymentId} for {Total:C} at {Date:F}", 
    paymentId, amount, date);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-message-template-formatting-54121665/?t=115)

通过使用这些内置格式化器,对于支持结构化数据的提供程序(例如 JSON 日志记录器),结构化数据保持完整,同时渲染出的文本输出也会被正确格式化,便于人阅读。

## 2. Dealing with complex objects

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/dealing-with-complex-objects-54121666/) · 2:55

### 总结

在 .NET 中记录日志时,把复杂对象直接传给日志记录器方法,往往会导致日志记录器调用该对象的 ToString() 方法,而该方法默认只输出类型名称。
为了捕获有意义的数据,开发者可以重写 ToString()、使用 C# record 类型来获得自动格式化,或者用 JsonSerializer.Serialize 把对象序列化为 JSON。
虽然 JSON 序列化提供了便于在日志提供程序中查询的结构化数据,但在使用 JSON 控制台格式化器时必须小心,避免出现"JSON 套 JSON"的嵌套问题。

### 核心概念

* 默认的 ToString() 行为:标准类除非被重写,否则只记录其类型名称。
* 重写 ToString():自定义对象的字符串表示形式,以提高日志的可读性。
* C# Records:使用 record 类型,利用其内置的、感知属性的 ToString() 实现。
* JSON 序列化:把对象转换为 JSON 字符串,以在日志输出中保留其结构。
* 格式化器兼容性:处理已序列化的参数与 AddJsonConsole 等结构化日志格式化器之间的相互作用。

### 课程笔记

在许多场景下,与其向日志记录器传入多个单独的参数,不如传入一个复杂对象。
然而,直接把一个对象实例传入日志消息模板,可能会导致意想不到的结果。

```csharp
ILogger logger = loggerFactory.CreateLogger<Program>();

var paymentData = new PaymentData
{
    PaymentId = 1,
    Amount = 15.99m
};

logger.LogInformation(
    "New Payment with data {PaymentData}", paymentData);

class PaymentData
{
    public int PaymentId { get; set; }
    public decimal Amount { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/dealing-with-complex-objects-54121666/?t=40)

执行这段代码时,输出并不会显示对象的属性。
相反,它显示的是类型的名称:

```text
info: Program[0]
      New Payment with data PaymentData
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/dealing-with-complex-objects-54121666/?t=55)

出现这种情况,是因为日志框架调用了该对象的 `ToString()` 方法。
默认情况下,`System.Object.ToString()` 返回类型的完全限定名称。
要解决这个问题,可以在类中重写 `ToString()` 方法,以提供更具描述性的字符串。

```csharp
logger.LogInformation(
    "New Payment with data {PaymentData}", paymentData);

class PaymentData
{
    public int PaymentId { get; set; }

    public decimal Amount { get; set; }

    public override string ToString()
    {
        return $"PaymentId: {PaymentId}, Amount: {Amount}";
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/dealing-with-complex-objects-54121666/?t=95)

另外,现代 C# 提供了 `record` 类型。
Record 会自动实现一个 `ToString()` 方法,以简洁易读的格式打印出各个属性及其值。
这样既不需要手动重写,又仍然能提供有用的日志输出。

如果需要确保数据以外部日志提供程序可以查询的结构化格式被捕获(例如搜索特定的嵌套参数),可以把对象序列化为 JSON。

```csharp
using System.Text.Json;

ILogger logger = loggerFactory.CreateLogger<Program>();

var paymentData = new PaymentData
{
    PaymentId = 1,
    Amount = 15.99m
};

logger.LogInformation(
    "New Payment with data {PaymentData}", JsonSerializer.Serialize(paymentData));

class PaymentData
{
    public int PaymentId { get; set; }
    public decimal Amount { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/dealing-with-complex-objects-54121666/?t=130)

使用 JSON 序列化时,要留意你的日志配置。
如果已经通过 `AddJsonConsole()` 把控制台日志记录器配置为使用 JSON 格式,那么手动序列化参数会导致一个 JSON 字符串嵌套在外层的 JSON 日志条目之中。

```csharp
using System.Text.Json;
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddJsonConsole(x =>
    {
        x.JsonWriterOptions = new JsonWriterOptions
        {
            Indented = true
        };
    });
});

ILogger logger = loggerFactory.CreateLogger<Program>();

var paymentData = new PaymentData
{
    PaymentId = 1,
    Amount = 15.99m
};

logger.LogInformation(
    "New Payment with data {PaymentData}", JsonSerializer.Serialize(paymentData));

class PaymentData
{
    public int PaymentId { get; set; }
    public decimal Amount { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/dealing-with-complex-objects-54121666/?t=170)

## 3. Log scopes

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-scopes-54121668/) · 4:43

### 总结

.NET 中的日志作用域(log scope)提供了一种机制,可以把交易 ID 或请求详情等公共上下文数据附加到某个特定逻辑操作中产生的所有日志条目上。
通过把代码包裹在一个作用域中,开发者既可以避免在每次日志调用中重复传递参数,又能确保下游的日志提供程序能够捕获并关联这些共享状态。

### 核心概念

- **上下文分组**:作用域让多个日志条目共享公共数据(例如 PaymentId、OrderId),而无需把这些参数显式传给每一个日志方法。
- **提供程序支持**:作用域必须得到日志提供程序的支持并被启用;例如,JSON 控制台格式化器需要设置 `IncludeScopes = true`。
- **IDisposable 模式**:`ILogger.BeginScope` 返回一个 `IDisposable` 对象,因此非常适合配合 `using` 块来定义作用域的生命周期。
- **叠加性**:嵌套的作用域是累积的;位于多个作用域内的日志条目会包含所有处于活动状态的父作用域的数据。
- **结构化日志**:作用域在结构化日志(如 JSON)中尤其强大,在那里它们会以一个包含作用域状态的独立对象数组的形式出现。

### 课程笔记

当一段代码从逻辑上的开始到结束都在处理某个特定上下文时(例如一笔支付、一个订单,甚至一个特定的 HTTP 请求),就会用到日志作用域。
作用域让这些数据可以在该代码块内发出的所有日志之间共享,而不必手动把同样的上下文数据添加到每一条日志消息中。

#### 启用作用域

要使用作用域,日志提供程序必须支持它们。
标准的 Console 提供程序支持作用域,但默认是禁用的。
使用 JSON 控制台格式化器时,必须在选项中显式启用它们。

```csharp
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddJsonConsole(x =>
    {
        x.IncludeScopes = true;
    });
});

ILogger logger = loggerFactory.CreateLogger<Program>();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-scopes-54121668/?t=55)

如果没有启用作用域,生成的日志输出会包含消息和状态,但缺少 `Scopes` 元数据。

#### 实现日志作用域

要创建作用域,请调用 `logger.BeginScope`。
该方法接受一个消息格式和参数(与日志方法类似),或者一个状态对象。
由于它返回一个 `IDisposable`,通常会在 `using` 语句中使用它,以确保操作完成时作用域被释放。

```csharp
var paymentId = 1;
var amount = 15.99;

using (logger.BeginScope("{PaymentId}", paymentId))
{
    try
    {
        logger.LogInformation("New payment for ${Total}", amount);
        // Simulated processing...
        logger.LogInformation("Payment processing completed");
    }
    finally
    {
        // The scope ends when the using block is exited
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-scopes-54121668/?t=140)

在上面的示例中,`PaymentId` 并没有显式传给 `LogInformation` 调用,但由于这两条日志条目都发生在该作用域内,它会与这两条条目都关联起来。

```text
using (logger.BeginScope("{PaymentId}", paymentId))
+-----------------------------------------------------------+
| LogInformation("New payment for ${Total}", amount)        |
|   -> 携带 PaymentId                                       |
| LogInformation("Payment processing completed")            |
|   -> 携带 PaymentId                                       |
+-----------------------------------------------------------+
退出 using 块 -> 作用域结束
```

#### 带作用域的结构化输出

当日志被格式化为 JSON 时,作用域数据会被包含在一个 `Scopes` 数组中。
为了让这种输出在开发期间更易读,可以配置 `JsonWriterOptions` 来缩进输出。

```csharp
using System.Text.Json;
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddJsonConsole(x =>
    {
        x.IncludeScopes = true;
        x.JsonWriterOptions = new JsonWriterOptions
        {
            Indented = true
        };
    });
});
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-scopes-54121668/?t=190)

生成的 JSON 日志条目会包含作用域参数:

```json
{
  "LogLevel": "Information",
  "Message": "New payment for $15.99",
  "State": {
    "Message": "New payment for $15.99",
    "Total": 15.99,
    "{OriginalFormat}": "New payment for ${Total}"
  },
  "Scopes": [
    {
      "Message": "1",
      "PaymentId": 1,
      "{OriginalFormat}": "{PaymentId}"
    }
  ]
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-scopes-54121668/?t=205)

#### 嵌套与多个作用域

作用域是可叠加的。
如果在另一个作用域已处于活动状态时开启一个新的作用域,日志条目会同时携带这两个作用域的上下文。

```csharp
var paymentId = 1;
var amount = 15.99;

using (logger.BeginScope("{PaymentId}", paymentId))
using (logger.BeginScope("{TotalAmount}", amount))
{
    logger.LogInformation("Processing payment...");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/log-scopes-54121668/?t=250)

在这种情况下,输出中的 `Scopes` 数组会包含两个对象:一个对应 `PaymentId`,一个对应 `TotalAmount`。

```text
BeginScope("{PaymentId}", paymentId)
  +-- BeginScope("{TotalAmount}", amount)
        +-- LogInformation("Processing payment...")

"Scopes": [ { PaymentId }, { TotalAmount } ]
```

## 4. Checking for enabled log levels

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/checking-for-enabled-log-levels-54121669/) · 3:26

### 总结

ILogger.IsEnabled 方法提供了一种机制,可以在执行日志操作之前确认某个特定日志级别是否处于活动状态。
通过把日志调用包裹在这个检查中,开发者可以避免结构化日志带来的性能开销,例如值类型(如整数和小数)的装箱,以及为消息参数分配对象数组。
尽管日志提供程序会在内部执行这些检查,但在性能关键路径上,或者在准备日志数据需要大量计算时,在调用处手动检查是一种最佳实践。

### 核心概念

- **ILogger.IsEnabled(LogLevel)**:用于判断日志记录器是否被配置为捕获某个特定级别日志的方法。
- **性能优化**:通过跳过已禁用级别的日志逻辑来减少 CPU 和内存开销。
- **装箱与分配**:把值类型转换为引用类型以及为 params 参数创建数组的过程,即使日志被过滤掉,这些也会发生。
- **短路**:提前退出日志路径以节省资源。
- **提供程序内部逻辑**:Console 提供程序等日志提供程序如何处理级别过滤。

### 课程笔记

每个实现了 ILogger 接口的日志记录器都可以使用 IsEnabled 方法。
该方法允许你传入一个 LogLevel,并根据应用程序的配置得到一个布尔值,表示该级别当前是否启用。

```csharp
ILogger logger = loggerFactory.CreateLogger<Program>();

var paymentId = 1;
var amount = 15.99;

if (logger.IsEnabled(LogLevel.Information))
{
    logger.LogInformation(
        "New Payment with id {PaymentId} for ${Total}", paymentId, amount);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/checking-for-enabled-log-levels-54121669/?t=30)

如果日志记录器配置的最低级别高于被检查的级别,该方法会返回 false,日志代码块将被完全跳过。
例如,如果最低级别设置为 Warning,那么对 Information 级别的检查会阻止该日志被处理。

```csharp
using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddJsonConsole(x =>
    {
        x.JsonWriterOptions = new JsonWriterOptions
        {                Indented = true
        };
    });
    builder.SetMinimumLevel(LogLevel.Warning);
});
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/checking-for-enabled-log-levels-54121669/?t=55)

#### 为什么手动检查很重要

你可能会以为手动检查是多余的,因为日志提供程序已经在内部执行了这些检查。
虽然一个实现良好的提供程序确实会在处理之前检查级别,但等到提供程序的 Log 方法被调用时,开销已经发生了。

```csharp
public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
{
    if (!IsEnabled(logLevel))
    {
        return;
    }
    // ... further processing ...
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/checking-for-enabled-log-levels-54121669/?t=115)

当你调用 LogInformation 这样的方法时,编译器会使用一个接受 params object[] 数组作为消息参数的重载。
如果你传入值类型,例如 int 类型的 paymentId 或 double 类型的 amount,会发生两件事:

1. 在堆上分配一个新的 object[] 数组。
2. 值类型被**装箱**,也就是从值类型转换为引用类型,以便能存放在该 object 数组中。

这些分配发生在日志记录器能够检查级别是否启用之前。
在高吞吐量的应用程序中,这会导致更大的内存压力和更频繁的垃圾回收。
把调用包裹在 IsEnabled 中,就可以完全避免这些分配。

```text
不检查 IsEnabled:
  LogInformation(..., paymentId, amount)
    -> 分配 object[]
    -> 装箱 paymentId (int), amount (double)
    -> 提供程序 Log(...)
         -> if (!IsEnabled(logLevel)) return;   // 开销已经发生

先检查 IsEnabled:
  if (logger.IsEnabled(LogLevel.Information))   // false -> 跳过
    -> 不分配 object[],不装箱
```

虽然不需要把每条日志语句都包裹在 IsEnabled 检查中,但对于以下情况,强烈推荐这样做:
- 高频的日志语句。
- 涉及大量数据处理或复杂字符串转换的日志。
- 以尽量减少分配为优先事项的性能关键路径。

## 5. Timed log entries

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/timed-log-entries-54121670/) · 6:55

### 总结

本课演示如何利用 IDisposable 模式和 using 语句在 .NET 中创建计时日志条目。
通过把操作包裹在一个自定义的 TimedOperation 作用域中,开发者可以使用高精度的 Stopwatch API 自动测量执行耗时,并把这些数据追加到结构化日志中,而不会丢失原有的消息参数。
这种技巧提供了一种优雅、可复用的方式来监控性能,并把计时指标推送到 Application Insights 等日志提供程序中。

### 核心概念

*   **IDisposable 降级(Lowering)**:`using` 语句会被编译成一个 `try/finally` 块,确保即使发生异常也会调用 `Dispose` 方法。
*   **高精度计时**:使用 `Stopwatch.GetTimestamp()` 和 `Stopwatch.GetElapsedTime()` 可以在 .NET 中得到最精确的耗时测量。
*   **结构化日志状态**:动态扩展参数数组,可以在保留原有日志参数的同时添加计时元数据(例如 `OperationDurationMs`)。
*   **扩展方法**:用 `BeginTimedOperation` 增强 `ILogger` 接口,可以为开发者提供一个简洁、易于发现的 API。

### 课程笔记

测量一个操作的耗时并把该耗时包含在日志中,是一个常见的需求。
借助 C# 的 `using` 语句,可以优雅地实现这一点。
在幕后,`using` 语句会被降级为一个 `try/finally` 块。
当退出 `using` 块的作用域时,目标对象的 `Dispose` 方法会在 `finally` 块中被调用。
这种行为让我们可以在对象创建时记录开始时间,并在对象被释放时计算经过的时间。

```text
using (var op = new TimedOperation(...))     try
{                                             {
    // 操作                         ==>           // 操作
}                                             }
                                              finally
                                              {
                                                  op.Dispose();
                                              }

构造函数: 记录开始时间        Dispose: 计算经过的时间
```

为了实现这一点,我们创建一个实现了 `IDisposable` 的 `TimedOperation` 类。

```csharp
namespace BasicConsoleApp;

public class TimedOperation : IDisposable
{
    public void Dispose()
    {

    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/timed-log-entries-54121670/?t=70)

该实现需要保存日志记录器、所需的日志级别、消息模板,以及传给日志的所有参数。
为了在最终的日志条目中包含耗时,我们必须把参数数组扩展一位,以容纳计时值。
我们在构造函数中使用 `Stopwatch.GetTimestamp()` 来高精度地记录开始时间。

```csharp
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace BasicConsoleApp;

public class TimedOperation : IDisposable
{
    private readonly ILogger _logger;
    private readonly LogLevel _logLevel;
    private readonly string _messageTemplate;
    private readonly object?[] _args;
    private readonly long _startingTimestamp;

    public TimedOperation(ILogger logger, LogLevel logLevel, string messageTemplate, object?[] args)
    {
        _logger = logger;
        _logLevel = logLevel;
        _messageTemplate = messageTemplate;
        _args = new object[args.Length + 1];
        Array.Copy(args, _args, args.Length);
        _startingTimestamp = Stopwatch.GetTimestamp();
    }

    public void Dispose()
    {
        var delta = Stopwatch.GetElapsedTime(_startingTimestamp);
        _args[^1] = delta.TotalMilliseconds;
        _logger.Log(_logLevel, "{_messageTemplate} completed in {OperationDurationMs}ms", _args);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/timed-log-entries-54121670/?t=235)

```text
args:   [ paymentId ]
_args:  [ paymentId | (空位) ]          args.Length + 1, Array.Copy
Dispose 之后:
_args:  [ paymentId | delta.TotalMilliseconds ]   _args[^1]
                       -> {OperationDurationMs}
```

为了让这个功能易于使用,我们在 `ILogger` 接口上定义扩展方法。
这让开发者可以直接从日志记录器实例开始一个计时操作。

```csharp
using Microsoft.Extensions.Logging;

namespace BasicConsoleApp;

public static class TimedOperationExtensions
{
    public static IDisposable BeginTimedOperation(
        this ILogger logger, string messageTemplate, params object[] args)
    {
        return BeginTimedOperation(logger, LogLevel.Information, messageTemplate, args);
    }
    
    public static IDisposable BeginTimedOperation(
        this ILogger logger, LogLevel logLevel, string messageTemplate, params object[] args)
    {
        return new TimedOperation(logger, logLevel, messageTemplate, args);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/timed-log-entries-54121670/?t=295)

在应用程序代码中,使用计时操作的方式是把相关逻辑包裹在一个 `using` 块中。
块内产生的任何日志都保持独立,而最终的计时日志会在该块结束时发出。

```csharp
ILogger logger = loggerFactory.CreateLogger<Program>();

var paymentId = 1;
var amount = 15.99;

using (logger.BeginTimedOperation("Handling new payment {PaymentId}", paymentId))
{
    logger.LogInformation(
        "New Payment with id {PaymentId} for ${Total}", paymentId, amount);

    await Task.Delay(10);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/timed-log-entries-54121670/?t=345)

生成的日志输出(此处以 JSON 格式展示)既包含原有参数(如 `PaymentId`),也包含计算出的 `OperationDurationMs`。
这种结构化数据非常适合在 Application Insights 等工具中进行分析、制作仪表板和设置告警。

```json
{
  "EventId": 0,
  "LogLevel": "Information",
  "Category": "Program",
  "Message": "Handling new payment 1 completed in 26.8556ms",
  "State": {
    "Message": "Handling new payment 1 completed in 26.8556ms",
    "PaymentId": 1,
    "OperationDurationMs": 26.8556,
    "{OriginalFormat}": "Handling new payment {PaymentId} completed in {OperationDurationMs}ms"
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/timed-log-entries-54121670/?t=385)

## 6. Changing the log level during runtime

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/changing-the-log-level-during-runtime-54121671/) · 3:51

### 总结

本课演示如何在无需重启的情况下,动态调整正在运行的 .NET 应用程序的最低日志级别。
借助 ASP.NET Core 中内置的配置提供程序(它们支持对 appsettings.json 等设置文件进行热重载),开发者可以随时修改日志的详细程度。
这项能力对生产环境中的故障排查至关重要,它允许临时收集详细的 Information 级别日志,以调试那些未被更高级别的 Warning 或 Error 过滤器捕获的问题。

### 核心概念

*   **运行时配置重载**:.NET 配置提供程序监视源文件、并在不重启应用程序的情况下更新内存中设置的能力。
*   **默认生成器行为**:`WebApplication.CreateBuilder` 会自动为 `appsettings.json` 以及特定于环境的覆盖文件配置 `reloadOnChange: true` 的 JSON 配置提供程序。
*   **日志级别过滤**:日志系统如何在运行时查询配置提供程序,以确定是否应发出某条日志消息。
*   **生产环境调试**:在活跃事故期间使用动态日志级别来提高对应用程序状态的可见性。

### 课程笔记

在一个标准的控制台应用程序中,日志级别通常在启动时就固定下来了。
如果最低级别被设置为 `Warning`,那么任何以 `Information` 级别发出的日志都会被抑制,不会在控制台或其他提供程序中显示。

```csharp
using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddJsonConsole(x =>
    {
        x.JsonWriterOptions = new JsonWriterOptions
        {
            Indented = true
        };
    });
    builder.SetMinimumLevel(LogLevel.Warning);
});

ILogger logger = loggerFactory.CreateLogger<Program>();

var paymentId = 1;
var amount = 15.99;

while (true)
{
    logger.LogInformation(
        "New Payment with id {PaymentId} for ${Total}", paymentId, amount);
    await Task.Delay(1000);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/changing-the-log-level-during-runtime-54121671/?t=10)

要实现运行时修改日志级别,基于 ASP.NET Core 的应用程序更为有效,因为它的配置系统本身就是为适应动态更新而设计的。
通过创建一个 `BackgroundService`,我们可以模拟一个每秒记录一次信息的持续运行的过程。

```csharp
namespace BasicMinimalApi;

public class LogBackgroundService : BackgroundService
{
    private readonly ILogger<LogBackgroundService> _logger;

    public LogBackgroundService(ILogger<LogBackgroundService> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var paymentId = 1;
        var amount = 15.99;
        
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation(
                "New Payment with id {PaymentId} for ${Total}", paymentId, amount);
            await Task.Delay(1000, stoppingToken);
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/changing-the-log-level-during-runtime-54121671/?t=70)

该服务在 `Program.cs` 文件中注册到依赖注入容器里。
`WebApplication.CreateBuilder(args)` 方法承担了加载 `appsettings.json` 和 `appsettings.Development.json` 的繁重工作,同时默认为这些参数启用热重载。

```csharp
using BasicMinimalApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<LogBackgroundService>();

var app = builder.Build();

app.MapGet("/", (ILogger<Program> logger) =>
{
    return "Hello Log!";
});

app.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/changing-the-log-level-during-runtime-54121671/?t=140)

最初,`appsettings.json` 配置可能把默认日志级别设置为 `Warning`。
在这种状态下,后台服务输出的 `Information` 日志在控制台中是看不到的。

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/changing-the-log-level-during-runtime-54121671/?t=85)

当把 `appsettings.json` 中的 `Default` 日志级别改为 `Information` 并保存文件后,应用程序会立即检测到这一变化。
日志提供程序会在运行时被更新,后台服务的日志开始出现在控制台中,而无需重启应用程序。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/changing-the-log-level-during-runtime-54121671/?t=160)

```text
appsettings.json            JSON 配置提供程序          日志系统
"Default": "Warning"   -->  (reloadOnChange: true) --> Information 被抑制
        | 修改并保存
        v
"Default": "Information" -> 立即检测到变化        --> Information 开始输出
                                                       (无需重启)
```

这项功能严格来说并不是一个日志功能,而是日志系统与配置提供程序之间的协同作用。
这种机制适用于多种提供程序,包括 Azure App Configuration、AWS、Key Vault,或任何支持运行时重载的提供程序。
在生产环境中,当你可能需要临时降低日志级别、以收集更多数据来调试一个正在发生的问题时,这是一个强大的工具。

## 7. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121672/) · 1:07

### 总结

本课总结了 .NET 内置日志框架中可用的高级日志功能。
它回顾了自定义消息模板、通过序列化或 record 处理复杂对象,以及使用日志作用域把上下文数据附加到多条日志条目上的技巧。
本回顾还涵盖了实现计时操作以进行性能监控,以及利用配置热重载在运行时动态调整日志级别而无需重启应用程序。

### 核心概念

- 自定义消息模板,以自动格式化参数。
- 序列化复杂对象并使用 record 来获得详细的日志数据。
- 实现日志作用域,把通用上下文附加到日志消息上。
- 使用计时操作测量并记录执行耗时。
- 利用运行时配置重载,在不重启的情况下修改日志级别。

### 课程笔记

.NET 日志框架提供了若干高级能力,可以提升遥测数据的丰富程度和日志管理的灵活性。

#### 消息模板自定义

消息模板可以被自定义,把格式化逻辑直接包含在模板字符串中。
这一功能可以自动格式化参数,确保值以期望的格式被记录,而无需在日志调用之前手动处理字符串。

#### 记录复杂对象

要从复杂对象中捕获详细信息,可以通过序列化,或者利用 record 和 `ToString` 重写等 C# 特性来实现。
这确保了结构化日志包含关于对象状态的全面数据,这在使用支持结构化数据索引的日志提供程序时尤其有价值。

#### 日志作用域

日志作用域提供了一种机制,可以把共享上下文附加到某个特定逻辑块内产生的所有日志消息上。
通过开启一个作用域,开发者可以在多条日志条目中包含通用参数(例如关联 ID 或用户信息),而无需在每次日志调用中显式定义它们。
这减少了冗余,并确保一个流程中的所有日志都能被正确地关联起来。

#### 计时操作

可以使用计时操作,把对特定代码执行耗时的测量集成到日志工作流中。
这种技巧包括记录一个开始时间戳,并在任务完成时计算经过的时间,然后把它作为该操作元数据的一部分记录下来。
这提供了一种通过现有日志接收器(sink)监控应用程序性能的标准化方式。

#### 运行时配置与热重载

日志系统通过 .NET 配置提供程序支持配置设置的热重载。
这使得可以在运行时动态调整日志级别和过滤器。
例如,可以降低某个特定类别的日志级别,以在一次正在进行的排查中捕获更细粒度的细节,而无需重启应用程序。

#### 过渡到第三方库

虽然 .NET 内置日志记录器提供了一套强大的功能,但许多高级场景会受益于第三方库提供的额外能力。
Serilog 是 .NET 生态系统中最流行的日志库,在结构化日志和接收器(sink)管理方面提供了更多能力。
