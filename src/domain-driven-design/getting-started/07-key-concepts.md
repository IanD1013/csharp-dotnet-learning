# Key Concepts

> 课程:[Getting Started: Domain-Driven Design](https://dometrain.com/course/getting-started-domain-driven-design-ddd/) · 第 7 章
> 共 6 课 · 约 19:15
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Application Services vs Domain Services](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/application-services-vs-domain-services-54123470/) | 5:11 | [↓](#1-application-services-vs-domain-services) |
| 2 | [Transactional vs. Eventual Consistency](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/transactional-vs-eventual-consistency-54123471/) | 5:08 | [↓](#2-transactional-vs-eventual-consistency) |
| 3 | [Rich vs. Anemic Domain Models](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/rich-vs-anemic-domain-models-54123472/) | 3:57 | [↓](#3-rich-vs-anemic-domain-models) |
| 4 | [Always Valid vs. Not Always Valid](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/always-valid-vs-not-always-valid-54123473/) | 2:32 | [↓](#4-always-valid-vs-not-always-valid) |
| 5 | [Persistence Ignorance](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/persistence-ignorance-54123474/) | 1:08 | [↓](#5-persistence-ignorance) |
| 6 | [Section Recap](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/section-recap-54123475/) | 1:19 | [↓](#6-section-recap) |

## 1. Application Services vs Domain Services

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/application-services-vs-domain-services-54123470/) · 5:11

### 总结

在领域驱动设计中,应用服务和领域服务扮演着截然不同的角色:应用服务充当编排者,通过与仓储和领域对象交互来管理用例的流程,而领域服务则封装那些无法自然地放进单个实体或值对象中的复杂业务逻辑和决策过程。
理解这两者之间的边界,对于保持技术编排与核心业务规则之间的清晰分离至关重要,它确保领域层始终是业务决策的唯一权威。

### 核心概念

* **应用服务(Application Services)**:负责编排领域对象(聚合、实体、值对象)以及仓储、工厂等战术模式。
* **用例(Use Cases)**:应用服务代表用户可以执行的具体操作,例如创建、获取或列出资源。
* **领域服务(Domain Services)**:封装涉及多个领域对象的领域逻辑,或者不属于某个特定实体的逻辑。
* **业务决策(Business Decisions)**:领域逻辑由业务决策和规则定义,这些决策和规则常常受到产品需求或不断变化的业务状态的影响。
* **编排与决策(Orchestration vs. Decision Making)**:应用服务编排决策的执行,而领域服务(连同实体和值对象)做出这些决策。

### 课程笔记

#### 作为编排者的应用服务

应用服务负责领域对象的编排。
这包括管理聚合、实体、值对象以及仓储和工厂等战术模式之间的交互。
它们本身不包含业务逻辑;相反,它们协调必要的组件来满足某个具体需求。

在 Clean Architecture 的语境中,应用服务代表应用程序的"用例"。
这些是应用程序允许的操作,例如创建一个健身房、列出健身房,或根据标识符获取某个特定的健身房。
应用服务处理技术流程:它可能调用仓储来获取一个实体,验证它是否存在,然后把结果返回给调用方。

```csharp
public async Task<ErrorOr<Gym>> Handle(GetGymQuery query, CancellationToken cancellationToken)
{
    var gym = await _gymRepository.GetByIdAsync(query.GymId);

    if (gym is null)
    {
        return Error.NotFound(description: "Gym not found");
    }

    return gym;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/application-services-vs-domain-services-54123470/?t=40)

#### 领域服务与业务逻辑

当领域逻辑无法自然地放在某个特定的领域对象中时,就会使用领域服务。
主要的区别在于,领域逻辑关注的是业务决策。
虽然实体和值对象也参与决策,但领域服务为那些跨越多个对象、或涉及领域必须授权的复杂业务规则的逻辑提供了一个归宿。

作为一条指导原则,领域逻辑就是任何与业务决策过程相关的东西。
如果某段逻辑决定了产品体验(例如在某个特定操作被阻止之前允许用户失败多少次),那它就是领域逻辑。
这些规则通常由产品经理定义,并且依赖于系统的状态或特定的业务常量。

#### 区分两者

区分这两者的一个实用经验法则是:问一问这段逻辑是在决定用户体验还是业务规则。
如果这段逻辑涉及一个可能根据业务需求而变化的"魔法数字"或阈值(例如库存上限或预订上限),那它就属于领域层,可能放在一个领域服务中。

设想一个预订服务,它根据已经有多少参与者没空,来决定一组参与者能否预订某个课时。
"在拒绝整组之前最多允许三次不成功的预订"这一决定是一个业务决策,因此它是领域逻辑。

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
        {
            return ReservationServiceErrors.CannotScheduleTooManyUnavailableParticipants;
        }

        participants.ForEach(participant =>
            participant.AddToScheduleOrBacklog(session));

        return Result.Success;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/application-services-vs-domain-services-54123470/?t=220)

在这个示例中,`ReservationService` 根据业务规则做出决策,而应用服务则负责从各自的仓储中获取 `Session` 和 `Participants`,然后调用这个领域服务。

```text
+---------------------+   fetch Session / Participants   +--------------+
| Application Service |--------------------------------->| Repositories |
|   (orchestration)   |                                  +--------------+
+---------------------+
           |
           | Reserve(session, participants)
           v
+---------------------+
| ReservationService  |  MaxUnsuccessfulReservations = 3
|  (business decision)|
+---------------------+
```

## 2. Transactional vs. Eventual Consistency

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/transactional-vs-eventual-consistency-54123471/) · 5:08

### 总结

Transactional vs. Eventual Consistency 关注的是 DDD 中在原子操作与分布式状态同步之间的战略选择。
事务一致性保证一组变更作为一个单元整体成功或整体失败,它实现起来更简单,但如果应用范围过广,就会降低性能。
相比之下,最终一致性把流程分解为更小的异步步骤,以暂时的状态不一致为代价,换取更高的吞吐量和更有韧性的错误处理。
通过遵循"在聚合边界内使用事务一致性、在边界外使用最终一致性"的原则,开发者可以在维护业务不变量的同时,确保系统响应迅速且可扩展。

### 核心概念

- **事务一致性(Transactional Consistency)**:一种"全有或全无"的方式,流程中的所有操作都必须成功,事务才能提交。
- **最终一致性(Eventual Consistency)**:一种模型,其中系统状态可能暂时不一致,但最终会在所有组件之间达到一致状态。
- **聚合边界(Aggregate Boundaries)**:事务一致性的主要单元;聚合内部的业务决策和状态变更是原子的。
- **性能权衡(Performance Trade-offs)**:跨多个聚合的事务一致性迫使用户等待所有变更完成,而最终一致性允许立即响应。
- **韧性(Resilience)**:最终一致性使健壮的错误处理(例如重试和后台处理)成为可能,而不会有客户端超时的风险。

### 课程笔记

#### 事务一致性

事务一致性遵循一种原子方式,其中处理作为一个单元进行。
如果操作的任何部分失败,整个事务就会失败。
虽然这种模型由于活动部件较少而更容易管理,但如果跨多个聚合应用,它可能导致严重的性能问题。
要求所有变更都在单个事务中发生,会迫使用户在整个过程中保持在线,等待每一项更新生效后才能收到响应。

#### 最终一致性

最终一致性把处理拆分成更小的、独立的块。
在系统达到一致状态的过程中,它可能暂时处于不一致的状态。
例如,如果从 Gym 聚合中删除了一个房间,这个房间会被立即移除。
然而,在用户收到该房间内的课时已被取消的通知之前,或者在他们的日程被更新之前,可能会有一段延迟。

尽管存在这种暂时的不一致,最终一致性仍提供了几个优势:
- **改善用户体验**:用户会立即收到响应(例如"Room deleted"),而后台流程负责处理次要的更新。
- **健壮性与韧性**:由于客户端并不在等待响应,系统可以实现复杂的错误处理机制,例如指数退避重试或异步通知(比如电子邮件),而不会有请求超时的风险。

#### 边界规则

领域驱动设计中一条由 Vaughn Vernon 推广的基本经验法则是:在边界之外使用最终一致性。
在这里,"边界"指的是聚合(Aggregate)。

DDD 的主要动机之一,是能够把一个复杂的领域分解为小的、原子的单元。
这些聚合封装了维护业务不变量所需的数据和逻辑,而业务不变量就是必须始终保持为真的核心规则。
当一个聚合做出业务决策时,它会转换自己的状态(例如从 X 到 Y),并把这次变更作为一个单一的、事务一致的单元持久化。
该决策对系统其他部分造成的任何后果(例如更新其他聚合或通知外部服务)都应该通过最终一致性来传播。
这种方式确保业务决策在一个受保护的边界内被有把握地做出,同时维持整个系统的性能和可扩展性。

```text
+------------- Aggregate boundary --------------+
|  business decision: state X -> Y              |
|  persisted as one transactionally consistent  |
|  unit                                         |
+-----------------------------------------------+
                       |
                       | eventual consistency
                       v
     other aggregates / external services
```

## 3. Rich vs. Anemic Domain Models

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/rich-vs-anemic-domain-models-54123472/) · 3:57

### 总结

领域驱动设计根据逻辑所在的位置以及状态的管理方式,区分贫血领域模型和充血领域模型。
贫血模型充当带有公共属性的简单数据容器,常常导致领域逻辑泄漏到应用服务中。
相比之下,充血领域模型封装了数据和行为,通过暴露特定的交互方法并保护内部字段,来确保对象保持在有效状态。

### 核心概念

- **贫血领域模型(Anemic Domain Model)**:一种模型,其中对象主要是带有公共 getter 和 setter 的数据结构,逻辑在外部处理。
- **充血领域模型(Rich Domain Model)**:一种同时封装数据和行为的模型,确保内部状态一致,以及对象"始终有效"。
- **封装(Encapsulation)**:隐藏内部数据(使用私有字段),只通过方法暴露必要操作的做法。
- **基本类型偏执(Primitive Obsession)**:用基本类型(如 `double` 或 `string`)来表示复杂的领域概念,而不是使用专门的值对象。
- **逻辑放置(Logic Placement)**:把领域特定规则从应用服务移入领域模型本身的原则。

### 课程笔记

贫血领域模型和充血领域模型都是设计领域层的面向对象方法。
贫血领域模型的特征是,其对象本质上是属性包。
在这种方式下,属性通常带有公共的 getter 和 setter,允许任何外部组件直接修改对象的状态。

```csharp
public class Session
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Participant> Participants { get; set; }
}

public void AddParticipantCommandHandler(
    Session session,
    Participant participant)
{
    session.Participants.Add(participant);
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/rich-vs-anemic-domain-models-54123472/?t=25)

在上面的示例中,`AddParticipantCommandHandler` 包含了添加参与者的逻辑,因为 `Session` 模型没有保护它的 `Participants` 列表。
这是有问题的,因为领域模型无法强制执行不变量,例如检查课时中是否还有足够的名额,或者某个特定参与者是否被允许加入。
当逻辑被放在模型之外时,使用方无法确定对象内部的数据是有效的。

充血领域模型通过隐藏底层数据、只暴露特定的交互方法来解决这些问题。
这确保了对象保护自己的状态并维护不变量。

```csharp
public class Session
{
    private readonly List<Participant> _participants = new();
    public string Name { get; }
    public string Description { get; }

    public void AddParticipant(Participant participant)
    {
        _participants.Add(participant);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/rich-vs-anemic-domain-models-54123472/?t=100)

在充血领域模型中,字段和属性默认应该是私有的(通常一开始就是 `private readonly`)。
它们只在必要时才被暴露。
通过使用 `AddParticipant` 这样的方法而不是公共 setter,模型可以在更新内部状态之前验证该操作。
这就得到了一个"始终有效"的模型,其中该对象的任何实例都保证处于一致的状态。

#### 识别充实模型的机会

表明一个模型应该被充实的两个常见信号是基本类型偏执和应用服务中的逻辑泄漏:

1. **基本类型偏执(Primitive Obsession)**:当使用基本类型来表示具有特定约束的领域概念时,就会出现这种情况。
例如,在整个系统中把一个位置表示为两个 `double` 值(纬度和经度)。
相反,这些值应该被封装到一个 `Location` 值对象中,由它在构造时验证坐标。
2. **应用服务逻辑(Application Service Logic)**:如果一个应用服务正在对某个领域对象执行复杂的操作或业务逻辑,那么这段逻辑很可能属于领域模型本身。
把这种行为移入模型会"充实"它,并防止整个系统中出现逻辑重复。

## 4. Always Valid vs. Not Always Valid

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/always-valid-vs-not-always-valid-54123473/) · 2:32

### 总结

本课探讨管理领域模型状态的两种不同方式:始终有效和非始终有效。
始终有效的领域模型确保对象从构造的那一刻起、以及在其属性被访问的任何时刻都处于有效状态,而非始终有效的模型允许对象存在于无效状态,要求使用方在使用之前检查一个显式的验证属性。
本课推荐把始终有效的方式作为经验法则,以防止意外访问无效数据,并建议使用工厂模式来处理对象创建过程中的错误累积。

### 核心概念

* **始终有效的领域模型(Always Valid Domain Model)**:一种模型,其中数据保证在任何时刻都处于有效状态。
* **非始终有效的领域模型(Not Always Valid Domain Model)**:一种允许对象处于无效状态的方式,通常要求在使用之前进行显式检查。
* **状态责任(State Responsibility)**:在始终有效的模型中,父对象负责维护其内部状态和属性的有效性。
* **验证模式(Validation Patterns)**:使用工厂(Factory)模式或静态工厂方法(Static Factory Method)模式,在构造过程中累积错误,而不实例化一个无效对象。
* **首选方式(Preferred Approach)**:始终有效的模型是领域驱动设计推荐的标准。

### 课程笔记

#### 始终有效的领域模型

始终有效的领域模型的设计方式是,在任何时刻,对象中的数据都处于有效状态。
这意味着,如果一个对象已经被成功构造,它就保证包含有效的值。
在访问公共属性时,使用方可以信任这些数据,因为父对象负责维护它们的有效性。
这种方式消除了意外处理一个包含不一致或非法数据的对象的风险。

#### 非始终有效的领域模型

相比之下,非始终有效的领域模型允许对象存在于无效状态。
这种方式有时用于构造过程中,以收集一份完整的验证错误列表。
在这种模式中,领域模型通常会暴露一个 `IsValid` 属性。
在使用对象之前,使用方必须检查这个属性,以确保状态是正确的。
如果对象无效,它可能会暴露一个验证错误集合,解释状态为什么不正确。

#### 推荐方式与实现

领域驱动设计中的首选方式是始终有效的模型。
通过确保对象始终有效,开发者可以避免访问处于不正确状态的属性这种错误。

为了处理需要在对象创建期间报告多个验证错误的场景,你不应该实例化一个无效对象。
相反,应该利用工厂模式或静态工厂方法。
这些模式让你可以在构造逻辑中累积所有验证错误,并在验证失败时把这些错误返回给调用方,而不是返回领域对象的一个无效实例。
作为一条经验法则,保持始终有效的状态是管理领域逻辑最健壮的方式。

## 5. Persistence Ignorance

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/persistence-ignorance-54123474/) · 1:08

### 总结

持久化无知(Persistence Ignorance)是 DDD 的一条核心原则,即领域模型的设计独立于数据的存储方式。
通过关注业务规则和术语,而不是数据库模式(无论是关系型的还是基于文档的),开发者可以创建一个更健壮的领域层。
仓储模式通过提供一种把数据访问当作内存集合来对待的抽象,促进了这一点,从而有效地把领域逻辑与底层基础设施解耦。

### 核心概念

* 基于业务规则和术语对领域进行建模。
* 把领域逻辑与特定的存储技术(例如 SQL 与 NoSQL)解耦。
* 尽量减少持久化变更对领域层的影响。
* 使用仓储模式来创建数据访问的内存抽象。

### 课程笔记

持久化无知是领域驱动设计(DDD)中的一个基本概念,它把 DDD 与其他架构方法区分开来。
其主要目标是在不考虑持久化的情况下对领域进行建模。
在建模时,关注点完全放在领域上:业务规则、决策、术语和对象。
在这个阶段,开发者应该避免去考虑数据最终是存放在关系型数据库还是文档数据库中。

虽然实现"纯粹"的持久化无知是有挑战的(因为更换持久化方案有时可能需要在领域内做一些小的改动),但正确的领域建模可以把这些依赖降到最低。
这种方式确保应用程序的核心逻辑不会被某种特定数据库技术的约束所左右。

仓储模式是促进持久化无知的一个关键工具。
它定义了一个 API,营造出在内存中操作领域对象的假象,把这些对象实际如何被持久化的细节抽象掉。
这让领域层可以与一个类集合的接口交互,而无需知道数据是存储在 SQL 数据库、NoSQL 存储还是外部 API 中。

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

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/persistence-ignorance-54123474/?t=55)

通过定义像 `ISessionsRepository` 这样的接口,领域层可以始终专注于从业务角度需要完成什么,而把数据存储的实现细节留给基础设施层。

## 6. Section Recap

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/section-recap-54123475/) · 1:19

### 总结

本课对领域驱动设计(DDD)中的五个基本概念做了全面回顾。
它区分了应用服务和领域服务,探讨了事务一致性和最终一致性模型,比较了充血领域模型和贫血领域模型,讨论了"始终有效"的状态原则,并解释了在对领域建模时持久化无知的重要性。

### 核心概念

- Application Services vs. Domain Services
- Transactional vs. Eventual Consistency
- Rich vs. Anemic Domain Models
- Always Valid vs. Not Always Valid
- Persistence Ignorance

### 课程笔记

#### 应用服务与领域服务

应用服务负责领域对象的编排。
它们充当一个协调工作流程但不包含业务逻辑的门面(facade)。
相比之下,领域服务负责业务决策,或直接参与业务决策过程。

#### 事务一致性与最终一致性

事务一致性确保处理作为一个单一的、原子的工作单元完成。
最终一致性则把处理拆分成更小的步骤,这些步骤随着时间依次完成,确保系统最终达到一致的状态。

#### 充血领域模型与贫血领域模型

它们代表了设计领域对象的两种不同的面向对象方法。
贫血领域模型本质上是一个数据结构,其所有属性都通过公共的 getter 和 setter 暴露,允许外部组件操纵其状态。
充血领域模型隐藏其内部数据,通过方法暴露特定的行为,并严格控制其数据被修改的方式。

#### 始终有效与非始终有效

"始终有效"的方式确保领域模型永远不会进入无效状态;任何会导致无效状态的操作都会被阻止。
"非始终有效"的方式允许领域模型在其生命周期中的某些时刻处于无效状态。

#### 持久化无知

持久化无知是在不考虑对象将如何存储到数据库中的情况下对领域进行建模的做法。
这让关注点可以完全放在业务逻辑和领域需求上,而不是技术实现细节上。
