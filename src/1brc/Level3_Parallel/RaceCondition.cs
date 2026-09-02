using System.Diagnostics;

namespace Level3_Parallel;

/// <summary>
/// The counter experiments from the "What is a Race Condition?" lesson, in the order the lesson
/// shows them: sequential, parallel without synchronization, parallel with a lock on an object,
/// parallel with the .NET 9 <see cref="Lock"/> type.
/// </summary>
internal static class RaceCondition
{
    private const int Iterations = 100_000_000;

    internal static void Run()
    {
        Console.WriteLine("=== Level 3: Race Condition ===");
        Console.WriteLine($"Processor Count: {Environment.ProcessorCount}");
        Console.WriteLine($"Iterations: {Iterations:N0}");
        Console.WriteLine();

        Sequential();
        ParallelUnsynchronized();
        ParallelWithObjectLock();
        ParallelWithLockType();
    }

    private static void Sequential()
    {
        var counter = 0;
        var stopwatch = Stopwatch.StartNew();

        for (var i = 0; i < Iterations; i++)
        {
            counter++;

            // 1. Read the counter from memory - store it in cpu's registers
            // 2. Increase its value by 1 - INC counter 1
            // 3. Write the counter back to memory - STR counter
        }

        stopwatch.Stop();
        Report("for                     ", counter, stopwatch.Elapsed);
    }

    private static void ParallelUnsynchronized()
    {
        var counter = 0;
        var stopwatch = Stopwatch.StartNew();

        Parallel.For(0, Iterations, _ =>
        {
            counter++;
        });

        stopwatch.Stop();
        Report("Parallel.For, no lock   ", counter, stopwatch.Elapsed);
    }

    private static void ParallelWithObjectLock()
    {
        var counter = 0;
        // Locking on a plain object is the first fix the lesson shows, before it introduces
        // System.Threading.Lock as the more efficient one below. Both stay.
        object lockObject = new object();
        var stopwatch = Stopwatch.StartNew();

        Parallel.For(0, Iterations, _ =>
        {
            lock (lockObject)
            {
                counter++;
            }
        });

        stopwatch.Stop();
        Report("Parallel.For, object    ", counter, stopwatch.Elapsed);
    }

    private static void ParallelWithLockType()
    {
        var counter = 0;
        System.Threading.Lock lockObject = new();
        var stopwatch = Stopwatch.StartNew();

        Parallel.For(0, Iterations, _ =>
        {
            lock (lockObject)
            {
                counter++;
            }
        });

        stopwatch.Stop();
        Report("Parallel.For, Lock      ", counter, stopwatch.Elapsed);
    }

    private static void Report(string label, int counter, TimeSpan elapsed)
    {
        var verdict = counter == Iterations ? "correct" : $"lost {Iterations - counter:N0} increments";
        Console.WriteLine($"{label} counter = {counter,12:N0}  {elapsed}  <- {verdict}");
    }
}
