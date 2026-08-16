# Control Flow

> Course: [Getting Started: C#](https://dometrain.com/course/getting-started-csharp/) · Chapter 5
> 3 lessons · ~30:32
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [If Statements](https://dometrain.com/take/course/getting-started-csharp-2732244/if-statements-54134803/) | 15:45 | [↓](#1-if-statements) |
| 2 | [Ternary Operators](https://dometrain.com/take/course/getting-started-csharp-2732244/ternary-operators-54134804/) | 4:41 | [↓](#2-ternary-operators) |
| 3 | [Switch Statements and Expressions](https://dometrain.com/take/course/getting-started-csharp-2732244/switch-statements-and-expressions-54134805/) | 10:06 | [↓](#3-switch-statements-and-expressions) |

---

## 1. If Statements

> [Watch the lesson](https://dometrain.com/take/course/getting-started-csharp-2732244/if-statements-54134803/) · 15:45

### Summary

Control flow enables a program to execute different logic paths based on specific conditions, transforming a static sequence of instructions into a dynamic decision-making process.
The if statement is the primary mechanism for this, evaluating boolean expressions to determine which block of code, enclosed in curly braces, should be executed.

### Key concepts

- **If Statements**: The fundamental building block for conditional logic.
- **Conditions**: Boolean expressions evaluated within parentheses to determine execution.
- **Code Blocks**: Groups of statements enclosed in curly braces `{}` that execute together.
- **Else and Else If**: Keywords used to define alternative paths when the initial condition is false.
- **Comparison Operators**: Symbols used to compare values, such as `==`, `!=`, `<`, and `>`.
- **Logical Operators**: `&&` (And) and `||` (Or) used to combine multiple conditions.

### Lesson notes

Control flow allows us to set rules for the computer, directing it to take different paths based on whether certain conditions are true or false.
The most basic form of control flow in C# is the `if` statement.
It consists of the `if` keyword, a condition inside parentheses, and a block of code inside curly braces.

```csharp
// the most basic type of control flow that we
// have is called the "if-statement". It allows us
// to execute a block of code if a certain condition
// is true. If the condition is false, the block of
// code is skipped.

// the expression inside of the parentheses
// is called the "condition"
if (true)
{
    Console.WriteLine("This will always print");
}

if (false)
{
    Console.WriteLine("This will never print");
}

// we can use variables as the condition to check
bool condition = true;
if (condition)
{
    Console.WriteLine("This prints when the condition is true!");
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/if-statements-54134803/?t=85)

The condition inside the parentheses must evaluate to a boolean value.
You can use boolean literals, boolean variables, or expressions that result in a boolean.
The logical "not" operator (`!`) can also be used to invert a condition.

```csharp
if (!false)
{
    Console.WriteLine("This will print because !false is true");
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/if-statements-54134803/?t=205)

To handle the scenario where a condition is not met, C# provides the `else` keyword.
The code inside the `else` block executes only if the preceding `if` condition evaluates to false.

```csharp
if (condition)
{
    Console.WriteLine("This prints when the condition is true!");
}
else
{
    Console.WriteLine("This prints when the condition is false!");
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/if-statements-54134803/?t=295)

For more complex logic involving multiple distinct possibilities, you can use `else if`.
This allows you to check a second condition if the first one was false.
Note that in a chain of `if`, `else if`, and `else`, only the first block whose condition is true will execute.
If you use a boolean variable to check `if (condition)` and `else if (!condition)`, a final `else` block becomes unreachable because a boolean has no third state.

```csharp
if (condition)
{
    Console.WriteLine("This will print when condition is true");
}
else if (!condition)
{
    Console.WriteLine("This will print when condition is false");
}
else
{
    // This block can never run because a boolean is always true or false
    Console.WriteLine("Trick question?!");
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/if-statements-54134803/?t=355)

While booleans are the direct input for conditions, we often generate those booleans by comparing other data types, such as integers or floating-point numbers.
C# uses standard mathematical symbols for these comparisons:
- `<` (Less than)
- `>` (Greater than)
- `<=` (Less than or equal to)
- `>=` (Greater than or equal to)
- `==` (Equal to)
- `!=` (Not equal to)

Note that `==` is used for comparison, whereas a single `=` is used for variable assignment.

```csharp
int number = 1;
if (number < 5)
{
    Console.WriteLine("The number is less than 5");
}
else if (number == 5)
{
    Console.WriteLine("The number is equal to 5");
}
else
{
    Console.WriteLine("The number is greater than 5");
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/if-statements-54134803/?t=475)

Finally, conditions can be combined using logical operators.
The `&&` (And) operator requires both sides of the expression to be true for the entire condition to be true.
The `||` (Or) operator requires at least one side to be true.
These are useful for checking if a value falls within a specific range.

```csharp
number = 3;
// Check if number is between 1 and 5 inclusive
if (number >= 1 && number <= 5)
{
    Console.WriteLine("The number is between 1 and 5");
}
else
{
    Console.WriteLine("The number is not between 1 and 5");
}

// Check if number is outside the range of 1 to 5
if (number < 1 || number > 5)
{
    Console.WriteLine("The number is not between 1 and 5");
}
else
{
    Console.WriteLine("The number is between 1 and 5");
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/if-statements-54134803/?t=730)

---

## 2. Ternary Operators

> [Watch the lesson](https://dometrain.com/take/course/getting-started-csharp-2732244/ternary-operators-54134804/) · 4:41

### Summary

The ternary operator in C# is a concise alternative to the if-else statement, specifically used for conditional value assignment.
It evaluates a boolean condition and returns one of two expressions depending on whether the condition is true or false, allowing developers to write more compact code for simple logic.

### Key concepts

* **Conditional Assignment**: Streamlines the process of assigning a value to a variable based on a condition.
* **Syntax**: Uses the `?` and `:` symbols in the format `condition ? trueExpression : falseExpression`.
* **Boolean Condition**: The first part of the operator must be an expression that evaluates to a boolean (`true` or `false`).
* **Type Consistency**: Both the true and false expressions must return values that are compatible with the variable receiving the assignment.
* **Conciseness**: Reduces the boilerplate code required for simple `if-else` blocks.

### Lesson notes

The ternary operator serves as a shorthand for `if-else` logic when the goal is to assign a value to a variable.
Instead of using multiple lines to branch execution, the ternary operator evaluates a condition and selects a value in a single expression.

The basic syntax and usage are demonstrated below:

```csharp
// ternary operators are used to assign a
// value to a variable based on a condition
// The syntax is:
// variable = (condition) ? expressionTrue : expressionFalse;

int x = 10;
string result = x > 5
    ? "x is greater than 5"
    : "x is less than 5";
Console.WriteLine(result);

result = x == 10
    ? "x is equal to 10"
    : "x is not equal to 10";
Console.WriteLine(result);

result = x < 20
    ? "x is less than 20"
    : "x is greater than 20";
Console.WriteLine(result);
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/ternary-operators-54134804/?t=25)

In this structure, the condition is evaluated first.
If the condition evaluates to `true`, the value immediately following the question mark (`?`) is selected.
If it evaluates to `false`, the value following the colon (`:`) is selected.

In the first assignment above, `x > 5` evaluates to `true` (since 10 > 5), so the string "x is greater than 5" is assigned to `result`.
In the second assignment, `x == 10` is `true`, so the first string is chosen.
In the third, `x < 20` is `true`, so "x is less than 20" is assigned.

If the value of `x` is changed to 5, the logic branches differently:

```csharp
// ternary operators are used to assign a
// value to a variable based on a condition
// The syntax is:
// variable = (condition) ? expressionTrue : expressionFalse;

int x = 5;
string result = x > 5
    ? "x is greater than 5"
    : "x is less than 5";
Console.WriteLine(result);

result = x == 10
    ? "x is equal to 10"
    : "x is not equal to 10";
Console.WriteLine(result);

result = x < 20
    ? "x is less than 20"
    : "x is greater than 20";
Console.WriteLine(result);
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/ternary-operators-54134804/?t=150)

With `x = 5`, the condition `x > 5` is now `false`.
Consequently, the operator skips the first expression and assigns the value after the colon: "x is less than 5".
Similarly, `x == 10` is `false`, resulting in the assignment of "x is not equal to 10".

Ternary operators are not restricted to strings; they can be used with any data type, such as integers or custom objects, as long as the types are consistent across the assignment.
The operator requires a boolean expression as the condition, and the expressions for both the true and false cases must return types compatible with the target variable.

While ternary operators are more concise than `if-else` statements, they are a stylistic choice.
They are best used for simple, single-line assignments to keep code readable.
If a condition or the resulting expressions are complex, a standard `if-else` block may be more appropriate for clarity.

---

## 3. Switch Statements and Expressions

> [Watch the lesson](https://dometrain.com/take/course/getting-started-csharp-2732244/switch-statements-and-expressions-54134805/) · 10:06

### Summary

This lesson introduces switch statements and switch expressions as alternatives to complex if-else chains for managing control flow in C#.
It covers the syntax of the switch statement, including the use of cases, the break keyword, and the default case, while also demonstrating the more compact switch expression which uses arrow syntax and the discard pattern to assign values to variables.

### Key concepts

- Switch statements for selecting specific code blocks to execute.
- Switch expressions for selecting values to assign to variables.
- The `case` keyword and the requirement for constant values.
- Control flow management using `break` and `return` keywords.
- Case stacking (fall-through) for executing shared logic across multiple values.
- The `default` case in switch statements and the discard pattern (`_`) in switch expressions.

### Lesson notes

Switch statements provide a structured way to select a block of code for execution based on the value of an expression.
Unlike `if-else` chains, which can become difficult to navigate as logic grows, a switch statement clearly maps specific constant values to corresponding logic blocks.

```csharp
int dayOfWeek = 4;
switch (dayOfWeek)
{
    case 1:
        Console.WriteLine("Monday");
        break;
    case 2:
        Console.WriteLine("Tuesday");
        break;
    case 3:
        Console.WriteLine("Wednesday");
        break;
    case 4:
        Console.WriteLine("Thursday");
        break;
    case 5:
        Console.WriteLine("Friday");
        break;
    case 6:
        Console.WriteLine("Saturday");
        break;
    case 7:
        Console.WriteLine("Sunday");
        break;
    default:
        Console.WriteLine("Invalid day");
        break;
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/switch-statements-and-expressions-54134805/?t=70)

In a switch statement, the program evaluates the expression in the parentheses and jumps to the matching `case`.
The `break` keyword is required to exit the switch block once a case is completed.
If no case matches, the optional `default` block executes, serving a similar purpose to an `else` block.

The expression inside the `switch` parentheses can be any valid expression (e.g., `switch (7 + 1)`), but the values following the `case` keyword must be constant values known at compile time.
This means you cannot use values that must be evaluated at runtime, such as the length of a dynamic string.

C# allows "fall-through" behavior by stacking multiple cases.
This is useful when several values should trigger the same logic.

```csharp
int dayOfWeek = 4;
switch (dayOfWeek)
{
    case 1:
    case 2:
    case 3:
    case 4:
    case 5:
        Console.WriteLine("Week Day");
        break;
    case 6:
    case 7:
        Console.WriteLine("Weekend");
        break;
    default:
        Console.WriteLine("Invalid day");
        break;
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/switch-statements-and-expressions-54134805/?t=285)

While `break` exits the switch statement and continues execution on the next line, the `return` keyword can be used to exit the entire containing method or block of code immediately, bypassing any code following the switch statement.

```csharp
int dayOfWeek = 4;
switch (dayOfWeek)
{
    case 1:
    case 2:
    case 3:
    case 4:
    case 5:
        Console.WriteLine("Week Day");
        return;
    case 6:
    case 7:
        Console.WriteLine("Weekend");
        break;
    default:
        Console.WriteLine("Invalid day");
        break;
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/switch-statements-and-expressions-54134805/?t=325)

Switch expressions provide a more concise syntax for selecting a value to assign to a variable, similar to how a ternary operator functions.
In a switch expression, the variable being evaluated precedes the `switch` keyword, and the logic uses arrow syntax (`=>`) to map values to results.

```csharp
string dayOfWeekName = "Thursday";
string result = dayOfWeekName switch
{
    "Monday" => "First day of the week",
    "Tuesday" => "Second day of the week",
    "Wednesday" => "Third day of the week",
    "Thursday" => "Fourth day of the week",
    "Friday" => "Fifth day of the week",
    "Saturday" => "Sixth day of the week",
    "Sunday" => "Seventh day of the week",
    _ => "Invalid day"
};
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/switch-statements-and-expressions-54134805/?t=490)

In switch expressions, the underscore (`_`) represents the discard pattern, which acts as the default case for any values not explicitly handled.
Unlike switch statements, switch expressions do not require `break` or `return` keywords within the arms of the expression.
