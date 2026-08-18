# Mastering Pattern Matching

> Course: [Mastering: C#](https://dometrain.com/course/mastering-csharp/) · Chapter 11
> 10 lessons · ~12:20
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Overview](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958907/) | 0:40 | [↓](#1-overview) |
| 2 | [Imperative vs. Declarative Code](https://dometrain.com/take/course/mastering-csharp-3256129/imperative-vs-declarative-code-69958908/) | 1:12 | [↓](#2-imperative-vs-declarative-code) |
| 3 | [Two Dimensions of Pattern Matching](https://dometrain.com/take/course/mastering-csharp-3256129/two-dimensions-of-pattern-matching-69958909/) | 1:14 | [↓](#3-two-dimensions-of-pattern-matching) |
| 4 | [Exploring Pattern Matching Variety](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-pattern-matching-variety-69958910/) | 1:40 | [↓](#4-exploring-pattern-matching-variety) |
| 5 | [Recursive Patterns Explained](https://dometrain.com/take/course/mastering-csharp-3256129/recursive-patterns-explained-69958911/) | 1:25 | [↓](#5-recursive-patterns-explained) |
| 6 | [Pattern Matching Exhaustiveness](https://dometrain.com/take/course/mastering-csharp-3256129/pattern-matching-exhaustiveness-69958912/) | 0:42 | [↓](#6-pattern-matching-exhaustiveness) |
| 7 | [Exhaustiveness Checks in Action](https://dometrain.com/take/course/mastering-csharp-3256129/exhaustiveness-checks-in-action-69958913/) | 1:50 | [↓](#7-exhaustiveness-checks-in-action) |
| 8 | [Open and Closed Hierarchies](https://dometrain.com/take/course/mastering-csharp-3256129/open-and-closed-hierarchies-69958914/) | 0:42 | [↓](#8-open-and-closed-hierarchies) |
| 9 | [Exhaustiveness Checks for Classes and Unions](https://dometrain.com/take/course/mastering-csharp-3256129/exhaustiveness-checks-for-classes-and-unions-69958915/) | 1:43 | [↓](#9-exhaustiveness-checks-for-classes-and-unions) |
| 10 | [Know When to Stop](https://dometrain.com/take/course/mastering-csharp-3256129/know-when-to-stop-69958916/) | 1:12 | [↓](#10-know-when-to-stop) |

---

## 1. Overview

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958907/) · 0:40

### Summary

Pattern matching in C# is a powerful tool for writing expressive, declarative code.
It allows developers to move away from imperative instruction sets—characterized by nested casts and null checks—toward code that clearly expresses intent.
This lesson covers the foundation of is patterns, the composition of recursive patterns, the use of switch expressions with exhaustiveness checks, and the differences between handling open-type hierarchies based on classes and closed-type hierarchies based on union types.

### Key concepts

- **Imperative vs. Declarative**: Moving from "how" to "what".
- **is Patterns**: The fundamental building block for type and property checking.
- **Recursive Patterns**: Composing patterns to inspect deep object graphs.
- **Switch Expressions**: A concise syntax for multi-branch logic with exhaustiveness checking.
- **Exhaustiveness**: Compiler-verified coverage of all possible input values.
- **Open vs. Closed Hierarchies**: Handling extensible class hierarchies versus fixed union types.

### Lesson notes

Pattern matching is a primary tool for making C# code more expressive.
The transition to pattern matching often involves moving from imperative code to declarative code.
Imperative code contains a set of instructions for the program to execute, such as manual type checks and nested logic.
Declarative code, by contrast, expresses the intent of what the code should achieve.

For example, checking if a symbol is a public async method can be written imperatively with nested `as` casts and null checks, or declaratively using a single recursive pattern.

```csharp
using Microsoft.CodeAnalysis;

public static class SymbolChecks
{
    // Imperative version: nested `as` casts and null checks.
    public static bool IsPublicAsyncMethod_Imperative(ISymbol symbol)
    {
        IMethodSymbol? method = symbol as IMethodSymbol;

        if (method != null && method.IsAsync)
        {
            INamedTypeSymbol? type = method.ContainingType as INamedTypeSymbol;

            if (type != null && type.DeclaredAccessibility == Accessibility.Public)
            {
                return true;
            }
        }

        return false;
    }

    // Declarative version: one recursive pattern, no temporaries.
    public static bool IsPublicAsyncMethod_Patterns(ISymbol symbol) =>
        symbol is IMethodSymbol
        {
            IsAsync: true,
            ContainingType.DeclaredAccessibility: Accessibility.Public
        };
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958907/?t=6)

#### The is Pattern

The `is` pattern serves as the foundation for pattern matching.
It allows for type checking, null checking, and property inspection in a single expression.

```csharp
static string Describe(object? candidate)
{
    if (candidate is null)
        return "Missing";

    if (candidate is string { Length: <= 4 } text)
        return $"Short text: {text}";

    if (candidate is string valueText)
        return $"Text ({valueText.Length} chars)";

    if (candidate is int value)
        return $"Number: {value}";

    if (candidate is Point { X: 0, Y: 0 })
        return "Origin";

    if (candidate is Point { Y: 0 } point)
        return $"X-axis at {point.X}";

    return "Other";
}

record Point(int X, int Y);
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958907/?t=19)

#### Recursive Patterns

Patterns can be composed using recursive patterns, allowing for deep inspection of an object's properties and their types without introducing temporary local variables or deeply nested `if` statements.

```csharp
using Microsoft.CodeAnalysis;

public static class RecursivePatterns
{
    // Declarative version: one recursive pattern, no temp locals or nesting
    public static bool IsPublicAsyncValueTaskMethod(ISymbol symbol) =>
        symbol is IMethodSymbol
        {
            IsAsync: true,
            ContainingType.DeclaredAccessibility: Accessibility.Public,
            // ReturnType is of type ISymbol, we're using recursive pattern to narrow it.
            ReturnType: INamedTypeSymbol { Name: "ValueTask" }
        };
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958907/?t=23)

#### Switch Expressions and Exhaustiveness

Switch expressions provide a concise way to handle multiple patterns.
A key feature of switch expressions is the exhaustiveness check, where the compiler attempts to prove that every possible case is handled.
If the compiler cannot prove coverage (e.g., with open types like `string` or `int`), it requires a discard pattern (`_`) as a fallback.

```csharp
public static class Verified
{
    // bool. Two values, both must appear.
    public static string Bool(bool b) => b switch
    {
        true  => "yes",
        false => "no",
    };

    // Null axis on reference types.
    public static int Length(string? s) => s switch
    {
        null     => 0,
        not null => s.Length,
    };
}

public static class FallbackRequired
{
    // string values. Open value domain.
    public static int Priority(string level) => level switch
    {
        "low"    => 1,
        "medium" => 2,
        "high"   => 3,
        _        => 0,
    };
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958907/?t=27)

#### Open vs. Closed Hierarchies

Pattern matching behaves differently depending on whether a type hierarchy is open or closed.

- **Open Hierarchies**: Based on classes where anyone can derive a new subtype. The compiler cannot guarantee that all possible subtypes are handled, necessitating a default arm.
- **Closed Hierarchies**: Based on union types where the set of cases is fixed. The compiler can verify exhaustiveness, and adding a new case to the union will trigger a compile-time error in switch expressions that do not handle it.

```csharp
// Open Hierarchy (Classes)
public abstract class CliArgument;
public sealed class Flag(string name) : CliArgument { public string Name { get; } = name; }

public static class Processor
{
    public static string Render(CliArgument arg) => arg switch
    {
        Flag f       => $"--{f.Name}",
        _            => "", // Required for open hierarchy
    };
}

// Closed Hierarchy (Unions)
public record Flag(string Name);
public record Option(string Name, string Value);
public record Positional(string Value);

public union CliArgument(Flag Flag, Option Option, Positional Positional);

public static class ClosedProcessor
{
    public static string RenderKeyword(CliArgument arg) => arg switch
    {
        Flag f       => $"--{f.Name}",
        Option o     => $"--{o.Name} {o.Value}",
        Positional p => p.Value,
        // No default arm needed for exhaustive union
    };
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958907/?t=31)

---

## 2. Imperative vs. Declarative Code

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/imperative-vs-declarative-code-69958908/) · 1:12

### Summary

This lesson compares imperative and declarative programming styles in C# using pattern matching.
By examining a real-world scenario involving the Roslyn API, it demonstrates how to identify specific code symbols—such as public asynchronous methods returning a ValueTask—using both traditional nested conditional logic and modern recursive patterns.
The declarative approach significantly reduces boilerplate code, such as manual type casting and null checks, resulting in more readable and maintainable logic that focuses on the intended data shape rather than the mechanics of retrieval.

### Key concepts

* **Imperative Programming**: A style that focuses on the specific steps and instructions required to achieve a result, often involving manual type casting and nested conditional checks.
* **Declarative Programming**: A style that focuses on describing the desired outcome or the shape of the data, allowing the language features to handle the underlying implementation.
* **Recursive Patterns**: A pattern matching feature that allows nesting patterns within other patterns to inspect complex object hierarchies in a single expression.
* **Property Patterns**: Matching against specific properties of an object to verify their values during the pattern matching process.
* **Type Narrowing**: The process of identifying a more specific type for an object and gaining access to its members within a pattern.

### Lesson notes

The difference between imperative and declarative code is best illustrated by a practical example using the Roslyn API, which is used by the C# compiler.
In this scenario, the goal is to determine if a given `ISymbol` represents a public, asynchronous method that returns a `ValueTask`.

#### The Imperative Approach

In an imperative implementation, the logic is defined by a series of explicit checks and casts.
First, the symbol must be cast to an `IMethodSymbol`.
If the cast is successful (not null), the code then checks if the `IsAsync` property is true and if the `DeclaredAccessibility` of the `ContainingType` is `Accessibility.Public`.
Finally, the `ReturnType` must be cast to an `INamedTypeSymbol` to verify that its name is "ValueTask".
This approach is verbose and requires multiple levels of nesting and temporary local variables.

```csharp
public static bool IsPublicAsyncValueTaskMethod_Imperative(ISymbol symbol)
{
    IMethodSymbol? method = symbol as IMethodSymbol;
    if (method != null)
    {
        if (method.IsAsync && 
            method.ContainingType.DeclaredAccessibility == Accessibility.Public)
        {
            // Nested conditions: a candidate for recursive patterns
            INamedTypeSymbol? returnType = method.ReturnType as INamedTypeSymbol;
            if (returnType != null)
            {
                if (returnType.Name == "ValueTask")
                {
                    return true;
                }
            }
        }
    }

    return false;
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/imperative-vs-declarative-code-69958908/?t=10)

#### The Declarative Approach

Pattern matching allows for a declarative expression of the same intent.
Instead of writing step-by-step instructions, the code describes the "shape" of the symbol it is looking for.
By using a recursive pattern, the type check, property verification, and nested type narrowing are combined into a single, concise expression.

The declarative version uses the `is` operator followed by the target type and a property pattern `{ ... }`.
Inside the property pattern, we can check properties like `IsAsync` and even drill down into nested properties like `ContainingType.DeclaredAccessibility`.
Furthermore, we can apply another pattern to the `ReturnType` property to narrow it to `INamedTypeSymbol` and check its `Name` property simultaneously.

```csharp
public static bool IsPublicAsyncValueTaskMethod(ISymbol symbol) =>
    symbol is IMethodSymbol
    {
        IsAsync: true,
        ContainingType.DeclaredAccessibility: Accessibility.Public,
        // ReturnType is of type ISymbol, we're using recursive pattern to narrow it.
        ReturnType: INamedTypeSymbol { Name: "ValueTask" }
    };
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/imperative-vs-declarative-code-69958908/?t=42)

While this syntax may require some initial familiarity, it results in code that is significantly more readable and easier to maintain once the pattern is understood.

---

## 3. Two Dimensions of Pattern Matching

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/two-dimensions-of-pattern-matching-69958909/) · 1:14

### Summary

Pattern matching in C# is categorized into two dimensions: the syntactic locations where patterns can be applied and the specific types of patterns available for matching.
This lesson explores the various contexts—such as is expressions, switch constructs, and exception filters—alongside the diverse pattern categories, including type, property, relational, and collection patterns.

### Key concepts

- **Syntactic contexts**: Patterns can be used in `is` expressions, `switch` expressions, `switch` statements, exception filters, and deconstruction.
- **Pattern categories**: Available patterns include type, constant, property, positional, relational, logical, and collection patterns.
- **Exhaustiveness**: Switch expressions require all cases to be covered (exhaustive), whereas switch statements allow for a subset of cases.
- **Deconstruction**: Local variable declarations and `foreach` loop variables utilize deconstruction as a form of pattern matching.

### Lesson notes

Pattern matching can be understood through two dimensions: where patterns are used and what patterns are available.

#### Syntactic Contexts

The most common use of pattern matching is within an `is` expression.
Patterns are also extensively used in `switch` expressions and `switch` statements.
A key distinction is that a `switch` expression must be exhaustive, meaning it must account for all possible input values, whereas a `switch` statement can handle a subset of cases without issue.

Beyond basic branching, patterns can be applied in exception filters using the `when` keyword.
Additionally, deconstructing local variables or loop variables in a `foreach` loop is considered a form of pattern matching.

```csharp
namespace PatternMatchingVariety;

public static class WhereCanIUsePatterns
{
    // is expression
    public static bool HasText(object value) =>
        value is string { Length: > 0 };

    // switch expression
    public static string Describe(object value) =>
        value switch
        {
            string { Length: > 0 } => "text",
            _ => "empty or not text"
        };

    // switch statement
    public static void Print(object value)
    {
        switch (value)
        {
            case string { Length: > 0 }:
                Console.WriteLine("text");
                break;

            default:
                Console.WriteLine("empty or not text");
                break;
        }
    }

    // exception filter
    public static void Run(Action<object> action, object input)
    {
        try
        {
            action(input);
        }
        catch (Exception e) when (input is string { Length: > 0 } text)
        {
            Console.WriteLine($"failed for '{text}': {e.Message}");
        }
    }

    // deconstruction in a local declaration
    public static string Endpoint(Uri uri)
    {
        var (scheme, host, port) = uri;
        return $"{scheme}://{host}:{port}";
    }

    // deconstruction in a foreach loop variable
    public static void Print(Dictionary<string, int> scores)
    {
        foreach (var (name, score) in scores)
            Console.WriteLine($"{name}: {score}");
    }
}

file static class UriExtensions
{
    public static void Deconstruct(this Uri uri, out string scheme, out string host, out int port) =>
        (scheme, host, port) = (uri.Scheme, uri.Host, uri.Port);
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/two-dimensions-of-pattern-matching-69958909/?t=10)

#### Pattern Categories

The second dimension defines the types of patterns available for matching:

1.  **Type patterns**: Test the type of an object and optionally capture it into a variable.
2.  **Constants and Literals**: Match against specific values like `null`, `0`, or string literals.
3.  **Property and Positional patterns**: Match based on the shape of an object, either by checking specific properties or using a deconstructor.
4.  **Relational constraints**: Use operators like `>`, `<`, `>=`, or `<=` to check values.
5.  **Logical composition**: Combine patterns using `and`, `or`, and `not` keywords.
6.  **Collection patterns**: Express and match against the shape and elements of a collection.

```csharp
using System.Text;

namespace PatternMatchingVariety;

public static class PatternKinds
{
    // 1. Type patterns — test, optionally capture.
    public static int Length(object value)
    {
        if (value is StringBuilder)
        {
            return ((StringBuilder)value).Length;
        }

        if (value is string s)
            return s.Length;

        if (value is Array a)
            return a.Length;

        return -1;
    }

    // 2. Constants, literals, null.
    public static string Classify(object? value)
    {
        if (value is null)
            return "missing";

        if (value is 0)
            return "zero";

        if (value is "admin")
            return "admin role";

        return "other";
    }

    // 3. Property and positional shapes.
    public record Point(int X, int Y);
    public static string Locate(Point p)
    {
        // Property pattern.
        if (p is { X: 0, Y: 0 })
            return "origin";

        // Positional pattern (uses Deconstruct).
        if (p is (0, _))
            return "y-axis";

        // Property pattern with only one property checked
        if (p is { Y: 0 })
            return "x-axis";

        return "elsewhere";
    }

    // 4. Relational constraints.
    public static string Bucket(int score) => score switch
    {
        < 0 => "invalid",
        < 50 => "fail",
        < 80 => "pass",
        <= 100 => "distinction",
        _ => "invalid"
    };

    // 5. Logical composition: and, or, not.
    public static bool IsLetter(char c) =>
        c is (>= 'a' and <= 'z') or (>= 'A' and <= 'Z');

    // 6. Shape of the collection.
    public static string Describe(IReadOnlyList<int> xs) => xs switch
    {
        [] => "empty",
        [var only] => $"one: {only}",
        [var first, .., var last] => $"first={first}, last={last}",
    };
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/two-dimensions-of-pattern-matching-69958909/?t=46)

---

## 4. Exploring Pattern Matching Variety

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-pattern-matching-variety-69958910/) · 1:40

### Summary

Pattern matching in C# offers a versatile set of tools for inspecting data structures, ranging from simple type checks to complex collection shapes.
By leveraging type, constant, property, relational, and logical patterns, developers can write more expressive and concise code, particularly when combined with switch expressions to handle various data states and shapes.

### Key concepts

* Type patterns with automatic local variable assignment.
* Constant patterns for matching null, literals, and enums.
* Property patterns for inspecting specific members of an object.
* Positional patterns utilizing an object's Deconstruct method.
* Relational patterns for performing range and comparison checks.
* Logical patterns (and, or, not) for composing complex conditions.
* Collection patterns for matching the structure and elements of arrays or lists.

### Lesson notes

#### Type Patterns

Type patterns allow you to check if an instance is of a specific type and, if so, automatically assign it to a local variable of that type.
This replaces the traditional two-step process of checking a type and then performing a manual cast.

```csharp
public static int Length(object value)
{
    if (value is StringBuilder)
    {
        return ((StringBuilder)value).Length;
    }

    if (value is string s)
        return s.Length;

    if (value is Array a)
        return a.Length;

    return -1;
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-pattern-matching-variety-69958910/?t=10)

#### Constant and Property Patterns

Pattern matching can be used to check for specific constant values, such as `null`, numeric literals, or string literals.
Beyond simple constants, property patterns allow you to match an object based on the values of its properties.
Unlike positional patterns, property patterns only require you to specify the properties you are interested in.

```csharp
public static string Classify(object? value)
{
    if (value is null)
        return "missing";

    if (value is 0)
        return "zero";

    if (value is "admin")
        return "admin role";

    return "other";
}

public record Point(int X, int Y);

public static string Locate(Point p)
{
    // Property pattern matching multiple properties
    if (p is { X: 0, Y: 0 })
        return "origin";

    // Property pattern checking only one property
    if (p is { Y: 0 })
        return "x-axis";

    return "elsewhere";
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-pattern-matching-variety-69958910/?t=25)

#### Positional Patterns

For types that support deconstruction, such as positional records, you can use positional patterns.
These patterns match based on the order of properties in the constructor-like syntax.
If you only wish to check specific positions, you can use the discard symbol (`_`) to skip others.

```csharp
public static string LocatePositional(Point p)
{
    // Positional pattern (uses Deconstruct)
    if (p is (0, _))
        return "y-axis";

    return "elsewhere";
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-pattern-matching-variety-69958910/?t=40)

#### Relational Patterns

Relational patterns allow the use of comparison operators (`<`, `>`, `<=`, `>=`) within pattern matching, which is particularly powerful when used inside switch expressions.
The runtime evaluates these cases sequentially, making it essential to provide a fallback case (the discard pattern `_`) to ensure the expression is exhaustive.

```csharp
public static string Bucket(int score) => score switch
{
    < 0 => "invalid",
    < 50 => "fail",
    < 80 => "pass",
    <= 100 => "distinction",
    _ => "invalid"
};
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-pattern-matching-variety-69958910/?t=70)

#### Logical Composition and Collection Patterns

Patterns can be composed using logical operators: `and`, `or`, and `not`.
Additionally, collection patterns allow you to match the shape of an array or list, including checking for empty collections, specific element counts, or extracting specific elements using the spread operator (`..`).

```csharp
// Logical composition
public static bool IsLetter(char c) =>
    c is (>= 'a' and <= 'z') or (>= 'A' and <= 'Z');

// Collection patterns
public static string Describe(IReadOnlyList<int> xs) => xs switch
{
    [] => "empty",
    [var only] => $"one: {only}",
    [var first, .., var last] => $"first={first}, last={last}",
};
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-pattern-matching-variety-69958910/?t=85)

---

## 5. Recursive Patterns Explained

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/recursive-patterns-explained-69958911/) · 1:25

### Summary

Recursive patterns in C# provide a declarative way to inspect nested data structures, such as the Roslyn symbol hierarchy, by nesting patterns within property patterns.
This approach replaces deeply nested if statements and manual type casting with a single, readable expression.
The lesson demonstrates how the compiler lowers these high-level patterns into standard type checks and casts, ensuring that performance remains consistent with imperative implementations.

### Key concepts

- **Recursive/Nested Patterns**: Patterns that contain other patterns to inspect nested properties.
- **Roslyn Symbol API**: A common use case where symbols (methods, types) contain other symbols.
- **Property Patterns**: Using `{ Property: Pattern }` syntax to drill into object members.
- **Compiler Lowering**: How the C# compiler translates declarative patterns into imperative type checks and casts.

### Lesson notes

When dealing with recursive data structures, such as the Roslyn-based API or reflection-based APIs, symbols often contain other symbols of varying types and shapes.
For instance, a method symbol contains a return type symbol, which might itself be a named type symbol.
Traditionally, analyzing these structures required nested `if` blocks and multiple type casts.

```csharp
public static bool IsPublicAsyncValueTaskMethod_Imperative(ISymbol symbol)
{
    IMethodSymbol? method = symbol as IMethodSymbol;
    if (method != null)
    {
        if (method.IsAsync && 
            method.ContainingType.DeclaredAccessibility == Accessibility.Public)
        {
            // Nested conditions: a candidate for recursive patterns
            INamedTypeSymbol? returnType = method.ReturnType as INamedTypeSymbol;
            if (returnType != null)
            {
                if (returnType.Name == "ValueTask")
                {
                    return true;
                }
            }
        }
    }

    return false;
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/recursive-patterns-explained-69958911/?t=10)

C# allows these nested checks to be flattened using recursive patterns.
A recursive pattern is simply a pattern that contains another pattern.
In the following example, the outer pattern checks if the symbol is an `IMethodSymbol`, and a nested pattern checks if its `ReturnType` property is an `INamedTypeSymbol` with a specific `Name` property.

```csharp
public static bool IsPublicAsyncValueTaskMethod(ISymbol symbol) =>
    symbol is IMethodSymbol
    {
        IsAsync: true,
        ContainingType.DeclaredAccessibility: Accessibility.Public,
        // ReturnType is of type ISymbol, we're using recursive pattern to narrow it.
        ReturnType: INamedTypeSymbol { Name: "ValueTask" }
    };
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/recursive-patterns-explained-69958911/?t=70)

Under the hood, the C# compiler translates these declarative patterns into imperative code.
Using tools like ILSpy or ReSharper's IL viewer, you can see that the compiler generates standard type checks and casts.
Even though the source code is concise, the resulting binary performs the same null checks and property accesses as the manual imperative version.

```csharp
public static bool IsPublicAsyncValueTaskMethod(ISymbol symbol)
{
    if (symbol is IMethodSymbol methodSymbol && methodSymbol.IsAsync)
    {
        INamedTypeSymbol containingType = methodSymbol.ContainingType;
        if (containingType != null && containingType.DeclaredAccessibility == Accessibility.Public)
        {
            ITypeSymbol returnType = methodSymbol.ReturnType;
            if (returnType is INamedTypeSymbol)
                return string.op_Equality(returnType.Name, "ValueTask");
        }
    }
    return false;
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/recursive-patterns-explained-69958911/?t=80)

---

## 6. Pattern Matching Exhaustiveness

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/pattern-matching-exhaustiveness-69958912/) · 0:42

### Summary

Switch expressions in C# require exhaustiveness to ensure that every possible input value results in a valid output.
Unlike switch statements, which can simply fall through, switch expressions must return a value to be assigned or returned.
The compiler attempts to prove that all possible cases are covered; if it cannot guarantee coverage—such as with open value domains like strings or enums that might contain undefined values—it requires a discard pattern (_) or fallback arm to prevent runtime exceptions.

### Key concepts

* **Expression vs. Statement**: Switch expressions must always produce a result, necessitating exhaustive coverage.
* **Compiler Verification**: The compiler can prove exhaustiveness for finite sets like `bool`, `bool?`, and specific tuple combinations.
* **Enum Limitations**: Even if all named enum members are handled, the compiler requires a fallback because enums can be cast from arbitrary underlying numeric values.
* **Open Domains**: Types like `string` or `int` (without exhaustive relational patterns) require a discard pattern.
* **Opaque Guards**: `when` clauses are evaluated at runtime and cannot be used by the compiler to prove exhaustiveness.

### Lesson notes

The fundamental difference between a switch expression and a switch statement is that the expression must produce a result that can be assigned to a variable or returned from a method.
Because of this, the compiler and runtime must ensure that a value is produced by one of the arms for every possible input.

When working with enums, listing every named member is often insufficient to satisfy the compiler's exhaustiveness check.
This is because an enum variable can hold a value that is not defined in its declaration, such as a value cast from an integer or deserialized from an external source.

```csharp
public enum TrafficLight { Red, Yellow, Green }

public static string Light(TrafficLight t) => t switch
{
    TrafficLight.Red    => "stop",
    TrafficLight.Yellow => "slow",
    TrafficLight.Green  => "go",
};
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/pattern-matching-exhaustiveness-69958912/?t=10)

In the example above, even though `Red`, `Yellow`, and `Green` are covered, the compiler will issue a warning (CS8509) because it cannot guarantee that `t` will only ever be one of those three values.
At runtime, a value like `(TrafficLight)999` would cause a `SwitchExpressionException` if no fallback is provided.

#### Verified Exhaustiveness

The compiler can prove exhaustiveness in several scenarios where the domain of values is finite or logically partitioned.
For Boolean types, covering `true` and `false` is sufficient.
For nullable Booleans, the compiler requires `true`, `false`, and `null` to be handled.

```csharp
public static string Bool(bool b) => b switch
{
    true  => "yes",
    false => "no",
};

public static string NullableBool(bool? b) => b switch
{
    true  => "yes",
    false => "no",
    null  => "unknown",
};
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/pattern-matching-exhaustiveness-69958912/?t=38)

The compiler also understands the null axis on reference types and can verify exhaustiveness when both `null` and `not null` patterns are used.

```csharp
public static int Length(string? s) => s switch
{
    null     => 0,
    not null => s.Length,
};
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/pattern-matching-exhaustiveness-69958912/?t=38)

#### Relational and Tuple Exhaustiveness

Exhaustiveness can be achieved through relational patterns and slice patterns by partitioning the value space.
For example, numeric ranges can be partitioned using `<` and `>` operators.
Similarly, the compiler can verify the Cartesian product of finite axes in tuples, such as a tuple of two Booleans.

```csharp
public static string Cell(bool row, bool col) => (row, col) switch
{
    (true,  true)  => "on-on",
    (true,  false) => "on-off",
    (false, true)  => "off-on",
    (false, false) => "off-off",
};
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/pattern-matching-exhaustiveness-69958912/?t=38)

#### Fallback Requirements

In cases where the value domain is open or the logic is opaque to the compiler, a discard pattern (`_`) is mandatory.
This includes:
1. **Strings**: Since every string literal is a unique case, the compiler cannot enumerate all possibilities.
2. **Guards**: Patterns using `when` clauses are opaque because the compiler cannot determine if the predicate will ever evaluate to true for all remaining cases.

```csharp
public static string Dispatch(string method, bool isAuthenticated) =>
    (method, isAuthenticated) switch
    {
        ("GET",  false)              => "Public read",
        ("GET",  true)               => "Private read",
        ("POST", true)               => "Create resource",
        ("POST", false)              => "Unauthorized",
        (var m, _) when IsAllowed(m) => "Allowed method",
        _                            => "Unsupported",
    };
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/pattern-matching-exhaustiveness-69958912/?t=38)

---

## 7. Exhaustiveness Checks in Action

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/exhaustiveness-checks-in-action-69958913/) · 1:50

### Summary

The C# compiler performs exhaustiveness checks on switch expressions to ensure all possible input values are handled.
For types with finite domains, such as booleans, nullable booleans, and reference types (via null/not null checks), the compiler can verify total coverage and omit the need for a fallback arm.
However, for types with open domains like strings, or when using enums and 'when' clauses, the compiler cannot guarantee coverage, necessitating a discard pattern (_) to avoid compiler warnings (CS8509).

### Key concepts

*   **Exhaustiveness Verification**: The compiler's ability to prove every possible case is handled without a fallback arm.
*   **Finite Domains**: Types like `bool` and `bool?` where the compiler knows all possible values.
*   **Partitioning**: Using relational patterns (numeric ranges) or slice patterns (collection lengths) to cover a domain.
*   **Open Domains**: Types like `string` or `int` where the compiler cannot enumerate every possible literal.
*   **Enum Safety**: Enums require fallbacks because they can be cast to underlying numeric values not defined in the enum members.
*   **Opaque Predicates**: `when` clauses are treated as conditional coverage that the compiler cannot verify as exhaustive.

### Lesson notes

#### Verified Coverage

The compiler attempts to prove that every case is handled.
If it can prove coverage, no fallback arm (`_ =>`) is required.
For booleans, there are only two options; if one is missing, the compiler issues a warning.
For nullable booleans, the compiler requires handling `true`, `false`, and `null` to reach exhaustiveness.

```csharp
public static class Verified
{
    // bool. Two values, both must appear.
    public static string Bool(bool b) => b switch
    {
        true  => "yes",
        false => "no",
    };

    // bool?. Three values: true, false, null.
    public static string NullableBool(bool? b) => b switch
    {
        true  => "yes",
        false => "no",
        null  => "unknown",
    };

    // Null axis on reference types.
    public static int Length(string? s) => s switch
    {
        null     => 0,
        not null => s.Length,
    };
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/exhaustiveness-checks-in-action-69958913/?t=10)

#### Relational and Slice Patterns

In relational patterns, the compiler reasons about the partition of the numeric domain rather than individual values.
If a segment of the range is missing (for example, the zero case in a sign check), the compiler will warn about the missing case.

```csharp
    // Numeric ranges via relational patterns.
    // The compiler reasons about the partition, not the values.
    public static string Sign(int x) => x switch
    {
        < 0 => "negative",
        //0   => "zero",
        > 0 => "positive",
    };
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/exhaustiveness-checks-in-action-69958913/?t=25)

Collection patterns function similarly to relational patterns by partitioning the domain based on the length of the collection.
Using the spread operator (`..`) allows a pattern to match any remaining collection length, effectively acting as a fallback within the pattern itself.

```csharp
    // Collections via slice patterns. Length is partitioned the same
    // way relational patterns partition numbers.
    public static string Bucket(int[] xs) => xs switch
    {
        null       => "none",
        []         => "empty",
        [_]        => "single",
        //[_, _, ..] => "two or more",
    };
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/exhaustiveness-checks-in-action-69958913/?t=40)

#### Tuples

Tuples represent the Cartesian product of their constituent types.
If the axes of the tuple are finite (such as a tuple of two booleans), the compiler can prove exhaustiveness if all combinations are provided.

```csharp
    // Tuples. Cartesian product of finite axes.
    public static string Cell(bool row, bool col) => (row, col) switch
    {
        (true,  true)  => "on-on",
        (true,  false) => "on-off",
        (false, true)  => "off-on",
        (false, false) => "off-off",
    };
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/exhaustiveness-checks-in-action-69958913/?t=55)

#### Cases Requiring Fallbacks

Fallbacks are mandatory for types with open domains, such as strings or integers where the full range is not partitioned.
Additionally, enums require a fallback arm because it is possible to cast any integer to an enum type at runtime (e.g., `(TrafficLight)999`), meaning the named members do not strictly define the entire domain.

```csharp
public static class FallbackRequired
{
    // string values. Open value domain — every string literal is a
    // case the compiler cannot enumerate.
    public static int Priority(string level) => level switch
    {
        "low"    => 1,
        "medium" => 2,
        "high"   => 3,
        _        => 0,
    };

    public enum TrafficLight { Red, Yellow, Green }

    public static string Light(TrafficLight t) => t switch
    {
        TrafficLight.Red    => "stop",
        TrafficLight.Yellow => "slow",
        TrafficLight.Green  => "go",
        // Fallback required to handle values like (TrafficLight)999
    };
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/exhaustiveness-checks-in-action-69958913/?t=70)

#### The `when` Clause

A `when` clause contains a predicate that must be true for the pattern to match.
Because these predicates can contain arbitrary code or method calls, they are opaque to the compiler.
Even if a developer knows the predicates cover all possibilities, the compiler cannot guarantee it and will demand a fallback arm.

```csharp
    // `when` clauses with method calls are opaque to the compiler.
    public static string Dispatch(string method, bool isAuthenticated) =>
        (method, isAuthenticated) switch
        {
            ("GET",  false)              => "Public read",
            ("GET",  true)               => "Private read",
            ("POST", true)               => "Create resource",
            ("POST", false)              => "Unauthorized",
            (var m, _) when IsAllowed(m) => "Allowed method",
            _                            => "Unsupported",
        };

    private static bool IsAllowed(string method) => true;

    public static string Toggle(string name, bool flag) 
        => flag switch
        {
            true when name != null => "allowed",
            false => "off",
            // Even if name is logically never null here, the compiler needs a fallback
            _ => "unknown"
        };
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/exhaustiveness-checks-in-action-69958913/?t=85)

---

## 8. Open and Closed Hierarchies

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/open-and-closed-hierarchies-69958914/) · 0:42

### Summary

This lesson explores the fundamental differences between open and closed hierarchies in C# and how they impact pattern matching exhaustiveness.
While standard class hierarchies are 'open' and can be extended at any time, requiring a default arm in switch expressions to handle unknown subtypes, union types represent 'closed' hierarchies.
In a closed hierarchy, the compiler knows the exact set of possible types, allowing it to verify that every case is handled without a fallback arm and ensuring that adding a new type becomes a compile-time breaking change rather than a runtime risk.

### Key concepts

- **Open Hierarchies**: Standard class inheritance where the base class can be extended by new subtypes at any point, preventing the compiler from knowing the full set of possible implementations.
- **Closed Hierarchies (Union Types)**: A fixed set of types where the compiler is aware of every possible variant, enabling exhaustive pattern matching.
- **Exhaustiveness Checks**: The compiler's ability to prove that all possible values or types for a given expression are handled in a switch expression.
- **Breaking Changes**: In a closed hierarchy, adding a new member is a breaking change because it invalidates existing exhaustive switch expressions.

### Lesson notes

#### Open Hierarchies and Class Inheritance

In a standard C# class hierarchy, the relationship between a base class and its derived types is considered "open."
Even if all current subtypes are marked as `sealed`, the base class itself remains open to further derivation unless specifically restricted.
Because the compiler cannot guarantee that a fourth or fifth subtype won't be added in a different assembly or at a later time, it cannot perform a complete exhaustiveness check.

When using a switch expression to process an open hierarchy, the compiler requires a discard pattern (`_`) or a default case.
Without it, the compiler cannot be certain that every possible `CliArgument` is handled.

```csharp
namespace OpenVsClosedHierarchies.Open;

// Open hierarchy: anyone can derive a new CliArgument.
// `sealed` on each subtype does not close the *base*.
public abstract class CliArgument;

public sealed class Flag(string name) : CliArgument
{
    public string Name { get; } = name;
}

public sealed class Option(string name, string value) : CliArgument
{
    public string Name { get; } = name;
    public string Value { get; } = value;
}

public sealed class Positional(string value) : CliArgument
{
    public string Value { get; } = value;
}

public static class Processor
{
    public static string Render(CliArgument arg) => arg switch
    {
        Flag f       => $"--{f.Name}",
        Option o     => $"--{o.Name} {o.Value}",
        Positional p => p.Value,
        // What to do here? Throw, return null?
        _            => "",
    };
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/open-and-closed-hierarchies-69958914/?t=13)

#### Closed Hierarchies and Union Types

Closed hierarchies, often implemented as union types, define a fixed set of objects.
In this model, the compiler is explicitly aware of every possible type within the set (e.g., `Flag`, `Option`, and `Positional`).
Because the set is fixed, adding a fourth item is a breaking change that the compiler can detect across the entire codebase.

This allows for true exhaustiveness checks.
In the following example, the `RenderKeyword` method does not require a default arm because the compiler can prove that all cases of the `CliArgument` union are covered.
If a new case were added to the union definition, the switch expression would immediately produce a compiler error, forcing the developer to handle the new case.

```csharp
using System.Runtime.CompilerServices;

public record Flag(string Name);
public record Option(string Name, string Value);
public record Positional(string Value);

// 1. Keyword form. Compiler-generated union; storage and case set are implicit.
//    CliArgument = Flag | Option | Positional
public union CliArgument(Flag Flag, Option Option, Positional Positional);

// 2. Manual form. Plain record with [Union] applied.
[Union]
public record CliArgumentManual
{
    public CliArgumentManual(Flag value)       => Value = value;
    public CliArgumentManual(Option value)     => Value = value;
    public CliArgumentManual(Positional value) => Value = value;

    public object Value { get; }
}

public static class ClosedProcessor
{
    // Exhaustive over the keyword union — no default arm needed.
    public static string RenderKeyword(CliArgument arg) => arg switch
    {
        Flag f       => $"--{f.Name}",
        Option o     => $"--{o.Name} {o.Value}",
        Positional p => p.Value,
    };

    public static string RenderManual(CliArgumentManual arg) => arg switch
    {
        Flag f       => $"--{f.Name}",
        Option o     => $"--{o.Name} {o.Value}",
        Positional p => p.Value,
    };
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/open-and-closed-hierarchies-69958914/?t=28)

#### Exhaustiveness in Other Contexts

The compiler also performs exhaustiveness checks on other finite domains.
For example, a switch over a `bool` is exhaustive if both `true` and `false` are handled.
Similarly, relational patterns that cover the entire range of a numeric type or slice patterns that cover all possible collection lengths can be proven exhaustive by the compiler.

Conversely, types with open value domains, such as `string` or `enum` (which can hold underlying values not defined in the enumeration), always require a fallback arm to satisfy the compiler's exhaustiveness requirements.

---

## 9. Exhaustiveness Checks for Classes and Unions

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/exhaustiveness-checks-for-classes-and-unions-69958915/) · 1:43

Exhaustiveness checking ensures that a switch expression handles every possible input value.
The compiler's ability to verify this depends on whether the type hierarchy is open or closed.

### Key concepts

- **Open Hierarchies**: Standard class hierarchies where the compiler cannot know all possible subclasses, requiring a fallback arm (`_`).
- **Closed Hierarchies**: Union types that define a fixed set of cases, allowing the compiler to prove exhaustiveness.
- **Struct vs. Class Unions**: Struct-based unions (keyword form) may require a `null` or `default` check, while class-based unions with non-nullable properties may not.
- **Compile-time vs. Runtime**: Unhandled cases trigger compiler warning CS8509 and result in a `SwitchExpressionException` at runtime.

### Lesson notes

#### Open Hierarchies and Fallback Requirements

In a standard class hierarchy, the compiler treats the set of possible types as "open."
Even if you handle all currently defined subclasses, the compiler cannot guarantee that a new subclass won't be added in a different assembly.
Consequently, it requires a fallback arm (discard pattern `_`) to satisfy exhaustiveness checks.

```csharp
namespace OpenVsClosedHierarchies.Open;

public abstract class CliArgument;
public sealed class Flag(string name) : CliArgument { public string Name { get; } = name; }
public sealed class Option(string name, string value) : CliArgument
{
    public string Name { get; } = name;
    public string Value { get; } = value;
}

public sealed class Positional(string value) : CliArgument
{
    public string Value { get; } = value;
}

public static class Processor
{
    public static string Render(CliArgument arg) => arg switch
    {
        Flag f       => $"--{f.Name}",
        Option o     => $"--{o.Name} {o.Value}",
        Positional p => p.Value,
        // What to do here? Throw, return null?
        //_          => "",
    };
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/exhaustiveness-checks-for-classes-and-unions-69958915/?t=10)

#### Closed Hierarchies with Union Types

Union types allow for closed hierarchies where the compiler knows the exact set of possible cases.
There are two primary ways to define these: the keyword form and the manual form using the `[Union]` attribute.

```csharp
public record Flag(string Name);
public record Option(string Name, string Value);
public record Positional(string Value);

// 1. Keyword form. Compiler-generated union; storage and case set are implicit.
//    CliArgument = Flag | Option | Positional
public union CliArgument(Flag Flag, Option Option, Positional Positional);

// 2. Manual form. Plain record with [Union] applied.
//    Same conceptual shape, explicit storage, recognized by the same pattern lookup.
[Union]
public record CliArgumentManual
{
    public CliArgumentManual(Flag value)       => Value = value;
    public CliArgumentManual(Option value)     => Value = value;
    public CliArgumentManual(Positional value) => Value = value;

    public object Value { get; }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/exhaustiveness-checks-for-classes-and-unions-69958915/?t=25)

#### Exhaustiveness and Nullability

The implementation of the union affects how the compiler views the "default" or `null` state.

1.  **Struct Unions (Keyword form)**: Because these are generated as structs, they can be created using `default(CliArgument)`. The compiler may require a `null` check or a fallback to handle this uninitialized state.
2.  **Class Unions (Manual form)**: When using a record or class with the `[Union]` attribute, you can define properties as non-nullable. In this scenario, the compiler is satisfied if all defined cases are handled, and no `null` check is required for exhaustiveness.

```csharp
public static class ClosedProcessor
{
    // Exhaustive over the keyword union - no default arm needed.
    // Adding a fourth case to CliArgument turns this into a compile error.
    // (CS8655 about null is suppressed: union default-state is a separate concern.)
    public static string RenderKeyword(CliArgument arg) => arg switch
    {
        Flag f       => $"--{f.Name}",
        Option o     => $"--{o.Name} {o.Value}",
        Positional p => p.Value,
        // null => "",
    };

    public static string RenderManual(CliArgumentManual arg) => arg switch
    {
        Flag f       => $"--{f.Name}",
        Option o     => $"--{o.Name} {o.Value}",
        Positional p => p.Value,
    };
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/exhaustiveness-checks-for-classes-and-unions-69958915/?t=55)

If a case is removed (e.g., commenting out the `Positional` case), the compiler will issue a warning indicating that the switch expression does not handle all possible inputs.

#### Runtime Behavior

If a developer ignores the compile-time warning and an unhandled case is encountered at runtime, the application will crash with a `System.Runtime.CompilerServices.SwitchExpressionException`.

```csharp
CliArgument arg = new(new Flag("verbose"));

Console.WriteLine(ClosedProcessor.RenderKeyword(arg));
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/exhaustiveness-checks-for-classes-and-unions-69958915/?t=75)

#### General Exhaustiveness Rules

The compiler can prove exhaustiveness for several built-in types and patterns without a fallback arm:
- **Booleans**: Handling both `true` and `false`.
- **Nullable Booleans**: Handling `true`, `false`, and `null`.
- **Null Axis**: Handling both `null` and `not null` for reference types.
- **Numeric Ranges**: Using relational patterns that partition the entire range (e.g., `< 0`, `0`, and `> 0`).
- **Tuples**: Handling the Cartesian product of all possible values for each element in the tuple.

Conversely, a fallback arm is mandatory for:
- **Strings**: The domain of string literals is infinite.
- **Enums**: While the compiler knows the named members, any integer can be cast to the enum type at runtime.
- **When Clauses**: Predicates in `when` clauses are opaque to the compiler's static analysis.

---

## 10. Know When to Stop

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/know-when-to-stop-69958916/) · 1:12

### Summary

Pattern matching in C# is a powerful tool for expressing intent through syntactic sugar, but it should be used judiciously to maintain readability.
While the compiler translates complex patterns into efficient type checks and nested logic, developers should avoid overly clever or deeply nested patterns that obscure logic.
Switch expressions are particularly valuable for expression-based programming due to their compile-time exhaustiveness checks, though certain types like strings or enums still require fallback arms to ensure a value is always produced.

### Key concepts

* **Readability over Cleverness**: Production code should prioritize maintainability; if a pattern becomes too complex, it should be refactored or moved to imperative code.
* **Syntactic Sugar**: The compiler handles the complexity of type checks and local variable introduction behind the scenes.
* **Pattern Translation**: Property patterns translate to member access, while recursive patterns translate into nested `if` blocks.
* **Exhaustiveness Checks**: Switch expressions require all possible cases to be handled to guarantee a return value, verified by the compiler at both compile-time and runtime.
* **Expression-Based Programming**: Using switch expressions allows for more declarative and concise code compared to traditional statement-based logic.

### Lesson notes

#### The Limits of Pattern Matching

Production code is not a competition to see who can write the most clever code; it is about readability and maintainability.
Pattern matching is a tool to better express intent, but when a pattern becomes too complicated to reason about, it is better to split it apart or revert to imperative code.
We are paid to write maintainable solutions that solve customer problems, not to maximize the use of complex language features.

Behind the scenes, the compiler treats pattern matching as syntactic sugar.
It introduces the necessary type checks and local variables.
Property patterns are translated into member access, and recursive patterns are translated into nested `if` blocks.

#### Refactoring Complex Patterns

A deeply nested recursive pattern can quickly become unreadable.
For example, a pattern attempting to identify a specific public async method returning a `ValueTask<int>` might look like this:

```csharp
public static bool IsPublicAsyncValueTaskOfInt(ISymbol symbol) =>
symbol is IMethodSymbol
{
    DeclaredAccessibility: Accessibility.Public,
    IsAsync: true,
    ContainingType:
    {
        DeclaredAccessibility: Accessibility.Public,
        IsStatic: false,
    },
    ReturnType: INamedTypeSymbol
    {
        Name: "ValueTask",
        TypeArguments: [INamedTypeSymbol { SpecialType: SpecialType.System_Int32 }],
        ContainingNamespace:
        {
            Name: "Tasks",
            ContainingNamespace.Name: "Threading",
        },
    },
};
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/know-when-to-stop-69958916/?t=10)

While technically correct, this is difficult to maintain.
It is often better to decompose such logic into smaller, descriptive methods that use simpler patterns:

```csharp
public static bool IsPublicAsyncValueTaskOfInt(ISymbol symbol) =>
    symbol is IMethodSymbol method
    && IsPublicAsync(method)
    && IsOnPublicInstanceType(method)
    && ReturnsValueTaskOfInt(method);

static bool IsPublicAsync(IMethodSymbol m) =>
    m is { DeclaredAccessibility: Accessibility.Public, IsAsync: true };
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/know-when-to-stop-69958916/?t=10)

#### Switch Expressions and Exhaustiveness

One of the most effective ways to use pattern matching is through switch expressions.
Because an expression must always produce a value, the compiler performs exhaustiveness checks during compilation.

For finite domains, the compiler can often prove that every case is handled without a fallback arm:

```csharp
// bool. Two values, both must appear.
public static string Bool(bool b) => b switch
{
    true  => "yes",
    false => "no",
};

// bool?. Three values: true, false, null.
public static string NullableBool(bool? b) => b switch
{
    true  => "yes",
    false => "no",
    null  => "unknown",
};
```

However, for open value domains or types where the compiler cannot enumerate every possibility, a fallback arm (`_`) is required.
This includes `string` values and `enum` members (since an enum can technically hold any value of its underlying type at runtime via casting).

```csharp
public enum TrafficLight { Red, Yellow, Green }

public static string Light(TrafficLight t) => t switch
{
    TrafficLight.Red    => "stop",
    TrafficLight.Yellow => "slow",
    TrafficLight.Green  => "go",
    _                   => "invalid"
};
```

If the compiler's exhaustiveness warning is ignored and an unhandled case occurs at runtime, a `SwitchExpressionException` will be thrown.
Pattern matching is just a tool; use it appropriately to ensure your code remains both robust and readable.
