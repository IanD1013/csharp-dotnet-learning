# Scheduled Background Work

> 课程:[From Zero to Hero: Background Processing in .NET](https://dometrain.com/course/from-zero-to-hero-background-processing-in-dotnet/) · 第 6 章
> 共 4 课 · 约 46:48
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

---

## 课程索引

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Time-Based Workloads in Real Systems](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/time-based-workloads-in-real-systems-69958199/) | 10:18 | [↓](#1-time-based-workloads-in-real-systems) |
| 2 | [Scheduling Jobs With Hangfire](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/scheduling-jobs-with-hangfire-69958200/) | 10:11 | [↓](#2-scheduling-jobs-with-hangfire) |
| 3 | [Scheduling Jobs With Quartz](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/scheduling-jobs-with-quartz-69958201/) | 10:42 | [↓](#3-scheduling-jobs-with-quartz) |
| 4 | [Lightweight Scheduling with TickerQ](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/lightweight-scheduling-with-tickerq-69958202/) | 15:37 | [↓](#4-lightweight-scheduling-with-tickerq) |

---

## 1. Time-Based Workloads in Real Systems

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/time-based-workloads-in-real-systems-69958199/) · 10:18

### 总结

定时工作负载是由时间而非用户事件触发的后台任务,对于每日报表、数据清理和周期性同步等操作必不可少。
虽然 .NET 提供了 PeriodicTimer,可在 BackgroundService 中执行简单的基于间隔的任务,但这种自制方案缺少作业持久化、仪表盘和分布式执行等高级特性。
对于业务关键型工作负载或多服务器环境,更推荐使用 Hangfire 或 Quartz 这样的专用调度框架,以确保可靠性和运维控制。

### 核心概念

*   **Event-driven vs. Time-driven:** 事件驱动的工作因用户操作(例如上传文件)而开始,而时间驱动的工作由系统时钟触发。
*   **Common Use Cases:** 生成报表、发送提醒邮件、重试失败的集成、清理过期的数据库记录,以及刷新缓存数据。
*   **PeriodicTimer:** 一个现代 .NET 工具,用于为后台服务中的循环创建精确的基于间隔的 tick。
*   **Design Challenges:** 处理计划时间窗口内的应用停机、管理重叠执行,以及在多服务器部署中防止重复运行。
*   **Scheduling Frameworks:** Hangfire、Quartz 和 TickerQ 等工具提供持久化作业存储、重试逻辑和分布式执行管理。

### 课程笔记

定时工作负载几乎出现在每一个生产应用中。
与事件驱动的后台工作不同,它们由时钟触发。
触发机制的这一转变带来了新的设计问题,例如应用离线时如何处理错过的运行,或者如何在多台服务器之间管理执行。

#### 实现一个简单的定时 Worker

对于简单的基于间隔的任务,使用 `BackgroundService` 和 `PeriodicTimer` 的自制方案通常就足够了。
相比旧的计时器类型,更推荐 `PeriodicTimer`,因为它允许使用一个尊重取消令牌的异步等待循环。

```csharp
namespace APIProject.Services
{
    public sealed class CleanupWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<CleanupWorker> _logger;

        public CleanupWorker(
            IServiceScopeFactory scopeFactory,
            ILogger<CleanupWorker> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {

        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/time-based-workloads-in-real-systems-69958199/?t=155)

要实现调度,用所需的间隔初始化 `PeriodicTimer`。
然后 Worker 进入一个循环,等待下一个 tick。
这种模式确保工作按指定频率运行,而不会不必要地阻塞线程。

```csharp
protected override async Task ExecuteAsync(CancellationToken stoppingToken)
{
    using var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));

    while (await timer.WaitForNextTickAsync(stoppingToken))
    {
        try
        {
            await RunCleanUpAsync(stoppingToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during cleanup");
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/time-based-workloads-in-real-systems-69958199/?t=220)

#### 将逻辑解耦到 Scoped 服务中

后台服务通常注册为单例。
如果清理逻辑需要 scoped 依赖,例如 `AppDbContext`,则应把该逻辑封装到一个单独的 scoped 服务中。
然后在 Worker 内部创建的作用域中手动解析这个服务。

```csharp
namespace APIProject.Services
{
    public class DocumentCleanUpService
    {
        private readonly ILogger<DocumentCleanUpService> _logger;

        public DocumentCleanUpService(ILogger<DocumentCleanUpService> logger)
        {
            _logger = logger;
        }

        public async Task DeleteExpiredDocumentsAsync(CancellationToken cancellationToken)
        {
            // In a real implementation, this method would query the database for documents
            // that have expired and delete them. For demonstration, we'll just log the action.
            _logger.LogInformation("Starting cleanup of expired documents at {Time}", DateTime.UtcNow);
            // Simulate some work
            await Task.Delay(1000, cancellationToken);
            _logger.LogInformation("Completed cleanup of expired documents at {Time}", DateTime.UtcNow);
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/time-based-workloads-in-real-systems-69958199/?t=295)

Worker 使用 `IServiceScopeFactory` 来解析 `DocumentCleanUpService` 并执行任务。

```csharp
private async Task RunCleanUpAsync(CancellationToken cancellationToken)
{
    using var scope = _scopeFactory.CreateScope();
    var cleanUpService = scope.ServiceProvider.GetRequiredService<DocumentCleanUpService>();
    await cleanUpService.DeleteExpiredDocumentsAsync(cancellationToken);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/time-based-workloads-in-real-systems-69958199/?t=340)

```text
CleanupWorker (singleton, BackgroundService)
  |
  |  PeriodicTimer: WaitForNextTickAsync, every 5 seconds
  v
RunCleanUpAsync
  |
  |  _scopeFactory.CreateScope()
  v
+--------------------------------------------------+
| scope                                            |
|   GetRequiredService<DocumentCleanUpService>()   |
|   DeleteExpiredDocumentsAsync(cancellationToken) |
+--------------------------------------------------+
```

#### 服务注册

scoped 服务和托管 Worker 都必须在 `Program.cs` 中注册到依赖注入容器。

```csharp
builder.Services.AddScoped<DocumentCleanUpService>();
builder.Services.AddHostedService<CleanupWorker>();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/time-based-workloads-in-real-systems-69958199/?t=355)

#### 自制方案的局限

虽然这种方案对于缓存刷新或文件清理这类内部维护任务来说简单而有效,但它有明显的局限:
*   **No Persistence:** 没有作业历史,也没有成功或失败运行的记录。
*   **No Resilience:** 如果作业预定运行时应用处于停机状态,这次运行就会被完全错过。
*   **No Distributed Locking:** 在横向扩展的环境中,应用的每个实例都会同时运行该作业,这可能导致数据损坏或竞态条件。
*   **Limited Scheduling:** 它不原生支持用于复杂调度(例如"每月的第一个星期一")的 Cron 表达式。

对于业务关键型工作流,建议迁移到 Hangfire 或 Quartz 这样的专用框架,以获得持久化调度、重试逻辑和分布式执行等特性。

## 2. Scheduling Jobs With Hangfire

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/scheduling-jobs-with-hangfire-69958200/) · 10:11

### 总结

Hangfire 是一个健壮的 .NET 后台作业框架,为后台任务提供持久化存储、调度和监控。
与简单的计时器不同,Hangfire 会把作业信息记录到数据库(例如 SQLite)中,从而支持重试、通过内置仪表盘跟踪状态,以及在应用重启后仍能可靠执行。
它支持多种作业类型,包括 fire-and-forget、延迟作业和周期性作业,对于复杂的调度需求来说,它是手动实现后台服务的一个强大替代方案。

### 核心概念

*   **Persistent Job Storage**:作业存储在数据库(例如 SQLite、SQL Server)中,而不仅仅是内存中,从而确保它们在应用重启后依然存在。
*   **Hangfire Dashboard**:一个内置 UI,用于监控作业状态、历史、重试,以及手动触发作业。
*   **Recurring Jobs**:按 Cron 表达式定义的特定计划运行的任务。
*   **Hangfire Server**:负责从存储介质中取出并执行作业的后台进程。
*   **Job IDs**:周期性作业的唯一标识符,通过更新已有的计划来防止重复注册。

### 课程笔记

虽然 `BackgroundService` 中的 `PeriodicTimer` 适用于简单、轻量的周期性任务,但它缺乏关键定时工作所需的持久化和监控能力。

```csharp
namespace APIProject.Services
public sealed class CleanupWorker : BackgroundService
{
    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));

        while(await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                await RunCleanUpAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during cleanup");
            }
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/scheduling-jobs-with-hangfire-69958200/?t=10)

Hangfire 通过提供一个用于创建、处理、调度、重试和监控作业的框架来解决这些局限。
要使用 Hangfire,必须安装 `Hangfire.AspNetCore` 包以及一个存储提供程序,例如 `Hangfire.Storage.SQLite`。

#### 配置

在 `Program.cs` 中,通过把 Hangfire 添加到服务集合并指定存储提供程序来配置它。
连接字符串通常从 `appsettings.json` 中获取。

```csharp
builder.Services.AddControllers();

builder.Services.AddHangfire(config =>
{
    config.UseSQLiteStorage(builder.Configuration.GetConnectionString("Hangfire"));
});

builder.Services.AddTransient<DocumentService>();
builder.Services.AddTransient<NotificationService>();
builder.Services.AddTransient<DocumentProcessorService>();
builder.Services.AddSingleton<IBackgroundJobQueue, BackgroundJobQueue>();
builder.Services.AddHostedService<QueuedJobWorker>();
builder.Services.AddScoped<DocumentCleanUpService>();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/scheduling-jobs-with-hangfire-69958200/?t=130)

除了这些服务之外,还必须注册 Hangfire 服务器,它是真正处理后台工作的组件。

```csharp
builder.Services.AddHangfireServer();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/scheduling-jobs-with-hangfire-69958200/?t=175)

为了监控作业,Hangfire 提供了一个仪表盘中间件。
默认情况下,它通常映射到 `/hangfire`。
虽然它对调试和技术支持很有用,但在生产环境中应对这个端点加以保护,以符合网络安全实践。

```csharp
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHangfireDashboard("/hangfire");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/scheduling-jobs-with-hangfire-69958200/?t=160)

#### 实现和调度作业

后台任务通常封装在服务中。
例如,一个 `ReportService` 可以负责生成每日报表的逻辑。

```csharp
namespace APIProject.Services
{
    public sealed class ReportService
    {
        private readonly ILogger<ReportService> _logger;

        public ReportService(
            ILogger<ReportService> logger)
        {
            _logger = logger;
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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/scheduling-jobs-with-hangfire-69958200/?t=235)

要让这个服务按周期运行,使用 `RecurringJob.AddOrUpdate` 方法。
该方法需要一个唯一的 Job ID、一个调用服务方法的 lambda 表达式,以及一个 Cron 表达式。
使用固定不变的 Job ID 可以确保 Hangfire 更新已有的计划,而不是在每次应用启动时创建重复的作业。

```csharp
// Scheduling a daily report
RecurringJob.AddOrUpdate<ReportService>(
    "daily-report", 
    reportService => reportService.GenerateDailyReportAsync(), 
    Cron.Daily);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/scheduling-jobs-with-hangfire-69958200/?t=325)

#### 监控与执行

Hangfire 仪表盘提供了对作业生命周期的洞察,包括已入队、处理中、成功和失败等状态。
它还会跟踪延迟和执行时长等指标。
在内部,Hangfire 会自动处理服务激活和方法调用。

```csharp
// Internal execution logic as seen in Dashboard
// Id: #1
using APIProject.Services;

var reportService = Activate<ReportService>();
await reportService.GenerateDailyReportAsync();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/scheduling-jobs-with-hangfire-69958200/?t=385)

```text
RecurringJob.AddOrUpdate<ReportService>("daily-report", ..., Cron.Daily)
        |
        v
  storage (SQLite, connection string "Hangfire")
        |
        v
  Hangfire Server (AddHangfireServer)
        |   Activate<ReportService>()
        |   GenerateDailyReportAsync()
        v
  Enqueued -> Processing -> Succeeded / Failed
        |
        v
  Hangfire Dashboard (/hangfire)
```

#### 设计考量

虽然 Hangfire 简化了后台处理,但它仍需要可靠的工程判断:
1.  **Storage Provider**:选择一个与你的规模和可靠性需求相匹配的提供程序(SQLite、SQL Server、Redis)。
2.  **Security**:确保仪表盘经过了正确的授权。
3.  **Abstraction**:避免把复杂的业务逻辑直接写在注册代码中;应使用服务和抽象。
4.  **Scaling**:如果运行多个应用实例,要考虑需要多少个 Hangfire 服务器才能有效处理工作负载。

## 3. Scheduling Jobs With Quartz

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/scheduling-jobs-with-quartz-69958201/) · 10:42

Quartz 是一个强大的 .NET 调度框架,为后台工作提供了一种结构化的方法。
与简单的计时器不同,Quartz 把工作的定义与决定该工作何时运行的逻辑分离开来。
这种分离通过三个主要组件实现:
- **Job**:定义要执行的实际工作。
- **Trigger**:定义工作应在何时执行的计划或条件。
- **Scheduler**:通过把作业与各自的触发器连接起来,协调执行。

```text
              +-----------+
              | Scheduler |
              +-----------+
               /         \
              v           v
       +---------+    +---------+
       |   Job   |<---| Trigger |
       |  (what) |    |  (when) |
       +---------+    +---------+
```

### 核心概念

- **IJob**:任何定义 Quartz 任务的类都必须实现的接口。
- **JobKey**:一个唯一标识符,用于在配置触发器时引用特定的作业类型。
- **Simple Schedule**:一种以固定间隔运行作业的基本调度方式。
- **Cron Schedule**:一种使用标准 cron 记法、适用于复杂的基于日历的定时的灵活调度方式。
- **DisallowConcurrentExecution**:一个特性(attribute),防止同一作业的多个实例同时运行。

### 课程笔记

要在 ASP.NET Core 应用中开始使用 Quartz,请安装 `Quartz.AspNetCore`(或 `Quartz.Extensions.Hosting`)NuGet 包。

#### 实现一个作业

Quartz 中的每个后台任务都必须实现 `IJob` 接口。
该接口要求实现一个 `Execute` 方法,该方法接收一个 `IJobExecutionContext`。
这个上下文提供有关作业运行时环境的信息。
在下面的示例中,创建了一个 `CleanupJob`,它把信息记录到控制台,以模拟一个后台清理任务。

```csharp
using Quartz;

namespace APIProject.QuartzJobs
{
    public class CleanupJob : IJob
    {
        private readonly ILogger<CleanupJob> _logger;

        public CleanupJob(ILogger<CleanupJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Executing cleanup job at {Time}", DateTimeOffset.Now);

            return Task.CompletedTask;
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/scheduling-jobs-with-quartz-69958201/?t=175)

#### 注册 Quartz 服务

Quartz 必须在 `Program.cs` 文件中注册。
这包括把 Quartz 添加到服务集合、定义一个用于标识的 `JobKey`,以及配置作业及其触发器。
`JobKey` 使触发器能够显式地关联到正确的作业定义。

```csharp
builder.Services.AddQuartz(quartz =>
{
    var jobKey = new JobKey("CleanupJob");
    quartz.AddJob<CleanupJob>(jobOptions =>
    {
        jobOptions.WithIdentity(jobKey);
    });

    quartz.AddTrigger(triggerOptions =>
    {
        triggerOptions.
            ForJob(jobKey)
            .WithIdentity("CleanupJob-Trigger")
            .WithSimpleSchedule(scheduleOptions =>
                scheduleOptions.WithIntervalInSeconds(10)
                    .RepeatForever());
    });
});
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/scheduling-jobs-with-quartz-69958201/?t=400)

```text
AddJob<CleanupJob>                    AddTrigger
  WithIdentity(jobKey)                  WithIdentity("CleanupJob-Trigger")
        ^                               WithSimpleSchedule(10s, RepeatForever)
        |                                       |
        +------ jobKey = "CleanupJob" <---------+
                                         ForJob(jobKey)
```

虽然上面的示例使用 `WithSimpleSchedule` 设置了 10 秒的间隔,但 Quartz 也支持 `WithCronSchedule("0 0 * * * ?")`,用于更复杂的需求,例如每小时运行一次作业,或每周一凌晨 2:00 运行。

#### 配置托管服务

要把 Quartz 集成到 ASP.NET Core 应用的生命周期中,必须添加 Quartz 托管服务。
一个关键的配置选项是 `WaitForJobsToComplete`,它确保应用关闭过程会等待所有正在执行的作业完成后再退出。

```csharp
builder.Services.AddQuartzHostedService(hostedServiceOptions =>
{
    hostedServiceOptions.WaitForJobsToComplete = true;
});
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/scheduling-jobs-with-quartz-69958201/?t=460)

#### 管理并发

默认情况下,即使上一次执行仍在运行,Quartz 也可能会启动作业的一个新实例(例如,一个每 10 秒调度一次的作业需要 30 秒才能完成)。
为了防止数据冲突或重复工作,请在作业类上使用 `[DisallowConcurrentExecution]` 特性。
这确保 Quartz 会等待上一个实例完成后再启动下一个。

```csharp
using Quartz;

namespace APIProject.QuartzJobs
{
    [DisallowConcurrentExecution]
    public class CleanupJob : IJob
    {
        private readonly ILogger<CleanupJob> _logger;

        public CleanupJob(ILogger<CleanupJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Executing cleanup job at {Time}", DateTimeOffset.Now);

            return Task.CompletedTask;
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/scheduling-jobs-with-quartz-69958201/?t=550)

## 4. Lightweight Scheduling with TickerQ

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/lightweight-scheduling-with-tickerq-69958202/) · 15:37

TickerQ 是一个轻量级的 .NET 调度库,它让开发者可以定义和调度后台作业,而无需立即承担数据库迁移或复杂队列基础设施的开销。
它介于手动的 `PeriodicTimer` 循环与 Hangfire 或 Quartz 这类功能完备的框架之间。

### 核心概念

*   **Lightweight Scheduling**:只需极少的设置;可作为迁移到更重量级框架之前的"第一步"调度方案。
*   **Source Generators**:使用特性在编译时发现并注册作业函数。
*   **In-Memory by Default**:作业存储在内存中,这意味着除非添加持久化提供程序,否则它们在应用重启后不会保留。
*   **Time Tickers**:被安排在未来某个特定时间运行的一次性作业(延迟处理)。
*   **Cron Tickers**:使用标准 CRON 表达式定义的周期性作业。
*   **Dashboard**:一个可选的 UI,用于监控作业状态、执行历史以及手动触发。

### 课程笔记

#### 配置与设置

要使用 TickerQ,请安装 `TickerQ` 和 `TickerQ.Dashboard` NuGet 包。
配置包括在 DI 容器中注册服务,以及把中间件添加到请求管道中。
默认情况下,TickerQ 使用内存存储,这适合开发环境,但对于生产工作负载缺乏持久性。

```csharp
builder.Services.AddTickerQ(options => 
{
    // Configuration options can be set here
});
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/lightweight-scheduling-with-tickerq-69958202/?t=130)

在应用构建器初始化之后,启用 TickerQ 引擎及其仪表盘:

```csharp
var app = builder.Build();

app.UseTickerQ();
// The dashboard is typically mapped to /tickerq/dashboard
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/lightweight-scheduling-with-tickerq-69958202/?t=160)

#### 定义作业函数

TickerQ 中的作业被定义为类中的方法。
与需要实现接口的 Quartz 不同,TickerQ 使用 `[TickerFunction]` 特性。
一个源生成器会发现这些方法并自动注册它们。

```csharp
namespace APIProject.TickerQJobs
{
    public class ReportJobs
    {
        private readonly ILogger<ReportJobs> _logger;

        public ReportJobs(ILogger<ReportJobs> logger)
        {
            _logger = logger;
        }

        [TickerFunction("GenerateDailyReport")]
        public Task GenerateDailyReport(TickerFunctionContext context, CancellationToken cancellation)
        { 
            _logger.LogInformation("Generating daily report at {Time}", DateTimeOffset.Now);
            return Task.CompletedTask;
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/lightweight-scheduling-with-tickerq-69958202/?t=280)

#### 调度一次性作业(Time Tickers)

要安排一个作业在特定时间运行一次,请注入 `ITimeTickerManager<TimeTickerEntity>`。
这对于延迟任务很有用。
`AddAsync` 方法会返回一个结果,指示该作业是否已成功添加到调度器中。

```csharp
app.MapPost("/reports/schedule", 
    async (ITimeTickerManager<TimeTickerEntity> timeTicker, CancellationToken cancellation) =>
    {
        var result = await timeTicker.AddAsync(new TimeTickerEntity
        {
            Function = "GenerateDailyReport",
            ExecutionTime = DateTime.UtcNow.AddSeconds(5) // Schedule to run in 5 seconds
        }, cancellation);

        return result.IsSucceeded ?
            Results.Accepted($"/reports/jobs/{result.Result.Id}") :
            Results.BadRequest("Failed to schedule report generation");
    });
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/lightweight-scheduling-with-tickerq-69958202/?t=475)

```text
POST /reports/schedule
   |
   v
ITimeTickerManager<TimeTickerEntity>.AddAsync
   |   Function      = "GenerateDailyReport"
   |   ExecutionTime = DateTime.UtcNow.AddSeconds(5)
   |
   +-- IsSucceeded --> Results.Accepted("/reports/jobs/{Id}")
   +-- otherwise ----> Results.BadRequest(...)

        ... 5 seconds later ...

[TickerFunction("GenerateDailyReport")] GenerateDailyReport(...)
```

#### 使用类型化载荷

作业通常需要输入数据。
TickerQ 通过使用 `TickerFunctionContext<TRequest>` 来支持类型化请求。
你必须为载荷定义一个 record 或 class,并在调度作业时传入它。

```csharp
public record ReportRequest(Guid RequestId);

[TickerFunction("GenerateDailyReport")]
public Task GenerateDailyReport(TickerFunctionContext<ReportRequest> context, CancellationToken cancellation)
{
    var request = context.Request;
    _logger.LogInformation("Request {RequestId} Received: Generating daily report at {Time}", request.RequestId, DateTimeOffset.Now);
    return Task.CompletedTask;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/lightweight-scheduling-with-tickerq-69958202/?t=580)

通过管理器进行调度时,把请求对象包含在 `TimeTickerEntity` 中:

```csharp
app.MapPost("/reports/{reportId:guid}/schedule",
    async (ITimeTickerManager<TimeTickerEntity> timeTicker, CancellationToken cancellation, Guid reportId) =>
{
    var result = await timeTicker.AddAsync(new TimeTickerEntity
    {
        Function = "GenerateDailyReport",
        ExecutionTime = DateTime.UtcNow.AddSeconds(5),
        Request = new ReportRequest(reportId)
    }, cancellation);

    return result.IsSucceeded ?
        Results.Accepted($"/reports/jobs/{result.Result.Id}") :
        Results.BadRequest("Failed to schedule report generation");
});
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/lightweight-scheduling-with-tickerq-69958202/?t=625)

#### 周期性作业(Cron Tickers)

对于周期性工作,把一个 CRON 表达式作为 `[TickerFunction]` 特性的第二个参数传入。
这会把该作业转变为一个按计划自动运行的 Cron Ticker。

```csharp
[TickerFunction("GenerateDailyReport", "*/1 * * * * *")] // Example: Runs every second
public Task GenerateDailyReport(TickerFunctionContext<ReportRequest> context, CancellationToken cancellation)
{
    _logger.LogInformation("Recurring report generation triggered.");
    return Task.CompletedTask;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/lightweight-scheduling-with-tickerq-69958202/?t=685)

#### 监控与持久化

TickerQ 仪表盘(可通过 `/tickerq/dashboard` 访问)提供了对已排队作业、执行历史以及成功/失败状态的可见性。
虽然本课使用的是内存存储,但 TickerQ 支持通过 EF Core 或 Redis 实现持久化,可以在 `Program.cs` 中进行配置,以确保作业在应用重启后依然存在。
