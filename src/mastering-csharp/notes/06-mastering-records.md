# Mastering Records

> Course: [Mastering: C#](https://dometrain.com/course/mastering-csharp/) · Chapter 6
> 7 lessons · ~10.8 minutes
> Source: Dometrain. Every section links back to the lesson it came from.
> Companion project: [`src/mastering-csharp/06-mastering-records`](../06-mastering-records). See [Running the demo](#running-the-demo).
> Picks up the struct-equality thread deferred at the end of [Mastering Structs](05-mastering-structs.md#threads-into-later-chapters).

---

## The mental model

The chapter's own framing, from the [Overview](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958863/):

> **Records are the most effective way to model values in C#.**

That sentence hides two separate arguments, and the chapter makes both.

The first is about **effort**.
Value-based equality is five members that have to agree with each other, and a record is one line that generates all five.
This half is well known and takes two lessons.

The second is about **correctness**, and it is the half worth rereading.
Structs already have value-based equality without any keyword, which sounds like the problem is already solved for value types.
The chapter spends its last two lessons showing that the free version is a **performance bug waiting for a `HashSet`**:

> **Default struct equality is not a slower version of the right answer. It is the wrong answer, and it degrades an O(1) lookup to O(N).**

So the chapter has a shape worth holding onto:

| | what you get by default | what the compiler generates for you |
| --- | --- | --- |
| `class` | referential equality, `ToString` that prints the type name | `record` gives value equality, `with`, `Deconstruct`, readable `ToString` |
| `struct` | value equality that boxes and may hash **one field** | `record struct` gives typed, non-boxing, all-field equality |

Read left to right, the class row is a **convenience** upgrade and the struct row is a **correctness** upgrade.
If only one thing survives from this chapter, make it the struct row: a plain `struct` used as a dictionary key is a latent bug, and the fix is one keyword.

---

## Lesson index

| # | Lesson | Length | Covered in |
| --- | --- | --- | --- |
| 1 | [Overview](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958863/) | 0:49 | [The mental model](#the-mental-model) |
| 2 | [Manual Value-based Equality](https://dometrain.com/take/course/mastering-csharp-3256129/manual-value-based-equality-69958864/) | 0:30 | [1.1](#11-the-five-members-that-have-to-agree) |
| 3 | [Referential and Value-based Equality](https://dometrain.com/take/course/mastering-csharp-3256129/referential-and-value-based-equality-69958865/) | 1:45 | [1.2](#12-three-ways-to-compare-and-only-two-of-them-are-yours) · [1.3](#13-the-one-line-replacement) |
| 4 | [Records Under the Hood](https://dometrain.com/take/course/mastering-csharp-3256129/records-under-the-hood-69958866/) | 1:29 | [2.1](#21-a-record-is-a-class) · [2.2](#22-equalitycontract) · [2.3](#23-with-is-clone--init) |
| 5 | [Records Limitations](https://dometrain.com/take/course/mastering-csharp-3256129/records-limitations-69958867/) | 1:46 | [3.1](#31-equality-is-all-or-nothing) · [3.2](#32-the-composition-fix) · [3.3](#33-the-other-two-limits) |
| 6 | [Issues With the Default Structs Equality](https://dometrain.com/take/course/mastering-csharp-3256129/issues-with-the-default-structs-equality-69958868/) | 1:57 | [4.1](#41-the-symptom-1ms-vs-3-seconds) · [4.2](#42-the-cause-blittable-vs-non-blittable) |
| 7 | [Record Structs vs. Default Structs](https://dometrain.com/take/course/mastering-csharp-3256129/record-structs-vs-default-structs-69958869/) | 2:33 | [4.3](#43-the-second-cost-boxing) · [4.4](#44-the-fix) |

Every lesson in this chapter has a document; nothing was skipped.

Note the chapter has no summary or conclusion lesson: it ends on lesson 7 mid-argument, and the structure above is therefore reconstructed rather than announced.

---

## Part 1 · Value equality, by hand and by keyword

### 1.1 The five members that have to agree

> [Manual Value-based Equality](https://dometrain.com/take/course/mastering-csharp-3256129/manual-value-based-equality-69958864/)

C# classes use referential equality: two instances are equal only when they are the same object on the heap.
Some built-in types, `string` most obviously, override that. Yours do not, unless you write it.

The lesson's starting point is deliberately minimal, because the point is how much has to be added to it:

```csharp
// The default: reference based equality
class PointClass
{
    public int X { get; }
    public int Y { get; }
    public PointClass(int x, int y)
        => (X, Y) = (x, y);
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/manual-value-based-equality-69958864/?t=10)

Doing it properly means implementing `IEquatable<T>`, overriding `Equals(object?)` and `GetHashCode()`, overriding `ToString()` for debugging, and overloading `==` and `!=`:

```csharp
// Value-based equality implemented by hand: Equals, GetHashCode,
// ToString, and the == / != operators all have to agree.
sealed class PointValue : IEquatable<PointValue>
{
    public int X { get; }
    public int Y { get; }

    public PointValue(int x, int y)
        => (X, Y) = (x, y);

    public bool Equals(PointValue? other)
        => other is not null && X == other.X && Y == other.Y;

    public override bool Equals(object? obj)
        => Equals(obj as PointValue);

    public override int GetHashCode()
        => HashCode.Combine(X, Y);

    public override string ToString()
        => $"PointValue {{ X = {X}, Y = {Y} }}";

    public static bool operator ==(PointValue? left, PointValue? right)
        => left is null ? right is null : left.Equals(right);

    public static bool operator !=(PointValue? left, PointValue? right)
        => !(left == right);
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/manual-value-based-equality-69958864/?t=10)

The lesson is explicit about why `GetHashCode` is not optional: objects that compare equal **must** hash equal, or hash-based collections silently misbehave.
That constraint is what makes the whole cluster a unit, and it is also what makes hand-written equality decay.
Every one of these members mentions `X` and `Y` by name, so adding a third property means remembering to edit four places.

### 1.2 Three ways to compare, and only two of them are yours

> [Referential and Value-based Equality](https://dometrain.com/take/course/mastering-csharp-3256129/referential-and-value-based-equality-69958865/)

The lesson enumerates the three comparisons up front, which is what makes the results readable:

1. the equality operator `==`
2. the instance `Equals` method
3. the static `Object.ReferenceEquals`

```csharp
Console.WriteLine("=== Class equality (reference) ===");
var c1 = new PointClass(1, 2);
var c2 = new PointClass(1, 2);
Console.WriteLine($"c1 == c2:           {c1 == c2}");
Console.WriteLine($"c1.Equals(c2):      {c1.Equals(c2)}");
Console.WriteLine($"ReferenceEquals:    {ReferenceEquals(c1, c2)}");
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/referential-and-value-based-equality-69958865/?t=10)

All three are `false`, and `ToString()` returns the type name.

Swap in `PointValue` and the first two become `true` while `ReferenceEquals` stays `false`.
That is the correct answer, not a failure: **`ReferenceEquals` cannot be customised**, so it keeps answering the question it was asked, which is whether these are one object or two.

The lesson also names the specific way a partial implementation fails:

> If you override the `Equals` method but fail to overload the equality operator, `v1 == v2` will still return `false` while `v1.Equals(v2)` returns `true`.

Two comparisons that look interchangeable in source disagree at runtime, which is precisely the bug class that generated members exist to remove.

### 1.3 The one-line replacement

> [Referential and Value-based Equality](https://dometrain.com/take/course/mastering-csharp-3256129/referential-and-value-based-equality-69958865/)

```csharp
record Point(int X, int Y);
```

The lesson shows the compiler's output as hand-written C#, which is the clearest possible statement of what you stop maintaining:

```csharp
// The compiler generates code similar to this manual implementation:
class PointClass : IEquatable<PointClass>
{
    public int X { get; init; }
    public int Y { get; init; }

    public PointClass(int x, int y) =>
        (X, Y) = (x, y);

    public void Deconstruct(out int x, out int y) =>
        (x, y) = (X, Y);

    public bool Equals(PointClass? other) =>
        other is not null && X == other.X && Y == other.Y;

    public override bool Equals(object? obj) =>
        obj is PointClass other && Equals(other);

    public override int GetHashCode() =>
        HashCode.Combine(X, Y);

    public override string ToString() =>
        $"PointClass {{ X = {X}, Y = {Y} }}";

    public static bool operator ==(PointClass? left, PointClass? right) =>
        Equals(left, right);

    public static bool operator !=(PointClass? left, PointClass? right) =>
        !Equals(left, right);
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/referential-and-value-based-equality-69958865/?t=85)

Beyond equality, the record also gets `Deconstruct`, `init` accessors, and support for `with`.
What it does **not** get is a different storage model: a `record` is still a class, so `ReferenceEquals` on two equal records is still `false`, and it is still a heap allocation.

---

## Part 2 · What the compiler actually generates

> [Records Under the Hood](https://dometrain.com/take/course/mastering-csharp-3256129/records-under-the-hood-69958866/)

### 2.1 A record is a class

Decompiling `record Point(int X, int Y)` gives an ordinary class implementing `IEquatable<Point>`, with private backing fields:

```csharp
internal class Point : IEquatable<Point>
{
  private readonly int <X>k__BackingField;
  private readonly int <Y>k__BackingField;

  public Point(int X, int Y)
  {
    this.<X>k__BackingField = X;
    this.<Y>k__BackingField = Y;
    base..ctor();
  }

  protected virtual Type EqualityContract => typeof(Point);
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/records-under-the-hood-69958866/?t=5)

Positional parameters become properties with `init` accessors, which is what makes object initializer syntax legal **provided the record also offers a parameterless constructor**:

```csharp
record Point(int X, int Y)
{
    public Point()
        : this(0, 0)
    {
    }
}

var p2 = new Point { X = 3, Y = 4 };
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/records-under-the-hood-69958866/?t=30)

### 2.2 EqualityContract

`EqualityContract` is a `protected virtual Type` property returning the record's own type.

```csharp
[CompilerGenerated]
protected virtual Type EqualityContract
{
  [CompilerGenerated] get
  {
    return typeof (Point);
  }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/records-under-the-hood-69958866/?t=15)

It exists for inheritance.
Because it is `virtual`, a derived record overrides it, and because generated `Equals` compares it first, a `Point3D` is never equal to a `Point` even when `X` and `Y` match:

```csharp
[CompilerGenerated]
public virtual bool Equals(Point other)
{
  if ((object) this == (object) other)
    return true;
  return (object) other != null && Type.op_Equality(this.EqualityContract, other.EqualityContract) && EqualityComparer<int>.Default.Equals(this.<X>k__BackingField, other.<X>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Y>k__BackingField, other.<Y>k__BackingField);
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/records-under-the-hood-69958866/?t=55)

This is the fix for a classic hand-written-equality bug, where `a.Equals(b)` and `b.Equals(a)` disagree across a type hierarchy.
The contract is folded into `GetHashCode` too, so the two types also occupy different buckets:

```csharp
[CompilerGenerated]
public override int GetHashCode()
{
  return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<X>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Y>k__BackingField);
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/records-under-the-hood-69958866/?t=40)

The lesson states the constraint that Part 3 then works around:

> These implementations use all properties of the record; it is not possible to exclude specific properties from the compiler-generated equality logic.

### 2.3 `with` is `<Clone>$` + `init`

Non-destructive mutation is not a language trick, it is two generated members: a hidden virtual `<Clone>$` and a `protected` copy constructor.

```csharp
[CompilerGenerated]
public virtual Point <Clone>$()
{
  return new Point(this);
}

[CompilerGenerated]
protected Point(Point original)
{
  base..ctor();
  this.<X>k__BackingField = original.<X>k__BackingField;
  this.<Y>k__BackingField = original.<Y>k__BackingField;
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/records-under-the-hood-69958866/?t=55)

A `with` expression lowers to a clone followed by the `init` setters for the changed properties:

```csharp
// 'with' expression implementation
Point point = p2_1.<Clone>$();
point.X = 10;
Point p2_2 = point;

// Deconstruction implementation
int X;
int Y;
p2_2.Deconstruct(out X, out Y);
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/records-under-the-hood-69958866/?t=70)

Two things follow from reading the lowered form rather than the sugar.
`<Clone>$` is **virtual**, so `with` on a variable typed as the base record still produces the derived type.
And the clone copies **every** field before one is overwritten, which is where the cost noted in [3.3](#33-the-other-two-limits) comes from.

`ToString` is generated too, and is the one generated member with a supported extension point: override `PrintMembers` to change what gets printed.

---

## Part 3 · Where generated equality stops being what you want

> [Records Limitations](https://dometrain.com/take/course/mastering-csharp-3256129/records-limitations-69958867/)

### 3.1 Equality is all or nothing

Records compare every property with that property's default comparer.
For `string` that means ordinal, case-sensitive, which is wrong for anything path-like:

```csharp
var l1 = new Location(Path: "/users/sergey/readme.md", Position: 42);
var l2 = new Location(Path: "/users/sergey/ReadMe.md", Position: 42);

Console.WriteLine($"l1: {l1}, l2: {l2}");
Console.WriteLine($"l1 == l2: {l1 == l2}");

record Location(string Path, int Position);
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/records-limitations-69958867/?t=40)

`false`, for two paths that name the same file on Windows.

You can override `Equals` and `GetHashCode` on the record itself, and the lesson mentions that option only to reject it: doing so puts you back in the business of keeping hand-written members in sync with the property list, which is what the record was for.

### 3.2 The composition fix

> Wrapping standard types (like `string`) in specialized record structs allows for custom equality logic without bloating the primary record.

Push the comparison rule down into the property's **type** instead:

```csharp
readonly record struct NormalizedPath(string Path)
{
    private static readonly StringComparer PathComparer = StringComparer.OrdinalIgnoreCase;

    public static implicit operator NormalizedPath(string path) =>
        new(path);

    public bool Equals(NormalizedPath other) =>
        PathComparer.Equals(Path, other.Path);

    public override int GetHashCode() =>
        PathComparer.GetHashCode(Path);

    public override string ToString() =>
        Path;
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/records-limitations-69958867/?t=70)

```csharp
record Location(NormalizedPath Path, int Position);
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/records-limitations-69958867/?t=85)

`l1 == l2` is now `true`, and three details make this the version worth copying:

- **`readonly record struct`** as the wrapper, so the abstraction costs no allocation.
- **The implicit operator**, so `new Location("/users/...", 42)` still compiles unchanged. The record's shape changed and its call sites did not.
- **`GetHashCode` overridden alongside `Equals`**, keeping the pair consistent, which is what lets `Location` work as a dictionary key.

`Location` keeps all of its generated members. The rule lives in one place, and it lives with the data it describes.

> **Aside.** `Equals` here is declared without `override`, which looks like a slip and is not.
> The compiler generates `Equals(NormalizedPath)` for a record struct, and declaring it yourself **replaces** the generated one rather than overriding anything, so no modifier is correct.
> `GetHashCode` does need `override`, because that one comes from `object`.

### 3.3 The other two limits

**`with` is not free.**
Every `with` expression clones the whole instance through the copy constructor, so a loop that threads a record through ten transformations allocates ten records.
Fine for a request handler, worth knowing about in a hot path.

**Immutability is shallow.**
`init` protects the property, not what it refers to.
A record holding a `List<T>` hands out a fully mutable list, and `with` copies the reference, so the "copy" shares it.
Verified on this machine below.

---

## Part 4 · Structs: the free equality is the expensive one

### 4.1 The symptom: 1ms vs 3 seconds

> [Issues With the Default Structs Equality](https://dometrain.com/take/course/mastering-csharp-3256129/issues-with-the-default-structs-equality-69958868/)

The lesson opens with a measurement rather than an explanation, which is the right order:

```csharp
using System.Diagnostics;

var sw = Stopwatch.StartNew();

var locations = Enumerable.Range(1, 10_000)
    .Select(n => new Location(path: "", position: n))
    .ToArray();

Console.WriteLine($"Array created in {sw.ElapsedMilliseconds}ms");
sw.Restart();

var uniqueLocations = new HashSet<Location>(locations);
Console.WriteLine($"HashSet created in {sw.ElapsedMilliseconds}ms");

// All structs support value-based equality
readonly struct Location
{
    public string Path { get; }
    public int Position { get; }

    public Location(string path, int position)
    {
        Path = path;
        Position = position;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/issues-with-the-default-structs-equality-69958868/?t=15)

Array: about 1ms. `HashSet` of the same 10,000 items: **over 3 seconds**.
Nothing in that struct looks expensive, and there is no user-written `Equals` anywhere to blame.

### 4.2 The cause: blittable vs non-blittable

The default `GetHashCode` for a struct has **two** implementations, and which one runs depends on the type:

| | definition | default `GetHashCode` |
| --- | --- | --- |
| **blittable** | no reference-type fields, no gaps between fields | hashes all of the struct's bits |
| **non-blittable** | contains a reference type, or has padding | hashes the **first field only** |

`Location` holds a `string`, so it takes the second path, and every instance built with `path: ""` hashes identically no matter what `Position` is:

```csharp
for (int i = 0; i < 5; i++)
{
    var l = new Location(path: "", i);
    Console.WriteLine(l.GetHashCode());
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/issues-with-the-default-structs-equality-69958868/?t=35)

The lesson's proof that "first field" is meant literally: swap the two declarations, and the hash starts varying.

```csharp
readonly struct Location
{
    public int Position { get; } // Now the first field
    public string Path { get; }
    ...
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/issues-with-the-default-structs-equality-69958868/?t=55)

**Reordering two property declarations changed the runtime behaviour of a `HashSet`.**
Nothing else about the type changed, and nothing in the language marks those declarations as order-sensitive.

The consequence chain is the part to remember:

- Identical hash codes put every element in one bucket.
- A bucket is a linked list, searched linearly with `Equals`.
- Lookup goes from **O(1) to O(N)**.
- Building the set is N lookups, so construction goes from **O(N) to O(N²)**.

At 10,000 items that is roughly 50 million comparisons for what should have been 10,000 hash probes.

### 4.3 The second cost: boxing

> [Record Structs vs. Default Structs](https://dometrain.com/take/course/mastering-csharp-3256129/record-structs-vs-default-structs-69958869/)

The 50 million comparisons are not cheap comparisons.
A struct with no `Equals` of its own inherits `ValueType.Equals`, which is defined on a reference type, so **each side of each comparison is boxed**:

> In a lookup involving 10,000 items, this can result in 20,000 boxing allocations (two per comparison), leading to massive memory pressure.

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/record-structs-vs-default-structs-69958869/?t=46)

So the algorithmic blow-up and the allocation blow-up multiply rather than merely coexist, and the same applies to `GetHashCode` and `ToString`.
This is also the same mechanism as [chapter 5's boxing lesson](05-mastering-structs.md#11-boxing-a-cast-is-an-allocation): calling an inherited `object`/`ValueType` member on a struct boxes it.

### 4.4 The fix

```csharp
readonly record struct LocationRecordStruct(string Path, int Position);
```

The compiler generates a **typed** `Equals(LocationRecordStruct)` and a `GetHashCode` over all fields.
Typed means no boxing; all fields means no systematic collisions.

The lesson's own hash comparison, using public fields to make the shapes as close as possible:

```csharp
var a = new PersonKey("Alice", 25);
var b = new PersonKey("Alice", 99);
Console.WriteLine($"Same hash? {a.GetHashCode() == b.GetHashCode()}"); // True — only Name is used!

var ra = new PersonRecord("Alice", 25);
var rb = new PersonRecord("Alice", 99);
Console.WriteLine($"Same hash? {ra.GetHashCode() == rb.GetHashCode()}"); // False

struct PersonKey
{
    public string Name;
    public int Age;
    public PersonKey(string name, int age) { Name = name; Age = age; }
}

record struct PersonRecord(string Name, int Age);
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/record-structs-vs-default-structs-69958869/?t=78)

The chapter's summary of what `record struct` buys, all four of which are about the compiler rather than about you: members stay in sync with the declaration, all properties are used, hash distribution is good, and member access never boxes.

The benchmark the lesson runs to prove it is reproduced in [Measured on this machine](#measured-on-this-machine):

```csharp
[Benchmark(Baseline = true)]
public bool RecordStructEquality() =>
    _recordStructLocations.Contains(new LocationRecordStruct(Path: "", Position: 0));

[Benchmark]
public bool DefaultStructEquality() =>
    _defaultStructLocations.Contains(new LocationDefaultStruct(path: "", position: 0));
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/record-structs-vs-default-structs-69958869/?t=10)

---

## Measured on this machine

Intel Core i7-10700 @ 2.90GHz, 8 physical cores, Windows 11 26200, .NET 10.0.11, BenchmarkDotNet 0.15.8.

### Lookup: `HashSet.Contains`, one miss

The chapter's own benchmark, reproduced.

| Method | Count | Mean | Ratio | Allocated |
| --- | --- | ---: | ---: | ---: |
| RecordStructEquality | 100 | 3.510 ns | 1.00 | - |
| DefaultStructEquality | 100 | 7,156.298 ns | 2,042 | 15,232 B |
| RecordStructEquality | 1000 | 3.028 ns | 1.00 | - |
| DefaultStructEquality | 1000 | 68,592.704 ns | 22,655 | 152,032 B |
| RecordStructEquality | 10000 | 3.140 ns | 1.00 | - |
| DefaultStructEquality | 10000 | 676,920.085 ns | 215,947 | 1,520,033 B |

Both complexity claims are visible in one table.
The record struct is **flat**: 3.5ns, 3.0ns, 3.1ns as the set grows 100x, which is what O(1) looks like.
The default struct is **linear**: 7µs, 69µs, 677µs, tracking `Count` almost exactly.
By 10,000 items the same call is **215,947x slower**, and it allocates 1.5 MB to answer one `Contains`.

> **Aside: the allocation is worse than the lesson's estimate, consistently.**
> The lesson predicts "20,000 boxing allocations, two per comparison" for 10,000 items, which on x64 would be roughly 640 KB.
> Measured is **1,520,033 B**, and the per-comparison figure is suspiciously stable across all three sizes: **152.32, 152.03, 152.00 bytes**.
> Two boxed structs account for 64 of those bytes, so something in `ValueType.Equals` allocates the other 88 per comparison.
> That is consistent with its reflection path, which fetches a `FieldInfo[]` and boxes field values on both sides, though I did not confirm the breakdown beyond the arithmetic.
> The direction of the error is the useful part: the lesson's number is a floor, not a ceiling.

### Construction: building the `HashSet`

This one is not in the course, and it is where the O(N²) claim actually lives.

| Method | Count | Mean | Ratio | Allocated |
| --- | --- | ---: | ---: | ---: |
| BuildRecordStructSet | 100 | 1.125 µs | 1.00 | 3.07 KB |
| BuildDefaultStructSet | 100 | 471.435 µs | 419 | 740.96 KB |
| BuildRecordStructSet | 1000 | 11.235 µs | 1.00 | 30.30 KB |
| BuildDefaultStructSet | 1000 | 33,587.194 µs | 2,990 | 74,206.13 KB |
| BuildRecordStructSet | 10000 | 158.179 µs | 1.00 | 276.42 KB |
| BuildDefaultStructSet | 10000 | 3,627,235.167 µs | 22,932 | 7,421,722.41 KB |

Read down each column at 10x steps, because the growth rates are the whole point:

- Record struct: 1.1 → 11.2 → 158 µs. Roughly **10x per 10x**, so linear.
- Default struct: 471 → 33,587 → 3,627,235 µs. Roughly **71x then 108x per 10x**, so quadratic.

**3,627,235 µs is 3.63 seconds** to build one 10,000-element `HashSet`, which lands squarely on the "over 3 seconds" the lesson quotes, recorded on different hardware and a different runtime.
The demo's `Stopwatch` version of the same thing measured 3.9-4.2 seconds across runs.

The allocation column is the one to show anybody who doubts this matters: **7,421,722 KB is 7.1 GB of garbage** to build a set of 10,000 two-field structs.
The record struct does it in 276 KB.

### The demo output

```
-- Non-blittable struct: only the first field reaches GetHashCode --
  new LocationDefaultStruct("", 0).GetHashCode() = -96275623
  new LocationDefaultStruct("", 1).GetHashCode() = -96275623
  new LocationDefaultStruct("", 2).GetHashCode() = -96275623
  new LocationDefaultStruct("", 3).GetHashCode() = -96275623
  new LocationDefaultStruct("", 4).GetHashCode() = -96275623
  Position never enters the hash, so all five land in one bucket.

-- Same data, fields declared in the other order --
  new LocationReorderedStruct("", 0).GetHashCode() = -373409742
  new LocationReorderedStruct("", 1).GetHashCode() = -1525218898
  new LocationReorderedStruct("", 2).GetHashCode() = 783693460
  new LocationReorderedStruct("", 3).GetHashCode() = -119359675
  new LocationReorderedStruct("", 4).GetHashCode() = -598013639
  Position is the first field now, so the hash finally varies.
  ("alpha", 7) hash = -1723825111
  ("omega", 7) hash = -1723825111
  same hash?        = True   <- Path is now the ignored field
  but equal?        = False   <- Equals still compares everything

-- Blittable struct (int, int): all fields are used --
  new BlittablePoint(0, 0).GetHashCode() = -2123002223
  new BlittablePoint(0, 1).GetHashCode() = 1598582086
  new BlittablePoint(0, 2).GetHashCode() = -1342592668
  X is identical across all three and the hash still differs.

-- record struct: generated, typed, all-field hashing --
  PersonKey(Alice,25) hash    = 891344700
  PersonKey(Alice,99) hash    = 891344700
  same hash?                  = True   <- only Name was used
  PersonRecord(Alice,25) hash = -2132064524
  PersonRecord(Alice,99) hash = -2132064450
  same hash?                  = False

-- What the collisions cost, 10,000 items --
  HashSet<LocationDefaultStruct> built in  4,195 ms  (10,000 items)
  HashSet<LocationRecordStruct>  built in      4 ms  (10,000 items)
  Same data, same collection type. The only difference is how the key hashes.
```

Every claim in lessons 6 and 7 reproduced. Three things are worth adding.

**The demo includes a control the course does not run.**
Showing that the hash varies once `Position` moves first is suggestive, not conclusive: it rules out "the hash ignores `Position`" but not "the hash uses everything and the earlier struct was special".
The `("alpha", 7)` / `("omega", 7)` pair closes that gap.
Same `Position`, completely different `Path`, **identical hash code** and `Equals` still returning `false`.
So on the reordered struct `Path` is now the ignored field, which is the "first field only" rule stated positively.

**The hash values are mixed and per-process randomized in .NET 10.**
Re-running the demo gives different numbers every time: the empty-string struct hashed `120267509` on one run and `-96275623` on the next.
The blittable `BlittablePoint(0, 1)` hashing to `1598582086` rather than to `1` also says the old "XOR the bits together" description no longer matches the implementation.
None of this changes the lesson: **which fields participate** is what the chapter is about, and that is unchanged.
It does mean you cannot recognise the bug by eyeballing a hash for a suspiciously round number, only by noticing that different values produce the same one.

**Blittability is genuinely the switch.**
`BlittablePoint(0, 0..2)` varies its hash despite `X` being constant, while `LocationDefaultStruct("", 0..4)` does not.
The only difference between those two types is that one holds a `string`.
Adding a single reference-typed field to an existing struct silently changes how every instance of it hashes, which is the version of this trap most likely to reach production: the struct was fine when it was written.

---

## Common misconceptions

**"A record is a value type."**
It is a class unless you write `record struct`.
Two equal records are still two heap objects, and `ReferenceEquals` still says `false`.

**"Structs already do value equality, so `record struct` is just syntax."**
The default version boxes both operands on every comparison and, for any struct holding a reference type, hashes only the first field.
This is the chapter's whole second half, and the measured gap is four orders of magnitude.

**"The first-field hashing thing is a curiosity."**
It is the difference between O(1) and O(N) lookup, and O(N) and O(N²) construction, and it triggers on the completely ordinary case of a struct with a `string` in it.

**"I can exclude a property from a record's equality."**
Not with the generated members. Either hand-write `Equals`/`GetHashCode` and accept the maintenance, or wrap the property in a type that defines its own comparison.

**"A record is immutable."**
Shallowly. `init` stops the property from being reassigned; it says nothing about the object the property points at, and `with` copies the reference.

**"`with` is cheap because it only changes one property."**
It clones the entire instance through the copy constructor, then applies the change.

---

## Self-test

1. Name the five members you have to write by hand to give a class value-based equality, and say which one the compiler will not let you skip without breaking hash collections.
2. `v1.Equals(v2)` is `true` but `v1 == v2` is `false`. What was forgotten?
3. Two `record Point(1, 2)` instances. What do `==`, `Equals`, and `ReferenceEquals` each return, and why is one of them different from the others?
4. What is `EqualityContract`, why is it `virtual`, and what would break without it?
5. `p2 = p1 with { X = 10 }` lowers to what two operations? How many fields get copied?
6. You need `Location` to compare its `Path` case-insensitively. Give the approach the chapter recommends and the two things that make it non-invasive at the call sites.
7. A `readonly struct` with a `string` and an `int`, 10,000 instances with the same string, into a `HashSet`. What is the construction complexity, and how many boxing allocations per comparison?
8. Same struct. Swap the declaration order of the two properties. What changes at runtime, and why is that alarming?
9. When does the default struct `GetHashCode` use all the fields?
10. Give the one-word change that fixes item 7, and list what the compiler then generates differently.

<details>
<summary>Answer key</summary>

1. `IEquatable<T>.Equals(T)`, `override Equals(object?)`, `override GetHashCode()`, `override ToString()`, and the `==` / `!=` operator pair. `GetHashCode` is the non-negotiable one: equal objects must hash equally or `Dictionary` and `HashSet` fail to find items that are present.
2. The `==` operator was not overloaded, so it still compares references. Overriding `Equals` alone leaves the two comparisons disagreeing.
3. `==` and `Equals` are `true` because the generated members compare `X` and `Y`. `ReferenceEquals` is `false` because a record is a class, these are two heap objects, and `ReferenceEquals` cannot be customised.
4. A `protected virtual Type` property returning the record's own type, compared first inside generated `Equals` and folded into `GetHashCode`. It is virtual so derived records override it. Without it a base record and a derived record with matching base fields would compare equal, and `a.Equals(b)` could disagree with `b.Equals(a)`.
5. A call to the compiler-generated virtual `<Clone>$()`, which invokes the protected copy constructor, followed by the `init` setter for `X`. Every field is copied, then the changed one is overwritten.
6. Wrap `Path` in a `readonly record struct NormalizedPath` that implements `Equals`/`GetHashCode` with `StringComparer.OrdinalIgnoreCase`. Non-invasive because of the implicit conversion from `string`, which keeps existing constructor calls compiling, and because `Location` keeps all its generated members.
7. O(N²) to construct, because every element hashes identically, so each insert linearly scans one bucket. Two boxing allocations per comparison, since `ValueType.Equals` boxes both operands.
8. The hash codes start varying, so the `HashSet` behaves correctly and construction returns to roughly O(N). Alarming because reordering two property declarations is a refactor no reviewer would flag, and nothing in the language marks the order as significant.
9. When the struct is blittable: no reference-type fields and no padding gaps. Then the runtime hashes all of its bits.
10. `record struct` (or `readonly record struct`). The compiler then generates a typed `Equals(T)` that does not box, plus `GetHashCode` over every field, `ToString`, `Deconstruct`, and the equality operators, all kept in sync with the declaration.

</details>

---

## Running the demo

The demo is instant. The benchmarks take a few minutes and must be Release.

```bash
cd src/mastering-csharp/06-mastering-records/MasteringCSharp.Records.Demos
dotnet run -c Release                 # all four sections
dotnet run -c Release -- equality     # part 1
dotnet run -c Release -- underhood    # part 2
dotnet run -c Release -- limitations  # part 3
dotnet run -c Release -- structs      # part 4, the interesting one
```

```bash
cd src/mastering-csharp/06-mastering-records/MasteringCSharp.Records.Benchmarks
dotnet run -c Release -- --filter '*LocationContainsBenchmarks*'   # lookup cost, the chapter's benchmark
dotnet run -c Release -- --filter '*HashSetBuildBenchmarks*'       # construction cost, the O(N^2) claim
dotnet run -c Release -- --list flat
```

The `structs` section runs the 10,000-item `HashSet` build twice and takes a few seconds on the default-struct half. That delay is the lesson, not a hang.

---

## Threads into later chapters

| Deferred here | Picked up in |
| --- | --- |
| `readonly record struct` as a cheap wrapper type | Mastering Tuples and Union Types |
| Records as the payload of closed hierarchies | Mastering Pattern Matching (exhaustiveness over records and unions) |
| `Deconstruct` as the hook positional patterns use | Mastering Pattern Matching (recursive patterns) |
| Boxing as a systematic allocation source | Mastering LINQ (the cost of boxed iterators) |
