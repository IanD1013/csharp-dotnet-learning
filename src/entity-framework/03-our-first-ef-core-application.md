# Our first EF Core Application

> 课程:[From Zero to Hero: Entity Framework Core in .NET](https://dometrain.com/course/from-zero-to-hero-entity-framework-core-in-dotnet/) · 第 3 章
> 共 8 课 · 约 56:42
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [The application we will build the data access for](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/the-application-we-will-build-the-data-access-for-54129608/) | 6:32 | [↓](#1-the-application-we-will-build-the-data-access-for) |
| 2 | [Creating our first EFCore Model](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/creating-our-first-efcore-model-54129609/) | 4:07 | [↓](#2-creating-our-first-efcore-model) |
| 3 | [Getting the Model to work in our application](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/getting-the-model-to-work-in-our-application-54129610/) | 10:59 | [↓](#3-getting-the-model-to-work-in-our-application) |
| 4 | [Implementing data manipulation operations](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/implementing-data-manipulation-operations-54129611/) | 17:43 | [↓](#4-implementing-data-manipulation-operations) |
| 5 | [Implementing query behavior](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/implementing-query-behavior-54129612/) | 8:15 | [↓](#5-implementing-query-behavior) |
| 6 | [Seeing what goes on in the Database](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/seeing-what-goes-on-in-the-database-54129613/) | 4:08 | [↓](#6-seeing-what-goes-on-in-the-database) |
| 7 | [Projecting data to optimize queries](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/projecting-data-to-optimize-queries-54129614/) | 4:04 | [↓](#7-projecting-data-to-optimize-queries) |
| 8 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54129615/) | 0:54 | [↓](#8-section-recap) |

## 1. The application we will build the data access for

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/the-application-we-will-build-the-data-access-for-54129608/) · 6:32

### 总结

本课通过介绍一个入门级 ASP.NET Core Web API,并使用 Docker 配置本地 SQL Server 环境,为整个课程打下基础。
入门应用包含一个用于电影管理的基础控制器和对应的数据模型,而数据库设置则通过容器化确保在不同操作系统之间拥有一致的开发环境。

### 核心概念

- **入门解决方案(Starter Solution)**:一个带有预定义控制器和模型的基础 ASP.NET Core Web API。
- **Docker 化的 SQL Server**:使用 Docker 运行 SQL Server 可确保跨平台兼容性(Windows、macOS、Linux),并且便于清理环境。
- **容器配置**:设置用于接受 EULA、SA 密码和重启策略的环境变量。
- **数据库初始化**:连接到容器化实例并创建一个专用的应用数据库。

### 课程笔记

本课程的入门解决方案是一个简单的 ASP.NET Core Web API。
它包含一个 `MoviesController`,其中有标准的 CRUD 动作(Get、GetAll、Create、Update、Delete),它们目前都会抛出 `NotImplementedException`。
这些端点将是使用 Entity Framework Core 实现数据访问的主要关注点。

```csharp
using Dometrain.EFCore.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dometrain.EFCore.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MoviesController : Controller
{
    [HttpGet]
    [ProducesResponseType(typeof(List<Movie>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(Movie), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] Movie movie)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Movie movie)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remove([FromRoute] int id)
    {
        throw new NotImplementedException();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/the-application-we-will-build-the-data-access-for-54129608/?t=55)

API 使用的数据模型是一个简单的 `Movie` 类,包含用于标识、标题、上映日期和剧情简介的基础属性。

```csharp
namespace Dometrain.EFCore.API.Models;

public class Movie
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Synopsis { get; set; }
}
```

为了给应用提供数据库,SQL Server 运行在一个 Docker 容器中。
这确保了无论宿主操作系统是什么,环境都保持一致。
该容器配置了一个用于接受最终用户许可协议的环境变量(`ACCEPT_EULA=Y`),以及系统管理员(`SA`)用户的密码。
使用 `--restart unless-stopped` 标志是为了确保宿主系统重启时数据库会自动启动。

```powershell
PS C:\Users\hlowette> docker run --restart unless-stopped -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=MySaPassword123" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/the-application-we-will-build-the-data-access-for-54129608/?t=235)

容器运行起来之后,你可以使用 Azure Data Studio 之类的工具连接到它。
使用 `SA` 用户名和 Docker 命令中配置的密码,连接到默认 SQL Server 端口(`1433`)上的 `localhost`。
由于容器使用的是自行生成的证书,你必须在连接设置中启用 "Trust server certificate" 选项。

连接之后,你应当为应用创建一个专用的数据库,而不是使用系统的 `master` 数据库。

```sql
CREATE DATABASE MoviesDB;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/the-application-we-will-build-the-data-access-for-54129608/?t=375)

## 2. Creating our first EFCore Model

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/creating-our-first-efcore-model-54129609/) · 4:07

### 总结

本课介绍在 .NET 应用中初始设置 Entity Framework Core 的过程,包括安装必要的 SQL Server 提供程序包以及创建数据库上下文。
它演示了如何定义 `DbContext`、如何使用 `DbSet` 把模型类映射到数据库表,以及如何在上下文定义中处理 C# 可空引用类型,以确保代码干净且没有警告。

### 核心概念

* **NuGet 包**:安装 `Microsoft.EntityFrameworkCore.SqlServer` 会同时提供 EF Core 的基础功能和 SQL Server 专用的提供程序。
* **DbContext**:为给定数据模型协调 Entity Framework 功能的主要类。
* **DbSet**:`DbContext` 上的一个属性,表示上下文中可以从数据库查询的实体集合。
* **可空引用类型**:在现代 C# 中定义 `DbSet` 属性时处理编译器警告(CS8618)。
* **POCO 模型**:使用简单的 C# 类作为模型,不需要特定的基类。

### 课程笔记

要开始使用 Entity Framework Core,项目必须包含相应的数据库提供程序包。
对于 SQL Server,所需的包是 `Microsoft.EntityFrameworkCore.SqlServer`。
这个包包含 Entity Framework Core 的基础库,以及与 SQL Server 数据库通信所需的逻辑。
对于 PostgreSQL 或 SQLite 等其他数据库引擎,也存在类似的提供程序包。

在实现开始时,项目包含一个带有未实现动作的控制器:

```csharp
using Dometrain.EFCore.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dometrain.EFCore.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MoviesController : Controller
{    
    [HttpGet]
    [ProducesResponseType(typeof(List<Movie>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        throw new NotImplementedException();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/creating-our-first-efcore-model-54129609/?t=10)

为了管理数据访问,创建一个 `Data` 文件夹,并定义一个数据库上下文类,例如 `MoviesContext`。
这个类必须继承自 `Microsoft.EntityFrameworkCore.DbContext`,才能启用 Entity Framework 的功能。

```csharp
using Microsoft.EntityFrameworkCore;

namespace Dometrain.EFCore.API.Data;

public class MoviesContext : DbContext
{    
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/creating-our-first-efcore-model-54129609/?t=85)

为了让上下文知道有哪些数据库表,定义类型为 `DbSet<T>` 的属性,其中 `T` 是模型类。
例如,要把 `Movie` 模型映射到 `Movies` 表:

```csharp
using Dometrain.EFCore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Dometrain.EFCore.API.Data;

public class MoviesContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/creating-our-first-efcore-model-54129609/?t=125)

在启用了可空引用类型的项目中,编译器可能会发出警告(CS8618),因为 `Movies` 属性是不可空的,而且没有在构造函数中初始化。
虽然可以用 null 包容运算符(`!`)来抑制这个警告,但一种更干净、也更能表达意图的做法是使用一个调用 `Set<T>()` 方法的只读属性。
Entity Framework 会自动初始化这个属性。

```csharp
using Dometrain.EFCore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Dometrain.EFCore.API.Data;

public class MoviesContext : DbContext
{
    public DbSet<Movie> Movies => Set<Movie>();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/creating-our-first-efcore-model-54129609/?t=205)

模型类本身不需要特定的基类。
它们由直接映射到数据库表中各列的属性组成。

```csharp
namespace Dometrain.EFCore.API.Models;

public class Movie
{
    public int Id { get; set; }
    public string? Title { get; set; }    
    public DateTime ReleaseDate { get; set; }
    public string? Synopsis { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/creating-our-first-efcore-model-54129609/?t=235)

## 3. Getting the Model to work in our application

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/getting-the-model-to-work-in-our-application-54129610/) · 10:59

### 总结

本课介绍把 DbContext 集成到 ASP.NET Core 应用中的实际步骤。
它演示了如何在依赖注入容器中注册上下文、如何把它注入到控制器中,以及如何用连接字符串配置 SQL Server 提供程序。
此外,它还介绍了 EnsureDeleted 和 EnsureCreated 方法,作为在早期开发阶段让数据库架构与应用模型保持同步的一种临时方式。

### 核心概念

- **依赖注入(DI)**:在服务容器中注册 `DbContext`,使其可以被注入到控制器中。
- **Scoped 生命周期**:默认情况下,`AddDbContext` 以 scoped 生命周期注册上下文,这意味着每个 HTTP 请求都会创建一个新实例。
- **异步操作**:使用 `ToListAsync()` 执行非阻塞的数据库查询。
- **提供程序配置**:重写 `OnConfiguring` 以指定数据库引擎(SQL Server)和连接字符串。
- **连接字符串参数**:在开发环境中使用 `TrustServerCertificate=True` 绕过 SSL 证书验证。
- **架构管理**:在开发期间使用 `EnsureDeleted()` 和 `EnsureCreated()` 快速迭代架构。

### 课程笔记

为了让 `MoviesContext` 在应用中可用,必须把它集成到 ASP.NET Core 依赖注入框架中。
这使框架可以通过构造函数向控制器提供上下文实例。

```csharp
using Dometrain.EFCore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Dometrain.EFCore.API.Data;

public class MoviesContext : DbContext
{
    public DbSet<Movie> Movies => Set<Movie>();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/getting-the-model-to-work-in-our-application-54129610/?t=10)

在 `MoviesController` 中,上下文被注入并保存在一个私有只读字段里。
这使控制器的所有方法都能访问数据库。

```csharp
private readonly MoviesContext _context;

public MoviesController(MoviesContext context)
{
    _context = context;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/getting-the-model-to-work-in-our-application-54129610/?t=55)

实现 `GetAll` 方法时,最佳实践是使用异步执行。
`ToListAsync()` 扩展方法(来自 `Microsoft.EntityFrameworkCore`)用于在不阻塞执行线程的情况下从数据库获取所有电影。

```csharp
[HttpGet]
[ProducesResponseType(typeof(List<Movie>), StatusCodes.Status200OK)]
public async Task<IActionResult> GetAll()
{
    return Ok(await _context.Movies.ToListAsync());
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/getting-the-model-to-work-in-our-application-54129610/?t=130)

为了让控制器能接收到上下文,必须在 `Program.cs` 中使用 `AddDbContext` 扩展方法注册它。
这会以 scoped 生命周期注册上下文,这是 Web 应用的标准做法。

```csharp
// Add a DbContext here
builder.Services.AddDbContext<MoviesContext>();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/getting-the-model-to-work-in-our-application-54129610/?t=175)

如果此时运行应用,会出现一个异常,提示没有配置数据库提供程序。
为了解决这个问题,在 `MoviesContext` 类中重写 `OnConfiguring` 方法。
在这里,使用 `UseSqlServer` 方法定义连接字符串,包括服务器地址、数据库名称和凭据。
在使用自签名证书的开发环境中(例如在 Docker 中),必须在连接字符串里加上 `TrustServerCertificate=True`。

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.UseSqlServer("""
        Data Source=localhost;
        Initial Catalog=MoviesDB;
        User Id=sa;
        Password=MySaPassword123;
        TrustServerCertificate=True;
        """);
    base.OnConfiguring(optionsBuilder);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/getting-the-model-to-work-in-our-application-54129610/?t=430)

默认情况下,EF Core 使用约定把模型映射到数据库对象。
例如,它会查找名为 `Movies` 的表,因为 `DbSet` 属性的名称是 `Movies`。
如果该表不存在,就会抛出异常。
为了在开发期间快速让数据库架构与模型同步,你可以使用一种临时方法,在每次应用启动时重新创建数据库。
这包括创建一个服务作用域、解析上下文,然后依次调用 `EnsureDeleted()` 和 `EnsureCreated()`。

```csharp
// DIRTY HACK, we WILL come back to fix this
var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<MoviesContext>();
context.Database.EnsureDeleted();
context.Database.EnsureCreated();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/getting-the-model-to-work-in-our-application-54129610/?t=595)

*注意:这种方法会删除数据库中的所有数据,绝不应在生产环境中使用。*

## 4. Implementing data manipulation operations

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/implementing-data-manipulation-operations-54129611/) · 17:43

### 总结

本课介绍如何在 ASP.NET Core 控制器中使用 Entity Framework Core 实现 CRUD 操作。
它演示了如何把 DbSet 当作仓储来获取、添加、更新和删除实体,同时解释了底层的工作单元(Unit of Work)模式,以及变更跟踪器(Change Tracker)在把修改持久化到数据库中所起的作用。

### 核心概念

- **DbSet 作为仓储**:`DbSet<T>` 是查询和操作特定实体类型数据的主要接口。
- **查询方法**:对比 `FirstOrDefaultAsync`(找不到时返回 null)、`SingleOrDefaultAsync`(找到多个匹配时抛出异常)和 `FindAsync`(先检查内存再查询数据库)。
- **工作单元模式**:对 `DbContext` 所做的更改(添加、更新、删除)会在内存中排队,只有在调用 `SaveChangesAsync` 时才会持久化到数据库。
- **自动生成 ID**:新实体成功持久化到数据库之后,Entity Framework Core 会自动填充它的主键属性。
- **变更跟踪**:EF Core 使用代理类和变更跟踪器来监视从数据库获取的实体上的修改,从而无需显式的 "Update" 调用就能自动更新。

### 课程笔记

#### 获取数据

要按标识符获取单个条目,`DbSet` 上有几个可用的 LINQ 扩展方法。
`FirstOrDefaultAsync` 方法接受一个谓词(一个 lambda 函数),返回第一个匹配的条目,如果没有找到匹配项则返回 `null`。
相比在没有匹配项时会抛出异常的 `FirstAsync`,更推荐使用它,因为它让 API 可以返回一个干净的 `404 Not Found` 状态码。

或者,如果你预期恰好只有一个结果,可以使用 `SingleOrDefaultAsync`;如果在数据库中找到多于一个匹配项,它会抛出异常。

第三种选择是 `FindAsync`。
与另外两个不同,`FindAsync` 直接接受主键值,而不是谓词。
它首先检查该实体是否已经在 `DbContext` 内存中被跟踪。
如果找到了,它会立即返回内存中的对象,从而避免一次数据库往返。
如果在内存中没有找到,它会查询数据库。
这带来了性能上的好处,但如果数据库被另一个进程更新过,就会有提供过时数据的轻微风险。

```csharp
[HttpGet("{id:int}")]
[ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public async Task<IActionResult> Get([FromRoute] int id)
{
    // Queries database, returns first match, null if not found.
    // var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
    // Similar to FirstOrDefault, but throws if more than one match is found.
    // var movie = await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);
    // Serves match from memory if already fetched, otherwise queries DB.
    var movie = await _context.Movies.FindAsync(id);
    
    return movie == null
        ? NotFound()
        : Ok(movie);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/implementing-data-manipulation-operations-54129611/?t=280)

```text
FindAsync(id)
    |
    v
已在 DbContext 内存中被跟踪? --是--> 返回内存中的对象(无数据库往返)
    |
    否
    v
查询数据库
```

#### 创建数据

创建一条新记录需要使用 `AddAsync` 把反序列化得到的对象添加到 `DbSet` 中。
由于 `DbContext` 实现了工作单元模式,实体不会立即保存到数据库。
相反,该操作会被排队,直到调用 `SaveChangesAsync`。
这个方法会创建一个数据库事务,执行所有挂起的更改,然后提交事务。

在调用 `SaveChangesAsync` 之前,新实体通常带有一个默认 ID(例如 0)。
调用之后,Entity Framework Core 会自动用数据库生成的值填充 ID 属性。
随后使用 `CreatedAtAction` 辅助方法返回一个 `201 Created` 响应,其中包含新资源的位置以及已填充好的对象。

```csharp
[HttpPost]
[ProducesResponseType(typeof(Movie), StatusCodes.Status201Created)]
public async Task<IActionResult> Create([FromBody] Movie movie)
{
    await _context.Movies.AddAsync(movie);
    
    // movie has no ID
    
    await _context.SaveChangesAsync();
    
    // movie has an ID

    return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/implementing-data-manipulation-operations-54129611/?t=580)

```text
AddAsync(movie)        -> 在内存中排队,movie 没有 ID(例如 0)
SaveChangesAsync()     -> 创建事务 -> 执行所有挂起的更改 -> 提交事务
之后                    -> movie.Id 由数据库生成的值填充
CreatedAtAction(...)   -> 201 Created
```

#### 更新数据

更新借助的是 EF Core 的变更跟踪器。
当通过 `DbContext` 获取实体时(例如使用 `FindAsync`),EF Core 通常会返回一个继承自该模型的代理类。
每当属性值发生变化时,这个代理都会通知 `DbContext`。
要更新一条记录,你需要获取现有实体、修改它的属性,然后调用 `SaveChangesAsync`。
变更跟踪器会识别出哪些属性被修改了,并生成相应的 SQL `UPDATE` 语句。

```csharp
[HttpPut("{id:int}")]
[ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Movie movie)
{
    var existingMovie = await _context.Movies.FindAsync(id);

    if (existingMovie is null)
        return NotFound();

    existingMovie.Title = movie.Title;
    existingMovie.ReleaseDate = movie.ReleaseDate;
    existingMovie.Synopsis = movie.Synopsis;

    await _context.SaveChangesAsync();

    return Ok(existingMovie);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/implementing-data-manipulation-operations-54129611/?t=880)

#### 删除数据

要删除数据,必须先确定是哪个实体。
标准做法是先获取现有实体以确认它存在(如果不存在则返回 `404 Not Found`),然后把这个被跟踪的对象传给 `Remove` 方法。
与添加和更新一样,删除也会被排队,只有在调用 `SaveChangesAsync` 时才最终完成。

```csharp
[HttpDelete("{id:int}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public async Task<IActionResult> Remove([FromRoute] int id)
{
    var existingMovie = await _context.Movies.FindAsync(id);

    if (existingMovie is null)
        return NotFound();

    _context.Movies.Remove(existingMovie);

    await _context.SaveChangesAsync();

    return Ok();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/implementing-data-manipulation-operations-54129611/?t=955)

执行删除还有其他方式。
你可以直接在 `DbContext` 上而不是在 `DbSet` 上调用 `Remove`,因为上下文知道实体属于哪个表。
此外,如果你想避免先获取实体所需的那次数据库往返,可以实例化一个该实体类型的新对象,设置它的主键,然后把这个 "stub" 传给 `Remove` 方法。
EF Core 会使用主键来确定要在数据库中删除哪条记录。

```csharp
// Alternative: Remove without fetching first to save a round-trip
_context.Movies.Remove(new Movie { Id = id });
await _context.SaveChangesAsync();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/implementing-data-manipulation-operations-54129611/?t=1015)

## 5. Implementing query behavior

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/implementing-query-behavior-54129612/) · 8:15

### 总结

本课探讨 Entity Framework Core 如何使用 LINQ 和 IQueryable 接口把 C# 代码翻译成数据库查询。
它涵盖了延迟执行这一关键概念:查询被定义后并不会执行,直到显式请求结果时(例如通过 ToListAsync)才会执行。
本课还区分了 IQueryable 和 IEnumerable:前者把筛选条件翻译成 SQL 在服务器端执行,后者在内存中执行操作;并演示了编写 LINQ 的 Lambda 语法和查询语法两种方式。

### 核心概念

- **延迟执行(Deferred Execution)**:针对 IQueryable 或 IEnumerable 的 LINQ 查询在定义时不会执行;只有在显式请求结果时才会执行。
- **IQueryable<T>**:一个接口,允许 LINQ 提供程序(EF Core)把查询翻译成底层数据源的语言(SQL)。
- **IEnumerable<T>**:用于内存集合的接口;针对它的查询使用 C# 逻辑在内存中执行。
- **Lambda 语法**:一种基于方法的 LINQ 查询编写方式,使用 .Where() 之类的扩展方法。
- **查询语法**:一种声明式、类似 SQL 的 LINQ 查询编写语法。

### 课程笔记

为了实现按上映年份获取电影的查询,我们从 `MoviesController` 中的一个方法存根开始。

```csharp
[HttpGet("by-year/{year:int}")]
[ProducesResponseType(typeof(List<Movie>), StatusCodes.Status200OK)]
public async Task<IActionResult> GetAllByYear([FromRoute] int year)
{
    throw new NotImplementedException();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/implementing-query-behavior-54129612/?t=10)

其逻辑是从上下文中访问 `Movies` DB set 并应用一个筛选条件。

```csharp
[HttpGet("by-year/{year:int}")]
[ProducesResponseType(typeof(List<Movie>), StatusCodes.Status200OK)]
public async Task<IActionResult> GetAllByYear([FromRoute] int year)
{
    var allMovies = _context.Movies;

    var filteredMovies = allMovies.Where(m => m.ReleaseDate.Year == year);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/implementing-query-behavior-54129612/?t=70)

乍一看,这似乎效率不高,好像所有电影都被取到内存中之后才进行筛选。
然而,EF Core 使用的是**延迟执行**。
定义查询(`allMovies` 和 `filteredMovies` 变量)并不会触发一次数据库往返。
只有在调用 `ToListAsync()` 之类的物化方法时,查询才会执行。

```csharp
[HttpGet("by-year/{year:int}")]
[ProducesResponseType(typeof(List<Movie>), StatusCodes.Status200OK)]
public async Task<IActionResult> GetAllByYear([FromRoute] int year)
{
    var allMovies = _context.Movies;

    var filteredMovies = allMovies.Where(m => m.ReleaseDate.Year == year);

    return Ok(await filteredMovies.ToListAsync());
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/implementing-query-behavior-54129612/?t=100)

```text
allMovies = _context.Movies          -> 不访问数据库
filteredMovies = allMovies.Where(..) -> 不访问数据库
await filteredMovies.ToListAsync()   -> 执行查询(一次数据库往返)
```

#### IQueryable vs. IEnumerable

这之所以高效,原因在于 `IQueryable<T>` 接口。
虽然 `IQueryable` 和 `IEnumerable` 都支持 LINQ,但它们的行为不同:
- `IEnumerable` 用于内存集合。
  任何筛选都发生在应用的内存中。
- `IQueryable` 用于内存之外的数据源。
  EF Core 会把 LINQ 表达式(例如 `.Where(m => m.ReleaseDate.Year == year)`)翻译成真正的 SQL `WHERE` 子句。

通过显式标注变量类型,`IQueryable` 的使用就变得清晰了:

```csharp
[HttpGet("by-year/{year:int}")]
[ProducesResponseType(typeof(List<Movie>), StatusCodes.Status200OK)]
public async Task<IActionResult> GetAllByYear([FromRoute] int year)
{
    IQueryable<Movie> allMovies = _context.Movies;

    IQueryable<Movie> filteredMovies = allMovies.Where(m => m.ReleaseDate.Year == year);

    return Ok(await filteredMovies.ToListAsync());
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/implementing-query-behavior-54129612/?t=190)

#### LINQ 语法

在 C# 中编写 LINQ 查询主要有两种方式。

##### Lambda 语法

这是大多数场景下的首选方式。
它使用扩展方法,在链式调用多个操作时非常易于维护。

```csharp
[HttpGet("by-year/{year:int}")]
[ProducesResponseType(typeof(List<Movie>), StatusCodes.Status200OK)]
public async Task<IActionResult> GetAllByYear([FromRoute] int year)
{
    var filteredMovies = _context.Movies
        .Where(m => m.ReleaseDate.Year == year);

    return Ok(await filteredMovies.ToListAsync());
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/implementing-query-behavior-54129612/?t=355)

##### 查询语法

查询语法是一种类似 SQL 的声明式方言。
对于连接(join)或分组(group-by)等复杂操作,它可能更具可读性。
注意,与 SQL 不同,`from` 子句放在最前面,以便为查询的其余部分提供 IntelliSense。

```csharp
[HttpGet("by-year/{year:int}")]
[ProducesResponseType(typeof(List<Movie>), StatusCodes.Status200OK)]
public async Task<IActionResult> GetAllByYear([FromRoute] int year)
{
    var filteredMovies =
        from movie in _context.Movies
        where movie.ReleaseDate.Year == year
        select movie;

    return Ok(await filteredMovies.ToListAsync());
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/implementing-query-behavior-54129612/?t=400)

## 6. Seeing what goes on in the Database

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/seeing-what-goes-on-in-the-database-54129613/) · 4:08

### 总结

本课演示如何通过启用简单的控制台日志,来查看 Entity Framework Core 生成的 SQL 查询。
通过把 DbContext 配置为把日志输出到控制台,开发者可以验证 LINQ 查询是如何被翻译成 SQL 的、观察出于安全目的的自动参数化,并了解在日志中屏蔽敏感数据的默认隐私保护。

### 核心概念

- **简单日志(Simple Logging)**:使用 `LogTo` 把 EF Core 的诊断信息输送到控制台。
- **LINQ 翻译**:C# 表达式(例如 `DateTime.Year`)如何被转换为特定数据库的 SQL 函数(例如 `DATEPART`)。
- **SQL 注入防护**:查询的自动参数化,以确保安全。
- **敏感数据保护**:在日志中屏蔽参数值的默认行为,以确保安全并符合 GDPR。
- **日志详细程度**:理解 EF Core 提供的详细生命周期日志,从查询转换到执行。

### 课程笔记

要了解 Entity Framework Core 如何与数据库交互,你可以在 `DbContext` 中启用日志。
最直接的做法是重写 `OnConfiguring` 方法,并使用 `optionsBuilder` 来定义日志应当发送到哪里。

```csharp
namespace Dometrain.EFCore.API.Data;

public class MoviesContext : DbContext
{
    public DbSet<Movie> Movies => Set<Movie>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("""
            Data Source=localhost;
            Initial Catalog=MoviesDB;
            User Id=sa;
            Password=MySaPassword123;
            TrustServerCertificate=True;
            """);
        base.OnConfiguring(optionsBuilder);

    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/seeing-what-goes-on-in-the-database-54129613/?t=25)

通过在 `optionsBuilder` 上调用 `LogTo` 方法,你可以提供一个接受字符串的委托。
传入 `Console.WriteLine` 会把 Entity Framework 所有的调试和诊断语句重定向到标准输出。
虽然这不能替代生产级的日志框架,但对于本地开发和快速调试来说,它是一个有效的工具,可以准确看到幕后发生了什么。

```csharp
namespace Dometrain.EFCore.API.Data;

public class MoviesContext : DbContext
{
    public DbSet<Movie> Movies => Set<Movie>();

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
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/seeing-what-goes-on-in-the-database-54129613/?t=55)

#### 分析生成的 SQL

当执行一个 LINQ 查询时(例如按年份筛选电影),EF Core 会记录发送到数据库的确切 SQL 语句。
在这段输出中可以观察到几个关键行为:

1.  **表别名**:EF Core 会自动为表起别名(例如把 `Movies` 表的别名设为 `m`),为了可读性,通常使用表名的首字母。
2.  **函数翻译**:C# 逻辑会被翻译成等价的 SQL。
    例如,在 LINQ `Where` 子句中访问 `DateTime` 对象的 `Year` 属性,会被翻译成 SQL 的 `DATEPART` 函数,用来从数据库列中提取年份。
3.  **参数化**:查询使用参数(例如 `@__year_0`)而不是硬编码的值。
    这提供了内置的 SQL 注入攻击防护。
4.  **数据隐私**:默认情况下,EF Core 不会记录参数的实际值。
    这种 "敏感数据日志"(Sensitive Data Logging)是禁用的,以防止敏感信息出现在日志文件中,有助于安全和 GDPR 合规。
5.  **执行详细程度**:日志提供了内部过程的详细视图,包括查询表达式是如何被转换的、执行的开始,以及数据读取器的行为。
    这种详细程度可以在之后实现正式日志时进行调整。

## 7. Projecting data to optimize queries

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/projecting-data-to-optimize-queries-54129614/) · 4:04

### 总结

Entity Framework Core 中的投影让开发者可以只选取某个特定操作所需的字段,而不是获取完整的实体。
通过使用 LINQ 的 Select 方法,生成的 SQL 查询会被优化为只从数据库中检索必要的列。
这种做法能显著减少数据传输开销并提升应用性能,尤其是在处理当前上下文并不完全需要的大型文本字段或复杂对象时。

### 核心概念

* 数据投影:塑造查询结果,使其返回特定属性而不是完整实体。
* 性能优化:减少从数据库 "过度获取"(over-fetching)数据。
* LINQ Select 方法:在 IQueryable 中定义投影的主要工具。
* 强类型投影与匿名投影:投影到专用的 DTO(例如 MovieTitle)或匿名类型。
* SQL 翻译:EF Core 如何把投影翻译成 SQL 中特定的 SELECT 语句。

### 课程笔记

本课首先演示了一种返回数据子集的低效方式。
在 `GetAllByYear` 方法的初始实现中,代码按年份筛选电影,然后手动遍历结果来填充一个 `MovieTitle` 对象列表。
这种方式在开始遍历时触发延迟执行,但它从数据库获取的是完整的实体对象。

```csharp
[HttpGet("by-year/{year:int}")]
[ProducesResponseType(typeof(List<MovieTitle>), StatusCodes.Status200OK)]
public async Task<IActionResult> GetAllByYear([FromRoute] int year)
{
    var filteredMovies = _context.Movies
        .Where(movie => movie.ReleaseDate.Year == year);

    List<MovieTitle> filteredTitles = new();

    foreach (var movie in filteredMovies)
    {
        filteredTitles.Add(new MovieTitle { Id = movie.Id, Title = movie.Title});
    }

    return Ok(filteredTitles);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/projecting-data-to-optimize-queries-54129614/?t=10)

`MovieTitle` 类是一个简单的数据传输对象(DTO),只包含 `Id` 和 `Title`。
它用来模拟这样一种场景:对于特定的 API 响应,并不需要完整的 `Movie` 实体(其中包含一个可能很大的 `Synopsis` 字段)。

```csharp
namespace Dometrain.EFCore.API.Models;

public class Movie
{
    public int Id { get; set; }
    public string? Title { get; set; }    
    public DateTime ReleaseDate { get; set; }
    public string? Synopsis { get; set; }
}

public class MovieTitle
{
    public int Id { get; set; }
    public string? Title { get; set; }  
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/projecting-data-to-optimize-queries-54129614/?t=25)

尽管 API 只返回 `MovieTitle` 对象,初始实现仍然是低效的。
由于投影发生在查询执行之后(在 `foreach` 循环中),Entity Framework 会从数据库获取所有列,包括 `Synopsis` 和 `ReleaseDate`。
这种 "过度获取" 是 EF Core 应用中常见的性能问题,因为开发者常常为了简单而倾向于使用完整对象。

为了解决这个问题,应当使用 `Select` 方法把投影移到 `IQueryable` 链中。
这使 EF Core 可以把投影直接翻译进 SQL `SELECT` 语句,确保只从数据库检索 `Id` 和 `Title` 列。

```csharp
[HttpGet("by-year/{year:int}")]
[ProducesResponseType(typeof(List<MovieTitle>), StatusCodes.Status200OK)]
public async Task<IActionResult> GetAllByYear([FromRoute] int year)
{
    var filteredTitles = await _context.Movies
        .Where(movie => movie.ReleaseDate.Year == year)
        .Select(movie => new MovieTitle { Id = movie.Id, Title = movie.Title})
        .ToListAsync();

    return Ok(filteredTitles);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/projecting-data-to-optimize-queries-54129614/?t=190)

```text
foreach 中投影:  数据库 --[Id, Title, ReleaseDate, Synopsis]--> 应用 -> MovieTitle
Select 中投影:   数据库 --[Id, Title]-----------------------------> 应用 -> MovieTitle
```

使用 `Select` 让代码更简洁,也显著更高效。
虽然 API 响应保持完全相同,但底层的 SQL 查询被优化为只通过网络拉取必要的字段,这是控制数据库与应用之间数据传输的一个重要工具。

## 8. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54129615/) · 0:54

### 总结

本课通过回顾把 EF Core 集成到 .NET 应用中所需的基本步骤,为 Entity Framework Core 的入门章节收尾。
它涵盖了包的安装、DbContext 的创建与配置、在 API 控制器中实现 CRUD 操作,以及通过投影和日志来优化数据库交互。

### 核心概念

- 安装 EF Core 提供程序包(例如 SQL Server)。
- 为实体映射定义 DbContext 和 DbSet。
- 通过 OnConfiguring 配置连接字符串和日志。
- 实现异步 CRUD 操作(ToListAsync、FindAsync、AddAsync、SaveChangesAsync)。
- 使用 LINQ 投影(Select)优化查询。
- 使用 LogTo 监视数据库活动。

### 课程笔记

第一章为在 .NET 环境中使用 Entity Framework Core(EF Core)打下了基础。
这个过程从向项目添加必要的 NuGet 包开始,例如 Microsoft.EntityFrameworkCore.SqlServer。

一个核心组件是 DbContext,它充当应用与数据库之间的桥梁。
在这个实现中,MoviesContext 定义了一个 DbSet<Movie> 来表示 movies 表,并配置了到 SQL Server 实例的连接。

```csharp
using Dometrain.EFCore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Dometrain.EFCore.API.Data;

public class MoviesContext : DbContext
{
    public DbSet<Movie> Movies => Set<Movie>();

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
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54129615/?t=4)

MoviesController 演示了如何使用 DbContext 执行 CRUD(Create、Read、Update、Delete)操作。
这包括用 ToListAsync 检索所有记录、用 FindAsync 按主键查找特定记录,以及用 SaveChangesAsync 持久化更改。

```csharp
[HttpGet("{id:int}")]
[ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public async Task<IActionResult> Get([FromRoute] int id)
{
    // Serves match from memory if already fetched, otherwise queries DB.
    var movie = await _context.Movies.FindAsync(id);
    
    return movie == null
        ? NotFound()
        : Ok(movie);
}

[HttpPost]
[ProducesResponseType(typeof(Movie), StatusCodes.Status201Created)]
public async Task<IActionResult> Create([FromBody] Movie movie)
{
    await _context.Movies.AddAsync(movie);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54129615/?t=4)

查询优化通过投影来实现,即只从数据库检索必要的列。
这是使用 LINQ 的 Select 方法实现的,把数据库实体映射为更轻量的数据传输对象,或者 MovieTitle 之类的特定模型。

```csharp
[HttpGet("by-year/{year:int}")]
[ProducesResponseType(typeof(List<MovieTitle>), StatusCodes.Status200OK)]
public async Task<IActionResult> GetAllByYear([FromRoute] int year)
{
    var filteredTitles = await _context.Movies
        .Where(movie => movie.ReleaseDate.Year == year)
        .Select(movie => new MovieTitle { Id = movie.Id, Title = movie.Title})
        .ToListAsync();

    return Ok(filteredTitles);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54129615/?t=14)

为了监视 EF Core 生成的 SQL 查询,在 DbContext 的 OnConfiguring 方法中使用了 LogTo 方法。
这在开发期间提供了对数据库端操作的可见性。
此外,项目中还添加了一个 Genre 模型,以便进一步练习这些原则。

```csharp
namespace Dometrain.EFCore.API.Models;

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54129615/?t=25)
