# Reference Types and Value Types

> Course: [Deep Dive: C#](https://dometrain.com/course/deep-dive-csharp/) · Chapter 2
> 5 lessons · ~55:38
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Primer on Classes vs Value Types](https://dometrain.com/take/course/deep-dive-csharp-2732260/primer-on-classes-vs-value-types-54135422/) | 7:02 | [↓](#1-primer-on-classes-vs-value-types) |
| 2 | [Enums](https://dometrain.com/take/course/deep-dive-csharp-2732260/enums-54135423/) | 15:02 | [↓](#2-enums) |
| 3 | [Structs](https://dometrain.com/take/course/deep-dive-csharp-2732260/structs-54135424/) | 7:43 | [↓](#3-structs) |
| 4 | [The Problem with Equality](https://dometrain.com/take/course/deep-dive-csharp-2732260/the-problem-with-equality-54135425/) | 11:52 | [↓](#4-the-problem-with-equality) |
| 5 | [Records](https://dometrain.com/take/course/deep-dive-csharp-2732260/records-54135426/) | 13:59 | [↓](#5-records) |

---

## 1. Primer on Classes vs Value Types

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/primer-on-classes-vs-value-types-54135422/) · 7:02

### Summary

This lesson provides a foundational comparison between reference types and value types in C#.
It explores how classes (reference types) share a single memory reference, allowing modifications to persist across method boundaries, whereas value types (like primitives) are copied, meaning changes within a method do not affect the original variable.
The lesson also demonstrates the ref keyword as a mechanism to pass value types by reference when modification of the original value is required.

### Key concepts

- Reference types (e.g., classes, lists) use shared memory references.
- Value types (e.g., int, double, bool) are copied when passed to methods.
- Modifying a reference type's internal state persists outside the method scope.
- Reassigning a value type parameter does not affect the caller's original variable.
- The ref keyword enables passing value types by reference to allow external modification.

### Lesson notes

In C#, classes are reference types, while primitive types such as integers, floating-point numbers, and Booleans are value types.
The fundamental difference lies in how they are handled in memory: reference types use a shared reference, whereas value types are copied.

When a reference type like a `List<string>` is passed to a method, the code operates on the same memory reference.
Consequently, any modifications to the object—such as adding elements—are reflected in the original instance.

```csharp
// classes are reference types in C#
// the primitive types (like integers, doubles, and booleans) are value types

// recall that when we use a reference type, we are passing
// around a reference to the object in memory

List<string> ourList = new()
{
    "Hello",
    "World",
};

void DoSomethingWithReference(List<string> list)
{
    list.Add("From");
    list.Add("Nick");
}

Console.WriteLine("Reference Before:");
foreach (var item in ourList)
{
    Console.WriteLine(item);
}

DoSomethingWithReference(ourList);

Console.WriteLine("Reference After:");
foreach (var item in ourList)
{
    Console.WriteLine(item);
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/primer-on-classes-vs-value-types-54135422/?t=70)

In contrast, value types are passed by value, meaning a copy is created for use within the method.
If a value type is reassigned inside a method, the original variable outside the method remains unchanged.
While `string` is technically a reference type, its immutability often leads to behavior similar to value types when reassigning parameters.

```csharp
string ourString = "Hello, World!";
void DoSomethingWithValue(string value)
{
    value = "Goodbye, World!";
}

Console.WriteLine("Value Before:");
Console.WriteLine(ourString);

DoSomethingWithValue(ourString);

Console.WriteLine("Value After:");
Console.WriteLine(ourString);
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/primer-on-classes-vs-value-types-54135422/?t=235)

To modify a value type directly within a method, C# provides the `ref` keyword.
This allows a value to be passed by reference rather than by value.
When using `ref`, the method must explicitly declare the parameter with the keyword, and the caller must also use `ref` when passing the argument.

```csharp
// we can pass a value type by reference using the ref keyword
void DoSomethingWithValueByRef(ref string value)
{
    value = "Goodbye, World!";
}

Console.WriteLine("Value Before By Ref:");
Console.WriteLine(ourString);

DoSomethingWithValueByRef(ref ourString);

Console.WriteLine("Value After By Ref:");
Console.WriteLine(ourString);
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/primer-on-classes-vs-value-types-54135422/?t=325)

---

## 2. Enums

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/enums-54135423/) · 15:02

### Summary

Enums in C# are value types that provide a way to work with a fixed set of named constants.
While they appear as strings in code and console output, they are fundamentally numeric types, typically backed by integers.
This lesson covers enum definition, explicit value assignment, conversion between strings and numeric types using methods like ToString and Enum.Parse, and the implementation of bitwise flags using the [Flags] attribute and powers of two.

### Key concepts

* **Value Type**: Enums are value types that store numeric data.
* **Implicit vs. Explicit Values**: By default, enum members start at 0 and increment by 1, but they can be manually assigned specific values.
* **Casting**: Enums can be explicitly cast to and from their underlying numeric type, but not directly to strings.
* **Parsing**: Converting strings to enums requires Enum.Parse or Enum.TryParse.
* **Metadata**: The Enum class provides static methods like GetValues and GetNames to reflect on the type.
* **Flags Attribute**: Using [Flags] allows an enum to represent a bitmask, enabling the combination of multiple values using bitwise operators.

### Lesson notes

Enums are defined using the enum keyword.
By default, the first member of an enum has the value 0, and each subsequent member increases by 1.
However, you can also explicitly assign numeric values to each member.

```csharp
enum DaysOfWeek
{
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday,
    Sunday
}

enum DaysOfWeek2
{
    Monday      = 1,
    Tuesday     = 2,
    Wednesday   = 3,
    Thursday    = 4,
    Friday      = 5,
    Saturday    = 6,
    Sunday      = 7
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/enums-54135423/?t=25)

It is a recommended practice to reserve enums for sets of values that are fixed or change very infrequently, such as days of the week, to ensure long-term code maintainability.

#### Numeric Nature and Casting

Although enums look like strings in the editor, they are numeric.
You can cast an enum value to its underlying integer type, but attempting to cast an enum directly to a string will result in a compilation error.

```csharp
// We can cast an enum to an int:
int monday = (int)DaysOfWeek.Monday;

// We cannot cast an enum to a string:
// string mondayString = (string)DaysOfWeek.Monday; // This will not compile
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/enums-54135423/?t=145)

When an enum is passed to Console.WriteLine or used in string interpolation, C# automatically calls the .ToString() method, which returns the name of the enum member rather than its numeric value.

```csharp
Console.WriteLine($"Enum Value Directly: {DaysOfWeek.Monday}");
Console.WriteLine($"Enum Value Casted: {(int)DaysOfWeek.Monday}");
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/enums-54135423/?t=160)

To explicitly get the string representation of an enum value, use the ToString() method.

```csharp
string mondayString = DaysOfWeek.Monday.ToString();
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/enums-54135423/?t=205)

#### Parsing Strings to Enums

To convert a string back into an enum value, use Enum.Parse or the generic Enum.Parse<T>.

```csharp
DaysOfWeek mondayEnum = (DaysOfWeek)Enum.Parse(typeof(DaysOfWeek), "Monday");
DaysOfWeek mondayEnum2 = Enum.Parse<DaysOfWeek>("Monday");
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/enums-54135423/?t=220)

For safer conversion, Enum.TryParse is available.
It returns a boolean indicating success and uses an out parameter for the result.
If parsing fails, the out variable is assigned the default value of the enum (usually 0).

```csharp
DaysOfWeek mondayEnum3;
bool parseSucceeded = Enum.TryParse("Hello World!", out mondayEnum3);
Console.WriteLine($"Enum {(parseSucceeded ? "Was Parsed" : "Was Not Parsed")}: {mondayEnum3}");
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/enums-54135423/?t=325)

In the example above, if "Hello World!" fails to parse, mondayEnum3 becomes 0.
If the enum defines a member with the value 0 (like Monday in DaysOfWeek), the output will display that member's name.
If the enum does not define a member with the value 0 (as in DaysOfWeek2), the output will display the numeric value 0 instead.

#### Iteration and Metadata

The Enum class provides methods to retrieve all defined values or names within an enum type.

```csharp
Console.WriteLine("All Enum Values:");
foreach (DaysOfWeek day in Enum.GetValues(typeof(DaysOfWeek)))
{
    Console.WriteLine($"Enum Value: {(int)day}");
}

Console.WriteLine("All Enum Names:");
foreach (string day in Enum.GetNames(typeof(DaysOfWeek)))
{
    Console.WriteLine($"Enum Name: {day}");
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/enums-54135423/?t=505)

C# also allows casting any integer to an enum type, even if that integer does not correspond to a defined named constant.

```csharp
DaysOfWeek invalidDay = (DaysOfWeek)8;
Console.WriteLine($"Invalid Enum Value: {invalidDay}");
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/enums-54135423/?t=535)

#### Flag Enums

By applying the [Flags] attribute, an enum can represent a set of flags that can be combined.
For this to work correctly, the enum members must be assigned values that are powers of two (1, 2, 4, 8, etc.), representing individual bits in the underlying numeric value.

```csharp
[Flags]
enum Permissions
{
    None     = 0,    // 0000 0000
    Read     = 1,    // 0000 0001
    Write    = 2,    // 0000 0010
    Execute  = 4     // 0000 0100
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/enums-54135423/?t=625)

You can combine these flags using the bitwise OR operator (|) and check for the presence of a specific flag using the bitwise AND operator (&).

```csharp
Permissions readWrite = Permissions.Read | Permissions.Write;
Console.WriteLine($"RW: {readWrite}");

bool canRead = (readWrite & Permissions.Read) == Permissions.Read;
bool canWrite = (readWrite & Permissions.Write) == Permissions.Write;
bool canExecute = (readWrite & Permissions.Execute) == Permissions.Execute;
Console.WriteLine($"Can Read: {canRead}");
Console.WriteLine($"Can Write: {canWrite}");
Console.WriteLine($"Can Execute: {canExecute}");
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/enums-54135423/?t=760)

When the [Flags] attribute is present, .ToString() automatically produces a comma-separated list of all named constants that correspond to the set bits.

---

## 3. Structs

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/structs-54135424/) · 7:43

### Summary

Structs are value types in C# that share syntactical similarities with classes but operate with value semantics.
They are allocated on the stack, passed by value through copying, and do not incur the garbage collection overhead associated with heap-allocated reference types.
Structs are ideal for representing small, simple data structures like geometric points or colors, and they maintain a default parameterless constructor even when custom constructors are defined.

### Key concepts

* Structs as value types.
* Stack allocation vs. Heap allocation.
* Pass-by-value semantics (data copying).
* Persistence of the default parameterless constructor.
* Performance benefits in high-frequency allocation scenarios.
* Guidelines for choosing between structs and classes.

### Lesson notes

Structs are value types defined using the `struct` keyword.
While they look similar to classes, they have distinct memory and behavioral characteristics.
A struct can contain fields, properties, and constructors.

```csharp
// a struct is a value type, even though it looks like a class!

// here is an example of a struct
public struct Point
{
    public int X;
    public int Y;
}

// here is the same struct but with properties:
public struct PointWithProperties
{
    public int X { get; set; }
    public int Y { get; set; }
}

// here is the same struct but with a constructor:
public struct PointWithConstructor
{
    public PointWithConstructor(int x, int y)
    {
        X = x;
        Y = y;
    }
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/structs-54135424/?t=25)

One significant difference between structs and classes involves constructors.
In a class, defining a parameterized constructor hides the default parameterless constructor.
In a struct, the public parameterless constructor remains available even after a custom constructor is defined.
Adding a constructor with parameters does not hide the default one.

Structs can also contain methods, allowing them to encapsulate behavior alongside data.

```csharp
public int Y { get; set; }
}

// here is the same struct but with a constructor:
public struct PointWithConstructor
{
    public PointWithConstructor(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; set; }

    public int Y { get; set; }
}

// we can also have a struct with a method, just like with classes:
public struct PointWithMethod
{
    public int X;
    public int Y;

    public void Move(int x, int y)
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/structs-54135424/?t=70)

#### Memory and Value Semantics

The primary difference between a struct and a class is that a struct is a value type, while a class is a reference type.
This has two major implications:

1.  **Memory Allocation**: Structs are stored on the stack, whereas classes are stored on the heap. Objects on the heap must be managed by the garbage collector, while stack-allocated structs are removed when they go out of scope. Using structs can reduce garbage collection pressure in performance-sensitive applications.
2.  **Copying Behavior**: When a reference type is passed to a method, a reference to the original object is passed. When a struct is passed, a copy of the entire value is created. Modifications made to the struct inside the method do not affect the original instance.

```csharp
// a struct is a value type, even though it looks like a class!

// so what is the difference between a struct and a class?
// why would we make a struct instead of a class?
// the main difference is that a struct is a value type, and a class is a reference type
// - A struct is stored on the stack, and a class is stored on the heap
// - A struct is copied when it is passed to a method, and a class is passed by reference

// here is an example of a struct being copied when passed to a method:
void DoSomethingWithPoint(Point p)
{
    p.X = 111;
    p.Y = 222;
}

var ourPoint = new Point()
{
    X = 123,
    Y = 456
};
Console.WriteLine(
    $"ourPoint before DoSomethingWithPoint: " +
    $"{ourPoint.X}, {ourPoint.Y}");
DoSomethingWithPoint(ourPoint);
Console.WriteLine(
    $"ourPoint after DoSomethingWithPoint: " +
    $"{ourPoint.X}, {ourPoint.Y}");

// because structs can look like classes, it can be
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/structs-54135424/?t=130)

In the example above, the values of `ourPoint` remain 123 and 456 after the method call because the method operated on a copy of the struct.

#### Usage Guidelines

Structs should be used for small, simple, primitive-like objects.
Common examples include geometric shapes (rectangles, points), colors, or vectors.
These are often used in high-performance areas like game development to optimize memory usage and reduce garbage collection overhead.

```csharp
var ourPoint = new Point()
{
    X = 123,
    Y = 456
};
Console.WriteLine(
    $"ourPoint before DoSomethingWithPoint: " +
    $"{ourPoint.X}, {ourPoint.Y}");
DoSomethingWithPoint(ourPoint);
Console.WriteLine(
    $"ourPoint after DoSomethingWithPoint: " +
    $"{ourPoint.X}, {ourPoint.Y}");

// because structs can look like classes, it can be
// confusing when to use a struct and when to use a class
// here are some guidelines:
// - use a struct when you have a small, simple object
//   that you want to pass by value
// - use a struct when you want to avoid the overhead
//   of heap allocation, garbage collecting, etc...
// I try to think about very primitive things like a
// Point, or a Color, or a Rectangle, or other geometric things

// here is an example of a struct
public struct Point
{
    public int X;
    public int Y;
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/structs-54135424/?t=295)

If there is no specific performance or semantic reason to use a struct, the default choice should be a class.
Mixing structs and classes without a clear strategy can make code difficult to navigate because developers must constantly track whether a type is being passed by value or by reference.

---

## 4. The Problem with Equality

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/the-problem-with-equality-54135425/) · 11:52

### Summary

C# handles equality differently for reference types and value types, which can lead to unexpected behavior when comparing objects with identical data.
While classes default to reference equality (comparing memory addresses), structs default to value equality (comparing property values).
Developers often attempt to force reference types to behave like value types by overriding equality methods and operators, but this process is complex, requires multiple boilerplate implementations, and is highly error-prone, especially as object complexity increases.

### Key concepts

- **Reference Equality**: The default for classes, where two variables are equal only if they point to the same memory address.
- **Value Equality**: The default for structs (via the `Equals` method), where two variables are equal if their underlying data matches.
- **Equality Operator (`==`) vs. `Equals()`**: In classes, `==` defaults to reference comparison. In structs, the `==` operator is not available by default and must be explicitly implemented.
- **Boilerplate Requirements**: Overriding equality in a class requires overriding `Equals(object)`, `GetHashCode()`, and overloading the `==` and `!=` operators.
- **Hashing**: `GetHashCode` must be overridden whenever `Equals` is overridden to ensure consistency in collections like `Dictionary` or `HashSet`.

### Lesson notes

In C#, reference types and value types exhibit distinct behaviors during equality checks.
Even when two instances contain identical property values, their equality status depends entirely on their underlying type definition.

```csharp
// one of the big challenges with value and reference
// types has to do with checking for equality

public class MyClass
{
    public int NumericValue { get; set; }

    public string StringValue { get; set; }
}

public struct MyStruct
{
    public int NumericValue { get; set; }

    public string StringValue { get; set; }
}

// let's look at class equality ...

public class MyClassWithEquality ...

// how does this one shape up?? ...

public class MyClassWithEqualityAndOperator ...

// does this fix our issue? ...
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/the-problem-with-equality-54135425/?t=40)

#### Default Class Equality

By default, classes use reference-based equality.
When two distinct instances are created with the same values, they occupy different locations in memory.
Consequently, equality checks using the `==` operator, the `Equals()` method, or the static `object.Equals()` method will all return `false`.

```csharp
// one of the big challenges with value and reference
// types has to do with checking for equality

// let's look at class equality
var myClass1 = new MyClass { NumericValue = 123, StringValue = "ABC" };
var myClass2 = new MyClass { NumericValue = 123, StringValue = "ABC" };
Console.WriteLine("myClass1 equal to myClass2:");
Console.WriteLine(myClass1 == myClass2); // False
Console.WriteLine(myClass1.Equals(myClass2)); // False
Console.WriteLine(object.Equals(myClass1, myClass2)); // False

// let's look at struct equality ...

public class MyClassWithEquality ...

// how does this one shape up?? ...

public class MyClassWithEqualityAndOperator ...

// does this fix our issue? ...

public class MyClass ...

public struct MyStruct ...
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/the-problem-with-equality-54135425/?t=100)

#### Default Struct Equality

Structs utilize value-based equality.
When comparing two struct instances with identical data, the `Equals()` method returns `true`.
However, structs do not support the `==` operator out of the box; attempting to use it will result in a compilation error unless the operator is explicitly overloaded.

```csharp
// one of the big challenges with value and reference
// types has to do with checking for equality

// let's look at class equality
var myClass1 = new MyClass { NumericValue = 123, StringValue = "ABC" };
var myClass2 = new MyClass { NumericValue = 123, StringValue = "ABC" };
Console.WriteLine("myClass1 equal to myClass2:");
Console.WriteLine(myClass1 == myClass2); // False
Console.WriteLine(myClass1.Equals(myClass2)); // False
Console.WriteLine(object.Equals(myClass1, myClass2)); // False

// let's look at struct equality
var myStruct1 = new MyStruct { NumericValue = 123, StringValue = "ABC" };
var myStruct2 = new MyStruct { NumericValue = 123, StringValue = "ABC" };
Console.WriteLine("myStruct1 equal to myStruct2:");
//Console.WriteLine(myStruct1 == myStruct2); // does not compile
Console.WriteLine(myStruct1.Equals(myStruct2)); // True
Console.WriteLine(object.Equals(myStruct1, myStruct2)); // True

// people then try to fix the class equality by overriding the Equals method
public class MyClassWithEquality ...

// how does this one shape up?? ...

public class MyClassWithEqualityAndOperator ...

// does this fix our issue? ...

public class MyClass ...

public struct MyStruct ...
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/the-problem-with-equality-54135425/?t=175)

#### Overriding Equality in Classes

To force a class to behave like a value type, you must override the `Equals(object)` method.
This implementation typically involves checking for null, verifying the type, casting the object, and then comparing each property.
Additionally, `GetHashCode()` must be overridden to provide a unique hash based on the object's values, which is essential for performance in hash-based collections.

```csharp
// one of the big challenges with value and reference ...
public class MyClassWithEquality
{
    public int NumericValue { get; set; }

    public string StringValue { get; set; }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var other = (MyClassWithEquality)obj;
        return NumericValue == other.NumericValue && StringValue == other.StringValue;
    }

    public override int GetHashCode()
    {
        return NumericValue.GetHashCode() ^ StringValue.GetHashCode();
    }
}

// how does this one shape up?? ...
public class MyClassWithEqualityAndOperator...

// does this fix our issue? ...
public class MyClass...
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/the-problem-with-equality-54135425/?t=255)

Even with `Equals` overridden, the `==` operator still performs a reference check.
This creates an inconsistent API where `Equals()` returns `true` but `==` returns `false` for the same two instances.

```csharp
var myClassWithEquality1 = new MyClassWithEquality { NumericValue = 123, StringValue = "ABC" };
var myClassWithEquality2 = new MyClassWithEquality { NumericValue = 123, StringValue = "ABC" };
Console.WriteLine("myClassWithEquality1 equal to myClassWithEquality2:");
Console.WriteLine(myClassWithEquality1 == myClassWithEquality2); // False
Console.WriteLine(myClassWithEquality1.Equals(myClassWithEquality2)); // True
Console.WriteLine(object.Equals(myClassWithEquality1, myClassWithEquality2)); // True

// the problem is that the == operator is not overridden
// and the default implementation is used. let's fix it.
public class MyClassWithEqualityAndOperator...

// does this fix our issue? ...

public class MyClass...

public struct MyStruct...

// people then try to fix the class equality by overriding the Equals method
public class MyClassWithEquality...
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/the-problem-with-equality-54135425/?t=385)

#### Operator Overloading

To achieve full value-based equality, you must overload the `==` and `!=` operators.
These are static methods that take two instances (left and right) and typically delegate the logic to the overridden `Equals` method.

```csharp
public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var other = (MyClassWithEqualityAndOperator)obj;
        return NumericValue == other.NumericValue && StringValue == other.StringValue;
    }

    public override int GetHashCode()
    {
        return NumericValue.GetHashCode() ^ StringValue.GetHashCode();
    }

    public static bool operator ==(MyClassWithEqualityAndOperator left, MyClassWithEqualityAndOperator right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(MyClassWithEqualityAndOperator left, MyClassWithEqualityAndOperator right)
    {
        return !left.Equals(right);
    }
}

// does this fix our issue? ...

public class MyClass...
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/the-problem-with-equality-54135425/?t=475)

Once all four elements—`Equals`, `GetHashCode`, `==`, and `!=`—are implemented, the class finally behaves consistently like a value type.
However, this approach is highly manual and becomes increasingly difficult to maintain as classes grow in complexity or include nested reference types and collections.

```csharp
// one of the big challenges with value and reference ...

var myClassWithEquality1 = new MyClassWithEquality { NumericValue = 123, StringValue = "ABC" };
var myClassWithEquality2 = new MyClassWithEquality { NumericValue = 123, StringValue = "ABC" };
Console.WriteLine("myClassWithEquality1 equal to myClassWithEquality2:");
Console.WriteLine(myClassWithEquality1 == myClassWithEquality2); // False
Console.WriteLine(myClassWithEquality1.Equals(myClassWithEquality2)); // True
Console.WriteLine(object.Equals(myClassWithEquality1, myClassWithEquality2)); // True

// does this fix our issue?
var myClassWithEqualityAndOperator1 = new MyClassWithEqualityAndOperator { NumericValue = 123, StringValue = "ABC" };
var myClassWithEqualityAndOperator2 = new MyClassWithEqualityAndOperator { NumericValue = 123, StringValue = "ABC" };
Console.WriteLine("myClassWithEqualityAndOperator1 equal to myClassWithEqualityAndOperator2:");
Console.WriteLine(myClassWithEqualityAndOperator1 == myClassWithEqualityAndOperator2); // True
Console.WriteLine(myClassWithEqualityAndOperator1.Equals(myClassWithEqualityAndOperator2)); // True
Console.WriteLine(object.Equals(myClassWithEqualityAndOperator1, myClassWithEqualityAndOperator2)); // True

// we can see that this gets really complicated really quickly
// and this just comes back to the fundamental difference between
// value and reference types.
// trying to change the behavior of a reference type for equality
// is very error prone - and we only looked at a class that
// had two basic value types!

public class MyClass...

public struct MyStruct...

// people then try to fix the class equality by overriding the Equals method
public class MyClassWithEquality...

// the problem is that the == operator is not overridden
// and the default implementation is used. let's fix it.
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/the-problem-with-equality-54135425/?t=655)

---

## 5. Records

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/records-54135426/) · 13:59

### Summary

Records, introduced in C# 9, provide a concise way to define reference types with value-based equality semantics.
Primarily used for Data Transfer Objects (DTOs), records reduce boilerplate by automatically generating properties, constructors, and deconstructors.
They support immutability through init-only properties, non-destructive mutation via the with keyword, and offer enhanced ToString implementations for better debugging.
While records are reference types by default, they can also be defined as structs to optimize memory allocation and performance.

### Key concepts

*   Value-based equality for reference types.
*   Positional property syntax for concise DTO definitions.
*   Immutability via init-only properties.
*   Non-destructive mutation using the with expression.
*   Built-in ToString formatting for property inspection.
*   Positional deconstruction into variables.
*   record struct for stack-allocated value semantics.

### Lesson notes

Records address the challenges of implementing equality in standard classes and structs.
In C#, classes compare by reference while structs compare by value.
Records provide the benefits of reference types (passing by reference) while implementing value-based equality automatically.
This makes them ideal for Data Transfer Objects (DTOs), which are simple objects used to pass state between system components without containing complex logic.

```csharp
// records were introduced in C# 9 and aim to help with
// the equality problem, especially for simple situations
// where we have "data transfer objects" or "DTOs"
// a record is a reference type, but it has value semantics

public record MyRecord(
    int NumericValue,
    string StringValue);

// notice how we don't need to define the properties?! They
// are automatically created for us. we could do it manually
// though if we wanted to:

public record MyRecord2
{
    public int NumericValue { get; init; }
    public string StringValue { get; init; }
}

// note that the init keyword is used to make the properties
// immutable. we can still use the object initializer syntax
// to create the record though:
MyRecord myRecord1 = new(123, "ABC");
MyRecord2 myRecord2 = new()
{
    NumericValue = 123,
    StringValue = "ABC"
};

// but note that in both cases, we cannot change the properties
// because they are both "init" only:
//myRecord1.NumericValue = 456; // does not compile
//myRecord2.NumericValue = 456; // does not compile
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/records-54135426/?t=40)

The shorthand syntax for records allows for the declaration of positional properties in a single line.
This syntax automatically generates a constructor and public properties.
These properties are "init-only," meaning they can be set during object creation but are immutable thereafter.
This behavior can also be explicitly defined using the init keyword within a standard property block.

```csharp
// a record is a reference type, but it has value semantics


// note that the init keyword is used to make the properties
// immutable. we can still use the object initializer syntax
// to create the record though:
MyRecord myRecord1 = new(123, "ABC");
MyRecord2 myRecord2 = new()
{
    NumericValue = 123,
    StringValue = "ABC"
};


// but note that in both cases, we cannot change the properties
// because they are both "init" only:
//myRecord1.NumericValue = 456; // does not compile
//myRecord2.NumericValue = 456; // does not compile

//// so how does the equality work?
MyRecord recordA = new(123, "ABC");
MyRecord recordB = new(123, "ABC");
Console.WriteLine("recordA equal to recordB:");
Console.WriteLine(recordA == recordB); // True
Console.WriteLine(recordA.Equals(recordB)); // True
Console.WriteLine(object.Equals(recordA, recordB)); // True
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/records-54135426/?t=250)

Equality in records is determined by the values of their properties rather than their memory addresses.
When comparing two record instances, the == operator and .Equals() method return true if all properties match.
This eliminates the need to manually override GetHashCode or equality operators, which is often error-prone in standard classes.

```csharp
Console.WriteLine("recordA equal to recordB:");
Console.WriteLine(recordA == recordB); // True
Console.WriteLine(recordA.Equals(recordB)); // True
Console.WriteLine(object.Equals(recordA, recordB)); // True

//// we can also use the "with" keyword to create a new record
//// with the same values as the original, but with some changes!
MyRecord recordC = recordA with { NumericValue = 456 };

//// let's print this to the console and see what these look like:
Console.WriteLine(recordA); // MyRecord { NumericValue = 123, StringValue = ABC }
Console.WriteLine(recordB); // MyRecord { NumericValue = 123, StringValue = ABC }
Console.WriteLine(recordC); // MyRecord { NumericValue = 456, StringValue = ABC }
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/records-54135426/?t=355)

To modify an immutable record, C# provides the with keyword for non-destructive mutation.
This creates a new instance that copies all values from the original except for the properties specified in the with block.
Records also feature a specialized ToString implementation that automatically prints the type name and all property values, facilitating easier debugging compared to the default behavior of the Object class.

```csharp
MyRecord recordC = recordA with { NumericValue = 456 };

        //// let's print this to the console and see what these look like:
        Console.WriteLine(recordA); // MyRecord { NumericValue = 123, StringValue = ABC }
        Console.WriteLine(recordB); // MyRecord { NumericValue = 123, StringValue = ABC }
        Console.WriteLine(recordC); // MyRecord { NumericValue = 456, StringValue = ABC }

        // woah! records have a really nice ToString() implementation!

        // we can deconstruct the record into its properties:
        var (numericValue, stringValue) = recordA;

        // notice that it's positional based on the order of the properties
        // so this won't work:
        //(string stringValue2, int numericValue2) = recordA; // does not compile!

        // records can also be defined as structs, which means they'll
        // go on the stack instead of the heap. this can be useful for
        // performance reasons, especially if we have a lot of them.
    public record struct MyRecordStruct(
        int NumericValue,
        string StringValue);

    // if needed, we can mix in things like additional properties
    // that aren't just from the positional ones on the constructor
    public record MyRecordWithExtraProperties(
        int NumericValue,
        string StringValue)
    {
        public string ExtraProperty { get; set; }
    }
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/records-54135426/?t=625)

Records support deconstruction, allowing developers to unpack property values into individual variables.
This deconstruction is positional, meaning the order of variables must match the order of properties defined in the record declaration.
Furthermore, while records are typically reference types, they can be declared as record struct.
This allows the developer to utilize record features while allocating the object on the stack to improve performance and reduce heap pressure.

```csharp
public record struct MyRecordStruct(
        int NumericValue,
        string StringValue);

    // if needed, we can mix in things like additional properties
    // that aren't just from the positional ones on the constructor
    public record MyRecordWithExtraProperties(
        int NumericValue,
        string StringValue)
    {
        public string ExtraProperty { get; set; }
    }

    MyRecordWithExtraProperties recordWithExtraProperties = new(123, "ABC")
    {
        ExtraProperty = "DEF"
    };
    Console.WriteLine("recordWithExtraProperties.ExtraProperty (before):");
    Console.WriteLine(recordWithExtraProperties.ExtraProperty); // DEF
    recordWithExtraProperties.ExtraProperty = "AAA BBB CCC";
    Console.WriteLine("recordWithExtraProperties.ExtraProperty (after):");
    Console.WriteLine(recordWithExtraProperties.ExtraProperty); // AAA BBB CCC
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/records-54135426/?t=685)

Finally, records allow for the addition of standard mutable properties alongside positional ones.
While positional properties are immutable by default, developers can define additional properties with both getters and setters if the application requires a mix of fixed and modifiable state.
