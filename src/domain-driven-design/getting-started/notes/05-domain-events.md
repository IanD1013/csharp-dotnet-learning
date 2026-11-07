# Domain Events

> 课程:[Getting Started: Domain-Driven Design](https://dometrain.com/course/getting-started-domain-driven-design-ddd/) · 第 5 章
> 共 7 课 · 约 33:03
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Accessing the Domain Layer via a web API](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/accessing-the-domain-layer-via-a-web-api-54123388/) | 4:49 | [↓](#1-accessing-the-domain-layer-via-a-web-api) |
| 2 | [A system without Domain Events](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/a-system-without-domain-events-54123389/) | 6:45 | [↓](#2-a-system-without-domain-events) |
| 3 | [Eventual Consistency](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/eventual-consistency-54123390/) | 1:15 | [↓](#3-eventual-consistency) |
| 4 | [What is a Domain Event?](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/what-is-a-domain-event-54123391/) | 5:27 | [↓](#4-what-is-a-domain-event) |
| 5 | [The Domain Event Sausage Factory](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-domain-event-sausage-factory-54123392/) | 6:43 | [↓](#5-the-domain-event-sausage-factory) |
| 6 | [Implementing a Domain Event](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-a-domain-event-54123393/) | 6:41 | [↓](#6-implementing-a-domain-event) |
| 7 | [Section Recap](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/section-recap-54123394/) | 1:23 | [↓](#7-section-recap) |

## 1. Accessing the Domain Layer via a web API

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/accessing-the-domain-layer-via-a-web-api-54123388/) · 4:49

本课讲解如何通过 Web API 暴露领域模型,并利用 Mediator 模式把表示层与应用逻辑解耦。
它详细说明了应用层在编排领域聚合中的角色,以及基础设施层在为持久化和系统服务提供具体实现中的角色,为识别需要领域事件的跨聚合副作用做好铺垫。

### 核心概念

- **Presentation Layer(表示层)**:用户交互的入口,这里实现为一个 ASP.NET Core Web API。
- **Mediator Pattern(中介者模式)**:通过 MediatR 库使用,借助分发命令和查询,把 API 控制器与应用逻辑解耦。
- **Application Layer(应用层)**:包含应用服务(处理器),它们编排领域对象来执行业务操作。
- **Infrastructure Layer(基础设施层)**:为领域和应用接口提供具体实现,例如仓储和系统时钟提供者。
- **Aggregate Orchestration(聚合编排)**:应用服务协调多个领域对象并通过仓储持久化更改的过程。

### 课程笔记

为了让用户能够与领域及其强制执行的不变量进行交互,需要一个表示层。
虽然可以使用 WPF 或移动框架等各种技术,但本实现使用的是 ASP.NET Core Web API。
API 项目由代表资源(例如 Gyms)的控制器组成,它们接收 HTTP 请求,并把实际逻辑委托给应用层。

`GymsController` 使用 MediatR 的 `ISender` 来分发命令。
这种做法把一个操作所需的数据(例如 `CreateGymCommand`)封装起来,并把 HTTP 请求处理与业务逻辑分离开。

```csharp
namespace DomeGym.Api.Controllers;

[Route("subscriptions/{subscriptionId:guid}/gyms")]
public class GymsController : ApiController
{
    private readonly ISender _sender;

    public GymsController(ISender sender)
    { 
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateGym(CreateGymRequest request, Guid subscriptionId)
    {
        var command = new CreateGymCommand(request.Name, subscriptionId);

        var createGymResult = await _sender.Send(command);

        return createGymResult.Match(
            gym => CreatedAtAction(
                nameof(GetGym),
                new { subscriptionId, GymId = gym.Id },
                new GymResponse(gym.Id, gym.Name)),
            Problem);
    }

    [HttpGet]
    public async Task<IActionResult> ListGyms(Guid subscriptionId)
    {
        var command = new ListGymsQuery(subscriptionId);

        var listGymsResult = await _sender.Send(command);
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/accessing-the-domain-layer-via-a-web-api-54123388/?t=85)

控制器处理各种 HTTP 动词,把它们映射到相应的查询或命令,并根据操作结果返回合适的响应。

```csharp
Problem);
    }

    [HttpGet]
    public async Task<IActionResult> ListGyms(Guid subscriptionId)
    {
        var command = new ListGymsQuery(subscriptionId);

        var listGymsResult = await _sender.Send(command);

        return listGymsResult.Match(
            gyms => Ok(gyms.ConvertAll(gym => new GymResponse(gym.Id, gym.Name))),
            Problem);
    }

    [HttpGet("{gymId:guid}")]
    public async Task<IActionResult> GetGym(Guid subscriptionId, Guid gymId)
    {
        var command = new GetGymQuery(subscriptionId, gymId);

        var getGymResult = await _sender.Send(command);

        return getGymResult.Match(
            gym => Ok(new GymResponse(gym.Id, gym.Name)),
            Problem);
    }

    [HttpPost("{gymId:guid}/trainers")]
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/accessing-the-domain-layer-via-a-web-api-54123388/?t=130)

应用层包含每个功能的逻辑。
例如,`CreateGymCommandHandler` 充当一个编排领域对象的应用服务。
它首先从数据库中取出所需的聚合。

```csharp
using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.GymAggregate;

using ErrorOr;

using MediatR;

namespace DomeGym.Application.Gyms.CreateGym;

public class CreateGymCommandHandler : IRequestHandler<CreateGymCommand, ErrorOr<Gym>>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;

    public CreateGymCommandHandler(ISubscriptionsRepository subscriptionsRepository)
    {
        _subscriptionsRepository = subscriptionsRepository;
    }

    public async Task<ErrorOr<Gym>> Handle(CreateGymCommand command, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionsRepository.GetByIdAsync(command.SubscriptionId);

        if (subscription is null)
        {
            return Error.NotFound(description: "Subscription not found");
        }
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/accessing-the-domain-layer-via-a-web-api-54123388/?t=175)

聚合被取出后,处理器执行领域操作,在本例中就是创建一个新的 gym 并把它添加到 subscription 中。
如果领域逻辑成功,处理器就更新仓储以持久化这些更改。

```csharp
public async Task<ErrorOr<Gym>> Handle(CreateGymCommand command, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionsRepository.GetByIdAsync(command.SubscriptionId);

        if (subscription is null)
        {
            return Error.NotFound(description: "Subscription not found");
        }

        var gym = new Gym(
            name: command.Name,
            maxRooms: subscription.GetMaxRooms(),
            subscriptionId: subscription.Id);

        var addGymResult = subscription.AddGym(gym);

        if (addGymResult.IsError)
        {
            return addGymResult.Errors;
        }

        await _subscriptionsRepository.UpdateAsync(subscription);

        return gym;
    }
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/accessing-the-domain-layer-via-a-web-api-54123388/?t=190)

```text
GymsController --ISender.Send(CreateGymCommand)--> CreateGymCommandHandler
                                                        |
        1. _subscriptionsRepository.GetByIdAsync(...)   |
        2. new Gym(...) + subscription.AddGym(gym)      |
        3. _subscriptionsRepository.UpdateAsync(...)    |
                                                        v
GymsController <--------------- ErrorOr<Gym> -----------+
     |
     +--> createGymResult.Match(CreatedAtAction(...), Problem)
```

基础设施层实现在领域层和应用层中定义的接口。
这包括时间提供者和数据持久化等系统层面的关注点。

```csharp
using DomeGym.Domain.Common.Interfaces;

namespace DomeGym.Infrastructure.Services;

public class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/accessing-the-domain-layer-via-a-web-api-54123388/?t=235)

基础设施层中的仓储使用 Entity Framework Core 与数据库交互,通过实现仓储接口来提供具体的数据访问逻辑。

```csharp
using DomeGym.Application.Common.Interfaces;
using DomeGym.Application.Profiles.Common;
using DomeGym.Domain.ParticipantAggregate;
using DomeGym.Domain.Profiles;

using Microsoft.EntityFrameworkCore;

namespace DomeGym.Infrastructure.Persistence.Repositories;

public class ParticipantsRepository : IParticipantsRepository
{
    private readonly DomeGymDbContext _dbContext;

    public ParticipantsRepository(DomeGymDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddParticipantAsync(Participant participant)
    {
        await _dbContext.Participants.AddAsync(participant);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Participant?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Participants.FirstOrDefaultAsync(parti
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/accessing-the-domain-layer-via-a-web-api-54123388/?t=250)

这些实现确保应用能够获取和存储领域聚合的状态,同时让领域层不受基础设施关注点的影响。

```csharp
public ParticipantsRepository(DomeGymDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddParticipantAsync(Participant participant)
    {
        await _dbContext.Participants.AddAsync(participant);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Participant?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Participants.FirstOrDefaultAsync(participant => participant.Id == i
    }

    public async Task<Profile?> GetProfileByUserIdAsync(Guid userId)
    {
        var participant = await _dbContext.Participants
            .AsNoTracking()
            .FirstOrDefaultAsync(participant => participant.UserId ==

        return participant is null ? null : new Profile(participant.I
    }
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/accessing-the-domain-layer-via-a-web-api-54123388/?t=270)

## 2. A system without Domain Events

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/a-system-without-domain-events-54123389/) · 6:45

### 总结

本课通过考察订阅创建流程中的一个 bug,演示一个缺少领域事件的系统有哪些局限。
虽然一个命令处理器成功地用新的 SubscriptionId 更新了 Admin 聚合,但 Subscription 本身从未被持久化,因为该处理器只与 AdminsRepository 交互。
本课把"编排"(Orchestration)方式(由单个处理器手动管理多个仓储和副作用)与领域事件模式进行对比,强调编排会如何导致延迟增加、错误处理复杂以及聚合之间的紧耦合。

### 核心概念

* **Aggregate Persistence Gaps(聚合持久化缺口)**:当一个命令处理器更新了一个聚合,却未能持久化操作中创建的相关实体或聚合时就会出现。
* **Orchestration Approach(编排方式)**:一种由单个处理器在一次请求中协调多个聚合和仓储之间更改的设计。
* **Side Effect Latency(副作用延迟)**:用户在收到响应之前必须等待所有次要操作(例如通知、关联删除)完成而引入的延迟。
* **Error Handling and Retries(错误处理与重试)**:当在一个处理器中依次执行多个数据库操作时,管理部分失败的困难。
* **Eventual Consistency(最终一致性)**:一种一致性模型,更新会随时间传播到相关的系统或聚合,通常借助领域事件实现。

### 课程笔记

在实现跨越多个聚合的功能时,一个常见的陷阱是没能持久化所有受影响的实体。
在当前系统中,创建一个订阅需要向 `/subscriptions` 端点发送一个 POST 请求。

```http
POST {{host}}/subscriptions
Content-Type: application/json

{
    "SubscriptionType": "Free",
    "AdminId": "{{adminId}}"
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/a-system-without-domain-events-54123389/?t=25)

虽然响应表明成功,数据库中的 `Admin` 记录也更新了 `SubscriptionId`,但随后列出订阅的请求却返回一个空列表。

```http
GET {{host}}/subscriptions
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/a-system-without-domain-events-54123389/?t=45)

根本原因在于 `CreateSubscriptionCommandHandler`。
该处理器取出 `Admin`,创建一个新的 `Subscription` 对象,并更新 `Admin` 聚合。
然而,它只在 `IAdminsRepository` 上调用了 `UpdateAsync`。

```csharp
public CreateSubscriptionCommandHandler(IAdminsRepository adminsRepository)
{
    _adminsRepository = adminsRepository;
}

public async Task<ErrorOr<Subscription>> Handle(CreateSubscriptionCommand command, CancellationToken cancellationToken)
{
    var admin = await _adminsRepository.GetByIdAsync(command.AdminId);

    if (admin is null)
    {
        return Error.NotFound(description: "Admin not found");
    }

    if (admin.SubscriptionId is not null)
    {
        return Error.Conflict(description: "Admin already has active subscription");
    }

    var subscription = new Subscription(command.SubscriptionType, command.AdminId);
    admin.SetSubscription(subscription);

    await _adminsRepository.UpdateAsync(admin);

    return subscription;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/a-system-without-domain-events-54123389/?t=115)

`Admin` 聚合的 `SetSubscription` 方法正确地赋值了 ID,但 `Subscription` 实体本身从未被保存到它自己的仓储中。

```csharp
public Admin(
    Guid userId,
    Guid? subscriptionId = null,
    Guid? id = null)
    : base(id ?? Guid.NewGuid())
{
    UserId = userId;
    SubscriptionId = subscriptionId;
}

public ErrorOr<Success> SetSubscription(Subscription subscription)
{
    if (SubscriptionId.HasValue)
    {
        return Error.Conflict(description: "Admin already has an active subscription");
    }

    SubscriptionId = subscription.Id;

    return Result.Success;
}

private Admin() { }
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/a-system-without-domain-events-54123389/?t=145)

#### 编排方式

修复这个问题的一种方式是通过编排。
这需要把 `ISubscriptionsRepository` 注入到处理器中,并手动调用 `AddSubscriptionAsync`。

```csharp
public async Task<ErrorOr<Subscription>> Handle(CreateSubscriptionCommand command, CancellationToken cancellationToken)
{
    var admin = await _adminsRepository.GetByIdAsync(command.AdminId);

    if (admin is null)
    {
        return Error.NotFound(description: "Admin not found");
    }

    if (admin.SubscriptionId is not null)
    {
        return Error.Conflict(description: "Admin already has active subscription");
    }

    var subscription = new Subscription(command.SubscriptionType, command.AdminId);
    admin.SetSubscription(subscription);

    await _adminsRepository.UpdateAsync(admin);
    await _subscriptionsRepository.AddSubscriptionAsync(subscription);

    return subscription;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/a-system-without-domain-events-54123389/?t=230)

虽然这在简单情况下可行,但随着系统增长,编排就会变得有问题。
例如,删除 gym 中的一个 room 需要删除所有关联的 session,并通知 participant 和 trainer。

```csharp
var room = await _roomsRepository.GetByIdAsync(command.RoomId);

if (room is null)
{
    return Error.NotFound(description: "Room not found");
}

var removeGymResult = gym.RemoveRoom(room);

if (removeGymResult.IsError)
{
    return removeGymResult.Errors;
}

await _gymsRepository.UpdateAsync(gym);

return Result.Deleted;
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/a-system-without-domain-events-54123389/?t=325)

如果由处理器来编排所有这些副作用,用户就必须等待每一个操作完成。
此外,如果某个操作失败(例如通知服务宕机),处理器就必须管理复杂的重试逻辑或回滚。
与其采用这种紧耦合的编排,系统应当转向使用领域事件模式来实现最终一致性。

## 3. Eventual Consistency

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/eventual-consistency-54123390/) · 1:15

### 总结

最终一致性是一种设计模式,它把处理过程划分为多个独立的步骤,而不是在单个原子事务中执行。
这种方式能给用户即时反馈,同时允许系统范围内的更改随时间传播,直到整个系统达到一致状态。

### 核心概念

- **Divided Processing(分割处理)**:把单个业务操作拆分成多个异步步骤,而不是一个单一的事务块。
- **Immediate Feedback(即时反馈)**:主要参与者(例如管理员)在整个系统处理完所有副作用之前就收到操作的确认。
- **State Propagation(状态传播)**:更改在系统中层层扩散、更新相关实体和聚合的过程。
- **Consistency Models(一致性模型)**:把最终一致性与原子或事务一致性进行对比,后者要求所有更改必须一起成功或一起失败。

### 课程笔记

最终一致性是一种通过把必要的处理划分成更小的、解耦的单元来管理复杂操作的模式。
与在一整块中执行所有操作的原子或事务一致性不同,最终一致性允许系统先确认请求,然后随时间传播由此产生的更改。

例如,当管理员在系统中删除一个 room 时,他们会立即收到操作成功的通知。
从管理员的角度看,操作已经完成。
然而,系统可能仍需执行次要操作,例如通知 participant 他们关联的 session 已被删除。
虽然系统的各个部分并非立即一致,但一旦所有后台流程和通知都完成,它最终会达到一致状态。

这种方式提升了感知性能和系统响应性。
领域事件是领域驱动设计中实现这一模式的主要机制,它让系统的不同部分能够独立地对更改做出反应。

## 4. What is a Domain Event?

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/what-is-a-domain-event-54123391/) · 5:27

### 总结

领域事件是对系统中已经发生的一次重要业务事件的记录。
它充当一种通知机制,在不同聚合之间触发副作用,从而在保持解耦架构的同时实现最终一致性。
通过使用过去时命名并实现专门的处理器,开发者可以确保主要操作保持原子性,而次要后果则被独立且有韧性地处理。

### 核心概念

*   **Definition(定义)**:系统中发生的一个值得关注的业务事件。
*   **Naming(命名)**:用过去时表达(例如 `SessionSpotReservedEvent`),以表明一个已完成的动作。
*   **Eventual Consistency(最终一致性)**:促成跨聚合的更新,而无需在整个系统上进行即时的原子事务。
*   **Decoupling(解耦)**:允许多个独立的处理器响应同一个事件,而发起事件的聚合无需知道它们的存在。
*   **Resiliency(韧性)**:需要错误处理策略,例如重试或手动兜底,来管理最终一致流程中的失败。

### 课程笔记

领域事件被定义为系统中发生的、从业务角度看值得关注的事情。
命名约定至关重要:事件必须用过去时命名,以表示该动作已经发生。
一旦事件发生,它就会作为通知被引发,让系统的其他部分能够做出反应。

在下面的示例中,`Session` 类中的 `ReserveSpot` 方法处理添加预订的核心逻辑。
一旦满足业务规则且预订被添加到内部列表,就会创建一个 `SessionSpotReservedEvent` 并添加到领域事件集合中。

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

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/what-is-a-domain-event-54123391/?t=40)

事件本身就像一个命令或查询对象,包含处理器处理它所需的数据。
这些事件通常与聚合状态一起持久化到数据库中。
一旦 session 被存储,系统就引发该事件,交由一个或多个处理器处理。

处理器接收事件通知,并执行相应的副作用逻辑。
例如,当在一个 session 中预订了一个名额时,系统需要更新 participant 的日程。
虽然预订本身在 session 聚合内是原子的,但更新 participant 的日程是一个最终一致的操作。

```csharp
public async Task Handle(SessionSpotReservedEvent notification, CancellationToken cancellationToken)
{
    var participant = await _participantsRepository.GetByIdAsync(notification.Reservation.ParticipantId);
    participant.ThrowIfNull();

    var updateParticipantScheduleResult = participant.AddToSchedule(notification.Session);

    if (updateParticipantScheduleResult.IsError)
    {
        throw new EventualConsistencyException(
            SessionSpotReservedEvent.ParticipantScheduleUpdateFailed,
            updateParticipantScheduleResult.Errors);
    }

    await _participantsRepository.UpdateAsync(participant);
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/what-is-a-domain-event-54123391/?t=115)

```text
Session.ReserveSpot(participant)          (原子, Session 聚合内)
   |  _reservations.Add(reservation)
   |  _domainEvents.Add(new SessionSpotReservedEvent(this, reservation))
   v
SessionSpotReservedEvent
   |
   v
Handle(SessionSpotReservedEvent notification, ...)   (最终一致)
   |  participant.AddToSchedule(notification.Session)
   |  IsError -> throw EventualConsistencyException(
   |               ParticipantScheduleUpdateFailed, ...)
   v
_participantsRepository.UpdateAsync(participant)
```

由于最终一致性可能失败,因此需要一个有韧性的错误处理机制。
如果某个处理器失败,系统可能会使用重试机制或兜底方案,例如通知管理员手动解决该问题。
这通常通过向异常传入一个常量值(例如 `ParticipantScheduleUpdateFailed`)来标识具体的失败点。

领域事件模式还通过允许多个处理器订阅同一个事件来促进解耦。
如果一个 room 被删除,不同的处理器可以分别独立地更新 participant 和 trainer 的日程。
这让逻辑可以留在相关的领域上下文中(例如 participant 的逻辑留在 participant 聚合中),而不是让发起事件的聚合变得臃肿。

## 5. The Domain Event Sausage Factory

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-domain-event-sausage-factory-54123392/) · 6:43

### 总结

本课详细介绍如何在 .NET 应用中使用领域事件模式实现最终一致性机制。
通过利用 MediatR 发布事件,并借助自定义的 ASP.NET Core 中间件管理事务,系统确保副作用在主请求完成之后得到可靠处理。
该架构在 HTTP 上下文中采用基于队列的方式来处理递归产生的事件,确保所有领域事件都在单个数据库事务中处理,而该事务只有在所有处理器都成功执行后才会提交。

### 核心概念

* **AggregateRoot Event Collection(AggregateRoot 事件集合)**:聚合维护一个内部的领域事件列表,在持久化过程中这些事件会被"弹出"并清空。
* **MediatR Integration(MediatR 集成)**:领域事件实现一个包装了 MediatR `INotification` 的接口,以便进行解耦的事件处理。
* **Middleware-Driven Transactions(中间件驱动的事务)**:一个 ASP.NET Core 中间件管理数据库事务的生命周期,确保它同时覆盖初始请求和随后的事件处理。
* **Post-Response Processing(响应后处理)**:使用 `context.Response.OnCompleted` 让系统在处理可能耗时较长的副作用之前,先把响应返回给用户。
* **Recursive Event Queuing(递归事件排队)**:存储在 `HttpContext` 中的一个队列,让系统能够捕获并处理由先前事件的处理器所引发的新领域事件。

### 课程笔记

#### The Aggregate Root and Domain Events

领域事件模式的基础在于 `AggregateRoot`。
每个聚合都维护一个 `IDomainEvent` 对象列表。
为了确保事件只被处理一次,`PopDomainEvents` 方法会创建当前事件的一份副本,清空内部列表,并把副本返回给调用方(通常是数据库上下文)。

```csharp
namespace DomeGym.Domain.Common;

public abstract class AggregateRoot : Entity
{
    protected AggregateRoot(Guid id) : base(id)
    {
    }

    protected readonly List<IDomainEvent> _domainEvents = new();

    public List<IDomainEvent> PopDomainEvents()
    {
        var copy = _domainEvents.ToList();
        _domainEvents.Clear();

        return copy;
    }

    protected AggregateRoot() { }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-domain-event-sausage-factory-54123392/?t=55)

`IDomainEvent` 接口既充当标记,也是对 MediatR `INotification` 的包装。
这使得任何领域事件都可以通过 MediatR 管道发布到各自的处理器。

```csharp
using MediatR;

namespace DomeGym.Domain.Common;

public interface IDomainEvent : INotification
{
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-domain-event-sausage-factory-54123392/?t=70)

#### The Eventual Consistency Middleware

为了管理领域事件和事务的生命周期,使用了一个自定义中间件。
该中间件在请求开始时开启一个数据库事务。
然后它使用 `context.Response.OnCompleted` 注册一个回调,该回调在用户收到响应之后执行。

在这个回调中,中间件从 `HttpContext` 中取出领域事件队列。
它遍历该队列,通过 MediatR 发布每个事件。
如果所有事件及其后续副作用(可能会向队列中添加更多事件)都处理成功,事务就会提交。
如果发生 `EventualConsistencyException`,可以由一个有韧性的错误处理机制捕获并处理。
事务在 `finally` 块中被释放;如果由于错误而从未调用 `CommitAsync`,更改会被自动回滚。

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

    await _next(context);
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-domain-event-sausage-factory-54123392/?t=235)

#### Database Context Integration

`DbContext` 负责在保存过程中从聚合中提取领域事件。
通过重写 `SaveChangesAsync`,上下文使用 `ChangeTracker` 找出所有被修改的 `AggregateRoot` 实体,并对它们调用 `PopDomainEvents`。

在调用基类的 `SaveChangesAsync`(持久化主要更改)之后,提取出的事件会被添加到存储在 `HttpContext.Items` 中的一个队列里。
如果队列尚不存在,就创建一个。
这确保即使事件处理器触发了再次调用 `SaveChangesAsync` 的进一步更改,所有由此产生的事件也都会被追加到同一个队列中,交由中间件处理。

```csharp
public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
{
    var domainEvents = ChangeTracker.Entries<AggregateRoot>()
        .Select(entry => entry.Entity.PopDomainEvents())
        .SelectMany(x => x)
        .ToList();

    var result = await base.SaveChangesAsync(cancellationToken);

    Queue<IDomainEvent> domainEventsQueue = _httpContextAccessor.HttpContext.Items.TryGetValue(EventualConsistencyMiddleware.DomainEventsKey, out var value) &&
        value is Queue<IDomainEvent> existingDomainEvents
        ? existingDomainEvents
        : new();

    domainEvents.ForEach(domainEventsQueue.Enqueue);
    _httpContextAccessor.HttpContext.Items[EventualConsistencyMiddleware.DomainEventsKey] = domainEventsQueue;
    return result;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-domain-event-sausage-factory-54123392/?t=250)

```text
EventualConsistencyMiddleware.InvokeAsync
  |
  +-- dbContext.Database.BeginTransactionAsync()
  +-- context.Response.OnCompleted(callback)      (注册回调)
  +-- await _next(context)
        |
        +-- SaveChangesAsync()
              |-- ChangeTracker.Entries<AggregateRoot>() -> PopDomainEvents()
              |-- base.SaveChangesAsync()
              +-- Enqueue -> HttpContext.Items[DomainEventsKey]
                                 (Queue<IDomainEvent>)
  ... 用户收到响应 ...
  callback:
    while (domainEvents.TryDequeue(out var nextEvent))
        publisher.Publish(nextEvent)
            +-- 处理器 -> SaveChangesAsync() -> 新事件追加到同一队列
    transaction.CommitAsync()
    catch (EventualConsistencyException e)
    finally: transaction.DisposeAsync()
```

## 6. Implementing a Domain Event

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-a-domain-event-54123393/) · 6:41

### 总结

本课演示如何从命令处理器中的手动编排过渡到使用领域事件的领域驱动方式。
通过把持久化 subscription 的逻辑从 CreateSubscriptionCommandHandler 中移出,放进一个专门的事件处理器,系统实现了更好的关注点分离,并支持最终一致性。
这个过程包括在聚合中定义一个领域事件、在状态变化时引发它,以及利用基础设施在主事务提交之后发布并处理该事件。

### 核心概念

- **Domain Events(领域事件)**:捕获聚合内的重要状态变化以触发副作用。
- **Orchestration vs. Choreography(编排与协同)**:从由一个中心处理器管理多个实体,转变为由实体把变化通知给系统的事件驱动流程。
- **Eventual Consistency(最终一致性)**:在主请求已经响应用户之后再执行副作用(例如保存一个相关实体)。
- **MediatR Integration(MediatR 集成)**:利用 `INotification` 和 `INotificationHandler` 把事件的生产者与其消费者解耦。
- **Infrastructure Interception(基础设施拦截)**:重写 `SaveChangesAsync` 来捕获领域事件,并把它们存储在 `HttpContext` 中以便在响应后处理。

### 课程笔记

最初,`CreateSubscriptionCommandHandler` 通过手动保存 `Admin` 和 `Subscription` 两个实体来执行编排。
这让命令处理器与多个仓储紧密耦合,并在单个事务内确保即时一致性,而这未必总是期望的架构方式。

```csharp
public async Task<ErrorOr<Subscription>> Handle(CreateSubscriptionCommand command, CancellationToken cancel
{
    var admin = await _adminsRepository.GetByIdAsync(command.AdminId);

    if (admin is null)
    {
        return Error.NotFound(description: "Admin not found");
    }

    if (admin.SubscriptionId is not null)
    {
        return Error.Conflict(description: "Admin already has active subscription");
    }

    var subscription = new Subscription(command.SubscriptionType, command.AdminId);
    admin.SetSubscription(subscription);

    await _adminsRepository.UpdateAsync(admin);
    await _subscriptionsRepository.AddSubscriptionAsync(subscription);

    return subscription;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-a-domain-event-54123393/?t=10)

为了转向事件驱动模型,我们首先定义一个领域事件。
在 `AdminAggregate` 中,我们创建一个 `SubscriptionSetEvent` record。
这个 record 实现 `IDomainEvent` 接口,并携带必要的数据:`Admin` 聚合以及被设置的 `Subscription`。

```csharp
namespace DomeGym.Domain.AdminAggregate.Events;

public record SubscriptionSetEvent(Admin Admin, Subscription Subscription) : IDomainEvent;
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-a-domain-event-54123393/?t=65)

接下来,更新 `Admin` 聚合以引发这个事件。
在 `SetSubscription` 方法中,更新内部状态之后,把该事件添加到从 `AggregateRoot` 基类继承来的 protected `_domainEvents` 列表中。

```csharp
public class Admin : AggregateRoot
{
    public Guid UserId { get; }
    public Guid? SubscriptionId { get; private set; }

    public Admin(
        Guid userId,
        Guid? subscriptionId = null,
        Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        UserId = userId;
        SubscriptionId = subscriptionId;
    }

    public ErrorOr<Success> SetSubscription(Subscription subscription)
    {
        if (SubscriptionId.HasValue)
        {            return Error.Conflict(description: "Admin already has an active subscription");
        }

        SubscriptionId = subscription.Id;

        _domainEvents.Add(new SubscriptionSetEvent(this, subscription));

        return Result.Success;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-a-domain-event-54123393/?t=85)

既然事件由聚合引发,就可以重构 `CreateSubscriptionCommandHandler`。
对 `_subscriptionsRepository.AddSubscriptionAsync` 的直接调用被移除,处理器只负责更新 `Admin` 聚合。
subscription 的持久化现在以最终一致的方式处理。

```csharp
public async Task<ErrorOr<Subscription>> Handle(CreateSubscriptionCommand command, CancellationToken cance
{
    var admin = await _adminsRepository.GetByIdAsync(command.AdminId);

    if (admin is null)
    {
        return Error.NotFound(description: "Admin not found");
    }

    if (admin.SubscriptionId is not null)
    {
        return Error.Conflict(description: "Admin already has active subscription");
    }

    var subscription = new Subscription(command.SubscriptionType, command.AdminId);
    admin.SetSubscription(subscription);

    await _adminsRepository.UpdateAsync(admin);

    return subscription;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-a-domain-event-54123393/?t=130)

为了处理这个副作用,在应用层中实现了一个 `SubscriptionSetEventHandler`。
这个类实现 MediatR 的 `INotificationHandler<SubscriptionSetEvent>`。
当事件被发布时,这个处理器使用 `ISubscriptionsRepository` 来持久化 subscription。

```csharp
using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.AdminAggregate.Events;
using MediatR;

namespace DomeGym.Application.Subscriptions.Events;

public class SubscriptionSetEventHandler : INotificationHandler<SubscriptionSetEvent>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;

    public SubscriptionSetEventHandler(ISubscriptionsRepository subscriptionsRepository)
    {
        _subscriptionsRepository = subscriptionsRepository;
    }

    public async Task Handle(SubscriptionSetEvent notification, CancellationToken cancellationToken)
    {
        await _subscriptionsRepository.AddSubscriptionAsync(notification.Subscription);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-a-domain-event-54123393/?t=175)

```text
CreateSubscriptionCommandHandler
  |-- admin.SetSubscription(subscription)
  |     +-- _domainEvents.Add(new SubscriptionSetEvent(this, subscription))
  +-- _adminsRepository.UpdateAsync(admin)
        ...
SubscriptionSetEvent  --publish-->  SubscriptionSetEventHandler
                                      +-- _subscriptionsRepository
                                            .AddSubscriptionAsync(
                                               notification.Subscription)
```

基础设施通过在数据库保存过程中拦截事件来促成这一流程。
`DbContext` 重写 `SaveChangesAsync`,使用 `PopDomainEvents()` 从所有被跟踪的 `AggregateRoot` 实体中提取领域事件。
然后这些事件被放入存储在 `HttpContext.Items` 中的一个队列里,留待稍后处理。

```csharp
public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
{
    var domainEvents = ChangeTracker.Entries<AggregateRoot>()
        .Select(entry => entry.Entity.PopDomainEvents())
        .SelectMany(x => x)
        .ToList();

    var result = await base.SaveChangesAsync(cancellationToken);

    Queue<IDomainEvent> domainEventsQueue = _httpContextAccessor.HttpContext.Items.TryGetValue(EventualConsistencyMiddleware.DomainEventsKey, out var value) &&
        value is Queue<IDomainEvent> existingDomainEvents
            ? existingDomainEvents
            : new();

    domainEvents.ForEach(domainEventsQueue.Enqueue);
    _httpContextAccessor.HttpContext.Items[EventualConsistencyMiddleware.DomainEventsKey] = domainEventsQueue;
    
    return result;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-a-domain-event-54123393/?t=250)

最后,`EventualConsistencyMiddleware` 确保这些事件只在主响应已发送给用户之后才被发布。
它挂接到 `context.Response.OnCompleted`,把事件出队,并通过 MediatR 的 `IPublisher` 发布它们。

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
                {                    await publisher.Publish(nextEvent);
                }
            }

            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            // Handle eventual consistency exceptions
        }
    });

    await _next(context);
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-a-domain-event-54123393/?t=325)

## 7. Section Recap

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/section-recap-54123394/) · 1:23

### 总结

本课总结了如何把领域层集成到一个完整的应用架构中,强调从孤立的领域逻辑过渡到可用的表示层和应用层。
它强化了增量式领域建模的做法(只在需要时才添加属性),并详细说明了使用领域事件模式实现最终一致性:事件在领域操作期间被捕获,并在用户收到响应之后被分发。

### 核心概念

- Layered Integration(分层集成):用应用逻辑和表示逻辑包裹领域层,以支持 Web API 或 WPF 应用等多种客户端。
- Incremental Modeling(增量建模):采用 YAGNI(You Ain't Gonna Need It)方式,只在需要时才向领域模型添加属性。
- Consistency Models(一致性模型):理解事务一致性与最终一致性之间的区别。
- Domain Event Pattern(领域事件模式):把事件存储在实体内的一个列表中,并在主要操作之后依次调用处理器。

### 课程笔记

领域层虽然常常被单独开发和测试,但它是一个更大系统的核心。
为了让用户能够访问领域逻辑,必须用应用层和表示层把它包裹起来。
这种结构让同一套领域逻辑可以在不同类型的应用中使用,例如 Web API 或 WPF 这样的桌面界面。

领域驱动设计的一个关键方面是领域模型的演进。
不应基于猜测向模型添加属性。
相反,应当在实现应用功能的过程中识别出需要时,再增量地引入它们。
这确保领域保持专注,不掺杂不必要的复杂性。

领域事件的实现促成了从严格的事务一致性向最终一致性的转变。
通过在业务流程中把领域事件捕获到聚合或实体内部的一个列表中,系统可以推迟副作用。
一旦主要操作完成且用户已收到响应,这些事件就会被依次处理。
这种方式把核心业务逻辑与通知或外部系统更新等次要关注点解耦。
