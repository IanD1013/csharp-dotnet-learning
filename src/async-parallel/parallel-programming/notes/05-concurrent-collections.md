# Concurrent Collections

> 课程:[From Zero to Hero: Parallel Programming in C#](https://dometrain.com/course/from-zero-to-hero-parallel-programming-in-csharp/) · 第 5 章
> 共 2 课 · 约 24:10
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

---

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction to Concurrent Collections](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-concurrent-collections-69957397/) | 8:41 | [↓](#1-introduction-to-concurrent-collections) |
| 2 | [Improving Performance of a Mobile App](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/improving-performance-of-a-mobile-app-69957398/) | 15:29 | [↓](#2-improving-performance-of-a-mobile-app) |

---

## 1. Introduction to Concurrent Collections

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-concurrent-collections-69957397/) · 8:41

并发集合提供了一种在多线程环境中以线程安全方式管理数据的手段。
它们允许多个线程同时读写同一个集合,而不会有竞态条件或数据损坏的风险,从而在许多常见场景中免去了手动管理锁的需要。

### 核心概念

*   **Thread Safety(线程安全)**:为并发访问而设计、无需手动同步的集合。
*   **ConcurrentBag<T>**:一种无序的对象集合,允许重复项,并针对同一线程既生产又消费数据的场景做了优化。
*   **ConcurrentStack<T>**:一种无锁、线程安全的后进先出(LIFO)集合。
*   **ConcurrentQueue<T>**:一种无锁、线程安全的先进先出(FIFO)集合。
*   **ConcurrentDictionary<TKey, TValue>**:一种线程安全的键值对集合,读取无锁,写入则采用按键划分的细粒度锁。
*   **Lock-Free Operations(无锁操作)**:许多并发集合在底层使用 `Interlocked` 方法,以避免代价高昂的线程同步和上下文切换。

### 课程笔记

#### 旧式集合与异步兼容性

虽然 `BlockingCollection<T>` 是一个线程安全的集合,但在现代 .NET 开发中通常会避免使用它。
它是在 `async/await` 模式出现之前引入的,依赖的是不会释放调用线程的阻塞调用。
对于现代的异步工作流,System.Threading.Channels 是首选的替代方案。

#### ConcurrentBag<T>

`ConcurrentBag<T>` 是位于 `System.Collections.Concurrent` 命名空间中的一种无序集合。
当元素的顺序不重要时,它可以作为 `IEnumerable<T>` 或 `List<T>` 的线程安全替代品。
与 Set 不同,Bag 允许重复的元素。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-concurrent-collections-69957397/?t=130)

`ConcurrentBag` 通过线程本地存储来优化性能。
当写入某个元素的线程同时也是读取或移除它的线程时,它的性能最好。
如果元素经常在一个线程上添加、在另一个线程上移除,性能优势就会减弱,这也使它成为较少使用的并发集合之一。

#### ConcurrentStack<T>

`ConcurrentStack<T>` 在多线程的 LIFO(后进先出)操作中取代标准的 `Stack<T>`。
它是完全无锁的,在底层利用 `Interlocked.Exchange` 来管理栈指针,而不需要传统的锁。
这使得 `Push`、`Pop` 和 `TryPeek` 等操作非常高效。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-concurrent-collections-69957397/?t=250)

栈非常适合只有最近添加的元素才有意义的场景,例如管理调用栈。

#### ConcurrentQueue<T>

`ConcurrentQueue<T>` 是 `Queue<T>` 的线程安全等价物,提供 FIFO(先进先出)行为。
与并发栈一样,它也是无锁的,并使用 `Interlocked` 操作来避免线程切换和同步开销。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-concurrent-collections-69957397/?t=310)

#### ConcurrentDictionary<TKey, TValue>

`ConcurrentDictionary<TKey, TValue>` 提供了键值存储的线程安全实现。
它针对并发访问做了高度优化:

*   **读取**:完全无锁。
*   **写入**:使用细粒度锁,每个键都有自己的锁。多个线程可以同时写入不同的键而互不干扰。只有当多个线程同时尝试写入完全相同的键时,才会发生加锁。

```text
  写入不同的键: 互不干扰
    线程 1 --> [键 1 的锁] --> 键 1
    线程 2 --> [键 2 的锁] --> 键 2

  同时写入同一个键: 才会加锁
    线程 1 --> [键 1 的锁] --> 键 1
    线程 2 --> [键 1 的锁] (等待)
```

该集合的一个强大特性是 `AddOrUpdate` 方法,它借助委托工厂,以原子方式同时处理新键的插入和已有键的修改。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-concurrent-collections-69957397/?t=490)

---

## 2. Improving Performance of a Mobile App

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/improving-performance-of-a-mobile-app-69957398/) · 15:29

### 总结

本课演示如何优化一个真实的 .NET MAUI 移动应用:用并发版本替换标准集合,并实现线程同步。
通过重构一个股票跟踪应用,本课展示了 `ConcurrentDictionary`、`ConcurrentBag` 和 `SemaphoreSlim` 的实际应用,用来解决竞态条件,并在跨多个后台线程处理高频 API 更新时提升性能。

### 核心概念

- 识别在使用 `Parallel.ForEachAsync` 时共享状态中的线程安全问题。
- 使用 `ConcurrentDictionary.AddOrUpdate`,基于数据时间戳执行原子的读-改-写操作。
- 实现 `SemaphoreSlim`,在状态变化期间保护非线程安全的资源(例如 `Timer` 实例)免受并发访问。
- 在无法进行异步调用的场景中(例如在属性 getter 内部),借助 `Parallel.ForEach` 配合 `ConcurrentBag` 进行并行数据处理。
- 通过同步后台线程来暂停和恢复数据获取,从而应对 API 速率限制。

### 课程笔记

StockWatch 应用使用一个计时器,每两秒获取一次股票报价。
`DashboardViewModel` 负责管理这一过程,它初始化一个计时器来触发 `UpdateStockPrices` 方法。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/improving-performance-of-a-mobile-app-69957398/?t=70)

`UpdateStockPrices` 方法使用 `Parallel.ForEachAsync` 同时为多个股票代码获取数据。
这种做法会启动多个后台线程来执行非阻塞的 API 调用。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/improving-performance-of-a-mobile-app-69957398/?t=115)

```text
Timer (每 2 秒)
  |
  v
UpdateStockPrices
  |
  v
Parallel.ForEachAsync(_stockSymbols.Keys)
  |-- 后台线程 --> UpdateStockPrice(symbol, token)
  |-- 后台线程 --> UpdateStockPrice(symbol, token)
  '-- 后台线程 --> UpdateStockPrice(symbol, token)
```

最初,应用为 `_latestStockQuotes` 使用的是标准的 `Dictionary<string, StockQuoteModel>`。
由于 `Parallel.ForEachAsync` 允许多个线程写入这个字典,而 UI 线程同时在读取它,因此必须把它替换为 `ConcurrentDictionary` 以确保线程安全。

```csharp
readonly ConcurrentDictionary<string, StockQuoteModel> _latestStockQuotes = new();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/improving-performance-of-a-mobile-app-69957398/?t=370)

`ConcurrentDictionary` 可以借助 `AddOrUpdate` 方法实现更高效的更新。
该方法接收一个键、一个"添加"工厂和一个"更新"工厂。
更新工厂会比较新报价与已有报价的时间戳,确保只保留最新的数据,防止较旧的 API 响应覆盖较新的数据。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/improving-performance-of-a-mobile-app-69957398/?t=505)

`StartRetrievingStockData` 和 `StopRetrievingStockData` 方法中存在一个竞态条件。
当 API 返回"Too Many Requests"(429)错误时,会调用这两个方法。
由于多个并行线程可能同时遇到这个错误,它们可能会在同一时间尝试释放或重新初始化 `_getStockDataTimer`。
为了解决这个问题,引入了一个 `SemaphoreSlim`,确保同一时间只有一个线程能修改计时器的状态。

```csharp
readonly SemaphoreSlim _retrievingStockDataRaceConditionSemaphoreSlim = new(1, 1);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/improving-performance-of-a-mobile-app-69957398/?t=670)

该实现使用 `try-finally` 块,确保即使在释放或初始化过程中发生异常,信号量也会被释放,从而避免潜在的死锁。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/improving-performance-of-a-mobile-app-69957398/?t=745)

最后,对为 UI 准备数据的 `GetStockSymbols` 方法进行了优化。
虽然 `Parallel.ForEach` 是一个阻塞调用,但这里仍然使用它,因为该方法是由一个只读属性调用的,而在那里无法使用 `async/await`。
通过使用 `Parallel.ForEach` 配合 `ConcurrentBag` 来收集结果,应用会先并行处理股票列表,然后再为 UI 对最终列表进行排序。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/improving-performance-of-a-mobile-app-69957398/?t=880)
