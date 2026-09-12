# Level 5: SIMD

> 课程:[From Zero to Hero: 1 Billion Row Performance Challenge in .NET](https://dometrain.com/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet/) · 第 7 章
> 共 10 课 · 约 111:54
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/introduction-69958250/) | 6:55 | [↓](#1-introduction) |
| 2 | [Int Parser instead of Double.Parse()](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/int-parser-instead-of-double-parse-69958251/) | 8:22 | [↓](#2-int-parser-instead-of-doubleparse) |
| 3 | [Custom: FastHashTable Part 1](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/custom-fasthashtable-part-1-69958252/) | 12:18 | [↓](#3-custom-fasthashtable-part-1) |
| 4 | [CPU Pipelining and Branching](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/cpu-pipelining-and-branching-69958253/) | 17:35 | [↓](#4-cpu-pipelining-and-branching) |
| 5 | [Custom: FastHashTable Part 2](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/custom-fasthashtable-part-2-69958254/) | 11:07 | [↓](#5-custom-fasthashtable-part-2) |
| 6 | [Custom: FastHashTable Part 3](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/custom-fasthashtable-part-3-69958255/) | 13:50 | [↓](#6-custom-fasthashtable-part-3) |
| 7 | [CPU Inlining](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/cpu-inlining-69958256/) | 8:59 | [↓](#7-cpu-inlining) |
| 8 | [Introduction to SIMD](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/introduction-to-simd-69958257/) | 10:20 | [↓](#8-introduction-to-simd) |
| 9 | [Finalizing the Approach](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/finalizing-the-approach-69958258/) | 13:56 | [↓](#9-finalizing-the-approach) |
| 10 | [Testing the Fastest Version](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/testing-the-fastest-version-69958259/) | 8:32 | [↓](#10-testing-the-fastest-version) |

## 1. Introduction

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/introduction-69958250/) · 6:55

### 总结

Level 5 聚焦于微优化,把 1 Billion Row Challenge 中剩下的那些毫秒也挤出来。
关键策略包括:实现 SIMD(Single Instruction, Multiple Data)来搜索分隔符,用自定义的 Fast Hash Table 替换标准字典以降低查找开销,以及把浮点温度解析换成无分支的整数方案。

### 核心概念

- 用于并行数据处理的 SIMD(Single Instruction, Multiple Data)。
- 无分支的整数温度解析。
- 用自定义的 Fast Hash Table 替换 `Dictionary<K, V>`。
- 用 AVX2 优化分隔符(分号和换行)的搜索。
- 减少浮点运算的开销。

### 课程笔记

本课标志着一个转折:从显著的架构级提速(把运行时间从 20 分钟降到 2 秒)转向每一毫秒都要计较的微优化。
之前的各个 Level 用的是 memory-mapped files、指针和并行处理,而 Level 5 引入 SIMD(Single Instruction, Multiple Data)来挖掘现代 CPU 的能力。

SIMD 是这一阶段的主要焦点。
它让应用在一条 CPU 指令里处理多个字节,这在缓冲区中扫描分号、换行这类分隔符时尤其有效。

```csharp
[MethodImpl(MethodImplOptions.AggressiveInlining)]
static unsafe long FindByteFast(byte* basePtr,
                                long start,
                                long end,
                                Vector256<byte> targetVec,
                                byte target)
{
    var pos = start;

    if (Avx.IsSupported)
    {
        while (pos + 32 <= end)
        {
            var data = Avx.LoadVector256(basePtr + pos);
            var cmp = Avx2.CompareEqual(data, targetVec);
            var mask = (uint)Avx2.MoveMask(cmp);
            
            if (mask != 0)
                return pos + BitOperations.TrailingZeroCount(mask);

            pos += 32;
        }
    }

    while (pos < end)
    {
        if (basePtr[pos] == target)
            return pos;

        pos++;
    }

    return end;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/introduction-69958250/?t=232)

另一个显著的瓶颈是温度解析。
标准的 double 解析涉及浮点运算和分支,在这个规模下代价很高。
利用数据格式固定(恰好只有一位小数)这一点,解析器可以改写成整数运算,返回按 10 倍缩放后的值(例如 12.3 变成 123)。

```csharp
[MethodImpl(MethodImplOptions.AggressiveInlining)]
static unsafe int ParseTemperatureBranchless(byte* ptr, int len)
{
    var sign = 1;

    if (ptr[0] == '-')
    {
        sign = -1;
        ptr++;
        len--;
    }

    int value;

    if (len == 3)
    {
        // "D.D" -> D*10 + D (e.g. "9.1" -> 91)
        value = ((ptr[0] - '0') * 10) + (ptr[2] - '0');
    }
    else
    {
        // "DD.D" -> D*100 + D*10 + D (e.g. "32.4" -> 324)
        value = ((ptr[0] - '0') * 100)
              + ((ptr[1] - '0') * 10)
              + (ptr[3] - '0');
    }

    return sign * value;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/introduction-69958250/?t=279)

最后,在对 10 亿行做查找时,标准的 `Dictionary<TKey, TValue>` 会成为瓶颈。
为了解决这一点,这里实现了一个自定义的 `FastHashTable`。
这个结构使用线性探测和一个简化的哈希算法(FNV-1a),把气象站名字的查找与更新开销降到最低。

```csharp
internal unsafe class FastHashTable
{
    private Entry[] _entries;
    private int _count;

    public void AddOrUpdate(byte* namePtr, int nameLen, int temperature)
    {
        var hash = ComputeHash(namePtr, nameLen);
        var index = (int)(hash & (uint)(_entries.Length - 1));

        while (true)
        {
            ref Entry entry = ref _entries[index];

            if (entry.Name is null)
            {
                entry.Name = new byte[nameLen];
                fixed (byte* dest = entry.Name)
                {
                    Buffer.MemoryCopy(namePtr, dest, nameLen, nameLen);
                }
                entry.StationName = Encoding.UTF8.GetString(entry.Name);
                entry.Hash = hash;
                entry.Min = temperature;
                entry.Max = temperature;
                entry.Sum = temperature;
                entry.Count = 1;
                _count++;
                return;
            }

            if (entry.Hash == hash && entry.Name.Length == nameLen)
            {
                entry.Min = Math.Min(temperature, entry.Min);
                entry.Max = Math.Max(temperature, entry.Max);
                entry.Sum += temperature;
                entry.Count++;
                return;
            }

            index = (index + 1) & (_entries.Length - 1);
        }
    }

    private static uint ComputeHash(byte* ptr, int len)
    {
        var hash = 2166136261u;
        for (var i = 0; i < len; i++)
        {
            hash ^= ptr[i];
            hash *= 16777619u;
        }
        return hash;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/introduction-69958250/?t=343)

## 2. Int Parser instead of Double.Parse()

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/int-parser-instead-of-double-parse-69958251/) · 8:22

### 总结

在 1 Billion Row Challenge 中,用自定义的整数解析器替换 `double.Parse()` 能显著提升性能。
利用温度总是恰好带一位小数这个约束,我们可以把输入当作整数处理(乘以 10),从而避开昂贵的浮点运算和通用解析逻辑。
这个做法让解析时间缩短约 3 倍,并且由于用 4 字节整数而不是 8 字节 double,CPU 缓存效率也更高。

### 核心概念

* 浮点运算与整数运算的开销对比。
* 利用数据约束(固定的小数精度)。
* 在解析逻辑中避免通用循环。
* 内存与 CPU 缓存优化(8 字节 double 对比 4 字节 int)。
* 缩放数值,在不使用浮点类型的前提下保持精度。

### 课程笔记

本课指出 `double.Parse()` 是一个显著的性能瓶颈。
在之前的迭代中,使用 `double.Parse()` 的执行时间大约是 3.25 秒,而基于 span 的自定义解析器把它降到大约 1.7 到 1.8 秒。

```csharp
static double ParseTemperature(ReadOnlySpan<byte> span)
{
    // double.Parse() // IEEE-754 Standards
    // London;36.1\r
    if (span.Length > 0 && span[^1] == '\r')
    {
        span = span[..^1];
    }

    var negative = false;
    var index = 0;

    // check for negative sign
    if (span[0] == '-')
    {
        negative = true;
        index = 1;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/int-parser-instead-of-double-parse-69958251/?t=10)

基准测试显示,解析 `-12.7` 这样的值,`DoubleParse` 大约需要 2.9 纳秒,而基于整数的做法只要 0.9 纳秒。

```text
| Method               | RawValue | Mean      | Error     | StdDev    | Ratio |
|--------------------- |--------- |----------:|----------:|----------:|------:|
| DoubleParse          | -12.7    | 2.9380 ns | 1.0126 ns | 0.2630 ns |  1.01 |
| IntParseBranchless   | -12.7    | 0.9081 ns | 0.1584 ns | 0.0411 ns |  0.31 |
| IntParseWithDivision | -12.7    | 1.1209 ns | 0.8821 ns | 0.1365 ns |  0.38 |
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/int-parser-instead-of-double-parse-69958251/?t=55)

性能提升由两个主要因素驱动。
第一,整数运算对 CPU 来说比浮点运算更快,因为它绕开了 FPU 流水线。
第二,从 8 字节的 double 换成 4 字节的整数,让 CPU 缓存里能放下的温度数据翻了一倍。

```csharp
// Key Difference:
//
//    Level4 - DoubleParse:
//       * Advances byte by byte in a loop
//       * Floating-point multiplication for each decimal digit: result += digit * decimalPlace
//       * decimalPlace *= 0.1 -> float operation, repeated every iteration
//       * General-purpose (handles any number of decimal digits)
//
//    Level5 - IntParseBranchless:
//       * NO loop, direct index access (ptr[0], ptr[1], ptr[2] ...)
//       * Integer arithmetic only (*10, *100, +)
//       * Result returned as temperature x 10 (12.3 -> 123, -9.5 -> -95)
//       * Exploits format knowledge: always exactly 1 decimal digit
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/int-parser-instead-of-double-parse-69958251/?t=145)

这个实现利用了输入格式被限制为一位小数(例如 `12.3` 或 `-9.5`)这一事实。
把数值乘以 10 以去掉小数点之后,应用就可以用整数完成全部中间计算,包括 Min、Max 和 Sum。
除以 10 只发生在最后打印结果的输出阶段。

```csharp
private static unsafe int ParseTemperatureInt(byte* ptr, int len)
{
    var sign = 1;

    if (ptr[0] == '-')
    {
        sign = -1;
        ptr++;
        len--;
    }

    int value;

    if (len == 3)
    {
        // "D.D" -> D*10 + D   (e.g. "9.1" -> 91)
        value = (ptr[0] - '0') * 10
              + (ptr[2] - '0');
    }
    else
    {
        // 32.5 => 325
        // "DD.D" -> D*100 + D*10 + D   (e.g. "32.4" -> 324)
        value = (ptr[0] - '0') * 100
              + (ptr[1] - '0') * 10
              + (ptr[3] - '0') * 1;
    }

    return sign * value;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/int-parser-instead-of-double-parse-69958251/?t=400)

## 3. Custom: FastHashTable Part 1

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/custom-fasthashtable-part-1-69958252/) · 12:18

### 总结

本课详细讲解如何为 1 Billion Row Challenge 创建一个专用的 FastHashTable 来替换标准的 .NET Dictionary。
使用自定义哈希表,实现可以针对特定的数据模式做优化,例如对气象站名字使用原始字节比较,以及把温度缩放成整数以避免浮点开销。
本课涵盖内部的 Entry 结构、FNV-1a 哈希算法,以及用于管理表容量的无分支位运算。

### 核心概念

- 用专用哈希表替换通用的 `Dictionary`,以减少开销。
- 使用 FNV-1a 哈希以获得均匀的数据分布和快速的计算。
- 把温度以整数存储(乘以 10),避免浮点带来的性能损失。
- 用无分支的位运算逻辑计算下一个 2 的幂容量。
- 用负载因子阈值(0.75)和 2 的幂数组尺寸来管理容量。

### 课程笔记

标准的 .NET `Dictionary` 针对通用场景做了高度优化,但它包含的一些特性和泛型开销在 1 Billion Row Challenge 这类高性能场景里会成为瓶颈。
自定义哈希表允许我们做特定的优化,例如直接的指针比较和量身定制的探测策略。

`FastHashTable` 使用 FNV-1a 哈希算法。
选择这个算法是因为它速度快,并且能把数据均匀地分布到哈希空间中,从而把冲突降到最低。

```csharp
internal unsafe class FastHashTable
{
    private static uint ComputeHash(byte* ptr, int len)
    {
        // FNV-1a hash - good distribution, fast
        var hash = 2166136261u;
        for (var i = 0; i < len; i++)
        {
            hash ^= ptr[i];
            hash *= 16777619u;
        }

        return hash;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/custom-fasthashtable-part-1-69958252/?t=460)

表中的数据存放在一个 `Entry` 结构体里。
为了提升性能,温度以整数而不是 double 存储;这是通过把输入值乘以 10 实现的。
这个结构体保留原始的 UTF-8 字节以便与指针做快速比较,同时也把气象站名字缓存为 string,用于最终生成输出。

```csharp
internal unsafe class FastHashTable
{
    public struct Entry
    {
        public byte[] Name;          // Raw UTF-8 bytes (for comparison)
        public string StationName;   // Cached string (created once)
        public uint Hash;
        public int Min;              // Temperature * 10
        public int Max;              // Temperature * 10
        public long Sum;             // Sum of temperatures * 10
        public long Count;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/custom-fasthashtable-part-1-69958252/?t=490)

这个哈希表用 0.75 的负载因子阈值来管理容量。
当唯一条目的数量超过当前容量的 75% 时,表就可以扩容。
容量始终保持为 2 的幂,这样索引和分布都更高效。

```csharp
internal unsafe class FastHashTable
{
    private const int DefaultCapacity = 1024;
    private const int MaxCapacity = 32768;
    private const double LoadFactorThreshold = 0.75;

    private Entry[] _entries;
    private int _count;
    private readonly bool _allowResize;

    public FastHashTable(int expectedCount = 500, bool allowResize = true)
    {
        _allowResize = allowResize;

        // Calculate initial capacity
        var targetCapacity = (int)(expectedCount / LoadFactorThreshold);
        var capacity = NextPowerOf2(Math.Max(DefaultCapacity, targetCapacity));
        capacity = Math.Min(capacity, MaxCapacity);

        _entries = new Entry[capacity];
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/custom-fasthashtable-part-1-69958252/?t=610)

为了高效地计算下一个 2 的幂,实现使用了无分支的位运算。
这种技巧避免了 CPU 的分支预测失败,而分支预测失败在紧凑循环里会明显影响性能。
通过移位和 OR 运算,这段代码不用传统的条件逻辑就能确定正确的容量。

```csharp
// Branchless
private static int NextPowerOf2(int n)
{
    if (n <= 0)
        return 1;

    n--;
    n |= n >> 1;
    n |= n >> 2;
    n |= n >> 4;
    n |= n >> 8;
    n |= n >> 16;

    return n + 1;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/custom-fasthashtable-part-1-69958252/?t=700)

## 4. CPU Pipelining and Branching

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/cpu-pipelining-and-branching-69958253/) · 17:35

### 总结

CPU 流水线和分支是高性能计算中的基础概念,它们决定了处理器如何处理指令。
流水线让 CPU 通过 fetch、decode、execute 等阶段重叠执行多条指令,就像一条装配线。
然而,条件分支(例如 if 语句或循环)会打断这个流程;如果 CPU 的分支预测器猜错了,整条流水线都必须被清空,浪费多个时钟周期。
为了在 1 Billion Row Challenge 中获得最高性能,开发者会使用无分支编程技巧,用位运算和算术代替条件逻辑,让 CPU 流水线保持满载,避免预测失败的开销。

### 核心概念

*   **Instruction Pipelining**:把指令执行划分成若干离散阶段(Fetch、Decode、Execute)以提高吞吐量的过程。
*   **Instruction Level Parallelism (ILP)**:CPU 在不同流水线阶段同时执行多条指令的能力。
*   **Branch Misprediction**:当 CPU 对条件跳转的路径猜错时所付出的性能代价,需要清空流水线。
*   **Pipeline Stall/Flush**:由打断顺序执行的条件逻辑引起的流水线延迟或重置。
*   **Branchless Programming**:用数学或位运算代替条件逻辑的技术,以确保执行路径线性、可预测。
*   **Bitwise Operators**:使用 `|`、`&`、`>>` 这类运算符,在单个 CPU 周期内完成逻辑运算而不产生分支。

### 课程笔记

当一个 C# 应用运行时,JIT(Just-In-Time)编译器把代码转换成汇编指令。
对于两个变量相加这种简单操作,CPU 走的是一条顺序路径:把值从内存地址加载到寄存器,执行加法,再把结果存回内存。

```csharp
var x = 10;
var y = 20;

var sum = x + y;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/cpu-pipelining-and-branching-69958253/?t=25)

然而,引入 `if` 语句这样的条件逻辑会改变执行流。
CPU 必须对条件求值,并可能跳转到另一个内存地址,这要求 CPU 记录跳转之后该返回到哪里。

```csharp
var x = 10;
var y = 20;

var sum = x + y;

if (sum > 99)
    sum = 0;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/cpu-pipelining-and-branching-69958253/?t=35)

#### The CPU Pipeline

为了优化吞吐量,现代 CPU 使用 **Instruction Pipelining**。
这可以类比成一家有多名员工的咖啡店:一个人负责点单(**Fetch**),一个人负责做咖啡(**Decode**),一个人负责送出(**Execute**)。
这让 CPU 能够同时在不同阶段处理三条不同的指令。
在每个时钟周期(tick)上,Fetch 阶段取入一条新指令,Decode 阶段处理上一条,Execute 阶段完成再上一条。

#### The Impact of Branching

问题出在分支上。
当 CPU 取入紧跟在条件分支之后的指令时,它还不知道这个分支会不会被走到。
如果条件不成立(例如需要一个原本没预料到的跳转),CPU 就必须"打断"或清空流水线。
这意味着当前正在 fetch 或 decode 的所有指令都要被丢弃,损失若干个时钟周期。

```csharp
var x = 10;
var y = 20;

var sum = x + y;

while (sum > 0)
{
    sum--;
}

if (sum > 99)
    sum = 0;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/cpu-pipelining-and-branching-69958253/?t=220)

现代 CPU 使用 **Branch Prediction**,根据执行历史来猜测会走哪条路径。
虽然它对循环或可预测的模式很有效,但预测失败的代价依然高昂。
在 1 Billion Row Challenge 中,每一个周期都重要,所以我们更倾向于 **branchless programming**。

#### Branchless Programming Techniques

无分支编程指的是用位运算或算术来得到同样的结果,而不使用条件跳转。
例如,计算下一个 2 的幂可以用位移和 OR 运算来完成,而不是用带条件的循环:

```csharp
private static int NextPowerOf2(int n)
{
    if (n <= 0)
        return 1;

    n--;
    n |= n >> 1;
    n |= n >> 2;
    n |= n >> 4;
    n |= n >> 8;
    n |= n >> 16;

    return n + 1;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/cpu-pipelining-and-branching-69958253/?t=925)

同样地,在解析温度时,与其用 `if` 语句处理负号,不如把符号存成一个乘数(1 或 -1),最后把结果乘上它。
这保证执行路径保持线性、顺序,避免分支逻辑的开销。

```csharp
if (len == 3)
{
    // 1.2 -> 12
    value = ((ptr[0] - '0') * 10) + (ptr[2] - '0');
}
else
{
    // 12.3 -> 123
    value = ((ptr[0] - '0') * 100)
        + ((ptr[1] - '0') * 10)
        + (ptr[3] - '0');
}

// Branchless return using a sign multiplier
return sign * value;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/cpu-pipelining-and-branching-69958253/?t=1030)

## 5. Custom: FastHashTable Part 2

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/custom-fasthashtable-part-2-69958254/) · 11:07

### 总结

本课详细讲解自定义 FastHashTable 的扩容与冲突解决策略的实现。
它解释了为什么在表容量增大时必须对所有已有条目重新哈希,因为按位取索引的逻辑依赖于当前数组的长度。
实现使用线性探测来解决冲突,借助 CPU 缓存行的局部性获得高性能的顺序内存访问。
此外,本课演示了如何用 ref 关键字直接访问堆上的 struct 条目,避免把数据拷贝到栈上的性能开销。

### 核心概念

* **Dynamic Resizing**:增大哈希表容量,并对所有已有条目重新哈希以保持索引正确。
* **Bitwise Indexing**:用 `hash & (length - 1)` 作为取模运算的无分支、单周期替代方案。
* **Linear Probing**:一种顺序查找下一个空槽的冲突解决策略,由于 CPU 缓存行预取而非常高效。
* **Reference Access**:用 `ref` 关键字直接在数组内操作 `struct` 条目,避免不必要的内存拷贝。
* **Entry Iteration**:实现 `GetEntries`,让线程本地结果可以合并到最终的结果字典里。

### 课程笔记

`FastHashTable` 需要一种机制,在处理完成后取出全部存储的数据。
这是通过 `GetEntries` 方法实现的,它遍历内部数组并 yield 出那些已经被填充的条目。
这对 1BRC 挑战的最后阶段至关重要,因为那时要把各线程本地的哈希表合并成一个结果集。

```csharp
public FastHashTable(int expectedCount = 500, bool allowResize = true)
{
    _allowResize = allowResize;

    // Calculate initial capacity
    var targetCapacity = (int)(expectedCount / LoadFactorThreshold);
    var capacity = NextPowerOf2(Math.Max(DefaultCapacity, targetCapacity));
    capacity = Math.Min(capacity, MaxCapacity);

    _entries = new Entry[capacity];
}

public IEnumerable<Entry> GetEntries()
{
    foreach (var entry in _entries)
    {
        if (entry.Name != null)
            yield return entry;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/custom-fasthashtable-part-2-69958254/?t=10)

#### Resizing and Rehashing

给哈希表扩容并不像扩大底层数组那么简单。
因为索引计算依赖数组长度(例如 `index = hash % length`),改变容量就会改变每一个已有 key 的目标索引。
因此,当表增长时,每一个已有条目都必须重新哈希,并移动到更大数组中的新的正确位置。

为了实现这一点,`Resize` 方法接受一个 `newCapacity`。
它把当前的条目存进一个临时变量,用增大后的容量初始化一个新数组,并重置内部计数。
然后它遍历旧条目,把它们重新插入新数组。

```csharp
private void Resize(int newCapacity)
{
    var oldEntries = _entries;
    _entries = new Entry[newCapacity];
    _count = 0;

    // Rehash all existing entries
    foreach (var oldEntry in oldEntries)
    {
        if (oldEntry.Name != null)
        {
            fixed (byte* ptr = oldEntry.Name)
            {
                // Reinsert into new table
                var hash = oldEntry.Hash;
                var index = (int)(hash & (uint)(_entries.Length - 1));

                while (true)
                {
                    ref Entry entry = ref _entries[index];

                    if (entry.Name is null)
                    {
                        // Found empty slot - copy data
                        entry = oldEntry;
                        _count++;
                        break;
                    }

                    // Linear probing
                    index = (index + 1) & (_entries.Length - 1);
                }
            }
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/custom-fasthashtable-part-2-69958254/?t=190)

#### Performance Optimizations

在索引逻辑中,取模运算符(`%`)被替换成按位 AND 运算符:`hash & (length - 1)`。
取模运算可能需要 3 到 4 个 CPU 周期,而按位 AND 是无分支操作,通常在一个周期内完成。
这个优化要求数组容量始终是 2 的幂。

在 `while` 循环里访问条目时使用了 `ref` 关键字:`ref Entry entry = ref _entries[index]`。
由于 `Entry` 是 `struct`,按常规方式访问会把数据从堆上分配的数组拷贝到栈上。
使用引用就能避免这次拷贝,并直接在它位于堆上的内存位置修改该条目。

#### Collision Resolution: Linear Probing

当两个不同的 key 得到相同的索引时就发生了冲突。
这个实现使用线性探测,如果目标槽位已被占用,就检查数组中紧邻的下一个槽位。

```csharp
// Linear probing
index = (index + 1) & (_entries.Length - 1);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/custom-fasthashtable-part-2-69958254/?t=385)

在通用哈希表里,线性探测有时会导致分布不佳,但在这里它非常有效,因为内存访问是顺序的。
当 CPU 加载某个内存地址时,它也会把周围的数据一并载入 CPU 缓存(缓存行)。
由于线性探测检查的是相邻槽位,下一个槽位的数据很可能已经在缓存里,这使得查找比那些跳到远处内存位置的策略快得多。

## 6. Custom: FastHashTable Part 3

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/custom-fasthashtable-part-3-69958255/) · 13:50

### 总结

本课详细讲解自定义 FastHashTable 中 AddOrUpdate 方法的实现,聚焦于 1 Billion Row Challenge 所需的高性能技巧。
内容涵盖基于负载因子的动态扩容、使用 Buffer.MemoryCopy 的指针内存操作,以及通过线性探测高效解决冲突。
这个实现强调无分支编程,用按位取索引和 JIT 优化过的 Math 方法来更新气象站统计数据,而不引入条件分支。

### 核心概念

*   **Dynamic Resizing**:当负载因子(75%)被超过时自动把表容量翻倍以维持性能。
*   **Pointer-based Memory Copying**:用 `Buffer.MemoryCopy` 直接在内存位置之间移动数据,避开托管开销。
*   **Branchless Indexing**:用按位 AND 运算计算索引,当容量是 2 的幂时这比取模运算符快得多。
*   **Linear Probing**:一种冲突解决策略,依次探测后续槽位直到找到正确的 key 或一个空槽。
*   **JIT-Optimized Statistics**:用 `Math.Min` 和 `Math.Max` 更新温度值,让 JIT 编译器生成优化过的无分支代码。

### 课程笔记

`AddOrUpdate` 方法是这个自定义哈希表的核心,它提供插入新气象站或更新已有气象站统计数据的功能。
与标准的 `Dictionary` 不同,这个实现直接用指针来高效处理原始字节数据。

```csharp
_entries = new Entry[capacity];
    }

    public void AddOrUpdate(byte* namePtr, int nameLen, int temperature)
    {

    }

    private void Resize(int newCapacity) ...
    public IEnumerable<Entry> GetEntries()
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/custom-fasthashtable-part-3-69958255/?t=10)

在添加条目之前,表会检查是否需要扩容。
如果 `_allowResize` 为 true 并且当前计数超过了 `LoadFactorThreshold`(通常是 0.75),容量就翻倍。
随后用与 `_entries.Length - 1` 的按位 AND 运算来计算索引。
这是一个无分支操作,大约耗费一个 CPU 周期,而标准的取模运算符要 3 到 4 个周期。

```csharp
public void AddOrUpdate(byte* namePtr, int nameLen, int temperature)
    {
        // Check if resize is needed before adding
        if (_allowResize && _count >= _entries.Length * LoadFactorThreshold)
        {            var newCapacity = Math.Min(_entries.Length * 2, MaxCapacity);
            if (newCapacity > _entries.Length)
            {                Resize(newCapacity);
            }
        }

        var hash = ComputeHash(namePtr, nameLen);
        var index = (int)(hash & (uint)(_entries.Length - 1)); // // 1 cycle
        // 143 % 10 => [3] // ~3-4 cycle
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/custom-fasthashtable-part-3-69958255/?t=55)

这个实现用一个 `while(true)` 循环来做线性探测。
在循环内部,我们拿到当前索引处条目的引用。
如果条目的 `Name` 属性为 null,就说明这是一个空槽(第一次访问)。

当找到空槽时,用 `Buffer.MemoryCopy` 把气象站名字从源指针拷贝到条目内一个新的 byte 数组里。
这会直接在内存一侧完成拷贝,而不必把数据不必要地带到"用户侧"(托管空间)。
随后用 `Encoding.UTF8.GetString` 一次性创建 `StationName` 字符串,供后续报告使用。

```csharp
ref Entry entry = ref _entries[index];

if (entry.Name is null) // first visit
{
    // New entry - copy name bytes AND create string once
    entry.Name = new byte[nameLen];

    fixed (byte* dest = entry.Name)
    {
        Buffer.MemoryCopy(namePtr, dest, nameLen, nameLen);
    }

    entry.StationName = Encoding.UTF8.GetString(entry.Name); // <- Create string O
    entry.Hash = hash;
    entry.Min = temperature;
    entry.Max = temperature;
    entry.Sum = temperature;
    entry.Count = 1;
    _count++;

    return;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/custom-fasthashtable-part-3-69958255/?t=250)

如果槽位不为空,实现必须验证该条目是否与当前 key 匹配。
它先做一次提前退出的检查,比较存储的哈希值和名字长度。
如果这两项匹配,再用指针做逐字节比较。
这是必要的,因为不同的字符串偶尔会产生相同的哈希码(哈希冲突)。

```csharp
// not the first visit
    if (entry.Hash == hash && entry.Name.Length == nameLen)
    {
        // Verify bytes match
        fixed (byte* entryName = entry.Name)
        {
            var match = true;
            for (var i = 0; i < nameLen; i++)
            {                if (entryName[i] != namePtr[i])
                {                    match = false;
                    break;
                }
            }
        }

        if (match)
        {
            // Update statistics
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/custom-fasthashtable-part-3-69958255/?t=430)

如果确认匹配,就更新统计数据(Min、Max、Sum 和 Count)。
这里优先使用 `Math.Min` 和 `Math.Max` 而不是 `if` 语句,因为 JIT 编译器能识别这些方法并把它们优化成无分支指令,从而降低 CPU 分支预测失败的性能代价。
如果没有找到匹配,就递增索引(使用线性探测)去检查下一个槽位。

```csharp
        if (match)
        {
            // Update statistics
            //if (temperature < entry.Min) entry.Min = temperature;
            //if (temperature > entry.Max) entry.Max = temperature;

            // less branching???
            // source.dot.net
            entry.Min = Math.Min(temperature, entry.Min);
            entry.Max = Math.Max(temperature, entry.Max);

            entry.Sum += temperature;
            entry.Count++;
            return;
        }
    }
}

// Linear probing
index = (index + 1) & (_entries.Length - 1);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/custom-fasthashtable-part-3-69958255/?t=745)

## 7. CPU Inlining

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/cpu-inlining-69958256/) · 8:59

### 总结

CPU inlining 是一项关键的性能优化,JIT 编译器把函数调用替换成函数体本身,以消除分支带来的开销。
在 1 Billion Row Challenge 的语境下,给"热路径"上的方法(例如哈希计算和温度解析)加上 AggressiveInlining 特性,可以把 CPU 跳转和保存状态的操作降到最低,不过这会因为代码重复而增大最终应用的体积。

### 核心概念

* **Function Call Overhead**:每次函数调用都会在 CPU 指令列表中产生一个分支,系统需要跳到新的内存地址、保存当前状态、执行指令,然后再返回。
* **Hot Path**:被执行极多次的代码段(例如 10 亿次迭代)。这些区域里微小的开销会累积成显著的性能瓶颈。
* **Aggressive Inlining**:一个指示 Just-In-Time(JIT)编译器把方法逻辑直接嵌入调用方指令序列、而不是执行标准调用的指令。
* **JIT Level Optimization**:内联发生在 JIT 编译阶段,也就是 C# 被翻译成汇编指令的时候。
* **Code Bloat**:内联的主要缺点,把代码块复制到多个调用点会增大应用二进制的体积。

### 课程笔记

代码被编译时,JIT 会在 CPU 内存中创建一份汇编指令列表。
循环内部的标准函数调用会产生分支。
每次函数被调用,CPU 必须跳到另一段指令去执行,然后再跳回原来的位置继续执行。
这个过程需要保存和恢复 CPU 状态。

```csharp
// CPU Inlining

var x = 10;

while(x > 0)
{
    x--;
    aa();
    x = 10;
}

void aa()
{

}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/cpu-inlining-69958256/?t=55)

在 1 Billion Row Challenge 这类高性能场景里,"热路径"上的方法会被调用数十亿次。
为了优化这一点,开发者会使用 `[MethodImpl(MethodImplOptions.AggressiveInlining)]` 特性。
它告诉 JIT 把目标函数的指令直接复制到调用点,完全去掉跳转。

在这个项目中,`FastHashTable` 里的 `AddOrUpdate` 函数是这项优化的首选对象,因为数据集里的每一行都会调用它。

```csharp
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddOrUpdate(byte* namePtr, int nameLen, int temperature)
    {
        // Check if resize is needed before adding
        if (_allowResize && _count >= _entries.Length * LoadFactorThreshold)
        {
            var newCapacity = Math.Min(_entries.Length * 2, MaxCapacity);
            if (newCapacity > _entries.Length)
            {
                Resize(newCapacity);
            }
        }

        var hash = ComputeHash(namePtr, nameLen);
        var index = (int)(hash & (uint)(_entries.Length - 1));

        while (true)
        {
            ref Entry entry = ref _entries[index];

            if (entry.Name is null)
            {
                entry.Name = new byte[nameLen];
                fixed (byte* dest = entry.Name)
                {
                    Buffer.MemoryCopy(namePtr, dest, nameLen, nameLen);
                }
                entry.StationName = Encoding.UTF8.GetString(entry.Name);
                entry.Hash = hash;
                entry.Min = temperature;
                entry.Max = temperature;
                entry.Sum = temperature;
                entry.Count = 1;
                _count++;
                return;
            }

            if (entry.Hash == hash && entry.Name.Length == nameLen)
            {
                fixed (byte* entryName = entry.Name)
                {
                    var match = true;
                    for (var i = 0; i < nameLen; i++)
                    {
                        if (entryName[i] != namePtr[i])
                        {
                            match = false;
                            break;
                        }
                    }

                    if (match)
                    {
                        entry.Min = Math.Min(temperature, entry.Min);
                        entry.Max = Math.Max(temperature, entry.Max);
                        entry.Sum += temperature;
                        entry.Count++;
                        return;
                    }
                }
            }
            index = (index + 1) & (_entries.Length - 1);
        }
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/cpu-inlining-69958256/?t=325)

其他被频繁调用的工具函数,例如 `ComputeHash` 或 `ParseTemperatureBranchless`,也应该做激进内联以减少分支开销。

```csharp
[MethodImpl(MethodImplOptions.AggressiveInlining)]
private static uint ComputeHash(byte* ptr, int len)
{
    // FNV-1a hash - good distribution, fast
    var hash = 2166136261u;
    for (var i = 0; i < len; i++)
    {
        hash ^= ptr[i];
        hash *= 16777619u;
    }

    return hash;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/cpu-inlining-69958256/?t=340)

```csharp
[MethodImpl(MethodImplOptions.AggressiveInlining)]
static unsafe int ParseTemperatureBranchless(byte* ptr, int len)
{
    var sign = 1;

    if (ptr[0] == '-')
    {
        sign = -1;
        ptr++;
        len--;
    }

    int value;

    if (len == 3)
    {
        value = ((ptr[0] - '0') * 10) + (ptr[2] - '0');
    }
    else
    {
        value = ((ptr[0] - '0') * 100)
              + ((ptr[1] - '0') * 10)
              + (ptr[3] - '0');
    }

    return sign * value;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/cpu-inlining-69958256/?t=415)

内联通过减少分支来提升性能,但它也有缺点。
如果一个函数很庞大,或者从很多不同的地方被调用,把它到处内联会显著增大应用的二进制体积。
JIT 实际上是在每一个调用点复制了这段逻辑。

```csharp
var x = 1000000000;

while(x > 0)
{
    x--;
    /// something
    /// something
    /// something
    /// //asdlaildaisldialdilasidlaisldiaslidlasiasldiasldiaslidalda
    x += 1;
}


[MethodImpl(MethodImplOptions.AggressiveInlining)]
void aa()
{

}


[MethodImpl(MethodImplOptions.AggressiveInlining)]
void bb()
{
    //asdlaildaisldialdilasidlaisldiaslidlasiasldiasldiaslidalda
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/cpu-inlining-69958256/?t=490)

## 8. Introduction to SIMD

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/introduction-to-simd-69958257/) · 10:20

### 总结

本课介绍 Single Instruction Multiple Data(SIMD),这是一项强大的 CPU 特性,允许一条指令同时作用于多个数据点。
理解 Arithmetic Logical Unit(ALU)以及它如何用标志位(Overflow、Zero、Negative)来处理比较之类的运算之后,开发者就能利用 SIMD 大幅提升性能。
在 1 Billion Row Challenge 的语境下,SIMD 让应用可以在一个 CPU 周期内跨 32 字节数据搜索分号和换行这类分隔符,而不是逐字节检查,这依赖于 AVX2 这样的现代指令集。

### 核心概念

* SIMD(Single Instruction Multiple Data)
* ALU(Arithmetic Logical Unit)与 CPU 标志位
* 向量化与向量宽度(AVX2、AVX-512)
* 并行比较运算
* 掩码与位操作(MoveMask、TrailingZeroCount)

### 课程笔记

#### The Role of the ALU

要理解 SIMD,必须先理解 Arithmetic Logical Unit(ALU)。
ALU 是 CPU 中负责算术运算的部件。
它通常接受两个数据输入和一个运算符(比如加法)。
除了产生结果,ALU 还会设置特定的 CPU 标志位,例如:
* **Overflow**:表示结果超出了寄存器的容量。
* **Zero**:表示运算结果为零。
* **Negative**:表示结果是负数。

这些标志位对逻辑比较至关重要。
例如,"大于"运算(`x > y`)常常被 CPU 当作减法(`x - y`)来执行。
随后 CPU 检查标志位;如果结果既不为零也不为负,条件就成立。

```csharp
var x = 5;
var y = 2;

if (x > y)
{
    /*
    Flags: Ov (Overflow), Zero, Neg (Negative)
    The CPU performs x - y and checks if the result is positive via flags.
    */
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/introduction-to-simd-69958257/?t=235)

#### The SIMD Paradigm

现代 CPU 包含多个 ALU,因此可以并行处理数据。
SIMD(Single Instruction Multiple Data)利用这一点,把一条指令(运算符)同时作用于多个数据点。
标量运算一次处理一对输入,而 SIMD 处理的是一个输入"向量"。

在 1 Billion Row Challenge 目前的实现里,代码是逐字节地搜索分号和换行。
这种标量做法效率不高,因为它忽略了硬件的并行处理能力。

```csharp
// Current scalar approach: checking bytes one by one
while(pos < endPos)
{
    // \nHamburg;23.9\n

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
    // ...
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/introduction-to-simd-69958257/?t=460)

#### Vector Widths and Hardware Support

SIMD 的硬件支持按 AVX(Advanced Vector Extensions)这样的指令集来分类。
一次能处理多少数据取决于 CPU 的向量宽度:
* **AVX2**:支持 256 位向量,可以同时处理 32 个字节(字符)。
* **AVX-512**:支持 512 位向量,可以同时处理 64 个字节。

#### Applying SIMD to Delimiter Searching

使用 SIMD 之后,应用可以把一块数据(例如 32 字节)加载进寄存器,并在一条指令里把每个字节与目标字符(比如分号)比较。
这会生成一个位掩码,其中每一位表示对应位置是否匹配。
借助位操作(例如 `TrailingZeroCount`),分隔符的确切位置可以立即确定,而不需要手写循环。

```csharp
[MethodImpl(MethodImplOptions.AggressiveInlining)]
static unsafe long FindByteFast(byte* basePtr, long start, long end, Vector256<byte> targetVec, byte target)
{
    var pos = start;

    if (Avx.IsSupported)
    { // Process 32 bytes at a time using AVX2
        while (pos + 32 <= end)
        { // Load 32 bytes into a 256-bit register
            var data = Avx.LoadVector256(basePtr + pos);
            // Compare all 32 bytes against the target (e.g., ';') in one instruction
            var cmp = Avx2.CompareEqual(data, targetVec);
            // Create a bitmask from the comparison results
            var mask = (uint)Avx2.MoveMask(cmp);
            
            if (mask != 0)
                return pos + BitOperations.TrailingZeroCount(mask);

            pos += 32;
        }
    }

    // Fallback for remaining bytes or if AVX is not supported
    while (pos < end)
    {
        if (basePtr[pos] == target)
            return pos;
        pos++;
    }

    return end;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/introduction-to-simd-69958257/?t=514)

## 9. Finalizing the Approach

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/finalizing-the-approach-69958258/) · 13:56

### 总结

本课把 SIMD(Single Instruction, Multiple Data)通过 AVX2 库集成进来,以加速 1 Billion Row Challenge 中的分隔符搜索。
通过一次处理 32 字节而不是逐字节处理,这个实现显著减少了定位分号和换行所花的 CPU 周期。
这个方案把硬件内建函数与自定义的快速哈希表以及无分支温度解析结合起来,为海量数据集打造出一条高度优化的处理流水线。

### 核心概念

* **SIMD Intrinsics**:利用 `System.Runtime.Intrinsics.X86`(AVX2)对 256 位向量(32 字节)执行并行运算。
* **Vectorized Searching**:用 `Avx2.CompareEqual` 和 `Avx2.MoveMask` 在一个 CPU 周期内于 32 字节块中找到特定字符。
* **Bit Manipulation**:用 `BitOperations.TrailingZeroCount` 从位掩码中确定字符匹配的确切索引。
* **Hardware Support Checks**:在运行时检查 `Avx2.IsSupported` 和 `Avx512F.IsSupported`,以确保兼容性并提供回退路径。
* **Pipeline Integration**:把 memory-mapped files、并行分块、SIMD 搜索和零拷贝哈希表更新结合起来。

### 课程笔记

要在 .NET 中使用 SIMD(Single Instruction, Multiple Data),这个实现用到了 `System.Runtime.Intrinsics` 命名空间。
它允许做底层的 CPU 优化,同时仍然是托管的、由 JIT 编译器优化的。
这个实现的主要目标是 AVX2,它作用于 256 位向量(32 字节),不过代码同时也会检查 AVX-512(64 字节)的支持情况。

```csharp
using Shared;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using System.Text;

Console.WriteLine("=== Level 5: SIMD (AVX2) Implementation ===");
Console.WriteLine($"File: {GlobalConstants.FilePath}");
Console.WriteLine($"Processor Count: {Environment.ProcessorCount}");
Console.WriteLine($"AVX2 Supported: {Avx2.IsSupported}"); // 32 bytes
Console.WriteLine($"AVX-512 Supported: {Avx512F.IsSupported}"); // 64 bytes
Console.WriteLine();

if (!File.Exists(GlobalConstants.FilePath))
{
    Console.WriteLine($"ERROR: File not found at {GlobalConstants.FilePath}");
    return;
}

if (!Avx2.IsSupported)
{
    Console.WriteLine("WARNING: AVX2 not supported. Performance will be limited.");
}

GC.Collect();
GC.WaitForPendingFinalizers();
GC.Collect();

var stopwatch = Stopwatch.StartNew();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/finalizing-the-approach-69958258/?t=10)

这个实现用 memory-mapped files 来获得数据的基指针。
文件按处理器数量被切分成若干块,每个线程把自己的起止位置对齐到最近的换行符,以确保处理的是完整的行。

```csharp
var fileInfo = new FileInfo(GlobalConstants.FilePath);
var fileSize = fileInfo.Length;

if (fileSize == 0)
{
    Console.WriteLine("File is empty.");
    return;
}

var threadCount = Environment.ProcessorCount;
var threadLocalResults = new FastHashTable[threadCount];
var lineCounters = new long[threadCount];

using var mmf = MemoryMappedFile.CreateFromFile(GlobalConstants.FilePath, FileMode.Open, null, 0, MemoryMappedFileAccess.Read);
using var accessor = mmf.CreateViewAccessor(0, fileSize, MemoryMappedFileAccess.Read);

unsafe
{
    byte* basePtr = null;
    accessor.SafeMemoryMappedViewHandle.AcquirePointer(ref basePtr);

    try
    {
        // Skip UTF-8 BOM if present
        long dataStart = 0;
        if (fileSize >= 3 && basePtr[0] == 0xEF && basePtr[1] == 0xBB && basePtr[2] == 0xBF)
        {
            dataStart = 3;
        }

        var dataSize = fileSize - dataStart;
        var chunkSize = dataSize / threadCount;

        Parallel.For(0, threadCount, threadIndex =>
        {
            var startPos = dataStart + (threadIndex * chunkSize);
            var endPos = (threadIndex == threadCount - 1) ? fileSize : dataStart + ((threadIndex + 1) * chunkSize);

            // Align to line boundaries
            if (startPos > dataStart)
            {                while (startPos < fileSize && basePtr[startPos - 1] != '\n')
                    startPos++;
            }

            if (endPos < fileSize && threadIndex < threadCount - 1)
            {                while (endPos < fileSize && basePtr[endPos - 1] != '\n')
                    endPos++;
            }

            var localTable = new FastHashTable(
                expectedCount: GlobalConstants.ExpectedStationCount,
                allowResize: true);  // Enable dynamic growth by default

            long localLineCount = 0;
            var pos = startPos;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/finalizing-the-approach-69958258/?t=130)

为了高效搜索分隔符,代码创建了用目标字节(分号或换行)填满的 256 位向量。
`FindByteFast` 方法用 AVX2 加载 32 字节数据,并在一次运算中把它们与目标向量比较。

```csharp
[MethodImpl(MethodImplOptions.AggressiveInlining)]
static unsafe long FindByteFast(byte* basePtr,
                                long start,
                                long end,
                                Vector256<byte> targetVec,
                                byte target)
{
    var pos = start;

    if (Avx.IsSupported)
    {
        while (pos + 32 <= end)
        {
            var data = Avx.LoadVector256(basePtr + pos);
            var cmp = Avx2.CompareEqual(data, targetVec);
            var mask = (uint)Avx2.MoveMask(cmp);
            
            if (mask != 0)
                return pos + BitOperations.TrailingZeroCount(mask);

            pos += 32;
        }
    }

    while (pos < end)
    {
        if (basePtr[pos] == target)
            return pos;

        pos++;
    }

    return end;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/finalizing-the-approach-69958258/?t=325)

在 `FindByteFast` 里,`Avx2.CompareEqual` 产生一个向量:匹配目标的字节为 `0xFF`,否则为 `0x00`。
`Avx2.MoveMask` 随后把每个字节的最高位提取到一个 32 位整数中。
如果这个掩码非零,说明找到了匹配。
`BitOperations.TrailingZeroCount` 确定第一个置位比特的位置,它对应该字符在这个 32 字节块中的索引。

主处理循环用这些向量来定位气象站名字和温度。
一旦位置确定,就用无分支的整数方法解析温度,并通过零拷贝指针把结果存入 `FastHashTable`。

```csharp
            // SIMD vectors for delimiter search
            Vector256<byte> semicolonVec = Vector256.Create((byte)';');
            Vector256<byte> newlineVec = Vector256.Create((byte)'\n');

            while (pos < endPos)
            {
                var lineStart = pos;

                // Find semicolon using SIMD
                var semicolonPos = FindByteFast(basePtr, pos, endPos, semicolonVec, (byte)';');
                if (semicolonPos >= endPos)
                    break;

                // Find newline using SIMD
                var newlinePos = FindByteFast(basePtr, semicolonPos + 1, endPos, newlineVec, (byte)'\n');
                if (newlinePos >= endPos)
                    break;

                // Station name: [lineStart, semicolonPos)
                var nameLen = (int)(semicolonPos - lineStart);
                var namePtr = basePtr + lineStart;

                // Temperature: [semicolonPos+1, newlinePos) - handle \r\n
                var tempPtr = basePtr + semicolonPos + 1;
                var tempLen = (int)(newlinePos - semicolonPos - 1);
                if (tempLen > 0 && basePtr[newlinePos - 1] == '\r')
                    tempLen--;

                // Parse temperature as integer (branchless) - scaled by 10
                var temperature = ParseTemperatureBranchless(tempPtr, tempLen);

                // Update hash table (zero-copy)
                localTable.AddOrUpdate(namePtr, nameLen, temperature);
                localLineCount++;

                pos = newlinePos + 1;
            }

            threadLocalResults[threadIndex] = localTable;
            lineCounters[threadIndex] = localLineCount;
        });
    }
    finally
    {
        accessor.SafeMemoryMappedViewHandle.ReleasePointer();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/finalizing-the-approach-69958258/?t=670)

## 10. Testing the Fastest Version

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/testing-the-fastest-version-69958259/) · 8:32

### 总结

本课评估 SIMD 优化版本在 1 Billion Row Challenge 上的性能,对十亿行达到了约 1.4 秒的处理时间。
它解释了标量指针循环与 `Span.IndexOf` 这类 SIMD 加速方法之间的性能差距,并回顾了最终架构:memory-mapped files、带动态扩容的自定义高性能哈希表,以及无分支整数解析。
最终吞吐量达到约 17 GB 的数据处理量,相比之前的非 SIMD 版本提升了 15% 到 20%。

### 核心概念

* **SIMD vs. Scalar Processing**:手写的指针循环是标量的(一次处理一个字节),而 `Span.IndexOf` 这类 SIMD 加速方法在一条指令里处理多个字节(例如通过 AVX2 处理 32 字节)。
* **JIT Inlining and Overhead**:对于耗时不到 1ns 的操作,性能差异往往来自 JIT 的内联决策和方法调用开销(比如 `.AsSpan()`),而不是算法效率。
* **Vectorized Delimiter Search**:用 `Vector256<byte>` 扫描分号和换行,显著减少花在行解析上的周期。
* **Dynamic Hash Table Growth**:在自定义哈希表中实现基于负载因子的扩容策略,确保唯一 key 增多时性能保持稳定。
* **Integer-Based Temperature Parsing**:把温度乘以 10 用整数表示,避免热路径上浮点运算的开销。

### 课程笔记

#### SIMD vs. Scalar Performance Gap

在高性能 .NET 代码里,手写的指针循环往往比 `Span.IndexOf` 这类内置方法更慢。
这是因为 `Span.IndexOf` 用 SIMD(Single Instruction, Multiple Data)做了向量化。
手动的指针扫描逐个检查字符,而 SIMD 让 CPU 在一个时钟周期内比较 32 字节(使用 AVX2)或 16 字节(使用 SSE)。

```csharp
 * [2] Why does PointerScan slow down dramatically as the string grows?
 * --------------------------------------------------------------------
 *   PointerScan is a scalar loop -> checks 1 character per step.
 *   StringIndexOf / SpanIndexOf  -> SpanHelpers.IndexOf -> SIMD (AVX2/SSE4.2):
 *
 *     PointerScan (scalar), "Petropavlovsk-Kamchatsky":
 *       'P'==';'? 'e'==';'? 't'==';'? ... -> 24 steps
 *
 *     StringIndexOf (AVX2, 256-bit - 16 chars/iteration):
 *       ['P','e','t','r','o','p','a','v','l','o','v','s','k','-','K','a'] -> No
 *       ['m','c','h','a','t','s','k','y',';', ... ] -> Yes  -> 2 steps
 *
 *   The farther away the separator is, the wider the scalar/SIMD step gap grows.
 *   A hand-written pointer loop cannot beat SIMD.
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/testing-the-fastest-version-69958259/?t=130)

基准测试显示,对于较长的字符串,手动指针扫描可能比 SIMD 加速的替代方案慢五倍以上。

```csharp
| PointerScan    | Petropavlovsk ... ;-12.7 | 6.0376 ns  | 5.49  | <- 5.5x slower!
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/testing-the-fastest-version-69958259/?t=80)

#### Core Implementation Architecture

最终的优化版本用 `MemoryMappedFiles` 做零拷贝数据访问,并把文件切分成块,在所有可用的 CPU 核心上并行处理。

```csharp
var fileInfo = new FileInfo(GlobalConstants.FilePath);
var fileSize = fileInfo.Length;

if (fileSize == 0)
{
    Console.WriteLine("File is empty.");
    return;
}

var threadCount = Environment.ProcessorCount;
var threadLocalResults = new FastHashTable[threadCount];
var lineCounters = new long[threadCount];

using var mmf = MemoryMappedFile.CreateFromFile(GlobalConstants.FilePath, FileMode.Open, null, 0, MemoryMappedFileAccess.Read);
using var accessor = mmf.CreateViewAccessor(0, fileSize, MemoryMappedFileAccess.Read);

unsafe
{
    byte* basePtr = null;
    accessor.SafeMemoryMappedViewHandle.AcquirePointer(ref basePtr);

    try
    {
        // Skip UTF-8 BOM if present
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/testing-the-fastest-version-69958259/?t=220)

#### Fast Hash Table with Dynamic Resizing

为了高效处理十亿行的查找,这里使用了一个自定义的 `FastHashTable`。
它的容量是 2 的幂以便快速索引,并用负载因子阈值(0.75)触发动态扩容,确保分布均匀、冲突最少。

```csharp
public FastHashTable(int expectedCount = 500, bool allowResize = true)
{
    _allowResize = allowResize;

    // Calculate initial capacity
    var targetCapacity = (int)(expectedCount / LoadFactorThreshold);
    var capacity = NextPowerOf2(Math.Max(DefaultCapacity, targetCapacity));
    capacity = Math.Min(capacity, MaxCapacity);

    _entries = new Entry[capacity];
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/testing-the-fastest-version-69958259/?t=235)

#### Vectorized Processing Loop

主处理循环用 `Vector256<byte>` 搜索分号和换行这两个分隔符。
这种向量化让引擎在数据中跳进的速度远快于逐字节比较。

```csharp
// SIMD vectors for delimiter search
Vector256<byte> semicolonVec = Vector256.Create((byte)';');
Vector256<byte> newlineVec = Vector256.Create((byte)'\n');

while (pos < endPos)
{
    var lineStart = pos;

    // Find semicolon using SIMD
    var semicolonPos = FindByteFast(basePtr, pos, endPos, semicolonVec, (byte)';');
    if (semicolonPos >= endPos)
        break;

    // Find newline using SIMD
    var newlinePos = FindByteFast(basePtr, semicolonPos + 1, endPos, newlineVec, (byte)'\n');
    if (newlinePos >= endPos)
        break;

    // Station name: [lineStart, semicolonPos)
    var nameLen = (int)(semicolonPos - lineStart);
    var namePtr = basePtr + lineStart;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/testing-the-fastest-version-69958259/?t=280)

温度用无分支的整数策略解析,结果通过零拷贝的字节指针存入本地哈希表。

```csharp
    // Temperature: [semicolonPos+1, newlinePos) - handle \r\n
    var tempPtr = basePtr + semicolonPos + 1;
    var tempLen = (int)(newlinePos - semicolonPos - 1);
    if (tempLen > 0 && basePtr[newlinePos - 1] == '\r')
        tempLen--;

    // Parse temperature as integer (branchless) - scaled by 10
    var temperature = ParseTemperatureBranchless(tempPtr, tempLen);

    // Update hash table (zero-copy)
    localTable.AddOrUpdate(namePtr, nameLen, temperature);
    localLineCount++;

    pos = newlinePos + 1;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/testing-the-fastest-version-69958259/?t=310)

#### Final Merging and Results

并行处理结束后,线程本地的结果会被合并进一份最终字典。
这个实现使用缓存好的气象站名字(每个唯一站点只创建一次),以尽量减少合并阶段的分配。

```csharp
foreach (var localTable in threadLocalResults)
{
    if (localTable == null) continue;

    foreach (var entry in localTable.GetEntries())
    {
        var name = entry.StationName;  // ← Use cached string (created only once)

        if (finalResults.TryGetValue(name, out var existing))
        {
            finalResults[name] = (
                Math.Min(existing.Min, entry.Min),
                Math.Max(existing.Max, entry.Max),
                existing.Sum + entry.Sum,
                existing.Count + entry.Count
            );
        }
        else
        {
            finalResults[name] = (entry.Min, entry.Max, entry.Sum, entry.Count);
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/testing-the-fastest-version-69958259/?t=340)

最终输出把整数缩放的温度转换回 double 以符合 1BRC 的格式。
在测试中,这个 SIMD 版本达到了约 1.4 秒的运行时间,处理了 17 GB 数据,吞吐量比之前最快的非 SIMD 版本提升了大约 15% 到 20%。

---

## 运行 Demo

Demo 代码按课程官方仓库 [Dometrain/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet](https://github.com/Dometrain/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet) 的方式组织,本章对应上游的 `Level5_SIMD/` 和 `Benchmarks/Level5_SIMD/`:

```
src/1brc/
├── notes/                  各章笔记
├── Shared/                 SharedTypes.cs:GlobalConstants、ResultLogger、StationStats、StationStatsStruct
├── DataGenerator/          Program.cs:413 个气象站 + Box-Muller 生成器
├── Level1_Naive/           Program.cs:第 3 章的 LINQ 朴素实现
├── Level2_Stream/          Program.cs:第 4 章的 StreamReader 流式实现
├── Level3_Parallel/        Program.cs:第 5 章的 Parallel.For 实现;RaceCondition.cs
├── Level4_SharedMemory/    Program.cs:第 6 章的 MMF + 指针实现;Pointers.cs:指针讲解
├── Level5_SIMD/            Program.cs:本章的 SIMD 实现;FastHashTable.cs:第 3、5、6、7 课的自定义哈希表
└── Benchmarks/             Level4_SharedMemory/:第 6 章的四组基准测试
                            Level5_SIMD/:本章的两组基准测试
```

`FastHashTable.cs` 按第 3、5、6、7 课的顺序拼成一个文件,`Program.cs` 则是第 9 课的完整实现。

### 与课程代码的三处偏差

**一、`Entry` 的两个引用字段声明成可空。**
课上写的是 `public byte[] Name;` 和 `public string StationName;`,而本仓库开着 nullable。
`entry.Name is null` 这个判断本身就说明空槽是正常状态,所以字段声明成 `byte[]?` 和 `string?`,合并阶段取 `entry.StationName!`。

**二、`Resize` 去掉了那个没有用到的 `fixed` 块。**
课上的 `Resize` 里有 `fixed (byte* ptr = oldEntry.Name)`,但循环体里从头到尾没用过 `ptr`,整条搬迁走的是 `entry = oldEntry` 这一句结构体赋值。
留着它只会多一次固定和一个用不上的局部变量。

**三、`FindByteFast` 的硬件判断。**
课上写的是 `if (Avx.IsSupported)`,但块里调用的是 `Avx2.CompareEqual` 和 `Avx2.MoveMask`。
AVX 和 AVX2 是两套指令集,字节向量的比较与掩码提取属于后者,所以这里改成 `Avx2.IsSupported`。

第三点后面还接了一段课上没有的代码,原因见下。

### 这台机器没有 AVX2

本仓库跑在 Snapdragon X 上,`win-arm64`。
`Avx2.IsSupported` 是 `false`,`Avx512F.IsSupported` 也是 `false`,课程那段 AVX2 代码在这里一行都不会执行,整个 `FindByteFast` 会直接掉到最后那个逐字节的标量循环里 - 一章讲 SIMD 的 demo 跑起来完全没有 SIMD。

所以 `FindByteFast` 在 AVX2 分支后面多了一个 `Vector128` 分支:

```csharp
else if (Vector128.IsHardwareAccelerated)
{
    var targetVec128 = targetVec.GetLower();

    while (pos + 16 <= end)
    {
        var data = Vector128.Load(basePtr + pos);
        var mask = Vector128.Equals(data, targetVec128).ExtractMostSignificantBits();

        if (mask != 0)
            return pos + BitOperations.TrailingZeroCount(mask);

        pos += 16;
    }
}
```

`Vector128` 是同一个想法的跨平台 API,JIT 在 ARM64 上把它编译成 AdvSimd(NEON)指令,一步 16 字节。
`targetVec.GetLower()` 只是取寄存器的低半部分,`targetVec` 的 32 个通道装的本来就是同一个字节。

### 跑起来

先用 DataGenerator 生成测量文件,再跑 Level5_SIMD:

```bash
cd src/1brc
dotnet run --project DataGenerator -c Release -- 100_000_000
dotnet run --project Level5_SIMD -c Release
```

下面是 12 核机器上一亿行(1.29 GB)的真实输出。
`{...}` 那一行有一万多个字符(413 个气象站),这里只保留开头,其余用 `…` 省略。

```text
=== Level 5: SIMD (AVX2) Implementation ===
File: C:\Users\iantu\AppData\Local\Temp\1brc\Files\measurements.txt
Processor Count: 12
AVX2 Supported: False
AVX-512 Supported: False
ARM AdvSimd Supported: True

WARNING: AVX2 not supported. Performance will be limited.
{Abéché=-17.3/29.4/74.0, Abha=-25.9/18.0/66.6, Abidjan=-18.2/26.0/68.8, Accra=-17.6/26.4/76.6, Addis Ababa=-32.8/16.0/60.0, Adelaide=-25.9/17.3/59.3, Aden=-15.3/29.1/71.7, Ahvaz=-22.5/25.4/71.1, Albuquerque=-30.4/14.0/57.4, Alexandra=-32.8/11.0/51.8, Alexandria=-21.1/20.0/64.4, Algiers=-30.5/18.2/66.…}

Processed 100,000,000 rows using 12 threads
Found 413 unique stations
Elapsed: 00:00:00.3797349

📁 Results saved: C:\Users\iantu\AppData\Local\Temp\1brc\Files\results.log

Per-Thread Statistics:
  Thread 0: 8,333,746 lines, 413 stations
  Thread 1: 8,332,870 lines, 413 stations
  Thread 2: 8,333,865 lines, 413 stations
  Thread 3: 8,333,641 lines, 413 stations
  Thread 4: 8,333,118 lines, 413 stations
  Thread 5: 8,333,343 lines, 413 stations
  Thread 6: 8,332,896 lines, 413 stations
  Thread 7: 8,332,981 lines, 413 stations
  Thread 8: 8,333,150 lines, 413 stations
  Thread 9: 8,333,783 lines, 413 stations
  Thread 10: 8,334,398 lines, 413 stations
  Thread 11: 8,332,209 lines, 413 stations
```

`{...}` 那一整行与同一个文件上 `Level4_SharedMemory` 的输出逐字节相同 - 自定义哈希表加整数温度换出来的结果,和 `Dictionary<int, ...>` 加 `double` 完全一致。

同一台机器、同一个一亿行文件,各跑三次:

| 实现 | 耗时(三次) | Working Set | Gen0/1/2 |
| --- | --- | --- | --- |
| `Level4_SharedMemory`(第 6 章) | 0.397 / 0.421 / 0.397 s | 1,349 MB | 2/2/2 |
| `Level5_SIMD`(本章) | 0.412 / 0.382 / 0.383 s | 1,348 MB | 2/2/2 |

第 10 课说 SIMD 版本比之前最快的非 SIMD 版本快 15% 到 20%。
这台机器上两者基本打平,Level 5 大约快 2% 到 4%,落在 run-to-run 的抖动范围里。
原因在下面的分隔符基准测试里。

十亿行没有跑:`GlobalConstants.FilePath` 指向临时目录,13 GB 的文件放在那里不合适,而一亿行已经足以把两代实现的差距量出来。

### 第 2 课的整数解析基准测试

```bash
dotnet run --project Benchmarks -c Release -- --filter "*TemperatureIntParseBenchmark*"
```

```text
| Method               | Mean     | Error     | StdDev    | Ratio | RatioSD |
|--------------------- |---------:|----------:|----------:|------:|--------:|
| DoubleParse          | 5.955 ns | 0.1139 ns | 0.1065 ns |  1.00 |    0.02 |
| IntParseBranchless   | 1.301 ns | 0.0258 ns | 0.0307 ns |  0.22 |    0.01 |
| IntParseWithDivision | 4.172 ns | 0.0162 ns | 0.0144 ns |  0.70 |    0.01 |
```

方向与课上一致:课上 `DoubleParse` 2.938 ns、`IntParseBranchless` 0.908 ns、`IntParseWithDivision` 1.121 ns,比值是 1 / 0.31 / 0.38;这里是 1 / 0.22 / 0.70。
无分支整数版本在这台机器上比 `double` 版本快 4.6 倍,比课上的 3.2 倍还多一点。

两处需要说明的地方。
一是课上只给了这张表,没有给 `IntParseWithDivision` 的代码,所以那个方法是本仓库按名字自己写的:保留循环、不假设小数点在第几位,最后用除法把多余的小数位除掉。
它落在 0.70 而不是课上的 0.38,说明"去掉循环"这件事本身贡献了相当大一部分收益,而不只是"换成整数运算"。
二是按第 6 章 `TemperatureParseBenchmark` 那样一次只解析一个固定值时,`IntParseBranchless` 报出来是 0.04 ns,并带着 BenchmarkDotNet 的 "The method duration is indistinguishable from the empty method duration" 警告 - 输入固定又没有循环,JIT 直接把它折叠掉了。
所以这里改成每次调用解析 4096 个不同的温度串,用 `OperationsPerInvoke` 折算回单次的开销。

### 第 8、9 课的分隔符搜索基准测试

课上没有给分隔符搜索本身的基准测试,这组是本仓库自己写的:在 1 MB 的测量数据上反复找 `;` 和换行,分别用标量循环、`FindByteFast` 和 `Span.IndexOf`。
`StationNames` 控制气象站名字的长度,也就是两个分隔符之间的距离。

```bash
dotnet run --project Benchmarks -c Release -- --filter "*DelimiterSearchBenchmark*"
```

```text
| Method          | StationNames | Mean       | Error    | StdDev   | Median     | Ratio | RatioSD |
|---------------- |------------- |-----------:|---------:|---------:|-----------:|------:|--------:|
| ScalarScan      | long         |   367.3 μs |  7.34 μs | 17.44 μs |   361.0 μs |  1.00 |    0.07 |
| SimdScan        | long         |   651.9 μs |  4.05 μs |  3.79 μs |   650.8 μs |  1.78 |    0.08 |
| SpanIndexOfScan | long         |   721.9 μs |  4.40 μs |  4.11 μs |   722.3 μs |  1.97 |    0.09 |
|                 |              |            |          |          |            |       |         |
| ScalarScan      | mixed        |   374.3 μs |  6.47 μs |  6.64 μs |   372.5 μs |  1.00 |    0.02 |
| SimdScan        | mixed        |   937.5 μs |  9.86 μs |  9.22 μs |   932.7 μs |  2.51 |    0.05 |
| SpanIndexOfScan | mixed        | 1,043.6 μs |  9.14 μs |  8.11 μs | 1,041.4 μs |  2.79 |    0.05 |
|                 |              |            |          |          |            |       |         |
| ScalarScan      | short        |   381.1 μs |  5.14 μs |  4.29 μs |   379.8 μs |  1.00 |    0.02 |
| SimdScan        | short        | 1,371.0 μs | 21.22 μs | 26.84 μs | 1,364.7 μs |  3.60 |    0.08 |
| SpanIndexOfScan | short        | 1,527.1 μs | 16.91 μs | 15.81 μs | 1,523.4 μs |  4.01 |    0.06 |
```

第 10 课说"手写的指针循环赢不了 SIMD"。
在这台 ARM64 机器上这个结论没有复现:三种名字长度下标量循环都是最快的,`Span.IndexOf` 每一组都是最慢的。

趋势倒是对的。
名字越长,SIMD 的劣势越小:short 时慢 3.60 倍,mixed 时慢 2.51 倍,long 时慢 1.78 倍。
标量那一列几乎不动(367 到 381 μs),因为缓冲区固定是 1 MB,逐字节扫完整个缓冲区的总比较次数与行的长短无关;变的只是查找被调用了多少次。

换句话说,决定胜负的是每次查找要跨过多少字节。
1BRC 的一行里,气象站名字平均十来个字节,温度只有三到五个字节,而一次向量步长是 16 字节(ARM)或 32 字节(AVX2)。
查找距离比向量宽度还短时,向量化省不下步数,只剩下建立掩码的开销 - 这一点在 ARM64 上尤其明显,因为 `ExtractMostSignificantBits` 没有 x86 `MOVMSK` 那样的单条指令对应,JIT 要用一串移位和归约来模拟。
.NET 自己的 `Span.IndexOf` 走的也是同一条路,所以它同样输给了标量循环。

这也解释了上面那张耗时表:Level 5 相对 Level 4 的收益来自整数解析和 `FastHashTable`,而分隔符搜索这一项在这台机器上是倒贴的,两边基本抵消。
课上的 15% 到 20% 是在有 AVX2 的 x86 机器上测出来的,那里一步 32 字节,而且 `MoveMask` 是一条指令。
