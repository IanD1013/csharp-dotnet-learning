# Level 3: Multi-Threading

> 课程:[From Zero to Hero: 1 Billion Row Performance Challenge in .NET](https://dometrain.com/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet/) · 第 5 章
> 共 8 课 · 约 86:14
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction to CPU](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/introduction-to-cpu-69958228/) | 7:58 | [↓](#1-introduction-to-cpu) |
| 2 | [What is a Race Condition?](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/what-is-a-race-condition-69958229/) | 12:22 | [↓](#2-what-is-a-race-condition) |
| 3 | [What does Amdahl's Law say?](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/what-does-amdahl-s-law-say-69958230/) | 3:07 | [↓](#3-what-does-amdahls-law-say) |
| 4 | [Explaining the Thread Boundaries](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/explaining-the-thread-boundaries-69958231/) | 6:50 | [↓](#4-explaining-the-thread-boundaries) |
| 5 | [Let's Calculate the Boundaries](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/let-s-calculate-the-boundaries-69958232/) | 13:31 | [↓](#5-lets-calculate-the-boundaries) |
| 6 | [Dictionaries Benchmark](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/dictionaries-benchmark-69958233/) | 10:09 | [↓](#6-dictionaries-benchmark) |
| 7 | [Let's Implement Multi-Threading](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/let-s-implement-multi-threading-69958234/) | 18:43 | [↓](#7-lets-implement-multi-threading) |
| 8 | [Walk Through and Test the App](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/walk-through-and-test-the-app-69958235/) | 13:34 | [↓](#8-walk-through-and-test-the-app) |

## 1. Introduction to CPU

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/introduction-to-cpu-69958228/) · 7:58

### 总结

本课对 CPU 架构以及它对 .NET 中高性能数据处理的影响,给出了一个基础性的概览。
它解释了单核处理器如何通过快速的上下文切换,以及把应用状态"写回"(write back)内存,来模拟并发并制造出同时执行的错觉。
讨论从之前 Naive 与 Stream 实现的单线程局限,过渡到多核处理对 1 Billion Row Challenge 的潜力,并强调了物理核心与虚拟核心的区别,以及 Amdahl's Law 所定义的理论性能约束。

### 核心概念

*   **The Illusion of Concurrency**:单核 CPU 在单一处理循环内顺序执行指令,但它在任务之间切换得如此之快,以至于看起来像是在并行运行。
*   **Context Switching and Write Back**:操作系统通过把指令集发送给 CPU 来管理多任务处理,并在切换到另一个任务之前把当前应用状态保存到内存(write back)。
*   **CPU Hardware Components**:算术逻辑单元(ALU)执行位运算和算术运算,而寄存器和缓存提供高速的临时存储。
*   **Physical vs. Virtual Cores**:现代处理器使用多个物理核心和超线程(虚拟核心)来增加可用执行线程的数量。
*   **Amdahl's Law**:把一个任务并行到多个核心上会显著提升性能,但总的加速比会被那部分必须保持串行的工作所限制。

### 课程笔记

要优化海量数据集的处理,理解中央处理器(CPU)如何执行指令是必不可少的。
在较老的硬件上,CPU 通常只有一个核心。
一个单核 CPU 本质上就像一个单一的高速 `while` 循环在运转。
虽然看起来多个应用程序,比如 Spotify 和 Visual Studio,是在同时运行的,但这是操作系统和 CPU 执行速度共同制造出来的错觉。

操作系统的做法是,把来自不同应用程序的指令集分成小批次发送给 CPU。
例如,操作系统可能把 Spotify 的 10 条指令发送给 CPU。
要切换到另一个应用程序,比如编译器或调试器,CPU 必须停止当前任务并保存该应用的状态。
这个被称为"write back"的过程,是把当前的数据和指令指针存放到内存里,以便 CPU 之后能恢复这个任务。
随后 CPU 从内存中读取新任务的下一批指令。
这种在任务之间的快速切换叫做上下文切换(context switch)。

在硬件层面,CPU 依赖几个关键组件:
*   **Arithmetic Logic Unit (ALU)**:处理器的核心,执行算术运算和位运算。
*   **Registers**:极快的小容量存储位置,用于在执行期间临时保存数据。
*   **Cache**:容量更大但仍然很高速的存储区域,用于减少从主内存访问数据所需的时间。
*   **Hardware Clock**:一个计时器,按特定顺序同步指令的执行。

在 C# 这样的高级编程语言里,我们通过线程来接触这些概念。
在单核 CPU 上,这些是共享同一个物理执行循环的虚拟线程。
然而现代硬件提供了多个物理核心。
例如,一个有 16 个物理核心的处理器可以通过超线程提供 32 个虚拟核心(线程)。

在 1 Billion Row Challenge 之前的 Naive 和 Stream 实现里,处理被刻意限制在单个核心上。
要达到最高性能,实现必须转向能利用所有可用核心的多线程方案。
如果单核的 stream 方案处理一亿行需要 10 秒,那么理论上使用 32 个核心应该会让这个过程快得多。
不过,由于线程管理的开销以及 Amdahl's Law 的约束(它指出代码中的串行部分最终会限制可能达到的最大加速比),加速并不是完美线性的(也就是说,并不是简单地除以 32)。

## 2. What is a Race Condition?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/what-is-a-race-condition-69958229/) · 12:22

### 总结

本课探讨从顺序处理转向并行处理时出现的竞态条件(race condition)概念。
它用一个被自增一亿次的简单计数器,演示了非原子操作如何在多线程环境中导致非确定性的结果。
本课解释了底层的 CPU 机制(读取、自增、写回),并引入 `lock` 关键字和 .NET 9 新增的 `Lock` 类型作为保证线程安全的手段,同时指出这些同步原语会带来显著的性能开销。

### 核心概念

*   **Race Condition**:多个线程同时竞争访问并修改共享数据,从而导致结果不可预测的情形。
*   **Non-Atomic Operations**:像 `counter++` 这样看起来只有一行代码,却会被翻译成多条 CPU 指令(Read、Modify、Write)的操作。
*   **Determinism**:顺序代码是确定性的(每次都产生相同的结果),而没有同步的并行代码是非确定性的。
*   **Synchronization Primitives**:诸如 `lock` 关键字这样的工具,用来确保同一时刻只有一个线程能访问临界区。
*   **Performance Trade-offs**:加锁虽然保证了正确性,但由于线程排队和线程管理,它会引入显著的开销。

### 课程笔记

为了理解并行处理的复杂性,我们从一个基础的准备工作开始:识别运行环境,比如处理器数量,并通过强制垃圾回收让计时更准确。

```csharp
using Shared;
using System.Diagnostics;
using System.Text;

Console.WriteLine("=== Level 3: Parallel Implementation ===");
Console.WriteLine($"File: {GlobalConstants.FilePath}");
Console.WriteLine($"Processor Count: {Environment.ProcessorCount}");
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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/what-is-a-race-condition-69958229/?t=10)

#### Sequential vs. Parallel Execution

在标准的顺序 `for` 循环里,把一个计数器自增一亿次是确定性的。
结果永远正好是 100,000,000,因为只有一个线程在修改那个内存位置。

```csharp
int counter = 0;

for (int i = 0; i < 100_000_000; i++)
{
    counter++;
}

Console.WriteLine("Counter: {0}", counter); // counter = 100_000_000;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/what-is-a-race-condition-69958229/?t=100)

然而,一旦改用 `Parallel.For`,结果就变成非确定性的了。
把同样的逻辑并行运行,通常会得到一个低得多的最终计数值(例如 330 万而不是一亿)。
这是因为多个线程在"竞争"更新同一个内存地址。

```csharp
counter = 0;

Parallel.For(0, 100_000_000, _ =>
{
    counter++;
});

Console.WriteLine("Counter: {0}", counter); // Result is non-deterministic
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/what-is-a-race-condition-69958229/?t=220)

#### The Mechanics of a Race Condition

问题的根源在于:`counter++` 在 CPU 层面并不是一个原子操作。
它由三个不同的步骤组成:
1.  **Read**:CPU 从内存读取计数器的当前值,并把它存进一个寄存器。
2.  **Increase**:CPU 把寄存器里的值加 1。
3.  **Write**:CPU 把新值写回那个内存位置。

在多核环境中,两个线程可能在同一时刻读到相同的初始值(例如 0)。
两者都在各自的寄存器里把它加到 1,然后都把 1 写回内存。
计数器没有变成 2,而是丢掉了一次自增。

```csharp
int counter = 0;

for (int i = 0; i < 100_000_000; i++)
{
    counter++;

    // 1. Read the counter from memory - store it in cpu's registers
    // 2. Increase its value by 1 - INC counter 1
    // 3. Write the counter back to memory - STR counter
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/what-is-a-race-condition-69958229/?t=490)

#### Resolving Race Conditions with Locking

要修复这个问题,我们必须确保临界区(自增操作)在同一时刻只被一个线程访问。
这可以通过 `lock` 关键字配合一个共享的同步对象来实现。
当一个线程获得了锁,其他线程必须排队等待,直到锁被释放。

```csharp
counter = 0;
object lockObject = new object();

Parallel.For(0, 100_000_000, _ =>
{
    lock (lockObject)
    {
        counter++;
    }
});

Console.WriteLine("Counter: {0}", counter); // counter = 100_000_000;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/what-is-a-race-condition-69958229/?t=625)

这确实保证了正确的结果,但它显著地拖慢了性能。
加锁一亿次会让这个并行实现比顺序实现慢得多,因为线程一直在互相等待。

在现代 .NET(从 .NET 9 开始)中,可以使用一个新的 `System.Threading.Lock` 类型,相比在一个普通 object 上加锁,它提供了略微更高效的同步方式。

```csharp
counter = 0;
System.Threading.Lock lockObject = new ();

Parallel.For(0, 100_000_000, _ =>
{
    lock (lockObject)
    {
        counter++;
    }
});

Console.WriteLine("Counter: {0}", counter); // counter = 100_000_000;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/what-is-a-race-condition-69958229/?t=730)

## 3. What does Amdahl's Law say?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/what-does-amdahl-s-law-say-69958230/) · 3:07

### 总结

Amdahl's Law 把一个任务的加速比定义为处理器数量的函数,由此界定了它的理论上限,并强调程序中的顺序部分会限制并行化带来的整体性能收益。
在多线程 .NET 应用中,这条定律解释了为什么把线程数翻倍并不会让执行时间线性下降,以及为什么无论再添加多少核心,性能最终都会趋于平缓。

### 核心概念

- 并行化的理论极限。
- 多线程系统中的非线性加速。
- 顺序代码瓶颈对整体性能的影响。
- 增加处理器或线程数量时的收益递减。

### 课程笔记

多线程带来显著的性能优势,同时也引入了可扩展性上的理论极限。
Amdahl's Law 讨论的正是这些极限,它解释了为什么增加线程或处理器的数量并不总能带来成比例的速度提升。
这个概念紧接在竞态条件的讨论之后,是许多进程同时尝试执行同一任务时另一个关键的考虑因素。

如果某个特定任务在单线程上需要一分钟完成,把资源加倍到两个线程并不一定会把执行时间降到 30 秒。
这是因为每个应用程序都有一部分工作必须顺序执行。
随着处理器数量增加,加速比走的是一条非线性的曲线。
例如,一个系统在 64 个处理器上可能达到 15 倍加速,但把处理器翻倍到 128 个,得到的可能只是 17 倍加速,而不是人们直觉上期待的 30 倍。

最终,加速比会到达一个峰值。
超过这个点之后,再增加处理器或线程也不会带来额外的性能收益。
在某些模型里,峰值可能出现在 4,096 个处理器,之后无论再增加多少硬件,加速比都保持不变。
对 1 Billion Row Challenge 而言,这意味着:尽管我们利用多核 CPU 并行处理数据,总执行时间也不会被机器上可用的核心数完美地整除。
理解这个限制,对于设定现实的性能目标、以及识别并行化系统中真正的瓶颈,都至关重要。

## 4. Explaining the Thread Boundaries

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/explaining-the-thread-boundaries-69958231/) · 6:50

### 总结

本课从单线程流式处理过渡到多线程文件处理,先指出共享 stream reader 的局限。
它解释了:虽然 `StreamReader` 通过 `ReadLine` 简化了数据访问,但它的内部状态和游标管理会导致竞态条件,因此不适合并发访问。
要通过并行获得高性能,文件必须被划分成字节级的块,分配给各个线程。
然而,由于文本行的长度是可变的,简单的按字节等分会在块的边界处切坏数据。
本课确立了一个理论上的要求:必须计算精确的线程边界,让它们对齐到换行符,以保证并行任务之间的数据完整性。

### 核心概念

- **StreamReader Statefulness**:`StreamReader` 维护着一个内部游标和位置;调用 `ReadLine` 会推进这个游标,直到找到一个换行符。
- **Race Conditions in Streams**:多个线程在同一个 `StreamReader` 实例上调用 `ReadLine` 会争夺同一个游标,导致数据被重复处理或损坏。
- **Byte-Level Partitioning**:要并行化文件 I/O,应该把文件看成一个字节数组,并把其中的片段分配给可用的处理器。
- **Boundary Alignment Problem**:由于行的长度是可变的,把文件切成等长的字节块很可能会从中间切断某一行,因此需要一段逻辑把边界调整到最近的换行符。

### 课程笔记

在之前的实现里,应用程序使用单个 `StreamReader` 来处理文件。
这种做法很直接,因为 `StreamReader` 会处理与操作系统的交互:在调用 `ReadLine` 时获取元数据,并自动把字节解析成字符串。

```csharp
// Force garbage collection before measurement for accurate timing
GC.Collect();
GC.WaitForPendingFinalizers();
GC.Collect();

var stopwatch = Stopwatch.StartNew();

// one time allocation
using var reader = new StreamReader(GlobalConstants.FilePath,
                                    Encoding.UTF8,
                                    true,
                                    bufferSize: 1024); // 1KB

// HomeWork: Disk Sector Size, Disk Cluster Size, OS Page Size, Buffer Size

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

stopwatch.Stop();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/explaining-the-thread-boundaries-69958231/?t=25)

Stream 从根本上是面向字节的。
`StreamReader` 本质上是包裹在一个 stream 之外的一层,并维护一个游标(位置)。
当 `ReadLine` 被调用时,reader 会不断递增游标索引,直到识别出一个换行符,然后把累积的数据作为字符串返回。

试图把这套机制用在多个线程上会带来严重的问题。
如果 32 个线程同时在同一个 reader 上调用 `ReadLine`,它们就会遇到竞态条件。
由于 reader 的位置是共享的,多个线程可能试图读取同一段数据,或者干脆跳过某些段。

```csharp
// 1. Read the counter from memory - store it in cpu's registers
    // 2. Increase its value by 1 - INC counter 1
    // 3. Write the counter back to memory - STR counter
}

Console.WriteLine("Counter: {0}", counter); // counter = 100_000_000;

counter = 0;

Lock lockObject = new ();

Parallel.For(0, 100_000_000, _ =>
{
    lock (lockObject) // Synchronization required to prevent race conditions
    {
        counter++;
    }
});

Console.WriteLine("Counter: {0}", counter); // counter = 100_000_000;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/explaining-the-thread-boundaries-69958231/?t=10)

要实现真正的并行,我们必须放弃共享的 `ReadLine` 调用。
取而代之的是,我们可以取得文件的总字节数,再把这个大小除以可用的处理器数量。
例如,如果文件是 100 字节而我们有 10 个处理器,就可以给每个线程分配 10 字节。

然而,这带来了**边界问题(Boundary Problem)**。
由于数据集中各行的长度是可变的,严格按字节数等分很可能会把某个线程的起始或结束游标落在一个站点名或一个温度值的中间。

要解决这个问题,我们必须计算"块边界(chunk boundaries)"。
每个线程都会被分配一个起始位置,但我们必须把这个位置调整到下一整行的开头。
这样就能保证每个线程负责的是一组离散且合法的行,既不重叠,也不会损坏数据。
在实现的下一个阶段,我们会通过定位(seek)到目标字节偏移、再把游标前推到下一个可用的换行符(`\n`)来计算这些边界。

## 5. Let's Calculate the Boundaries

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/let-s-calculate-the-boundaries-69958232/) · 13:31

### 总结

要并行处理一个海量文件,必须把文件切分成块,使每个线程处理的是一组离散的完整行。
本课演示如何计算这些边界:先处理字节顺序标记(BOM),再从一个初始的目标位置向前扫描到最近的换行符。
这保证了没有任何一条记录会被切分到两个线程之间,从而为并行处理提供一组干净的起止偏移量。

### 核心概念

- 字节顺序标记(BOM)的检测与处理。
- 基于线程数计算初始块大小。
- 在 `FileStream` 中定位(seek)到目标位置。
- 扫描换行符(`\n`),让边界与记录的结尾对齐。
- 在计算边界时处理文件末尾(EOF)的情况。

### 课程笔记

`ComputeChunkBoundaries` 函数负责把一个文件划分成可以被多个线程独立处理的片段。
它返回一个 `long` 数组,表示每个线程应该开始和结束的字节偏移量。

```csharp
static long[] ComputeChunkBoundaries(string filePath, int threadCount)
{
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/let-s-calculate-the-boundaries-69958232/?t=10)

在计算偏移量之前,实现必须先考虑字节顺序标记(BOM)。
BOM 是文件开头的一些隐藏字节,用来标明编码(例如 UTF-8、UTF-16)。
如果 BOM 存在,第一个线程必须从这些字节之后开始读取。
这段逻辑读取文件的头几个字节,并把它们与 UTF-8、UTF-16(小端和大端)以及 UTF-32 的已知 BOM 签名进行比较。

```csharp
    var fileInfo = new FileInfo(filePath);
    var fileSize = fileInfo.Length;

    var boundaries = new long[threadCount + 1];
    int bomSize = 0; // we may want to hande BOM

    Span<byte> bom = stackalloc byte[4];
    using (var bomStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
    {
        int bytesRead = bomStream.Read(bom);
        if (bytesRead >= 3 && bom[0] == 0xEF && bom[1] == 0xBB && bom[2] == 0xBF)
            bomSize = 3; // UTF-8
        else if (bytesRead >= 2 && bom[0] == 0xFF && bom[1] == 0xFE)
            bomSize = 2; // UTF-16 LE
        else if (bytesRead >= 2 && bom[0] == 0xFE && bom[1] == 0xFF)
            bomSize = 2; // UTF-16 BE
        else if (bytesRead >= 4 && bom[0] == 0xFF && bom[1] == 0xFE && bom[2] == 0x00 && bom[3] == 0x00)
            bomSize = 4; // UTF-32 LE
    }

    boundaries[threadCount] = fileSize;
    boundaries[0] = bomSize;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/let-s-calculate-the-boundaries-69958232/?t=385)

确定了 BOM 的大小之后,用文件总大小减去 BOM 就得到了数据的总大小。
再把这个数据大小除以线程数,就得到每个线程大致的 `chunkSize`。
然后打开一个 `FileStream`,用于定位并扫描出真正的行边界。

```csharp
    var dataSize = fileSize - bomSize;
    var chunkSize = dataSize / threadCount;

    using var stream = new FileStream(filePath,
                                      FileMode.Open,
                                      FileAccess.Read,
                                      FileShare.Read);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/let-s-calculate-the-boundaries-69958232/?t=490)

核心逻辑是遍历每个线程(最后一个除外),找出它各自的结束边界。
stream 定位到理论上的 `targetPos`(计算为 `i * chunkSize`)。
由于这个位置很可能落在某一行的中间,代码用 `ReadByte()` 一个字节一个字节地向前扫描,直到遇到一个换行符(`\n`)。
紧跟在这个换行符之后的位置,就成了下一个线程块的起点。
如果在找到换行符之前就到达了文件末尾,边界就被设为文件的总大小。

```csharp
    for (int i = 1; i < threadCount; i++)
    {
        // Target end byte position for each thread
        var targetPos = i * chunkSize;

        // See to he target byte position
        stream.Seek(targetPos, SeekOrigin.Begin);

        int b;
        while ((b = stream.ReadByte()) != -1)
        {
            if (b == '\n')
            {
                boundaries[i] = stream.Position;
                break;
            }
        }

        if (b == -1)
        {
            boundaries[i] = fileSize;
        }
    }

    return boundaries;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/let-s-calculate-the-boundaries-69958232/?t=760)

## 6. Dictionaries Benchmark

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/dictionaries-benchmark-69958233/) · 10:09

### 总结

本课评估了在 1 Billion Row Challenge 的多线程数据处理中,不同字典策略的性能表现。
通过对 `ConcurrentDictionary`、加锁的 `Dictionary` 以及线程本地字典方案做基准测试,它证明了线程本地这种模式(每个线程维护自己的字典,最后再合并)提供了最好的性能和内存效率。
本课强调了随着数据量和基数(cardinality)增加,线程同步开销与分配模式如何影响可扩展性。

### 核心概念

*   **Thread Safety vs. Synchronization**:把内置的线程安全集合、手动加锁以及无锁模式放在一起比较。
*   **ConcurrentDictionary Overhead**:理解 `AddOrUpdate` 委托和内部对象创建所带来的分配成本。
*   **Lock Contention**:多个线程争夺同一个共享字典上的一把锁时的性能代价。
*   **Thread-Local Storage Pattern**:给每个线程一个私有字典,从而在热路径上避免同步,以此提升性能。
*   **Cardinality Impact**:唯一键的数量(例如 413 个站点)如何影响同步策略的选择。
*   **Result Merging**:把各个线程本地字典中的部分结果合并成最终结果集的过程。

### 课程笔记

在之前的单线程实现里,一个单独的 `Dictionary` 就足以跟踪站点统计信息。
过程包括:用 `TryGetValue` 做一次查找;如果站点不存在就添加它,然后更新统计数据。

```csharp
if (!stations.TryGetValue(stationName, out var stat))
{
    stat = new StationStats(); // new allocation for each unique stationName - 413
    stations.Add(stationName, stat);
}

stat.Update(temp); // no allocation
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/dictionaries-benchmark-69958233/?t=40)

一旦转入多线程环境,比如使用 `Parallel.For`,标准字典就会因为竞态条件而失效。
为了测试不同的解决方案,基准测试的准备工作会生成 413 个唯一的站点名以及可配置数量的测量值(最多一千万行)。

```csharp
[Params(100_000, 1_000_000, 10_000_000)]
public int MeasurementCount { get; set; }

[GlobalSetup]
public void Setup()
{
    var random = new Random(0197);

    // Generate 413 unique station names (matching 1BRC challenge)
    var stations = new string[413];
    for (var i = 0; i < 413; i++)
    {
        stations[i] = $"Station_{i:D3}"; // Station_000 to Station_412
    }

    _measurements = [];
    for (var i = 0; i < MeasurementCount; i++)
    {
        var station = stations[random.Next(stations.Length)];
        var temp = random.NextDouble() * 40 - 10;
        _measurements.Add((station, temp));
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/dictionaries-benchmark-69958233/?t=100)

#### ConcurrentDictionary Approach

`ConcurrentDictionary` 是一个线程安全的选项,它允许多个线程在不手动加锁的情况下执行查找和更新。
它提供了一个 `AddOrUpdate` 方法来处理"添加新条目或更新已有条目"的逻辑。

```csharp
[Benchmark]
[BenchmarkCategory("ConcurrentDict")]
public int ConcurrentDictionary()
{
    var shared = new ConcurrentDictionary<string, (double, double, double, int)>();
    var threadCount = Environment.ProcessorCount;
    var chunkSize = MeasurementCount / threadCount;

    Parallel.For(0, threadCount, i =>
    {
        var start = i * chunkSize;
        var end = (i == threadCount - 1) ? MeasurementCount : (i + 1) * chunkSize;

        for (var j = start; j < end; j++)
        {
            var (station, temp) = _measurements[j];

            shared.AddOrUpdate(
                station,
                (temp, temp, temp, 1),
                (key, existing) => (
                    Math.Min(existing.Item1, temp),
                    Math.Max(existing.Item2, temp),
                    existing.Item3 + temp,
                    existing.Item4 + 1
                )
            );
        }
    });

    return shared.Count;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/dictionaries-benchmark-69958233/?t=160)

虽然是线程安全的,`ConcurrentDictionary` 却相对慢且吃内存,因为它会创建大量内部对象,并且每次更新都需要调用委托。

#### Locked Dictionary Approach

另一种做法是使用标准的 `Dictionary` 配合手动 `lock`。
这样能保证同一时刻只有一个线程访问这个字典。

```csharp
[Benchmark]
[BenchmarkCategory("LockedDict")]
public int LockedDictionary()
{
    var shared = new Dictionary<string, (double, double, double, int)>();
    var threadCount = Environment.ProcessorCount;
    var chunkSize = MeasurementCount / threadCount;

    Parallel.For(0, threadCount, i =>
    {
        var start = i * chunkSize;
        var end = (i == threadCount - 1) ? MeasurementCount : (i + 1) * chunkSize;

        for (var j = start; j < end; j++)
        {
            var (station, temp) = _measurements[j];

            lock (_lock)
            {
                if (!shared.TryGetValue(station, out var stats))
                    stats = (temp, temp, temp, 1);
                else
                    stats = (Math.Min(stats.Item1, temp), Math.Max(stats.Item2, temp), stats.Item3 + temp, stats.Item4 + 1);

                shared[station] = stats;
            }
        }
    });

    return shared.Count;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/dictionaries-benchmark-69958233/?t=235)

由于许多线程试图同时访问同一把锁时争用很高,加锁通常比 `ConcurrentDictionary` 更慢。
不过,如果基数极低(例如只有 5 到 10 个站点),加锁实际上可能比 `ConcurrentDictionary` 表现更好。

#### Thread-Local Pattern

最高效的方案是线程本地模式。
每个线程维护自己私有的 `Dictionary`。
由于每个字典只被一个线程访问,处理阶段完全不需要加锁或同步。
所有线程结束之后,再把各个字典合并成最终的结果集。

```csharp
[Benchmark(Baseline = true)]
[BenchmarkCategory("ThreadLocal")]
public int ThreadLocal()
{
    var threadCount = Environment.ProcessorCount;
    var chunkSize = MeasurementCount / threadCount;
    var locals = new Dictionary<string, (double, double, double, int)>[threadCount];

    Parallel.For(0, threadCount, i =>
    {
        var start = i * chunkSize;
        var end = (i == threadCount - 1) ? MeasurementCount : (i + 1) * chunkSize;
        var local = new Dictionary<string, (double, double, double, int)>();

        for (var j = start; j < end; j++)
        {
            var (station, temp) = _measurements[j];
        
            if (!local.TryGetValue(station, out var stats))
                stats = (temp, temp, temp, 1);
            else
                stats = (Math.Min(stats.Item1, temp), Math.Max(stats.Item2, temp), stats.Item3 + temp, stats.Item4 + 1);
        
            local[station] = stats;
        }

        locals[i] = local;
    });

    // Merge
    var final = new Dictionary<string, (double, double, double, int)>();
    foreach (var local in locals)
    {
        if (local == null) continue;
        foreach (var (station, stats) in local)
        {
            if (!final.TryGetValue(station, out var existing))
                final[station] = stats;
            else
                final[station] = (
                    Math.Min(existing.Item1, stats.Item1),
                    Math.Max(existing.Item2, stats.Item2),
                    existing.Item3 + stats.Item3,
                    existing.Item4 + stats.Item4
                );
        }
    }

    return final.Count;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/dictionaries-benchmark-69958233/?t=445)

#### Benchmark Results

基准测试结果确认,`ThreadLocal` 方案比 `ConcurrentDictionary` 和 `LockedDictionary` 都显著更快、也更省内存。

| Method               | MeasurementCount | Mean         | Gen0       | Gen1       | Gen2     | Allocated     |
|--------------------- |----------------- |-------------:|-----------:|-----------:|---------:|--------------:|
| ConcurrentDictionary | 10,000,000       | 154,472.1 us | 96750.0000 | 11250.0000 | 250.0000 | 1562640.75 KB |
| LockedDictionary     | 10,000,000       | 375,206.0 us |          - |          - |        - |      50.23 KB |
| ThreadLocal          | 10,000,000       |  11,414.0 us |    78.1250 |    62.5000 |        - |    1332.64 KB |

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/dictionaries-benchmark-69958233/?t=370)

`ThreadLocal` 既避免了 `ConcurrentDictionary` 中那种巨量的分配,也避免了 `LockedDictionary` 因锁争用造成的极端性能退化。
由于每个线程只处理很少的一部分唯一站点(最多 413 个),最后的合并步骤极快。

## 7. Let's Implement Multi-Threading

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/let-s-implement-multi-threading-69958234/) · 18:43

### 总结

本课使用 .NET 的 Parallel.For 实现了 1 Billion Row Challenge 的多线程方案。
内容涵盖:按处理器数量把一个大文件划分成块、保证块与行边界对齐、以及使用线程本地字典来避免同步开销。
这个实现还利用 ReadOnlySpan<char> 来高效解析行,并以一个合并阶段收尾,把所有线程的结果汇总成最终的全局状态。

### 核心概念

- 用 `Parallel.For` 并行化工作负载,以利用所有可用的 CPU 核心。
- 使用尊重换行符的字节偏移边界来划分文件。
- 用线程本地存储(字典和计数器)消除锁争用。
- 使用 `ReadOnlySpan<char>` 和 `AsSpan` 高效处理字符串,把分配降到最低。
- 用后处理的合并逻辑聚合各线程独立得出的结果。

### 课程笔记

开始并行实现之前,应用程序先确定可用的硬件资源。
线程数取自运行环境的处理器数量,它决定了文件会被切成多少块。

```csharp
var threadCount = Environment.ProcessorCount; // 32
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/let-s-implement-multi-threading-69958234/?t=100)

接下来,文件被划分成块。
`ComputeChunkBoundaries` 函数为每个线程计算字节偏移量,确保每个块正好在一个换行符处结束,从而避免把同一行拆到两个线程上。

```csharp
long[] chunkBoundaries = ComputeChunkBoundaries(GlobalConstants.FilePath, threadCount);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/let-s-implement-multi-threading-69958234/?t=130)

为了避免同步开销和锁争用,每个线程维护自己的本地结果。
一个字典数组和一个行计数器数组被初始化出来,用于存放这些线程各自的值。

```csharp
var threadLocalResults = new Dictionary<string, StationStats>[threadCount];
var lineCounters = new long[threadCount];
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/let-s-implement-multi-threading-69958234/?t=175)

核心处理由一个 `Parallel.For` 循环完成。
每个线程从预先算好的边界里取得自己的起止字节位置,把分配给它的那块读进一个本地缓冲区,然后处理这些数据。

```csharp
Parallel.For(0, threadCount, threadIndex =>
{
    // by each thread
    var localLineCounter = 0;
    var startByte = chunkBoundaries[threadIndex];
    var endByte = chunkBoundaries[threadIndex + 1];
    var chunkLength = (int)(endByte - startByte);

    var localStats = new Dictionary<string, StationStats>(GlobalConstants.ExpectedStationCount);

    var buffer = new byte[chunkLength];
    using (var stream = new FileStream(GlobalConstants.FilePath,
                                     FileMode.Open,
                                     FileAccess.Read,
                                     FileShare.Read))
    {
        stream.Seek(startByte, SeekOrigin.Begin);
        stream.ReadExactly(buffer, 0, chunkLength);
    }

    var chunkText = Encoding.UTF8.GetString(buffer);

    var lineStart = 0;

    for (var i = 0; i < chunkText.Length; i++)
    {
        if (chunkText[i] == '\n')
        {
            var lineEnd = i;
            if (lineEnd > lineStart && chunkText[lineEnd - 1] == '\r')
                lineEnd--;

            if (lineEnd > lineStart)
            {
                ProcessLine(chunkText.AsSpan(lineStart, lineEnd - lineStart), localStats);
                localLineCounter++;
            }

            lineStart = i + 1;
        }
    }

    threadLocalResults[threadIndex] = localStats;
    lineCounters[threadIndex] = localLineCounter;
});
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/let-s-implement-multi-threading-69958234/?t=1030)

`ProcessLine` 方法为效率而设计,它接受一个 `ReadOnlySpan<char>`,从而避免为每一行创建新的字符串对象。
它找出分号分隔符,解析站点名和温度,然后更新线程本地的字典。

```csharp
static void ProcessLine(ReadOnlySpan<char> line, Dictionary<string, StationStats> stats)
{
    var separationIndex = line.IndexOf(';');
    if (separationIndex < 0)
        return;

    var stationName = line[..separationIndex].ToString(); // new allocation
    var temp = double.Parse(line[(separationIndex + 1)..]);

    if (!stats.TryGetValue(stationName, out var stationStats))
    {
        stationStats = new StationStats();
        stats[stationName] = stationStats;
    }

    stationStats.Update(temp);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/let-s-implement-multi-threading-69958234/?t=895)

所有线程处理完之后,各个字典必须被合并成一个最终结果。
这一步是顺序完成的,遍历 `threadLocalResults` 数组即可。

```csharp
var finalResults = new Dictionary<string, StationStats>(GlobalConstants.ExpectedStationCount);

foreach (var localStats in threadLocalResults)
{
    if (localStats == null)
        continue;

    foreach (var (stationName, stats) in localStats)
    {
        if (!finalResults.TryGetValue(stationName, out var existingStats))
        {
            existingStats = new StationStats();
            finalResults[stationName] = existingStats;
        }

        existingStats.Merge(stats);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/let-s-implement-multi-threading-69958234/?t=1045)

`StationStats` 类中的 `Merge` 操作把另一个 `StationStats` 实例的各项指标(Min、Max、Sum 和 Count)合并到当前实例中。

```csharp
public void Merge(StationStats other)
{
    if (other.Min < Min)
        Min = other.Min;

    if (other.Max > Max)
        Max = other.Max;

    Sum += other.Sum;
    Count += other.Count;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/let-s-implement-multi-threading-69958234/?t=1075)

## 8. Walk Through and Test the App

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/walk-through-and-test-the-app-69958235/) · 13:34

### 总结

本课完整走一遍 1 Billion Row Challenge 多线程解法的实现与性能测试。
通过把输入文件划分成离散的块并用 Parallel.For 处理它们,应用程序的吞吐量相比单线程版本有了显著提升。
这个实现使用线程本地字典来避免竞态条件,不过它通过巨大的字节数组分配和字符串转换引入了可观的内存压力。
本课以性能分析收尾:并行方案虽然"快得惊人",但海量分配带来的垃圾回收开销,成了扩展到十亿行时的首要瓶颈。

### 核心概念

* 使用 `Parallel.For` 并行处理文件,以利用所有可用的 CPU 核心。
* 计算块边界,以保证数据完整性、避免行被切分到多个线程。
* 使用线程本地存储(字典)消除同步开销和竞态条件。
* 由 `byte[]` 缓冲区和 `Encoding.UTF8.GetString` 转换造成的内存分配瓶颈。
* 垃圾回收(GC)对高性能执行的影响,尤其是在大数据集上。
* 从一亿行扩展到十亿行的性能分析。

### 课程笔记

本课先处理从单线程到并行执行的过渡。
在之前的单线程例子里,只用了一个字典,因此不存在竞态条件。
转向并行执行则需要一套处理共享状态的策略。
虽然加锁或 `ConcurrentDictionary` 之类的选项都存在,这里更倾向的做法是使用线程本地字典,从根本上消除争用。

要让多个线程处理同一个大文件,应用程序必须计算块边界。
这保证了每个线程处理的是文件中一段唯一的片段,既不重叠,也不会错误地切断行。
`ComputeChunkBoundaries` 方法确定这些偏移量,确保每个线程都从某一行的开头开始。

核心逻辑用 `Parallel.For` 把工作负载分摊到可用的处理器核心上。
在并行循环内部,每个线程确定自己的字节范围并分配一个本地字典。

```csharp
var threadCount = Environment.ProcessorCount; // 32
long[] chunkBoundaries = ComputeChunkBoundaries(GlobalConstants.FilePath, threadCount);

var threadLocalResults = new Dictionary<string, StationStats>[threadCount];
var lineCounters = new long[threadCount];

Parallel.For(0, threadCount, threadIndex =>
{
    // by each thread
    var localLineCounter = 0;
    var startByte = chunkBoundaries[threadIndex];
    var endByte = chunkBoundaries[threadIndex + 1];
    var chunkLength = (int)(endByte - startByte);

    // allocation
    var localStats = new Dictionary<string, StationStats>(GlobalConstants.ExpectedStationCount);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/walk-through-and-test-the-app-69958235/?t=160)

数据读取阶段引入了一个显著的性能瓶颈。
每个线程按自己那块的大小分配一个 `byte[]` 缓冲区,并从文件流中读取数据。
接着这个缓冲区被 `Encoding.UTF8.GetString` 转换成一个托管字符串。
对一个 13GB 的文件分给 32 个线程来说,每个线程可能为字节数组分配大约 300 到 400MB,再为字符串分配另外 300 到 400MB。

```csharp
    var buffer = new byte[chunkLength]; // HUGE allocation

    using (var stream = new FileStream(GlobalConstants.FilePath,
                                       FileMode.Open,
                                       FileAccess.Read,
                                       FileShare.Read))
    {
        stream.Seek(startByte, SeekOrigin.Begin);
        stream.ReadExactly(buffer, 0, chunkLength);
    }

    // another HUGE allocation
    var chunkText = Encoding.UTF8.GetString(buffer);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/walk-through-and-test-the-app-69958235/?t=175)

一旦这一块被作为字符串加载进来,线程就遍历其中的字符来寻找换行符。
为了避免为每一行再产生字符串分配,代码通过 `AsSpan` 方法使用 `ReadOnlySpan<char>`。

```csharp
    var lineStart = 0;

    for (var i = 0; i < chunkText.Length; i++)
    {
        // Processing each line from a bigger string that contains multiple lines.
        if (chunkText[i] == '\n')
        {
            var lineEnd = i;
            if (lineEnd > lineStart && chunkText[lineEnd - 1] == '\r')
                lineEnd--;

            if (lineEnd > lineStart)
            {
                // process file
                ProcessLine(chunkText.AsSpan(lineStart, lineEnd - lineStart), localStats);
                localLineCounter++;
            }

            lineStart = i + 1;
        }
    }

    threadLocalResults[threadIndex] = localStats;
    lineCounters[threadIndex] = localLineCounter;

});
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/walk-through-and-test-the-app-69958235/?t=340)

`ProcessLine` 方法负责解析每个站点和温度。
虽然行本身用的是 `ReadOnlySpan`,但站点名仍然需要一次新的字符串分配来充当字典键,在使用标准 `Dictionary<string, StationStats>` 的情况下这一点目前无法避免。

```csharp
static void ProcessLine(ReadOnlySpan<char> line, Dictionary<string, StationStats> stats)
{
    var separationIndex = line.IndexOf(';');
    if (separationIndex < 0)
        return;

    var stationName = line[..separationIndex].ToString(); // new allocation
    var temp = double.Parse(line[(separationIndex + 1)..]);

    if (!stats.TryGetValue(stationName, out var stationStats))
    {
        stationStats = new StationStats();
        stats[stationName] = stationStats;
    }

    stationStats.Update(temp);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/walk-through-and-test-the-app-69958235/?t=400)

所有线程完成工作之后,各个线程本地字典必须被合并成一个结果集。
由于字典的数量只等于线程数(例如 32),而唯一站点的数量也有限(例如 413),这次合并操作极快,对整体性能没有显著影响。

```csharp
// MERGE Dictionary

var finalResults = new Dictionary<string, StationStats>(GlobalConstants.ExpectedStationCount);

foreach (var localStats in threadLocalResults)
{
    if (localStats == null)
        continue;

    foreach (var (stationName, stats) in localStats)
    {
        if (!finalResults.TryGetValue(stationName, out var existingStats))
        {
            existingStats = new StationStats();
            finalResults[stationName] = existingStats;
        }

        existingStats.Merge(stats);
    }
}

var totalLines = lineCounters.Sum();
stopwatch.Stop();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/walk-through-and-test-the-app-69958235/?t=430)

最终结果被排序、格式化并记录下来。

```csharp
var sortedResults = finalResults.OrderBy(kvp => kvp.Key).ToList();

var output = ResultLogger.FormatOutput(sortedResults);
Console.WriteLine(output);

Console.WriteLine();
Console.WriteLine($"Processed {totalLines:N0} rows using {threadCount} threads");
Console.WriteLine($"Found {finalResults.Count} unique stations");
Console.WriteLine($"Elapsed: {stopwatch.Elapsed}");

// Save results to file
ResultLogger.SaveResult(
    projectName: "Level03_Parallel",
    output: output,
    elapsed: stopwatch.Elapsed,
    rowCount: totalLines,
    stationCount: finalResults.Count);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/walk-through-and-test-the-app-69958235/?t=480)

性能测试揭示了这个并行方案的效果。
在一亿行的数据集上,执行时间从单线程版本的将近 10 秒下降到了约 1.3 秒。
然而内存用量飙升到 4GB,伴随 250 次 Generation 0 垃圾回收。
扩展到十亿行时,应用程序耗时 18 秒、消耗 32GB 内存,并触发了超过 2,000 次垃圾回收。
这说明:并行虽然带来了巨大的加速,但当前实现中的"巨量分配"以 GC 压力的形式造就了一个新的瓶颈。
下一阶段的优化将聚焦于"零拷贝、零分配"的技术,来缓解这些问题。

---

## 运行 Demo

Demo 代码按课程官方仓库 [Dometrain/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet](https://github.com/Dometrain/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet) 的方式组织,本章对应上游的 `Level3_Parallel/` 和 `Benchmarkts/Level3_Parallel/`:

```
src/1brc/
├── notes/            各章笔记
├── Shared/           SharedTypes.cs:GlobalConstants、ResultLogger、StationStats
├── DataGenerator/    Program.cs:413 个气象站 + Box-Muller 生成器
├── Level1_Naive/     Program.cs:第 3 章的 LINQ 朴素实现
├── Level2_Stream/    Program.cs:第 4 章的 StreamReader 流式实现
├── Level3_Parallel/  Program.cs:本章的 Parallel.For 实现;RaceCondition.cs:第 2 课的计数器实验
└── Benchmarks/       Level3_Parallel/ThreadLocalVsSharedBenchmark.cs:第 6 课的字典基准测试
```

本章第 7 课新增的 `StationStats.Merge` 放进了 `Shared/SharedTypes.cs`,与课程仓库一致。
上游把基准测试放在一个叫 `Benchmarkts/` 的目录里(拼写如此),这里叫 `Benchmarks/`,类的命名空间保持上游的 `Benchmarks.Level03_Parallel`。

先用 DataGenerator 生成测量文件,再跑 Level3_Parallel:

```bash
cd src/1brc
dotnet run --project DataGenerator -c Release -- 10_000_000
dotnet run --project Level3_Parallel -c Release
```

`Level3_Parallel/Program.cs` 与第 8 课结束时的课程代码逐行一致,只有两处偏差:

- `double.Parse(...)` 外面包了 `#pragma warning disable CA1305`,原因与 Level1_Naive、Level2_Stream 相同:课程原样的写法没有传 `IFormatProvider`,而仓库开启了 `EnforceCodeStyleInBuild`。
- 开头多了一个 `args` 分支,把第 2 课的计数器实验挪到了 `RaceCondition.cs`。那些代码在课上就写在这个 Program.cs 里,后来被并行实现替换掉了,所以上游仓库里已经看不到它们。

下面是 12 核机器上的真实输出。
`{...}` 那一行有一万多个字符(413 个气象站),这里只保留开头,其余用 `…` 省略。

10,000,000 行:

```text
=== Level 3: Parallel Implementation ===
File: C:\Users\iantu\AppData\Local\Temp\1brc\Files\measurements.txt
Processor Count: 12

{Abéché=-9.7/29.4/68.5, Abha=-20.6/17.9/59.2, Abidjan=-13.2/26.0/65.7, Accra=-13.2/26.5/66.2, Addis Ababa=-22.1/16.0/55.8, …}

Processed 10,000,000 rows using 12 threads
Found 413 unique stations
Elapsed: 00:00:00.2513291

📁 Results saved: C:\Users\iantu\AppData\Local\Temp\1brc\Files\results.log

Per-Thread Statistics:
  Thread 0: 833,483 lines, 413 stations
  Thread 1: 833,469 lines, 413 stations
  Thread 2: 833,267 lines, 413 stations
  Thread 3: 833,388 lines, 413 stations
  Thread 4: 833,583 lines, 413 stations
  Thread 5: 833,350 lines, 413 stations
  Thread 6: 833,217 lines, 413 stations
  Thread 7: 833,229 lines, 413 stations
  Thread 8: 833,432 lines, 413 stations
  Thread 9: 833,369 lines, 413 stations
  Thread 10: 833,301 lines, 413 stations
  Thread 11: 832,912 lines, 413 stations
```

这一行的温度值与第 4 章 `Level2_Stream` 跑一千万行时的输出逐字相同,读的是同一个文件。
12 个线程各自的行数在 832,912 到 833,583 之间,差异来自第 5 课的边界对齐:每个线程的起点都被推到了下一个换行符之后。

`ResultLogger` 写出的 `results.log` 记下了内存数字(结果行同样省略):

```text
================================================================================
[2026-08-31 22:05:53] Level03_Parallel
================================================================================
Performance:
  Rows:               10,000,000
  Stations:           413
  Elapsed:            00:00:00.2513291
  Throughput:         39,788,469 rows/sec (948.63 MB/sec)

Memory:
  Working Set:        384 MB
  GC Memory:          330 MB
  Gen0 Collections:   103
  Gen1 Collections:   10
  Gen2 Collections:   5

Processor:
  CPU Cores:          12
--------------------------------------------------------------------------------
```

与第 4 章 `Level2_Stream` 在同一台机器上跑一千万行的数字对照:1.27 秒、Working Set 35 MB、Gen0/1/2 回收 220/2/2。
`Level3_Parallel` 是 0.25 秒、Working Set 384 MB、Gen0/1/2 回收 103/10/5。
第 8 课说的两件事同时出现了:并行带来了数倍加速,而按块分配的 `byte[]` 加 `Encoding.UTF8.GetString` 让内存占用涨了一个数量级。

第 2 课的计数器实验:

```bash
dotnet run --project Level3_Parallel -c Release -- race
```

```text
=== Level 3: Race Condition ===
Processor Count: 12
Iterations: 100,000,000

for                      counter =  100,000,000  00:00:00.0381393  <- correct
Parallel.For, no lock    counter =   10,804,572  00:00:00.6523791  <- lost 89,195,428 increments
Parallel.For, object     counter =  100,000,000  00:00:08.3255104  <- correct
Parallel.For, Lock       counter =  100,000,000  00:00:08.3309254  <- correct
```

一亿次自增里丢了 89,195,428 次。
加锁把结果修正回一亿,代价是 8.33 秒对顺序循环的 0.038 秒,也就是第 2 课说的"加锁一亿次会让这个并行实现比顺序实现慢得多"。
这台机器上 `object` 和 `System.Threading.Lock` 的耗时几乎相同。

第 6 课的字典基准测试:

```bash
dotnet run --project Benchmarks -c Release -- --filter *ThreadLocalVsSharedBenchmark*
```

```text
BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.9278/25H2/2025Update/HudsonValley2)
Snapdragon X 12-core X1E80100 3.40 GHz (Max: 3.42GHz), 1 CPU, 12 logical and 12 physical cores
.NET SDK 10.0.102
  [Host]     : .NET 10.0.2 (10.0.2, 10.0.225.61305), Arm64 RyuJIT armv8.0-a
  DefaultJob : .NET 10.0.2 (10.0.2, 10.0.225.61305), Arm64 RyuJIT armv8.0-a

| Method               | MeasurementCount | Mean         | Error       | StdDev       | Median       | Ratio | RatioSD | Gen0        | Gen1      | Allocated     | Alloc Ratio |
|--------------------- |----------------- |-------------:|------------:|-------------:|-------------:|------:|--------:|------------:|----------:|--------------:|------------:|
| ConcurrentDictionary | 100000           |   3,272.6 us |    65.11 us |    141.54 us |   3,240.0 us |  8.94 |    0.46 |   3890.6250 |    7.8125 |   15721.61 KB |       29.92 |
| LockedDictionary     | 100000           |   8,853.9 us |    71.48 us |     66.86 us |   8,851.5 us | 24.19 |    0.69 |           - |         - |      44.45 KB |        0.08 |
| ThreadLocal          | 100000           |     366.3 us |     6.09 us |     10.67 us |     362.8 us |  1.00 |    0.04 |    130.8594 |   62.5000 |     525.54 KB |        1.00 |
|                      |                  |              |             |              |              |       |         |             |           |               |             |
| ConcurrentDictionary | 1000000          |  30,901.8 us |   580.52 us |    514.61 us |  30,888.8 us |  8.34 |    0.14 |  38687.5000 |   62.5000 |  156343.87 KB |      297.85 |
| LockedDictionary     | 1000000          |  55,391.0 us |   394.60 us |    349.80 us |  55,406.3 us | 14.95 |    0.10 |           - |         - |      44.44 KB |        0.08 |
| ThreadLocal          | 1000000          |   3,705.4 us |    12.27 us |     10.88 us |   3,704.9 us |  1.00 |    0.00 |    125.0000 |   62.5000 |     524.91 KB |        1.00 |
|                      |                  |              |             |              |              |       |         |             |           |               |             |
| ConcurrentDictionary | 10000000         | 333,044.9 us | 6,548.51 us | 11,119.87 us | 336,067.6 us | 10.22 |    2.15 | 387000.0000 | 1000.0000 | 1562597.38 KB |    2,976.25 |
| LockedDictionary     | 10000000         | 565,818.1 us | 4,873.38 us |  5,801.42 us | 565,243.2 us | 17.36 |    3.61 |           - |         - |      44.77 KB |        0.09 |
| ThreadLocal          | 10000000         |  33,823.5 us | 2,141.25 us |  6,074.37 us |  36,427.9 us |  1.04 |    0.29 |     93.7500 |   31.2500 |     525.02 KB |        1.00 |
```

三种方案的排序和第 6 课课上的表格一致:`ThreadLocal` 最快,`ConcurrentDictionary` 次之,`LockedDictionary` 最慢。
一千万行那一组里 `ThreadLocal` 是 33.8 ms、`ConcurrentDictionary` 是 333.0 ms、`LockedDictionary` 是 565.8 ms。
`ConcurrentDictionary` 每次操作分配 1,562,597.38 KB,与课上表格里的 1,562,640.75 KB 几乎一致;`LockedDictionary` 只分配 44.77 KB 却最慢,因为代价全在锁争用上,不在分配上。
