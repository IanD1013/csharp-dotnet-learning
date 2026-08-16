# Put It All Together!

> Course: [Getting Started: C#](https://dometrain.com/course/getting-started-csharp/) · Chapter 11
> 1 lesson · ~10:26
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Demo Program](https://dometrain.com/take/course/getting-started-csharp-2732244/demo-program-54135016/) | 10:26 | [↓](#1-demo-program) |

---

## 1. Demo Program

> [Watch the lesson](https://dometrain.com/take/course/getting-started-csharp-2732244/demo-program-54135016/) · 10:26

### Summary

This lesson demonstrates a practical application of C# fundamentals by building a console-based calculator.
The program utilizes a class-based structure where a Start method manages a continuous execution loop.
Key implementation details include using a dictionary to map and validate mathematical operators, employing int.TryParse for robust user input handling, and implementing a switch expression within a try-catch block to perform calculations and handle potential runtime errors like division by zero.

### Key concepts

* Class instantiation and constructor-based configuration.
* Infinite loops for interactive console applications.
* Input validation using Dictionary.TryGetValue and int.TryParse.
* Flow control using the continue keyword to handle invalid states.
* Pattern matching with switch expressions to return values.
* Exception handling for specific (DivideByZeroException) and general (Exception) scenarios.

### Lesson notes

The program begins by instantiating the NicksCoolCalculator class, passing a custom greeting string to the constructor.
This greeting is stored in a private field and displayed when the calculator starts.

```csharp
NicksCoolCalculator calculator = new(
    "Welcome to Nick's Cool Calculator!");
calculator.Start();
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/demo-program-54135016/?t=40)

The Start method contains the core logic.
It initializes a Dictionary<string, string> called supportedOperators to map mathematical symbols (keys) to their descriptive names (values).
This dictionary serves two purposes: it provides the list of available operations to the user and validates the user's operator choice.

```csharp
public sealed class NicksCoolCalculator
{
    private string _greeting;

    public NicksCoolCalculator(string greeting)
    {
        _greeting = greeting;
    }

    public void Start()
    {
        Console.WriteLine(_greeting);

        Dictionary<string, string> supportedOperators = new()
        {
            { "+", "Add" },
            { "/", "Divide" }
        };

        while (true)
        {
            Console.WriteLine("Operator choices are as follows:");
            foreach (var op in supportedOperators)
            {
                Console.WriteLine($"{op.Value}: {op.Key}");
            }

            Console.WriteLine("Enter an operator:");
            string operatorChoice = Console.ReadLine();

            if (!supportedOperators.TryGetValue(
                operatorChoice,
                out var selectedOperatorDescription))
            {
                Console.WriteLine("Invalid operator choice.");
                continue;
            }

            Console.WriteLine($"You selected: {selectedOperatorDescription}");
            Console.WriteLine();
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/demo-program-54135016/?t=160)

The program enters a while (true) loop to allow multiple calculations.
Inside the loop, it prompts the user for an operator.
If the input does not exist in the supportedOperators dictionary, the program informs the user and uses the continue keyword to jump back to the start of the loop.

Once a valid operator is selected, the program requests two integers.
Because Console.ReadLine() returns a string, int.TryParse is used to convert the input.
If parsing fails, the program notifies the user and restarts the loop.

```csharp
        Console.WriteLine(
            $"Recall that integers are on the range " +
            $"{int.MinValue} to {int.MaxValue}!");
        Console.WriteLine();

        Console.WriteLine("Enter the first integer:");
        string firstNumberInput = Console.ReadLine();
        if (!int.TryParse(firstNumberInput, out int firstNumber))
        {
            Console.WriteLine(
                $"{firstNumberInput} could not be parsed as an integer!");
            continue;
        }

        Console.WriteLine("Enter the second integer:");
        string secondNumberInput = Console.ReadLine();
        if (!int.TryParse(secondNumberInput, out int secondNumber))
        {
            Console.WriteLine($"{secondNumberInput} could not be parsed as an integer.");
            continue;
        }
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/demo-program-54135016/?t=205)

The calculation logic is wrapped in a try block.
A switch expression evaluates the operatorChoice and performs the corresponding arithmetic operation.
If an unsupported operator somehow reaches this point, a NotSupportedException is thrown using the discard (_) pattern.

The program handles specific exceptions, such as DivideByZeroException, which occurs if a user attempts to divide an integer by zero.
A general catch (Exception ex) block is also included to handle any other unforeseen errors, ensuring the application does not crash.
In both error cases, the continue keyword is used to return to the top of the loop.

```csharp
        int result;
        try
        {
            result = operatorChoice switch
            {
                "+" => firstNumber + secondNumber,
                "/" => firstNumber / secondNumber,
                _ => throw new NotSupportedException(
                    $"Arithmetic is not currently supported " +
                    $"for operator {operatorChoice}.")
            };
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("You cannot divide by zero.");
            continue;
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"There was an unhandled exception: {ex.Message}");
            continue;
        }

        Console.WriteLine($"The result is: {result}");
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/demo-program-54135016/?t=385)

After a successful calculation, the result is printed to the console, and the loop restarts, prompting the user for a new operation.
