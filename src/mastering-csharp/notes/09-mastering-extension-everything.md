# Mastering Extension Everything

> Course: [Mastering: C#](https://dometrain.com/course/mastering-csharp/) · Chapter 9
> 7 lessons · ~9:27
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Overview](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958890/) | 0:38 | [↓](#1-overview) |
| 2 | [Extension Methods](https://dometrain.com/take/course/mastering-csharp-3256129/extension-methods-69958891/) | 1:28 | [↓](#2-extension-methods) |
| 3 | [Refactoring to Extension Everything](https://dometrain.com/take/course/mastering-csharp-3256129/refactoring-to-extension-everything-69958892/) | 1:52 | [↓](#3-refactoring-to-extension-everything) |
| 4 | [The issue with Multi-Targeting](https://dometrain.com/take/course/mastering-csharp-3256129/the-issue-with-multi-targeting-69958893/) | 0:42 | [↓](#4-the-issue-with-multi-targeting) |
| 5 | [Creating Polyfills for Argument Validation](https://dometrain.com/take/course/mastering-csharp-3256129/creating-polyfills-for-argument-validation-69958894/) | 2:24 | [↓](#5-creating-polyfills-for-argument-validation) |
| 6 | [Extending the C# Language](https://dometrain.com/take/course/mastering-csharp-3256129/extending-the-csharp-language-69958895/) | 1:40 | [↓](#6-extending-the-c-language) |
| 7 | [Summary](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958896/) | 0:43 | [↓](#7-summary) |

---

## 1. Overview

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958890/) · 0:38

### Summary

Extension Everything is a C# 14 feature that expands the capabilities of extension methods, allowing developers to add properties, static methods, static properties, and operators to existing types.
While traditional extension methods are limited to instance-level method calls, this new feature enables the projection of a more natural API shape onto types you do not own, such as adding static guard clauses to framework exceptions or implementing pattern-based language features like foreach and await on arbitrary types.

### Key concepts

- **Extension Members**: The ability to extend types with properties, indexers, operators, and static members in addition to instance methods.
- **Extension Syntax**: Using the `extension` keyword to define blocks that target a receiver instance or a static type context.
- **Application-Level Polyfills**: Adding modern static methods (e.g., `ArgumentNullException.ThrowIfNull`) to older versions of the .NET framework.
- **Pattern-Based Extensions**: Satisfying compiler requirements for features like `foreach`, `await`, and deconstruction via extension members.

### Lesson notes

Extension methods are a fundamental part of C# development, particularly within the LINQ ecosystem.
However, traditional extension methods have significant limitations: they can only add instance-level methods.
They cannot be used to add static methods, properties, indexers, or operators to a type.

```csharp
static class StringExtensions
{
    public static int WordCount(this string s) =>
        s.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries).Length;

    // Should be a property — but extension methods can't express that.
    public static bool IsBlank(this string s) =>
        string.IsNullOrWhiteSpace(s);

    public static string Truncate(this string s, int maxLength) =>
        s.Length <= maxLength ? s : s[..maxLength] + "…";
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958890/?t=8)

C# 14's "extension everything" closes this gap by allowing developers to group related extensions into blocks that project a natural API shape onto the target type.
This includes instance properties and static members.

```csharp
static class StringUtilities
{
    private static readonly ConcurrentDictionary<string, string> Cache = new(StringComparer.Ordinal);

    // Adding 'instance' members.
    extension(string @this)
    {
        public bool IsEmpty => string.IsNullOrWhiteSpace(@this);

        public bool ContainsIgnoreCase(string value)
            => @this.Contains(value, StringComparison.OrdinalIgnoreCase);
    }

    // Adding 'static' members.
    extension(string)
    {
        public static int DedupCacheSize => Cache.Count;

        public static string Deduplicate(string value)
            => Cache.GetOrAdd(value, static v => v);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958890/?t=19)

One powerful application of this feature is creating application-level polyfills.
You can add static methods to existing types to match newer framework APIs, such as adding `ThrowIfNull` to `ArgumentNullException` when working in older environments like .NET Standard 2.0.

```csharp
public static class Guard
{
    extension(ArgumentNullException)
    {
        public static void ThrowIfNull(
            [NotNull] object? argument,
            [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        {
            if (argument is null)
                Throw(paramName);

            [DoesNotReturn]
            static void Throw(string? paramName)
                => throw new ArgumentNullException(paramName);
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958890/?t=23)

Finally, extension everything allows you to extend the C# language itself by providing the methods expected by the compiler for specific patterns.
This enables features like `foreach` over a `Range`, deconstruction of a `Uri`, or `await` on a `TimeSpan` by providing extension versions of `GetEnumerator`, `Deconstruct`, and `GetAwaiter` respectively.

```csharp
// 1. foreach: provide GetEnumerator for Range (enables: foreach (var n in 1..5))
static class RangeForeachExtensions
{
    extension(Range range)
    {
        public IEnumerator<int> GetEnumerator()
        {
            var start = range.Start.GetOffset(0);
            var end = range.End.GetOffset(0);
            for (var i = start; i < end; i++)
                yield return i;
        }
    }
}

// 2. Deconstruction: provide Deconstruct for Uri
static class UriDeconstructExtensions
{
    extension(Uri uri)
    {
        public void Deconstruct(out string scheme, out string host, out int port)
        {
            scheme = uri.Scheme;
            host = uri.Host;
            port = uri.Port;
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958890/?t=29)

---

## 2. Extension Methods

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/extension-methods-69958891/) · 1:28

### Summary

Extension methods allow developers to add new functionality to existing types without modifying the original source code or using inheritance.
By defining static methods with the 'this' keyword in a static class, these methods can be invoked using instance method syntax, improving code readability and discoverability.
While traditional extension methods are limited to instance-like methods, upcoming C# features aim to expand these capabilities to properties, operators, and static members.

### Key concepts

- Static utility methods vs. Extension methods
- The `this` parameter modifier for extension methods
- Improved discoverability and readability via IntelliSense
- Limitations: methods only (no properties or operators)
- Limitations: instance-like only (no static extensions)
- Introduction to Extension Everything (C# 14)

### Lesson notes

Extension methods provide a way to "attach" methods to existing types.
For example, if a developer needs a helper method to check for a substring using ordinal ignore-case comparison, they could implement a static utility method.
However, calling a static method with a fully qualified name often feels unnatural compared to instance methods.

```csharp
public static class StringUtilities
{
    public static bool ContainsOrdinalIgnoreCase(string value, string substring) =>
        value.IndexOf(substring, StringComparison.OrdinalIgnoreCase) >= 0;
}

public static class StringExtensions
{
    public static bool ContainsOrdinalIgnoreCase(this string value, string substring) =>
        value.IndexOf(substring, StringComparison.OrdinalIgnoreCase) >= 0;
}

// Usage comparison
var text = "Hello, World!";
// Static utility call
Console.WriteLine(StringUtilities.ContainsOrdinalIgnoreCase(text, "hello"));
// Extension method call
Console.WriteLine(text.ContainsOrdinalIgnoreCase("world"));
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/extension-methods-69958891/?t=10)

Extension methods solve the problem of unnatural syntax by allowing the method to be called directly on the instance.
Under the hood, the compiler treats the call as a static method invocation, so the behavior is identical, but the syntax is more readable and discoverable through IntelliSense.

Despite their utility, traditional extension methods have significant limitations.
They can only be defined as methods; they cannot be properties, indexers, or operators.
Furthermore, they can only mimic instance members, not static members or static properties on the target type.

```csharp
// Desired natural syntax (not possible with traditional extension methods)
var text = "";
// bool empty = text.IsEmpty; // Property access
// string dedup = string.Deduplicate(text); // Static method on string
// int size = string.DedupCacheSize; // Static property on string

// Traditional extension method workarounds
bool empty = text.IsEmpty(); // Forced to use method syntax
string dedup = StringUtilities.Deduplicate(text); // Forced to use utility class
int size = StringUtilities.GetDedupCacheSize(); // Forced to use utility class
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/extension-methods-69958891/?t=55)

To address these limitations, C# 14 introduces "Extension Everything" (extension types).
This feature allows developers to group related extensions and unlock the ability to add properties, indexers, operators, and even static members to existing types, providing a more natural API surface.

---

## 3. Refactoring to Extension Everything

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/refactoring-to-extension-everything-69958892/) · 1:52

This lesson explores the "Extension Everything" feature in C#, which expands the capabilities of extensions beyond traditional instance methods.
By using extension blocks, developers can add properties, static members, and operators to existing types, including those from external libraries.
The lesson demonstrates refactoring traditional static utility classes into extension blocks that project members directly onto the target type, resulting in a more natural and integrated API surface where helper methods and properties appear as native members of the extended type.

### Key concepts

- **Extension blocks**: A new syntax (`extension(T)`) used to group related extension members for a specific type.
- **Instance extension members**: The ability to add both methods and properties to an instance of a type.
- **Static extension members**: The ability to add static methods and properties that are accessible via the type name (e.g., `string.Deduplicate`).
- **Extension operators**: Overloading operators (such as `/` or `+`) for types that are otherwise closed for modification.
- **Compiler Lowering**: How the C# compiler translates these natural-looking calls back into static calls on the underlying utility class.

### Lesson notes

Traditional C# extension methods are limited to instance methods and require the `this` parameter.
Often, utility classes contain a mix of extension methods and standard static helpers, leading to an inconsistent API where some members are called on the instance and others on the utility class itself.

```csharp
using System.Collections.Concurrent;

var text = "Hello, world";

// Classic extension methods read as method calls, not properties.
bool empty = text.IsEmpty();                        // False
bool contains = text.ContainsIgnoreCase("hello");   // True

// "Static-like" helpers force the helper type name onto the call site.
string dedup = StringUtilities.Deduplicate(text);
int size = StringUtilities.DedupCacheSize;

static class StringUtilities
{
    private static readonly ConcurrentDictionary<string, string> Cache = new(StringComparer.Ordinal);

    // Classic instance extension methods.
    public static bool IsEmpty(this string @this) => string.IsNullOrWhiteSpace(@this);

    public static bool ContainsIgnoreCase(this string @this, string value)
        => @this.Contains(value, StringComparison.OrdinalIgnoreCase);

    // No way to hang these off `string` directly — they live on the helper type.
    public static int DedupCacheSize => Cache.Count;

    public static string Deduplicate(string value)
        => Cache.GetOrAdd(value, static v => v);
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/refactoring-to-extension-everything-69958892/?t=10)

The "Extension Everything" feature introduces extension blocks to resolve these limitations.
An extension block is defined using the `extension` keyword followed by the type being extended.
If the block includes a parameter name (e.g., `extension(string @this)`), the members inside are instance members, and the parameter provides access to the current instance.

```csharp
static class StringUtilities
{
    private static readonly ConcurrentDictionary<string, string> Cache = new(StringComparer.Ordinal);

    extension(string @this)
    {
        public bool IsEmpty() => string.IsNullOrWhiteSpace(@this);

        public bool ContainsIgnoreCase(string value)
            => @this.Contains(value, StringComparison.OrdinalIgnoreCase);
    }

    // Standard static methods still work within the class
    public static string Deduplicate(string @this)
        => Cache.GetOrAdd(@this, static v => v);

    public static int GetDedupCacheSize() => Cache.Count;
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/refactoring-to-extension-everything-69958892/?t=25)

Within these blocks, you can define properties as easily as methods.
Furthermore, static members can be added by defining an extension block without an instance parameter (e.g., `extension(string)`).
These static members are then accessible directly through the type name, such as `string.Deduplicate(text)` or `string.DedupCacheSize`.

```csharp
var text = "Hello, world";

// Now using properties and static members directly on the string type
bool empty = text.IsEmpty;                          // False
bool contains = text.ContainsIgnoreCase("hello");   // True

string dedup = string.Deduplicate(text);
int size = string.DedupCacheSize;

static class StringUtilities
{
    private static readonly ConcurrentDictionary<string, string> Cache = new(StringComparer.Ordinal);

    // Adding 'instance' members.
    extension(string @this)
    {
        public bool IsEmpty => string.IsNullOrWhiteSpace(@this);

        public bool ContainsIgnoreCase(string value)
            => @this.Contains(value, StringComparison.OrdinalIgnoreCase);
    }

    // Adding 'static' members.
    extension(string)
    {
        public static int DedupCacheSize => Cache.Count;

        public static string Deduplicate(string value)
            => Cache.GetOrAdd(value, static v => v);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/refactoring-to-extension-everything-69958892/?t=65)

Under the hood, the compiler lowers these calls to standard static method calls.
For example, `string.Deduplicate` is replaced with `StringUtilities.Deduplicate` during compilation.
This allows for a more natural syntax at the call site while maintaining compatibility with the existing .NET type system.

Extension everything also supports operators.
This is particularly useful for types that are outside of your control, such as library-provided structs.
You can project operators like `/`, `+`, `==`, or `++` onto these types to provide a more intuitive syntax for operations like path combination.

```csharp
// Pretend this type ships from a library we don't own —
// we can't add operators directly to it.
public readonly record struct Path(string Value)
{
    public override string ToString() => Value;

    public static implicit operator Path(string value) => new(value);

    public static void CombiningPaths()
    {
        Path p = "c:\\foobar";
        Path p2 = p / "a.txt";
    }
}

// Project a '/' operator onto Path via extension everything.
public static class PathExtensions
{
    extension(Path)
    {
        public static Path operator /(Path left, string right) =>
            new(System.IO.Path.Combine(left.Value, right));

        public static Path operator /(Path left, Path right) =>
            new(System.IO.Path.Combine(left.Value, right.Value));
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/refactoring-to-extension-everything-69958892/?t=85)

---

## 4. The issue with Multi-Targeting

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/the-issue-with-multi-targeting-69958893/) · 0:42

### Summary

Multi-targeting different .NET frameworks often introduces maintenance challenges when newer APIs are unavailable in older targets.
This lesson demonstrates how conditional compilation (using #if directives) leads to fragmented, unreadable code when attempting to use modern features like ArgumentNullException.ThrowIfNull or generic Enum.IsDefined in legacy environments.

### Key concepts

- Multi-targeting across modern .NET and legacy frameworks (e.g., .NET 10.0 and .NET Standard 2.0).
- API surface discrepancies between framework versions.
- The use of conditional compilation (#if directives) to handle cross-framework compatibility.
- Maintenance and readability overhead of framework-specific branching.
- Application-level polyfills as a strategy to unify API surfaces.

### Lesson notes

When a project targets multiple frameworks—such as .NET 10.0 and .NET Standard 2.0—developers often encounter functionality gaps.
Modern .NET versions include convenient APIs that are missing in older versions.
For example, `ArgumentNullException.ThrowIfNull` is available in modern .NET but not in .NET Standard.

To support both targets, developers typically resort to conditional compilation.
This results in code that branches based on the target framework, making the implementation significantly harder to read and maintain.

```csharp
static void Process(List<int> items, Configuration configuration)
{
#if NETSTANDARD
    if (items is null)
        throw new ArgumentNullException(nameof(items));
#else
    ArgumentNullException.ThrowIfNull(items);
#endif

#if NETSTANDARD
    if (!Enum.IsDefined(typeof(Configuration), configuration))
#else
    if (!Enum.IsDefined(configuration))
#endif
    {
        throw new ArgumentException($"Invalid: {configuration}",
            nameof(configuration));
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/the-issue-with-multi-targeting-69958893/?t=25)

In the example above, the logic for basic argument validation is duplicated and obscured by preprocessor directives.
The code must manually check for nulls and use the non-generic `Enum.IsDefined` for the .NET Standard target, while it can use the cleaner, modern APIs for the other target.

This fragmentation makes the code hard to read and hard to maintain.
The solution to this gap is the use of application-level polyfills, which allow developers to bridge these differences and maintain a single, readable codebase across all target frameworks by providing the missing API surface to the older targets.

---

## 5. Creating Polyfills for Argument Validation

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/creating-polyfills-for-argument-validation-69958894/) · 2:24

### Summary

This lesson demonstrates how to use the "Extension Everything" feature to create polyfills that bridge API gaps between different .NET target frameworks.
By extending existing system types like ArgumentNullException and Enum within the System namespace, developers can write clean, modern code that compiles against both .NET Core and older targets like .NET Standard without cluttering the main logic with conditional compilation (#if) directives.

### Key concepts

* Polyfilling missing APIs in older target frameworks.
* Using extension blocks to add static methods to existing classes.
* Namespace shadowing (placing extensions in the System namespace) to match canonical APIs.
* Reducing conditional compilation noise in business logic.
* Targeting ArgumentNullException and Enum for common validation patterns.

### Lesson notes

When targeting multiple frameworks, such as .NET Standard 2.0 and modern .NET Core, developers often encounter APIs available in newer versions that are missing in older ones.
A common example is `ArgumentNullException.ThrowIfNull` and the generic `Enum.IsDefined<TEnum>`.
Without polyfills, the code becomes cluttered with conditional compilation directives to handle these differences.

```csharp
using System;
using System.Collections.Generic;

static void Process(List<int> items, Configuration configuration)
{
#if NETSTANDARD
    if (items is null)
        throw new ArgumentNullException(nameof(items));
#else
    ArgumentNullException.ThrowIfNull(items);
#endif

#if NETSTANDARD
    if (!Enum.IsDefined(typeof(Configuration), configuration))
#else
    if (!Enum.IsDefined(configuration))
#endif
    {
        throw new ArgumentException($"Invalid: {configuration}",
            nameof(configuration));
    }
}

enum Configuration { Debug, Release }
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/creating-polyfills-for-argument-validation-69958894/?t=10)

To address this, a custom guard implementation can be created.
Initially, this might be a standard static class within the application's namespace.

```csharp
#if NETSTANDARD2_0

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace MyApplication;

public static class Guard
{
    public static void ThrowIfNull(
        [NotNull] object? argument,
        [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        if (argument is null)
            Throw(paramName);
        return;

        [DoesNotReturn]
        static void Throw(string? paramName)
            => throw new ArgumentNullException(paramName);
    }
}

#endif
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/creating-polyfills-for-argument-validation-69958894/?t=25)

However, using the "Extension Everything" syntax, we can improve this by extending the `ArgumentNullException` class directly.
By changing the namespace to `System`, the extension method becomes available as if it were a native part of the class, allowing the removal of custom `using` statements and providing a seamless experience across frameworks.

```csharp
#if NETSTANDARD2_0

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System;

public static class Guard
{
    extension(ArgumentNullException)
    {
        public static void ThrowIfNull(
            [NotNull] object? argument,
            [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        {
            if (argument is null)
                Throw(paramName);

            [DoesNotReturn]
            static void Throw(string? paramName)
                => throw new ArgumentNullException(paramName);
        }
    }
}

#endif
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/creating-polyfills-for-argument-validation-69958894/?t=40)

Once the polyfill is defined, the `Process` method can be simplified.
The compiler will emit different code depending on the target: it will use the extension member for the .NET Standard target and the native method for modern .NET targets.

```csharp
using System;
using System.Collections.Generic;

static void Process(List<int> items, Configuration configuration)
{
    ArgumentNullException.ThrowIfNull(items);

#if NETSTANDARD
    if (!Enum.IsDefined(typeof(Configuration), configuration))
#else
    if (!Enum.IsDefined(configuration))
#endif
    {
        throw new ArgumentException($"Invalid: {configuration}",
            nameof(configuration));
    }
}

enum Configuration { Debug, Release }
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/creating-polyfills-for-argument-validation-69958894/?t=55)

This same pattern can be applied to `System.Enum`.
In older frameworks, `Enum.IsDefined` requires the `Type` to be passed explicitly and boxes the argument.
We can create a generic `IsDefined<TEnum>` extension method in the `System` namespace to provide a modern API for older frameworks.

```csharp
#if !NET7_0_OR_GREATER

namespace System
{
    public static class EnumPolyfill
    {
        extension(Enum)
        {
            public static bool IsDefined<TEnum>(TEnum value) where TEnum : struct, Enum
                => Enum.IsDefined(typeof(TEnum), value);
        }
    }
}

#endif
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/creating-polyfills-for-argument-validation-69958894/?t=100)

By isolating conditional compilation logic into polyfills, the primary codebase is significantly cleaner.
Instead of spreading `#if` directives throughout the logic, they are contained within isolated polyfill files, making the business logic easier to maintain.

```csharp
static void Process(List<int> items, Configuration configuration)
{
#if NETSTANDARD
    if (items is null)
        throw new ArgumentNullException(nameof(items));
#else
    ArgumentNullException.ThrowIfNull(items);
#endif

#if NETSTANDARD
    if (!Enum.IsDefined(typeof(Configuration), configuration))
#else
    if (!Enum.IsDefined(configuration))
#endif
    {
        throw new ArgumentException($"Invalid: {configuration}",
            nameof(configuration));
    }
}

static void ProcessClean(List<int> items, Configuration configuration)
{
    ArgumentNullException.ThrowIfNull(items);
    if (!Enum.IsDefined(configuration))
    {
        throw new ArgumentException($"Invalid: {configuration}", 
            nameof(configuration));
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/creating-polyfills-for-argument-validation-69958894/?t=130)

---

## 6. Extending the C# Language

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/extending-the-csharp-language-69958895/) · 1:40

### Summary

C# allows developers to extend the language's built-in syntax patterns—such as foreach, deconstruction, await, and collection expressions—by providing specific extension members.
By implementing methods like GetEnumerator, Deconstruct, GetAwaiter, or Add as extension members, you can enable these language features on types you do not own or that do not natively support them.
While powerful for creating Domain Specific Languages (DSLs), these patterns should be used judiciously due to potential maintenance costs.

### Key concepts

- Extending foreach via GetEnumerator on Range.
- Enabling deconstruction on existing types (e.g., Uri) via extension Deconstruct.
- Creating custom await patterns by extending types with GetAwaiter or conversion methods.
- Supporting collection expressions ([...]) and initializers on custom types via extension Add methods.
- The compiler's reliance on method names rather than specific interfaces for certain syntax patterns.

### Lesson notes

C# syntax patterns often rely on the presence of specific method names rather than strict interface implementation.
By using extension members, you can "teach" the compiler how to handle types in contexts like loops, deconstruction, and asynchronous waiting.

#### Extending Foreach with Range

The foreach loop requires a GetEnumerator method.
By extending the Range struct with this method, you can iterate directly over a range like 1..5.

```csharp
using System.Collections;
using System.Runtime.CompilerServices;

foreach (var n in 1..5)
    Console.Write($"{n} ");
Console.WriteLine();

static class RangeForeachExtensions
{
    extension(Range range)
    {
        public IEnumerator<int> GetEnumerator()
        {
            var start = range.Start.GetOffset(0);
            var end = range.End.GetOffset(0);
            for (var i = start; i < end; i++)
                yield return i;
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/extending-the-csharp-language-69958895/?t=25)

#### Deconstruction Extensions

Deconstruction allows you to break an object into multiple variables.
The compiler expects a Deconstruct method to be available.
This can be provided as an extension method for types like System.Uri to extract components like the scheme, host, and port.

```csharp
var uri = new Uri("https://example.com:8080/api/data");
var (scheme, host, port) = uri;
Console.WriteLine($"Scheme: {scheme}, Host: {host}, Port: {port}");

static class UriDeconstructExtensions
{
    extension(Uri uri)
    {
        public void Deconstruct(out string scheme, out string host, out int port)
        {
            scheme = uri.Scheme;
            host = uri.Host;
            port = uri.Port;
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/extending-the-csharp-language-69958895/?t=40)

#### Custom Awaitable Patterns

You can create a Domain Specific Language (DSL) for asynchronous operations by extending basic types.
For example, you can extend int to return a TimeSpan and then extend TimeSpan to return a Task, enabling a clean await syntax.

```csharp
await 1.Seconds.AsDelay();
Console.WriteLine("await 1.Seconds.AsDelay() — waited 1 second");

static class TimeExtensions
{
    extension(int value)
    {
        public TimeSpan Seconds => TimeSpan.FromSeconds(value);
    }

    extension(TimeSpan delay)
    {
        public Task AsDelay() => Task.Delay(delay);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/extending-the-csharp-language-69958895/?t=70)

#### Collection Expressions and Initializers

Collection expressions ([...]) and collection initializers require an Add method.
If a custom collection uses a different method name (like AddItem) and cannot be modified, an extension method named Add can satisfy the compiler's requirements.

```csharp
StringBag bag = ["1", "2"];

class StringBag : IEnumerable<string>
{
    private readonly List<string> _items = [];
    public IReadOnlyList<string> Items => _items;

    public void AddItem(string item) => _items.Add(item);

    public IEnumerator<string> GetEnumerator() => _items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

static class StringBagAddExtensions
{
    extension(StringBag bag)
    {
        public void Add(string item) => bag.AddItem(item);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/extending-the-csharp-language-69958895/?t=85)

---

## 7. Summary

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958896/) · 0:43

### Summary

Extension Everything in C# expands the extension member model to include instance and static properties, indexers, and operators, alongside traditional extension methods.
This feature acts as syntactic sugar, allowing developers to project a more natural API onto existing types without modifying their original definition.
While it cannot introduce state through fields or define constructors, it provides a powerful mechanism for implementing polyfills and domain-specific languages while maintaining strict member resolution rules where native members take precedence.

### Key concepts

* Extension of both instance and static members.
* Support for properties, indexers, and operators in addition to methods.
* Implementation via syntactic sugar; the underlying types are not modified.
* Restrictions: No support for fields, constructors, or finalizers.
* Member resolution: Native members always take precedence over extension members.
* Use cases: Creating custom polyfills and domain-specific languages (DSLs).

### Lesson notes

The "Extension Everything" feature enhances code readability and discoverability by allowing developers to project a wider range of members onto existing types.
This goes beyond traditional extension methods to include instance and static properties, indexers, and operators.

```csharp
static class MyExtensions
{
    // Adding 'instance' members
    // will use 's' instead of 'this'
    extension(string s)
    {
        // Could add "instance" methods and properties.
        // But not constructors
    }

    // Adding 'static' members
    extension(string)
    {
        // Could add "static" methods, properties
        // and operators
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958896/?t=10)

It is important to recognize that these extensions are not physically added to the target types.
Instead, the compiler provides syntactic sugar that allows these members to be invoked as if they were native to the type.
This maintains the integrity of the original type while providing a more fluent API for the consumer.

There are several architectural boundaries to this feature.
Developers cannot add state to a type via fields, nor can they define new constructors or finalizers.
Additionally, existing fields cannot be removed.
From a resolution perspective, if a naming collision occurs between a native member and an extension member, the compiler will always prioritize the native member.

Despite these limitations, the ability to extend types with properties and operators is highly valuable.
It allows for the creation of polyfills that bring modern functionality to older frameworks and enables the development of custom domain-specific languages that feel native to the C# environment.
