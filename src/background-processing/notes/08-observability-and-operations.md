# Observability and Operations

> 课程:[From Zero to Hero: Background Processing in .NET](https://dometrain.com/course/from-zero-to-hero-background-processing-in-dotnet/) · 第 8 章
> 共 3 课 · 约 41:34
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

---

## 课程索引

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Logging in Background Systems](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/logging-in-background-systems-69958210/) | 13:45 | [↓](#1-logging-in-background-systems) |
| 2 | [Monitoring Background Workloads](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/monitoring-background-workloads-69958211/) | 10:13 | [↓](#2-monitoring-background-workloads) |
| 3 | [Health Checks for Workers](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/health-checks-for-workers-69958212/) | 17:36 | [↓](#3-health-checks-for-workers) |

---

## 1. Logging in Background Systems

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/logging-in-background-systems-69958210/) · 13:45

### 总结

日志对后台系统至关重要,因为作业是异步运行的,并且在标准请求管道之外执行,这使得失败对用户不可见。
通过实现结构化日志和日志作用域,开发者可以把关键元数据(例如作业 ID、类型和尝试次数)附加到每一条日志上,从而为作业的生命周期建立一条可搜索的时间线。
正确使用日志级别并避免记录敏感数据,可以确保日志始终是用于调试和告警的有效运维工具,而不会引入噪音或安全风险。

### 核心概念

*   **Observability Challenges**:后台作业通常运行时间长、会被自动重试,并且与最初的用户请求脱节,这使得日志成为调试的主要证据。
*   **Structured Logging**:使用消息模板而不是手动字符串拼接,让日志可以在可观测性平台中被轻松搜索、过滤和查询。
*   **Logging Scopes**:把上下文属性(例如 Job ID 和 Attempt 次数)附加到在某个特定执行块内写入的每一条日志消息上。
*   **Lifecycle Events**:在作业被创建、开始、状态转换以及完成或失败时记录日志的必要性。
*   **Log Level Discipline**:对临时性重试使用 `Warning` 等级别,对最终失败使用 `Error`,以防止运维团队产生告警疲劳。
*   **Data Privacy**:避免记录 PII(个人身份信息)、令牌或大型负载。

### 课程笔记

#### 后台日志的重要性

当后台作业移出请求管道后,它们会变得更难观测。
与标准 API 请求的失败会立即对用户可见不同,后台作业可能在很久之后才失败,可能发生在另一个服务上,而那时用户早已离开页面。
没有恰当的日志,后台处理就会像一个"黑盒"。

有效的日志必须能回答运维问题:作业被创建了吗?
哪个 worker 处理了它?
它关联的是哪个实体?
它失败了吗,如果失败了,异常是什么?
为了回答这些问题,我们必须记录作业的整个生命周期,包括它的开始、完成以及任何重试尝试。

#### 实现结构化日志和作用域

结构化日志确保元数据作为独立字段被保留,而不是被固化到单个字符串中。
这是通过在 `ILogger` 方法中使用消息模板实现的。
为了避免把相同的元数据(例如 Job ID)传入每一个日志调用,我们使用日志作用域。

```csharp
using APIProject.Interfaces;
using APIProject.Models;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;

namespace APIProject.Services
{
    public class QueuedJobWorker : BackgroundService
    {
        private readonly IBackgroundJobQueue _jobQueue;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<QueuedJobWorker> _logger;
        private readonly HttpClient _httpClient;

        public QueuedJobWorker(
            IBackgroundJobQueue jobQueue,
            IServiceScopeFactory scopeFactory,
            ILogger<QueuedJobWorker> logger,
            IHttpClientFactory httpClientFactory)
        {
            _jobQueue = jobQueue;
            _scopeFactory = scopeFactory;
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient("QueuedJobWorkerClient");
        }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/logging-in-background-systems-69958210/?t=10)

使用 `ILogger.BeginScope` 时,你可以传入一个属性字典。
不过,某些日志提供程序(例如默认的 Console 提供程序)可能只会对该字典调用 `ToString()`,从而产生没有用处的输出。

```csharp
        private async Task ProcessJobAsync(BackgroundJob job, CancellationToken cancellationToken)
        {
            using var loggingScope = _logger.BeginScope(new Dictionary<string, object>
            {
                ["JobId"] = job?.Id,
                ["JobType"] = job?.Type,
                ["Attempt"] = job != null ? job.RetryCount + 1 : 0
            });

            _logger.LogInformation("Attempting to process job");

            if (job == null)
            { // ...
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/logging-in-background-systems-69958210/?t=325)

为了确保作用域在控制台中可见并被正确格式化,你必须在应用配置中启用它们,并使用 `BeginScope` 的消息模板重载。

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    },
    "Console": {
      "IncludeScopes": true
    }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/logging-in-background-systems-69958210/?t=460)

为作用域使用强类型的消息模板,可以确保元数据被正确附加到该 `using` 块内的每一条日志上:

```csharp
        private async Task ProcessJobAsync(BackgroundJob job, CancellationToken cancellationToken)
        {
            using var loggingScope = _logger.BeginScope(
                "JobId: {JobId}, JobType: {JobType}, Attempt: {Attempt}",
                job.Id, job.Type, job.RetryCount + 1);

            _logger.LogInformation("Attempting to process job");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/logging-in-background-systems-69958210/?t=580)

```text
using var loggingScope = _logger.BeginScope(
    "JobId: {JobId}, JobType: {JobType}, Attempt: {Attempt}", ...)
+----------------------------------------------------------------+
| LogInformation("Attempting to process job")                    |
|   + JobId, JobType, Attempt                                    |
| LogInformation(...)                                            |
|   + JobId, JobType, Attempt                                    |
+----------------------------------------------------------------+
  "Console": { "IncludeScopes": true }  -> 作用域在控制台中可见
```

#### 记录作业生命周期

在处理逻辑内部,日志应当跟踪状态转换和具体操作。
如果某个作业被另一个 worker 拾取,或在某个特定步骤中失败,这就能提供一条清晰的审计轨迹。

```csharp
                switch (job.Type)
                {
                    case "ProcessDocument":
                        _logger.LogInformation("Processing document job with payload: {Payload}", job.Payload);
                        var payload = JsonSerializer.Deserialize<ProcessDocumentPayload>(job.Payload);
                        var documentProcessor = scope.ServiceProvider.GetService<DocumentProcessorService>();
                        _logger.LogInformation("Attempting to transition job to Processing");
                        var dequeued = await TryTransitionToProcessingAsync(db, job, cancellationToken);
                        if(dequeued == false)
                        {
                            _logger.LogInformation("Job already picked up by another worker");
                            return;
                        }

                        _logger.LogInformation("Processing document...");
                        await documentProcessor.ProcessAsync(payload, cancellationToken);
                        _logger.LogInformation("Job processed successfully");
                        break;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/logging-in-background-systems-69958210/?t=610)

#### 日志级别与最佳实践

选择正确的日志级别对运维健康至关重要:
*   **Information**:用于生命周期里程碑(Started、Completed、Worker Start/Stop)。
*   **Warning**:用于临时性问题,例如作业重试,或某个过程运行得比预期慢。
    把重试记录为 `Warning` 而不是 `Error`,可以防止嘈杂的告警。
*   **Error**:用于最终失败,例如作业被移入死信队列,或某个外部依赖无法访问。
*   **Critical**:用于导致 worker 无法继续运行的基础设施故障。

避免使用"Something went wrong."这样含糊的消息。
此外,绝不要记录敏感数据,例如访问令牌、API 密钥或 PII(电子邮件地址、支付信息)。
不要记录大型文档负载,而是记录唯一标识符(例如 `DocumentId`),以保持日志存储高效且安全。

---

## 2. Monitoring Background Workloads

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/monitoring-background-workloads-69958211/) · 10:13

### 总结

监控为后台处理系统的健康状况和行为提供实时可见性,它与日志的区别在于关注的是聚合的系统状态,而不是单个作业事件。
通过跟踪队列长度、失败率以及最早待处理作业的存在时长等指标,开发者可以识别出仅靠日志可能会遗漏的性能瓶颈和系统级问题。

### 核心概念

- **Logging vs. Monitoring**:日志记录特定的历史事件(发生了什么),而监控提供系统行为的实时视图(正在发生什么)。
- **Critical Metrics**:关键指标包括队列长度、处理时长、失败率和重试率。
- **Pending Job Age**:在检测处理停滞方面,跟踪队列中最早作业的存在时长通常比单纯的队列长度更有信息量。
- **Alerting Strategies**:有效的告警关注持续性问题或一段时间内的失败率,而不是单个临时性失败,以避免噪音。
- **Integration**:指标通常会被导出到 Prometheus、Grafana、Datadog 或 Application Insights 等外部平台,用于可视化和告警。

### 课程笔记

虽然日志对于调查特定作业的失败(例如某个文档为什么处理失败)必不可少,但要了解系统的整体健康状况,就需要监控。
监控回答的是关于队列增长、worker 吞吐量以及失败率是否在正常范围内的问题。
例如,一条日志可能显示一次 API 超时,而监控会揭示某个作业类型的失败率是否从 2% 飙升到了 25%。

后台处理中的一个关键指标是最早待处理作业的存在时长。
一个由小而快速流转的作业组成的大队列也许是可以接受的,但一个最早作业已经等待了数小时的小队列,则意味着可能存在系统停滞或瓶颈。

```text
  大队列,作业小而快速流转          小队列,最早作业已等待数小时
  [job][job][job][job][job]...     [job (hours)][job]
  -> 也许可以接受                  -> 可能存在系统停滞或瓶颈
```

要在 .NET 应用中实现基本的监控,你可以暴露一个查询底层作业存储的指标端点。
下面的示例演示了一个 Minimal API 端点,它使用 Entity Framework Core 聚合作业状态,并计算最早待处理作业的存在时长:

```csharp
app.MapGet("/background-jobs/metrics",
    async (AppDbContext db, CancellationToken cancellationToken) =>
    {
        var now = DateTimeOffset.UtcNow;

        var pendingJobs = await db.BackgroundJobs
            .CountAsync(x => x.Status == "Pending", cancellationToken);

        var processingJobs = await db.BackgroundJobs
            .CountAsync(x => x.Status == "Processing", cancellationToken);

        var completedJobs = await db.BackgroundJobs
            .CountAsync(x => x.Status == "Completed", cancellationToken);

        var deadLetterJobs = await db.BackgroundJobs
            .CountAsync(x => x.Status == "DeadLetter", cancellationToken);

        var oldestPendingJobCreatedAt = await db.BackgroundJobs
            .Where(x => x.Status == JobStatuses.Pending)
            .OrderBy(x => x.CreatedAt)
            .Select(x => (DateTimeOffset?)x.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);

        var oldestPendingJobAgeSeconds = oldestPendingJobCreatedAt is null
            ? 0
            : (now - oldestPendingJobCreatedAt.Value).TotalSeconds;

        return Results.Ok(new
        {
            PendingJobs = pendingJobs,
            ProcessingJobs = processingJobs,
            CompletedJobs = completedJobs,
            DeadLetterJobs = deadLetterJobs,
            OldestPendingJobAgeSeconds = oldestPendingJobAgeSeconds
        });
    });
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/monitoring-background-workloads-69958211/?t=205)

这个端点返回一个表示数据库支撑的队列当前状态的 JSON 负载,它可以被仪表盘或简单的监控工具使用:

```json
{"pendingJobs":0,"processingJobs":0,"completedJobs":0,"deadLetterJobs":1,"oldestPendingAgeSeconds":0}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/monitoring-background-workloads-69958211/?t=325)

当监控驱动自动化告警时,它的价值才最大。
告警应当在需要人工介入时通知开发者,例如队列积压在一段持续时间内超过某个阈值(例如超过 10 分钟都有 1,000 个作业),或者在某个时间范围内没有任何作业完成。

一个常见的陷阱是对每一个单独的失败都发出告警。
由于后台系统通常会通过自动重试来处理临时性错误,对每一个失败都告警会产生过多的噪音。
相反,告警应当基于模式,例如失败率的骤升或死信作业数量的不断增长。
对于实时的仪表盘更新,可以使用 SignalR 等技术把这些指标推送给客户端,而无需手动刷新页面。

---

## 3. Health Checks for Workers

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/health-checks-for-workers-69958212/) · 17:36

### 总结

健康检查为编排器或负载均衡器等外部系统提供了一种标准化机制,用来验证应用的运行状态。
对于后台 worker,健康检查不只是简单的进程存活检查,还包括对数据库和消息队列等依赖项的就绪检查。
后台处理中的一个关键模式是"心跳"(heartbeat):worker 在执行循环中定期更新一个时间戳。
这使得自定义健康检查可以通过把最后一次心跳与一个设定的时间阈值进行比较,检测出 worker 是否已经停滞或崩溃,即使进程本身仍在运行。

### 核心概念

*   **Liveness vs. Readiness**:存活(Liveness)确认进程正在运行;就绪(Readiness)确认应用已准备好执行工作(例如已连接到数据库)。
*   **ASP.NET Core Health Checks**:内置的中间件,用于注册健康状态并通过 HTTP 端点暴露它。
*   **Database Health Monitoring**:使用专门的扩展来验证 worker 能够访问它的作业存储。
*   **Heartbeat Pattern**:一个线程安全的服务,跟踪 worker 最后一次完成其处理循环迭代的时间。
*   **Custom IHealthCheck**:实现逻辑来评估应用特定的健康标准,例如心跳是否过期。

### 课程笔记

健康检查是为机器之间的通信而设计的,它让 Kubernetes 或 Azure Container Apps 等托管平台能够监控应用的健康状况。
在后台 worker 的场景中,健康检查验证进程是否存活、数据库是否可达,以及 worker 循环是否在积极地推进。

#### 基础健康检查配置

要在 ASP.NET Core 应用中实现健康检查,你必须注册相关服务,并把中间件映射到一个端点。
默认情况下,一次成功的检查会返回 `200 OK` 状态以及一个 "Healthy" 字符串。

```csharp
// In Program.cs
builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapHealthChecks("/health");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/health-checks-for-workers-69958212/?t=205)

#### 监控数据库连接

由于后台 worker 严重依赖数据库来认领作业和更新状态,数据库连接是健康的前提条件。
使用 `Microsoft.Extensions.Diagnostics.HealthChecks.EntityFrameworkCore` 包,你可以轻松集成 EF Core 上下文检查。

```csharp
builder.Services.AddHealthChecks()
    .AddDbContextCheck<AppDbContext>();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/health-checks-for-workers-69958212/?t=265)

#### 实现心跳服务

一个 worker 可能正在运行,却因死锁或无限循环而"卡住"。
心跳服务跟踪最后一次成功的执行。
这个服务应当注册为单例,以便在后台 worker 和健康检查逻辑之间共享。

```csharp
namespace APIProject.Services
{
    public class WorkerHeartbeatService
    {
        private readonly object _lock = new();
        public DateTimeOffset? LastHeartBeat { get; private set; }

        public void Beat()
        {
            lock (_lock)
            {
                LastHeartBeat = DateTimeOffset.UtcNow;
            }
        }

        public DateTimeOffset? GetLastHeartBeat()
        {
            lock(_lock)
            {
                return LastHeartBeat;
            }
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/health-checks-for-workers-69958212/?t=415)

在 `Program.cs` 中注册该服务:

```csharp
builder.Services.AddSingleton<WorkerHeartbeatService>();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/health-checks-for-workers-69958212/?t=460)

#### 把心跳集成到 Worker 中

worker 应当在其处理循环的每一次迭代中调用 `Beat()` 方法。
如果 worker 在每次迭代中处理多个作业,通常更好的做法是在每个作业之后都发出一次心跳,以保持规律的节奏。

```csharp
protected async override Task ExecuteAsync(CancellationToken stoppingToken)
{
    while (!stoppingToken.IsCancellationRequested)
    {
        _workerHeartbeatService.Beat();
        
        using var scope = _scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetService<AppDbContext>();

        var job = await db.BackgroundJobs.Where(x => x.Status == "Pending")
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync(stoppingToken);

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/health-checks-for-workers-69958212/?t=580)

#### 创建自定义心跳健康检查

要暴露心跳状态,需要实现 `IHealthCheck` 接口。
这个检查会把距离最后一次心跳所经过的时间与一个阈值(例如 30 秒)进行比较。

```csharp
namespace APIProject.HealthChecks
{
    public class WorkerHeartBeatHealthCheck : IHealthCheck
    {
        private readonly WorkerHeartbeatService _workerHeartBeatService;

        public WorkerHeartBeatHealthCheck(WorkerHeartbeatService workerHeartBeatService)
        {
            _workerHeartBeatService = workerHeartBeatService;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var lastHeartbeat = _workerHeartBeatService.GetLastHeartBeat();
            var heartbeatThreshold = TimeSpan.FromSeconds(30);

            if (lastHeartbeat is null)
            {
                return Task.FromResult(HealthCheckResult.Unhealthy("No heartbeat recorded yet"));
            }

            var timeSinceLastHeartbeat = DateTimeOffset.UtcNow - lastHeartbeat.Value;

            if (timeSinceLastHeartbeat <= heartbeatThreshold)
            {
                return Task.FromResult(HealthCheckResult.Healthy($"Last heartbeat was {timeSinceLastHeartbeat.TotalSeconds} seconds ago"));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy($"Last heartbeat was {timeSinceLastHeartbeat.TotalSeconds} seconds ago, which exceeds the threshold of {heartbeatThreshold.TotalSeconds} seconds"));
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/health-checks-for-workers-69958212/?t=895)

```text
ExecuteAsync                              WorkerHeartBeatHealthCheck
  while (!stoppingToken...)                 CheckHealthAsync()
    Beat() -----------+           +-------- GetLastHeartBeat()
    ProcessJobAsync() |           |           null        -> Unhealthy
    Beat() -----------+           |           <= 30 s     -> Healthy
                      v           |           > 30 s      -> Unhealthy
            +--------------------------+
            | WorkerHeartbeatService   |
            | (AddSingleton)           |
            | LastHeartBeat            |
            +--------------------------+
```

最后,在服务集合中注册这个自定义健康检查:

```csharp
builder.Services.AddHealthChecks()
    .AddDbContextCheck<AppDbContext>()
    .AddCheck<WorkerHeartBeatHealthCheck>("heartbeat_check");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/health-checks-for-workers-69958212/?t=865)
