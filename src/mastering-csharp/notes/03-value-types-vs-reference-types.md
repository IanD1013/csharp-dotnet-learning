# Value Types vs. Reference Types

> 课程：[Mastering: C#](https://dometrain.com/course/mastering-csharp/) · 第 3 章
> 共 12 课 · 约 21:27
> 来源：Dometrain。由课程文档翻译整理；每一节都链接到对应课程。

---

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Module Overview](https://dometrain.com/take/course/mastering-csharp-3256129/module-overview-69958834/) | 1:24 | [↓](#1-module-overview) |
| 2 | [Overview](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958835/) | 0:56 | [↓](#2-overview) |
| 3 | [Storage Model: Inline vs. Indirection](https://dometrain.com/take/course/mastering-csharp-3256129/storage-model-inline-vs-indirection-69958836/) | 2:49 | [↓](#3-storage-model-inline-vs-indirection) |
| 4 | [Exploring Storage](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-storage-69958837/) | 1:03 | [↓](#4-exploring-storage) |
| 5 | [Analyzing Storage Model with Benchmarks](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-storage-model-with-benchmarks-69958838/) | 1:58 | [↓](#5-analyzing-storage-model-with-benchmarks) |
| 6 | [Inspecting Type Layout (part 2)](https://dometrain.com/take/course/mastering-csharp-3256129/inspecting-type-layout-part-2-69958839/) | 1:06 | [↓](#6-inspecting-type-layout-part-2) |
| 7 | [Exploring Access Patterns with Benchmarks](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-access-patterns-with-benchmarks-69958840/) | 1:00 | [↓](#7-exploring-access-patterns-with-benchmarks) |
| 8 | [Analyzing Access Patterns Benchmark Results](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-access-patterns-benchmark-results-69958841/) | 3:16 | [↓](#8-analyzing-access-patterns-benchmark-results) |
| 9 | [Copy Semantics: Assignment](https://dometrain.com/take/course/mastering-csharp-3256129/copy-semantics-assignment-69958842/) | 1:25 | [↓](#9-copy-semantics-assignment) |
| 10 | [Copy Semantics: Parameter Passing](https://dometrain.com/take/course/mastering-csharp-3256129/copy-semantics-parameter-passing-69958843/) | 3:27 | [↓](#10-copy-semantics-parameter-passing) |
| 11 | [Equality Semantics](https://dometrain.com/take/course/mastering-csharp-3256129/equality-semantics-69958844/) | 1:15 | [↓](#11-equality-semantics) |
| 12 | [Summary](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958845/) | 1:48 | [↓](#12-summary) |

---

## 1. Module Overview

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/module-overview-69958834/) · 1:24

### 总结

C# 类型系统是应用语义的基础，无论采用面向对象还是函数式的方式，它都会影响标识（identity）、可变性和相等性。
除了逻辑之外，类型还会通过内存分配模式和对象布局显著影响性能。
要用好 record 和模式匹配等现代语言特性，就必须掌握类型系统，因为这些特性建立在基于值的相等性和基于类型的查询这两个基本概念之上。

### 核心概念

- **应用语义**：类型定义了数据如何流动，以及系统在不同编程范式下的行为方式。
- **性能影响**：在引用类型和值类型之间的选择会影响分配、复制语义和对象布局。
- **领域建模**：record、元组和联合类型（union types）可以对领域进行精确建模，从而带来更清晰的 API。
- **现代语言特性**：record 和模式匹配等特性是类型系统核心原则的直接延伸。
- **基于值的相等性**：这是 record 类型行为背后的基本概念。

### 课程笔记

类型系统是任何 C# 应用的基石，无论架构是面向对象的（使用 class 和 struct）还是函数式的（使用 record 和纯函数），它都定义了基本语义。
类型支配着软件行为的关键方面，包括标识、可变性和相等性。
这些定义决定了数据如何在系统中流动，以及系统的行为是否正确。

除了逻辑行为之外，类型还会直接影响应用的性能。
引用类型和值类型之间的区别决定了内存分配模式、复制语义和对象布局。
通过使用 record、元组和联合类型可以实现精确的领域建模，它们能让 API 的意图更清晰并减少歧义。
定义不佳的类型会导致 API 不清晰，而定义良好的类型会让开发者的意图一目了然。

理解类型系统是掌握现代 C# 特性的前提。
例如，record 并不是一种"神奇"的抽象；它们是实现了基于值的相等性和受控不可变性的引用类型。
如果理解了基于值的相等性这一概念，record 的行为就会变得直观。
同样，模式匹配不只是一种语法特性，而是一种就某个值的类型、可空性和成员状态向类型系统发起查询的方式。
当把模式匹配看作向类型系统提问的方法时，它就更容易推理、阅读和实现。

---

## 2. Overview

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958835/) · 0:56

### 总结

本课介绍 C# 中值类型与引用类型之间的根本区别，超越简单的栈与堆之分，探讨存储、复制和相等性语义。
它把 C# 类型分为引用类型、值类型和指针类型，建立起一个框架，用于理解这些分类如何影响内存分配、性能和应用逻辑。

### 核心概念

- **类型类别**：C# 类型分为引用类型、值类型和指针类型。
- **引用类型**：包括 class、record、数组、object 和委托。
- **值类型**：包括 struct、record struct、枚举和基元类型（它们是 struct 的别名）。
- **存储语义**：关注数据在内存中位于何处，以及对象或结构的物理布局。
- **复制语义**：定义赋值操作时复制的是什么（引用还是实际数据）。
- **相等性语义**：决定比较实例时的默认行为（标识相等还是基于值的相等）。

### 课程笔记

C# 类型分为三大类：引用类型、值类型和指针类型。
指针类型主要用于非托管上下文，在常规应用开发中不像另外两类那样广泛使用。

#### 类型分类

类型根据运行时处理它们的方式以及它们在内存中的存储方式进行分类：
- **引用类型**：包括 class、record、数组、object 和委托。这些类型的变量存储的是对数据的引用，而不是数据本身。
- **值类型**：包括 struct、record struct、枚举和基元类型。在 C# 中，基元类型（如 `int` 或 `bool`）被实现为 struct 的别名。这些类型的变量包含实际的数据。

#### 比较的三大支柱

要完全理解这些类型之间的区别，必须从三个不同的角度来分析它们：

1. **存储语义**：指数据在内存中位于何处。这包括类型的分配模式，例如 struct 数组如何只产生一次连续的分配，而 class 数组则涉及多次分配（数组本身一次，每个实例各一次）。这还涵盖对象布局，包括对象头和引用槽。

2. **复制语义**：定义赋值运算符的行为。对于引用类型，赋值复制的是引用，这意味着原变量和新变量都指向内存中的同一个实例。对于值类型，赋值复制的是实际内容，从而得到两份相互独立的数据副本。

3. **相等性语义**：定义默认如何判断相等。引用类型通常使用引用标识（检查两个变量是否指向同一个内存地址）。值类型使用值相等，检查底层数据是否相同。这还包括如何重写这些默认行为，以及默认相等性检查的性能影响（例如在某些 struct 相等性场景中反射带来的开销）。

这些区别对性能有显著影响，尤其是在迭代成本和"洗牌效应"（shuffle effect）方面。
值类型的内存布局可以更高效地利用缓存和硬件预取，而引用类型的间接寻址以及可能出现的内存碎片会导致更高的解引用成本。

---

## 3. Storage Model: Inline vs. Indirection

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/storage-model-inline-vs-indirection-69958836/) · 2:49

### 总结

C# 类型的存储模型由内联存储与间接寻址之间的区别来定义。
值类型（struct）以内联方式存储，这意味着它们的数据直接包含在变量的内存槽或父对象之中。
引用类型（class）使用间接寻址，变量持有一个指向托管堆上所分配对象的引用（指针）。
虽然值类型常常与栈相关联、引用类型常常与堆相关联，但实际的存储位置由上下文决定，例如值类型是否是某个 class 中的字段，或者在现代运行时中引用类型是否适用栈分配优化。

### 核心概念

- **内联存储**：值类型把数据直接存储在变量声明的位置。
- **间接寻址**：引用类型存储一个指向别处数据（通常是托管堆）的指针。
- **依赖上下文的分配**：存储位置（栈还是堆）由声明所处的上下文决定，而不仅仅由类型本身决定。
- **数组布局**：值类型数组连续地存储数据，而引用类型数组存储的是一组指向各个独立堆对象的引用。

### 课程笔记

值类型与引用类型之间的根本区别在于它们的存储语义。
为了说明这一点，考虑一个 `Point` struct 和一个 `PointRef` class，二者都包含整型属性 `X` 和 `Y`。

```csharp
public struct Point(int x, int y)
{
    public int X { get; } = x;
    public int Y { get; } = y;
}

public class PointRef(int x, int y)
{
    public int X { get; } = x;
    public int Y { get; } = y;
}

public class C
{
    public PointRef P2 { get; set; }
    public Point P1 { get; set; }
}

static void Main(string[] args)
{
    var p1 = new Point(1, 2);
    var p2 = new PointRef(3, 4);
    var c = new C { P1 = p1, P2 = p2 };
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/storage-model-inline-vs-indirection-69958836/?t=10)

在这个场景中，如果在初始化 `c` 之后暂停执行，内存布局会呈现出不同的行为：
- `p1`（值类型）直接存储在栈上。
- `p2`（引用类型）在栈上产生一个引用，指向托管堆上的一个实例。
- `c`（引用类型）同样是栈上的一个引用，指向一个在堆上分配的对象。

在 `c` 的实例内部，其字段的存储方式取决于字段的类型。
`P1` 属性（一个 `Point` struct）以**内联**方式存储在为 `c` 对象分配的内存中。
相反，`P2` 属性（一个 `PointRef` class）存储的是一个引用，它指向栈上已经引用的同一个 `PointRef` 实例。

```text
  Stack                     Managed heap
+--------------+
| p1  X = 1    |
|     Y = 2    |          +---------------------+
+--------------+          | PointRef            |
| p2  (ref) ---+--------->|   X = 3             |
+--------------+          |   Y = 4             |
| c   (ref) ---+---+      +---------------------+
+--------------+   |                 ^
                   |                 |
                   |      +----------+----------+
                   +----->| C                   |
                          |   P1: X = 1, Y = 2  |  (inline)
                          |   P2: (ref) --------+
                          +---------------------+
```

需要注意的是，类型本身并不能严格决定存储位置；决定它的是上下文。
值类型并不总是在栈上；它们可以嵌入到在堆上分配的对象中（例如 `c` 中的 `P1`）。
同样，现代运行时在特定场景下可能会对引用类型执行栈分配。

#### 数组分配与性能

存储模型会显著影响数组在内存中的分配方式。
引用类型数组存储的是对象的引用。
如果分配一个包含 10 个 class 的数组，运行时会执行 11 次分配：数组本身一次，10 个独立对象各一次。
然而，值类型数组会把数据内联存储在数组的内存块中，只需要一次分配。

```csharp
[Benchmark]
public PointRef[] CreateArrayOfPointRefs()
{
    var array = new PointRef[Size];
    for (int i = 0; i < array.Length; i++)
        // Creating the instance on the heap
        // and storing a reference in the array
        array[i] = new PointRef(i, i);
    return array;
}

[Benchmark]
public Point[] CreateArrayOfPoints()
{
    var array = new Point[Size];
    for (int i = 0; i < array.Length; i++)
        // Creating the value and store it
        // inline in the array
        array[i] = new Point(i, i);
    return array;
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/storage-model-inline-vs-indirection-69958836/?t=115)

虽然这些基准测试突显了分配方式上的结构差异，但它们应当用来建立内存布局的心智模型，而不是作为性能优化的唯一依据。
性能是一个复杂的话题，需要端到端的分析。

---

## 4. Exploring Storage

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-storage-69958837/) · 1:03

### 总结

本课通过分析数组分配来探讨值类型和引用类型的存储模型。
它演示了引用类型数组存储的是指向堆上已分配对象的指针，需要多次分配；而值类型数组把数据内联存储在数组本身之中，只需要一次分配。
这一区别是理解 C# 中内存布局和分配行为的关键因素。

### 核心概念

- **引用类型存储**：class 数组存储的是指向托管堆上已分配实例的引用。
- **值类型存储**：struct 数组把数据内联存储在数组的内存块中。
- **分配开销**：创建一个包含 N 个引用类型的数组会产生 N+1 次分配，而包含 N 个值类型的数组只产生一次分配。
- **心智模型**：对这些分配进行基准测试，有助于直观地看到 struct 与 class 在内存布局上的差异。

### 课程笔记

通过比较 struct 数组和 class 数组的分配方式，可以理解 C# 类型的存储模型。
这种比较是用来理解内存布局的心智模型，而不是直接的性能比较，因为性能是一个涉及端到端分析的复杂话题。

考虑以下这些表示坐标点的定义：

```csharp
public struct Point(int x, int y)
{
    public int X { get; } = x;
    public int Y { get; } = y;
}

public class PointRef(int x, int y)
{
    public int X { get; } = x;
    public int Y { get; } = y;
}
```

创建引用类型（`PointRef`）数组时，数组存储的是指向托管堆上已分配实例的引用。
对于大小为 10 的数组，这总共会产生 11 次分配：数组本身一次，10 个独立对象各一次。
相比之下，值类型（`Point`）数组以内联方式存储数据。

```csharp
[Benchmark]
public PointRef[] CreateArrayOfPointRefs()
{
    var array = new PointRef[Size];
    for (int i = 0; i < array.Length; i++)
        // Creating the instance on the heap
        // and storing a reference in the array
        array[i] = new PointRef(i, i);
    return array;
}

[Benchmark]
public Point[] CreateArrayOfPoints()
{
    var array = new Point[Size];
    for (int i = 0; i < array.Length; i++)
        // Creating the value and store it
        // inline in the array
        array[i] = new Point(i, i);
    return array;
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-storage-69958837/?t=10)

在 `CreateArrayOfPointRefs` 方法中，运行时会为数组中的每个元素执行一次分配。
然而，在 `CreateArrayOfPoints` 方法中，`Point` struct 的值内联存储在数组的内存中。
无论元素有多少，这都只会为整个数组产生一次分配。
这种存储模型提供了更好的数据局部性，并减轻了托管堆的压力。

```text
PointRef[]  (1 + 10 = 11 allocations)
+-----+-----+-----+-----+
| ref | ref | ref | ... |   array
+--+--+--+--+--+--+-----+
   |     |     |
   v     v     v
 [PointRef] [PointRef] [PointRef] ...   separate heap objects

Point[]  (1 allocation)
+-------+-------+-------+-----+
| X | Y | X | Y | X | Y | ... |   values inline in the array
+-------+-------+-------+-----+
```

---

## 5. Analyzing Storage Model with Benchmarks

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-storage-model-with-benchmarks-69958838/) · 1:58

### 总结

本课通过分析基准测试结果来探讨值类型与引用类型的内存存储模型。
它量化了堆上分配对象的分配开销（包括对象头和填充），并比较 struct 数组与 class 数组的内存占用，以说明值类型用于小型数据结构时的效率。

### 核心概念

* 堆上分配对象的内存开销（16 字节的对象头）。
* 数组结构的开销，包括长度字段和填充。
* 引用大小（64 位系统上为 8 字节）与内联值存储的对比。
* 使用 BenchmarkDotNet 进行内存分配分析。
* 对象布局对应用性能的影响。

### 课程笔记

为了理解值类型和引用类型存储模型的实际影响，我们使用一个基准测试来比较分别包含 `Point`（struct）和 `PointRef`（class）的数组的分配情况。
该基准测试在 Release 模式下运行，以确保性能和内存指标准确。

```csharp
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace ArrayAllocationBenchmarks
{
    public struct Point(int x, int y)
    {
        public int X { get; } = x;
        public int Y { get; } = y;
    }

    public class PointRef(int x, int y)
    {
        public int X { get; } = x;
        public int Y { get; } = y;
    }

    [MemoryDiagnoser]
    [ShortRunJob]
    [HideColumns("Error", "Gen1")]
    public class ArrayAllocationBenchmark
    {
        [Params(10, 100, 1000)]
        public int Size { get; set; }

        [Benchmark]
        public PointRef[] CreateArrayOfPointRefs()
        {
            var array = new PointRef[Size];
            for (int i = 0; i < array.Length; i++)
                // Creating the instance on the heap
                // and storing a reference in the array
                array[i] = new PointRef(i, i);
            return array;
        }

        [Benchmark]
        public Point[] CreateArrayOfPoints()
        {
            var array = new Point[Size];
            for (int i = 0; i < array.Length; i++)
                // Creating the value and store it
                // inline in the array
                array[i] = new Point(i, i);
            return array;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<ArrayAllocationBenchmark>();
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-storage-model-with-benchmarks-69958838/?t=10)

#### 引用类型的内存分析

分配 class 数组（`PointRef[]`）时，由于存在多层开销，内存消耗要高得多：
1. **对象头**：每个在堆上分配的对象都有 16 字节的开销。
2. **数组结构**：数组包含一个长度字段和填充（通常各 4 字节）。
3. **引用**：数组中的每个元素都是一个引用（64 位系统上为 8 字节），指向堆上别处的一个对象。
4. **实例开销**：堆上的每个 `PointRef` 实例除了两个 4 字节的整型字段（`X` 和 `Y`）之外，同样要承担 16 字节的对象头，每个实例共计 24 字节。

对于包含 10 个 `PointRef` 实例的数组，数组本身大约占用 108 字节。
10 个 `PointRef` 实例占用 240 字节（10 * 24）。
理论上，这会产生大约 344 字节的分配，不过由于舍入误差，基准测试可能会显示出细微的差异（例如 345 字节）。

```text
PointRef[10] array: header 16 | length 4 | padding 4 | 10 x ref (8 each)
PointRef instance:  header 16 | X 4 | Y 4                     = 24 bytes
                    10 instances x 24                         = 240 bytes
```

#### 值类型的内存分析

相比之下，struct 数组（`Point[]`）要高效得多。
因为值类型以内联方式存储在数组中：
1. 不会为每个元素单独进行堆分配。
2. 数组中不存储 8 字节的引用；数据本身直接位于数组的内存块中。
3. 总大小就是数组的开销（大小为 10 时为 108 字节），没有额外的逐元素堆开销。

这一比较表明，对于小型数据结构，使用 class 的开销可能相当可观。
虽然这未必总会成为性能瓶颈，但要确定它对特定应用的影响，基准测试是必不可少的。
可以使用 "Object Layout Inspector" 之类的工具进一步研究公共语言运行时（CLR）如何在内存中布局对象。

---

## 6. Inspecting Type Layout (part 2)

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/inspecting-type-layout-part-2-69958839/) · 1:06

### 总结

本课演示如何使用 ObjectLayoutInspector NuGet 包在运行时可视化 C# 类型的内存布局。
通过比较一个 struct 和一个 class，本课突显了引用类型的开销（具体来说是对象头和方法表指针），并说明值类型数组以内联方式存储数据，而引用类型数组存储的是指向各个独立堆分配的指针。

### 核心概念

*   **ObjectLayoutInspector**：一个 NuGet 包，用于在运行时检查对象和类型的物理内存布局。
*   **TypeLayout.PrintLayout<T>()**：一个 API 调用，用于打印特定类型的内存布局。
*   **ArrayLayout.PrintLayout(instance)**：一个 API 调用，用于打印特定数组实例的内存布局，从而可以检查元素的存储方式。
*   **引用类型开销**：每个 class 实例都包含 CLR 使用的对象头（Object Header）和方法表指针（Method Table Pointer）。
*   **值类型效率**：struct 没有任何开销，直接存储其字段，没有额外的元数据指针。
*   **数组存储模型**：struct 数组在内存中连续地存储元素；class 数组存储的是一块连续的引用，这些引用指向位于堆上别处的对象。

### 课程笔记

要使用对象布局检查器，必须把 `ObjectLayoutInspector` NuGet 包添加到项目中。
这个工具提供了一个简单的 API，用于可视化公共语言运行时（CLR）如何在内存中组织数据。

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFrameworks>net48;net10.0</TargetFrameworks>
    <RootNamespace>StorageModelExplained</RootNamespace>
    <ImplicitUsings>enable</ImplicitUsings>
    <LangVersion>latest</LangVersion>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="ObjectLayoutInspector" Version="0.2.0" />
  </ItemGroup>

</Project>
```

下面的实现比较了一个 struct（`Point`）和一个 class（`PointRef`），以及它们各自的数组表示。
对 struct 和 class 类型使用 `TypeLayout.PrintLayout`，对数组实例使用 `ArrayLayout.PrintLayout`，以查看特定元素数量下的实际布局。

```csharp
using ObjectLayoutInspector;
using System.Linq;

namespace StorageModelExplained
{
    public struct Point(int x, int y)
    {
        public int X { get; } = x;
        public int Y { get; } = y;
    }

    public class PointRef(int x, int y)
    {
        public int X { get; } = x;
        public int Y { get; } = y;
    }

    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Point (struct) ===");
            TypeLayout.PrintLayout<Point>();

            Console.WriteLine();
            Console.WriteLine("=== PointRef (class) ===");
            TypeLayout.PrintLayout<PointRef>();

            Console.WriteLine();
            Console.WriteLine("=== Point[] (array of structs) ===");
            var structs = new Point[10];
            ArrayLayout.PrintLayout(structs);

            Console.WriteLine();
            Console.WriteLine("=== PointRef[] (array of class references) ===");
            PointRef p = new PointRef(1, 2);
            PointRef[] refs = Enumerable.Range(1, 10).Select(_ => p).ToArray();
            ArrayLayout.PrintLayout(refs);
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/inspecting-type-layout-part-2-69958839/?t=10)

#### 运行时布局观察

##### Struct 与 Class 的布局

在运行时检查 `Point` struct 时，它显示没有任何开销；字段直接紧密排列。
相比之下，`PointRef` class 包含两个 CLR 用于内部管理的额外字段：
1.  **对象头**：用于同步以及其他内部元数据。
2.  **方法表指针**：指向该类型的方法表。

在这些元数据字段之后，class 包含与 struct 相同的两个整型字段。

```text
Point (struct)          PointRef (class)
+--------+              +----------------------+
| X      |              | Object Header        |
| Y      |              | Method Table Pointer |
+--------+              | X                    |
                        | Y                    |
                        +----------------------+
```

##### 数组布局

两种类型的数组具有相同的初始开销：一个对象头、一个方法表指针和一个长度字段（以及相关的填充）。
然而，元素的存储方式有显著不同：

*   **struct 数组（`Point[]`）**：`Point` 实例本身直接存储在数组的内存块中。
*   **class 引用数组（`PointRef[]`）**：数组存储的不是值，而是指向 `PointRef` 实例的引用（指针）。

虽然在检查器中数组对象本身的总大小可能看起来相同，但引用类型的实现除了数组本身之外，还需要另外十个在堆上分配的对象。
struct 数组则把所有数据存储在一次分配之中。

---

## 7. Exploring Access Patterns with Benchmarks

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-access-patterns-with-benchmarks-69958840/) · 1:00

### 总结

这一比较考察访问值类型（struct）数组与引用类型（class）数组时的性能影响。
它关注内存间接寻址和缓存局部性的影响，演示数据在内存中的物理布局如何在不同的数组大小和访问模式下影响迭代速度。

### 核心概念

- **内存间接寻址**：引用类型数组存储的是指针，需要额外跳转一次才能访问堆上分配的数据。
- **直接访问**：值类型数组以内联方式存储数据，使 CPU 可以直接在数组的内存块中访问字段。
- **缓存局部性**：连续的内存布局对缓存更友好，能实现更好的硬件预取。
- **访问模式**：struct 与 class 之间的性能差距受数组大小以及访问是顺序的还是随机的影响。
- **心智模型**：性能取决于数据布局如何与 CPU 的内存层次结构相互作用。

### 课程笔记

在为值类型和引用类型分别分配数组之后，这一比较考察消费这些数据时的性能特征。
主要区别在于 CPU 访问底层值的方式。
对于引用类型（`PointRef`）数组，数组包含指向堆上对象的指针，因此需要额外一层间接寻址。
相比之下，值类型（`Point`）数组把数据连续地存储在数组本身之中，从而可以直接访问。

下面的基准测试通过遍历数组并累加每个元素的 `X` 属性来比较这两种访问模式：

```csharp
[Benchmark]
public int ConsumeArrayOfPointRefs()
{
    int sum = 0;
    // Indirection
    foreach (var pr in _pointRefs)
        sum += pr.X;

    return sum;
}

[Benchmark]
public int ConsumeArrayOfPoints()
{
    int sum = 0;
    // Direct access
    foreach (var pr in _points)
        sum += pr.X;

    return sum;
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-access-patterns-with-benchmarks-69958840/?t=10)

间接寻址对性能的影响受多个因素左右，包括数组大小以及对象在内存中的物理布局。
虽然顺序分配可能会让对象彼此保持相对靠近，但任何对引用的打乱或数据量的增加都可能导致对缓存不友好的访问模式。
理解这一心智模型对于预测不同数据结构在各种工作负载下的表现至关重要，因为直接访问连续内存通常比涉及多层间接寻址的模式更能让硬件预取器有效工作。

---

## 8. Analyzing Access Patterns Benchmark Results

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-access-patterns-benchmark-results-69958841/) · 3:16

### 总结

本课分析比较值类型（struct）与引用类型（class）数组访问的基准测试结果。
虽然最初的顺序访问基准测试显示两者性能相当，但这往往是由于基准测试环境"干净"，对象是连续分配的。
通过引入随机化的访问模式（打乱），基准测试揭示了间接寻址的真实成本。
对于超出 L1 缓存的大型数据集，值类型的性能最多可比引用类型快 10 倍，因为它们避免了由分散的堆引用造成的反复主存查找。

### 核心概念

- **内存局部性**：存储在数组中的值类型在内存中是连续的，而引用类型是指向可能分散在各处的堆位置的指针。
- **硬件预取器**：现代 CPU 可以预测并预先加载顺序数据，这在简单的基准测试中可能会掩盖引用间接寻址的性能损失。
- **缓存层级**：小型数据集（例如 100 个元素）往往完全驻留在 L1 缓存中，因此两种类型的性能完全相同。
- **间接寻址开销**：沿着引用访问某个内存地址的性能成本，当数据在缓存中不"热"时，这一成本会变得显著。
- **基准测试的真实性**：打乱引用可以模拟真实应用的行为，因为在真实应用中对象很少会被分配在一个完全连续的块中。

### 课程笔记

最初的基准测试比较遍历 `Point`（struct）数组与 `PointRef`（class）数组。
在顺序访问模式下，对于 1,000,000 个元素，性能差异只有大约 60%，考虑到引用类型理论上的开销，这可能显得出奇地低。

```csharp
[Benchmark]
public int ConsumeArrayOfPointRefs()
{
    int sum = 0;
    // Indirection
    foreach (var pr in _pointRefs)
        sum += pr.X;

    return sum;
}

[Benchmark]
public int ConsumeArrayOfPoints()
{
    int sum = 0;
    // Direct access
    foreach (var pr in _points)
        sum += pr.X;

    return sum;
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-access-patterns-benchmark-results-69958841/?t=10)

之所以性能相当，是因为该基准测试在 `GlobalSetup` 方法中分配了所有实例。
在这种隔离的环境中，引用对象是一个接一个地分配的，这意味着它们在内存中彼此非常靠近。
这使 CPU 的硬件预取器能够优化访问模式，从而掩盖了间接寻址的成本。

```csharp
[GlobalSetup]
public void Setup()
{
    // Contiguous allocation: struct values sit inline in the array,
    // and ref objects land next to each other on the heap.
    _points = new Point[Size];
    _pointRefs = new PointRef[Size];
    for (int i = 0; i < Size; i++)
    {
        _points[i] = new Point(i, i);
        _pointRefs[i] = new PointRef(i, i);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-access-patterns-benchmark-results-69958841/?t=55)

在真实应用中，内存是碎片化的，引用很少是顺序排列的。
为了模拟这一点，引入了一个使用 `Shuffle` 参数的 `RandomizedAccess` 基准测试。
当 `Shuffle` 为 true 时，引用数组通过在一个大型对象池中随机位置采样来填充，从而让硬件预取器失效，并把间接寻址的成本与分配顺序隔离开来。

```csharp
[ShortRunJob]
[HideColumns("Error", "Gen1")]
public class RandomizedAccess
{
    // When true, sample refs at random positions from the pool
    // (cache-hostile). When false, take the first Size entries in
    // order — same pool, same allocation layout, but a prefetcher-
    // friendly access pattern. The contrast isolates access order
    // from allocation order.
    [Params(false, true)]
    public bool Shuffle { get; set; }

    [Params(100, 10_000, 1_000_000)]
    public int Size { get; set; }

    // Size of the backing pool. Large enough that sampled refs are
    // scattered across many cache lines / pages. Even when Size is
    // small, the working array points all over the pool — defeating
    // the hardware prefetcher.
    private const int PoolSize = 1_000_000;

    private Point[] _points = [];
    private PointRef[] _pointRefs = [];
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-access-patterns-benchmark-results-69958841/?t=70)

随机化基准测试的结果揭示了缓存局部性和间接寻址的影响：
1. **100 个元素**：访问 class 和 struct 之间没有差异，因为数据在 L1 缓存中是"热"的，而 L1 缓存极其快速。
2. **10,000 个元素**：struct 大约快 2 倍。数据集已经无法放进 L1，但 CPU 仍然能进行一定程度的预取。
3. **1,000,000 个元素（打乱）**：性能差异相当大，接近 10 倍。打乱迫使 CPU 为每个引用反复访问主存，而 struct 数组无论是否打乱都保持连续且对缓存友好。

```csharp
        _pointRefs = new PointRef[Size];
        if (Shuffle)
        {
            // Slice Size refs from random positions in the pool.
            var rng = new Random(42);
            for (int i = 0; i < Size; i++)
                _pointRefs[i] = pool[rng.Next(PoolSize)];
        }
        else
        {
            // Same pool, sequential slice — refs point at adjacent
            // pool entries, preserving allocation order.
            for (int i = 0; i < Size; i++)
                _pointRefs[i] = pool[i];
        }
    }
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-access-patterns-benchmark-results-69958841/?t=90)

```text
Shuffle = false                     Shuffle = true
_pointRefs                          _pointRefs
[0] [1] [2] [3] ...                 [0] [1] [2] [3] ...
 |   |   |   |                       |   |   |   |
 v   v   v   v                       |   |   |   +-------------+
pool (adjacent entries)              |   +---|-----------+     |
[0] [1] [2] [3] ...                  v       v           v     v
                                    pool (random positions, PoolSize)
                                    [..] ... [..] ... [..] ... [..]
```

这证实了，虽然简单的基准测试可能具有误导性，但间接寻址的成本是真实存在的。
当数据访问模式并非完全顺序时，访问值数组在理论上和实践中都更快。

---

## 9. Copy Semantics: Assignment

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/copy-semantics-assignment-69958842/) · 1:25

### 总结

本课解释值类型与引用类型在赋值时复制语义上的根本区别。
它演示了，虽然赋值总是复制一个值，但结果却不同：引用类型会创建指向同一内存实例的别名，而值类型会创建一份完全独立的数据副本。

### 核心概念

- 引用类型赋值（别名）。
- 值类型赋值（独立复制）。
- 共享实例与本地副本中的修改行为。
- 不同上下文中"值"的定义（数据还是指针）。

### 课程笔记

复制语义是 C# 中区分引用类型和值类型的一个关键方面。
下面的代码演示了赋值如何影响这两种类型的变量：

```csharp
public struct Point
{
    public int X { get; set; }
    public int Y { get; set; }
}

public class PointRef
{
    public int X { get; set; }
    public int Y { get; set; }
}

// Reference Type Assignment
PointRef p1 = new PointRef() { X = 1, Y = 2 };
PointRef p2 = p1; // p2 is an alias for p1

// Value Type Assignment
Point p3 = new Point() { X = 3, Y = 4 };
Point p4 = p3; // p4 is a separate copy of p3

// Mutating a shared instance
p2.X++;
p2.Y++;

// Mutating its own copy
p4.X++;
p4.Y++;
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/copy-semantics-assignment-69958842/?t=10)

当把一个引用类型变量赋值给另一个变量时（例如 `p2 = p1`），系统会创建一个别名。
`p1` 和 `p2` 都指向内存中完全相同的实例。
因此，对 `p2` 执行的任何修改，在通过 `p1` 访问该实例时都会立即可见，因为它们共享同一份底层数据。

相反，值类型的赋值（例如 `p4 = p3`）会对数据进行按位复制。
`p4` 成为一个独立的实例，包含与赋值那一刻 `p3` 相同的值。
因为它们彼此独立，修改 `p4` 对 `p3` 的状态没有任何影响。

```text
p2 = p1  (reference type)          p4 = p3  (value type)

p1 --+                             p3: X = 3, Y = 4
     +--> PointRef { X, Y }        p4: X = 3, Y = 4   (separate copy)
p2 --+
```

虽然常常把栈和堆当作心智模型（值类型位于栈上，引用类型位于堆上），但这是一种教学上的简化。
实际上，.NET 运行时可能会通过内联数据或使用 CPU 寄存器来优化存储。

需要记住的基本规则是：赋值运算符总是复制"值"。
区别在于这个值由什么构成：
- 对于 **struct**，值就是存储在对象中的实际数据。
- 对于 **class**，值是指向实例的引用（或指针）。

通过复制引用，赋值操作创建了两个指向同一内存位置的变量，这就是引用类型表现出别名行为的原因。

---

## 10. Copy Semantics: Parameter Passing

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/copy-semantics-parameter-passing-69958843/) · 3:27

### 总结

C# 的参数传递默认采用复制语义，其行为取决于类型是值类型还是引用类型。
值类型会复制底层数据，而引用类型会复制指向共享对象的引用。
开发者可以使用别名（ref、out、in 和 ref readonly）按引用传递参数，每一种在可变性以及能否接受右值（r-value）方面都有特定的规则。

### 核心概念

- 默认的参数传递使用复制语义。
- 值类型（struct）复制数据；引用类型（class）复制引用。
- 可变别名：`ref` 和 `out`（其中 `out` 必须由被调用方赋值）。
- 只读别名：`in` 和 `ref readonly`。
- `in` 参数允许右值（字面量或新实例），而 `ref` 和 `ref readonly` 的要求更严格。
- 按 `ref` 传递 class 时，被调用方可以把调用方的引用重新赋值为一个新实例。

### 课程笔记

#### 复制与别名

默认情况下，C# 在把参数传给方法时会复制参数的值。
这种复制的性质取决于类型：
- **值类型（struct）**：整个数据结构被复制。在方法内部修改参数只会影响本地副本。
- **引用类型（class）**：引用（内存地址）被复制。调用方和被调用方都指向同一个共享对象，因此在方法内部修改对象的属性会影响原始实例。

```csharp
var p1  = new Point    { X = 1, Y = 1 };
var p2 = new PointRef { X = 1, Y = 1 };

PassByValue(p1, p2);

Console.WriteLine(p1.X); // Output: 1
Console.WriteLine(p2.X); // Output: 2

static void PassByValue(Point v1, PointRef v2)
{
    // mutates a COPY of the struct
    v1.X++;
    // mutates the SHARED instance
    v2.X++;
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/copy-semantics-parameter-passing-69958843/?t=55)

#### 可变别名：ref 和 out

按引用传递会为原始变量创建一个别名。
C# 提供了两种主要的可变别名：
- **ref**：允许方法既读取又修改原始变量。对于 struct，这意味着可以修改真正的实例。对于 class，方法甚至可以把引用重新赋值，使其指向一个完全不同的实例。
- **out**：与 `ref` 类似，但被调用方必须在方法返回之前给参数赋值。

```csharp
var p  = new Point    { X = 1, Y = 1 };
var pr = new PointRef { X = 1, Y = 1 };

PassByRef(ref p, ref pr);

static void PassByRef(ref Point a1, ref PointRef a2)
{
    a1.X++;                               // mutates the caller's struct
    a2.X++;                               // mutates the shared object
    a2 = new PointRef { X = 0, Y = 0 };   // reassigns the caller's variable!
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/copy-semantics-parameter-passing-69958843/?t=100)

#### 只读别名：in 和 ref readonly

只读别名通过避免复制同时防止修改来优化性能，主要针对大型 struct。
- **in**：一种允许右值（例如 `new Point()`）的只读别名。如果传入的是右值，编译器可能会创建一个临时变量。
- **ref readonly**：一种使用约束更严格的只读别名。它通常要求左值（l-value，即一个变量），如果直接传入右值会产生警告。

`in` 和 `ref readonly` 都不允许被调用方重新给参数赋值或修改其字段（对于 struct 而言）。

```csharp
static void PassByRefVariants(in Point i, ref Point r, ref readonly Point rr)
{
    // i  = new Point();  // Error: 'in' - no reassignment
    r    = new Point();   // OK: 'ref' - caller's variable is rebound
    // rr = new Point();  // Error: 'ref readonly' - no reassignment

    // i.X++;    // Error: 'in' - field write blocked
    r.X++;       // OK: 'ref' - mutates caller
    // rr.X++;   // Error: 'ref readonly' - field write blocked
}

var p = new Point { X = 1, Y = 2 };
// 'in' accepts r-values; 'ref' and 'ref readonly' require variables (l-values)
PassByRefVariants(i: new Point(), r: ref p, rr: ref p);
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/copy-semantics-parameter-passing-69958843/?t=145)

注意，对可变 struct 使用 `in` 可能会导致创建防御性副本，这一话题将在后续模块中介绍。

#### 辅助类型

```csharp
public struct Point
{
    public int X { get; set; }
    public int Y { get; set; }
}

public class PointRef
{
    public int X { get; set; }
    public int Y { get; set; }
}
```

---

## 11. Equality Semantics

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/equality-semantics-69958844/) · 1:15

相等性是 C# 中区分值类型和引用类型的一个关键点。
理解运行时如何比较实例，对于实现正确的逻辑和保持性能至关重要。

### 核心概念

- **基于值的相等性**：struct 根据其字段和属性的值进行比较。如果内容一致，这些实例就被视为相等。
- **基于引用的相等性**：class 默认使用基于引用的相等性，这意味着只有当它们指向同一个内存地址时才相等。
- **运算符约束**：相等运算符（`==`）不会自动为 struct 定义；必须由开发者显式重载。
- **装箱与 ReferenceEquals**：对 struct 使用 `object.ReferenceEquals` 会导致装箱，从而创建新的对象实例，使该方法即使在把一个 struct 与它自身比较时也返回 `false`。
- **性能**：虽然 struct 提供了默认的相等性，但建议重写 `Equals` 和 `GetHashCode`，以避免默认的基于反射的实现带来的性能开销。

### 课程笔记

#### 值类型的相等性

struct 是值，这意味着它们的相等性由实例的内容决定。
如果有两个不同的 struct 实例，它们的属性值完全相同，`Equals` 方法会返回 `true`。
然而，与引用类型不同，编译器不会为 struct 生成相等运算符（`==`）的默认实现。
如果希望对 struct 使用 `==`，必须显式定义该运算符。

需要注意避免对 struct 使用 `object.ReferenceEquals`。
因为该方法接受 `object` 类型的参数，传入 struct 会导致它被装箱。
即使把同一个 struct 实例传给两个参数，每个参数也会被装箱成堆上的一个独立对象，该方法会返回 `false`。

```text
object.ReferenceEquals(s1, s1)

s1 --box--> [object #1 on heap] --+
                                  +--> different objects -> False
s1 --box--> [object #2 on heap] --+
```

当一个 struct 实例被修改时，它相对于其他实例的相等状态会发生变化。
如果两个 struct 之前相等，而其中一个被修改了，那么 `Equals` 方法随后就会返回 `false`。

#### 引用类型的相等性

class 默认使用引用语义。
即使两个独立的 class 实例包含完全相同的数据，它们也不被视为相等，因为它们位于内存中的不同位置。
在 class 的默认状态下，`Equals` 方法、相等运算符（`==`）和 `object.ReferenceEquals` 执行的都是同一种检查：验证变量是否指向同一个实例。

如果把一个引用变量赋值给另一个变量（例如 `r2 = r1`），它们都会指向内存中的同一个实例。
此时，相等运算符和 `ReferenceEquals` 都会返回 `true`。

```csharp
var s1 = new Point { X = 1, Y = 1 };
var s2 = new Point { X = 1, Y = 1 };

Console.WriteLine(s1.Equals(s2)); // True
// X operator == is not defined by default
// Console.WriteLine(s1 == s2);
Console.WriteLine(object.ReferenceEquals(s1, s2)); // False
Console.WriteLine(object.ReferenceEquals(s1, s1)); // False
s1.X++;

Console.WriteLine(s1.Equals(s2)); // False

public struct Point
{
    // Override Equals/GetHashCode
    // for performance reasons
    public int X { get; set; }
    public int Y { get; set; }
    // Override operator== if needed
}

var r1 = new PointRef { X = 1, Y = 1 };
var r2 = new PointRef { X = 1, Y = 1 };

Console.WriteLine(r1.Equals(r2)); // False
Console.WriteLine(r1 == r2); // False
Console.WriteLine(object.ReferenceEquals(r1, r2)); // False
r2 = r1;
Console.WriteLine(r1 == r2); // True
Console.WriteLine(object.ReferenceEquals(r1, r2)); // True

public class PointRef
{
    // Overriding Equals, GetHashCode, and operator==
    // can change the default behavior to have
    // value-based semantics
    public int X { get; set; }
    public int Y { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/equality-semantics-69958844/?t=10)

虽然 class 的默认行为是引用相等，但可以通过重写 `Equals`、`GetHashCode` 和相等运算符来实现基于值的语义，从而改变这一行为。
课程后续的主题将探讨 `records`（它为引用类型默认提供基于值的相等性），以及在 struct 中不重写相等性成员所带来的具体性能影响。

---

## 12. Summary

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958845/) · 1:48

### 总结

值类型与引用类型之间的根本区别由三个语义视角来定义（存储模型、复制行为和相等性），而不是常见误解中的栈分配与堆分配之分。
值类型直接表示数据并按值复制，而引用类型表示对数据的间接引用并按引用复制。
数据的实际物理位置（栈或堆）是运行时的实现细节，取决于执行上下文，例如是否位于 async 方法中，或者现代编译器的优化。

### 核心概念

*   **语义模型**：值类型以内联方式存储数据；引用类型存储对数据的引用（间接寻址）。
*   **复制语义**：对值类型赋值会复制数据；对引用类型赋值会复制指向同一实例的引用。
*   **相等性语义**：值类型默认使用基于值的相等性；引用类型默认使用引用相等性。
*   **实现细节**：栈分配与堆分配并不是固定规则，而是取决于运行时上下文（例如迭代器、async 方法或逃逸分析）。

### 课程笔记

#### 类型差异的三个视角

一个常见的误解是，值类型与引用类型的主要区别在于值类型位于栈上、引用类型位于堆上。
这是一个实现细节，而不是根本性的定义。
相反，应当通过三个具体视角来看待这些差异：语义模型、复制语义和相等性语义。

##### 1. 语义模型（存储）

值类型直接表示数据，并以内联方式存储。
相比之下，引用类型表示对数据的引用，提供了一层间接寻址。
这一点从 `struct` 和 `class` 等类型的定义方式以及它们在内存中的布局方式就可以看出来。

```csharp
public struct Point
{
    public int X { get; set; }
    public int Y { get; set; }
}

public class PointRef
{
    public int X { get; set; }
    public int Y { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958845/?t=21)

##### 2. 复制语义

复制语义定义了把变量赋值给另一个变量或传给方法时会发生什么。
对于值类型，复制会复制实际数据。
对于引用类型，复制会复制引用，从而得到两个指向内存中同一实例的变量。

```csharp
static void Step1_PassByValue()
{
    var p1  = new Point    { X = 1, Y = 1 };
    var p2 = new PointRef { X = 1, Y = 1 };

    PassByValue(p1, p2);

    Console.WriteLine(p1.X); // Outputs 1 (original unchanged)
    Console.WriteLine(p2.X); // Outputs 2 (shared instance mutated)

    static void PassByValue(Point v1, PointRef v2)
    {
        // mutates a COPY of the struct
        v1.X++;
        // mutates the SHARED instance
        v2.X++;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958845/?t=30)

按引用（`ref`）传递变量时也有所不同。
在两种情况下它都会创建一个别名，但对于引用类型，使用 `ref` 可以让方法改变原始变量所指向的位置。

```csharp
static void Step2_PassByRef()
{
    var p  = new Point    { X = 1, Y = 1 };
    var pr = new PointRef { X = 1, Y = 1 };

    PassByRef(ref p, ref pr);

    static void PassByRef(ref Point a1, ref PointRef a2)
    {
        a1.X++;                               // mutates the caller's struct
        a2.X++;                               // mutates the shared object
        a2 = new PointRef { X = 0, Y = 0 };   // reassigns the caller's variable!
    }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958845/?t=48)

##### 3. 相等性语义

默认情况下，值类型在相等性上使用值语义。
引用类型使用引用相等性，只有当两个变量指向完全相同的实例时，它们才被视为相等。

```csharp
static void Step7_StructEquality()
{
    var s1 = new Point { X = 1, Y = 1 };
    var s2 = new Point { X = 1, Y = 1 };

    Console.WriteLine(s1.Equals(s2)); // True (Value-based)
    Console.WriteLine(object.ReferenceEquals(s1, s2)); // False
}

static void Step8_ClassEquality()
{
    var r1 = new PointRef { X = 1, Y = 1 };
    var r2 = new PointRef { X = 1, Y = 1 };

    Console.WriteLine(r1.Equals(r2)); // False (Referential-based)
    Console.WriteLine(r1 == r2); // False
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958845/?t=64)

#### 栈与堆只是实现细节

分配完全取决于上下文和运行时。
认为值类型总是在栈上、引用类型总是在堆上是不正确的：
*   **位于堆上的值类型**：如果值类型的局部变量属于迭代器块或 `async` 方法的一部分，它们可以存储在堆上。
*   **位于栈上的引用类型**：现代运行时可以执行逃逸分析，如果引用类型没有离开局部作用域，就可以把它们分配在栈上。

虽然数据的物理位置可能会影响性能，但它并不是这两种类型体系之间的根本区别。

---

## 运行 Demo

```bash
cd src/mastering-csharp/03-value-types-vs-reference-types/MasteringCSharp.ValueVsReference.Demos
dotnet run -c Release              # all three sections
dotnet run -c Release -- storage   # or: copy, equality
```

```bash
cd src/mastering-csharp/03-value-types-vs-reference-types/MasteringCSharp.ValueVsReference.Benchmarks
dotnet run -c Release -- --list flat
dotnet run -c Release -- --filter *ArrayAllocationBenchmark*
dotnet run -c Release -- --filter *ArrayAccessBenchmark*
dotnet run -c Release -- --filter *RandomizedAccess*
```

基准测试需要 Release 模式，并且要运行好几分钟，因为 `RandomizedAccess` 会为每种参数组合构建一个包含 1,000,000 个对象的对象池。
