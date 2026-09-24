# Worker Services

> 课程:[From Zero to Hero: Background Processing in .NET](https://dometrain.com/course/from-zero-to-hero-background-processing-in-dotnet/) · 第 4 章
> 共 5 课 · 约 41:17
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

---

## 课程索引

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Why We're Moving Background Work Out of the API](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/why-we-re-moving-background-work-out-of-the-api-69958188/) | 1:34 | [↓](#1-why-were-moving-background-work-out-of-the-api) |
| 2 | [Creating a Worker Service](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/creating-a-worker-service-69958189/) | 8:53 | [↓](#2-creating-a-worker-service) |
| 3 | [Hosting, Configuration and Dependency Injection](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/hosting-configuration-and-dependency-injection-69958190/) | 7:10 | [↓](#3-hosting-configuration-and-dependency-injection) |
| 4 | [Designing Worker Services](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/designing-worker-services-69958191/) | 12:31 | [↓](#4-designing-worker-services) |
| 5 | [Running Worker Services in Production](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/running-worker-services-in-production-69958192/) | 11:09 | [↓](#5-running-worker-services-in-production) |

---

## 1. Why We're Moving Background Work Out of the API

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/why-we-re-moving-background-work-out-of-the-api-69958188/) · 1:34

### 总结

把后台处理从 API 中移出、放进专门的 worker service,可以解决资源竞争、扩展效率低下和可靠性方面的问题。
虽然 API 内部的 hosted service 适合轻量任务,但像文件处理或报表生成这样更重的工作负载会拖慢 API 的性能。
把这些关注点解耦之后,后台处理层就可以独立扩展,并且后台任务的失败不会影响 Web 应用的可用性。

### 核心概念

- **Resource Competition**:后台任务与 API 共享 CPU 和内存,可能拖慢请求的响应时间。
- **Scaling Inefficiency**:与 API 耦合在一起的后台任务,要提升处理能力就必须扩展整个 Web 层。
- **Architectural Resilience**:进程分离可以防止后台服务的故障或重启影响 API 的生命周期。
- **Worker Services**:一种 .NET 模板,专为独立于 Web 主机的长时间运行后台任务而设计。

### 课程笔记

虽然 hosted service 对于属于应用程序本身的后台任务很有效,但它们有一个明显的局限:它们运行在与 API 相同的进程中。
这种共享环境意味着后台任务和 API 共享同样的内存、CPU 资源和生命周期。

对于轻量任务来说,这种共享进程模型已经足够。
然而,随着工作负载变重,比如处理大文件、生成复杂报表,或编排多个外部系统调用,这套架构就开始失效了。
繁重的后台操作会与进来的 HTTP 请求争抢系统资源,导致延迟上升和 API 性能不稳定。

```text
  In-process (hosted service)        Out-of-process (worker service)
+-----------------------------+    +-------------+  +----------------+
| API process                 |    | API process |  | Worker process |
|   HTTP requests             |    |  HTTP       |  |  background    |
|   background task           |    |  requests   |  |  work          |
|   shared memory / CPU       |    +-------------+  +----------------+
|   shared lifecycle          |      独立部署、独立扩展、独立重启
+-----------------------------+
```

#### 扩展性与效率

一个关键的考量是系统如何扩展。
当后台处理被托管在 API 内部时,扩展后台工作的唯一方式就是扩展整个 API。
这非常低效,因为即使 Web 层本身并没有承受压力,也被迫部署更多的 Web 服务器实例。
把后台工作负载分离出来之后,处理层就可以根据这些任务本身的具体需求独立扩展。

#### 可靠性与弹性

分离后台工作还能提升系统整体的可靠性。
在耦合的架构中,后台服务的崩溃有可能拖垮整个 API 进程。
此外,如果后台 worker 因为更新或配置变更需要重启,整个 Web 应用也必须一起重启。
把这些组件解耦会形成一个更有弹性的架构,无论后台 worker 处于什么状态,API 都能保持可用。

为了解决这些架构上的挑战,.NET 提供了 **Worker Service** 模板,可以用来创建专用的、长时间运行的后台进程。

---

## 2. Creating a Worker Service

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/creating-a-worker-service-69958189/) · 8:53

### 总结

在 .NET 中创建独立的 Worker Service,可以让后台任务独立于 API 运行,从而带来更好的可扩展性和部署方式。
本课讲解如何使用 Worker Service 模板创建项目、BackgroundService 类的结构,以及在长生命周期的 worker 应用中使用 IServiceScopeFactory 解析 scoped 服务这一必备模式。

### 核心概念

- **Worker Service Template**:一种专门的 .NET 项目类型(`dotnet new worker`),为长时间运行的后台进程而设计。
- **Independent Scaling**:把后台工作移出 API 进程,使 worker 可以独立部署和扩展。
- **BackgroundService**:实现长时间运行任务的基类,使用 `ExecuteAsync` 方法。
- **Scoped Service Resolution**:在类似单例的 hosted service 中,必须使用 `IServiceScopeFactory` 来解析 scoped 依赖(比如数据库上下文)。

### 课程笔记

Worker Service 是一个专门用于后台处理的独立应用程序。
与嵌入在 Web API 内部的 hosted service 不同,Worker Service 作为自己的进程运行。
这种分离的好处在于,worker 可以独立运行、部署和扩展,而不必绑定在 API 的生命周期上。

要创建一个新项目,你可以使用 .NET CLI 命令 `dotnet new worker`,或者在 Visual Studio 中选择 **Worker Service** 模板。
这个模板会生成 `Program.cs` 中的基础主机配置,以及一个默认的 worker 实现。

```csharp
namespace Job_Processor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Worker>();

            var host = builder.Build();
            host.Run();
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/creating-a-worker-service-69958189/?t=100)

生成的 `Worker` 类继承自 `BackgroundService`。
核心逻辑位于 `ExecuteAsync` 方法中,其中包含一个循环,它一直运行到 `CancellationToken` 发出应用程序正在关闭的信号为止。

```csharp
namespace Job_Processor
{
    public class Worker(ILogger<Worker> logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (logger.IsEnabled(LogLevel.Information))
                {
                    logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/creating-a-worker-service-69958189/?t=115)

#### 把逻辑抽象到 Processor 中

为了保持整洁的架构并便于测试,业务逻辑应该从 `Worker` 类中抽离出去。
可以引入一个独立的组件,比如 `JobProcessor`,来处理真正的工作,例如获取和处理作业。

```csharp
namespace Job_Processor
{
    public class JobProcessor
    {
        public Task ProcessNextJob()
        {
            // Placeholder for job processing logic
            return Task.CompletedTask;
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/creating-a-worker-service-69958189/?t=220)

这个 processor 必须注册到依赖注入(DI)容器中。
在后台处理场景里,这些 processor 往往依赖数据库上下文,因此需要以 **Scoped** 生命周期注册。

```csharp
namespace Job_Processor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Worker>();
            builder.Services.AddScoped<JobProcessor>();
            var host = builder.Build();
            host.Run();
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/creating-a-worker-service-69958189/?t=250)

#### 在单例 Worker 中处理 Scoped 服务

因为 `BackgroundService` 是作为 hosted service 注册的(其行为类似单例),你不能把像 `JobProcessor` 这样的 scoped 服务直接注入它的构造函数。
这样做会导致运行时错误,因为 scoped 服务无法从根 provider 中解析出来。

要解决这个问题,请把 `IServiceScopeFactory` 注入到 worker 中。
在 `ExecuteAsync` 循环内部,为每一次迭代手动创建一个 scope。
这样你就能正确地解析 scoped 的 `JobProcessor`,确保它的依赖(比如数据库上下文)在每个作业周期结束后被妥善管理和释放。

```csharp
namespace Job_Processor
{
    public class Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Processing next job at: {time}", DateTimeOffset.Now);

                using (var scope = serviceScopeFactory.CreateScope())
                {
                    var jobProcessor = scope.ServiceProvider.GetRequiredService<JobProcessor>();
                    await jobProcessor.ProcessNextJob();
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/creating-a-worker-service-69958189/?t=475)

```text
Worker (hosted service, 类似单例)
  |
  |  ExecuteAsync 循环的每一次迭代
  v
+------------------------------------------------+
| serviceScopeFactory.CreateScope()              |
|   GetRequiredService<JobProcessor>()   (Scoped)|
|   await jobProcessor.ProcessNextJob()          |
+------------------------------------------------+
  |  scope 释放,其依赖随之释放
  v
await Task.Delay(1000, stoppingToken)
```

这种模式建立了一个稳固的结构:worker 负责协调时机和执行,而 scoped 服务负责真正的业务逻辑。

---

## 3. Hosting, Configuration and Dependency Injection

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/hosting-configuration-and-dependency-injection-69958190/) · 7:10

### 总结

.NET 中的 worker service 使用与 ASP.NET 应用程序相同的泛型主机(generic host)基础设施,为依赖注入、配置和日志提供了统一的方式。
这种架构让后台服务可以作为结构良好、可观测的应用程序运行,并通过使用 scoped 服务和外部化配置来有效管理资源。

### 核心概念

- Generic Host(`Host.CreateApplicationBuilder`)
- 依赖注入(DI)容器
- Worker 中的 Singleton 与 Scoped 生命周期
- 用于手动管理 scope 的 `IServiceScopeFactory`
- 通过 `appsettings.json` 和 `IConfiguration` 进行配置
- 使用 `ILogger<T>` 记录日志以获得可观测性

### 课程笔记

#### Generic Host

.NET worker service 的核心是 generic host。
这与驱动 ASP.NET 应用程序的是同一个主机,只是没有 Web 服务器组件。
主机负责创建依赖注入容器、加载配置、设置日志,以及管理应用程序的启动和关闭生命周期。

在 `Program.cs` 中,主机通过 `Host.CreateApplicationBuilder` 进行配置和构建。
这使得可以在运行主机之前注册服务。

```csharp
namespace Job_Processor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Worker>();
            builder.Services.AddScoped<JobProcessor>();
            var host = builder.Build();
            host.Run();
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/hosting-configuration-and-dependency-injection-69958190/?t=10)

#### 依赖注入与 Scoped 服务

Worker 类应该保持轻薄,它的主要职责是编排执行流程并把处理工作委托出去。
业务逻辑应该移到独立的服务中,比如 `JobProcessor` 类。

因为 `BackgroundService` 是作为单例注册的,你不能把 scoped 服务直接注入它的构造函数。
取而代之的是,你必须注入 `IServiceScopeFactory`,并在执行循环内部手动创建一个 scope。
这可以确保依赖(比如数据库上下文)在每次迭代中被正确释放和刷新。

```csharp
namespace Job_Processor
{
    public class Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Processing next job at: {time}", DateTimeOffset.Now);
                using var scope = serviceScopeFactory.CreateScope();
                var jobProcessor = scope.ServiceProvider.GetRequiredService<JobProcessor>();

                await jobProcessor.ProcessNextJob();
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/hosting-configuration-and-dependency-injection-69958190/?t=220)

#### 配置

worker service 中的配置遵循与 ASP.NET 相同的模式,支持 `appsettings.json`、环境变量和 user secrets。
与其把轮询间隔这类值硬编码,不如把它们移到配置文件中。

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "LoopInterval": 2000
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/hosting-configuration-and-dependency-injection-69958190/?t=250)

要访问这些值,请把 `IConfiguration` 注入到 worker 中。
然后你可以使用 `GetValue<T>` 获取具体的设置,并把它们赋给私有字段。

```csharp
namespace Job_Processor
{
    public class Worker(ILogger<Worker> logger,
        IServiceScopeFactory serviceScopeFactory,
        IConfiguration configuration) : BackgroundService
    {
        private int _loopInterval = configuration.GetValue<int>("LoopInterval");

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Processing next job at: {time}", DateTimeOffset.Now);
                // ...
            }
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/hosting-configuration-and-dependency-injection-69958190/?t=295)

#### 日志与可观测性

由于 worker service 往往在没有用户界面的情况下运行,日志是监控应用健康状况和诊断故障的首要事实来源。
`ILogger<T>` 接口默认可以通过依赖注入获得。
开发者应该使用不同的日志级别,比如 Information、Warning、Error 和 Critical,来提供服务运行状况的可见性。

```csharp
namespace Job_Processor
{
    public class Worker(ILogger<Worker> logger,
        IServiceScopeFactory serviceScopeFactory,
        IConfiguration configuration) : BackgroundService
    {
        private int _loopInterval = configuration.GetValue<int>("LoopInterval");

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Processing next job at: {time}", DateTimeOffset.Now);
                // example critical logging
                logger.LogCritical("WARNING CRITICAL ERROR!");
                using var scope = serviceScopeFactory.CreateScope();
                var jobProcessor = scope.ServiceProvider.GetRequiredService<JobProcessor>();

                await jobProcessor.ProcessNextJob();
                await Task.Delay(_loopInterval, stoppingToken);
            }
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/hosting-configuration-and-dependency-injection-69958190/?t=385)

---

## 4. Designing Worker Services

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/designing-worker-services-69958191/) · 12:31

### 总结

要设计出有效的 worker service,需要超越简单的循环,以可预测的方式处理长时间运行的工作负载。
关键的考量包括:通过合理的延迟或批处理来避免忙等待,实现健壮的错误处理以防止单个任务的失败拖垮整个服务,以及管理作业状态以保证跨处理周期的一致性。

### 核心概念

- **Long-running workloads**:需要数秒、数分钟甚至数小时的任务,比如文档处理或报表生成。
- **Retrieve-Process-Repeat**:后台 worker 的基本模式。
- **Busy Waiting**:worker 在没有工作可做时仍不断循环,从而消耗不必要的 CPU 周期的情况。
- **Batch Processing**:在进入主延迟之前,用嵌套循环处理掉所有可用作业,以此优化吞吐量。
- **Resilience**:使用 try-catch 块,确保单个作业的失败不会终止整个后台服务。
- **Job State Management**:通过 In Progress、Complete 和 Failed 等状态跟踪作业的生命周期。

### 课程笔记

Worker service 主要是为那些无法瞬间完成的、长时间运行的后台工作负载而设计的。
这些任务往往涉及外部系统集成、文档处理或报表生成。
一旦部署,这些服务会持续运行,因此架构设计对于稳定性和资源效率至关重要。

#### 核心模式

从高层来看,大多数 worker 都遵循一个简单的循环:获取工作、处理这份工作、然后重复。
一个使用 `BackgroundService` 的基础实现可能是这样的:

```csharp
namespace Job_Processor
{
    public class Worker(ILogger<Worker> logger,
        IServiceScopeFactory serviceScopeFactory,
        IConfiguration configuration) : BackgroundService
    {
        private int _loopInterval = configuration.GetValue<int>("LoopInterval");

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Processing next job at: {time}", DateTimeOffset.Now);
                using var scope = serviceScopeFactory.CreateScope();
                var jobProcessor = scope.ServiceProvider.GetRequiredService<JobProcessor>();

                await jobProcessor.ProcessNextJob();
                await Task.Delay(_loopInterval, stoppingToken);
            }
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/designing-worker-services-69958191/?t=10)

```text
  +--> retrieve work --> process that work --> repeat --+
  |                                                     |
  +------------ await Task.Delay(_loopInterval) <-------+
```

`JobProcessor` 服务负责真正的逻辑。
在基础的设置中,它提供获取和处理作业数据的方法。

```csharp
using System;
using System.Collections.Generic;
using System.Text;

namespace Job_Processor
{
    public class JobProcessor
    {
        public Task ProcessNextJob(string job)
        {
            return Task.CompletedTask;
        }

        public Task<string> GetNextJob()
        {
            return Task.FromResult("Next job details");
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/designing-worker-services-69958191/?t=85)

#### 避免忙等待

后台处理中一个常见的问题是「忙等待」(busy waiting),也就是循环持续运行,即使没有工作可做也在消耗 CPU。
一个简单的改进是在尝试处理之前先检查作业是否存在。

```csharp
while (!stoppingToken.IsCancellationRequested)
{
    logger.LogInformation("Processing next job at: {time}", DateTimeOffset.Now);
    using var scope = serviceScopeFactory.CreateScope();
    var jobProcessor = scope.ServiceProvider.GetRequiredService<JobProcessor>();

    var job = await jobProcessor.GetNextJob();

    if (job != null)
    {
        await jobProcessor.ProcessNextJob(job);
    }

    await Task.Delay(_loopInterval, stoppingToken);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/designing-worker-services-69958191/?t=145)

#### 批处理

如果该服务是某个作业队列的唯一消费者,那么在进入延迟期之前批量处理掉所有可用作业通常更高效。
通过检查待处理作业的总数,worker 可以用一个嵌套的 `while` 循环把队列清空。

```csharp
while (!stoppingToken.IsCancellationRequested)
{
    logger.LogInformation("Processing next job at: {time}", DateTimeOffset.Now);
    using var scope = serviceScopeFactory.CreateScope();
    var jobProcessor = scope.ServiceProvider.GetRequiredService<JobProcessor>();

    var pendingJobs = await jobProcessor.GetTotalPendingJobs();

    if (pendingJobs > 0)
    {
        while (pendingJobs > 0)
        {
            var job = await jobProcessor.GetNextJob();
            await jobProcessor.ProcessNextJob(job);
            pendingJobs--;
        }
    }

    await Task.Delay(_loopInterval, stoppingToken);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/designing-worker-services-69958191/?t=310)

```text
外层 while (!stoppingToken.IsCancellationRequested)
  |
  +-- pendingJobs = GetTotalPendingJobs()
  |
  +-- 内层 while (pendingJobs > 0)
  |        GetNextJob() --> ProcessNextJob(job) --> pendingJobs--
  |
  +-- await Task.Delay(_loopInterval, stoppingToken)
```

注意:这种批处理策略最适合单实例的 worker。
如果多个服务实例共享同一个队列,待处理作业数可能会在外部发生变化,使得这种本地递减的逻辑不那么可靠。

#### 处理失败

弹性至关重要;单个作业的失败绝不应该拖垮整个 worker service。
把处理逻辑包裹在 `try-catch` 块中,可以确保异常被记录下来,并且 worker 继续处理下一个作业。
关键是要把 `pendingJobs` 计数器的递减放在 `try` 块之外或者 `finally` 块之内,以免某个作业持续失败时造成无限循环。

```csharp
while (pendingJobs > 0)
{
    var job = await jobProcessor.GetNextJob();
    try
    {
        await jobProcessor.ProcessNextJob(job);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error processing job: {job}", job);
    }

    pendingJobs--;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/designing-worker-services-69958191/?t=505)

#### 管理作业状态

对于生产级的系统,跟踪作业状态对于一致性和恢复是必要的。
这意味着在处理之前把作业标记为「In Progress」,成功时标记为「Complete」,发生异常时标记为「Failed」。

```csharp
while (pendingJobs > 0)
{
    var job = await jobProcessor.GetNextJob();
    try
    {
        await jobProcessor.MarkJobInProcess(job);
        await jobProcessor.ProcessNextJob(job);
        await jobProcessor.MarkJobAsComplete(job);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error processing job: {job}", job);
        await jobProcessor.MarkJobAsFailed(job);
    }

    pendingJobs--;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/designing-worker-services-69958191/?t=655)

```text
GetNextJob()
    |
    v
MarkJobInProcess --> ProcessNextJob --+--> MarkJobAsComplete
                                      |
                              (Exception)
                                      |
                                      +--> MarkJobAsFailed
```

---

## 5. Running Worker Services in Production

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/running-worker-services-in-production-69958192/) · 11:09

### 总结

Worker service 是长时间运行的后台应用程序,在生产环境中需要可靠的管理、自动重启和故障恢复。
与 API 不同,它们的运行与 HTTP 请求无关,既可以作为原生 Windows Service 托管,也可以运行在 Docker 容器中。
本课介绍使用传统 Windows 基础设施和现代容器化两种方式部署 worker 的实际步骤,并强调按环境区分配置以及弹性设计的重要性。

### 核心概念

- Worker Service 与 API 之间的差异。
- 使用 `AddWindowsService` 作为 Windows Service 托管。
- 使用 `sc.exe` 注册服务。
- 使用 Docker 和 Visual Studio 工具进行容器化。
- 生产环境的要求:弹性、环境配置和独立可扩展性。

### 课程笔记

Worker service 与标准的 Web API 不同,因为它们不响应 HTTP 请求;相反,它们在后台持续运行。
这种运行模式要求它们能够自动启动、从故障中恢复,并且无需人工干预即可运行。

#### Worker 的实现

worker service 被实现为一个继承自 `BackgroundService` 的类。
它使用 `CancellationToken` 来处理优雅关闭,并且通常从一个 scoped provider 中解析服务来完成工作。

```csharp
using Microsoft.Extensions.DependencyInjection;

namespace Job_Processor
{
    public class Worker(ILogger<Worker> logger,
        IServiceScopeFactory serviceScopeFactory,
        IConfiguration configuration) : BackgroundService
    {
        private int _loopInterval = configuration.GetValue<int>("LoopInterval");

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Processing next job at: {time}", DateTimeOffset.Now);
                using var scope = serviceScopeFactory.CreateScope();
                var jobProcessor = scope.ServiceProvider.GetRequiredService<JobProcessor>();

                var pendingJobs = await jobProcessor.GetTotalPendingJobs();

                if (pendingJobs > 0)
                {
                    while (pendingJobs > 0)
                    {
                        var job = await jobProcessor.GetNextJob();
                        // Process job logic here
                    }
                }
                await Task.Delay(_loopInterval, stoppingToken);
            }
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/running-worker-services-in-production-69958192/?t=10)

#### 作为 Windows Service 托管

要把一个 .NET worker 作为原生 Windows Service 运行,你必须引入 `Microsoft.Extensions.Hosting.WindowsServices` NuGet 包。
这个包提供了 `AddWindowsService` 扩展方法,它把 .NET 主机与 Windows Service Control Manager(SCM)集成起来。

```csharp
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Job_Processor
{
    public class Program
    {
        public static void Main(string[] args)
        { 
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Worker>();
            builder.Services.AddScoped<JobProcessor>();
            builder.Services.AddWindowsService();
            var host = builder.Build();
            host.Run();
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/running-worker-services-in-production-69958192/?t=70)

应用程序发布之后(例如通过 Visual Studio 的 Publish 图形界面,或在 Release 模式下使用 CLI),你可以在管理员命令提示符中用 `sc.exe` 工具注册它。
注意 `binPath=` 后面必须有一个空格。

```powershell
sc create MyJobProcessor binPath= "C:\Repos\Dometrain\backproc\Job Processor\bin\Release\net10.0\publish\Job Processor.exe"
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/running-worker-services-in-production-69958192/?t=175)

#### 使用 Docker 进行容器化

一种更现代的做法是把 worker service 运行在容器中,这在云环境中很常见。
Visual Studio 提供了内置工具,可以为项目生成一个多阶段的 `Dockerfile`。

```dockerfile
FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Job Processor/Job Processor.csproj", "Job Processor/"]
RUN dotnet restore "./Job Processor/Job Processor.csproj"
COPY . .
WORKDIR "/src/Job Processor"
RUN dotnet build "./Job Processor.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Job Processor.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Job Processor.dll"]
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/running-worker-services-in-production-69958192/?t=370)

```text
base (runtime:10.0) ---------------------------------+
                                                     |
build (sdk:10.0) --> publish (dotnet publish) -------+--> final
  restore / build                                         COPY --from=publish
                                                          ENTRYPOINT dotnet
```

你可以使用 Docker CLI 构建并运行这个镜像:

- `docker build -t my-job-processor .`
- `docker run my-job-processor`

#### 生产思维与弹性

在迁移到生产环境时,有几个因素变得至关重要:

- **Restart and Resilience**:worker 必须能够安全重启,不丢失工作,也不让系统处于不一致的状态。这是通过持久化作业状态实现的。
- **Environment Configuration**:使用 .NET Host 可以通过 `appsettings.json` 或环境变量提供按环境区分的设置(Development、Staging、Production),涵盖连接字符串和轮询间隔。
- **Scalability**:worker 是独立的进程,可以与主 API 分开部署和管理,从而实现独立扩展。
