# Value Types vs. Reference Types

> Course: [Mastering: C#](https://dometrain.com/course/mastering-csharp/) · Chapter 3
> 12 lessons · ~21:27
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
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

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/module-overview-69958834/) · 1:24

### Summary

The C# type system serves as the foundation for application semantics, influencing identity, mutability, and equality regardless of whether an object-oriented or functional approach is used.
Beyond logic, types significantly impact performance through memory allocation patterns and object layout.
Mastery of the type system is essential for utilizing modern language features like records and pattern matching, which are built upon fundamental concepts of value-based equality and type-based querying.

### Key concepts

- **Application Semantics**: Types define how data flows and how the system behaves across different programming paradigms.
- **Performance Impact**: The choice between reference and value types affects allocations, copying semantics, and object layout.
- **Domain Modeling**: Records, tuples, and union types allow for precise modeling of the domain, leading to clearer APIs.
- **Modern Language Features**: Features like records and pattern matching are direct extensions of core type system principles.
- **Value-based Equality**: The fundamental concept underlying the behavior of record types.

### Lesson notes

The type system is the cornerstone of any C# application, defining the fundamental semantics whether the architecture is object-oriented (utilizing classes and structs) or functional (utilizing records and pure functions).
Types govern critical aspects of software behavior, including identity, mutability, and equality.
These definitions determine how data flows through a system and whether that system behaves correctly.

Beyond logical behavior, types have a direct impact on application performance.
The distinction between reference types and value types determines memory allocation patterns, copying semantics, and object layout.
Precise domain modeling is achieved through the use of records, tuples, and union types, which clarify API intent and reduce ambiguity.
Poorly defined types lead to unclear APIs, whereas well-defined types make developer intent obvious.

Understanding the type system is a prerequisite for mastering modern C# features.
For example, records are not a "magical" abstraction; they are reference types that implement value-based equality and controlled immutability.
If the concept of value-based equality is understood, the behavior of records becomes intuitive.
Similarly, pattern matching is not merely a syntax feature but a way to query the type system regarding a value's type, nullability, and member state.
When viewed as a method for asking questions about the type system, pattern matching becomes easier to reason about, read, and implement.

---

## 2. Overview

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958835/) · 0:56

### Summary

This lesson introduces the fundamental distinctions between value types and reference types in C#, moving beyond the simple stack vs. heap distinction to explore storage, copy, and equality semantics.
It categorizes C# types into reference, value, and pointer types, establishing a framework for understanding how these classifications affect memory allocation, performance, and application logic.

### Key concepts

- **Type Categories**: C# types are divided into reference types, value types, and pointer types.
- **Reference Types**: Includes classes, records, arrays, objects, and delegates.
- **Value Types**: Includes structs, record structs, enums, and primitive types (which are aliases for structs).
- **Storage Semantics**: Concerns where the data lives in memory and the physical layout of the object or structure.
- **Copy Semantics**: Defines what is copied during an assignment operation (the reference vs. the actual data).
- **Equality Semantics**: Determines the default behavior for comparing instances (identity vs. value-based equality).

### Lesson notes

C# types are categorized into three primary groups: reference types, value types, and pointer types.
Pointer types are primarily utilized in unmanaged contexts and are not as widely used in standard application development as the other two categories.

#### Type Classification

Types are classified based on how they are handled by the runtime and stored in memory:
- **Reference Types**: These consist of classes, records, arrays, objects, and delegates. Variables of these types store a reference to the data rather than the data itself.
- **Value Types**: These consist of structs, record structs, enums, and primitive types. In C#, primitive types (like `int` or `bool`) are implemented as aliases for structs. Variables of these types contain the actual data.

#### The Three Pillars of Comparison

To fully understand the differences between these types, they must be analyzed from three distinct angles:

1. **Storage Semantics**: This refers to where the data lives in memory. This includes the allocation patterns for types—for instance, how an array of structs results in a single contiguous allocation, whereas an array of classes involves multiple allocations (one for the array itself and one for each instance). This also covers object layout, including object headers and reference slots.

2. **Copy Semantics**: This defines the behavior of the assignment operator. For reference types, an assignment copies the reference, meaning both the original and the new variable point to the same instance in memory. For value types, the assignment copies the actual content, resulting in two independent copies of the data.

3. **Equality Semantics**: This defines how equality is evaluated by default. Reference types typically use reference identity (checking if two variables point to the same memory address). Value types use value equality, checking if the underlying data is identical. This also encompasses how these defaults can be overridden and the performance implications of default equality checks (such as the overhead of reflection in certain struct equality scenarios).

These distinctions have significant impacts on performance, particularly regarding iteration costs and the "shuffle effect."
The memory layout of value types allows for more efficient cache usage and hardware prefetching, whereas the indirection and potential memory fragmentation of reference types can lead to higher dereference costs.

---

## 3. Storage Model: Inline vs. Indirection

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/storage-model-inline-vs-indirection-69958836/) · 2:49

### Summary

The storage model of C# types is defined by the distinction between inline storage and indirection.
Value types (structs) are stored inline, meaning their data is contained directly within the variable's memory slot or the parent object.
Reference types (classes) utilize indirection, where the variable holds a reference (pointer) to an object allocated on the managed heap.
While value types are often associated with the stack and reference types with the heap, the actual storage location is determined by the context—such as whether a value type is a field within a class or if a reference type is subject to stack allocation optimizations in modern runtimes.

### Key concepts

- **Inline Storage**: Value types store their data directly where the variable is declared.
- **Indirection**: Reference types store a pointer to data located elsewhere (typically the managed heap).
- **Context-Dependent Allocation**: The storage location (stack vs. heap) is determined by the context of the declaration, not just the type itself.
- **Array Layout**: Arrays of value types store data contiguously, while arrays of reference types store a collection of references to separate heap objects.

### Lesson notes

The fundamental difference between value types and reference types is their storage semantics.
To illustrate this, consider a `Point` struct and a `PointRef` class, both containing `X` and `Y` integer properties.

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

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/storage-model-inline-vs-indirection-69958836/?t=10)

In this scenario, if we pause execution after initializing `c`, the memory layout reveals distinct behaviors:
- `p1` (a value type) is stored directly on the stack.
- `p2` (a reference type) results in a reference on the stack pointing to an instance on the managed heap.
- `c` (a reference type) is also a reference on the stack pointing to a heap-allocated object.

Inside the instance of `c`, the storage of its fields depends on their types.
The `P1` property (a `Point` struct) is stored **inline** within the memory allocated for the `c` object.
Conversely, the `P2` property (a `PointRef` class) stores a reference that points to the same `PointRef` instance already referenced by the stack.

It is important to note that the type itself does not strictly determine the storage location; the context does.
Value types are not always on the stack; they can be embedded within heap-allocated objects (like `P1` inside `c`).
Similarly, modern runtimes may perform stack allocation for reference types in specific scenarios.

#### Array Allocation and Performance

The storage model significantly impacts how arrays are allocated in memory.
An array of reference types stores references to objects.
If you allocate an array of 10 classes, the runtime performs 11 allocations: one for the array itself and ten for the individual objects.
An array of value types, however, stores the data inline within the array's memory block, requiring only a single allocation.

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

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/storage-model-inline-vs-indirection-69958836/?t=115)

While these benchmarks highlight the structural differences in allocation, they should be used to build a mental model of memory layout rather than as a sole basis for performance optimization.
Performance is a complex topic requiring end-to-end analysis.

---

## 4. Exploring Storage

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-storage-69958837/) · 1:03

### Summary

This lesson explores the storage models of value types and reference types by analyzing array allocations.
It demonstrates that reference type arrays store pointers to heap-allocated objects, requiring multiple allocations, whereas value type arrays store data inline within the array itself, requiring only a single allocation.
This distinction is a key factor in understanding memory layout and allocation behavior in C#.

### Key concepts

- **Reference Type Storage**: Arrays of classes store references to instances allocated on the managed heap.
- **Value Type Storage**: Arrays of structs store data inline within the array's memory block.
- **Allocation Overhead**: Creating an array of N reference types results in N+1 allocations, while an array of N value types results in a single allocation.
- **Mental Model**: Benchmarking these allocations helps visualize the difference in memory layout between structs and classes.

### Lesson notes

The storage model of C# types can be understood by comparing how arrays of structs and classes are allocated.
This comparison serves as a mental model for understanding memory layout rather than a direct performance comparison, as performance is a complex topic involving end-to-end analysis.

Consider the following definitions for a coordinate point:

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

When creating an array of reference types (`PointRef`), the array stores references to instances allocated on the managed heap.
For an array of size 10, this results in 11 total allocations: one for the array itself and ten for the individual objects.
In contrast, an array of value types (`Point`) stores the data inline.

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

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-storage-69958837/?t=10)

In the `CreateArrayOfPointRefs` method, the runtime performs an allocation for each element in the array.
However, in the `CreateArrayOfPoints` method, the `Point` struct values are stored inline within the array's memory.
This results in a single allocation for the entire array, regardless of the number of elements.
This storage model provides better data locality and reduces the pressure on the managed heap.

---

## 5. Analyzing Storage Model with Benchmarks

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-storage-model-with-benchmarks-69958838/) · 1:58

### Summary

This lesson explores the memory storage models of value types versus reference types by analyzing benchmark results.
It quantifies the allocation overhead of heap-allocated objects, including headers and padding, and compares the memory footprint of an array of structs against an array of classes to illustrate the efficiency of value types for small data structures.

### Key concepts

* Memory overhead of heap-allocated objects (16-byte header).
* Array structure overhead, including length fields and padding.
* Reference size (8 bytes on 64-bit systems) vs. inline value storage.
* Memory allocation analysis using BenchmarkDotNet.
* Impact of object layout on application performance.

### Lesson notes

To understand the practical implications of the storage models for value types and reference types, we utilize a benchmark that compares the allocation of arrays containing `Point` (a struct) and `PointRef` (a class).
The benchmark is executed in Release mode to ensure accurate performance and memory metrics.

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

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-storage-model-with-benchmarks-69958838/?t=10)

#### Memory Analysis of Reference Types

When allocating an array of classes (`PointRef[]`), the memory consumption is significantly higher due to multiple layers of overhead:
1. **Object Header**: Every heap-allocated object has a 16-byte overhead.
2. **Array Structure**: An array includes a length field and padding (typically 4 bytes each).
3. **References**: Each element in the array is a reference (8 bytes on 64-bit systems) pointing to an object elsewhere on the heap.
4. **Instance Overhead**: Each `PointRef` instance on the heap also incurs the 16-byte object header in addition to its two 4-byte integer fields (`X` and `Y`), totaling 24 bytes per instance.

For an array of 10 `PointRef` instances, the array itself consumes approximately 108 bytes.
The 10 instances of `PointRef` consume 240 bytes (10 * 24).
Theoretically, this results in approximately 344 bytes of allocation, though benchmarks may show slight variations (e.g., 345 bytes) due to rounding errors.

#### Memory Analysis of Value Types

In contrast, an array of structs (`Point[]`) is much more efficient.
Because value types are stored inline within the array:
1. There are no separate heap allocations for each element.
2. There are no 8-byte references stored in the array; the data itself resides directly in the array's memory block.
3. The total size is simply the array overhead (108 bytes for a size of 10) with no extra per-element heap overhead.

This comparison demonstrates that for small data structures, the overhead of using classes can be substantial.
While this may not always be a performance bottleneck, benchmarking is essential to determine the impact on specific applications.
Tools like the "Object Layout Inspector" can be used to further investigate how the Common Language Runtime (CLR) lays out objects in memory.

---

## 6. Inspecting Type Layout (part 2)

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/inspecting-type-layout-part-2-69958839/) · 1:06

### Summary

This lesson demonstrates how to use the ObjectLayoutInspector NuGet package to visualize the memory layout of C# types at runtime.
By comparing a struct and a class, it highlights the overhead of reference types—specifically the object header and method table pointer—and illustrates how arrays of value types store data inline whereas arrays of reference types store pointers to separate heap allocations.

### Key concepts

*   **ObjectLayoutInspector**: A NuGet package used to inspect the physical memory layout of objects and types at runtime.
*   **TypeLayout.PrintLayout<T>()**: An API call used to print the memory layout of a specific type.
*   **ArrayLayout.PrintLayout(instance)**: An API call used to print the memory layout of a specific array instance, allowing for inspection of element storage.
*   **Reference Type Overhead**: Every class instance includes an Object Header and a Method Table Pointer used by the CLR.
*   **Value Type Efficiency**: Structs have zero overhead and store their fields directly without additional metadata pointers.
*   **Array Storage Models**: Struct arrays store elements contiguously in memory; class arrays store a contiguous block of references to objects located elsewhere on the heap.

### Lesson notes

To use the object layout inspector, you must add the `ObjectLayoutInspector` NuGet package to your project.
This tool provides a simple API to visualize how the Common Language Runtime (CLR) organizes data in memory.

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

The following implementation compares a struct (`Point`) and a class (`PointRef`) along with their respective array representations.
You use `TypeLayout.PrintLayout` for the struct and class types, and `ArrayLayout.PrintLayout` for array instances to see the actual layout for a specific number of elements.

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

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/inspecting-type-layout-part-2-69958839/?t=10)

#### Runtime Layout Observations

##### Struct vs. Class Layout

When inspecting the `Point` struct at runtime, it shows zero overhead; the fields are packed directly.
In contrast, the `PointRef` class includes two extra fields that the CLR uses for bookkeeping purposes:
1.  **Object Header**: Used for synchronization and other internal metadata.
2.  **Method Table Pointer**: Points to the type's method table.

Following these metadata fields, the class contains the same two integer fields as the struct.

##### Array Layouts

Arrays of both types share the same initial overhead: an object header, a method table pointer, and a length field (with associated padding).
However, the storage of elements differs significantly:

*   **Array of Structs (`Point[]`)**: The `Point` instances themselves are stored directly within the array's memory block.
*   **Array of Class References (`PointRef[]`)**: Instead of storing the values, the array stores references (pointers) to the `PointRef` instances.

While the total size of the array objects themselves may appear identical in the inspector, the reference type implementation requires the array itself plus ten additional heap-allocated objects.
The struct array stores all data within a single allocation.

---

## 7. Exploring Access Patterns with Benchmarks

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-access-patterns-with-benchmarks-69958840/) · 1:00

### Summary

This comparison examines the performance implications of accessing arrays of value types (structs) versus reference types (classes).
It focuses on the impact of memory indirection and cache locality, demonstrating how the physical layout of data in memory affects iteration speed across different array sizes and access patterns.

### Key concepts

- **Memory Indirection**: Reference type arrays store pointers, requiring an extra hop to access heap-allocated data.
- **Direct Access**: Value type arrays store data inline, allowing the CPU to access fields directly within the array's memory block.
- **Cache Locality**: Contiguous memory layouts are more cache-friendly, enabling better hardware prefetching.
- **Access Patterns**: The performance gap between structs and classes is influenced by array size and whether access is sequential or randomized.
- **Mental Model**: Performance is determined by how the data layout interacts with the CPU's memory hierarchy.

### Lesson notes

Following the allocation of arrays for both value types and reference types, the comparison examines the performance characteristics of consuming that data.
The primary distinction lies in how the CPU accesses the underlying values.
For an array of reference types (`PointRef`), the array contains pointers to objects on the heap, necessitating an extra layer of indirection.
In contrast, an array of value types (`Point`) stores the data contiguously within the array itself, allowing for direct access.

The following benchmarks compare these two access patterns by iterating through the arrays and summing the `X` property of each element:

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

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-access-patterns-with-benchmarks-69958840/?t=10)

The performance impact of indirection is influenced by several factors, including array size and the physical layout of the objects in memory.
While sequential allocation may keep objects relatively close together, any shuffling of references or increase in data volume can lead to cache-hostile access patterns.
Understanding this mental model is crucial for predicting how different data structures will perform under various workloads, as direct access to contiguous memory typically allows the hardware prefetcher to operate more effectively than patterns involving multiple layers of indirection.

---

## 8. Analyzing Access Patterns Benchmark Results

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-access-patterns-benchmark-results-69958841/) · 3:16

### Summary

This lesson analyzes benchmark results comparing array access for value types (structs) and reference types (classes).
While initial sequential benchmarks show comparable performance, this is often due to the "clean" nature of benchmarks where objects are allocated contiguously.
By introducing a randomized access pattern (shuffling), the benchmark reveals the true cost of indirection.
For large datasets that exceed the L1 cache, value types can perform up to 10 times faster than reference types because they avoid repeated main memory lookups caused by scattered heap references.

### Key concepts

- **Memory Locality**: Value types stored in an array are contiguous in memory, whereas reference types are pointers to potentially scattered heap locations.
- **Hardware Prefetcher**: Modern CPUs can predict and pre-load sequential data, which can mask the performance penalty of reference indirection in simple benchmarks.
- **Cache Levels**: Small datasets (e.g., 100 items) often reside entirely in the L1 cache, resulting in identical performance for both types.
- **Indirection Overhead**: The performance cost of following a reference to a memory address, which becomes significant when data is not "hot" in the cache.
- **Benchmark Realism**: Shuffling references simulates real-world application behavior where objects are rarely allocated in a perfectly contiguous block.

### Lesson notes

The initial benchmark compares iterating over an array of `Point` (struct) versus `PointRef` (class).
In a sequential access pattern, the performance difference for 1,000,000 items is only about 60%, which might seem surprisingly low given the theoretical overhead of reference types.

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

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-access-patterns-benchmark-results-69958841/?t=10)

This comparable performance occurs because the benchmark allocates all instances in a `GlobalSetup` method.
In this isolated environment, the reference objects are allocated one after another, meaning they sit very close to each other in memory.
This allows the CPU's hardware prefetcher to optimize the access pattern, masking the cost of indirection.

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

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-access-patterns-benchmark-results-69958841/?t=55)

In a real-world application, memory is fragmented, and references are rarely sequential.
To simulate this, a `RandomizedAccess` benchmark is introduced using a `Shuffle` parameter.
When `Shuffle` is true, the array of references is populated by sampling from a large pool of objects at random positions, defeating the hardware prefetcher and isolating the cost of indirection from the allocation order.

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

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-access-patterns-benchmark-results-69958841/?t=70)

The results of the randomized benchmark reveal the impact of cache locality and indirection:
1. **100 items**: There are no differences between accessing classes and structs because the data is "hot" in the L1 cache, which is extremely fast.
2. **10,000 items**: Structs are approximately 2x faster. The data set no longer fits in L1, but the CPU still manages some prefetching.
3. **1,000,000 items (Shuffled)**: The performance difference is substantial, reaching almost 10x. Shuffling forces the CPU to hit main memory repeatedly for each reference, whereas the struct array remains contiguous and cache-friendly regardless of the shuffle.

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

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-access-patterns-benchmark-results-69958841/?t=90)

This confirms that while simple benchmarks can be misleading, the cost of indirection is real.
Accessing an array of values is theoretically and practically faster when the data access pattern is not perfectly sequential.

---

## 9. Copy Semantics: Assignment

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/copy-semantics-assignment-69958842/) · 1:25

### Summary

This lesson explains the fundamental differences in copy semantics between value types and reference types during assignment.
It demonstrates that while assignment always copies a value, the result differs: reference types create an alias to the same memory instance, whereas value types create an entirely independent copy of the data.

### Key concepts

- Reference type assignment (aliasing).
- Value type assignment (independent copying).
- Mutation behavior in shared instances vs. local copies.
- The definition of "value" in different contexts (data vs. pointer).

### Lesson notes

Copy semantics is a critical aspect that differentiates reference types from value types in C#.
The following code demonstrates how assignment affects variables of both types:

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

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/copy-semantics-assignment-69958842/?t=10)

When a reference type variable is assigned to another (e.g., `p2 = p1`), the system creates an alias.
Both `p1` and `p2` point to the exact same instance in memory.
Consequently, any mutation performed on `p2` is immediately visible when accessing the instance through `p1` because they share the same underlying data.

In contrast, the assignment of value types (e.g., `p4 = p3`) results in a bitwise copy of the data.
`p4` becomes a separate instance containing the same values as `p3` at the moment of assignment.
Because they are independent, mutating `p4` has no effect on the state of `p3`.

While it is common to use the stack and heap as a mental model—where value types live on the stack and reference types on the heap—this is an educational simplification.
In reality, the .NET runtime may optimize storage by inlining data or using CPU registers.

The fundamental rule to remember is that the assignment operator always copies the "value."
The distinction lies in what constitutes that value:
- For a **struct**, the value is the actual data stored within the object.
- For a **class**, the value is the reference (or pointer) to the instance.

By copying the reference, the assignment operation creates two variables that point to the same location in memory, which is why reference types exhibit aliasing behavior.

---

## 10. Copy Semantics: Parameter Passing

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/copy-semantics-parameter-passing-69958843/) · 3:27

### Summary

C# parameter passing defaults to copy semantics, where the behavior depends on whether the type is a value type or a reference type.
While value types result in a copy of the underlying data, reference types result in a copy of the reference to a shared object.
Developers can use aliases—ref, out, in, and ref readonly—to pass parameters by reference, each with specific rules regarding mutability and the ability to accept r-values.

### Key concepts

- Default parameter passing uses copy semantics.
- Value types (structs) copy the data; reference types (classes) copy the reference.
- Mutable aliases: `ref` and `out` (where `out` must be assigned by the callee).
- Read-only aliases: `in` and `ref readonly`.
- `in` parameters allow r-values (literals or new instances), whereas `ref` and `ref readonly` have stricter requirements.
- Passing a class by `ref` allows the callee to reassign the caller's reference to a new instance.

### Lesson notes

#### Copy vs. Alias

By default, C# copies the value of a parameter when passing it to a method.
The nature of this copy depends on the type:
- **Value Types (structs):** The entire data structure is copied. Mutating the parameter inside the method only affects the local copy.
- **Reference Types (classes):** The reference (the memory address) is copied. Both the caller and the callee point to the same shared object, so mutating the object's properties inside the method affects the original instance.

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

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/copy-semantics-parameter-passing-69958843/?t=55)

#### Mutable Aliases: ref and out

Passing by reference creates an alias to the original variable.
C# provides two primary mutable aliases:
- **ref:** Allows the method to both read and mutate the original variable. For structs, this means the actual instance can be modified. For classes, the method can even reassign the reference to point to a completely different instance.
- **out:** Similar to `ref`, but the callee is required to assign a value to the parameter before the method returns.

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

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/copy-semantics-parameter-passing-69958843/?t=100)

#### Read-only Aliases: in and ref readonly

Read-only aliases optimize performance, primarily for large structs, by avoiding copies while preventing mutation. 
- **in:** A read-only alias that allows r-values (e.g., `new Point()`). If an r-value is passed, the compiler may create a temporary variable.
- **ref readonly:** A read-only alias with stricter usage constraints. It typically requires an l-value (a variable) and will produce a warning if an r-value is passed directly.

Neither `in` nor `ref readonly` allows the callee to reassign the parameter or mutate its fields (for structs).

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

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/copy-semantics-parameter-passing-69958843/?t=145)

Note that using `in` with mutable structs can lead to the creation of defensive copies, a topic covered in later modules.

#### Supporting Types

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

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/equality-semantics-69958844/) · 1:15

Equality in C# is a key differentiator between value types and reference types.
Understanding how the runtime compares instances is essential for implementing correct logic and maintaining performance.

### Key concepts

- **Value-Based Equality**: Structs are compared based on the values of their fields and properties. If the content matches, the instances are considered equal.
- **Reference-Based Equality**: Classes use reference-based equality by default, meaning they are only equal if they point to the same memory address.
- **Operator Constraints**: The equality operator (`==`) is not automatically defined for structs; it must be explicitly overloaded by the developer.
- **Boxing and ReferenceEquals**: Using `object.ReferenceEquals` on structs leads to boxing, which creates new object instances and causes the method to return `false` even when comparing a struct to itself.
- **Performance**: While structs provide default equality, overriding `Equals` and `GetHashCode` is recommended to avoid the performance overhead of the default reflection-based implementation.

### Lesson notes

#### Value Type Equality

Structs are values, which means their equality is determined by the content of the instance.
If you have two different instances of a struct with identical property values, the `Equals` method will return `true`.
However, unlike reference types, the compiler does not emit a default implementation for the equality operator (`==`) for structs.
If you wish to use `==` with a struct, you must define the operator explicitly.

It is important to avoid using `object.ReferenceEquals` with structs.
Because this method accepts arguments of type `object`, passing a struct causes it to be boxed.
Even if you pass the same struct instance into both parameters, each is boxed into a separate object on the heap, and the method will return `false`.

When a struct instance is mutated, its equality status changes relative to other instances.
If two structs were previously equal and one is modified, the `Equals` method will then return `false`.

#### Reference Type Equality

Classes utilize reference semantics by default.
Even if two separate class instances contain the exact same data, they are not considered equal because they reside at different locations in memory.
In the default state for classes, the `Equals` method, the equality operator (`==`), and `object.ReferenceEquals` all perform the same check: they verify if the variables point to the same instance.

If you assign one reference variable to another (e.g., `r2 = r1`), they both point to the same instance in memory.
At this point, both the equality operator and `ReferenceEquals` will return `true`.

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

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/equality-semantics-69958844/?t=10)

While the default behavior for classes is reference equality, this can be changed by overriding `Equals`, `GetHashCode`, and the equality operators to implement value-based semantics.
Later topics in the course will explore `records`, which provide value-based equality by default for reference types, as well as the specific performance impacts of not overriding equality members in structs.

---

## 12. Summary

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958845/) · 1:48

### Summary

The fundamental difference between value types and reference types is defined by three semantic perspectives—storage model, copy behavior, and equality—rather than the common misconception of stack versus heap allocation.
While value types represent data directly and are copied by value, reference types represent an indirection to data and are copied by reference.
The actual physical location of data (stack or heap) is an implementation detail of the runtime that depends on the execution context, such as within async methods or modern compiler optimizations.

### Key concepts

*   **Semantic Model**: Value types store data inline; reference types store a reference to the data (indirection).
*   **Copy Semantics**: Assigning a value type duplicates the data; assigning a reference type duplicates the reference to the same instance.
*   **Equality Semantics**: Value types use value-based equality by default; reference types use referential equality by default.
*   **Implementation Details**: Stack vs. heap allocation is not a fixed rule but depends on the runtime context (e.g., iterators, async methods, or escape analysis).

### Lesson notes

#### The Three Perspectives of Type Differences

It is a common misconception that the primary difference between value types and reference types is that value types live on the stack and reference types live on the heap.
This is an implementation detail rather than a fundamental definition.
Instead, the differences should be viewed through three specific perspectives: the semantic model, copy semantics, and equality semantics.

##### 1. Semantic Model (Storage)

Value types represent data directly and are stored inline.
In contrast, reference types represent a reference to data, providing one level of indirection.
This is evident in how types like `struct` and `class` are defined and laid out in memory.

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

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958845/?t=21)

##### 2. Copy Semantics

Copy semantics define what happens when a variable is assigned to another or passed to a method.
For value types, copying duplicates the actual data.
For reference types, copying duplicates the reference, resulting in two variables pointing to the same instance in memory.

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

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958845/?t=30)

Passing variables by reference (`ref`) also differs.
In both cases, it creates an alias, but for reference types, using `ref` allows a method to change where the original variable points.

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

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958845/?t=48)

##### 3. Equality Semantics

By default, value types use value semantics for equality.
Reference types use referential equality, where two variables are only considered equal if they point to the exact same instance.

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

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958845/?t=64)

#### Stack vs. Heap as an Implementation Detail

Allocation depends entirely on the context and the runtime.
It is incorrect to assume value types are always on the stack and reference types are always on the heap:
*   **Value types on the heap**: Local variables of a value type can be stored on the heap if they are part of an iterator block or an `async` method.
*   **Reference types on the stack**: Modern runtimes can perform escape analysis and allocate reference types on the stack if they do not leave the local scope.

While the physical location of data can impact performance, it is not the fundamental differentiator between the two type systems.

---

## Running the demo

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

The benchmarks need Release and take several minutes, since `RandomizedAccess` builds a 1,000,000-object pool for each parameter combination.
