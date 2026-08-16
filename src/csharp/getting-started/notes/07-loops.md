# Loops

> Course: [Getting Started: C#](https://dometrain.com/course/getting-started-csharp/) · Chapter 7
> 3 lessons · ~30:20
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [While Loops](https://dometrain.com/take/course/getting-started-csharp-2732244/while-loops-54134880/) | 13:59 | [↓](#1-while-loops) |
| 2 | [For Loops](https://dometrain.com/take/course/getting-started-csharp-2732244/for-loops-54134881/) | 6:00 | [↓](#2-for-loops) |
| 3 | [Foreach Loops](https://dometrain.com/take/course/getting-started-csharp-2732244/foreach-loops-54134882/) | 10:21 | [↓](#3-foreach-loops) |

---

## 1. While Loops

> [Watch the lesson](https://dometrain.com/take/course/getting-started-csharp-2732244/while-loops-54134880/) · 13:59

### Summary

C# provides while and do-while loops to execute code blocks repeatedly based on boolean conditions.
While both serve similar purposes, the while loop evaluates its condition before the first execution, whereas the do-while loop ensures the body runs at least once by evaluating the condition at the end of the iteration.
Control flow within loops can be further managed using the break keyword to exit a loop early and the continue keyword to skip the remainder of the current iteration.

### Key concepts

* **while loop**: A pre-condition loop that executes as long as a specified condition remains true.
* **do-while loop**: A post-condition loop that executes the body once before checking the condition.
* **Increment Operator (`++`)**: A shorthand syntax for increasing an integer value by one (e.g., `count++` is equivalent to `count = count + 1`).
* **break**: A control flow keyword that immediately terminates the loop and moves execution to the first line after the loop block.
* **continue**: A control flow keyword that skips the remaining code in the current iteration and jumps immediately to the next condition check.

### Lesson notes

#### The while Loop

A `while` loop evaluates a boolean condition before executing the code inside its block.
If the condition is true, the code inside the curly braces runs.
Once the end of the block is reached, execution returns to the `while` statement to re-evaluate the condition.
This process repeats until the condition evaluates to false.

```csharp
// here is a while loop that counts to 5
int count = 0;
while (count < 5)
{
    Console.WriteLine(count);
    count++;
}

Console.WriteLine($"The total count is {count}!");
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/while-loops-54134880/?t=55)

In the example above, the variable `count` is incremented using the `++` operator.
This is functionally identical to `count = count + 1`.
The loop runs five times (for values 0, 1, 2, 3, and 4).
When `count` reaches 5, the condition `count < 5` becomes false, and the loop terminates.

#### The do-while Loop

The `do-while` loop is similar to the `while` loop, but the condition is checked at the end of the loop body.
This guarantees that the code inside the `do` block will execute at least once, regardless of whether the condition is initially true or false.

```csharp
// here is a do while loop that counts to 5
count = 0;
do
{
    Console.WriteLine(count);
    count++;
} while (count < 5);
Console.WriteLine($"The total count is {count}!");
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/while-loops-54134880/?t=325)

#### Comparison of Pre-condition and Post-condition Loops

The difference in behavior becomes clear when the loop condition is false from the start.
In a `while` loop with a false condition, the body never executes.
In a `do-while` loop with the same false condition, the body executes once before the loop terminates.

```csharp
// what happens if we change the condition to false
// for a while loop?
count = 0;
while (count > 5)
{
    Console.WriteLine(count);
    count++;
}
Console.WriteLine($"The total count is {count}!");

// what happens if we change the condition to false
// for a do while loop?
count = 0;
do
{
    Console.WriteLine(count);
    count++;
} while (count > 5);
Console.WriteLine($"The total count is {count}!");
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/while-loops-54134880/?t=370)

In this scenario, the `while` loop prints only the final count of 0.
The `do-while` loop prints 0 inside the loop, increments the count to 1, and then prints a final count of 1.

#### Loop Control: break and continue

Complex logic can be implemented within loops by combining them with conditional `if` statements and the `break` and `continue` keywords.

*   `continue` skips the current iteration. In the example below, when `count` is 3, the program increments the count and hits `continue`, skipping the `Console.WriteLine(count)` call for that specific iteration.
*   `break` exits the loop entirely. In the example below, when `count` reaches 5, the `break` statement is triggered, exiting the loop even if the `while` condition (`count < 50`) is still true.

```csharp
// Let's add a condition to the while loop
// so we can see the behavior of
// break and continue
count = 0;
while (count < 50)
{
    if (count == 3)
    {
        count++;
        Console.WriteLine("I'm skipping 3!");
        continue;
    }

    Console.WriteLine(count);
    count++;

    if (count == 5)
    {
        Console.WriteLine("I'm out of here!");
        break;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/while-loops-54134880/?t=775)

---

## 2. For Loops

> [Watch the lesson](https://dometrain.com/take/course/getting-started-csharp-2732244/for-loops-54134881/) · 6:00

### Summary

The for loop in C# is a specialized control structure designed for scenarios requiring a specific number of iterations, such as counting.
It consolidates the initialization of a counter, the loop's termination condition, and the iteration expression into a single line, offering a more readable and compact alternative to counting while loops.
By localizing the loop variable's scope and providing built-in support for break and continue keywords, the for loop simplifies the implementation of repetitive logic while maintaining precise control over execution flow.

### Key concepts

- **Consolidated Loop Header**: Combines initialization, condition, and iteration in one line.
- **Initializer**: Executes once at the start to set up the loop variable (e.g., `int i = 0`).
- **Condition**: Evaluated before each iteration; the loop runs as long as this is `true`.
- **Iterator**: Executes after each loop body, typically used to increment or decrement the counter.
- **Block Scoping**: Variables declared in the initializer are only accessible within the loop's body.
- **Control Flow**: Supports `break` to exit the loop and `continue` to jump to the next iteration.

### Lesson notes

The `for` loop is specifically designed to handle counting logic more efficiently than standard `while` or `do-while` loops.
While those loops require manual counter management, the `for` loop compresses this logic into a single line.

```csharp
// a for loop is a loop that runs a specific number of times
// we saw how to count with while loops...
// but a for loop is designed to count!

// here is the syntax for a for loop:
for (int i = 0; i < 10; i++)
{
    Console.WriteLine(i);
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/for-loops-54134881/?t=40)

A `for` loop consists of three primary components in its header, separated by semicolons:
1. **The Initializer**: `int i = 0;` - This part sets the starting value for the counter. While `i` is the conventional name for this variable, any valid identifier (such as `counter`) can be used.
2. **The Condition**: `i < 10;` - This boolean expression is checked before each iteration. If it is `true`, the loop body executes; if `false`, the loop terminates. For example, once `i` reaches 10, the check `10 < 10` fails, and the program exits the loop.
3. **The Iterator**: `i++` - This expression runs after the loop body completes. It updates the counter before the next condition check.

The execution flow follows a specific order: the initializer runs once, the condition is checked, the body executes, the iterator runs, and then the condition is checked again.
This cycle continues until the condition evaluates to `false`.

#### Variable Scoping

Variables declared within the `for` loop's initializer are scoped to the loop itself.
This means they are not accessible outside the curly braces of the loop.
This scoping allows developers to use the same variable name in multiple loops within the same method without causing naming conflicts.

```csharp
// note that we can't access i outside of the for loop!
i = 123; // this will not work!
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/for-loops-54134881/?t=220)

#### Loop Control: Break and Continue

`for` loops support the same control flow keywords as other loop types.
The `break` keyword is used to exit the loop immediately, regardless of the condition.
While using `break` to exit when a counter reaches a certain value is possible, it is often more idiomatic to simply adjust the loop's condition.

The `continue` keyword skips the remaining code in the current iteration's body and moves directly to the iterator and the next condition check.

```csharp
// we can use break and continue in a for loop as well,
// just like we did with a while loop.

// here's an example of a for loop with a break:
for (int i = 0; i < 10; i++)
{
    if (i == 5)
    {
        Console.WriteLine("We're outta here!");
        break;
    }

    Console.WriteLine(i);
}

// here's an example of a for loop with a continue:
for (int i = 0; i < 10; i++)
{
    if (i == 5)
    {
        Console.WriteLine("Skipping 5!");
        continue;
    }

    Console.WriteLine(i);
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/for-loops-54134881/?t=295)

Although most commonly used for counting upward by one, `for` loops are highly flexible.
By adjusting the initializer, condition, and iterator, you can count downward or iterate in different increments.

---

## 3. Foreach Loops

> [Watch the lesson](https://dometrain.com/take/course/getting-started-csharp-2732244/foreach-loops-54134882/) · 10:21

### Summary

The foreach loop in C# is a specialized iteration construct designed specifically for traversing collections.
While while and do-while loops are open-ended and for loops are optimized for counting, the foreach loop is purpose-built to iterate over any collection that implements the IEnumerable interface, such as arrays, lists, and dictionaries.
It provides a clean syntax that automatically manages the iteration state, exposing each element through a local variable for the duration of the loop body.

### Key concepts

- Iterating over collections (Arrays, Lists, Dictionaries).
- The IEnumerable interface.
- Syntax: foreach (Type variableName in collection).
- Type inference with the var keyword.
- Iterating over KeyValuePair<TKey, TValue> in dictionaries.
- Control flow using break and continue.

### Lesson notes

The foreach loop is used to iterate over a collection of values.
This includes arrays, lists, and dictionaries, as well as any other type that implements the IEnumerable interface.
IEnumerable allows the loop to step through a collection one element at a time.

The syntax for a foreach loop requires the type of the elements, a variable name for the current element, the in keyword, and the collection to be iterated.
In an array of integers, the type would be int.
In a list of strings, the type would be string.

```csharp
// let's see a real example with a number array!
int[] numbers = { 1, 2, 3, 4, 5 };
foreach (int number in numbers)
{
    Console.WriteLine(number);
}

// what about with... a list of strings?
List<string> words = new List<string>
{
    "red",
    "green",
    "blue"
};
foreach (string word in words)
{
    Console.WriteLine(word);
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/foreach-loops-54134882/?t=55)

In the array example, the loop takes the first element (1), assigns it to the variable number, and executes the body.
It then proceeds to 2, 3, and so on, until the end of the array is reached.
The loop finishes automatically when there are no more elements.

Iterating over a dictionary is slightly different because dictionaries consist of key-value pairs.
When stepping through a dictionary, each element is of type KeyValuePair<string, int>.
This allows access to both the Key and the Value properties of each entry.

```csharp
Dictionary<string, int> ages = new()
{
    { "Alice", 25 },
    { "Bob", 24 },
    { "Charlie", 26 },
};

foreach (KeyValuePair<string, int> person in ages)
{
    Console.WriteLine($"{person.Key} is {person.Value} years old");
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/foreach-loops-54134882/?t=325)

To make the code more concise, the var keyword can be used for type inference.
The compiler determines the type of the variable at compile time based on the collection.
For a Dictionary<string, int>, the compiler infers the type as KeyValuePair<string, int>.
This is a matter of personal or team preference regarding readability.

```csharp
// Using var for type inference
foreach (var person in ages)
{
    Console.WriteLine($"{person.Key} is {person.Value} years old");
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/foreach-loops-54134882/?t=280)

The foreach loop also supports control flow statements like break and continue.
The break statement is used to exit the loop prematurely when a certain condition is met.

```csharp
foreach (int number in numbers)
{
    if (number == 3)
    {
        break;
    }

    Console.WriteLine(number);
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/foreach-loops-54134882/?t=520)

In this example, the loop iterates through the numbers array.
When it encounters the number 3, the if condition evaluates to true, and the break statement executes, terminating the loop.
As a result, only 1 and 2 are printed to the console.
The continue statement can similarly be used to skip the remaining code in the current iteration and move to the next element.
