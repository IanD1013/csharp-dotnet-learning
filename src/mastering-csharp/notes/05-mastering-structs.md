# Mastering Structs

> Course: [Mastering: C#](https://dometrain.com/course/mastering-csharp/) · Chapter 5
> 8 lessons · ~8:48
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Overview](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958855/) | 1:06 | [↓](#1-overview) |
| 2 | [Boxing and Indexers](https://dometrain.com/take/course/mastering-csharp-3256129/boxing-and-indexers-69958856/) | 0:27 | [↓](#2-boxing-and-indexers) |
| 3 | [Exploring Different Kind of Copies](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-different-kind-of-copies-69958857/) | 1:42 | [↓](#3-exploring-different-kind-of-copies) |
| 4 | [The Readonly Field Trap](https://dometrain.com/take/course/mastering-csharp-3256129/the-readonly-field-trap-69958858/) | 0:24 | [↓](#4-the-readonly-field-trap) |
| 5 | [Why the Compiler Makes a Copy?](https://dometrain.com/take/course/mastering-csharp-3256129/why-the-compiler-makes-a-copy-69958859/) | 1:25 | [↓](#5-why-the-compiler-makes-a-copy) |
| 6 | [How to Avoid Defensive Copies for Structs?](https://dometrain.com/take/course/mastering-csharp-3256129/how-to-avoid-defensive-copies-for-structs-69958860/) | 1:54 | [↓](#6-how-to-avoid-defensive-copies-for-structs) |
| 7 | [Defensive Copy Overview](https://dometrain.com/take/course/mastering-csharp-3256129/defensive-copy-overview-69958861/) | 1:09 | [↓](#7-defensive-copy-overview) |
| 8 | [Conclusion](https://dometrain.com/take/course/mastering-csharp-3256129/conclusion-69958862/) | 0:41 | [↓](#8-conclusion) |

---

## 1. Overview

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958855/) · 1:06

### Summary

Structs serve as the primary mechanism for defining value types in C#, emphasizing value-based equality where two instances are considered identical if their contents match.
While immutable structs are technically straightforward, mutable structs introduce significant complexity due to non-obvious copying behaviors.
These include boxing allocations when casting to interfaces, accidental mutations of copies returned by properties or indexers, and compiler-generated defensive copies inserted to preserve the state of readonly fields when calling non-readonly members.

### Key concepts

*   **Value Type Semantics**: Structs model values where identity is irrelevant; equality is determined by content.
*   **Boxing Allocations**: Casting a struct to an interface causes the runtime to copy the struct into a heap-allocated object.
*   **Mutation Traps**: Accessing structs through properties or indexers (like `List<T>`) often returns a copy, meaning mutations are applied to the copy rather than the original storage.
*   **Defensive Copies**: The compiler automatically creates temporary copies of structs in `readonly` contexts to prevent non-readonly members from potentially modifying the original state.

### Lesson notes

Structs are the canonical way of creating value types in C#.
They are specifically designed to model values where the identity of the instance is not relevant.
This leads to distinct equality semantics: two struct instances are equal when their content is exactly the same.

While immutable structs are simple to manage, mutable structs introduce complexities because copies can occur in multiple ways.
One of the most common is a boxing allocation.
When a struct instance is cast to an interface, the runtime copies the content of the struct into a heap-allocated object.
Any subsequent mutations performed through that interface affect the boxed copy, not the original value type.

```csharp
// === Boxing: mutation on a copy ===

// Explicit boxing
var counter = new Counter();
((IIncrementable)counter).Increment();
((IIncrementable)counter).Increment();
Console.WriteLine(counter.Value);
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958855/?t=40)

Other cases of copying are less obvious, such as when dealing with properties or indexers.
For instance, accessing a struct through a standard `List<T>` indexer returns a copy of the value.
Attempting to mutate that return value results in a "mutation trap" where only the temporary copy is modified.

```csharp
// List and Array
var list = new List<Counter> { new() };
list[0].Increment();
Console.WriteLine($"List element after Increment: {list[0].Value}");

var array = new Counter[1];
array[0].Increment();
Console.WriteLine($"Array element after Increment: {array[0].Value}");
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958855/?t=49)

The most complex scenario involves defensive copies.
To preserve invariance, the compiler emits defensive copies when a struct is stored in a `readonly` context (such as a `readonly` field) and a member is called that is not explicitly marked as `readonly`.
Because the compiler cannot guarantee that a non-readonly member won't mutate the state, it creates a temporary copy to perform the operation on, leaving the original field untouched.
This behavior can lead to silent bugs where code appears to execute correctly but the state never updates.

```csharp
struct SequenceReader
{
    private int _position;

    public int Position => _position;

    public bool TryAdvance()
    {
        if (_position >= 5) return false;
        _position++;
        return true;
    }
}

class Parser
{
    private readonly SequenceReader _reader;

    public void Parse()
    {
        for (int i = 0; i < 3; i++)
        {
            bool advanced = _reader.TryAdvance();
            Console.WriteLine(_reader.Position);
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958855/?t=55)

---

## 2. Boxing and Indexers

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/boxing-and-indexers-69958856/) · 0:27

### Summary

This lesson explores the unintended consequences of boxing and indexer behavior when working with mutable structs in C#.
It demonstrates that casting a struct to an interface results in boxing, where mutations are applied to a heap-allocated copy rather than the original instance.
Furthermore, the lesson highlights a critical difference between collection types: while `List<T>` indexers return a copy of a struct (preventing in-place mutation), array indexers provide direct access to the memory location, allowing the struct to be modified as expected.

### Key concepts

- **Interface Boxing**: Casting a struct to an interface boxes the value, creating a separate object on the heap.
- **Mutation on Copies**: Methods called on a boxed interface reference mutate the boxed copy, leaving the original local struct unchanged.
- **List Indexer Behavior**: The `List<T>` indexer is a property that returns a value by copy for structs.
- **Array Indexer Behavior**: Arrays have special runtime support allowing indexers to return a reference to the element's storage location.
- **Ref Indexers**: Custom collections can use the `ref` return type on indexers to allow in-place mutation of struct elements.

### Lesson notes

When a struct implements an interface, it remains a value type.
However, casting that struct to the interface type triggers a boxing operation.
This creates a copy of the struct on the heap.
If the interface defines methods that mutate the state of the struct, calling those methods on the interface reference will only affect the boxed copy.

In the following example, the `Counter` struct implements `IIncrementable`.
When `counter` is cast to `IIncrementable`, the `Increment` method is called on a boxed instance.
Consequently, the `Value` of the original `counter` remains 0.

```csharp
interface IIncrementable { void Increment(); }

struct Counter : IIncrementable
{
    public int Value { get; private set; }
    public void Increment() => Value++;
}

// Explicit boxing: mutation occurs on a boxed copy, not the original instance
var counter = new Counter();
((IIncrementable)counter).Increment();
((IIncrementable)counter).Increment();
Console.WriteLine(counter.Value); // Output: 0

// List indexer: returns a copy of the struct
List<Counter> list = [new()];
list[0].Increment();
Console.WriteLine(list[0].Value); // Output: 0

// Array indexer: provides direct access to the element memory
Counter[] array = [new()];
array[0].Increment();
Console.WriteLine(array[0].Value); // Output: 1
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/boxing-and-indexers-69958856/?t=10)

#### Indexer Differences

The behavior of indexers depends on the implementation of the collection.
For `List<T>`, the indexer is a standard property.
When `T` is a struct, the indexer returns the element by value (a copy).
Calling a mutating method like `Increment()` on `list[0]` modifies that temporary copy, which is immediately discarded.
This is why the value in the list remains unchanged.

Arrays are handled differently by the CLR.
An array indexer provides a reference to the actual memory location of the element.
This allows `array[0].Increment()` to mutate the struct instance stored directly within the array.

#### Implementing Ref Indexers

To achieve array-like behavior in custom collections, you can define an indexer that returns a reference using the `ref` keyword.
This allows callers to mutate struct elements inside the collection without copying them.

```csharp
class RefList<T> : IEnumerable<T>
{
    private T[] _items;
    private int _count;

    public RefList(int capacity = 4) { _items = new T[capacity]; }

    public void Add(T item)
    {
        if (_count == _items.Length)
        {
            Array.Resize(ref _items, _items.Length * 2);
        }
        _items[_count++] = item;
    }

    // Returning by reference allows in-place mutation of structs
    public ref T this[int index] => ref _items[index];

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < _count; i++)
        {
            yield return _items[i];
        }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
}
```

---

## 3. Exploring Different Kind of Copies

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-different-kind-of-copies-69958857/) · 1:42

### Summary

This lesson explores how struct copying behavior varies depending on how the struct is accessed and manipulated.
It demonstrates that casting a struct to an interface results in boxing, creating a heap-allocated copy that isolates mutations from the original instance.
Furthermore, it contrasts the behavior of collection indexers, showing that while List<T> indexers return a copy of a struct, array indexers provide a direct reference to the underlying memory location, allowing for in-place mutation.

### Key concepts

*   **Boxing Mutation**: Casting a struct to an interface creates a heap-allocated copy (boxing). Mutations performed on the interface reference affect the boxed copy, not the original struct.
*   **Pass-by-Value**: By default, .NET passes structs by value. This applies to method arguments, return values, properties, and most indexers.
*   **List<T> Indexer**: The indexer for `List<T>` returns the type `T`. For structs, this means a temporary copy is created upon access.
*   **Array Indexer**: Unlike standard collections, array indexers return a reference to the actual memory location of the element, enabling direct mutation of the struct within the array.

### Lesson notes

When working with structs, it is critical to understand when the runtime creates a copy versus when it allows access to the original instance.
A common source of confusion occurs when a struct implements an interface and is subsequently cast to that interface type.

#### Boxing and Interface Casting

When a struct is cast to an interface, the runtime performs a boxing operation.
This creates a temporary instance on the heap and copies the content of the struct into that heap-allocated object.
If a mutating method is called on the interface reference, it modifies the state of the boxed object.
The original stack-allocated struct remains unchanged.

#### Collection Indexer Behavior

There is a significant difference in how `List<T>` and arrays handle struct elements.
Although they may appear similar, their indexers have different signatures and behaviors:

1.  **List<T>**: The indexer returns `T`. When `T` is a struct, accessing `list[0]` creates a temporary copy. Calling a mutating method like `Increment()` on `list[0]` modifies this temporary copy, which is immediately discarded. Consequently, the value stored inside the list remains unchanged.
2.  **Arrays**: The array indexer returns a reference to the underlying location within the array. When you call a mutating method on an array element, you are changing the actual data stored in that memory slot.

The following code demonstrates these behaviors using a `Counter` struct that implements an `IIncrementable` interface:

```csharp
// === Boxing: mutation on a copy ===

// Explicit boxing
var counter = new Counter();
((IIncrementable)counter).Increment();
((IIncrementable)counter).Increment();
Console.WriteLine(counter.Value);

Console.WriteLine();

// List and Array
var list = new List<Counter> { new() };
list[0].Increment();
Console.WriteLine($"List element after Increment: {list[0].Value}");

var array = new Counter[1];
array[0].Increment();
Console.WriteLine($"Array element after Increment: {array[0].Value}");

Console.WriteLine();
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-different-kind-of-copies-69958857/?t=10)

In the boxing example, `counter.Value` remains `0` because the `Increment()` calls were executed on separate boxed copies.
In the collection example, the list element remains `0` because the indexer returned a copy, whereas the array element is successfully incremented to `1` because the array indexer provided a reference to the original storage location.

---

## 4. The Readonly Field Trap

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/the-readonly-field-trap-69958858/) · 0:24

The 'Readonly Field Trap' is a common pitfall in C# when using mutable structs within readonly fields.
When a method or property is accessed on a struct stored in a readonly field, the compiler creates a defensive copy of the struct to ensure the original field remains immutable.
This results in any state changes (mutations) being applied only to the temporary copy and then discarded, leaving the original struct's state unchanged.
This behavior can lead to logical errors, such as infinite loops or incorrect data processing, and introduces performance penalties due to repeated memory copying.

### Key concepts

- **Defensive Copying**: The mechanism where the C# compiler copies a struct to a temporary variable before calling a member on a readonly field to preserve the field's immutability.
- **Mutable Structs**: Structs that contain methods or properties that modify their internal state, which are particularly susceptible to this trap.
- **Readonly Invariant**: The language guarantee that a readonly field cannot be modified after the constructor completes.
- **State Loss**: The phenomenon where mutations appear to fail because they were executed on a discarded temporary copy rather than the intended field.

### Lesson notes

In C#, marking a struct field as `readonly` does not simply make the struct's members immutable; instead, it changes how the compiler interacts with that field.
If a struct is mutable—meaning it has methods that change its internal fields—using it within a `readonly` field leads to the "Readonly Field Trap."

Consider a `SequenceReader` struct designed to track a position within a data source.
It has a `TryAdvance` method that increments an internal `_position` field.
This reader is then used as a `readonly` field within a `Parser` class.

```csharp
var parser = new Parser();
parser.Parse();

struct SequenceReader
{
    private int _position;

    public int Position => _position;

    public bool TryAdvance()
    {
        if (_position >= 5) return false;
        _position++;
        return true;
    }
}

class Parser
{
    private readonly SequenceReader _reader;

    public void Parse()
    {
        for (int i = 0; i < 3; i++)
        {
            bool advanced = _reader.TryAdvance();
            Console.WriteLine($" Advanced: {advanced}, Position: {_reader.Position}");
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/the-readonly-field-trap-69958858/?t=10)

In the `Parser` class, the `_reader` field is marked as `readonly`.
When the `Parse` method calls `_reader.TryAdvance()`, the compiler must ensure that `_reader` itself is not modified, as that would violate the `readonly` constraint.
Because `SequenceReader` is a struct (a value type), the compiler handles this by creating a hidden defensive copy of `_reader` on the stack.
It then calls `TryAdvance()` on this temporary copy.

As a result, the `_position` field inside the copy is incremented, but the `_position` field in the actual `_reader` field remains `0`.
In the next iteration of the loop, the process repeats: a new copy of the original (unmodified) `_reader` is made, `TryAdvance()` is called on it, and the original remains unchanged.
This leads to a situation where the reader never actually advances, and the output will consistently show a position of `0` despite the method returning `true`.

---

## 5. Why the Compiler Makes a Copy?

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/why-the-compiler-makes-a-copy-69958859/) · 1:25

### Summary

When a struct is stored in a readonly field, the C# compiler enforces immutability by creating a defensive copy whenever a non-readonly member is accessed.
Because struct instance members conceptually receive the current instance as a mutable reference, the compiler cannot guarantee that a method or property getter won't modify the struct's state.
To protect the readonly field from mutation, the compiler copies the struct to a temporary local variable and executes the member on that copy, which can lead to unexpected behavior where state changes are lost and performance is degraded.

### Key concepts

- **Defensive Copying**: The process where the compiler creates a temporary copy of a struct to prevent mutation of a `readonly` field.
- **Readonly Field Guarantees**: For structs, `readonly` guarantees that the actual data within the struct cannot be changed, unlike reference types where only the reference itself is immutable.
- **Mutable `this`**: In a standard struct, the `this` parameter passed to instance members is a mutable reference (`ref`), allowing any member to reassign the entire instance.
- **Compiler Analysis**: The compiler automatically injects copying logic when it detects a potential mutation path through a non-readonly member access on a readonly field.

### Lesson notes

Consider a `SequenceReader` struct designed to track a position within a data source.
It contains a `TryAdvance` method that increments a private `_position` field.

```csharp
// A light-weight reader for processing
// data from a data source.
struct SequenceReader
{
    private int _position;

    public int Position => _position;

    public bool TryAdvance()
    {
        if (_position >= 5) return false;
        _position++;
        return true;
    }
}

class Parser
{
    private readonly SequenceReader _reader;

    public void Parse()
    {
        for (int i = 0; i < 3; i++)
        {
            bool advanced = _reader.TryAdvance();
            Console.WriteLine(_reader.Position);
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/why-the-compiler-makes-a-copy-69958859/?t=5)

In the `Parser` class, the `_reader` is marked as `readonly`.
When executing the `Parse` method, the `Position` remains unchanged despite calls to `TryAdvance`.
This happens because the compiler identifies that `TryAdvance` is a member of a struct stored in a `readonly` field.
To ensure the `readonly` constraint is not violated, the compiler generates Intermediate Language (IL) that copies `_reader` into a temporary variable and calls `TryAdvance` on that temporary instance.
The original `_reader` field remains untouched.

#### The Mechanics of Struct Mutation

To understand why the compiler is so conservative, it is helpful to view instance members conceptually.
An instance member in a struct is essentially a static member where the first parameter is `this`.
For structs, this parameter is passed by reference and is mutable.

```csharp
// This is what a Distance property conceptually is
public static double get_Distance(ref Point @this)
{
    @this = new Point(1, 2);
    return 0;
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/why-the-compiler-makes-a-copy-69958859/?t=45)

Because every member of a struct can potentially reassign `this` (and thus change the current instance), it conflicts with the `readonly` modifier on a field.
While a `readonly` field of a reference type only prevents changing the reference (the pointer), a `readonly` field of a struct type attempts to prevent any change to the instance's data.

#### Example: Mutation in a Property

Even property getters, which are typically expected to be side-effect free, can legally mutate a struct if the struct or the member is not explicitly marked `readonly`.

```csharp
struct Point
{
    public int X { get; }
    public int Y;

    public Point(int x, int y) { X = x; Y = y; }

    public double Distance
    {
        get
        {
            // legal for a non-readonly struct member
            this = new Point(1, 2);
            return 0;
        }
    }
}

class Holder
{
    private readonly Point _point = new(3, 4);

    public void Show()
    {
        Console.WriteLine($"X: {_point.X}");
        Console.WriteLine($"Y: {_point.Y}");
        Console.WriteLine($"Distance: {_point.Distance}");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/why-the-compiler-makes-a-copy-69958859/?t=45)

In this example, accessing `_point.Distance` triggers a defensive copy.
If the compiler did not do this, the `readonly` field `_point` would be overwritten by `new Point(1, 2)` inside the getter.
By creating a copy, the compiler ensures `_point` remains `(3, 4)` while allowing the code to execute.

---

## 6. How to Avoid Defensive Copies for Structs?

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/how-to-avoid-defensive-copies-for-structs-69958860/) · 1:54

Defensive copies are hidden performance costs incurred when the C# compiler copies a struct to protect its state from potential mutation during member access.
This occurs in readonly contexts, such as when a struct is passed as an in parameter or stored in a readonly field, and a non-readonly member is invoked.
By explicitly marking members or the entire struct as readonly, developers provide the compiler with the necessary guarantees to access the struct directly by reference, bypassing the need for a temporary copy.

### Key concepts

- Defensive copies in `readonly` contexts (`in` parameters, `readonly` fields).
- The `readonly` modifier on struct members (methods and properties).
- `readonly struct` declarations for global immutability.
- Compiler behavior regarding field vs. property access.
- Identifying copies via IL (Intermediate Language) inspection.

### Lesson notes

When working with structs in C#, the compiler often creates "defensive copies" to ensure that a struct marked as `readonly` is not inadvertently mutated.
This typically happens in two contexts: when a struct is passed via an `in` parameter or when it is stored in a `readonly` field.

#### Defensive Copies with `in` Parameters

When a struct is passed using the `in` modifier, it is passed by reference for performance, but the compiler must guarantee it remains immutable.
If you call a property or method that is not explicitly marked `readonly`, the compiler creates a copy of the struct and calls the member on that copy instead.
This ensures the original struct cannot be changed by the method call.

```csharp
using System;
public struct Point
{
    public int X { get; }
    public int Y { get; }

    public Point(int x, int y) { X = x; Y = y; }

    public double Distance => Math.Sqrt(X * X + Y * Y);
}

public class C {

    public void M( Point p) {

        Console.WriteLine(p.Distance);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/how-to-avoid-defensive-copies-for-structs-69958860/?t=40)

In the example above, if the method signature is changed to `public void M(in Point p)`, the access to `p.Distance` triggers a defensive copy because the `Distance` property is not marked `readonly`.

#### Marking Members as `readonly`

To prevent the compiler from creating these copies, you can apply the `readonly` modifier directly to the member.
This informs the compiler that the member does not mutate the struct's state, allowing it to call the member directly on the reference.

```csharp
using System;
public struct Point
{
    public int X { get; }
    public int Y { get; }

    public Point(int x, int y) { X = x; Y = y; }

    public readonly double Distance => Math.Sqrt(X * X + Y * Y);
}

public class C {

    public void M(in Point p) {

        Console.WriteLine(p.X);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/how-to-avoid-defensive-copies-for-structs-69958860/?t=55)

The compiler does not create defensive copies when accessing fields or auto-properties directly, as these are known to be safe.
However, if a property has a custom getter that is not marked `readonly`, the defensive copy returns.

```csharp
using System;
public struct Point
{
    public int X => 42;
    public int Y { get; }

    public Point(int x, int y) { X = x; Y = y; }

    public readonly double Distance => Math.Sqrt(X * X + Y * Y);
}

public class C {

    public void M(in Point p) {

        Console.WriteLine(p.X);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/how-to-avoid-defensive-copies-for-structs-69958860/?t=65)

#### Using `readonly struct`

A more comprehensive approach is to mark the entire struct as `readonly`.
This makes all members (including properties) implicitly `readonly`.
Once the struct itself is `readonly`, the compiler no longer needs to generate defensive copies for any member access.

```csharp
using System;
public readonly struct Point
{
    public int X => 42;
    public int Y { get; }

    public Point(int x, int y) { Y = y; }

    public readonly double Distance => Math.Sqrt(X * X + Y * Y);
}

public class C {

    public void M(in Point p) {

        Console.WriteLine(p.X);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/how-to-avoid-defensive-copies-for-structs-69958860/?t=80)

#### Defensive Copies in `readonly` Fields

Defensive copies also occur when accessing members of a struct stored in a `readonly` field.
While these copies might not be visible in lowered C# code in some tools, they are clearly visible in the Intermediate Language (IL).
In IL, the compiler emits a local variable to store the copy before the method call.

```csharp
using System;
public struct Point
{
    public int X => 42;
    public int Y { get; }

    public Point(int x, int y) { Y = y; }

    public double Distance => Math.Sqrt(X * X + Y * Y);
}

public class C {
    private  Point _p;
    public void M(in Point p) {

        Console.WriteLine(_p.Distance);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/how-to-avoid-defensive-copies-for-structs-69958860/?t=105)

If the `readonly` modifier is removed from the field, or if the member being accessed is marked `readonly`, the local variable in the IL disappears, indicating that the defensive copy has been eliminated and the field is being accessed directly.

---

## 7. Defensive Copy Overview

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/defensive-copy-overview-69958861/) · 1:09

### Summary

Defensive copies are a mechanism used by the C# compiler to preserve the immutability of structs within read-only contexts.
When a struct is stored in a readonly field or passed via in or ref readonly modifiers, the compiler ensures that any access to its members does not mutate the original instance.
If the compiler cannot verify that a member is non-mutating—such as a standard property or method in a non-readonly struct—it creates a temporary defensive copy of the struct to perform the operation on.
While this ensures correctness, it can introduce performance overhead, particularly with large structs or within tight loops.

### Key concepts

- **Read-only contexts**: Defensive copies are triggered in contexts where the struct is considered immutable, including `readonly` fields, `in` parameters, `ref readonly` parameters, and `ref readonly` locals.
- **Compiler Proof**: If the compiler can prove that accessing a member cannot mutate the instance, it will not emit a defensive copy.
- **Safe Accessors**: Accessing fields, auto-properties, and members explicitly marked with the `readonly` modifier does not trigger copies.
- **Readonly Structs**: Declaring a struct as a `readonly struct` guarantees all members are non-mutating, allowing the compiler to avoid defensive copies for all member accesses.
- **Performance**: Unnecessary copies of large structs can lead to performance degradation, making `readonly struct` the preferred design choice for immutable data.

### Lesson notes

The C# compiler prioritizes correctness when handling structs in read-only contexts.
If the compiler can prove that a member access is non-mutating, it accesses the member directly.
This is why accessing fields, auto-properties, or members specifically marked with the `readonly` modifier is efficient.

```csharp
// Accessing X won't cause a copy
public int X { get; }
// Accessing Y won't cause a copy
public int Y;

// Accessing Distance won't cause a copy because it is marked readonly
public readonly double Distance
    => Math.Sqrt(X * X + Y * Y);

// Accessing any members of a readonly struct won't cause a copy
readonly struct Point
{
    public int X { get; }
    public int Y { get; }
    public double Distance => Math.Sqrt(X * X + Y * Y);
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/defensive-copy-overview-69958861/?t=10)

When a struct is not marked `readonly`, the compiler must assume that any property or method call could potentially mutate the state of the struct (by modifying `this`).
To prevent this mutation in a read-only context, the compiler creates a defensive copy and calls the member on that copy instead of the original instance.

There are four primary contexts where these defensive copies occur:
1. `readonly` fields.
2. `in` parameters.
3. `ref readonly` parameters.
4. `ref readonly` locals.

Under the hood, `in` and `ref readonly` parameters are implemented as `ref` parameters with additional metadata (`[In][IsReadOnly]`).
The compiler uses this metadata to enforce safety.
If it sees an access to a member that is not explicitly marked `readonly`, it injects the defensive copy.

```csharp
private static void ByIn(in Point point)
{
    // A defensive copy is created because Distance is not readonly
    Console.WriteLine(point.Distance);
}

// The compiler effectively transforms the above into:
private static void ByIn_Decompiled([In][IsReadOnly] ref Point point)
{
    Point point2 = point; // Defensive copy
    Console.WriteLine(point2.Distance);
}

private static void ByRefReadonly(ref readonly Point point)
{
    // A defensive copy is created
    Console.WriteLine(point.Distance);
}

// Read-only local context
Point[] points = [new()];
ref readonly var first = ref points[0];
// A defensive copy is created
Console.WriteLine(first.Distance);
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/defensive-copy-overview-69958861/?t=40)

To avoid the performance implications of these copies, especially when working with large structs or in high-frequency loops, it is best practice to make structs `readonly` whenever possible.
This makes the design intent explicit and ensures the compiler can optimize member access.

---

## 8. Conclusion

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/conclusion-69958862/) · 0:41

### Summary

This lesson concludes the exploration of value semantics in C# structs, highlighting the complexities that arise when dealing with mutable types.
While immutable structs (especially those defined as readonly struct or with readonly members) are straightforward, mutable structs are prone to accidental "copy-mutation" bugs.
These occur when a developer attempts to modify a struct instance that has been copied—either through boxing, property accessors, indexers, or compiler-generated defensive copies designed to protect readonly fields and contexts.

### Key concepts

- Value vs. reference semantics in mutation.
- Boxing as a source of hidden copies.
- Defensive copies in `readonly` contexts.
- The impact of property getters and indexers on struct copies.
- `readonly struct` and `readonly` members as tools for performance and correctness.

### Lesson notes

Value semantics in C# can be more complex than they initially appear.
While immutable structs—particularly those defined as `readonly struct` or containing `readonly` members—behave predictably, mutable structs introduce significant risks.
The primary danger is accidentally mutating a copy of a struct rather than the intended instance.

Copies occur in several common scenarios, including boxing allocations, getting a struct instance from a property getter or an indexer, or because the compiler injects a defensive copy to preserve immutability invariants.

#### Boxing and Indexer Copies

Converting a struct to an interface type creates a boxed copy on the heap.
Any mutations performed through the interface reference affect the boxed copy, not the original value.
Similarly, accessing a struct through a standard property getter or a `List<T>` indexer returns a copy of the struct by value.
In contrast, arrays and `ref`-returning indexers allow direct mutation of the stored instance.

```csharp
// Boxing: mutation on a copy
var counter = new Counter();
((IIncrementable)counter).Increment(); // Mutates a boxed copy
Console.WriteLine(counter.Value); // Original remains 0

// List indexer returns by value
var list = new List<Counter> { new() };
list[0].Increment(); // Mutates a temporary copy returned by the indexer
Console.WriteLine(list[0].Value); // Original in list remains 0
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/conclusion-69958862/?t=21)

#### Defensive Copies

When a mutable struct is stored in a `readonly` field or accessed in a `readonly` context (such as an `in` parameter or a `ref readonly` local), the compiler may inject a defensive copy.
This ensures that calling a non-readonly member does not violate the `readonly` constraint of the container.
However, this results in mutations being applied to a hidden local copy, leaving the original field unchanged.

```csharp
struct SequenceReader
{
    private int _position;
    public bool TryAdvance()
    {
        if (_position >= 5) return false;
        _position++;
        return true;
    }
}

class Parser
{
    private readonly SequenceReader _reader; // Readonly field

    public void Parse()
    {
        // TryAdvance is called on a defensive copy of _reader
        bool advanced = _reader.TryAdvance();
        // _reader.Position remains 0 because the mutation happened on a copy
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/conclusion-69958862/?t=21)

To avoid these issues, developers should prefer immutable structs or explicitly mark members as `readonly` to signal to the compiler that no defensive copy is required.
The next section will cover the default equality behavior of structs and the associated performance implications.
