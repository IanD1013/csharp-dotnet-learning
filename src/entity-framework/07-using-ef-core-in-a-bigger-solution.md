# Using EF Core in a bigger solution

> 课程:[From Zero to Hero: Entity Framework Core in .NET](https://dometrain.com/course/from-zero-to-hero-entity-framework-core-in-dotnet/) · 第 7 章
> 共 10 课 · 约 68:27
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/introduction-54130148/) | 0:53 | [↓](#1-introduction) |
| 2 | [Dependency Injection](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/dependency-injection-54130150/) | 8:23 | [↓](#2-dependency-injection) |
| 3 | [Configuring Logging](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/configuring-logging-54130151/) | 10:06 | [↓](#3-configuring-logging) |
| 4 | [DbContext Lifecycle](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/dbcontext-lifecycle-54130152/) | 7:46 | [↓](#4-dbcontext-lifecycle) |
| 5 | [More on repositories](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/more-on-repositories-54130153/) | 17:06 | [↓](#5-more-on-repositories) |
| 6 | [Different multi-tenant approaches](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/different-multi-tenant-approaches-54130154/) | 4:00 | [↓](#6-different-multi-tenant-approaches) |
| 7 | [Multi-tenant with discriminators](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/multi-tenant-with-discriminators-54130155/) | 9:35 | [↓](#7-multi-tenant-with-discriminators) |
| 8 | [Multi-tenant with separate databases](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/multi-tenant-with-separate-databases-54130157/) | 4:59 | [↓](#8-multi-tenant-with-separate-databases) |
| 9 | [My take on architecture](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/my-take-on-architecture-54130158/) | 4:47 | [↓](#9-my-take-on-architecture) |
| 10 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54130159/) | 0:52 | [↓](#10-section-recap) |

## 1. Introduction

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/introduction-54130148/) · 0:53

### 总结

本课介绍如何从 Entity Framework Core(EF Core)的基础用法过渡到把它整合进更大的软件架构中。
前面几个部分聚焦于映射、迁移和测试,而本章探讨当 EF Core 作为复杂系统中的一个组件时所需要的策略。
本课强调,有效的软件设计不只是配置一个 DbContext,它要求开发者思考数据访问如何融入更广泛的应用生命周期和设计模式。

### 核心概念

- EF Core 作为更大系统中的一个组件。
- 数据访问的架构策略。
- 超越基础的 DbContext 配置。
- 用 EF Core 应对真实世界的软件设计挑战。

### 课程笔记

本课标志着从理解 Entity Framework Core(EF Core)的内部机制,转向在专业的软件架构中实现它。
之前的主题涵盖了对象关系映射(ORM)、架构迁移和单元测试的基础。
然而在生产环境中,EF Core 只是更大系统中的一个组件。

把 EF Core 整合进"更大图景"的设计,不只是实例化一个 DbContext。
它需要以下方面的策略:
- **依赖注入(Dependency Injection)**:在 DI 容器中管理 DbContext 的生命周期,以确保正确的作用域和资源管理。
- **配置管理**:组织实体配置(例如使用 IEntityTypeConfiguration),以便在领域模型增长时保持代码整洁。
- **抽象模式**:在控制器中直接使用上下文,与通过 Repository 模式等模式抽象数据访问之间做出选择。
- **解决方案结构**:处理包含多个项目的复杂解决方案结构,例如把 API 逻辑与数据访问层分离。

本章接下来的课程会探讨具体场景和架构模式,以解决在更大的解决方案中扩展 EF Core 应用时常遇到的挑战。

## 2. Dependency Injection

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/dependency-injection-54130150/) · 8:23

### 总结

本课演示如何从硬编码的 Entity Framework Core 配置过渡到稳健的依赖注入(DI)模式。
通过重构 DbContext,使其通过构造函数接收 DbContextOptions,并把配置逻辑移到 Program.cs,应用会变得更易维护,也更能适应不同的环境。
本课还涵盖把连接字符串外部化到 appsettings.json,以及为数据库上下文及其配置选项设置服务生命周期。

### 核心概念

- **Constructor Injection**:把 `DbContextOptions<TContext>` 传给 `DbContext` 的构造函数,以便从外部进行配置。
- **Removing OnConfiguring**:把连接字符串等与环境相关的设置移出上下文类。
- **DI Container Registration**:在 `Program.cs` 中使用 `builder.Services.AddDbContext<TContext>`。
- **Configuration System**:利用 `appsettings.json` 和 `IConfiguration` 来管理连接字符串。
- **Service Lifetimes**:理解 `DbContext` 默认的 `Scoped` 生命周期,以及选项使用 `Singleton` 的可能性。

### 课程笔记

#### OnConfiguring 的问题

在典型的开发流程中,`DbContext` 往往一开始就在重写的 `OnConfiguring` 方法里放一个硬编码的连接字符串。
然而,这种做法对真实世界的应用是有问题的,因为连接字符串和日志级别这类配置细节会随环境(例如 Development、Test、Production)而变化。

```csharp
public class MoviesContext : DbContext
{
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("""
            Data Source=localhost;
            Initial Catalog=MoviesDB;
            User Id=sa;
            Password=MySaPassword123;
            TrustServerCertificate=True;
            """);
        // Not proper logging
        optionsBuilder.LogTo(Console.WriteLine);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GenreMapping());
        modelBuilder.ApplyConfiguration(new MovieMapping());
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/dependency-injection-54130150/?t=25)

#### 为依赖注入重构

为了让 `DbContext` 可以通过 .NET 依赖注入(DI)容器进行配置,应当移除 `OnConfiguring` 重写。
取而代之,上下文必须提供一个接收 `DbContextOptions<TContext>` 的构造函数。
这个泛型参数确保当应用使用多个数据库上下文时,DI 容器能为每个上下文提供其专属的配置。
然后这些选项被传给基类 `DbContext` 的构造函数。

```csharp
using Dometrain.EFCore.API.Data.EntityMapping;
using Dometrain.EFCore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Dometrain.EFCore.API.Data;

public class MoviesContext : DbContext
{
    public MoviesContext(DbContextOptions<MoviesContext> options)
        : base (options)
    { }
    
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GenreMapping());
        modelBuilder.ApplyConfiguration(new MovieMapping());
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/dependency-injection-54130150/?t=130)

#### 在 Program.cs 中注册上下文

`DbContext` 重构完成后,必须在 `Program.cs` 中把它注册到应用的服务容器里。
`AddDbContext` 方法提供一个 `optionsBuilder`,让你可以定义数据库提供程序以及之前位于 `OnConfiguring` 中的其他设置。

```csharp
// Add a DbContext here
builder.Services.AddDbContext<MoviesContext>(optionsBuilder =>
{
    optionsBuilder.UseSqlServer("""
                                Data Source=localhost;
                                Initial Catalog=MoviesDB;
                                User Id=sa;
                                Password=MySaPassword123;
                                TrustServerCertificate=True;
                                """);
    
    // Not proper logging
    optionsBuilder.LogTo(Console.WriteLine);
});
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/dependency-injection-54130150/?t=210)

#### 将配置外部化

为了充分利用 DI 容器,连接字符串应当移到 `appsettings.json` 这样的配置文件中。
.NET 配置系统会自动处理不同的来源,例如环境变量或 JSON 文件,从而让代码保持与环境无关。

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "MoviesContext": "Data Source=localhost; Initial Catalog=MoviesDB; User Id=sa; Password=MySaPassword123; TrustServerCertificate=True;"
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/dependency-injection-54130150/?t=280)

#### 最终的 DI 配置与生命周期

最后一步是使用 `builder.Configuration.GetConnectionString("MoviesContext")` 从配置中取出连接字符串。
此外,`AddDbContext` 允许指定服务生命周期:

1.  **Context Lifetime**:默认为 `ServiceLifetime.Scoped`。
    在 ASP.NET Core 中,这意味着每个 HTTP 请求都会创建一个新的 `DbContext` 实例,并在该请求内共享。
2.  **Options Lifetime**:由于配置(连接字符串、日志设置)通常在应用启动后不会改变,选项可以注册为 `ServiceLifetime.Singleton`。

```csharp
// Add the DbContext
builder.Services.AddDbContext<MoviesContext>(optionsBuilder =>
    {
        var connectionString = builder.Configuration.GetConnectionString("MoviesContext");
        optionsBuilder
            .UseSqlServer(connectionString);
    },
    ServiceLifetime.Scoped,
    ServiceLifetime.Singleton);

var app = builder.Build();

// Check if the DB was migrated
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MoviesContext>();
    var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
    if (pendingMigrations.Any())
        throw new Exception("Database is not fully migrated for MoviesContext.");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/dependency-injection-54130150/?t=450)

## 3. Configuring Logging

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/configuring-logging-54130151/) · 10:06

### 总结

本课涵盖 Entity Framework Core 中日志记录和性能监控的配置。
它详细介绍了用于简单控制台日志的 LogTo,以及用于与 Microsoft.Extensions.Logging 集成的 UseLoggerFactory。
本课强调了在生产环境中使用 EnableSensitiveDataLogging 的安全风险,并演示了如何集成 Serilog 等第三方提供程序。
此外,它介绍了用于监控 EF Core 指标的 dotnet-counters,并通过正确管理服务作用域,为应用启动期间的一个 DbContext 资源泄漏提供了关键修复。

### 核心概念

- **LogTo**:一种把 EF Core 日志重定向到某个委托(例如 Console.WriteLine)的简单方法。
- **UseLoggerFactory**:一种把 EF Core 与标准 ILoggerFactory 集成的方法,用于高级日志场景。
- **Sensitive Data Logging**:启用 EnableSensitiveDataLogging 可以让 SQL 参数值出现在日志中,这对调试很有用,但在生产中不安全。
- **Serilog Integration**:EF Core 会通过 Microsoft.Extensions.Logging 自动与宿主进程中配置的外部日志提供程序集成。
- **Performance Monitoring**:使用 dotnet-counters 跟踪活动 DbContext 实例数和查询执行次数等指标。
- **Scope Management**:通过为 IServiceScope 使用 using 块,确保应用启动期间创建的 DbContext 实例被释放。

### 课程笔记

#### 使用 LogTo 进行基础日志记录

在 Entity Framework Core 中启用日志的最简单方式,是在 DbContext 配置中使用 LogTo 方法。
这个方法接收一个委托,指定日志消息应写到哪里。

```csharp
optionsBuilder
    .UseSqlServer(connectionString)
    .LogTo(Console.WriteLine);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/configuring-logging-54130151/?t=10)

LogTo 方法还可以配置特定的日志级别和其他选项,以过滤输出并自定义格式。

#### 与 ILoggerFactory 集成

对于更复杂的场景,EF Core 可以与 Microsoft.Extensions.Logging 中的标准 ILoggerFactory 集成。
这让 EF Core 能与应用的其余部分共享同一套日志基础设施。

```csharp
optionsBuilder
    .UseSqlServer(connectionString)
    .UseLoggerFactory(loggerFactory);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/configuring-logging-54130151/?t=70)

如果没有显式配置日志,EF Core 会自动与宿主进程服务集合中注册的日志提供程序集成。

#### 敏感数据日志记录

默认情况下,出于安全和合规(例如 GDPR)原因,EF Core 不会在日志中包含 SQL 参数的值。
要在调试时看到这些值,可以启用敏感数据日志。

```csharp
optionsBuilder
    .UseSqlServer(connectionString)
    .EnableSensitiveDataLogging();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/configuring-logging-54130151/?t=115)

**警告:** 永远不要在生产环境中启用 EnableSensitiveDataLogging,因为它可能会在日志文件中暴露敏感的用户信息。

#### 使用外部提供程序(Serilog)

当使用 Serilog 这样的外部日志框架时,只要 Serilog 已注册到 .NET 日志系统,EF Core 的日志就会被自动捕获。
配置通常在 appsettings.json 中处理。

```json
"Serilog": {
  "Using": [ "Serilog.Sinks.Console", "Serilog.Sinks.File" ],
  "MinimumLevel": {
    "Default": "Debug"
  },
  "WriteTo": [
    {
      "Name": "Console",
      "Args": {
        "outputTemplate": "[{Timestamp:HH:mm:ss} {SourceContext} [{Level}] {Message}{NewLine}{Exception}",
        "theme": "Serilog.Sinks.SystemConsole.Themes.ConsoleTheme::None, Serilog.Sinks.Console"
      }
    },
    {
      "Name": "File",
      "Args": {
        "path": "%TEMP%/Logs/dometrain-efcore-api.json",
        "rollingInterval": "Day",
        "formatter": "Serilog.Formatting.Compact.CompactJsonFormatter, Serilog.Formatting.Compact"
      }
    }
  ]
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/configuring-logging-54130151/?t=205)

#### 使用 dotnet-counters 进行性能监控

dotnet-counters 工具可用于监控 EF Core 暴露的性能指标。
这个工具让你可以实时跟踪活动上下文、查询执行速率以及其他内部指标。

安装该工具:

```powershell
dotnet tool install -g dotnet-counters
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/configuring-logging-54130151/?t=340)

要监控某个特定进程的 EF Core 计数器,使用下面的命令并带上进程 ID(PID):
`dotnet counters monitor Microsoft.EntityFrameworkCore --process-id <PID>`

#### 修复启动代码中的资源泄漏

使用 dotnet-counters 进行监控可以揭示诸如 DbContext 实例泄漏之类的 bug。
例如,如果在应用启动期间解析了一个 DbContext 来检查待处理的迁移,但作用域没有被释放,连接就可能一直保持打开。
解决办法是把作用域的创建包在一个 using 块中。

```csharp
// Check if the DB was migrated
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MoviesContext>();
    var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
    if (pendingMigrations.Any())
        throw new Exception("Database is not fully migrated for MoviesContext.");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/configuring-logging-54130151/?t=460)

## 4. DbContext Lifecycle

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/dbcontext-lifecycle-54130152/) · 7:46

### 总结

管理 DbContext 的生命周期,对于优化资源使用和防止数据库连接耗尽至关重要。
DbContext 在需要时打开连接,并在被释放时关闭连接,把它归还到连接池。
合适的生命周期策略取决于应用类型:Web 应用使用短生命周期、基于请求的作用域,此时更推荐预先加载(eager loading);而桌面应用使用长生命周期、基于界面的作用域,它们受益于延迟加载(lazy loading),并且需要连接弹性。
对于高吞吐量的 Web 应用,DbContext 池化可以通过复用上下文实例进一步提升性能。

### 核心概念

- **Connection Management**:DbContext 只在需要时打开数据库连接,并在上下文本身被释放后立即关闭并释放该连接。
- **Web/Request Scope**:一种与单个 API 调用、gRPC 请求或消息处理绑定的短生命周期作用域,通常持续几毫秒到几秒。
- **Desktop/Screen Scope**:一种与某个 UI 界面绑定的长生命周期作用域,可能保持打开数分钟甚至数小时。
- **Connection Resiliency**:对于桌面应用至关重要,因为在界面保持打开期间连接可能丢失(例如由于系统休眠)。
- **Lazy Loading vs. Eager Loading**:延迟加载在桌面场景中对临时按需获取数据很有用,而在 Web 场景中出于性能考虑更推荐使用 `.Include()` 进行预先加载。
- **DbContext Pooling**:一种性能优化,通过复用 DbContext 实例来减少高负载场景下的实例化开销。

### 课程笔记

当一个 `DbContext` 被实例化时,它会管理一个到数据库的连接。
这个连接在上下文工作期间保持打开,并在上下文被释放后立即关闭并归还到连接池。
正确管理这个生命周期至关重要;同时保持过多活动的上下文会消耗内存,并可能耗尽可用的数据库连接池。

在一个标准的 ASP.NET Core 应用中,`DbContext` 通常以 scoped 生命周期注册,这意味着它在单个请求期间存活。

```csharp
// Add the DbContext
builder.Services.AddDbContext<MoviesContext>(optionsBuilder =>
    {
        var connectionString = builder.Configuration.GetConnectionString("MoviesContext");
        optionsBuilder
            .UseSqlServer(connectionString);
    },
    ServiceLifetime.Scoped,
    ServiceLifetime.Singleton);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/dbcontext-lifecycle-54130152/?t=380)

#### 场景对比:Web 与桌面

在 Web 环境和桌面环境中,管理 `DbContext` 的要求差别很大:

1.  **Web Scenarios**:作用域是单个请求。
    生命周期非常短(毫秒级)。
    因为请求参数是预先已知的,开发者应当使用预先加载(通过 `.Include()`)而不是延迟加载。
    重新连接很少成为问题;如果某个请求因数据库的短暂故障而失败,客户端只需重试这个 API 调用即可。
2.  **Desktop Scenarios**:作用域通常是一个 UI 界面。
    这个生命周期很长(数分钟或数小时)。
    延迟加载在这里非常有益,因为它允许只在用户与特定元素交互时(例如点击列表项查看详情)才获取依赖实体。
    然而,开发者必须处理连接弹性,因为如果计算机在界面仍处于活动状态时进入休眠模式,应用可能会失去连接。

```text
Web                                  Desktop
+-----------------------------+      +-----------------------------+
| scope: single request       |      | scope: UI screen            |
| lifetime: milliseconds      |      | lifetime: minutes or hours  |
| eager loading: .Include()   |      | lazy loading                |
| failure: client retries API |      | needs connection resiliency |
+-----------------------------+      +-----------------------------+
```

#### DbContext 池化

创建一个 `DbContext` 实例需要系统资源。
对于处理高流量的应用(通常是每秒超过几百个请求),可以启用 `DbContext` 池化来复用现有实例。
这通过 `AddDbContextPool` 实现,它默认维护的池大小为 1024 个实例。

```csharp
// Add the DbContext
builder.Services.AddDbContextPool<MoviesContext>(optionsBuilder =>
{
    var connectionString = builder.Configuration.GetConnectionString("MoviesContext");
    optionsBuilder
        .UseSqlServer(connectionString);
});
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/dbcontext-lifecycle-54130152/?t=430)

使用池化时,Entity Framework Core 假定选项和上下文配置在每次复用之间都是一致的。
虽然这是一项强大的性能调优,但通常只应在性能分析表明上下文实例化是瓶颈时才去实现它。

## 5. More on repositories

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/more-on-repositories-54130153/) · 17:06

### 总结

Repository 作为一个抽象层,封装查询行为和数据操作,把应用的其余部分与 Entity Framework 隔离开。
它们应当由消费者拥有,也就是说,接口由调用方(例如控制器或服务)的需要来定义,而不是由数据库集合所能提供的内容来定义。
使用 repository 时的一个主要挑战是协调 Unit of Work 模式;在单个服务方法中多次调用 repository 可能触发多次 `SaveChanges` 操作,从而导致性能问题和潜在的数据不一致。
解决办法是实现一个 `Unit of Work Manager`,在一个作用域请求内协调跨 repository 的持久化。

### 核心概念

*   **Encapsulation**:Repository 应当包含所有查询行为和 CRUD 操作,把解决方案的其余部分与 Entity Framework 隔离开。
*   **Consumer-Owned Contracts**:接口应当由消费者的需要来定义,而不是由实现来定义。
    这确保 repository 只暴露必要的操作。
*   **Generic Repository Pitfalls**:泛型实现常常把 EF 特有的逻辑(例如谓词或 `IQueryable`)泄漏到应用中,并且缺乏精细调优的能力。
*   **Unit of Work Coordination**:标准的 repository 经常在内部调用 `SaveChangesAsync`,当在一个事务中使用多个 repository 时,这会破坏原子性。
*   **Unit of Work Manager**:一个 scoped 服务,跟踪事务是否正在进行,并为整个请求协调一次 `SaveChangesAsync` 调用。

### 课程笔记

Repository 在数据库持久化层与应用的其余部分之间提供了一层分隔。
在典型的架构中,请求会经过业务逻辑,业务逻辑使用 repository 来执行查询和数据操作。
而 repository 又使用 `DbContext` 与数据库通信。
这层抽象应当把 Entity Framework 与解决方案的其余部分隔离开,这意味着 repository 的契约不应基于 EF 类型。

```text
request -> business logic -> repository -> DbContext -> database
```

#### 由使用方拥有的接口

一个有效的 repository 接口由消费者需要的操作来定义。
例如,一个 `GenreRepository` 可能恰好提供五个操作,因为这些正是控制器所需要的,尽管底层的 `DbSet` 支持多得多的操作。

```csharp
public interface IGenreRepository
{
    Task<IEnumerable<Genre>> GetAll();
    Task<Genre?> Get(int id);
    Task<Genre> Create(Genre genre);
    Task<Genre?> Update(int id, Genre genre);
    Task<bool> Delete(int id);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/more-on-repositories-54130153/?t=130)

#### 泛型仓储的问题

泛型 repository 通常被实现为所有 `DbSet` 实例的单一包装器,以便更容易 mock。
然而,这些实现经常把谓词等 Entity Framework 细节泄漏到应用层。

```csharp
public interface IRepository<T> where T : class, new()
{
    Task<T> Get(string id);
    Task<T> Find(Expression<Func<T, bool>> predicate);

    Task<IEnumerable<T>> GetAll();
    Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate);

    void Add(T entity);
    void AddRange(IEnumerable<T> entities);

    void Update(T entity);
    void UpdateRange(IEnumerable<T> entities);

    void Delete(T entity);
    void DeleteRange(IEnumerable<T> entities);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/more-on-repositories-54130153/?t=235)

泛型 repository 在精细调优方面也很吃力,例如为不同查询添加或移除特定的 `Include` 语句,这往往导致开发者无论如何都要在泛型方法旁边再编写自定义方法。

#### 协调工作单元

标准 repository 实现的主要问题在于,它们把 `DbContext` 内置的 Unit of Work 模式抽象掉了。
如果某个服务需要执行一个涉及多次 repository 调用的复杂操作,每次调用都可能触发它自己的 `SaveChangesAsync`。

```csharp
public class GenresController : Controller
{
    private readonly IGenreRepository _repository;
    private readonly IBatchGenreService _batchService;

    public GenresController(IGenreRepository repository, IBatchGenreService batchService)
    {
        _repository = repository;
        _batchService = batchService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Genre>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _repository.GetAll());
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/more-on-repositories-54130153/?t=510)

在批量操作中,对 repository 多次调用 `Create` 会导致多次数据库往返,并且如果过程中途失败,还有出现不一致状态的风险。

```csharp
public class BatchGenreService : IBatchGenreService
{
    private readonly IGenreRepository _repository;

    public BatchGenreService(IGenreRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Genre>> CreateGenres(IEnumerable<Genre> genres)
    {
        List<Genre> response = new ();
        foreach (var genre in genres)
        {
            response.Add(await _repository.Create(genre));
        }

        return response;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/more-on-repositories-54130153/?t=580)

#### 实现工作单元管理器

为了解决这个问题,可以引入一个 `UnitOfWorkManager` 来跟踪业务事务的状态。
这个管理器让服务可以开启一个 unit of work,并手动触发一次 `SaveChangesAsync` 调用。

```csharp
public interface IUnitOfWorkManager
{
    void StartUnitOfWork();
    bool IsUnitOfWorkStarted { get; }
    Task<int> SaveChangesAsync();
}

public class UnitOfWorkManager : IUnitOfWorkManager
{
    private readonly MoviesContext _context;

    public UnitOfWorkManager(MoviesContext context)
    {
        _context = context;
    }

    private bool _isUnitOfWorkStarted = false;
    public void StartUnitOfWork()
    {
        _isUnitOfWorkStarted = true;
    }
    public bool IsUnitOfWorkStarted => _isUnitOfWorkStarted;

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/more-on-repositories-54130153/?t=670)

Repository 必须知道这个管理器的存在。
只有当上层服务没有开启环境(ambient)unit of work 时,它们才应在内部调用 `SaveChangesAsync`。

```csharp
public class GenreRepository: IGenreRepository
{
    private readonly MoviesContext _context;
    private readonly IUnitOfWorkManager _uowManager;

    public GenreRepository(MoviesContext context, IUnitOfWorkManager uowManager)
    {
        _context = context;
        _uowManager = uowManager;
    }

    public async Task<Genre> Create(Genre genre)
    {
        await _context.Genres.AddAsync(genre);

        if(!_uowManager.IsUnitOfWorkStarted)
            await _context.SaveChangesAsync();

        return genre;
    }
    
    // Similar checks are implemented in Update and Delete methods
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/more-on-repositories-54130153/?t=775)

然后由服务来协调事务:开启 unit of work,并在所有操作完成后调用保存。

```csharp
public class BatchGenreService : IBatchGenreService
{
    private readonly IGenreRepository _repository;
    private readonly IUnitOfWorkManager _uowManager;

    public BatchGenreService(IGenreRepository repository, IUnitOfWorkManager uowManager)
    {
        _repository = repository;
        _uowManager = uowManager;
    }

    public async Task<IEnumerable<Genre>> CreateGenres(IEnumerable<Genre> genres)
    {
        List<Genre> response = new ();
        
        _uowManager.StartUnitOfWork();
        
        foreach (var genre in genres)
        {
            response.Add(await _repository.Create(genre));
        }

        await _uowManager.SaveChangesAsync();
        
        return response;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/more-on-repositories-54130153/?t=865)

```text
BatchGenreService.CreateGenres
  |
  +-- _uowManager.StartUnitOfWork()
  |
  +-- foreach genre: _repository.Create(genre)
  |       AddAsync(genre)
  |       IsUnitOfWorkStarted == true -> no SaveChangesAsync
  |
  +-- _uowManager.SaveChangesAsync()
```

最后,`UnitOfWorkManager` 必须注册为 **scoped** 服务。
这确保在同一个 HTTP 请求内,repository 和服务共享同一个实例,从而能共享同一个事务状态。

```csharp
builder.Services.AddTransient<IGenreRepository, GenreRepository>();
builder.Services.AddTransient<IBatchGenreService, BatchGenreService>();
builder.Services.AddScoped<IUnitOfWorkManager, UnitOfWorkManager>();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/more-on-repositories-54130153/?t=925)

## 6. Different multi-tenant approaches

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/different-multi-tenant-approaches-54130154/) · 4:00

### 总结

多租户是一种架构模式,其中应用的单个实例为多个不同的用户群体(称为租户)提供服务,并对数据进行分区,使每个群体只能访问自己的信息。
Entity Framework Core 通过两种主要策略来支持多租户:鉴别器(discriminator)方式,它使用一个带有租户标识列和全局查询筛选器的共享数据库;以及连接字符串方式,它通过在运行时动态解析连接字符串,把每个租户隔离到专用的数据库中。
每种方法都在数据安全、隔离和运维复杂度方面有所取舍。

### 核心概念

- **Multi-tenancy**:在单次应用部署中进行数据分区。
- **Discriminator Approach**:使用一个共享数据库,每张表中都有一个租户标识列。
- **Global Query Filters**:自动对所有数据库查询应用基于租户的筛选,以防止跨租户的数据访问。
- **Connection String Approach**:通过在 DbContext 上切换连接字符串,把数据库请求路由到租户专属的数据库。
- **Data Isolation and Security**:评估共享数据库中数据泄露的风险,与独立数据库的严格边界进行对比。
- **Operational Complexity**:在单数据库部署的简便性与多数据库环境的管理开销之间取得平衡。

### 课程笔记

多租户系统让多个组织或用户群体共享同一套应用基础设施,同时保持严格的数据边界。
在这类系统中,同一租户内的用户可以共享数据,但严格禁止他们访问属于其他租户的数据。
这种隔离无需为每个租户单独部署即可实现。

#### 鉴别器方案

鉴别器方式是在数据库的每张表中添加一个鉴别器列。
这一列充当租户标识。
查询数据时,应用必须确保只取回鉴别器与当前活动租户 ID 相匹配的记录。
插入新数据时,系统必须确保租户 ID 被正确填充。

这种方式的一个显著优势是所有租户都使用单个数据库,从而简化了部署。
然而,它带有数据泄露的风险;如果发生编程错误(例如全局查询筛选器失效),用户可能会看到其他租户的数据。
此外,当某个租户的数据与其他租户的数据存放在一起时,要隔离、导出或删除单个租户的数据会更困难。

#### 连接字符串方案

连接字符串方式通过把租户的数据移到独立的数据库中来隔离租户。
应用为每个请求识别出租户,并动态切换 DbContext 上的连接字符串,使其指向相应的数据库。

这种方法提供了清晰而稳健的数据分离。
因为租户位于不同的数据库中,在物理上就无法跨越边界去访问其他租户的数据。
这使得提供数据导出或删除某个租户的全部数据变得容易得多。
主要缺点是运维开销增加;添加新租户需要预配新的数据库,这比单纯在共享表中添加一个新标识更难自动化和管理。

虽然两种方式都可行,但选择取决于对数据隔离的具体要求以及部署环境的复杂度。

## 7. Multi-tenant with discriminators

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/multi-tenant-with-discriminators-54130155/) · 9:35

基于鉴别器的多租户,是把所有租户的数据存放在共享表中,并通过一个特定的列(例如 TenantId)来区分。
本课演示如何在 EF Core 中实现这种方式:通过一个 scoped 服务从 HTTP 标头中提取租户标识,把该标识作为 DbContext 上的一个属性暴露出来,并应用全局查询筛选器。
它还涵盖了在 repository 层创建新实体时手动分配租户 ID 的必要性,以及确保数据库架构为鉴别器列建立索引以提升性能。

### 核心概念

- 基于鉴别器的多租户(共享数据库/共享架构)。
- 通过 HTTP 标头(`X-Tenant`)识别租户。
- 用于请求特定上下文的 scoped `TenantService`。
- 用于自动数据隔离的全局查询筛选器。
- 用于鉴别器列的影子属性(shadow property)。
- 在 repository 中为新实体手动分配租户。
- 为鉴别器列建立索引以优化查询。

### 课程笔记

本课首先定义了多租户的鉴别器方式,即在每张表中添加一个 `TenantId` 列来隔离数据。
为了在 Web API 中模拟多租户,实现了一个自定义的 Swagger 操作筛选器,要求 HTTP 请求中必须带有 `X-Tenant` 标头。

```csharp
public class TenantHeaderSwaggerAttribute : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "X-Tenant",
            In = ParameterLocation.Header,
            Required = true,
            Schema = new OpenApiSchema
            {
                Type = "string"
            }
        });
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/multi-tenant-with-discriminators-54130155/?t=55)

必须配置应用来注册这个筛选器以及所需的服务,包括 `IHttpContextAccessor` 和一个自定义的 `TenantService`。

```csharp
builder.Services.AddSwaggerGen(c => {
    c.OperationFilter<TenantHeaderSwaggerAttribute>();
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<TenantService>();

builder.Services.AddDbContextFactory<MoviesContext>(opt =>
{
    opt.UseSqlServer("""
        Data Source=localhost;
        Initial Catalog=MoviesDB;
        User Id=sa;
        Password=MySaPassword123;
        TrustServerCertificate=True;
        """);
});
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/multi-tenant-with-discriminators-54130155/?t=130)

创建一个 `TenantService` 来提取标头的值。
这个服务以 scoped 生命周期注册,使租户 ID 在单个 HTTP 请求的整个期间保持一致。

```csharp
public class TenantService
{
   private readonly IHttpContextAccessor _contextAccessor;

   public TenantService(IHttpContextAccessor contextAccessor)
   {
      _contextAccessor = contextAccessor;
   }

   private string? _tenantId = null;

   public string? GetTenantId()
   {
      if (_tenantId is not null)
         return _tenantId;
      
      var header = _contextAccessor.HttpContext?.Request.Headers
         .FirstOrDefault(h => h.Key.ToLower() == "x-tenant");
        
      if(header is { Value: { } value })
      {
         if(value.Any())
            _tenantId = value.First()!;
      }
      return _tenantId;
   }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/multi-tenant-with-discriminators-54130155/?t=85)

`MoviesContext` 被更新为通过构造函数注入接收 `TenantService`。
它通过一个公共属性暴露当前的租户 ID。
关键在于这必须是 `DbContext` 上的一个属性,而不是局部变量,也不是在映射配置中直接调用服务;否则,EF Core 可能会把它遇到的第一个租户 ID 当作常量缓存起来,从而无法切换租户。

```csharp
public class MoviesContext : DbContext
{
    private readonly TenantService _tenantService;

    public string? TenantId => _tenantService.GetTenantId(); 
    
    public MoviesContext(
        DbContextOptions<MoviesContext> options,
        TenantService tenantService)
        :base(options)
    {
        _tenantService = tenantService;
    }
    
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GenreMapping(this));
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/multi-tenant-with-discriminators-54130155/?t=175)

```text
HTTP request (X-Tenant header)
  -> TenantService.GetTenantId()      (scoped, per request)
  -> MoviesContext.TenantId           (property on the DbContext)
  -> HasQueryFilter(... == _context.TenantId)
```

为了在各实体间一致地应用鉴别器逻辑,使用了一个 `TenantAwareMapping<TEntity>` 基类。
这个类配置一个名为 `TenantId` 的影子属性,为它添加索引以提升性能,并应用一个全局查询筛选器。
该筛选器确保针对该实体执行的每个查询都会自动包含一个 `WHERE` 子句,把结果限制为当前的 `TenantId`。

```csharp
public abstract class TenantAwareMapping<TEntity> : IEntityTypeConfiguration<TEntity> 
    where TEntity : class
{
    private readonly MoviesContext _context;

    protected TenantAwareMapping(MoviesContext context)
    {
        _context = context;
    }
    
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder
            .Property<string?>("TenantId")
            .HasColumnType("varchar(32)");
        builder.HasIndex("TenantId");
        builder
            .HasQueryFilter(entity =>
                EF.Property<string>(entity, "TenantId") 
                == _context.TenantId);
        
        ConfigureEntity(builder);
    }

    public abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/multi-tenant-with-discriminators-54130155/?t=250)

具体的实体映射(例如 `GenreMapping`)继承这个基类,从而在实体特有属性之外包含租户感知的配置。

```csharp
public class GenreMapping : TenantAwareMapping<Genre>
{
    public GenreMapping(MoviesContext context) : base(context)
    { }

    public override void ConfigureEntity(EntityTypeBuilder<Genre> builder)
    {
        builder.Property<DateTime>("CreatedDate")
            .HasColumnName("CreatedAt")
            .HasValueGenerator<CreatedDateGenerator>();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/multi-tenant-with-discriminators-54130155/?t=205)

全局查询筛选器处理的是数据检索,而数据创建则需要确保 `TenantId` 被正确分配给新记录。
在 repository 的 `Create` 方法中,通过 `_context.Entry(entity)` 使用 `ChangeTracker`,在调用 `SaveChangesAsync` 之前设置 `TenantId` 影子属性的值。

```csharp
public async Task<Genre> Create(Genre genre)
{
    await _context.Genres.AddAsync(genre);

    _context.Entry(genre)
        .Property<string?>("TenantId")
        .CurrentValue = _context.TenantId;
    
    await _context.SaveChangesAsync();

    return genre;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/multi-tenant-with-discriminators-54130155/?t=325)

测试该实现可以确认,带有不同 `X-Tenant` 标头的请求会返回相互隔离的数据集。
尝试访问属于另一个租户的实体 ID 会得到 `404 Not Found`,因为全局查询筛选器会自动把该记录从底层 SQL 查询的结果集中排除。

## 8. Multi-tenant with separate databases

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/multi-tenant-with-separate-databases-54130157/) · 4:59

本课演示如何在 Entity Framework Core 中为每个租户使用独立的数据库来实现多租户架构。
通过把多租户逻辑从数据库架构转移到应用的基础设施层,开发者可以保持实体模型的整洁,无需鉴别器列。
这种方式的核心是:在依赖注入容器中注册服务时,根据从传入请求标头中提取的租户标识,动态解析出相应的连接字符串。

### 核心概念

- **Database-level isolation**:每个租户都有自己专用的数据库,确保数据完全分离。
- **Clean Schema**:实体不需要 `TenantId` 鉴别器列,也不需要全局查询筛选器。
- **Dynamic Connection Strings**:连接字符串在运行时根据请求元数据进行解析。
- **Tenant Identification**:使用 `IHttpContextAccessor` 从 HTTP 标头中提取租户信息。
- **Independent Identity**:标识规范(例如自增 ID)对每个数据库而言是本地的,因此不同租户可以有重叠的主键值。

### 课程笔记

#### 基础设施与租户识别

在多数据库租户场景中,数据库架构保持简单。
例如,`Genres` 表只包含 `Id`、`Name` 和 `CreatedAt` 字段,没有任何租户特有的鉴别器。
为了确定使用哪个数据库,使用一个 `TenantService` 从传入请求中提取租户 ID。

```csharp
namespace Dometrain.EFCore.Tenants.Databases.Tenants;

/// <summary>
/// This could get the tenant from anywhere (token, database, ...).
/// This implementation gets it from the header for brevity.
/// </summary>
public class TenantService
{
   private readonly IHttpContextAccessor _contextAccessor;

   public TenantService(IHttpContextAccessor contextAccessor)
   {
      _contextAccessor = contextAccessor;
   }

   private string? _tenantId = null;

   public string? GetTenantId()
   {
      if (_tenantId is not null)
         return _tenantId;
      
      var header = _contextAccessor.HttpContext?.Request.Headers
         .FirstOrDefault(h => h.Key.ToLower() == "x-tenant");
        
      if(header is { Value: { } value })
      {
         if(value.Any())
            _tenantId = value.First()!;
      }
      return _tenantId;
   }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/multi-tenant-with-separate-databases-54130157/?t=55)

为了便于通过 Swagger 进行测试,使用一个 `IOperationFilter` 来确保所有 API 操作都必须带有 `X-Tenant` 标头。

```csharp
namespace Dometrain.EFCore.Tenants.Databases.Tenants;

public class TenantHeaderSwaggerAttribute : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "X-Tenant",
            In = ParameterLocation.Header,
            Required = true,
            Schema = new OpenApiSchema
            {
                Type = "string"
            }
        });
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/multi-tenant-with-separate-databases-54130157/?t=70)

#### 简化后的数据上下文与仓储

因为隔离发生在数据库层面,`MoviesContext` 及相关的 repository 不需要任何多租户逻辑。
该上下文是一个标准的 `DbContext` 实现。

```csharp
namespace Dometrain.EFCore.Tenants.Databases.Data;

public class MoviesContext : DbContext
{
    public MoviesContext(DbContextOptions<MoviesContext> options)
        :base(options)
    { }
    
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GenreMapping());
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/multi-tenant-with-separate-databases-54130157/?t=85)

同样,用于创建或检索数据的 repository 方法与单租户应用中的完全相同,因为它们是在已经为当前请求选定的数据库上下文中运行的。

```csharp
public async Task<Genre?> Get(int id)
{
    return await _context.Genres.FindAsync(id);
}

public async Task<Genre> Create(Genre genre)
{
    await _context.Genres.AddAsync(genre);
    
    await _context.SaveChangesAsync();

    return genre;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/multi-tenant-with-separate-databases-54130157/?t=100)

#### 动态注册 DbContext

租户专属数据库的集成发生在 `Program.cs` 中。
`AddDbContext` 方法不再提供静态的连接字符串,而是使用一个委托,从服务提供程序中解析 `TenantService` 和 `IConfiguration`。
然后它动态构造一个连接字符串键(例如 `MoviesContext_Hannes` 或 `MoviesContext_Dometrain`),以从配置中取出正确的连接字符串。

```csharp
// Add a DbContext here
builder.Services.AddDbContext<MoviesContext>((services, options) =>
{
    var tenantService = services.GetRequiredService<TenantService>();
    var configuration = services.GetRequiredService<IConfiguration>();
    var connectionString = configuration
        .GetConnectionString($"MoviesContext_{tenantService.GetTenantId()}");
    options.UseSqlServer(connectionString)
        .LogTo(Console.WriteLine);
});
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/multi-tenant-with-separate-databases-54130157/?t=160)

```text
X-Tenant: Hannes     -> "MoviesContext_Hannes"     -> Hannes database
X-Tenant: Dometrain  -> "MoviesContext_Dometrain"  -> Dometrain database
         (TenantService.GetTenantId() -> GetConnectionString(...))
```

#### 运行时行为与隔离

应用运行时,为 `X-Tenant` 标头提供不同的值,会把请求路由到不同的物理数据库。

1.  **Identity Reset**:由于每个数据库都有自己的标识规范,在 Tenant A 中创建的第一条记录 ID 为 1,在 Tenant B 中创建的第一条记录 ID 同样为 1。
2.  **Data Isolation**:带有 `X-Tenant: Hannes` 的 ID 1 请求会返回 Hannes 数据库中的数据,而带有 `X-Tenant: Dometrain` 的相同请求会返回 Dometrain 数据库中的不同数据。

## 9. My take on architecture

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/my-take-on-architecture-54130158/) · 4:47

### 总结

本课概述了一种使用 Entity Framework Core 扩展 .NET 应用的推荐架构方式,强调一种解耦的结构,其中业务逻辑位于一个无引用的 Domain 项目中。
通过把 EF Core 的实现细节隔离在一个专门的 Data 项目中,并使用基于接口的 repository,该架构实现了控制反转,从而便于对领域进行单元测试、对持久化层进行集成测试。

### 核心概念

- 通过多程序集的项目结构实现关注点分离。
- 创建一个无引用的 Domain 层,其中包含实体和业务逻辑。
- 把 Entity Framework Core 的实现隔离在一个专门的 Data 项目中。
- 使用基于接口的 repository,把领域与持久化细节解耦。
- 把 EF Core Migrations 分离到一个单独的程序集中,以保持项目整洁。
- 实现控制反转(IoC),以管理引用流与数据流之间的关系。

### 课程笔记

随着应用增长,把所有逻辑都放在单个 Web 项目中会变得难以为继。
一种更稳健的架构是把解决方案拆分为若干个专门的程序集,以管理复杂度和耦合。

#### 架构分层

推荐的架构由以下项目组成:

- **Web Startup Project**:这是 API 的入口点。
  它处理应用的启动逻辑,并作为依赖注入(DI)容器的宿主。
- **Domain Project**:这个项目包含核心业务逻辑,包括实体和用例。
  它代表无论使用何种 UI 或持久化技术都保持不变的逻辑。
  关键在于,Domain 项目是无引用的,这意味着它不依赖解决方案中的任何其他项目。
  它定义了外部关注点(例如数据访问)所需的接口。
- **Data Project**:这一层是使用 Entity Framework Core 的地方。
  它包含 `DbContext`,并实现 Domain 项目中定义的 repository 接口。
  通过把 EF Core 留在这里,可以防止持久化相关的细节泄漏到业务逻辑中。
- **Migrations Project**:迁移通常被移到它们自己的程序集中。
  这可以防止 Data 项目被与迁移相关的代码和元数据弄得杂乱,让数据访问层专注于它的主要职责。
- **Infrastructure Project**:这个程序集存放横切关注点的代码,例如身份验证和与外部 API 的集成。
  和 Data 项目一样,它实现 Domain 中定义的接口。

#### 引用流与数据流

这种架构的一个关键特征,是区分项目引用的方向与数据流动的方向。
这可以被看作两组不同的关系:

- **Reference Flow**:Web、Data 和 Infrastructure 项目都引用 Domain 项目。
  由于 Domain 项目没有外部依赖,它高度解耦,并且易于单元测试。
- **Data Flow**:当 Web 项目接收到一个请求时,数据流入 Domain 进行处理,然后流出到 Data 或 Infrastructure 项目,用于持久化或外部通信。

```text
Reference flow                       Data flow

  Web ----------+                     Web
                v                      |
  Data -----> Domain                   v
                ^                    Domain
  Infrastructure+                    /    \
                                    v      v
                                 Data   Infrastructure
```

这种设计使用了**控制反转(IoC)**。
Domain 定义它所需要的接口,而 Web 项目中的 DI 容器在运行时提供来自 Data 或 Infrastructure 项目的具体实现,从而解决了引用方向与控制方向之间的冲突。

#### 对 Entity Framework Core 的好处

在这个架构框架中使用 EF Core,为持久化层提供了清晰的边界。
它让开发者能够:
- 把 EF Core 依赖与 Domain 完全隔离。
- 独立地对 Data 项目进行全面的集成测试。
- 轻松替换或更新 UI 或 API 框架,而不影响底层的数据访问或业务逻辑。

虽然更简单的模式(例如把服务直接注入控制器)对较小的项目来说是可以接受的,但这种多层方式提供了应用增长所需的灵活性,而不会因紧耦合而积累大量技术债。

## 10. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54130159/) · 0:52

### 总结

Entity Framework Core 通常被整合进更大的架构模式中,而不是孤立地使用。
本课总结了面向生产的实现中的关键考量,包括使用 repository 解耦逻辑、实现日志与监控、处理多租户架构,以及在应用的依赖注入容器中管理 DbContext 的生命周期。

### 核心概念

- 使用 Repository 模式解耦逻辑。
- 实现生产级的日志与监控。
- 为多租户进行架构设计(查询筛选器 vs. 独立数据库)。
- 管理 DbContext 生命周期以及 Unit of Work 模式。

### 课程笔记

把 Entity Framework Core 整合进专业的解决方案,需要超越基础的 CRUD 操作,去应对架构层面的关注点。

#### 仓储模式

为了防止业务逻辑与持久化层紧密耦合,使用 Repository 模式来抽象数据访问。
这确保数据库架构或 ORM 本身的变化不会导致应用核心逻辑的大范围改动。

```csharp
public class GenreRepository: IGenreRepository
{
    private readonly MoviesContext _context;
    private readonly IUnitOfWorkManager _uowManager;

    public GenreRepository(MoviesContext context, IUnitOfWorkManager uowManager)
    {
        _context = context;
        _uowManager = uowManager;
    }

    public async Task<IEnumerable<Genre>> GetAll()
    {
        return await _context.Genres.ToListAsync();
    }

    public async Task<Genre?> Get(int id)
    {
        return await _context.Genres.FindAsync(id);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54130159/?t=0)

#### 日志记录与监控

生产系统需要细致的可观测性。
配置结构化日志(例如 Serilog),可以通过捕获详细的执行上下文和异常,来监控数据库性能并排查问题。

```json
"Serilog": {
  "Using": [ "Serilog.Sinks.Console", "Serilog.Sinks.File" ],
  "MinimumLevel": {
    "Default": "Debug"
  },
  "WriteTo": [
    {
      "Name": "Console",
      "Args": {
        "outputTemplate": "[{Timestamp:HH:mm:ss} {SourceContext} [{Level}] {Message}{NewLine}{Exception}",
        "theme": "Serilog.Sinks.SystemConsole.Themes.ConsoleTheme::None, Serilog.Sinks.Console"
      }
    }
  ]
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54130159/?t=10)

#### 多租户

处理多个租户需要确保严格的数据隔离。
一种常见的方式是使用全局查询筛选器,根据存储在影子属性中的租户标识自动筛选数据。

```csharp
public abstract class TenantAwareMapping<TEntity> : IEntityTypeConfiguration<TEntity> 
    where TEntity : class
{
    private readonly MoviesContext _context;

    protected TenantAwareMapping(MoviesContext context)
    {
        _context = context;
    }
    
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder
            .Property<string?>("TenantId")
            .HasColumnType("varchar(32)");
        builder.HasIndex("TenantId");
        builder
            .HasQueryFilter(entity =>
                EF.Property<string>(entity, "TenantId") 
                == _context.TenantId);
        
        ConfigureEntity(builder);
    }

    public abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54130159/?t=13)

#### DbContext 生命周期与工作单元

DbContext 的生命周期通常在依赖注入容器中作为 Scoped 服务来管理。
为了协调跨多个 repository 的原子操作,实现了 Unit of Work 模式,提供一种集中提交更改的方式。

```csharp
public class UnitOfWorkManager : IUnitOfWorkManager
{
    private readonly MoviesContext _context;

    public UnitOfWorkManager(MoviesContext context)
    {
        _context = context;
    }

    private bool _isUnitOfWorkStarted = false;
    public void StartUnitOfWork()
    {
        _isUnitOfWorkStarted = true;
    }
    public bool IsUnitOfWorkStarted => _isUnitOfWorkStarted;

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54130159/?t=19)
