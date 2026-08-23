# Mastering LINQ Performance

> 课程:[Mastering: C#](https://dometrain.com/course/mastering-csharp/) · 第 15 章
> 共 15 课 · 约 23:27
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Overview](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958929/) | 1:01 | [↓](#1-overview) |
| 2 | [LINQ Performance Reputation](https://dometrain.com/take/course/mastering-csharp-3256129/linq-performance-reputation-69958932/) | 0:44 | [↓](#2-linq-performance-reputation) |
| 3 | [Performance Optimization Loop](https://dometrain.com/take/course/mastering-csharp-3256129/performance-optimization-loop-69958935/) | 1:47 | [↓](#3-performance-optimization-loop) |
| 4 | [Mastering LINQ Performance](https://dometrain.com/take/course/mastering-csharp-3256129/mastering-linq-performance-69958938/) | 1:02 | [↓](#4-mastering-linq-performance) |
| 5 | [LINQ vs. Manual Loop Performance Analysis](https://dometrain.com/take/course/mastering-csharp-3256129/linq-vs-manual-loop-performance-analysis-69958941/) | 1:52 | [↓](#5-linq-vs-manual-loop-performance-analysis) |
| 6 | [Analyzing Enumerable.Any Performance](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-enumerable-any-performance-69958944/) | 1:05 | [↓](#6-analyzing-enumerableany-performance) |
| 7 | [Spanification in LINQ](https://dometrain.com/take/course/mastering-csharp-3256129/spanification-in-linq-69958946/) | 0:57 | [↓](#7-spanification-in-linq) |
| 8 | [LINQ Performance Improvements in .NET 10](https://dometrain.com/take/course/mastering-csharp-3256129/linq-performance-improvements-in-dotnet-10-69958953/) | 0:43 | [↓](#8-linq-performance-improvements-in-net-10) |
| 9 | [LINQ Performance Optimization Patterns](https://dometrain.com/take/course/mastering-csharp-3256129/linq-performance-optimization-patterns-69958954/) | 1:06 | [↓](#9-linq-performance-optimization-patterns) |
| 10 | [Type-based Specialization](https://dometrain.com/take/course/mastering-csharp-3256129/type-based-specialization-69958955/) | 2:44 | [↓](#10-type-based-specialization) |
| 11 | [Iterator Fusion](https://dometrain.com/take/course/mastering-csharp-3256129/iterator-fusion-69958956/) | 2:04 | [↓](#11-iterator-fusion) |
| 12 | [Iterator Fast Path Overrides](https://dometrain.com/take/course/mastering-csharp-3256129/iterator-fast-path-overrides-69958958/) | 1:24 | [↓](#12-iterator-fast-path-overrides) |
| 13 | [For loop vs. Enumerable.SequenceEquals](https://dometrain.com/take/course/mastering-csharp-3256129/for-loop-vs-enumerable-sequenceequals-69958960/) | 1:51 | [↓](#13-for-loop-vs-enumerablesequenceequals) |
| 14 | [Vectorization in .NET Framework](https://dometrain.com/take/course/mastering-csharp-3256129/vectorization-in-dotnet-framework-69958961/) | 2:53 | [↓](#14-vectorization-in-net-framework) |
| 15 | [Summary](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958962/) | 2:14 | [↓](#15-summary) |

## 1. Overview

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958929/) · 1:01

### 总结

本课在 LINQ 的语境下介绍性能优化,强调性能是一种运行时特性,而不是语言的静态属性。
它勾勒出一套系统化的优化流程 - 定义目标、度量影响、反复迭代 - 同时预览了 LINQ 内部的机制,例如集合特化、迭代器融合、快速路径重写和向量化。
通过把 .NET Framework 与 .NET 10 这样的现代运行时做对比,本课演示了同样的 LINQ 代码如何因为 JIT 的改进和内部实现细节而呈现出截然不同的性能表现。

### 核心概念

- 性能是一种运行时属性(取决于上下文)。
- 优化流程:设定目标、度量、迭代。
- 框架对比(.NET Framework 对比 .NET 10)。
- 集合特化(Collection Specialization)。
- 迭代器融合(Iterator Fusion)。
- 快速路径优化。
- 向量化。

### 课程笔记

性能是系统的运行时特性,仅凭查看源代码或通用基准测试无法准确判断它的影响。
有效的优化需要一套结构化的流程:确立性能目标,度量代码当前的影响,并对实现反复迭代以达成期望的结果。

LINQ 性能的一个核心关注点是它在不同 .NET 运行时之间的差异。
例如,在 .NET Framework 4.8 上因委托分配和装箱枚举器而缓慢的代码,在 .NET 10 上可能表现得好得多。
在现代运行时中,JIT 编译器可以对 lambda 去抽象化,使 LINQ 操作能够达到手写 `foreach` 循环的效率并且零分配。

LINQ 的内部实现使用了若干优化模式,本节将逐一探讨:

#### 集合特化(Collection Specialization)

像 `Count()` 和 `ElementAt()` 这样的运算符会根据数据源类型进行优化。
当数据源实现了 `ICollection<T>` 或 `IList<T>` 时,LINQ 会使用特化的路径(例如 `TryGetNonEnumeratedCount` 或直接的索引器访问)来提供 O(1) 复杂度。
如果数据源只实现了 `IEnumerable<T>`,这些操作会退回到 O(N) 的枚举,在循环中使用时可能导致 O(N²) 的复杂度。

#### 迭代器融合(Iterator Fusion)

LINQ 会把多个查询阶段融合成单个迭代器以减少开销。
例如,在数组上链式调用 `.Where().Select()` 会产生一个像 `WhereSelectArrayIterator` 这样的特化迭代器。
这个优化确保只分配一个迭代器对象,并且每个元素只发生一次 `MoveNext` 调用,而不是查询的每个阶段各一次。

#### 快速路径重写(Fast-path Overrides)

某些运算符利用虚方法钩子来短路掉工作。
`Reverse().Last()` 可以直接返回数据源的第一个元素,而不必反转集合或构建缓冲区。
同样,`OrderBy(...).First()` 可以被优化成一次部分排序,在 O(N) 时间内找到最小元素,从而避免完整排序 O(N log N) 的代价。

#### 向量化(Vectorization)

现代 .NET 运行时通过 `Span<T>` 和 `Vector<T>` 使用 SIMD(Single Instruction, Multiple Data)来加速数据密集型操作。
例如,对字节数组执行 `SequenceEqual` 时,向量化版本相比标准的标量循环或 `IEnumerable` 枚举可以快 10-20 倍,因为它在单条 CPU 指令中处理多个字节。

## 2. LINQ Performance Reputation

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/linq-performance-reputation-69958932/) · 0:44

### 总结

LINQ 性能不佳的名声源自它在 .NET Framework 中的起源,当时开发效率的优先级高于执行速度。
尽管历史上的一些指南 - 比如 C# 编译器(Roslyn)的指南 - 因为委托分配和装箱而建议不要在性能关键的"热路径"中使用 LINQ,现代 .NET 运行时已经显著缩小了这个差距。
本课通过考察不同运行时如何处理同样的 LINQ 代码,来探讨"LINQ 很慢"这句老话是否依然成立,并指出性能往往是运行时的属性,而不是 LINQ 语法本身的属性。

### 核心概念

- **历史背景**:LINQ 是为 .NET Framework 时代的开发者生产力而设计的,常常以性能为代价。
- **分配开销**:传统的 LINQ 用法常常涉及委托分配和装箱枚举器,这会增加垃圾回收(GC)压力。
- **热路径规范**:像 Roslyn 编译器(OZ)这样的高性能项目常常禁用 LINQ,以在关键代码路径上保持效率。
- **运行时演进**:LINQ 的性能是特定 .NET 运行时(例如 .NET 4.8 与 .NET 10)的属性,而不是 C# 语言的局限。

### 课程笔记

LINQ 作为一项缓慢技术的名声植根于它的历史。
20 多年前 LINQ 刚推出时,.NET 团队的首要关注点是提升开发者的生产力。
因此,许多在 .NET Full Framework 中使用 LINQ 的开发者观察到,相比手写循环它有显著的性能开销。

这些观察促成了针对性能敏感项目制定严格的开发规范。
例如,C# 编译器(Roslyn,也称为 OZ)的贡献指南明确建议在"热路径"中避免使用 LINQ。
这些规范的目的是通过避免 LINQ、并且避开对不使用结构体迭代器的集合执行 `foreach` 循环,来把分配降到最少。

要判断这些顾虑是否仍然成立,就需要在不同版本的 .NET 运行时上,把 LINQ 与手写实现做基准测试对比。
一个常见的对比是:用手写 `foreach` 循环与 `Enumerable.Any` 扩展方法分别检查集合中是否包含某个特定值。
这个测试揭示出 LINQ 的开销是运行时属性,而不是语言属性;同样的源代码会因框架版本不同而产生不同的结果。

```csharp
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;

[HideColumns(Column.Error, Column.StdDev, Column.RatioSD, Column.Job, Column.AllocRatio, Column.Gen0)]
[Orderer(SummaryOrderPolicy.Method)]
[MemoryDiagnoser]
[ShortRunJob(RuntimeMoniker.Net48)]
[ShortRunJob(RuntimeMoniker.Net80)]
[ShortRunJob(RuntimeMoniker.Net10_0)]
public class LinqVsLoopBenchmarks
{
    private const int Count = 42;
    private readonly List<int> _list = Enumerable.Range(1, Count).ToList();
    private readonly int InstanceValue = Count + 1;

    [Benchmark(Baseline = true)]
    public bool ManualLoop()
    {
        foreach (var item in _list)
        {
            if (item == InstanceValue)
                return true;
        }
        return false;
    }

    [Benchmark]
    public bool LinqAny()
        => _list.Any(x => x == InstanceValue);
}

class Program
{
    static void Main(string[] args) => BenchmarkRunner.Run<LinqVsLoopBenchmarks>(args: args);
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/linq-performance-reputation-69958932/?t=38)

在 .NET 4.8 这样较老的运行时上,LINQ 版本通常要付出委托分配和装箱枚举器的代价,往往比手写循环慢三倍。
然而在 .NET 10 这样的现代运行时上,JIT 编译器往往可以对 lambda 去抽象化,使 LINQ 版本能够以零分配达到甚至超过手写循环的性能。
归根结底,仅凭源代码很难推断性能;瓶颈很少出现在预期的地方,而且行为会随 .NET 版本发生显著变化。

## 3. Performance Optimization Loop

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/performance-optimization-loop-69958935/) · 1:47

### 总结

性能优化是一个数据驱动的迭代过程,聚焦于达成具体的运行时目标,例如降低延迟或提升吞吐量。
这个生命周期包括确立性能目标、收集生产环境指标,以及利用性能分析工具定位瓶颈。
一旦定位到瓶颈,就通过假设驱动的方式实施优化,并使用微基准测试或压力测试来验证,以确保在不过度设计的前提下满足需求。

### 核心概念

- **运行时数据**:性能必须通过数据和指标来评估,而不是通过检视代码。
- **目标设定**:定义具体的目标,例如延迟、吞吐量或每秒请求数(RPS)。
- **性能分析**:使用 PerfView 或 Visual Studio Profiler 这样的工具来定位热路径和瓶颈。
- **优化生命周期**:形成假设、修改代码、验证结果的迭代循环。
- **验证方法**:对孤立的改动使用微基准测试,对系统性影响使用压力测试。
- **终止标准**:目标达成后知道何时停止优化,以避免不必要的复杂度。

### 课程笔记

性能是系统的运行时特性,这意味着它无法仅凭查看源代码来评估。
相反,优化必须建立在数据的基础上。
性能优化的生命周期是一个结构化的过程,始于定义清晰的目标并了解应用程序的当前状态。

#### 定义目标与指标

这个过程从设定一个具体目标开始,例如降低延迟、提升吞吐量,或者提高某个 API 的每秒请求数(RPS)。
为了了解当前的性能,应该收集指标,最好来自生产系统。
如果当前指标已经满足所定义的目标,优化过程就结束了。
如果没有,下一步就是找出系统在哪里未能达到这些目标。

#### 定位瓶颈

瓶颈是通过对系统做性能分析、找出"热路径"来定位的 - 也就是应用程序中消耗最多时间或资源的那些部分。
PerfView、Visual Studio Profiler 或其他追踪工具对于精确定位这些区域是必不可少的。
一旦定位到瓶颈,重心就转向优化。

#### 优化循环

优化应当遵循一个多阶段、假设驱动的过程:

1. **形成假设**:确定性能问题的可能原因。
2. **修改代码**:基于假设实施修复。
3. **比较结果**:如果代码是孤立的,就使用微基准测试来比较改动前后的性能。
   如果改动范围较广或影响多个区域,就搭建一个自定义的压力测试环境,或者部署到开发环境来评估影响。

如果改动成功满足了性能要求,代码就可以签入。
如果没有,循环就带着新的假设或不同的优化思路继续下去。

#### 迭代与收尾

优化往往是一个迭代过程,需要多轮循环才能满足复杂的需求。
知道何时停止至关重要;一旦达到预先定义的性能目标,进一步的优化可能引入不必要的复杂度却带不来显著的价值。

## 4. Mastering LINQ Performance

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/mastering-linq-performance-69958938/) · 1:02

### 总结

掌握 LINQ 性能需要深入理解底层的运行时和语言抽象。
当 LINQ 被判定处在应用程序的热路径上时,开发者应当遵循严谨的基准测试优化循环 - 形成假设并验证改动 - 同时确保测试环境(类型和运行时版本)准确反映生产环境,以避免得出误导性的结果。

### 核心概念

- 基准测试优化循环:形成假设、做出改动、分析结果。
- 理解底层运行时和语言机制的必要性。
- 基准测试中潜在的陷阱,例如运行时版本或数据类型不匹配。
- LINQ 抽象与手写迭代之间的性能权衡。

### 课程笔记

当性能分析发现 LINQ 操作位于应用程序的"热路径"上时,优化应当遵循一个结构化的基准测试循环。
这个过程包括形成技术假设、实施改动,并通过基准测试验证影响。
理解某项改动之所以提升效率的机制原因至关重要;缺少这种理解,基准测试的数字可能具有误导性。

基准测试的准确性对环境高度敏感。
基准测试与生产环境之间的差异 - 例如使用了不同的数据类型或不同版本的 .NET 运行时 - 会导致错误的结论。
由于 LINQ 的性能特征在不同运行时版本之间差异显著,针对生产环境所使用的那个特定版本进行测试是必须的。
这通常通过为多个运行时配置基准测试作业来实现,例如 .NET Framework 4.8 和现代 .NET 版本。

要掌握性能,就必须理解自己所用抽象层次紧邻的下一层。
在 LINQ 的语境下,这意味着把高层的声明式语法与手写的命令式实现做对比,看运行时分别如何处理它们。

```csharp
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;

[HideColumns(Column.Error, Column.StdDev, Column.RatioSD, Column.Job, Column.AllocRatio, Column.Gen0)]
[Orderer(SummaryOrderPolicy.Method)]
[MemoryDiagnoser]
[ShortRunJob(RuntimeMoniker.Net48)]
[ShortRunJob(RuntimeMoniker.Net80)]
[ShortRunJob(RuntimeMoniker.Net10_0)]
public class LinqVsLoopBenchmarks
{
    private const int Count = 42;
    private readonly List<int> _list = Enumerable.Range(1, Count).ToList();
    private readonly int InstanceValue = Count + 1;

    [Benchmark(Baseline = true)]
    public bool ManualLoop()
    {
        foreach (var item in _list)
        {
            if (item == InstanceValue)
                return true;
        }
        return false;
    }

    [Benchmark]
    public bool LinqAny()
        => _list.Any(x => x == InstanceValue);
}

class Program
{
    static void Main(string[] args) => BenchmarkRunner.Run<LinqVsLoopBenchmarks>(args: args);
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/mastering-linq-performance-69958938/?t=57)

## 5. LINQ vs. Manual Loop Performance Analysis

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/linq-vs-manual-loop-performance-analysis-69958941/) · 1:52

### 总结

本课在不同的 .NET 运行时上比较 LINQ.Any 与手写 foreach 循环的性能,具体是 .NET Framework 4.8、.NET 8 和 .NET 10。
它表明,虽然在较老的运行时上手写循环传统上更高效且无分配,但 .NET 10 中的现代优化已经在很大程度上消除了委托开销,使 LINQ 能够与手写迭代持平,甚至略占优势。

### 核心概念

- **运行时对比**:.NET Framework 4.8、.NET 8 和 .NET 10 之间的性能特征差异显著。
- **分配开销**:在较老的运行时上,LINQ 方法相比手写循环常常带来额外的内存分配。
- **委托优化**:.NET 10 引入了可以在运行时完全消除委托开销的抽象。
- **CPU 密集与 I/O 密集**:相比 I/O 密集型系统,运行时升级带来的性能收益在 CPU 密集型应用中最为明显。

### 课程笔记

为了分析 LINQ 与手写迭代之间的性能差异,构建了一个在 `List<int>` 中搜索某个值的基准测试。
这个基准测试针对三个特定的运行时:.NET Framework 4.8、.NET 8 和 .NET 10。
搜索被配置为查找一个列表中并不存在的 `InstanceValue`,迫使迭代遍历整个集合。

```csharp
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;

[HideColumns(Column.Error, Column.StdDev, Column.RatioSD, Column.Job, Column.AllocRatio, Column.Gen0)]
[Orderer(SummaryOrderPolicy.Method)]
[MemoryDiagnoser]
[ShortRunJob(RuntimeMoniker.Net48)]
[ShortRunJob(RuntimeMoniker.Net80)]
[ShortRunJob(RuntimeMoniker.Net10_0)]
public class LinqVsLoopBenchmarks
{
    private const int Count = 42;
    private readonly List<int> _list = Enumerable.Range(1, Count).ToList();
    private readonly int InstanceValue = Count + 1;

    [Benchmark(Baseline = true)]
    public bool ManualLoop()
    {
        foreach (var item in _list)
        {
            if (item == InstanceValue)
                return true;
        }
        return false;
    }

    [Benchmark]
    public bool LinqAny()
        => _list.Any(x => x == InstanceValue);
}

class Program
{
    static void Main(string[] args) => BenchmarkRunner.Run<LinqVsLoopBenchmarks>(args: args);
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/linq-vs-manual-loop-performance-analysis-69958941/?t=10)

#### 运行时性能观察

**旧版与现代运行时(.NET Framework 4.8 与 .NET 8)**

在 .NET Framework 4.8 和 .NET 8 上,手写 `foreach` 循环都比 LINQ 版本更高效。
在这些运行时上,LINQ 的实现会带来额外的内存分配。
不过,这两个运行时之间的对比显示出显著的性能飞跃;把代码从 .NET Framework 4.8 迁移到 .NET 8 可以带来 50% 到高达 8 倍的性能提升,在 CPU 密集型应用中尤其明显。
I/O 密集型应用通常收益较小。

**.NET 10 的优化**

在 .NET 10 上,由于新的抽象在运行时消除了委托开销,性能格局发生了变化。
在这个环境下,`LINQ.Any` 版本比手写循环略微更高效。
虽然差异是以纳秒计的,不太可能对真实应用的性能产生显著影响,但它表明 LINQ 历史上的开销正在被运行时层面的优化所削减。

## 6. Analyzing Enumerable.Any Performance

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-enumerable-any-performance-69958944/) · 1:05

### 总结

本课通过比较 Enumerable.Any 在 .NET Framework 4.8、.NET 8 和 .NET 10 上的实现,分析它的性能特征。
通过在项目中对这些框架做多目标编译,开发者可以查看基础类库(BCL)的源代码,看到 .NET 10 如何引入使用 ReadOnlySpan<T> 和 TryGetSpan 的优化,为兼容的集合绕开 IEnumerable 枚举的开销。

### 核心概念

- 对多个框架做多目标编译(net48、net8.0、net10.0)以比较 BCL 的实现。
- 通过 IDE 导航分析反编译或已编译的源代码。
- Enumerable.Any 的标准实现:对 IEnumerable<T> 使用 foreach。
- .NET 10 中使用 TryGetSpan 更高效访问底层数据的优化。
- 相比 IEnumerator<T>,遍历 Span<T> 带来的性能好处。

### 课程笔记

为了比较不同运行时之间基础类库(BCL)代码的差异,可以把项目配置为面向多个框架。
这样就可以在 IDE 中导航到每个目标版本各自的源代码实现。

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFrameworks>net10.0;net8.0;net48</TargetFrameworks>
    <LangVersion>latest</LangVersion>
  </PropertyGroup>

</Project>
```

下面的例子演示了在数组上对 `Enumerable.Any` 的标准调用,它是比较不同运行时如何处理执行过程的基础:

```csharp
using System.Linq;

static class Program
{
    static void Main()
    {
        int[] data = [10, 20, 30, 40, 50, 60];
        bool result = data.Any(x => x == 42);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-enumerable-any-performance-69958944/?t=25)

#### .NET Framework 4.8 与 .NET 8 的实现

在 .NET Framework 4.8 和 .NET 8 上,`Enumerable.Any` 的实现相对直白。
在对 source 和 predicate 做完参数校验之后,它用一个简单的 `foreach` 循环遍历 `IEnumerable<T>`,检查是否有元素满足谓词。

```csharp
//
// Exceptions:
//   T:System.ArgumentNullException:
//     source or predicate is null.
[__DynamicallyInvokable]
public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
{
    if (source == null)
    {
        throw Error.ArgumentNull("source");
    }

    if (predicate == null)
    {
        throw Error.ArgumentNull("predicate");
    }

    foreach (TSource item in source)
    {
        if (predicate(item))
        {            return true;
        }
    }

    return false;
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-enumerable-any-performance-69958944/?t=40)

#### .NET 10 的实现

.NET 10 的实现引入了一项重要的性能优化。
它保持了同样的参数校验逻辑,但会尝试用 `TryGetSpan` 从源集合中取得一个 `ReadOnlySpan<T>`。
如果能够取得 span(对数组和列表来说这很常见),方法就遍历这个 span 而不是可枚举对象。
这就避免了分配枚举器的开销,以及与 `IEnumerable` 相关的虚方法调用。

```csharp
public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
{
    if (source is null)
    {
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
    }

    if (predicate is null)
    {
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.predicate);
    }

    if (source.TryGetSpan(out ReadOnlySpan<TSource> span))
    {
        foreach (TSource element in span)
        {            if (predicate(element))
            {
                return true;
            }
        }
    }
    else
    {
        foreach (TSource element in source)
        {
            if (predicate(element))
            {
                return true;
            }
        }
    }

    return false;
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-enumerable-any-performance-69958944/?t=55)

使用 span 之所以带来性能提升,是因为它让运行时可以直接遍历底层内存,在源集合支持的情况下完全绕开 `IEnumerator` 接口。

## 7. Spanification in LINQ

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/spanification-in-linq-69958946/) · 0:57

### 总结

LINQ 中的 Spanification 是指:对于涉及连续内存的操作,从通用的 IEnumerable<T> 抽象转向高性能的 Span<T> 类型。
IEnumerable<T> 可以表示多种多样的数据源,而 Span<T> 提供的是一个类型安全的内存视图,.NET Core 的 JIT 可以通过内建函数(intrinsics)和硬件加速的向量化来优化它。
作为 ref struct,Span<T> 被限制在栈上,这意味着它不能用于异步方法、迭代器块,也不能被 lambda 捕获,但它在切片和序列比较上提供了显著的效率。

### 核心概念

- **连续内存抽象**:与更宽泛的 IEnumerable<T> 不同,Span<T> 表示一块连续的内存。
- **平台优化**:.NET Core 的 JIT 专门认识 Span<T>,并提供了 .NET Framework 中没有的优化。
- **广泛的内存支持**:支持托管数组、ImmutableArray<T>、BitArray、StringBuilder 的分块、非托管内存以及栈上分配的内存。
- **Ref struct 的约束**:作为仅限栈的类型,Span<T> 不能用于 async 方法、yield return 块,也不能被 lambda 捕获。
- **通过向量化获得性能**:把 Span<T> 与 SequenceEqual 这类方法配合使用,可以让 JIT 采用 SIMD 指令,更快地处理数据。

### 课程笔记

IEnumerable<T> 是一个灵活的抽象,可以表示各种序列,例如内存中的集合、生成的序列或数据库查询。
而 Span<T> 是一个更简单也更高效的抽象,专门为连续的内存块而设计。
虽然 .NET Framework 可以通过 NuGet 包获得 Span<T>,但只有 .NET Core 的 JIT 编译器做了独特的优化,能够识别并加速 Span<T> 的操作。

Span<T> 非常通用,可以用来包装托管数组、ImmutableArray<T>、BitArray,甚至是 StringBuilder 的单个分块。
它还支持非托管内存、栈上分配以及单个结构体实例。

```csharp
int[] data = [10, 20, 30, 40, 50, 60];
Span<int> span = data.AsSpan();
span.Slice(2, 3);
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/spanification-in-linq-69958946/?t=10)

因为 Span<T> 是 ref struct,它被限制在栈上。
这带来了几项重要的限制:

- 它不能用在迭代器块中(使用 yield return 的方法)。
- 它不能用在 async 方法中。
- 它不能被 lambda 表达式捕获。

在高性能的 LINQ 场景中,"Spanification"往往意味着用 MemoryExtensions 的方法替换标准的 Enumerable 方法。
例如,用 SequenceEqual 比较两个序列时,在 span 上执行会明显更快,因为 JIT 可以把这个操作当作内建函数并应用向量化。

```csharp
public bool EqualsSpan(Hash other)
{
    // MemoryExtensions.SequenceEqual on ReadOnlySpan<byte> is a JIT intrinsic
    // on modern .NET — it vectorizes internally without a manual Vector<byte> loop.
    return Bytes.AsSpan().SequenceEqual(other.Bytes);
}
```

这种做法避免了 IEnumerator<T> 和虚调用的开销,让硬件可以同时处理多个字节。

## 8. LINQ Performance Improvements in .NET 10

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/linq-performance-improvements-in-dotnet-10-69958953/) · 0:43

.NET 10 为 Any 这类 LINQ 方法引入了重要的性能优化,弥合了高层表达性代码与底层手写循环之间的差距。
通过利用委托抽象去掉调用开销,并利用 TryGetSpan 为数组和列表这类常见集合消除迭代器分配,.NET 10 让开发者可以写出可读、地道的 C#,同时不牺牲执行速度和内存效率。

### 核心概念

- 用委托抽象去掉调用开销。
- 通过 `TryGetSpan` 为数组和列表消除迭代器分配。
- LINQ `Any` 与手写 `foreach` 循环之间的性能持平。
- 把高层抽象优化成高效的机器码。

### 课程笔记

在 .NET 10 上,LINQ 的性能已经优化到这样的程度:相比手写的 `for` 或 `foreach` 循环,高层抽象不再带来性能损失。
这是通过两个主要机制实现的:去掉委托调用开销,以及消除迭代器分配。

针对 `Any` 的优化展示了 .NET 10 如何处理集合。
通过尝试从源 `IEnumerable<T>` 取得一个 `ReadOnlySpan<T>`,运行时可以在不分配枚举器对象的情况下遍历底层数据。
这本质上剥掉了代码中所用高层抽象的开销。

```csharp
public static bool Any<TSource>(
    this IEnumerable<TSource> source, Func<TSource, bool> predicate)
{
    // Special case: no allocations for arrays and lists
    if (source.TryGetSpan(out ReadOnlySpan<TSource> span))
    {
        foreach (TSource element in span)
        {
            if (predicate(element))
            {
                return true;
            }
        }
    }
    return false;
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/linq-performance-improvements-in-dotnet-10-69958953/?t=10)

这种做法确保委托抽象去掉了调用成本,同时 `TryGetSpan` 负责数组和 `List<T>` 的内存效率。
这是一个绝佳的例子,说明高层代码未必意味着慢代码;开发者可以同时享有高可读性和高效率的实现。

为了验证这些改进,可以在不同 .NET 版本上做基准测试,比较手写循环与 LINQ `Any` 的实现。
在 .NET 10 上,LINQ 版本的性能与手写循环趋于一致。

```csharp
[HideColumns(Column.Error, Column.StdDev, Column.RatioSD, Column.Job, Column.AllocRatio, Column.Gen0)]
[Orderer(SummaryOrderPolicy.Method)]
[MemoryDiagnoser]
[ShortRunJob(RuntimeMoniker.Net48)]
[ShortRunJob(RuntimeMoniker.Net80)]
[ShortRunJob(RuntimeMoniker.Net10_0)]
public class LinqVsLoopBenchmarks
{
    private const int Count = 42;
    private readonly List<int> _list = Enumerable.Range(1, Count).ToList();
    private readonly int InstanceValue = Count + 1;

    [Benchmark(Baseline = true)]
    public bool ManualLoop()
    {
        foreach (var item in _list)
        {
            if (item == InstanceValue)
                return true;
        }
        return false;
    }

    [Benchmark]
    public bool LinqAny()
        => _list.Any(x => x == InstanceValue);
}
```

## 9. LINQ Performance Optimization Patterns

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/linq-performance-optimization-patterns-69958954/) · 1:06

### 总结

LINQ 的性能通过一些降低算法复杂度和迭代开销的内部优化模式而得到显著提升。
这些优化包括:基于类型的特化,即 Count() 这样的方法通过检查集合接口达到常数时间复杂度;迭代器融合,它把管道开销降到最低;快速路径重写,它短路掉 Reverse().Last() 这类冗余操作;以及使用 SIMD 的向量化,实现硬件加速的数据处理。

### 核心概念

- **基于类型的特化**:根据底层集合类型来优化操作(例如检查 ICollection)。
- **迭代器融合**:组合多个迭代器以减少迭代开销。
- **迭代器快速路径重写**:短路 LINQ 管道以避免冗余的工作。
- **向量化**:利用 SIMD(Single Instruction Multiple Data)实现高效的数据处理。

### 课程笔记

LINQ 采用了若干策略,让性能超越基础的迭代。
一个主要的例子是**基于类型的特化**。
许多 LINQ 操作针对特定的集合类型或接口包含了特殊分支。
例如,`Count()` 方法并不总是遍历整个数据源。
相反,它会检查数据源是否实现了 `ICollection`。
如果实现了,它就直接返回 `Count` 属性,把算法复杂度从线性($O(n)$)变成常数($O(1)$)。

```csharp
public static IEnumerable<TResult> Select<TSource, TResult>(
    this IEnumerable<TSource> source, Func<TSource, TResult> selector)
{
    foreach (TSource element in source)
    {
        yield return selector(element);
    }
}

public static int Count<TSource>(this IEnumerable<TSource> source)
{
    int count = 0;
    foreach (TSource element in source)
    {
        count++;
    }

    return count;
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/linq-performance-optimization-patterns-69958954/?t=10)

另一个重要的模式是**迭代器融合**。
这项技术把多个迭代器组合在一起,以减少多层迭代所带来的开销。
通过融合操作,管道可以更高效地处理元素,而不必创建不必要的中间状态。

**迭代器快速路径重写**提供了另一层优化。
这个模式依赖 `Iterator<T>` 抽象类以及它数量庞大的派生类型来短路管道、避免冗余的工作。
例如,如果一条管道里包含一次 `Reverse()` 调用后面跟着一次 `Last()` 调用,LINQ 可以在内部把它替换成对原始数据源的一次 `First()` 调用,从而不必反转整个集合。

最后,LINQ 还利用了**向量化**。
这意味着使用单指令多数据(SIMD)指令来更高效地处理数据。
这对 `SequenceEqual` 这类操作尤其有效:硬件加速通过在单个 CPU 周期内处理多个元素,相比标准循环可以大幅提升吞吐量。

## 10. Type-based Specialization

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/type-based-specialization-69958955/) · 2:44

即使高层代码完全相同,LINQ 的性能特征也可能天差地别。
这种差异由基于类型的特化驱动:LINQ 方法会检查 `IEnumerable<T>` 的底层实现,以便应用优化。
当这些优化不可用时 - 比如数据源只实现了基础的 `IEnumerable<T>` 接口 - 性能可能从线性复杂度变成平方复杂度。

### 核心概念

- **基于类型的特化**:LINQ 根据集合的具体类型来优化操作的能力。
- **性能悬崖**:随输入类型不同,复杂度从线性($O(N)$)转变为平方($O(N^2)$)。
- **Enumerable.TryGetNonEnumeratedCount**:一个公开方法,尝试在不枚举序列的情况下以 $O(1)$ 时间取得集合的元素个数。
- **IList 特化**:针对 `ElementAt`、`First`、`Last` 这类方法的优化,利用索引器实现 $O(1)$ 访问。
- **TryGetSpan**:LINQ 内部针对 `Array` 和 `List<T>` 的优化,为 `Min`、`Max`、`Average` 这类操作启用向量化。

### 课程笔记

为了理解类型特化的影响,来看一个比较三种不同数据源的基准测试:标准的 `List<T>`、只实现了 `IEnumerable<T>` 的自定义 `ReadOnlyList<T>`,以及一个惰性的 LINQ 查询。

```csharp
[HideColumns(Column.Error, Column.StdDev, Column.RatioSD, Column.Job, Column.AllocRatio, Column.Gen0)]
[MemoryDiagnoser]
[ShortRunJob(RuntimeMoniker.Net10_0)]
public class ForLoopBenchmarks
{
    private IEnumerable<string> _list = [];
    private IEnumerable<string> _readOnlyList = [];
    private IEnumerable<string> _query = [];

    [GlobalSetup]
    public void Setup()
    {
        _list = Enumerable.Range(0, Count).Select(i => $"item-{i}").ToList();
        _readOnlyList = new ReadOnlyList<string>(_list);
        _query = Enumerable.Range(0, Count).Select(i => $"item-{i}");
    }

    [Benchmark(Baseline = true)]
    public void List() => ProcessData(_list);

    [Benchmark]
    public void ReadOnlyList() => ProcessData(_readOnlyList);

    [Benchmark]
    public void Query() => ProcessData(_query);

    private static void ProcessData(
        IEnumerable<string> data)
    {
        for (int i = 0; i < data.Count(); i++)
        {
            ProcessItem(data.ElementAt(i));
        }
    }

    [Params(1, 10, 100)]
    public int Count { get; set; }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ProcessItem(string item) { }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/type-based-specialization-69958955/?t=10)

在 `ProcessData` 方法中,代码使用了一个 `for` 循环,在条件里调用 `data.Count()`,并用 `data.ElementAt(i)` 取出元素。
这段代码的性能会随输入类型而变化:

1. **List**:提供线性速度($O(N)$)且零分配。
   LINQ 识别出 `List<T>` 实现了 `ICollection`(用于 $O(1)$ 的 `Count()`)和 `IList`(用于 $O(1)$ 的 `ElementAt`)。
2. **ReadOnlyList**:由于这个自定义实现只实现了 `IEnumerable<T>`,LINQ 无法使用特化的优化。
   `Count()` 必须枚举整个序列($O(N)$),而 `ElementAt(i)` 必须枚举到第 $i$ 个元素($O(N)$)。
   这导致平方级的速度($O(N^2)$)和线性的内存分配。
3. **Query**:作为一个惰性的 LINQ 表达式,它表现最差,导致平方级的速度和平方级的内存分配,因为查询被反复重新求值。

### 优化模式

LINQ 在可能的情况下采用了若干模式来缓解这些问题:

- **TryGetNonEnumeratedCount**:这是一个公开方法,它检查能否在不枚举的情况下取得元素个数(例如通过检查 `ICollection`)。
  这把获取个数的复杂度从线性变成常数。
  它被 `Count`、`Take`、`ToList`、`ToArray`、`Reverse` 和 `Concat` 这些方法在内部使用。
- **IList 索引器**:`ElementAt`、`First`、`Last` 和 `Single` 这类方法会检查数据源是否实现了 `IList`。
  如果实现了,它们就使用索引器直接访问,而不是枚举整个序列。
- **TryGetSpan**:这是一项内部优化,目前仅限于数组和 `List<T>`。
  它让 LINQ 可以直接访问底层内存,为 `SequenceEqual`、`Min`、`Max`、`Average` 和 `Select` 这类方法启用向量化。

为了确保基准测试中使用的 `ReadOnlyList<T>` 不会从这些优化中获益,它被实现成一个只暴露 `IEnumerable<T>` 接口的简单包装器:

```csharp
public sealed class ReadOnlyList<T> : IEnumerable<T>
{
    private readonly IEnumerable<T> _source;

    public ReadOnlyList(IEnumerable<T> source) => _source = source;

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var item in _source)
            yield return item;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/type-based-specialization-69958955/?t=25)

## 11. Iterator Fusion

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/iterator-fusion-69958956/) · 2:04

### 总结

迭代器融合是一项 LINQ 性能优化,它把多个运算符组合成单个迭代器实例,以减少开销和内存分配。
通过把连续的操作 - 例如一次 Where 后面跟着一次 Select - 合并进单个状态机,LINQ 把创建的对象数量和每个元素的处理成本降到最低。
此外,迭代器融合还能保持底层数据源的算法复杂度;例如,在 List 上链式调用 Select 之后再调用 ElementAt,可以通过组合选择器并利用数据源的索引器来维持 O(1) 的访问,而朴素的基于 yield 的实现则会退化成 O(n) 的线性复杂度。

### 核心概念

- **迭代器融合**:把多个迭代器组合进单个层次,以去掉不必要的开销。
- **减少分配**:把每次管道迭代中实例化的迭代器对象数量降到最低。
- **保持复杂度**:通过特化的迭代器,维持底层数据源(如数组或列表)的 O(1) 性能特征。
- **朴素实现**:标准的 `yield return` 模式,缺乏跨运算符的感知和针对源类型的优化。

### 课程笔记

为了理解迭代器融合的影响,来看标准 LINQ 与 `Where` 和 `Select` 的朴素实现之间的对比。
在自定义的、未经优化的实现中,每个运算符通常都被实现为一个独立的 `yield return` 迭代器。
这就创建出一条管道,其中每个阶段都必须被独立地实例化和遍历。

```csharp
internal static class CustomLinq
{
    public static IEnumerable<T> MyWhere<T>(
        this IEnumerable<T> source, Func<T, bool> predicate)
    {
        foreach (var item in source)
            if (predicate(item))
                yield return item;
    }

    public static IEnumerable<TResult> MySelect<TSource, TResult>(
        this IEnumerable<TSource> source, Func<TSource, TResult> selector)
    {
        foreach (var item in source)
            yield return selector(item);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/iterator-fusion-69958956/?t=10)

用一个链式调用 `Where` 和 `Select` 的基准测试来比较这些自定义迭代器与标准 LINQ 的性能时,LINQ 版本明显更高效。
这是因为 LINQ 使用了迭代器融合。
如果你在数组或列表上调用 `Where` 后面跟着 `Select`,LINQ 会创建单个特化的迭代器实例(例如 `WhereSelectArrayIterator`),而不是多层嵌套的迭代器。
这带来更少的分配和更低的单元素开销。

```csharp
[HideColumns(Column.Error, Column.StdDev, Column.RatioSD, Column.Job, Column.AllocRatio, Column.Gen0)]
[MemoryDiagnoser]
[ShortRunJob(RuntimeMoniker.Net10_0)]
public class WhereSelectFusionBenchmarks
{
    private List<int> _list = null!;

    [GlobalSetup]
    public void Setup() => _list = Enumerable.Range(0, N).ToList();

    [Benchmark(Baseline = true)]
    public int Linq_WhereSelect()
    {
        int sum = 0;
        foreach (var x in _list.Where(x => (x & 1) == 0).Select(x => x * 2))
            sum += x;
        return sum;
    }

    [Benchmark]
    public int Custom_WhereSelect()
    {
        int sum = 0;
        foreach (var x in _list.MyWhere(x => (x & 1) == 0).MySelect(x => x * 2))
            sum += x;
        return sum;
    }

    [Params(10_000)]
    public int N { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/iterator-fusion-69958956/?t=25)

迭代器融合的第二个、往往更关键的方面,是能够为某些操作保持常数复杂度($O(1)$)。
在朴素的实现中,多次调用 `Select` 之后再调用 `ElementAt`,会导致对数据源的线性遍历($O(n)$)。

然而,LINQ 的迭代器融合可以优化这一点。
当在一个支持索引的数据源(例如 `List<T>`)上调用 `Select` 时,LINQ 可以把这些选择器合并成单次变换,并直接作用于请求索引处的元素。
这就把操作转换成对底层数据源使用索引器的一次合并选择器调用,从而保持 $O(1)$ 的复杂度。

```csharp
[HideColumns(Column.Error, Column.StdDev, Column.RatioSD, Column.Job, Column.AllocRatio, Column.Gen0)]
[Config(typeof(FlatConfig))]
[MemoryDiagnoser]
[ShortRunJob(RuntimeMoniker.Net10_0)]
public class ElementAtFusionBenchmarks
{
    private List<int> _list = null!;

    [GlobalSetup]
    public void Setup() => _list = Enumerable.Range(0, Count).ToList();

    [Benchmark]
    public int Linq_ElementAt() => _list.Select(x => x + 1).Select(x => x * 2).ElementAt(Count / 2);

    [Benchmark]
    public int Custom_ElementAt() => _list.MySelect(x => x + 1).MySelect(x => x * 2).ElementAt(Count / 2);

    [Params(10, 100, 1000)]
    public int Count { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/iterator-fusion-69958956/?t=40)

基准测试结果证实,自定义实现的执行时间随输入规模线性增长,而 LINQ 版本无论数据规模多大都保持恒定。

## 12. Iterator Fast Path Overrides

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/iterator-fast-path-overrides-69958958/) · 1:24

### 总结

迭代器快速路径重写是 LINQ 中的一类性能优化:运行时识别出特定的运算符序列,并短路执行管道,以更高效地取得结果。
通过识别 Reverse().Last() 或 OrderBy().First() 这样的模式,.NET 运行时可以绕过完整反转序列或排序这类不必要的中间步骤,带来显著的性能提升和更少的内存分配。
这些优化在每个新的 .NET 版本中不断被引入,在 .NET 4.8、8.0 和 10.0 上可以观察到不同的行为。

### 核心概念

- **快速路径重写**:让 LINQ 通过以更高效的方式解读查询,直接从原始数据源取得结果的内部优化。
- **管道短路**:跳过 LINQ 链中无关的中间步骤(例如,当只需要第一个元素或做包含性检查时跳过排序)。
- **模式识别**:运行时把复杂的调用链等价成更简单操作的能力,例如把 `Reverse().Last()` 当作 `First()`。
- **运行时演进**:性能特征因 .NET 版本而异;例如,针对 `Reverse().Last()` 的某些优化是在 .NET 10 中引入的,而 `OrderBy().First()` 是在 .NET 8 中被优化的。

### 课程笔记

快速路径重写的设计目的是短路 LINQ 管道,直接从数据源取得结果。
在许多场景中,运行时可以用不同的方式解读查询,从而避免昂贵的操作。
例如,调用 `.Reverse()` 后面跟着 `.Last()`,在逻辑上等价于调用 `.First()`。
在另一些情况下,中间步骤可以被完全忽略;如果一个序列先用 `.OrderBy()` 排序,然后用 `.Contains()` 检查,那么排序这一步对结果无关紧要,可以被整个跳过。

下面的基准测试演示了这些优化在不同 .NET 运行时上的表现,具体针对 .NET 4.8、.NET 8.0 和 .NET 10.0。
这里使用 `MemoryDiagnoser` 来跟踪分配,因为这些优化常常通过避免创建中间迭代器对象来减少内存占用。

```csharp
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

[HideColumns(Column.Error, Column.StdDev, Column.RatioSD, Column.Job, Column.AllocRatio, Column.Gen0, Column.Gen1)]
[Config(typeof(MethodFirstConfig))]
[MemoryDiagnoser]
[ShortRunJob(RuntimeMoniker.Net48)]
[ShortRunJob(RuntimeMoniker.Net80)]
[ShortRunJob(RuntimeMoniker.Net10_0)]
public class FastPathOverridesBenchmarks
{
    private const int Count = 1000;

    private IEnumerable<int> _data = Array.Empty<int>();

    [GlobalSetup]
    public void Setup()
    {
        var rng = new Random(42);
        _data = Enumerable.Range(0, Count).Select(_ => rng.Next()).ToArray();
    }

    [Benchmark]
    public int ReverseLast() => _data.Reverse().Last();

    [Benchmark]
    public int OrderByFirst() => _data.OrderBy(x => x).First();
}

public sealed class MethodFirstConfig : ManualConfig
{
    public MethodFirstConfig() => Orderer = new MethodFirstOrderer();
}

#nullable enable
public sealed class MethodFirstOrderer : IOrderer
{
    private static readonly IOrderer Default = DefaultOrderer.Instance;

    public IEnumerable<BenchmarkCase> GetExecutionOrder(
        ImmutableArray<BenchmarkCase> benchmarksCase,
        IEnumerable<BenchmarkLogicalGroupRule>? order = null)
        => Default.GetExecutionOrder(benchmarksCase, order);

    public IEnumerable<BenchmarkCase> GetSummaryOrder(
        ImmutableArray<BenchmarkCase> benchmarksCases, Summary summary)
        => benchmarksCases
            .OrderBy(b => b.Descriptor.WorkloadMethod.MetadataToken)
            .ThenBy(b => b.Job.DisplayInfo);

    public string GetHighlightGroupKey(BenchmarkCase benchmarkCase) => string.Empty;

    public string GetLogicalGroupKey(
        ImmutableArray<BenchmarkCase> allBenchmarksCases, BenchmarkCase benchmarkCase) => "*";

    public IEnumerable<IGrouping<string, BenchmarkCase>> GetLogicalGroupOrder(
        IEnumerable<IGrouping<string, BenchmarkCase>> logicalGroups,
        IEnumerable<BenchmarkLogicalGroupRule>? order = null) => logicalGroups;

    public bool SeparateLogicalGroups => false;
}
#nullable disable
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/iterator-fast-path-overrides-69958958/?t=10)

基准测试结果表明,.NET 团队会根据常见的 LINQ 使用模式来安排优化的优先级,以便在各类应用中产生最广泛的影响。
这些重写带来的性能影响可能非常剧烈。
例如,针对 `OrderBy().First()` 的优化是在 .NET 8 中引入的,而针对 `Reverse().Last()` 的优化是在 .NET 10 中加入的。
在 .NET 4.8 这样较老的运行时上,这些调用通常会执行完整的管道,相比现代版本会导致更高的执行时间和更多的分配。

## 13. For loop vs. Enumerable.SequenceEquals

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/for-loop-vs-enumerable-sequenceequals-69958960/) · 1:51

### 总结

用手写 for 循环与 Enumerable.SequenceEqual 比较字节数组相等性,揭示出 .NET Framework 与现代 .NET 版本之间显著的性能反转。
在较老的框架上 LINQ 的做法慢得多,而在现代 .NET 上,得益于内部的向量化和 SIMD 指令,它比手写循环快 20 倍。
本课探讨现代 .NET 如何利用 Span 和硬件加速来优化序列比较,并演示如何为更老的环境手动实现向量化。

### 核心概念

- .NET Framework 与现代 .NET 之间的性能差距。
- 向量化与 SIMD(Single Instruction Multiple Data)。
- Enumerable.SequenceEqual 的实现细节(在现代 .NET 上基于 Span)。
- 使用 System.Numerics.Vector 手动向量化。
- 硬件加速(AVX2)。

### 课程笔记

本课比较了在 `Hash` 结构体内部检查字节数组相等性的两种方法:标准的 `for` 循环(`EqualsLoop`)和 `Enumerable.SequenceEqual`(`EqualsLinq`)。

```csharp
public readonly struct Hash : IEquatable<Hash>
{
    public byte[] Bytes { get; }

    public Hash(byte[] bytes) => Bytes = bytes;

    public bool EqualsLoop(Hash other)
    {
        if (Bytes.Length != other.Bytes.Length)
            return false;

        for (int i = 0; i < Bytes.Length; i++)
        {
            if (Bytes[i] != other.Bytes[i])
                return false;
        }

        return true;
    }

    public bool EqualsLinq(Hash other)
        => Enumerable.SequenceEqual(Bytes, other.Bytes);

    public bool Equals(Hash other) => EqualsLoop(other);
}
```

基准测试在 .NET Framework 4.8 和 .NET 10 上分别进行,以比较这些实现。

```csharp
[HideColumns(Column.Error, Column.StdDev, Column.RatioSD, Column.AllocRatio, Column.Gen1, Column.Gen0, Column.Job)]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByJob)]
[Orderer(SummaryOrderPolicy.Method)]
[MemoryDiagnoser]
[ShortRunJob(RuntimeMoniker.Net48)]
[ShortRunJob(RuntimeMoniker.Net10_0)]
public class SequenceEqualBenchmarks
{
    private Hash _left;
    private Hash _right;

    [GlobalSetup]
    public void Setup()
    {
        var bytes = Enumerable.Range(0, 256).Select(x => (byte)x).ToArray();
        _left = new Hash(bytes);
        _right = new Hash((byte[])bytes.Clone());
    }

    [Benchmark(Baseline = true)]
    public bool EqualsLoop() => _left.EqualsLoop(_right);

    [Benchmark]
    public bool EqualsLinq() => _left.EqualsLinq(_right);
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/for-loop-vs-enumerable-sequenceequals-69958960/?t=10)

结果显示出运行时之间性能的剧烈反转。
在 .NET Framework 上,LINQ 版本大约比手写 `for` 循环慢 20 倍。
然而在现代 .NET(Core/10)上,LINQ 版本比 `for` 循环快 20 倍。

这一性能提升归功于向量化。
在现代 .NET 上,`Enumerable.SequenceEqual` 在底层使用了 `Span<T>`。
许多 `Span` 操作会用 AVX2 这样的 SIMD(Single Instruction Multiple Data)指令集进行向量化。
标量比较一次处理一个字节(对 256 字节的数组需要 256 次操作),而使用 AVX2 的向量化版本每次操作可以处理 32 个字节,把任务减少到仅仅 8 次操作。
即使算法复杂度保持不变,这项优化也能带来 10 倍到 20 倍的性能提升。

要在 LINQ 未被向量化的 .NET Framework 上获得类似的性能,开发者可以使用 `System.Numerics.Vector<T>` 手动实现向量化。

```csharp
private static bool EqualsVectorized(byte[] left, byte[] right)
{
    if (left.Length != right.Length) return false;
    if (!Vector.IsHardwareAccelerated)
    {
        return EqualsLoop(left, right);
    }

    // Vector<byte>.Count is 32 on AVX2 systems — we process 32 bytes at a time
    int vectorSize = Vector<byte>.Count;
    int i;
    // Stop early so we don't read past the end of the array
    int limit = left.Length - vectorSize + 1;

    for (i = 0; i < limit; i += vectorSize)
    {
        var va = new Vector<byte>(left, i);
        var vb = new Vector<byte>(right, i);
        if (va != vb) return false;
    }

    // Process remaining bytes that don't fill a full vector
    for (; i < left.Length; i++)
    {
        if (left[i] != right[i]) return false;
    }

    return true;
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/for-loop-vs-enumerable-sequenceequals-69958960/?t=105)

## 14. Vectorization in .NET Framework

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/vectorization-in-dotnet-framework-69958961/) · 2:53

向量化通过用单条指令处理多个数据点来获得显著的性能提升。
现代 .NET 版本内置了广泛的向量化,而 .NET Framework 需要更多手动实现才能达到类似的效果。

### 核心概念

- **SIMD(Single Instruction, Multiple Data)**:让向量化成为可能的底层硬件能力。
- **Vector<T>**:`System.Numerics` 中用于在 .NET 上手动向量化的主要类型。
- **硬件加速**:向量化依赖 CPU 支持;代码在执行前必须检查 `Vector.IsHardwareAccelerated`。
- **向量大小与边界**:向量化循环必须按块处理数据(例如 32 字节),并在"尾部"循环中处理剩余的元素。
- **Framework 上的 Span<T>**:虽然可以通过 NuGet 获得,但 .NET Framework 上的 `Span<T>` 性能不如现代 .NET,不过仍然优于手写的逐字节循环。
- **平台目标**:在 .NET Framework 上,向量化往往需要显式指定 x64 目标,以避免软件模拟。

### 课程笔记

要在 .NET Framework 上为哈希结构体或字节数组比较实现向量化,一种选择是把字节数组当作 `ReadOnlySpan<byte>` 并使用 `SequenceEqual`。
虽然 `Span<T>` 在完整框架上也可用,但它的性能不如 .NET Core/现代 .NET 的版本,后者使用 JIT 内建函数在内部做向量化。
不过,它仍然比手写的 for 循环更高效。

```csharp
public bool EqualsLinq(Hash other)
{
    return Enumerable.SequenceEqual(Bytes, other.Bytes);
}

public bool EqualsSpan(Hash other)
{
    // MemoryExtensions.SequenceEqual on ReadOnlySpan<byte> is a JIT intrinsic
    // on modern .NET - it vectorizes internally without a manual Vector<byte> loop.
    return Bytes.AsSpan().SequenceEqual(other.Bytes);
}

public bool EqualsVectorized(Hash other)
{
    return EqualsVectorized(Bytes, other.Bytes);
}

private static bool EqualsVectorized(byte[] left, byte[] right)
{
    if (left.Length != right.Length) return false;
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/vectorization-in-dotnet-framework-69958961/?t=10)

对于更复杂的场景,或者为了在 .NET Framework 上把性能压榨到极致,你可以使用 `System.Numerics` 命名空间中的 `Vector<byte>`。
这需要更复杂的实现,因为你必须尊重硬件的向量大小。
例如,如果向量大小是 32 字节而数组是 33 字节,你就用单条指令比较前 32 字节,然后用普通循环处理剩下的那个字节。

在执行向量化代码之前,你必须检查 `Vector.IsHardwareAccelerated`。
如果它是 false,实现就应该回退到普通循环。
在实现向量化循环时,上界被算作 `left.Length - vectorSize + 1`,以确保在加载完整向量时代码不会越过数组末尾读取。

```csharp
private static bool EqualsVectorized(byte[] left, byte[] right)
{
    if (left.Length != right.Length) return false;
    if (!Vector.IsHardwareAccelerated)
    {
        return EqualsLoop(left, right);
    }

    // Vector<byte>.Count is 32 on my box (AVX2) - we process 32 bytes at a time
    int vectorSize = Vector<byte>.Count;
    int i;
    // Stop early so we don't read past the end of the array
    int limit = left.Length - vectorSize + 1;

    for (i = 0; i < limit; i += vectorSize)
    {
        var va = new Vector<byte>(left, i);
        var vb = new Vector<byte>(right, i);
        if (va != vb) return false;
    }

    // Process remaining bytes that don't fill a full vector
    for (; i < left.Length; i++)
    {
        if (left[i] != right[i]) return false;
    }

    return true;
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/vectorization-in-dotnet-framework-69958961/?t=40)

在不同架构上做基准测试时,例如基于 ARM 的笔记本,往往需要为 .NET Framework 显式指定 `x64` 作为目标框架。
否则,向量化可能会在软件中被模拟,导致没有性能收益甚至性能倒退。

```csharp
[HideColumns(Column.Error, Column.StdDev, Column.RatioSD, Column.Gen0, Column.AllocRatio, Column.Jit, Column.Platform, Column.Job)]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByJob)]
[Orderer(SummaryOrderPolicy.Method)]
[MemoryDiagnoser]
[ShortRunJob(RuntimeMoniker.Net10_0)]
// X64 platform will make the results reproducible across different machines (arm-based and x64-based).
// Even on arm machines, this is the only version that will show the vectorization benefits.
[ShortRunJob(RuntimeMoniker.Net48, Jit.Default, Platform.X64)]
public class SequenceEqualBenchmarks
{
    private Hash _left;
    private Hash _right;

    [GlobalSetup]
    public void Setup()
    {
        var bytes = Enumerable.Range(0, 256).Select(x => (byte)x).ToArray();
        _left = new Hash(bytes);
        _right = new Hash((byte[])bytes.Clone());
    }

    [Benchmark(Baseline = true)]
    public bool EqualsLoop() => _left.EqualsLoop(_right);

    [Benchmark]
    public bool EqualsLinq() => _left.EqualsLinq(_right);

    [Benchmark]
    public bool EqualsSpan() => _left.EqualsSpan(_right);

    [Benchmark]
    public bool EqualsVectorized() => _left.EqualsVectorized(_right);
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/vectorization-in-dotnet-framework-69958961/?t=85)

在 .NET Framework 上的基准测试显示,虽然算法复杂度仍然是 O(n),但性能差异极其剧烈。
LINQ 在完整框架上没有被向量化,是最慢的选项。
最慢的版本(LINQ)与最快的版本(向量化)之间的差距可以高达 300 倍。
相比之下,在现代 .NET(例如 .NET 10)上,LINQ、基于 Span 的实现和自定义向量化实现的表现大致相同,因为 LINQ 已经被更新为在内部使用向量化。

.NET 中的向量化支持随每个版本不断扩展:

- **.NET 7**:增加了对 `Sum`、`Average`、`Min` 和 `Max` 的支持。
- **.NET 8**:增加了对 `Any`、`Contains` 和 `SequenceEqual` 的支持。
- **.NET 9**:增加了对 `Select`、`Where` 和 `Zip` 的支持。

## 15. Summary

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958962/) · 2:14

### 总结

LINQ 的性能不是 C# 语言的静态属性,而是底层运行时和所涉及的具体数据结构的动态属性。
.NET Framework 4.8 这样的旧环境常常受累于委托分配和装箱枚举器,而 .NET 10 这样的现代运行时利用先进的 JIT 优化 - 包括 lambda 去抽象化和 SIMD 向量化 - 达到了与手写循环相当的性能。
开发者应当优先写可读的 LINQ 代码,只有当基准测试揭示出热路径上确实存在瓶颈时才转向手写实现,并确保优化建立在对运行时如何执行查询的正确心智模型之上。

### 核心概念

- 性能优化流程:设定目标、度量、找出瓶颈、优化、再度量。
- 依赖运行时的性能:.NET Framework 对比 .NET 10。
- 基于类型的特化:对 ICollection 和 IList 数据源实现 O(1) 复杂度。
- 迭代器融合:把多个 LINQ 阶段合并成单个迭代器,以减少分配和 MoveNext 调用。
- 快速路径重写:让 Reverse().Last() 这类查询得以短路的虚方法钩子。
- 向量化:通过 Span<T> 和 Vector<T> 使用 SIMD 完成高性能的批量操作。
- JIT 去抽象化:在现代 .NET 运行时中消除委托开销。

### 课程笔记

LINQ 究竟"慢"还是"快"要看上下文。
如果 LINQ 不是应用程序的瓶颈,把它改写成手写循环只会让代码更难维护,却不会提升性能。
性能取决于运行时、数据源(例如 list 还是 array)以及查询的复杂度。

一套技术性的优化流程必不可少:设定目标、度量、找出瓶颈、优化、再度量。
.NET 10 这样的现代运行时引入的优化,让高层的 LINQ 代码可以和手写实现一样高效。

#### LINQ 与手写循环

在 .NET Framework 4.8 这样较老的运行时上,LINQ 常常要为委托分配和装箱枚举器付出代价。
然而在 .NET 10 上,JIT 可以对 lambda 去抽象化,使 LINQ 版本能够以零分配达到甚至超过手写循环。

```csharp
[Benchmark(Baseline = true)]
public bool ManualLoop()
{
    foreach (var item in _list)
    {
        if (item == InstanceValue)
            return true;
    }
    return false;
}

[Benchmark]
public bool LinqAny()
    => _list.Any(x => x == InstanceValue);
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958962/?t=77)

#### 优化模式

有若干内部机制共同促成了 LINQ 的性能:

- **基于类型的特化**:`Count()` 和 `ElementAt()` 这样的运算符针对 `IList<T>` 和 `ICollection<T>` 做了特化。
  当检测到这些接口时,LINQ 会使用直接的属性或索引器访问(O(1)),而不是完整枚举(O(N))。

```csharp
private static void ProcessData(IEnumerable<string> data)
{
    for (int i = 0; i < data.Count(); i++)
    {
        ProcessItem(data.ElementAt(i));
    }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958962/?t=85)

- **迭代器融合**:LINQ 把 `Where` 和 `Select` 这样的多个阶段组合成单个融合后的迭代器(例如 `WhereSelectArrayIterator`)。
  这减少了分配的状态机对象数量,以及每个元素所需的 `MoveNext` 调用次数。

```csharp
public static IEnumerable<T> MyWhere<T>(this IEnumerable<T> source, Func<T, bool> predicate)
{
    foreach (var item in source)
        if (predicate(item))
            yield return item;
}

public static IEnumerable<TResult> MySelect<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
{
    foreach (var item in source)
        yield return selector(item);
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958962/?t=85)

- **快速路径重写**:迭代器可以重写虚方法钩子来优化特定的查询模式。
  例如,`Reverse().Last()` 会短路成返回数据源的第一个元素,而 `OrderBy().First()` 会在不做完整排序的情况下以 O(N) 找出最小元素。

```csharp
[Benchmark]
public int ReverseLast() => _data.Reverse().Last();

[Benchmark]
public int OrderByFirst() => _data.OrderBy(x => x).First();
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958962/?t=85)

- **向量化**:.NET 运行时使用 SIMD 来加速批量操作。
  `Enumerable.SequenceEqual` 依赖缓慢的 `IEnumerator` 调用链,而 `Span<T>.SequenceEqual` 是一个向量化的内建函数。
  使用 `Vector<T>` 手动向量化可以让每次迭代处理多个字节(例如在 AVX2 上是 32 字节)。

```csharp
private static bool EqualsVectorized(byte[] left, byte[] right)
{
    if (left.Length != right.Length) return false;
    if (!Vector.IsHardwareAccelerated) return EqualsLoop(left, right);

    int vectorSize = Vector<byte>.Count;
    int i;
    int limit = left.Length - vectorSize + 1;

    for (i = 0; i < limit; i += vectorSize)
    {
        var va = new Vector<byte>(left, i);
        var vb = new Vector<byte>(right, i);
        if (va != vb) return false;
    }

    for (; i < left.Length; i++)
    {
        if (left[i] != right[i]) return false;
    }
    return true;
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958962/?t=98)

是否用手写实现替换 LINQ,应当基于对性能成本和具体运行时行为的清晰理解,而不是基于对 LINQ 开销的笼统假设。
