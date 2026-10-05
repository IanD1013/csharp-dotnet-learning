# Scaling Background Processing

> 课程:[From Zero to Hero: Background Processing in .NET](https://dometrain.com/course/from-zero-to-hero-background-processing-in-dotnet/) · 第 9 章
> 共 6 课 · 约 55:12
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

---

## 课程索引

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Single Worker vs Distributed Workers](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/single-worker-vs-distributed-workers-69958213/) | 6:08 | [↓](#1-single-worker-vs-distributed-workers) |
| 2 | [Concurrency Control](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/concurrency-control-69958214/) | 12:13 | [↓](#2-concurrency-control) |
| 3 | [Integrating Message Brokers](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/integrating-message-brokers-69958215/) | 7:07 | [↓](#3-integrating-message-brokers) |
| 4 | [RabbitMQ](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/rabbitmq-69958216/) | 13:48 | [↓](#4-rabbitmq) |
| 5 | [Apache Kafka](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/apache-kafka-69958217/) | 11:48 | [↓](#5-apache-kafka) |
| 6 | [Putting it all Together](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/putting-it-all-together-69958218/) | 4:08 | [↓](#6-putting-it-all-together) |

---

## 1. Single Worker vs Distributed Workers

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/single-worker-vs-distributed-workers-69958213/) · 6:08

### 总结

从单个 worker 过渡到分布式 worker,是后台处理中一次根本性的架构转变。
单个 worker 易于管理和调试,但它最终无法应对高吞吐量的工作负载或突发流量。
水平扩展允许多个 worker 从同一个队列拉取作业,从而提高系统吞吐量。
然而,这一转变会引入竞态条件和资源争用等复杂性,需要健壮的并发控制来应对。

### 核心概念

- **Single Worker Simplicity**:顺序处理易于推理、调试和部署,非常适合内部系统或低流量系统。
- **Vertical vs. Horizontal Scaling**:垂直扩展是给单个实例增加更多 CPU/RAM;水平扩展是增加更多相互独立的 worker 实例。
- **Throughput vs. Latency**:扩展会增加并发作业的数量(吞吐量),但不会缩短单个作业完成所需的时间(延迟)。
- **Workload Types**:IO 密集型任务(等待外部系统)与 CPU 密集型任务(OCR、AI 推理)的扩展方式不同,后者如果配置过多,可能会压垮宿主机资源。
- **Distributed System Challenges**:转向多个 worker 会引入重复处理、竞态条件和资源争用的风险。

### 课程笔记

#### 单 Worker 模式

在一个基础的后台处理架构中,单个进程从队列中获取作业并按顺序处理它们。
这种模式通常用 `BackgroundService` 实现,由一个 `ExecuteAsync` 循环管理作业获取与执行的生命周期。

```csharp
namespace WorkerServiceExample
{
    public class Worker(ILogger<Worker> logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var job = await GetJobAsync(stoppingToken);
                if (job != null)
                {                    
                    await ProcessJobAsync(job, stoppingToken);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }

        private async Task<BackgroundJob> GetJobAsync(CancellationToken cancellationToken)
        {
            // Simulate getting a job
            await Task.Delay(500, cancellationToken);
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug("{time}: Job Retrieved", DateTimeOffset.Now);
            }
            return new BackgroundJob();
        }

        private async Task ProcessJobAsync(BackgroundJob job, CancellationToken cancellationToken)
        {
            // Simulate processing a job
            await Task.Delay(1000, cancellationToken);
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug("{time}: Job Processed", DateTimeOffset.Now);
            }
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/single-worker-vs-distributed-workers-69958213/?t=70)

```text
ExecuteAsync loop (while !stoppingToken.IsCancellationRequested)

  GetJobAsync --> job != null ? --> ProcessJobAsync --> Task.Delay(1000)
       ^                                                      |
       +------------------------------------------------------+
```

这种方法对许多内部系统来说已经足够,因为它易于调试和部署。
然而,随着工作负载增加(例如成批涌入的发票处理、AI 管道或缓慢的外部集成),单个 worker 可能会成为瓶颈。
当队列增长的速度快于 worker 消费的速度时,延迟就会增加,系统在最终用户看来就会变慢。

#### 扩展策略

基于队列的架构天然支持水平扩展。
与其增强单个 worker 的能力(垂直扩展,这通常代价高昂且收益递减),不如部署多个相互独立的 worker 副本。

当多个 worker 从同一个队列拉取作业时,它们会同时处理作业。
这会提高**吞吐量**。
例如,如果一个 worker 每分钟处理 10 个作业,那么五个 worker 每分钟就能处理 50 个作业。
需要注意的是,扩展并不会让单个 30 秒的作业运行得更快;它只是让更多 30 秒的作业能够同时进行。

```text
                    +--> Worker 1  (10 jobs/min)
                    +--> Worker 2  (10 jobs/min)
  Queue  -----------+--> Worker 3  (10 jobs/min)   = 50 jobs/min
                    +--> Worker 4  (10 jobs/min)
                    +--> Worker 5  (10 jobs/min)

  每个作业仍然需要 30 秒
```

#### IO 密集型与 CPU 密集型工作负载

扩展的效果取决于工作的性质:
- **IO-Bound**:如果 worker 大部分时间都在等待数据库查询或 HTTP 调用,那么增加更多 worker 会显著提高吞吐量,因为在等待期间 CPU 基本处于空闲状态。
- **CPU-Bound**:如果 worker 执行的是图像处理、PDF 渲染或 AI 推理这类密集型任务,那么在单台机器或单个容器上增加过多 worker 可能会压垮 CPU,形成性能上限。

#### 过渡到分布式系统

从一个 worker 变成多个 worker,会把应用转变为一个分布式系统。
这会引入新的工程挑战,必须在 `GetJobAsync` 和 `ProcessJobAsync` 的实现中加以解决:

```csharp
private async Task<BackgroundJob> GetJobAsync(CancellationToken cancellationToken)
{
    // Simulate getting a job
    await Task.Delay(500, cancellationToken);
    if (logger.IsEnabled(LogLevel.Debug))
    {
        logger.LogDebug("{time}: Job Retrieved", DateTimeOffset.Now);
    }
    return new BackgroundJob();
}

private async Task ProcessJobAsync(BackgroundJob job, CancellationToken cancellationToken)
{
    // Simulate processing a job
    await Task.Delay(1000, cancellationToken);
    if (logger.IsEnabled(LogLevel.Debug))
    {
        logger.LogDebug("{time}: Job Processed", DateTimeOffset.Now);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/single-worker-vs-distributed-workers-69958213/?t=340)

在分布式环境中,认领作业的逻辑必须足够健壮,以防止:
- **Duplicate Processing**:多个 worker 拾取并执行同一个作业。
- **Race Conditions**:多个 worker 同时尝试更新同一个资源时产生的冲突。
- **Resource Contention**:多个 worker 争夺相同的数据库连接或文件句柄。

---

## 2. Concurrency Control

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/concurrency-control-69958214/) · 12:13

### 总结

在分布式后台处理中,并发控制对于防止多个 worker 同时执行同一个作业至关重要。
如果没有适当的保护措施,竞态条件可能导致不一致的系统状态,例如重复付款或重复发送电子邮件。
本课介绍如何使用数据库事务、行级锁和分布式协调来原子地认领工作,从而在管理系统背压的同时,确保任务在多个 worker 实例之间可靠执行。

### 核心概念

*   **Atomic Claiming**:把作业的获取和状态更新合并为单个操作,以防止重复处理。
*   **Race Conditions**:多个 worker 查询数据库,并在任何一个 worker 把作业标记为 'Processing' 之前,都取到了同一个 'Pending' 作业的情形。
*   **Database Transactions**:使用工作单元来确保作业状态转换是原子的,并且在更新失败时可以回滚。
*   **Row-Level Locking**:数据库特定的提示(例如 SQL Server 中的 `UPDLOCK` 和 `ROWLOCK`),在当前事务完成之前阻止其他事务访问某一行。
*   **Distributed Locking**:当需要对非数据库资源进行并发控制时使用的协调机制(例如 Redis、ZooKeeper)。
*   **Backpressure**:通过限制并发来防止压垮 CPU、内存或外部 API 等系统资源的做法。

### 课程笔记

在拥有多个 worker 的分布式系统中,需要协调来确保只有一个 worker 处理某个特定作业。
一种常见的失败模式发生在两个 worker 同时查询数据库中待处理作业的时候。
如果两个 worker 在任何一方更新其状态之前都取到了同一行,那么两者都会尝试处理该工作负载,导致重复执行。

```csharp
var job = await db.BackgroundJobs.Where(x => x.Status == "Pending")
    .OrderByDescending(x => x.CreatedAt)
    .FirstOrDefaultAsync(stoppingToken);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/concurrency-control-69958214/?t=10)

```text
  Worker A                    BackgroundJobs              Worker B
     |  query Status=="Pending"     |                         |
     |----------------------------->|<------------------------|
     |<------ same row -------------|------- same row ------->|
     |                              |                         |
  process job                                            process job
     => 重复执行
```

为了防止这种情况,系统必须原子地认领工作。
不应只是简单地获取一个作业、稍后再更新它,而应当保护获取操作和状态转换(例如从 `Pending` 到 `Processing`)。
通过在作业被拾取时立即把它标记为 `Processing`,其他 worker 就会收到忽略它的信号。

```csharp
_logger.LogInformation("Attempting to transition job to Processing");
var dequeued = await TryTransitionAsync(job.Id, "Pending", "Processing", cancellationToken);
if(dequeued == false)
{
    // Another worker has taken this job, skip processing
    _logger.LogInformation("Failed to transition job to Processing, it may have been taken by another worker");
    return;
}
_logger.LogInformation("Job transitioned to Processing, starting document processing");
await documentProcessor.ProcessAsync(payload.DocumentId, cancellationToken);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/concurrency-control-69958214/?t=100)

#### 事务管理与作用域

有效的并发控制通常依赖于 SQL 事务。
事务确保更新是原子的。
然而,在 .NET 中实现这一点时,开发者必须小心依赖注入的作用域。
如果事务是在外层作用域中开启的,而数据库更新发生在另一个作用域中(使用的是不同的 `DbContext` 实例),那么这次更新就不会成为该事务的一部分。

推荐的做法是让负责数据库交互的逻辑自己拥有事务。
在下面的 `TryTransitionAsync` 实现中,该方法创建自己的作用域并开启一个事务,以确保状态更新和转换历史的记录一起发生。

```csharp
public async Task<bool> TryTransitionAsync(string jobId, string expectedStatus, string nextStatus, CancellationToken cancellationToken)
{
    using var scope = _scopeFactory.CreateScope();
    var db = scope.ServiceProvider.GetService<AppDbContext>();
    using var transaction = await db.Database.BeginTransactionAsync(cancellationToken);

    var affectedRows = await db.BackgroundJobs.Where(x => x.Id == jobId && x.Status == expectedStatus)
        .ExecuteUpdateAsync(x => x.SetProperty(p => p.Status, nextStatus), cancellationToken);

    if(affectedRows == 1)
    {
        var jobTransition = new BackgroundJobTransition
        {
            Id = Guid.NewGuid().ToString(),
            JobId = jobId,
            FromStatus = expectedStatus,
            ToStatus = nextStatus,
            TransitionedAt = DateTime.UtcNow
        };

        db.BackgroundJobTransitions.Add(jobTransition);
        await db.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
    }
    else
    {
        await transaction.RollbackAsync(cancellationToken);
    }

    return affectedRows == 1;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/concurrency-control-69958214/?t=475)

```text
TryTransitionAsync(jobId, expectedStatus, nextStatus)
  CreateScope -> AppDbContext -> BeginTransactionAsync
  |
  ExecuteUpdateAsync  WHERE Id == jobId && Status == expectedStatus
  |
  +-- affectedRows == 1 --> BackgroundJobTransitions.Add
  |                         SaveChangesAsync -> CommitAsync  -> true
  |
  +-- otherwise ----------> RollbackAsync                    -> false
```

#### 优化 Worker 循环

当一个 worker 因为另一个 worker 已经转换了作业状态而未能认领该作业时,这个 worker 不应退出。
在 `ExecuteAsync` 循环中使用 `return` 语句会终止后台服务。
相反,worker 应当使用 `continue` 回到循环开头,去寻找下一个可用的作业。
此外,作业获取应当使用 `OrderBy` 而不是 `OrderByDescending`,以确保作业按照创建的顺序处理(先进先出)。

```csharp
protected async override Task ExecuteAsync(CancellationToken stoppingToken)
{
    while (!stoppingToken.IsCancellationRequested)
    {
        _workerHeartbeatService.Beat();
        using var scope = _scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetService<AppDbContext>();

        var job = await db.BackgroundJobs.Where(x => x.Status == "Pending")
            .OrderBy(x => x.CreatedAt)
            .FirstOrDefaultAsync(stoppingToken);

        if(job is not null)
        {
            var dequeued = await TryTransitionAsync(job.Id, "Pending", "Processing", stoppingToken);
            if (dequeued == false)
            { 
                await Task.Delay(3000);
                continue;
            }
        }

        try
        {
            await ProcessJobAsync(job, stoppingToken);
            _workerHeartbeatService.Beat();
            await Task.Delay(3000);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing job {JobId}", job?.Id);
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/concurrency-control-69958214/?t=535)

#### 高级锁定与背压

对于 SQL Server 或 PostgreSQL 这类数据库,可以使用提示显式地请求行级锁。
这会阻止其他事务在当前事务完成之前读取该行。

```sql
BEGIN TRANSACTION;

SELECT TOP (1) *
FROM SalesLT.Customer WITH (UPDLOCK, ROWLOCK)
WHERE CustomerID = 1;

-- do some work here

COMMIT;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/concurrency-control-69958214/?t=550)

锁和事务负责管理数据库层面的并发,而分布式系统可能还需要分布式锁(通过 Redis 或 ZooKeeper)来协调报表生成或外部 API 轮询之类的任务。

最后,区分并发与性能非常重要。
无限制地提高并发可能会压垮系统资源(CPU、内存、线程池),并使系统变得不稳定。
高质量的分布式系统会施加背压和并发限制,以维持稳定的吞吐量。

---

## 3. Integrating Message Brokers

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/integrating-message-brokers-69958215/) · 7:07

### 总结

消息代理(message broker)是分布式系统中必不可少的中间件,它促成异步通信并解耦服务。
通过突破数据库支撑的队列的局限(例如轮询开销和争用),RabbitMQ 和 Apache Kafka 这类代理能够实现更高的可扩展性、灵活的路由以及健壮的事件驱动架构。
本课探讨传统工作队列与基于日志的事件流之间的架构差异,为针对特定后台处理需求选择合适的消息技术打下基础。

### 核心概念

* **Service Decoupling**:消息代理让服务之间无需直接依赖即可通信,这意味着发送方不需要知道消费者的位置、状态或数量。
* **Database Queue Limitations**:数据库支撑的队列对简单系统很有效,但最终会遭遇轮询开销、数据库争用和有限的路由能力等问题。
* **RabbitMQ (AMQP)**:一个用途广泛的代理,针对传统工作队列、任务分发以及使用各种交换器类型的复杂路由进行了优化。
* **Apache Kafka (Event Streaming)**:一个基于日志的系统,专为高吞吐量的事件持久化和可重放性而设计,消息在被消费后仍保留在日志中。
* **Reliability and Acknowledgments**:显式的消费者确认等机制,确保在消费者处理过程中失败时消息会被重新投递。

### 课程笔记

#### 消息代理的作用

随着系统向分布式架构过渡,后台处理往往需要的不仅仅是简单的数据库查询。
消息代理充当中间件,使服务能够异步地交换消息。
Service A 不再直接调用 Service B,而是两者都通过代理进行通信。
这种解耦确保发送方与消费者的实现细节、可用性和规模相互隔离,显著提高了系统的灵活性。

```text
  直接调用:   Service A ------------------------> Service B

  通过代理:   Service A ---> Message Broker ---> Service B
```

#### 数据库支撑队列的局限

数据库队列是常见的起点,但它们最终会遇到扩展瓶颈。
这些瓶颈包括:
* **Polling Overhead**:不断查询数据库以检查是否有新工作。
* **Contention**:多个 worker 争夺表中的同一些行。
* **Limited Routing**:难以在多个服务之间实现复杂的消息分发模式。

专用代理正是为这类消息工作负载而设计的,在高并发场景下比通用数据库更高效。

#### RabbitMQ 与传统工作队列

RabbitMQ 是一个被广泛采用的代理,基于 AMQP(Advanced Message Queuing Protocol)。
它对后台作业、任务分发和微服务编排尤其有效。
RabbitMQ 模型依赖四个主要组件:
1. **Producers**:发送消息的服务。
2. **Exchanges**:路由引擎,从生产者接收消息,并根据特定规则把它们导向队列(例如 Direct、Topic、Fan-out 或 Header 交换器)。
3. **Queues**:缓冲区,存储消息直到它们被消费。
4. **Consumers**:处理消息的服务。

```text
  Producers --> Exchanges --(rules: Direct/Topic/Fan-out/Header)--> Queues
                                                                      |
                                                                      v
                                                                  Consumers
```

RabbitMQ 的核心特性之一是确认系统。
如果消费者在发送确认之前崩溃,RabbitMQ 可以把消息重新投递给另一个消费者,从而为事务性业务逻辑提供高可靠性。

#### Apache Kafka 与事件流

Apache Kafka 采用一种基于日志的模型,与传统队列有显著不同。
它把事件存储在称为 topic 的只追加日志中。
与 RabbitMQ 中消息通常在被消费后即被移除不同,Kafka 会保留消息,从而实现**可重放性**。
这是重建投影、恢复系统状态或为分析重新处理历史数据的关键特性。

Kafka 针对极高的吞吐量和水平扩展进行了优化。
它使用消费者组,让多个消费者通过处理单个 topic 的不同分区来协作。
这使它成为遥测管道、事件溯源和实时流分析的首选。

#### 选择合适的代理

在 RabbitMQ 和 Kafka 之间如何选择取决于具体的用例:
* **RabbitMQ** 非常适合工作队列、事务性任务,以及需要低延迟消息和复杂路由的场景。
* **Kafka** 更适合事件流、大规模分析,以及需要持久、可重放事件历史的系统。

其他云原生和专用的选项包括 Azure Service Bus、Amazon SQS、Redis Streams 和 ActiveMQ,它们在持久性、顺序和运维复杂度方面各有不同的取舍。

---

## 4. RabbitMQ

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/rabbitmq-69958216/) · 13:48

### 总结

本课为在 .NET 中使用 RabbitMQ 进行后台处理提供了实践性的入门,内容包括通过 Docker 搭建本地实例,以及使用 RabbitMQ.Client 库实现生产者和消费者。
它解释了 RabbitMQ 的核心组件,包括连接、通道、队列和交换器,同时演示了如何声明具有特定属性的队列、发布字节编码的消息,以及使用事件处理器异步地消费消息。

### 核心概念

*   **Docker Setup**:开发时推荐在 Docker 中运行 RabbitMQ,暴露端口 5672 用于 AMQP 流量,端口 15672 用于 Management Portal。
*   **Management Portal**:一个 Web 界面(默认凭据:`guest`/`guest`),用于监控连接、通道和队列。
*   **Connections and Channels**:连接代表到代理的 TCP 链路,而通道是连接内部的虚拟连接,大多数 API 操作都在通道上进行。
*   **Queue Declaration**:队列通过 `durable`(在代理重启后仍然存在)、`exclusive`(仅限一个连接使用)和 `autoDelete`(在最后一个消费者取消订阅时删除)等属性来定义。
*   **Exchanges and Routing Keys**:消息被发布到交换器(默认交换器使用空字符串),并通过路由键路由到队列。
*   **Message Encoding**:RabbitMQ 把消息当作字节数组处理,因此在发布之前需要序列化(例如 UTF-8 字符串编码或 JSON)。
*   **Acknowledgements (ACK)**:用于确认消息已被处理的机制,这样它就可以安全地从队列中移除。

### 课程笔记

#### 环境搭建

RabbitMQ 通常使用 Docker 在本地运行。
该实例暴露两个主要端口:**5672** 用于客户端使用的 AMQP 协议,**15672** 用于 RabbitMQ Management Portal。
该门户让开发者可以查看活动连接、通道以及各个队列的状态。
默认情况下,该门户的用户名和密码都是 `guest`。

```text
             +--------------------------+
  clients -->| 5672   AMQP              |
             |        RabbitMQ (Docker) |
  browser -->| 15672  Management Portal | guest / guest
             +--------------------------+
```

#### 建立连接

要在 .NET 中与 RabbitMQ 交互,需要安装 `RabbitMQ.Client` NuGet 包。
第一步是用合适的 `HostName`(例如 `localhost`)配置一个 `ConnectionFactory`。
然后用这个工厂异步地创建一个连接和一个通道。

```csharp
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQIntro
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/rabbitmq-69958216/?t=220)

```text
ConnectionFactory (HostName = "localhost")
  |
  +-- CreateConnectionAsync() --> connection   (TCP 链路)
                                    |
                                    +-- CreateChannelAsync() --> channel
                                        (连接内部的虚拟连接)
```

#### 声明队列

在发布或消费之前,必须先在通道上声明一个队列。
`QueueDeclareAsync` 方法定义队列的名称及其行为:
*   **Durable**:如果为 `false`,队列在代理重启后不会保留。
*   **Exclusive**:如果为 `true`,队列仅限当前连接使用。
*   **AutoDelete**:如果为 `true`,队列会在最后一个消费者断开连接后被删除。

```csharp
await channel.QueueDeclareAsync(
    queue: "invoice-jobs",
    durable: false,
    exclusive: false,
    autoDelete: true);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/rabbitmq-69958216/?t=235)

#### 发布消息

RabbitMQ 要求消息以字节数组的形式发送。
生产环境的应用通常使用结构化的 JSON,而简单的字符串可以用 `Encoding.UTF8.GetBytes` 进行转换。
消息使用 `BasicPublishAsync` 发布,它需要一个交换器名称(默认交换器是空字符串)和一个路由键(使用默认交换器时,路由键对应队列名称)。

```csharp
var message = "Generate invoice with Id 12345";
var body = Encoding.UTF8.GetBytes(message);

await channel.BasicPublishAsync(
    exchange: "",
    routingKey: "invoice-jobs",
    basicProperties: new BasicProperties(),
    body: body,
    mandatory: true
);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/rabbitmq-69958216/?t=460)

#### 消费消息

要消费消息,使用 `AsyncEventingBasicConsumer`。
这个消费者附加到一个通道上,并提供一个 `ReceivedAsync` 事件处理器,每当有消息到达时就会触发。
在处理器内部,字节数组会被转换回其原始格式。
要启动消费过程,调用 `BasicConsumeAsync`,并指定是否自动确认(`autoAck`)消息。

```csharp
var consumer = new AsyncEventingBasicConsumer(channel);

consumer.ReceivedAsync += (model, args) =>
{
    var body = args.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine(message);
    return Task.CompletedTask;
};

await channel.BasicConsumeAsync(
    queue: "invoice-jobs",
    autoAck: true,
    consumer: consumer
);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/rabbitmq-69958216/?t=735)

```text
"Generate invoice with Id 12345"
  | Encoding.UTF8.GetBytes
  v
BasicPublishAsync(exchange: "", routingKey: "invoice-jobs")
  |
  v
queue "invoice-jobs"
  |
  v
AsyncEventingBasicConsumer.ReceivedAsync
  | args.Body.ToArray() -> Encoding.UTF8.GetString
  v
Console.WriteLine(message)
```

---

## 5. Apache Kafka

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/apache-kafka-69958217/) · 11:48

### 总结

Apache Kafka 是一个分布式事件流平台,专为高吞吐量、持久的事件存储和独立消费而设计。
与 RabbitMQ 这类专注于工作分发和作业队列的传统消息代理不同,Kafka 把关于"发生了什么"的事实记录在一个只追加日志中,从而实现可重放性和多系统事件响应等强大特性。
本课演示如何使用 Confluent.Kafka 客户端把 Kafka 集成到 .NET worker service 中,以生产和消费事件,并强调从面向任务的思维方式向事件驱动架构的转变。

### 核心概念

- **Distributed Event Streaming Platform**:设计用于存储事件流,并允许不同的消费者独立读取。
- **Topic**:存储在只追加日志中的一个具名事件流。
- **Producer**:把事件写入特定 topic 的客户端。
- **Consumer**:从 topic 读取事件的客户端。
- **Consumer Group**:一种允许多个消费者分担处理某个 topic 的工作的机制,从而实现水平扩展。
- **Replayability**:消费者从日志中重新读取历史事件的能力,这对审计轨迹、分析和重建状态很有用。
- **Append-only Log**:Kafka 按顺序存储事件,并在配置的期限内保留它们,而不是在被消费后立即删除。

### 课程笔记

RabbitMQ 主要用于工作分发(确保某个作业被一个 worker 处理),而 Apache Kafka 用于事件流(记录某个事件已经发生,以便多个系统可以做出响应)。
在 Kafka 中,事件被写入 topic 并被保留,从而支持独立消费和可重放性。

要把 Kafka 集成到 .NET 应用中,使用 `Confluent.Kafka` NuGet 包。
这是与 Apache Kafka 集群交互的标准客户端。

#### 实现生产者

要生产事件,必须先定义一个包含 `BootstrapServers` 地址的 `ProducerConfig`。
然后使用 `ProducerBuilder` 创建生产者实例。
在这个示例中,我们使用基于字符串的键和值。

```csharp
using Confluent.Kafka;

namespace WorkerServiceExample
{
    public class Worker(ILogger<Worker> logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };

            using var producer = new ProducerBuilder<string, string>(config).Build();

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/apache-kafka-69958217/?t=265)

生产者构建好之后,就可以创建一条消息。
一条 Kafka 消息通常由一个键(用于分区和标识)和一个值(事件数据,通常序列化为 JSON)组成。

```csharp
var invoiceEvent = new
{
    InvoiceId = Guid.NewGuid(),
    Amount = new Random().Next(100, 1000),
    Supplier = "Amazon"
};

var message = new Message<string, string>
{
    Key = invoiceEvent.InvoiceId.ToString(),
    Value = System.Text.Json.JsonSerializer.Serialize(invoiceEvent)
};

await producer.ProduceAsync("invoice-created", message, stoppingToken);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/apache-kafka-69958217/?t=355)

#### 实现消费者

消费事件需要一个 `ConsumerConfig`。
关键属性包括 `GroupId`(标识该消费者在某个扩展组中的成员身份)和 `AutoOffsetReset`(决定在没有记录先前偏移量时消费者从哪里开始读取,例如 `Earliest` 表示从日志的开头读取)。

```csharp
var consumerConfig = new ConsumerConfig
{
    BootstrapServers = "localhost:9092",
    GroupId = "worker-service-group",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
consumer.Subscribe("invoice-created");

while (!stoppingToken.IsCancellationRequested)
{
    var result = consumer.Consume(stoppingToken);
    if (result?.Message != null)
    {
        logger.LogInformation("Received message: {Message}", result.Message.Value);
    }

    await Task.Delay(1000, stoppingToken);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/apache-kafka-69958217/?t=430)

```text
Producer                      topic "invoice-created"         Consumer
ProduceAsync(message) --> [ e1 | e2 | e3 | ... ] --> Subscribe + Consume
  Key   = InvoiceId           只追加日志               GroupId =
  Value = JSON                                         "worker-service-group"
                                                       AutoOffsetReset =
                                                       Earliest
```

#### 可重放性与用例

Kafka 最强大的特性之一是可重放性。
因为事件被保留在只追加日志中,所以可以配置一个新的消费者组从 topic 的最开头读取。
这与 RabbitMQ 有显著不同,在 RabbitMQ 中消息通常在被确认后就会被删除。

```csharp
namespace WorkerServiceExample

public class Worker(ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "worker-service-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
        consumer.Subscribe("invoice-created");

        while (!stoppingToken.IsCancellationRequested)
        {
            var result = consumer.Consume(stoppingToken);
            if (result != null)
            {
                logger.LogInformation("Received message: {Message}", result.Message.Value);
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/apache-kafka-69958217/?t=580)

Kafka 非常适合:
- 分析管道和遥测。
- 审计轨迹。
- 事件驱动的微服务。
- 事件溯源和重建投影。

对于简单的后台作业队列,RabbitMQ 或 Azure Service Bus 通常更易于运维。
Kafka 在 topic 设计、消息顺序和部署拓扑方面引入了运维复杂度,这些都必须在生产环境中加以管理。

---

## 6. Putting it all Together

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/putting-it-all-together-69958218/) · 4:08

### 总结

本课通过 RabbitMQ 把一个 ASP.NET API 与一个 Worker Service 集成起来,演示了一个完整的分布式后台处理工作流。
它展示了生产者-消费者模式:API 端点接受请求,把工作卸载到消息队列,并立即返回响应,而一个独立的后台 worker 消费这些消息来执行诸如发送电子邮件通知之类的任务。

### 核心概念

- 使用消息队列把 API 逻辑与长时间运行的任务解耦。
- 使用 RabbitMQ 的 `IBasicPublishAsync` 实现生产者。
- 在 `BackgroundService` 中使用 `AsyncEventingBasicConsumer` 实现消费者。
- 对异步操作使用 `202 Accepted` HTTP 状态码。
- 对作业数据进行序列化和反序列化,以便通过网络传输。

### 课程笔记

本课演示了一个使用 RabbitMQ 把 API 与后台 worker 解耦的分布式系统的实际实现。
这个示例聚焦于一个订单更新系统,其中状态变更会触发电子邮件通知。

#### API 生产者实现

该 API 提供一个最小端点 `POST /order-update/{status}`。
被调用时,它会创建一个 `BackgroundJob` 对象,其中包含一个唯一 ID、状态类型和一个时间戳。
然后它使用 `ConnectionFactory` 建立到本地 RabbitMQ 实例的连接。

```csharp
namespace APIProject
public class Program
    public static void Main(string[] args)
        app.MapPost("/order-update/{status}",
            async (
            CancellationToken cancellationToken,
            string status) =>
            {
                var job = new APIProject.Models.BackgroundJob()
                {
                    Id = Guid.NewGuid().ToString(),
                    Type = status,
                    CreatedAt = DateTime.UtcNow
                };

                var factory = new ConnectionFactory()
                {
                    HostName = "localhost"
                };

                using var connection = await factory.CreateConnectionAsync();
                using var channel = await connection.CreateChannelAsync();

                await channel.QueueDeclareAsync(
                    queue: "order-updates",
                    durable: false,
                    exclusive: false,
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/putting-it-all-together-69958218/?t=40)

该端点通过调用 `QueueDeclareAsync` 确保 `order-updates` 队列存在。
队列就绪后,作业对象被序列化为 JSON,转换为字节数组,并使用 `BasicPublishAsync` 发布到交换器。

```csharp
public static void Main(string[] args)
        app.MapPost("/order-update/{status}",
            async ( CancellationToken cancellationToken, string status) =>
            await channel.QueueDeclareAsync(
                autoDelete: false);

            var message = JsonSerializer.Serialize(job);
            var body = Encoding.UTF8.GetBytes(message);

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: "order-updates",
                basicProperties: new BasicProperties(),
                body: body,
                mandatory: true
            );

            return Results.Accepted($"/background-jobs/{job.Id}");
        });
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/putting-it-all-together-69958218/?t=55)

通过返回 `Results.Accepted`,API 告知客户端请求已被接收并已排队等待处理,这让 API 无论实际后台任务需要多长时间都能保持响应。

#### Worker Service 消费者实现

`EmailNotificationService` 是一个继承自 `BackgroundService` 的专用 Worker Service。
在它的 `ExecuteAsync` 方法中,它连接到 RabbitMQ,并设置一个 `AsyncEventingBasicConsumer` 来处理传入的消息。

```csharp
namespace EmailNotificationService
    public class Worker(ILogger<Worker> logger) : BackgroundService
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += (model, args) =>
            {
                var body = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var job = JsonSerializer.Deserialize<BackgroundJob>(message);
                SendEmail(job);
                return Task.CompletedTask;
            };
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/putting-it-all-together-69958218/?t=70)

消费者通过 `BasicConsumeAsync` 注册到 `order-updates` 队列。
当收到消息时,事件处理器会反序列化该消息,并触发电子邮件通知逻辑。

```csharp
namespace EmailNotificationService
    public class Worker(ILogger<Worker> logger) : BackgroundService
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
            using var channel = await connection.CreateChannelAsync();

            var consumer = new AsyncEventingBasicConsumer(channel);

            // ... consumer event handler setup ...

            while (!stoppingToken.IsCancellationRequested)
            {
                await channel.BasicConsumeAsync(
                    queue: "order-updates",
                    autoAck: true,
                    consumer: consumer
                );
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/putting-it-all-together-69958218/?t=85)

```text
Client
  | POST /order-update/{status}
  v
APIProject                                    EmailNotificationService
  BackgroundJob { Id, Type, CreatedAt }         (BackgroundService)
  JsonSerializer.Serialize                      AsyncEventingBasicConsumer
  BasicPublishAsync --> queue "order-updates" --> ReceivedAsync
  |                                               JsonSerializer.Deserialize
  v                                               SendEmail(job)
202 Accepted  /background-jobs/{job.Id}
```

这种解耦的架构让系统得以扩展;可以部署 worker service 的多个实例来处理大量的订单更新,而不会影响 API 的性能。
