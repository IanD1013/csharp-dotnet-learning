# Level 4: Memory Mapped Files

> 课程:[From Zero to Hero: 1 Billion Row Performance Challenge in .NET](https://dometrain.com/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet/) · 第 6 章
> 共 14 课 · 约 177:40
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction to MMF](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/introduction-to-mmf-69958236/) | 5:49 | [↓](#1-introduction-to-mmf) |
| 2 | [CPU Cycles Explained](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/cpu-cycles-explained-69958237/) | 10:02 | [↓](#2-cpu-cycles-explained) |
| 3 | [User Mode vs Kernel Mode on CPU](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/user-mode-vs-kernel-mode-on-cpu-69958238/) | 14:27 | [↓](#3-user-mode-vs-kernel-mode-on-cpu) |
| 4 | [Cache Hit vs Cache Miss Tests](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/cache-hit-vs-cache-miss-tests-69958239/) | 13:05 | [↓](#4-cache-hit-vs-cache-miss-tests) |
| 5 | [Working with Pointers in C#](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/working-with-pointers-in-csharp-69958240/) | 31:25 | [↓](#5-working-with-pointers-in-c) |
| 6 | [What is Hashing?](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/what-is-hashing-69958241/) | 17:43 | [↓](#6-what-is-hashing) |
| 7 | [Memory Mapped Files](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/memory-mapped-files-69958242/) | 6:44 | [↓](#7-memory-mapped-files) |
| 8 | [Let's Start Coding](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/let-s-start-coding-69958243/) | 12:59 | [↓](#8-lets-start-coding) |
| 9 | [Continue Coding](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/continue-coding-69958244/) | 17:29 | [↓](#9-continue-coding) |
| 10 | [Hash Comparison](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/hash-comparison-69958245/) | 6:28 | [↓](#10-hash-comparison) |
| 11 | [Custom Double Parse and Benchmarks](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/custom-double-parse-and-benchmarks-69958246/) | 8:21 | [↓](#11-custom-double-parse-and-benchmarks) |
| 12 | [Coding the Custom Double Parse](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/coding-the-custom-double-parse-69958247/) | 10:44 | [↓](#12-coding-the-custom-double-parse) |
| 13 | [Finalizing the Approach](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/finalizing-the-approach-69958248/) | 8:49 | [↓](#13-finalizing-the-approach) |
| 14 | [It's time to test our App](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/it-s-time-to-test-our-app-69958249/) | 13:35 | [↓](#14-its-time-to-test-our-app) |

## 1. Introduction to MMF

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/introduction-to-mmf-69958236/) · 5:49

### 总结

本课介绍一种面向 .NET 开发的高性能范式,把关注点从高层抽象转移到面向硬件的编程上。
通过把字符串分配和垃圾回收(GC)开销确认为首要瓶颈,本课主张一种"无字符串"(string-less)的架构。
该实现利用 Memory Mapped Files(MMF)和 unsafe 指针,直接在内存中与文件数据交互,把托管对象的开销降到最低,并让 CPU 效率最大化,以便处理 1 Billion Row Challenge 这类海量数据集。

### 核心概念

- 托管 string 对象和堆分配的性能代价。
- 垃圾回收(GC)的影响:线程挂起和 "stop-the-world" 暂停。
- 用于零拷贝文件访问的 Memory Mapped Files(MMF)。
- 用于手动内存管理的 unsafe 代码和指针运算。
- 硬件层面的优化:CPU 周期、指令流水线,以及无分支代码。

### 课程笔记

进入 Level 4 意味着要离开 LINQ 和字符串操作这类标准的 .NET 惯用写法。
虽然它们适合通用开发,但在高性能场景下会引入显著的开销。
string 是分配在堆上的不可变对象,会给垃圾回收器带来压力。
每次创建或回收一个 string,GC 都必须跟踪它;而在回收期间,它可能会挂起应用程序的所有线程。
当目标是在 20 秒内处理完 13GB 数据时,哪怕一次很短的暂停都可能是致命的。

为了突破这些限制,该实现采用了"无字符串"的做法。
这需要对操作系统和硬件的交互方式有更深的理解。
借助 Memory Mapped Files,应用程序可以把磁盘上的文件当作进程地址空间里的一块内存来对待。

```csharp
using var mmf = MemoryMappedFile.CreateFromFile(GlobalConstants.FilePath,
                                                FileMode.Open,
                                                null,
                                                0,
                                                MemoryMappedFileAccess.Read);

using var accessor = mmf.CreateViewAccessor(0,
                                            fileSize,
                                            MemoryMappedFileAccess.Read);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/introduction-to-mmf-69958236/?t=0)

通过使用 unsafe 块和指针,应用程序可以直接访问被映射的内存。
这为了速度而绕过了托管环境的安全检查,从而可以做手动的字节级解析。

```csharp
unsafe
{
    // istanbul;25.4
    byte* basePtr = null;
    accessor.SafeMemoryMappedViewHandle.AcquirePointer(ref basePtr);

    try
    {
        // Direct memory access via basePtr
    }
    finally
    {
        accessor.SafeMemoryMappedViewHandle.ReleasePointer();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/introduction-to-mmf-69958236/?t=0)

这种做法实现了零分配解析。
代码不再为气象站名称或温度创建 string 对象,而是用 `ReadOnlySpan<byte>` 表示对现有内存缓冲区的视图。
这把垃圾回收器需要做的工作降到最低,并让 CPU 能在其物理极限上运转,把每一毫秒、每一纳秒的性能都作为目标。

## 2. CPU Cycles Explained

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/cpu-cycles-explained-69958237/) · 10:02

### 总结

本课解释 CPU 周期这一基础概念,以及它如何决定软件的执行速度。
它涵盖了晶体振荡器的机制、基于 GHz 计算周期时长的方法,以及流水线(pipelining)和指令级并行(ILP)这类现代 CPU 架构的复杂性。
更关键的是,它强调了内存延迟带来的巨大性能影响,展示了访问 RAM 的缓存未命中如何让 CPU 空转数百个周期,而这对高性能 .NET 开发是至关重要的考量。

### 核心概念

- 晶体振荡器与时钟节拍
- CPU 周期与赫兹(GHz)
- 流水线(取指、译码、执行)
- 指令级并行(ILP)
- 内存延迟层级(L1、L2、L3、RAM)
- 缓存未命中的代价

### 课程笔记

CPU 的运转基于一个晶体振荡器,它发出规律的脉冲,也就是"时钟节拍"。
每一次节拍代表一个周期。
例如,一个 4 GHz 的 CPU 每秒执行 40 亿个周期。
在这样的处理器里,单个周期大约持续 0.25 纳秒。
这个时长短到难以想象,大致相当于光传播约 7.5 厘米所需的时间。

在现代处理器上,要准确判断一条指令需要多少个周期是很复杂的。
不像指令时序固定的老式 CPU,现代架构会利用流水线和并行。
流水线包含三个主要阶段:Fetch(从内存取出指令)、Decode(解析指令以判断它涉及算术逻辑单元还是数据存储),以及 Execute(执行运算)。
虽然 "write back" 发生在执行之后,用于存放结果,但严格来说它被视为最终结果,而不是流水线本身的一个阶段。

指令级并行(ILP)让性能预测变得更加复杂。
因为 CPU 可以乱序执行或并行执行指令,一串指令的周期数并不是简单相加的关系。
例如,十条各需五个周期的指令,并不一定就意味着 50 个周期的执行时间。
由于 CPU 会重叠执行各条指令的不同阶段,有效吞吐率被提高了。

对性能而言最关键的因素,往往不是指令本身,而是数据所在的位置。
从 CPU 寄存器或缓存中访问数据,比访问 RAM 要快得多:

- **L1 Cache**:3–4 个周期(约 1 ns)
- **L2 Cache**:10–12 个周期
- **L3 Cache**:40–70 个周期
- **RAM**:100–300 个周期

如果一个 C# 应用遇到缓存未命中、必须从 RAM 取数据,那么 CPU 可能为了一个字节就空等多达 300 个周期。
高性能编程的重点在于让缓存命中率最大化、确保内存对齐正确,从而把这些空转时间降到最低,让处理器保持在有效工作状态。

## 3. User Mode vs Kernel Mode on CPU

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/user-mode-vs-kernel-mode-on-cpu-69958238/) · 14:27

### 总结

本课探讨 CPU 上 User Mode 与 Kernel Mode 的根本区别,解释操作系统如何保护硬件资源免受用户应用程序侵扰。
理解这两种模式对高性能 .NET 开发至关重要,因为每一次 I/O 操作,比如从磁盘读取或发送网络数据包,都需要从 User Mode 切换到 Kernel Mode。
通过掌握 CPU 如何利用硬件标志位、中断以及内存管理单元(MMU)来管理这些切换,开发者就能更好地理解高吞吐场景下系统调用和硬件交互所带来的开销。

### 核心概念

- **User Mode (Ring 3)**:受限的执行环境,常规应用程序(Chrome、Excel、C# 应用)在其中运行。
- **Kernel Mode (Ring 0)**:特权模式,允许 CPU 执行任何指令、访问任何硬件地址。
- **CPU Mode Flag**:硬件上的一个物理位,决定处理器当前的特权级别。
- **System Calls (Syscalls)**:用户应用请求操作系统执行特权任务的接口。
- **Interrupts and Timers**:让操作系统能从应用程序手中重新夺回控制权并执行上下文切换的硬件机制。
- **Memory Management Unit (MMU)**:一个强制执行内存保护的硬件组件,确保用户应用无法访问内核内存。
- **Drivers**:运行在 Kernel Mode 下的软件组件,负责让操作系统与特定硬件设备之间能够通信。

### 课程笔记

虽然 C# 提供了 string 和托管对象这类高层抽象,让开发变得轻松,但这些抽象是有性能代价的,比如垃圾回收。
要在 1 Billion Row Challenge 这类挑战中达到极致性能,就必须理解底层硬件和操作系统是如何管理执行的。

CPU 主要有两种运行模式:User Mode 和 Kernel Mode。
这一区分由 CPU 硬件上的一个物理位标志控制。
当该标志置为 1 时,CPU 处于 Kernel Mode(或称特权模式),可以运行任何指令。
当标志为 0 时,CPU 处于 User Mode,特权指令受到限制。
为了保证系统稳定性和安全性,大多数应用程序运行在 User Mode 下。

#### Hardware Interaction and the MMU

用户应用程序不能直接访问磁盘、GPU 或网卡这类 I/O 设备。
相反,它们必须请求操作系统来执行这些动作。
这项保护由内存管理单元(MMU)强制执行。
MMU 是负责管理内存访问的硬件单元;它为操作系统和特权指令保留了一块受保护的 RAM 区域。
如果用户应用能够直接访问 MMU,它就可以翻转 CPU 的模式标志位,从而绕过所有安全限制。

#### Interrupts and Context Switching

CPU 利用中断和定时器来管理应用程序的执行。
例如,如果一个 C# 应用进入了无限循环,CPU 会用一个基于时钟周期的物理定时器来触发中断。
这让操作系统能够停止该循环的执行并执行上下文切换,确保没有任何单个应用能够独占处理器。

#### System Calls and OS APIs

当应用程序需要执行读取文件这样的操作时,它会调用一个 OS API。
在 Windows 上,这可能是 `ReadFile` 或 `CreateProcess`;在 Linux 上,可能是 `read` 或 `fork`。
这些高层框架调用最终会落到低层指令上,触发一个系统中断,把 CPU 切换到 Kernel Mode。

```rust
fn open(filename: String): Result<...>{ 
    mov rax, 2
    lea rdi, [rel filename]
    mov rsi, 0
    mov rdx, 0
    int 0x80
    ret
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/user-mode-vs-kernel-mode-on-cpu-69958238/?t=400)

上面的代码演示了一次函数调用如何被翻译成汇编指令。
`int 0x80` 指令是一个软中断,它通知内核接管并执行所请求的特权操作。

```rust
fn open(filename: String): Result<...>{ 
    mov rax, 2
    lea rdi, [rel filename]
    mov rsi, 0
    mov rdx, 0
    int 0x80
    ret
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/user-mode-vs-kernel-mode-on-cpu-69958238/?t=460)

#### Drivers and Kernel Privileges

虽然常规应用程序被限制在 User Mode,硬件驱动却运行在 Kernel Mode。
因为操作系统本身并不知道如何与每一种具体型号的 GPU 或网卡通信,它依赖这些驱动充当中间人。
由于驱动运行在 Kernel Mode,其中的 bug(比如安全软件或显卡驱动里出现过的那些)可能导致系统级的故障,例如蓝屏死机(BSOD)。

理解这条从应用程序到 OS API 再到 Kernel Mode 切换的流程,对优化 I/O 密集型任务是必不可少的,因为这些模式之间的切换会引入延迟,而这种延迟可以通过内存映射文件这类技术来减少。

## 4. Cache Hit vs Cache Miss Tests

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/cache-hit-vs-cache-miss-tests-69958239/) · 13:05

### 总结

本课通过在不同数据规模上对顺序访问和随机访问做基准测试,探讨 CPU 缓存命中与缓存未命中对性能的影响。
它演示了现代 CPU 如何利用缓存行(通常是 64 字节)和硬件预取器来优化顺序读取,以及为什么对大数据集的随机访问会因为频繁往返 RAM 而导致显著的性能下降。
测试结果还突出了一点:由于缓存行预取的高效,对 RAM 的顺序访问实际上可以比对 CPU 缓存的随机访问更快。

### 核心概念

- **CPU Cache Hierarchy**:现代 CPU 使用 L1、L2 和 L3 缓存,其延迟各不相同(L1 约 1ns 到 L3 约 40ns),用来弥合与 RAM(约 100ns)之间的差距。
- **Cache Lines**:CPU 以 64 字节为块获取数据;访问一个字节会自动把它周围的 63 个字节一起带入缓存。
- **Hardware Prefetching**:CPU 会识别顺序访问模式,并在后续缓存行被请求之前就从 RAM 预先加载它们。
- **Spatial Locality**:顺序访问数据能最大化利用每一次取回的缓存行。
- **Random Access Penalty**:随机访问模式会让预取器失效,并且常常导致缓存未命中,迫使程序付出昂贵的主内存往返。

### 课程笔记

CPU 与内存之间的关系是软件性能的一个根本瓶颈。
每当 CPU 需要处理数据或指令时,它都必须从内存中取出它们并放入自己的寄存器。
为了把等待数据的时间降到最低,CPU 使用了多层缓存体系。
这个过程中的一个关键机制是"缓存行"。
当 CPU 访问某个特定内存位置时,它会取回一个连续的 64 字节块。
这一行为假定了空间局部性,也就是:如果你需要某一份数据,你很可能也需要紧随其后的那些数据。

为了量化这些效应,我们实现一个基准测试,在两种不同数据规模上比较顺序访问和随机访问。
"Small" 数据集大小为 32MB,以便舒适地放进 64MB 的 L3 缓存;而 "Large" 数据集为 256MB,以确保它超出缓存并强制访问 RAM。

```csharp
[MemoryDiagnoser]
[SimpleJob(warmupCount: 1, iterationCount: 3)]
[BenchmarkCategory("Level04", "MemoryAccess")]
public class SequentialVsRandomAccessBenchmark
{
    // Small: Fits in L3 Cache including indices
    // 4M ints = 16MB data + 16MB indices = 32MB total (half of 64MB L3, safe margin)
    private const int SmallSize = 4 * 1024 * 1024;

    // Large: Far exceeds L3 Cache
    // 32M ints = 128MB data + 128MB indices = 256MB total (4x L3, guaranteed RAM)
    private const int LargeSize = 32 * 1024 * 1024;

    private int[] smallData = [];
    private int[] largeData = [];

    private int[] smallRandomIndices = [];
    private int[] largeRandomIndices = [];
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/cache-hit-vs-cache-miss-tests-69958239/?t=160)

准备工作包括初始化两个整型数组,并生成打乱顺序的索引数组,用 Fisher-Yates 洗牌来模拟随机访问。

```csharp
[GlobalSetup]
public void Setup()
{
    var rng = new Random(42);

    // Small data (32MB - fits in L3)
    smallData = new int[SmallSize];
    for (var i = 0; i < SmallSize; i++)
        smallData[i] = rng.Next();

    // Large data (128MB - exceeds L3)
    largeData = new int[LargeSize];
    for (var i = 0; i < LargeSize; i++)
        largeData[i] = rng.Next();

    // Generate random indices
    smallRandomIndices = GenerateRandomIndices(SmallSize, rng);
    largeRandomIndices = GenerateRandomIndices(LargeSize, rng);
}

private static int[] GenerateRandomIndices(int size, Random rng)
{
    var indices = new int[size];
    for (var i = 0; i < size; i++)
        indices[i] = i;

    // Fisher-Yates shuffle
    for (var i = size - 1; i > 0; i--)
    {
        var j = rng.Next(i + 1);
        (indices[i], indices[j]) = (indices[j], indices[i]);
    }

    return indices;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/cache-hit-vs-cache-miss-tests-69958239/?t=430)

`SmallSequential` 测试代表了可能达到的最快访问模式。
数据能放进 L3 缓存,而顺序访问让硬件预取器可以高效地加载缓存行。

```csharp
[Benchmark]
[BenchmarkCategory("Sequential", "CacheHit")]
public long SmallSequential()
{
    long sum = 0;
    var data = smallData;

    for (var i = 0; i < data.Length; i++)
        sum += data[i];

    return sum;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/cache-hit-vs-cache-miss-tests-69958239/?t=460)

在 `SmallRandom` 里,数据仍然驻留在 CPU 缓存中,但随机访问模式让预取器无法帮上忙。
这导致相比顺序版本有显著的性能下降,尽管完全不需要访问 RAM。

```csharp
[Benchmark]
[BenchmarkCategory("Random", "CacheHit")]
public long SmallRandom()
{
    long sum = 0;
    var data = smallData;
    var indices = smallRandomIndices;

    for (var i = 0; i < indices.Length; i++)
        sum += data[indices[i]];

    return sum;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/cache-hit-vs-cache-miss-tests-69958239/?t=535)

对于大数据集,`LargeSequential` 必须访问 RAM。
不过,因为它是顺序的,CPU 依然会取回 64 字节的缓存行。
这意味着每 16 个整数(假定 int 为 4 字节),CPU 只需要去 RAM 一次,而预取器可以在后台取下一行来隐藏这段延迟。

```csharp
[Benchmark(Baseline = true)]
[BenchmarkCategory("Sequential", "RAMAccess")]
public long LargeSequential()
{
    long sum = 0;
    var data = largeData;

    for (var i = 0; i < data.Length; i++)
        sum += data[i];

    return sum;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/cache-hit-vs-cache-miss-tests-69958239/?t=565)

最慢的场景是 `LargeRandom`。
因为数据集对缓存来说太大,而访问模式又无法预测,CPU 几乎每次访问都必须完整往返一次 RAM。

```csharp
[Benchmark]
[BenchmarkCategory("Random", "RAMAccess")]
public long LargeRandom()
{
    long sum = 0;
    var data = largeData;
    var indices = largeRandomIndices;

    for (var i = 0; i < indices.Length; i++)
        sum += data[indices[i]];

    return sum;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/cache-hit-vs-cache-miss-tests-69958239/?t=580)

基准测试结果印证了这些理论预期。
值得注意的是,`LargeSequential`(访问 RAM)实际上比 `SmallRandom`(访问缓存)更快,因为在顺序模式下缓存行和预取的效率,超过了随机访问时 RAM 的原始延迟劣势。

```text
| Method          | Mean       |
|---------------- |-----------:|
| SmallSequential |   1.087 ms |
| LargeSequential |   8.538 ms |
| SmallRandom     |  18.753 ms |
| LargeRandom     | 210.469 ms |
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/cache-hit-vs-cache-miss-tests-69958239/?t=610)

要达到极致性能,软件的设计应当让被频繁访问的数据留在 CPU 缓存里,并偏向顺序访问模式,以便利用预取和缓存行这类硬件优化。

## 5. Working with Pointers in C#

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/working-with-pointers-in-csharp-69958240/) · 31:25

### 总结

本课探讨如何在 C# 中使用指针,通过绕开标准的托管数据处理来实现高性能的内存访问。
它涵盖了从托管 string 到 unsafe 指针操作的过渡、用 fixed 关键字固定对象以防止垃圾回收器干扰的必要性,以及指针运算的机制。
此外,它还把原始指针与 ReadOnlySpan<T> 做了对比,指出在 1 Billion Row Challenge 的语境下,何时该用哪一种才能兼顾性能和安全。

### 核心概念

- **User Mode vs. Kernel Mode**:标准 I/O 涉及用户模式与内核模式之间的上下文切换,这在计算上是昂贵的。指针允许直接访问内存,从而避免不必要的数据拷贝。
- **Managed Data**:在 .NET 中,string 是堆上的托管对象,带有头部信息(方法表、长度),并且是不可变的。
- **Unsafe Blocks**:要使用指针,项目必须在 `.csproj` 文件中启用 `AllowUnsafeBlocks`,并且代码必须包裹在 `unsafe` 块里。
- **Memory Pinning (`fixed`)**:垃圾回收器(GC)可能在堆压缩期间移动对象。`fixed` 关键字把对象固定在原地,为指针提供一个稳定的内存地址。
- **Pointer Arithmetic**:指针可以自增或自减。在 C# 中,`char*` 的运算按 2 字节步进(UTF-16),而 `byte*` 按 1 字节步进。
- **Null Termination**:.NET 的 string 天然并不以 null 结尾,但一个 `fixed char*` 指针在被固定的字符串末尾包含一个 null 终止符。
- **ReadOnlySpan<T>**:指针的现代替代品,提供高性能同时兼具安全性(边界检查),并且不需要显式固定。

### 课程笔记

#### Project Configuration

要使用指针,C# 项目必须显式允许 unsafe 代码。
这是在项目文件里配置的:

```xml
<PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <AllowUnsafeBlocks>true</AllowUnsafeBlocks>
</PropertyGroup>
```

#### 1. Managed String Memory Model

在标准的 .NET 开发中,string 是托管对象。
它们不可变,并存放在堆上。
每一个字面量或每一次修改都会产生一次新的分配。
内存中的一个 string 对象由对象头(8 字节)、方法表指针(8 字节)、长度(4 字节),以及字符数据(UTF-16 下每个字符 2 字节)组成。

```csharp
Section("1 — Managed String: Memory Model");
{
    // In .NET, strings are immutable and stored on the heap.
    // Every "" literal or operation creates a new heap allocation.
    string s = "Hello"; // 0x00001

    // length + 2 bytes per character (UTF-16)
    // Object header (8) + Method table ptr (8) + Length (4) + chars (n×2)
    Console.WriteLine($"  Value      : {s}");
    Console.WriteLine($"  Length     : {s.Length}  (character count)");
    Console.WriteLine($"  [2]        : {s[2]}  — indexer, no new allocation");
    
    // s[0] = 'X'  → ❌ compile error — string is immutable
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/working-with-pointers-in-csharp-69958240/?t=280)

#### 2. Pinning with the `fixed` Keyword

垃圾回收器可能在压缩期间移动堆上的对象。
要安全地使用指针,必须把对象固定住,以防 GC 改变它的内存地址。
`fixed` 语句会在该块的执行期间固定住对象。

```csharp
Section("2 — fixed: Preventing GC from Moving the String");
{
    // GC can move objects on the heap (compaction).
    // To take a pointer, the object must be pinned against GC movement.
    // During the 'fixed' block, GC cannot move this object.

    string s = "Hello"; // 0x00001

    unsafe
    {
        fixed (char* ptr = s)   // s is pinned, ptr = address of first char
        {
            Console.WriteLine($"  ptr address: 0x{(nint)ptr:X}");
            Console.WriteLine($"  ptr[0]     : {ptr[0]}   (= s[0])");
            Console.WriteLine($"  ptr[1]     : {ptr[1]}   (= s[1])");
            
            // Arithmetic: ptr + n → n characters ahead (each 2 bytes)
            Console.WriteLine($"  *(ptr+2)   : {*(ptr + 2)}   (= s[2])");
        }
        // Block ended → 'fixed' released, GC can move again
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/working-with-pointers-in-csharp-69958240/?t=505)

#### 3. Pointers vs. Indexers

托管索引器包含自动的边界检查,这保证了安全,但引入了轻微的开销。
指针不执行边界检查;访问分配范围之外的内存很可能会让应用崩溃或造成访问违规。
不过,C# 里的 `fixed char*` 指针是以 null 结尾的,因此可以做 C 风格的向前扫描。

```csharp
Section("3 — String Character Reading with Pointer (vs indexer)");
{
    // Indexer: bounds check on every access  → safe but slight overhead
    // Pointer : no bounds check              → fast but programmer is responsible
    string s = "Hello, World!";

    unsafe
    {
        fixed (char* ptr = s)
        {
            // ---- Forward scan with pointer ----
            Console.Write("  With pointer: ");
            char* p = ptr;
            while (*p != '\0')      // .NET strings are not null-terminated, but
            {                       // fixed char* includes a null terminator
                Console.Write(*p);
                p++;                // advances 2 bytes (sizeof(char))
            }
            Console.WriteLine();
            
            // ---- Comparison with indexer ----
            Console.Write("  With indexer: ");

            for (int i = 0; i < s.Length; i++)
                Console.Write(s[i]);

            Console.WriteLine();
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/working-with-pointers-in-csharp-69958240/?t=1150)

#### 4. Pointer Arithmetic and `sizeof`

理解类型的大小对指针运算至关重要。
在 C# 中,一个 `char` 是 2 字节(UTF-16)。
计算两个指针之间的距离时,结果取决于指针的类型。
两个 `char*` 指针相减得到的是以字符为单位的距离,而先把它们转换成 `byte*` 再相减,得到的是以字节为单位的距离。

```csharp
Section("4 — Pointer Arithmetic: sizeof and Address Difference");
{
    unsafe
    {
        Console.WriteLine($"  sizeof(char)  : {sizeof(char)}  byte  (UTF-16)");
        Console.WriteLine($"  sizeof(byte)  : {sizeof(byte)}  byte");
        Console.WriteLine($"  sizeof(int)   : {sizeof(int)}  byte");
        Console.WriteLine($"  sizeof(long)  : {sizeof(long)}  byte");

        string s = "ABCD";
        fixed (char* ptr = s)
        {
            char* pA = &ptr[0];
            char* pD = &ptr[3];

            // Pointer difference: how many 'char' apart?
            long charDistance = pD - pA;
            // Byte difference
            long byteDistance = (byte*)pD - (byte*)pA;

            Console.WriteLine($"\n  &s[0] = 0x{(int)pA:X}");
            Console.WriteLine($"  &s[3] = 0x{(int)pD:X}");
            Console.WriteLine($"  Diff  = {charDistance} char  = {byteDistance} bytes");
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/working-with-pointers-in-csharp-69958240/?t=1240)

#### 5. String Mutation (Dangerous)

虽然在托管的 C# 中 string 是不可变的,指针却允许直接操作内存,从而绕开这一限制。
这极其危险,因为它可能破坏字符串驻留(interning,即多个引用指向同一个字面量),并导致未定义行为。

```csharp
Section("5 — String Mutation: Modifying with Pointer (Dangerous!)");
{
    // String is immutable — but we can force it with unsafe.
    // ⚠️ This should NEVER be used in real code:
    //    • String interning breaks (same literal changes everywhere)
    //    • Can lead to undefined behavior
    // Shown only to answer the question "what happens?"

    string s = new string("Harmless");   // new string() → non-interned copy

    Console.WriteLine($"  Before: {s}");

    unsafe
    {
        fixed (char* ptr = s)
        {
            ptr[0] = 'D';   // 'H' → 'D'
            ptr[1] = 'a';   // 'a' → 'a'
            ptr[2] = 'n';   // 'r' → 'n'
        }
    }

    Console.WriteLine($"  After : {s}");   // "Danmless" — string was mutated
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/working-with-pointers-in-csharp-69958240/?t=1495)

#### 6. Modern Alternatives: ReadOnlySpan<T>

`ReadOnlySpan<char>` 提供了一种安全的方式来获得类似指针的性能。
它与 GC 兼容,不需要固定(`fixed`),也不需要 `unsafe` 块。
它支持零分配切片,因此非常适合解析。

```csharp
Section("6 — ReadOnlySpan<char> vs Pointer: Modern Alternative");
{
    // Safe way to achieve similar performance without using pointers.
    // Span<T> / ReadOnlySpan<T>:
    //   • Bounds check present (in debug) — but JIT optimizes most of them away
    //   • GC-compatible, no 'fixed' required
    //   • No unsafe block required

    string s = "Hello, World!";

    ReadOnlySpan<char> span = s.AsSpan();
    ReadOnlySpan<char> sub = span.Slice(7, 5);   // "World" — no allocation

    Console.WriteLine($"  Full string: {s}");
    Console.WriteLine($"  Span[7..12]: {sub}");
    Console.WriteLine($"  span[0]    : {span[0]}");

    // When you want a pointer:
    unsafe
    {
        fixed (char* ptr = span)
            Console.WriteLine($"  span ptr   : 0x{(int)ptr:X}  (same address as s's first char)");

        fixed (char* ptr = s)
            Console.WriteLine($"  s    ptr   : 0x{(int)ptr:X}");
    }
    // → Both addresses are the same; Span doesn't copy, it points to the same memory.
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/working-with-pointers-in-csharp-69958240/?t=1720)

#### Summary: When to Use What?

```csharp
Section("Summary: When to Use What?");
Console.WriteLine("""
  string indexer    → General use. Safe, readable.
  ReadOnlySpan<T>   → Substrings, parsing, zero-allocation reads. ✅ Recommended
  fixed + char*     → Interop, unsafe parsing, max performance critical path.
  string mutation   → ❌ Never — only to understand what happens.
""");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/working-with-pointers-in-csharp-69958240/?t=1825)

## 6. What is Hashing?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/what-is-hashing-69958241/) · 17:43

### 总结

哈希是一项基础技术,它把任意数据映射成固定大小的整数值,从而让 HashSet 和 Dictionary 这类数据结构达到 O(1) 的性能。
通过利用数组索引,这些结构为查找、插入和更新操作提供了接近常数的时间复杂度。
在 1 Billion Row Challenge 中,选择 FNV-1a 这样健壮的哈希算法是必要的,它能确保 413 个不同的气象站在内存中均匀分布,避免在处理十亿条测量数据时因哈希冲突而导致性能退化。

### 核心概念

* Data Structure Selection:根据操作需求在链表、二分查找和哈希表之间做选择。
* Array Indexing:实现 O(1) 查找的基础机制。
* Hash Functions:从任意类型的数据生成整数输出的不可逆算法。
* Modular Arithmetic:把很大的哈希值收进后备数组的范围之内。
* Collisions:不同的键产生同一个数组索引的情况。
* Collision Resolution:包括线性探测、二次探测、双重哈希和分桶(链表)在内的各种技术。
* Distribution Quality:数据均匀铺开对于在大规模下维持性能的重要性。

### 课程笔记

数据结构的选择取决于具体实现的需求,比如是否需要快速插入、删除或查找。
链表擅长插入,二分查找在有序列表上提供高效的查找,而哈希则通过利用数组索引带来了一个达到 O(1) 性能的独特机会。

在整数数据范围有限的简单场景里(例如 1 到 10),值可以直接存放在与之对应的数组索引上。
然而,当面对更大的范围或者像字符串这类非整数数据(例如 "Istanbul"、"Hamburg")时,就需要一个哈希函数把输入映射成整数。
与编码不同,哈希是一个单向过程;无法从哈希值恢复出原始值。

要把一个很大的哈希值放进较小的后备数组,就要用到取模运算。
例如,哈希值 209 映射到大小为 10 的数组上,会落在索引 9(209 % 10 = 9)。
这种映射不可避免地会带来冲突,即不同的输入产生相同的索引。
冲突解决策略包括探测(线性、二次或双重哈希)以及分桶。
.NET 默认的 Dictionary 实现采用分桶,每个数组索引指向一个存放条目的链表。

```csharp
Section("6 - ReadOnlySpan<char> vs Pointer: Modern Alternative");

Section("Summary: When to Use What?");
Console.WriteLine("""
Indexer     -> General use. Safe, readable.
Span<T>    -> Substrings, parsing, zero-allocation reads. [x] Recommended
char*      -> Interop, unsafe parsing, max performance critical path.
Implementation -> [X] Never - only to understand what happens.
""");

void Section(string title)
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/what-is-hashing-69958241/?t=820)

在 1 Billion Row Challenge 中,字典的性能至关重要。
每一次查找、插入和更新都依赖于哈希算法的效率。
如果分布很差,查找就会变慢,因为系统必须在冲突簇或链表里逐个遍历。

```csharp
// allocation
var localStats = new Dictionary<string, StationStats>(GlobalConstants.ExpectedStationCount);

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

var lineStart = 0;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/what-is-hashing-69958241/?t=880)

在合并线程本地结果时,会用 TryGetValue 方法检查是否已存在该气象站的数据。
高质量的哈希能确保这次查找几乎是瞬时完成的。

```csharp
var finalResults = new Dictionary<string, StationStats>(GlobalConstants.ExpectedStationCount);

foreach (var localStats in threadLocalResults)
{
    if (localStats == null)
        continue;

    foreach (var (stationName, stats) in localStats)
    {        if (!finalResults.TryGetValue(stationName, out var existingStats))
        {
            existingStats = new StationStats();
            finalResults[stationName] = existingStats;
        }

        existingStats.Merge(stats);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/what-is-hashing-69958241/?t=895)

对 1BRC 而言,气象站名称在十亿行里反复出现,因此 FNV-1a 算法因其出色的分布性和"雪崩效应"(输入的微小变化会导致哈希值大不相同)而更受青睐。
一个好的哈希算法让字典能准确知道某个气象站的数据在哪里,从而不必检查相邻索引或遍历很长的桶。

```csharp
static int ComputeHash(ReadOnlySpan<byte> span)
{
    unchecked
    {
        var hash = (int)2166136261;
        foreach (var b in span)
        {
            hash ^= b;
            hash *= 16777619;
        }

        return hash;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/what-is-hashing-69958241/?t=1049)

## 7. Memory Mapped Files

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/memory-mapped-files-69958242/) · 6:44

### 总结

内存映射文件(MMF)提供了一种高性能机制,它把文件内容直接映射到应用程序的虚拟内存地址空间,以此访问大型数据集。
这种做法利用了操作系统的原生能力,比如 Linux 上的 mmap,把传统基于流的 I/O 所带来的开销降到最低。
通过绕开用户模式与内核模式之间的频繁切换,并利用操作系统的虚拟内存缓存,MMF 让 .NET 应用可以把磁盘上的数据当作已经在 RAM 里一样来使用。
当它与 C# 指针和 unsafe 块结合时,MMF 能够实现极快的数据处理,而这对应对 1 Billion Row Challenge 的规模是必不可少的。

### 核心概念

- Windows 和 Linux 两者都为内存映射提供了原生支持。
- 把磁盘文件映射到虚拟内存,从而免去显式的 Read 调用。
- 最小化内核模式与用户模式之间的上下文切换。
- 操作系统级缓存:即使应用程序终止,文件仍留在虚拟内存中。
- 使用指针和 unsafe C# 代码实现高性能数据访问。
- 使用非托管内存访问时,需要手动做边界检查。

### 课程笔记

要在 1 Billion Row Challenge 中达到极致性能,我们必须优化数据从磁盘到 CPU 的传输方式。
之前的课程已经确立了 CPU 周期、时钟频率,以及内核模式与用户模式之别的重要性。
我们也探讨过 CPU 缓存行(L1、L2 和 L3),以及指针如何让我们在不把数据拷贝到用户侧的情况下引用它。
这一点很关键,因为我们希望在最终处理步骤之前都不去解析数十亿个字符串。

内存映射文件(MMF)通过让操作系统把文件当作一段内存来管理,解决了数据访问的瓶颈。
这是现代操作系统的原生特性,C# 中无需外部包即可支持。
在 Linux 上,它通过 mmap 系统调用实现。

```c
#include <sys/mman.h>

void *mmap(void *addr, size_t length, int prot, int flags,
           int fd, off_t offset);
int munmap(void *addr, size_t length);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/memory-mapped-files-69958242/?t=145)

当应用程序请求一个内存映射文件时,它把文件路径交给操作系统。
在首次调用时,操作系统会把文件从磁盘拷贝进内存。
这一操作发生在内核模式中。
虽然首次访问受物理磁盘读取速度的限制,操作系统会把文件缓存在它的虚拟内存里。
之后的访问,哪怕应用被关闭并重启,都会直接从内存中取回数据,这比磁盘 I/O 要快得多。

在传统的基于流的处理中,逐行读取文件会涉及反复的 API 调用,迫使 CPU 在用户模式和内核模式之间来回切换。
每次调用取回一个缓冲区的数据,随后再被拷贝进应用程序的内存空间。
这个过程对处理十亿行数据来说是低效的。
使用 MMF 之后,数据驻留在内存中,应用程序可以用指针直接访问它。
这绕过了标准的 I/O 管道,减少了在真正必要之前把数据作为托管对象带到用户侧的需要。

因为 MMF 允许直接访问内存,它们在 C# 中通常被用在 unsafe 块里。
这就移除了 .NET 运行时自动边界检查这道安全网。
在用指针操作内存映射数据时,开发者要自己负责实现手动的边界检查,以防内存损坏或访问违规。
向 unsafe 代码和内存映射的这次转变,是本挑战高性能实现的前置条件。

## 8. Let's Start Coding

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/let-s-start-coding-69958243/) · 12:59

### 总结

本课开启 Level 4 的实现,聚焦于 Memory Mapped Files(MMF)和内存效率。
它演示如何从堆分配的 class 过渡到栈分配的 struct,以此把垃圾回收器的开销降到最低。
此外,它引入了一种"无字符串"处理策略,把气象站名称作为原始字节或哈希来处理,只在最终输出阶段才转换成字符串。
技术上的准备工作包括使用 unsafe C# 获取指向文件的直接内存指针,从而实现绕开传统基于流的 I/O 的高性能数据扫描。

### 核心概念

- **Memory Mapped Files (MMF)**:把文件直接映射进进程的虚拟地址空间,以实现高性能访问。
- **Struct vs. Class**:使用栈分配的 `struct` 类型,消除堆分配并减少垃圾回收(GC)压力。
- **String-less Processing**:把数据作为原始字节或哈希来处理,避免托管 string 对象的开销。
- **SIMD vs. Scalar**:理解为什么像 `Span.IndexOf`(内部使用 SIMD)这类内置方法会胜过手写的标量指针循环。
- **Unsafe Pointers**:配合 `MemoryMappedViewAccessor` 使用 `byte*` 指针来直接操作内存。
- **UTF-8 BOM Handling**:检测并跳过文件开头的字节序标记(`0xEF, 0xBB, 0xBF`)。

### 课程笔记

#### Performance Analysis of Search Methods

在实现新架构之前,先理解各种查找和解析方法的性能特征是至关重要的。
基准测试表明,对于亚纳秒级的操作,JIT 的内联决策是首要因素。
例如,在非常短的字符串上 `string.IndexOf` 往往比 `Span.IndexOf` 更快,因为 JIT 能识别它并激进地内联,而 `AsSpan()` 又多加了一层方法调用。

不过,随着字符串长度增加,经过 SIMD 优化的方法(比如 `Span.IndexOf` 所用的那些)会显著胜过手写的标量指针循环。
手写循环一次只检查一个字符,而 SIMD(AVX2/SSE)可以在一次迭代里检查 16 或 32 个字符。

```csharp
/*
 * | Method        | Line                    | Mean       | Ratio |
 * |-------------- |------------------------ |-----------:|------:|
 * | StringIndexOf | A;0.0                   |  0.3384 ns |  1.00 |
 * | SpanIndexOf   | A;0.0                   |  0.7801 ns |  2.31 |  ← no wait!
 * | PointerScan   | A;0.0                   |  0.5436 ns |  1.61 |
 * | SpanParse     | A;0.0                   | 18.1852 ns | 53.78 |
 * | PointerParse  | A;0.0                   | 18.3355 ns | 54.22 |
 * |               |                         |            |       |
 * | StringIndexOf | Ouagadougou;32.4        |  1.1006 ns |  1.00 |
 * | SpanIndexOf   | Ouagadougou;32.4        |  1.1844 ns |  1.08 |
 * | PointerScan   | Ouagadougou;32.4        |  2.8255 ns |  2.57 |  <- gap starts opening
 * | SpanParse     | Ouagadougou;32.4        | 26.9212 ns | 24.46 |
 * | PointerParse  | Ouagadougou;32.4        | 26.4674 ns | 24.05 |
 * |               |                         |            |       |
 * | StringIndexOf | Petropavlovsk...;-12.7  |  1.1006 ns |  1.00 |
 * | SpanIndexOf   | Petropavlovsk...;-12.7  |  1.1072 ns |  1.01 |
 * | PointerScan   | Petropavlovsk...;-12.7  |  6.0376 ns |  5.49 |  <- 5.5x slower!
 * | SpanParse     | Petropavlovsk...;-12.7  | 28.9211 ns | 26.28 |
 * | PointerParse  | Petropavlovsk...;-12.7  | 31.2659 ns | 28.41 |
 */
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/let-s-start-coding-69958243/?t=25)

#### Memory Management with Structs

为了把 GC 开销降到最低,应用把气象站统计数据从 `class` 改成了 `struct`。
这确保数据存活在栈上或被内联进字典里,免去 GC 管理数百万个小对象的负担。

```csharp
public struct StationStatsStruct
{
    public double Min;
    public double Max;
    public double Sum;
    public long Count;

    public static StationStatsStruct Create() => new()
    {
        Min = double.MaxValue,
        Max = double.MinValue,
        Sum = 0,
        Count = 0
    };

    public readonly double Mean => Count > 0 ? Sum / Count : 0;

    public void Update(double temperature)
    {
        if (temperature < Min) 
            Min = temperature;

        if (temperature > Max) 
            Max = temperature;

        Sum += temperature;
        Count++;
    }

    public void Merge(in StationStatsStruct other)
    {
        if (other.Min < Min) 
            Min = other.Min;

        if (other.Max > Max) 
            Max = other.Max;

        Sum += other.Sum;
        Count += other.Count;
    }

    public readonly override string ToString() => $"{Min:F1}/{Mean:F1}/{Max:F1}";
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/let-s-start-coding-69958243/?t=190)

#### String-less Processing Strategy

在之前的级别里,每一行都被解析成一个托管 `string`。
在 Level 4 中,我们通过把气象站名称视为原始字节或整数(哈希)来避免这一点。
既然十亿行文件里只有大约 413 个不同的气象站,我们只需要在首次遇到某个气象站时才分配一个字符串。
之后的所有出现,我们都用整数哈希来做字典查找,把处理过程完全保持在内存的"栈侧"。

#### Implementing Memory Mapped Files

内存映射文件让操作系统能高效地处理文件缓存。
首次运行时,操作系统从磁盘读取;之后的运行则直接从内存中的操作系统页缓存里取数据。
要实现这一点,我们创建一个 `MemoryMappedFile` 和一个 `MemoryMappedViewAccessor` 来查看数据。

```csharp
using var mmf = MemoryMappedFile.CreateFromFile(GlobalConstants.FilePath,
                                                FileMode.Open,
                                                null,
                                                0,
                                                MemoryMappedFileAccess.Read);

using var accessor = mmf.CreateViewAccessor(0,
                                            fileSize,
                                            MemoryMappedFileAccess.Read);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/let-s-start-coding-69958243/?t=520)

#### Acquiring Pointers and Initializing Parallelism

在一个 `unsafe` 块里,我们获取指向内存映射视图的原始 `byte*` 指针。
这让指针运算成为可能,而它比基于流的读取更快。
我们还会检查 UTF-8 字节序标记(BOM),以确保从正确的偏移量开始读取。

```csharp
unsafe
{
    byte* basePtr = null;
    accessor.SafeMemoryMappedViewHandle.AcquirePointer(ref basePtr);

    try
    {
        // Skip UTF-8 BOM if present (EF BB BF)
        long dataStart = 0;
        if (fileSize >= 3 && basePtr[0] == 0xEF && basePtr[1] == 0xBB && basePtr[2] == 0xBF)
        {            dataStart = 3;
        }

        var dataSize = fileSize - dataStart;
        var chunkSize = dataSize / threadCount;

        Parallel.For(0, threadCount, threadIndex => 
        {
            // calculate the chunk boundaries (relative to data start)
            var startPos = dataStart + (threadIndex * chunkSize);
            var endPos = (threadIndex == threadCount - 1)
                            ? fileSize
                            : dataStart + ((threadIndex + 1) * chunkSize);
            // ... further processing logic to follow
        });
    }
    finally
    {        accessor.SafeMemoryMappedViewHandle.ReleasePointer();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/let-s-start-coding-69958243/?t=580)

## 9. Continue Coding

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/continue-coding-69958244/) · 17:29

### 总结

本课聚焦于用内存映射文件和指针实现 1 Billion Row Challenge 的核心处理循环。
它涵盖了计算各线程的分块边界、把这些边界对齐到换行符以保证数据完整性,以及在内存中高效扫描分号和换行这类分隔符。
通过使用指针和 ReadOnlySpan<byte>,该实现避免了不必要的分配,并利用 CPU 层面的比较来实现高性能的数据解析。

### 核心概念

- 为并行处理计算分块边界。
- 把分块的起点和终点对齐到换行符(\n)。
- 使用指针(byte*)手动扫描分隔符。
- 理解 CPU 侧与用户侧的内存访问之别。
- 从指针出发用 ReadOnlySpan<byte> 做零分配切片。

### 课程笔记

实现的第一步是把内存映射文件划分成若干块以便并行处理。
dataStart 会被调整以跳过存在的 UTF-8 BOM。
Parallel.For 循环里的每个线程,都会根据总数据大小和可用处理器数量,被分配一个分块大小。

```csharp
var dataSize = fileSize - dataStart;
var chunkSize = dataSize / threadCount;

Parallel.For(0, threadCount, threadIndex =>
{
    // calculate the chunk boundaries (relative to data start)
});
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/continue-coding-69958244/?t=10)

对每个线程都必须计算 startPos 和 endPos。
最后一个线程会被分配文件的剩余部分,以确保整个文件都被处理到。

```csharp
var startPos = dataStart + (threadIndex * chunkSize);
var endPos = (threadIndex == threadCount - 1)
    ? fileSize
    : dataStart + ((threadIndex + 1) * chunkSize);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/continue-coding-69958244/?t=145)

因为分块是按原始大小计算的,它们很可能从某一行的中间开始或结束。
为了修正这一点,除第一个线程外,每个线程都必须通过扫描上一行的换行符,把它的 startPos 前移到下一行的开头。

```csharp
if (startPos > dataStart)
{
    while (startPos < fileSize && basePtr[startPos - 1] != '\n')
    {
        startPos++;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/continue-coding-69958244/?t=235)

同样地,除最后一个线程外,所有线程的 endPos 都必须被调整到当前行的末尾。
另外,确保在 finally 块中释放指针也很关键,以防内存泄漏。

```csharp
if (endPos < fileSize && threadIndex < threadCount - 1)
{
    while (endPos < fileSize && basePtr[endPos - 1] != '\n')
    {
        endPos++;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/continue-coding-69958244/?t=355)

在处理循环内部,每个线程维护一个本地字典来存放统计数据,以及一个记录已处理行数的计数器。
循环从调整后的 startPos 迭代到 endPos。

```csharp
var localStats = new Dictionary<int, (string Name, StationStatsStruct Stats)>();
long localLineCount = 0;
var pos = startPos;

while(pos < endPos)
{
    // Hamburg;23.9
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/continue-coding-69958244/?t=415)

为了提取气象站名称和温度,代码手动扫描分号分隔符。
用指针做这次比较非常高效,因为 JIT 编译器生成的汇编会在 CPU 侧完成比较。
这样就避免了在必要之前把数据带到"用户侧"(托管变量)。

```csharp
var semicolonPos = pos;
while(semicolonPos < endPos && basePtr[semicolonPos] != ';')
{
    semicolonPos++;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/continue-coding-69958244/?t=610)

找到分号之后,代码再扫描换行符,以确定温度值的结束位置。

```csharp
var newLinePos = semicolonPos + 1;
while(newLinePos < endPos && basePtr[newLinePos] != '\n')
{
    newLinePos++;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/continue-coding-69958244/?t=835)

一旦气象站名称的边界被确定下来(在 pos 和 semicolonPos 之间),就创建一个 ReadOnlySpan<byte>。
这个 span 是对内存映射文件的零分配视图。
要把这个 span 用作字典键,它最终会被转换成一个整数哈希。

```csharp
var nameSpan = new ReadOnlySpan<byte>(basePtr + pos, (int)(semicolonPos - pos));
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/continue-coding-69958244/?t=985)

## 10. Hash Comparison

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/hash-comparison-69958245/) · 6:28

本课评估各种哈希算法,以优化 1 Billion Row Challenge 中字典查找的性能。
通过把 .NET 默认的 String.GetHashCode()(Marvin32)与 Simple Multiplicative、FNV-1a 这类自定义的基于字节的实现做对比,本课演示了避免堆分配和确保哈希分布均匀如何显著缩短执行时间。
最终 FNV-1a 因其更出色的雪崩效应和在大规模下稳定的表现而被选中,与基于字符串的哈希相比,把处理时间几乎缩短了一半。

### 核心概念

*   **Allocation Overhead**:仅仅为了调用 `GetHashCode()` 而把 `ReadOnlySpan<byte>` 转换成 `string` 所付出的性能代价。
*   **Marvin32**:.NET 默认的哈希算法,被 `String.GetHashCode()` 和通用集合所使用。
*   **Hash Distribution**:哈希值在可用空间中的均匀程度,它决定了字典桶冲突的数量。
*   **Avalanche Effect**:一种特性,即输入的微小变化(例如气象站名称中的一个字符)会导致哈希值大不相同。
*   **FNV-1a**:一种为字节序列优化的非加密哈希算法,在速度和高质量分布之间取得平衡。

### 课程笔记

1 Billion Row Challenge 的性能严重依赖于字典查找所用哈希算法的效率。
既然哈希函数会被调用十亿次,哪怕微小的低效或糟糕的分布都会导致显著的性能退化。
为了分析这些影响,一个性能分析器用 413 个不同的气象站名称模拟了一亿次字典操作。

```csharp
using BenchmarkDotNet.Running;
using Benchmarks.Level4_SharedMemory;

// Run hash performance analysis (simulates 100M measurements)
HashDistributionAnalyzer.AnalyzeHashPerformance(100_000_000);

// Or run benchmark
BenchmarkRunner.Run<HashAlgorithmBenchmark>();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/hash-comparison-69958245/?t=10)

该分析器比较三种主要做法:`String.GetHashCode()`、一个简单的乘法哈希,以及 FNV-1a。
这套准备工作模拟了 1BRC 的场景:一小组唯一键(气象站名称)被重复数百万次。

```csharp
public static class HashDistributionAnalyzer
{
    public static void AnalyzeHashPerformance(int measurementCount = 10_000_000)
    {
        Console.WriteLine($"=== Hash Algorithm Performance for Dictionary<int, Stats> ===");
        Console.WriteLine($"Test: {measurementCount:N0} dictionary operations (lookup/insert)");
        Console.WriteLine($"Keys: 413 unique station names (each repeated ~{measurementCount / 413:N0} times)\n");

        var stationNames = GenerateStationNames(413);
        var stationBytes = stationNames.Select(s => Encoding.UTF8.GetBytes(s)).ToArray();

        // Simulate 1BRC: repeated measurements from same stations
        var random = new Random(42);
        var measurementIndices = new int[measurementCount];
        for (int i = 0; i < measurementCount; i++)
        {
            measurementIndices[i] = random.Next(413);
        }

        Console.WriteLine("=== Algorithm 1: String.GetHashCode() with allocation ===");
        Console.WriteLine("    Implementation: Encoding.UTF8.GetString(bytes).GetHashCode()");
        Console.WriteLine("    Issue: Creates string object for every hash computation!");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/hash-comparison-69958245/?t=25)

#### The Default: Marvin32

.NET 默认的哈希算法是 Marvin32,它被 `String.GetHashCode()`、`Dictionary` 和 `HashSet` 使用。
虽然 Marvin32 是一个健壮的通用算法,但它并没有针对 1BRC 的特定约束做优化,因为它通常作用于 string 对象而不是原始字节 span。

```csharp
internal static partial class Marvin
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ComputeHash32(ReadOnlySpan<byte> data, ulong seed) => ComputeHash32(ref MemoryMarshal.GetReference(data), (uint)data.Length, (uint)seed, (uint)(seed >> 32));

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int ComputeHash32(ref byte data, uint count, uint p0, uint p1)
    {
        if (count < 8)
        {
            if (count >= 4) { goto Between4And7BytesRemain; }
            else { goto InputTooSmallToEnterMainLoop; }
        }

        uint loopCount = count / 8;
        do
        {
            // Main loop processing 8 bytes at a time
        } while (--loopCount > 0);
        // ...
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/hash-comparison-69958245/?t=40)

#### The Allocation Problem

在这个语境下使用 `String.GetHashCode()` 的主要问题在于,它要求为每一次测量都把一个 `ReadOnlySpan<byte>` 转换成 `string` 对象。
这导致了海量的堆分配和显著的垃圾回收(GC)开销。

```csharp
private static long TestStringHashCodeAlgorithm(byte[][] stationBytes, int[] measurementIndices)
{
    GC.Collect();
    GC.WaitForPendingFinalizers();
    GC.Collect();

    var sw = Stopwatch.StartNew();
    var dict = new Dictionary<int, long>();

    foreach (var idx in measurementIndices)
    { fish
        var bytes = stationBytes[idx];
        var hash = Encoding.UTF8.GetString(bytes).GetHashCode(); // String allocation!

        if (dict.TryGetValue(hash, out var count))
            dict[hash] = count + 1;
        else
            dict[hash] = 1;
    }

    sw.Stop();
    return sw.ElapsedMilliseconds;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/hash-comparison-69958245/?t=105)

#### Custom Byte-Based Hashing

为了消除分配,课程评估了自定义的基于字节的哈希算法。
一个 "Simple" 乘法哈希极快,但可能分布不佳,从而在字典里导致桶冲突。

```csharp
private static int ComputeSimpleHash(ReadOnlySpan<byte> bytes)
{
    unchecked
    {
        var hash = 0;
        foreach (var b in bytes)
        {
            hash = hash * 31 + b;
        }
        return hash;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/hash-comparison-69958245/?t=85)

FNV-1a 作为更优的替代方案被引入,它提供出色的雪崩效应,使输入的微小变化产生截然不同的哈希值,从而确保在字典桶之间均匀分布。

```csharp
static int ComputeHash(ReadOnlySpan<byte> span)
{
    unchecked
    {
        var hash = unchecked((int)2166136261);
        foreach (var b in span)
        { 
            hash ^= b;
            hash *= 16777619;
        }
        return hash;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/hash-comparison-69958245/?t=214)

#### Performance and Distribution Results

基准测试表明,虽然 Simple 哈希在原始计算上略快一些,FNV-1a 提供的分布更为均匀。
在十亿行的规模下,这种均匀性对于确保可预测的查找时间、把字典每个桶里的最大条目数降到最低是至关重要的。

```text
| Method                      | MeasurementCount | Mean        | Allocated  | Alloc Ratio |
|---------------------------- |----------------- |------------:|-----------:|------------:|
| DotNetStringHash_Dictionary | 1000000          | 32,539.5 us | 48518352 B |       1.000 |
| SimpleHash_Dictionary       | 1000000          | 15,710.1 us |    15984 B |       0.000 |
| FNV1aHash_Dictionary        | 1000000          | 17,327.8 us |    15984 B |       0.000 |
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/hash-comparison-69958245/?t=355)

把这些结果外推到十亿次测量,可以看出从基于字符串的哈希切换到 FNV-1a 大约节省 17.5 秒,通过避免字符串分配和改善分布质量,实际上把处理时间缩短了一半。

```text
EXTRAPOLATION TO 1 BILLION MEASUREMENTS
----------------------------------------------------------------------
String.GetHashCode(): 35.0s (0.6 minutes)
Simple Hash:          16.9s (0.3 minutes)
FNV-1a Hash:          17.5s (0.3 minutes)

Time saved (FNV-1a vs String): 17.5s (0.3 minutes)
+ Additional GC overhead avoided from string allocations
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/hash-comparison-69958245/?t=220)

## 11. Custom Double Parse and Benchmarks

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/custom-double-parse-and-benchmarks-69958246/) · 8:21

### 总结

本课探讨 .NET 标准浮点数解析在应用于 1 Billion Row Challenge 时的性能局限。
它解释了为什么 double.Parse 计算代价高昂:它要遵循 IEEE 754 标准,还要处理任意精度和各种边界情况。
通过利用本挑战的一个特定约束,即温度值总是恰好带一位小数,课程实现了一个自定义的字节级解析器。
基准测试表明,这个专用解析器比 Utf8Parser 和基于 Span 的解析等内置方法快 7 到 15 倍,显著缩短了处理海量数据集的总时间。

### 核心概念

*   **IEEE 754 Standard**:C# 和现代 CPU 都遵循的浮点算术技术标准,它为通用解析引入了额外开销。
*   **Precision vs. Performance**:标准解析器必须处理无穷多种小数可能性,而 1BRC 的约束允许一个专用的、更快的实现。
*   **Utf8Parser**:一个 .NET 内置工具,可直接从字节 span 解析值而无需字符串分配。
*   **Span-based Parsing**:使用 `stackalloc` 把 UTF-8 字节转换成字符,以便配合 `double.Parse` 使用而不产生堆分配。
*   **Custom Byte Parsing**:手动遍历字节来构造一个 double,避免通用库带来的开销。

### 课程笔记

#### The Cost of Floating Point Standards

现代计算中的浮点算术由 IEEE 754 标准所规范。
该标准确保了跨不同平台和语言的一致性,但它在小数的表示和计算方式上引入了复杂性。
这方面一个常见的例子就是 0.1 加 0.2 时的精度问题。

```csharp
// 1 / 3 => 0.3333333333333334
double x = 0.1;
double y = 0.2;
double z = x + y;

Console.WriteLine(z);  // 0.3 ???
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/custom-double-parse-and-benchmarks-69958246/?t=145)

因为 CPU 必须用二进制表示这些值,某些十进制值无法被精确表示,从而导致细微的精度瑕疵。
`double.Parse` 这类标准 .NET 方法被设计成高度通用,并严格遵循这些标准以处理任何可能的输入。
然而在 1 Billion Row Challenge 的语境下,我们有一个严格的约束:每个温度值都恰好有一位小数。
这让我们可以绕开通用解析器的开销。

#### Benchmarking Parsing Strategies

为了找出处理温度数据最高效的方式,我们用 BenchmarkDotNet 比较三种主要方法。
基准测试测试数据集中出现的四种常见温度格式:`9.1`、`32.4`、`-9.5` 和 `-12.7`。

```csharp
[Params("9.1", "32.4", "-9.5", "-12.7")]
public string RawValue { get; set; } = string.Empty;

private byte[] _bytes = [];

[GlobalSetup]
public void Setup() => _bytes = Encoding.UTF8.GetBytes(RawValue);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/custom-double-parse-and-benchmarks-69958246/?t=260)

##### 1. Span-based Parsing

这个方法通过用 `stackalloc` 创建字符缓冲区来避免堆分配。
它把 UTF-8 字节解码成字符,然后调用接受 `ReadOnlySpan<char>` 的那个标准 `double.Parse` 重载。
虽然这比基于字符串的解析更快,UTF-8 解码这一步以及 `double.Parse` 本身的复杂性仍然带来了显著的代价。

```csharp
[Benchmark]
public double SpanParse()
{
    Span<char> chars = stackalloc char[_bytes.Length];
    var count = Encoding.UTF8.GetChars(_bytes, chars);
    return double.Parse(chars[..count]);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/custom-double-parse-and-benchmarks-69958246/?t=285)

##### 2. Custom Byte Parsing

最高效的做法是一个手写解析器,它直接在字节 span 上操作。
它手动检查负号,遍历各个数字,并处理那唯一的小数点。
通过避免 IEEE 754 合规所需的通用逻辑、只关注预期的格式,它达到了最高的性能。

```csharp
private static double ParseTemperature(ReadOnlySpan<byte> span)
{
    if (span.Length > 0 && span[^1] == '\r')
        span = span[..^1];

    var negative = false;
    var index    = 0;

    if (span[0] == '-')
    {
        negative = true;
        index    = 1;
    }

    double result       = 0;
    var decimalFound    = false;
    var decimalPlace    = 0.1;

    while (index < span.Length)
    {
        var c = span[index++];

        if (c == '.')
        {
            decimalFound = true;
            continue;
        }

        var digit = c - '0';

        if (decimalFound)
        {
            result      += digit * decimalPlace;
            decimalPlace *= 0.1;
        }
        else
        {
            result = result * 10 + digit;
        }
    }

    return negative ? -result : result;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/custom-double-parse-and-benchmarks-69958246/?t=295)

#### Performance Comparison

基准测试的结果显示,自定义解析器显著快于内置的那些替代方案。
对于 `-12.7` 这样的值,自定义解析器大约耗时 3.1 纳秒,而 `Utf8Parser` 接近 20 纳秒,`SpanParse` 则是 34 纳秒。

```markdown
| Method      | RawValue | Mean      | Error     | StdDev    |
|------------ |--------- |----------:|----------:|----------:|
| CustomParse | -12.7    |  3.137 ns | 1.0576 ns | 0.1637 ns |
| Utf8Parse   | -12.7    | 19.875 ns | 4.9043 ns | 0.7590 ns |
| SpanParse   | -12.7    | 33.937 ns | 8.2457 ns | 2.1414 ns |
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/custom-double-parse-and-benchmarks-69958246/?t=370)

在处理十亿行时,这些纳秒级的差异会被放大成数小时甚至数天的总执行时间。
在这个特定用例中,自定义解析器大约比标准 .NET 解析方法快 7 到 15 倍。

## 12. Coding the Custom Double Parse

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/coding-the-custom-double-parse-69958247/) · 10:44

### 总结

本课演示如何为温度数据实现一个高性能的自定义 double 解析器,以取代标准的 .NET double.Parse 方法。
通过绕开 IEEE-754 标准合规所带来的通用开销、只针对 1 Billion Row Challenge 的特定数据格式,这个实现获得了显著的性能提升。
该做法利用直接的 ReadOnlySpan<byte> 操作、手动的符号检测,以及对数字的数学累加,把 ASCII 字节转换成浮点数而不产生堆分配。

### 核心概念

- **FNV-1a Hashing**:用于气象站名称,相比更简单的乘法哈希,它能确保均匀分布和极少的冲突。
- **Custom Parsing**:避开 `double.Parse`,以消除通用浮点验证的开销。
- **Span Manipulation**:使用 `ReadOnlySpan<byte>` 直接处理来自内存映射文件的数据,不产生字符串分配。
- **ASCII-to-Integer Trick**:用一个字节字符减去 '0'(ASCII 48)的字节值,把它转换成对应的整数值。
- **Decimal Accumulation**:手动跟踪小数点,并对小数部分的数字施加一个递减的乘数(0.1、0.01 等)。

### 课程笔记

#### Hashing for Station Names

在实现温度解析器之前,本课先建立起处理气象站名称的背景。
虽然简单的乘法哈希很快,FNV-1a(Fowler-Noll-Vo)算法因其更优的分布性和雪崩效应而更受青睐,这能把用于聚合的字典中的冲突降到最低。

```csharp
unchecked
{
    var hash = unchecked((int)2166136261);
    foreach (var b in span)
    {
        hash ^= b;
        hash *= 16777619;
    }

    return hash;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/coding-the-custom-double-parse-69958247/?t=25)

#### Custom Temperature Parsing

标准的 `double.Parse` 方法被设计用来处理各式各样的国际标准和复杂的浮点边界情况。
对 1BRC 而言,格式是被严格控制的(例如 `36.1` 或 `-0.5`),因此自定义解析器要快得多。

##### Handling Line Endings

第一步是校验输入 span,并去掉初始分行逻辑之后可能残留的尾部回车符(`\r`)。

```csharp
static double ParseTemperature(ReadOnlySpan<byte> span)
{
    // double.Parse() // IEEE-754 Standards
    // London;36.1\r
    if (span.Length > 0 && span[^1] == '\r')
    {
        span = span[..^1];
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/coding-the-custom-double-parse-69958247/?t=175)

##### Sign Detection and Initialization

解析器检查第一个字节是否为负号。
如果是,它设置一个标志并把起始索引加一,以便在处理数字时跳过符号字符。

```csharp
    var negative = false;
    var index = 0;

    // check for negative sign
    if (span[0] == '-')
    {
        negative = true;
        index = 1;
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/coding-the-custom-double-parse-69958247/?t=235)

##### Digit Accumulation and the ASCII Trick

解析器遍历这个 span。
当它遇到一个数字时,会用一个常见的 C# 优化把字节转换成整数:用当前字节减去字符 '0'(字节值 48)。
例如,'8' 的字节值(56)减去 '0'(48)等于整数 8。

如果遇到小数点,解析器会翻转 `decimalFound` 标志并继续。
如果还没遇到小数点,就把当前结果乘以 10 再加上新的数字。
如果已经遇到小数点,就把该数字乘以一个 `decimalPlace` 系数(从 0.1 开始)再加到结果上。

```csharp
    double result = 0;
    var decimalFound = false;
    var decimalPlace = 0.1;

    while(index < span.Length)
    {
        var c = span[index++];

        if (c == '.')
        {
            decimalFound = true;
            continue;
        }

        int digit = c - '0';

        if (decimalFound)
        {
            result += digit * decimalPlace;
            decimalPlace *= 0.1;
        }
        else
        {
            result = (result * 10) + digit;
        }
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/coding-the-custom-double-parse-69958247/?t=280)

##### Finalizing the Result

循环结束后,解析器返回累加出的结果,并在初始符号检查为正时应用负号。

```csharp
    return !negative ? result : -result;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/coding-the-custom-double-parse-69958247/?t=595)

这个自定义实现避开了完整 IEEE-754 合规所需的分支和复杂逻辑,使它成为处理数十亿行时一项关键的优化。

## 13. Finalizing the Approach

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/finalizing-the-approach-69958248/) · 8:49

### 总结

本课把自定义哈希和零分配解析整合进一个多线程的内存映射文件处理器,以此结束 Level 4 的实现。
通过用 unsafe 指针在文件中导航、用自定义的 FNV-1a 哈希算法处理气象站名称,该方案避免了不必要的字符串分配,并把字典查找的开销降到最低。
这个做法确保每个唯一气象站的名称只被转换成字符串一次,而大部分处理工作,包括温度解析和统计更新,都直接在字节 span 上进行,从而在 1 Billion Row Challenge 中带来显著的性能提升。

### 核心概念

* 使用 `Parallel.For` 和内存映射文件分块进行多线程文件处理。
* 对 `ReadOnlySpan<byte>` 做自定义 FNV-1a 哈希以优化字典查找。
* 从原始字节做零分配温度解析,避开 `double.Parse` 的开销。
* 通过在以哈希为键的字典里缓存气象站名称字符串,把堆分配降到最低。
* 高效地把线程本地结果合并成最终的全局数据集。

### 课程笔记

核心处理逻辑被封装在一个 `Parallel.For` 循环里,它根据处理器数量把内存映射文件划分成若干块。
每个线程用一个 unsafe 字节指针(`basePtr`)处理分配给它的分块。
循环通过扫描分号分隔符来确定气象站名称,并立即为得到的 `ReadOnlySpan<byte>` 计算一个整数哈希。

```csharp
// 123, 45, 22, 58, 97
var nameSpan = new ReadOnlySpan<byte>(basePtr + pos,
                                      (int)(semicolonPos - pos));

var hash = ComputeHash(nameSpan);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/finalizing-the-approach-69958248/?t=55)

定位到分号之后,该实现找出下一个换行符,以确定温度片段的范围。
它专门处理 `\r\n` 行尾:检查紧挨换行符前面的那个字节,并相应地调整温度 span 的长度,以确保解析准确。

```csharp
// Parse temp length
var tempLen = (int)(newLinePos - semicolonPos - 1);

// Handle \r\n endings
if (tempLen > 0 && newLinePos > 0
   && basePtr[newLinePos - 1] == '\r')
{
    tempLen--;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/finalizing-the-approach-69958248/?t=130)

随后温度被提取为一个从分号后一个字节开始的 span。
不同于使用 `double.Parse` 这类标准库方法(它们因文化和校验开销而可能更慢),这个 span 被传给一个自定义的 `ParseTemperature` 函数,由它直接处理字节。

```csharp
var tempSpan = new ReadOnlySpan<byte>(basePtr + semicolonPos + 1,
                                      tempLen);
var temperature = ParseTemperature(tempSpan);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/finalizing-the-approach-69958248/?t=175)

为了存放结果,每个线程维护一个本地字典,键就是预先算好的整数哈希。
这就免去了为每一行都用气象站名称字符串作键的需要。
如果哈希已存在,就原地更新已有的 `StationStatsStruct`。
如果不存在,气象站名称会被分配成字符串,这是第一次也是唯一一次,然后创建一个新条目。
接着指针位置被推进到换行符之后。

```csharp
if (localStats.TryGetValue(hash, out var entry))
{
    entry.Stats.Update(temperature);
}
else
{
    string name = Encoding.UTF8.GetString(nameSpan); // new allocation
    var stats = StationStatsStruct.Create();
    stats.Update(temperature);
    localStats[hash] = (name, stats);
}

localLineCount++;
pos = newLinePos + 1;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/finalizing-the-approach-69958248/?t=400)

一旦所有线程处理完各自的分块,线程本地结果就被合并进单个 `finalResults` 字典。
这一步遍历每个线程的字典,并为每个气象站名称合并统计数据。
由于唯一气象站的数量很小(通常在 413 左右),这次合并极快。

```csharp
var finalResults = new Dictionary<string, StationStatsStruct>(GlobalConstants.ExpectedStationCount);

foreach (var localStats in threadLocalResults)
{
    if (localStats == null) continue;

    foreach (var (_, (name, stats)) in localStats)
    {
        if (finalResults.TryGetValue(name, out var existingStats))
        {
            existingStats.Merge(in stats);
            finalResults[name] = existingStats;
        }
        else
        {
            finalResults[name] = stats;
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/finalizing-the-approach-69958248/?t=475)

最后一步是把结果按气象站名称的字母顺序排序,并格式化输出到控制台。
该实现还输出详细的性能和内存统计,包括总耗时和垃圾回收次数,以便验证共享内存方案的效率。

```csharp
var sortedResults = finalResults.OrderBy(kvp => kvp.Key).ToList();

var output = ResultLogger.FormatOutput(sortedResults, s => $"{s.Min:F1}/{s.Mean:F1}/{s.Max:F1}");
Console.WriteLine(output);

Console.WriteLine();
Console.WriteLine($"Processed {totalLines:N0} rows using {threadCount} threads");
Console.WriteLine($"Found {finalResults.Count} unique stations");
Console.WriteLine($"Elapsed: {stopwatch.Elapsed}");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/finalizing-the-approach-69958248/?t=505)

## 14. It's time to test our App

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/it-s-time-to-test-our-app-69958249/) · 13:35

### 总结

本课通过测试 Memory Mapped Files(MMF)结合 unsafe 指针和零分配解析的性能,为 Level 4 的实现收尾。
通过从标准文件流转向 MMF,并把字符串创建和 double.Parse 这类高分配操作替换成自定义的基于 span 的替代方案,应用获得了巨大的性能提升,把十亿行的处理时间从大约 19 秒缩短到 1.8 秒。
本课还展示了操作系统缓存对 MMF 性能的影响,以及标准库解析方法带来的显著开销。

### 核心概念

* **Zero-Copy Architecture**:使用内存映射文件和指针直接在内存中访问数据,不把它拷贝到应用程序的堆上。
* **Pointer-Based Parsing**:用 `byte*` 手动遍历内存以定位分隔符(分号和换行符)。
* **Custom Hashing**:为 `ReadOnlySpan<byte>` 实现一个快速的 FNV-1a 哈希,以便在不产生字符串分配的情况下做字典查找。
* **Custom Numeric Parsing**:用一个专用的基于 span 的解析器替换 `double.Parse`,以避开 IEEE-754 标准合规的开销。
* **OS Page Cache**:理解基于 MMF 的应用首次运行时包含磁盘到内存的延迟,而之后的运行则受益于数据已驻留内存。

### 课程笔记

该实现的重点是消除之前级别里识别出的瓶颈:磁盘 I/O、字符串分配,以及垃圾回收器(GC)的开销。
通过使用 `MemoryMappedFile`,数据被映射进进程的地址空间,让应用程序可以把文件当作一大块内存来对待。

```csharp
var stopwatch = Stopwatch.StartNew();

var fileInfo = new FileInfo(GlobalConstants.FilePath);
var fileSize = fileInfo.Length;

if (fileSize == 0)
{
    Console.WriteLine("File is empty.");
    return;
}

var threadCount = Environment.ProcessorCount;
var threadLocalResults = new Dictionary<int, (string Name, StationStatsStruct Stats)>[threadCount];
var lineCounters = new long[threadCount];

using var mmf = MemoryMappedFile.CreateFromFile(GlobalConstants.FilePath,
                                                FileMode.Open,
                                                null,
                                                0,
                                                MemoryMappedFileAccess.Read);

using var accessor = mmf.CreateViewAccessor(0,
                                            fileSize,
                                            MemoryMappedFileAccess.Read);

unsafe
{
    byte* basePtr = null;
    accessor.SafeMemoryMappedViewHandle.AcquirePointer(ref basePtr);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/it-s-time-to-test-our-app-69958249/?t=120)

为了保证解析正确,应用必须处理可能存在的 UTF-8 字节序标记(BOM),然后把数据划分成若干块以供并行处理。

```csharp
    try
    {
        // Skip UTF-8 BOM if present (EF BB BF)
        long dataStart = 0;
        if (fileSize >= 3 && basePtr[0] == 0xEF && basePtr[1] == 0xBB && basePtr[2] == 0xBF)
        {
            dataStart = 3;
        }

        var dataSize = fileSize - dataStart;
        var chunkSize = dataSize / threadCount;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/it-s-time-to-test-our-app-69958249/?t=130)

并行处理时,每个线程被分配一个分块。
因为分块很可能从某一行的中间开始或结束,边界必须被调整到最近的换行符(`\n`)。

```csharp
        Parallel.For(0, threadCount, threadIndex => 
        {
            // calculate the chunk boundaries (relative to data start)
            var startPos = dataStart + (threadIndex * chunkSize);
            var endPos = (threadIndex == threadCount - 1)
                            ? fileSize
                            : dataStart + ((threadIndex + 1) * chunkSize);

            // Adjust start to next line boundary (except for the first chunk)
            if (startPos > dataStart)
            {                while (startPos < fileSize && basePtr[startPos - 1] != '\n')
                {
                    startPos++;
                }
            }

            // Adjust end to line boundary
            if (endPos < fileSize && threadIndex < threadCount - 1)
            {                while (endPos < fileSize && basePtr[endPos - 1] != '\n')
                {
                    endPos++;
                }
            }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/it-s-time-to-test-our-app-69958249/?t=460)

在处理循环内部,应用用指针找到分号分隔符和换行符。
这使得可以为气象站名称和温度分别创建 `ReadOnlySpan<byte>`,而不分配任何堆内存。

```csharp
            var localStats = new Dictionary<int, (string Name, StationStatsStruct Stats)>();
            long localLineCount = 0;
            var pos = startPos;

            while(pos < endPos)
            {
                var semicolonPos = pos;
                while(semicolonPos < endPos && basePtr[semicolonPos] != ';')
                {
                    semicolonPos++;
                }

                if (semicolonPos >= endPos)
                    break;

                // find new line
                var newLinePos = semicolonPos + 1;
                while(newLinePos < endPos && basePtr[newLinePos] != '\n')
                {
                    newLinePos++;
                }

                if (newLinePos >= endPos && threadIndex < threadCount - 1)
                {
                    break;
                }
                
                var nameSpan = new ReadOnlySpan<byte>(basePtr + pos,
                                                      (int)(semicolonPos - pos));

                var hash = ComputeHash(nameSpan);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/it-s-time-to-test-our-app-69958249/?t=175)

提取出这些 span 之后,应用解析温度并更新线程本地字典。
只有在首次遇到一个新气象站时才会发生字符串分配(在这个数据集里大约 413 次),而不是为十亿行中的每一行都分配。

```csharp
                // Parse temp length
                var tempLen = (int)(newLinePos - semicolonPos - 1);
                
                // Handle \r\n endings
                if (tempLen > 0 && newLinePos > 0
                   && basePtr[newLinePos - 1] == '\r')
                {
                    tempLen--;
                }

                var tempSpan = new ReadOnlySpan<byte>(basePtr + semicolonPos + 1,
                                                      tempLen);
                var temperature = ParseTemperature(tempSpan);

                if (localStats.TryGetValue(hash, out var entry))
                {                    entry.Stats.Update(temperature);
                }
                else
                {                    string name = Encoding.UTF8.GetString(nameSpan); // new allocation
                    var stats = StationStatsStruct.Create();
                    stats.Update(temperature);
                    localStats[hash] = (name, stats);
                }

                localLineCount++;
                pos = newLinePos + 1;
            }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/it-s-time-to-test-our-app-69958249/?t=250)

自定义的 `ParseTemperature` 函数显著快于 `double.Parse`,因为它避开了完整 IEEE-754 标准的复杂性,只关注数据集中出现的那种特定格式(可选的负号、若干数字,以及一个小数点)。

```csharp
static double ParseTemperature(ReadOnlySpan<byte> span)
{
    if (span.Length > 0 && span[^1] == '\r')
    {
        span = span[..^1];
    }

    var negative = false;
    var index = 0;

    if (span[0] == '-')
    {
        negative = true;
        index = 1;
    }

    double result = 0;
    var decimalFound = false;
    var decimalPlace = 0.1;

    while(index < span.Length)
    {
        var c = span[index++];

        if (c == '.')
        {
            decimalFound = true;
            continue;
        }

        int digit = c - '0';

        if (decimalFound)
        {            result += digit * decimalPlace;
            decimalPlace *= 0.1;
        }
        else
        {            result = (result * 10) + digit;
        }
    }

    return !negative ? result : -result;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/it-s-time-to-test-our-app-69958249/?t=670)

### Performance Results

* **Level 3 (Parallel Streams)**:约 18.7 秒。
* **Level 4 (MMF + Pointers) - First Run**:约 2.9 秒(包含磁盘到内存的映射)。
* **Level 4 (MMF + Pointers) - Subsequent Runs**:约 1.8 秒(数据已被操作系统缓存在 RAM 中)。
* **Impact of `double.Parse`**:改回标准库的 `double.Parse` 会让运行时间从 1.8s 增加到 3.2s,几乎让执行时间翻倍,并把吞吐从 12 GB/s 降到 7 GB/s。

---

## 运行 Demo

Demo 代码按课程官方仓库 [Dometrain/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet](https://github.com/Dometrain/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet) 的方式组织,本章对应上游的 `Level4_SharedMemory/` 和 `Benchmarkts/Level4_SharedMemory/`:

```
src/1brc/
├── notes/                  各章笔记
├── Shared/                 SharedTypes.cs:GlobalConstants、ResultLogger、StationStats、StationStatsStruct
├── DataGenerator/          Program.cs:413 个气象站 + Box-Muller 生成器
├── Level1_Naive/           Program.cs:第 3 章的 LINQ 朴素实现
├── Level2_Stream/          Program.cs:第 4 章的 StreamReader 流式实现
├── Level3_Parallel/        Program.cs:第 5 章的 Parallel.For 实现;RaceCondition.cs
├── Level4_SharedMemory/    Program.cs:本章的 MMF + 指针实现;Pointers.cs:第 5 课的指针讲解
└── Benchmarks/             Level4_SharedMemory/:第 4、8、10、11 课的四组基准测试
```

本章第 8 课新增的 `StationStatsStruct` 放进了 `Shared/SharedTypes.cs`,与 `StationStats` 并存,因为 Level 1 到 Level 3 还在用后者。

`Level4_SharedMemory/Program.cs` 与第 8、9、12、13、14 课的课程代码逐行一致,只有一处偏差。
课上写的是:

```csharp
if (localStats.TryGetValue(hash, out var entry))
{
    entry.Stats.Update(temperature);
}
```

`entry` 是元组的一份拷贝,`entry.Stats.Update(...)` 改的是这份拷贝,而它从未被写回字典,所以每个气象站的 `Count` 都会停在 1,Min/Mean/Max 全部错误。
这里改成取字典槽位的引用:

```csharp
ref var entry = ref CollectionsMarshal.GetValueRefOrNullRef(localStats, hash);
if (!Unsafe.IsNullRef(ref entry))
{
    entry.Stats.Update(temperature);
}
```

同样是一次查找,但是原地更新。
另一个常见写法 `localStats[hash] = entry;` 也能修正结果,但会在热路径上多一次哈希查找和一次写入,而本章的重点正是这条路径的开销。

先用 DataGenerator 生成测量文件,再跑 Level4_SharedMemory:

```bash
cd src/1brc
dotnet run --project DataGenerator -c Release -- 10_000_000
dotnet run --project Level4_SharedMemory -c Release
```

下面是 12 核机器上一千万行的真实输出。
`{...}` 那一行有一万多个字符(413 个气象站),这里只保留开头,其余用 `…` 省略。

```text
=== Level 4: Memory Mapped Files ===
File: C:\Users\iantu\AppData\Local\Temp\1brc\Files\measurements.txt
Processor Count: 12

{Abéché=-9.7/29.4/68.5, Abha=-20.6/17.9/59.2, Abidjan=-13.2/26.0/65.7, Accra=-13.2/26.5/66.2, Addis Ababa=-22.1/16.0/55.8, Adelaide=-18.3/17.2/55.0, Aden=-12.3/29.1/66.7, Ahvaz=-14.6/25.4/70.0, …}

Processed 10,000,000 rows using 12 threads
Found 413 unique stations
Elapsed: 00:00:00.1219283

📁 Results saved: C:\Users\iantu\AppData\Local\Temp\1brc\Files\results.log

Per-Thread Statistics:
  Thread 0: 833,483 lines, 413 stations
  Thread 1: 833,468 lines, 413 stations
  Thread 2: 833,268 lines, 413 stations
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

每个气象站的 Min/Mean/Max 与同一个文件上 `Level2_Stream`、`Level3_Parallel` 的结果完全一致。
同一台机器、同一个一千万行文件的三代对照:

| 实现 | 耗时 | Working Set | Gen0/1/2 |
| --- | --- | --- | --- |
| `Level2_Stream`(第 4 章) | 1.27 s | 35 MB | 220/2/2 |
| `Level3_Parallel`(第 5 章) | 0.25 s | 384 MB | 103/10/5 |
| `Level4_SharedMemory`(本章) | 0.12 s | 162 MB | 2/2/2 |

Gen0 回收从 103 次降到 2 次,这就是第 1 课说的"无字符串"架构的直接效果:十亿行里只有 413 次 `Encoding.UTF8.GetString`,其余全部在 `ReadOnlySpan<byte>` 上完成。
Working Set 从 384 MB 降到 162 MB,因为 Level 3 每个线程都要 `new byte[chunkLength]` 再 `GetString` 一遍,而 Level 4 只是把文件映射进地址空间。
第 14 课提到的首次运行较慢(磁盘到内存)在这里没有体现,因为这个文件在之前的章节里已经被操作系统页缓存收下了。

第 5 课的指针讲解:

```bash
dotnet run --project Level4_SharedMemory -c Release -- pointers
```

```text
=== Level 4: Working with Pointers in C# ===


1 — Managed String: Memory Model
--------------------------------
  Value      : Hello
  Length     : 5  (character count)
  [2]        : l  — indexer, no new allocation

2 — fixed: Preventing GC from Moving the String
-----------------------------------------------
  ptr address: 0x23E46B30BFC
  ptr[0]     : H   (= s[0])
  ptr[1]     : e   (= s[1])
  *(ptr+2)   : l   (= s[2])

3 — String Character Reading with Pointer (vs indexer)
------------------------------------------------------
  With pointer: Hello, World!
  With indexer: Hello, World!

4 — Pointer Arithmetic: sizeof and Address Difference
-----------------------------------------------------
  sizeof(char)  : 2  byte  (UTF-16)
  sizeof(byte)  : 1  byte
  sizeof(int)   : 4  byte
  sizeof(long)  : 8  byte

  &s[0] = 0x23E46B31264
  &s[3] = 0x23E46B3126A
  Diff  = 3 char  = 6 bytes

5 — String Mutation: Modifying with Pointer (Dangerous!)
--------------------------------------------------------
  Before: Harmless
  After : Danmless

6 — ReadOnlySpan<char> vs Pointer: Modern Alternative
-----------------------------------------------------
  Full string: Hello, World!
  Span[7..12]: World
  span[0]    : H
  span ptr   : 0x23E46B30FE4  (same address as s's first char)
  s    ptr   : 0x23E46B30FE4

Summary: When to Use What?
--------------------------
  string indexer    → General use. Safe, readable.
  ReadOnlySpan<T>   → Substrings, parsing, zero-allocation reads. ✅ Recommended
  fixed + char*     → Interop, unsafe parsing, max performance critical path.
  string mutation   → ❌ Never — only to understand what happens.
```

第 5 节的 `new string("Harmless")` 在现在的 .NET 上没有这个重载,改成了 `new string("Harmless".AsSpan())`,同样是一份未驻留的拷贝。
输出的 "Danmless" 与课上一致:通过指针改写了本该不可变的 string。

第 10 课的哈希分析器:

```bash
dotnet run --project Benchmarks -c Release -- hash-analysis
```

```text
=== Hash Algorithm Performance for Dictionary<int, Stats> ===
Test: 100,000,000 dictionary operations (lookup/insert)
Keys: 413 unique station names (each repeated ~242,130 times)

=== Algorithm 1: String.GetHashCode() with allocation ===
    Implementation: Encoding.UTF8.GetString(bytes).GetHashCode()
    Issue: Creates string object for every hash computation!
    Elapsed: 7,719 ms

=== Algorithm 2: Simple multiplicative hash (hash * 31 + b) ===
    Implementation: byte-based, no allocation
    Elapsed: 4,980 ms

=== Algorithm 3: FNV-1a ===
    Implementation: byte-based, no allocation, avalanche effect
    Elapsed: 5,319 ms

Simple Hash distribution over 512 buckets: 285 used, 227 empty, max 4 keys in one bucket
FNV-1a Hash distribution over 512 buckets: 281 used, 231 empty, max 4 keys in one bucket

EXTRAPOLATION TO 1 BILLION MEASUREMENTS
----------------------------------------------------------------------
String.GetHashCode(): 77.2s (1.3 minutes)
Simple Hash:          49.8s (0.8 minutes)
FNV-1a Hash:          53.2s (0.9 minutes)

Time saved (FNV-1a vs String): 24.0s (0.4 minutes)
+ Additional GC overhead avoided from string allocations
```

课上没有展示 `HashDistributionAnalyzer` 里 `GenerateStationNames` 的实现,这里用随机生成的 3 到 20 个字符的名字代替真实气象站名单,所以桶分布那两行是本仓库自己的补充,不是课上的输出。
三种算法的排序和课上一致:`String.GetHashCode()` 最慢,两种字节哈希差不多快,`Simple` 略快于 `FNV-1a`。
在这台机器上,把 FNV-1a 换成基于 string 的哈希,外推到十亿次要多花 24.0 秒。

四组基准测试:

```bash
dotnet run --project Benchmarks -c Release -- --filter "*Level4_SharedMemory*"
```

```text
BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.9278/25H2/2025Update/HudsonValley2)
Snapdragon X 12-core X1E80100 3.40 GHz (Max: 3.42GHz), 1 CPU, 12 logical and 12 physical cores
.NET SDK 10.0.102
  [Host]     : .NET 10.0.2 (10.0.2, 10.0.225.61305), Arm64 RyuJIT armv8.0-a
```

第 4 课的 `SequentialVsRandomAccessBenchmark`:

```text
| Method          | Mean       | Error     | StdDev    | Ratio | RatioSD | Allocated | Alloc Ratio |
|---------------- |-----------:|----------:|----------:|------:|--------:|----------:|------------:|
| SmallRandom     |   7.765 ms | 0.3911 ms | 0.0214 ms |  0.46 |    0.00 |         - |          NA |
| LargeRandom     | 160.110 ms | 1.5538 ms | 0.0852 ms |  9.45 |    0.02 |      84 B |          NA |
| SmallSequential |   2.148 ms | 0.7779 ms | 0.0426 ms |  0.13 |    0.00 |         - |          NA |
| LargeSequential |  16.934 ms | 0.7513 ms | 0.0412 ms |  1.00 |    0.00 |         - |          NA |
```

四个用例的绝对排序里,`SmallSequential` 最快、`LargeRandom` 最慢,与课上一致。
但课上强调的那一点在这台机器上没有复现:课上 `SmallRandom` 是 18.753 ms、`LargeSequential` 是 8.538 ms,所以"顺序访问 RAM 比随机访问缓存更快";这里 `SmallRandom` 是 7.765 ms、`LargeSequential` 是 16.934 ms,顺序反了。
基准测试的注释按课上的 64MB L3 来划分 Small 和 Large,这台 Snapdragon X 的缓存层级与之不同,32MB 的 Small 数据集在这里随机访问的代价没有课上那么高。

第 8 课开头那张查找方法对照表:

```text
| Method        | Line                 | Mean       | Error     | StdDev    | Median     | Ratio  | RatioSD |
|-------------- |--------------------- |-----------:|----------:|----------:|-----------:|-------:|--------:|
| StringIndexOf | A;0.0                |  0.1694 ns | 2.3913 ns | 0.1311 ns |  0.2188 ns |   3.18 |    4.88 |
| SpanIndexOf   | A;0.0                |  0.1276 ns | 0.1040 ns | 0.0057 ns |  0.1282 ns |   2.39 |    2.80 |
| PointerScan   | A;0.0                |  0.0009 ns | 0.0296 ns | 0.0016 ns |  0.0000 ns |   0.02 |    0.04 |
| SpanParse     | A;0.0                | 12.9578 ns | 0.5849 ns | 0.0321 ns | 12.9396 ns | 243.12 |  284.14 |
| PointerParse  | A;0.0                | 12.1087 ns | 1.2660 ns | 0.0694 ns | 12.0753 ns | 227.19 |  265.52 |
|               |                      |            |           |           |            |        |         |
| StringIndexOf | Ouagadougou;32.4     |  0.3361 ns | 0.0295 ns | 0.0016 ns |  0.3369 ns |   1.00 |    0.01 |
| SpanIndexOf   | Ouagadougou;32.4     |  0.3511 ns | 0.0088 ns | 0.0005 ns |  0.3510 ns |   1.04 |    0.00 |
| PointerScan   | Ouagadougou;32.4     |  3.0216 ns | 0.0652 ns | 0.0036 ns |  3.0205 ns |   8.99 |    0.04 |
| SpanParse     | Ouagadougou;32.4     | 22.7912 ns | 0.7534 ns | 0.0413 ns | 22.8111 ns |  67.80 |    0.30 |
| PointerParse  | Ouagadougou;32.4     | 21.0735 ns | 0.4951 ns | 0.0271 ns | 21.0632 ns |  62.69 |    0.27 |
|               |                      |            |           |           |            |        |         |
| StringIndexOf | Petro(...)-12.7 [30] |  0.9242 ns | 0.0199 ns | 0.0011 ns |  0.9239 ns |   1.00 |    0.00 |
| SpanIndexOf   | Petro(...)-12.7 [30] |  1.5433 ns | 0.3763 ns | 0.0206 ns |  1.5330 ns |   1.67 |    0.02 |
| PointerScan   | Petro(...)-12.7 [30] |  6.3106 ns | 1.6594 ns | 0.0910 ns |  6.2596 ns |   6.83 |    0.09 |
| SpanParse     | Petro(...)-12.7 [30] | 24.3374 ns | 3.5519 ns | 0.1947 ns | 24.2415 ns |  26.33 |    0.18 |
| PointerParse  | Petro(...)-12.7 [30] | 26.2018 ns | 0.6358 ns | 0.0348 ns | 26.1996 ns |  28.35 |    0.04 |
```

课上只把这张表作为注释贴在代码里,没有给出对应的基准测试类,所以 `LineParsingBenchmark` 是本仓库按表头自己写的,第三个用例用的完整名字是 `Petropavlovsk-Kamchatsky;-12.7`。
BenchmarkDotNet 对 `StringIndexOf`、`SpanIndexOf`、`PointerScan` 三项都给出了 "The method duration is indistinguishable from the empty method duration" 的警告,所以 `A;0.0` 那一组的数字不可信,`PointerScan` 的 0.0009 ns 只是被 JIT 消掉了。
长度增加后的趋势和课上一致:`PointerScan` 从 3.02 ns 涨到 6.31 ns,而 `SpanIndexOf` 只从 0.35 ns 涨到 1.54 ns,也就是第 8 课说的手写标量循环被 SIMD 拉开差距。

第 10 课的 `HashAlgorithmBenchmark`:

```text
| Method                      | MeasurementCount | Mean     | Error    | StdDev   | Median   | Ratio | RatioSD | Gen0       | Allocated   | Alloc Ratio |
|---------------------------- |----------------- |---------:|---------:|---------:|---------:|------:|--------:|-----------:|------------:|------------:|
| DotNetStringHash_Dictionary | 1000000          | 37.17 ms | 0.732 ms | 0.751 ms | 37.14 ms |  1.00 |    0.03 | 11642.8571 | 47685.02 KB |       1.000 |
| SimpleHash_Dictionary       | 1000000          | 18.74 ms | 0.317 ms | 0.722 ms | 18.37 ms |  0.50 |    0.02 |          - |    21.79 KB |       0.000 |
| FNV1aHash_Dictionary        | 1000000          | 19.29 ms | 0.153 ms | 0.144 ms | 19.27 ms |  0.52 |    0.01 |          - |    21.79 KB |       0.000 |
```

与课上表格几乎重合:课上是 32.5 / 15.7 / 17.3 ms,这里是 37.2 / 18.7 / 19.3 ms,两种字节哈希都是 string 版本的一半。
分配量对得更准:课上 `DotNetStringHash_Dictionary` 是 48,518,352 B(约 47,381 KB),这里是 47,685.02 KB,而两种字节哈希都只有 21.79 KB。

第 11 课的 `TemperatureParseBenchmark`:

```text
| Method      | RawValue | Mean      | Error     | StdDev    | Ratio | RatioSD |
|------------ |--------- |----------:|----------:|----------:|------:|--------:|
| SpanParse   | -12.7    | 26.927 ns | 0.0685 ns | 0.0572 ns |  7.56 |    0.03 |
| Utf8Parse   | -12.7    | 13.447 ns | 0.0426 ns | 0.0356 ns |  3.78 |    0.02 |
| CustomParse | -12.7    |  3.562 ns | 0.0153 ns | 0.0128 ns |  1.00 |    0.00 |
|             |          |           |           |           |       |         |
| SpanParse   | -9.5     | 24.516 ns | 0.0375 ns | 0.0333 ns | 17.60 |    0.62 |
| Utf8Parse   | -9.5     | 12.811 ns | 0.0585 ns | 0.0547 ns |  9.20 |    0.33 |
| CustomParse | -9.5     |  1.395 ns | 0.0613 ns | 0.0543 ns |  1.00 |    0.05 |
|             |          |           |           |           |       |         |
| SpanParse   | 32.4     | 33.970 ns | 0.0200 ns | 0.0178 ns |  9.45 |    0.03 |
| Utf8Parse   | 32.4     | 12.998 ns | 0.0262 ns | 0.0232 ns |  3.61 |    0.01 |
| CustomParse | 32.4     |  3.596 ns | 0.0159 ns | 0.0124 ns |  1.00 |    0.00 |
|             |          |           |           |           |       |         |
| SpanParse   | 9.1      | 24.584 ns | 0.1543 ns | 0.1443 ns | 17.91 |    0.17 |
| Utf8Parse   | 9.1      | 12.335 ns | 0.0219 ns | 0.0204 ns |  8.98 |    0.07 |
| CustomParse | 9.1      |  1.373 ns | 0.0131 ns | 0.0110 ns |  1.00 |    0.00 |
```

`-12.7` 这一组和课上几乎一样:课上 CustomParse 3.137 ns、Utf8Parse 19.875 ns、SpanParse 33.937 ns,这里是 3.562 / 13.447 / 26.927 ns。
比值落在 3.6 到 17.9 倍之间,第 11 课末尾说的"快 7 到 15 倍"在这台机器上大致成立。

