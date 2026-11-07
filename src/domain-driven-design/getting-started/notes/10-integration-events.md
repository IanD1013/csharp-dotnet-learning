# Integration Events

> 课程:[Getting Started: Domain-Driven Design](https://dometrain.com/course/getting-started-domain-driven-design-ddd/) · 第 10 章
> 共 9 课 · 约 32:13
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [What are Integration Events?](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/what-are-integration-events-54123519/) | 2:46 | [↓](#1-what-are-integration-events) |
| 2 | [The chain of Integration and Domain Events](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-chain-of-integration-and-domain-events-54123520/) | 4:21 | [↓](#2-the-chain-of-integration-and-domain-events) |
| 3 | [Using a Message Broker for distributing Integration Events](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/using-a-message-broker-for-distributing-integration-events-54123521/) | 4:29 | [↓](#3-using-a-message-broker-for-distributing-integration-events) |
| 4 | [Error handling when publishing Integration Events](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/error-handling-when-publishing-integration-events-54123522/) | 3:56 | [↓](#4-error-handling-when-publishing-integration-events) |
| 5 | [The Outbox Pattern](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-outbox-pattern-54123523/) | 4:25 | [↓](#5-the-outbox-pattern) |
| 6 | [The Integration Events Sausage Factory - Outbox Writer](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-integration-events-sausage-factory-outbox-writer-54123524/) | 4:18 | [↓](#6-the-integration-events-sausage-factory---outbox-writer) |
| 7 | [The Integration Events Sausage Factory - Integration Event Publisher](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-integration-events-sausage-factory-integration-event-publisher-54123525/) | 1:56 | [↓](#7-the-integration-events-sausage-factory---integration-event-publisher) |
| 8 | [The Integration Events Sausage Factory - Consuming Integration Events](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-integration-events-sausage-factory-consuming-integration-events-54123526/) | 5:17 | [↓](#8-the-integration-events-sausage-factory---consuming-integration-events) |
| 9 | [Section Recap](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/section-recap-54123527/) | 0:45 | [↓](#9-section-recap) |

## 1. What are Integration Events?

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/what-are-integration-events-54123519/) · 2:46

### 总结

集成事件(integration event)是跨越限界上下文边界、具有业务意义的事件,用于促成整个系统范围内的最终一致性。
领域事件管理的是单个上下文内聚合之间的一致性,而集成事件让彼此独立的限界上下文能够对变化做出反应并同步状态。

### 核心概念

- **定义**:以过去时命名、跨越限界上下文边界的业务事件。
- **范围**:集成事件用于不同限界上下文之间的通信。
- **最终一致性**:它们是在整个系统层面实现最终一致性的机制。
- **与领域事件的比较**:领域事件位于限界上下文内部;集成事件位于外部。

### 课程笔记

在领域驱动设计中,领域事件表示系统内发生的、从业务角度看值得关注的事情,通常以过去时命名。
领域事件用于在单个限界上下文内的聚合之间实现最终一致性,而集成事件的作用与之类似,只是范围更广。

当一个事件由某个聚合触发时,它首先由同一限界上下文内的各个处理器处理,以更新其他聚合。
这维护了该特定边界内的一致性。
然而,如果该事件与系统的其他部分相关,也就是说它需要跨越限界上下文的边界,它就被归类为集成事件。

设想有两个限界上下文:Context A 和 Context B。
如果 Context A 中的某个操作导致了 Context B 需要做出反应的变化,这一通信就通过集成事件来处理。
这让整个系统能够在不同限界上下文基于共享事件应用变化的过程中达到最终一致性。

两类事件的区别可以按其范围概括如下:

```csharp
eventType = @event.Scope switch
{
    Scope.InsideBoundedContext => "Domain Event",
    _ => "Integration Event"
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/what-are-integration-events-54123519/?t=145)

归根结底,事件类型的选择取决于边界。
如果事件的范围局限在限界上下文之内,它就是领域事件。
如果事件的范围跨越了该边界,它就成为集成事件,促成整个分布式系统的最终一致性。

## 2. The chain of Integration and Domain Events

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-chain-of-integration-and-domain-events-54123520/) · 4:21

### 总结

本课探讨集成事件与领域事件在多个限界上下文之间的级联关系。
它演示了集成事件如何促成跨上下文通信,而领域事件如何管理单个上下文内的副作用。
通过把这些事件串联起来,开发者可以用更小、可复用且经过充分测试的领域操作组合出复杂的系统行为,例如删除健身房或订阅这样的多步骤过程。

### 核心概念

- **跨上下文传播**:集成事件在限界上下文之间架起桥梁(例如从 User Management 到 Gym Management)。
- **事件链**:一个集成事件可以触发领域事件,而领域事件又可能触发更多集成事件,从而形成一条响应式的链。
- **逻辑组合**:复杂操作通过层层叠加事件构建而成;例如,删除一个房间会自动触发课程场次的取消,因为这一逻辑已经封装在场次的领域事件中。
- **封装**:使用事件让每个限界上下文能够维护自己的业务规则,同时对系统其他地方的变化做出反应。

### 课程笔记

限界上下文之间的交互通常始于一个用户操作。
例如,当用户创建一个管理员档案时,他们与 User Management 上下文交互,用户聚合就位于这个上下文中。
该聚合会强制执行业务规则,例如确保用户尚未拥有管理员档案。
校验通过后,系统会引发一个 `AdminProfileCreatedIntegrationEvent`。
这个事件由 Gym Management 上下文消费,随后该上下文会在自己的数据库中创建该管理员档案的本地表示。
类似的模式也适用于教练和参与者,User Management 中的变化会通过集成事件传播到 Session Reservation 上下文。

集成事件可以触发一条内部领域事件链。
当一个集成事件在某个限界上下文(比如 Session Reservation)中被处理时,它可能会修改某个聚合。
这一修改可能引发一个领域事件,把变化传播到同一上下文内的其他聚合。
这些次级变化又可能触发一个新的集成事件,把状态变化告知另一个上下文(比如 Gym Management)。
这就形成了一种流程:一个上下文中的变化通过集成事件和领域事件的组合在整个系统中层层扩散。

这种事件驱动的方法让复杂的系统行为可以由简单、离散的操作组合而成。
来看在 Session Reservation 上下文中取消一个课程场次的过程:
1. 在 session 聚合上调用 `Cancel` 方法。
2. 引发一个 `SessionCanceled` 领域事件并在内部存储。
3. 事件处理器做出反应,更新参与者和教练双方的日程。

由于这一逻辑已被封装,更高层的操作实现起来就更简单了。
如果在 Gym Management 上下文中删除了一个房间,它会引发一个 `RoomDeletedIntegrationEvent`。
Session Reservation 上下文中的处理器只需找出受影响的场次并调用它们的 `Cancel` 方法。
现有的领域事件基础设施会自动处理下游的日程更新。
这种组合还可以进一步延伸:删除健身房会触发房间删除,删除订阅会触发健身房删除。
层级中的每一步都利用了在更低层级已经定义并测试过的逻辑。

```text
删除订阅 --> 删除健身房 --> 删除房间
                               |
                               v
                    RoomDeletedIntegrationEvent
                               |
                               v
               Session Reservation: Cancel()
                               |
                               v
                     SessionCanceled 领域事件
                               |
                               v
                  更新参与者和教练的日程
```

## 3. Using a Message Broker for distributing Integration Events

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/using-a-message-broker-for-distributing-integration-events-54123521/) · 4:29

### 总结

集成事件通过消息代理(message broker)实现限界上下文之间的异步通信,确保系统某一部分的变化能传播到其他部分。
当一个操作发生时,首先会在本地上下文内引发一个领域事件;随后由一个专门的处理器把对应的集成事件发布到代理。
订阅的上下文接收到该事件后执行必要的本地更新(可能会触发更多内部领域事件),然后把消息标记为已消费,以维护系统范围的一致性。

### 核心概念

- **消息代理**:通过扇出(fan-out)模式把集成事件分发给多个订阅者的基础设施。
- **领域事件与集成事件**:领域事件位于限界上下文内部;集成事件为跨上下文通信而设计。
- **扇出分发**:单个集成事件会同时投递给所有感兴趣的限界上下文。
- **事件消费**:每个上下文负责在其内部处理完成后把消息标记为已消费。
- **级联更新**:在下游上下文中处理集成事件可能会触发内部领域事件和更多逻辑,以维护本地完整性。

### 课程笔记

集成事件的实现依赖消息代理来在系统的各个限界上下文之间同步状态。
这一过程确保当一个领域发生变化时,所有其他相关领域都能相应地做出反应。

#### 档案创建与事件传播

当用户与 **User Management** 限界上下文交互以创建一个档案(例如 Admin、Trainer 或 Participant)时,系统会先校验请求。
校验成功后,聚合会引发一个 **领域事件**,例如 `AdminProfileCreated`。
领域事件专门用于发起方限界上下文内部的状态变化和副作用。

为了把这一变化传播到系统的其他部分,一个处理器会捕获该领域事件,并把对应的 **集成事件** 发布到消息代理。
随后代理把这条消息扇出给所有已注册的订阅者,包括 **Gym Management** 和 **Session Reservation**。

```text
User Management
  aggregate --(AdminProfileCreated 领域事件)--> handler
                                                  |
                                       集成事件 publish
                                                  v
                                           Message Broker
                                            (fan-out)
                                            /        \
                                           v          v
                                Gym Management    Session Reservation
```

#### 事件消费与过滤

每个订阅的上下文会根据自身需求评估收到的集成事件:
1. **Session Reservation**:如果该事件(例如 `AdminProfileCreatedIntegrationEvent`)与其特定的领域逻辑无关,它会忽略其内容,但仍会把消息标记为已消费,以便从队列中清除。
2. **Gym Management**:这个上下文需要管理员档案数据来维护其本地记录。它处理该事件,创建对应的本地档案,然后把消息标记为已消费。

一旦每个订阅的限界上下文都成功处理了消息并更新了内部状态,系统就达到了最终一致性。

#### 级联逻辑:房间删除示例

集成事件经常会在下游上下文中触发涉及多个内部步骤的复杂工作流。
例如,当在 **Gym Management** 上下文中删除一个房间时,它会引发一个 `RoomDeleted` 领域事件,该事件随后作为集成事件被发布。

在 **Session Reservation** 上下文中,这单个集成事件可能会触发多个内部处理器来维护一致性:
- **Room Handler**:从本地数据库中移除该房间实体。
- **Session Handler**:找出与被删除房间 ID 关联的所有场次,并在这些 session 聚合上调用 `Cancel` 方法。

在 session 聚合上调用 `Cancel` 会在 Session Reservation 上下文内引发一个 `SessionCanceled` **领域事件**。
这个内部事件随后触发次级处理器:
- **Participant Module**:获取被取消场次的所有参与者,并把该场次从他们各自的日程中清除。
- **Trainer Module**:把该场次从所分配教练的日程中清除。

```text
Gym Management: RoomDeleted --> 集成事件
                                   |
                                   v
Session Reservation
  +-- Room Handler:    移除房间实体
  +-- Session Handler: Cancel() --> SessionCanceled 领域事件
                                      +-- Participant Module: 清除日程
                                      +-- Trainer Module:     清除日程
```

这一层级展示了单个集成事件如何启动一条特定于领域的逻辑和内部领域事件链,以确保不同模块之间的所有相关数据保持同步。

## 4. Error handling when publishing Integration Events

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/error-handling-when-publishing-integration-events-54123522/) · 3:56

### 总结

最终一致性引入了更多的活动部件和状态,增加了步骤之间出错的风险。
一个关键挑战是确保本地状态变化与向消息代理发布对应集成事件这两件事原子地发生。
如果系统在数据库更新之后、事件发送之前崩溃,或者代理不可用,系统就会进入不一致的状态。
Outbox 模式是在这类场景中实现原子性的标准解决方案。

### 核心概念

- **最终一致性的复杂性**:与同步操作相比,更多的活动部件和中间状态增加了故障点。
- **有韧性的传播**:变化必须在限界上下文之间可靠地传播,以维护整个企业范围内的系统完整性。
- **代理的韧性**:消息代理虽然能应对消费者宕机,但无法解决生产者在初次发布时发生的故障。
- **原子性缺口**:数据库事务成功,而随后的事件发布却因网络问题或服务崩溃而失败的风险。
- **Outbox 模式**:一种通过把状态变化和事件记录绑定在单个原子事务中来确保至少一次投递的机制。

### 课程笔记

实现最终一致性,无论是在单个限界上下文内还是跨多个上下文,都需要一种有韧性的方法来应对数量增多的故障点。
当一次状态转换涉及多个步骤时,系统必须防止出现变化未能正确传播的"中间"状态。

#### 传播的挑战

设想在 **User Management** 上下文中创建了一个新的教练档案。
这一变化必须传达给 **Session Reservation** 上下文。
如果 Session Reservation 上下文处于离线状态(例如服务不可用),消息代理通常可以缓解这一问题。
消息会被存储在队列中,一旦 Session Reservation 上下文重新上线,它就会消费这条消息并相应地更新自己的状态。

#### 原子性问题

然而,消息代理本身也引入了一个潜在的故障点。
如果生产者(User Management)更新了本地数据库,却未能成功把消息发布到代理,就会出现一个严重问题。
这可能在以下情况下发生:

1. 消息代理暂时不可用。
2. 生产服务在数据库提交之后、发往代理的网络请求完成之前立即崩溃。

在这些情况下,本地状态已经改变,但系统的其余部分对此毫不知情。
即使可靠性很高(例如 99.99%),在规模化的情况下(数以百万计的请求),这种边缘情况也会导致相当多的用户遇到损坏或不一致的系统状态。
系统作为一个整体必须被设计成能够管理这些故障。

#### 实现原子性

为了解决这个问题,对内部聚合的更新和集成事件的发布必须是原子的。
系统需要一种保证:要么两个操作都成功,要么都不成功。
这通过 **Outbox 模式** 实现,它让系统在同一个事务中把事件保存到存放聚合状态的同一个数据库里,从而确保即使首次尝试失败,事件最终也会被发布。

## 5. The Outbox Pattern

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-outbox-pattern-54123523/) · 4:25

### 总结

Outbox 模式是一种可靠性模式,用于确保数据库更新与集成事件发布之间的原子一致性。
通过在聚合变更所在的同一事务中把事件存入专用的 outbox 表,系统保证即使消息代理暂时不可用,事件也永远不会丢失。
随后由一个后台进程读取这些事件并把它们发布到代理,为分布式通信提供了一种有韧性的机制。

### 核心概念

- **原子一致性**:确保聚合状态变化与集成事件的持久化作为单个工作单元一起成功或一起失败。
- **Outbox 表**:一张专用的数据库表,用于在集成事件成功发布到消息代理之前存储它们。
- **事务完整性**:利用数据库事务同时写入领域模型变更和 outbox 消息。
- **后台发布器**:一个异步服务或 worker,轮询 outbox 表,把消息发布到代理,并在成功后删除它们或将其标记为已处理。
- **有韧性的重试**:在代理宕机时能够重试消息发布,确保不会因为暂时性的网络或服务故障而丢失任何事件。

### 课程笔记

当一个聚合被更新时(例如用户档案变更),这一更新会作为事实来源存储在数据库中。
如果系统随后尝试向消息代理发布集成事件,而该代理不可用,系统就会变得不一致:数据库反映了这一变化,但外部系统仍然不知情。

Outbox 模式通过在聚合所在的同一个数据库中引入一张 `OutboxIntegrationEvents` 表来解决这个问题。
当用户聚合被更新时,对应的集成事件会作为同一个数据库事务的一部分插入这张 outbox 表。
这确保了聚合更新和事件存储要么都发生,要么都不发生。

```csharp
public class UserManagementDbContext : DbContext
{
    public DbSet<OutboxIntegrationEvent> OutboxIntegrationEvents { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-outbox-pattern-54123523/?t=175)

在这一实现中,`OutboxIntegrationEvent` 是一个简单的实体或 record,记录事件的名称及其序列化后的内容。
当引发一个需要集成事件的领域事件时,一个处理器会创建该集成事件并把它持久化到 outbox。

```csharp
public record OutboxIntegrationEvent(string EventName, string EventContent);

private async Task AddOutboxIntegrationEventAsync(IIntegrationEvent integrationEvent)
{
    await _dbContext.OutboxIntegrationEvents.AddAsync(new OutboxIntegrationEvent(
        EventName: integrationEvent.GetType().Name,
        EventContent: JsonSerializer.Serialize(integrationEvent)));

    await _dbContext.SaveChangesAsync();
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-outbox-pattern-54123523/?t=190)

一旦数据库中的数据达到事务一致,一个后台服务(通常称为"message relay"或"outbox processor")就会从 outbox 表中读取消息。
该服务随后把这些消息发布到消息代理。
发布成功后,该服务会把消息从 outbox 表中删除,或将其标记为已处理。

```text
            同一个数据库事务
+-------------------------------------------+
|  User 聚合更新                            |
|  OutboxIntegrationEvents 插入集成事件     |
+-------------------------------------------+
                     |
                     v
   后台服务 (message relay / outbox processor)
     读取 outbox --> 发布到 message broker
                 --> 删除或标记为已处理
                 (失败则重试)
```

如果后台服务未能发布某条消息,它只需重试即可。
虽然这偶尔可能导致向代理发送重复消息(至少一次投递),但这在分布式系统中是一个可控的问题,通常通过幂等消费者来处理。
这一模式确保集成事件被可靠地制造和发送,维护了系统通信的完整性。

## 6. The Integration Events Sausage Factory - Outbox Writer

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-integration-events-sausage-factory-outbox-writer-54123524/) · 4:18

### 总结

本课详细介绍 Outbox Writer 模式的技术实现,以确保集成事件的可靠发布。
通过在 UserManagement 限界上下文中捕获领域事件并把它们转换为集成事件,系统把这些事件持久化到一张专用的 outbox 表中。
这一过程借助自定义中间件在单个数据库事务中编排,确保聚合状态和 outbox 记录被原子地更新,从而维护整个系统的最终一致性。

### 核心概念

- 为集成事件实现 Outbox 模式。
- 通过 MediatR 处理器把领域事件映射为集成事件。
- 聚合更新与 outbox 持久化之间的事务原子性。
- 使用 `EventualConsistencyMiddleware` 管理事务生命周期。
- 序列化集成事件以便存入 `OutboxIntegrationEvents` 表。

### 课程笔记

实现从 `UserManagement` 限界上下文开始,具体是在 `User` 聚合根内部。
当创建一个档案(例如管理员、教练或参与者)时,聚合会更新其内部状态并引发对应的领域事件。

```csharp
public class User : AggregateRoot
{
    public string FirstName { get; } = null!;
    public string LastName { get; } = null!;
    public string Email { get; } = null!;
    public Guid? AdminId { get; private set; }
    public Guid? ParticipantId { get; private set; }
    public Guid? TrainerId { get; private set; }

    public ErrorOr<Guid> CreateAdminProfile()
    {
        if (AdminId is not null)
        {
            return Error.Conflict(description: "User already has an admin profile");
        }

        AdminId = Guid.NewGuid();

        _domainEvents.Add(new AdminProfileCreatedEvent(Id, AdminId.Value));

        return AdminId.Value;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-integration-events-sausage-factory-outbox-writer-54123524/?t=40)

领域事件本身是一个简单的 record,携带这次变化所需的标识符。

```csharp
public record AdminProfileCreatedEvent(Guid UserId, Guid AdminId) : IDomainEvent;
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-integration-events-sausage-factory-outbox-writer-54123524/?t=65)

为了在领域事件和集成事件之间架起桥梁,系统使用了一个 `OutboxWriterEventHandler`。
这个类为需要跨上下文通信的领域事件实现了 MediatR 的 `INotificationHandler`。
当引发一个领域事件时,该处理器会把它映射为对应的集成事件。

```csharp
public class OutboxWriterEventHandler
    : INotificationHandler<AdminProfileCreatedEvent>,
      INotificationHandler<ParticipantProfileCreatedEvent>,
      INotificationHandler<TrainerProfileCreatedEvent>
{
    private readonly UserManagementDbContext _dbContext;

    public OutboxWriterEventHandler(UserManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(AdminProfileCreatedEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new AdminProfileCreatedIntegrationEvent(
            notification.UserId,
            notification.AdminId);

        await AddOutboxIntegrationEventAsync(integrationEvent);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-integration-events-sausage-factory-outbox-writer-54123524/?t=125)

`AddOutboxIntegrationEventAsync` 方法负责序列化集成事件并把它添加到 `OutboxIntegrationEvents` 表中。
这一操作使用与聚合更新相同的 `DbContext` 来执行,确保它们共享同一个数据库事务。

```csharp
private async Task AddOutboxIntegrationEventAsync(IIntegrationEvent integrationEvent)
{
    await _dbContext.OutboxIntegrationEvents.AddAsync(new OutboxIntegrationEvent(
        EventName: integrationEvent.GetType().Name,
        EventContent: JsonSerializer.Serialize(integrationEvent)));

    await _dbContext.SaveChangesAsync();
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-integration-events-sausage-factory-outbox-writer-54123524/?t=160)

事务的原子性由 `EventualConsistencyMiddleware` 管理。
当请求到达服务时,这个中间件会开启一个事务。
接着它发布领域事件(这会触发 `OutboxWriterEventHandler`),并且只有在所有操作都成功时才提交事务。
如果过程中的任何部分失败,事务会被释放,聚合和 outbox 都不会持久化任何变更。

```csharp
public async Task InvokeAsync(HttpContext context, IPublisher publisher, UserManagementDbContext dbContext)
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
        catch (EventualConsistencyException)
        {
            // Handle resiliently
        }
        finally
        {
            await transaction.DisposeAsync();
        }
    });

    await _next(context);
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-integration-events-sausage-factory-outbox-writer-54123524/?t=190)

```text
EventualConsistencyMiddleware
  BeginTransactionAsync()
    |
    v
  publisher.Publish(AdminProfileCreatedEvent)
    |
    v
  OutboxWriterEventHandler
    --> AdminProfileCreatedIntegrationEvent
    --> AddOutboxIntegrationEventAsync() --> OutboxIntegrationEvents
    |
    v
  全部成功: CommitAsync()      任何失败: DisposeAsync(), 不持久化
```

## 7. The Integration Events Sausage Factory - Integration Event Publisher

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-integration-events-sausage-factory-integration-event-publisher-54123525/) · 1:56

### 总结

本课详细介绍一个后台服务的实现,它充当存储在 outbox 表中的集成事件的发布者。
课程讲解了如何使用 PeriodicTimer 定期轮询数据库、反序列化已存储的事件,并通过扇出交换机(fanout exchange)把它们分发到消息代理,以确保可靠地投递给其他限界上下文。
整个过程最后会从数据库中清除已处理的事件,以维护 outbox 表。

### 核心概念

- 用于异步处理的后台服务(`IHostedService`)。
- 使用 `PeriodicTimer` 进行定时执行的轮询机制。
- 用于可靠事件发布和一致性的 Outbox 模式。
- 用于把消息广播给多个订阅者的扇出交换机。
- 后台处理中的韧性考量与错误日志记录。

### 课程笔记

`PublishIntegrationEventsBackgroundService` 是 `IHostedService` 的一个实现,负责集成事件发布过程的生命周期。
它在数据库 outbox 和消息代理之间进行协调。

```csharp
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SharedKernel.IntegrationEvents;
using Throw;
using UserManagement.Api.IntegrationEvents.IntegrationEventsPublisher;
using UserManagement.Infrastructure.Persistence;

namespace UserManagement.Api.IntegrationEvents.BackgroundService;

public class PublishIntegrationEventsBackgroundService : IHostedService
{
    private Task? _doWorkTask = null;
    private PeriodicTimer? _timer = null!;
    private readonly IIntegrationEventsPublisher _integrationEventPublisher;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<PublishIntegrationEventsBackgroundService> _logger;
    private readonly CancellationTokenSource _cts = new();

    // Constructor implementation...
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-integration-events-sausage-factory-integration-event-publisher-54123525/?t=10)

该服务在 `DoWorkAsync` 中执行一个持续的循环。
它使用一个间隔设为 5 秒的 `PeriodicTimer`。
每次计时器触发时,服务都会尝试从数据库发布事件。
如果在这一过程中发生异常,它会被捕获并记录日志,以确保后台服务保持运行,不过生产环境需要更高级的监控和有韧性的恢复机制。

```csharp
private async Task DoWorkAsync()
{
    _logger.LogInformation("Starting integration event publisher background service.");

    _timer = new PeriodicTimer(TimeSpan.FromSeconds(5));

    while (await _timer.WaitForNextTickAsync(_cts.Token))
    {
        try
        {
            await PublishIntegrationEventsFromDbAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Exception occurred while publishing integration events.");
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-integration-events-sausage-factory-integration-event-publisher-54123525/?t=25)

处理 outbox 的核心逻辑包含在 `PublishIntegrationEventsFromDbAsync` 中。
由于这是一个后台服务,它会创建一个新的依赖注入作用域来解析 `UserManagementDbContext`。
它从 `OutboxIntegrationEvents` 表中获取待处理事件的列表,把 JSON 内容反序列化为 `IIntegrationEvent` 对象,并使用 `IIntegrationEventPublisher` 把它们发送到代理。
发布成功后,这些事件会从数据库中删除。

```csharp
private async Task PublishIntegrationEventsFromDbAsync()
{
    using var scope = _serviceScopeFactory.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<UserManagementDbContext>();

    var outboxIntegrationEvents = dbContext.OutboxIntegrationEvents.ToList();

    _logger.LogInformation("Read a total of {NumEvents} outbox integration events", outboxIntegrationEvents.Count);
    
    outboxIntegrationEvents.ForEach(outboxIntegrationEvent =>
    {
        var integrationEvent = JsonSerializer.Deserialize<IIntegrationEvent>(outboxIntegrationEvent.Content);
        integrationEvent.ThrowIfNull();

        _logger.LogInformation("Publishing event of type: {EventType}", integrationEvent.GetType().Name);
        _integrationEventPublisher.PublishEvent(integrationEvent);
        _logger.LogInformation("Integration event published successfully");
    });

    dbContext.RemoveRange(outboxIntegrationEvents);
    await dbContext.SaveChangesAsync();
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-integration-events-sausage-factory-integration-event-publisher-54123525/?t=55)

`IntegrationEventsPublisher` 负责与消息代理之间的底层通信。
它配置一个 RabbitMQ 连接并声明一个 **Fanout** 交换机。
使用扇出交换机可以把消息分发到多个队列,使不同的限界上下文能够根据各自的需求独立地消费同一个事件。

```csharp
public class IntegrationEventsPublisher : IIntegrationEventsPublisher
{
    private readonly MessageBrokerSettings _messageBrokerSettings;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public IntegrationEventsPublisher(IOptions<MessageBrokerSettings> messageBrokerOptions)
    {
        _messageBrokerSettings = messageBrokerOptions.Value;
        ConnectionFactory connectionFactory = new ConnectionFactory
        {
            HostName = _messageBrokerSettings.HostName,
            Port = _messageBrokerSettings.Port,
            UserName = _messageBrokerSettings.UserName,
            Password = _messageBrokerSettings.Password
        };

        _connection = connectionFactory.CreateConnection();

        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(
            _messageBrokerSettings.ExchangeName,
            ExchangeType.Fanout,
            durable: true);
    }

    public void PublishEvent(IIntegrationEvent integrationEvent)
    {
        string serializedIntegrationEvent = JsonSerializer.Serialize(integrationEvent);

        byte[] body = Encoding.UTF8.GetBytes(serializedIntegrationEvent);

        _channel.BasicPublish(
            exchange: _messageBrokerSettings.ExchangeName,
            routingKey: string.Empty,
            body: body);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-integration-events-sausage-factory-integration-event-publisher-54123525/?t=80)

## 8. The Integration Events Sausage Factory - Consuming Integration Events

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-integration-events-sausage-factory-consuming-integration-events-54123526/) · 5:17

### 总结

本课讲解领域驱动设计架构中集成事件的消费端。
课程详细说明了 Gym Management 和 Session Reservation 这样的限界上下文如何使用后台服务监听来自消息代理(RabbitMQ)的消息、把它们反序列化为集成事件,并通过 Mediator 在内部发布。
本课演示了这些事件如何触发处理器来维护整个系统的最终一致性,例如同步管理员档案,或者传播房间删除以取消相关场次并更新教练日程。

### 核心概念

- **后台服务消费者**:一个托管服务,与消息代理保持连接以接收集成事件。
- **内部事件传播**:使用 Mediator 把收到的集成事件分派给本地处理器。
- **数据同步**:响应来自其他上下文的事件,创建实体(例如 Admins、Participants)的本地表示。
- **级联副作用**:单个集成事件(例如 Room Removed)如何触发多个本地操作以维护系统完整性。
- **有韧性的错误处理**:使用像 `EventualConsistencyException` 这样的特定异常来管理事件驱动链中的故障。

### 课程笔记

在 Gym Management 和 Session Reservation 限界上下文中,集成事件通过一个专门的后台服务来消费。
这个服务 `ConsumeIntegrationEventsBackgroundService` 是一个托管服务,负责管理与消息代理之间的连接。

```csharp
public class ConsumeIntegrationEventsBackgroundService : IHostedService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<ConsumeIntegrationEventsBackgroundService> _logger;
    private readonly CancellationTokenSource _cts;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly MessageBrokerSettings _messageBrokerSetting;
    // ...
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-integration-events-sausage-factory-consuming-integration-events-54123526/?t=115)

该服务在 RabbitMQ 通道上配置一个消费者。
它把队列绑定到交换机,并为 `EventingBasicConsumer` 的 `Received` 事件挂上一个事件处理器。

```csharp
_channel.QueueBind(
    _messageBrokerSettings.QueueName,
    _messageBrokerSettings.ExchangeName,
    routingKey: string.Empty);

var consumer = new EventingBasicConsumer(_channel);

consumer.Received += PublishIntegrationEvent;

_channel.BasicConsume(_messageBrokerSettings.QueueName, autoAck: false, consumer);
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-integration-events-sausage-factory-consuming-integration-events-54123526/?t=25)

收到消息时,由 `PublishIntegrationEvent` 方法处理它。
它把 JSON 负载反序列化为 `IIntegrationEvent`,从一个作用域服务提供器中解析出 Mediator 的 `IPublisher`,并在内部发布该事件。
一旦成功发布并处理完毕,它会向代理发回一个基本确认(Ack)。

```csharp
private async void PublishIntegrationEvent(object? sender, BasicDeliverEventArgs eventArgs)
{
    if (_cts.IsCancellationRequested) return;

    try
    {
        using var scope = _serviceScopeFactory.CreateScope();

        var body = eventArgs.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);

        var integrationEvent = JsonSerializer.Deserialize<IIntegrationEvent>(message);
        integrationEvent.ThrowIfNull();

        var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();
        await publisher.Publish(integrationEvent);

        _channel.BasicAck(eventArgs.DeliveryTag, multiple: false);
    }
    catch (Exception e)
    {
        _logger.LogError(e, "Exception occurred while consuming integration event");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-integration-events-sausage-factory-consuming-integration-events-54123526/?t=40)

```text
Message Broker (exchange) --QueueBind--> queue
                                          |
                                          v
                     EventingBasicConsumer.Received
                                          |
                                          v
                         PublishIntegrationEvent
                           Deserialize<IIntegrationEvent>
                           IPublisher.Publish(integrationEvent)
                           BasicAck
```

#### 处理集成事件

一旦集成事件在内部发布,应用层中特定的处理器就会对它做出反应。
在 Gym Management 上下文中,`AdminProfileCreatedEventHandler` 通过创建一个本地 `Admin` 实体来处理该事件,使该上下文与 Users 上下文保持同步。

```csharp
public class AdminProfileCreatedEventHandler : INotificationHandler<AdminProfileCreatedIntegrationEvent>
{
    private readonly IAdminsRepository _adminsRepository;

    public AdminProfileCreatedEventHandler(IAdminsRepository adminsRepository)
    {
        _adminsRepository = adminsRepository;
    }

    public async Task Handle(AdminProfileCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var admin = new Admin(notification.UserId, id: notification.AdminId);

        await _adminsRepository.AddAdminAsync(admin);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-integration-events-sausage-factory-consuming-integration-events-54123526/?t=85)

#### 复杂的事件传播

有些事件会触发更复杂的工作流来维护最终一致性。
例如,当在 Gym Management 中移除一个房间时,Session Reservation 上下文必须做出反应。
`RoomRemovedEventHandler` 首先从其本地仓储中移除该房间。

```csharp
public class RoomRemovedEventHandler : INotificationHandler<RoomRemovedIntegrationEvent>
{
    private readonly IRoomsRepository _roomsRepository;

    public RoomRemovedEventHandler(IRoomsRepository roomsRepository)
    {
        _roomsRepository = roomsRepository;
    }

    public async Task Handle(RoomRemovedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        Room? room = await _roomsRepository.GetByIdAsync(notification.RoomId);

        if (room is not null)
        {
            await _roomsRepository.RemoveAsync(room);
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-integration-events-sausage-factory-consuming-integration-events-54123526/?t=145)

此外,同一事件的另一个处理器会遍历与该房间关联的所有场次,并对每个场次调用 `Cancel()`。
这个领域操作会在 session 聚合内引发一个 `SessionCanceledEvent`。

```csharp
public void Cancel()
{
    _domainEvents.Add(new SessionCanceledEvent(this));
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-integration-events-sausage-factory-consuming-integration-events-54123526/?t=190)

`SessionCanceledEvent` 随后由其他组件处理,以更新教练日程并通知参与者。
为了确保可靠性,这些处理器使用了特定的错误处理逻辑。
如果某次更新失败,就会抛出 `EventualConsistencyException`,它可以被一个有韧性的错误处理机制捕获,以便重试或管理该故障。

```csharp
public async Task Handle(SessionCanceledEvent notification, CancellationToken cancellationToken)
{
    var trainer = await _trainersRepository.GetByIdAsync(notification.Session.TrainerId)
        ?? throw new EventualConsistencyException(SessionCanceledEvent.TrainerNotFound);

    var removeFromScheduleResult = trainer.RemoveFromSchedule(notification.Session);

    if (removeFromScheduleResult.IsError)
    {
        throw new EventualConsistencyException(
            SessionCanceledEvent.TrainerScheduleUpdateFailed,
            removeFromScheduleResult.Errors);
    }

    await _trainersRepository.UpdateAsync(trainer);
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-integration-events-sausage-factory-consuming-integration-events-54123526/?t=210)

这种事件驱动方法的模块化,让像删除健身房这样的复杂操作只需遍历该健身房的房间并调用 `RemoveRoom` 就能实现。
由此产生的集成事件和领域事件链会自动处理这些变化在整个系统中的传播。

## 9. Section Recap

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/section-recap-54123527/) · 0:45

### 总结

本课总结领域驱动设计中集成事件的核心原则,重点讲述它们如何促成跨限界上下文边界的通信。
课程回顾了最终一致性的实现、用于原子操作的 Outbox 模式,以及下游消费者在传播系统范围变化中的作用。

### 核心概念

- **集成事件**:跨越限界上下文边界、用于促成上下文间通信的事件。
- **最终一致性**:一种一致性模型,一个上下文中的变化会随时间推移传播到其他上下文,而不是立即传播。
- **下游传播**:次级限界上下文处理集成事件以更新其自身内部状态的过程。
- **Outbox 模式**:一种可靠性模式,用于确保数据库变更与事件发布原子地发生。
- **错误处理**:在事件生命周期中管理故障以维护系统完整性的策略。

### 课程笔记

集成事件是彼此独立的限界上下文之间通信的主要机制。
与局限于单个上下文内部逻辑的领域事件不同,集成事件专门用于把重要的变化广播给外部系统或同一领域内的其他上下文。
这种方法对于维护一种解耦的架构至关重要,在这种架构中,各个上下文不直接依赖彼此的内部实现。

使用集成事件的一个主要目标是实现最终一致性。
在分布式系统中,让所有上下文立即一致往往不切实际。
通过发布集成事件,发起方上下文把变化告知下游上下文。
这些下游上下文随后处理这些事件,在各自的边界内传播必要的更新,确保系统随着时间推移保持同步。

事件投递的可靠性对于维持这种一致性至关重要。
Outbox 模式的实现确保数据库更新与事件持久化原子地发生。
通过在业务逻辑变更所在的同一事务中把事件写入 outbox 表,系统保证事件永远不会因为发布阶段的网络或基础设施故障而丢失。
此外,还需要健壮的错误处理策略来应对下游消费者未能处理事件的情况,允许重试或进入死信队列,以维护系统完整性。
