# Mastering Argument Validation

> Course: [Mastering: C#](https://dometrain.com/course/mastering-csharp/) · Chapter 8
> 10 lessons · ~11:28
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Section Overview](https://dometrain.com/take/course/mastering-csharp-3256129/section-overview-69958880/) | 1:04 | [↓](#1-section-overview) |
| 2 | [Overview](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958881/) | 0:27 | [↓](#2-overview) |
| 3 | [Argument Validation and Nullable Reference Types](https://dometrain.com/take/course/mastering-csharp-3256129/argument-validation-and-nullable-reference-types-69958882/) | 1:38 | [↓](#3-argument-validation-and-nullable-reference-types) |
| 4 | [Argument Null Checks in C#](https://dometrain.com/take/course/mastering-csharp-3256129/argument-null-checks-in-csharp-69958883/) | 0:59 | [↓](#4-argument-null-checks-in-c) |
| 5 | [Checking for Null in C#](https://dometrain.com/take/course/mastering-csharp-3256129/checking-for-null-in-csharp-69958884/) | 1:31 | [↓](#5-checking-for-null-in-c) |
| 6 | [Recreating ThrowIfNull Manually](https://dometrain.com/take/course/mastering-csharp-3256129/recreating-throwifnull-manually-69958885/) | 2:21 | [↓](#6-recreating-throwifnull-manually) |
| 7 | [Analyzing ThrowIfNull Boxing Allocations](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-throwifnull-boxing-allocations-69958886/) | 1:19 | [↓](#7-analyzing-throwifnull-boxing-allocations) |
| 8 | [Issues With Nullable Strings in .NET Framework](https://dometrain.com/take/course/mastering-csharp-3256129/issues-with-nullable-strings-in-dotnet-framework-69958887/) | 0:37 | [↓](#8-issues-with-nullable-strings-in-net-framework) |
| 9 | [Implementing Custom String Validation](https://dometrain.com/take/course/mastering-csharp-3256129/implementing-custom-string-validation-69958888/) | 0:38 | [↓](#9-implementing-custom-string-validation) |
| 10 | [Summary](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958889/) | 0:54 | [↓](#10-summary) |

---

## 1. Section Overview

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/section-overview-69958880/) · 1:04

### Summary

This lesson introduces the "Expressive C#" module, which focuses on writing high-level, declarative code that is both readable and performant.
The curriculum covers four primary pillars: robust argument validation techniques, the expanded "Extension Everything" capabilities of C# 14, the internal mechanics and performance costs of delegates and lambdas, and the expressive power of pattern matching.
By examining these features "under the hood," developers can understand the implementation details and performance implications for their systems.

### Key concepts

- **Argument Validation**: Ensuring runtime safety even when Nullable Reference Types (NRTs) are enabled.
- **Extension Everything**: Utilizing C# 14 features to extend types with properties, indexers, and static members.
- **Delegates and Lambdas**: Understanding compiler lowering, closures, and the associated allocation costs.
- **Pattern Matching**: Leveraging recursive patterns and functional-style constructs for expressive logic.
- **Performance Awareness**: Analyzing how high-level abstractions impact the underlying system.

### Lesson notes

The module is designed to move beyond basic syntax, focusing on how to write code that is expressive and declarative while remaining aware of the underlying implementation.

#### Argument Validation

While C# provides Nullable Reference Types to help catch null-related issues at compile time, runtime validation remains essential.
Callers may be "nullable-oblivious," use the null-forgiving operator (`!`), or provide null values through reflection, deserialization, or default struct values.
The module explores how to implement efficient null checks and build custom guard helpers similar to `ArgumentNullException.ThrowIfNull` to ensure system integrity.

#### Extension Everything

C# has supported extension methods for a long time, but C# 14 significantly expands this capability.
Developers can now add a wider range of members to types they do not own, including instance and static properties, indexers, and operators.
This allows for a more natural API shape on existing types without modifying the original source code.

#### Delegates and Lambdas

This section examines how the compiler implements delegates and lambdas.
It covers the lowering process where lambdas are transformed into methods or classes (DisplayClasses), the mechanics of closures, and the performance implications of capturing state.
Understanding these details is critical for identifying and avoiding unnecessary allocations in performance-sensitive code.

#### Pattern Matching

Pattern matching is a powerful tool for making code more declarative and expressive.
The module covers various pattern types, including type, constant, property, positional, relational, and logical patterns.
It specifically highlights recursive patterns, where property patterns can be nested to perform complex validation and downcasting in a single, readable expression.

The following structure outlines the topics covered in this section:

```markdown
# Expressive C#

Extension everything, argument validation, pattern matching, delegates, and lambdas.

## Topics

- [Extension Everything](ExtensionEverything/README.md)
- [Argument Validation](ArgumentValidation/README.md)
- [Pattern Matching](PatternMatching/README.md)
- [Delegates and Lambdas](DelegatesAndLambdas/README.md)
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/section-overview-69958880/?t=0)

---

## 2. Overview

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958881/) · 0:27

### Summary

Modern C# argument validation aims to replace repetitive, error-prone if blocks with declarative guard clauses.
This lesson demonstrates how to implement a custom Guard class that leverages [CallerArgumentExpression] to automatically capture parameter names, [NotNull] to assist static analysis, and [DoesNotReturn] to optimize the calling method's control flow.
By centralizing validation logic, developers can ensure consistency across a codebase while maintaining the performance characteristics of built-in .NET validation helpers.

### Key concepts

- Null checking variations (`== null` vs `is null`).
- Drawbacks of explicit `if` blocks for validation.
- Implementing custom validation helpers (Guard clauses).
- Using `[CallerArgumentExpression]` to automatically capture parameter names.
- Optimizing exception throwing with `[DoesNotReturn]` and separate throw methods.

### Lesson notes

Argument validation should be simple, consistent, and declarative.
A fundamental part of this is checking for nulls.
While multiple ways to check for null exist in C#, they behave differently.
Using `is null` is generally preferred over `== null` because the equality operator can be overloaded, potentially changing the behavior of the check, whereas `is null` always performs a reference check against null.

```csharp
X x = new X();

Console.WriteLine($"  x == null: {x == null}");
Console.WriteLine($"  x is null: {x is null}");
Console.WriteLine($"  ReferenceEquals(x, null): {ReferenceEquals(x, null)}");
Console.WriteLine($"  x is not object: {x is not object}");
Console.WriteLine($"  x is not {{ }}: {x is not { }}");

sealed class X
{
    public static bool operator ==(X? left, X? right) => true;
    public static bool operator !=(X? left, X? right) => true;
    public override bool Equals(object? obj) => ReferenceEquals(this, obj);
    public override int GetHashCode() => 0;
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958881/?t=6)

Explicit null checks using `if` blocks can become verbose and repetitive across a codebase.
To solve this, we can build a custom validation helper, often called a `Guard` class.
This approach is similar to the `ArgumentNullException.ThrowIfNull` method introduced in .NET 6 and 7.
By using the `[CallerArgumentExpression]` attribute, the helper can automatically capture the name of the variable passed to it, ensuring that the `ParamName` property of the resulting exception is accurate without requiring the developer to manually pass a string.

```csharp
public sealed class Items
{
    private readonly List<Item> _items = new();

    public void AddItem(Item? item)
    {
        Guard.ThrowIfNull(item);
        _items.Add(item);
    }
}

static class Guard
{
    public static void ThrowIfNull(
        [NotNull] object? argument,
        [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        if (argument is null)
            Throw(paramName);
    }

    [DoesNotReturn]
    private static void Throw(string? paramName)
        => throw new ArgumentNullException(paramName);
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958881/?t=16)

To make these helpers efficient and declarative, we use specific attributes.
The `[NotNull]` attribute informs the compiler's static analysis that if the method returns, the argument is guaranteed not to be null.
The `[DoesNotReturn]` attribute on a dedicated `Throw` method helps the compiler understand the control flow, often resulting in better code generation by allowing the main validation method to be inlined while keeping the exception-throwing logic separate.

Beyond simple null checks, the `Guard` pattern can be extended to handle various types of validation, such as checking for empty strings or numeric ranges, while maintaining a clean and readable API at the call site.

```csharp
static class Guard
{
    public static void AgainstNullOrWhiteSpace(
        [NotNull] string? value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (value is null)
            ThrowArgumentNull(paramName);

        if (string.IsNullOrWhiteSpace(value))
            ThrowNullOrWhiteSpace(paramName);
    }

    public static void AgainstNegativeOrZero(
        int value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (value <= 0)
            ThrowOutOfRange(paramName, value, "Value must be greater than zero.");
    }

    [DoesNotReturn]
    private static void ThrowArgumentNull(string? paramName) => throw new ArgumentNullException(paramName);

    [DoesNotReturn]
    private static void ThrowNullOrWhiteSpace(string? paramName) =>
        throw new ArgumentException("Value cannot be null, empty, or whitespace.", paramName);

    [DoesNotReturn]
    private static void ThrowOutOfRange(string? paramName, object? actualValue, string message) =>
        throw new ArgumentOutOfRangeException(paramName, actualValue, message);
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958881/?t=24)

---

## 3. Argument Validation and Nullable Reference Types

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/argument-validation-and-nullable-reference-types-69958882/) · 1:38

### Summary

Nullable Reference Types (NRTs) in C# provide compile-time warnings to help prevent null-related errors, but they do not offer runtime enforcement.
Because null values can still enter a system through uninitialized arrays, null-suppression operators, or code where nullability is disabled, eager argument validation remains essential for public methods to prevent the "butterfly effect" of delayed and hard-to-trace failures.

### Key concepts

- Nullable Reference Types (NRTs) are a compile-time feature with no runtime semantics.
- Eager argument validation prevents the "butterfly effect" of delayed null-related failures.
- Null values can enter NRT-enabled code through `#nullable disable` blocks, the null-forgiving operator (`!`), or uninitialized arrays.

### Lesson notes

Since the introduction of C# 8, developers can distinguish between nullable and non-nullable reference types.
When NRTs are enabled (often via the `<Nullable>enable</Nullable>` setting in the project file), the compiler assumes that a variable of a reference type, such as `string`, cannot be null unless explicitly marked with a `?`.
However, it is important to understand that this is strictly a compile-only feature; it does not introduce runtime semantics to prevent null assignments.

Eager argument validation is critical to avoid the "butterfly effect."
If a null value is allowed to enter a system, a `NullReferenceException` might not occur immediately.
Instead, the error may manifest much later in a different part of the application, making the root cause difficult to identify.

Consider the following example of a public class where an `Item` is added to an internal collection.
Even with NRTs enabled, the method should validate its arguments to ensure no null values are stored:

```csharp
#nullable enable
public class Items
{
    private readonly List<Item> _items = new();

    // This is a public method: should
    // we validate the arguments?
    // For public methods: Yes!
    public void AddItem(Item item)
    {
        _items.Add(item);
    }
}

#nullable disable
// No compiler warning,
// nullability is off
items.AddItem(item: null);

#nullable enable
items.AddItem(GetItem()!);

static Item? GetItem() => null;

#nullable enable

Item[] array = new Item[42];
// No warnings!
items.AddItem(array[0]);
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/argument-validation-and-nullable-reference-types-69958882/?t=10)

There are three primary ways a null value can bypass the compiler's nullability analysis and enter a non-nullable context:

1.  **Non-nullable contexts**: Code may be called from blocks where nullability is explicitly disabled using `#nullable disable`. This can happen within your own application or when your code is used as a reusable library in a project where NRTs are turned off.
2.  **Nullability suppression**: Developers can use the null-forgiving operator (`!`) to suppress compiler warnings. While not a recommended practice, it is often used when the logic is too complex for the compiler to track or when the developer trusts their own logic over the compiler's analysis.
3.  **Array initialization gaps**: The C# compiler has known gaps in its nullability analysis regarding arrays. It assumes that all elements within an array are properly initialized upon instantiation. Consequently, passing an element from a newly allocated array (e.g., `new Item[42]`) to a method expecting a non-nullable type will not trigger a warning, even though the value is null at runtime.

---

## 4. Argument Null Checks in C#

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/argument-null-checks-in-csharp-69958883/) · 0:59

### Summary

This lesson covers various strategies for argument null validation in C#, ranging from modern .NET 6+ APIs to legacy patterns and custom extension methods.
It highlights the importance of choosing the right validation technique based on the target framework and explains the technical differences between various null-checking syntaxes, such as == null, is null, and ReferenceEquals, particularly in the context of operator overloading.

### Key concepts

- `ArgumentNullException.ThrowIfNull` (.NET 6+).
- Conditional compilation for cross-framework null validation.
- Fluent validation via extension methods.
- Comparison of null-checking patterns: `== null`, `is null`, and `ReferenceEquals`.
- Impact of operator overloading on null checks.

### Lesson notes

The simplest way to enforce that a parameter is not null is using the `ArgumentNullException.ThrowIfNull` method.
This static method provides a concise way to validate arguments and throw an exception if the provided value is null.

```csharp
public void AddItem(Item item)
{
    ArgumentNullException.ThrowIfNull(item);
    _items.Add(item);
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/argument-null-checks-in-csharp-69958883/?t=10)

However, this API is only available in .NET Core starting with version 6.
It is not available in .NET Standard or the full .NET Framework.
If a project targets multiple frameworks, such as .NET Framework 4.8 and .NET 6.0, conditional compilation can be used to provide the appropriate validation logic for each target.

```csharp
public void AddItem(Item item)
{
#if NET
    // Only available in .NET 6.0 and later.
    ArgumentNullException.ThrowIfNull(item);
#else
    // an old style validation for .NET Framework
    if (item == null)
    {
        throw new ArgumentNullException(nameof(item));
    }
#endif

    _items.Add(item);
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/argument-null-checks-in-csharp-69958883/?t=10)

Another popular approach is using extension methods to enforce nullability.
This allows for inline validation where the method returns the non-null object, which can then be passed directly to another method or assigned to a field.

```csharp
public void AddItem(Item item)
{
    // Using a custom extension method to throw if null
    _items.Add(item.ThrowIfNull());
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/argument-null-checks-in-csharp-69958883/?t=10)

Beyond these helper methods, C# provides several ways to check if an object is null.
It is important to understand how these behave, especially when operators are overloaded.
For example, if the `==` operator is overloaded to always return `true`, a standard `x == null` check will fail to identify a null reference correctly.
In such cases, using `is null` or `ReferenceEquals` is safer as they ignore operator overloads.

```csharp
X x = new X();

Console.WriteLine($"  x == null: {x == null}");
Console.WriteLine($"  x is null: {x is null}");
Console.WriteLine($"  ReferenceEquals(x, null): {ReferenceEquals(x, null)}");
Console.WriteLine($"  x is not object: {x is not object}");
Console.WriteLine($"  x is not {{ }}: {x is not { }}");

sealed class X
{
    public static bool operator ==(X? left, X? right) => true;
    public static bool operator !=(X? left, X? right) => true;
    public override bool Equals(object? obj) => ReferenceEquals(this, obj);
    public override int GetHashCode() => 0;
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/argument-null-checks-in-csharp-69958883/?t=56)

---

## 5. Checking for Null in C#

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/checking-for-null-in-csharp-69958884/) · 1:31

### Summary

This lesson explores various methods for null checking in C#, including the equality operator, the is keyword, and pattern matching.
It highlights the risks of using the == operator due to potential operator overloading and establishes is null as the industry standard for reliable null validation.
Additionally, it compares manual argument validation across different .NET versions and introduces the motivation for creating custom guard clauses to improve code readability and maintainability.

### Key concepts

- Comparison of null-checking techniques: `== null`, `is null`, `ReferenceEquals`, and pattern matching.
- Risks of operator overloading with the `==` operator.
- Industry preference for `is null` for semantic correctness.
- Evolution of argument validation from manual checks to `ArgumentNullException.ThrowIfNull`.
- Motivation for custom guard implementations using `CallerArgumentExpression`.

### Lesson notes

There are several ways to check if a variable is null in C#.
Common approaches include the equality operator (`==`), the `is null` constant pattern, `ReferenceEquals`, and other forms of pattern matching such as checking if an object is not a specific type or does not match an empty property pattern `{ }`.

```csharp
X x = new X();

Console.WriteLine($"  x == null: {x == null}");
Console.WriteLine($"  x is null: {x is null}");
Console.WriteLine($"  ReferenceEquals(x, null): {ReferenceEquals(x, null)}");
Console.WriteLine($"  x is not object: {x is not object}");
Console.WriteLine($"  x is not {{ }}: {x is not { }}");
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/checking-for-null-in-csharp-69958884/?t=10)

While these checks often return the same result, there are significant semantic differences.
The `==` operator can be overloaded by a developer, which can lead to unexpected behavior where a non-null object evaluates as null.
Because of this theoretical risk, the C# community has adopted `is null` as the standard for null checking, as it ignores operator overloads and checks for literal nullity.

```csharp
X x = new X();

Console.WriteLine($"  x == null: {x == null}");
Console.WriteLine($"  x is null: {x is null}");
Console.WriteLine($"  ReferenceEquals(x, null): {ReferenceEquals(x, null)}");
Console.WriteLine($"  x is not object: {x is not object}");
Console.WriteLine($"  x is not {{ }}: {x is not { }}");

sealed class X
{
    public static bool operator ==(X? left, X? right) => true;
    public static bool operator !=(X? left, X? right) => true;
    public override bool Equals(object? obj) => ReferenceEquals(this, obj);
    public override int GetHashCode() => 0;
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/checking-for-null-in-csharp-69958884/?t=50)

Manual argument validation presents challenges for code readability and maintenance.
In modern .NET (6.0 and later), `ArgumentNullException.ThrowIfNull` provides a concise way to validate arguments.
However, in older versions like .NET Framework 4.8, developers often rely on manual `if` checks and the `nameof` operator to throw exceptions.

```csharp
public void AddItem(Item item)
{
#if NET
    // Only available in .NET 6.0 and later.
    ArgumentNullException.ThrowIfNull(item);
#else
    // an old style validation for .NET Framework
    if (item is null)
    {
        throw new ArgumentNullException(nameof(item));
    }
#endif

    _items.Add(item);
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/checking-for-null-in-csharp-69958884/?t=70)

To avoid repeating these manual checks and to improve the developer experience across different framework versions, it is often beneficial to implement a custom `ThrowIfNull` guard method.

---

## 6. Recreating ThrowIfNull Manually

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/recreating-throwifnull-manually-69958885/) · 2:21

### Summary

This lesson demonstrates how to manually implement a ThrowIfNull guard method, replicating the behavior of ArgumentNullException.ThrowIfNull.
It covers the use of the [CallerArgumentExpression] attribute to automatically capture parameter names, the [NotNull] attribute to assist the compiler's nullability analysis, and the "throw helper" pattern to optimize JIT compilation by moving exception-throwing logic into a separate, non-inlined method.

### Key concepts

- [CallerArgumentExpression]: Automatically captures the expression passed to a parameter as a string literal.
- [NotNull]: Informs the compiler that an argument will not be null if the method returns successfully, aiding nullability flow analysis.
- Throw Helpers: A pattern that extracts throw statements into a separate method to keep the primary method small, increasing the likelihood of JIT inlining.
- [DoesNotReturn]: An attribute used to inform the compiler that a method (like a throw helper) will never return normally, which helps resolve nullability warnings.

### Lesson notes

A basic implementation of a null-check guard method involves checking if an argument is null and throwing an ArgumentNullException.
However, in a naive implementation, the ParamName property of the exception remains null unless the caller explicitly passes the name of the variable being checked.

```csharp
try { items.AddItem(null); }
catch (ArgumentNullException ex)
{
    Console.WriteLine($" ParamName: '{ex.ParamName}'");
    Console.WriteLine(ex);
}

public sealed record Item(string Name);

public sealed class Items
{
    private readonly List<Item> _items = new();

    public void AddItem(Item? item)
    {
        Guard.ThrowIfNull(item);
        _items.Add(item);
        return;

        Item GetItem() => null!;
    }
}

static class Guard
{
    public static void ThrowIfNull(object? argument, string? paramName = null)
    {
        if (argument is null)
            throw new ArgumentNullException(paramName);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/recreating-throwifnull-manually-69958885/?t=10)

To automate the capture of the parameter name, C# 11 introduced the [CallerArgumentExpression] attribute.
When applied to a parameter, the compiler automatically injects the string representation of the expression passed to the specified target parameter at the call site.

```csharp
static class Guard
{
    public static void ThrowIfNull(object? argument,
        [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        if (argument is null)
            throw new ArgumentNullException(paramName);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/recreating-throwifnull-manually-69958885/?t=40)

This attribute captures the entire expression, not just simple variable names.
For example, if a method call like GetItem() is passed to the guard, the paramName will capture the literal string "GetItem()".
Decompiled code reveals that the compiler injects this string literal as the second argument.

```csharp
public void AddItem(Item? item)
{
    Guard.ThrowIfNull(GetItem());
    _items.Add(item);
    return;

    Item GetItem() => null!;
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/recreating-throwifnull-manually-69958885/?t=55)

Even with the guard in place, the compiler may still issue nullability warnings if the target collection (e.g., List<Item>) expects non-nullable types while the input is nullable.
To resolve this, the [NotNull] attribute is applied to the argument parameter.
This tells the compiler that if ThrowIfNull returns without throwing, the argument is guaranteed to be non-null on subsequent lines.

```csharp
static class Guard
{
    public static void ThrowIfNull(
        [NotNull] object? argument,
        [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        if (argument is null)
            throw new ArgumentNullException(paramName);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/recreating-throwifnull-manually-69958885/?t=85)

For highly reusable or performance-critical code, a "throw helper" pattern is preferred.
The JIT compiler is more likely to inline the "happy path" of a method if it is small.
By moving the throw instruction into a separate private method, the main ThrowIfNull method becomes a better candidate for inlining optimizations.

```csharp
static class Guard
{
    public static void ThrowIfNull(
        [NotNull] object? argument,
        [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        if (argument is null)
            Throw(paramName);
    }

    private static void Throw(string? paramName)
        => throw new ArgumentNullException(paramName);
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/recreating-throwifnull-manually-69958885/?t=115)

Finally, to ensure the compiler understands the flow of the helper method, it should be marked with the [DoesNotReturn] attribute.
Without this, the compiler might emit a warning suggesting the argument could be null at the exit of the method.
The attribute informs the static analysis that any code path entering the Throw method will terminate with an exception.

```csharp
static class Guard
{
    public static void ThrowIfNull(
        [NotNull] object? argument,
        [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        if (argument is null)
            Throw(paramName);
    }

    [DoesNotReturn]
    private static void Throw(string? paramName)
        => throw new ArgumentNullException(paramName);
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/recreating-throwifnull-manually-69958885/?t=130)

---

## 7. Analyzing ThrowIfNull Boxing Allocations

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-throwifnull-boxing-allocations-69958886/) · 1:19

### Summary

This lesson examines how the JIT compiler handles boxing allocations when value types are passed to a ThrowIfNull method that accepts an object? parameter.
By comparing a standard inlinable guard method with one explicitly marked to prevent inlining, the lesson demonstrates that modern runtimes—including both .NET 10 and .NET Framework 4.8—can optimize away the boxing allocation if the method is inlined.
This highlights the importance of JIT optimization in maintaining performance when using generalized guard clauses for both reference and value types.

### Key concepts

* Boxing behavior when passing value types to object parameters.
* Impact of JIT inlining on memory allocations.
* Using MethodImplOptions.NoInlining to observe runtime behavior.
* Cross-framework benchmarking with .NET 10 and .NET Framework 4.8.
* Memory profiling using BenchmarkDotNet's MemoryDiagnoser.

### Lesson notes

The lesson begins by defining a Guard class with two implementations of a null-check helper.
Both methods take an object? argument, which theoretically should cause boxing when an int (a value type) is passed.
One version is a standard method, while the other is decorated with [MethodImpl(MethodImplOptions.NoInlining)] to force the runtime to perform a method call.

```csharp
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace CustomThrowIfNullBenchmarks;

public static class Guard
{
    public static void ThrowIfNull(
        [NotNull] object? argument,
        [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        if (argument is null)
            Throw(paramName);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowIfNullNoInline(
        [NotNull] object? argument,
        [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        if (argument is null)
            Throw(paramName);
    }

    [DoesNotReturn]
    private static void Throw(string? paramName)
        => throw new ArgumentNullException(paramName);
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-throwifnull-boxing-allocations-69958886/?t=10)

To analyze the performance and allocation behavior, a benchmark is constructed using BenchmarkDotNet.
The benchmark targets multiple frameworks, specifically .NET 10 and .NET Framework 4.8, to observe if the JIT compiler behavior is consistent across different runtime versions.
The MemoryDiagnoser attribute is applied to track allocations.

```csharp
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace CustomThrowIfNullBenchmarks;

[ShortRunJob(RuntimeMoniker.Net10_0)]
[ShortRunJob(RuntimeMoniker.Net48)]
[MemoryDiagnoser]
[HideColumns("Error", "StdDev", "Median", "RatioSD", "Job", "Gen0", "Alloc Ratio")]
public class BoxingBenchmarks
{
    private const int Iterations = 256;

    [Benchmark(Baseline = true)]
    public int Inlinable_Int()
    {
        var total = 0;
        for (int i = 0; i < Iterations; i++)
            total += ProcessInt(42);
        return total;
    }

    [Benchmark]
    public int NoInline_Int()
    {
        var total = 0;
        for (int i = 0; i < Iterations; i++)
            total += ProcessIntNoInline(42);
        return total;
    }

    private static int ProcessInt(int value)
    {
        Guard.ThrowIfNull(value);
        return 42;
    }

    private static int ProcessIntNoInline(int value)
    {
        Guard.ThrowIfNullNoInline(value);
        return 42;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-throwifnull-boxing-allocations-69958886/?t=25)

The project file is configured to support both target frameworks.
When benchmarking multiple frameworks, it is recommended to list the modern .NET version (e.g., net10.0) first in the TargetFrameworks property.

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFrameworks>net10.0;net48</TargetFrameworks>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <LangVersion>latest</LangVersion>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="BenchmarkDotNet" Version="0.15.8" />
    <PackageReference Include="PolySharp" Version="1.*" PrivateAssets="all" />
  </ItemGroup>

</Project>
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-throwifnull-boxing-allocations-69958886/?t=50)

Upon running the benchmarks in the Release configuration, the results show that the Inlinable_Int method results in zero allocations in both .NET 10 and .NET Framework 4.8.
This is because the JIT compiler inlines the ThrowIfNull call and recognizes that the int value can never be null, thus eliminating the need to box the value into an object.
However, when the NoInlining attribute is used, the runtime is forced to perform a method call that accepts an object, necessitating a boxing allocation for the int value in all tested frameworks.

---

## 8. Issues With Nullable Strings in .NET Framework

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/issues-with-nullable-strings-in-dotnet-framework-69958887/) · 0:37

### Summary

This lesson addresses the challenges of using C# nullability features when targeting the .NET Framework.
Because legacy versions of the Base Class Library lack nullability annotations on methods like string.IsNullOrEmpty, the compiler may emit false-positive warnings even after explicit null checks.
The lesson demonstrates how to overcome these limitations by implementing custom validation guards decorated with attributes like [NotNull], ensuring the compiler correctly tracks the nullability state across different Target Framework Monikers (TFMs).

### Key concepts

- Multi-targeting between .NET Framework 4.8 and modern .NET.
- Missing nullability annotations in legacy Base Class Libraries (BCL).
- Using PolySharp to enable modern nullability attributes in older frameworks.
- Compiler warnings (e.g., CS8604) occurring despite valid null checks.
- Custom Guard implementations using [NotNull] and [CallerArgumentExpression].

### Lesson notes

Most modern C# language features, including nullability, can be used when targeting .NET Standard or the full .NET Framework.
To enable these features in a multi-targeted project, the project file must be configured with `<Nullable>enable</Nullable>` and a modern `<LangVersion>`.
To support nullability attributes in older frameworks, the `PolySharp` library is used to provide the necessary polyfills.

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFrameworks>net48;net10.0</TargetFrameworks>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <LangVersion>latest</LangVersion>
    <TreatWarningsAsErrors>false</TreatWarningsAsErrors>
  </PropertyGroup>

  <ItemGroup>
    <!-- Using PolySharp for nullable attributes-->
    <PackageReference Include="PolySharp" Version="1.*" PrivateAssets="all" />
  </ItemGroup>

</Project>
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/issues-with-nullable-strings-in-dotnet-framework-69958887/?t=10)

A significant issue arises when using built-in helper methods like `string.IsNullOrEmpty`.
Because the .NET Framework was released before non-nullable types were added to C#, its Base Class Library (BCL) lacks the necessary nullability annotations.
In modern .NET, `string.IsNullOrEmpty` is decorated with `[NotNullWhen(false)]`, which tells the compiler that if the method returns `false`, the input is not null.
In .NET Framework, this annotation is missing.

As a result, even if you check a nullable string for null or empty, the compiler will still emit a warning (CS8604) if you attempt to pass that string to a non-nullable parameter immediately afterward.

```csharp
public sealed class Items
{
    private readonly Dictionary<string, Item> _items = new();

    public void AddItem(string? name, Item item)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));

        // In .NET Framework, this still emits a warning
        _items.Add(name, item);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/issues-with-nullable-strings-in-dotnet-framework-69958887/?t=10)

To resolve this, you can implement custom validation logic.
By creating a `Guard` class and using the `[NotNull]` attribute, you can explicitly inform the compiler about the nullability state of an argument.
When a parameter is marked with `[NotNull]`, the compiler understands that if the method returns without throwing an exception, the argument is guaranteed to be non-null.

```csharp
public sealed class Items
{
    private readonly Dictionary<string, Item> _items = new();

    public void AddItem(string? name, Item item)
    {
        Guard.ThrowIfNullOrEmpty(name);

        _items.Add(name, item);
    }
}

static class Guard
{
    public static void ThrowIfNullOrEmpty(
        [NotNull] string? argument,
        [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        if (string.IsNullOrEmpty(argument))
            Throw(argument, paramName);
    }

    [DoesNotReturn]
    private static void Throw(string? argument, string? paramName)
    {
        if (argument is null)
            throw new ArgumentNullException(paramName);
        throw new ArgumentException("Value cannot be empty.", paramName);
    }
}
```

This approach ensures that the code remains clean and warning-free across all target frameworks while maintaining robust argument validation.

---

## 9. Implementing Custom String Validation

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/implementing-custom-string-validation-69958888/) · 0:38

### Summary

This lesson demonstrates how to implement a custom `ThrowIfNullOrEmpty` validation method to satisfy the C# compiler's nullability analysis, particularly when working with older target frameworks like .NET Framework 4.8.
By using the `[NotNull]` attribute on a validation parameter, developers can ensure that subsequent code treats the variable as non-nullable, avoiding warnings when performing operations like adding items to a dictionary.

### Key concepts

- **Nullability Flow Analysis**: How the compiler tracks whether a variable can be null based on previous checks or method calls.
- **[NotNull] Attribute**: Informs the compiler that an input argument will not be null if the method returns successfully.
- **[DoesNotReturn] Attribute**: Indicates that a method (typically a throw helper) will never return to its caller, allowing the compiler to understand that code paths following the call are unreachable if the condition is met.
- **[CallerArgumentExpression]**: Automatically captures the name of the expression passed to a parameter, simplifying exception reporting.
- **Framework Compatibility**: Implementing custom guards is essential for maintaining clean nullability analysis in .NET Framework projects where modern `ArgumentException` helpers may be unavailable.

### Lesson notes

When targeting the full .NET Framework (e.g., net48), the compiler often lacks the built-in guard methods found in modern .NET Core.
This can lead to nullability warnings when passing potentially null strings into methods that require non-null values, such as `Dictionary.Add`.

To resolve this, a custom `Guard` class can be implemented.
By applying the `[NotNull]` attribute to the argument in a `ThrowIfNullOrEmpty` method, the compiler's flow analysis is updated.
If the method completes without throwing an exception, the compiler guarantees that the variable is no longer null in the subsequent lines of the calling method.

```csharp
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

public sealed record Item(string Name);

public sealed class Items
{
    private readonly Dictionary<string, Item> _items = new();

    public void AddItem(string? name, Item item)
    {
        Guard.ThrowIfNullOrEmpty(name);

        _items.Add(name, item);
    }
}

static class Guard
{
    public static void ThrowIfNullOrEmpty(
        [NotNull] string? argument,
        [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        if (string.IsNullOrEmpty(argument))
            Throw(argument, paramName);
    }

    [DoesNotReturn]
    private static void Throw(string? argument, string? paramName)
    {
        if (argument is null)
            throw new ArgumentNullException(paramName);
        throw new ArgumentException("Value cannot be empty.", paramName);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/implementing-custom-string-validation-69958888/?t=10)

In the implementation above, the `Throw` helper is marked with the `[DoesNotReturn]` attribute.
This is crucial for the compiler to understand that if `string.IsNullOrEmpty(argument)` is true, the execution flow will stop at the `Throw` call.
Without this, the compiler might still issue warnings because it wouldn't be certain that the code following the guard is only reachable when the argument is valid.

While modern .NET versions provide `ArgumentException.ThrowIfNullOrEmpty`, implementing a custom version is a powerful pattern for maintaining a clean, warning-free codebase in multi-targeted projects or legacy environments.

---

## 10. Summary

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958889/) · 0:54

This lesson demonstrates how to implement custom argument validation helpers, such as ThrowIfNull and ThrowIfNullOrEmpty, that mirror the functionality of the .NET Base Class Library (BCL).
By utilizing attributes like [CallerArgumentExpression] to capture parameter names automatically and [NotNull] to assist compiler nullability analysis, developers can create declarative and maintainable guard clauses.
A key technical insight is that while the BCL version uses the [Intrinsic] attribute to optimize Tier 0 compilation, custom implementations using object parameters still benefit from JIT optimizations that prevent boxing allocations, making them both efficient and maintainable.

### Key concepts

- **Custom Guard Helpers**: Creating reusable static methods for parameter validation to centralize logic and reduce boilerplate.
- **[CallerArgumentExpression]**: Automatically capturing the expression passed as an argument to provide descriptive error messages without hardcoding string names.
- **[NotNull] and [DoesNotReturn]**: Attributes that guide the compiler's static analysis for nullability and code flow to prevent false-positive warnings.
- **JIT Boxing Optimization**: The runtime's ability to avoid boxing when passing arguments to methods accepting object, ensuring high performance even in the full Framework.
- **[Intrinsic] Attribute**: A BCL-specific attribute used to prevent redundant boxing during Tier 0 compilation, which is the primary difference between custom and BCL implementations.

### Lesson notes

Custom validation helpers allow for a more declarative approach to checking parameters.
A standard implementation of a `ThrowIfNull` helper uses the `[NotNull]` attribute on the argument to inform the compiler that the value will not be null if the method returns successfully.
The `[CallerArgumentExpression]` attribute is applied to the `paramName` parameter, allowing the compiler to automatically pass the name of the variable or expression being checked.

```csharp
public static void ThrowIfNull(
    [NotNull] object? argument,
    [CallerArgumentExpression(nameof(argument))] string? paramName = null)
{
    if (argument is null)
        Throw(paramName);
}

[DoesNotReturn]
private static void Throw(string? paramName)
    => throw new ArgumentNullException(paramName);

[Intrinsic] // Tier0 intrinsic to avoid redundant boxing in generics
public static void ThrowIfNull([NotNull] object? argument,
    [CallerArgumentExpression(nameof(argument))] string? paramName = null)
{
    if (argument is null)
    {
        Throw(paramName);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958889/?t=10)

The implementation often separates the actual `throw` statement into a private helper method marked with the `[DoesNotReturn]` attribute.
This separation helps with method inlining and ensures the compiler understands that the execution path ends at that point, which is critical for accurate nullability analysis in the calling code.

A common concern when using `object?` as a parameter type for these helpers is the potential for boxing allocations, especially when passing value types.
However, JIT optimizations in both modern .NET and the full Framework are designed to avoid boxing in these scenarios.

The primary difference between a custom implementation and the version found in the BCL is the `[Intrinsic]` attribute.
This attribute is specific to the BCL and tells the JIT compiler to avoid boxing allocations specifically during Tier 0 compilation.
While this is a tricky optimization only available to the BCL, custom implementations remain highly efficient because the standard JIT optimizations available in the runtime prevent boxing in most production scenarios.

By following this pattern, you can implement your own versions of `ArgumentNullException.ThrowIfNull` or `ThrowIfNullOrEmpty` if the built-in BCL versions are not available for your specific application or target framework.

---

## Running the demo

```bash
cd src/mastering-csharp/08-mastering-argument-validation/MasteringCSharp.ArgumentValidation.Demos
dotnet run -c Release -f net10.0              # all five sections
dotnet run -c Release -f net48                # the same five, on .NET Framework 4.8
dotnet run -c Release -f net10.0 -- nrt       # lesson 3
dotnet run -c Release -f net10.0 -- nulls     # lessons 4 and 5
dotnet run -c Release -f net10.0 -- guard     # lessons 2 and 6
dotnet run -c Release -f net10.0 -- strings   # lessons 8 and 9
dotnet run -c Release -f net10.0 -- boxing    # lessons 7 and 10
```

`-f` is required because the project multi-targets, and both targets are worth running.

```bash
cd src/mastering-csharp/08-mastering-argument-validation/MasteringCSharp.ArgumentValidation.Benchmarks
dotnet run -c Release -f net10.0 -- --filter '*BoxingBenchmarks*'     # the chapter's benchmark, both runtimes
dotnet run -c Release -f net10.0 -- --filter '*BclGuardBenchmarks*'   # custom guard vs the BCL, .NET 10 only
dotnet run -c Release -f net10.0 -- --list flat
```

Launch the benchmark host with `-f net10.0` even though it measures both runtimes.
The `[ShortRunJob(RuntimeMoniker.Net48)]` attribute is what builds and runs the .NET Framework leg, from a host that itself runs on .NET 10.
Benchmarks need Release and take a few minutes.
