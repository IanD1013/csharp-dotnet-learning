# Parallel LINQ

> Course: [From Zero to Hero: Parallel Programming in C#](https://dometrain.com/course/from-zero-to-hero-parallel-programming-in-csharp/) · Chapter 7
> 2 lessons · ~17:02
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Introduction to PLINQ](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-plinq-69955988/) | 8:52 | [↓](#1-introduction-to-plinq) |
| 2 | [Using PLINQ in Code](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/using-plinq-in-code-69955989/) | 8:10 | [↓](#2-using-plinq-in-code) |

---

## 1. Introduction to PLINQ

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-plinq-69955988/) · 8:52

### Summary

Parallel LINQ (PLINQ) is a parallel implementation of LINQ to Objects that allows standard LINQ queries to be executed across multiple processors.
By using the AsParallel extension method, the .NET engine can partition the data source and execute query operations concurrently.
PLINQ includes a sophisticated optimization engine that determines whether parallelization will actually provide a performance benefit, taking into account the overhead of thread management and resource allocation.
It also provides granular control over ordering, concurrency limits, and buffering strategies to ensure efficient execution in various application scenarios.

### Key concepts

*   **AsParallel**: The entry point that converts an `IEnumerable<T>` into a `ParallelQuery<T>`, enabling parallel execution.
*   **Optimization Engine**: PLINQ analyzes queries and may choose to run them sequentially if the overhead of parallelization (thread creation, memory allocation) exceeds the performance gains.
*   **AsSequential**: Reverts a parallel query back to a sequential `IEnumerable<T>`.
*   **AsOrdered / AsUnordered**: Controls whether the original sequence order is preserved in the results, which is important because parallel threads return results non-deterministically.
*   **WithDegreeOfParallelism**: Limits the maximum number of concurrent threads to prevent resource exhaustion.
*   **WithMergeOptions**: Configures how results are buffered and yielded to the consumer.
*   **WithExecutionMode**: Allows developers to force parallel execution even when the engine would normally default to sequential.

### Lesson notes

Standard LINQ queries operate sequentially, functioning essentially as highly optimized `foreach` loops.
When a query like `numbers.Where(x => x % 2 == 0)` is executed, .NET iterates through the collection item by item to test the predicate.
While .NET engineers have optimized these sequential operations over many years, they remain single-threaded by default.

#### Enabling Parallelism

To enable parallel execution, use the `AsParallel()` extension method.
This allows .NET to examine the operation and determine the most efficient way to execute it across available CPU resources.

```csharp
// Get Even Numbers
var evenNumbers = numbers.Where(x => x % 2 == 0).ToList();

// Get Even Numbers in Parallel
var evenNumbersInParallel = numbers.AsParallel().Where(x => x % 2 == 0).ToList();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-plinq-69955988/?t=25)

It is important to note that `AsParallel()` is a request, not a guarantee.
The PLINQ engine analyzes the collection size and the complexity of the operation.
If the collection is small (e.g., only a few items), the overhead of spinning up new threads and assigning memory on the heap may outweigh the benefits of parallelism.
In such cases, .NET may still choose to run the query sequentially to ensure the least amount of overhead.

#### Controlling Sequence and Ordering

If you need to revert a parallel query to a sequential one later in your code, you can use the `AsSequential()` method.

```csharp
// Get Even Numbers
ParallelQuery<int> evenNumbersInParallel = numbers.AsParallel().Where(x => x % 2 == 0);

// Get Even Numbers Sequentially
IEnumerable<int> evenNumbers = evenNumbersInParallel.AsSequential();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-plinq-69955988/?t=205)

Because parallel operations run on different threads with varying priorities, results are typically returned in a random order.
To ensure the output maintains the same order as the source sequence, use `AsOrdered()`.
Conversely, `AsUnordered()` can be used to remove ordering constraints from a query that was previously marked as ordered.

```csharp
// Get Even Numbers in Parallel in Order
ParallelQuery<int> evenNumbersInParallel = numbers.AsParallel().AsOrdered().Where(x => x % 2 == 0 );

// Get Even Numbers in Parallel without Preserving Order
ParallelQuery<int> unorderedNumbersInParallel = evenNumbersInParallel.AsUnordered();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-plinq-69955988/?t=235)

#### Concurrency and Buffering

To prevent thread pool exhaustion and ensure other parts of the application are not starved of resources, you can set a maximum degree of parallelism.

```csharp
// Get Even Numbers in Parallel
var evenNumbersInParallel = numbers.AsParallel().WithDegreeOfParallelism(5).Where(x => x % 2 == 0);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-plinq-69955988/?t=310)

Buffering behavior can be controlled via `WithMergeOptions`.
The `AutoBuffered` (default) option allows the system to choose an optimized buffer size.
`NoBuffered` returns results as soon as they are processed, while `FullyBuffered` waits for the entire query to complete before returning any results.

```csharp
// Get Even Numbers in Parallel
var evenNumbersInParallel = numbers.AsParallel()
                            .WithMergeOptions(ParallelMergeOptions.FullyBuffered)
                            .Where(x => x % 2 == 0);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-plinq-69955988/?t=355)

#### Execution Mode

If you have benchmarked your code and determined that parallel execution is faster despite the PLINQ engine's decision to run sequentially, you can force parallelism using `WithExecutionMode`.

```csharp
// Get Even Numbers in Parallel
var evenNumbersInParallel = numbers.AsParallel()
                                   .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                                   .Where(x => x % 2 == 0);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-plinq-69955988/?t=430)

#### Implementation Example: Finding Prime Numbers

The following example demonstrates a practical application of PLINQ to find prime numbers, utilizing `AsParallel`, `AsOrdered`, and cancellation tokens for robust execution.

```csharp
using System.Diagnostics;

IReadOnlyList<int> iterationsList = [.. Enumerable.Range(1, 10_000)];

Trace.WriteLine("***Finding prime numbers in series using LINQ...");
var seriesStopwatch = new Stopwatch();
seriesStopwatch.Start();

var primeNumbers = iterationsList.Select(FindNextPrimeNumber).ToList();
seriesStopwatch.Stop();

Trace.WriteLine($"***Finding prime numbers in series took {seriesStopwatch.Elapsed.TotalSeconds:F3}s.");

Trace.WriteLine("***Finding prime in Parallel using PLINQ...");
var parallelStopwatch = new Stopwatch();
parallelStopwatch.Start();

var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
var primeNumbersInParallel = iterationsList
										.AsParallel()
										.AsOrdered()
										.WithCancellation(cts.Token)
										.Select(FindNextPrimeNumber).ToList();

parallelStopwatch.Stop();

Trace.WriteLine($"***Finding prime numbers in parallel took {parallelStopwatch.Elapsed.TotalSeconds:F3}s.");

foreach (var primeNumber in primeNumbersInParallel)
{
	Trace.Write($"{primeNumber}, ");
}

static long FindNextPrimeNumber(int n)
{
	int count = 0;
	long a = 2;
	while (count < n)
	{
		long b = 2;
		int prime = 1;
		while (b * b <= a)
		{
			if (a % b is 0)
			{
				prime = 0;
				break;
			}

			b++;
		}

		if (prime > 0)
		{
			count++;
		}

		a++;
	}

	return (--a);
}
```

---

## 2. Using PLINQ in Code

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/using-plinq-in-code-69955989/) · 8:10

### Summary

This lesson demonstrates the practical implementation of Parallel LINQ (PLINQ) to optimize computationally intensive tasks.
By comparing a sequential LINQ query against a parallelized version using a prime number calculation, the lesson illustrates how to use `AsParallel` to distribute work across CPU cores, `AsOrdered` to maintain sequence integrity, and `WithCancellation` to manage execution timeouts.
The results show a significant reduction in execution time and more efficient utilization of hardware resources.

### Key concepts

- Converting LINQ to PLINQ using `AsParallel()`.
- Preserving sequence order with `AsOrdered()`.
- Handling timeouts and cancellation with `WithCancellation()`.
- Deferred execution in PLINQ.
- Performance benchmarking considerations.
- CPU utilization patterns in sequential vs. parallel processing.

### Lesson notes

The lesson begins by establishing a test environment to compare sequential and parallel processing.
A list of 10,000 integers is generated, and a computationally expensive method, `FindNextPrimeNumber`, is defined.
This method uses nested loops to find the next prime number for a given input, resulting in an $O(n^2)$ complexity that serves as an ideal candidate for parallelization.

```csharp
using System.Diagnostics;

IReadOnlyList<int> iterationsList = [.. Enumerable.Range(1, 10_000)];

Trace.WriteLine("***Finding prime numbers in series using LINQ...");
var seriesStopwatch = new Stopwatch();
seriesStopwatch.Start();

// ToDo: Retrieve Prime Numbers Sequentially

seriesStopwatch.Stop();

Trace.WriteLine($"***Finding prime numbers in series took {seriesStopwatch.Elapsed.TotalSeconds:F3}s.");

Trace.WriteLine("***Finding prime in Parallel using PLINQ...");
var parallelStopwatch = new Stopwatch();
parallelStopwatch.Start();

// ToDo: Retrieve Prime Numbers in Parallel

parallelStopwatch.Stop();

Trace.WriteLine($"***Finding prime numbers in parallel took {parallelStopwatch.Elapsed.TotalSeconds:F3}s.");

return;

static long FindNextPrimeNumber(int n)
{
	int count = 0;
	long a = 2;
	while (count < n)
	{
		long b = 2;
		int prime = 1;
		while (b * b <= a)
		{
			if (a % b is 0)
			{
				prime = 0;
				break;
			}

			b++;
		}

		if (prime > 0)
		{
			count++;
		}

		a++;
	}

	return (--a);
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/using-plinq-in-code-69955989/?t=10)

#### Sequential Execution

The sequential implementation uses standard LINQ's `Select` method.
In this context, a "method group" shorthand is used to pass `FindNextPrimeNumber` directly into the `Select` call.
Because LINQ uses deferred execution, the `ToList()` method is called to force the query to execute immediately, allowing the `Stopwatch` to accurately measure the time taken to process the entire list.
Note that while `Stopwatch` is used here for simplicity, `Benchmark.net` is recommended for professional performance analysis due to JIT optimization and overhead considerations.

```csharp
// ToDo: Retrieve Prime Numbers Sequentially
var primeNumbers = iterationsList.Select(FindNextPrimeNumber).ToList();

seriesStopwatch.Stop();

Trace.WriteLine($"***Finding prime numbers in series took {seriesStopwatch.Elapsed.TotalSeconds:F3}s.");
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/using-plinq-in-code-69955989/?t=130)

#### Parallel Execution with PLINQ

To transition to parallel execution, the `AsParallel()` extension method is called on the source collection.
To ensure the output list maintains the same order as the input range (1 to 10,000), the `AsOrdered()` method is applied.
Additionally, a `CancellationTokenSource` is implemented with a 30-second timeout and passed into the query via `WithCancellation()`.
This is a best practice to prevent long-running parallel operations from hanging indefinitely.
Similar to standard LINQ, PLINQ queries are deferred until a method like `ToList()` is called.

```csharp
// ToDo: Retrieve Prime Numbers in Parallel
var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
var primeNumbersInParallel = iterationsList
										.AsParallel()
										.AsOrdered()
										.WithCancellation(cts.Token)
										.Select(FindNextPrimeNumber).ToList();

parallelStopwatch.Stop();

Trace.WriteLine($"***Finding prime numbers in parallel took {parallelStopwatch.Elapsed.TotalSeconds:F3}s.");
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/using-plinq-in-code-69955989/?t=280)

#### Performance and Utilization

When running the code, sequential execution typically takes significantly longer (e.g., 17 seconds) while utilizing only a single core, resulting in a flat line in CPU monitoring.
In contrast, the PLINQ version completes much faster (e.g., 3 seconds) by utilizing all available CPU cores, visible as a significant spike in CPU utilization.
The `AsOrdered()` call ensures that despite the parallel processing, the final output remains sorted.

```csharp
foreach (var primeNumber in primeNumbersInParallel)
{
	Trace.Write($"{primeNumber}, ");
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/using-plinq-in-code-69955989/?t=355)
