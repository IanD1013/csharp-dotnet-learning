# Strategic Phase - Bounded Contexts

> 课程:[Deep Dive: Domain-Driven Design](https://dometrain.com/course/deep-dive-domain-driven-design-ddd/) · 第 6 章
> 共 8 课 · 约 30:40
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Refresher - What are Bounded Contexts](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/refresher-what-are-bounded-contexts-54855795/) | 6:59 | [↓](#1-refresher---what-are-bounded-contexts) |
| 2 | [Reviewing Our Ubiquitous Language](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/reviewing-our-ubiquitous-language-54855796/) | 4:28 | [↓](#2-reviewing-our-ubiquitous-language) |
| 3 | [Reviewing Our Invariants](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/reviewing-our-invariants-54855797/) | 2:38 | [↓](#3-reviewing-our-invariants) |
| 4 | [Reviewing Our Sub-Domains](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/reviewing-our-sub-domains-54855798/) | 3:46 | [↓](#4-reviewing-our-sub-domains) |
| 5 | [How Bounded Contexts Affect Domain Models](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/how-bounded-contexts-affect-domain-models-54855799/) | 1:56 | [↓](#5-how-bounded-contexts-affect-domain-models) |
| 6 | [Defining Bounded Contexts](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/defining-bounded-contexts-54855800/) | 3:45 | [↓](#6-defining-bounded-contexts) |
| 7 | [Defining Our Bounded Contexts](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/defining-our-bounded-contexts-54855801/) | 6:34 | [↓](#7-defining-our-bounded-contexts) |
| 8 | [Section Recap](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/section-recap-54855802/) | 0:34 | [↓](#8-section-recap) |

## 1. Refresher - What are Bounded Contexts

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/refresher-what-are-bounded-contexts-54855795/) · 6:59

### 总结

本课标志着领域驱动设计战略阶段中从问题空间到解决方案空间的过渡。
限界上下文(Bounded Context)被定义为某个特定领域模型适用的逻辑边界,它带来模块化并降低耦合。
子领域帮助组织业务问题,而限界上下文代表战略层面的实现决策,它可以体现为命名空间、模块或微服务。

### 核心概念

- 从问题空间过渡到解决方案空间。
- 限界上下文的定义:领域模型的一个逻辑边界。
- 模块化:降低复杂度与耦合。
- 子领域(问题空间)与限界上下文(解决方案空间)。
- 实现上的灵活性:命名空间、模块或微服务。

### 课程笔记

过渡到限界上下文意味着从分析问题空间转向设计解决方案空间。
此前,关注点在于业务决策,例如不变量(invariant)、通用语言(ubiquitous language)和子领域。
现在,要做的是关于解决方案结构的战略决策。

限界上下文是定义领域模型所适用的逻辑边界最简洁的方式。
从系统的整体视角看,目标是使用包含数据和行为的领域对象,在软件中为领域建模。
限界上下文指明了某个特定模型在何处有效。
例如,一个 "Session" 模型在排期(scheduling)上下文中可能具有特定的属性和行为,而这些在计费(billing)或用户管理上下文中是无关的,或者根本不存在。

通过划出这些"沙地上的界线",系统被拆分为更小、更易管理的组件。
这种模块化降低了系统整体的复杂度,并将应用程序不同部分之间的耦合降到最低。

区分子领域和限界上下文非常重要。
子领域属于问题空间;它们用于探索业务、识别价值所在的领域,并对开发资源进行优先级排序。
限界上下文属于解决方案空间。
它们在识别出子领域之后才被定义,代表的是如何实现在问题空间分析中发现的模型、行为和语言的战略决策。
单个限界上下文可以涵盖多个子领域。
例如,一个 "Facility Management" 上下文可能同时处理健身房管理和课程预约这两个子领域。

```text
  Problem Space                      Solution Space
+------------------------------+    +---------------------------+
| Subdomain:                   |--->|                           |
|   gym management             |    |  Bounded Context:         |
+------------------------------+    |  Facility Management      |
| Subdomain:                   |--->|                           |
|   session reservation        |    |                           |
+------------------------------+    +---------------------------+
```

在实践中,限界上下文并不严格等同于一个微服务。
它是一个逻辑边界,可以用多种方式实现:
- 项目中不同的命名空间。
- 模块化单体中的模块。
- 单个微服务。
- 一组协同工作的微服务。

虽然业界常把一个限界上下文映射为一个微服务,但这只是一种实现选择,而不是该模式的要求。

## 2. Reviewing Our Ubiquitous Language

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/reviewing-our-ubiquitous-language-54855796/) · 4:28

### 总结

本课回顾在领域驱动设计战略阶段建立的通用语言草稿和高层不变量。
它为一个健身房管理系统定义了核心术语和业务规则,包括用户角色(Admin、Trainer、Participant)、实体设施(Gym、Room、Session),以及不同订阅等级(Free、Starter 和 Pro)所施加的约束。

### 核心概念

- 通用语言作为领域术语的"唯一事实来源"。
- 附加在 User 上的基于角色的档案(Admin、Trainer、Participant)。
- 针对健身房、房间和课程的基于订阅的容量约束。
- 运营操作:预约(Reserving)、签到(Check-in)和取消(Cancellation)。
- 特定功能的术语:目标追踪(Goal Tracking)。

### 课程笔记

领域驱动设计的战略阶段包括把从事件风暴(event storming)和流程建模中获得的知识形式化为通用语言。
这份文档充当所有利益相关者共享的词汇表,并提供关于领域范围和规则的整体视图。

首先定义的实体是 **User** 及其相关角色。
User 是任何与系统交互的人,他们可以担任特定角色,例如 Admin、Trainer 或 Participant。

```markdown
# Ubiquitous Language Draft

## User
**Definition**: Anyone who uses the system, with roles such as Admin, Trainer, or Participant.
**Usage**: A User registers on the platform.
**Usage**: A User sets their payment method.

## Participant Profile
**Definition**: A User's role enabling them to join and participate in sessions.
**Usage**: A User creates a Participant Profile.
**Usage**: A Participant checks into a session.
```

[▶ 观看](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/reviewing-our-ubiquitous-language-54855796/?t=55)

领域的实体结构由 **Gyms**、**Rooms** 和 **Sessions** 表示。
Gym 是提供各种健身课程的场馆,这些课程在指定的 Room 中进行。

```markdown
## Session
**Definition**: A fitness class held in a gym's room.
**Usage**: A Participant reserves a spot in a Session.
**Usage**: The Session has a capacity limit of 20 participants.

## Room
**Definition**: A designated area within a gym for conducting sessions.
**Usage**: The Room has a capacity limit of 30 participants.
**Usage**: The Room was added to the Gym.

## Gym
**Definition**: A facility offering various fitness sessions.
**Usage**: An Admin creates a Gym within their subscription.
**Usage**: The Gym was deleted.
```

[▶ 观看](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/reviewing-our-ubiquitous-language-54855796/?t=100)

管理操作由拥有 **Admin Profile** 的用户负责。
这些用户必须购买一个 **Subscription**,它决定了他们运营的容量。

```markdown
## Admin Profile
**Definition**: A User's role with privileges to manage gym operations.
**Usage**: An Admin purchased a subscription.
**Usage**: The Admin created a yoga session in Room 3.

## Subscription
**Definition**: A service plan offering Admins different capacities for creating gyms, rooms, and sessions.
```

[▶ 观看](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/reviewing-our-ubiquitous-language-54855796/?t=115)

```text
User
 +-- Admin Profile -- purchases --> Subscription
 +-- Trainer Profile
 +-- Participant Profile

Gym
 +-- Room
      +-- Session
```

系统根据所选的 **Subscription Type** 强制执行特定的不变量:

- **Free**:允许 1 个健身房、1 个房间,每个房间每天最多 4 节课程。
- **Starter**:允许 1 个健身房、3 个房间,每日课程数量不限。
- **Pro**:允许最多 3 个健身房,房间和课程数量不限。

```markdown
## Subscription Usage Rules
**Usage**: An Admin chooses a Subscription type.
**Usage**: An Admin with a Free subscription can create up to 1 gym, 1 room in that gym, and a maximum of 4 daily sessions in each room.
**Usage**: An Admin with a Starter subscription can create up to 1 gym, 3 rooms in that gym, and an unlimited number of daily sessions.
**Usage**: An Admin with a Pro subscription can create up to 3 gyms and can create an unlimited number of rooms and sessions.
```

[▶ 观看](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/reviewing-our-ubiquitous-language-54855796/?t=130)

**Trainer Profile** 分配给有资格带课的用户。

```markdown
## Trainer Profile
**Definition**: A User's role qualified to teach sessions.
**Usage**: A Trainer starts their session.
**Usage**: A Trainer received feedback for the session.
```

[▶ 观看](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/reviewing-our-ubiquitous-language-54855796/?t=190)

**Reserving**(预约名额)和 **Check-In**(签到)等运营流程对参与者体验至关重要。
一旦教练开始课程,签到就变为可用。

```markdown
## Subscription Type
**Definition**: The category of a Subscription that determines an Admin's Gym, Room, and Session Capacities.
**Usage**: An Admin upgrades to a higher Subscription Type.
**Usage**: Capacities change based on the Subscription Type.

## Check-In
**Definition**: The action of joining a session.
**Usage**: The Participant checked into the session.
**Usage**: The Trainer started the session; Check-In is now available.
```

[▶ 观看](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/reviewing-our-ubiquitous-language-54855796/?t=205)

最后,领域还包括用于管理预约撤销的 **Cancellation Policy**,以及用于监测参与者健身进展的 **Goal Tracking**。

```markdown
## Cancellation Policy
**Definition**: The rules governing the ability to cancel a reservation.
**Usage**: A Participant reviews the Cancellation Policy before reserving.
**Usage**: The user cannot cancel the session due to the Cancellation Policy.
```

[▶ 观看](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/reviewing-our-ubiquitous-language-54855796/?t=220)

```markdown
## Goal Tracking
**Definition**: A feature that records and monitors participant fitness goals.
**Usage**: A Participant sets goals using Goal Tracking.
**Usage**: Goal Tracking alerts a Participant of progress.
```

[▶ 观看](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/reviewing-our-ubiquitous-language-54855796/?t=235)

## 3. Reviewing Our Invariants

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/reviewing-our-invariants-54855797/) · 2:38

### 总结

本课全面回顾在领域驱动设计战略建模阶段识别出的业务不变量。
这些不变量代表必须始终保持的核心业务规则和约束,以确保领域模型的完整性。
回顾涵盖了广泛的约束,包括健身房和房间的资源容量限制、取消和反馈的时间限制、教练和参与者的排期冲突防止,以及年龄验证和教练资质等资格要求。

### 核心概念

* **资源容量限制**:对每节课程的参与者数量、每个健身房的房间数量以及每个订阅的健身房数量的约束。
* **时间约束**:预约取消和反馈提交的特定时间窗口。
* **排期完整性**:防止房间、教练和参与者出现时间重叠的课程。
* **基于订阅的不变量**:由用户订阅等级决定的每日课程数量和场馆数量限制。
* **资格与合规**:对教练认证、参与者年龄和有效支付方式的要求。

### 课程笔记

不变量是为了在领域内维持有效状态而必须始终成立的业务规则。
我们先来看与课程和预约相关的约束。
一节课程有确定的参与者容量上限。
此外,为了保持运营效率并让其他会员能够填补空出的名额,如果课程将在 24 小时内开始,则预约不能被取消。

```markdown
# Invariants Draft
## Reservation
reservation cannot
be canceled less
than 24 hours
before the session
starts.

**Reasoning**:
Allows time for
others to reserve
the spot,
maintaining full
attendance and
operational
efficiency.
```

[▶ 观看](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/reviewing-our-invariants-54855797/?t=10)

场馆限制通常与订阅等级挂钩。
一个健身房能容纳的房间数量受到限制,每个房间每天可承办的课程数量也有上限。
例如,免费订阅可能会把一个房间限制为每天四节课程。

```markdown
# Invariants Draft
## Gym Room
Capacity Limits

**Description**: A
gym cannot have
more rooms than
the subscription
allows.

**Reasoning**:
Prevents
overbooking and
ensures compliance
with subscription
terms.

## Room Daily
Session Limits

**Description**: A
room cannot have
more daily
sessions than the
subscription
allows.

**Reasoning**:
Manages facility
usage efficiently
and prevents
excessive wear and
tear.
```

[▶ 观看](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/reviewing-our-invariants-54855797/?t=25)

排期完整性确保实体资源和人力资源不会被重复预订。
特定房间不能承办时间重叠的课程,从而确保所有相关人员的日程没有冲突。

```markdown
# Invariants Draft
## Session Overlap
Prevention

**Description**: A
room cannot have
overlapping
sessions.

**Reasoning**:
Ensures clear and
conflict-free
scheduling for
participants and
trainers.
```

[▶ 观看](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/reviewing-our-invariants-54855797/?t=40)

在订阅层面,可创建的健身房总数有上限。
人力资源约束同样关键:一名教练不能被分配到两节同时进行的课程,参与者也被禁止为同一时间进行的课程创建预约。

```markdown
# Invariants Draft
## Subscription
Capacity Limits

**Description**: A
subscription
cannot have more
gyms than allowed.

**Reasoning**:
Maintains service
quality and
adherence to
financial and
operational limits.

## Trainer Session
Conflict Prevention

**Description**: A
trainer cannot
teach overlapping
sessions.

**Reasoning**:
Allows trainers to
focus on providing
quality
instruction
without scheduling
conflicts.
```

[▶ 观看](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/reviewing-our-invariants-54855797/?t=55)

资格和安全标准构成了另一层不变量。
教练必须具备所带课程类型的资质和认证。
参与者也必须满足某些课程类型的最低年龄要求,以确保安全和内容的适宜性。

```markdown
# Invariants Draft
## Participant Age
Verification

**Description**: 
Participants must
meet the minimum
age requirement
for certain types
of sessions.

**Reasoning**:
Ensures safety and
appropriateness of
session content
for different age
groups.
```

[▶ 观看](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/reviewing-our-invariants-54855797/?t=70)

运营就绪要求在课程可被预订之前,必要的器材必须可用。
从业务角度看,任何试图在课程中预约名额的用户都必须拥有有效的支付方式。

```markdown
# Invariants Draft
## Equipment
Availability Check

**Description**: 
A session cannot be
booked unless the
necessary
equipment is
available.

**Reasoning**:
Prevents session
bookings that
cannot be properly
facilitated,
ensuring a smooth
experience for
participants and
trainers.

## Subscription
Validity Check

**Description**:
Users must have a
payment method to
book a session.

**Reasoning**:
Ensures that only
paying customers
can utilize the
resources.
```

[▶ 观看](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/reviewing-our-invariants-54855797/?t=85)

最后,我们来处理工作量和反馈。
为了防止过度疲劳并保持教学质量,教练每天可教授的课程数量是有限的。
此外,课程反馈只有在课程结束后的特定时间段内提交才有效,以确保数据及时且相关。

```markdown
# Invariants Draft
## Trainer Daily
Session Limit

**Description**:
A trainer cannot be
assigned to more
sessions per day
than a specified
limit.

**Reasoning**:
Ensures that
trainers are not
overworked,
maintaining the
quality of
instruction and
trainer well-being.

## Feedback
Validity Window

**Description**:
Feedback for a
session can only
be submitted
within a certain
timeframe after
the session has
ended.

**Reasoning**:
Ensures timely and
relevant feedback,
reflecting the
immediate
experience of the
participants.
```

[▶ 观看](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/reviewing-our-invariants-54855797/?t=115)

## 4. Reviewing Our Sub-Domains

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/reviewing-our-sub-domains-54855798/) · 3:46

### 总结

本课评估一个健身平台的各个子领域,以确定它们的战略重要性。
通过把领域划分为核心(Core)、支撑(Supporting)和通用(Generic)三类,开发者和利益相关者可以优先投入 Session Scheduling 这个核心领域,把 Payments 这类通用领域交给第三方提供商,并对支撑领域采用简化的模式。

### 核心概念

* **核心子领域**:收入和竞争优势的主要驱动力(例如 Session Scheduling)。
* **支撑子领域**:必要但不提供独特优势的业务功能(例如 User Management 或 Gym Management)。
* **通用子领域**:已有第三方解决方案的标准业务问题(例如 Payments and Billing)。
* **战略性资源分配**:把最多的工程投入导向核心子领域,而对其他子领域使用 CRUD 或现成工具。

### 课程笔记

识别子领域是领域驱动设计战略阶段的关键一步。
通过对这些子领域进行分类,组织可以决定把最有才华的资源投入到哪里,以及在哪里使用现有解决方案。
下面的草稿列出了一个健身管理系统的各个子领域:

```markdown
# Subdomains Draft

* **Session Scheduling Subdomain** - Core
* **User Management Subdomain** - Supporting
* **Gym and Room Management Subdomain** - Supporting
* **Subscription Management Subdomain** - Supporting
* **Payments and Billing Subdomain** - Generic
* **Reporting and Analytics Subdomain** - Supporting
* **Feedback and Quality Assurance Subdomain** - Supporting
* **Goal Tracking and Progress Monitoring Subdomain** - Supporting
```

[▶ 观看](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/reviewing-our-sub-domains-54855798/?t=10)

### 子领域分类

**Session Scheduling Subdomain** 被确定为核心子领域。
这一领域为业务提供独特的竞争优势,并预期带来最多的收入。
由于它是高层管理者的首要关注点,并且包含最密集的功能,它需要最大的工程投入和关注。

有几个领域被归为支撑子领域,包括 **User Management**、**Gym and Room Management** 和 **Subscription Management**。
虽然这些是系统运作所必需的,但它们并不提供竞争优势。
例如,Gym and Room Management 涉及定义场馆以及健身房内的指定区域,这些任务可能需要特定的领域专家参与,但并不构成核心业务价值。
其他支撑子领域包括 **Reporting and Analytics**、**Feedback and Quality Assurance** 以及 **Goal Tracking and Progress Monitoring**。
这些支撑领域通常可以用简单的 CRUD 模式来实现。

**Payments and Billing Subdomain** 被归为通用子领域。
由于支付处理是一个有成熟行业解决方案的标准需求,使用现成的第三方服务比开发定制实现更高效。

```text
Core        Session Scheduling             -> 专门团队, 最大工程投入
Supporting  User Management                -> 简单 CRUD 模式
            Gym and Room Management
            Subscription Management
            Reporting and Analytics
            Feedback and Quality Assurance
            Goal Tracking and Progress Monitoring
Generic     Payments and Billing           -> 现成的第三方服务
```

### 战略决策

对子领域进行分类,使得关于团队组织和资源分配的战略决策成为可能。
一个团队可以管理多个支撑子领域,例如 User、Gym 和 Subscription management,而核心子领域应该由一个专门的团队负责,以确保其成功。
这种洞察让业务可以把通用功能委托给第三方,并把精力集中在最重要的业务问题上。

## 5. How Bounded Contexts Affect Domain Models

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/how-bounded-contexts-affect-domain-models-54855799/) · 1:56

### 总结

本课探讨战略层面的限界上下文边界与由此产生的领域模型之间的直接关系。
通过针对 User Management 和 Payment 子领域,对比统一上下文的做法与分离上下文的做法,本课展示了限界上下文如何防止模型膨胀,并确保领域对象始终聚焦于特定的业务需求。

### 核心概念

* **子领域整合**:决定在限界上下文中把 User Management 和 Payment and Billing 等子领域放在一起还是分开。
* **模型膨胀**:当多个子领域共享同一个领域模型定义时,产生"上帝对象"(God Objects)的风险。
* **上下文化建模**:根据领域模型所在限界上下文的具体需要,以不同方式定义领域模型(例如 "User")。
* **战略影响**:关于边界的高层架构决策如何直接决定代码的实现和复杂度。

### 课程笔记

子领域与限界上下文之间的关系是领域建模中的关键因素。
来看 User Management 子领域(支撑)与 Payment and Billing 子领域(通用)之间的交互。
如果把这两个子领域放在同一个限界上下文中,它们就必须共享同一个逻辑边界。
在这种场景下,"User" 的领域模型必须同时满足两个子领域的需求。
这会导致一个单一而复杂的模型,它既包含身份数据(姓名、ID),也包含财务数据(支付方式、账单地址)。

然而,如果把这两个子领域分到两个不同的限界上下文中,每个上下文都维护自己的逻辑边界和自己对 "User" 的特定定义。
在 User Management 上下文中,模型是为身份和访问量身定制的。
在 Billing 上下文中,模型是为财务交易和开票量身定制的。

```text
单一限界上下文:
+-----------------------------------------------+
| User                                          |
|   identity data:  names, IDs                  |
|   financial data: payment methods,            |
|                   billing addresses           |
+-----------------------------------------------+

两个限界上下文:
+-----------------------+   +-----------------------+
| User Management       |   | Billing               |
|   User                |   |   User                |
|   (identity, access)  |   |   (transactions,      |
|                       |   |    invoicing)         |
+-----------------------+   +-----------------------+
```

下面的示例展示了当 "User" 模型被拆分到这两个上下文时,数据表示上的差异:

```json
{
    "firstName": "Amichai",
    "lastName": "Mantinband",
    "adminId": "{GUID}",
    "participantId": "{GUID}",
    "trainerId": "{GUID}"
}

{
    "fullNameOnCard": "Amichai Mantinband",
    "billingAddress": {
        "zipCode": "{zipCode}",
        "..": ".."
    }
}
```

[▶ 观看](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/how-bounded-contexts-affect-domain-models-54855799/?t=85)

关于限界上下文的战略决策会直接影响代码。
在选择限界上下文的边界时,开发者同时也在选择领域模型的形态和范围。
这确保了模型保持聚焦,不会从无关的子领域累积不必要的属性。

## 6. Defining Bounded Contexts

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/defining-bounded-contexts-54855800/) · 3:45

### 总结

定义限界上下文是一个迭代过程,重点在于做出有依据的架构决策,而不是寻找唯一的完美方案。
它包括通过领域发现和子领域分类来分析问题空间,然后基于可用的专业能力、数据所有权、组织结构、技术约束和预算来调整解决方案空间。
由于领域和业务优先级会不断演变,限界上下文必须被视为活的结构,随其所封装的领域模型一起变化。

### 核心概念

* **迭代演进**:限界上下文和子领域分类不是静态的;它们会随着系统和业务需求的成熟而变化。
* **领域发现**:通过识别和分类子领域而获得的对问题空间的深入理解,是定义边界的前提。
* **解决方案空间的驱动因素**:上下文边界受到外部因素的影响,包括团队专业能力、数据所有权、组织结构、技术栈和预算。
* **权衡分析**:架构由对利弊的权衡来驱动,以指导更好的决策,而不是追求一个绝对"正确"的模型。

### 课程笔记

定义限界上下文并不遵循一个僵硬、通用的公式。
由于每个领域都是独特的,识别子领域和定义解决方案边界有多种合理的方式。
目标不是找到一个绝对或"完美"的配置,而是基于当前的权衡做出有依据的决策。

#### 上下文的动态本质

软件系统是活的实体。
随着时间推移,子领域的重要性可能发生变化,例如,一个支撑子领域可能变成核心子领域,或者一个通用子领域可能转为支撑角色。
因此,限界上下文及其底层的领域模型必须随之更新,以反映业务的当前状态。
在这一阶段,DDD 的首要目标是提供一个框架,用于做出支持系统成长的更好决策。

#### 影响限界上下文边界的因素

##### 1. 领域发现与知识

定义边界的第一步是深入研究领域。
这包括识别并分类所有子领域,以彻底理解问题空间。
一旦理解了问题空间,你就可以评估解决方案空间中可用于解决它的工具。

##### 2. 专业能力

组织所拥有的特定专业能力可以决定上下文如何拆分。
例如,如果一个组织在行为心理学和健身训练方面有深厚的专业能力,那么创建一个专门的 **Participant Engagement** 限界上下文可能是有益的。
这让一个专门的团队可以专注于目标设定、用户留存和激励策略。

##### 3. 数据流与所有权

分析数据如何被管理、存储和拥有,是定义边界的关键驱动因素。
识别系统的哪些部分"拥有"特定的数据集,有助于避免职责重叠,并确保限界上下文维持一致的模型。

##### 4. 组织结构

组织现有或计划中的结构常常会影响解决方案。
让限界上下文与团队所有权保持一致(通常与康威定律(Conway's Law)相关),可以确保团队在自己的边界内自主工作,而不会持续产生跨上下文的摩擦。

##### 5. 技术与预算约束

技术和财务上的限制在架构决策中起着重要作用:
* **技术栈**:每一种技术选择都有其固有的利弊,可能影响上下文的实现方式或其边界的划分位置。
* **预算**:财务约束决定了开发人员的数量和功能的范围。
  这直接影响一个项目实际能够支撑的限界上下文的复杂度和数量。

## 7. Defining Our Bounded Contexts

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/defining-our-bounded-contexts-54855801/) · 6:34

### 总结

本课讲解一个健身房管理系统中从子领域到限界上下文的战略过渡。
通过应用预算、.NET 技术栈和 MVP 需求等现实约束,本课说明了如何对核心功能进行优先排序并合并支撑子领域。
最终的架构由四个限界上下文组成:Billing、Session Scheduling、User Management 和 Gym Management,这表明上下文边界往往由实际的实现考量塑造,而不仅仅是理论上的领域拆分。

### 核心概念

- **战略优先级排序**:基于预算和资源限制,为最小可行产品(MVP)选择特定的子领域。
- **上下文合并**:把多个子领域合并为单个限界上下文(例如 Gym Management 和 Subscription Management),以降低技术复杂度。
- **把子领域映射到上下文**:定义边界的过程,虽然通常是一对一,但根据领域的需要也可以是多对一。
- **迭代设计**:承认 DDD 是一个非线性的过程,涉及持续的头脑风暴、建模和改进。
- **外部集成**:对通用子领域(例如 Billing)使用外部服务,以简化最初的实现。

### 课程笔记

定义限界上下文的过程始于根据项目约束来评估已识别的子领域,这些约束包括预算、技术专长(具体来说是一个专注于 .NET 的工程团队)以及第一次迭代所期望的范围。
在这个健身房管理场景中,有几个子领域(Reporting and Analytics、Goal Tracking and Progress Monitoring,以及 Feedback and Quality Assurance)被推迟到后续阶段,以便把资源集中在主要的业务价值上。

#### 把子领域映射到上下文

虽然子领域常常一对一地映射到限界上下文,但实际的考量促成了健身房系统的如下配置:

1. **Billing Context**:这个通用子领域被定义为独立的上下文。
   在第一次迭代中,它被简化为只通过外部服务处理管理员层面的订阅购买,而参与者与前台之间按单节课程付款的复杂性被推迟处理。
2. **Session Scheduling Context**:作为核心子领域,它被隔离到自己的限界上下文中,以便容纳这一领域预期会有的大量功能增长和复杂度。
3. **User Management Context**:这个上下文处理支撑性关注点,包括用户注册、登录、身份验证,以及各种用户角色和档案的管理。
4. **Gym Management Context**:作为一次战略性合并,"Gym and Room Management" 和 "Subscription Management" 两个子领域被合并到单个限界上下文中。
   做出这一决定,是因为订阅逻辑的复杂度相对较低(主要涉及强制执行健身房和房间数量限制等不变量),并且希望在当前预算内简化技术实现。

```text
Subdomain                                   Bounded Context
------------------------------------------  ---------------------------
Payments and Billing (Generic)          --> Billing Context
Session Scheduling (Core)               --> Session Scheduling Context
User Management (Supporting)            --> User Management Context
Gym and Room Management (Supporting)    -+
                                         +-> Gym Management Context
Subscription Management (Supporting)    -+

Deferred: Reporting and Analytics,
          Goal Tracking and Progress Monitoring,
          Feedback and Quality Assurance
```

#### 战略设计的迭代本质

定义限界上下文并不是从领域发现到实现的线性推进。
它是一个迭代的过程。
架构师常常需要先为一个领域建模,发现某个特定边界不符合技术或业务现实,然后回到起点重新设计。
这种头脑风暴与改进之间的反复,对于找到适合项目特定需求和约束的解决方案至关重要。

## 8. Section Recap

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/section-recap-54855802/) · 0:34

### 总结

本课总结了在领域驱动设计战略阶段中对限界上下文的探索。
它强调,定义限界上下文是一个受技术约束、组织资源和团队专业能力影响的迭代过程,最终目标是做出有依据的决策,而不是一步到位地追求完美。

### 核心概念

* **限界上下文的定义**:确立某个特定领域模型及其语言有效的边界。
* **实际约束**:技术栈、预算和团队专业能力对架构决策的影响。
* **迭代设计**:战略设计与技术实现之间持续的来回往复。
* **有依据的决策**:优先追求务实的、更好的决策,而不是理论上的完美。

### 课程笔记

定义限界上下文是领域驱动设计战略阶段中的基础性一步。
虽然首要目标是隔离领域模型及其各自的语言,但实际的边界往往由现实世界的约束所塑造。

特定的技术选择、预算限制以及开发团队现有的专业能力等因素,在决定如何划分上下文时起着重要作用。
这就需要一个递归的过程:战略设计指导技术设计,而技术现实又反过来提供反馈,以完善战略边界。

这一过程的最终目标不是立即实现一个完美、不可变的设计,而是在整个软件开发生命周期中促进更好的决策。
通过认识到领域需求与技术约束之间的相互作用,团队可以构建更具韧性、更易维护的架构。

战略设计过程的下一阶段涉及上下文映射(context mapping),它关注这些已定义的限界上下文之间的交互和关系。
