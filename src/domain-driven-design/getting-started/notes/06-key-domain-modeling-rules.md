# Key Domain Modeling Rules

> 课程:[Getting Started: Domain-Driven Design](https://dometrain.com/course/getting-started-domain-driven-design-ddd/) · 第 6 章
> 共 6 课 · 约 15:46
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Domain Modeling & Analysis Paralysis](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/domain-modeling-analysis-paralysis-54123402/) | 1:41 | [↓](#1-domain-modeling--analysis-paralysis) |
| 2 | [Domain Model Identity Uniqueness](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/domain-model-identity-uniqueness-54123403/) | 2:28 | [↓](#2-domain-model-identity-uniqueness) |
| 3 | [Domain Object Deletion](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/domain-object-deletion-54123405/) | 3:38 | [↓](#3-domain-object-deletion) |
| 4 | [One Aggregate Per Transaction](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/one-aggregate-per-transaction-54123406/) | 3:21 | [↓](#4-one-aggregate-per-transaction) |
| 5 | [Reference Between Domain Models](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/reference-between-domain-models-54123407/) | 3:43 | [↓](#5-reference-between-domain-models) |
| 6 | [Section Recap](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/section-recap-54123408/) | 0:55 | [↓](#6-section-recap) |

## 1. Domain Modeling & Analysis Paralysis

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/domain-modeling-analysis-paralysis-54123402/) · 1:41

### 总结

领域建模是一个迭代且充满挑战的过程,战略设计与战术设计在其中交汇,共同定义系统边界与内部逻辑。
由于现实世界的领域十分复杂并会随时间演变,实现完美的表示往往是不可能的,因此必须避免分析瘫痪(analysis paralysis),接受模型会随着系统增长而需要持续重构和更新。

### 核心概念

* 战略设计与战术设计的相互依存。
* 实现精确领域表示的困难。
* 领域建模中的分析瘫痪。
* 领域模型作为有生命、不断演进的实体的本质。
* 接受建模中的权衡与取舍。

### 课程笔记

战略设计与战术设计紧密相连。
这个过程通常从战略设计开始,即把一个大型系统分解为更小、更易管理的子系统。
在每个子系统内部,使用战术设计来定义具体的领域模型。
然而,这个过程很少是线性的;随着开发过程中逻辑实体逐渐浮现,往往会清楚地发现最初的领域设计需要调整。

领域建模本质上是困难的。
几乎不可能找到一种完全精确的领域表示。
这种困难的产生是因为领域并非静态的;随着系统增长,以及现实用户引入了初始设计阶段未曾预料到的边界情况,领域会变得越来越复杂。

开发者在尝试为领域建模时经常会陷入分析瘫痪。
当花费过多时间试图找到一种与脑海中业务逻辑图景完全匹配的表示时,就会出现这种情况。
重要的是要认识到,找到精确的表示是一项不可能完成的任务。
建模涉及做出必要的牺牲与权衡。

领域模型应被视为一个有生命的实体。
可以预期,领域及其相关模型会随时间被更新和重构。
随着系统逐渐成熟,代码必须加以调整,以反映对领域及其不断演变的需求的更深理解。

## 2. Domain Model Identity Uniqueness

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/domain-model-identity-uniqueness-54123403/) · 2:28

### 总结

本课定义了在领域驱动设计语境下不同领域对象的标识唯一性范围。
它确立了这样一点:聚合根需要在整个系统中全局唯一以确保正确引用,而内部实体只需要在其父聚合的边界内唯一。
本课还涵盖了识别特定实体的实际要求,以及选择不同 ID 生成策略(例如 GUID 与本地自增值)所带来的影响。

### 核心概念

* **聚合根唯一性**:聚合根的标识符必须在整个系统中唯一。
* **实体唯一性**:非聚合根实体的标识符只需在其父聚合的范围内唯一。
* **全局实体识别**:要在系统范围内唯一地识别一个实体,需要父聚合 ID 与实体 ID 的组合。
* **ID 生成策略**:在 GUID 与自增 ID 之间的选择,会影响系统内唯一性的管理方式和被看待的方式。

### 课程笔记

在领域驱动设计中,标识唯一性的要求取决于对象是聚合根还是内部实体。
在把领域对象持久化到数据库时,这一区别尤为重要。

#### 聚合根标识

聚合根是一组领域对象的主要入口点。
为了让系统能够正确地引用和检索某个特定聚合,其标识符必须在整个系统中唯一。
无论使用 GUID 还是系统范围内的自增整数,同一类型的任意两个聚合都不应共享同一个 ID。

#### 实体标识

与聚合根不同,内部实体不一定需要全局唯一。
由于实体始终是某个父聚合的一部分,它的标识符只需在该特定聚合的边界内唯一。
例如,两个不同的聚合可以都包含一个 ID 为 "1" 的实体,而不会在领域模型中造成冲突。

#### 全局识别实体

由于实体 ID 只在局部唯一,仅凭其 ID 无法在整个系统中唯一地识别一个实体。
要定位某个特定实体,系统必须使用由父聚合 ID 和实体 ID 组成的复合引用。
这两条信息结合在一起,提供了对该特定对象的全局唯一引用。

```text
  全局唯一引用 = (父聚合 ID, 实体 ID)

  Aggregate A (ID 全局唯一)        Aggregate B (ID 全局唯一)
    +-- Entity (ID = "1")            +-- Entity (ID = "1")

  (A, "1") != (B, "1")
```

#### 务实的考量

这些规则的实现往往取决于所选的 ID 生成策略。
如果系统为所有领域对象(包括聚合和实体)都使用 GUID,那么从技术上讲每个实体都会拥有一个全局唯一的 ID。
在这种情况下,无需显式引用其父聚合也能识别一个实体。
然而,如果系统使用的是自增的或相对于父聚合分配的 ID,那么使用父 ID 进行全局识别的要求依然至关重要。

## 3. Domain Object Deletion

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/domain-object-deletion-54123405/) · 3:38

在删除一个领域对象(具体来说是聚合根)时,整个聚合被视为一个单一的单元。
这确保了聚合的内部一致性在其整个生命周期中(包括从系统中移除时)都得到维护。

### 核心概念

- **原子删除**:聚合作为一个整体被删除,包括其所有内部实体和值对象。
- **从属实体(Owned Entities)**:内部集合(例如某个 session 中的 reservations 或 categories)被映射为从属实体,由持久化层级联删除。
- **聚合隔离**:聚合之间不得存在直接的数据库级关系(外键)。
- **最终一致性**:删除之后需要在其他聚合中发生的变更,必须通过领域事件而不是数据库约束来处理。
- **集成事件**:当把聚合在不同的限界上下文或应用之间迁移时,领域事件会被替换为通过消息总线通信的集成事件。

### 课程笔记

`Session` 聚合是说明删除如何影响相关数据的一个主要示例。
该聚合包含 reservations 和 categories 的内部列表,以及定义 session 状态的各种属性。

```csharp
using ErrorOr;

namespace DomeGym.Domain.SessionAggregate;

public class Session : AggregateRoot
{
    private readonly List<Reservation> _reservations = new();
    private readonly List<SessionCategory> _categories = new();

    public int NumParticipants => _reservations.Count;

    public DateOnly Date { get; }

    public TimeRange Time { get; } = null!;

    public string Name { get; } = null!;

    public string Description { get; } = null!;

    public int MaxParticipants { get; }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/domain-object-deletion-54123405/?t=10)

其中一个内部集合管理 `SessionCategory` 对象,它们以 `SmartEnum` 实现,用来表示特定类型的 session,例如 Kickboxing 或 Pilates。

```csharp
using Ardalis.SmartEnum;

namespace DomeGym.Domain.SessionAggregate;

public class SessionCategory : SmartEnum<SessionCategory>
{
    public static readonly SessionCategory Kickboxing = new(nameof(Kickboxing), 0);
    public static readonly SessionCategory Functional = new(nameof(Functional), 1);
    public static readonly SessionCategory Zoomba = new(nameof(Zoomba), 2);
    public static readonly SessionCategory Pilates = new(nameof(Pilates), 3);
    public static readonly SessionCategory Yoga = new(nameof(Yoga), 4);

    public SessionCategory(string name, int value) : base(name, value)
    {
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/domain-object-deletion-54123405/?t=25)

在关系型数据库中,`Session` 聚合被映射到多张表:主表 `Sessions`、`SessionReservations` 表和 `SessionCategories` 表。
为确保聚合作为一个单一单元被删除,这些关系被配置为从属实体。
这种配置确保它们会随 session 自动加载,并在 session 被移除时被级联删除。

```csharp
builder.Property(s => s.TrainerId);

builder.OwnsMany<Reservation>("_reservations", rb =>
{
    rb.ToTable("SessionReservations");

    rb.HasKey(r => r.Id);

    rb.Property(r => r.Id)
        .ValueGeneratedNever();

    rb.WithOwner().HasForeignKey("SessionId");

    rb.Property(r => r.ParticipantId);
});

builder.OwnsMany(s => s.Categories, cb =>
{
    cb.ToTable("SessionCategories");

    cb.WithOwner().HasForeignKey("SessionId");
    cb.HasKey("Id");

    cb.Property(c => c.Name);
    cb.Property(c => c.Value);
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/domain-object-deletion-54123405/?t=55)

一条关键的建模规则是:在数据库中,关系不得跨越聚合的边界。
例如,`Session` 聚合与 `Participant` 聚合之间没有数据库级关系。
如果删除一个 session 需要更新某个 participant,这些变更必须通过领域事件以最终一致的方式进行。

```text
  Session 聚合 (作为一个单元删除)
  +---------------------------------------------+
  |  Sessions                                   |
  |    |-- SessionReservations  (SessionId)     |
  |    `-- SessionCategories    (SessionId)     |
  +---------------------------------------------+
            |
            |  无数据库级关系; 通过领域事件实现最终一致性
            v
  Participant 聚合
```

这种隔离对重构至关重要。
如果需要把某个聚合从一个限界上下文迁移到另一个,没有数据库级依赖使得这种迁移成为可能。
在这种情况下,内部领域事件会转变为集成事件,通过消息总线促进不同应用或服务之间的通信。

## 4. One Aggregate Per Transaction

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/one-aggregate-per-transaction-54123406/) · 3:21

### 总结

本课探讨了领域驱动设计的一项基本原则:把事务限制在单个聚合边界内,以确保系统的可扩展性和性能。
通过偏好更小的聚合,并利用领域事件处理跨聚合的副作用,开发者可以实现最终一致性,同时避免长时间运行、阻塞式事务和高争用竞态条件带来的陷阱。
本课还演示了一种务实的中间件方法,用于在 Web 请求生命周期内管理这些事务和副作用。

### 核心概念

- **聚合一致性边界**:聚合是一致性的单元;它们作为一个整体被读取和写入。
- **小聚合**:更小的聚合可以减少数据传输开销,并降低并发更新时发生竞态条件的可能性。
- **一个事务一个聚合**:一条主要规则,规定单个事务只应修改一个聚合实例。
- **最终一致性**:跨越多个聚合的副作用应通过领域事件异步处理。
- **务实的事务管理**:使用中间件把副作用包裹在一个安全事务中,以确保最终一致性更新的原子性。

### 课程笔记

有效的聚合设计由两条主要规则指导:保持一个事务一个聚合,以及偏好更小的聚合。
偏好更小的聚合,是因为它们减少了网络上传输的数据量。
由于聚合是一个一致性边界,每次变更都需要读取整个聚合、应用变更,然后把整个聚合写回数据库。
大型聚合会增加数据传输成本,也会提高竞态条件的概率,即两个同时到达的请求试图修改同一个聚合,从而导致请求失败。

遵守"一个事务一个聚合"规则,就需要对初始聚合边界之外的副作用使用最终一致性。
例如,删除一个 room 涉及若干副作用:删除该 room 内的所有 session,并把这些 session 从各个 participant 的日程中移除。
如果这些操作在单个阻塞式事务中执行,用户就必须等待整个过程完成。
这会增加客户端超时的风险,并使错误处理变得复杂。

通过使用领域事件,这些副作用可以以最终一致的方式处理。
这使得系统能够立即响应用户,同时在后台以具备弹性的重试机制处理副作用。

在实践中,可以采用一种务实的方法:使用中间件来管理这些副作用的事务生命周期。
下面的中间件实现演示了如何把领域事件发布和随后的聚合更新包裹在一个事务中,该事务在初始请求逻辑之后执行。

```csharp
public async Task InvokeAsync(HttpContext context, IPublisher publisher, DomeGymDbContext dbContext)
{
    var transaction = await dbContext.Database.BeginTransactionAsync();
    context.Response.OnCompleted(async () =>
    {
        try
        {
            if (context.Items.TryGetValue(DomainEventsKey, out var value) && value is Queue<IDomainEvent> domainEvents)
            {
                while (domainEvents.TryDequeue(out var nextEvent))
                {
                    await publisher.Publish(nextEvent);
                }
            }

            await transaction.CommitAsync();
        }
        catch (EventualConsistencyException e)
        {
            // Handle eventual consistency exceptions
        }
        finally
        {
            await transaction.DisposeAsync();
        }
    });
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/one-aggregate-per-transaction-54123406/?t=145)

这种实现确保了即使副作用是通过最终一致性处理的,它们仍然被包裹在一个安全机制中。
如果在处理副作用期间发生未处理的异常,事务可以被回滚,从而确保系统不会落入部分更新的状态。
这种方法在提供安全网的同时,仍然让用户能够得到及时的响应。

## 5. Reference Between Domain Models

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/reference-between-domain-models-54123407/) · 3:43

### 总结

本课概述了在领域驱动设计中建立领域模型之间关系的基本规则,强调聚合只应通过 ID 引用其他聚合,而不是使用直接的对象引用,以维持隔离和边界。
本课还确立了这样一点:外部聚合绝不能引用另一个聚合内部的嵌套实体;相反,它们必须只通过聚合根进行交互,或者引用聚合本身,从而确保实体 ID 只在其父聚合内有效,并通过最终一致性来管理系统的一致性。

### 核心概念

- **聚合只通过 ID 引用**:聚合不应持有对其他聚合根的直接对象引用。
- **强类型 ID**:使用值对象来封装作为 ID 的 GUID,可以提供更好的类型安全和领域清晰度。
- **不引用嵌套实体**:聚合内部的实体绝不应在该聚合外部被通过 ID 或对象引用。
- **通过聚合根交互**:聚合之间的所有交互都必须通过聚合根进行。
- **最终一致性**:跨聚合边界的变更通过最终一致性处理,而不是通过即时的事务一致性。

### 课程笔记

#### 通过 ID 引用聚合

在领域驱动设计中,聚合必须保持完全分离,以维持清晰的边界。
为此,聚合只通过唯一标识符(ID)引用其他聚合,而不是持有对另一个聚合对象的直接内存引用。
这可以防止紧耦合,并确保当某个聚合被删除或修改时,引用它的聚合不会以违反事务边界的方式受到直接影响。

在下面 `Session` 聚合的示例中,该模型使用 GUID 引用 `Room` 和 `Trainer` 聚合。

```csharp
public class Session : AggregateRoot
{
    private readonly List<Reservation> _reservations = new();
    private readonly List<SessionCategory> _categories = new();

    public int NumParticipants => _reservations.Count;
    public DateOnly Date { get; }
    public TimeRange Time { get; } = null!;
    public string Name { get; } = null!;
    public string Description { get; } = null!;
    public int MaxParticipants { get; }

    public Guid RoomId { get; }
    public Guid TrainerId { get; }

    public Session(
        string name,
        string description,
        int maxParticipants,
        Guid roomId,
        Guid trainerId,
        // ...
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/reference-between-domain-models-54123407/?t=25)

一种常见且推荐的模式是使用**强类型 ID**。
不使用原始的 `Guid`,而是创建一个封装该 GUID 的值对象(例如 `RoomId`)。
这确保了只有当两个 ID 类型相同且持有相同的值时,它们才被视为相等,从而防止在需要 `RoomId` 的地方意外传入 `TrainerId`。

#### 不引用嵌套实体

存在于聚合内部的实体(嵌套实体)绝不应从该聚合外部被引用。
例如,一个 `Session` 聚合可能包含一个 `Reservation` 实体列表。

```csharp
using DomeGym.Domain.Common;

namespace DomeGym.Domain.SessionAggregate;

public class Reservation : Entity
{
    public Guid ParticipantId { get; }

    public Reservation(Guid participantId, Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        ParticipantId = participantId;
    }

    private Reservation() { }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/reference-between-domain-models-54123407/?t=75)

有人可能会想在 `Participant` 聚合中存储一个 `ReservationIds` 列表,用来追踪某个 participant 加入了哪些 session。
然而,出于两个主要原因,不建议这样做:

1.  **ID 唯一性**:实体的 ID 只保证在其父聚合内唯一。
    在 `Participant` 聚合中存储 `ReservationId`,如果该 ID 不是系统范围内唯一的,就可能导致冲突或无效引用。
2.  **封装**:聚合之间只能通过聚合根进行交互。
    `Participant` 不应知道 `Session` 聚合内部 `Reservation` 实体的结构。

`Participant` 聚合不应引用嵌套的 `Reservation` 实体,而应通过 ID 引用 `Session` 聚合根。

```csharp
namespace DomeGym.Domain.ParticipantAggregate;

public class Participant : AggregateRoot
{
    private readonly List<Guid> _sessionIds = new();
    public Guid UserId { get; }

    // ...
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/reference-between-domain-models-54123407/?t=130)

```text
  Participant                     Session (Aggregate Root)
  +----------------+              +---------------------------+
  | _sessionIds    |---- ID ----->| RoomId, TrainerId         |
  | UserId         |              | _reservations             |
  +----------------+              |   +-- Reservation         |
          |                       |         ParticipantId     |
          |                       +---------------------------+
          |                                     ^
          +----------- X 不引用 ReservationId ---+
```

要查找某个特定的 reservation,系统应获取 `Session` 聚合,并使用聚合根上的方法来查询其内部状态。
例如,`Session` 聚合可以提供一个方法,检查某个特定 participant 是否有预订。

```csharp
public bool HasReservationForParticipant(Guid participantId)
{
    return _reservations.Any(reservation => reservation.ParticipantId == participantId);
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/reference-between-domain-models-54123407/?t=175)

如果一个嵌套实体需要被多个聚合直接引用,这是一个强烈的信号,表明该实体应被提升为独立的聚合根。

## 6. Section Recap

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/section-recap-54123408/) · 0:55

### 总结

本课总结了对关键领域建模规则的探讨,强调了聚合之间的关系与行为,包括它们如何相互引用以及如何在存储中被管理。
它强调领域建模是一个随业务一同演进的迭代过程,需要在充分分析与基于反馈进行实现和完善的实际需要之间取得平衡。

### 核心概念

* **聚合关系**:定义聚合如何交互以及如何相互引用。
* **持久化与生命周期**:管理聚合如何被存储和删除。
* **迭代式建模**:认识到领域层并非静态,会随着业务需求的演变而变化。
* **避免分析瘫痪**:把推进置于完美之上,以便进行基于反馈的改进。

### 课程笔记

关键领域建模规则聚焦于领域模型中聚合的结构与行为方面。
这包括定义清晰的边界、确立聚合之间如何相互引用,以及确定它们的持久化和删除机制。

领域建模不是在项目开始时一次性完成的任务。
相反,它是一个贯穿应用整个生命周期的持续过程。
随着新的业务场景和规则出现,领域模型必须做出调整以反映这些变化。
系统是一个有生命的实体,会随着业务规则在应用中的体现而成长。

领域驱动设计中的一个常见挑战是陷入分析瘫痪,即试图创建完美模型反而阻碍了实际实现。
通常更有效的做法是先实现一个可用的模型,再随着更多信息和反馈的出现对其加以完善。
这种迭代方法确保系统始终与不断演变的实际业务需求保持一致。
