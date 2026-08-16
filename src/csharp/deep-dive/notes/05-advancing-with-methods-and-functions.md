# Advancing with Methods and Functions

> Course: [Deep Dive: C#](https://dometrain.com/course/deep-dive-csharp/) · Chapter 5
> 5 lessons · ~55:36
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Callbacks and Delegates](https://dometrain.com/take/course/deep-dive-csharp-2732260/callbacks-and-delegates-54135559/) | 14:40 | [↓](#1-callbacks-and-delegates) |
| 2 | [Extension Methods](https://dometrain.com/take/course/deep-dive-csharp-2732260/extension-methods-54135560/) | 5:05 | [↓](#2-extension-methods) |
| 3 | [LINQ](https://dometrain.com/take/course/deep-dive-csharp-2732260/linq-54135561/) | 16:27 | [↓](#3-linq) |
| 4 | [Lazy](https://dometrain.com/take/course/deep-dive-csharp-2732260/lazy-54135562/) | 5:43 | [↓](#4-lazy) |
| 5 | [Events](https://dometrain.com/take/course/deep-dive-csharp-2732260/events-54135563/) | 13:41 | [↓](#5-events) |

---

## 1. Callbacks and Delegates

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/callbacks-and-delegates-54135559/) · 14:40

### Summary

Callbacks and delegates enable methods to be treated as first-class objects in C#, allowing them to be stored in variables, passed as arguments, and executed asynchronously or at a later time.
This lesson explores the conceptual foundation of callbacks, the usage of built-in delegate types like Action and Func, and the implementation of custom delegates to enhance code readability and maintainability.

### Key concepts

- **Callback**: A conceptual pattern where a method is provided to another piece of code to be executed after a specific task or event completes.
- **Delegate**: A type-safe object that defines a method signature and can hold references to methods matching that signature.
- **Action**: A built-in delegate type used for methods that return `void` and can take zero or more parameters.
- **Func**: A built-in delegate type used for methods that return a value. The last type parameter always specifies the return type.
- **Predicate**: A specialized built-in delegate that takes one input and returns a `bool`, commonly used for filtering.
- **Custom Delegates**: User-defined delegate types created with the `delegate` keyword to provide more descriptive parameter names and improve IntelliSense support.

### Lesson notes

#### Introduction to Delegates and Callbacks

In C#, methods are typically defined and called immediately.
However, delegates allow us to treat methods like variables.
A delegate defines a method signature, and any method matching that signature can be assigned to a variable of that delegate type.

A callback is a conceptual application of delegates.
It allows a developer to say, "Here is a method; run it when you are finished with your current task."
This is useful when the timing of completion is unknown, such as after a file download, a button click, or a system alert.

#### Built-in Delegates: Action and Func

C# provides several built-in delegate types to handle common scenarios.
The most basic is `Action`, which represents a `void` method.
While a standard `Action` takes no parameters, it can be made generic to accept arguments.

```csharp
// the most basic form is "Action", so let's store 
// a method into an Action variable
Action action = NicksAction;

// now we can call the method by invoking the variable:
action();
action.Invoke(); // either way works!

void NicksAction()
{
    Console.WriteLine("Hello from Nick!");
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/callbacks-and-delegates-54135559/?t=40)

When a return type is required, the `Func` delegate is used.
`Func` can take up to 16 input parameters, with the final type parameter always representing the return value.

```csharp
// if you want to define a function, we can use the "Func" type:
// the very last type parameter provided is the return type!
Func<int, int, int> addFunction = AddFunction;
Func<int, int, int> subtractFunction = SubtractFunction;

int AddFunction(int a, int b)
{
    return a + b;
}

int SubtractFunction(int a, int b)
{
    return a - b;
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/callbacks-and-delegates-54135559/?t=250)

#### Implementing Callbacks

Callbacks are implemented by passing a delegate as a parameter to another method.
In the following example, `DoSomethingAfterUserPressesEnter` accepts an `Action` and executes it only after the user provides input.

```csharp
void DoSomethingAfterUserPressesEnter(Action callback)
{
    Console.WriteLine("Press enter for a surprise!");
    Console.ReadLine();
    callback();
}

DoSomethingAfterUserPressesEnter(NicksAction);
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/callbacks-and-delegates-54135559/?t=340)

This pattern decouples the logic of *when* something happens from *what* happens.
For instance, a `Calculate` method can manage user input and output while remaining agnostic about the specific mathematical operation being performed.

```csharp
// we can also pass a function as a parameter:
void Calculate(Func<int, int, int> calculateCallback)
{
    Console.WriteLine("Enter the first integer: ");
    int a = int.Parse(Console.ReadLine());

    Console.WriteLine("Enter the second integer: ");
    int b = int.Parse(Console.ReadLine());

    int result = calculateCallback(a, b);
    Console.WriteLine($"The result is: {result}");
}

Console.WriteLine("Addition Example:");
Calculate(addFunction);
Console.WriteLine("Subtraction Example:");
Calculate(subtractFunction);
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/callbacks-and-delegates-54135559/?t=520)

#### Custom Delegates for Readability

While `Action` and `Func` are convenient, they lack descriptive parameter names in IntelliSense (often showing generic names like `arg1`, `arg2`).
By defining a custom delegate, developers can provide meaningful names for parameters, which significantly improves code readability and maintainability.

```csharp
delegate int CalculateDelegate(
    int firstNumber,
    int secondNumber);

// Usage in a method
void Calculate2(CalculateDelegate calculateCallback)
{
    Console.WriteLine("Enter the first integer: ");
    int a = int.Parse(Console.ReadLine());

    Console.WriteLine("Enter the second integer: ");
    int b = int.Parse(Console.ReadLine());

    // check the intellisense to see that we can
    // get the names clearly provided now!
    int result = calculateCallback(a, b);
    Console.WriteLine($"The result is: {result}");
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/callbacks-and-delegates-54135559/?t=790)

In addition to `Action` and `Func`, C# includes the `Predicate<T>` delegate, which is essentially a `Func<T, bool>`.
It is primarily used for criteria-based filtering.

---

## 2. Extension Methods

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/extension-methods-54135560/) · 5:05

Extension methods allow you to add functionality to a class or interface without modifying its source code, inheriting from it, or recompiling the original type.
While they appear to be instance methods when called, they are actually a form of syntactic sugar for static methods.

### Key concepts

- **Syntactic Sugar**: Extension methods make static calls look like instance calls for better readability.
- **Static Requirements**: Extension methods must be defined as static methods within a non-generic, static class.
- **The "this" Modifier**: The first parameter of the method specifies the type being extended and must be preceded by the `this` keyword.
- **Parameter Ordering**: The parameter marked with `this` must always be the first parameter in the method signature.
- **LINQ Integration**: Most LINQ functionality (e.g., `.Where()`, `.Select()`) is implemented via extension methods on `IEnumerable<T>`.
- **Best Practices**: Use extension methods for small helper utilities; avoid using them for complex logic that requires dependency injection or extensive unit testing.

### Lesson notes

To create an extension method, you must define a static method inside a static class.
The first parameter of this method determines which type is being extended.
By prefixing that first parameter with the `this` keyword, you tell the C# compiler to allow this method to be called on instances of that type using dot notation.

```csharp
// extension methods in C# allow us to add new methods
// to existing types without modifying the original type
// or creating a new derived type

// extension methods are a special kind of static method
// but they look like instance methods!

// The requirements are:
// - We need a static class
// - We need a static method on the class
// - We need the "this" keyword on the parameter that we are "extending"
// - The parameter marked with "this" must be the first parameter
public static class Extensions
{
    public static string Reverse(
        this string str)
    {
        var reversedChars = str
            .Reverse<char>()
            .ToArray();
        var reversed = new string(reversedChars);
        return reversed;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/extension-methods-54135560/?t=40)

The name of the static class containing the extension methods generally does not matter unless you intend to call the method using traditional static syntax.
When called as an extension, the class name is omitted, and the method appears as a member of the object itself.
This is particularly powerful for creating fluent APIs.

```csharp
// extension methods in C# allow us to add new methods
// to existing types without modifying the original type
// or creating a new derived type

// extension methods are a special kind of static method
// but they look like instance methods!



// the name of this class ONLY matters if your goal
// is to call it the traditional static method call way
var reversedStr = Extensions.Reverse("Hello World");

// but when we call it like an extension method
// we get this really cool syntax where it looks
// like Reverse() is a method that's built
// into the string class!
var forwardStr = reversedStr.Reverse();

// there's a popular part of dotnet that uses extension methods
// called LINQ (Language Integrated Query)!

IEnumerable<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

// nearly all of the methods we see in intellisense
// when accessing numbers are extension methods
// from LINQ!
var evenNumbers = numbers.Where(n => n % 2 == 0);


// The requirements are:
// - We need a static class
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/extension-methods-54135560/?t=145)

LINQ (Language Integrated Query) relies heavily on this pattern.
For instance, the `Where` method is not a member of the `IEnumerable<T>` interface itself; it is a static extension method defined in the `System.Linq.Enumerable` class.
This allows every collection implementing `IEnumerable<T>` to gain LINQ capabilities automatically.

```csharp
namespace System.Linq
{
    public static partial class Enumerable
    {
        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
            }

            if (predicate == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.predicate);
            }

            if (source is Iterator<TSource> iterator)
            {
                return iterator.Where(predicate);
            }

            if (source is TSource[] array)
            {
                return array.Length == 0 ?
                    Empty<TSource>() :
                    new WhereArrayIterator<TSource>(array, predicate);
            }

            if (source is List<TSource> list)
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/extension-methods-54135560/?t=235)

While extension methods are convenient, they should be used with caution.
Chaining too many extension methods can make code complex and difficult to debug.
Furthermore, because they are static, they do not support dependency injection, which can make testing more difficult if they contain complex logic.
It is recommended to keep extension methods limited to simple helper functions.

---

## 3. LINQ

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/linq-54135561/) · 16:27

### Summary

LINQ (Language Integrated Query) provides a powerful set of extension methods for IEnumerable<T> within the System.Linq namespace, enabling developers to perform mapping, filtering, and reduction operations on collections.
A critical aspect of LINQ is its lazy execution model, where queries are treated as iterators and only evaluated when the collection is materialized through methods like ToList() or via a foreach loop.
Understanding this deferred execution is essential for managing performance and avoiding redundant, expensive calculations.

### Key concepts

* **System.Linq Namespace**: The location of the standard LINQ extension methods.
* **IEnumerable<T> Extensions**: LINQ methods operate on any type implementing the IEnumerable interface.
* **Mapping**: Transforming elements in a collection using the `Select` method.
* **Filtering**: Reducing the number of items in a collection based on a predicate using the `Where` method.
* **Reduction**: Calculating a single value from a collection (e.g., `Average`, `Sum`, `Count`).
* **Method Chaining**: The ability to link multiple LINQ operations into a single pipeline because most methods return a new IEnumerable.
* **Lazy Evaluation**: LINQ queries are iterators that act as function pointers; they do not execute until the data is requested.
* **Materialization**: The process of forcing evaluation and storing results in memory using methods like `ToList()` or `ToArray()`.

### Lesson notes

#### Core LINQ Operations

LINQ methods generally fall into three categories: mapping, filtering, and reducing.
Mapping involves transforming each item in a set.
In traditional C#, this requires initializing a new list and using a `foreach` loop to parse or convert values.
LINQ simplifies this with the `Select` method, which accepts a delegate to perform the transformation.

Filtering reduces the set of items based on a condition.
Instead of a `foreach` loop containing an `if` statement, LINQ uses the `Where` method.
Reduction operations take a collection and return a single calculated value, such as an average or a sum.

```csharp
// LINQ stands for Language Integrated Query
// We get access to a bunch of LINQ methods in the System.Linq namespace
// that operate on IEnumerable<T>
// They're all... extension methods!

// LINQ can help us
// - map: transform each item
// - filter: only take some items
// - reduce: combine items

// map: transform each element in a collection
List<string> rawNumbers = [ "1", "2", "3", "4", "5" ];

List<int> numbers = new();
foreach (string rawNumber in rawNumbers)
{
    numbers.Add(int.Parse(rawNumber));
}

// The basic LINQ method for mapping is Select:
var numbers2 = rawNumbers
    .Select(number => int.Parse(number))
    .ToList();

// filter: remove elements from a collection
List<int> evenNumbers = new();
foreach (int number in numbers)
{
    if (number % 2 == 0)
    {
        evenNumbers.Add(number);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/linq-54135561/?t=25)

Reduction methods like `Average()` replace manual accumulation logic.
These methods are terminal operations that return a scalar value rather than another `IEnumerable`.

```csharp
if (number % 2 == 0)
    {
        evenNumbers.Add(number);
    }
}

// using LINQ we could do...
var evenNumbers2 = evenNumbers
    .Where(number => number % 2 == 0)
    .ToList();

// to do a reduction (average in this case) without using LINQ:
int sum = 0;
foreach (int number in numbers)
{
    sum += number;
}

double average = sum / (double)numbers.Count;

// We can use Average() from LINQ:
var averageByLinq = numbers.Average();
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/linq-54135561/?t=205)

#### Method Chaining

Because LINQ methods that map or filter return an `IEnumerable<T>`, they can be chained together to create complex data processing pipelines.
This allows for expressive, vertical code organization, though developers should be mindful of readability and debugging complexity when chains become excessively long.

```csharp
double average = sum / (double)numbers.Count;

// We can use Average() from LINQ:
var averageByLinq = numbers.Average();

// there are MANY more LINQ methods...
// and we can chain them together to build
// more complex pipelines!
List<string> biggerListOfRawNumbers = [ "0", "9", "1", "8", "2", "7", "3", "6", "4", "5" ];
var magicNumber = biggerListOfRawNumbers
    .Select(int.Parse) // converts everything to integers
    .OrderByDescending(number => number) // orders from biggest to smallest number
    .TakeLast(5) // should only take 4, 3, 2, 1, 0
    .Where(number => number % 2 == 0) // should only take 4, 2, 0 (even numbers)
    .Average(); // should be 2
Console.WriteLine($"The magic number is {magicNumber}!");
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/linq-54135561/?t=235)

#### Lazy Evaluation and Materialization

LINQ uses deferred execution.
A LINQ query is essentially a function pointer (an iterator) that defines how to get the data, but it does not actually execute the logic until the collection is enumerated.
This means simply assigning a LINQ query to a variable does not perform any work.

```csharp
.Select(int.Parse) // converts everything to integers
            .OrderByDescending(number => number) // orders from biggest to smallest number
            .TakeLast(5) // should only take 4, 3, 2, 1, 0
            .Where(number => number % 2 == 0) // should only take 4, 2, 0 (even numbers)
            .Average(); // should be 2
Console.WriteLine($"The magic number is {magicNumber}!");
*/

// LINQ methods are "lazy" because they are "iterators"
// they don't do anything until you start enumerating them
Console.WriteLine("Press enter to start the lazy example.");
Console.ReadLine();
Console.WriteLine("Before the LINQ line for lazyNumbersAsStrings");
var lazyNumbersAsStrings = numbers
    .Select(number =>
    {
        Console.WriteLine($"Transforming {number} to a string");
        return number.ToString();
    });
Console.WriteLine("After the LINQ line for lazyNumbersAsStrings");

// force enumeration
Console.WriteLine("Before forcing enumeration of lazyNumbersAsStrings.");
lazyNumbersAsStrings.ToArray();
Console.WriteLine("After forcing enumeration of lazyNumbersAsStrings.");
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/linq-54135561/?t=490)

If a LINQ query is not materialized (using `ToArray()` or `ToList()`), it will re-evaluate every time it is iterated.
This can lead to significant performance penalties if the underlying operation is expensive, such as a database call or a `Thread.Sleep` simulation.

```csharp
// in a variable!
Console.WriteLine("Press enter to start expensive operation.");
Console.ReadLine();
var expensiveToCalculate = numbers
    .Select(number =>
    {
        Console.WriteLine($"Transforming {number} to a string");
        Thread.Sleep(1000);
        return number.ToString();
    });

Console.WriteLine("Before first enumeration of expensive operation...");
foreach (var numberAsString in expensiveToCalculate)
{
    Console.WriteLine(numberAsString);
}
Console.WriteLine("After first enumeration of expensive operation...");

Console.WriteLine("Before second enumeration of expensive operation...");
foreach (var numberAsString in expensiveToCalculate)
{
    Console.WriteLine(numberAsString);
}
Console.WriteLine("After second enumeration of expensive operation...");
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/linq-54135561/?t=715)

#### Custom LINQ Methods

Since LINQ methods are just extension methods on `IEnumerable<T>`, you can implement custom LINQ-like behavior.
These methods should be static, reside in a static class, and use the `yield return` keyword to maintain the iterator/lazy behavior expected of LINQ.

```csharp
// we can make our own LINQ methods!
var myLinqResult = numbers
    .NicksFancyLinqMethod(number => number * 2)
    .ToArray();

foreach (var number in myLinqResult)
{
    Console.WriteLine(number);
}

public static class MyLinq
{
    public static IEnumerable<T> NicksFancyLinqMethod<T>(
        this IEnumerable<T> source,
        Func<T, T> selector)
    {
        foreach (T item in source)
        {
            Console.WriteLine($"Applying selector to {item}");
            yield return selector(item);
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/linq-54135561/?t=775)

---

## 4. Lazy

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/lazy-54135562/) · 5:43

### Summary

The `Lazy<T>` class in C# provides a mechanism for deferred initialization, allowing developers to define an expensive or resource-intensive operation that only executes when its result is first accessed.
By wrapping a delegate, `Lazy<T>` ensures that the initialization logic runs exactly once and caches the result for all subsequent requests.
This pattern is particularly useful for optimizing application startup times and implementing thread-safe, singleton-like behavior without global state.

### Key concepts

- **Deferred Execution**: Initialization logic is only executed when the `Value` property is accessed for the first time.
- **Memoization**: The result of the initialization is cached; subsequent calls return the same instance or value instantly.
- **Thread Safety**: `Lazy<T>` is thread-safe by default, preventing race conditions during the initialization of the value.
- **Singleton-like Behavior**: Provides a way to ensure a single instance of a value is created without requiring it to be globally accessible.
- **Resource Optimization**: Useful for deferring expensive operations (like reading configuration files) until they are strictly necessary.

### Lesson notes

The `Lazy<T>` class is a generic type that allows you to pass in a method or delegate to be executed at a later point in time.
This is particularly powerful because it allows you to define how a value should be created, but delay that creation until the moment the value is actually needed.

To use `Lazy<T>`, you instantiate the class with a callback (anonymous delegate) that returns the desired type.
In the following example, a `Lazy<int>` is created to find the maximum value in an array.
A `Thread.Sleep` is included to simulate a time-consuming, expensive operation.

```csharp
// Lazy<T> is a generic type that we have in C#
// that allows us to defer the creation of a value.
// It also acts like a singleton, without being
// global. It's a thread-safe way to create a value
// only when it's needed.

Lazy<int> lazyValue = new Lazy<int>(() =>
{
    Console.WriteLine("This will only run once.");
    Console.WriteLine("Finding the max...");
    int[] numbers = [35, 20, 30, 40, 50];

    int max = int.MinValue;
    foreach (var number in numbers)
    {
        if (number > max)
        {
            max = number;
        }

        // pretend this is an expensive operation
        Thread.Sleep(1000);
    }

    Console.WriteLine("The max value is: " + max);
    return max;
});
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/lazy-54135562/?t=220)

When you first ask the instance for its value using the `.Value` property, the delegate runs.
Because the example array contains five elements and each iteration sleeps for one second, the first access to `.Value` will take approximately five seconds.

```csharp
Console.WriteLine("The value of lazyValue is: " + lazyValue.Value);
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/lazy-54135562/?t=130)

Once the value is computed, `Lazy<T>` stores it.
Any subsequent calls to the `.Value` property return the cached result immediately without re-executing the delegate logic.

```csharp
Lazy<int> lazyValue = new Lazy<int>(() =>
{
    Console.WriteLine("This will only run once.");
    Console.WriteLine("Finding the max...");
    int[] numbers = [35, 20, 30, 40, 50];

    int max = int.MinValue;
    foreach (var number in numbers)
    {
        if (number > max)
        {
            max = number;
        }

        // pretend this is an expensive operation
        Thread.Sleep(1000);
    }

    Console.WriteLine("The max value is: " + max);
    return max;
});

Console.WriteLine("The value of lazyValue is: " + lazyValue.Value);
Console.WriteLine("The value of lazyValue is: " + lazyValue.Value);
Console.WriteLine("The value of lazyValue is: " + lazyValue.Value);
Console.WriteLine("The value of lazyValue is: " + lazyValue.Value);
Console.WriteLine("The value of lazyValue is: " + lazyValue.Value);
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/lazy-54135562/?t=250)

#### Use Cases and Thread Safety

`Lazy<T>` is highly valuable for application startup optimization.
Instead of performing all configuration and initialization at the beginning, which can lead to long load times, you can defer these tasks so they occur "just in time."
For example, reading a settings file can be wrapped in a `Lazy` object so the file is only read when a specific setting is requested.

Furthermore, the `Lazy<T>` class is thread-safe.
If multiple threads attempt to access the `.Value` property simultaneously, the class manages the synchronization internally to ensure the initialization logic does not suffer from race conditions and the delegate only runs once.

---

## 5. Events

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/events-54135563/) · 13:41

### Summary

Events in C# implement the observer pattern, allowing an object (the sender) to notify subscribers when a specific action or state change occurs.
They are built on delegates but provide a restricted interface that only allows external callers to subscribe or unsubscribe using the += and -= operators.
This lesson covers the standard EventHandler pattern, the creation of custom EventArgs, safe event invocation using the null-conditional operator, and the importance of managing subscription lifetimes to prevent memory leaks.

### Key concepts

- **EventHandler<TEventArgs>**: The standard delegate used for events, requiring a void return type and two parameters: object sender and TEventArgs e.
- **EventArgs**: The base class for all event data; custom data should be passed via a class inheriting from this.
- **Multicast Support**: Events allow multiple handlers to be attached; they are executed in the order they were added.
- **Encapsulation**: The event keyword prevents external classes from clearing the handler list or raising the event directly.
- **Null-Safety**: Events with no subscribers are null, requiring checks before invocation.
- **Memory Management**: Unsubscribing from events is critical when the source and handler have different lifetimes.

### Lesson notes

Events provide a mechanism for a class to notify other components about changes.
While frequently associated with UI frameworks like WPF or WinForms, they are a general-purpose tool for implementing the observer pattern in C#.

#### The Event Handler Signature

Standard C# events follow a specific delegate signature.
The handler returns void and accepts two arguments: the sender (the object raising the event) and the event arguments containing relevant data.

```csharp
public delegate void EventHandler<TEventArgs>(object sender, TEventArgs e)
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/events-54135563/?t=115)

#### Custom Event Arguments

To pass data with an event, you should create a class that inherits from System.EventArgs.
This ensures compatibility with the standard EventHandler<T> delegate.

```csharp
public class MessageEventArgs : EventArgs
{
    public MessageEventArgs(string message)
    {
        Message = message;
    }
    public string Message { get; }
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/events-54135563/?t=175)

#### Declaring and Raising Events

A class that acts as an event source declares the event using the event keyword.
This keyword provides protection: only the containing class can "raise" (invoke) the event.
From the outside, only subscription (+=) and unsubscription (-=) are permitted.

When raising an event, it is essential to check if any handlers are subscribed.
If no one has subscribed, the event variable will be null, and attempting to invoke it will throw a NullReferenceException.
The modern, thread-safe way to handle this is using the null-conditional operator (?.).

```csharp
public class EventSource
{
    // this declares the event, and the type of the event
    // but nobody outside of this class can raise the event
    // directly by accessing this
    public event EventHandler<MessageEventArgs> SourceChanged;

    public void RaiseEvent(string message)
    {
        // but that can be greatly simplified to the following:
        SourceChanged?.Invoke(this, new MessageEventArgs(message));
    }
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/events-54135563/?t=445)

#### Subscribing to Events

Subscribers use the += operator to attach a method to an event.
Because events are multicast, multiple methods can be attached to the same event.
Unsubscribing is handled via the -= operator.
It is safe to call -= on a method that is not currently subscribed; it will simply have no effect.

```csharp
EventSource source = new EventSource();

// we hook up a new handler with +=
source.SourceChanged += Source_SourceChanged;

// this will cause the event to be raised
source.RaiseEvent("Hello, world!");

// we can remove the event with -=
source.SourceChanged -= Source_SourceChanged;
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/events-54135563/?t=505)

#### Multicast Handlers

When multiple handlers are subscribed, they are chained together and executed in the order they were registered.

```csharp
void Source_SourceChanged1(object? sender, MessageEventArgs e)
{
    Console.WriteLine("This is the first handler!");
    Console.WriteLine($"Sender: {sender}");
    Console.WriteLine($"Message: {e.Message}");
}

void Source_SourceChanged2(object? sender, MessageEventArgs e)
{
    Console.WriteLine("This is the second handler!");
    Console.WriteLine($"Sender: {sender}");
    Console.WriteLine($"Message: {e.Message}");
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/events-54135563/?t=640)

#### Memory Leak Considerations

Events can easily cause memory leaks in managed code.
When a handler subscribes to a source, the source holds a reference to the handler's object.
If the source has a longer lifetime than the subscriber (e.g., a long-running service vs. a short-lived object), the subscriber will not be garbage collected as long as the subscription exists.
Always ensure that objects unsubscribe from events when they are no longer needed, especially in long-running server applications where event leaks can build up over time.
