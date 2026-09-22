# Hosted Services Fundamentals

> 课程:[From Zero to Hero: Background Processing in .NET](https://dometrain.com/course/from-zero-to-hero-background-processing-in-dotnet/) · 第 3 章
> 共 6 课 · 约 18:51
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

---

## 课程索引

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Understanding The Hosted Service Lifecycle](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/understanding-the-hosted-service-lifecycle-69958182/) | 2:19 | [↓](#1-understanding-the-hosted-service-lifecycle) |
| 2 | [Background Service vs IHostedService](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/background-service-vs-ihostedservice-69958183/) | 2:46 | [↓](#2-background-service-vs-ihostedservice) |
| 3 | [Registering Hosted Services](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/registering-hosted-services-69958184/) | 5:09 | [↓](#3-registering-hosted-services) |
| 4 | [Working with Scoped Dependencies](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/working-with-scoped-dependencies-69958185/) | 5:11 | [↓](#4-working-with-scoped-dependencies) |
| 5 | [Graceful Shutdown and Cancellation](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/graceful-shutdown-and-cancellation-69958186/) | 2:03 | [↓](#5-graceful-shutdown-and-cancellation) |
| 6 | [When Hosted Services are the Right Choice](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/when-hosted-services-are-the-right-choice-69958187/) | 1:23 | [↓](#6-when-hosted-services-are-the-right-choice) |

---

## 1. Understanding The Hosted Service Lifecycle

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/understanding-the-hosted-service-lifecycle-69958182/) · 2:19

### 总结

本课介绍 .NET 中托管服务(hosted service)的基础知识,讲解它们如何通过泛型主机(generic host)接入应用程序的生命周期。
内容涵盖 IHostedService 接口、优雅关闭的重要性,以及在 ASP.NET Core 环境中安全管理后台工作负载时取消令牌(cancellation token)所扮演的角色。

### 核心概念

- Generic Host 对 DI、日志和配置的管理。
- IHostedService 接口作为后台工作的基础。
- 生命周期方法:StartAsync 与 StopAsync。
- 优雅关闭与资源管理。
- 必须使用 CancellationToken 来实现安全终止。

### 课程笔记

在 .NET 中,实现后台处理最简单的方式就是使用托管服务。
这些服务运行在 ASP.NET Core 应用程序内部,并由 .NET 托管基础设施管理。

现代 ASP.NET Core 应用程序建立在 **Generic Host** 之上。
这个主机负责管理应用程序的生命周期,包括依赖注入、配置、日志、应用启动和应用关闭。
托管服务直接接入这个生命周期。

这种集成的核心是 `IHostedService` 接口,它定义了服务如何响应应用程序的启动与停止事件:

```csharp
#nullable enable

using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Hosting
{
    public interface IHostedService
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/understanding-the-hosted-service-lifecycle-69958182/?t=10)

当应用程序启动时(通常由 `Program.cs` 中的 `app.Run()` 触发),主机会遍历所有已注册的托管服务并调用它们的 `StartAsync` 方法。

```csharp
namespace _3._1_Understanding_Host_Service_Lifecycle
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/understanding-the-hosted-service-lifecycle-69958182/?t=55)

`IHostedService` 接口定义了两个主要方法:`StartAsync` 和 `StopAsync`。
这两个方法让后台任务能够安全地参与应用程序的生命周期。
应用程序启动时调用 `StartAsync`;应用程序停止时调用 `StopAsync`。

```text
  app.Run()                                application shutdown
      |                                             |
      v                                             v
+-------------------+                    +-------------------+
| Generic Host      |                    | Generic Host      |
+-------------------+                    +-------------------+
      |                                             |
      | StartAsync(cancellationToken)               | StopAsync(...)
      v                                             v
+-------------------------------------------------------------+
| IHostedService                                               |
+-------------------------------------------------------------+
```

### Graceful Shutdown

生命周期管理对后台处理至关重要。
当应用程序关闭时,后台工作不应该被突然中止。
相反,任务应该优雅地停止,并释放它们正在使用的任何资源,比如文件句柄或数据库连接。

为此,托管服务依赖 **cancellation token**。
传给生命周期方法的 `CancellationToken` 是一项绝对的要求。
它向后台任务发出信号,表示主机正在请求关闭,从而让服务能够完成当前这个工作单元并干净地退出。

虽然可以直接实现 `IHostedService`,但大多数开发者会使用 ASP.NET Core 提供的 `BackgroundService` 辅助类,它极大地简化了长时间运行后台任务的实现。

---

## 2. Background Service vs IHostedService

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/background-service-vs-ihostedservice-69958183/) · 2:46

### 总结

BackgroundService 类是 .NET 中的一个抽象基类,它通过处理 IHostedService 接口相关的样板代码来简化后台任务的实现。
开发者不必同时管理 StartAsync 和 StopAsync,而是只需重写一个 ExecuteAsync 方法,把长时间运行的逻辑放在其中。
本课演示了标准的 Worker Service 模板,重点展示 BackgroundService 如何管理应用程序生命周期,并在关闭期间响应取消请求。

### 核心概念

* BackgroundService 基类
* 重写 ExecuteAsync 方法
* Worker Service 模板
* 取消令牌管理
* 与主机的生命周期同步

### 课程笔记

IHostedService 接口提供了对后台任务的细粒度控制,但需要手动实现生命周期方法。
为了简化开发,ASP.NET Core 提供了 BackgroundService 基类,它是生产环境中最常见的实现方式。
这个类处理了底层的管道工作,确保任务与应用程序主机同步地启动和停止。

使用 .NET Worker Service 模板时,应用程序在 Program.cs 中配置。
AddHostedService<T> 方法把后台 worker 注册到依赖注入容器中。

```csharp
namespace _3._2_Background_Service_vs_IHostedService
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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/background-service-vs-ihostedservice-69958183/?t=10)

Worker 类继承自 BackgroundService 并重写 ExecuteAsync 方法。
这个方法被设计为在整个应用程序生命周期内持续运行。

```csharp
namespace _3._2_Background_Service_vs_IHostedService
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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/background-service-vs-ihostedservice-69958183/?t=25)

这种实现通常包含一个 while 循环,用来检查 CancellationToken 的 IsCancellationRequested 属性。
当应用程序开始其关闭流程时,这个令牌就会被触发。
在循环内部执行工作(比如记录日志信息),随后进行一次异步延迟。
把 stoppingToken 传给 Task.Delay 可以确保在关闭开始时延迟立即被中断,从而让服务能够优雅退出并遵从主机的生命周期请求。

---

## 3. Registering Hosted Services

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/registering-hosted-services-69958184/) · 5:09

### 总结

本课演示在 .NET 应用程序中实现和注册托管服务。
内容包括:通过继承 BackgroundService 基类创建后台任务、实现带取消支持的 ExecuteAsync 方法,以及把服务注册到依赖注入(DI)容器中。
本课说明了无论这些服务是托管在专门的 Worker Service 中还是 ASP.NET Core Web API 中,它们的运作方式都完全相同,因为两种环境使用的是同一套底层托管抽象和生命周期管理。

### 核心概念

* 继承 `BackgroundService` 基类以简化 `IHostedService` 的实现。
* 实现抽象方法 `ExecuteAsync`,用于编写长时间运行的后台逻辑。
* 利用 `CancellationToken`(stopping token)来处理应用程序的优雅关闭。
* 通过 `builder.Services.AddHostedService<T>()` 在 DI 容器中注册服务。
* 理解 .NET Host 会自动管理已注册托管服务的生命周期。
* 在 Worker Service 和 Web API 项目中一致地应用后台处理模式。

### 课程笔记

托管服务通过 .NET 的依赖注入(DI)容器进行注册。
要创建一个自定义托管服务,你需要定义一个继承自 `BackgroundService` 基类的类。

默认的 Worker Service 模板提供了一个初始的 `Worker` 类,可以作为这种模式的参考:

```csharp
namespace _3._2_Background_Service_vs_IHostedService
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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/registering-hosted-services-69958184/?t=10)

### Implementing a Custom Background Service

在创建一个新服务(比如 `JobProcessor`)时,你必须实现抽象成员 `ExecuteAsync`。
这个方法通常包含一个循环,持续运行直到 `stoppingToken` 发出取消请求的信号。
在这个循环内部,你执行后台工作,并加入一次异步延迟,以避免循环消耗过多的 CPU 资源。

```csharp
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace _3._2_Background_Service_vs_IHostedService
{
    public class JobProcessor : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("Checking for jobs....");
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/registering-hosted-services-69958184/?t=85)

### BackgroundService vs IHostedService

虽然人们常把它们统称为“托管服务”,但如果你使用 `BackgroundService` 类,就不需要直接实现 `IHostedService` 接口。
`BackgroundService` 是 `Microsoft.Extensions.Hosting` 命名空间提供的一个基类,它已经替你实现了 `IHostedService`。

`IHostedService` 接口定义了主机会调用的核心生命周期方法:

```csharp
namespace Microsoft.Extensions.Hosting
{
    public interface IHostedService
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/registering-hosted-services-69958184/?t=105)

`BackgroundService` 基类处理了启动和停止任务的样板代码,让你可以专注于 `ExecuteAsync` 中的执行逻辑:

```csharp
public abstract class BackgroundService : IHostedService, IDisposable
{
    private Task? _executeTask;
    private CancellationTokenSource? _stoppingCts;

    public virtual Task? ExecuteTask => _executeTask;

    protected abstract Task ExecuteAsync(CancellationToken stoppingToken);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/registering-hosted-services-69958184/?t=115)

```text
+----------------------------------------------------------+
| IHostedService                                           |
|   StartAsync(CancellationToken)                          |
|   StopAsync(CancellationToken)                           |
+----------------------------------------------------------+
                          ^
                          | implements
+----------------------------------------------------------+
| abstract BackgroundService : IHostedService, IDisposable |
|   protected abstract ExecuteAsync(stoppingToken)         |
+----------------------------------------------------------+
                          ^
                          | inherits
+----------------------------------------------------------+
| JobProcessor                                             |
|   override ExecuteAsync(stoppingToken)                   |
+----------------------------------------------------------+
```

### Service Registration

要启用该服务,必须在 `Program.cs` 中使用 `AddHostedService` 扩展方法注册它。
一旦注册,主机会在应用程序启动时自动启动该服务。
可以注册多个托管服务,它们会并发运行。

```csharp
namespace _3._2_Background_Service_vs_IHostedService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Worker>();
            builder.Services.AddHostedService<JobProcessor>();

            var host = builder.Build();
            host.Run();
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/registering-hosted-services-69958184/?t=145)

### Hosted Services in Web APIs

同样的后台处理模式也适用于 ASP.NET Core Web API 项目。
你可以在 API 项目中包含一个 `BackgroundService` 实现,在 API 继续监听 HTTP 请求的同时于后台处理任务。

```csharp
using Microsoft.Extensions.Hosting;

namespace _3._2_Background_Service_vs_IHostedService_in_API
{
    public class JobProcessor : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("Checking for jobs....");
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/registering-hosted-services-69958184/?t=205)

在 Web API 中,注册是在 `Program.cs` 文件里通过 `WebApplicationBuilder` 完成的。
主机会在管理 Web 服务器生命周期的同时,一并管理后台服务的生命周期。

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHostedService<JobProcessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/registering-hosted-services-69958184/?t=250)

---

## 4. Working with Scoped Dependencies

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/working-with-scoped-dependencies-69958185/) · 5:11

### 总结

.NET 中的后台服务是以单例(singleton)方式注册的,当它们需要使用像 Entity Framework DbContext 这样的作用域(scoped)依赖时,就会产生生命周期不匹配的问题。
要解决这个问题,开发者必须注入 IServiceScopeFactory,在后台任务内部手动创建一个服务作用域,确保作用域依赖能为每个工作单元正确地创建和释放,从而避免状态陈旧或访问已释放对象之类的问题。

### 核心概念

- **Singleton Lifetime**:托管服务(实现 `IHostedService` 或 `BackgroundService`)以单例方式注册,并在整个应用程序运行期间存在。
- **Scoped Lifetime**:像 `DbContext`、仓储(repository)和工作单元(unit-of-work)模式这样的服务通常以作用域方式注册,意味着它们只应在某个特定操作(比如一次 Web 请求)期间存活。
- **Captive Dependencies**:把作用域服务直接注入到单例的托管服务中是一种设计错误,会导致不可预测的行为,比如使用已释放的对象或维持陈旧的数据库连接。
- **IServiceScopeFactory**:用于手动创建 `IServiceScope` 的标准接口,使你能够在单例中解析作用域服务。

### 课程笔记

在 ASP.NET Core 中,许多常见服务都是以作用域生命周期注册的。
一个典型的例子是 Entity Framework 的 `DbContext`。
在使用数据库上下文之前,必须先把它注册到依赖注入容器中,通常是在 `Program.cs` 里使用 `AddDbContext`。

```csharp
using Microsoft.EntityFrameworkCore;

namespace APIProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddHostedService<JobProcessor>();
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
            );
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/working-with-scoped-dependencies-69958185/?t=85)

默认情况下,`AddDbContext` 会把上下文注册为作用域服务。
这对托管服务来说是个问题,因为它们是单例。
如果你试图把作用域的 `AppDbContext` 直接注入到 `BackgroundService` 的构造函数中,就打破了生命周期边界。
这可能导致服务生命周期不正确、依赖被释放以及状态陈旧。

```csharp
using Microsoft.Extensions.Hosting;

namespace APIProject
{
    public class JobProcessor : BackgroundService
    {
        private AppDbContext _dbContext;

        // INCORRECT: Do not inject scoped services into a singleton constructor
        public JobProcessor(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("Checking for jobs...");
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/working-with-scoped-dependencies-69958185/?t=175)

要在后台服务中正确使用作用域依赖,你必须注入 `IServiceScopeFactory`。
这个工厂让你可以在需要解析作用域服务时手动创建一个新的作用域,比如在处理循环的每一次迭代中。

在 `ExecuteAsync` 方法内部,使用该工厂调用 `CreateScope()`。
这应该包裹在 `using` 语句或 using 声明中,以确保工作完成后作用域(以及在其中解析出的所有服务)被正确释放。
然后你就可以从该作用域的 `ServiceProvider` 中解析所需的作用域服务。

```csharp
namespace APIProject
{
    public class JobProcessor : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public JobProcessor(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("Checking for jobs...");

                using (var scope = _scopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    await ProcessJob(dbContext);
                }

                await Task.Delay(1000, stoppingToken);
            }
        }

        private static async Task ProcessJob(AppDbContext dbContext)
        {
            Console.WriteLine("Processing a job...");
            // Logic to interact with the database using the scoped context
            await Task.Delay(500);
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/working-with-scoped-dependencies-69958185/?t=280)

```text
JobProcessor  (singleton)
  IServiceScopeFactory _scopeFactory
        |
        | 每一次循环迭代
        v
  +-------------------------------------------+
  | using scope = _scopeFactory.CreateScope() |
  |   scope.ServiceProvider                   |
  |     -> AppDbContext  (scoped)             |
  |   ProcessJob(dbContext)                   |
  +-------------------------------------------+
        | scope 释放
        v
  await Task.Delay(1000, stoppingToken)
```

这种模式确保了作用域依赖在后台任务的每一次迭代中都被正确地创建和释放,从而维护了应用程序服务生命周期的完整性。

---

## 5. Graceful Shutdown and Cancellation

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/graceful-shutdown-and-cancellation-69958186/) · 2:03

### 总结

在生产环境中,应用程序经常会因为更新、容器重新部署或扩缩容操作而重启。
为了防止数据损坏并确保系统可靠性,后台任务必须实现优雅关闭机制。
这主要通过遵从传给 BackgroundService 的 ExecuteAsync 方法的 CancellationToken 来实现,确保长时间运行的循环在收到关闭信号时能够迅速且安全地终止。

### 核心概念

- **Production Volatility**:生产环境中的应用程序是动态的,服务不断地扩缩容、更新和重新部署,这就要求干净地退出。
- **CancellationToken**:传给 `ExecuteAsync` 的 `stoppingToken` 是用来发出主机正在关闭信号的标准机制。
- **Loop Management**:后台循环应该把 `stoppingToken.IsCancellationRequested` 作为终止条件。
- **Data Consistency**:正确处理取消可以避免内存数据与数据库记录不同步的不一致状态。
- **Asynchronous Propagation**:取消令牌应该传递给下游的异步方法(比如 `Task.Delay`),以确保能够立即响应关闭信号。

### 课程笔记

在生产环境中,后台服务并不会不受干扰地无限运行下去。
无论是 CI/CD 流水线部署新版本、容器编排器对实例进行扩缩容,还是例行的服务器更新,服务都会频繁地被停止和启动。
如果一个后台服务在执行关键工作时(比如处理支付或更新数据库记录)被突然终止,就可能让系统处于不一致或不可靠的状态。
例如,数据可能已经在数据库中被修改,却没有与内存状态整合,从而导致数据损坏。

为了应对这一点,.NET 会向 `BackgroundService` 类的 `ExecuteAsync` 方法提供一个 `CancellationToken`。
一旦应用程序开始其关闭流程,这个令牌就会被触发。

### Implementing the Cancellation Loop

健壮后台服务的标准模式,是在主执行循环内检查 `stoppingToken` 的 `IsCancellationRequested` 属性。
这可以确保服务在收到关闭请求后立即停止工作。

```csharp
namespace APIProject

public class JobProcessor : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public JobProcessor(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine("Checking for jobs...");

            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await ProcessJob(dbContext);
            
            // Passing the stoppingToken to Task.Delay ensures the delay is 
            // cancelled immediately when the service stops.
            await Task.Delay(1000, stoppingToken);
        }
    }

    private static async Task ProcessJob(AppDbContext dbContext)
    {
        Console.WriteLine("Processing a job...");
        await Task.Delay(500);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/graceful-shutdown-and-cancellation-69958186/?t=10)

通过把 `stoppingToken` 传给 `Task.Delay` 这类异步方法,任务可以立即退出,而不必等待计时器到期。
这让应用程序能够快速而干净地关闭。

目前的重点一直放在 API 应用程序内部的托管服务上,但这些原则同样适用于独立的 worker service。
在集成式托管服务和专用 worker service 之间如何选择,取决于系统的架构需求,比如后台工作是否需要独立于 API 进行扩缩容。

---

## 6. When Hosted Services are the Right Choice

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/when-hosted-services-are-the-right-choice-69958187/) · 1:23

### 总结

本课探讨在 .NET 后台处理中,如何在进程内托管服务与专用 worker service 之间做出选择。
它指出托管服务适合轻量的、与应用程序耦合的任务(比如刷新缓存),而资源密集型或需要独立扩缩容的工作负载则应该迁移到独立的 worker service 中。

### 核心概念

- 识别轻量与重量级的后台工作负载。
- 理解后台任务中的应用程序耦合。
- 评估是否需要独立扩缩容。
- 保护 API 性能不受资源密集型后台进程影响。

### 课程笔记

托管服务为在 .NET 应用程序内部运行后台任务提供了一种便捷的方式,但它并不是万能方案。
选择正确的架构取决于任务的性质以及它对宿主进程的影响。

### When to Use Hosted Services

托管服务最适合那些轻量的、与应用程序本身内在绑定的后台工作。
这些任务不应该需要大量资源或独立扩缩容。
常见的例子包括:

- **Cache Refreshing**:周期性地更新应用程序内部缓存。
- **Data Polling**:检查小规模数据集是否发生变化。
- **Maintenance Tasks**:执行例行的内部清理或状态管理。

因为这些任务与应用程序逻辑紧密耦合且占用极小,所以在进程内运行它们既高效又能降低架构复杂度。

### When to Use Dedicated Worker Services

随着后台工作负载在复杂度或资源消耗上的增长,就有必要把它们迁移到专用的 worker service 中。
这种分离有几个好处:

- **Independent Scaling**:worker service 可以独立于 Web API 进行横向扩展,从而实现更好的资源分配。
- **Performance Isolation**:繁重的后台处理会消耗 CPU 和内存,可能拖慢主 API。把这些任务移到独立的进程中,可以确保 API 保持响应能力。
- **Resource Intensity**:长时间运行或计算开销大的任务,更适合放在不会与请求处理竞争资源的环境中。

### Decision Rule of Thumb

- **Use a Hosted Service**:如果任务是轻量的、与应用程序紧密耦合的,并且不需要独立扩缩容。
- **Use a Worker Service**:如果任务是长时间运行的、资源密集的,或者需要自己的扩缩容策略。
