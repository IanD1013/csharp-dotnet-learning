# Mastering Tuples and Union Types

> Course: [Mastering: C#](https://dometrain.com/course/mastering-csharp/) · Chapter 7
> 10 lessons · ~16:58
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Overview](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958870/) | 0:39 | [↓](#1-overview) |
| 2 | [System.Tuple vs. System.ValueTuple](https://dometrain.com/take/course/mastering-csharp-3256129/system-tuple-vs-system-valuetuple-69958871/) | 1:15 | [↓](#2-systemtuple-vs-systemvaluetuple) |
| 3 | [Tuples in C#](https://dometrain.com/take/course/mastering-csharp-3256129/tuples-in-csharp-69958872/) | 3:01 | [↓](#3-tuples-in-c) |
| 4 | [Knowing the Limits: When to Move to Actual Types](https://dometrain.com/take/course/mastering-csharp-3256129/knowing-the-limits-when-to-move-to-actual-types-69958873/) | 0:48 | [↓](#4-knowing-the-limits-when-to-move-to-actual-types) |
| 5 | [Union Types: Why Do We Need Them?](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-why-do-we-need-them-69958874/) | 1:20 | [↓](#5-union-types-why-do-we-need-them) |
| 6 | [Union Types: the Basics](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-the-basics-69958875/) | 1:31 | [↓](#6-union-types-the-basics) |
| 7 | [Union Types Under the Hood](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-under-the-hood-69958876/) | 1:21 | [↓](#7-union-types-under-the-hood) |
| 8 | [Union Types: the Deep Dive](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-the-deep-dive-69958877/) | 4:12 | [↓](#8-union-types-the-deep-dive) |
| 9 | [UnionTypes - Recap](https://dometrain.com/take/course/mastering-csharp-3256129/uniontypes-recap-69958878/) | 1:23 | [↓](#9-uniontypes---recap) |
| 10 | [Conclusion](https://dometrain.com/take/course/mastering-csharp-3256129/conclusion-69958879/) | 1:28 | [↓](#10-conclusion) |

---

## 1. Overview

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958870/) · 0:39

### Summary

This lesson provides an overview of advanced type modeling in C#, focusing on the evolution of tuples and the introduction of union types.
It contrasts the heap-allocated System.Tuple with the stack-allocated System.ValueTuple, demonstrating how tuple syntax enables structural equality, deconstruction, and pattern matching.
The lesson also addresses the limitations of tuples, suggesting when to transition to record types for better domain modeling, and previews the upcoming C# 15 union types, including their syntax and underlying implementation as object-backed structs.

### Key concepts

*   **System.Tuple vs. System.ValueTuple**: Understanding the difference between reference-type tuples and value-type tuples.
*   **Tuple Semantics**: Leveraging name inference, mutability, and structural equality.
*   **Deconstruction and Patterns**: Using tuples for expressive code through deconstruction and switch expressions.
*   **Record Transition**: Identifying when tuples become too complex and should be replaced by record types.
*   **Union Types (C# 15)**: A preview of native union types for modeling mutually exclusive data states.
*   **Union Implementation**: How the compiler handles union types using the `[Union]` attribute and object-backed storage.

### Lesson notes

#### Tuples and ValueTuples

C# supports two primary tuple implementations.
`System.Tuple` is a reference type that is heap-allocated and lacks the modern syntax features found in newer versions of the language.
In contrast, `System.ValueTuple` is a value type that supports named elements and is the target for modern C# tuple syntax.

```csharp
// === System.Tuple vs System.ValueTuple ===

Console.WriteLine("=== Tuple vs ValueTuple ===");

// System.Tuple: heap allocated. No extra semantics
Tuple<string, int> refTuple = Tuple.Create("api.example.com", 443);
// System.ValueTuple: value type. Named elements for extra clarity
(string host, int port) valTuple = ("api.example.com", 443);

Console.WriteLine($"Tuple<string,int>:  {refTuple}         (reference type)");
Console.WriteLine($"ValueTuple:         {valTuple}         (value type)");
Console.WriteLine($"ValueTuple type:    {valTuple.GetType().Name}");

Console.WriteLine();
Console.WriteLine("=== Named tuples ===");
var endpoint = (host: "api.example.com", port: 443);
Console.WriteLine($"endpoint.host = {endpoint.host}");
Console.WriteLine($"endpoint.port = {endpoint.port}");

Console.WriteLine();
Console.WriteLine("=== Names are compile-time only ===");
(string host, int port) parsed = endpoint;
(string server, int number) renamed = parsed;


Console.WriteLine($"renamed.server = {renamed.server}");
Console.WriteLine($"renamed.number = {renamed.number}");
Console.WriteLine($"Runtime type:    {renamed.GetType().Name}");
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958870/?t=4)

#### Tuple Features and Deconstruction

Tuples provide several features to make code more readable, including name inference from variables and support for deconstruction.
While tuples allow for naming elements, these names are erased at compile time and do not affect the runtime type identity.
Tuples also provide structural equality and member-based hashing out of the box.

```csharp
using System;
using EndpointAlias = (string host, int port);

#nullable disable

#region Name inference and mutability

var host = "api.example.com";
var port = 443;
// Names could be inferred from variables
var endpoint = (name: host, port);

// Tuples are mutable
endpoint.port++;
endpoint.name = string.Empty;

// Names are not part of the type
(string addr, int portNum) = endpoint;
// The names are erased at compile time
string server = endpoint.Item1;

// System.ValueTuple`2[System.String,System.Int32]
Console.WriteLine(endpoint.GetType());

// The compiler emits metadata to track the names of tuple elements
var parsedEndpoint = TupleMetadataDemo.ParseEndpoint("api.example.com:444");
Console.WriteLine($"{parsedEndpoint.host}:{parsedEndpoint.port}");
#endregion

#region Aliases

// Local endpoint is only visible in the current file
EndpointAlias e = ("host", port);
// GlobalEndpoint is visible in the entire assembly.
GlobalEndpoint e2 = ("host", port);
#endregion

#region Tuple equality, GetHashCode, and ToString

var p1 = (x: 1, y: 2);
var p2 = (x: 1, y: 3);
Console.WriteLine(p1 == p2); // false

p2.y = p1.y;
Console.WriteLine(p1 == p2); // true
Console.WriteLine(p1.GetHashCode() == p2.GetHashCode()); // true

// ToString() is supported, but the names are gone!
Console.WriteLine($"p1: {p1}"); // (1, 2)

#endregion

#region Deconstruction and tuple patterns

// Deconstruction
(host, port) = endpoint;
// Discards
var (_, p) = endpoint;
Console.WriteLine($"{host}:{port}");

// Pattern Matching
var description = (host, port) switch
{
    ("api.example.com", 443) => "production HTTPS endpoint",
    (_, 443) => "HTTPS endpoint",
    _ => "other endpoint",
};
Console.WriteLine(description);

#endregion
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958870/?t=8)

#### Transitioning to Records

While tuples are useful for local data structures, they have limitations as domain types.
For instance, extension methods defined for a specific tuple shape (e.g., `(string, int)`) will appear on all tuples with that same signature, regardless of their semantic meaning.
In such cases, using a `record struct` or `record` provides better encapsulation and clearer `ToString` behavior.

```csharp
using System;

var endpoint = (host: "api.example.com", port: 443);
Console.WriteLine($"Tuple ToString: {endpoint}");
Console.WriteLine($"Extension method: {endpoint.ToEndpointString()}");

var retryPolicy = (name: "attempts", count: 3);
Console.WriteLine($"Same extension on another tuple: {retryPolicy.ToEndpointString()}");

var typedEndpoint = new Endpoint("api.example.com", 443);
Console.WriteLine($"Record struct ToString: {typedEndpoint}");

internal static class EndpointTupleExtensions
{
    // Extending every tuple with string and int
    public static string ToEndpointString(this (string host, int port) endpoint) =>
        $"{endpoint.host}:{endpoint.port}";
}

// Defining a lightweight type
internal readonly record struct Endpoint(string Host, int Port)
{
    public override string ToString() =>
        $"{Host}:{Port}";
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958870/?t=13)

#### Union Types (C# 15 Preview)

Union types allow a single variable to hold one of several different types.
This is particularly useful for modeling results or lookup keys that could be an ID (integer) or a name (string).
The syntax uses the `union` keyword, and the compiler handles the logic for switching between the possible types.

```csharp
// LookupKey = int Id | string Name
public readonly union LookupKey(int Id, string Name)
{
    public string Describe() => this switch
    {
        int id => $"id {id}",
        string name => $"name {name}",
        null => "uninitialized",
    };
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958870/?t=17)

#### Compilation and Under the Hood

Under the hood, the `union` declaration generates a struct decorated with a `[Union]` attribute.
It typically uses an `object? Value` field to store the active case, which means value types like `int` may be boxed when stored in the union.
Pattern matching is used to recover the active case safely.

```csharp
using System.Runtime.CompilerServices;

// Readable version of the shape generated for:
// public union LookupKey(int Id, string Name)
[Union]
public readonly struct UnionDecompiled
{
    public object? Value { get; }

    // Case constructors for creating an instance from 'int'
    public UnionDecompiled(int value) => Value = value;

    // Case constructors for creating an instance from 'string'
    public UnionDecompiled(string value) => Value = value;

    public string Describe()
    {
        object? value = Value;

        if (value is int id)
        {
            return $"id {id}";
        }

        if (value is string name)
        {
            return $"name {name}";
        }

        if (value is null)
        {
            return "uninitialized";
        }

        throw new SwitchExpressionException(this);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958870/?t=34)

---

## 2. System.Tuple vs. System.ValueTuple

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/system-tuple-vs-system-valuetuple-69958871/) · 1:15

### Summary

C# provides two distinct ways to group related data without defining a named type: the legacy System.Tuple and the modern System.ValueTuple.
System.Tuple is a heap-allocated reference type introduced in .NET 4.0 that lacks language-level syntax and forces the use of generic element names like Item1, which can hinder code readability.
In contrast, System.ValueTuple, introduced in C# 7.0, is a stack-allocated struct that supports concise syntax, named elements, and deconstruction.
Because it avoids heap allocations and offers superior language integration, System.ValueTuple is the preferred choice for modern C# development.

### Key concepts

- `System.Tuple` is a reference type (class) introduced in .NET 4.0.
- `System.ValueTuple` is a value type (struct) introduced in C# 7.0.
- `System.Tuple` requires heap allocation and uses fixed element names (`Item1`, `Item2`).
- `System.ValueTuple` provides direct language support, including concise syntax and named elements.
- `System.ValueTuple` is the preferred choice for modern C# development due to performance and readability.

### Lesson notes

Tuples are a fundamental concept in many programming languages, used to group related data elements without the overhead of defining a formal named type.
A typical example is grouping a host string and a port integer for network operations.

C# offers two implementations for tuples.
The first is `System.Tuple`, introduced in .NET Framework 4.0.
The second is `System.ValueTuple`, which became available in C# 7.0.

#### System.Tuple

`System.Tuple` is a reference type (a class).
Consequently, creating a tuple instance results in a heap allocation, which can be a performance consideration in high-throughput applications.
Furthermore, `System.Tuple` lacks integrated language support.
Elements are not nameable and must be accessed via the properties `Item1`, `Item2`, and so on.
This limitation often makes code harder to read and maintain.
For these reasons, `System.Tuple` should generally be avoided in modern applications.

#### System.ValueTuple

`System.ValueTuple` is a value type (a struct), meaning it is typically stack-allocated and does not incur the garbage collection overhead of heap allocations.
It is the modern standard for tuples in C# and features extensive language support.
This support allows for a concise syntax, the ability to name elements for better clarity, and the use of deconstruction and pattern matching.

```csharp
// System.Tuple: heap allocated. No extra semantics
Tuple<string, int> refTuple = Tuple.Create("api.example.com", 443);
// System.ValueTuple: value type. Named elements for extra clarity
(string host, int port) valTuple = ("api.example.com", 443);
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/system-tuple-vs-system-valuetuple-69958871/?t=10)

When used locally, `System.ValueTuple` significantly improves code readability and maintainability.
It allows developers to group data on the fly while maintaining the expressiveness of named properties, which can then be deconstructed or used within pattern matching expressions.

---

## 3. Tuples in C#

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/tuples-in-csharp-69958872/) · 3:01

### Summary

C# tuples provide a lightweight way to group multiple data elements into a single structure.
They support name inference, are mutable by default, and their identity is defined by the types and order of elements rather than their names.
While names are erased at runtime, the compiler uses metadata attributes to preserve them for development and public APIs.
Tuples are particularly useful for composite dictionary keys, variable swapping, and simplifying boilerplate in equality and hash code implementations.

### Key concepts

- **Name Inference**: The compiler can automatically assign tuple element names based on the variable names used to initialize them.
- **Mutability**: Tuples in C# (ValueTuple) are mutable, allowing elements to be modified after initialization.
- **Type Identity**: A tuple's type is determined by the types and order of its elements; element names are not part of the formal type signature.
- **Metadata Preservation**: The `TupleElementNamesAttribute` allows the compiler to persist element names in assembly metadata for use across assembly boundaries.
- **Aliasing**: Tuples can be aliased locally with `using` or globally with `global using` to improve code readability.
- **Value-based Equality**: Tuples implement value-based equality, making them ideal for composite keys in collections.

### Lesson notes

Tuples allow you to combine multiple variables into a single unit.
You can optionally provide names for these elements; otherwise, the compiler can infer names automatically from the variable names used during construction.
Because tuples are just combinations of variables, they are mutable, allowing you to use increment operators or assign new values to individual elements.

```csharp
#region Name inference and mutability

var host = "api.example.com";
var port = 443;
// Names could be inferred from variables
var endpoint = (name: host, port);

// Tuples are mutable
endpoint.port++;
endpoint.name = string.Empty;

// Names are not part of the type
(string addr, int portNum) = endpoint;
// The names are erased at compile time
string server = endpoint.Item1;

// System.ValueTuple`2[System.String,System.Int32]
Console.WriteLine(endpoint.GetType());

// The compiler emits metadata to track the names of tuple elements
var parsedEndpoint = TupleMetadataDemo.ParseEndpoint("api.example.com:444");
Console.WriteLine($"{parsedEndpoint.host}:{parsedEndpoint.port}");
#endregion
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/tuples-in-csharp-69958872/?t=10)

An important distinction in C# is that tuple element names are not part of the underlying type.
You can assign a tuple to another tuple with different element names as long as the types and order match.
At runtime, names are erased, and elements are accessed via built-in names like `Item1` or `Item2`.
However, to maintain readability, you should use descriptive element names.

When a tuple is defined in another assembly, the compiler knows the element names because it emits a `TupleElementNamesAttribute`.
This attribute persists in the assembly metadata, allowing the consumer to see the names used in the return value.

```csharp
internal static class TupleMetadataDemo
{
    public static (string host, int port) ParseEndpoint(string endpoint)
    {
        var separatorIndex = endpoint.LastIndexOf(':');

        return (
            endpoint.Substring(0, separatorIndex),
            int.Parse(endpoint.Substring(separatorIndex + 1)));
    }

    public static GlobalEndpoint ParseGlobalEndpoint(string endpoint)
    {
        var separatorIndex = endpoint.LastIndexOf(':');
        return (
            endpoint.Substring(0, separatorIndex),
            int.Parse(endpoint.Substring(separatorIndex + 1)));
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/tuples-in-csharp-69958872/?t=100)

To avoid duplicating tuple definitions, you can use aliases.
A local alias is visible only within the current source file, while a global alias is available throughout the entire project.
Note that while element names are preserved across assembly boundaries via attributes, the name of the global alias itself is not preserved in the metadata of public APIs.

```csharp
using EndpointAlias = (string host, int port);

#region Aliases

// Local endpoint is only visible in the current file
EndpointAlias e = ("host", port);
// GlobalEndpoint is visible in the entire assembly.
GlobalEndpoint e2 = ("host", port);
#endregion
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/tuples-in-csharp-69958872/?t=70)

Tuples are highly effective when used as composite keys in dictionaries or hash sets because they implement value-based equality out of the box.

```csharp
#region Composite keys

// Tuples make excellent composite keys because they implement value-based equality
var routes = new Dictionary<EndpointAlias, string>
{
    [("api.example.com", 443)] = "production",
    [("api.example.com", 8443)] = "staging",
    [("admin.example.com", 443)] = "admin",
};

var allowedEndpoints = new HashSet<(string Host, int Port)>
{
    ("api.example.com", 443),
    ("admin.example.com", 443),
};

#endregion
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/tuples-in-csharp-69958872/?t=120)

Another common use case is swapping two variables without an explicit temporary variable.
The compiler generates the same optimized code (using a hidden temporary variable) as the manual approach.

```csharp
void Swap(int left, int right)
{
    (right, left) = (left, right);
    Console.WriteLine(left);
    Console.WriteLine(right);
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/tuples-in-csharp-69958872/?t=145)

Finally, tuples can be used as an implementation detail to simplify constructors and equality logic.
You can assign multiple properties in a single expression or implement `GetHashCode` and `Equals` by delegating to a tuple.
This is particularly useful in environments like the full .NET Framework where `HashCode.Combine` might not be available, as tuple syntax provides a similar benefit across all versions of .NET that support tuples.

```csharp
internal readonly struct Location : IEquatable<Location>
{
    public string Path { get; }
    public int Position { get; }

    public Location(string path, int position) =>
        (Path, Position) = (path, position);

    public bool Equals(Location other) =>
        (Path, Position) == (other.Path, other.Position);

    public override int GetHashCode() =>
        // Similar to HashCode.Combine, but tuples can be used in full framework as well!
        (Path?.GetHashCode() ?? 0, Position).GetHashCode();

    public override bool Equals(object obj) =>
        obj is Location other && Equals(other);
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/tuples-in-csharp-69958872/?t=175)

---

## 4. Knowing the Limits: When to Move to Actual Types

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/knowing-the-limits-when-to-move-to-actual-types-69958873/) · 0:48

### Summary

Tuples are effective for local data grouping and tactical operations like pattern matching, but they lack the type identity required for robust public APIs or domain modeling.
When logic like extension methods is needed for a specific data shape, or when the same tuple structure is reused frequently, transitioning to a nominal type like a record struct ensures type safety and prevents logic from leaking to unrelated tuples with the same underlying types.

### Key concepts

- Tactical usage of tuples for local grouping and composite keys.
- Limitations of tuples in public APIs and domain modeling.
- Type identity vs. structural identity: tuple element names are not part of the type.
- Extension method leakage across structurally identical tuples.
- Transitioning to nominal types like `record struct` for encapsulation.

### Lesson notes

Tuples serve as excellent tactical tools for local implementation details.
They are frequently used to combine multiple variables, create composite keys, or group items within a single collection.
Additionally, they are useful for pattern matching, expression-based constructor initialization, and implementing hash code or equality logic.

However, tuples should be replaced with nominal types (such as classes or records) when they are used to model core domain concepts in public APIs, when they become excessively large, or when they are used very frequently throughout the application. 

A primary indicator that a tuple has reached its limit is the need for an extension method.
Because tuple element names are not part of the underlying type, an extension method defined for a specific tuple shape—such as `(string host, int port)`—will be available to every tuple with the same sequence of types, regardless of the element names or the intended domain concept.

```csharp
using System;

var endpoint = (host: "api.example.com", port: 443);
Console.WriteLine($"Tuple ToString: {endpoint}");
Console.WriteLine($"Extension method: {endpoint.ToEndpointString()}");

var retryPolicy = (name: "attempts", count: 3);
// The extension method is available here because element names are not part of the type
Console.WriteLine($"Same extension on another tuple: {retryPolicy.ToEndpointString()}");

var typedEndpoint = new Endpoint("api.example.com", 443);
Console.WriteLine($"Record struct ToString: {typedEndpoint}");

internal static class EndpointTupleExtensions
{
    // Extending every tuple with string and int
    public static string ToEndpointString(this (string host, int port) endpoint) =>
        $"{endpoint.host}:{endpoint.port}";
}

// Defining a lightweight type
internal readonly record struct Endpoint(string Host, int Port)
{
    public override string ToString() =>
        $"{Host}:{Port}";
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/knowing-the-limits-when-to-move-to-actual-types-69958873/?t=10)

In the example above, the `ToEndpointString` method is intended for host/port pairs.
However, because it targets the underlying `ValueTuple<string, int>`, it also appears on the `retryPolicy` tuple, which represents attempts and counts.
To resolve this and provide proper encapsulation, a `readonly record struct` should be used.
This creates a distinct nominal type where logic can be safely contained without leaking to unrelated data structures.

---

## 5. Union Types: Why Do We Need Them?

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-why-do-we-need-them-69958874/) · 1:20

### Summary

Union types (or sum types) represent a value that can be one of several distinct types, providing an "exclusive OR" relationship between properties.
Unlike traditional classes or structs that combine multiple properties into a single instance, unions allow for modeling variant types like a Result (Success or Error) or a LookupKey (ID or Name).
This feature, introduced in C# 15, enables exhaustive pattern matching and supports functional programming patterns where data and behavior are separate, making error handling and data modeling more robust and expressive.

### Key concepts

*   **Product Types vs. Sum Types**: Classes, structs, and tuples are "AND" types (product types) where all properties coexist; Unions are "OR" types (sum types) where only one case is valid at a time.
*   **Variant Modeling**: Unions are ideal for modeling types that have mutually exclusive states, such as a operation result that is either a success or an error.
*   **Exhaustive Pattern Matching**: Union types allow the compiler to verify that all possible cases of a type are handled in a switch expression or statement.
*   **Functional Programming Influence**: Unions facilitate a functional style where data structures are separated from the logic that operates on them.
*   **C# 15 Implementation**: Native union support allows for a closed set of types that the compiler can trust for exhaustiveness checks.

### Lesson notes

In C#, standard types such as classes, structs, records, and tuples are considered "product types."
This means they combine multiple properties together.
For example, a `Point` type with `X` and `Y` properties will always have both values present in every instance.
However, many programming scenarios require an exclusive set of properties where a value can be one thing or another, but not both. 

Union types (or sum types) provide a pattern to model these variant types.
They are commonly used in functional programming where data and behavior are kept separate.
One of the most practical examples is a `Result` type, which represents either a successful operation with a value or a failure with an error message or exception.

```csharp
public record class Result<T> : Result<T>.IUnionMembers
{
    object? _value;

    public interface IUnionMembers
    {
        public static Result<T> Create(T value) => new() { _value = value };
        public static Result<T> Create(Exception value) => new() { _value = value };

        public object? Value { get; }
    }

    object? IUnionMembers.Value => _value;
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-why-do-we-need-them-69958874/?t=55)

While C# has traditionally relied on exceptions for error handling, result-based error handling is gaining popularity.
Union types make this adoption easier by providing a formal way to define these results.
In C# 15, union types allow developers to express values from a closed set of types.
This ensures that pattern matching is exhaustive, meaning the compiler can guarantee that every possible case of the union has been handled.

For example, a `LookupKey` can be defined as either an `int` ID or a `string` Name.
Using the C# 15 preview syntax, this is expressed as a `union` type:

```csharp
// LookupKey = int Id | string Name
public readonly union LookupKey(int Id, string Name)
{
    public string Describe() => this switch
    {
        int id => $"id {id}",
        string name => $"name {name}",
        null => "uninitialized",
    };
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-why-do-we-need-them-69958874/?t=43)

Under the hood, the compiler generates a structure to manage these variants.
This generated code typically includes a `Value` property and implicit conversions to allow seamless assignment from the underlying types (e.g., assigning an `int` directly to a `LookupKey`).

```csharp
[Union]
public readonly struct UnionDecompiled
{
    public object? Value { get; }

    // Case constructors for creating an instance from 'int'
    public UnionDecompiled(int value) => Value = value;

    // Case constructors for creating an instance from 'string'
    public UnionDecompiled(string value) => Value = value;

    public string Describe()
    {
        object? value = Value;

        if (value is int id)
        {
            return $"id {id}";
        }

        if (value is string name)
        {
            return $"name {name}";
        }

        if (value is null)
        {
            return "uninitialized";
        }

        throw new SwitchExpressionException(this);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-why-do-we-need-them-69958874/?t=61)

By using union types, developers can create more expressive APIs where the possible return types or input types are explicitly defined and enforced by the compiler, reducing the reliance on runtime checks and exceptions.

---

## 6. Union Types: the Basics

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-the-basics-69958875/) · 1:31

### Summary

Union types, introduced as a preview feature in C# 15 and .NET 11, allow developers to define a closed set of types that a single variable can represent.
Implemented as a readonly struct by the compiler, union types do not require specific runtime support, making them compatible with older target frameworks.
They facilitate type-safe programming by providing implicit conversions from their constituent types and enforcing exhaustive pattern matching within switch expressions, including handling for the uninitialized default state.

### Key concepts

* **Union Keyword**: A new keyword used to define a type that can hold one of several specified types.
* **Closed Set**: The compiler knows all possible types in a union, allowing for exhaustiveness checks.
* **Implicit Conversions**: The compiler generates constructors that allow constituent types (e.g., int or string) to be implicitly converted to the union type.
* **Struct Implementation**: Unions are compiled into structs, which means they can be created using default and may contain a null value internally.
* **Exhaustive Pattern Matching**: Switch expressions must handle every case defined in the union, as well as the null case, to avoid compiler warnings.

### Lesson notes

Union types are a feature of C# 15, released alongside .NET 11.
Although they are currently in preview, the core implementation is stable.
Notably, union types do not require specific runtime support, allowing them to be used with older target frameworks, including the full .NET Framework.

To define a union type, use the union keyword followed by the type name and a list of cases.
For example, a LookupKey can be defined as either an int Id or a string Name.
While this syntax resembles a constructor, it actually defines the list of types that are convertible to the union.

```csharp
var entries = new Entries();

// An implicit conversion from int to LookupKey
LookupKey key = 42;

Console.WriteLine(key.Describe());
var result = entries.Lookup(key);

// LookupKey = int Id | string Name
public readonly union LookupKey(int Id, string Name)
{
    public string Describe() => this switch
    {
        int id => $"id {id}",
        string name => $"name {name}",
        null => "uninitialized",
    };
}

public sealed class Entries
{
    public Entry Lookup(LookupKey key) => key switch
    {
        int id => byId[id],
        string name => byName[name],
        null => throw new InvalidOperationException("Lookup key must be initialized before it is processed."),
    };

    private readonly Dictionary<int, Entry> byId = new()
    {
        [42] = new Entry(42, "Launch Plan"),
        [1001] = new Entry(1001, "Archive Policy"),
    };

    private readonly Dictionary<string, Entry> byName;

    public Entries()
    {
        byName = new Dictionary<string, Entry>
        {
            ["Launch Plan"] = byId[42],
            ["Archive Policy"] = byId[1001],
        };
    }
}

public sealed record Entry(int Id, string Name);
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-the-basics-69958875/?t=40)

The compiler generates constructors for each case, enabling implicit conversions from the constituent types to the union type.
This allows a string to be assigned directly to a LookupKey variable.

```csharp
// An implicit conversion from string to LookupKey
LookupKey key = "foo";
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-the-basics-69958875/?t=55)

The primary way to interact with union types is through pattern matching, specifically using switch expressions.
Because union types represent a closed set of types, the compiler can enforce exhaustiveness.
If a case is omitted from a switch expression, the compiler issues a warning indicating that the pattern is not exhaustive.

```csharp
public readonly union LookupKey(int Id, string Name)
{
    public string Describe() => this switch
    {
        int id => $"id {id}",
        // If the string case is removed, the compiler warns that the case is missing
        // string name => $"name {name}",
        null => "uninitialized",
    };
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-the-basics-69958875/?t=70)

Under the hood, the compiler implements a union as a struct.
Because structs can be instantiated using the default keyword or a default constructor, a union instance can exist in an uninitialized state where its internal value is null.
To ensure safety, the compiler requires the null case to be handled in switch expressions.
If the null case is omitted, the compiler will emit a warning, and failing to handle it at runtime could lead to errors.

```csharp
// A union can be created with a default expression
LookupKey key = default;

public readonly union LookupKey(int Id, string Name)
{
    public string Describe() => this switch
    {
        int id => $"id {id}",
        string name => $"name {name}",
        // If the null case is not handled, the compiler emits a warning
        // null => "uninitialized",
    };
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-the-basics-69958875/?t=85)

---

## 7. Union Types Under the Hood

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-under-the-hood-69958876/) · 1:21

### Summary

Union types in C# are implemented by the compiler as readonly struct types that store their state in a single object? Value property.
This implementation strategy allows for flexible storage of different types within the same structure but introduces boxing allocations when a value type, such as an int, is used as a union case.
By examining the decompiled code and running memory benchmarks, the performance characteristics of union types reveal that while they provide type safety and expressive pattern matching, developers must be aware of the 24-byte allocation overhead associated with boxing value types.

### Key concepts

*   **Compiler Generation**: Union types are transformed into `readonly struct` types marked with a `[Union]` attribute.
*   **State Management**: The underlying value is stored in an `object? Value` property, which acts as a container for any of the union's cases.
*   **Boxing**: Because the internal storage is an `object`, assigning a value type (like `int`) to a union results in a boxing allocation.
*   **Pattern Matching**: Methods like `Describe` or custom switch expressions are compiled into type checks against the internal `Value` property.
*   **Implicit Conversions**: The compiler generates constructors and implicit conversion operators to allow seamless assignment from case types to the union type.

### Lesson notes

Under the hood, the C# compiler generates a `readonly struct` for union types.
This struct is decorated with a `[Union]` attribute and implements the `IUnion` interface.
It contains a constructor for each case and stores the state in a `Value` property. 

```csharp
// SimpleUnionTypes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// LookupKey
using System.Runtime.CompilerServices;

[Union]
public readonly struct LookupKey : IUnion
{
    public object? Value { get; }

    public string Describe()
    {
        ...
    }

    [CompilerGenerated]
    public LookupKey(int value)
    {
        Value = value;
    }

    [CompilerGenerated]
    public LookupKey(string value)
    {
        Value = value;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-under-the-hood-69958876/?t=5)

A more readable version of the decompiled code shows how the `Describe` method and constructors function.
The `Value` property is an `object?`, which means any value type assigned to it will be boxed.
The `Describe` method essentially checks the type of the value and throws an exception if no matches are found.

```csharp
using System.Runtime.CompilerServices;

// Readable version of the shape generated for:
// public union LookupKey(int Id, string Name)
[Union]
public readonly struct UnionDecompiled
{
    public object? Value { get; }

    // Case constructors for creating an instance from 'int'
    // Compile adds an implicit conversion
    // to allow LookupKey key = 42;
    public UnionDecompiled(int value) => Value = value;

    // Case constructors for creating an instance from 'string'
    // Compile adds an implicit conversion
    // to allow LookupKey key = "foo";
    public UnionDecompiled(string value) => Value = value;

    public string Describe()
    {
        object? value = Value;

        if (value is int id)
        {
            return $"id {id}";
        }

        if (value is string name)
        {
            return $"name {name}";
        }

        // The generated struct can still be default-initialized, so Value may be null
        // even though null is not one of the declared union cases.
        if (value is null)
        {
            return "uninitialized";
        }

        // Logically unreachable for `Id | Name`; kept to preserve switch-expression exhaustiveness semantics
        throw new SwitchExpressionException(this);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-under-the-hood-69958876/?t=30)

To assess the performance impact of this implementation, particularly the boxing of value types, benchmarks can be used to compare direct lookups against lookups using union types. 

```csharp
[MemoryDiagnoser]
[SimpleJob]
[HideColumns("StdDev", "RatioSD", "Gen0", "Alloc Ratio")]
public class LookupBenchmarks
{
    private readonly Entries entries = new();

    [Benchmark(Baseline = true)]
    public int DirectById() => entries.Lookup(42).Id;

    [Benchmark]
    public int DirectByName() => entries.Lookup("Launch Plan").Id;

    [Benchmark]
    public int LookupKeyById() => entries.Lookup((LookupKey)42).Id;

    [Benchmark]
    public int LookupKeyByName() => entries.Lookup((LookupKey)"Launch Plan").Id;
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-under-the-hood-69958876/?t=40)

The `Entries` class handles the logic for looking up values using either primitive types or the `LookupKey` union.
When looking up by `LookupKey`, the implementation switches on the internal value.

```csharp
public sealed class Entries
{
    private readonly Dictionary<int, Entry> byId = new()
    {
        [42] = new Entry(42, "Launch Plan"),
        [1001] = new Entry(1001, "Archive Policy"),
    };

    private readonly Dictionary<string, Entry> byName;

    public Entries()
    {
        byName = new Dictionary<string, Entry>
        {
            ["Launch Plan"] = byId[42],
            ["Archive Policy"] = byId[1001],
        };
    }

    public Entry Lookup(int id) => byId[id];

    public Entry Lookup(string name) => byName[name];

    public Entry Lookup(LookupKey key) => key switch
    {
        int id => Lookup(id),
        string name => Lookup(name),
        null => throw new InvalidOperationException("Lookup key must be initialized before it is processed."),
    };
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-under-the-hood-69958876/?t=55)

Running these benchmarks with a memory diagnoser reveals that `LookupKeyById` incurs a 24-byte allocation.
This is the exact size of a boxed `int` on a 64-bit system.
In contrast, `LookupKeyByName` does not incur additional transient allocations because `string` is already a reference type and does not require boxing when stored in the `object? Value` field.

---

## 8. Union Types: the Deep Dive

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-the-deep-dive-69958877/) · 4:12

### Summary

This lesson explores advanced implementation details of union types in C#, focusing on manual definitions to avoid boxing, targeting older frameworks via polyfills, and managing the default state of struct-based unions.
It demonstrates how to implement custom equality and string representations using pattern matching, and highlights the differences between struct-based and class-based union implementations, particularly regarding nullability and exhaustiveness checks.

### Key concepts

* **Framework Polyfilling**: Enabling union types on older .NET versions by manually defining `UnionAttribute` and `IUnion` and using the preview language version.
* **Manual Union Definition**: Creating custom structs or classes with the `[Union]` attribute to control internal storage and behavior.
* **Boxing Avoidance**: Implementing the `TryGetValue` pattern to allow the compiler to access union cases without boxing value types into an `object`.
* **Default State Management**: Using nullable backing fields (e.g., `int?`) in struct-based unions to distinguish between a valid zero-value case and an uninitialized `default` state.
* **Custom Equality**: Implementing `IEquatable<T>` and overriding equality members using pattern matching to provide robust comparison logic.
* **Class-based Unions**: Utilizing classes for unions to eliminate the uninitialized state inherent to structs, thereby simplifying exhaustiveness checks in switch expressions.

### Lesson notes

#### Targeting Older Frameworks

Union types can be used even when targeting older frameworks.
The C# compiler relies on duck typing for union support, expecting specific types to be available during compilation regardless of their source.
To enable this, you must manually define the `UnionAttribute` and the `IUnion` interface.

```csharp
namespace System.Runtime.CompilerServices;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
public sealed class UnionAttribute : Attribute;

public interface IUnion
{
    object? Value { get; }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-the-deep-dive-69958877/?t=25)

Additionally, the project file must be configured to use the `preview` language version and a compatible SDK (version 11 or higher), even if the target framework is older (e.g., .NET 10).

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <LangVersion>preview</LangVersion>
  </PropertyGroup>

</Project>
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-the-deep-dive-69958877/?t=10)

#### Manual Union Implementation and Boxing

While the compiler can generate union types automatically, manual implementation allows for greater control, specifically regarding performance.
The standard compiler-generated union stores state in an `object Value` property, which causes boxing when the union is created from a value type. 

By manually defining a struct with the `[Union]` attribute and providing `TryGetValue` methods for each case, the compiler can use these members for pattern matching, avoiding the boxing penalty associated with the `Value` property.

```csharp
using System.Runtime.CompilerServices;

// The shorthand `union` declaration always generates a struct.
// Manual `[Union]` types can choose their own storage and can be structs or classes.
[Union]
public readonly record struct LookupKey
{
    private readonly int? id;
    private readonly string? name;

    public LookupKey(int id)
    {
        this.id = id;
        name = null;
    }

    public LookupKey(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        id = default;
        this.name = name;
    }

    // Union recognition is pattern-based. The compiler looks for public members by shape;
    // implementing IUnion is not required for this manual union today.
    // Making Value non-nullable also tells the compiler that the union contents cannot be null.
    // With this storage convention, default(LookupKey) is treated as the id case with value 0.
    public object? Value => name is not null ? name : id;

    // TryGetValue is the non-boxing access pattern. Pattern matching can use these
    // members instead of reading Value, which would box the int case.
    public bool TryGetValue(out int value)
    {
        value = id.GetValueOrDefault();
        return id is not null;
    }

    public bool TryGetValue(out string value)
    {
        value = name!;
        return name is not null;
    }

    public string Describe() => this switch
    {
        int value => $"id {value}",
        string value => $"name {value}",
        _ => "<uninitalized>"
    };

    public override string ToString() => Describe();
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-the-deep-dive-69958877/?t=175)

#### Handling the Default State

Because every struct can be created using a `default` expression, struct-based unions have an uninitialized state.
If a union case uses a value type like `int`, the default value (0) might be indistinguishable from a valid instance created with 0.
Using a nullable field (e.g., `int? id`) internally allows the union to separate the "zero" case from the "uninitialized" case in switch expressions.

```csharp
LookupKey idKey = 0;
LookupKey defaultKey = default;

Console.WriteLine(idKey.Equals(defaultKey));

Console.WriteLine($"idKey(0): {idKey}");
Console.WriteLine($"default: {defaultKey}");
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-the-deep-dive-69958877/?t=145)

#### Custom Equality and Records

Compiler-generated unions do not include `ToString` or equality members by default.
However, unions are regular types and can implement interfaces like `IEquatable<T>`.
By using a `record struct` for a manual union, you can leverage record equality.
Alternatively, you can implement custom equality logic using pattern matching.

```csharp
public union MyLookup(int Id, string Name) : IEquatable<MyLookup>
{
    public string Describe() => this switch
    {
        int value => $"id {value}",
        string value => $"name {value}",
        _ => "<uninitalized>"
    };

    public override string ToString()
    {
        return Describe();
    }
    public bool Equals(MyLookup other) 
        => (this, other) switch
        {            (int a, int b) => a == b,
            (string a, string b) => string.Equals(a, b, StringComparison.Ordinal),
            _ => true,
        };

    public override bool Equals(object? obj) => obj is MyLookup other && Equals(other);

    public override int GetHashCode() => this switch
    {
        int id => HashCode.Combine(typeof(int), id),
        string name => HashCode.Combine(typeof(string), name),
        _ => 0,
    };

    public static bool operator ==(MyLookup left, MyLookup right) => left.Equals(right);
    public static bool operator !=(MyLookup left, MyLookup right) => !left.Equals(right);
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-the-deep-dive-69958877/?t=160)

#### Class-Based Unions

While the shorthand `union` keyword generates a struct, manually authored unions can be classes.
Classes do not support the `default` expression in the same way structs do, meaning they cannot exist in an uninitialized state.
If the `Value` property is non-nullable, the compiler can verify that all cases are handled without requiring a discard (`_`) or "uninitialized" branch in switch expressions.

```csharp
using System.Runtime.CompilerServices;

[Union]
public sealed class ClassLookupKey
{
    private readonly int id;
    private readonly string? name;

    public ClassLookupKey(int id)
    {
        this.id = id;
        name = null;
    }

    public ClassLookupKey(string name)
    {
        ArgumentNullException.ThrowIfNull(name);

        id = default;
        this.name = name;
    }

    public object Value => name is not null ? name : id;

    public bool TryGetValue(out int value)
    {
        value = id;
        return name is null;
    }

    public bool TryGetValue(out string value)
    {
        value = name!;
        return name is not null;
    }

    public string Describe() => this switch
    {
        int id => $"id {id}",
        string name => $"name {name}"
    };
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-the-deep-dive-69958877/?t=235)

---

## 9. UnionTypes - Recap

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/uniontypes-recap-69958878/) · 1:23

Union types in C# provide a mechanism to model a closed set of alternative types, allowing a single type to hold one of several specified types, such as an integer or a string.
They can be implemented using the native union keyword or by applying the [Union] attribute to existing types like classes, records, or structs.
While the compiler handles much of the generation, developers should be aware of boxing allocations when using value types and the current lack of generated equality and string formatting members.
Notably, this feature does not require specific runtime support, making it compatible with older .NET frameworks including the full .NET Framework.

### Key concepts

* Union types as closed sets of alternatives.
* Implementation via the native `union` keyword.
* Implementation via the `[Union]` attribute on classes, records, or structs.
* Internal storage using an `object? Value` property, leading to boxing for value types.
* Current limitations: No automatic `ToString()` or equality member generation.
* Performance implications of default struct equality and `GetHashCode()`.
* Broad compatibility across .NET versions, including .NET Framework.

### Lesson notes

C# union types allow for the modeling of a type that can represent one of several distinct alternatives.
This is useful for scenarios where a value might be, for example, either an `int` or a `string`.

There are two primary ways to define union types.
The first is by using the `union` keyword followed by the type name and its cases.

```csharp
public readonly union LookupKey(int Id, string Name)
{
    public string Describe() => this switch
    {
        int id => $"id {id}",
        string name => $"name {name}",
        null => "uninitialized",
    };
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/uniontypes-recap-69958878/?t=10)

When using this syntax, the compiler generates a backing struct.
Internally, the state is stored in a `Value` property of type `object?`.

```csharp
[Union]
public readonly struct UnionDecompiled
{
    public object? Value { get; }

    // Case constructors for creating an instance from 'int'
    // Compile adds an implicit conversion
    // to allow LookupKey key = 42;
    public UnionDecompiled(int value) => Value = value;

    // Case constructors for creating an instance from 'string'
    // Compile adds an implicit conversion
    // to allow LookupKey key = "foo";
    public UnionDecompiled(string value) => Value = value;
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/uniontypes-recap-69958878/?t=16)

Because the underlying storage is an `object`, creating a union instance from a struct (a value type) results in a boxing allocation.
Additionally, the generated struct does not currently implement a custom `ToString()` method; it defaults to returning the type name.
Equality members are also not automatically generated, meaning the runtime uses default struct equality.
This can lead to further boxing allocations whenever `GetHashCode()` or `Equals()` is called.
To mitigate these performance issues, developers can manually implement equality.

The second approach to creating union types is to use standard types—such as records, classes, or structs—and mark them with the `[Union]` attribute.

```csharp
[Union]
public sealed class ClassLookupKey
{
    private readonly int id;
    private readonly string? name;
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/uniontypes-recap-69958878/?t=58)

If the type contains the necessary members, the compiler treats it as a union type.
A significant advantage of this feature is that it does not require specific runtime support.
Consequently, union types can be used when targeting older frameworks, including the full .NET Framework.

---

## 10. Conclusion

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/conclusion-69958879/) · 1:28

### Summary

This lesson concludes the "Mastering Types in C#" module, synthesizing key learnings about the C# type system.
It reviews the nuances of class construction, the performance characteristics of generic constraints, the hazards of mutable value types, and the modern features—records, tuples, and union types—that enable expressive, multi-paradigm programming.

### Key concepts

- Object initialization order and the impact of field-like initializers.
- The implementation and performance of the `new()` constraint across different .NET runtimes.
- The risks of mutable structs and the mechanics of compiler-injected defensive copies.
- Value-based semantics and equality performance in records and record structs.
- Structural grouping with tuples and the introduction of native union types in C# 15.

### Lesson notes

The module provided a deep dive into the C# type system, focusing on how different type definitions affect performance, memory, and code expressiveness.

#### Classes and Generic Constraints

The exploration of classes focused on the construction lifecycle, specifically how field initializers are injected into constructor execution.
A significant portion of the module was dedicated to the `where T : new()` constraint.
On modern .NET, the JIT compiler can optimize this constraint into a direct constructor call.
In contrast, on older frameworks like .NET Framework, this constraint often routes through `Activator.CreateInstance`, which can wrap exceptions and negatively impact performance.

#### Structs and Defensive Copies

The module addressed the complexities of value types, particularly the dangers of mutable structs.
Because structs are copied by value, mutating a copy instead of the original is a common source of bugs.
To preserve immutability invariants, the compiler often injects defensive copies when a non-readonly member is accessed on a readonly receiver (such as a `readonly` field or an `in` parameter).
This ensures the original state is not modified, though it can lead to silent performance overhead or logic errors if the developer expects the mutation to persist.

#### Records and Equality

Records were introduced as a modern way to implement value-based semantics.
The module highlighted how record structs solve the "Default Struct Equality Problem."
Default struct equality often falls back to `ValueType.Equals`, which may use reflection or inefficient hashing (such as keying only off the first field).
Record structs generate typed equality and hashing logic based on the entire declared data shape, providing both safety and performance.

#### Tuples and Union Types

Tuples were discussed as a lightweight mechanism for grouping data without the need for named types, supporting structural equality and positional deconstruction.
The module concluded with a look at Union Types, a feature arriving in C# 15.
Unions allow for the direct modeling of alternatives (e.g., a result that is either a Success or an Error), enabling a more functional style of programming within the C# ecosystem.

---

## Running the demo

```bash
cd src/mastering-csharp/07-mastering-tuples-and-union-types/MasteringCSharp.TuplesAndUnions.Demos
dotnet run -c Release                  # all four sections
dotnet run -c Release -- valuetuple
dotnet run -c Release -- tuples
dotnet run -c Release -- limits
dotnet run -c Release -- unions
```

```bash
cd src/mastering-csharp/07-mastering-tuples-and-union-types/MasteringCSharp.TuplesAndUnions.Benchmarks
dotnet run -c Release -- --list flat
dotnet run -c Release -- --filter '*LookupBenchmarks*'
dotnet run -c Release -- --filter '*TupleAllocationBenchmarks*'
```

The demo is instant.
The benchmarks need Release and take a few minutes.
The `unions` section runs hand-written stand-ins for the C# 15 feature rather than the `union` keyword itself.
