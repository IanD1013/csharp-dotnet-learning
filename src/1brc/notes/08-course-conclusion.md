# Course conclusion

> 课程:[From Zero to Hero: 1 Billion Row Performance Challenge in .NET](https://dometrain.com/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet/) · 第 8 章
> 本章共 3 课,这里只收录第 2 课 Final Project with Conclusion · 约 14:43
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 2 | [Final Project with Conclusion](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/final-project-with-conclusion-69958260/) | 14:43 | [↓](#2-final-project-with-conclusion) |

## 2. Final Project with Conclusion

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/final-project-with-conclusion-69958260/) · 14:43

### 总结

本课讲解 1 Billion Row Challenge 的 Expert Level(Level 6)优化,通过 AVX-512 向量化和自定义的原生内存管理达到最大带宽。
关键技术包括:用 `NativeMemory.AlignedAlloc` 实现一个按缓存行对齐的 `FixedDictionary` 来消除垃圾回收器的开销,以及用 64 字节的 SIMD 向量以接近 20 GB/s 的速度处理数据。
这个实现还利用了无分支的位运算、FNV-1a 哈希的循环展开以及 long-running 任务,把 CPU 利用率和指令级并行度拉到最高。

### 核心概念

- **AVX-512 向量化**:每次迭代处理 64 字节的块,以获得最大吞吐量。
- **原生内存管理**:用 `NativeMemory.AlignedAlloc` 在托管堆之外管理内存,降低 GC 压力。
- **内存对齐**:把数据结构对齐到 64 字节的缓存行,保证 CPU 能在一次操作里读取并缓存对象。
- **固定容量哈希表**:针对已知的工作负载做超额预分配,从而消除扩容开销和分支预测失败。
- **无分支编程**:用位技巧和布尔运算来避开热路径上代价高昂的 CPU 分支预测失败。
- **任务并行**:利用 `TaskCreationOptions.LongRunning` 来确保每个数据块都由一个专用线程处理。

### 课程笔记

Level 6 是专家级的优化,重点在 AVX-512 指令和高效率的内存结构。
这个实现先检查硬件是否支持 AVX-512,然后建立 memory-mapped file 的访问。

```csharp
using Shared;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using System.Text;

// =============================================================================
// Level 6: AVX-512 Implementation (Maximum Bandwidth)
// =============================================================================

Console.WriteLine("=== Level 6: Expert (Optimized) ===");
Console.WriteLine($"File: {GlobalConstants.FilePath}");
Console.WriteLine($"Processor Count: {Environment.ProcessorCount}");
Console.WriteLine($"AVX2 Supported: {Avx2.IsSupported}");
Console.WriteLine($"AVX-512 Supported: {Avx512F.IsSupported}");

Console.WriteLine();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/final-project-with-conclusion-69958260/?t=10)

为了把性能榨到最大,应用使用 `MemoryMappedFile` 并取得一个直接指向数据的指针。
这样就避免了在内核空间和用户空间之间复制数据的开销。

```csharp
var fileInfo = new FileInfo(GlobalConstants.FilePath);
var fileSize = fileInfo.Length;

if (fileSize == 0)
{
    Console.WriteLine("File is empty.");
    return;
}

var threadCount = Environment.ProcessorCount;
var tasks = new Task<FixedDictionary>[threadCount];

using var mmf = MemoryMappedFile.CreateFromFile(GlobalConstants.FilePath, FileMode.Open, null, 0, MemoryMappedFileAccess.Read);
using var accessor = mmf.CreateViewAccessor(0, fileSize, MemoryMappedFileAccess.Read);

unsafe
{
    byte* basePtr = null;
    accessor.SafeMemoryMappedViewHandle.AcquirePointer(ref basePtr);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/final-project-with-conclusion-69958260/?t=25)

#### FixedDictionary 与内存对齐

Level 6 的一个关键组件是 `FixedDictionary`。
与标准的 C# 字典不同,它是一个管理原生内存的 struct。
它借助内存对齐来保证 CPU 能高效地读取数据。
如果一个对象没有按 2 的幂次对齐(比如是 9 字节而不是 8 字节),CPU 可能需要多次读取和多次缓存操作才能处理它。
通过使用 64 字节对齐的 `NativeMemory.AlignedAlloc`,字典的条目就针对 CPU 的缓存行做了优化。

```csharp
internal unsafe struct FixedDictionary : IDisposable
{
    private const int Capacity = 16384;
    private const int CapacityMask = Capacity - 1;
    private Entry* _entries;

    public FixedDictionary()
    {
        // Cache-line aligned allocation (64 bytes)
        nuint alignment = 64;
        var size = (nuint)(Capacity * sizeof(Entry));
        _entries = (Entry*)NativeMemory.AlignedAlloc(size, alignment);

        // Zero initialize
        NativeMemory.Clear(_entries, size);
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/final-project-with-conclusion-69958260/?t=60)

`FixedDictionary` 的 `Update` 方法用线性探测来解决冲突。
它用按位掩码(`hash & CapacityMask`)而不是取模运算来计算索引,这要快得多。
添加新条目时,它对气象站名字使用分块内存复制(一次 8 字节),利用了现代 CPU 的 64 位数据通路。

```csharp
public void Update(byte* namePtr, int nameLen, ulong hash, int temp)
{
    var idx = (uint)hash & CapacityMask;

    while (true)
    {
        var entry = &_entries[idx];

        if (entry->Count == 0)
        {
            entry->Hash = hash;
            entry->Min = temp;
            entry->Max = temp;
            entry->Sum = temp;
            entry->Count = 1;
            entry->NameLen = nameLen;

            if (nameLen >= 8)
            {
                var i = 0;
                for (; i + 8 <= nameLen; i += 8)
                {
                    *(ulong*)(entry->NameBuffer + i) = *(ulong*)(namePtr + i);
                }
                for (; i < nameLen; i++)
                {
                    entry->NameBuffer[i] = namePtr[i];
                }
            }
            else
            {
                for (var k = 0; k < nameLen; k++)
                {
                    entry->NameBuffer[k] = namePtr[k];
                }
            }
            return;
        }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/final-project-with-conclusion-69958260/?t=220)

对已经存在的条目,字典在比较名字时采用一种自适应策略。
如果名字长度大于等于 12 字节,它就做 8 字节的并行比较。
这种 SIMD 风格的做法让 CPU 能同时比较多个字符。

```csharp
if (entry->Hash == hash && entry->NameLen == nameLen)
{
    bool match;
    if (nameLen >= 12)
    {
        match = true;
        var i = 0;
        for (; i + 8 <= nameLen; i += 8)
        {
            if (*(ulong*)(entry->NameBuffer + i) != *(ulong*)(namePtr + i))
            {
                match = false;
                break;
            }
        }
        if (match)
        {
            for (; i < nameLen; i++)
            {
                if (entry->NameBuffer[i] != namePtr[i])
                { match = false; break; }
            }
        }
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/final-project-with-conclusion-69958260/?t=310)

统计值的更新用的是普通分支,在这个场景下由于时间局部性(同一个气象站的温度彼此相近),这些分支是高度可预测的。

```csharp
if (match)
{
    if (temp < entry->Min) entry->Min = temp;
    if (temp > entry->Max) entry->Max = temp;

    entry->Sum += temp;
    entry->Count++;
    return;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/final-project-with-conclusion-69958260/?t=370)

`Entry` 结构体本身被仔细填充到 192 字节,正好对应三条 64 字节的缓存行。
这保证了最佳的对齐,也避免条目跨越缓存行的边界。

```csharp
[StructLayout(LayoutKind.Sequential, Pack = 8, Size = 192)]
struct Entry
{
    public ulong Hash;          // 8 bytes
    public int Min;             // 4 bytes
    public int Max;             // 4 bytes
    public long Sum;            // 8 bytes
    public long Count;          // 8 bytes
    public int NameLen;         // 4 bytes
    public fixed byte NameBuffer[100];  // 100 bytes
    // Implicit padding to 192 bytes (3 cache lines) for optimal alignment
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/final-project-with-conclusion-69958260/?t=400)

#### 用 Long-Running Tasks 做并行处理

这个实现没有用 `Parallel.For`,而是用 `TaskCreationOptions.LongRunning` 手动管理任务。
这是在向 .NET 调度器发出信号:该任务会长时间占用一个线程,促使它创建一个专用线程,而不是在线程池上使用一个虚拟任务。
这带来了与原生线程相近的性能特征。

```csharp
tasks[i] = Task.Factory.StartNew(
    () => ProcessChunkAvx512(ptrCopy, startPos, endPos),
    TaskCreationOptions.LongRunning
);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/final-project-with-conclusion-69958260/?t=535)

#### AVX-512 向量化引擎

`ProcessChunkAvx512` 函数以 64 字节为一块进行处理。
它把数据加载进一个 `Vector512<byte>`,并与广播开来的分号分隔符做比较。
`ExtractMostSignificantBits` 方法生成一个 64 位掩码,其中每一个置位的比特代表一个分号的位置。

```csharp
while (ptr < safeEndPtr)
{
    var blockStart = ptr;
    Vector512<byte> vData = Vector512.Load(ptr);
    var maskSemi = Vector512.Equals(vData, vSemi).ExtractMostSignificantBits();

    if (maskSemi == 0)
    {
        ptr += 64;
        continue;
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/final-project-with-conclusion-69958260/?t=610)

为了处理单个 64 字节块里的多条记录,引擎用一个 `do-while` 循环配合 `BitOperations.TrailingZeroCount`(TZCNT)来找到下一个分号。
一个无分支的位技巧(`maskSemi &= maskSemi - 1`)清掉最低的那个置位比特,从而不用条件跳转就能移动到下一条记录。

```csharp
do
{
    int semiAbs = BitOperations.TrailingZeroCount(maskSemi);
    maskSemi &= maskSemi - 1;

    var nameStart = ptr;
    var nameLen = semiAbs - (int)(ptr - blockStart);

    ulong hash = 0;
    var hi = 0;
    for (; hi + 1 < nameLen; hi += 2)
    {
        hash ^= nameStart[hi];
        hash *= 1099511628211ul;
        hash ^= nameStart[hi + 1];
        hash *= 1099511628211ul;
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/final-project-with-conclusion-69958260/?t=640)

温度解析同样用无分支逻辑做了优化。
通过一次加载 8 个字节并用布尔运算来检测小数点,这段代码避免了分支预测失败。
编译器通常会把这段逻辑翻译成一条 `CMOV`(conditional move)指令。

```csharp
var isDot = (secondByte == '.');
var notDot = !isDot;

var temp1Digit = ((byte)valueWrapper - '0') * 10 + ((byte)(valueWrapper >> 16) - '0');
var temp2Digits = ((byte)valueWrapper - '0') * 100 + (secondByte - '0') * 10 + ((byte)(valueWrapper >> 24) - '0');

temperature = (isDot ? temp1Digit : temp2Digits);
ptr += isDot ? 4 : 5;

dict.Update(nameStart, nameLen, hash, temperature * sign);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/final-project-with-conclusion-69958260/?t=685)

---

## 运行 Demo

Demo 代码按课程官方仓库 [Dometrain/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet](https://github.com/Dometrain/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet) 的方式组织,本章对应上游的 `Level6_Expert/`:

```
src/1brc/
├── notes/                  各章笔记
├── Shared/                 SharedTypes.cs:GlobalConstants、ResultLogger、StationStats、StationStatsStruct
├── DataGenerator/          Program.cs:413 个气象站 + Box-Muller 生成器
├── Level1_Naive/           Program.cs:第 3 章的 LINQ 朴素实现
├── Level2_Stream/          Program.cs:第 4 章的 StreamReader 流式实现
├── Level3_Parallel/        Program.cs:第 5 章的 Parallel.For 实现;RaceCondition.cs
├── Level4_SharedMemory/    Program.cs:第 6 章的 MMF + 指针实现;Pointers.cs:指针讲解
├── Level5_SIMD/            Program.cs:第 7 章的 AVX2 实现;FastHashTable.cs:自定义哈希表
├── Level6_Expert/          Program.cs:本课的 Level 6 实现,含 FixedDictionary
└── Benchmarks/             第 6、7 章的基准测试
```

上游把整个 Level 6 放在一个 `Program.cs` 里:顶层语句负责分块和合并,`ProcessChunkAvx512` 是向量化引擎,`FixedDictionary` 和它的 `Entry` 跟在后面。
本仓库保持这个结构。

### 与课程代码的三处偏差

**一、向量宽度在运行时选择。**
课上只有一条 AVX-512 的循环。
本仓库跑在 Snapdragon X 上,`win-arm64`,`Avx512F.IsSupported` 和 `Avx2.IsSupported` 都是 `false`,`Vector512.IsHardwareAccelerated` 也是 `false` - 课上那条循环在这里会走 .NET 的软件模拟路径,一章讲 AVX-512 的 demo 反而比第 7 章还慢。
所以 `ProcessChunkAvx512` 改名成 `ProcessChunk`,同一条循环按 64 / 32 / 16 字节写了三遍,运行时挑最宽的那个硬件真正支持的宽度:

```csharp
if (Vector512.IsHardwareAccelerated) { /* 64 bytes */ }
else if (Vector256.IsHardwareAccelerated) { /* 32 bytes */ }
else { /* Vector128, 16 bytes - ARM64 走这条 */ }
```

循环体三份是一样的:把 `;` 广播成整个向量,一次比较一整块,把比较结果变成位掩码,再把掩码里的每条记录都排空。
排空那段(课上的 `do-while`)抽成了一个 `DrainBlock` 方法,三条循环共用。

**二、记录起点单独用一个变量保存。**
这是上一条带出来的必要修改,也是本章唯一一处实质性的逻辑改动。

课上的 `nameStart = ptr`,即用 `ptr` 隐式地表示"当前记录从哪里开始";掩码为 0 时直接 `ptr += 64` 跳过整块。
这在 64 字节窗口上成立,因为 1BRC 的一行大约十几到二十几个字节,从记录起点开的 64 字节窗口里必然有一个 `;`。
16 字节窗口不成立:`Ho Chi Minh City` 正好 16 个字节,分号落在第 17 个字节上,窗口里没有分号,`ptr += 16` 就把记录起点丢了;下一个窗口从 `;23.4\n` 开始,`nameLen` 算出来是 0,于是输出里多出一个名字为空的气象站,而 `Ho Chi Minh City` 和 `City of San Marino` 这些超过 15 字节的名字整个消失。
第一次跑出来是 408 个气象站而不是 413 个,就是这个原因。

修法是把记录起点提成一个跟着走的变量,只在一条记录处理完时前进:

```csharp
var nameStart = ptr;
...
var nameLen = (int)(blockStart + semiAbs - nameStart);
...
dict.Update(nameStart, nameLen, hash, temperature * sign);
nameStart = ptr;
```

末尾的标量循环同样改成用这个变量,否则向量循环在名字中间退出时,标量部分会从半截开始算名字。

**三、输出走 `ResultLogger.FormatOutput`。**
课上在 Level 6 里手写了一遍 `"{" + string.Join(", ", ...) + "}"`,而 `Shared` 里本来就有 `ResultLogger.FormatOutput` 在做同一件事,前面五个 Level 用的也都是它。
排序保留课上的 `StringComparer.Ordinal`,所以本章的输出顺序和第 3 到 7 章不同(`Abéché` 排在 `Abidjan` 之后而不是最前面),内容一致。

另外 `Program.cs` 顶部多打印了两行 `ARM AdvSimd Supported` 和 `Widest accelerated vector`,免得横幅在一台没有 AVX-512 的机器上声称自己在跑 AVX-512。

### 跑起来

先用 DataGenerator 生成测量文件,再跑 Level6_Expert:

```bash
cd src/1brc
dotnet run --project DataGenerator -c Release -- 100_000_000
dotnet run --project Level6_Expert -c Release
```

下面是 12 核机器上一亿行(1.29 GB)的真实输出。
`{...}` 那一行有一万多个字符(413 个气象站),这里只保留开头,其余用 `…` 省略。

```text
=== Level 6: Expert (Optimized) ===
File: C:\Users\iantu\AppData\Local\Temp\1brc\Files\measurements.txt
Processor Count: 12
AVX2 Supported: False
AVX-512 Supported: False
ARM AdvSimd Supported: True
Widest accelerated vector: Vector128 (16 bytes)

WARNING: AVX-512 not supported. Falling back to Vector128 (16 bytes).
{Abha=-32.0/18.0/65.1, Abidjan=-21.3/26.0/72.4, Abéché=-17.7/29.4/74.1, Accra=-21.6/26.4/73.5, Addis Ababa=-34.0/16.0/60.9, Adelaide=-25.9/17.3/63.3, Aden=-15.3/29.1/77.4, Ahvaz=-21.3/25.4/72.4, Albuquerque=-30.4/14.0/59.5, Alexandra=-35.6/11.0/57.6, Alexandria=-24.6/20.0/71.2, Algiers=-26.1/18.2/66.…}

Processed 100,000,000 rows using 12 threads
Found 413 unique stations
Elapsed: 00:00:00.2999315

📁 Results saved: C:\Users\iantu\AppData\Local\Temp\1brc\Files\results.log

Memory Statistics:
  Working Set: 1,346 MB
  GC Total Memory: 0 MB
  Gen0 Collections: 2
  Gen1 Collections: 2
  Gen2 Collections: 2
```

413 个气象站的三元组与同一个文件上 `Level5_SIMD` 的输出逐项相同(排序规则不同,按名字对齐后 413 项全部一致)。

同一台机器、同一个一亿行文件,各跑三次:

| 实现 | 耗时(三次) | Working Set | Gen0/1/2 |
| --- | --- | --- | --- |
| `Level4_SharedMemory`(第 6 章) | 0.397 / 0.381 / 0.402 s | 1,348-1,349 MB | 2/2/2 |
| `Level5_SIMD`(第 7 章) | 0.356 / 0.383 / 0.397 s | 1,348 MB | 2/2/2 |
| `Level6_Expert`(本课) | 0.366 / 0.308 / 0.303 s | 1,345-1,348 MB | 2/2/2 |

Level 6 在这台机器上确实是三者里最快的,比 Level 5 快大约 15% 到 20%,和课上给 AVX-512 版本的提升幅度接近。

有意思的是,这个提升并不来自向量宽度 - 这里只有 16 字节,和第 7 章在这台机器上实际走到的 `Vector128` 分支一样宽。
第 7 章的基准测试还说明,在 ARM64 上分隔符搜索的向量化本身是倒贴的。
剩下的解释只能是掩码之外的东西:`FixedDictionary` 的固定容量(不再有扩容检查)、64 字节对齐的原生内存(`GC Total Memory` 直接是 0 MB)、192 字节正好三条缓存行的 `Entry`,以及 `LongRunning` 换来的专用线程。
换句话说,本课标题里的 AVX-512 在这台机器上一点没用上,而课程真正教会的内存布局那部分,在没有 AVX-512 的硬件上照样兑现。

第一次跑(刚生成完文件、页缓存还没热)是 0.86 s,上面三次都是热缓存的结果。

十亿行没有跑:`GlobalConstants.FilePath` 指向临时目录,13 GB 的文件放在那里不合适,而一亿行已经足以把三代实现的差距量出来。
