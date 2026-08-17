# Channels

> Course: [From Zero to Hero: Parallel Programming in C#](https://dometrain.com/course/from-zero-to-hero-parallel-programming-in-csharp/) · Chapter 6
> 2 lessons · ~36:10
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Introduction to Channels](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-channels-69955986/) | 17:23 | [↓](#1-introduction-to-channels) |
| 2 | [Using Channels in Code](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/using-channels-in-code-69955987/) | 18:47 | [↓](#2-using-channels-in-code) |

---

## 1. Introduction to Channels

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-channels-69955986/) · 17:23

### Summary

Channels are a high-performance producer/consumer implementation in .NET that facilitate thread-safe communication between writers and readers via a centralized buffer.
Designed with an async-first approach, they eliminate the need for manual synchronization primitives like locks, thereby preventing deadlocks and race conditions while optimizing resource utilization for parallel workloads.

### Key concepts

- **Writer/Reader Model**: Channels use "Writers" to produce data and "Readers" to consume it.
- **Async-First Design**: Optimized for asynchronous operations to handle high concurrency without blocking threads.
- **Unbounded Channels**: Provide a buffer with no fixed size limit, constrained only by hardware memory.
- **Bounded Channels**: Enforce a fixed capacity, allowing for backpressure management through various "Full Mode" behaviors.
- **Priority Channels**: Order items in the buffer based on an `IComparer<T>` implementation.
- **Channel Completion**: Writers can signal completion to notify readers that no further data is expected.

### Lesson notes

The producer/consumer model in .NET is most effectively implemented using Channels.
In this model, multiple writers (producers) send data to a buffer, and multiple readers (consumers) retrieve data from that buffer.
Channels are designed to be thread-safe and asynchronous, handling concurrency internally to avoid deadlocks and race conditions.

#### Unbounded Channels

An unbounded channel has a buffer that is theoretically unlimited, though it is practically limited by available system memory.
If the buffer grows too large without being consumed, the application may encounter `OutOfMemoryException`.

When creating an unbounded channel, `UnboundedChannelOptions` can be used to tune performance:

- `SingleWriter` / `SingleReader`: Setting these to `true` allows the channel to use optimizations specific to single-threaded access.
- `AllowSynchronousContinuations`: If `true`, operations may continue on the same thread that completed the previous operation, reducing context switching.

```csharp
var options = new UnboundedChannelOptions
{
    AllowSynchronousContinuations = true,
    SingleReader = false,
    SingleWriter = false
};

var newsChannel = Channel.CreateUnbounded<NewsStory>(options);

// Produce News Stories
var story = new NewsStory(text);
await newsChannel.Writer.WriteAsync(story);

// Consume News Stories
var storyResult = await newsChannel.Reader.ReadAsync();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-channels-69955986/?t=120)

#### Unbounded Priority Channels

Priority channels function similarly to unbounded channels but use a `PriorityQueue` internally.
Instead of a First-In-First-Out (FIFO) order, items are read based on their priority.
This requires an `IComparer<T>` to determine the relative order of items.
Priority values are relative; for example, a story with priority 100 will be read before a story with priority 0.

```csharp
var options = new UnboundedPrioritizedChannelOptions<NewsStory>()
{
    AllowSynchronousContinuations = true,
    SingleReader = false,
    SingleWriter = false,
    Comparer = null, // Optional: Defaults to IComparer on the type
};

var newsChannel = Channel.CreateUnboundedPrioritized<NewsStory>(options);

// Produce News Stories
var story = new NewsStory(newsText, 0);
await newsChannel.Writer.WriteAsync(story);

var breakingNews = new NewsStory(breakingNewsText, 100);
await newsChannel.Writer.WriteAsync(breakingNews);

// Consume News Stories
var storyResult = await newsChannel.Reader.ReadAsync();

class NewsStory(string text, int priority) : IComparer<NewsStory>
{
    public string Text { get; } = text;
    public int Priority { get; } = priority;

    public int Compare(NewsStory? x, NewsStory? y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (y is null) return 1;
        if (x is null) return -1;

        return x.Priority.CompareTo(y.Priority);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-channels-69955986/?t=265)

#### Bounded Channels

Bounded channels are generally preferred for production applications because they prevent memory exhaustion by limiting the buffer size.
When a bounded channel reaches its capacity, the `FullMode` property determines the behavior of subsequent `WriteAsync` calls:

- `Wait`: The writer asynchronously waits for space to become available.
- `DropNewest`: The newest item in the buffer is removed to make room for the incoming item.
- `DropOldest`: The oldest item in the buffer is removed.
- `DropWrite`: The item currently being written is discarded.

Reading from a channel can be done individually via `ReadAsync` or by using `ReadAllAsync`, which returns an `IAsyncEnumerable<T>`.
This allows for a clean `await foreach` loop that continues until the channel is marked as complete.

```csharp
var options = new BoundedChannelOptions(30)
{
    AllowSynchronousContinuations = true,
    SingleReader = false,
    SingleWriter = false,
    FullMode = BoundedChannelFullMode.Wait
};

var restaurantOrdersChannel = Channel.CreateBounded<Food>(options);

// Submit Food Orders
var turkey = new Turkey();
var potatoes = new MashedPotatoes();
var gravy = new Gravy();
await restaurantOrdersChannel.Writer.WriteAsync(turkey);
await restaurantOrdersChannel.Writer.WriteAsync(potatoes);
await restaurantOrdersChannel.Writer.WriteAsync(gravy);

// Cook Food
await foreach (var foodOrder in restaurantOrdersChannel.Reader.ReadAllAsync())
{
    await foodOrder.Cook(default);
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-channels-69955986/?t=640)

#### Channel Completion

It is critical to signal when a writer has finished sending data by calling `Writer.Complete()`.
This marks the channel as closed for writing.
Once the channel is complete and the buffer is empty, any `ReadAllAsync` loops will terminate gracefully.
If you are unsure if a channel is already closed, `TryComplete()` can be used to avoid exceptions.

---

## 2. Using Channels in Code

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/using-channels-in-code-69955987/) · 18:47

### Summary

This lesson provides a practical demonstration of C# Channels through a restaurant simulation, where waiters act as producers and a cook acts as a consumer.
It covers the configuration of BoundedChannelOptions, including capacity management, backpressure handling with BoundedChannelFullMode.Wait, and optimizations for single-reader scenarios.
The lesson also details the lifecycle of a channel, from creation and asynchronous data production to graceful shutdown using Complete() and task synchronization.

### Key concepts

- Configuring BoundedChannelOptions for specific concurrency and capacity requirements.
- Implementing background consumers using ReadAllAsync to process channel data as an asynchronous stream.
- Managing backpressure using BoundedChannelFullMode.Wait to block producers when the buffer is full.
- Type-safe messaging with Channel.CreateBounded<T>.
- Graceful termination of producer-consumer workflows using ChannelWriter.Complete and task awaiting.

### Lesson notes

This lesson implements a restaurant simulation to demonstrate the producer-consumer pattern using C# Channels.
The domain is defined by a `Food` abstract class and several concrete implementations, where each food item has a specific cooking duration simulated via `Task.Delay`.

```csharp
using System.Diagnostics;

namespace Channels;

public abstract class Food
{
	readonly TimeSpan _cookTime;

	protected Food(TimeSpan cookTime)
	{
		_cookTime = cookTime;
		Name = GetType().Name;
	}

	public string Name { get; }

	public async Task Cook(CancellationToken cancellationToken)
	{
		Trace.WriteLine($"Cooking {Name} on Thread {Environment.CurrentManagedThreadId}");
		await Task.Delay(_cookTime, cancellationToken);
		Trace.WriteLine($"{Name} Completed on Thread {Environment.CurrentManagedThreadId}");
	}
}

public class Turkey() : Food(TimeSpan.FromSeconds(5));
public class MashedPotatoes() : Food(TimeSpan.FromSeconds(2));
public class Gravy() : Food(TimeSpan.FromSeconds(1));
public class Stuffing() : Food(TimeSpan.FromSeconds(2));
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/using-channels-in-code-69955987/?t=25)

To manage the flow of orders between waiters and the kitchen, a `BoundedChannel` is configured using `BoundedChannelOptions`.
Key settings include:

- **Capacity**: Limits the number of unread orders in the queue.
- **SingleReader**: Optimized for a single consumer (one cook).
- **SingleWriter**: Set to false to allow multiple waiters to submit orders simultaneously.
- **FullMode**: Set to `Wait`, which causes `WriteAsync` to block until space becomes available in the channel.
- **AllowSynchronousContinuations**: Set to true to allow the reader or writer to continue on the same thread if an operation completes synchronously, providing a performance benefit.

```csharp
var options = new BoundedChannelOptions(numTables)
{
	Capacity = numTables,
	SingleReader = true,
	FullMode = BoundedChannelFullMode.Wait,
	AllowSynchronousContinuations = true,
	SingleWriter = false
};
var ordersChannel = Channel.CreateBounded<Food>(options);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/using-channels-in-code-69955987/?t=195)

The consumer side of the channel is implemented in a background task called `KitchenTask`.
This task uses `ReadAllAsync` to iterate through all items in the channel as they arrive.
The loop continues until the channel is marked as complete and all buffered items have been processed.

```csharp
async Task KitchenTask(CancellationToken token = default)
{
	await foreach (var food in ordersChannel.Reader.ReadAllAsync(token))
	{
		Trace.WriteLine("");
		Trace.WriteLine($"Reading Order for {food.Name}");
		Trace.WriteLine("");

		await food.Cook(token);
	}
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/using-channels-in-code-69955987/?t=475)

The producer side is a loop that simulates a restaurant being open for a set duration.
Waiters generate random orders and use `WriteAsync` to place them into the channel.
If the channel reaches its capacity (e.g., five orders), the `WriteAsync` call will be awaited, effectively slowing down the producers until the kitchen processes an order and frees up space.

```csharp
while (restaurantOpenedTime + restaurantOpenDuration > DateTimeOffset.UtcNow)
{
	// Take Order
	var food = GetOrder();

	Trace.WriteLine("");
	Trace.WriteLine($"Submitting Order for {food.Name}. Total Unread Orders: {ordersChannel.Reader.Count}.");
	Trace.WriteLine("");

	var numSecondsBeforeNextOrder = Random.Shared.Next(1, 2);
	await ordersChannel.Writer.WriteAsync(food);

	// Wait for next order
	await Task.Delay(TimeSpan.FromSeconds(numSecondsBeforeNextOrder));
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/using-channels-in-code-69955987/?t=595)

When the restaurant closes, the system must ensure all remaining orders in the buffer are cooked before the application exits.
This is achieved by calling `writer.Complete()`, which signals to the `ReadAllAsync` stream that no more items will be added.
The application then awaits the `KitchenTask` to ensure the consumer has finished processing the remaining buffer.

```csharp
ordersChannel.Writer.Complete();
await kitchenTask;

Trace.WriteLine("");
Trace.WriteLine("Kitchen Closed");
Trace.WriteLine("");
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/using-channels-in-code-69955987/?t=760)

During execution, logging artifacts may show orders being read before they are submitted.
This is a result of parallel execution and the overhead associated with task creation and trace log buffering; logically, the channel ensures that items are written before they can be read.
The simulation demonstrates that the kitchen continues to work after the restaurant closes until the channel buffer is empty.
