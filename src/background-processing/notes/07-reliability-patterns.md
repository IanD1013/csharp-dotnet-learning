# Reliability Patterns

> 课程:[From Zero to Hero: Background Processing in .NET](https://dometrain.com/course/from-zero-to-hero-background-processing-in-dotnet/) · 第 7 章
> 共 4 课 · 约 57:21
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

---

## 课程索引

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Designing Idempotent Background Jobs](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/designing-idempotent-background-jobs-69958203/) | 16:12 | [↓](#1-designing-idempotent-background-jobs) |
| 2 | [Retries and Exponential Backoff](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/retries-and-exponential-backoff-69958204/) | 17:34 | [↓](#2-retries-and-exponential-backoff) |
| 3 | [Handling Retries and Poison Jobs](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/handling-retries-and-poison-jobs-69958205/) | 7:03 | [↓](#3-handling-retries-and-poison-jobs) |
| 4 | [Reliable Job State Transitions](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/reliable-job-state-transitions-69958206/) | 16:32 | [↓](#4-reliable-job-state-transitions) |

---

## 1. Designing Idempotent Background Jobs

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/designing-idempotent-background-jobs-69958203/) · 16:12

幂等性确保一个后台作业可以被执行多次,而在第一次成功运行之后不会改变最终结果。
这一点至关重要,因为后台处理系统通常保证至少一次投递,这意味着作业可能因 worker 崩溃、超时或数据库故障而重试。
本课演示如何从脆弱的实现(在这种实现中,发送邮件等副作用发生在数据库更新之前)转向稳健的模式,例如 Outbox 模式,它使用数据库事务和唯一约束来可靠地协调状态变更与外部操作。

### 核心概念

- **Idempotency**:操作的一种性质,多次执行所产生的状态与一次成功执行相同。
- **At-Least-Once Delivery**:后台处理中一种常见的保证,系统确保作业至少运行一次,但可能不止一次。
- **Side Effect Coordination**:确保外部操作(如发送邮件或扣款)与内部数据库状态保持同步这一挑战。
- **Outbox Pattern**:一种可靠性模式,执行某个副作用的意图与业务状态变更保存在同一个数据库事务中。
- **Database Constraints**:使用唯一索引在存储层面强制保证正确性,防止并发重试期间出现重复记录。
- **Idempotency Keys**:传给外部 API 的唯一标识符,让服务提供方能够识别并忽略重复请求。

### 课程笔记

后台作业并不保证恰好运行一次。
在分布式系统中,你必须假设一个作业可能运行多次、部分完成后失败,或者成功完成但因崩溃而未能记录该完成状态。
可靠性取决于把作业设计成重试不会产生重复或损坏的结果。

#### 问题:未协调的副作用

一个常见的错误是在把状态变更提交到数据库之前就执行外部副作用,例如发送邮件。
如果数据库更新失败,重试系统会再次运行该作业,导致副作用重复发生。

```csharp
namespace APIProject.Services
{
    public class DocumentProcessorService
    {
        public DocumentProcessorService(AppDbContext dbContext, ILogger<DocumentProcessorService> logger)

        public async Task ProcessCompletedDocumentNotification(Document document)
        {
            var documentJob = await _dbContext.BackgroundJobs.SingleAsync
                (j => j.Id == document.Id);

            documentJob.Status = "Completed";
            documentJob.CompletedAt = DateTime.UtcNow;

            await _notificationService.SendEmailProcessingCompleteAsync(document.Id, cancellationToken);

            _dbContext.BackgroundJobs.Update(documentJob);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Document> ProcessAsync(Guid id, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new Document())
            {
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/designing-idempotent-background-jobs-69958203/?t=90)

在上面的示例中,如果 `SaveChangesAsync` 失败,用户会收到邮件,但数据库仍然显示该作业处于待处理状态。
重试时,邮件会被第二次发送。

```text
 第 1 次运行                               第 2 次运行(重试)
 SendEmailProcessingCompleteAsync  OK     SendEmailProcessingCompleteAsync  OK
 SaveChangesAsync                  失败    -> 邮件被第二次发送
 -> 数据库仍显示待处理
```

#### 初步改进:状态检查

常见的第一步是在执行任何工作之前先检查作业的当前状态。
这可以防范这样一种场景:作业已经完全完成并保存,但作业运行器在确认成功之前崩溃了。

```csharp
namespace APIProject.Services

public class DocumentProcessorService
{
    public DocumentProcessorService(AppDbContext dbContext, ILogger<DocumentProcessorService> logger)

    public async Task ProcessCompleteDocumentNotification(Document document)
    {
        var documentJob = await _dbContext.BackgroundJobs.SingleAsync
            (j => j.Id == document.Id);

        if(documentJob.Status == "Completed")
        {
            return;
        }

        documentJob.Status = "Completed";
        documentJob.CompletedAt = DateTime.UtcNow;

        await _notificationService.SendEmailProcessingCompleteAsync(document.Id, CancellationToken.None);

        _dbContext.BackgroundJobs.Update(documentJob);
        await _dbContext.SaveChangesAsync();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/designing-idempotent-background-jobs-69958203/?t=250)

这种检查虽然有用,但如果副作用已经发生而数据库更新失败,它就不可靠了。
如果记录从未被保存,数据库就无法告诉系统副作用已经发生过。

#### Outbox 模式

为了解决协调问题,你可以使用一种 Outbox 风格的模式。
作业不再直接发送邮件,而是在数据库中记录发送邮件的*意图*。
通过在同一个事务中保存业务状态(作业完成)和通知请求,你可以确保两者要么都成功,要么都失败。

首先,定义一个实体来表示待发送的通知:

```csharp
namespace APIProject.Models
{
    public class EmailNotification
    {
        public Guid Id { get; set; }
        public Guid DocumentId { get; set; }
        public string Type { get; set; }
        public string Recipient { get; set; }
        public bool Sent { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/designing-idempotent-background-jobs-69958203/?t=445)

然后,更新处理器,让它记录通知而不是发送通知:

```csharp
namespace APIProject.Services
public class DocumentProcessorService
{
    public async Task ProcessCompletedDocumentNotification(Document document, string email)
    {
        var documentJob = await _dbContext.BackgroundJobs.SingleAsync
            (j => j.Id == document.Id);

        if(documentJob.Status == "Completed")
            return;

        var notificationExists = await _dbContext.EmailNotifications
            .AnyAsync(n => n.DocumentId == document.Id
            && n.Type == "ProcessingComplete"
            && n.Sent);

        if (notificationExists == false)
        {
            _dbContext.EmailNotifications.Add(new EmailNotification()
            {
                Id = Guid.NewGuid(),
                DocumentId = document.Id,
                Type = "ProcessingComplete",
                Recipient = email
            });
        }

        documentJob.Status = "Completed";
        documentJob.CompletedAt = DateTime.UtcNow;

        _dbContext.BackgroundJobs.Update(documentJob);
        await _dbContext.SaveChangesAsync();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/designing-idempotent-background-jobs-69958203/?t=565)

```text
 DocumentProcessorService
   |
   |  同一个事务 (SaveChangesAsync)
   v
 +--------------------------------+   +--------------------------------+
 | BackgroundJobs                 |   | EmailNotifications             |
 |   Status = "Completed"         |   |   Type = "ProcessingComplete"  |
 |   CompletedAt                  |   |   Recipient = email            |
 +--------------------------------+   +--------------------------------+
            要么都成功,要么都失败
```

#### 用数据库约束强制保证正确性

如果两个 worker 同时处理同一个作业,应用层面的检查(如 `AnyAsync`)不足以防止重复。
为了确保正确性,数据库必须通过唯一索引来强制保证唯一性。

```csharp
namespace APIProject
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<EmailNotification>()
            .HasIndex(n => new { n.DocumentId, n.Type })
            .IsUnique();
    }

    public DbSet<BackgroundJob> BackgroundJobs { get; set; }
    public DbSet<EmailNotification> EmailNotifications { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/designing-idempotent-background-jobs-69958203/?t=625)

#### 处理 Outbox

一个单独的 worker 负责获取未发送的通知、执行副作用,并将其标记为已发送。
这把业务逻辑与外部通信这两个关注点分离开来。

```csharp
public class NotificationService
{
    private async Task SendEmailAsync(Guid documentId, string recipient, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task SendEmailProcessingCompleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var notifications = await _dbContext.EmailNotifications
            .Where(n => !n.Sent)
            .OrderBy(n => n.CreatedAt)
            .Take(50)
            .ToListAsync(cancellationToken);

        foreach (var notification in notifications)
        {
            await SendEmailAsync(
                notification.DocumentId,
                notification.Recipient,
                cancellationToken);

            notification.Sent = true;
            notification.SentAt = DateTime.UtcNow;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/designing-idempotent-background-jobs-69958203/?t=760)

```text
 EmailNotifications (!n.Sent, OrderBy CreatedAt, Take 50)
   |
   v
 NotificationService
   |-- SendEmailAsync(DocumentId, Recipient)
   |-- notification.Sent = true, SentAt = DateTime.UtcNow
   v
 SaveChangesAsync
```

#### 外部幂等键

即使使用了 Outbox 模式,通知 worker 本身也可能在发送邮件之后、更新数据库之前失败。
要实现真正的"恰好一次"行为,外部系统必须支持幂等键。
通过把一个唯一键(例如 `document-complete:ID`)传给邮件服务提供方,提供方就能识别重复请求,避免把同一封邮件发送两次。

## 2. Retries and Exponential Backoff

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/retries-and-exponential-backoff-69958204/) · 17:34

在后台处理中,失败是不可避免的。
外部 API 会失败,网络会超时,数据库会繁忙,云服务会限流请求。
由于这些失败通常是暂时性的,最好的做法往往是稍等片刻再试一次。
然而,重试必须谨慎处理:重试过快会加剧系统压力,无限重试会消耗容量,而重试非幂等操作可能会产生重复的副作用。

### 核心概念

- **Job-level Retries**:通过把整个后台任务重新安排到稍后的时间来管理它的失败。
- **Operational-level Retries**:使用 Polly 之类的库,在让整个作业失败之前重试特定的外部调用(例如 HTTP 请求)。
- **Exponential Backoff**:一种增加重试尝试之间等待时间的算法,用来降低系统压力。
- **Jitter**:为退避间隔加入随机性,以防止"惊群"(thundering herd)问题。
- **Persistence**:把重试次数和下次尝试的时间戳存储在数据库中,确保它们在服务重启后依然持久保留。

### 课程笔记

#### 立即重试(反模式)

最简单的重试策略是在 try-catch 块中立即重试。
通常不鼓励这样做,因为它缺少重试上限、日志记录,也没有区分暂时性失败和永久性失败。
如果某个外部系统正处于困境,立即重试很可能会让问题更糟。

```csharp
namespace APIProject.Services
    public sealed class ReportService
    {
        public ReportService(
            ILogger<ReportService> logger)
        {
            _logger = logger;
        }

        private async Task StartDailyRun()
        {
            //DON'T DO THIS!!!
            try
            {
                await GenerateDailyReportAsync();
            }
            catch
            {
                await GenerateDailyReportAsync();
            }
        }

        public Task GenerateDailyReportAsync()
        {
            _logger.LogInformation(
                "Generating daily report at {Time}",
                DateTimeOffset.UtcNow);

            return Task.CompletedTask;
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/retries-and-exponential-backoff-69958204/?t=175)

#### 作业级重试与持久化

在作业层面,重试信息应当存储在作业记录本身上。
这让系统能够跟踪进度,并在服务重启后存活下来。
关键属性包括当前重试次数、下次尝试的时间戳,以及遇到的最后一条错误消息。

```csharp
namespace APIProject.Models
    public sealed class BackgroundJob
    {
        public Guid Id { get; set; }

        public string Type { get; set; } = string.Empty;

        public string Payload { get; set; } = string.Empty;

        public string Status { get; set; } = JobStatuses.Pending;

        public DateTime CreatedAt { get; set; }

        public DateTime? StartedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public int RetryCount { get; set; }
        public DateTimeOffset NextAttemptAt { get; set; }

        public string? LastError { get; set; }
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/retries-and-exponential-backoff-69958204/?t=205)

#### 实现指数退避

为了避免猛烈冲击一个正在失败的系统,请使用指数退避策略。
虽然这可以用数学方式计算,但一个基于尝试次数的简单 switch 语句就能提供清晰、固定的重试时间表。

```csharp
private static TimeSpan CalculateBackoff(int attempt)
{
    return attempt switch
    {
        1 => TimeSpan.FromSeconds(5),
        2 => TimeSpan.FromSeconds(15),
        3 => TimeSpan.FromSeconds(30),
        _ => TimeSpan.FromMinutes(1)
    };
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/retries-and-exponential-backoff-69958204/?t=325)

当作业失败时,worker 应当递增重试次数并计算下次尝试的时间。
如果超过了最大重试次数,该作业最终会被标记为失败。

```csharp
private async Task SetJobToFailed(BackgroundJob job,
    CancellationToken cancellationToken,
    AppDbContext db,
    Exception exception)
{
    job.RetryCount++;
    job.LastError = exception.Message;
    var maximumRetries = 5;

    if(job.RetryCount <= maximumRetries)
    {
        var backoff = CalculateBackoff(job.RetryCount);
        job.NextAttemptAt = DateTime.UtcNow.Add(backoff);
        job.Status = "Pending";
    }
    else
    {
        job.Status = "Failed";
    }
    db.BackgroundJobs.Update(job);
    await db.SaveChangesAsync();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/retries-and-exponential-backoff-69958204/?t=445)

```text
 RetryCount   CalculateBackoff   Status
 ----------   ----------------   ---------
     1             5s            "Pending"
     2            15s            "Pending"
     3            30s            "Pending"
     4             1m            "Pending"
     5             1m            "Pending"
     6             -             "Failed"   (RetryCount > maximumRetries)
```

#### 使用 Polly 进行操作级重试

Polly 是一个 .NET 弹性库,用于把外部调用(HTTP、存储等)包裹在一个弹性管道(resilience pipeline)中。
它处理单个作业执行过程中短暂的暂时性失败。
一个 `ResiliencePipeline` 可以组合重试、超时和断路器等策略。

在 Polly 中配置重试策略时,你可以指定退避类型、是否使用 jitter,以及使用 `PredicateBuilder` 精确指定哪些异常应触发重试。

```csharp
private ResiliencePipeline CreateDocAPIRetryPipeline()
{
    return new ResiliencePipelineBuilder()
        .AddRetry(new Polly.Retry.RetryStrategyOptions()
        {
            MaxRetryAttempts = 3,
            Delay = TimeSpan.FromSeconds(5),
            BackoffType = DelayBackoffType.Exponential,
            UseJitter = true,
            ShouldHandle = new PredicateBuilder()
                .Handle<HttpRequestException>()
                .Handle<TimeoutException>(),
            OnRetry = args =>
            {
                _logger.LogWarning("Retrying API call due to transient error. Attempt {RetryAttempt}.", args.AttemptNumber);
                return default;
            }
        }).Build();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/retries-and-exponential-backoff-69958204/?t=730)

管道构建好之后,使用 `ExecuteAsync` 方法来包裹这个不可靠的操作。
这让核心逻辑保持整洁,同时又应用了弹性策略。

```csharp
public async Task PostDocumentIdToAPI(Guid docId, CancellationToken cancellationToken)
{
    await _docApiRetryPipeline.ExecuteAsync(
        async token =>
        {
            var response = await _httpClient.PostAsJsonAsync("/api/documents/process",
                new { DocumentId = docId }, token);
        },
        cancellationToken
    );
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/retries-and-exponential-backoff-69958204/?t=970)

#### Polly 与调度器的区别

Polly 不是调度器,也不能替代持久化的作业状态。
它应当用于一次操作内部的短暂暂时性失败。
周期性任务或长期重试应当由作业系统或专门的调度器(如 Hangfire 或 Quartz)来处理。
把 Polly 用于长期调度是对这个库的不当使用。

## 3. Handling Retries and Poison Jobs

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/handling-retries-and-poison-jobs-69958205/) · 7:03

### 总结

毒作业(poison job)是因为无效数据或违反业务规则等根本性问题(而不是暂时的网络故障)而反复失败的后台任务。
为了维持系统可靠性和 worker 容量,必须区分暂时性失败与永久性失败:前者值得重试,后者应被移入一个终止性的 'Dead Letter' 状态。
通过捕获详细的错误信息并停止对这些作业的自动重试,开发者可以为人工调查和修正提供一条路径,而不会阻塞处理管道的其余部分。

### 核心概念

- **Poison Jobs**:由于无效的 payload、缺失的数据库记录或违反业务规则等内在问题而反复失败的任务。
- **Transient vs. Permanent Failures**:暂时性失败(例如超时、死锁)在重试时可能成功,而永久性失败(例如未知的作业类型、校验错误)需要人工干预。
- **Dead Letter State**:一种终止状态,用于已停止自动重试、并被保留下来以供人工调查的作业。
- **Diagnostic Metadata**:存储错误类型、错误消息和失败时间戳,以协助排查问题。
- **Manual Intervention**:修正数据或配置,并显式地把一个死信作业重新入队的过程。

### 课程笔记

#### 理解毒作业

毒作业是由于作业本身固有的问题(例如无效数据或处理逻辑中的错误假设)而反复失败的作业。
例子包括无效的 payload、引用了已删除的数据库记录,或违反业务规则的作业。
无限地继续重试这些作业并不会提高可靠性;相反,它会制造噪音、浪费 worker 容量,并可能延误其他有用的工作。

为了有效地处理这些作业,系统必须区分两类失败:
1. **Transient Failures**:网络超时、数据库死锁或 API 速率限制之类的问题,稍后重试可能会成功。
2. **Permanent Failures**:无效的 payload 或未知的作业类型之类的问题,如果系统或数据不做改变,它们不太可能成功。

#### 更新 Background Job 模型

为了支持对失败作业的调查,应当更新 `BackgroundJob` 模型以包含诊断信息。
这包括最后一条错误消息、错误的类型,以及失败的时间戳。

```csharp
namespace APIProject.Models
{
    public sealed class BackgroundJob
    {
        public Guid Id { get; set; } 
        public string Type { get; set; } = string.Empty;
        public string Payload { get; set; } = string.Empty;
        public string Status { get; set; } = JobStatuses.Pending;
        public DateTime CreatedAt { get; set; } 
        public DateTime? StartedAt { get; set; } 
        public DateTime? CompletedAt { get; set; } 
        public int RetryCount { get; set; } 
        public DateTimeOffset NextAttemptAt { get; set; } 
        public string? LastError { get; set; } 
        public string? LastErrorType { get; set; } 
        public DateTimeOffset? FailedAt { get; set; } 
    }

    public static class JobStatuses
    {
        public const string Pending = "Pending";
        public const string Processing = "Processing";
        public const string Completed = "Completed";
        public const string Failed = "Failed";
        public const string DeadLetter = "DeadLetter";
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/handling-retries-and-poison-jobs-69958205/?t=130)

#### 识别永久性失败

worker 服务不应以相同的方式对待每一个异常,而应识别出代表永久性失败的特定异常类型。
例如,`UnknownJobTypeException` 或 `PayloadInvalidException` 应当立即触发转入终止状态,而不是进入重试循环。

```csharp
public static bool IsPermanentFailure(Exception ex)
{
    return ex is UnknownJobTypeException
        or ex is PayloadInvalidException;
}

public class UnknownJobTypeException : Exception
{
    public UnknownJobTypeException(string jobType)
        : base($"Unknown job type: {jobType}")
    { }
}

public class PayloadInvalidException : Exception
{
    public PayloadInvalidException(string message) : base(message) { }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/handling-retries-and-poison-jobs-69958205/?t=250)

#### 实现死信策略

在失败处理例程(例如 `SetJobToFailed`)中,系统会检查该异常是否属于永久性失败。
如果是,作业状态会被设置为 `DeadLetter`。
这个状态表示自动重试已经停止,但作业被保留下来以供人工审查。
如果失败是暂时性的,系统会继续执行标准的指数退避与重试逻辑,直到达到最大重试次数上限。

```csharp
private async Task SetJobToFailed(BackgroundJob job,
    CancellationToken cancellationToken,
    AppDbContext db,
    Exception exception)
{
    job.RetryCount++;
    job.LastError = exception.Message;
    job.LastErrorType = exception.GetType().Name;
    job.FailedAt = DateTimeOffset.UtcNow;
    var maximumRetries = 5;

    if (IsPermanentFailure(exception))
    {
        job.Status = JobStatuses.DeadLetter;
        db.BackgroundJobs.Update(job);
        await db.SaveChangesAsync();
        return;
    }

    if (job.RetryCount <= maximumRetries)
    {
        var backOff = CalculateBackoff(job.RetryCount);
        job.NextAttemptAt = DateTime.UtcNow.Add(backOff);
    }
    else
    {
        job.Status = JobStatuses.Failed;
        db.BackgroundJobs.Update(job);
    }

    await db.SaveChangesAsync();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/handling-retries-and-poison-jobs-69958205/?t=280)

```text
 SetJobToFailed(exception)
   |
   +-- IsPermanentFailure(exception)?
   |     yes --> JobStatuses.DeadLetter  (停止自动重试,保留以供人工审查)
   |     no
   |      |
   |      +-- RetryCount <= maximumRetries?
   |            yes --> NextAttemptAt = UtcNow + CalculateBackoff(RetryCount)
   |            no  --> JobStatuses.Failed
```

死信机制让运维团队能够在数据库中查询失败的作业,利用存储的错误详情诊断根本原因,并可能在手动重新入队之前修复数据。
这提供了一种受控的错误处理方式,而不会带来无限循环或系统性能下降的风险。

## 4. Reliable Job State Transitions

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/reliable-job-state-transitions-69958206/) · 16:32

可靠的后台处理要求严格控制作业如何在 Pending、Processing 和 Completed 等状态之间移动。
如果没有强制执行的规则,系统可能会遇到竞态条件:多个 worker 认领同一个作业,或者作业转换到不可能的状态,例如一个已完成的作业被重置为处理中状态。
把这部分逻辑集中起来,可以确保状态转换是可预测且原子的。

### 核心概念

- **Centralized State Transitions**:把逻辑从直接更新状态转移到一个强制执行业务规则的专用例程中。
- **State Machine Pattern**:使用一种数据结构来定义作业状态之间的有效路径。
- **Atomicity**:确保状态变更要么完整发生、要么完全不发生,以防止重复处理。
- **Conditional Updates**:在数据库查询中使用"预期状态"检查来处理并发。
- **Audit Trails**:记录状态变更的历史,用于可观测性和调试。

### 课程笔记

后台 worker 的一个简单实现可能会在处理循环中直接设置作业的状态。
然而,这会把转换逻辑散布到整个应用中,使约束难以强制执行。
为了解决这个问题,请用一个字典定义允许的转换映射,其中键是当前状态,值是允许的下一个状态组成的数组。

```csharp
private static readonly Dictionary<string, string[]> allowedTransitions = new()
{
    ["Pending"] = [
        "Processing",
        "DeadLetter"
    ],
    ["Processing"] = [
        "Complete",
        "Pending",
        "DeadLetter"
    ],
    ["DeadLetter"] = [
        "Pending"
    ],
    ["Complete"] = [],
};
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/reliable-job-state-transitions-69958206/?t=265)

```text
            +------------------------------+
            |                              |
            v                              |
      +-----------+               +--------------+
      |  Pending  |-------------->|  Processing  |
      +-----------+               +--------------+
         |     ^                     |        |
         |     |                     |        v
         |     |                     |   +----------+
         |     |                     |   | Complete |  (无后续状态)
         |     |                     |   +----------+
         v     |                     v
      +--------------------------------------+
      |              DeadLetter              |
      +--------------------------------------+

  Pending    -> Processing, DeadLetter
  Processing -> Complete, Pending, DeadLetter
  DeadLetter -> Pending
  Complete   -> (none)
```

不要允许任何代码随意修改状态,而是实现一个集中的 `TransitionAsync` 方法。
这个方法会在把变更提交到数据库之前,先根据 `allowedTransitions` 映射验证所请求的转换。

```csharp
public async Task TransitionAsync(BackgroundJob job,
    string newStatus,
    CancellationToken cancellationToken)
{
    var allowed = allowedTransitions[job.Status].Contains(newStatus);
    if (allowed == false)
    {
        throw new InvalidOperationException($"Invalid status transition from {job.Status} to {newStatus}");
    }

    using var scope = _scopeFactory.CreateScope();
    var db = scope.ServiceProvider.GetService<AppDbContext>();
    job.Status = newStatus;
    db.BackgroundJobs.Update(job);
    await db.SaveChangesAsync(cancellationToken);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/reliable-job-state-transitions-69958206/?t=415)

在有多个 worker 的系统中,状态转换必须是原子的,以防止两个 worker 认领同一个待处理作业。
这是通过一种条件更新来实现的:只有当行仍处于预期状态(例如 "Pending")时才修改它。
在 Entity Framework Core 中,可以使用 `ExecuteUpdateAsync` 在一次数据库往返中完成这个检查和更新。

```csharp
public async Task<bool> TryTransitionAsync(Guid jobId, string expectedStatus,
    string nextStatus, CancellationToken cancellationToken)
{
    var allowed = allowedTransitions[expectedStatus].Contains(nextStatus);
    if (allowed == false)
    {
        throw new InvalidOperationException($"Invalid status transition from {expectedStatus}");
    }

    using var scope = _scopeFactory.CreateScope();
    var db = scope.ServiceProvider.GetService<AppDbContext>();
    var affectedRows = await db.BackgroundJobs.Where(x => x.Id == jobId
    && x.Status == expectedStatus)
        .ExecuteUpdateAsync(x =>
            x.SetProperty(p => p.Status, nextStatus), cancellationToken);

    return affectedRows == 1;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/reliable-job-state-transitions-69958206/?t=700)

当 worker 处理一个作业时,它会使用 `TryTransitionAsync` 尝试认领该作业。
如果该方法返回 `false`,就意味着另一个 worker 已经转换了这个作业,当前 worker 应当跳过处理,以避免重复工作。

```csharp
private async Task ProcessJobAsync(BackgroundJob job, CancellationToken cancellationToken)
{
    try
    {
        switch (job.Type)
        {
            case "ProcessDocument":
                var payload = JsonSerializer.Deserialize<ProcessDocumentPayload>(job.Payload);
                var documentProcessor = scope.ServiceProvider.GetService<DocumentProcessorService>();
                var dequeued = await TryTransitionAsync(job.Id, "Pending", "Processing", cancellationToken);
                if(dequeued == false)
                {
                    // Another worker has taken this job, skip processing
                    return;
                }

                await documentProcessor.ProcessAsync(payload.DocumentId, cancellationToken);
                await TransitionAsync(job, "Complete", cancellationToken);
                break;

            default:
                throw new InvalidOperationException($"Unknown job type: {job.Type}");
        }
    }
    catch (Exception ex)
    {
        await TransitionAsync(job, "DeadLetter", cancellationToken);
        throw;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/reliable-job-state-transitions-69958206/?t=805)

```text
 Worker A                              Worker B
 TryTransitionAsync(                   TryTransitionAsync(
   "Pending" -> "Processing")            "Pending" -> "Processing")
   |                                     |
   v                                     v
 UPDATE ... WHERE Id = jobId AND Status = "Pending"
   |                                     |
 affectedRows == 1 -> true             affectedRows == 0 -> false
   |                                     |
 ProcessAsync                          return (跳过处理)
```

最后,捕获每一次转换的审计轨迹对于生产环境的可观测性至关重要。
一个专门的模型可以存储状态变更的历史,包括之前的状态、新的状态以及转换的时间戳。

```csharp
public class BackgroundJobTransition
{
    public Guid Id { get; set; }
    public Guid JobId { get; set; }
    public string FromStatus { get; set; } = string.Empty;
    public string ToStatus { get; set; } = string.Empty;
    public DateTimeOffset TransitionedAt { get; set; }
    public string? Reason { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/reliable-job-state-transitions-69958206/?t=850)

每当状态变更成功发生时,都应把这条转换记录保存到数据库中。

```csharp
if(affectedRows == 1)
{
    var jobTransition = new BackgroundJobTransition
    {
        Id = Guid.NewGuid(),
        JobId = jobId,
        FromStatus = expectedStatus,
        ToStatus = nextStatus,
        TransitionedAt = DateTime.UtcNow
    };

    db.BackgroundJobTransitions.Add(jobTransition);
    await db.SaveChangesAsync(cancellationToken);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/reliable-job-state-transitions-69958206/?t=940)
