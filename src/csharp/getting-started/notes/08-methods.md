# Methods

> Course: [Getting Started: C#](https://dometrain.com/course/getting-started-csharp/) · Chapter 8
> 3 lessons · ~17:55
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Simple Method](https://dometrain.com/take/course/getting-started-csharp-2732244/simple-method-54134885/) | 5:54 | [↓](#1-simple-method) |
| 2 | [Arguments](https://dometrain.com/take/course/getting-started-csharp-2732244/arguments-54134886/) | 4:28 | [↓](#2-arguments) |
| 3 | [Return Types](https://dometrain.com/take/course/getting-started-csharp-2732244/return-types-54134887/) | 7:33 | [↓](#3-return-types) |

---

## 1. Simple Method

> [Watch the lesson](https://dometrain.com/take/course/getting-started-csharp-2732244/simple-method-54134885/) · 5:54

### Summary

Methods are named blocks of code designed to perform specific tasks, enhancing both code reusability and readability.
By encapsulating logic into a method, developers can avoid redundancy and provide descriptive names to complex operations, making the codebase easier to maintain and understand.

### Key concepts

*   **Reusability**: Define logic once and invoke it multiple times to avoid duplication.
*   **Readability**: Assigning descriptive names to code blocks clarifies their purpose, even if they are only used once.
*   **void Keyword**: Specifies that a method performs an action but does not return a value to the caller.
*   **Method Signature**: Composed of the return type, the method name, and a parameter list in parentheses.
*   **PascalCase Naming**: C# methods typically use Title Case (e.g., `PrintHeader`), where each word starts with a capital letter.
*   **Method Invocation**: Executing a method by using its name followed by parentheses and a semicolon.

### Lesson notes

Methods and functions are fundamental building blocks in C# that allow developers to group code into reusable, named units.
This practice not only reduces repetition but also significantly improves code readability by providing context for what a specific block of code is intended to do.

A basic method definition starts with a return type.
The keyword `void` is used when a method does not return any data to the caller; it simply executes the code within its body.
Following the return type is the method name, which typically follows PascalCase conventions.
Parentheses follow the name; if the method requires no input data, these parentheses remain empty.
The body of the method is enclosed in curly braces `{}`.

```csharp
// a method is a block of code that performs a specific task
// we use methods to break our code into smaller, more manageable pieces!

// here is an example of a method
void ThisIsAMethod()
{
    // this is the body of the method
}

// we can call the method like this
ThisIsAMethod();
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/simple-method-54134885/?t=25)

To execute or "call" a method, you write its name followed by parentheses and a semicolon.
If the method definition has empty parentheses, the call must also have empty parentheses.

Methods are particularly useful for refactoring repetitive code.
For instance, if a program frequently prints a separator line to the console, that logic can be moved into a dedicated method.
This ensures that if the separator style needs to change (e.g., changing the number of dashes), it only needs to be updated in one location rather than at every instance where the line is printed.

```csharp
// if we had code like the following, how might
// we go make a method for it?
Console.WriteLine("-----------------");
Console.WriteLine("New Example!");
Console.WriteLine("-----------------");

// we could make a method like this:
void PrintSeparator()
{
    Console.WriteLine("-----------------");
}

// and then call it like this
PrintSeparator();
Console.WriteLine("New Example!");
PrintSeparator();
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/simple-method-54134885/?t=190)

Methods can also call other methods, allowing for higher levels of abstraction.
A `PrintHeader` method might call the `PrintSeparator` method multiple times to encapsulate the entire logic of displaying a header.
This further simplifies the main program logic to a single method call.

```csharp
// method that prints out the entire header
void PrintHeader()
{
    PrintSeparator();
    Console.WriteLine("New Example!");
    PrintSeparator();
}

// and then call it like this
PrintHeader();
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/simple-method-54134885/?t=310)

---

## 2. Arguments

> [Watch the lesson](https://dometrain.com/take/course/getting-started-csharp-2732244/arguments-54134886/) · 4:28

### Summary

This lesson introduces the concept of passing data into methods to increase code flexibility and reusability.
It clarifies the technical distinction between parameters—the variables defined in a method's signature—and arguments—the actual values provided during a method call.
By refactoring static methods to accept parameters, developers can create more versatile tools, such as a header printing method that accepts custom text for different sections of an application.

### Key concepts

- **Parameters**: Variables defined within the parentheses of a method signature that act as placeholders for incoming data.
- **Arguments**: The actual data values passed into a method's parameters when the method is invoked.
- **Method Reusability**: The ability to use a single method definition to perform operations on varying data by using parameters.
- **Parameter Lists**: Methods can accept multiple parameters of different types, such as strings and integers.

### Lesson notes

In C#, methods can be made more dynamic by allowing them to accept input.
This is handled through the definition of parameters and the passing of arguments.

#### Defining Parameters and Passing Arguments

A **parameter** is the variable defined in the method's declaration.
An **argument** is the specific data passed into that parameter when the method is called.
While these terms are often used interchangeably in casual conversation, the parameter is technically the definition, and the argument is the data.

```csharp
// a parameter is a variable in a method definition. When a method is called
// the arguments are the data you pass into the method's parameters.

// the parameters go into the parentheses of the method
// the arguments go into the parentheses of the method call

// here is an example of a method with parameters
void MyMethod(string name, int age)
{
    // the method body
}

// here is an example of a method call with arguments
MyMethod("Nick", 35);
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/arguments-54134886/?t=25)

In the example above, `name` and `age` are parameters defined in `MyMethod`.
When calling the method, `"Nick"` and `35` are the arguments that map to those parameters.

#### Refactoring for Reusability

By using parameters, static methods can be transformed into reusable components.
For example, a method that prints a header can be updated to accept a `name` parameter.
This allows the method to print different text based on the argument provided, rather than printing the same hardcoded string every time.

```csharp
void PrintSeparator()
{
    Console.WriteLine("--------------------");
}

void PrintHeader(string name)
{
    PrintSeparator();
    Console.WriteLine(name);
    PrintSeparator();
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/arguments-54134886/?t=120)

Inside `PrintHeader`, the `name` parameter is passed as an argument to `Console.WriteLine`.
This demonstrates that even built-in functions like `WriteLine` are methods that accept arguments.

#### Invoking Methods with Arguments

Once a method is defined with parameters, it can be invoked multiple times with different arguments to achieve different results within the same program flow.

```csharp
PrintHeader("Example 1:");
PrintHeader("Example 2:");
PrintHeader("Example 3:");
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/arguments-54134886/?t=220)

This approach allows for cleaner code and reduces redundancy by centralizing the logic for printing headers while allowing the content of those headers to vary.
Methods are not limited to simple types; they can also accept collections like arrays and lists as parameters.

---

## 3. Return Types

> [Watch the lesson](https://dometrain.com/take/course/getting-started-csharp-2732244/return-types-54134887/) · 7:33

### Summary

Return types enable methods to output data back to the calling environment, transforming a simple procedure into what is technically termed a function.
By replacing the void keyword with a specific data type and using the return keyword, a method can pass a single value back to the caller.
This value can be assigned to variables, used as an argument in subsequent method calls, or even ignored, provided that all data types involved in the return and assignment operations are compatible.

### Key concepts

*   **Return Values**: Data passed from a method back to the code that invoked it.
*   **Method vs. Function**: While often used interchangeably, a "function" technically refers to a method that has a return type, whereas a "method" (in the strictest sense) does not.
*   **The return Keyword**: Used inside a function to exit the block and send a specific value back to the caller.
*   **Type Matching**: The data type of the returned value must match the return type declared in the method signature.
*   **Order of Operations**: Nested function calls are evaluated from the innermost call outward to resolve arguments.

### Lesson notes

In C#, a return value is the data passed out of a method back to the calling code once execution completes.
While the terms are often used interchangeably, a method with a return type is technically called a function, whereas a method without one (using the `void` keyword) is simply a method.
A function is limited to returning a single value.

To define a function, replace the `void` keyword in the signature with the desired data type (such as `int` or `string`).
Inside the function body, the `return` keyword is used to specify the result.
For example, an `Add` function can take two integer parameters and return their sum.

```csharp
// here is an example of a method with a return value
int Add(int a, int b)
{
    return a + b;
}

// we can call the method like this
int sum = Add(5, 3);

// we can also call the method like this
int x = 5;
int y = 3;
int sum2 = Add(x, y);

// we can also call the method like this
int sum3 = Add(Add(1, 2), Add(3, 4));
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/return-types-54134887/?t=25)

When calling a function, the arguments can be constant values or variables.
Additionally, functions can be nested.
When nesting functions, C# follows an order of operations to resolve the expressions.
For a call like `Add(Add(1, 2), Add(3, 4))`, the compiler first evaluates the inner calls:

1.  `Add(1, 2)` is executed, returning `3`.
2.  `Add(3, 4)` is executed, returning `7`.
3.  The outer call becomes `Add(3, 7)`, which returns `10`.

```csharp
// we can also call the method like this
int sum3 = Add(Add(1, 2), Add(3, 4));
// int sum3 = Add(3, Add(3, 4));
// int sum3 = Add(3, 7);
// int sum3 = 10;
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/return-types-54134887/?t=280)

It is critical that data types match throughout the operation.
If a method is declared with an `int` return type, it must return an integer.
Attempting to declare a `string` return type for a method that returns the result of an integer addition will result in a compilation error, as an `int` is not implicitly compatible with a `string`.

```csharp
// the return value must match the type of the method
// so this would be an error
string Add(int a, int b)
{
    return a + b;
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/return-types-54134887/?t=340)

Similarly, the variable used to store the function's result must be compatible with the function's return type.
You cannot assign the result of an `int`-returning function directly into a `string` variable.

```csharp
// and similarly, this would be an error
string answer = Add(5, 3);
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/return-types-54134887/?t=385)

Finally, it is syntactically valid to call a function and ignore its return value.
If the logic within the function needs to execute but the specific result is not required for the next steps of the program, the call can stand alone without being assigned to a variable.

```csharp
// calling a function without storing the value
Add(5, 3);
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/return-types-54134887/?t=415)
