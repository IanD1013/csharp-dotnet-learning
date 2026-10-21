# Channels

> 课程:[From Zero to Hero: Parallel Programming in C#](https://dometrain.com/course/from-zero-to-hero-parallel-programming-in-csharp/) · 第 6 章
> 共 2 课 · 约 36:10
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

---

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction to Channels](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-channels-69955986/) | 17:23 | [↓](#1-introduction-to-channels) |
| 2 | [Using Channels in Code](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/using-channels-in-code-69955987/) | 18:47 | [↓](#2-using-channels-in-code) |

---

## 1. Introduction to Channels

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-channels-69955986/) · 17:23

### 总结

Channels 是 .NET 中一种高性能的生产者/消费者实现,它通过一个集中式缓冲区,促成写入方与读取方之间线程安全的通信。
它以异步优先的方式设计,无需再手动使用锁之类的同步原语,从而避免死锁和竞态条件,同时为并行工作负载优化资源利用。

### 核心概念

- **Writer/Reader Model**:Channels 使用 "Writers" 生产数据,使用 "Readers" 消费数据。
- **Async-First Design**:针对异步操作进行了优化,可以在不阻塞线程的情况下应对高并发。
- **Unbounded Channels**:提供一个没有固定大小限制的缓冲区,只受硬件内存的约束。
- **Bounded Channels**:强制固定容量,并允许通过各种 "Full Mode" 行为来管理背压(backpressure)。
- **Priority Channels**:根据 `IComparer<T>` 实现对缓冲区中的项进行排序。
- **Channel Completion**:写入方可以发出完成信号,通知读取方不会再有更多数据。

### 课程笔记

在 .NET 中,生产者/消费者模型最有效的实现方式是使用 Channels。
在这个模型中,多个写入方(生产者)把数据发送到一个缓冲区,多个读取方(消费者)从该缓冲区中取出数据。
Channels 被设计为线程安全且异步的,它在内部处理并发,以避免死锁和竞态条件。

```text
  Writers (producers)                    Readers (consumers)
  +----------+                            +----------+
  | Writer 1 |---+                    +-->| Reader 1 |
  +----------+   |   +------------+   |   +----------+
                 +-->|   buffer   |---+
  +----------+   |   +------------+   |   +----------+
  | Writer 2 |---+                    +-->| Reader 2 |
  +----------+                            +----------+
```

#### Unbounded Channels

无界 channel 的缓冲区在理论上是无限的,但实际上受可用系统内存的限制。
如果缓冲区不断增长却得不到消费,应用程序可能会遇到 `OutOfMemoryException`。

创建无界 channel 时,可以使用 `UnboundedChannelOptions` 来调优性能:

- `SingleWriter` / `SingleReader`:把它们设为 `true`,可以让 channel 使用专门针对单线程访问的优化。
- `AllowSynchronousContinuations`:如果为 `true`,操作可能会在完成上一个操作的同一线程上继续执行,从而减少上下文切换。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-channels-69955986/?t=120)

#### Unbounded Priority Channels

优先级 channel 的工作方式与无界 channel 类似,但在内部使用 `PriorityQueue`。
项不是按先进先出(FIFO)的顺序读取,而是根据其优先级读取。
这需要一个 `IComparer<T>` 来确定项之间的相对顺序。
优先级的值是相对的;例如,优先级为 100 的新闻会先于优先级为 0 的新闻被读取。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-channels-69955986/?t=265)

#### Bounded Channels

在生产环境的应用程序中,通常更推荐使用有界 channel,因为它通过限制缓冲区大小来防止内存耗尽。
当有界 channel 达到其容量时,`FullMode` 属性决定后续 `WriteAsync` 调用的行为:

- `Wait`:写入方异步等待,直到有可用空间。
- `DropNewest`:移除缓冲区中最新的项,为传入的项腾出空间。
- `DropOldest`:移除缓冲区中最旧的项。
- `DropWrite`:丢弃当前正在写入的项。

从 channel 读取时,既可以通过 `ReadAsync` 逐个读取,也可以使用返回 `IAsyncEnumerable<T>` 的 `ReadAllAsync`。
这样就可以写出一个简洁的 `await foreach` 循环,它会一直运行,直到 channel 被标记为完成。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-channels-69955986/?t=640)

#### Channel Completion

通过调用 `Writer.Complete()` 来表明写入方已经发送完数据,这一点至关重要。
这会把 channel 标记为对写入关闭。
一旦 channel 已完成且缓冲区为空,所有 `ReadAllAsync` 循环都会优雅地结束。
如果你不确定 channel 是否已经关闭,可以使用 `TryComplete()` 来避免异常。

---

## 2. Using Channels in Code

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/using-channels-in-code-69955987/) · 18:47

### 总结

本课通过一个餐厅模拟,实际演示 C# Channels,其中服务员充当生产者,厨师充当消费者。
内容涵盖 BoundedChannelOptions 的配置,包括容量管理、使用 BoundedChannelFullMode.Wait 处理背压,以及针对单读取方场景的优化。
本课还详细介绍了 channel 的生命周期,从创建、异步生产数据,到使用 Complete() 和任务同步进行优雅关闭。

### 核心概念

- 根据特定的并发和容量需求配置 BoundedChannelOptions。
- 使用 ReadAllAsync 实现后台消费者,把 channel 数据作为异步流来处理。
- 使用 BoundedChannelFullMode.Wait 管理背压,在缓冲区已满时阻塞生产者。
- 使用 Channel.CreateBounded<T> 进行类型安全的消息传递。
- 使用 ChannelWriter.Complete 和等待任务来优雅地终止生产者-消费者工作流。

### 课程笔记

本课实现了一个餐厅模拟,用来演示如何使用 C# Channels 实现生产者-消费者模式。
领域由一个 `Food` 抽象类和若干具体实现定义,其中每种食物都有特定的烹饪时长,通过 `Task.Delay` 来模拟。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/using-channels-in-code-69955987/?t=25)

为了管理服务员与厨房之间的订单流转,使用 `BoundedChannelOptions` 配置了一个 `BoundedChannel`。
关键设置包括:

- **Capacity**:限制队列中未读订单的数量。
- **SingleReader**:针对单个消费者(一名厨师)进行优化。
- **SingleWriter**:设为 false,以允许多名服务员同时提交订单。
- **FullMode**:设为 `Wait`,这会让 `WriteAsync` 阻塞,直到 channel 中有可用空间。
- **AllowSynchronousContinuations**:设为 true,以便在操作同步完成时,让读取方或写入方在同一线程上继续执行,从而带来性能上的好处。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/using-channels-in-code-69955987/?t=195)

channel 的消费者一端实现在一个名为 `KitchenTask` 的后台任务中。
这个任务使用 `ReadAllAsync`,在 channel 中的项到达时逐一遍历它们。
循环会一直进行,直到 channel 被标记为完成,并且所有缓冲的项都已处理完毕。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/using-channels-in-code-69955987/?t=475)

生产者一端是一个循环,模拟餐厅在一段设定的时长内营业。
服务员生成随机订单,并使用 `WriteAsync` 把它们放入 channel。
如果 channel 达到其容量(例如五个订单),`WriteAsync` 调用就会处于等待状态,实际上会让生产者放慢速度,直到厨房处理完一个订单并腾出空间。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/using-channels-in-code-69955987/?t=595)

```text
  Waiters (producers)       ordersChannel             KitchenTask
                        (Capacity = numTables)        (one cook)
  +---------+  WriteAsync  +----------------+  ReadAllAsync  +--------+
  | Waiter  |------------->| Food Food ...  |--------------->|  Cook  |
  | Waiter  |              +----------------+                +--------+
  +---------+
       ^                            |
       +--- full: WriteAsync waits -+  (FullMode = Wait)
```

当餐厅打烊时,系统必须确保缓冲区中剩余的所有订单都在应用程序退出前烹饪完毕。
这是通过调用 `writer.Complete()` 实现的,它向 `ReadAllAsync` 流发出信号,表明不会再添加更多项。
随后,应用程序等待 `KitchenTask`,以确保消费者已处理完剩余的缓冲区。

```csharp
ordersChannel.Writer.Complete();
await kitchenTask;

Trace.WriteLine("");
Trace.WriteLine("Kitchen Closed");
Trace.WriteLine("");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/using-channels-in-code-69955987/?t=760)

在执行过程中,日志记录可能会显示订单在提交之前就被读取了。
这是并行执行以及与任务创建和跟踪日志缓冲相关的开销所导致的结果;从逻辑上讲,channel 确保项在被写入之后才能被读取。
该模拟表明,在餐厅打烊后,厨房会继续工作,直到 channel 缓冲区为空。
