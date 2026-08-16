# Basic Value Types

> Course: [Getting Started: C#](https://dometrain.com/course/getting-started-csharp/) · Chapter 4
> 7 lessons · ~50:18
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [What are variables?](https://dometrain.com/take/course/getting-started-csharp-2732244/what-are-variables-54134782/) | 4:17 | [↓](#1-what-are-variables) |
| 2 | [Strings](https://dometrain.com/take/course/getting-started-csharp-2732244/strings-54134783/) | 10:22 | [↓](#2-strings) |
| 3 | [Integers](https://dometrain.com/take/course/getting-started-csharp-2732244/integers-54134784/) | 7:33 | [↓](#3-integers) |
| 4 | [Float & Double](https://dometrain.com/take/course/getting-started-csharp-2732244/float-double-54134785/) | 5:56 | [↓](#4-float--double) |
| 5 | [Boolean](https://dometrain.com/take/course/getting-started-csharp-2732244/boolean-54134786/) | 8:12 | [↓](#5-boolean) |
| 6 | [DateTime](https://dometrain.com/take/course/getting-started-csharp-2732244/datetime-54134787/) | 5:42 | [↓](#6-datetime) |
| 7 | [Casting & Parsing](https://dometrain.com/take/course/getting-started-csharp-2732244/casting-parsing-54134788/) | 8:16 | [↓](#7-casting--parsing) |

---

## 1. What are variables?

> [Watch the lesson](https://dometrain.com/take/course/getting-started-csharp-2732244/what-are-variables-54134782/) · 4:17

### Summary

Variables are fundamental building blocks in C# used to store and reference data in memory, representing the "state" of a program.
They allow developers to track information such as user input, calculation results, or application status throughout the execution of a program.

### Key concepts

- **Memory Storage**: Variables hold data in RAM for immediate access during execution.
- **Program State**: Variables represent the current context or status of an application.
- **Basic Value Types**: Includes strings (text), integers (whole numbers), floating-point numbers (decimals), and Booleans (true/false).
- **Strong Typing**: C# requires variables to have a defined type that cannot be changed to an incompatible type later, ensuring code predictability.

### Lesson notes

Variables serve as the fundamental mechanism for data storage within a C# application.
While data can be persisted to files or databases, variables specifically facilitate the referencing of data in memory (RAM).
This allows a program to maintain "state"—the contextual information required to execute logic and solve problems.

In practice, variables track both simple and complex data.
For instance, in a basic mathematical operation, variables store user inputs and the resulting output.
In more complex scenarios, they might function as counters for iterative processes or store session data like usernames and passwords.

C# utilizes several primitive value types to represent data:

- **Strings**: Represent sequences of characters or words.
- **Integers**: Represent whole numbers without decimal components.
- **Floating-point and Double-precision**: Represent numbers with decimal places.
- **Booleans**: Represent binary states, such as true/false or on/off.
- **DateTime**: Represents specific dates and times.

A defining characteristic of C# is that it is a strongly typed language.
In contrast to dynamically typed languages where a variable's data type can change during execution, C# requires that a variable's type remain consistent.
Once a variable is defined to hold a specific type, such as an integer, it cannot be reassigned to hold an incompatible type, such as a string.
This strictness enhances code maintainability and clarity by ensuring the developer always knows the nature of the data being accessed.

---

## 2. Strings

> [Watch the lesson](https://dometrain.com/take/course/getting-started-csharp-2732244/strings-54134783/) · 10:22

### Summary

This lesson covers the fundamentals of working with strings in C#, including declaration, initialization, and common operations.
It explores string literals, the use of escape sequences for special characters, and string concatenation using the plus operator.
The lesson also demonstrates how to interact with the console for input and output, retrieve string metadata like length, access individual characters via indexing, and the importance of terminating statements with semicolons.

### Key concepts

- String literals are enclosed in double quotes.
- Escape sequences (e.g., `\"`, `\n`) allow inclusion of special characters.
- Variables are declared using the `string` keyword.
- String concatenation is performed using the `+` operator.
- `Console.ReadLine()` is used to capture string input from the user.
- The `.Length` property provides the number of characters in a string.
- Individual characters can be accessed using zero-based indexing (e.g., `[0]`).
- The `char` type represents a single character and uses single quotes.
- C# statements must be terminated with a semicolon (`;`).

### Lesson notes

In C#, strings are a foundational data type used to represent sequences of characters.
A string literal is defined by enclosing text within double quotes.
If you need to include a double quote character within the string itself, you must use an escape sequence by preceding the quote with a backslash (`\`).

```csharp
// strings are represented by double quotes ""
// In the following line, what part is the string?
Console.WriteLine("Hello,\" World!");
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/strings-54134783/?t=70)

To use strings dynamically, you declare string variables using the `string` keyword.
While there are various naming conventions, the most common in C# is camelCase (e.g., `myString`), though PascalCase or underscores are sometimes seen.
It is best practice to use descriptive, verbose names to ensure code readability.
You can declare a variable and assign it a value in separate steps or combine them into a single initialization statement.
Multiple strings can be combined into one through a process called concatenation, using the `+` operator.

```csharp
// We can "declare" a string variable
string myString;
string my_string;
string MyString;

// we can assign a value to a string variable
myString = "Hello, World!";

// we can declare and assign in one line
string coolString = "Hello, World!";

// we can re-assign a value to a string variable
coolString = "Goodbye, World!";

// we can "concatenate" strings
string firstName = "John";
string lastName = "Doe";
string fullName = firstName + " " + lastName;
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/strings-54134783/?t=100)

Beyond static literals, you can interact with the user by reading input from the console using `Console.ReadLine()`.
This method captures the text entered by the user as a string.
Once you have a string variable, you can access its properties, such as `.Length`, which returns the total number of characters.
You can also access specific characters within the string using square brackets and a zero-based index; for example, `[0]` retrieves the first character.
If you only need to store a single character, C# provides the `char` type, which uses single quotes instead of double quotes.

```csharp
coolString = "Goodbye, World!";

// we can "concatenate" strings
string firstName = "John";
string lastName = "Doe";
string fullName = firstName + " " + lastName;

// we can use Console.WriteLine() to print strings
Console.WriteLine(fullName);

// we can use Console.ReadLine() to read strings
// (this will be helpful for some basic programs!)
myString = Console.ReadLine();

// we can print the length of a string
Console.WriteLine(myString.Length);

// we can access individual characters in a string
Console.WriteLine(myString[0]);

// if you just wanted to declare a single character
// and assign it, it would look like:
char myChar = 'a';
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/strings-54134783/?t=415)

Special formatting can be applied to strings using additional escape sequences, such as `\n` to insert a new line.
Finally, it is critical to remember that every line of execution in C#, including variable assignments and method calls, must end with a semicolon (`;`).
This character tells the compiler where one statement ends and the next begins.

```csharp
string coolString = "Hello, World!";

// we can re-assign a value to a string variable
coolString = "Goodbye, World!";

// we can "concatenate" strings
string firstName = "John";
string lastName = "Doe\n";
string fullName = firstName + " " + lastName;

// we can use Console.WriteLine() to print strings
Console.WriteLine(fullName);

// we can use Console.ReadLine() to read strings
// (this will be helpful for some basic programs!)
myString = Console.ReadLine();

// we can print the length of a string
Console.WriteLine(myString.Length);

// we can access individual characters in a string
Console.WriteLine(myString[0]);
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/strings-54134783/?t=565)

---

## 3. Integers

> [Watch the lesson](https://dometrain.com/take/course/getting-started-csharp-2732244/integers-54134784/) · 7:33

### Summary

Integers in C# are 32-bit signed whole numbers that represent values within a specific range without decimal precision.
This lesson covers the declaration, assignment, and basic arithmetic operations of the int type, while highlighting the behavior of integer division, which truncates decimal results.
It also introduces string interpolation as a method for displaying numeric results within text output.

### Key concepts

- Integers represent whole numbers (no decimals).
- The `int` type is a 32-bit (4-byte) signed integer.
- Range: -2,147,483,648 to 2,147,483,647.
- Arithmetic operations: addition (+), subtraction (-), multiplication (*), and division (/).
- Integer division behavior: results are truncated (rounded down toward zero).
- String interpolation: using the `$` prefix to embed variables in strings.

### Lesson notes

In C#, integers are used to represent whole numbers.
An integer is a 32-bit (4-byte) value, which means it uses 32 binary "on/off" switches to represent data.
This specific size results in a fixed range of values, approximately from negative two billion to positive two billion.
Because of this data structure, integers cannot store decimal places; there are simply not enough placeholders in the 32-bit representation to account for them.

To declare an integer, use the `int` keyword followed by the variable name.
C# follows standard variable naming rules for integers.
You can declare a variable and assign it a value later, or perform both actions on a single line.

```csharp
// Integers are whole numbers
// An integer in C# is 32 bits or 4 bytes
// The range of an integer is -2,147,483,648 to 2,147,483,647

// We can declare an integer variable
int myInt;
int my_int;
int MyInt;

// We can assign a value to an integer variable
myInt = 5;

// We can declare and assign in one line
int coolInt = 5;

// We can re-assign a value to an integer variable
coolInt = 10;

// We can do math with integers
int sum = 5 + 10;
int difference = 5 - 10;
int product = 5 * 10;
int quotient = 5 / 10;
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/integers-54134784/?t=25)

Integers only accept whole numbers.
Attempting to assign a decimal value, such as `5.5`, to an `int` variable will result in a compile-time error in the IDE.
However, negative whole numbers are perfectly valid.

C# supports standard mathematical operations for integers, including addition, subtraction, multiplication, and division.
To display the results of these operations, you can use string interpolation.
By placing a dollar sign (`$`) before the opening double quote of a string, you can insert variables or expressions directly into the string using curly braces `{}`.

```csharp
// We can assign a value to an integer variable
myInt = -5;

// We can declare and assign in one line
int coolInt = 5;

// We can re-assign a value to an integer variable
coolInt = 10;

// We can do math with integers
int sum = 5 + 10;
int difference = 5 - 10;
int product = 5 * 10;
int quotient = 5 / 10;

// This is a slightly more advanced but we can see
// the results of our math with string "interpolation"
Console.WriteLine($"5 + 10={sum}");
Console.WriteLine($"5 - 10={difference}");
Console.WriteLine($"5 * 10={product}");
Console.WriteLine($"5 / 10={quotient}");

// Do we notice anything weird about the quotient?
// Why is it 0?!
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/integers-54134784/?t=235)

One critical aspect of working with integers is the behavior of division.
Because integers cannot hold decimals, the result of an integer division is always truncated (rounded down).
For example, dividing `5` by `10` results in `0` rather than `0.5` because the decimal component is removed.

```text
5 + 10=15
5 - 10=-5
5 * 10=50
5 / 10=0
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/integers-54134784/?t=325)

This truncation occurs regardless of how close the result is to the next whole number.
For instance, `15 / 10` results in `1` because the `.5` is simply trimmed off.

```csharp
// We can re-assign a value to an integer variable
coolInt = 10;

// We can do math with integers
int sum = 5 + 10;
int difference = 5 - 10;
int product = 5 * 10;
int quotient = 15 / 10;

// This is a slightly more advanced but we can see
// the results of our math with string "interpolation"
Console.WriteLine($"5 + 10={sum}");
Console.WriteLine($"5 - 10={difference}");
Console.WriteLine($"5 * 10={product}");
Console.WriteLine($"15 / 10={quotient}");

// Do we notice anything weird about the quotient?
// Why is it 0?!

// We'll need to use another type to help us here!
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/integers-54134784/?t=415)

---

## 4. Float & Double

> [Watch the lesson](https://dometrain.com/take/course/getting-started-csharp-2732244/float-double-54134785/) · 5:56

### Summary

C# provides float and double types to represent numbers with decimal places, addressing the limitations of integer types.
A float is a 32-bit single-precision type, while a double is a 64-bit double-precision type, offering greater range and resolution.
By default, the C# compiler treats decimal literals as double, requiring an f suffix for float assignments.
Unlike integer division, which truncates remainders, floating-point arithmetic preserves decimal precision, making these types essential for complex mathematical operations.

### Key concepts

- **float**: A 32-bit (4-byte) single-precision floating-point type.
- **double**: A 64-bit (8-byte) double-precision floating-point type; the default for decimal literals.
- **Literal Suffixes**: Use `f` for `float` (e.g., `5.5f`) and `d` for `double` (e.g., `5.5d`).
- **Precision**: `double` provides higher resolution and a larger range than `float`.
- **Arithmetic**: Floating-point types preserve decimal remainders during division, unlike integer types which truncate them.

### Lesson notes

C# uses floating-point types to represent values with decimal components.
The two primary types are `float` and `double`.

#### Type Characteristics and Declaration

A `float` (single-precision) occupies 32 bits of memory, while a `double` (double-precision) occupies 64 bits.
Because a `double` uses twice the memory, it provides significantly more resolution and a much larger range for representing numbers.

```csharp
// Floating point numbers are numbers with a decimal point
// A float in C# is 32 bits or 4 bytes
// The range of a float is 1.5 x 10^-45 to 3.4 x 10^38
// A double in C# is 64 bits or 8 bytes
// The range of a double is 5.0 x 10^-324 to 1.7 x 10^308

// We can declare a float variable
float myFloat;
float my_float;
float MyFloat;

// We can declare a double variable
double myDouble;
double my_double;
double MyDouble;

// We can assign a value to these variables
myFloat = 5.5f;
myDouble = 5.5;

// We can declare and assign in one line
float coolFloat = 5.5f;
double coolDouble = 5.5;

// We can re-assign a value to these variables
coolFloat = 10.5f;
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/float-double-54134785/?t=40)

#### Literals and Suffixes

In C#, any number written with a decimal point is automatically treated as a `double` by the compiler to ensure maximum resolution.
To assign a decimal literal to a `float` variable, you must append the `f` suffix.
Without this suffix, the compiler will produce an error because it cannot implicitly convert a higher-precision `double` to a lower-precision `float` without potential data loss.
While optional for `double` types, a `d` suffix can also be used for clarity.

```csharp
// We can assign a value to these variables
myFloat = 5.5f;
myDouble = 5.5d;

// We can declare and assign in one line
float coolFloat = 5.5f;
double coolDouble = 5.5;

// We can re-assign a value to these variables
coolFloat = 10.5f;
coolDouble = 10.5;

// We can do math with these numbers
float sum = 5.5f + 10.5f;
float difference = 5.5f - 10.5f;
float product = 5.5f * 10.5f;
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/float-double-54134785/?t=100)

#### Mathematical Operations

Floating-point types support standard arithmetic operators: addition (`+`), subtraction (`-`), multiplication (`*`), and division (`/`).

A critical difference between floating-point and integer arithmetic is found in division.
In integer math, any fractional remainder is truncated.
However, `float` and `double` types preserve the decimal remainder, providing high-resolution results.
For example, dividing `15.5f` by `10.5f` results in approximately `1.476`.

```csharp
// We can do math with these numbers
float sum = 5.5f + 10.5f;
float difference = 5.5f - 10.5f;
float product = 5.5f * 10.5f;
float quotient = 15.5f / 10.5f;

// The results of our math with string "interpolation"
Console.WriteLine($"5.5 + 10.5={sum}");
Console.WriteLine($"5.5 - 10.5={difference}");
Console.WriteLine($"5.5 * 10.5={product}");
Console.WriteLine($"15.5 / 10.5={quotient}");
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/float-double-54134785/?t=190)

When performing complex calculations where decimal accuracy is required, floating-point types are the appropriate choice over integers.

---

## 5. Boolean

> [Watch the lesson](https://dometrain.com/take/course/getting-started-csharp-2732244/boolean-54134786/) · 8:12

### Summary

The Boolean type (bool) is a fundamental value type in C# used to represent logical truth values: true or false.
While a single bit could theoretically store this information, C# allocates one byte (8 bits) for a Boolean variable.
Booleans are essential for implementing program logic, allowing developers to define conditions and control the execution path of an application using logical operators like AND, OR, and NOT.

### Key concepts

- The bool keyword and the true and false literals.
- Memory allocation: 1 byte (8 bits) per Boolean.
- Logical AND operator (&&): Evaluates to true only if both sides are true.
- Logical OR operator (||): Evaluates to true if at least one side is true.
- Logical NOT operator (!): Inverts the Boolean value.
- Conditional logic as the foundation for program decision-making and algorithms.

### Lesson notes

A Boolean is a basic value type that represents only two possible values: true or false.
This type is fundamental to computing and programming as it allows for the representation of logical conditions.
In C#, a Boolean is represented by an entire byte (8 bits), which is technically more than the single bit required to store a binary state.

Variables of this type are declared using the bool keyword.
Like other value types, they can be declared and assigned in separate steps or initialized on a single line.
Because a bool can only hold two values, you can use the built-in keywords true and false for assignments and reassignments.

```csharp
// A boolean is a true or false value
// A boolean in C# is 8 bits or 1 byte

// We can declare a boolean variable
bool myBool;
bool my_bool;
bool MyBool;

// We can assign a value to these variables
myBool = true;
myBool = false;

// We can declare and assign in one line
bool coolBool = true;

// We can re-assign a value to these variables
coolBool = false;

// We can do boolean logic with these variables
// && is the AND operator
bool trueAndFalse = true && false;
bool trueAndTrue = true && true;
bool falseAndFalse = false && false;

// || is the OR operator
bool trueOrFalse = true || false;
bool trueOrTrue = true || true;
bool falseOrFalse = false || false;

// ! is the NOT operator
bool notTrue = !true;
bool notFalse = !false;
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/boolean-54134786/?t=40)

#### Boolean Logic Operators

Boolean logic is critical for making decisions within a program.
By evaluating conditions, a program can take different execution paths based on whether a condition is met.
This is how rules are defined for algorithms.

##### The AND Operator (&&)

The logical AND operator is represented by a double ampersand (&&).
It evaluates two Boolean expressions and returns true only if both the left and right sides are true.
If either side (or both) is false, the result is false.

##### The OR Operator (||)

The logical OR operator is represented by two pipe symbols (||).
It returns true as long as at least one of the operands is true.
It only returns false if both sides are false.

##### The NOT Operator (!)

The logical NOT operator is represented by an exclamation mark (!).
It is a unary operator that inverts the value of the Boolean that follows it.
For example, !true evaluates to false, and !false evaluates to true.

The following code demonstrates these operators and how their results can be output to the console using string interpolation.

```csharp
// We can declare and assign in one line
bool coolBool = true;

// We can re-assign a value to these variables
coolBool = false;

// We can do boolean logic with these variables
// && is the AND operator
bool trueAndFalse = true && false;
bool trueAndTrue = true && true;
bool falseAndFalse = false && false;

// || is the OR operator
bool trueOrFalse = true || false;
bool trueOrTrue = true || true;
bool falseOrFalse = false || false;

// ! is the NOT operator
bool notTrue = !true;
bool notFalse = !false;

// The results of our boolean logic
// as we see with string interpolation:
Console.WriteLine($"true && False: {trueAndFalse}");
Console.WriteLine($"true && True: {trueAndTrue}");
Console.WriteLine($"false && False: {falseAndFalse}");
Console.WriteLine($"true || False: {trueOrFalse}");
Console.WriteLine($"true || True: {trueOrTrue}");
Console.WriteLine($"false || False: {falseOrFalse}");
Console.WriteLine($"!True: {notTrue}");
Console.WriteLine($"!False: {notFalse}");
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/boolean-54134786/?t=325)

---

## 6. DateTime

> [Watch the lesson](https://dometrain.com/take/course/getting-started-csharp-2732244/datetime-54134787/) · 5:42

### Summary

C# provides several types for handling temporal data, primarily DateTime, DateOnly, and TimeOnly.
While DateTime encapsulates both date and time information, DateOnly and TimeOnly allow for more granular control when only one component is needed.
These types follow standard variable declaration and assignment patterns, including the ability to instantiate specific values via constructors or capture the current system time using DateTime.Now.
Although date and time logic can become complex due to factors like time zones, these basic types provide the foundation for representing and combining temporal values in human-readable formats.

### Key concepts

- **DateTime**: A type representing a specific point in time (date and time combined).
- **DateOnly**: A specialized type for representing a date without a time component.
- **TimeOnly**: A specialized type for representing a time without a date component.
- **DateTime.Now**: A property used to retrieve the current local date and time.
- **Constructors**: Using the `new` keyword to initialize specific dates (year, month, day) or times (hour, minute, second).
- **Type Combination**: Creating a `DateTime` object by combining `DateOnly` and `TimeOnly` instances.

### Lesson notes

C# offers several built-in types for managing dates and times.
The `DateTime` type is the most comprehensive, representing both a date and a specific time.
In recent versions of C#, the `DateOnly` and `TimeOnly` types were introduced to allow developers to work with these components independently when a full `DateTime` object is unnecessary.

Declaring these variables follows the standard C# syntax: the type keyword followed by the variable name.
Values can be assigned using `DateTime.Now` to capture the current local system time or by using constructors to define specific points in time.
For `DateOnly`, the constructor accepts parameters for the year, month, and day.
For `TimeOnly`, the constructor accepts the hour, minute, and second, with optional parameters available for higher precision, such as milliseconds.
Variables can be reassigned after their initial declaration, just like other basic value types.

```csharp
// we can declare a DateTime variable
DateTime myDateTime;

// we can declare a DateOnly variable
DateOnly myDate;

// we can declare a TimeOnly variable
TimeOnly myTime;

// We can assign a value to these variables
myDateTime = DateTime.Now;
myDate = new DateOnly(2024, 1, 23);
myTime = new TimeOnly(1, 23, 45);

// We can declare and assign in one line
DateTime myDateTime2 = DateTime.Now;
DateOnly myDate2 = new DateOnly(2024, 1, 23);
TimeOnly myTime2 = new TimeOnly(1, 23, 45);

// We can re-assign a value to these variables
myDateTime = DateTime.Now;
myDate = new DateOnly(2024, 1, 23);
myTime = new TimeOnly(1, 23, 45);
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/datetime-54134787/?t=130)

While date and time logic can become complex—particularly when accounting for time zones across different systems—the basic types allow for straightforward manipulation.
For instance, a `DateOnly` instance and a `TimeOnly` instance can be combined to create a new `DateTime` object by passing both as arguments to the `DateTime` constructor.
This is useful when date and time data are captured separately but need to be processed as a single unit.

```csharp
// we can make a DateTime variable out of
// a DateOnly and a TimeOnly variable
DateTime dateTimeFromCombination = new DateTime(
    myDate,
    myTime);

// Let's write these to the console!
Console.WriteLine($"Date Only: {myDate}");
Console.WriteLine($"Time Only: {myTime}");
Console.WriteLine($"Date Time: {dateTimeFromCombination}");
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/datetime-54134787/?t=205)

When these types are output to the console using string interpolation, they use default formatting based on the system's locale.
The default behavior for `TimeOnly` may omit the seconds in the output, whereas the `DateTime` representation typically includes the full time component, including seconds.

```text
Date Only: 1/23/2024
Time Only: 1:23 AM
Date Time: 1/23/2024 1:23:45 AM
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/datetime-54134787/?t=235)

---

## 7. Casting & Parsing

> [Watch the lesson](https://dometrain.com/take/course/getting-started-csharp-2732244/casting-parsing-54134788/) · 8:16

### Summary

C# provides several mechanisms for converting data between different types, primarily through casting and parsing.
Casting is used for compatible numeric types, where implicit casting occurs automatically when no data loss is possible, and explicit casting is required when precision might be lost.
For incompatible types like strings and numbers, C# uses parsing methods to extract values from string representations.

### Key concepts

- Implicit Casting: Automatic conversion between compatible types where no data is lost.
- Explicit Casting: Manual conversion using the casting operator (type) when data loss (truncation) may occur.
- Truncation: The process of removing decimal places during a double to int cast.
- Parsing: Using built-in methods (e.g., int.Parse) to convert strings into other data types.
- Type Compatibility: The compiler's rules for determining which types can be directly cast.

### Lesson notes

C# provides a framework for converting variables from one data type to another.
This is a fundamental requirement when dealing with diverse data types such as integers, floating-point numbers, and strings.

#### Casting Compatible Types

Casting is the process of converting one data type to another compatible type.
There are two primary forms: implicit and explicit.

**Implicit casting** occurs when the compiler automatically converts a type with lower precision to one with higher precision.
For example, an integer can be assigned to a double without any special syntax because a double-precision floating-point number can fully represent any integer value without losing data.

**Explicit casting** is required when a conversion might result in data loss.
For instance, converting a double to an integer requires the casting operator—the target type in parentheses, such as `(int)`.
During this conversion, C# performs truncation, meaning it removes all decimal places rather than rounding.
A value of 5.5 or 5.999 cast to an integer will result in 5.

```csharp
// How do we convert between different variable types?
// We can "cast" them, which means to convert them to a different type.

// we can "implicitly" cast between types that are compatible
// for example, we can convert an int to a double
int myInt = 5;
double myDouble = myInt;
Console.WriteLine("Implicit Cast");
Console.WriteLine($"myInt={myInt}");
Console.WriteLine($"myDouble={myDouble}");

// we can also "explicitly" cast between types that are compatible
// for example, we can convert a double to an int
myDouble = 5.5;
myInt = (int)myDouble;
Console.WriteLine("Explicit Cast");
Console.WriteLine($"myInt={myInt}");
Console.WriteLine($"myDouble={myDouble}");

// we cannot "cast" when the types are not compatible
// for example, we cannot cast a string to an int
string myString = "5";
//myInt = (int)myString; // this will not compile

// we can also convert between types that are not compatible
// for example, we can convert a string to numbers
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/casting-parsing-54134788/?t=70)

#### Parsing Strings

Casting cannot be used for incompatible types, such as converting a string to an integer.
Even if a string contains a numeric character like "5", the C# compiler requires a more explicit mechanism to perform the conversion.
Attempting to use the casting operator on a string will result in a compilation error because the compiler cannot determine the intent or safety of the conversion at compile-time.

To handle these scenarios, C# uses **parsing**.
Types like `int` and `double` provide a `.Parse()` method that takes a string as input and returns the corresponding numeric value.
This allows the program to extract numerical data from text-based sources.
While the compiler is excellent at preventing invalid casts at compile-time, it cannot validate the contents of a string before a program runs; therefore, parsing errors typically occur at runtime if the string is not in a valid format for the target type.

```csharp
// we cannot "cast" when the types are not compatible
// for example, we cannot cast a string to an int
string myString = "5";
//myInt = (int)myString; // this will not compile

// we can also convert between types that are not compatible
// for example, we can convert a string to numbers
myInt = int.Parse(myString);
Console.WriteLine("Parse");
Console.WriteLine($"myString={myString}");
Console.WriteLine($"myInt={myInt}");

myString = "5.5";
myDouble = double.Parse(myString);
Console.WriteLine($"myString={myString}");
Console.WriteLine($"myDouble={myDouble}");
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/casting-parsing-54134788/?t=370)
