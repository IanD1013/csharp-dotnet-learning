# Mastering Tuples and Union Types

> Course: [Mastering: C#](https://dometrain.com/course/mastering-csharp/) · Chapter 7
> 10 lessons · ~17 minutes
> Source: Dometrain. Every section links back to the lesson it came from.
> Companion project: [`src/mastering-csharp/07-mastering-tuples-and-union-types`](../07-mastering-tuples-and-union-types). See [Running the demo](#running-the-demo).
> Picks up the `readonly record struct` thread deferred at the end of [Mastering Records](06-mastering-records.md#threads-into-later-chapters).

---

## The mental model

This chapter looks like two unrelated topics stapled together, and it is not.
Both halves are about **how you combine types**, and the chapter's own framing comes from the [Why Do We Need Them?](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-why-do-we-need-them-69958874/) lesson:

> In C#, standard types such as classes, structs, records, and tuples are considered "product types". This means they combine multiple properties together.
> However, many programming scenarios require an exclusive set of properties where a value can be one thing or another, but not both.

A `Point` has an `X` **and** a `Y`.
A `Result` is a success **or** an error.
C# has always had a rich vocabulary for the first kind and no vocabulary at all for the second.

The other axis is the one the [Knowing the Limits](https://dometrain.com/take/course/mastering-csharp-3256129/knowing-the-limits-when-to-move-to-actual-types-69958873/) lesson is built on: whether a type has a **name** the compiler treats as meaningful, or only a **shape**.

Putting both axes together gives the map the chapter is filling in:

| | product type ("and") | sum type ("or") |
| --- | --- | --- |
| **structural** - identity is the shape | tuple: `(string, int)` | C# has none |
| **nominal** - identity is the name | `class` / `struct` / `record` | `union` (C# 15) |

Read that way the chapter has one argument, made twice.

**Lessons 2-3 sell the top-left box.** A tuple gives you grouping, value equality, hashing, deconstruction and pattern matching for the cost of a pair of parentheses, because its identity is purely its shape.

**Lesson 4 is the pivot**, and it is the most reusable idea in the chapter: structural identity is exactly what makes a tuple cheap, and exactly what makes it unable to carry meaning.
An extension method written for `(string host, int port)` lands on every `(string, int)` in the assembly, because as far as the compiler is concerned there is only one such type.
The moment a shape needs behaviour, it needs a name.

**Lessons 5-9 fill in the bottom-right box**, and the same rule holds there: unions are nominal only.
There is no anonymous `int | string`, because a union that could not be named could not be given a `Describe` method either.

If one thing survives from this chapter, make it the pivot: **a tuple is a shape, and the moment you want to attach meaning to that shape, you need a type.**

> **Aside.** The table above is reconstructed, not quoted. The chapter states the product/sum axis (lesson 5) and the structural/nominal axis (lesson 4) separately and never crosses them.

---

## Lesson index

| # | Lesson | Length | Covered in |
| --- | --- | --- | --- |
| 1 | [Overview](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958870/) | 0:39 | [The mental model](#the-mental-model) |
| 2 | [System.Tuple vs. System.ValueTuple](https://dometrain.com/take/course/mastering-csharp-3256129/system-tuple-vs-system-valuetuple-69958871/) | 1:15 | [1.1](#11-two-tuples-and-only-one-of-them-is-the-language-feature) |
| 3 | [Tuples in C#](https://dometrain.com/take/course/mastering-csharp-3256129/tuples-in-csharp-69958872/) | 3:01 | [1.2](#12-the-names-are-not-real) · [1.3](#13-what-comes-for-free) · [1.4](#14-where-tuples-earn-their-place) |
| 4 | [Knowing the Limits: When to Move to Actual Types](https://dometrain.com/take/course/mastering-csharp-3256129/knowing-the-limits-when-to-move-to-actual-types-69958873/) | 0:48 | [2.1](#21-the-extension-method-that-goes-everywhere) · [2.2](#22-the-fix-give-the-shape-a-name) |
| 5 | [Union Types: Why Do We Need Them?](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-why-do-we-need-them-69958874/) | 1:20 | [3.1](#31-product-types-and-sum-types) |
| 6 | [Union Types: the Basics](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-the-basics-69958875/) | 1:31 | [4.1](#41-the-syntax) · [4.2](#42-exhaustiveness-and-the-third-case) |
| 7 | [Union Types Under the Hood](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-under-the-hood-69958876/) | 1:21 | [4.3](#43-one-object-field-and-what-it-costs) |
| 8 | [Union Types: the Deep Dive](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-the-deep-dive-69958877/) | 4:12 | [5.1](#51-no-runtime-support-required) · [5.2](#52-the-manual-union-that-does-not-box) · [5.3](#53-separating-zero-from-empty) · [5.4](#54-class-based-unions) |
| 9 | [UnionTypes - Recap](https://dometrain.com/take/course/mastering-csharp-3256129/uniontypes-recap-69958878/) | 1:23 | [4.4](#44-what-you-do-not-get) |
| 10 | [Conclusion](https://dometrain.com/take/course/mastering-csharp-3256129/conclusion-69958879/) | 1:28 | [Module wrap-up](#module-wrap-up) |

Every lesson in this chapter has a document; nothing was skipped.

Note that lesson 10 is the **module** conclusion, not the chapter's.
It reaches back over classes, structs, records, tuples and unions together, which is why it is summarised separately at the end rather than folded into a section.

---

## Part 1 · Tuples

### 1.1 Two tuples, and only one of them is the language feature

> [System.Tuple vs. System.ValueTuple](https://dometrain.com/take/course/mastering-csharp-3256129/system-tuple-vs-system-valuetuple-69958871/)

C# ships two unrelated implementations of the same idea, separated by seven years and a storage model.

```csharp
// System.Tuple: heap allocated. No extra semantics
Tuple<string, int> refTuple = Tuple.Create("api.example.com", 443);
// System.ValueTuple: value type. Named elements for extra clarity
(string host, int port) valTuple = ("api.example.com", 443);
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/system-tuple-vs-system-valuetuple-69958871/?t=10)

| | `System.Tuple` | `System.ValueTuple` |
| --- | --- | --- |
| Introduced | .NET Framework 4.0 | C# 7.0 |
| Kind | class | struct |
| Storage | heap | inline, no allocation |
| Element access | `Item1`, `Item2` only | named elements |
| Language syntax | none | literals, deconstruction, patterns |

The lesson's verdict is unambiguous: `System.Tuple` "should generally be avoided in modern applications".

The important part is **why the second one exists at all**.
Everything in the rest of this chapter - named elements, deconstruction, tuple patterns, `==` - is language support layered on `ValueTuple`, and none of it works on `System.Tuple`.
The old type is not a slower version of the new one; it is a library type that the language never learned to speak.

The storage claim is measured in [Measured on this machine](#tuple-storage-and-what-it-costs), including one place where it no longer holds literally on .NET 10.

### 1.2 The names are not real

> [Tuples in C#](https://dometrain.com/take/course/mastering-csharp-3256129/tuples-in-csharp-69958872/)

This is the lesson's central point and the one most likely to bite.

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

Three separate facts are packed into that block.

**Names can be inferred.** `(name: host, port)` names the first element explicitly and lets the second take the variable's name.

**Tuples are mutable.** A `ValueTuple` is a struct of public fields, so `endpoint.port++` compiles.
There is no `readonly` tuple, which is one of the quieter reasons to move to a `readonly record struct`.

**Names are erased.** The runtime type is `ValueTuple<string, int>` and nothing else.
That is why the assignment across different element names compiles, and why `Item1` is always available.

The names are not thrown away entirely, though, and the distinction matters at assembly boundaries:

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

The compiler emits a `TupleElementNamesAttribute` on the signature, which is how a consumer in another assembly still sees `host` and `port`.
So the names live in **metadata**, not in the type.
The demo reads that attribute back with reflection, because the difference between "erased" and "erased from the type but kept in metadata" is the kind of thing that is much easier to believe once you have seen the string come out:

```
TupleElementNamesAttribute   = [host, port]
```

Aliases are the other way to make a shape readable:

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

A plain `using` alias is file-scoped; `global using` covers the assembly.
The lesson adds the limit that makes aliases a readability tool rather than a modelling one: **the alias name is not preserved in metadata**.
Element names survive to a consumer, the alias does not.
An alias renames a shape for you; it does not create a type, and the demo confirms both methods above have the identical signature at runtime.

### 1.3 What comes for free

> [Tuples in C#](https://dometrain.com/take/course/mastering-csharp-3256129/tuples-in-csharp-69958872/)

Because a tuple's identity is its shape, the compiler can generate everything that follows from the shape.

```csharp
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

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/tuples-in-csharp-69958872/?t=10)

Equality is element-wise and, consistently with everything else, **ignores names**: `(x: 1, y: 2) == (a: 1, b: 2)` is `true`.
`ToString` prints positions, not names.

This is worth contrasting with [chapter 6's struct-equality disaster](06-mastering-records.md#42-the-cause-blittable-vs-non-blittable).
A hand-written `readonly struct` holding a `string` gets a default `GetHashCode` that uses the first field only, which quietly destroys `HashSet` performance.
A `ValueTuple` of exactly the same fields hashes **all** of them, because `ValueTuple<T1, T2>` overrides `GetHashCode` itself rather than inheriting the runtime fallback.

The chapter does not make this comparison, and it is load-bearing for [1.4](#14-where-tuples-earn-their-place), so the demo runs it as a control:

```
  ValueTuple<string, int>, first field held constant:
    ("", 0).GetHashCode() = 1205130051
    ("", 1).GetHashCode() = 861861700
    ("", 2).GetHashCode() = 792527867
  The same two fields as a plain struct with no GetHashCode of its own:
    new NaiveEndpointStruct("", 0).GetHashCode() = -782061113
    new NaiveEndpointStruct("", 1).GetHashCode() = -782061113
    new NaiveEndpointStruct("", 2).GetHashCode() = -782061113
  ValueTuple<string,int>.GetHashCode is declared by System.ValueTuple`2[System.String,System.Int32]
```

Same two fields, same order, same constant first element.
The struct collapses to one hash and the tuple does not, and the last line says why: `GetHashCode` is declared **on `ValueTuple`**, so it never reaches the runtime fallback that the struct inherits.
The tuple was never subject to that trap, which is what makes the next section's use case safe.

As in [chapter 6](06-mastering-records.md#the-demo-output), the specific hash values are randomised per process and differ on every run.
What is reproducible is the pattern: the tuple's five values differ from each other, the struct's five do not.

### 1.4 Where tuples earn their place

> [Tuples in C#](https://dometrain.com/take/course/mastering-csharp-3256129/tuples-in-csharp-69958872/)

**Composite keys.** The headline use, and it follows directly from all-field structural equality.

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

**Swapping.** The idiom, with the note that it costs nothing:

```csharp
void Swap(int left, int right)
{
    (right, left) = (left, right);
    Console.WriteLine(left);
    Console.WriteLine(right);
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/tuples-in-csharp-69958872/?t=145)

The compiler emits the same hidden temporary you would have written by hand.

**As an implementation detail.** The subtlest use, and the one that generalises furthest:

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

Three members collapse into one tuple expression each, and the tuple never appears in the public API.
The `GetHashCode` trick is the interesting one: it is `HashCode.Combine` for codebases that cannot use `HashCode.Combine`, which is to say .NET Framework.
That framing recurs throughout this course, and it is the same argument that returns in [5.1](#51-no-runtime-support-required) for unions.

---

## Part 2 · Where a tuple stops being enough

> [Knowing the Limits: When to Move to Actual Types](https://dometrain.com/take/course/mastering-csharp-3256129/knowing-the-limits-when-to-move-to-actual-types-69958873/)

### 2.1 The extension method that goes everywhere

The lesson gives a clean rule for when to stop: use tuples for local grouping, composite keys, pattern matching and equality plumbing; replace them when they model a domain concept in a public API, when they grow large, or when they are used very frequently.

Then it gives a single concrete symptom that beats all three heuristics.

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

`retryPolicy` is a name and a count.
It is not an endpoint, it has nothing to do with endpoints, and IntelliSense offers it `.ToEndpointString()` anyway:

```
retryPolicy.ToEndpointString() = attempts:3   <- offered on an unrelated tuple
```

This is [1.2](#12-the-names-are-not-real) coming back to collect.
Element names are not part of the type, so `this (string host, int port)` really means `this ValueTuple<string, int>`, and every tuple of that shape in the assembly qualifies.

**The needing of an extension method is the signal.**
That is a sharper rule than "when it gets big", because it fires at the exact moment the shape acquired meaning, which is the moment structural identity became the wrong model.

### 2.2 The fix: give the shape a name

```csharp
internal readonly record struct Endpoint(string Host, int Port)
{
    public override string ToString() =>
        $"{Host}:{Port}";
}
```

`readonly record struct` is the right landing spot because it gives up nothing that made the tuple attractive:

- **No allocation.** Still a struct, still inline.
- **Value equality and hashing over all fields.** Generated, and generated correctly - this is the [chapter 6](06-mastering-records.md#44-the-fix) result.
- **Deconstruction.** Generated, so the positional style survives.
- **A real `ToString`.** `api.example.com:443` instead of `(api.example.com, 443)`.
- **Immutability**, which the mutable tuple never offered.

What it adds is the thing that was missing: an identity.
`ToEndpointString` cannot reach an `Endpoint`, and `Endpoint.ToString` cannot leak onto a retry policy.

---

## Part 3 · Unions: the missing half

### 3.1 Product types and sum types

> [Union Types: Why Do We Need Them?](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-why-do-we-need-them-69958874/)

Everything up to here has been product types: a value with an `X` **and** a `Y`.
The lesson's motivating example for the other kind is the one everybody has hand-rolled at least once:

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

A `Result<T>` is a value **or** an exception, never both and never neither.
The `object? _value` field and the manual `Create` overloads are what modelling that costs today.

The lesson is careful about what the language feature adds over the hand-rolled version, and it is not the storage:

> In C# 15, union types allow developers to express values from a closed set of types.
> This ensures that pattern matching is exhaustive, meaning the compiler can guarantee that every possible case of the union has been handled.

**Closedness is the feature.**
The hand-rolled `Result` above stores the same bytes, but no compiler will tell you that you forgot the exception branch.
An `abstract class` hierarchy with two subclasses has the same gap, because anybody can add a third subclass.

The chapter's running example for the rest of the material:

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

Two things to notice before the mechanics.
The declaration **looks like a constructor and is not**: `(int Id, string Name)` is the list of types convertible to `LookupKey`, not a parameter list, and an instance holds exactly one of them.
And there is a `null` arm for a union whose declared cases are `int` and `string`, neither of which is nullable.
[4.2](#42-exhaustiveness-and-the-third-case) is about where that came from.

---

## Part 4 · The feature

### 4.1 The syntax

> [Union Types: the Basics](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-the-basics-69958875/)

```csharp
var entries = new Entries();

// An implicit conversion from int to LookupKey
LookupKey key = 42;

Console.WriteLine(key.Describe());
var result = entries.Lookup(key);
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-the-basics-69958875/?t=40)

The compiler generates a constructor per case and an implicit conversion to match, so `LookupKey key = 42;` and `LookupKey key = "foo";` both compile with no ceremony.

The payoff is on the receiving side:

```csharp
public sealed class Entries
{
    public Entry Lookup(LookupKey key) => key switch
    {
        int id => byId[id],
        string name => byName[name],
        null => throw new InvalidOperationException("Lookup key must be initialized before it is processed."),
    };
    ...
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-the-basics-69958875/?t=40)

One method, two key kinds, and nothing else can be passed.
The alternative today is two overloads that drift apart, or one `object` parameter that accepts anything.

### 4.2 Exhaustiveness and the third case

Because the set of cases is closed, the compiler can check that a switch covers it:

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

Now the `null` arm.
The `union` keyword always produces a **struct**, and every struct has a `default`:

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

So a union declared with two cases has three states at runtime, and the third one was never declared.
`default(LookupKey)` is not `Id`, is not `Name`, and is reachable from any array, any uninitialized field, and any generic `default(T)`.

This is the same structural problem as [chapter 5's mutable struct traps](05-mastering-structs.md): C# lets you declare invariants that `default` walks straight through.
[5.4](#54-class-based-unions) is the escape hatch.

### 4.3 One object field, and what it costs

> [Union Types Under the Hood](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-under-the-hood-69958876/)

The generated type, decompiled:

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

And in readable form, with the switch expression lowered to its type tests:

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

**The whole implementation is one `object?` field.**
That single decision explains everything else in the chapter.

- `default` produces `Value == null`, hence the extra case in [4.2](#42-exhaustiveness-and-the-third-case).
- The type needs no runtime support, hence [5.1](#51-no-runtime-support-required).
- **Storing an `int` in an `object` field boxes it.**

The lesson benchmarks the last point:

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

> Running these benchmarks with a memory diagnoser reveals that `LookupKeyById` incurs a 24-byte allocation.
> This is the exact size of a boxed `int` on a 64-bit system.
> In contrast, `LookupKeyByName` does not incur additional transient allocations because `string` is already a reference type.

Reproduced exactly in [Measured on this machine](#union-lookup-the-chapters-benchmark), 24 bytes and all.

### 4.4 What you do not get

> [UnionTypes - Recap](https://dometrain.com/take/course/mastering-csharp-3256129/uniontypes-recap-69958878/)

The recap lists what the compiler does **not** write for you, and the list is longer than you would guess from how much it does write:

- **No `ToString()`.** You get the type name.
- **No equality members.** The union falls back to `ValueType.Equals`, which is the [chapter 6](06-mastering-records.md#43-the-second-cost-boxing) problem: it boxes, and it can behave badly as a hash key.

> Equality members are also not automatically generated, meaning the runtime uses default struct equality.
> This can lead to further boxing allocations whenever `GetHashCode()` or `Equals()` is called.

So a union used as a dictionary key today inherits every pathology the previous chapter was about.
The fix is the same fix: declare it as a `record struct` with `[Union]` rather than with the `union` keyword, which is exactly what [5.2](#52-the-manual-union-that-does-not-box) does.

The recap also names the second way to declare a union, which the deep dive then uses throughout:

```csharp
[Union]
public sealed class ClassLookupKey
{
    private readonly int id;
    private readonly string? name;
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/uniontypes-recap-69958878/?t=58)

`union` is the shorthand; `[Union]` on a type you wrote yourself is the general form, and it works on classes, structs and records.

---

## Part 5 · The deep dive

> [Union Types: the Deep Dive](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-the-deep-dive-69958877/)

At 4:12 this is the longest lesson in the chapter and it carries most of the practical content.

### 5.1 No runtime support required

Union recognition is **duck-typed by the compiler**, so the two types it looks for can simply be declared in your own project:

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

That is the entire contract, and it is why unions work on old target frameworks including .NET Framework.
Compare [chapter 2's PolySharp material](02-mastering-the-modern-csharp-stack.md): the same trick, at a language-feature scale.

What you cannot polyfill is the **compiler**:

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

Note what that csproj is and is not saying.
`TargetFramework` stays `net10.0`; only `LangVersion` moves to `preview`, and the **SDK** has to be 11 or higher.
The target framework is where your code runs; the SDK is which compiler compiles it.
This is precisely the distinction [chapter 2](02-mastering-the-modern-csharp-stack.md) spends four lessons on, and it is the reason the demo in this repo cannot use the keyword - see [What does not compile here](#what-does-not-compile-here-and-why).

### 5.2 The manual union that does not box

The reason to hand-write a union rather than use the keyword:

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

Three separate wins in one type, and they are worth separating because each has its own reason.

**Storage is two typed fields, not one `object`.** The `int` lives in an `int?`, so nothing is boxed on the way in.

**`TryGetValue` is the access pattern the compiler prefers.** Given both `Value` and a `TryGetValue` overload per case, pattern matching compiles to `TryGetValue`.
That is what makes the storage change actually pay off: without it, every `is int` test would go back through the boxed `Value`.

**`record struct` supplies the missing members.** The `Equals`, `GetHashCode` and `==` that [4.4](#44-what-you-do-not-get) says the keyword does not generate come free, over the private fields.

### 5.3 Separating zero from empty

The `int?` is doing more work than avoiding boxing.

```csharp
LookupKey idKey = 0;
LookupKey defaultKey = default;

Console.WriteLine(idKey.Equals(defaultKey));

Console.WriteLine($"idKey(0): {idKey}");
Console.WriteLine($"default: {defaultKey}");
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/union-types-the-deep-dive-69958877/?t=145)

With a plain `int` field, `default(LookupKey)` and `LookupKey(0)` are the same bytes and therefore indistinguishable.
Every id-zero would read as uninitialized, or every uninitialized would read as id zero, depending on which way the `Describe` chain is written.
`int?` adds the one bit that separates them.

The comment in the lesson's own source is worth reading twice, because it says the opposite of the code around it:

> With this storage convention, `default(LookupKey)` is treated as the id case with value 0.

That describes the `int` version, not the `int?` version shown.
Measured on this machine, the `int?` version reports `default` as `<uninitialized>` and `zero.Equals(default)` as `False`, which is the behaviour the surrounding lesson argues for.

**Equality is also not free**, and the lesson shows the hand-written form for unions declared with the keyword:

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

Note `HashCode.Combine(typeof(int), id)`: the **case** goes into the hash alongside the value, so an id and a name that happen to hash alike stay in different buckets.
That is the same job `EqualityContract` does for records in [chapter 6](06-mastering-records.md#22-equalitycontract).

> **Aside.** The `_ => true` arm in that `Equals` is worth pausing on.
> It is reached whenever the two operands are in different cases, and it reports them **equal**.
> That makes `LookupKey(42).Equals(LookupKey("x"))` return `true`, which is almost certainly not intended.
> The `record struct` in [5.2](#52-the-manual-union-that-does-not-box) does not have this problem, which is one more argument for that form.

### 5.4 Class-based unions

The `union` keyword always generates a struct, but `[Union]` does not have to:

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

Look at `Describe`: **two arms, no `null`, no discard.**
That is the whole point of the class version.
A class has no `default` that is a usable instance, so `Value` can be declared non-nullable, and the compiler can then prove that a two-arm switch is exhaustive.
The uninitialized case from [4.2](#42-exhaustiveness-and-the-third-case) does not exist.

The cost is the obvious one, and the chapter does not hide it: every key is now a heap allocation.

| | struct union | class union |
| --- | --- | --- |
| Uninitialized state | yes, `default` | no |
| Switch arms needed | cases + `null` | cases |
| `int` case | boxed, unless `TryGetValue` | boxed, unless `TryGetValue` |
| Cost per instance | none | one allocation |

---

## What does not compile here, and why

The demo project in this repo runs the tuple half of the chapter as-is and **hand-writes** the union half.
This section exists so that is a known limitation rather than a surprise.

Unions are a C# 15 feature, which means an SDK whose Roslyn understands them.
The newest SDK installed on this machine is 10.0.102:

```
$ dotnet --list-sdks
8.0.424 [C:\Program Files\dotnet\sdk]
9.0.317 [C:\Program Files\dotnet\sdk]
10.0.102 [C:\Program Files\dotnet\sdk]
```

Setting `<LangVersion>preview</LangVersion>` as the lesson instructs is not enough, because "preview" means the newest version *this* compiler knows:

```
error CS0106: The modifier 'public' is not valid for this item
error CS0106: The modifier 'readonly' is not valid for this item
error CS1513: } expected
```

Those are **parse** errors, not feature-gate errors.
The compiler is not refusing a known keyword; it does not know `union` is a keyword at all and is trying to read the declaration as something else.
`-p:LangVersion=15` fails earlier still, with `error CS1617: Invalid option '15' for /langversion`.

The manual `[Union]` route from [5.1](#51-no-runtime-support-required) gets further and still stops.
Declaring `UnionAttribute` and `IUnion` compiles fine, and a `[Union] readonly record struct` with `TryGetValue` overloads compiles fine, because those are ordinary C#.
What fails is the part that needs compiler cooperation:

```
error CS8121: An expression of type 'LookupKey' cannot be handled by a pattern of type 'int'.
error CS8121: An expression of type 'LookupKey' cannot be handled by a pattern of type 'string'.
```

So **union recognition is entirely a compiler feature**.
The attribute and the interface are the handshake, but without a compiler that looks for them they are just two unused declarations.
That is a sharper version of the lesson's "no runtime support required" claim than the lesson itself gives: no runtime support, and *all* compiler support.

**What the demo does instead.** It transcribes the decompiled shape from [4.3](#43-one-object-field-and-what-it-costs) by hand, with the type tests written out as the `if (value is int id)` chain the compiler would emit.
That is enough to reproduce every measurable claim in the chapter, because all of them are properties of the generated shape rather than of the syntax: the boxed `object?` storage, the missing `ToString` and equality, the `default` case, and the `TryGetValue` fix.
What it cannot show is the part that only exists at compile time - the exhaustiveness warning when an arm is missing.

**To run the real thing** when an SDK 11 is available, two changes are needed:

1. `global.json` pins `10.0.100` with `rollForward: latestFeature`, which rolls forward across feature bands of 10.0 but **not** to a new major. It has to be repointed.
2. `LangVersion` has to move to `preview`, overriding `latest` from `src/Directory.Build.props`.

---

## Measured on this machine

Intel Core i7-10700 @ 2.90GHz, 8 physical cores, Windows 11 26200, .NET 10.0.11, BenchmarkDotNet 0.15.8.

### Union lookup: the chapter's benchmark

The lesson's own four benchmarks, plus two for the manual union from [5.2](#52-the-manual-union-that-does-not-box).

| Method | Mean | Ratio | Gen0 | Allocated |
| --- | ---: | ---: | ---: | ---: |
| DirectById | 1.091 ns | 1.00 | - | - |
| DirectByName | 3.583 ns | 3.29 | - | - |
| LookupKeyById | 2.616 ns | 2.40 | 0.0057 | **24 B** |
| LookupKeyByName | 4.101 ns | 3.76 | - | - |
| ManualKeyById | 1.585 ns | 1.45 | - | - |
| ManualKeyByName | 3.663 ns | 3.36 | - | - |

**The 24 bytes are exactly the lesson's number**, on different hardware and a different runtime, and the string case allocates nothing.
That pair is the cleanest possible demonstration that the cost is boxing and not "unions are slow": same type, same call, and the only difference is whether the active case was already a reference.

The two manual rows are the part the course describes but does not measure.
Swapping `object? Value` for `int?` + `string?` with `TryGetValue` removes the allocation **and** most of the overhead: `ManualKeyById` is 1.59 ns against the direct call's 1.09 ns, where the generated union costs 2.62 ns.
The remaining half-nanosecond is the nullable check.

The means above move by a few percent between runs; the `Allocated` column does not move at all, and it is the column the chapter is about.

> **Aside: this benchmark needs more iterations than it looks like it does.**
> Everything here runs in single-digit nanoseconds, and at BenchmarkDotNet's default of 5 iterations a single outlier moved `ManualKeyById` to **29.7 ns**, a 22x ratio that would have read as a real and dramatic finding.
> Fifteen iterations gives 1.575 ns and is stable across runs.
> The benchmark class pins `iterationCount: 15` for that reason, with a comment saying so.

### Tuple storage, and what it costs

| Method | Mean | Gen0 | Allocated |
| --- | ---: | ---: | ---: |
| CreateValueTuple | 0.0425 ns | - | - |
| CreateSystemTuple | 3.1518 ns | 0.0077 | 32 B |
| CreateSystemTupleNoEscape | 0.0026 ns | - | - |
| ValueTupleKeyLookup | 14.7959 ns | - | - |
| SystemTupleKeyLookup | 26.2096 ns | 0.0249 | **104 B** |

Two findings here that the course does not have.

> **Aside: "System.Tuple is heap allocated" is no longer literally true on .NET 10.**
> `CreateSystemTupleNoEscape` builds a `Tuple<string, int>`, reads `Item2`, and returns.
> It allocates **nothing**, because the JIT can prove the instance never leaves the method and keeps it off the heap.
> The first version of this benchmark returned only `Item2` from both cases and measured 0 B for each, which flatters the old tuple for a reason that has nothing to do with the lesson.
> `CreateSystemTuple` returns the tuple so it genuinely escapes, and there the 32 bytes appear.
> The lesson's advice is unaffected - anything you return, store or put in a collection escapes - but the mechanism is now "allocates when it escapes" rather than "always allocates".

**The composite-key case is where the two tuples really separate**, and 104 bytes per lookup is a much bigger number than the 32 the storage model suggests.
It decomposes exactly, measured separately:

| | Allocated |
| --- | ---: |
| `Tuple.Create(...)`, escaping | 32 B |
| `Tuple.GetHashCode()` | 24 B |
| `Tuple.Equals(other)` | 48 B |
| Dictionary lookup with a pre-built key | 72 B |
| **Full lookup** | **104 B** |

`Tuple<T1, T2>` implements hashing and equality through `IStructuralEquatable`, which takes an `IEqualityComparer` and therefore works in terms of `object`.
So the `int` element is boxed once for the hash and once per side for the comparison: 24 + 24 + 24 = 72 bytes of boxing per lookup, on top of the 32 for the key itself.
The `ValueTuple` version allocates zero and runs in less than half the time.

A `Dictionary<Tuple<string, int>, V>` is therefore not a slightly worse composite key than a `ValueTuple` one.
It allocates 104 bytes on every read, which is the kind of thing that only shows up as GC pressure under load.

### The demo output

```
-- What 10,000 of each allocate --
  10,000 x Tuple.Create(string, int) = 320,000 bytes  (32.0 per instance)
  10,000 x (string, int)             =       0 bytes

-- Where the names actually go: TupleElementNamesAttribute --
  ParseEndpoint("api.example.com:444") = api.example.com:444
  return type in metadata      = System.ValueTuple`2[System.String,System.Int32]
  TupleElementNamesAttribute   = [host, port]
  ParseGlobalEndpoint metadata = [host, port]
  Element names survive into metadata. The alias name `GlobalEndpoint` does not.

-- Structural equality, for free --
  (1,2) == (1,3) : False
  (1,2) == (1,2) : True
  hash codes equal: True
  (x:1,y:2) == (a:1,b:2) : True   <- names are not compared
  ToString() = (1, 2)   <- and the names are gone here too

-- An extension method cannot be aimed at a tuple's meaning --
  endpoint.ToEndpointString() = api.example.com:443
  retryPolicy.ToEndpointString() = attempts:3   <- offered on an unrelated tuple
  The compiler has no way to tell the two apart: both are ValueTuple<string, int>.

-- What 10,000 of each case allocate --
  GeneratedLookupKey from int    =  240,000 bytes  (24.0 per instance)
  GeneratedLookupKey from string =        0 bytes  (0.0 per instance)
  ManualLookupKey    from int    =        0 bytes  (0.0 per instance)

-- The case nobody declared: default --
  default(GeneratedLookupKey).Value      = null
  default(GeneratedLookupKey).Describe() = uninitialized
  `int Id | string Name` has two cases. The runtime type has three.
  ManualLookupKey zero    = id 0
  ManualLookupKey default = <uninitialized>
  zero.Equals(default)    = False   <- int? keeps 'id 0' and 'no case' apart

-- What a generated union does not come with --
  a.ToString()   = MasteringCSharp.TuplesAndUnions.Demos.GeneratedLookupKey   <- the type name, not the value
  a.Equals(b)    = True   (two unions over the same id)
  hashes equal   = True
  Equality falls back to ValueType.Equals, which boxes and compares the object field.
  one a.Equals(b) call allocated 80 bytes (result True)
  record struct version: ma == mb = True, ToString = id 42
```

Two of those lines are worth stopping on.

**`ManualLookupKey    from int = 0 bytes`** confirms the deep dive's central claim from the other direction.
The generated shape costs 24 bytes per instance and the manual shape costs nothing, for the same two cases and the same public API.

**`one a.Equals(b) call allocated 80 bytes`** is the [4.4](#44-what-you-do-not-get) warning made concrete.
Comparing two unions with no generated equality goes through `ValueType.Equals` and allocates 80 bytes for a single `bool`.
The `record struct` version on the next line allocates nothing and prints a useful `ToString` besides.
If you take one practical rule out of the union half of this chapter, it is that the `union` keyword is the demo form and `[Union] readonly record struct` is the production form.

---

## Common misconceptions

**"Tuple element names are part of the type."**
They are compile-time only. `(string host, int port)` and `(string server, int number)` are the same type, assignment between them compiles, and an extension method on one is offered on both.

**"So the names are thrown away entirely."**
Not quite. They are erased from the *type* but emitted as a `TupleElementNamesAttribute` on the signature, which is how a consumer in another assembly still sees them. Alias names get no such treatment.

**"Tuples are immutable."**
A `ValueTuple` is a struct of public fields. `endpoint.port++` compiles. There is no readonly tuple.

**"A struct key hashes all of its fields, so a tuple key and a hand-written struct key behave the same."**
A `ValueTuple` overrides `GetHashCode` and uses everything. A hand-written struct holding a reference type inherits the runtime fallback that uses the first field only - that is [chapter 6](06-mastering-records.md#42-the-cause-blittable-vs-non-blittable), and it is why tuples are safe as keys where a naive struct is not.

**"A union is a discriminated union with its own storage per case."**
The keyword form is one `object?` field. The int case boxes, and there is no tag beyond the runtime type of that field.

**"A union with two cases has two states."**
It has three. `union` always generates a struct, and `default` gives a `Value` of `null` that matches neither declared case. Only a class-based `[Union]` type escapes this.

**"Unions need .NET 11 at runtime."**
They need an SDK 11 compiler. The runtime contract is one attribute and one interface you can declare yourself, so a union can target .NET Framework.

**"`LangVersion=preview` unlocks C# 15."**
It unlocks the newest version the installed compiler knows. On the .NET 10 SDK that is C# 14, and `union` does not even parse.

**"Unions come with equality and `ToString` like records do."**
Neither is generated. Equality falls back to `ValueType.Equals`, which boxes, and measured 80 bytes for a single comparison here.

---

## Self-test

1. Two variables typed `(string host, int port)` and `(string server, int number)`. Does assignment between them compile, and what does that tell you about where element names live?
2. You write an extension method on `(string host, int port)`. Name a tuple in your codebase that will be offered the method and should not be, and explain the rule that causes it.
3. Element names are erased at compile time, yet a caller in another assembly still sees `host` and `port`. Reconcile those two statements.
4. Give the chapter's rule for when a tuple should become a `record struct`, and the single symptom that signals it most sharply.
5. Why is `readonly record struct` the recommended landing spot rather than `class` or `record`? List what carries over from the tuple and what is added.
6. `union LookupKey(int Id, string Name)` declares two cases. How many states can a `LookupKey` be in at runtime, and where does the extra one come from?
7. `LookupKeyById` allocates 24 bytes and `LookupKeyByName` allocates none. Explain both halves from the generated implementation.
8. What two declarations does a project need in order to use unions when targeting .NET Framework, and what does that tell you about where the feature actually lives?
9. A manual `[Union]` type exposes both `object? Value` and `TryGetValue(out int)`. Which does pattern matching use, and why does the answer determine whether the type allocates?
10. Why does the deep dive back the int case with `int?` rather than `int`? Name the two separate problems it solves.
11. Why does a class-based union need fewer switch arms than a struct-based one?
12. A `Dictionary<Tuple<string, int>, V>` lookup allocated 104 bytes per read here. Break that number down.

<details>
<summary>Answer key</summary>

1. It compiles. Element names are compile-time metadata, not part of the type: both variables are `ValueTuple<string, int>`, so the assignment is between two values of one type.
2. Any `(string, int)` tuple at all - the lesson uses `(name: "attempts", count: 3)`. The extension targets `ValueTuple<string, int>`, and since names are not part of the type, the compiler cannot distinguish an endpoint from a retry policy.
3. The names are erased from the *type* but the compiler emits a `TupleElementNamesAttribute` on the member's signature. The type is nameless; the signature is annotated. Alias names get no equivalent treatment and do not survive.
4. Replace a tuple when it models a domain concept in a public API, when it grows large, or when it is used very frequently. The sharpest symptom is needing an extension method, because that is the moment the shape acquired meaning that structural identity cannot carry.
5. It keeps everything the tuple had: no allocation, all-field value equality and hashing, and deconstruction. It adds an identity the compiler enforces, a real `ToString`, and immutability. A `class` or `record` would add a heap allocation the tuple never had.
6. Three. The `union` keyword always generates a struct, so `default(LookupKey)` exists and leaves the backing `object?` as `null`, matching neither `Id` nor `Name`. That is why switch expressions over struct unions must handle `null`.
7. The generated union stores its case in a single `object?` field. An `int` is a value type and must be boxed to fit, which is 24 bytes on x64. A `string` is already a reference, so it is stored as-is with no additional allocation.
8. `UnionAttribute` and `IUnion`, both declarable in your own project under `System.Runtime.CompilerServices`. It tells you the feature needs no runtime support at all - it lives entirely in the compiler, which recognises those two shapes by duck typing.
9. `TryGetValue`. The compiler prefers it over reading `Value`, and that is the whole point: reading `Value` would box the int case, while `TryGetValue` reads it out of a typed field. Without the compiler preferring `TryGetValue`, changing the storage would not help.
10. First, it avoids boxing, since the int lives in a typed field rather than an `object`. Second, it separates `default` from a genuine id of 0, which with a plain `int` field would be the same bytes and therefore indistinguishable.
11. A class has no `default` that is a valid instance, so its `Value` can be non-nullable and the compiler can prove there is no uninitialized state. A struct union always has `default`, so it needs an extra arm for it.
12. 32 bytes for the `Tuple.Create` key, which escapes into the dictionary call; 24 bytes for `Tuple.GetHashCode`, which boxes the int through `IStructuralEquatable`; and 48 bytes for `Equals`, which boxes the int on each side. 32 + 24 + 48 = 104.

</details>

---

## Running the demo

The demo is instant. The benchmarks take a few minutes and must be Release.

```bash
cd src/mastering-csharp/07-mastering-tuples-and-union-types/MasteringCSharp.TuplesAndUnions.Demos
dotnet run -c Release                  # all four sections
dotnet run -c Release -- valuetuple    # part 1.1
dotnet run -c Release -- tuples        # part 1.2 to 1.4
dotnet run -c Release -- limits        # part 2
dotnet run -c Release -- unions        # parts 3 to 5, hand-written
```

```bash
cd src/mastering-csharp/07-mastering-tuples-and-union-types/MasteringCSharp.TuplesAndUnions.Benchmarks
dotnet run -c Release -- --filter '*LookupBenchmarks*'            # the chapter's union benchmark
dotnet run -c Release -- --filter '*TupleAllocationBenchmarks*'   # tuple storage and key cost
dotnet run -c Release -- --list flat
```

The `unions` section runs hand-written stand-ins for the C# 15 feature, for the reasons in [What does not compile here](#what-does-not-compile-here-and-why).
Every measurement it prints is real; the syntax it demonstrates is not the syntax you will eventually write.

`LookupBenchmarks` pins 15 iterations rather than BenchmarkDotNet's default 5, because everything it measures runs in single-digit nanoseconds and 5 iterations produced a 20x outlier.

---

## Module wrap-up

> [Conclusion](https://dometrain.com/take/course/mastering-csharp-3256129/conclusion-69958879/)

Lesson 10 closes the whole type-modelling module rather than this chapter, and the through-line it draws is worth keeping:

| Chapter | The thing that is not obvious |
| --- | --- |
| [Classes](04-mastering-classes.md) | Field initializers are injected into the constructor, and `where T : new()` routes through `Activator.CreateInstance` on .NET Framework |
| [Structs](05-mastering-structs.md) | The compiler injects defensive copies on readonly receivers, so mutations silently do not stick |
| [Records](06-mastering-records.md) | Default struct equality boxes and may hash one field; `record struct` generates typed all-field equality |
| Tuples and unions | A tuple is a shape, not a type; a union is one `object?` field, and the closed set is the feature |

The recurring shape across all four: **C# gives you a default behaviour that looks like the one you wanted, and the gap only shows up under measurement.**
Defensive copies, first-field hashing, and boxed union storage are all the same kind of bug - correct-looking code whose cost or semantics live one layer below the syntax.

---

## Threads into later chapters

| Deferred here | Picked up in |
| --- | --- |
| Exhaustiveness checks over unions and closed hierarchies | [Mastering Pattern Matching](https://dometrain.com/take/course/mastering-csharp-3256129/exhaustiveness-checks-for-classes-and-unions-69958915/) |
| Tuple patterns and positional deconstruction as recursive patterns | Mastering Pattern Matching |
| Boxing as a systematic allocation source | Mastering LINQ (the cost of boxed iterators) |
| Result-based error handling as an alternative to exceptions | Not covered further in this course |
