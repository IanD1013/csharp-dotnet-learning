# Introduction to Background Processing

> 课程:[From Zero to Hero: Background Processing in .NET](https://dometrain.com/course/from-zero-to-hero-background-processing-in-dotnet/) · 第 2 章
> 共 4 课 · 约 7:59
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

---

## 课程索引

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [The Problem With Synchronous Systems](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/the-problem-with-synchronous-systems-69958178/) | 2:19 | [↓](#1-the-problem-with-synchronous-systems) |
| 2 | [In-Process vs Out of Process Background Work](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/in-process-vs-out-of-process-background-work-69958179/) | 2:00 | [↓](#2-in-process-vs-out-of-process-background-work) |
| 3 | [The Background Processing Landscape](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/the-background-processing-landscape-69958180/) | 1:50 | [↓](#3-the-background-processing-landscape) |
| 4 | [The Architecture Concepts We'll Explore](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/the-architecture-concepts-we-ll-explore-69958181/) | 1:50 | [↓](#4-the-architecture-concepts-well-explore) |

---

## 1. The Problem With Synchronous Systems

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/the-problem-with-synchronous-systems-69958178/) · 2:19

### 总结

同步请求处理,也就是 API 在请求-响应周期内完成所有操作,包括数据库写入、外部服务调用和通知发送,会带来严重的性能和可靠性问题。
随着系统不断增长,这种模式会引入高延迟,增加因部分失败而导致状态不一致的风险,并且因为占用服务器资源而限制了可扩展性。
转向后台处理,可以把复杂或长时间运行的任务卸载给异步的工作进程,从而让 API 保持响应能力。

### 核心概念

- **Synchronous Request Pattern(同步请求模式)**:在向用户返回响应之前,顺序执行所有任务。
- **Latency(延迟)**:响应时间由整条链路中最慢的那个操作决定,比如外部 API 调用或繁重的计算。
- **Reliability & Partial Failures(可靠性与部分失败)**:当一个操作成功(例如数据库写入)而后续操作失败(例如邮件通知)时,系统进入不一致状态的风险。
- **Resource Bottlenecks(资源瓶颈)**:长时间运行的请求会消耗线程和内存这类有限的服务器资源,降低整体吞吐量。
- **Background Workers(后台工作进程)**:一种架构上的转变,API 只记录下需要完成的工作并立即返回,把执行交给异步的进程。

### 课程笔记

API 开发中一种常见的设计模式,是在单个请求处理器中处理所有操作。
当请求到达时,服务器通常遵循一个线性的顺序:校验输入、把数据保存到数据库、执行必要的处理,最后发送通知或邮件。
只有当上述每一个步骤都完成之后,服务器才会向用户返回响应。

这种做法直接、易于理解,但随着系统规模扩大,它会带来三个主要问题:

#### 1. 延迟

在同步系统中,用户和服务器都必须等待每一个步骤完成。
如果工作流中的任何一个环节很慢,比如调用第三方 API、处理文件或者执行繁重的计算,整个请求就会变慢。
由于请求线程在这整段时间里一直被占用,服务器处理其他到来请求的能力就会下降。

#### 2. 可靠性与状态不一致

同步工作流很容易受到部分失败的影响。
例如,如果系统成功写入了数据库,但随后未能发送确认邮件,那么这个请求整体上就算失败了。
然而数据库写入已经发生了,系统因此处于不一致的状态。
随着工作流复杂度的增长,管理这些部分失败会变得越来越困难。

#### 3. 可扩展性

每一个长时间运行的请求都会消耗关键的服务器资源,包括线程、内存和数据库连接。
随着工作负载增加,这些资源会成为瓶颈。
单纯增加更多的服务器往往无法解决底层的设计问题,因为每个请求的资源消耗依然很高。

#### 后台处理这一解决方案

现代系统通过把请求与工作解耦来应对这些挑战。
API 不再立即执行工作,而是只记录下这项工作需要发生,然后向用户返回响应。
随后由后台工作进程异步地处理这些任务。
这种做法既确保 API 保持快速和响应及时,又让复杂、耗时的处理能够在后台可靠地进行。

---

## 2. In-Process vs Out of Process Background Work

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/in-process-vs-out-of-process-background-work-69958179/) · 2:00

### 总结

本课探讨 .NET 中进程内与进程外后台处理在架构上的区别。
进程内的工作运行在与 API 相同的应用程序里,通过 hosted services 复用依赖注入和日志这类共享的基础设施。
进程外的工作运行在一个独立的 worker service 中,从而可以独立扩展,并与主 API 的性能相互隔离。
在这两种模式之间的选择,取决于任务的资源消耗强度以及它与主应用程序之间的关系。

### 核心概念

- **In-process background work(进程内后台工作)**:使用 Hosted Services,在与 Web 应用相同的进程中运行的任务。
- **Out-of-process background work(进程外后台工作)**:在一个独立的应用程序中运行的任务,通常使用 Worker Service 项目类型。
- **Shared Infrastructure(共享基础设施)**:进程内的任务与 API 共享依赖注入、配置和日志。
- **Independent Scaling(独立扩展)**:进程外的 worker 可以扩缩容或重启,而不会影响主 API。
- **Workload Suitability(工作负载的适配性)**:在轻量的进程内任务与资源密集的进程外任务之间做选择的指导原则。

### 课程笔记

在决定后台工作应该在哪里执行时,开发者必须在现有 Web 应用内部运行任务和在一个完全独立的服务中运行任务之间做出选择。
这个选择定义了进程内与进程外后台工作之间的区别。

#### 进程内后台工作

进程内后台工作运行在与 API 相同的应用程序进程中。
在 ASP.NET Core 中,这通常使用 **hosted services** 来实现。
这些服务与 Web 服务器并行运行,并被集成到同一套基础设施中,因而可以共享:

- 依赖注入容器
- 应用程序配置
- 日志提供程序

正因为这种共享的环境,进程内工作易于实现,并且让整体的系统架构保持简单。
当任务比较轻量、并且与应用程序的主要功能密切相关时,这种做法最为有效。
例子包括:

- 刷新内部的缓存数据
- 执行周期性的维护任务
- 执行小型的、非阻塞的后台操作

#### 进程外后台工作

进程外后台工作运行在一个独立的应用程序实例中。
在 .NET 生态中,这通常通过 **Worker Service** 项目类型来实现。
worker service 独立于 API 运行,处理那些由主应用程序调度或入队的作业。

把后台工作分离到它自己的进程中,带来了若干架构上的优势:

- **Independent Scaling(独立扩展)**:worker service 可以根据后台处理的负载进行横向或纵向扩展,与 API 的流量无关。
- **Resilience(韧性)**:worker service 可以被重启或者发生故障,而不会直接影响 Web API 的可用性。
- **Resource Isolation(资源隔离)**:繁重的、资源密集的工作负载可以在 worker service 中运行,而不必与 API 的请求处理逻辑争抢 CPU 或内存。

这种分离使得进程外的 worker 非常适合长时间运行或资源消耗大的任务。

#### 选择正确的方案

在进程内与进程外工作之间的决定,可以参考下面这条经验法则:

- **In-Process(进程内)**:用于那些与应用程序密切相关、并且不需要独立扩展的轻量任务。
- **Out-of-Process(进程外)**:用于更重的工作负载,它们需要资源隔离或独立扩展来保持 API 的性能。

---

## 3. The Background Processing Landscape

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/the-background-processing-landscape-69958180/) · 1:50

### 总结

本课概览 .NET 中可用于后台处理的各种工具和架构。
它把这些工具划分为 hosted services、worker services、作业队列、调度系统和消息系统,并解释在分布式或独立的应用环境中,每一种各自的具体使用场景和优势。

### 核心概念

- Hosted Services 与 Worker Services 的对比
- 作业队列与异步处理
- 调度库(Hangfire、Quartz)
- 消息系统与消息代理(RabbitMQ、Azure Service Bus)

### 课程笔记

.NET 提供了若干用于运行后台工作负载的工具,每一种都在应用程序的架构中扮演着特定的角色。

#### Hosted Services

hosted services 运行在 ASP.NET Core 应用程序内部,并直接与应用程序的生命周期集成。
这种集成使它们非常适合那些属于 Web 应用自身的小型后台任务。

#### Worker Services

worker services 是专为后台工作负载而设计的独立应用程序。
它们使用与 Web 应用相同的 .NET 宿主基础设施,但独立于任何 Web 服务器运行。
这使它们非常适合长时间运行或处理量很大的作业。

#### 作业队列

许多后台系统会使用作业队列。
系统不会立即处理工作,而是记录下有一个作业需要被处理。
随后由一个 worker 从队列中取出作业,并异步地处理它们。
这种模式提供了可靠性,并允许工作被分发到多个 worker 上。

#### 调度系统

有些任务是由时间而不是用户操作触发的,比如夜间作业或周期性的集成。
在 .NET 中,通常使用 Hangfire 或 Quartz 这样的库来管理这些定时的工作负载。

#### 消息系统

大型的分布式系统经常使用消息代理,比如 RabbitMQ 或 Azure Service Bus。
这些系统让多个服务能够异步通信,并在大规模下可靠地处理工作。

---

## 4. The Architecture Concepts We'll Explore

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-background-processing-in-dotnet-3256115/the-architecture-concepts-we-ll-explore-69958181/) · 1:50

### 总结

本课介绍将在整个课程中构建的后台处理系统的架构蓝图。
它勾勒出一个与文档处理有关的场景,其中长时间运行的任务被从 API 中卸载出去,以改善延迟和可靠性。
这个架构包括一个用于记录作业的 API、一个用于持久化状态的数据库、一个负责执行的后台 worker,以及一个用于处理周期性任务的调度器。
状态跟踪、重试策略、幂等性和可扩展性这些关键考量,也被强调为构建健壮系统的基础要素。

### 核心概念

- **Decoupling(解耦)**:把 API 请求与繁重的处理分离开,以降低延迟。
- **Job Persistence(作业持久化)**:使用数据库来记录作业的详细信息和当前状态。
- **State Management(状态管理)**:跟踪状态之间的转换(例如 Pending、Processing、Completed、Failed),以便支持恢复和重试。
- **Worker Pattern(Worker 模式)**:利用后台 worker 来执行实际的处理逻辑。
- **Reliability & Scaling(可靠性与扩展)**:实现重试策略、幂等的设计,以及用多个 worker 进行横向扩展。

### 课程笔记

本课程的核心目标,是把一个简单的系统演进为一个可靠的后台处理平台。
主要场景涉及一个为文档上传而设计的 API。
当用户上传一份文档时,必须发生若干耗时的任务,比如信息抽取、文件生成、通知投递,以及与外部服务的交互。
在最初的那个 HTTP 请求内部执行这些任务,会导致高延迟和糟糕的可靠性。

#### 系统组件

为了高效地处理这些任务,这个架构被划分为若干组件:

- **API**:API 不会立即执行繁重的工作,而是充当一个入口点,在系统中记录一个"job"。
- **Database(数据库)**:它作为所有作业的事实来源,存储它们的元数据和当前状态。
- **Background Worker(后台 worker)**:一个专门的组件,负责从数据库中取出作业并执行实际的处理。
- **Scheduler(调度器)**:负责触发周期性的或基于时间的作业的组件。

#### 作业状态与可靠性

跟踪一个作业的状态,对系统的韧性至关重要。
作业通常会经历这样一个生命周期:

1. **Pending**:作业已被记录,但尚未开始。
2. **Processing**:worker 已经取走了这个作业。
3. **Completed**:工作已成功完成。
4. **Failed**:处理过程中发生了错误。

通过在数据库中维护这个状态,系统能够从故障中恢复,并安全地重试工作。
随着系统的增长,可靠性还会通过重试策略、幂等的作业设计(确保一个作业可以被多次运行而不产生副作用)以及可观测性来进一步增强。

#### 可扩展性

这个架构被设计为可以横向扩展。
随着作业量的增加,可以添加额外的 worker 实例,以更快地处理队列。

在接下来的模块中,实现会从 hosted services 开始,它们允许后台工作直接在 ASP.NET Core 应用程序内部运行。
