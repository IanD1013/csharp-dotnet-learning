# Aggregate Design In Action

> 课程:[Deep Dive: Domain-Driven Design](https://dometrain.com/course/deep-dive-domain-driven-design-ddd/) · 第 10 章
> 共 17 课 · 约 1:04:19
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Aggregate Design In Action - Step 1](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/aggregate-design-in-action-step-1-54855946/) | 2:06 | [↓](#1-aggregate-design-in-action---step-1) |
| 2 | [Aggregate Design In Action - Step 2](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/aggregate-design-in-action-step-2-54855947/) | 5:56 | [↓](#2-aggregate-design-in-action---step-2) |
| 3 | [Aggregate Design In Action - Step 3](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/aggregate-design-in-action-step-3-54855948/) | 2:04 | [↓](#3-aggregate-design-in-action---step-3) |
| 4 | [Aggregate Design In Action - Step 4 Setup](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/aggregate-design-in-action-step-4-setup-54855949/) | 2:02 | [↓](#4-aggregate-design-in-action---step-4-setup) |
| 5 | [Merging Aggregates in the User Management Context](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/merging-aggregates-in-the-user-management-context-54855950/) | 5:43 | [↓](#5-merging-aggregates-in-the-user-management-context) |
| 6 | [Merging Aggregates in the Gym Management Context](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/merging-aggregates-in-the-gym-management-context-54855951/) | 10:41 | [↓](#6-merging-aggregates-in-the-gym-management-context) |
| 7 | [Merging Aggregates in the Session Management Context - The Chain Effect of Merging Aggregates](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/merging-aggregates-in-the-session-management-context-the-chain-effect-of-merging-aggregates-54855952/) | 4:05 | [↓](#7-merging-aggregates-in-the-session-management-context---the-chain-effect-of-merging-aggregates) |
| 8 | [Merging Aggregates in the Session Management Context - Identifying Missing Domain Objects](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/merging-aggregates-in-the-session-management-context-identifying-missing-domain-objects-54855953/) | 6:01 | [↓](#8-merging-aggregates-in-the-session-management-context---identifying-missing-domain-objects) |
| 9 | [Merging Aggregates in the Session Management Context Continued](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/merging-aggregates-in-the-session-management-context-continued-54855954/) | 2:23 | [↓](#9-merging-aggregates-in-the-session-management-context-continued) |
| 10 | [Merging Aggregates in the Session Management Context - Propagating Data Across Bounded Contexts](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/merging-aggregates-in-the-session-management-context-propagating-data-across-bounded-contexts-54855955/) | 3:49 | [↓](#10-merging-aggregates-in-the-session-management-context---propagating-data-across-bounded-contexts) |
| 11 | [Merging Aggregates Across Bounded Contexts](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/merging-aggregates-across-bounded-contexts-54855956/) | 4:50 | [↓](#11-merging-aggregates-across-bounded-contexts) |
| 12 | [Merging Aggregates in the Session Management Context - Continued](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/merging-aggregates-in-the-session-management-context-continued-54855957/) | 1:57 | [↓](#12-merging-aggregates-in-the-session-management-context---continued) |
| 13 | [Aggregate Design Draft Reflection](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/aggregate-design-draft-reflection-54855958/) | 4:29 | [↓](#13-aggregate-design-draft-reflection) |
| 14 | [Merging Aggregates Based On Eventual Consistency Constraints](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/merging-aggregates-based-on-eventual-consistency-constraints-54855959/) | 1:12 | [↓](#14-merging-aggregates-based-on-eventual-consistency-constraints) |
| 15 | [Evolving Aggregate Design With Time or Refactoring Existing Aggregate Designs](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/evolving-aggregate-design-with-time-or-refactoring-existing-aggregate-designs-54855960/) | 3:19 | [↓](#15-evolving-aggregate-design-with-time-or-refactoring-existing-aggregate-designs) |
| 16 | [Converting Aggregate Design To Code](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/converting-aggregate-design-to-code-54855961/) | 2:32 | [↓](#16-converting-aggregate-design-to-code) |
| 17 | [Section Recap](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/section-recap-54855962/) | 1:10 | [↓](#17-section-recap) |

## 1. Aggregate Design In Action - Step 1

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/aggregate-design-in-action-step-1-54855946/) · 2:06

### 总结

本课通过识别各个限界上下文中的领域实体并对其分类,开启聚合设计的实践过程。
在前面上下文映射环节的基础上,这一步需要把具体的领域模型(例如 Users、Subscriptions、Gyms 和 Sessions)分别列在 User Management、Gym Management 和 Session Reservation 上下文之下,为定义聚合边界打下基础。

### 核心概念

- 从上下文映射过渡到聚合设计。
- 按限界上下文(Bounded Context)对领域模型进行分类。
- 识别跨上下文的实体关系和触发点。
- 建立一份完整的实体清单,作为定义聚合边界的第一步。

### 课程笔记

聚合设计的过程始于系统地识别系统各个限界上下文中的实体。
这一步把上下文映射期间识别出的高层领域模型转化为一份结构化的清单,作为定义聚合根和边界的基础。

#### User Management Context

在 User Management 上下文中,主要实体是 **User**。
从这个实体派生出具体的档案(profile):
- **Admin**
- **Participant**
- **Trainer**

#### Gym Management Context

Gym Management 上下文与 User Management 共享一些概念上的实体,但关注的是管理和结构方面的问题。
当在 User Management 上下文中创建一个 Admin 时,这里会建立一个对应的 **Admin** 实体。
层级如下:
- **Admin**:管理订阅。
- **Subscription**:包含健身房。
- **Gym**:包含房间。
- **Room**:进行课程(session)的物理空间。

#### Session Reservation Context

Session Reservation 上下文处理排期和预订逻辑。
这里的实体往往是由 Gym Management 上下文中的操作触发的。
例如,在 Gym Management 中创建一个房间,会在这里产生一个对应的 **Room** 实体。
实体包括:
- **Room**:承载课程。
- **Session**:一个具体的已排期活动。
- **Trainer**:被分配到一节课程。
- **Participant**:参加一节课程。
- **Reservation**:参与者与课程之间的关联。

```text
User Management        Gym Management         Session Reservation
---------------        --------------         -------------------
User                   Admin                  Room
  Admin                  Subscription         Session
  Participant              Gym                Trainer
  Trainer                    Room             Participant
                                              Reservation
```

这种结构化的识别确保在继续定义聚合的事务边界和不变量之前,所有领域模型都已被考虑到。

## 2. Aggregate Design In Action - Step 2

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/aggregate-design-in-action-step-2-54855947/) · 5:56

### 总结

聚合设计过程的第 2 步是映射实体之间的关系,以识别它们必须如何交互才能强制执行业务不变量。
通过建立这些联系(例如 User 引用它的各个档案,或 Subscription 引用它的各个 Gym),领域模型可以在相关对象之间保持一致性。
这种映射是定义聚合边界的前提,确保每个聚合都拥有校验其内部状态和业务规则所需的信息。

### 核心概念

- **Invariant Enforcement(不变量强制执行)**:建立关系主要是为了让一个实体能够校验业务规则(例如 User 在创建新档案之前检查是否已有档案)。
- **Point of Truth(真相之源)**:实体充当涉及其相关实体的操作的权威,以确保满足领域约束。
- **Unidirectional vs. Bidirectional(单向与双向)**:关系按需定义;除非领域逻辑明确要求,否则不需要是双向的。
- **Relationship Hierarchy(关系层级)**:映射从高层实体(User、Admin)到细粒度实体(Session、Reservation)的流向。

### 课程笔记

聚合设计的第二步是映射上一步识别出的实体之间的关系。
这个过程是由一项需求驱动的:识别哪些实体需要引用其他实体才能强制执行领域不变量。

#### User and Profile Invariants

**User** 实体管理它与 **Trainer**、**Admin** 和 **Participant** 档案之间的关系。
这是强制执行"一个用户每种档案类型最多只能有一个"这一不变量所必需的。
通过持有对这些档案的引用,User 领域模型可以在创建档案的操作中检查自己的内部状态。
如果引用已经存在,模型就会阻止创建重复的档案,从而使 User 保持为档案管理的"真相之源"。

#### Subscription and Gym Limits

**Admin** 实体持有对其所创建的 **Subscription** 的引用。
**Subscription** 实体则维护与多个 **Gyms** 的关系。
这种结构让 Subscription 能够强制执行与容量相关的不变量,例如根据具体的订阅等级限制可以创建的健身房数量。

#### Physical and Operational Hierarchy

关系映射沿着健身房系统的组织结构继续进行:
- **Gyms** 引用多个 **Rooms**。
- **Rooms** 引用多个 **Sessions**。
- **Sessions** 引用一个 **Trainer** 和多个 **Reservations**。注意 Session 并不直接引用 Participant;它引用的是 **Reservation** 实体。

#### Participant and Trainer Activity

- **Trainers** 可以被分配到多个 **Sessions**。
- **Participants** 可以持有多个 **Reservations**。
- **Reservations** 同时与 **Session** 和 **Participant** 保持关系。

```text
User --> Trainer, Admin, Participant
Admin --> Subscription --> Gym (many) --> Room (many) --> Session (many)
Session --> Trainer (one)
Session --> Reservation (many)
Trainer --> Session (many)
Participant --> Reservation (many)
Reservation --> Session, Participant
```

#### Design Considerations

领域驱动设计中的关系是由逻辑驱动的,而不是由数据库模式的需求驱动的。
它们不必是双向的。
例如,虽然 User 引用 Trainer,但 Trainer 并不一定需要引用 User。
目标是达到一种稳定的状态,使所有不变量都可以通过已定义的关系来强制执行。
这种映射是一个迭代式的起点,会在下一步建立聚合边界时得到细化。

## 3. Aggregate Design In Action - Step 3

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/aggregate-design-in-action-step-3-54855948/) · 2:04

### 总结

在聚合设计的第三步,边界内的所有实体都被提升为独立的聚合。
这一转变要求遵守"聚合之间只通过 ID 而不是直接的对象引用相互引用"这条规则。
通过用对应的 ID(值对象)替换被封装的实体,模型确保每个聚合维持自己的一致性边界,同时仍允许领域中存在逻辑上的关系。

### 核心概念

* **Entity Promotion(实体提升)**:把内部实体转换为独立的聚合。
* **Reference by ID(通过 ID 引用)**:DDD 的基本规则,一个聚合只应通过标识符引用另一个聚合的根。
* **Aggregate Boundaries(聚合边界)**:确保先前封装在某个实体内部的对象被解耦,作为各自的聚合根独立存在。
* **Value Object References(值对象引用)**:使用 ID(通常是 GUID)作为值对象来表示不同聚合根之间的关联。

### 课程笔记

聚合设计过程的第三步是把所有实体标记为独立的聚合。
在此之前,模型中可能有一个 `User` 实体,它封装了其他实体,例如 `Trainer`、`Admin` 和 `Participant`。
在那个初始状态下,这些实体被当作嵌套在 `User` 对象中的内部对象。

为了遵循正确的 DDD 原则,这些内部实体被提升为各自的聚合根。
一旦这些实体被重新定义为聚合,它们之间的关系就必须改变。
聚合设计的一条核心规则是:聚合只通过 ID 引用其他聚合。
因此,`User` 聚合不能再包含实际的 `Trainer`、`Admin` 或 `Participant` 对象。

取而代之的是,`User` 聚合根被更新为只存储这些其他聚合的标识符(ID)。
这些 ID 在 `User` 聚合内被当作值对象。
这在聚合之间建立了有效的关系,而不会破坏它们各自边界的封装。
在代码实现中,这会使 `User` 聚合根包含三个 GUID,分别作为对 `Trainer` 聚合根、`Admin` 聚合根和 `Participant` 聚合根的引用。

```text
Before                         After
+------------------+           +------------------+
| User             |           | User (root)      |
|   Trainer        |           |   Guid           |---> Trainer (root)
|   Admin          |           |   Guid           |---> Admin (root)
|   Participant    |           |   Guid           |---> Participant (root)
+------------------+           +------------------+
```

## 4. Aggregate Design In Action - Step 4 Setup

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/aggregate-design-in-action-step-4-setup-54855949/) · 2:02

### 总结

本课通过对照当前设计评估业务不变量,为细化聚合边界打下基础。
它引入了合并聚合的概念,也就是把独立的聚合转变为一个更大边界内的本地实体,以确保保持一致性所需的所有数据都能在单个事务中获得。
此外,本课通过识别哪些不变量(例如与自动支付和器材跟踪相关的不变量)将被推迟,界定了 P0 实现的范围。

### 核心概念

*   **Merging Aggregates(合并聚合)**:把多个聚合合并为一个,以确保特定业务规则的原子一致性的过程。
*   **Local Entities(本地实体)**:当一个聚合被合并进另一个聚合时,它就失去了独立聚合的地位,成为新聚合边界内的一个本地实体。
*   **Invariant Enforcement(不变量强制执行)**:聚合设计的首要驱动因素;边界必须包含校验业务规则所需的全部数据。
*   **P0 Scope Refinement(P0 范围细化)**:识别哪些不变量对首次发布至关重要,哪些(例如自动支付或器材跟踪)可以推迟。

### 课程笔记

这一阶段的目标是确保当前的聚合设计能够严格强制执行所有已识别的业务不变量。
在最初的设计中,`User` 聚合通过唯一标识符(ID)引用 `Trainer`、`Admin` 和 `Participant` 档案。
然而,某些业务规则要求这些实体之间具有原子一致性,因此有必要重新评估这些边界。

#### Merging Aggregates

当一个不变量横跨多个聚合时,可能需要合并这些聚合以维持一个一致性边界。
例如,如果系统必须强制执行一条同时涉及用户凭据和教练专属数据的规则,`User` 聚合就可以被重新设计为直接包含 `Trainer` 数据。
在这种情况下,`User` 不再存储 `TrainerId` 引用;取而代之的是,`Trainer` 从一个独立的聚合降级为位于 `User` 聚合边界内的本地实体。

```text
Before                              After
User (root)                         User (root)
  TrainerId ---> Trainer (root)       Trainer (local entity)
```

#### Invariant Scoping for P0

为了简化最初的实现(P0),不变量清单已经过细化,排除了首次发布不需要的功能:

*   **Feedback Validity Window(反馈有效时间窗)**:可以提交反馈的时间范围在 P0 中不会被强制执行。
*   **Payment Method Validity(支付方式有效性)**:由于参与者将在课程开始时当面付款,系统不需要在软件的预订过程中校验支付方式。
*   **Equipment Availability(器材可用性)**:跟踪不同房间之间器材的物理可用性被排除在当前范围之外。

所有剩余的不变量都按负责强制执行它们的领域模型进行分组。
评估过程从 User Management 限界上下文开始,以确定当前的聚合边界是否足够,还是需要进一步合并。

## 5. Merging Aggregates in the User Management Context

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/merging-aggregates-in-the-user-management-context-54855950/) · 5:43

### 总结

本课通过用各种业务不变量来检验,评估为 User Management 限界上下文提出的聚合设计。
重点是判断 User 和 Trainer 这样的聚合是否需要合并以保持一致性。
通过分析"Trainer qualification compliance"(教练资质合规)这一不变量,本课说明 User 聚合可以借助外部校验(例如一份签名证书)独立地管理 Trainer 档案的创建,从而在不牺牲业务规则或数据完整性的前提下保持聚合边界。

### 核心概念

- 针对聚合边界评估不变量。
- 在 User 聚合内处理档案创建。
- 使用外部证明(例如签名证书)来满足业务规则。
- 在聚合根层面管理并发和竞态条件。
- 不变量的上下文划分(User Management、Session Reservation 与 Gym Management)。

### 课程笔记

聚合的设计必须用系统的业务不变量来验证,以确保能够在不引入不必要复杂性的前提下保持一致性。
在 User Management 上下文中,大多数已识别的不变量实际上属于其他限界上下文,例如 Session Reservation 或 Gym Management。

#### Invariants Outside the User Management Context

有几个不变量由 **Session Reservation** 上下文管理:
- **Participant session conflict prevention(参与者课程冲突预防)**:确保参与者不会预订时间重叠的课程。
- **Participant age verification(参与者年龄验证)**:校验参与者满足某节课程的最低年龄要求。
- **Trainer schedule conflicts(教练日程冲突)**:防止教练被重复预订。
- **Trainer to session ratio(教练与课程比例)**:强制执行教练每天可以带领的课程数量的法定上限。

其他不变量由 **Gym Management** 上下文管理:
- **Subscription capacity limits(订阅容量上限)**:根据订阅等级限制创建的健身房数量。
- **Gym room capacity limits(健身房房间容量上限)**:限制每个健身房的房间数量。
- **Room session limits(房间课程上限)**:限制每个房间每天的课程数量。
- **Session overlap prevention(课程重叠预防)**:确保同一房间内的课程不会重叠。

#### Trainer Qualification Compliance

与 User Management 上下文相关的主要不变量是 **Trainer Qualification Compliance(教练资质合规)**。
这条规则规定,用户只有在获得认证的情况下才能创建教练档案。

为了强制执行这一点,`User` 聚合充当真相之源。
当用户尝试成为教练时,他们向领域模型提供一份签名证书。
`User` 聚合暴露一个创建教练档案的方法,该方法接受这份证书作为参数。
如果证书有效,用户的状态就会转变为包含一个教练档案。

这种设计避免了合并 `User` 和 `Trainer` 聚合,因为 `User` 聚合在创建的那一刻拥有做出决定所需的全部信息。
一旦教练档案被创建、`User` 聚合被持久化,它就成为关于该用户是否拥有教练档案的唯一真相之源。

#### Handling Race Conditions

聚合边界还能防范竞态条件。
如果用户同时两次尝试创建教练档案,持久化层会确保原子性。
只有第一个用新的教练档案 ID 更新 `User` 聚合的请求会成功;第二个请求会因并发检查而失败,从而维护该不变量的完整性。

基于这一分析,User Management 当前的聚合设计足以强制执行所有相关的不变量,无需进一步合并。

## 6. Merging Aggregates in the Gym Management Context

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/merging-aggregates-in-the-gym-management-context-54855951/) · 10:41

### 总结

本课评估在 Gym Management 上下文中强制执行容量不变量时涉及的架构权衡,具体聚焦于健身房和房间的数量限制。
通过分析这些业务规则的数据需求,本课说明了为什么把 Gym 实体合并进 Subscription 聚合是更优的设计选择,因为它提供了原子一致性所需的数据局部性,而没有管理最终一致性或复杂的多聚合交互的开销。

### 核心概念

- Invariant Enforcement(不变量强制执行):在状态变更期间确保业务规则得到满足的过程。
- Aggregate Root Selection(聚合根选择):确定哪个实体应当管理一组对象的一致性。
- Data Locality(数据局部性):要求一个聚合包含做出业务决策所需的全部信息。
- Aggregate Merging(聚合合并):把分离的聚合合并为一个,以简化跨实体不变量的强制执行。
- Eventual Consistency(最终一致性):一种不同聚合之间的数据随时间推移变得一致、而非立即一致的模型。

### 课程笔记

在 Gym Management 上下文中,设计必须考虑那些根据用户订阅计划限制健身房和房间数量的不变量。
有几个不变量(例如参与者预订和教练分配)被排除在这个上下文之外,因为它们属于 Session Reservation 限界上下文。

#### Subscription Gym Limits

第一个相关的不变量规定,一个订阅包含的健身房数量不能超过其类型所允许的数量。
这里存在一个设计选择:由 Admin 聚合还是 Subscription 聚合来强制执行它。
如果使用 Admin 聚合,它将管理健身房的创建。
然而,把 Subscription 当作真相之源更为有效。
通过实现一个诸如 `subscription.AddGym()` 的方法,该聚合可以检查自己的 `SubscriptionType` 和当前的健身房 ID 列表,以判断是否已达到上限。

#### Gym Room Capacity Limits

一个更复杂的挑战是限制健身房内房间数量的不变量。
Gym 聚合目前只持有一个 `SubscriptionId`,这不足以确定房间上限,因为它不包含 `SubscriptionType` 数据。

有三种策略可以解决这种数据不足:

1. **Passing Aggregates(传入聚合)**:应用层可以获取 Subscription 聚合,并把它传入一个 `gym.AddRoom(subscription)` 方法。这要求领域模型校验该健身房确实属于这个订阅,给实现逻辑带来了很大负担,并且一次操作需要获取多个聚合。
2. **Data Duplication (Eventual Consistency)(数据复制(最终一致性))**:Gym 聚合可以存储一份 `SubscriptionType` 的副本或一个 `MaxRooms` 整数。每当订阅计划发生变化时,这些数据会通过领域事件进行更新。然而,这引入了一个最终一致性的时间窗口。例如,如果管理员升级了他们的计划,Gym 聚合反映新容量之前可能会有延迟,导致管理员在升级后无法立即创建新房间。
3. **Merging Aggregates(合并聚合)**:Gym 从自身作为聚合根转变为 Subscription 聚合内的一个本地实体。

#### Final Design Decision

对这个领域来说,最自然的解决方案是合并 Gym 和 Subscription 聚合。
通过让 Subscription 成为健身房和房间两者的聚合根,它获得了原子地强制执行所有容量上限所需的数据局部性。
在这个设计中,Subscription 聚合成为 Gym Management 上下文中健身房和房间管理的唯一真相之源,确保所有上限都得到遵守,而无需应对多聚合协调的复杂性。

```text
Subscription (root)
  SubscriptionType
  Gym (local entity)
    Room
```

## 7. Merging Aggregates in the Session Management Context - The Chain Effect of Merging Aggregates

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/merging-aggregates-in-the-session-management-context-the-chain-effect-of-merging-aggregates-54855952/) · 4:05

### 总结

本课探讨强制执行参与者课程冲突预防不变量对设计的影响,该不变量规定参与者不能有时间重叠的预订。
为了在不依赖外部服务或延迟加载的情况下在领域模型内维持这一不变量,Reservation 聚合被合并进 Participant 聚合,成为一个嵌套实体。
这一改变引发了"连锁效应",需要移除多余的 ID,并更新 Session 聚合,使其直接引用 Participant 聚合,而不是引用现在已被嵌套的 Reservation 实体。

### 核心概念

* **Participant Session Conflict Prevention(参与者课程冲突预防)**:确保参与者不会在同一时间被预订到两节课程的不变量。
* **Aggregate Merging(聚合合并)**:把一个独立的聚合变成另一个聚合内的嵌套实体以简化不变量强制执行的过程。
* **Chain Effect of Merging(合并的连锁效应)**:当一个被引用的聚合变成嵌套实体时,相关聚合所需要的结构性改变。
* **Encapsulation of Invariants(不变量的封装)**:把逻辑移入聚合,以避免基础校验对仓储或领域服务产生复杂的依赖。

### 课程笔记

本课聚焦于"participant session conflict prevention"(参与者课程冲突预防)不变量,它确保参与者不会在两节时间安排重叠的课程中预订名额。
因为一个参与者同一时间只能在一节课程中,系统必须用已有的预订来校验新的预订。

在最初的设计中,Participant 聚合只持有其预订的 ID。
在这种状态下强制执行冲突不变量,需要 Participant 从仓储中获取完整的预订数据(要么通过领域服务,要么通过"disconnected domain model"(断开的领域模型)模式,即延迟加载),以比较开始时间、结束时间和日期。
这种做法引入了架构上的复杂性,并要求实现者在执行领域逻辑期间管理数据获取。

一个更内聚的设计是把 Reservation 聚合合并进 Participant 聚合。
在这个新结构中,Reservation 成为 Participant 内的一个嵌套实体。
这让 Participant 聚合能够完整访问其预订的细节(例如开始时间、结束时间和日期),从而在内部强制执行该不变量,而无需外部依赖。

合并这些聚合会在整个限界上下文中造成"连锁效应":

1. **Redundant IDs(多余的 ID)**:由于 Reservation 现在是 Participant 聚合的一部分,Reservation 之前持有的 ParticipantId 不再必要,可以移除。
2. **Reference Rules(引用规则)**:领域驱动设计的原则规定,外部聚合永远不应直接引用嵌套实体;它们只应引用聚合根。之前,Session 聚合持有对 Reservation ID 的引用。因为 Reservation 现在是嵌套实体,Session 聚合必须更新为引用其父级 Participant 聚合。

```text
Before                                After
Session --> ReservationId             Session --> ParticipantId
Participant --> ReservationId         Participant (root)
Reservation (root)                      Reservation (nested entity)
  ParticipantId                           (ParticipantId removed)
```

这一转变带来了一个有效的领域状态,其中不变量由聚合根自然地强制执行,不过这需要调整 Session、Participant 以及现在已被嵌套的 Reservation 实体之间的关系。

## 8. Merging Aggregates in the Session Management Context - Identifying Missing Domain Objects

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/merging-aggregates-in-the-session-management-context-identifying-missing-domain-objects-54855953/) · 6:01

### 总结

本课处理在为强制执行业务不变量而把 Reservation 实体合并进 Participant 聚合时出现的聚合膨胀这一设计挑战。
虽然合并让系统能够防止重复预订,但随着参与者累积历史和未来的预订,它有造出一个过大聚合的风险。
为了解决这个问题,本课识别出一个缺失的领域概念,即 Schedule,它让 Participant 能够高效地管理可用性并校验时间段,而无需加载每个 Reservation 的完整细节,从而保持 Participant 聚合小巧,并让 Reservation 能够保持为一个独立、可单独访问的聚合。

### 核心概念

* **Aggregate Bloat(聚合膨胀)**:当一个聚合因为包含一个随时间扩张的实体集合(例如所有过去和未来预订的列表)而变得过大时出现的性能和延迟问题。
* **Invariant Enforcement(不变量强制执行)**:在聚合边界内维护校验业务规则所需的全部数据的必要性,例如确保参与者不会被重复预订。
* **Missing Domain Concepts(缺失的领域概念)**:聚合设计中的技术摩擦往往表明某个具体的业务概念(例如 Schedule)尚未在领域中被显式建模。
* **Decoupled Aggregates(解耦的聚合)**:通过使用 Schedule 来跟踪可用性,Reservation 可以作为拥有唯一 ID 的独立聚合存在,从而可以在不加载 Participant 的情况下直接更新。

### 课程笔记

当前的设计迭代涉及把对预订的引用从 Session 移到 Participant 聚合中。
这一改变让系统能够强制执行"参与者不能有重叠预订"这一关键不变量。
然而,这一设计引入了一个关于 Participant 聚合大小的重大隐患。
如果一个参与者有几十、几百甚至几千个预订,那么每次发生变更时都从数据库加载整个 Participant 对象图,会导致高延迟和不必要的数据传输。

这种技术上的挣扎表明领域模型缺少了一个显式的概念。
在这个语境下,缺失的领域概念是 **Schedule**。
Participant 聚合不应包含所有 Reservation 实体的集合,而应包含一个 Schedule。
Schedule 负责跟踪时间段,并判断用户何时忙碌、何时空闲。

通过把 Schedule 作为 Participant 聚合内的一个实体引入,设计获得了几项改进:

1. **Optimized Aggregate Size(优化的聚合大小)**:Participant 聚合保持小巧,因为它只需要存储日程状态(被占用的时间段),而不是每个历史 Reservation 的完整细节。
2. **Independent Reservation Aggregates(独立的 Reservation 聚合)**:Reservation 现在可以成为拥有唯一 ID 的独立聚合。这让系统可以通过 ID 直接获取和更新某个特定的 Reservation,而无需加载 Participant 聚合及其完整的日程。
3. **Efficient Invariant Validation(高效的不变量校验)**:当创建一个新的预订时,系统取得开始和结束时间,将其与 Participant 的 Schedule 进行比较,如果该时间空闲,就占用该时间段并把 Reservation ID 添加到参与者的列表中。

```text
Participant (root)                     Reservation (root)
  Schedule (entity)                      Id
    blocked time slots
  Reservation IDs  ------------------->  Reservation
```

这一顿悟表明,当一个实现因为性能或复杂性而让人感觉"不对"时,往往说明领域建模过程尚不完整。
显式地对 Schedule 建模,让软件与实际的业务领域保持一致,同时解决了聚合膨胀这一技术问题。

## 9. Merging Aggregates in the Session Management Context Continued

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/merging-aggregates-in-the-session-management-context-continued-54855954/) · 2:23

### 总结

本课详细说明如何在 Session Management 上下文中实现业务不变量,重点是参与者年龄验证和教练排期约束。
它演示了细化聚合边界(例如把日程的概念泛化,以及纳入年龄或每日课程上限这类具体元数据)如何让系统能够直接在聚合内强制执行冲突预防和法定课程比例这样的复杂规则。

### 核心概念

- **Participant Age Verification(参与者年龄验证)**:通过在预订过程中纳入年龄数据,确保参与者满足课程的年龄要求。
- **Schedule Generalization(日程泛化)**:为参与者和教练复用一个通用的 "Schedule" 概念,以防止时间冲突。
- **Trainer Session Ratio(教练课程比例)**:通过在 Trainer 聚合内跟踪分配情况,强制执行教练每天可以带领的课程数量的法定上限。
- **Invariant Enforcement(不变量强制执行)**:在状态变更之前于聚合边界内校验业务规则。

### 课程笔记

#### Participant Age Verification

系统必须确保参与者在预订名额时满足课程的最低年龄要求。
在当前的设计中,这一不变量在 `Reservation` 聚合内强制执行。
通过把参与者的年龄作为预订数据中的一个属性,聚合可以在预订最终确定之前将其与课程的年龄要求进行比较,以校验该请求。

#### Trainer Session Conflict Prevention

与参与者类似,教练也必须避免多节课程重叠的排期冲突。
为了支持这一点,"Schedule" 的概念被泛化了。
日程不再是参与者独有的实体,而是成为一个同时适用于 `Participant` 和 `Trainer` 聚合的可复用组件。
当教练被分配到一节课程时,聚合会接收课程的细节(包括它的时间段),并将其与教练已有的日程进行比较,以确保不存在重叠。

```text
Participant (root)        Trainer (root)
  Schedule                  Schedule
                            maxSessionsPerDay
```

#### Trainer to Session Ratio Enforcement

教练每天可以被分配的最大课程数量通常有法律上或业务上的限制。
为了强制执行这一点,`Trainer` 聚合存储一个 `maxSessionsPerDay` 值。
在分配一节新课程时,聚合接收该课程作为参数,并检查教练在那个具体日期的日程。
如果教练在那一天已经达到允许的最大课程数,操作就会失败,从而维护该业务规则的完整性。

## 10. Merging Aggregates in the Session Management Context - Propagating Data Across Bounded Contexts

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/merging-aggregates-in-the-session-management-context-propagating-data-across-bounded-contexts-54855955/) · 3:49

### 总结

本课探讨强制执行跨限界上下文的业务不变量这一架构挑战,具体来说是根据健身房的订阅类型限制一个房间每天可以承载的课程数量。
由于 Session Reservation 上下文中的 Room 聚合缺少订阅数据,本课提出通过集成事件传播这些信息,并在聚合内维护一个本地日程来校验每日课程数,同时接受最终一致性这一权衡。

### 核心概念

- **Cross-context invariant enforcement(跨上下文的不变量强制执行)**:管理需要来自多个限界上下文(Gym Management 与 Session Reservation)的数据的规则。
- **Data Propagation(数据传播)**:使用集成事件在上下文之间同步必要的状态。
- **Aggregate Enrichment(聚合充实)**:向聚合添加外部元数据(例如订阅上限)和内部跟踪(例如课程日程),以实现本地校验。
- **Eventual Consistency(最终一致性)**:接受一个上下文中的状态变更(订阅升级)与另一个上下文中对它的强制执行之间存在延迟。

### 课程笔记

#### The Invariant Challenge

系统必须强制执行一条规则:一个房间每天的课程数不能超过健身房订阅所允许的数量。
虽然订阅类型决定了容量,但这些数据位于 Gym Management 上下文中,而课程创建是在 Session Reservation 上下文中处理的。
目前,Session Reservation 上下文中的 `Room` 聚合对这些上限一无所知。

在之前的课程中,Gym 和 Subscription 聚合之所以被合并,是因为它们位于同一个上下文中。
然而,因为 `Room`(在 Session Reservation 中)和 `Subscription`(在 Gym Management 中)位于不同的限界上下文,合并它们不是一个直接可行的选项。

#### Proposed Solution: State Propagation

为了在 `Room` 聚合内强制执行该不变量,必须用必要的数据来充实这个聚合。
这涉及对 `Room` 聚合状态的两项主要改变:
1. **Subscription Metadata(订阅元数据)**:`Room` 需要存储 `SubscriptionType`,或者一个表示允许的最大课程数的具体整数。
2. **Daily Schedule(每日日程)**:因为上限是按天计算而不是总数,`Room` 必须跟踪任意给定日期安排了多少节课程。这让 `AddSession` 方法能够针对新课程所在的那一天校验数量。

#### Integration Workflow

数据通过集成事件在上下文之间传播:

1. **Room Creation(房间创建)**:当管理员在 Gym Management 上下文中添加一个房间时,会发布一个 `RoomCreated` 集成事件。这个事件应当包含 `RoomId` 以及当前的订阅上限或类型。
2. **Event Handling(事件处理)**:Session Reservation 上下文监听这个事件。它的内部处理器创建本地的 `Room` 聚合,并用事件中提供的上限对其进行初始化。
3. **Subscription Updates(订阅更新)**:如果健身房的订阅类型被更新(例如从 Free 变为 Basic),Gym Management 上下文必须发出一个 `SubscriptionTypeUpdated` 集成事件。Session Reservation 上下文必须通过更新所有相关 `Room` 聚合的上限来处理这个事件。

```text
Gym Management                         Session Reservation
--------------                         -------------------
add room       --- RoomCreated ------> handler creates Room
                   (RoomId, limit)       (limit from event)

subscription   --- SubscriptionType -> handler updates limits
type updated       Updated               on all relevant Rooms
```

#### Consistency Trade-offs

这种做法依赖于最终一致性。
在订阅更新与该更新传播到 Session Reservation 上下文之间,会有一个短暂的时间窗口。
在这个窗口期间,课程创建可能仍受旧上限的限制。
在大多数领域场景中,为了保持上下文之间清晰的边界,这种延迟是可以接受的权衡。

## 11. Merging Aggregates Across Bounded Contexts

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/merging-aggregates-across-bounded-contexts-54855956/) · 4:50

### 总结

本课探讨一种通过复制并合并聚合来跨限界上下文强制执行不变量的策略。
通过把一个上下文(例如 Session Reservation)中某个聚合的简化版本作为本地实体带入另一个上下文(例如 Gym Management),开发者可以把业务逻辑集中到单个聚合根内。
这种做法允许对规则(例如根据订阅类型限制课程数量)进行原子校验,同时使用集成事件与原上下文同步状态。

### 核心概念

- **Aggregate Duplication(聚合复制)**:把一个模型复制到另一个限界上下文中,只保留特定业务逻辑所需的属性。
- **Aggregate Merging(聚合合并)**:把一个独立的聚合转变为一个更大聚合根内的本地实体,以确保原子性。
- **Invariant Enforcement(不变量强制执行)**:把规则校验(例如课程上限)的职责转移到持有主要状态的聚合(例如 Subscription)上。
- **Integration Events(集成事件)**:使用异步通信把变更从管理上下文传播回操作上下文。
- **API Gateway Routing(API 网关路由)**:把客户端的接口与底层微服务结构解耦,以便内部架构可以调整。

### 课程笔记

#### The Duplication and Merging Strategy

当一个不变量横跨多个限界上下文时,一种有效的做法是把必要的聚合复制到目标上下文中。
在这个场景中,Session Reservation 上下文中的 `Room` 聚合被复制到 Gym Management 上下文中。
这个副本并不是完整的拷贝;它只保留强制执行特定业务规则所需的一部分属性。

一旦 `Room` 存在于 Gym Management 上下文中,它就可以被合并进 `Subscription` 聚合。
在这个新的层级中,`Room` 不再是独立的聚合根,而是一个 `Gym` 内的本地实体,而 `Gym` 本身又是 `Subscription` 聚合根内的本地实体。
最终的结构如下:
- **Subscription**(聚合根)
  - **Gym**(实体)
    - **Room**(实体)
      - **Session**(实体)

#### Shifting Responsibility for Invariants

通过以这种方式重构聚合,强制执行该不变量(确保每天创建的课程数不超过订阅所允许的数量)的职责从 `Room` 转移到了 `Subscription`。
`Subscription` 聚合成为容量和课程上限的唯一真相之源。

当用户尝试创建一节课程时,系统与 Gym Management 上下文交互。
`Subscription` 聚合检查该房间的日程以及当前的课程数。
如果不变量得到满足,状态变更就在 `Subscription` 聚合内原子地发生。

#### Synchronization via Integration Events

因为 Gym Management 上下文现在负责课程创建的逻辑,Session Reservation 上下文必须保持同步。
当一节课程成功通过校验并被添加到 `Subscription` 聚合时,系统会发出一个集成事件。

Session Reservation 上下文中的一个处理器观察到这个事件,并执行以下操作:
1. 创建底层的课程记录。
2. 在它的本地数据库中把这节课程与相应的房间关联起来。

```text
client --> Gym Management                    Session Reservation
           Subscription (aggregate root)
             check room schedule + count
             atomic state change
             raise integration event ------> handler:
                                               1. create session record
                                               2. associate with room
```

#### Architectural and Client Implications

这种设计可能会影响客户端与系统交互的方式。
如果 Gym Management 和 Session Reservation 上下文托管在不同的微服务中,客户端现在要把课程创建请求发送到 Gym Management 服务。

为了维持一个稳定的契约,可以使用 API 网关来路由请求。
客户端继续与同一个网关端点交互,而底层的路由逻辑被更新为指向 Gym Management 服务。
这让内部的领域模型和职责可以演进,而不会破坏面向客户端的接口。

选择这种做法可以确保对订阅容量和课程上限的更新是原子地发生的,而这往往是涉及资源分配的业务逻辑的一项关键要求。

## 12. Merging Aggregates in the Session Management Context - Continued

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/merging-aggregates-in-the-session-management-context-continued-54855957/) · 1:57

### 总结

本课评估在为课程管理提出的聚合设计中强制执行业务不变量的可行性。
通过分析课程重叠预防、容量上限和取消时限,本课说明该聚合包含了保持一致性、防止无效状态所需的全部数据(例如房间日程和预订数量),无需进一步修改设计。

### 核心概念

- **Session Overlap Prevention(课程重叠预防)**:通过把新课程的时间与房间现有的日程进行比对,确保房间不会被重复预订。
- **Capacity Management(容量管理)**:用为课程定义的最大参与者数来校验新的预订。
- **Temporal Invariants(时间相关的不变量)**:通过访问课程的开始时间和当前系统时间,强制执行基于时间的业务规则,例如 24 小时取消时间窗。
- **Aggregate Consistency(聚合一致性)**:确认聚合边界持有在状态转换期间保护业务规则所需的全部信息。

### 课程笔记

#### Session Overlap Prevention

为了防止课程在某个特定房间内重叠,系统必须用现有的日程来校验新课程的开始和结束时间。
由于房间聚合维护着日程,这个不变量在当前设计中很容易强制执行。
系统在持久化状态之前,把新课程的细节与现有日程进行比对,以确保不存在冲突。

#### Session Capacity Limits

系统必须确保参与者数量不超过最大容量。
当请求一个新的预订时,课程聚合会把当前的预订数量与允许的最大值进行比较。
因为课程聚合跟踪自己的预订和容量上限,它可以自主决定是接受还是拒绝一个新的预订。

#### Reservation Cancellation Time Limit

一条业务规则规定,预订不能在课程开始前不足 24 小时时取消。
课程聚合通过比较课程的开始时间与当前时间来管理这一点。
通过在课程聚合内获取当前时间,系统可以判断取消请求是有效的,还是离课程开始时间太近。

#### Design Validation

审视这些不变量证实了该聚合设计是稳健的。
通过确保做决策所需的全部数据都存在于聚合边界内,系统防止了在各种用例和流程中产生无效状态。
设计过程的这一阶段涉及验证领域模型能够处理所有业务需求,并在所有操作中保持完整性。

## 13. Aggregate Design Draft Reflection

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/aggregate-design-draft-reflection-54855958/) · 4:29

### 总结

本课评估一个课程预订系统的聚合设计草案,识别出一个关键缺陷:系统无法强制执行"一个参与者不能多次预订同一节课程"这一不变量。
通过分析 Session、Participant 和 Reservation 聚合之间的关系,本课演示了把 Reservation 聚合折叠进 Session 或 Participant 聚合如何让系统能够维护数据完整性。
最终,设计被细化为让 Reservation 成为 Participant 聚合内的本地实体,确保父聚合能够有效地校验预订逻辑。

### 核心概念

- **Invariant Enforcement(不变量强制执行)**:聚合必须包含在其边界内强制执行业务规则所需的全部数据。
- **Aggregate Collapsing(聚合折叠)**:把一个单独的聚合作为本地实体合并进另一个聚合,以解决可见性问题并简化模型。
- **Reference by Identity(通过标识引用)**:聚合应当通过 ID 引用其他聚合根,但聚合内的本地实体只能通过根来管理和访问。
- **Domain Concept Discovery(领域概念发现)**:识别缺失的概念,例如一个专门的参与者集合,它可能更好地表达领域约束。

### 课程笔记

#### Identifying the Design Flaw

在预订系统的最初草案中,设计使用了三个不同的聚合:`Session`、`Participant` 和 `Reservation`。
然而,这种拆分使一个基本的业务不变量无法被强制执行:单个参与者不应能够在同一节课程中预订超过一次名额。

在当前设计下,当一个包含 `ParticipantId` 和 `SessionId` 的预订请求到来时,`Session` 聚合无法校验该参与者是否已经登记。
这是因为 `Session` 聚合只维护一个 `ReservationId` 的集合。
它看不到与这些预订相关联的底层 `ParticipantId`。
同样,`Participant` 聚合只通过 ID 知道自己的预订,如果不查询外部聚合,它本身并不知道这些 ID 对应哪些课程。

```text
Session (root)            Participant (root)        Reservation (root)
  ReservationId[]           ReservationId[]           ParticipantId
                                                      SessionId
```

#### Strategies for Refactoring

为了解决这个问题,`Reservation` 聚合必须被折叠进两个主要聚合中的一个,以便把必要的数据带入单个一致性边界。

1.  **Merging into the Session Aggregate(合并进 Session 聚合)**:如果 `Reservation` 成为 `Session` 内的本地实体,`Session` 聚合将持有包含其 `ParticipantId` 在内的所有预订的列表。在添加新预订时,`Session` 可以遍历它的本地实体,以确保该 `ParticipantId` 尚未存在。
2.  **Merging into the Participant Aggregate(合并进 Participant 聚合)**:另一种做法是,`Reservation` 可以成为 `Participant` 聚合内的本地实体。在这种场景下,`Participant` 聚合将跟踪它已预订的所有 `SessionId`。

#### Final Design Decision

在这些选项之间如何选择,取决于系统主要是如何被访问的。
在这个领域中,一个常见的需求是参与者通过个人档案或仪表盘一眼看到自己即将到来的预订。
这个视图通常需要课程细节的一个子集。

通过把 `Reservation` 合并进 `Participant` 聚合,会发生以下变化:
- `Reservation` 成为由 `Participant` 聚合根拥有的本地实体。
- `Reservation` 实体不再需要对 `ParticipantId` 的引用,因为它被包含在那个参与者的边界之内。
- `Session` 聚合不能再直接引用 `Reservation` 实体(因为它现在是本地实体)。取而代之的是,`Session` 必须引用 `Participant` 聚合根。

当参与者尝试预订名额时,会调用 `Session` 聚合上的 `reserveSpot` 方法,并把 `Participant`(或它的 ID)作为参数传入。
这让系统能够核实参与者的状态,并强制执行"该特定课程不存在重复预订"这一不变量。

## 14. Merging Aggregates Based On Eventual Consistency Constraints

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/merging-aggregates-based-on-eventual-consistency-constraints-54855959/) · 1:12

### 总结

本课探讨在无法容忍最终一致性约束时,保持聚合分离与合并聚合之间的权衡。
以一个同时涉及 Session 和 Participant 聚合的课程预订为例,本课着重说明"每个事务一个聚合"规则可能如何导致边界情况和竞态条件,从而需要在设计上评估该业务流程是否要求即时一致性。

### 核心概念

- Transaction boundaries(事务边界):每个事务只应修改一个聚合。
- Consistency trade-offs(一致性权衡):评估最终一致性对特定业务操作是否可以接受。
- Race conditions(竞态条件):后续事务中状态变更失败的风险,例如日程冲突。
- Aggregate merging(聚合合并):合并聚合或引入新概念以强制执行即时一致性。
- Iterative design(迭代式设计):随着领域需求和一致性约束的演进而细化聚合边界。

### 课程笔记

在当前的设计中,Session 和 Participant 被建模为单独的聚合。
遵循领域驱动设计"每个事务一个聚合"的原则,任何同时涉及这两个实体的操作都必须拆分为多个步骤。
例如,当参与者尝试在一节课程中预订名额时,系统必须要么先更新 Session 来预订名额、再更新 Participant 的日程,要么以相反的顺序执行这些操作。

```text
Option A                              Option B
tx 1: Session  (reserve spot)         tx 1: Participant (schedule)
tx 2: Participant (schedule)          tx 2: Session  (reserve spot)
      may fail: schedule taken              may fail: session at capacity
```

这种对最终一致性的依赖引入了潜在的边界情况。
如果先更新 Session 聚合,那么当参与者在那段特定时长内的日程不再可用时,后续更新 Participant 聚合的事务就可能失败。
反过来,如果先更新 Participant,Session 可能在第二个事务发生之前就已经满员。
此外,这些变更在系统中传播所需的时间可能导致向用户呈现不一致的状态。

关键的架构决策在于,对于某个给定的流程,业务领域能否容忍最终一致性。
如果答案是否定的(因为竞态条件或数据不一致的风险太高),就应当把这些聚合合并到单个一致性边界中。
或者,也可以引入一个新的领域概念来管理这种交互。
聚合设计不是一次性的任务;它是一个迭代的过程,随着对系统一致性需求的理解加深,边界会不断得到细化。

## 15. Evolving Aggregate Design With Time or Refactoring Existing Aggregate Designs

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/evolving-aggregate-design-with-time-or-refactoring-existing-aggregate-designs-54855960/) · 3:19

### 总结

本课讨论随着系统规模扩大而重构聚合设计的必要性,具体来说是从大型、集中式的聚合转向更小、更细粒度的聚合,以消除并发瓶颈。
它详细说明了把实体提升为聚合根的过程,以及如何通过数据传播和事件处理来管理由此产生的跨聚合一致性。

### 核心概念

- **Identifying Concurrency Bottlenecks(识别并发瓶颈)**:当多个用户同时尝试更新不同的子实体时,大型聚合往往会导致竞态条件。
- **Aggregate Splitting(聚合拆分)**:通过把内部实体提升为聚合根,把单个聚合重构为多个更小聚合的过程。
- **Data Propagation(数据传播)**:通过复制或事件驱动的更新,确保必要的状态(例如订阅上限)在新的聚合中可用。
- **Invariant Maintenance(不变量维护)**:当即时一致性不再可能时,使用领域事件和处理器在聚合边界之间同步状态。

### 课程笔记

聚合设计不是一成不变的;它必须随着业务需求和系统负载的变化而演进。
一种常见的场景是一个 "God" 聚合,例如包含 `Gyms`、`Rooms` 和 `Sessions` 的 `Subscription`,它充当所有操作的唯一真相之源。
虽然这确保了严格的一致性,但它在高流量环境中会引入严重的性能问题。
例如,在一个大型健身房系统中,多个尝试向同一订阅下不同房间添加课程的事务将会发生冲突。
因为整个 `Subscription` 聚合必须被读出并写回,一个事务常常会因为聚合状态已发生变化而失败,导致频繁的竞态条件。

为了解决这些问题,可以拆分聚合。
这涉及把一个内部实体(例如 `Room`)提升为聚合根。
当 `Room` 成为独立的聚合时,`Subscription` 聚合不再持有 `Room` 实体的集合,而是持有对它们的引用(ID)。

```text
Before                                After
Subscription (root)                   Subscription (root)
  Gym                                   Gym
    Room                                  Room IDs  --->  Room (root)
      Session                                               Session
```

然而,拆分聚合带来了一个挑战:如何维持那些先前依赖于 "God" 聚合共享状态的不变量。
如果 `Room` 聚合需要知道允许的最大课程数(一个由 `Subscription` 定义的值),这些数据就必须被传播。
这是通过在 `Room` 聚合中包含订阅细节,并在父级 `Subscription` 发生变化时使用一个事件处理器更新这些细节来实现的。

另一种做法是,可以把课程管理的职责完全移到另一个上下文,例如 Session Reservation 上下文。
在某些设计中,`Gym` 也可能被提升为聚合根,而预订作为 `Session` 聚合的一部分来处理,而不是反过来。
这种重构路径通过减少对单条数据库记录的争用,让系统得以扩展。
当前讨论的这个设计曾作为 Getting Started 课程中对领域建模的起点,这说明了随着对领域复杂性的理解加深,最初的模型往往会得到细化。

## 16. Converting Aggregate Design To Code

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/converting-aggregate-design-to-code-54855961/) · 2:32

### 总结

本课演示如何把聚合设计直接转换为 C# 实现。
它展示了 Room 和 Session 这样的聚合根如何封装其内部状态和实体(例如共享的 Schedule 实体),同时通过 ID 维持对其他聚合的引用。
这个过程强调使用测试驱动开发直接在领域模型内强制执行业务不变量,确保软件反映预期的领域逻辑。

### 核心概念

* 聚合设计产物与领域层代码库之间的直接映射。
* 把聚合根实现为领域逻辑的主要入口点。
* 使用位于公共命名空间中的共享实体(例如 Schedule),以便在多个聚合之间使用。
* 通过 ID 而不是直接的对象引用来引用外部聚合,以维护边界的完整性。
* 通过测试驱动开发强制执行不变量,把业务逻辑推入领域模型。

### 课程笔记

从可视化的聚合设计过渡到可运行的代码库,涉及一一对应的映射。
设计阶段识别出的每个组件都对应领域层中的一个具体类或实体。

#### The Room Aggregate

`Room` 类充当聚合根。
它封装了自己的内部状态,包括一个 `Schedule` 实体和一个课程 ID 的集合。
通过持有 ID 列表而不是对 `Session` 对象的直接引用,该聚合维持了清晰的边界,并避免了不同聚合根之间不必要的耦合。

```csharp
namespace SessionReservation.Domain.RoomAggregate;

public class Room : AggregateRoot
{
    private readonly int _maxSessions;

    private readonly Schedule _schedule = Schedule.Empty();

    private readonly List<Guid> _sessionIds = new();

    public string Name { get; } = null!;

    public Guid GymId { get; }

    public IReadOnlyList<Guid> SessionIds => _sessionIds.AsReadOnly();
}
```

[▶ 观看](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/converting-aggregate-design-to-code-54855961/?t=40)

#### Shared Entities

多个聚合都需要的实体(例如 `Schedule`)被放在一个公共命名空间中(例如 `SessionReservation.Domain.Common.Entities`)。
这让不同的聚合根可以复用同一套基于时间的约束和排期逻辑,而不会破坏聚合边界。

#### The Session Aggregate

`Session` 聚合根管理一节具体课程的生命周期,包括它的预订和被分配的教练。
它跟踪参与者的数量,并确保不超过最大容量。
在内部,它包含一个 `Reservation` 实体的列表,而这些实体又持有每位参加者的 `ParticipantId`。

```csharp
namespace SessionReservation.Domain.SessionAggregate;

public class Session : AggregateRoot
{
    private readonly List<Reservation> _reservations = new();
    private readonly List<SessionCategory> _categories = new();

    public int NumParticipants => _reservations.Count;

    public DateOnly Date { get; }

    public TimeRange Time { get; } = null!;

    public string Name { get; } = null!;
}
```

[▶ 观看](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/converting-aggregate-design-to-code-54855961/?t=55)

`Session` 聚合还通过各自的 ID 维持对其所在的 `Room` 和被分配的 `Trainer` 的引用。
这种结构确保 `Session` 聚合独立于 `Room` 或 `Trainer` 聚合的内部状态。

```csharp
public class Session : AggregateRoot
{
    public Guid RoomId { get; }

    public int MaxParticipants { get; }

    public IReadOnlyList<SessionCategory> Categories => _categories;

    public Guid TrainerId { get; }

    public Session(
        string name,
        string description,
        int maxParticipants,
        Guid roomId,
        Guid trainerId,
        DateOnly date,
        TimeRange time)
    {
        // Constructor implementation
    }
}
```

[▶ 观看](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/converting-aggregate-design-to-code-54855961/?t=70)

```text
Room (root)                     Session (root)
  Schedule                        Reservation[]
  SessionIds  ------------------>   ParticipantId
  GymId                           RoomId     ---> Room
                                  TrainerId  ---> Trainer
```

#### Driving Logic through Invariants

构建领域包括列出所有业务不变量,并为每一个编写测试。
这种测试驱动的做法确保所有业务逻辑都被推入领域模型。
一旦不变量在聚合内得到强制执行,核心领域就完成了,后续的开发则聚焦于编排这些模型的应用层。

## 17. Section Recap

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/section-recap-54855962/) · 1:10

### 总结

本课对聚合设计进行全面回顾,强调十条规则以及四步流程(识别实体、映射关系、标记聚合、合并聚合),以定义最佳的边界。
通过专注于创建仍能保护业务不变量的尽可能小的聚合,从设计到领域层代码的过渡变得直接而高效。

### 核心概念

* 聚合设计的 10 条规则。
* 四步设计方法:Identify(识别)、Map(映射)、Mark(标记)和 Merge(合并)。
* 优化聚合大小,以平衡性能与不变量强制执行。
* 把概念性的聚合设计转化为领域层的实现。

### 课程笔记

聚合设计的过程是构建稳健的领域驱动设计(DDD)架构的基础。
它由十条基本规则指导,这些规则确保聚合在逻辑上是合理的、在技术上是高效的。
从复杂的领域到可运行的代码库的过渡,是通过一个系统的四步设计流程实现的:

1. **Identifying Entities(识别实体)**:第一步是发现领域上下文中存在的各种实体。
2. **Mapping Relationships(映射关系)**:实体被识别出来之后,必须清晰地映射它们之间的关系和依赖。
3. **Marking Aggregates(标记聚合)**:实体最初被归类为聚合,以建立潜在的边界。
4. **Merging Aggregates(合并聚合)**:最后一步是根据具体的业务需要和需求合并这些聚合。

```text
Identify --> Map --> Mark --> Merge
```

这种系统化的做法确保最终得到的聚合尽可能小。
小聚合之所以更受青睐,是因为它们把技术开销和数据库争用降到最低。
然而,在为了强制执行业务不变量(也就是在特定事务边界内必须始终为真的规则)而有必要时,它们必须被合并。
合并这一步正是评估这些权衡的地方,确保所有必要的业务逻辑都被封装在单个事务边界之内。

通过遵循这一严谨的设计流程,从概念产物到代码库中实际领域层的过渡就变成了一项轻而易举的任务。
在为领域建模以及考虑各种设计上的"岔路口"时投入的脑力,会以一份清晰、可直接实现的软件蓝图的形式得到回报。
