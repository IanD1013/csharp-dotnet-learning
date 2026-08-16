# Mastering Records

> Course: [Mastering: C#](https://dometrain.com/course/mastering-csharp/) · Chapter 6
> 7 lessons · ~10:49
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Overview](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958863/) | 0:49 | [↓](#1-overview) |
| 2 | [Manual Value-based Equality](https://dometrain.com/take/course/mastering-csharp-3256129/manual-value-based-equality-69958864/) | 0:30 | [↓](#2-manual-value-based-equality) |
| 3 | [Referential and Value-based Equality](https://dometrain.com/take/course/mastering-csharp-3256129/referential-and-value-based-equality-69958865/) | 1:45 | [↓](#3-referential-and-value-based-equality) |
| 4 | [Records Under the Hood](https://dometrain.com/take/course/mastering-csharp-3256129/records-under-the-hood-69958866/) | 1:29 | [↓](#4-records-under-the-hood) |
| 5 | [Records Limitations](https://dometrain.com/take/course/mastering-csharp-3256129/records-limitations-69958867/) | 1:46 | [↓](#5-records-limitations) |
| 6 | [Issues With the Default Structs Equality](https://dometrain.com/take/course/mastering-csharp-3256129/issues-with-the-default-structs-equality-69958868/) | 1:57 | [↓](#6-issues-with-the-default-structs-equality) |
| 7 | [Record Structs vs. Default Structs](https://dometrain.com/take/course/mastering-csharp-3256129/record-structs-vs-default-structs-69958869/) | 2:33 | [↓](#7-record-structs-vs-default-structs) |

---

## 1. Overview

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958863/) · 0:49

### Summary

Records are the primary mechanism for modeling values in C#, offering a more efficient alternative to manually implementing equality members in classes and structs.
This lesson introduces the core concepts of records, including compiler-generated boilerplate for equality and hashing, the shift toward a functional design style that separates data from behavior, and the performance advantages of record structs over default struct equality.
By prioritizing composition over inheritance, developers can create robust, value-oriented data models that avoid common pitfalls like boxing and poor hash distribution.

### Key concepts

* **Value-Based Equality**: Determining equality based on the data contained within an object rather than its memory reference.
* **Compiler-Generated Boilerplate**: Automatic implementation of `Equals`, `GetHashCode`, and `ToString` for record types.
* **Functional Programming Paradigm**: A design approach that separates data structures from the logic that operates on them.
* **Composition over Inheritance**: The practice of building complex values by combining simpler ones instead of using record inheritance.
* **Struct Performance Pitfalls**: Issues with default struct equality, including boxing and field-order-dependent hashing.

### Lesson notes

Records represent the most effective way to model values in C#.
While classes are reference types that use referential equality by default, records shift the focus to the data itself.
This lesson provides an overview of how records replace the need for manual equality implementation and addresses the performance characteristics of both record classes and record structs.

#### Manual Equality and Boilerplate

In standard C# classes, achieving value-based equality requires significant manual effort.
Developers must override `Equals` and `GetHashCode`, and often overload the `==` and `!=` operators to ensure consistent behavior.
Records eliminate this boilerplate by having the compiler automatically generate these members based on the record's properties.
This ensures that two record instances with the same data are treated as equal, regardless of their location in memory.

#### Functional Design Principles

The introduction of records encourages a functional programming style where data and behavior are strictly separated.
This paradigm shift suggests that records should be used as pure data containers.
While C# technically supports inheritance for records, it is generally discouraged in favor of composition.
Composing values allows for more flexible and maintainable designs without the complexities and potential pitfalls of inheritance hierarchies in value-based models.

#### Performance and Struct Equality

The lesson also examines the default behavior of structs.
Standard structs can encounter performance issues during equality checks.
Specifically, the default implementation of `GetHashCode` for structs may only consider the first field.
For example, if a struct has a leading field that rarely changes, all instances may produce the same hash code, leading to collisions.
Furthermore, the default `Equals` implementation often relies on `ValueType.Equals`, which can involve reflection and boxing, significantly impacting performance.
Record structs provide a solution by generating optimized, typed equality and hashing logic that avoids these issues.

---

## 2. Manual Value-based Equality

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/manual-value-based-equality-69958864/) · 0:30

### Summary

In C#, classes default to reference equality, where two instances are equal only if they point to the same object on the heap.
To implement value-based equality—where equality is determined by the object's data—developers must manually implement several members.
This process involves implementing the IEquatable<T> interface, overriding Equals(object) and GetHashCode(), and typically overriding ToString() for better debugging.
For a complete implementation, the equality (==) and inequality (!=) operators should also be overloaded to ensure consistent behavior across all comparison methods.

### Key concepts

- **Reference Equality**: The default class behavior where equality is based on memory identity.
- **Value-based Equality**: Equality determined by the state or properties of an object.
- **IEquatable<T>**: An interface providing a strongly-typed method for equality comparison.
- **GetHashCode()**: A required override when implementing equality to support hash-based collections.
- **Operator Overloading**: Implementing == and != to provide intuitive value-based comparisons.

### Lesson notes

By default, C# classes utilize reference equality.
This means that two separate instances are considered equal only when they point to the exact same heap-allocated object.
While some built-in types like string override this behavior to provide value-based equality, custom classes require manual implementation to achieve the same result.

Consider a standard class definition where equality is reference-based:

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

To implement manual value-based equality, a class should implement the `IEquatable<T>` interface.
This requires providing a type-specific `Equals` method.
Additionally, it is necessary to override the base `Object.Equals(object?)` method and `GetHashCode()`.
Overriding `GetHashCode()` is critical because objects used in hash-based collections (like `Dictionary` or `HashSet`) must produce the same hash code if they are considered equal.

It is also common practice to override `ToString()` to provide a string representation of the object's state, which simplifies debugging and tracing.
For a robust implementation, the equality (`==`) and inequality (`!=`) operators should be overloaded to ensure they agree with the `Equals` method.

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

---

## 3. Referential and Value-based Equality

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/referential-and-value-based-equality-69958865/) · 1:45

### Summary

C# records provide a concise way to create reference types with value-based equality semantics, automatically generating the boilerplate code typically required for such behavior.
While standard classes use reference equality by default—where two distinct instances are considered unequal even if their data is identical—records override equality members to compare the actual content of the properties.
This lesson explores the three primary ways to compare objects (the equality operator, the Equals method, and ReferenceEquals) and demonstrates how a single-line record definition replaces manual implementations of IEquatable<T>, hash code generation, and operator overloading.

### Key concepts

- **Reference Equality**: The default behavior for classes where equality is based on memory address.
- **Value-based Equality**: Equality based on the content (property values) of the object.
- **Comparison Methods**: The three ways to compare values are the equality operator (`==`), the instance `Equals` method, and `Object.ReferenceEquals`.
- **Boilerplate Reduction**: Records replace manual implementations of `IEquatable<T>`, `GetHashCode`, and operator overloading with a single line of code.
- **Compiler Generation**: Records automatically provide `Equals`, `GetHashCode`, `ToString`, deconstruction, and support for non-destructive mutation (`with`).

### Lesson notes

In C#, there are three primary ways to compare objects: the equality operator (`==`), the instance-level `Equals` method, and the static `Object.ReferenceEquals` method.
By default, classes use reference equality.
This means that even if two instances contain the same data, they are not considered equal because they reside at different locations in memory.

```csharp
// === Generated equality: record class vs regular class ===

Console.WriteLine("=== Class equality (reference) ===");
var c1 = new PointClass(1, 2);
var c2 = new PointClass(1, 2);
Console.WriteLine($"c1 == c2:           {c1 == c2}");
Console.WriteLine($"c1.Equals(c2):      {c1.Equals(c2)}");
Console.WriteLine($"ReferenceEquals:    {ReferenceEquals(c1, c2)}");

// --- Types ---

// The default: reference based equality
class PointClass
{
    public int X { get; }
    public int Y { get; }
    public PointClass(int x, int y)
        => (X, Y) = (x, y);
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/referential-and-value-based-equality-69958865/?t=10)

In the example above, `c1 == c2` and `c1.Equals(c2)` both return `false` because the default behavior for classes is to compare references.
`ReferenceEquals` also returns `false` because they are two distinct instances.
Furthermore, the default `ToString` implementation for a class returns the type name (e.g., `PointClass`).

To achieve value-based equality in a standard class, you must manually implement `IEquatable<T>`, override `Equals(object)`, override `GetHashCode`, and overload the `==` and `!=` operators.
If you override the `Equals` method but fail to overload the equality operator, `v1 == v2` will still return `false` while `v1.Equals(v2)` returns `true`.

```csharp
// Manual value-based equality implemented by hand
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

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/referential-and-value-based-equality-69958865/?t=25)

With this manual implementation, `v1.Equals(v2)` returns `true` because the method compares the values of `X` and `Y`.
However, `ReferenceEquals(v1, v2)` still returns `false` because `ReferenceEquals` cannot be customized and correctly identifies them as different instances in memory.
Additionally, a custom `ToString` implementation can be provided for better debugging and tracing.

Records eliminate this boilerplate.
A single line of code defines a reference type with full value semantics.
The compiler generates all the equality members, a content-based `ToString` implementation, and support for deconstruction and non-destructive mutation.

```csharp
record Point(int X, int Y);

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

Records also support inheritance and can be made immutable, allowing for the `with` pattern to create modified copies of an instance without changing the original.

---

## 4. Records Under the Hood

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/records-under-the-hood-69958866/) · 1:29

### Summary

This lesson explores the C# compiler's internal implementation of records, demonstrating how they are transformed into standard classes with specialized boilerplate.
It covers the generation of value-based equality members, the EqualityContract for inheritance safety, init-only properties for immutability, and the underlying mechanisms for non-destructive mutation via the with expression and deconstruction.

### Key concepts

* Records as classes: By default, records are compiled into classes that implement `IEquatable<T>`.
* Equality Contract: A protected property used to ensure type compatibility during equality checks in inheritance scenarios.
* Init-only Properties: Properties are generated with `init` accessors, supporting both constructor initialization and object initializer syntax.
* Value-based Equality: Automatic generation of `Equals`, `GetHashCode`, and equality operators (`==`, `!=`) based on all properties.
* Non-destructive Mutation: The `with` expression uses a hidden `<Clone>$` method and a copy constructor.
* Deconstruction: Positional records automatically receive a `Deconstruct` method.

### Lesson notes

By default, C# records are compiled into classes.
When examining the intermediate language (IL) or decompiled code, a record definition like `record Point(int X, int Y)` reveals a class that implements `IEquatable<Point>`.
The compiler generates private readonly backing fields for the positional parameters and a constructor to initialize them.

```csharp
// Program.cs
var p1 = new Point(1, 2);
var p2 = new Point(1, 2);
Console.WriteLine($"p1 == p2:        {p1 == p2}");
Console.WriteLine($"p1.Equals(p2):   {p1.Equals(p2)}");
Console.WriteLine($"ReferenceEquals: {ReferenceEquals(p1, p2)}");

p2 = p2 with { X = 10 };
Console.WriteLine($"p2: {p2}");
Console.WriteLine($"p1 == p2: {p1 == p2}");

Console.WriteLine();
Console.WriteLine("=== Deconstruction ===");
var (x, y) = p2;
Console.WriteLine($"x = {x}, y = {y}");
p2 = new Point { X = 3, Y = 4 };

record Point(int X, int Y)
{
    public Point()
        : this(0, 0)
    {
    }
}

// IL Viewer (Decompiled Class Structure)
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

The `EqualityContract` is a protected virtual property that returns the type of the record.
This is used during equality checks to ensure that two instances are of the same type, which is particularly important when records are used in inheritance hierarchies.

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

The positional parameters are exposed as properties with `init` accessors.
This design ensures immutability while allowing the use of object initialization syntax if a default constructor is provided.

```csharp
public int X
{
  [CompilerGenerated] get
  {
    return this.<X>k__BackingField;
  }
  [CompilerGenerated] init
  {
    this.<X>k__BackingField = value;
  }
}

public int Y
{
  [CompilerGenerated] get
  {
    return this.<Y>k__BackingField;
  }
  [CompilerGenerated] init
  {
    this.<Y>k__BackingField = value;
  }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/records-under-the-hood-69958866/?t=30)

The compiler also synthesizes a `ToString` method, which can be customized by overriding the `PrintMembers` method.
For equality, the compiler generates `GetHashCode`, `Equals(object)`, `Equals(T)`, and the equality/inequality operators.
These implementations use all properties of the record; it is not possible to exclude specific properties from the compiler-generated equality logic.

```csharp
[NullableContext(2)]
[CompilerGenerated]
[SpecialName]
public static bool op_Equality(Point left, Point right)
{
  if ((object) left == (object) right)
    return true;
  return (object) left != null && left.Equals(right);
}

[CompilerGenerated]
public override int GetHashCode()
{
  return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<X>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Y>k__BackingField);
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/records-under-the-hood-69958866/?t=40)

Non-destructive mutation via the `with` expression is handled by a hidden `<Clone>$` method and a protected copy constructor.
The clone method creates a new instance using the copy constructor, which copies the values of all backing fields from the original instance.

```csharp
[NullableContext(2)]
[CompilerGenerated]
public virtual bool Equals(Point other)
{
  if ((object) this == (object) other)
    return true;
  return (object) other != null && Type.op_Equality(this.EqualityContract, other.EqualityContract) && EqualityComparer<int>.Default.Equals(this.<X>k__BackingField, other.<X>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Y>k__BackingField, other.<Y>k__BackingField);
}

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

When a `with` expression is used, the compiler calls the `<Clone>$` method and then applies the property updates using the `init` accessors.

```csharp
// Decompiled Top-Level Statements showing 'with' and Deconstruction
Point p1 = new Point(1, 2);
Point p2_1 = new Point(1, 2);

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

Positional records also include a `Deconstruct` method, allowing the instance to be unpacked into individual variables using deconstruction syntax.

```csharp
[CompilerGenerated]
public void Deconstruct(out int X, out int Y)
{
  X = this.X;
  Y = this.Y;
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/records-under-the-hood-69958866/?t=85)

---

## 5. Records Limitations

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/records-limitations-69958867/) · 1:46

While C# records provide powerful built-in features like value-based equality and non-destructive mutation, they possess specific limitations regarding equality configuration, performance overhead of the `with` keyword, and shallow immutability.
Developers cannot natively exclude specific properties from equality checks or modify comparison logic without manually overriding `Equals` and `GetHashCode`.
A robust workaround for these limitations involves composing records with specialized types, such as record structs, to encapsulate custom comparison logic while maintaining a clean API through implicit conversions.

### Key concepts

- **Non-configurable equality**: Records automatically include all properties in equality checks; individual properties cannot be excluded or compared using custom logic natively.
- **Performance of `with` expressions**: Every `with` expression creates a complete new instance, which can become expensive in performance-critical paths.
- **Shallow immutability**: Records only ensure the reference or value of a property is immutable; if a property points to a mutable object, that object's state can still be changed.
- **Composition for custom behavior**: Wrapping standard types (like `string`) in specialized record structs allows for custom equality logic without bloating the primary record.
- **Implicit conversions**: Using implicit operators allows custom wrapper types to be used interchangeably with their underlying types in many scenarios.

### Lesson notes

Records are highly effective for many use cases, but they come with inherent limitations.
One primary constraint is that the default equality implementation is not configurable.
You cannot easily skip a property during comparison or change how specific fields, such as strings, are compared.
Additionally, while the `with` expression is a powerful tool for non-destructive mutation in immutable records, it can be expensive because it creates a new instance for every modification.
Finally, record immutability is shallow; if a record contains a mutable property, the contents of that property can still be modified by external code.

#### The Equality Problem

By default, records use the default equality for every member.
For a `string` property, this results in case-sensitive comparison.
In the following example, two `Location` instances with identical paths but different casing are considered unequal:

```csharp
var l1 = new Location(Path: "/users/sergey/readme.md", Position: 42);
var l2 = new Location(Path: "/users/sergey/ReadMe.md", Position: 42);

Console.WriteLine($"l1: {l1}, l2: {l2}");
Console.WriteLine($"l1 == l2: {l1 == l2}");

record Location(string Path, int Position);
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/records-limitations-69958867/?t=40)

#### Implementing Custom Equality via Composition

One way to address this is to manually override `Equals` and `GetHashCode` within the record, but this requires the developer to manually keep those methods in sync with the record's property declarations.

A more maintainable approach is to wrap the property in a specialized type that encapsulates the desired comparison logic.
By using a `readonly record struct`, we can implement custom equality (e.g., case-insensitive string comparison) without significant runtime overhead.

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

#### Refactoring with Implicit Operators

By defining an implicit operator for the wrapper type, we can update the record definition without breaking existing instantiation code.
The `Location` record now uses `NormalizedPath`, and the comparison `l1 == l2` returns `true` because the underlying `NormalizedPath` handles the case-insensitive logic.

```csharp
var l1 = new Location("/users/sergey/readme.md", Position: 42);
var l2 = new Location("/users/sergey/ReadMe.md", Position: 42);

Console.WriteLine($"l1: {l1}, l2: {l2}");
Console.WriteLine($"l1 == l2: {l1 == l2}");

record Location(NormalizedPath Path, int Position);
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/records-limitations-69958867/?t=85)

This pattern demonstrates how to compose records and highlights that records can be implemented as either classes or structs depending on the performance and memory requirements of the application.

---

## 6. Issues With the Default Structs Equality

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/issues-with-the-default-structs-equality-69958868/) · 1:57

### Summary

Default struct equality in C# can lead to significant performance degradation, particularly when used in hash-based collections like HashSet<T> or Dictionary<TKey, TValue>.
For non-blittable structs—those containing reference types or memory gaps—the default GetHashCode implementation only considers the first field.
This behavior causes massive hash collisions, transforming constant-time O(1) lookups into linear O(N) operations and resulting in quadratic O(N²) complexity when building collections.

### Key concepts

- **Blittable vs. Non-blittable structs**: Blittable structs contain no reference types and have no memory gaps between fields; non-blittable structs do.
- **Default GetHashCode behavior**: For non-blittable structs, the default implementation often relies only on the first field of the struct.
- **Hash Collisions**: When multiple distinct objects produce the same hash code, they are stored in the same bucket, leading to linked list chaining.
- **Algorithmic Complexity**: Default struct equality can degrade lookup performance from O(1) to O(N) and collection creation from O(N) to O(N²).

### Lesson notes

The default equality implementation for structs in C# can introduce severe performance bottlenecks.
When a standard struct is used to populate a `HashSet`, the time taken to create the collection can be disproportionately high compared to creating a simple array.
For example, creating an array of 10,000 items might take 1ms, while creating a `HashSet` from those same items can take over 3 seconds.

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

The root of this issue lies in how `GetHashCode` is implemented for structs.
The behavior changes based on whether a struct is "blittable."
A struct is considered blittable if it contains no reference types and has no memory gaps between fields. 

For non-blittable structs—such as `Location`, which contains a `string` (a reference type)—the default implementation of `GetHashCode` only utilizes the first field defined in the struct.
This leads to identical hash codes for different instances if the first field is the same.

```csharp
using System.Diagnostics;

for (int i = 0; i < 5; i++)
{
    var l = new Location(path: "", i);
    Console.WriteLine(l.GetHashCode());
}

// All structs support value-based equality
readonly struct Location
{
    public string Path { get; } // First field used for GetHashCode
    public int Position { get; }

    public Location(string path, int position)
    {
        Path = path;
        Position = position;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/issues-with-the-default-structs-equality-69958868/?t=35)

In the example above, because `Path` is the same for every instance, every instance generates the exact same hash code, regardless of the `Position` value.
If the order of properties is reversed, the hash code changes because a different field becomes the "first" field used for the hash calculation.

```csharp
using System.Diagnostics;

for (int i = 0; i < 5; i++)
{
    var l = new Location(path: "", i);
    Console.WriteLine(l.GetHashCode());
}

// All structs support value-based equality
readonly struct Location
{
    public int Position { get; } // Now the first field
    public string Path { get; }

    public Location(string path, int position)
    {
        Path = path;
        Position = position;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/issues-with-the-default-structs-equality-69958868/?t=55)

When multiple distinct objects share the same hash code, they are stored in the same bucket within a `HashSet` or `Dictionary`, typically as a single linked list.
This forces the collection to perform a linear search using the `Equals` method for every lookup.

```csharp
var locationSet = Enumerable.Range(1, 5)
    .Select(n => new Location(path: "", position: n))
    .ToHashSet();

// Since the hash code is the same for all instances, this lookup has linear complexity
Console.WriteLine(locationSet.Contains(new Location(path: "", position: 6)));
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/issues-with-the-default-structs-equality-69958868/?t=85)

This degradation changes lookup complexity from constant speed O(1) to linear complexity O(N).
When building a `HashSet` of 10,000 items where every item has the same hash code, the complexity of the entire operation becomes quadratic (O(N²)), requiring millions of comparisons and significantly increasing execution time and memory pressure.

---

## 7. Record Structs vs. Default Structs

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/record-structs-vs-default-structs-69958869/) · 2:33

Record structs solve significant performance and correctness issues inherent in default C# structs.
While default structs rely on `ValueType` implementations that often involve boxing and inefficient hashing, record structs provide compiler-generated members that ensure type-safe, non-boxing equality and comprehensive hash code generation.

### Key concepts

- **Boxing Overhead**: Calling `Equals`, `GetHashCode`, or `ToString` on a default struct causes boxing because the implementation resides in `System.ValueType`.
- **Hashing Behavior**: The default `GetHashCode` behavior changes based on whether a struct is blittable. Non-blittable structs (containing reference types like strings) only hash the first field.
- **Hash Collisions**: Because non-blittable default structs only hash the first field, different instances with the same first field but different subsequent fields will produce identical hash codes.
- **Compiler-Generated Members**: Record structs avoid these pitfalls by having the compiler generate specific, typed implementations of equality and hashing that use all fields and avoid boxing.

### Lesson notes

When comparing the performance of record structs against default structs in collection lookups, the differences are stark.
In a `HashSet<T>` lookup scenario, record structs maintain constant complexity, whereas default structs can exhibit significantly degraded performance and high memory allocation.

```csharp
readonly record struct LocationRecordStruct(string Path, int Position);

readonly struct LocationDefaultStruct
{
    public string Path { get; }
    public int Position { get; }

    public LocationDefaultStruct(string path, int position) =>
        (Path, Position) = (path, position);
}

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net10_0, iterationCount: 3, warmupCount: 3)]
[HideColumns("Error", "StdDev", "Median", "RatioSD")]
[Config(typeof(MethodFirstConfig))]
public class LocationContainsBenchmarks
{
    [Params(100, 1_000, 10_000)]
    public int Count { get; set; }

    private HashSet<LocationRecordStruct> _recordStructLocations = null!;
    private HashSet<LocationDefaultStruct> _defaultStructLocations = null!;

    [GlobalSetup]
    public void Setup()
    {
        _recordStructLocations = Enumerable.Range(1, Count)
            .Select(static n => new LocationRecordStruct(Path: "", Position: n))
            .ToHashSet();

        _defaultStructLocations = Enumerable.Range(1, Count)
            .Select(static n => new LocationDefaultStruct(path: "", position: n))
            .ToHashSet();
    }

    [Benchmark(Baseline = true)]
    public bool RecordStructEquality() =>
        _recordStructLocations.Contains(new LocationRecordStruct(Path: "", Position: 0));

    [Benchmark]
    public bool DefaultStructEquality() =>
        _defaultStructLocations.Contains(new LocationDefaultStruct(path: "", position: 0));
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/record-structs-vs-default-structs-69958869/?t=10)

The performance degradation in default structs is primarily due to boxing.
Every call to `Equals` or `GetHashCode` implemented in `ValueType` requires the struct to be boxed.
In a lookup involving 10,000 items, this can result in 20,000 boxing allocations (two per comparison), leading to massive memory pressure.

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/record-structs-vs-default-structs-69958869/?t=46)

Furthermore, the default `GetHashCode` implementation is context-dependent.
If a struct is blittable (contains only value types), all fields are used in the hash computation.
However, if the struct is non-blittable (e.g., it contains a `string`), the runtime optimization only uses the first field for the hash code.
This leads to severe hash collisions if multiple instances share the same first field value.

```csharp
// Default struct GetHashCode problem with non-blittable types
var a = new PersonKey("Alice", 25);
var b = new PersonKey("Alice", 99);
Console.WriteLine($"PersonKey(Alice,25) hash: {a.GetHashCode()}");
Console.WriteLine($"PersonKey(Alice,99) hash: {b.GetHashCode()}");
Console.WriteLine($"Same hash? {a.GetHashCode() == b.GetHashCode()}"); // True — only Name is used!

// Record struct GetHashCode fix
var ra = new PersonRecord("Alice", 25);
var rb = new PersonRecord("Alice", 99);
Console.WriteLine($"PersonRecord(Alice,25) hash: {ra.GetHashCode()}");
Console.WriteLine($"PersonRecord(Alice,99) hash: {rb.GetHashCode()}");
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

Record structs eliminate these issues because the compiler does the heavy lifting.
It generates `Equals` and `GetHashCode` implementations that are in sync with the record declaration, use all properties in the computation, provide a high-quality hash distribution, and never cause boxing allocations during member access.

---

## Running the demo

```bash
cd src/mastering-csharp/06-mastering-records/MasteringCSharp.Records.Demos
dotnet run -c Release                 # all four sections
dotnet run -c Release -- equality     # part 1
dotnet run -c Release -- underhood    # part 2
dotnet run -c Release -- limitations  # part 3
dotnet run -c Release -- structs      # part 4
```

```bash
cd src/mastering-csharp/06-mastering-records/MasteringCSharp.Records.Benchmarks
dotnet run -c Release -- --list flat
dotnet run -c Release -- --filter '*LocationContainsBenchmarks*'
dotnet run -c Release -- --filter '*HashSetBuildBenchmarks*'
```

The demo is instant.
The benchmarks need Release and take a few minutes.
The `structs` section runs the 10,000-item `HashSet` build twice and takes a few seconds on the default-struct half.
