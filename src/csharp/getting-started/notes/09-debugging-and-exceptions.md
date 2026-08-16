# Debugging & Exceptions

> Course: [Getting Started: C#](https://dometrain.com/course/getting-started-csharp/) · Chapter 9
> 2 lessons · ~13:27
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Debugging and Exceptions](https://dometrain.com/take/course/getting-started-csharp-2732244/debugging-and-exceptions-54134892/) | 4:46 | [↓](#1-debugging-and-exceptions) |
| 2 | [Try Catch](https://dometrain.com/take/course/getting-started-csharp-2732244/try-catch-54134893/) | 8:41 | [↓](#2-try-catch) |

---

## 1. Debugging and Exceptions

> [Watch the lesson](https://dometrain.com/take/course/getting-started-csharp-2732244/debugging-and-exceptions-54134892/) · 4:46

### Summary

Debugging is the systematic process of identifying and resolving errors within a computer program.
In C#, runtime errors are referred to as exceptions, which are "thrown" when the application encounters an invalid operation, such as dividing an integer by zero.
An unhandled exception will cause the entire application to terminate, making it crucial for developers to understand how to troubleshoot these issues.
Visual Studio facilitates this process through tools like breakpoints, which pause execution at specific lines, and stepping (F10), which allows developers to execute code line-by-line to inspect variable values and program state.

### Key concepts

- **Debugging**: The process of finding and fixing errors within a computer program.
- **Exceptions**: Runtime errors thrown by the program when it encounters an invalid state.
- **Unhandled Exceptions**: Errors that are not caught by the program, leading to application termination.
- **Breakpoints**: Markers set in the IDE to pause execution at a specific line for inspection.
- **Stepping (F10)**: The "Step Over" command used to execute code one line at a time.
- **State Inspection**: The ability to view the current values of variables by hovering over them in the IDE during a debug session.

### Lesson notes

Debugging is a fundamental part of software development that allows programmers to step through their code and understand why it is not behaving as expected.
While writing code, it is common to encounter scenarios where the program runs but then fails due to an error.

In C#, these runtime errors are known as exceptions.
When an error occurs, the program "throws" an exception.
If the exception is not handled, the program will terminate immediately.
A classic example of an exception-throwing operation is integer division by zero.
Since dividing by zero results in an undefined value that cannot be represented as an integer, C# throws a `System.DivideByZeroException`.

```csharp
// debugging is the process of finding and
// fixing errors within a computer program
// errors in our C# programs are called exceptions
// exceptions are "thrown" when the program encounters an error

// let's create a simple program that throws an exception
int IntegerDivision(int x, int y)
{
    return x / y;
}

// the program will throw an exception when we try to divide by zero
int result = IntegerDivision(10, 0);
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/debugging-and-exceptions-54134892/?t=55)

When an exception occurs during a debug session in Visual Studio, the IDE highlights the line where the error happened in yellow.
This visual indicator shows the exact point where execution stopped.
Developers can then use the following tools to diagnose the problem:

- **Hovering**: Hovering the mouse over variables like `x` and `y` reveals their current values in the execution context. In the division example, hovering would show `x` as 10 and `y` as 0.
- **Breakpoints**: By clicking in the margin next to a line of code, a breakpoint can be set. This tells the IDE to pause the program's execution before it reaches that line, allowing the developer to examine the state of the application proactively.
- **Stepping (F10)**: Once the program is paused at a breakpoint, the "Step Over" command (F10) allows the developer to move through the code line-by-line. This is essential for observing how variable values change and identifying the exact moment an error is triggered.

Using these interactive tools is far more efficient than manually tracking program state, as it provides a real-time view of the application's behavior.

---

## 2. Try Catch

> [Watch the lesson](https://dometrain.com/take/course/getting-started-csharp-2732244/try-catch-54134893/) · 8:41

### Summary

This lesson covers error handling in C# using try-catch-finally blocks.
It demonstrates how to intercept exceptions to prevent application crashes, handle specific exception types, apply conditional exception filtering with the when keyword, and ensure code execution using the finally block.
Additionally, it explores debugging techniques in Visual Studio, such as using breakpoints and the call stack to inspect exception state.

### Key concepts

- **Try-Catch Blocks**: Mechanisms to intercept and handle exceptions to prevent program termination.
- **Specific Exception Handling**: Catching specific types like `DivideByZeroException` before generic `Exception` types.
- **Exception Filtering**: Using the `when` keyword to apply conditional logic to catch blocks.
- **Finally Block**: A block that executes after try and catch blocks, regardless of whether an exception was thrown or caught.
- **Debugging Tools**: Utilizing breakpoints, Step Over (F10), Step Into (F11), and the Call Stack to diagnose errors.

### Lesson notes

In C#, errors encountered during runtime are called exceptions.
When a program encounters an error it cannot handle, it "throws" an exception.
To prevent these exceptions from terminating the application, developers use `try-catch` blocks.
The `try` block contains the code that might fail, while the `catch` block contains the logic to handle the error if it occurs.

```csharp
// debugging is the process of finding and
// fixing errors within a computer program
// errors in our C# programs are called exceptions
// exceptions are "thrown" when the program encounters an error

// let's create a simple program that throws an exception
int IntegerDivision(int x, int y)
{
    return x / y;
}

// the program will throw an exception when we try to divide by zero
// int result = IntegerDivision(10, 0);

// exceptions are caught using try-catch blocks
// try-catch blocks look like this:
try
{
    // code that might throw an exception
}
catch (Exception e)
{
    // code that runs if an exception is thrown
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/try-catch-54134893/?t=5)

When an exception is caught, the program gains access to an exception object (often named `e`).
This object contains a `Message` property describing the error and a `StackTrace` indicating where the error occurred in the source code.

```csharp
// let's catch the exception from our IntegerDivision method
try
{
    IntegerDivision(10, 0);
}
catch (Exception e)
{
    Console.WriteLine("An exception was thrown!");
    Console.WriteLine(e.Message);
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/try-catch-54134893/?t=70)

#### Debugging Exceptions

Visual Studio provides powerful tools for inspecting exceptions.
By placing a breakpoint at the start of a `try` block, you can step through the code line-by-line using **F10 (Step Over)** or dive into specific methods using **F11 (Step Into)**.
When an exception occurs during debugging, the IDE identifies it as a "caught exception" and provides a **Call Stack**, which shows the sequence of method calls that led to the failure.

#### Specific Exceptions and Filtering

You can define multiple `catch` blocks to handle different types of errors uniquely.
C# evaluates these blocks in order from top to bottom; therefore, more specific exceptions (like `DivideByZeroException`) must be placed before the generic `Exception` class.

```csharp
// we can catch specific types of exceptions and handle them differently
try
{
    IntegerDivision(10, 0);
}
catch (DivideByZeroException e)
{
    Console.WriteLine("You can't divide by zero!");
}
catch (Exception e)
{
    Console.WriteLine("An exception was thrown!");
    Console.WriteLine(e.Message);
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/try-catch-54134893/?t=265)

C# also supports **exception filtering** using the `when` keyword.
This allows a catch block to execute only if a specific condition is met, such as checking the contents of the exception message.

```csharp
// we can also use exception filters to catch exceptions that meet certain conditions
try
{
    IntegerDivision(10, 0);
}
catch (Exception e) when (e.Message.Contains("divide by zero"))
{
    Console.WriteLine("You can't divide by zero!");
}
catch (Exception e)
{
    Console.WriteLine("An exception was thrown!");
    Console.WriteLine(e.Message);
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/try-catch-54134893/?t=310)

#### The Finally Block

The `finally` block is used to guarantee that a specific set of code runs regardless of whether an exception was thrown or caught.
This is useful for cleanup tasks, such as closing file streams or database connections.
However, if an exception is thrown that is not matched by any `catch` block, the program will terminate, and the `finally` block may not execute as expected in all environments.

```csharp
// we can use a finally block to run code after a try-catch block
try
{
    IntegerDivision(10, 0);
}
catch (Exception e)
{
    Console.WriteLine("An exception was thrown!");
    Console.WriteLine(e.Message);
}
finally
{
    Console.WriteLine("This code always runs!");
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/try-catch-54134893/?t=370)

If the `catch` block does not match the exception type thrown, the `finally` block will not run before the program crashes.
For example, if `IntegerDivision` throws a `DivideByZeroException` but the code only catches `FormatException`, the exception remains unhandled.

```csharp
// Example of an unhandled exception where finally won't help prevent a crash
try
{
    IntegerDivision(10, 0);
}
catch (FormatException e)
{
    Console.WriteLine("An exception was thrown!");
    Console.WriteLine(e.Message);
}
finally
{
    Console.WriteLine("This code always runs!");
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/try-catch-54134893/?t=400)
