# Concurrent Collections

> Course: [From Zero to Hero: Parallel Programming in C#](https://dometrain.com/course/from-zero-to-hero-parallel-programming-in-csharp/) · Chapter 5
> 2 lessons · ~24:10
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Introduction to Concurrent Collections](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-concurrent-collections-69957397/) | 8:41 | [↓](#1-introduction-to-concurrent-collections) |
| 2 | [Improving Performance of a Mobile App](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/improving-performance-of-a-mobile-app-69957398/) | 15:29 | [↓](#2-improving-performance-of-a-mobile-app) |

---

## 1. Introduction to Concurrent Collections

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-concurrent-collections-69957397/) · 8:41

Concurrent collections provide a thread-safe way to manage data in multi-threaded environments.
They allow multiple threads to read from and write to a collection simultaneously without the risk of race conditions or data corruption, eliminating the need for manual lock management in many common scenarios.

### Key concepts

*   **Thread Safety**: Collections designed for concurrent access without manual synchronization.
*   **ConcurrentBag<T>**: An unordered collection of objects that allows duplicates and is optimized for scenarios where the same thread both produces and consumes data.
*   **ConcurrentStack<T>**: A lock-free, thread-safe Last-In-First-Out (LIFO) collection.
*   **ConcurrentQueue<T>**: A lock-free, thread-safe First-In-First-Out (FIFO) collection.
*   **ConcurrentDictionary<TKey, TValue>**: A thread-safe collection of key-value pairs featuring lock-free reads and granular, per-key locking for writes.
*   **Lock-Free Operations**: Many concurrent collections utilize `Interlocked` methods under the hood to avoid expensive thread synchronization and context switching.

### Lesson notes

#### Legacy Collections and Async Compatibility

While `BlockingCollection<T>` is a thread-safe collection, it is generally avoided in modern .NET development.
It was introduced before the `async/await` pattern and relies on blocking calls that do not release the calling thread.
For modern asynchronous workflows, System.Threading.Channels is the preferred alternative.

#### ConcurrentBag<T>

`ConcurrentBag<T>` is an unordered collection found in the `System.Collections.Concurrent` namespace.
It serves as a thread-safe replacement for `IEnumerable<T>` or `List<T>` when the order of items is not important.
Unlike a Set, a Bag allows duplicate items.

```csharp
ConcurrentBag<StockSymbolModel> stockSymbolBag = [];

Parallel.ForEach(_stockSymbols, pair =>
{
    var (symbol, companyName) = pair;
    // color and latestStockQuote are derived from the current context
    stockSymbolBag.Add(new StockSymbolModel(symbol, companyName, color, latestStockQuote));

    Trace.WriteLine($"Number of {symbol} entries in bag: {stockSymbolBag.Count(x => x.Symbol == symbol)}");
});
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-concurrent-collections-69957397/?t=130)

`ConcurrentBag` is optimized for performance through thread-local storage.
It performs best when the same thread that writes an item is also the one that reads or removes it.
If items are frequently added on one thread and removed on another, the performance benefits diminish, making it one of the less frequently used concurrent collections.

#### ConcurrentStack<T>

`ConcurrentStack<T>` replaces the standard `Stack<T>` for multi-threaded LIFO (Last-In-First-Out) operations.
It is completely lock-free, utilizing `Interlocked.Exchange` under the hood to manage the stack pointer without requiring traditional locks.
This makes operations like `Push`, `Pop`, and `TryPeek` highly efficient.

```csharp
ConcurrentStack<StockSymbolModel> stockSymbolStack = [];

Parallel.ForEach(_stockSymbols, pair =>
{
    var (symbol, companyName) = pair;
    stockSymbolStack.Push(new StockSymbolModel(symbol, companyName, color, latestStockQuote));

    if (stockSymbolStack.TryPeek(out var result))
        Trace.WriteLine($"Top {symbol} entries in Stack: {result.Symbol}");
});
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-concurrent-collections-69957397/?t=250)

Stacks are ideal for scenarios where only the most recently added item is relevant, such as managing a call stack.

#### ConcurrentQueue<T>

`ConcurrentQueue<T>` is the thread-safe equivalent of `Queue<T>`, providing FIFO (First-In-First-Out) behavior.
Like the concurrent stack, it is lock-free and uses `Interlocked` operations to avoid thread switching and synchronization overhead.

```csharp
ConcurrentQueue<StockSymbolModel> stockSymbolQueue = [];

Parallel.ForEach(_stockSymbols, pair =>
{
    var (symbol, companyName) = pair;
    stockSymbolQueue.Enqueue(new StockSymbolModel(symbol, companyName, color, latestStockQuote));
});

if(stockSymbolQueue.TryDequeue(out var oldestStockSymbolModel))
    Trace.WriteLine($"This stock symbol was the oldest item in the Queue: {oldestStockSymbolModel.Symbol}");
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-concurrent-collections-69957397/?t=310)

#### ConcurrentDictionary<TKey, TValue>

`ConcurrentDictionary<TKey, TValue>` provides a thread-safe implementation of a key-value store.
It is highly optimized for concurrent access:

*   **Reads**: Completely lock-free.
*   **Writes**: Uses fine-grained locking where every key has its own lock. Multiple threads can write to different keys simultaneously without interference. Locking only occurs if multiple threads attempt to write to the exact same key at the same time.

A powerful feature of this collection is the `AddOrUpdate` method, which atomically handles both the insertion of new keys and the modification of existing ones using delegate factories.

```csharp
ConcurrentDictionary<string, StockQuoteModel> stockQuoteDictionary = [];

Parallel.ForEach(_stockSymbols, pair =>
{
    var (symbol, companyName) = pair;
    stockQuoteDictionary.AddOrUpdate(symbol,
        addValueFactory: _ => latestStockQuote,
        updateValueFactory: (_, existingStockQuote) => 
            latestStockQuote.Time > existingStockQuote.Time ? latestStockQuote : existingStockQuote);
});
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-concurrent-collections-69957397/?t=490)

---

## 2. Improving Performance of a Mobile App

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/improving-performance-of-a-mobile-app-69957398/) · 15:29

### Summary

This lesson demonstrates how to optimize a real-world .NET MAUI mobile application by replacing standard collections with concurrent versions and implementing thread synchronization.
By refactoring a stock tracking application, it illustrates the practical application of `ConcurrentDictionary`, `ConcurrentBag`, and `SemaphoreSlim` to resolve race conditions and improve performance when handling high-frequency API updates across multiple background threads.

### Key concepts

- Identifying thread-safety issues in shared state when using `Parallel.ForEachAsync`.
- Using `ConcurrentDictionary.AddOrUpdate` for atomic read-modify-write operations based on data timestamps.
- Implementing `SemaphoreSlim` to protect non-thread-safe resources, such as `Timer` instances, from concurrent access during state changes.
- Leveraging `Parallel.ForEach` with `ConcurrentBag` for parallel data processing in scenarios where asynchronous calls are not feasible (e.g., within property getters).
- Managing API rate limits by synchronizing background threads to pause and resume data retrieval.

### Lesson notes

The StockWatch application retrieves stock quotes every two seconds using a timer.
The `DashboardViewModel` manages this process, initializing a timer that triggers the `UpdateStockPrices` method.

```csharp
Timer CreateGetStockDataTimer()
{
    var timer = new Timer(async _ =>
    {
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        await UpdateStockPrices(cts.Token).ConfigureAwait(false);
    });
    timer.Change(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));

    return timer;
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/improving-performance-of-a-mobile-app-69957398/?t=70)

The `UpdateStockPrices` method uses `Parallel.ForEachAsync` to fetch data for multiple stock symbols simultaneously.
This approach spins up multiple background threads to perform non-blocking API calls.

```csharp
async Task UpdateStockPrices(CancellationToken token)
{
    try
    {
        await Parallel.ForEachAsync(_stockSymbols.Keys, token, async (symbol, token) => await UpdateStockPrice(symbol, token));
    }
    catch (OperationCanceledException)
    {
        await StopRetrievingStockData(token).ConfigureAwait(false);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/improving-performance-of-a-mobile-app-69957398/?t=115)

Initially, the application used a standard `Dictionary<string, StockQuoteModel>` for `_latestStockQuotes`.
Because `Parallel.ForEachAsync` allows multiple threads to write to this dictionary while the UI thread reads from it, it must be replaced with a `ConcurrentDictionary` to ensure thread safety.

```csharp
readonly ConcurrentDictionary<string, StockQuoteModel> _latestStockQuotes = new();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/improving-performance-of-a-mobile-app-69957398/?t=370)

The `ConcurrentDictionary` allows for more efficient updates using the `AddOrUpdate` method.
This method takes a key, an "add" factory, and an "update" factory.
The update factory compares the timestamp of the new quote with the existing one to ensure only the most recent data is preserved, preventing older API responses from overwriting newer data.

```csharp
async Task UpdateStockPrice(string symbol, CancellationToken token)
{
    try
    {
        var quote = await _finnHubApiService.GetStockQuote(symbol, token).ConfigureAwait(false);
        _latestStockQuotes.AddOrUpdate(symbol, _ => quote, (_, previousQuote) => quote.Time > previousQuote.Time ? quote : previousQuote);

        Trace.WriteLine($"Updated Stock Price for {quote}");

        OnPropertyChanged(nameof(AssetList));
    }
    catch (ApiException e) when (e.StatusCode is HttpStatusCode.TooManyRequests)
    {
        // Rate limiting logic...
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/improving-performance-of-a-mobile-app-69957398/?t=505)

A race condition exists in the `StartRetrievingStockData` and `StopRetrievingStockData` methods.
These methods are called when the API returns a "Too Many Requests" (429) error.
Since multiple parallel threads might hit this error simultaneously, they could all attempt to dispose of or re-initialize the `_getStockDataTimer` at the same time.
To resolve this, a `SemaphoreSlim` is introduced to ensure only one thread can modify the timer state at a time.

```csharp
readonly SemaphoreSlim _retrievingStockDataRaceConditionSemaphoreSlim = new(1, 1);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/improving-performance-of-a-mobile-app-69957398/?t=670)

The implementation uses a `try-finally` block to ensure the semaphore is released even if an exception occurs during disposal or initialization, preventing potential deadlocks.

```csharp
async ValueTask StopRetrievingStockData(CancellationToken token)
{
    if (_getStockDataTimer is null)
    {
        return;
    }

    await _retrievingStockDataRaceConditionSemaphoreSlim.WaitAsync(token);

    try
    {
        if (_getStockDataTimer is not null)
        {
            await _getStockDataTimer.DisposeAsync();
            _getStockDataTimer = null;
        }
    }
    finally
    {
        _retrievingStockDataRaceConditionSemaphoreSlim.Release();
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/improving-performance-of-a-mobile-app-69957398/?t=745)

Finally, the `GetStockSymbols` method, which prepares data for the UI, is optimized.
While `Parallel.ForEach` is a blocking call, it is used here because the method is called by a read-only property where `async/await` is not an option.
By using `Parallel.ForEach` with a `ConcurrentBag` to collect the results, the application processes the stock list in parallel before ordering the final list for the UI.

```csharp
IReadOnlyList<StockSymbolModel> GetStockSymbols()
{
    ConcurrentBag<StockSymbolModel> stockSymbolList = [];

    Parallel.ForEach(_stockSymbols, pair =>
    {
        var (symbol, companyName) = pair;
        _latestStockQuotes.TryGetValue(symbol, out var latestStockQuote);

        var color = latestStockQuote switch
        {
            null => Colors.Grey,
            { Change: > 0 } => Color.FromRgb(9, 133, 81),
            { Change: < 0 } => Color.FromRgb(207, 32, 47),
            _ => Colors.Grey
        };

        stockSymbolList.Add(new StockSymbolModel(symbol, companyName, color, latestStockQuote));
    });

    return [.. stockSymbolList.OrderBy(static x => x.Symbol)];
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/improving-performance-of-a-mobile-app-69957398/?t=880)
