# Level 1: Naive

> 课程:[From Zero to Hero: 1 Billion Row Performance Challenge in .NET](https://dometrain.com/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet/) · 第 3 章
> 共 3 课 · 约 37:17
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [The Naive Approach](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/the-naive-approach-69958221/) | 15:03 | [↓](#1-the-naive-approach) |
| 2 | [Analyze App with Performance Profile](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/analyze-app-with-performance-profile-69958222/) | 11:57 | [↓](#2-analyze-app-with-performance-profile) |
| 3 | [Analyze Memory Allocations](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/analyze-memory-allocations-69958223/) | 10:17 | [↓](#3-analyze-memory-allocations) |

## 1. The Naive Approach

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/the-naive-approach-69958221/) · 15:03

Naive 方案代表了标准 C# 开发者在面对数据处理任务时的第一直觉:使用 LINQ 和高层文件 API。
它对小数据集确实有效,但本课会演示,当行数向十亿逼近时,`File.ReadAllLines`、字符串切分以及匿名对象分配是如何造成显著的内存压力和性能瓶颈的。

### 核心概念

* 使用 `File.ReadAllLines` 进行高层文件 I/O。
* 使用 LINQ `Select` 和 `string.Split` 做数据转换。
* 使用 `GroupBy`、`Min`、`Max` 和 `Average` 做聚合。
* 匿名对象和字符串分配带来的内存开销。
* 垃圾回收(GC)对性能的影响。

### 课程笔记

实现从标准的样板代码开始,以确保性能测量在一个干净的环境中进行。
这包括校验数据文件是否存在,以及强制执行一次完整的垃圾回收来清除先前操作残留的内存,从而保证测量是从一个干净的状态开始的。

```csharp
using Shared;
using System.Diagnostics;
using System.Text;

Console.WriteLine("=== Level 1: Naive (LINQ) Implementation ===");
Console.WriteLine($"File: {GlobalConstants.FilePath}");
Console.WriteLine();

// Verify file exists
if (!File.Exists(GlobalConstants.FilePath))
{
    Console.WriteLine($"ERROR: File not found at {GlobalConstants.FilePath}");
    Console.WriteLine("Please create a test file or update GlobalConstants.FilePath");
    return;
}

// Force garbage collection before measurement for accurate timing
GC.Collect();
GC.WaitForPendingFinalizers();
GC.Collect();

var stopwatch = Stopwatch.StartNew();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/the-naive-approach-69958221/?t=55)

朴素处理逻辑的第一步是把整个文件读入内存。
使用 `File.ReadAllLines` 是最简单的做法,但它会立刻把全部内容加载为一个字符串数组,对大文件来说极其消耗内存。

```csharp
// Bring the file content with all the lines
string[] lines = File.ReadAllLines(GlobalConstants.FilePath, Encoding.UTF8);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/the-naive-approach-69958221/?t=145)

行加载完成后,应用使用 LINQ 把原始字符串转换成结构化数据。
每一行按分号切分,以分离气象站名称和温度值。
这个过程涉及多次分配:一个用于存放切分结果的新字符串数组,以及为每一行创建的一个新匿名对象。

```csharp
var results = lines.Select(line =>
{
    // Create an array (new allocation) with [size: 2]
    var parts = line.Split(';');

    var stationName = parts[0]; // no allocation just assignment
    double temperature = double.Parse(parts[1]); // no allocation

    return new // new allocation - object
    {
        Station = stationName,
        Temperature = temperature
    };
})
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/the-naive-approach-69958221/?t=165)

解析之后,数据按气象站名称分组。
对每个分组,使用标准的 LINQ 聚合方法计算最低温度、最高温度和平均温度。
最后,结果按气象站名称的字母顺序排序,并物化成一个列表。

```csharp
.GroupBy(x => x.Station) // after this point, we'll have 413 rows, iteration cost
.Select(g => new // new allocation - object
{
    Station = g.Key,
    Min = g.Min(x => x.Temperature), // iteration cost
    Mean = g.Average(x => x.Temperature),
    Max = g.Max(x => x.Temperature)
})
.OrderBy(o => o.Station) // 413 rows to order
.ToList(); // new allocation - List<AnonymousObject>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/the-naive-approach-69958221/?t=295)

最后一步是停止秒表,按挑战要求格式化输出字符串,并使用共享的 `ResultLogger` 工具记录性能指标。

```csharp
stopwatch.Stop();

var output = "{" + string.Join(", ",
    results.Select(r => $"{r.Station}={r.Min:F1}/{r.Mean:F1}/{r.Max:F1}")) +
    "}";

Console.WriteLine(output);

Console.WriteLine();
Console.WriteLine($"Processed {lines.Length:N0} rows");
Console.WriteLine($"Found {results.Count} unique stations");
Console.WriteLine($"Elapsed: {stopwatch.Elapsed}");

// Save results to file
ResultLogger.SaveResult(
    projectName: "Level01_Naive",
    output: output,
    elapsed: stopwatch.Elapsed,
    rowCount: lines.Length,
    stationCount: results.Count);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/the-naive-approach-69958221/?t=430)

#### 性能分析

用不同规模的数据集测试这个方案,会暴露出内存使用上一个显著的伸缩性问题:

* **10,000 行**:约 21ms 处理完成。对一个 134KB 的文件,工作集达到 35MB。
* **1 million 行**:约 846ms 处理完成。对一个 13.1MB 的文件,工作集达到 187MB。
* **10 million 行**:约 5.6 秒处理完成。对一个 131MB 的文件,工作集达到 1.6GB。

内存占用大致是输入文件大小的 10 到 12 倍。
这主要源于 `string` 对象、`string.Split` 数组以及匿名对象分配的开销。
在十亿行(约 13GB 文件)的规模下,这个方案将需要大约 160GB 内存,并触发过多的垃圾回收(GC)周期,使它对本挑战而言不可行。

## 2. Analyze App with Performance Profile

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/analyze-app-with-performance-profile-69958222/) · 11:57

### 总结

本课演示如何使用 Visual Studio Performance Profiler 来诊断 .NET 应用中的内存和性能瓶颈。
通过聚焦于 .NET Object Allocation Tracking 工具,本课揭示了朴素 LINQ 方案的隐藏成本,具体来说就是为字符串、数组和匿名类型进行的海量分配,它们触发了频繁而漫长的垃圾回收停顿。

### 核心概念

*   **Visual Studio Performance Profiler**:一套用于测量 CPU、内存和 I/O 性能的工具集。
*   **Object Allocation Tracking**:一种特定的性能分析模式,它跟踪堆上创建的每一个对象,并提供一棵分配树。
*   **Garbage Collection (GC) Pauses**:.NET 运行时回收内存期间应用执行被挂起的时间。
*   **Allocation Overhead**:在高吞吐数据处理过程中创建临时对象(例如字符串和数组)所累积的成本。

### 课程笔记

十亿行挑战的朴素实现使用标准 LINQ 方法处理数据。
虽然这种写法可读性好、功能上也正确,但它在内存使用方面非常低效。

```csharp
var results = lines.Select(line => // iteration cost
{
    // Create an array (new allocation) with [size: 2]
    var parts = line.Split(';');

    var stationName = parts[0]; // no allocation just assignment
    double temperature = double.Parse(parts[1]); // no allocation

    return new // new allocation - object
    {
        Station = stationName,
        Temperature = temperature
    };
})
.GroupBy(x => x.Station) // after this point, we'll have 413 rows, iteration cost
.Select(g => new // new allocation - object
{
    Station = g.Key,
    Min = g.Min(x => x.Temperature), // iteration cost
    Mean = g.Average(x => x.Temperature),
    Max = g.Max(x => x.Temperature)
})
.OrderBy(o => o.Station) // 413 rows to order
.ToList(); // new allocation - List<AnonymousObject>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/analyze-app-with-performance-profile-69958222/?t=55)

为了理解性能特征,Visual Studio Performance Profiler(位于 Debug 菜单下)提供了若干诊断工具。
就本次分析而言,**.NET Object Allocation Tracking** 工具是最相关的,因为它会跟踪在内存堆上创建的每一个对象。

在一个 100 万行的数据集上运行 profiler 时,应用产生了数量可观的存活对象。
尽管输入文件只包含 100 万行,profiler 却显示执行期间创建了超过 300 万个存活对象。

```csharp
var parts = line.Split(';');

var stationName = parts[0];
double temperature = double.Parse(parts[1]);

return new
{
    Station = stationName,
    Temperature = temperature
};
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/analyze-app-with-performance-profile-69958222/?t=490)

profiler 中的 "Allocations" 标签页按类型拆解了这些对象:
1.  **Strings**:创建了超过 300 万个字符串。对每一行,`line.Split(';')` 都会为气象站名称和温度值创建新的字符串实例。
2.  **String Arrays**:分配了超过 100 万个字符串数组,因为 `Split` 会为处理的每一行返回一个新数组。
3.  **Anonymous Types**:创建了 100 万个匿名类型 `{ Station, Temperature }` 的实例,用于在分组之前保存解析出的数据。

profiler 界面把分配归类到 Pin Object Heap、Large Object Heap (LOH) 和 Small Object Heap (SOH)。
它还提供 **Call Tree** 和 **Function Tree**,用于定位是哪些具体方法造成了最多的分配。
在本例中,`System.Linq` 和 `System.IO` 中的方法尤为突出。

如此大量的分配给 .NET 垃圾回收器带来了巨大压力。
profiler 会显示 "Pause Durations",也就是 GC 停止应用线程以回收内存的那些时段。
在某些情况下,这些停顿可以持续数百毫秒。
虽然应用看上去跑得很快,但它执行时间中相当可观的一部分花在了管理内存上,而不是处理数据。

## 3. Analyze Memory Allocations

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/analyze-memory-allocations-69958223/) · 10:17

### 总结

本课分析十亿行挑战中朴素 LINQ 实现的内存分配画像。
它探讨 `File.ReadAllLines`、`string.Split` 和匿名对象创建这类标准 .NET 操作是如何导致巨大的内存压力和频繁的垃圾回收周期的。
通过理解字符串头、数组分配和 LINQ 迭代的隐藏成本,开发者可以看清为什么一个简单的函数式写法无法伸缩到海量数据集。

### 核心概念

* **Memory Overhead of .NET Strings**:内存中的字符串是 UTF-16(每字符 2 字节),并且还包含头部和空终止符带来的额外开销。
* **Allocation Costs of string.Split**:对每一行调用 `Split`,都会为数据集中的每一行创建一个新的字符串数组和多个字符串对象。
* **Anonymous Object Pressure**:使用 LINQ `Select` 为每一行创建匿名对象,会产生数以百万计的堆分配,而垃圾回收器(GC)最终必须处理它们。
* **Iteration Costs**:多个 LINQ 操作(`Select`、`GroupBy`、`Min`、`Average`、`Max`)会带来 CPU 迭代成本,以及为分组和列表化而产生的额外对象分配。
* **Garbage Collection Impact**:高分配速率会触发频繁的 GC 周期,它们会暂停应用线程并显著降低性能。

### 课程笔记

要理解朴素实现的性能,我们必须考察 .NET 是如何为字符串和对象管理内存的。
把文件加载进内存时,磁盘上的大小并不等于托管堆中的大小。
例如,一个 13.1MB 的文件在磁盘上以 UTF-8 存储,但 .NET 字符串是 UTF-16,意味着每个字符通常占用两个字节。
此外,每个字符串对象都包含一个字符串头,其中有一个内存指针、字符串长度和一个空终止符。
这意味着一个已加载文件的内存占用可能是其磁盘大小的两倍甚至三倍。

```csharp
// Force garbage collection before measurement for accurate timing
GC.Collect();
GC.WaitForPendingFinalizers();
GC.Collect();

var stopwatch = Stopwatch.StartNew();

// Bring the file content with all the lines // 13.1MB
string[] lines = File.ReadAllLines(GlobalConstants.FilePath, Encoding.UTF8);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/analyze-memory-allocations-69958223/?t=40)

朴素方案的首要瓶颈,是 LINQ `Select` 转换内部发生的分配数量之庞大。
对文件中的每一行(本次测试是一百万行,而完整挑战中可能是十亿行),代码都执行了几项开销很大的操作:

1.  **string.Split**:它为每一行创建一个新的字符串数组。如果有一百万行,这就带来一百万次新的数组分配。
2.  **Anonymous Object Creation**:`Select` 代码块为每一行返回一个新的匿名对象。这又是一百万次堆分配,垃圾回收器必须跟踪它们并最终回收。

```csharp
var results = lines.Select(line => // iteration cost
{
    // Create an array (new allocation)
    var parts = line.Split(';');

    var stationName = parts[0]; // no allocation just assignment
    double temperature = double.Parse(parts[1]); // no allocation

    return new // new allocation - object
    {
        Station = stationName,
        Temperature = temperature
    };
})
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/analyze-memory-allocations-69958223/?t=205)

虽然 `stationName = parts[0]` 只是一次引用赋值(没有新分配),而 `double.Parse` 操作发生在栈上、效率很高,但包装这些值的匿名对象却是一次堆分配。

在初次转换之后,代码按气象站名称对数据进行分组。
在这个数据集中,一百万行被归并为 413 个不重复的气象站。
尽管行数现在变小了,`GroupBy` 操作以及随后用于计算统计值的 `Select` 仍然涉及迭代成本和额外对象的创建。

```csharp
.GroupBy(x => x.Station) // after this point, we'll have 413 rows, iteration cost
.Select(g => new // new allocation - object
{
    Station = g.Key,
    Min = g.Min(x => x.Temperature), // iteration cost
    Mean = g.Average(x => x.Temperature),
    Max = g.Max(x => x.Temperature)
})
.OrderBy(o => o.Station) // 413 rows to order
.ToList(); // new allocation - List<AnonymousObject>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/analyze-memory-allocations-69958223/?t=415)

这些分配的累积效应非常严重。
仅仅一次作业,我们就创建了数以百万计的对象。
当扩展到十亿行时,应用会把相当大一部分时间花在垃圾回收上。
每次 GC 运行时,它都可能暂停应用线程,导致处理变慢甚至完全停滞。
为了提升性能,后续迭代的目标将是避免这些不必要的分配,以更高效的方式处理数据,而不为每一行创建中间数组或对象。

---

## 运行 Demo

Demo 代码按课程官方仓库 [Dometrain/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet](https://github.com/Dometrain/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet) 的方式组织,本章对应上游的 `Level1_Naive/`:

```
src/1brc/
├── notes/            本章及后续各章笔记
├── Shared/           SharedTypes.cs:GlobalConstants、ResultLogger
├── DataGenerator/    Program.cs:413 个气象站 + Box-Muller 生成器
└── Level1_Naive/     Program.cs:本章的 LINQ 朴素实现
```

先用 DataGenerator 生成测量文件,再跑 Level1_Naive:

```bash
cd src/1brc
dotnet run --project DataGenerator -c Release -- 1_000_000
dotnet run --project Level1_Naive -c Release
```

`Level1_Naive/Program.cs` 与课程代码逐行一致,只多了一处 `#pragma warning disable CA1305`:
仓库开启了 `EnforceCodeStyleInBuild`,而课程原样的 `double.Parse(parts[1])` 没有传 `IFormatProvider`。
pragma 保留了课程的写法,同时让构建维持零警告。

下面是 12 核机器上的真实输出。
`{...}` 那一行有一万多个字符(413 个气象站),这里只保留开头,其余用 `…` 省略。

10,000 行:

```text
=== Level 1: Naive (LINQ) Implementation ===
File: C:\Users\iantu\AppData\Local\Temp\1brc\Files\measurements.txt

{Abéché=18.6/30.5/54.4, Abha=-14.0/15.1/38.2, Abidjan=13.0/28.0/43.3, Accra=1.9/24.3/41.5, Addis Ababa=-10.4/12.1/30.5, …}

Processed 10,000 rows
Found 413 unique stations
Elapsed: 00:00:00.0210771

📁 Results saved: C:\Users\iantu\AppData\Local\Temp\1brc\Files\results.log
```

1,000,000 行:

```text
=== Level 1: Naive (LINQ) Implementation ===
File: C:\Users\iantu\AppData\Local\Temp\1brc\Files\measurements.txt

{Abéché=-4.5/29.4/62.4, Abha=-19.6/18.1/55.6, Abidjan=-7.7/25.6/58.2, Accra=-7.6/26.4/60.7, Addis Ababa=-23.2/16.2/56.4, …}

Processed 1,000,000 rows
Found 413 unique stations
Elapsed: 00:00:00.7882751

📁 Results saved: C:\Users\iantu\AppData\Local\Temp\1brc\Files\results.log
```

10,000,000 行:

```text
=== Level 1: Naive (LINQ) Implementation ===
File: C:\Users\iantu\AppData\Local\Temp\1brc\Files\measurements.txt

{Abéché=-9.7/29.5/68.2, Abha=-22.0/17.9/57.6, Abidjan=-12.7/26.0/64.1, Accra=-13.0/26.4/66.1, Addis Ababa=-27.8/16.0/56.0, …}

Processed 10,000,000 rows
Found 413 unique stations
Elapsed: 00:00:07.0256538

📁 Results saved: C:\Users\iantu\AppData\Local\Temp\1brc\Files\results.log
```

`ResultLogger` 写出的 `results.log` 记下了这三次运行的内存数字,也就是第 1 课性能分析那一段讨论的东西(结果行同样省略):

```text
================================================================================
[2026-08-26 21:53:33] Level01_Naive
================================================================================
Performance:
  Rows:               10,000
  Stations:           413
  Elapsed:            00:00:00.0210771
  Throughput:         474,449 rows/sec (11.31 MB/sec)

Memory:
  Working Set:        26 MB
  GC Memory:          2 MB
  Gen0 Collections:   2
  Gen1 Collections:   2
  Gen2 Collections:   2

Processor:
  CPU Cores:          12
--------------------------------------------------------------------------------

================================================================================
[2026-08-26 21:53:36] Level01_Naive
================================================================================
Performance:
  Rows:               1,000,000
  Stations:           413
  Elapsed:            00:00:00.7882751
  Throughput:         1,268,593 rows/sec (30.25 MB/sec)

Memory:
  Working Set:        193 MB
  GC Memory:          147 MB
  Gen0 Collections:   41
  Gen1 Collections:   23
  Gen2 Collections:   7

Processor:
  CPU Cores:          12
--------------------------------------------------------------------------------

================================================================================
[2026-08-26 21:53:47] Level01_Naive
================================================================================
Performance:
  Rows:               10,000,000
  Stations:           413
  Elapsed:            00:00:07.0256538
  Throughput:         1,423,355 rows/sec (33.94 MB/sec)

Memory:
  Working Set:        1,421 MB
  GC Memory:          1,359 MB
  Gen0 Collections:   331
  Gen1 Collections:   170
  Gen2 Collections:   12

Processor:
  CPU Cores:          12
--------------------------------------------------------------------------------
```
