# Tactical Phase

> 课程:[Deep Dive: Domain-Driven Design](https://dometrain.com/course/deep-dive-domain-driven-design-ddd/) · 第 8 章
> 共 4 课 · 约 13:14
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Refresher - What is the Tactical Phase](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/refresher-what-is-the-tactical-phase-54855866/) | 1:35 | [↓](#1-refresher---what-is-the-tactical-phase) |
| 2 | [Refresher - Tactical Patterns](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/refresher-tactical-patterns-54855867/) | 7:20 | [↓](#2-refresher---tactical-patterns) |
| 3 | [Tactical Phase Objectives](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/tactical-phase-objectives-54855868/) | 3:47 | [↓](#3-tactical-phase-objectives) |
| 4 | [Section Recap](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/section-recap-54855869/) | 0:32 | [↓](#4-section-recap) |

## 1. Refresher - What is the Tactical Phase

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/refresher-what-is-the-tactical-phase-54855866/) · 1:35

### 总结

领域驱动设计(DDD)的战术阶段专注于把战略阶段中做出的战略决策和获得的概念性洞见,转化为具体的、可实现的软件组件。
它利用一组战术模式(例如聚合、实体和值对象)作为构建块,在已定义的限界上下文内设计内部的领域模型。
这一阶段弥合了高层架构边界与业务逻辑实际实现之间的鸿沟。

### 核心概念

- 从概念性的战略洞见过渡到实际的软件实现。
- 在已确立的限界上下文内设计领域模型(聚合、实体和值对象)。
- 把战术模式用作领域层的基本构建块。
- 定义聚合的边界和职责,以确保数据的一致性和完整性。

### 课程笔记

战术阶段是领域驱动设计中把战略决策转换为具体实现的阶段。
战略阶段识别限界上下文并映射它们之间的关系,而战术阶段则专注于这些上下文的内部设计。

其目标是提供一种结构化的方法,从纯粹的概念性想法走向一组实际的、可实现的软件组件。
这包括识别有效建模业务领域所需的具体战术模式。

#### Objectives of the Tactical Phase(战术阶段的目标)

这一阶段的主要目标包括:

1. **精炼领域模型**:把从战略阶段获得的洞见应用到领域模型的设计中。
2. **聚合设计**:确定领域中哪些实体应当充当聚合,哪些不应当。这对于定义一致性边界和管理复杂的对象图至关重要。
3. **为实现做好准备**:创建一个能直接转换为可运行代码的设计,确保软件准确反映预期的业务逻辑。

#### Tactical Patterns as Building Blocks(作为构建块的战术模式)

战术模式充当领域的构建块。
通过使用这些模式(例如实体、值对象、服务和聚合),开发者可以创建一个遵循 DDD 原则、健壮且可维护的代码库。
这些模式通过为实现业务规则提供共同的语言和结构,来帮助管理复杂性。

## 2. Refresher - Tactical Patterns

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/refresher-tactical-patterns-54855867/) · 7:20

### 总结

本课全面回顾领域驱动设计的七个核心战术模式:实体、值对象、聚合、领域服务、工厂、仓储和领域事件。
它解释了这些构建块如何协同工作,以在限界上下文内保持一致性、封装业务逻辑并管理领域对象的生命周期。

### 核心概念

* **实体(Entities)**:由唯一标识而非其属性来定义的对象。
* **值对象(Value Objects)**:由其值来定义的对象;它们是不可变的,并且没有唯一标识。
* **聚合(Aggregates)**:为了数据变更而被当作单一单元对待的一组领域对象,维护着一致性边界。
* **领域服务(Domain Services)**:封装那些并不自然地属于单个实体或值对象的业务逻辑的类。
* **工厂(Factories)**:用于创建复杂领域对象或聚合的机制。
* **仓储(Repositories)**:为访问和持久化聚合提供类似内存集合接口的抽象。
* **领域事件(Domain Events)**:领域内重要事件的记录,用于跨聚合边界触发副作用。

### 课程笔记

#### Entities(实体)

实体是由其唯一标识符(ID)来定义的领域对象。
比较两个实体时,只有 ID 才重要。
这让对象的状态可以随时间变化,而它在概念上仍是同一个对象。
例如,即使 `Participant` 的 `Age` 属性被更新,它仍然是同一个人,因为其 `Id` 是恒定的。

```csharp
class Participant
{
    public Guid Id { get; }
    public int Age { get; private set; }

    public void IncrementAge()
    {
        Age++;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/refresher-tactical-patterns-54855867/?t=10)

#### Value Objects(值对象)

值对象由其值而不是 ID 来标识。
如果两个值对象的属性完全相同,它们就被视为相等。
常见的例子包括位置(纬度和经度)、金额、地址和时间范围。
它们通常是不可变的;要改变一个值,就替换整个对象。

```csharp
public class Location
{
    public double Latitude { get; }
    public double Longitude { get; }

    public Location(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/refresher-tactical-patterns-54855867/?t=70)

#### Aggregates(聚合)

聚合是由一个或多个领域模型组成的集合,这些模型作为一个整体必须保持一致。
它充当一个状态机,确保从一个状态到另一个状态的转换在所有内部对象之间都是有效的。
对聚合内数据的所有操作都必须通过聚合根(主实体)进行。
这创建了一个一致性边界,保证数据在一次事务之后始终处于有效状态。

```csharp
public class Room
{
    public Location Location { get; }
    public List<Session> Sessions { get; }

    public void ScheduleSession(Session session) 
    {
        // Logic to ensure consistency when adding a session
    }
}
```

[▶ 观看](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/refresher-tactical-patterns-54855867/?t=115)

```text
            ScheduleSession(session)
                     |
                     v
+--------------------------------------------+
| Room (Aggregate Root)                      |
|                                            |
|   Location : Location                      |
|   Sessions : List<Session>                 |
+--------------------------------------------+
        一致性边界:所有操作都经过聚合根
```

#### Domain Services(领域服务)

领域逻辑理想情况下应当位于实体或值对象之中。
然而,当逻辑涉及多个对象,或代表一个并不自然地适合放进单个模型的业务决策时,就会使用领域服务。
它们是执行业务关键操作的无状态类。

```csharp
public class ReservationService
{
    public ErrorOr<Success> Reserve(
        Session session,
        List<Participant> participants)
    {
        const int MaxUnsuccessfulReservations = 3;

        var unavailableParticipantCount = participants.Count(
            participant => !participant.IsTimeSlotFree(session.Date, session.Time));

        if (unavailableParticipantCount > MaxUnsuccessfulReservations)
        {            return ReservationServiceErrors.CannotScheduleTooManyUnavailableParticipants;
        }

        participants.ForEach(participant =>
            participant.AddToScheduleOrBacklog(session));

        return Result.Success;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/refresher-tactical-patterns-54855867/?t=235)

#### Factories(工厂)

工厂负责创建复杂的领域对象。
它既可以实现为一个独立的工厂类,也可以实现为领域对象自身上的静态工厂方法。
这一模式封装了实例化逻辑,确保对象在创建时处于有效状态。

```csharp
public class SessionFactory
{
    public static Session Create(
        string name,
        string description,
        Guid? id = null)
    {
        return new Session(id ?? Guid.NewGuid(), name, description);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/refresher-tactical-patterns-54855867/?t=280)

#### Repositories(仓储)

仓储在数据访问层之上提供了一层抽象。
从领域逻辑的角度看,仓储的行为就像一个聚合的内存集合。
这让领域与具体的持久化技术(例如 SQL、NoSQL 或内存存储)解耦。

```csharp
public interface ISessionsRepository
{
    Task AddSessionAsync(Session session);
    Task<Session?> GetByIdAsync(Guid id);
    Task<List<Session>> ListByIdsAsync(IReadOnlyList<Guid> sessionIds, DateTime? startDateTime = null);
    Task<List<Session>> ListByGymIdAsync(Guid gymId, DateTime? startDateTime = null);
    Task UpdateAsync(Session session);
    Task<List<Session>> ListByRoomIdAsync(Guid roomId);
    Task RemoveRangeAsync(List<Session> sessions);
}
```

[▶ 观看](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/refresher-tactical-patterns-54855867/?t=310)

#### Domain Events(领域事件)

领域事件是领域内过去发生的某件重要的事。
在实践中,它们被用来解耦限界上下文的不同部分。
当某个聚合发生变更时,它会引发一个领域事件。
随后一个处理程序监听该事件,对其他聚合执行必要的更新,从而跨聚合边界维持最终一致性。

```csharp
public ErrorOr<Success> ReserveSpot(Participant participant)
{
    if (_reservations.Count >= MaxParticipants)
    {
        return SessionErrors.CannotHaveMoreReservationsThanParticipants;
    }

    if (_reservations.Any(reservation => reservation.ParticipantId == participant.Id))
    {
        return Error.Conflict(
            description: "Participants cannot reserve twice to the same session");
    }

    var reservation = new Reservation(participant.Id);
    _reservations.Add(reservation);

    _domainEvents.Add(new SessionSpotReservedEvent(this, reservation));

    return Result.Success;
}
```

[▶ 观看](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/refresher-tactical-patterns-54855867/?t=370)

```text
+-------------+   SessionSpotReservedEvent   +---------+      +----------+
| Session     |----------------------------->| handler |----->| 其他聚合 |
| ReserveSpot |                              +---------+      | (更新)   |
+-------------+                                               +----------+
          跨聚合边界维持最终一致性
```

## 3. Tactical Phase Objectives

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/tactical-phase-objectives-54855868/) · 3:47

### 总结

领域驱动设计的战术阶段弥合了高层战略洞见与具体软件实现之间的鸿沟。
它的主要目标包括:精炼领域模型以准确反映业务规则、在限界上下文内设计聚合,以及确立低层不变量。
这一阶段的特点是一个迭代的反馈循环,实现细节常常会促使通用语言得到精炼,并促使对战略阶段最初定义的限界上下文边界进行调整。

### 核心概念

- **精炼领域模型**:开发一个准确表示业务规则和流程的模型。
- **聚合设计**:设计位于每个限界上下文中的各种聚合。
- **精炼限界上下文**:基于实现中的发现来调整初步的战略边界。
- **低层不变量**:在领域对象内映射并强制执行具体的业务规则。
- **特定于上下文的通用语言**:把通用的领域语言精炼为每个限界上下文中的具体术语。
- **战略-战术反馈循环**:利用实现中的洞见来更新战略决策以及与利益相关者的对齐。

### 课程笔记

#### Objectives of the Tactical Phase(战术阶段的目标)

战术阶段专注于把战略洞见转化为实际的、可实现的软件组件。
这一转变包括应用战术模式来创建一个易于转换为实际代码的设计。
主要目标包括:

1.  **精炼领域模型**:目标是开发一个能准确表示领域中所涉及的业务规则和流程的模型。
2.  **设计聚合**:这包括对每个限界上下文内的聚合进行详细设计。
3.  **确立限界上下文**:虽然初步的限界上下文是在战略阶段定义的,但战术阶段需要"亲自动手"。随着聚合被设计出来,可能会发现某些战略决策已不再合理。这会促使对限界上下文进行精炼,并与利益相关者重新对齐。
4.  **确立低层不变量**:先前识别出的一般领域不变量,会被映射到具体的位置,在那里它们将作为低层不变量在代码中被强制执行。
5.  **精炼通用语言**:通用语言是特定于上下文的。在战术阶段,语言的通用草稿会被精炼为针对每个具体限界上下文的精确版本。

#### The Process of Domain Modeling(领域建模的过程)

领域建模是把领域建模为具体领域对象的过程。
它涵盖聚合设计、领域发现,以及把领域划分为限界上下文。
从本质上说,它就是把抽象的领域概念转换为具体的领域对象。

虽然领域建模始于战略阶段,通过流程建模和上下文映射等活动开展,但这些活动提供的是一种直觉性的、概念性的理解。
战术阶段才是进行详细建模和实现的地方。

由于战术阶段是亲自动手的,它常常会揭示出在战略阶段并不明显的新阻碍或新洞见。
这就需要一个迭代的过程:随着实现的推进,开发者可能需要回到战略阶段去更新限界上下文、与利益相关者对齐,并开展进一步的研究。
这种来回往复会一直持续,直到领域模型完全确立并得到精炼。

```text
+------------------+                          +------------------+
| 战略阶段         | ---- 战略洞见 ---------> | 战术阶段         |
| 流程建模         |                          | 聚合设计         |
| 上下文映射       | <--- 新阻碍 / 新洞见 --- | 详细建模与实现   |
+------------------+                          +------------------+
   更新限界上下文、与利益相关者对齐、进一步研究
        (来回往复,直到领域模型完全确立并得到精炼)
```

## 4. Section Recap

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/section-recap-54855869/) · 0:32

### 总结

本课为领域驱动设计战术阶段的介绍作结,把它定位为从高层战略规划到具体实现的过渡。
它强调把战术模式用作基本构建块,基于先前确立的战略洞见来精炼并实现领域模型。

### 核心概念

- 作为领域构建块的战术模式。
- 领域模型的精炼。
- 把战略洞见转化为实现。
- 为聚合设计做准备。

### 课程笔记

领域驱动设计的战术阶段专注于有效建模领域所需的具体模式和构建块。
战略阶段确立高层的边界和关系,而战术阶段则是把这些洞见转化为可运行实现的地方。

这一阶段的首要目标是持续精炼领域模型。
通过运用在战略映射期间获得的知识和做出的决策,开发者可以应用战术模式来创建业务逻辑的一个健壮表示。
这一过程让项目从抽象概念走向亲自动手的实现,为聚合设计等更详细的模式做好铺垫。
