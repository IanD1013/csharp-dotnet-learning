# Building a Job Queue

> 课程:[From Zero to Hero: Background Processing in .NET](https://dometrain.com/course/from-zero-to-hero-background-processing-in-dotnet/) · 第 5 章
> 共 3 课 · 约 59:52
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

---

## 课程索引

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [The Synchronous Baseline - What Not To Do](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/the-synchronous-baseline-what-not-to-do-69958193/) | 16:20 | [↓](#1-the-synchronous-baseline---what-not-to-do) |
| 2 | [Implementing a Simple Job Queue](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/implementing-a-simple-job-queue-69958194/) | 28:21 | [↓](#2-implementing-a-simple-job-queue) |
| 3 | [Persisting Jobs](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/persisting-jobs-69958195/) | 15:11 | [↓](#3-persisting-jobs) |

---

## 1. The Synchronous Baseline - What Not To Do

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/the-synchronous-baseline-what-not-to-do-69958193/) · 16:20

### 总结

本课探讨在 API 请求管道内直接执行长时间运行工作流的种种陷阱。
虽然使用 async/await 的同步实现写起来很简单,但它会导致糟糕的用户体验、资源耗尽以及含糊不清的失败状态。
本课建立了一个"不该怎么做"的基线,并引出向后台处理的架构转变:API 只负责接受工作并返回一个作业标识符,实际的处理则独立进行。

### 核心概念

* **Request-Response Coupling**:在一个多步骤业务流程的整个持续期间都让 HTTP 请求保持打开。
* **Async vs. Background Processing**:理解 `await` 会把线程释放回线程池,但并不会把请求生命周期与工作解耦。
* **Ambiguous Failures**:当一个多步骤流程部分失败时(例如处理成功但通知失败),很难确定系统的状态。
* **Idempotency**:要求同一操作执行多次(通常由客户端重试引起)与执行一次的效果相同。
* **Operational Visibility**:监控、查询和管理进行中工作的能力,而当工作被"困"在请求线程中时,这一点很难做到。
* **Scalability Coupling**:当处理资源与 Web 服务器托管在同一管道中时,无法独立于 Web 服务器扩展处理资源。

### 课程笔记

在一个典型的文档处理工作流中,用户可能会上传一个需要若干步骤的文件:存储、处理、数据提取、保存结果以及发送通知。
一种常见但危险的本能做法是直接在 API 控制器的 action 里执行所有这些步骤。

```csharp
namespace APIProject.Controllers
{
    public class DocumentProcessingController(DocumentService documentService,
        DocumentProcessorService documentProcessor,
        NotificationService notificationService) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> ProcessDocument(
            DocumentUpload upload,
            CancellationToken cancellationToken)
        {
            var document = await documentService.SaveAsync(
                upload,
                cancellationToken);

            var result = await documentProcessor.ProcessAsync(
                document.Id,
                cancellationToken);

            await documentService.SaveResultAsync(
                document.Id,
                result,
                cancellationToken);

            await notificationService.SendProcessingCompleteAsync(
                document.Id,
                cancellationToken);

            return Ok(new
            {
                DocumentId = document.Id,
                Status = "Processed"
            });
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/the-synchronous-baseline-what-not-to-do-69958193/?t=100)

虽然这段代码可读性好,也正确地使用了 `async/await` 来管理线程,但出于以下几个原因,它并不适合用于生产环境:

1.  **User Latency**:用户必须等待整个工作流完成。
    如果处理器耗时 30 秒,API 响应就要耗时 30 秒。
2.  **HTTP Fragility**:长时间运行的请求容易遭遇来自浏览器、反向代理、负载均衡器或 API 网关的超时。
    请求保持打开的时间越长,它就越脆弱。
3.  **Resource Exhaustion**:每个打开的请求都会消耗服务器资源。
    在高负载下,这些长时间运行的请求可能会耗尽 Web 服务器的可用容量。
4.  **Ambiguous Failures**:如果 `notificationService` 在文档已经处理并保存之后失败,客户端会收到 500 Internal Server Error。
    客户端并不知道主要工作其实已经成功了。
5.  **Retry Risks**:如果用户重试一个失败或超时的请求,可能会触发重复处理、重复通知或重复的数据库记录。
    这就需要复杂的幂等性逻辑。
6.  **Lack of Visibility**:没有简便的方法查询系统,看看当前有多少文档正在处理、哪些已经失败,因为状态保存在请求的执行过程中。

#### 过渡到后台处理

为了解决这些问题,架构必须从执行工作转变为接受工作。
服务不应再提供同步的 `ProcessAsync` 方法,而应提供一种登记作业的方式。

```csharp
using APIProject.Models;

namespace APIProject.Services
{
    public class DocumentProcessorService
    {
        public async Task<Document> ProcessAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> CreateDocProcessingJob(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/the-synchronous-baseline-what-not-to-do-69958193/?t=820)

随后控制器被修改为返回 `Accepted`(HTTP 202)状态码。
这会告知客户端请求是有效的、工作已经排队,并提供一个可用于后续状态跟踪的 `JobId`。

```csharp
namespace APIProject.Controllers
{
    public class AsyncDocument_ProcessingController(DocumentService documentService,
        DocumentProcessorService documentProcessor) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> ProcessDocument(
            DocumentUpload upload,
            CancellationToken cancellationToken)
        {
            var document = await documentService.SaveAsync(
                upload,
                cancellationToken);

            var jobId = await documentProcessor.CreateDocProcessingJob(document.Id, cancellationToken);

            return Accepted(new
            {
                DocumentId = document.Id,
                JobId = jobId,
                Status = "Queued"
            });
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/the-synchronous-baseline-what-not-to-do-69958193/?t=865)

在这种模型中,API 只负责校验请求并记录执行工作的意图。
实际的执行则交给某种后台机制,例如 Hosted Service、Worker Service 或专门的作业队列。

```text
 DocumentProcessingController      AsyncDocument_ProcessingController

 Client --> API                    Client --> API
   SaveAsync                         SaveAsync
   ProcessAsync                      CreateDocProcessingJob
   SaveResultAsync                 Client <-- Accepted (HTTP 202)
   SendProcessingCompleteAsync       { DocumentId, JobId,
 Client <-- Ok                         Status = "Queued" }
   { DocumentId,                          |
     Status = "Processed" }               v
                                   Hosted Service / Worker Service /
                                   job queue 执行实际工作
```

---

## 2. Implementing a Simple Job Queue

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/implementing-a-simple-job-queue-69958194/) · 28:21

在同步设计中,API 接受一个文档、处理它、保存结果并发送通知,然后才返回响应。
这会让用户一直等待,使失败变得含糊不清,并使重试变得危险。
本课引入一个简单的作业队列,以摆脱这种设计。

### 总结

本课演示如何在 .NET 中从同步处理模型过渡到异步作业队列架构。
通过引入作业模型、基于 System.Threading.Channels 的队列抽象以及一个后台 worker,API 可以把昂贵的任务卸载出去,并立即返回 HTTP 202 Accepted 响应。
这种方式提供了清晰的移交点,并通过有界 channel 实现背压(backpressure),从而改善用户体验和系统稳定性。

### 核心概念

*   **Asynchronous Handoff**:把请求的接受与工作的执行解耦。
*   **Job Modeling**:为工作创建一个结构化表示,包括唯一 ID、类型以及序列化后的负载。
*   **Producer-Consumer Pattern**:使用一个共享队列,由 API(生产者)添加工作,由后台服务(消费者)处理工作。
*   **Bounded Channels**:使用固定容量的 `System.Threading.Channels` 来实现背压,防止内存耗尽。
*   **HTTP 202 Accepted**:适用于请求已被接受处理但尚未完成的情况的状态码。
*   **Dependency Injection Scoping**:在单例后台服务中手动创建服务作用域,以解析数据库上下文之类的 scoped 依赖。

### 课程笔记

#### 同步基线与 Fire-and-Forget 陷阱

在同步工作流中,HTTP 请求要为整个长时间运行的流程负责。
如果流程在中途失败,对客户端来说状态就变得含糊不清。

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/implementing-a-simple-job-queue-69958194/?t=10)

开发者常常试图用 `Task.Run` 来解决这个问题:发起一个任务并立即返回响应。
然而这在 Web 应用中是有风险的,因为它无法控制并发任务的数量,不处理干净的关闭,还会让依赖注入的协调变得复杂。
如果应用被回收或崩溃,工作就会丢失,而且不留任何痕迹。

```csharp
[HttpPost]
public async Task<IActionResult> ProcessDocumentAsTask(
    DocumentUpload upload,
    CancellationToken cancellationToken)
{
    var document = await documentService.SaveAsync(
        upload,
        cancellationToken);

    _ = Task.Run(async() =>
    {
        await documentProcessor.ProcessAsync(document.Id, cancellationToken);
    });

    return Accepted();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/implementing-a-simple-job-queue-69958194/?t=135)

#### 定义作业模型

一个像样的队列需要一个模型来表示工作。
`BackgroundJob` 模型包含一个唯一标识符、一个类型(让 worker 知道该做什么)、一个包含工作所需数据(例如文档 ID)的序列化负载,以及一个时间戳。

```csharp
namespace APIProject.Models
{
    public class BackgroundJob
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Payload { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/implementing-a-simple-job-queue-69958194/?t=235)

#### 创建队列抽象

一个接口定义了添加和取出作业的操作。
对于这些异步操作,更推荐使用 `ValueTask`,以便在高吞吐场景下减少内存分配。

```csharp
using APIProject.Models;

namespace APIProject.Interfaces
{
    public interface IBackgroundJobQueue
    {
        ValueTask QueueAsync(BackgroundJob job, CancellationToken cancellationToken = default);
        ValueTask<BackgroundJob?> DequeueAsync(CancellationToken cancellationToken = default);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/implementing-a-simple-job-queue-69958194/?t=370)

#### 实现内存队列

对于内存中的生产者-消费者队列,`System.Threading.Channels` 是一个极好的选择。
这里使用一个**有界 channel** 来限制容量(例如 100 个条目)。
这引入了背压:如果队列已满,`QueueAsync` 会等待出现可用空间,而不是任由应用消耗无限的内存。

```csharp
namespace APIProject.Services
{
    public class BackgroundJobQueue : IBackgroundJobQueue
    {
        private readonly Channel<BackgroundJob> _queue;

        public BackgroundJobQueue()
        { 
            var options = new BoundedChannelOptions(100)
            {
                FullMode = BoundedChannelFullMode.Wait,
                SingleReader = false,
                SingleWriter = false
            };

            _queue = Channel.CreateBounded<BackgroundJob>(options);
        }

        public async ValueTask<BackgroundJob?> DequeueAsync(CancellationToken cancellationToken = default)
        { 
            return await _queue.Reader.ReadAsync(cancellationToken);
        }

        public async ValueTask QueueAsync(BackgroundJob job, CancellationToken cancellationToken = default)
        { 
            await _queue.Writer.WriteAsync(job, cancellationToken);
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/implementing-a-simple-job-queue-69958194/?t=640)

```text
 Producer (API)                        Consumer (worker)
 QueueAsync                            DequeueAsync
   _queue.Writer.WriteAsync              _queue.Reader.ReadAsync
     |                                     ^
     v                                     |
 +---------------------------------------------------------+
 |  Channel<BackgroundJob>   BoundedChannelOptions(100)    |
 |  [job][job][job] ...                                    |
 +---------------------------------------------------------+
   队列已满 -> FullMode = BoundedChannelFullMode.Wait
               QueueAsync 等待出现可用空间
```

#### 注册并使用队列

队列必须注册为 **Singleton**,因为它代表的是共享的应用级基础设施。
如果注册为 transient,生产者和消费者就会拿到不同的队列实例。

```csharp
builder.Services.AddSingleton<IBackgroundJobQueue, BackgroundJobQueue>();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/implementing-a-simple-job-queue-69958194/?t=715)

API 控制器被更新为:保存初始记录、创建一个作业,并把它加入队列。
它返回 HTTP 202 Accepted 响应,表明请求有效,但工作仍在等待处理。

```csharp
[HttpPost]
public async Task<IActionResult> ProcessDocument(
    DocumentUpload upload,
    CancellationToken cancellationToken)
{
    var document = await documentService.SaveAsync(upload, cancellationToken);
    var jobId = Guid.NewGuid();

    var payload = JsonSerializer.Serialize(new { DocumentId = document.Id });

    var job = new BackgroundJob()
    {
        Id = jobId,
        Type = "ProcessDocument",
        Payload = payload,
        CreatedAt = DateTime.UtcNow
    };

    await backgroundJobQueue.QueueAsync(job, cancellationToken);

    return Accepted(new
    {
        DocumentId = document.Id,
        JobId = jobId,
        Status = "Queued"
    });
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/implementing-a-simple-job-queue-69958194/?t=865)

#### 实现后台 Worker

一个 `BackgroundService`(hosted service)持续运行以处理作业。
由于 worker 是单例,它无法直接注入 scoped 服务(例如数据库上下文)。
取而代之,它注入 `IServiceScopeFactory`,为每个作业创建一个新的作用域,从而保证依赖管理的干净。

```csharp
public class QueuedJobWorker : BackgroundService
{
    private readonly IBackgroundJobQueue _jobQueue;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<QueuedJobWorker> _logger;

    public QueuedJobWorker(IBackgroundJobQueue jobQueue, IServiceScopeFactory scopeFactory, ILogger<QueuedJobWorker> logger)
    {
        _jobQueue = jobQueue;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var job = await _jobQueue.DequeueAsync(stoppingToken);
            try
            {
                await ProcessJobAsync(job!, stoppingToken);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error processing job {JobId}", job?.Id);
            }
        }
    }

    private async Task ProcessJobAsync(BackgroundJob job, CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        switch (job.Type)
        {
            case "ProcessDocument":
                var payload = JsonSerializer.Deserialize<ProcessDocumentPayload>(job.Payload);
                var processor = scope.ServiceProvider.GetRequiredService<DocumentProcessorService>();
                await processor.ProcessAsync(payload.DocumentId, cancellationToken);
                break;
            default:
                throw new InvalidOperationException($"Unknown job type: {job.Type}");
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/implementing-a-simple-job-queue-69958194/?t=1300)

```text
 QueuedJobWorker (Singleton, BackgroundService)
   ExecuteAsync: while (!stoppingToken.IsCancellationRequested)
     |
     +-- _jobQueue.DequeueAsync(stoppingToken)      --> job
     +-- ProcessJobAsync(job)
           using var scope = _scopeFactory.CreateScope()   // 每个作业一个
           switch (job.Type)
             "ProcessDocument" --> Deserialize<ProcessDocumentPayload>
                                   GetRequiredService<DocumentProcessorService>
                                   ProcessAsync(payload.DocumentId)
             default           --> InvalidOperationException
     +-- catch --> _logger.LogError("Error processing job {JobId}")
```

#### 内存队列的局限

虽然这种架构是一次重大改进,但它有几个关键的局限:
1.  **Volatility**:如果应用停止或崩溃,所有已排队的作业都会丢失。
2.  **No Scaling**:内存队列只属于本地实例;多个服务器实例看不到彼此的队列。
3.  **No Persistence**:没有可靠的方法来跨重启跟踪作业历史或管理重试。

这些局限为后续课程中引入持久化的、基于数据库的队列做好了铺垫。

---

## 3. Persisting Jobs

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/persisting-jobs-69958195/) · 15:11

本课演示如何使用 Entity Framework Core 和 SQLite,把一个短暂存在的内存队列替换为持久的、基于数据库的系统。
通过把后台作业持久化到数据库表中,应用可以确保工作不会在重启或崩溃时丢失,从而在 API(生产者)和后台 worker(消费者)之间提供可靠的移交。
实现内容包括:为作业模型加上状态跟踪、配置数据库上下文,以及修改后台 worker,让它轮询待处理任务,并通过状态更新来管理任务的生命周期。

### 核心概念

- **Durability**:通过把事实来源从内存移到数据库,确保后台作业能在应用重启或崩溃后留存下来。
- **Database Handoff**:使用一张数据库表作为 API(创建作业)与 worker(处理作业)之间的通信点。
- **Job State Management**:使用 Pending、Processing、Completed 和 Failed 等状态来跟踪作业的生命周期。
- **Audit Trails**:记录创建、开始和完成的时间戳,以及重试次数和错误日志,以获得运维可见性。
- **Polling**:把后台 worker 配置为查询数据库中下一个可用的待处理作业,而不是在内存 channel 上等待。

### 课程笔记

#### 后台作业模型

为了支持持久化和状态跟踪,`BackgroundJob` 模型必须扩展。
除了基本的负载和 ID 之外,模型现在还包含一个状态字符串和若干时间戳,为作业的整个历程提供审计轨迹。
这里使用一个静态的 `JobStatuses` 类来保持整个应用中的一致性。

```csharp
namespace APIProject.Models
{
    public sealed class BackgroundJob
    {
        public Guid Id { get; set; }

        public string Type { get; set; } = string.Empty;

        public string Payload { get; set; } = string.Empty;

        public string Status { get; set; } = JobStatuses.Pending;

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? StartedAt { get; set; }

        public DateTimeOffset? CompletedAt { get; set; }

        public int RetryCount { get; set; }

        public string? LastError { get; set; }
    }

    public static class JobStatuses
    {
        public const string Pending = "Pending";
        public const string Processing = "Processing";
        public const string Completed = "Completed";
        public const string Failed = "Failed";
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/persisting-jobs-69958195/?t=160)

#### 数据库配置

`AppDbContext` 被更新为包含 `BackgroundJob` 实体的 `DbSet`。
这张表充当持久化的队列。

```csharp
using APIProject.Models;
using Microsoft.EntityFrameworkCore;

namespace APIProject
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<BackgroundJob> BackgroundJobs { get; set; }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/persisting-jobs-69958195/?t=220)

在 `Program.cs` 文件中,数据库上下文被注册到依赖注入容器中,本实现具体使用的是 SQLite。

```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("Default")));
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/persisting-jobs-69958195/?t=295)

#### 通过 API 将作业入队

API 控制器被修改为把作业直接保存到数据库,而不是写入内存 channel。
当请求被接受时,会创建一条状态为 `Pending` 的新 `BackgroundJob` 记录并保存。

```csharp
var job = new BackgroundJob()
{
    Id = jobId,
    Type = "ProcessDocument",
    Payload = payload,
    CreatedAt = DateTime.UtcNow
};

await db.BackgroundJobs.AddAsync(job, cancellationToken);
await db.SaveChangesAsync(cancellationToken);

return Accepted(new
{
    DocumentId = document.Id,
    JobId = jobId,
    Status = "Queued"
});
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/persisting-jobs-69958195/?t=340)

#### 在 Worker 中处理作业

`QueuedJobWorker`(一个 `BackgroundService`)不再从内存 channel 中出队。
取而代之,它轮询数据库,按创建日期排序,取第一个状态为 `Pending` 的作业。

```csharp
protected async override Task ExecuteAsync(CancellationToken stoppingToken)
{
    while (!stoppingToken.IsCancellationRequested)
    {
        using var scope = _scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetService<AppDbContext>();

        var job = await db.BackgroundJobs.Where(x => x.Status == "Pending")
            .OrderBy(x => x.CreatedAt)
            .FirstOrDefaultAsync(stoppingToken);

        if (job != null)
        {
            try
            {
                await ProcessJobAsync(job, stoppingToken);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error processing job {JobId} of type {JobType}",
                    job?.Id, job?.Type);
            }
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/persisting-jobs-69958195/?t=520)

```text
 API (producer)                               QueuedJobWorker (consumer)
   db.BackgroundJobs.AddAsync(job)              轮询:
   db.SaveChangesAsync()                          Where(Status == "Pending")
         |                                        .OrderBy(CreatedAt)
         v                                        .FirstOrDefaultAsync()
 +---------------------------------+                    |
 | BackgroundJobs 表 (SQLite)      |<-------------------+
 | Id | Type | Payload | Status .. |
 +---------------------------------+
```

#### 管理作业的生命周期与状态

为了确保系统可靠,worker 必须在作业流经处理管道时更新作业的状态。
这里实现了若干辅助方法,把状态更新为 `Processing`、`Completed` 或 `Failed`。

```csharp
private async Task SetJobToInProgress(BackgroundJob job, CancellationToken cancellationToken, AppDbContext db)
{
    job.Status = "Processing";
    db.BackgroundJobs.Update(job);
    await db.SaveChangesAsync();
}

private async Task SetJobComplete(BackgroundJob job, CancellationToken cancellationToken, AppDbContext db)
{
    job.Status = "Complete";
    db.BackgroundJobs.Update(job);
    await db.SaveChangesAsync();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/persisting-jobs-69958195/?t=700)

`ProcessJobAsync` 方法把实际工作包在一个 try-catch 块中。
这确保了如果处理失败,作业状态会在数据库中被更新为 `Failed`,并且异常会被重新抛出以保留堆栈跟踪。

```csharp
private async Task ProcessJobAsync(BackgroundJob job, CancellationToken cancellationToken)
{
    using var scope = _scopeFactory.CreateScope();
    var db = scope.ServiceProvider.GetService<AppDbContext>();
    try
    {
        switch (job.Type)
        {
            case "ProcessDocument":
                var payload = JsonSerializer.Deserialize<ProcessDocumentPayload>(job.Payload);
                var documentProcessor = scope.ServiceProvider.GetService<DocumentProcessorService>();
                await SetJobToInProgress(job, cancellationToken, db);
                await documentProcessor.ProcessAsync(payload.DocumentId, cancellationToken);
                await SetJobComplete(job, cancellationToken, db);
                break;
            default:
                throw new InvalidOperationException($"Unknown job type: {job.Type}");
        }
    }
    catch
    {
        await SetJobToFailed(job, cancellationToken, db);
        throw;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/persisting-jobs-69958195/?t=730)

以下是通过 `search_code` 恢复的屏幕代码:

```csharp
namespace APIProject.Services
{
    public class QueuedJobWorker : BackgroundService
    {
        private async Task ProcessJobAsync(BackgroundJob job, CancellationToken cancellationToken)
        {
        }

        private async Task SetJobToInProgress(BackgroundJob job, CancellationToken cancellationToken, AppDbContext db)
        {
        }

        private async Task SetJobComplete(BackgroundJob job, CancellationToken cancellationToken, AppDbContext db)
        {
        }

        private async Task SetJobToFailed(BackgroundJob job, CancellationToken cancellationToken, AppDbContext db)
        {
            job.Status = "Failed";
            db.
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/persisting-jobs-69958195/?t=670)

```text
                  SetJobToInProgress        SetJobComplete
  Pending -------------------> Processing -----------------> Completed
                                   |
                                   | 处理失败 (catch)
                                   | SetJobToFailed, throw;
                                   v
                                 Failed
```
