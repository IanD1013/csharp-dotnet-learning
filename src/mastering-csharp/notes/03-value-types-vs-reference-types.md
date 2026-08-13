# Value Types vs. Reference Types

> Course: [Mastering: C#](https://dometrain.com/course/mastering-csharp/) · Chapter 3
> 12 lessons · ~21.5 minutes
> Source: Dometrain. Every section links back to the lesson it came from.
> Runnable companion code: [`../03-value-types-vs-reference-types/`](../03-value-types-vs-reference-types/)

---

## The mental model

The difference between a value type and a reference type is **not** stack versus heap.
That is an implementation detail the runtime is free to change.

The real difference is three sets of semantics, and every behaviour in this chapter falls out of them.

| | value type (`struct`) | reference type (`class`) |
| --- | --- | --- |
| **Storage** | inline: the data sits directly in the variable slot or inside the parent object | indirection: the variable holds a reference to data elsewhere |
| **Copy** | copies the **data**, producing an independent clone | copies the **reference**, producing an alias to one shared instance |
| **Equality** | value based by default: compares content | referential by default: compares identity |

How C# sorts its types into these buckets:

- **Reference types**: `class`, `record`, arrays, `object`, delegates
- **Value types**: `struct`, `record struct`, `enum`, and every primitive (`int`, `bool` and friends are aliases for structs)
- **Pointer types**: unmanaged contexts only, rare in application code

---

## Lesson index

| # | Lesson | Length | Covered in |
| --- | --- | --- | --- |
| 1 | [Module Overview](https://dometrain.com/take/course/mastering-csharp-3256129/module-overview-69958834/) | 1:24 | [Why the type system is the foundation](#why-the-type-system-is-the-foundation) |
| 2 | [Overview](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958835/) | 0:56 | [The mental model](#the-mental-model) |
| 3 | [Storage Model: Inline vs. Indirection](https://dometrain.com/take/course/mastering-csharp-3256129/storage-model-inline-vs-indirection-69958836/) | 2:49 | [1.1](#11-inline-vs-indirection) |
| 4 | [Exploring Storage](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-storage-69958837/) | 1:03 | [1.2](#12-what-an-array-actually-allocates) |
| 5 | [Analyzing Storage Model with Benchmarks](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-storage-model-with-benchmarks-69958838/) | 1:58 | [1.3](#13-the-memory-arithmetic) |
| 6 | [Inspecting Type Layout (part 2)](https://dometrain.com/take/course/mastering-csharp-3256129/inspecting-type-layout-part-2-69958839/) | 1:06 | [1.4](#14-seeing-the-layout-for-real) |
| 7 | [Exploring Access Patterns with Benchmarks](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-access-patterns-with-benchmarks-69958840/) | 1:00 | [1.5](#15-the-cost-of-indirection) |
| 8 | [Analyzing Access Patterns Benchmark Results](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-access-patterns-benchmark-results-69958841/) | 3:16 | [1.5](#15-the-cost-of-indirection) |
| 9 | [Copy Semantics: Assignment](https://dometrain.com/take/course/mastering-csharp-3256129/copy-semantics-assignment-69958842/) | 1:25 | [2.1](#21-assignment) |
| 10 | [Copy Semantics: Parameter Passing](https://dometrain.com/take/course/mastering-csharp-3256129/copy-semantics-parameter-passing-69958843/) | 3:27 | [2.2](#22-parameter-passing) |
| 11 | [Equality Semantics](https://dometrain.com/take/course/mastering-csharp-3256129/equality-semantics-69958844/) | 1:15 | [Part 3](#part-3--equality-semantics) |
| 12 | [Summary](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958845/) | 1:48 | [The stack-versus-heap misconception](#the-stack-versus-heap-misconception) |

---

## Why the type system is the foundation

> [Module Overview](https://dometrain.com/take/course/mastering-csharp-3256129/module-overview-69958834/)

Types define application semantics whether the codebase leans object-oriented (`class`, `struct`) or functional (`record`, pure functions).
They govern identity, mutability, and equality, which is to say they govern how data flows and whether the system behaves correctly at all.

They also govern performance directly.
The value-versus-reference choice determines allocation patterns, copy semantics, and object layout.

The part worth internalizing: modern C# features are consequences of this foundation rather than magic layered on top.

- A **record** is a reference type that implements value-based equality and controlled immutability. Understand value-based equality and record behaviour stops being surprising.
- **Pattern matching** is a way of asking the type system questions: what type is this, is it null, what state are its members in. Framed as a query mechanism rather than syntax, it becomes much easier to reason about.

Badly defined types produce ambiguous APIs.
Well defined types make intent obvious.

---

## Part 1 · Storage semantics

### 1.1 Inline vs. indirection

> [Storage Model: Inline vs. Indirection](https://dometrain.com/take/course/mastering-csharp-3256129/storage-model-inline-vs-indirection-69958836/)

The two types compared throughout the chapter:

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

Pausing after `c` is initialized:

- `p1` is a value type and sits directly on the stack.
- `p2` is a reference on the stack pointing to an instance on the managed heap.
- `c` is likewise a reference on the stack pointing to a heap object.
- **Inside** `c`, the two properties behave differently. `P1` is a `Point`, so it is stored **inline** within the memory allocated for `c`. `P2` stores a reference pointing at the same `PointRef` instance that `p2` already references.

That last point is the one that matters: **context determines storage, not the type**.
A value type is not always on the stack, as `C.P1` shows, and modern runtimes can allocate reference types on the stack in the right circumstances.

### 1.2 What an array actually allocates

> [Exploring Storage](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-storage-69958837/)

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

| | Allocations for N = 10 | Why |
| --- | --- | --- |
| `PointRef[]` | **11** | one for the array, one per element |
| `Point[]` | **1** | values are written inline, independent of element count |

The struct array gives better data locality and puts far less pressure on the managed heap.

> The course is emphatic that these benchmarks exist to **build a mental model of memory layout**, not to serve as a basis for optimization on their own.
> Real performance work needs end-to-end analysis.

### 1.3 The memory arithmetic

> [Analyzing Storage Model with Benchmarks](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-storage-model-with-benchmarks-69958838/)

The overheads on a 64-bit runtime:

| Item | Size |
| --- | --- |
| Object header on every heap object | 16 bytes (8 sync block + 8 method table pointer) |
| Array length field plus padding | 4 + 4 bytes |
| One reference (an array element) | 8 bytes |
| One `PointRef` instance | 16 header + 4 `X` + 4 `Y` = **24 bytes** |
| One `Point` value | 4 `X` + 4 `Y` = **8 bytes**, no header |

For ten elements:

- **`PointRef[10]`**: the array object plus ten separate heap instances, ten of which cost 24 bytes each. Total around **344 bytes**.
- **`Point[10]`**: a single allocation holding the values inline, around **104 bytes**.

Reference types cost roughly 3.3x the memory to hold the same 80 bytes of actual data.
Whether that matters depends entirely on the application, which is what benchmarking is for.

> The lesson's walkthrough quotes the array object as ~108 bytes while also giving ~344 as the total.
> Those two do not reconcile (108 + 240 = 348).
> Measured on .NET 10 the array object is exactly **104** bytes, and 104 + 240 = 344 matches the lesson's stated total, so 108 looks like a slip on screen.
> See [Measured on this machine](#measured-on-this-machine).

### 1.4 Seeing the layout for real

> [Inspecting Type Layout (part 2)](https://dometrain.com/take/course/mastering-csharp-3256129/inspecting-type-layout-part-2-69958839/)

The `ObjectLayoutInspector` package prints how the CLR actually arranged a type, which beats trusting a diagram.

```csharp
using ObjectLayoutInspector;

TypeLayout.PrintLayout<Point>();          // a type's layout
TypeLayout.PrintLayout<PointRef>();
ArrayLayout.PrintLayout(new Point[10]);   // a specific array instance's layout
ArrayLayout.PrintLayout(refs);
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/inspecting-type-layout-part-2-69958839/?t=10)

Two APIs with different jobs: `TypeLayout.PrintLayout<T>()` describes a **type**, while `ArrayLayout.PrintLayout(instance)` describes a **particular array instance** so you can see how its elements are stored.

What shows up at runtime:

- **`Point`**: zero overhead, fields packed back to back, no CLR metadata at all.
- **`PointRef`**: two bookkeeping slots ahead of the fields - an **object header** for synchronization and internal metadata, and a **method table pointer** to the type's method table - then the same two ints.
- **Both arrays**: identical prologue of object header, method table pointer, length, and padding. The difference is the element region, where `Point[]` holds values and `PointRef[]` holds pointers.

One easy misread: the two **array objects** are the same size in the inspector output.
The difference is that `PointRef[]` has ten more heap objects hanging off it, while `Point[]` is genuinely one block of memory.

### 1.5 The cost of indirection

> [Exploring Access Patterns with Benchmarks](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-access-patterns-with-benchmarks-69958840/) · [Analyzing Access Patterns Benchmark Results](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-access-patterns-benchmark-results-69958841/)

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

#### The trap: a clean benchmark lies to you

Iterating a million elements sequentially, structs come out only about **60% faster**, well short of what the theory suggests.
The reason is hiding in the setup:

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

Everything was allocated back to back, so the reference objects sit adjacent on the heap and the **hardware prefetcher hides the indirection**.
Real applications have fragmented memory where references are rarely that tidy.

This arc is the most valuable thing in the chapter: a benchmark that looks rigorous, produces a believable number, and is measuring the wrong thing.

#### The fix: shuffle to defeat the prefetcher

```csharp
[ShortRunJob]
[HideColumns("Error", "Gen1")]
public class RandomizedAccess
{
    // When true, sample refs at random positions from the pool
    // (cache-hostile). When false, take the first Size entries in
    // order - same pool, same allocation layout, but a prefetcher-
    // friendly access pattern. The contrast isolates access order
    // from allocation order.
    [Params(false, true)]
    public bool Shuffle { get; set; }

    [Params(100, 10_000, 1_000_000)]
    public int Size { get; set; }

    // Size of the backing pool. Large enough that sampled refs are
    // scattered across many cache lines / pages.
    private const int PoolSize = 1_000_000;

    private Point[] _points = [];
    private PointRef[] _pointRefs = [];
```

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
            // Same pool, sequential slice - refs point at adjacent
            // pool entries, preserving allocation order.
            for (int i = 0; i < Size; i++)
                _pointRefs[i] = pool[i];
        }
    }
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-access-patterns-benchmark-results-69958841/?t=90)

The design is careful: both arms draw from the same fixed 1,000,000-element pool with identical allocation layout, and only the **access order** changes.
That separates access order from allocation order, so the measurement attributes the cost to the right cause.

#### Results reported in the lesson

| Elements | struct vs. class | Why |
| --- | --- | --- |
| 100 | **no difference** | the data is hot in L1 cache, which is fast enough to hide everything |
| 10,000 | struct about **2x** faster | past L1, but the CPU still prefetches some of it |
| 1,000,000 shuffled | struct approaching **10x** faster | every reference is a trip to main memory; the struct array stays contiguous regardless |

Simple benchmarks mislead, but the cost of indirection is real.
**Once the access pattern stops being perfectly sequential, iterating values wins.**

---

## Part 2 · Copy semantics

### 2.1 Assignment

> [Copy Semantics: Assignment](https://dometrain.com/take/course/mastering-csharp-3256129/copy-semantics-assignment-69958842/)

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

`p2 = p1` creates an **alias**: both names refer to one instance, so a mutation through `p2` is visible through `p1`.
`p4 = p3` performs a bitwise copy, so `p4` is independent and mutating it leaves `p3` alone.

**The unifying rule**: assignment **always copies the value**.
Only the definition of "the value" changes.

- For a **struct**, the value is the data held in the object.
- For a **class**, the value is the reference to the instance.

Copying a reference leaves two variables pointing at one location, which is the entire explanation for aliasing.
There is one rule here, not two.

### 2.2 Parameter passing

> [Copy Semantics: Parameter Passing](https://dometrain.com/take/course/mastering-csharp-3256129/copy-semantics-parameter-passing-69958843/)

Passing an argument follows the same rule as assignment.

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

#### Mutable aliases: `ref` and `out`

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

The last line is the interesting one.
Passing a class by `ref` lets the callee **repoint the caller's variable at a different instance**, which ordinary reference passing cannot do.

`out` behaves like `ref` except the callee is **required** to assign before returning.

#### Read-only aliases: `in` and `ref readonly`

These avoid a copy without granting write access, which matters mainly for large structs.

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

#### The four modifiers side by side

| Modifier | Can reassign the parameter | Can write fields | Accepts r-values | Notes |
| --- | --- | --- | --- | --- |
| *(default)* | affects the local copy only | struct: the copy; class: the shared instance | yes | copy semantics |
| `ref` | yes, affects the caller | yes | no, needs an l-value | fully mutable alias |
| `out` | yes, affects the caller | yes | no, needs an l-value | callee **must** assign |
| `in` | no | no | yes, compiler may create a temporary | read-only alias |
| `ref readonly` | no | no | warns if given an r-value directly | stricter than `in` |

> Using `in` with a **mutable** struct can trigger **defensive copies**, undoing the optimization.
> That is the subject of the Mastering Structs chapter.

---

## Part 3 · Equality semantics

> [Equality Semantics](https://dometrain.com/take/course/mastering-csharp-3256129/equality-semantics-69958844/)

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

### Structs

- `Equals` compares **content**, so identical field values mean equal.
- **`==` does not exist by default.** The compiler emits no equality operator for a struct, so using `==` requires writing `operator ==` yourself. This is the detail most easily confused with class behaviour.
- Mutating a field changes the verdict: after `s1.X++`, `s1.Equals(s2)` becomes `false`.

### The `ReferenceEquals` boxing trap

This prints `False` even though both arguments are the **same variable**:

```csharp
Console.WriteLine(object.ReferenceEquals(s1, s1)); // False
```

`ReferenceEquals` takes `object` parameters, so passing a struct **boxes** it.
Each argument is boxed into its own separate heap object, so comparing references can never succeed.
**Never call `ReferenceEquals` on a struct.**

The demo project confirms the compiler already knows this: the call raises analyzer warning **CA2013** at build time, before it can mislead anyone at runtime.

### Classes

Reference semantics by default, so identical content at different addresses is not equal.
In the default state `Equals`, `==`, and `ReferenceEquals` all perform the **same check**: do these point at one instance.
After `r2 = r1` all three report `true`.

### Two practical notes

1. **Override `Equals` and `GetHashCode` on structs.** The course's reason is performance: the default implementation is reflection based.
   In practice the runtime uses a fast memory comparison when a struct contains no reference fields and has no padding, and falls back to the slow reflective path otherwise - `Point` happens to hit the fast path, but relying on that is fragile, so writing the overrides is still the right habit.
2. **Classes can opt into value semantics** by overriding `Equals`, `GetHashCode`, and `operator ==`. That is precisely what **records** automate, covered in the Mastering Records chapter.

---

## Measured on this machine

Produced by the companion demo project on .NET 10, Windows 11 x64.
Run it yourself with `dotnet run -c Release -- storage`.

### Type layout

```
=== Point (struct) ===
Type layout for 'Point'
Size: 8 bytes. Paddings: 0 bytes (%0 of empty space)
|==========================|
|   0-3: Int32 X (4 bytes) |
|--------------------------|
|   4-7: Int32 Y (4 bytes) |
|==========================|

=== PointRef (class) ===
Type layout for 'PointRef'
Size: 8 bytes. Paddings: 0 bytes (%0 of empty space)
|============================|
| Object Header (8 bytes)    |
|----------------------------|
| Method Table Ptr (8 bytes) |
|============================|
|   0-3: Int32 X (4 bytes)   |
|----------------------------|
|   4-7: Int32 Y (4 bytes)   |
|============================|
```

The struct is 8 bytes of pure data.
The class reports the same 8 bytes of fields but carries 16 bytes of header in front, so an instance costs 24 bytes.

### Array layout

Both arrays print the same total, which is exactly the point:

```
Array layout for 'Point[10]' (with values)
Element type: Point. Element size: 8 bytes. Length: 10
Total size: 104 bytes (Header: 16, Length: 4, Length Padding: 4, Data: 80, Trailing Padding: 0)

Array layout for 'PointRef[10]' (with values)
Element type: PointRef. Element size: 8 bytes. Length: 10
Total size: 104 bytes (Header: 16, Length: 4, Length Padding: 4, Data: 80, Trailing Padding: 0)
```

`Point[10]` is 104 bytes and that is the whole story.
`PointRef[10]` is 104 bytes **plus** ten 24-byte instances elsewhere on the heap, so 344 bytes for the same 80 bytes of data.

### Allocation benchmark

```
BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8875/25H2)
Intel Core i7-10700 CPU 2.90GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK 10.0.400, .NET 10.0.11, X64 RyuJIT x86-64-v3
Job=ShortRun  IterationCount=3  LaunchCount=1  WarmupCount=3
```

| Size | Method | Mean | Allocated |
| ---: | --- | ---: | ---: |
| 10 | `CreateArrayOfPointRefs` | 43.00 ns | **344 B** |
| 10 | `CreateArrayOfPoints` | 12.64 ns | **104 B** |
| 100 | `CreateArrayOfPointRefs` | 516.04 ns | 3,224 B |
| 100 | `CreateArrayOfPoints` | 115.62 ns | 824 B |
| 1000 | `CreateArrayOfPointRefs` | 5,098.39 ns | 32,024 B |
| 1000 | `CreateArrayOfPoints` | 820.78 ns | 8,024 B |

BenchmarkDotNet's allocation tracking confirms the arithmetic exactly at every size.
`Point[N]` costs `24 + 8N` bytes, `PointRef[N]` costs `24 + 32N`, and the ratio settles at 4x as N grows.
The 344 and 104 for ten elements land precisely on the predicted values, which is why the lesson's on-screen 108 reads as a slip rather than a runtime difference.

### Access benchmark, sequential vs. shuffled

`RandomizedAccess`, same pool and same allocation layout in both arms, only the access order differs:

| Size | Shuffle | `PointRef` | `Point` | struct advantage |
| ---: | --- | ---: | ---: | ---: |
| 100 | false | 34.64 ns | 35.49 ns | none |
| 100 | **true** | 32.15 ns | 30.35 ns | none |
| 10,000 | false | 5,904.61 ns | 2,993.31 ns | 1.97x |
| 10,000 | **true** | 5,819.67 ns | 2,864.77 ns | 2.03x |
| 1,000,000 | false | 1,585,946 ns | 363,871 ns | 4.36x |
| 1,000,000 | **true** | **4,547,086 ns** | **332,695 ns** | **13.7x** |

Read the last two rows as a pair, because that is where the whole argument lives.
Shuffling costs the reference array **2.9x** (1.59 ms to 4.55 ms) while the struct array does not move at all (0.364 ms to 0.333 ms, inside the noise).
The struct arm is the control: it is contiguous no matter what `Shuffle` says, so its flat line proves the reference arm's slowdown came from access order and nothing else.

The 100-element and 10,000-element rows reproduce the lesson exactly: no measurable difference in L1, then almost precisely 2x once the working set outgrows it.

> **One number did not reproduce.**
> The lesson reports the *sequential* million-element case at only ~60% apart.
> On this i7-10700 the sequential case is already **4.36x** apart, and shuffling widens it to 13.7x rather than opening a gap that was not there.
> The chapter's argument survives intact - randomizing access triples the cost of chasing references while leaving values untouched - but the size of the effect is clearly machine dependent, so treat the specific multipliers as illustrations rather than constants.

---

## The stack-versus-heap misconception

> [Summary](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958845/)

"Value types live on the stack, reference types live on the heap" is a teaching simplification, **not a definition**.
Where data actually lands depends on context and on the runtime.

**Value types that end up on the heap:**

- fields of a class, stored inline inside the heap object (see `C.P1` in [1.1](#11-inline-vs-indirection))
- locals inside an iterator block
- locals inside an `async` method

**Reference types that end up on the stack:**

- objects the runtime proves do not escape the local scope, via escape analysis

The runtime may also keep values in CPU registers and never materialize them in memory at all.

Physical location does affect performance, but it is **not** the distinction between the two type systems.
That distinction is always the three semantics: storage, copy, equality.

---

## Self-test

1. Ten elements each. How many heap allocations does `Point[]` make, and how many does `PointRef[]` make? How many bytes each?
2. Why does `object.ReferenceEquals(s1, s1)` return `False` for a struct?
3. Does `s1 == s2` compile for a plain struct? Why or why not?
4. Passing a class argument, what can the callee do with `ref` that it cannot do with `in`?
5. In `RandomizedAccess`, flipping `Shuffle` to true costs the reference array 2.9x while the struct array does not move. Why does the struct arm stay flat, and what makes it the right control for this experiment?
6. Name two situations where a value type is stored on the heap.
7. In one sentence, why are assignment semantics for structs and classes actually the same rule?

<details>
<summary>Answer key</summary>

1. `Point[10]`: 1 allocation, 104 bytes. `PointRef[10]`: 11 allocations, 344 bytes (104 array + 10 x 24 instances).
2. `ReferenceEquals` takes `object`, so each struct argument is boxed into its own separate heap object, and two distinct boxes can never be reference-equal.
3. No. The compiler emits no default `operator ==` for structs, so it must be written explicitly. `Equals` still works and is value based.
4. `ref` can mutate the object's fields **and** repoint the caller's variable at a brand new instance. `in` is a read-only alias that can do neither.
5. The struct array holds its values inline, so it is contiguous whether or not `Shuffle` is set - the flag only changes which pool entries the *reference* array points at. That makes the struct arm a control: both arms share one pool and one allocation layout, so the only variable left is access order, and the reference arm's 2.9x slowdown can be attributed to cache misses rather than to how the objects were allocated.
6. A struct field of a class, stored inline in the heap object; a local inside an iterator block or an `async` method.
7. Assignment always copies the value - for a struct the value is the data, for a class the value is the reference.

</details>

---

## Running the companion code

Two projects live in [`../03-value-types-vs-reference-types/`](../03-value-types-vs-reference-types/).

**Demos** print storage layout, copy behaviour, and equality results. Fast, run them freely:

```bash
cd src/mastering-csharp/03-value-types-vs-reference-types/MasteringCSharp.ValueVsReference.Demos
dotnet run -c Release              # all three sections
dotnet run -c Release -- storage   # or: copy, equality
```

**Benchmarks** reproduce the allocation and access-pattern measurements. These need Release and take several minutes, since `RandomizedAccess` builds a 1,000,000-object pool for each parameter combination:

```bash
cd src/mastering-csharp/03-value-types-vs-reference-types/MasteringCSharp.ValueVsReference.Benchmarks
dotnet run -c Release -- --list flat                            # see what is available
dotnet run -c Release -- --filter *ArrayAllocationBenchmark*    # allocations, quick
dotnet run -c Release -- --filter *ArrayAccessBenchmark*        # the misleading one
dotnet run -c Release -- --filter *RandomizedAccess*            # the honest one, slow
```

`ArrayAccessBenchmark` and `RandomizedAccess` are both included on purpose.
Running the naive one first and the shuffled one second reproduces the chapter's argument rather than just its conclusion.

---

## Threads into later chapters

| Deferred here | Picked up in |
| --- | --- |
| Defensive copies when `in` meets a mutable struct | Mastering Structs |
| Records as reference types with value-based equality | Mastering Records |
| The real cost of default struct equality | Mastering Records |
| Pattern matching as a query over the type system | Mastering Pattern Matching |
