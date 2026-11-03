# Introduction to Entity Framework Core

> 课程:[From Zero to Hero: Entity Framework Core in .NET](https://dometrain.com/course/from-zero-to-hero-entity-framework-core-in-dotnet/) · 第 2 章
> 共 6 课 · 约 16:14
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [What is an ORM?](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/what-is-an-orm-54129595/) | 3:23 | [↓](#1-what-is-an-orm) |
| 2 | [Where are ORMs useful?](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/where-are-orms-useful-54129596/) | 2:29 | [↓](#2-where-are-orms-useful) |
| 3 | [What is EF Core?](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/what-is-ef-core-54129597/) | 5:16 | [↓](#3-what-is-ef-core) |
| 4 | [Introducing: the database context](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/introducing-the-database-context-54129598/) | 2:05 | [↓](#4-introducing-the-database-context) |
| 5 | [How does a DbContext work?](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/how-does-a-dbcontext-work-54129599/) | 2:25 | [↓](#5-how-does-a-dbcontext-work) |
| 6 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54129600/) | 0:36 | [↓](#6-section-recap) |

## 1. What is an ORM?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/what-is-an-orm-54129595/) · 3:23

### 总结

对象关系映射器(Object Relational Mapper,ORM)充当 C# 这类面向对象编程语言与 SQL Server 这类关系型数据库之间的桥梁。
它把代码中定义的模型(类)映射到数据库结构(表),让开发者可以用标准的编程函数与数据交互,而由 ORM 负责生成用于持久化和检索数据的底层 SQL 语句。

### 核心概念

- **模型与数据点**:模型定义数据的形状或结构,而数据点是遵循该形状的单条记录或实例。
- **关系型表示**:在数据库中,模型表示为带有列的表,数据点表示为行。
- **面向对象表示**:在 C# 中,模型表示为带有属性的类或结构体,数据点表示为对象。
- **映射**:把代码端模型与数据库端模型连接起来的过程。
- **SQL 生成**:ORM 自动把代码端的函数调用翻译成 SQL 语句(SELECT、INSERT、UPDATE、DELETE)。

### 课程笔记

#### 数据的两面

在现代应用开发中,数据从两个不同的视角来看待:持久化层和应用代码。

在**关系型数据库端**(通常是 SQL Server),数据用表来组织。
表通过为数据点应具备的每个属性指定列来定义模型。
每个单独的数据点都作为该表中的一行来存储。
为了与这些数据交互,开发者传统上会编写 SQL 语句,例如用 `SELECT` 查询、用 `INSERT` 创建、用 `UPDATE` 修改、用 `DELETE` 删除记录。

在**代码端**(C#),数据用类或结构体来组织。
它们通过属性或字段定义数据的形状。
单个数据点表示为一个对象,也就是该类的一个实例。
为了操作这些数据,开发者在应用逻辑中调用函数或方法。

```text
  关系型数据库端 (SQL Server)          代码端 (C#)
+-----------------------------+    +-----------------------------+
| 表 (列)       = 模型        |    | 类 / struct (属性) = 模型   |
| 行            = 数据点      |    | 对象 (实例)        = 数据点 |
| SELECT/INSERT/UPDATE/DELETE |    | 函数 / 方法                 |
+-----------------------------+    +-----------------------------+
               ^                                  |
               |          ORM (映射)              |
               +---------- 生成 SQL <-------------+
```

#### ORM 的作用

对象关系映射器(ORM)充当这两种表示之间的中介。
它的主要职责是在 C# 模型与 SQL 数据库模型之间建立映射。

一旦建立了这种映射,ORM 就提供一组函数,让开发者与数据交互时就像只是在操作本地对象一样。
调用这些函数时,ORM 会在幕后自动生成所需的 SQL 语句。
这种抽象免去了开发者为标准持久化操作手动编写和维护复杂 SQL 查询的需要,从而简化开发流程,并确保应用的对象模型与数据库架构之间的一致性。

## 2. Where are ORMs useful?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/where-are-orms-useful-54129596/) · 2:29

### 总结

像 Entity Framework Core 这样的对象关系映射器(ORM)在面向对象代码与关系型数据库之间架起桥梁,用强类型映射取代脆弱的基于字符串的 SQL 查询。
通过集中定义数据模型,ORM 实现了编译时安全,并降低了因架构不匹配而导致运行时异常的风险。
虽然 ORM 历来因性能开销而受到批评,但现代 ORM 已足够高效,能够胜任绝大多数应用场景,因此成为 .NET 开发中数据访问的首选。

### 核心概念

* **字符串字面量与强类型代码**:避免把 SQL 作为未经验证的字符串嵌入所带来的风险。
* **编译时安全**:借助编译器在应用运行之前捕获数据访问错误。
* **集中映射**:在单一位置定义代码与数据模型之间的关系。
* **性能演进**:ORM 从缓慢的抽象转变为高性能工具。
* **热路径优化**:识别仍可能需要原始 SQL 的少数场景。

### 课程笔记

#### 字符串字面量的风险

在不使用 ORM 开发的应用中,SQL 语句通常以字符串字面量的形式存放在代码库里。
这种做法在应用的整个生命周期中都带来显著风险。
每当数据库架构或模型发生变化,每个对应的字符串字面量都必须手动更新。
如果漏掉了某一个字面量或更新有误,编译器无法检测到这个错误,因为它不会对照数据库去验证字符串的内容。
这会导致代码脆弱,并频繁出现运行时异常。

#### 强类型映射与安全性

ORM 让开发者只需定义一次代码模型与数据模型之间的映射。
这种集中的映射可以用集成测试来验证,以确保一致性。
映射建立之后,所有数据交互都通过强类型函数来完成。
这种转变确保了,如果数据被误用或数据的形状不正确,开发者得到的是编译器错误而不是运行时故障,使开发过程更加健壮和可预测。

#### 性能与现代最佳实践

历史上,ORM 往往比原始 SQL 慢好几个数量级,因此人们建议只把它们用于简单的 CRUD 应用,而性能关键的逻辑应使用手写 SQL。
然而,现代 ORM 已有显著改进,如今生成的 SQL 在性能上已非常接近手写语句。

当前的建议是在应用中尽可能多地使用 ORM。
离开 ORM 应只保留给极端场景,例如:
* **热路径**:调用极其频繁、以至于需要微优化的特定代码段。
* **极端复杂**:复杂到 ORM 抽象反而成为障碍而非助益的查询。

在典型应用中,这些场景只占代码库中很小的一部分。
对于绝大多数任务,Entity Framework Core 都能提供所需的性能和安全性。

## 3. What is EF Core?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/what-is-ef-core-54129597/) · 5:16

### 总结

Entity Framework Core(EF Core)是 Microsoft 现代的、跨平台的对象关系映射器(ORM),专为高性能和面向 Web 优化的场景而设计。
它由主要面向桌面应用的原始 Entity Framework(EF)演进而来,成为一种轻量、可扩展且功能丰富的数据访问技术,并利用 LINQ 实现强类型的数据库查询。

### 核心概念

- **对象关系映射器(ORM):** 把数据库表映射为 .NET 对象的框架。
- **LINQ(Language Integrated Query,语言集成查询):** 一项允许在 C# 代码中编写强类型查询的特性,比基于字符串的 SQL 提供更好的重构支持。
- **从 EF 6 演进而来:** EF Core 是对原始 Entity Framework 的彻底重新设计,以支持跨平台 .NET 和现代应用模式。
- **Web 优先设计:** 与原始 EF 不同,EF Core 的默认设置针对 Web 应用的无状态特性做了优化。

### 课程笔记

Microsoft 随 .NET 3.5 Service Pack 1 推出了它的第一个 ORM,即 Entity Framework(EF)。
虽然早期版本在功能和性能上有所欠缺,但纳入语言集成查询(LINQ)带来了显著优势。
LINQ 让开发者可以针对数据库、API 和内存中的集合编写强类型查询。
这种方式优于用字符串字面量编写 SQL 查询,因为它让编译器能够捕获错误,并在数据模型变化时简化重构。

原始的 Entity Framework 经历了 4、5、6 几个版本。
如今,EF 6.4 仍是受支持的版本,但被视为功能已完备。
随着 .NET Core 的发布,Microsoft 重新设计了该框架,打造出 Entity Framework Core。
这个新版本的设计目标是更快、生成更简洁的 SQL 查询,并更好地支持现代 Web 开发。

原始 EF 与 EF Core 的主要区别在于它们的默认配置。
原始 EF 诞生于桌面应用(WinForms 和 WPF)占主导的时代,那时长期存在的数据库连接以及延迟加载(lazy loading)之类的特性都是标准做法。
而 EF Core 是以"Web 优先"的思路构建的。
它的默认设置针对 HTTP 请求/响应周期量身定制,在这个周期中,资源被分配用来处理一个请求,随后被及时释放。
虽然 EF Core 也可以针对桌面场景进行配置,但它的架构是为现代 Web 服务的无状态需求而优化的。

```text
  .NET 3.5 SP1        EF 4 -> EF 5 -> EF 6 (6.4: 受支持, 功能已完备)
  Entity Framework ----------------------------------------------->
  (桌面: WinForms / WPF, 长期连接, lazy loading)

  .NET Core
  Entity Framework Core (重新设计: 更快, 更简洁的 SQL, Web 优先)
  ---------------------------------------------------------------->
```

就当前的 .NET 生态而言,EF Core 已在 EF 6 最重要的方面达到功能对等,并且是 Microsoft 持续开发的主要方向。

## 4. Introducing: the database context

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/introducing-the-database-context-54129598/) · 2:05

### 总结

DbContext 是 Entity Framework Core 的核心组件,充当 C# 代码与底层数据库之间的主要接口。
它管理数据库连接,提供用于查询和持久化数据的强类型 API,并处理关系型行与面向对象模型之间的映射,把特定数据库引擎的复杂性抽象掉。

### 核心概念

- **DbContext 基类**:Entity Framework Core 提供的核心类,开发者通过扩展它来创建自己的数据库上下文。
- **连接管理**:处理数据库连接的生命周期,包括消耗资源的三次握手。
- **数据库无关性**:提供一致的强类型 API,让开发者无需了解具体的底层数据库引擎即可编写 C# 代码。
- **物化(Materialization)**:在查询执行期间把数据库行转换为 C# 对象的过程。
- **持久化**:跟踪对象的更改(新增、更新、删除),并把这些更改同步到数据库。

### 课程笔记

DbContext 是 Entity Framework Core 生态中最关键的组件。
它是 C# 的面向对象世界与数据库表的关系型世界之间的桥梁。
作为对象关系映射器(ORM),它的主要职责是促成数据库行与内存中对象之间的映射。

#### 连接管理

DbContext 的主要角色之一是管理与数据库引擎的连接。
建立数据库连接是一项消耗资源的操作,涉及三次握手。
DbContext 通过在需要时打开连接、并在操作期间保持连接来优化这一过程,从而降低客户端和服务器两端的开销。

#### 数据库抽象与 API

DbContext 公开一个与底层数据库引擎无关的强类型 API。
这种抽象让开发者可以编写 C# 代码与数据交互,而不必关心所使用数据库的特定方言或实现细节。

#### 查询与物化

当通过 DbContext API 执行查询时,框架会把请求翻译成 SQL SELECT 语句。
随后,数据库返回的结果会被物化,也就是从原始数据行转换为可以在应用代码中操作的 C# 对象。

#### 变更跟踪与持久化

除了查询之外,DbContext 还会跟踪对象的状态。
如果对象被添加、修改或移除,上下文会管理这些更改。
当这些更改被持久化时,DbContext 会在数据库上生成并执行相应的 INSERT、UPDATE 或 DELETE 语句,确保数据库状态与应用状态一致。

```text
  C# 代码                    DbContext                       数据库
+-----------+   查询    +----------------+   SELECT     +-----------+
|           |---------->| 翻译           |------------->|           |
| C# 对象   |<----------| 物化           |<-------------| 数据行    |
|           |           |                |              |           |
| 添加/修改 |---------->| 变更跟踪       |------------->| INSERT/   |
| /移除     |           | 持久化         |              | UPDATE/   |
+-----------+           +----------------+              | DELETE    |
                                                        +-----------+
```

## 5. How does a DbContext work?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/how-does-a-dbcontext-work-54129599/) · 2:25

### 总结

DbContext 充当应用代码与数据库之间的桥梁,它维护着一个由类结构、数据库表以及二者之间映射组成的模型。
虽然 EF Core 在很大程度上依赖约定来自动完成这一过程,但它会在进程中创建第一个上下文实例时执行一次性的模型构建操作,这可能影响初始性能,但能确保后续操作高效。

### 核心概念

* **类形状发现**:上下文分析代码,以了解它将返回的对象的结构。
* **数据库架构映射**:上下文识别它将查询的数据库表的结构。
* **映射逻辑**:建立一个映射,用于定义数据如何在类属性与数据库列之间传递。
* **约定**:减少手动配置的默认行为(例如按 DbSet 命名表,或通过名称 'Id' 识别主键)。
* **模型初始化**:在进程中首次使用上下文时构建映射模型的一次性过程。

### 课程笔记

为了正确工作,`DbContext` 必须了解应用的三个特定方面:代码中类的形状、数据库中表的结构,以及这两个领域之间的映射。
这种映射至关重要,因为它定义了对 C# 对象所做的更改如何传递到底层的数据库表。

```text
  代码中的类形状          映射              数据库中的表结构
+----------------+   +--------------+   +----------------+
| C# 对象        |-->| DbContext    |-->| 数据库表       |
| (属性)         |   | 映射         |   | (列)           |
+----------------+   +--------------+   +----------------+
```

Entity Framework Core 利用约定来尽量减少所需的手动配置。
例如,框架会自动假定 `DbSet` 属性的名称与数据库中某个表的名称相对应。
类似地,它通过查找名为 `Id` 或由类名加 `Id` 组合而成(例如 `UserId`)的属性来识别主键。
只有当数据库架构或应用逻辑偏离这些标准约定时,才需要额外的配置。

内部模型的构建发生在运行中的进程内某个特定类型的第一个 `DbContext` 启动时。
在这个阶段,Entity Framework 会评估所有约定以及提供的任何额外配置,以构建数据层的完整心智模型。
这个初始化过程可能需要几百毫秒。
这一初始延迟常常是"Entity Framework 很慢"这一误解的来源;虽然第一次查询可能会经历这种"冷启动"开销,但之后所有的上下文实例和查询都非常高效,因为它们复用了已经构建好的模型。

## 6. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54129600/) · 0:36

### 总结

本课对 Entity Framework Core 的入门部分做了高层次的总结,涵盖对象关系映射器(ORM)的基本定义、EF Core 在 .NET 生态中的角色,以及 DbContext 的核心职责。

### 核心概念

* **对象关系映射(ORM):** 弥合面向对象应用代码与关系型数据库表之间鸿沟的机制。
* **.NET 中的 EF Core:** 理解 Entity Framework Core 相对于 .NET 领域中其他数据访问技术的定位。
* **DbContext:** 负责与数据库交互、跟踪更改和映射实体的主要类。

### 课程笔记

入门部分奠定了使用 Entity Framework Core 所需的基础知识。
对象关系映射器(ORM)充当应用中定义的对象与数据库表中存储的行之间的翻译层。
通过自动完成这种映射,开发者可以使用强类型对象与数据交互,而不必为每个操作手动编写 SQL 查询。

Entity Framework Core 是 Microsoft 为 .NET 生态提供的现代、跨平台 ORM。
它与其他数据访问选项并存,但为复杂的数据建模和变更跟踪提供了全面的功能集。

EF Core 的核心是 `DbContext`。
这个组件充当通往数据库的网关,管理实体的生命周期,并提供用于执行数据库操作的心智模型。
在进入实现之前,理解 `DbContext` 的职责(包括连接管理和变更跟踪)是必不可少的。
