# Level 2: Stream

> 课程:[From Zero to Hero: 1 Billion Row Performance Challenge in .NET](https://dometrain.com/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet/) · 第 4 章
> 共 4 课 · 约 32:29
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/introduction-69958224/) | 1:25 | [↓](#1-introduction) |
| 2 | [Let's Continue with Stream](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/let-s-continue-with-stream-69958225/) | 15:10 | [↓](#2-lets-continue-with-stream) |
| 3 | [Testing Stream Approach](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/testing-stream-approach-69958226/) | 9:52 | [↓](#3-testing-stream-approach) |
| 4 | [Homework: Disk Sector, Cluster, OS Page, BufferSize](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/homework-disk-sector-cluster-os-page-buffersize-69958227/) | 6:02 | [↓](#4-homework-disk-sector-cluster-os-page-buffersize) |

## 1. Introduction

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/introduction-69958224/) · 1:25

### 总结

本课介绍如何从朴素的、内存密集的方案过渡到基于流式处理的 1 Billion Row Challenge 解法。
把"把整个文件加载进内存"的做法替换成使用 StreamReader 的逐行处理模型之后,应用显著降低了内存压力和垃圾回收开销。
这种方案在任意给定时刻只把必要的数据保留在内存中,从而提升了性能,但它只是走向更高级优化的一个中间步骤。

### 核心概念

* 朴素实现中的内存压力
* 流式处理数据 vs. 把整个文件加载进内存
* 使用 StreamReader 做逐行处理
* 减少垃圾回收(GC)的暂停和开销

### 课程笔记

处理大数据集的最初"朴素"方案,通常是一次性把整个文件加载进内存。
在 .NET 中这通常用 `File.ReadAllLines` 来完成,它会创建一个很大的字符串数组。
对较小的文件(例如一百万行)这种做法或许还可以接受,但随着数据规模增长,它会变成一个显著的瓶颈。
主要问题在于大量的内存分配,它迫使垃圾回收器(GC)频繁地停止应用来回收内存,从而拉长了总处理时间。

```csharp
// Naive approach: Bring the file content with all the lines into memory
string[] lines = File.ReadAllLines(GlobalConstants.FilePath, Encoding.UTF8);

var results = lines.Select(line => 
{
    var parts = line.Split(';');
    var stationName = parts[0];
    double temperature = double.Parse(parts[1]);

    return new 
    {
        Station = stationName,
        Temperature = temperature
    };
})
.GroupBy(x => x.Station)
.Select(g => new 
{
    Station = g.Key,
    Min = g.Min(x => x.Temperature),
    Mean = g.Average(x => x.Temperature),
    Max = g.Max(x => x.Temperature)
})
.OrderBy(o => o.Station)
.ToList();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/introduction-69958224/?t=0)

为了提升性能,实现转向了流式模型。
应用不再加载整个数据集,而是用一个 `StreamReader` 逐行处理文件。
这种方案确保内存中只保留当前正在处理的数据。
通过遍历文件并实时更新一个统计信息字典,应用避免了与 LINQ 和大型字符串数组相关联的海量分配。

```csharp
// Streaming approach: Read the file line by line
using var reader = new StreamReader(GlobalConstants.FilePath,
                                    Encoding.UTF8,
                                    true,
                                    bufferSize: 1024);

string line;
var stations = new Dictionary<string, StationStats>(capacity: GlobalConstants.ExpectedStationCount);

while ((line = reader.ReadLine()) is not null)
{
    var separatorIndex = line.IndexOf(';');
    var stationName = line[..separatorIndex];
    var temp = double.Parse(line.AsSpan(start: separatorIndex + 1));

    if (!stations.TryGetValue(stationName, out var stat))
    {
        stat = new StationStats();
        stations.Add(stationName, stat);
    }
    stat.Update(temp);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/introduction-69958224/?t=68)

流式方案相比朴素实现显著提升了性能,但在 1 Billion Row Challenge 中它仍然只是进一步优化的起点。

## 2. Let's Continue with Stream

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/let-s-continue-with-stream-69958225/) · 15:10

### 总结

本课把 1 Billion Row Challenge 的实现从内存密集的朴素方案过渡到使用 StreamReader 的流式模型。
通过逐行处理文件并借助 ReadOnlySpan<char> 做数值解析,该实现显著减少了堆分配。
本课还引入了一个自定义的 StationStats 类和一个预先分配容量的 Dictionary 来高效处理数据聚合,避免了 LINQ GroupBy 和匿名类型的开销。

### 核心概念

*   **Streaming vs. Buffering**:把 `File.ReadAllLines`(它会把整个文件加载进内存)替换为 `StreamReader` 以进行顺序处理。
*   **Allocation Analysis**:识别出 LINQ 查询中的 `string.Split` 和匿名类型会在堆上创建数百万个不必要的对象。
*   **Span-based Parsing**:把 `ReadOnlySpan<char>` 与 `double.Parse` 配合使用,在不分配新子字符串的前提下从字符串中提取数值。
*   **Efficient Aggregation**:利用一个带预定义容量的 `Dictionary<string, StationStats>` 来手工完成分组和统计更新。
*   **Assignment Expressions**:使用在 `while` 循环条件内赋值的 C# 惯用法来简化流的读取。

### 课程笔记

在之前的迭代中,应用受困于过多的分配。
具体来说,一个一百万行的文件产生了约三百万次字符串分配:一百万次来自 `File.ReadAllLines` 得到的初始数组,两百万次来自对每一行调用 `string.Split(';')`(每行创建两个新字符串)。
为了解决这个问题,实现转向了流式方案。

```csharp
var stopwatch = Stopwatch.StartNew();

// Bring the file content with all the lines // 13.1MB
string[] lines = File.ReadAllLines(GlobalConstants.FilePath, Encoding.UTF8);

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
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/let-s-continue-with-stream-69958225/?t=40)

为了实现流式处理,我们使用带 `UTF8` 编码的 `StreamReader`。
读取流的一个常见 C# 模式是在 `while` 循环条件内部完成赋值。
赋值表达式会返回被赋的那个值;如果 `reader.ReadLine()` 返回一个字符串,循环就继续,而如果它返回 `null`(EOF),循环就终止。

```csharp
using var reader = new StreamReader(GlobalConstants.FilePath, Encoding.UTF8);

string line;

while ((line = reader.ReadLine()) is not null)
{

}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/let-s-continue-with-stream-69958225/?t=235)

我们用一个 `lineCounter` 来跟踪已处理的行数。
在循环内部,我们用 `line.IndexOf(';')` 定位分号分隔符。
虽然我们仍会用范围运算符 `line[..separatorIndex]` 为 `stationName` 分配一个字符串,但可以通过使用 `ReadOnlySpan<char>` 避免为温度值做第二次分配。

```csharp
string line;
var lineCounter = 0;

while ((line = reader.ReadLine()) is not null)
{
    lineCounter++;
    // line: istanbul;25.4
    var separatorIndex = line.IndexOf(';');
    var stationName = line[..separatorIndex]; // new string allocation

    var temp = double.Parse(line.AsSpan(start: separatorIndex + 1));
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/let-s-continue-with-stream-69958225/?t=460)

把 `line.AsSpan(start: separatorIndex + 1)` 传给 `double.Parse`,我们提供的是对现有字符串的一个"视图",而不是创建一个新的子字符串。
对一个一百万行的文件,这消除了一百万次字符串分配(完整挑战则是十亿次)。

为了存储结果,我们定义一个 `StationStats` 类。
这个类为每个气象站维护滚动的 `Min`、`Max`、`Sum` 和 `Count`,让我们可以按需计算 `Mean`,而不必存储每一个单独的温度值。

```csharp
public class StationStats
{
    public double Min { get; set; }
    public double Max { get; set; }
    public double Sum { get; set; }

    public int Count { get; set; }

    public double Mean => Count > 0 ? Sum / Count : 0;

    public void Update(double temp)
    {
        if (temp < Min)
            Min = temp;

        if (temp > Max)
            Max = temp;

        Sum += temp;
        Count++;
    }

    public override string ToString() => $"{Min:F1}/{Mean:F1}/{Max:F1}";
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/let-s-continue-with-stream-69958225/?t=895)

我们把这些统计信息聚合在一个 `Dictionary<string, StationStats>` 中。
为了优化这个字典,我们用 413 的容量来初始化它,这是数据集中已知的不重复气象站数量。
这避免了字典增长时代价高昂的内部扩容操作。

```csharp
var stations = new Dictionary<string, StationStats>
    (capacity: GlobalConstants.ExpectedStationCount);

while ((line = reader.ReadLine()) is not null)
{
    lineCounter++;
    // line: istanbul;25.4
    var separatorIndex = line.IndexOf(';'); // no allocation

    var stationName = line[..separatorIndex]; // new string allocation
    var temp = double.Parse(line.AsSpan(start: separatorIndex + 1)); // no allocation

    if (!stations.TryGetValue(stationName, out var stat)) // lookup
    {
        stat = new StationStats(); // new allocation for each unique stationName - 413
        stations.Add(stationName, stat);
    }

    stat.Update(temp); // no allocation
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/let-s-continue-with-stream-69958225/?t=805)

最后,结果按气象站名称排序并格式化输出。
`ResultLogger.FormatOutput` 辅助方法利用 `StationStats` 中重写的 `ToString` 方法,产出所要求的 `min/mean/max` 格式。

```csharp
// Sort by station name
var sortedResults = stations.OrderBy(kvp => kvp.Key).ToList(); // tolist allocation

var output = ResultLogger.FormatOutput(sortedResults);
Console.WriteLine(output);

Console.WriteLine();
Console.WriteLine($"Processed {lineCounter:N0} rows");
Console.WriteLine($"Found {stations.Count} unique stations");
Console.WriteLine($"Elapsed: {stopwatch.Elapsed}");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/let-s-continue-with-stream-69958225/?t=835)

## 3. Testing Stream Approach

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/testing-stream-approach-69958226/) · 9:52

### 总结

流式方案显著优于朴素的 LINQ 方案:在一千万行的场景下,它把内存占用从 1.6 GB 降到 46 MB,把执行时间从 5.6 秒缩短到 1 秒。
这一改进源于逐行处理数据而不是把整个文件加载进内存,从而把垃圾回收开销和内存分配降到最低。
借助 StreamReader 和基于 Span 的解析,即使输入规模扩大到一亿行,应用也能维持恒定的内存占用,这展示了内存管理在高性能 .NET 应用中的关键作用。

### 核心概念

*   **Memory Efficiency**:通过避免整体加载文件,把工作集从 1.6 GB 降低到 46 MB。
*   **Garbage Collection (GC) Optimization**:通过减少短命分配,把 GC 回收次数从约 270 次降到 60 次以下。
*   **Allocation Analysis**:区分一次性分配(StreamReader、Dictionary)与每行分配(ReadLine、气象站名称字符串)。
*   **Span-based Parsing**:把 `ReadOnlySpan<char>` 与 `double.Parse` 配合使用,消除数值数据转换的分配。
*   **Scalability**:在行数从一千万增加到一亿的过程中维持恒定的内存占用。

### 课程笔记

流式实现的性能是在一个一千万行的数据集上与朴素 LINQ 版本做对比评估的。
朴素方案需要 5.6 秒和 1.6 GB 内存,而流式实现处理同样的数据只用 1 秒和 46 MB 内存。
这相当于执行时间减少了 80-85%。

这一性能收益在很大程度上归功于垃圾回收器所承受的压力降低了。
朴素方案触发了约 270 次回收(包括 Gen 0、Gen 1 和 Gen 2),而流式方案触发的次数不到 60 次。
通过逐行处理文件,应用避免了把整个数据集保留在内存中所需的海量分配。

实现先强制执行一次垃圾回收,以确保测量有一个干净的基线,随后用一个特定的缓冲区大小初始化 `StreamReader`。

```csharp
GC.Collect();
GC.WaitForPendingFinalizers();
GC.Collect();

var stopwatch = Stopwatch.StartNew();

// one time allocation
using var reader = new StreamReader(GlobalConstants.FilePath, 
                                    Encoding.UTF8, 
                                    true, 
                                    bufferSize: 1024); // 1KB
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/testing-stream-approach-69958226/?t=10)

核心处理循环用 `reader.ReadLine()` 来获取数据。
虽然 `ReadLine()` 会为每一行分配一个新字符串,但这些字符串都是短命的。
实现进一步用 `ReadOnlySpan<char>` 做数值解析来优化,这避免了为温度值做额外的字符串分配。
`Dictionary` 按预期的气象站数量预先分配容量,以避免内部数组扩容。

```csharp
string line;
var lineCounter = 0;

// one time allocation
var stations = new Dictionary<string, StationStats>
    (capacity: GlobalConstants.ExpectedStationCount);

// each line allocates new string
while ((line = reader.ReadLine()) is not null)
{
    lineCounter++;
    // line: istanbul;25.4
    var separatorIndex = line.IndexOf(';'); // no allocation

    var stationName = line[..separatorIndex]; // new string allocation
    var temp = double.Parse(line.AsSpan(start: separatorIndex + 1)); // no allocation

    if (!stations.TryGetValue(stationName, out var stat)) // lookup
    {
        stat = new StationStats(); // new allocation for each unique stationName - 413
        stations.Add(stationName, stat);
    }

    stat.Update(temp); // no allocation
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/testing-stream-approach-69958226/?t=250)

`StationStats` 类负责最低、最高和平均温度的聚合。
它是一个引用类型,但它只为每个不重复的气象站名称实例化一次(约 413 次),而不是为数据集中的每一行都实例化。

```csharp
public class StationStats
{
    public double Min { get; set; }
    public double Max { get; set; }
    public double Sum { get; set; }

    public int Count { get; set; }

    public double Mean => Count > 0 ? Sum / Count : 0;

    public void Update(double temp)
    {
        if (temp < Min)
            Min = temp;

        if (temp > Max)
            Max = temp;

        Sum += temp;
        Count++;
    }

    public override string ToString() => $"{Min:F1}/{Mean:F1}/{Max:F1}";
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/testing-stream-approach-69958226/?t=370)

当扩大到一亿行时,流式方案维持着约 45 MB 的稳定工作集。
处理时间呈线性增长,一亿行大约需要 9 秒,同时维持约 245 MB/sec 的吞吐量。
这确认了流式模式对完整的十亿行挑战是可行的。

## 4. Homework: Disk Sector, Cluster, OS Page, BufferSize

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/homework-disk-sector-cluster-os-page-buffersize-69958227/) · 6:02

### 总结

本课引入了一个性能优化练习,聚焦于 .NET StreamReader 与底层硬件及操作系统之间的交互。
它要求开发者去调研磁盘扇区大小、磁盘簇大小和操作系统页大小,以确定文件 I/O 的最佳 bufferSize。
通过让应用的缓冲区与这些底层参数对齐,开发者可以把代价高昂的内核态上下文切换降到最低,并在顺序读取期间最大化操作系统级缓存的效率。

### 核心概念

* **Disk Sector Size**:硬盘上最小的物理存储单元。
* **Disk Cluster Size**:操作系统为存放一个文件所能分配的最小逻辑磁盘空间单元。
* **OS Page Size**:操作系统用于内存管理和缓存的固定长度虚拟内存块。
* **Buffer Size**:`StreamReader` 为减少 I/O 操作次数而使用的内部缓冲区大小。
* **Kernel Mode Transitions**:CPU 从用户态切换到内核态以执行诸如磁盘访问这类特权操作的过程。
* **Sequential I/O**:按顺序读取数据,这让操作系统可以预测并把数据预取到它的缓存中。

### 课程笔记

1BRC 解决方案的当前实现使用带默认参数的 `StreamReader`。
虽然它能正常工作,但通过调优应用向操作系统请求数据的方式,还有很大的改进空间。

```csharp
GC.WaitForPendingFinalizers();
GC.Collect();

var stopwatch = Stopwatch.StartNew();

// one time allocation
using var reader = new StreamReader(GlobalConstants.FilePath,
                        Encoding.UTF8);

string line;
var lineCounter = 0;

// one time allocation
var stations = new Dictionary<string, StationStats>
    (capacity: GlobalConstants.ExpectedStationCount);

// each line allocates new string
while ((line = reader.ReadLine()) is not null)
{
    lineCounter++;
    // line: istanbul;25.4
    var separatorIndex = line.IndexOf(';'); // no allocation
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/homework-disk-sector-cluster-os-page-buffersize-69958227/?t=10)

#### 作业任务

为了优化 I/O 层,你必须调研四个关键参数,以及它们与你具体的硬件和操作系统之间的关系:
1. Disk Sector Size
2. Disk Cluster Size
3. OS Page Size
4. Buffer Size

```csharp
GC.WaitForPendingFinalizers();
GC.Collect();

var stopwatch = Stopwatch.StartNew();

// one time allocation
using var reader = new StreamReader(GlobalConstants.FilePath,
                        Encoding.UTF8);

// Homework: Disk Sector Size, Disk Cluster Size, OS Page Size, Buffer Size

string line;
var lineCounter = 0;

// one time allocation
var stations = new Dictionary<string, StationStats>
    (capacity: GlobalConstants.ExpectedStationCount);

// each line allocates new string
while ((line = reader.ReadLine()) is not null)
{
    lineCounter++;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/homework-disk-sector-cluster-os-page-buffersize-69958227/?t=70)

#### 理解 I/O 栈

当一个 .NET 应用从磁盘读取数据时,它是穿过多个层次来完成的。
应用调用一个操作系统 API,这需要 CPU 切换到内核态才能访问硬件。
这次上下文切换在计算上是代价高昂的。

操作系统试图通过读取比请求量更多的数据来缓解这一点。
当你请求一个特定的字节范围时,操作系统会读取下一个簇或页的数据,并把它存进自己的缓存。
后续的读取就可以由这个缓存来满足,从而避免了一次访问物理磁盘的往返和一次内核态切换。

#### 调优 StreamReader 的缓冲区

`StreamReader` 有一个 `bufferSize` 参数,决定它一次从操作系统请求多少数据。
默认情况下这通常是 4 KB,但它是可以调优的。
例如,把它设为 1024 字节(1 KB)就会改变 reader 与操作系统缓存交互的方式。

```csharp
GC.WaitForPendingFinalizers();
GC.Collect();

var stopwatch = Stopwatch.StartNew();

// one time allocation
using var reader = new StreamReader(GlobalConstants.FilePath,
                                    Encoding.UTF8,
                                    true,
                                    bufferSize: 1024);

// Homework: Disk Sector Size, Disk Cluster Size, OS Page Size, Buffer Size

string line;
var lineCounter = 0;

// one time allocation
var stations = new Dictionary<string, StationStats>
    (capacity: GlobalConstants.ExpectedStationCount);

// each line allocates new string
while ((line = reader.ReadLine()) is not null)
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/homework-disk-sector-cluster-os-page-buffersize-69958227/?t=205)

如果缓冲区大小设得太小(例如 8 字节),应用就会被迫远为频繁地调用操作系统 API,由于持续不断的上下文切换而导致性能下降。

```csharp
var stopwatch = Stopwatch.StartNew();

// one time allocation
using var reader = new StreamReader(GlobalConstants.FilePath,
                                    Encoding.UTF8,
                                    true,
                                    bufferSize: 8); 

// Homework: Disk Sector Size, Disk Cluster Size, OS Page Size, Buffer Size

string line;
var lineCounter = 0;

// one time allocation
var stations = new Dictionary<string, StationStats>
    (capacity: GlobalConstants.ExpectedStationCount);

// each line allocates new string
while ((line = reader.ReadLine()) is not null)
{
    lineCounter++;
    // line: istanbul;25.4
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/homework-disk-sector-cluster-os-page-buffersize-69958227/?t=280)

由于 1BRC 涉及顺序数据访问(逐行读取),数据很可能位于同一个磁盘扇区或簇上。
这项调研的目标是为 `bufferSize` 找到与你的硬件规格(磁盘扇区和簇大小)以及操作系统规格(页大小)相匹配的"甜点值",从而最大化吞吐量。

---

## 运行 Demo

Demo 代码按课程官方仓库 [Dometrain/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet](https://github.com/Dometrain/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet) 的方式组织,本章对应上游的 `Level2_Stream/`:

```
src/1brc/
├── notes/            各章笔记
├── Shared/           SharedTypes.cs:GlobalConstants、ResultLogger、StationStats
├── DataGenerator/    Program.cs:413 个气象站 + Box-Muller 生成器
├── Level1_Naive/     Program.cs:第 3 章的 LINQ 朴素实现
└── Level2_Stream/    Program.cs:本章的 StreamReader 流式实现
```

本章第 2 课新增的 `StationStats` 和 `ResultLogger.FormatOutput` 放进了 `Shared/SharedTypes.cs`,与课程仓库一致。

先用 DataGenerator 生成测量文件,再跑 Level2_Stream:

```bash
cd src/1brc
dotnet run --project DataGenerator -c Release -- 1_000_000
dotnet run --project Level2_Stream -c Release
```

`Level2_Stream/Program.cs` 与第 4 课结束时的课程代码逐行一致,只有两处偏差:

- `string line;` 写成了 `string? line;`。仓库开启了 nullable,循环条件里的赋值否则会触发 CS8600。
- `double.Parse(...)` 外面包了 `#pragma warning disable CA1305`,原因与 Level1_Naive 相同:课程原样的写法没有传 `IFormatProvider`,而仓库开启了 `EnforceCodeStyleInBuild`。

下面是 12 核机器上的真实输出。
`{...}` 那一行有一万多个字符(413 个气象站),这里只保留开头,其余用 `…` 省略。
三次运行之间 DataGenerator 重新生成过文件,所以三组温度值互不相同。

10,000 行:

```text
=== Level 2: Stream Implementation ===
File: C:\Users\iantu\AppData\Local\Temp\1brc\Files\measurements.txt

{Abéché=0.0/28.3/48.7, Abha=-2.0/19.0/39.6, Abidjan=-3.1/25.7/41.1, Accra=0.0/26.1/43.1, Addis Ababa=0.0/15.5/29.0, …}

Processed 10,000 rows
Found 413 unique stations
Elapsed: 00:00:00.0284846

📁 Results saved: C:\Users\iantu\AppData\Local\Temp\1brc\Files\results.log
```

1,000,000 行:

```text
=== Level 2: Stream Implementation ===
File: C:\Users\iantu\AppData\Local\Temp\1brc\Files\measurements.txt

{Abéché=-11.8/29.6/64.0, Abha=-15.4/18.1/51.5, Abidjan=-7.5/25.8/63.0, Accra=-10.0/26.4/59.2, Addis Ababa=-18.8/15.8/48.0, …}

Processed 1,000,000 rows
Found 413 unique stations
Elapsed: 00:00:00.1704163

📁 Results saved: C:\Users\iantu\AppData\Local\Temp\1brc\Files\results.log
```

10,000,000 行:

```text
=== Level 2: Stream Implementation ===
File: C:\Users\iantu\AppData\Local\Temp\1brc\Files\measurements.txt

{Abéché=-9.7/29.4/68.5, Abha=-20.6/17.9/59.2, Abidjan=-13.2/26.0/65.7, Accra=-13.2/26.5/66.2, Addis Ababa=-22.1/16.0/55.8, …}

Processed 10,000,000 rows
Found 413 unique stations
Elapsed: 00:00:01.2686102

📁 Results saved: C:\Users\iantu\AppData\Local\Temp\1brc\Files\results.log
```

注意 10,000 行那一组里 `Abéché`、`Accra`、`Addis Ababa` 的最低温度都是 `0.0`。
`StationStats` 的 `Min` 和 `Max` 是普通自动属性,默认值都是 `0.0`,所以 `Update` 里的 `if (temp < Min)` 永远不会把 `Min` 抬到 0 以上,`if (temp > Max)` 也永远不会把 `Max` 压到 0 以下。
行数少的时候某些气象站抽不到负温度样本,`Min` 就停在 `0.0`。
这里保留了课程原样的写法;课程直到 Level 4 的 `StationStatsStruct` 才用 `double.MaxValue` / `double.MinValue` 做种子值。

`ResultLogger` 写出的 `results.log` 记下了这三次运行的内存数字(结果行同样省略):

```text
================================================================================
[2026-08-31 20:37:17] Level02_Stream
================================================================================
Performance:
  Rows:               10,000
  Stations:           413
  Elapsed:            00:00:00.0284846
  Throughput:         351,067 rows/sec (8.37 MB/sec)

Memory:
  Working Set:        31 MB
  GC Memory:          1 MB
  Gen0 Collections:   2
  Gen1 Collections:   2
  Gen2 Collections:   2

Processor:
  CPU Cores:          12
--------------------------------------------------------------------------------

================================================================================
[2026-08-31 20:37:26] Level02_Stream
================================================================================
Performance:
  Rows:               1,000,000
  Stations:           413
  Elapsed:            00:00:00.1704163
  Throughput:         5,867,983 rows/sec (139.90 MB/sec)

Memory:
  Working Set:        34 MB
  GC Memory:          3 MB
  Gen0 Collections:   23
  Gen1 Collections:   2
  Gen2 Collections:   2

Processor:
  CPU Cores:          12
--------------------------------------------------------------------------------

================================================================================
[2026-08-31 20:37:37] Level02_Stream
================================================================================
Performance:
  Rows:               10,000,000
  Stations:           413
  Elapsed:            00:00:01.2686102
  Throughput:         7,882,642 rows/sec (187.94 MB/sec)

Memory:
  Working Set:        35 MB
  GC Memory:          3 MB
  Gen0 Collections:   220
  Gen1 Collections:   2
  Gen2 Collections:   2

Processor:
  CPU Cores:          12
--------------------------------------------------------------------------------
```

与第 3 章 `Level1_Naive` 在同一台机器上跑一千万行的数字对照:7.03 秒、Working Set 1,421 MB、Gen0/1/2 回收 331/170/12。
Level2_Stream 是 1.27 秒、Working Set 35 MB、Gen0/1/2 回收 220/2/2。
Working Set 不随行数增长这一点,和第 3 课说的一致。
