# Bounded Contexts

> 课程:[Getting Started: Domain-Driven Design](https://dometrain.com/course/getting-started-domain-driven-design-ddd/) · 第 9 章
> 共 6 课 · 约 27:11
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Bounded Contexts](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/bounded-contexts-54123503/) | 5:29 | [↓](#1-bounded-contexts) |
| 2 | [Bounded Contexts vs. Sub-Domains (Problem vs. Solution Space)](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/bounded-contexts-vs-sub-domains-problem-vs-solution-space-54123504/) | 5:17 | [↓](#2-bounded-contexts-vs-sub-domains-problem-vs-solution-space) |
| 3 | [Exploring our Domain’s Bounded Contexts](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/exploring-our-domains-bounded-contexts-54123505/) | 4:06 | [↓](#3-exploring-our-domains-bounded-contexts) |
| 4 | [Exploring relationships between our Bounded Contexts](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/exploring-relationships-between-our-bounded-contexts-54123506/) | 6:10 | [↓](#4-exploring-relationships-between-our-bounded-contexts) |
| 5 | [Defining our Context Map](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/defining-our-context-map-54123507/) | 5:00 | [↓](#5-defining-our-context-map) |
| 6 | [Section Recap](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/section-recap-54123508/) | 1:09 | [↓](#6-section-recap) |

## 1. Bounded Contexts

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/bounded-contexts-54123503/) · 5:29

### 总结

限界上下文(bounded context)是一个逻辑边界,它定义了某个特定领域模型的范围,确保术语和概念在该边界内保持一致。
通过分离关注点(例如把用户管理上下文中的 "User" 与计费上下文中的 "User" 区分开),开发者可以创建更聚焦、更易维护的模型,这些模型反映特定的业务需求,而不会让单个实体背负大量无关的属性。

### 核心概念

- **Logical Boundary(逻辑边界)**:领域模型在其中被定义并适用的明确边界。
- **Model Integrity(模型完整性)**:确保通用语言(Ubiquitous Language)中的术语在该上下文内只有单一、无歧义的含义。
- **Contextual Modeling(上下文建模)**:允许同一个现实世界实体(例如 User)在不同上下文中(例如 User Management 与 Billing)由不同的模型来表示。
- **Implementation Independence(实现独立性)**:虽然限界上下文经常被映射为微服务,但它是一种逻辑上的区分,也可以在单体中实现为模块或命名空间。

### 课程笔记

限界上下文被定义为一个领域模型在其中适用的逻辑边界。
这个概念是领域驱动设计(DDD)中战略设计的核心,因为它让团队可以把领域划分成若干可管理的部分、并在每一部分中让特定定义成立,从而管理复杂度。

设想一个健身房管理平台这样的系统。
在 **User Management** 上下文中,"User" 模型聚焦于身份和访问。
它包含个人详细信息,以及对该用户在系统中可能担任的各种角色的引用,例如管理员、参与者或教练。

```json
{
    "firstName": "Amichai",
    "lastName": "Mantinband",
    "adminId": "{GUID}",
    "participantId": "{GUID}",
    "trainerId": "{GUID}"
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/bounded-contexts-54123503/?t=130)

相反,在 **Billing** 上下文中,系统并不关心教练资料或管理员 ID。
在这里,"User"(或 Customer)模型聚焦于处理付款所需的财务属性,例如信用卡上的姓名和账单地址。

```json
{
    "fullNameOnCard": "Amichai Mantinband",
    "billingAddress": {
        "zipCode": "{zipCode}",
        "..": ".."
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/bounded-contexts-54123503/?t=130)

通过把它们定义为两个独立的限界上下文,术语 "User" 可以在两个地方同时存在,并拥有完全不同的定义。
这避免了创建一个 "God Object(上帝对象)",也就是一个包含几十个属性、而这些属性只在特定且互不相关的场景中才有意义的 User 类。
限界上下文提供了灵活性,让你可以根据所要解决的具体问题来决定是把模型放在一起还是拆分开。

```text
+---------- User Management ---------+  +------------- Billing --------------+
| User                               |  | User (Customer)                    |
|   firstName                        |  |   fullNameOnCard                   |
|   lastName                         |  |   billingAddress                   |
|   adminId                          |  |     zipCode                        |
|   participantId                    |  |                                    |
|   trainerId                        |  |                                    |
+------------------------------------+  +------------------------------------+
```

#### 限界上下文与实现单元

一个常见的问题是:限界上下文是否必须是一个微服务。
虽然业界经常把一个限界上下文映射为一个微服务(或模块化单体中的一个模块),但二者并不严格等同。
限界上下文是一个逻辑边界,不一定是一个物理部署单元。

在实践中:

- **Microservices(微服务)**:每个上下文都是一个独立的部署单元和应用程序。
- **Modular Monolith(模块化单体)**:每个上下文都是同一个程序集中的一个独立模块或命名空间。

关键在于开发团队认识到这条边界。
即使两个模型同名且位于同一个代码库中,只要它们属于不同的限界上下文,就必须被当作不同的实体来对待。
这种灵活性让架构师可以在沙地上画出逻辑边界,以最好地解决业务问题。

## 2. Bounded Contexts vs. Sub-Domains (Problem vs. Solution Space)

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/bounded-contexts-vs-sub-domains-problem-vs-solution-space-54123504/) · 5:17

### 总结

本课阐明了领域驱动设计中问题空间与解决方案空间之间的区别。
它把子领域(sub-domain)定义为业务问题的逻辑划分,把限界上下文定义为解决这些问题的技术边界。
通过把这些概念分开,开发者可以更好地规划如何把复杂的业务需求映射到软件架构上,例如在单个单体限界上下文与多个专门的限界上下文之间做出选择。

### 核心概念

* **Problem Space(问题空间)**:识别、定义并理解业务挑战的逻辑区域。
* **Solution Space(解决方案空间)**:为应对问题空间中识别出的挑战而设计和构建软件的技术区域。
* **Sub-domains(子领域)**:问题空间中的逻辑划分,代表特定的业务能力或子问题(例如 Billing、Scheduling)。
* **Bounded Contexts(限界上下文)**:解决方案空间中的明确边界,某个特定的领域模型在其中被定义并适用。
* **Strategic Mapping(战略映射)**:根据架构需要,把一个或多个子领域映射到一个或多个限界上下文的灵活性。

### 课程笔记

#### 问题空间

问题空间是我们定义软件必须解决的具体挑战的区域。
它涉及收集细节,以及理解与业务领域相关的各种因素。
随着我们对领域了解得更多(例如健身房课程预约系统的需求),我们会为问题的不同部分识别出逻辑边界。
这些逻辑划分被称为**子领域**。

在健身房应用的语境中,问题空间包含几个不同的子问题:

* **Session Scheduling**:管理名额、处理日程冲突,以及定义课程规则。
* **User Management**:处理不同的角色和权限,例如管理员与参与者。
* **Billing**:管理付款和会员费,它通常可以被视为独立于核心排课逻辑的一个领域。

#### 解决方案空间

解决方案空间是进行技术实现的地方。
它涵盖用于解决问题空间中所识别问题的架构、技术决策和代码。
在解决方案空间中定义边界的主要工具是**限界上下文**。

子领域是在问题空间中被发现的,而限界上下文是在解决方案空间中被设计的。
开发者可以灵活地决定二者之间如何对应:

* **Monolithic Approach(单体方式)**:单个限界上下文可以包裹整个问题空间,在一个系统内包含所有子领域(Billing、Scheduling 等)。
* **Modular or Microservices Approach(模块化或微服务方式)**:每个子领域可以被包裹在它自己专属的限界上下文中,也可以把多个相关的子领域归入同一个限界上下文。

```text
Problem Space (sub-domains)        Solution Space (bounded contexts)

Monolithic Approach:
  Session Scheduling --+
  User Management -----+-------->  [ one bounded context ]
  Billing -------------+

Modular or Microservices Approach:
  Session Scheduling ----------->  [ bounded context ]
  User Management -------------->  [ bounded context ]
  Billing ---------------------->  [ bounded context ]
```

目前,健身房应用是以单体方式实现的,这意味着由单个限界上下文处理各个领域模型之间的交互,并在整个问题空间中强制执行业务不变量。
不过,随着系统的增长,这些逻辑界线可以被重新划定,把关注点分离到不同的限界上下文中。

## 3. Exploring our Domain’s Bounded Contexts

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/exploring-our-domains-bounded-contexts-54123505/) · 4:06

### 总结

本课通过让解决方案空间中的限界上下文与问题空间中的子领域对齐,把 Dome Gym 应用从单个单体限界上下文过渡到模块化架构。
它把 User Management、Gym Management 和 Session Reservations 确定为主要的限界上下文,并强调:虽然它们在逻辑上被分离以提升敏捷性和可扩展性,但它们仍必须通过上下文映射(context mapping)维持明确定义的关系,以确保整个系统的一致性。

### 核心概念

- **Sub-domain Identification(子领域识别)**:把宽泛的问题空间拆解为具体的区域:User Management、Session Reservations、Billing 和 Gym Management。
- **Core Domain(核心领域)**:把 Session Reservations 确定为主要的价值驱动者,也是最值得投入开发资源的关键区域。
- **Bounded Context Alignment(限界上下文对齐)**:从单个 "Dome Gym" 上下文转向直接映射到各子领域的独立限界上下文。
- **Logical Boundaries(逻辑边界)**:实现边界(通过命名空间、模块或微服务),以解耦 Trainers、Admins 和 Subscriptions 等领域模型。
- **Context Mapping(上下文映射)**:定义各独立限界上下文之间的关系和通信模式。

### 课程笔记

Dome Gym 的问题空间(具体而言是健身房课程预约方面)由几个不同的子领域组成。
虽然最初的实现把所有领域模型归入一个名为 `DomeGym` 的单一限界上下文中,但成熟的领域驱动设计方法要求把它分解为反映底层子领域的具体限界上下文。

#### 已识别的子领域

1. **User Management**:处理用户定义、权限和个人资料。
2. **Session Reservations (Core Domain)**:业务中最关键的部分,聚焦于日程、参与者管理、教练分配以及日历冲突的解决。
3. **Gym Management**:管理实体和组织层面的基础设施,包括健身房、房间和订阅类型。
4. **Billing**:处理财务交易(不在本实现的范围内)。

#### 过渡到限界上下文

在最初的单体状态中,Trainers、Admins、Participants、Gyms、Rooms 和 Subscriptions 的模型全都共存于同一条边界之内。
为了提升敏捷性并为潜在的架构转变(例如迁移到微服务)做准备,解决方案空间被重构为让每个子领域都由它自己的限界上下文来表示。
这形成了一种逻辑上的分离:User Management 上下文中 "User" 的定义,与 Session Reservation 上下文中的 "Trainer" 或 "Participant" 是不同的。

```text
Before:
+------------------------------ DomeGym -------------------------------+
| Trainers  Admins  Participants  Gyms  Rooms  Subscriptions           |
+----------------------------------------------------------------------+

After:
+------------------+  +----------------------+  +------------------+
| User Management  |  | Session Reservations |  | Gym Management   |
|                  |  |   (Core Domain)      |  |                  |
+------------------+  +----------------------+  +------------------+
```

#### 上下文之间的关系

限界上下文并不是孤立存在的。
例如,当在 User Management 上下文中创建一个管理员资料时,Gym Management 上下文必须知道这一点,才能允许该用户管理健身房和房间。
建立这些关系需要应用上下文映射模式,以便可视化并实现跨边界的数据流和逻辑流。

## 4. Exploring relationships between our Bounded Contexts

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/exploring-relationships-between-our-bounded-contexts-54123506/) · 6:10

### 总结

本课探讨 User Management、Gym Management 和 Session Reservation 这几个限界上下文之间的交互与关系。
通过追踪一个用户的生命周期(从注册和创建资料,到管理健身房和房间),它展示了一个上下文中的数据和事件(例如创建管理员资料或创建房间)如何要求其他上下文中发生相应的动作或状态变化。
这一分析为构建上下文映射图打下基础,该映射图定义这些各不相同的领域模型如何协作,同时各自保持自己的内部不变量。

### 核心概念

* **Contextual Domain Models(上下文领域模型)**:同一个物理实体(例如 Room)可以存在于多个限界上下文中,拥有不同的属性和职责。
* **Cross-Context Synchronization(跨上下文同步)**:一个上下文(例如 User Management)中的动作,经常会触发在另一个上下文(例如 Gym Management 或 Session Reservation)中创建相关实体。
* **Source of Truth(事实来源)**:识别哪个上下文拥有某个实体的主要不变量(例如 Gym Management 拥有房间数量的不变量)。
* **Identity Propagation(标识传播)**:在各上下文之间保持一致的唯一标识符,以把相关的领域模型关联起来。

### 课程笔记

要理解限界上下文之间的关系,我们必须审视每个上下文中包含的领域模型,以及它们在常见的应用流程中如何交互。

#### 限界上下文的定义

我们的系统被划分为三个主要的限界上下文,每个上下文都有一组特定的领域模型:

1.  **User Management Context**:包含 `User` 领域模型,它管理用户资料(Admin、Participant、Trainer)和系统范围的权限。
2.  **Gym Management Context**:包含 `Admin`、`Subscription`、`Gym` 和 `Room` 领域模型。
    这个上下文负责健身房在结构和管理方面的事务。
3.  **Session Reservation Context**:包含 `Room`、`Session`、`Participant` 和 `Trainer` 领域模型。
    这个上下文聚焦于健身课程的排期和执行。

```text
+-- User Management --+  +-- Gym Management --+  +-- Session Reservation --+
| User                |  | Admin              |  | Room                    |
|                     |  | Subscription       |  | Session                 |
|                     |  | Gym                |  | Participant             |
|                     |  | Room               |  | Trainer                 |
+---------------------+  +--------------------+  +-------------------------+
```

#### 用户注册与资料流程

当一个用户在应用中注册时,流程从 **User Management** 上下文开始。
一个 `User` 被创建,并被分配一个唯一标识符。
这个 ID 在整个系统中充当该个体的主要引用。

当该用户决定创建一个 **Admin** 资料时,上下文之间就建立了一种关系。
虽然资料的创建是在 User Management 中发起的,但 `Admin` 领域模型位于 **Gym Management** 上下文中。
因此,Gym Management 上下文必须收到这一事件的通知,以便使用 User Management 上下文生成的同一个唯一 ID 来创建它自己对 Admin 的表示。
这确保了 Gym Management 中的 Admin 实体被正确地映射到 User Management 中的 User 实体。

类似地,当用户创建一个 **Participant** 或 **Trainer** 资料时,**Session Reservation** 上下文必须收到通知,以便在它自己的边界内创建对应的 `Participant` 或 `Trainer` 领域模型。

```text
User Management                     Gym Management
  User (unique ID) --- Admin profile ---> Admin (same ID)

User Management                     Session Reservation
  User (unique ID) --- Participant ----> Participant (same ID)
                   --- Trainer --------> Trainer (same ID)
```

#### 健身房与房间管理

在 Gym Management 上下文内,Admin 可以创建一个 `Subscription` 和一个 `Gym`。
这些动作目前是局部的;其他上下文在执行各自的特定操作时,不需要知道订阅的详细信息,也不需要知道该健身房实体的存在。

然而,在健身房中创建一个 `Room` 会引入跨上下文的需求。
**Gym Management** 上下文充当创建房间的事实来源,因为它强制执行这样一个不变量:一个健身房拥有的房间数不能超过其订阅所允许的数量。
在这个上下文中,`Room` 由它的名称以及它与健身房容量之间的关系来定义。

一旦房间在 Gym Management 中创建成功,就必须通知 **Session Reservation** 上下文。
Session Reservation 上下文维护着它自己对 `Room` 的定义,这个定义聚焦于排期。
它需要知道该房间存在,这样才能管理该房间内的 `Session` 对象,确保没有相互冲突的日程。
通过把房间的唯一 ID 从 Gym Management 传播到 Session Reservation,系统让排期逻辑可以作用于同一个物理资源,同时保持管理关注点与排期关注点相互解耦。

```text
Gym Management                         Session Reservation
  Room (name, gym capacity) --- room ID ---> Room (scheduling)
                                               Session
```

## 5. Defining our Context Map

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/defining-our-context-map-54123507/) · 5:00

### 总结

本课为健身房管理系统定义上下文映射图(Context Map),说明 User Management、Gym Management 和 Session Reservation 这几个限界上下文之间的关系和集成点。
它介绍了上游(upstream)和下游(downstream)依赖的概念,并解释了防腐层(Anti-Corruption Layer,ACL)在保护限界上下文的内部领域模型免受外部变化影响方面所起的作用。
此外,它还探讨了这些关系如何转化为基础设施需求,例如为异步通信集成消息代理(message broker)。

### 核心概念

- 限界上下文集成
- 上游与下游关系
- 防腐层(ACL)
- 通过消息代理进行异步通信

### 课程笔记

上下文映射图可视化了 User Management、Gym Management 和 Session Reservation 这几个限界上下文之间的集成。
这些集成由数据的流向以及一个上下文对另一个上下文的影响来定义。
例如,在 Gym Management 中创建房间需要与 Session Reservation 同步,而在 User Management 中创建参与者或教练资料则要求在 Session Reservation 中进行相应的更新。

映射图中的关系被定义为上游(U)和下游(D)。
上游上下文是数据或变化的来源,而下游上下文消费这些数据并受到这些变化的影响。
在这个系统中,User Management 是 Gym Management 和 Session Reservation 两者的上游。
Gym Management 是 Session Reservation 的上游。

```text
                 +-------------------+
                 |  User Management  |
                 +-------------------+
                   U |           | U
                     |           |
                   D v           |
        +-------------------+    |
        |  Gym Management   |    |
        +-------------------+    |
                   U |           |
                     |           |
                   D v         D v
                 +-----------------------+
                 |  Session Reservation  |
                 +-----------------------+
```

为了维护限界上下文的完整性,会采用防腐层(ACL)。
ACL 是由下游上下文实现的一个转换层。
它把从上游上下文接收到的领域模型映射为下游上下文的内部领域模型。
这防止了下游领域被外部的结构(schema)变化所"腐化"。
如果某个上游模型发生了变化(例如增加了新的属性),只需要更新 ACL,从而让内部领域逻辑免受这一变化的影响。

```text
Upstream context          Downstream context
  domain model  ------->  [ ACL ] ---> internal domain model
```

上下文映射图也为基础设施设计提供了依据,特别是在使用消息代理方面。
在这一架构中,User Management 作为一个独立的上下文;它发布事件,但不消费来自其他上下文的消息。
Session Reservation 是一个纯粹的消费者,监听来自 User Management 和 Gym Management 两者的消息。
Gym Management 既是发布者(发送房间更新),也是消费者(接收用户更新)。

```text
User Management --publishes--> [ message broker ] --> Gym Management
                                                  --> Session Reservation
Gym Management  --room updates--> [ message broker ] --> Session Reservation
```

## 6. Section Recap

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/section-recap-54123508/) · 1:09

### 总结

本课总结了领域驱动设计中限界上下文的核心原则,强调它们作为解决方案空间中逻辑边界的作用。
它突出了子领域与限界上下文之间的区别、上下文映射图在可视化系统关系方面的用处,并为在分布式架构中通过事件集成这些上下文做好铺垫。

### 核心概念

*   **Bounded Context(限界上下文)**:解决方案空间中的一个逻辑边界,领域模型在其中具有特定且一致的含义。
*   **Strategic Design(战略设计)**:DDD 中聚焦于把一个大型系统分解为更小、可管理的限界上下文的阶段。
*   **Problem Space vs. Solution Space(问题空间与解决方案空间)**:子领域代表问题空间,而限界上下文代表解决方案空间;二者不一定是一对一的映射。
*   **Context Maps(上下文映射图)**:用于定义和查看不同限界上下文之间关系与依赖的可视化工具。
*   **Integration Events(集成事件)**:在分布式或基于微服务的系统中,限界上下文之间进行通信的机制。

### 课程笔记

限界上下文是软件系统解决方案空间中一条至关重要的逻辑边界。
在这条边界内,每个领域模型和术语都有特定且无歧义的含义。
定义这些上下文是领域驱动设计战略阶段的一个基础部分,因为它决定了一个复杂系统如何被分解为更小、更聚焦的子系统。

区分子领域和限界上下文是很重要的。
子领域存在于问题空间中(即我们所应对的业务现实),而限界上下文存在于解决方案空间中(即技术实现)。
虽然一个简单的系统可能在二者之间存在一对一的映射,但它们是不同的概念,并且在复杂架构中经常以不同的方式映射。

为了理解这些不同的上下文如何交互,开发者会使用上下文映射图。
这些映射图对不同限界上下文之间的关系、契约和数据流提供可视化的表示,确保整体系统架构是连贯的。

随着系统从单体演进为分布式或基于微服务的架构,这些限界上下文之间的集成变得至关重要。
这通常通过一个使用集成事件的消息代理来处理,让彼此分离的上下文可以异步通信,同时维持各自的内部一致性和边界。
