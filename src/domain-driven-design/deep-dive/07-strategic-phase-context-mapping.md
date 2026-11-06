# Strategic Phase - Context Mapping

> 课程:[Deep Dive: Domain-Driven Design](https://dometrain.com/course/deep-dive-domain-driven-design-ddd/) · 第 7 章
> 共 14 课 · 约 47:34
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Refresher - What is Context Mapping](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/refresher-what-is-context-mapping-54855845/) | 2:53 | [↓](#1-refresher---what-is-context-mapping) |
| 2 | [Team Relationships](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/team-relationships-54855846/) | 3:57 | [↓](#2-team-relationships) |
| 3 | [Context Map Patterns](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/context-map-patterns-54855847/) | 10:44 | [↓](#3-context-map-patterns) |
| 4 | [Context Mapping In Action - Setup & Context](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/context-mapping-in-action-setup-context-54855848/) | 3:36 | [↓](#4-context-mapping-in-action---setup--context) |
| 5 | [Creating an Admin Profile](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/creating-an-admin-profile-54855849/) | 8:58 | [↓](#5-creating-an-admin-profile) |
| 6 | [Creating a Subscription](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/creating-a-subscription-54855850/) | 2:21 | [↓](#6-creating-a-subscription) |
| 7 | [Creating a Gym](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/creating-a-gym-54855852/) | 2:35 | [↓](#7-creating-a-gym) |
| 8 | [Creating a Room](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/creating-a-room-54855853/) | 2:32 | [↓](#8-creating-a-room) |
| 9 | [Creating a Session](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/creating-a-session-54855854/) | 3:09 | [↓](#9-creating-a-session) |
| 10 | [Creating a Participant Profile](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/creating-a-participant-profile-54855855/) | 1:21 | [↓](#10-creating-a-participant-profile) |
| 11 | [Reserving a Spot in a Session](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/reserving-a-spot-in-a-session-54855856/) | 1:04 | [↓](#11-reserving-a-spot-in-a-session) |
| 12 | [Context Mapping In Action - Conclusion](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/context-mapping-in-action-conclusion-54855857/) | 1:11 | [↓](#12-context-mapping-in-action---conclusion) |
| 13 | [Strategic Goals Alignment](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/strategic-goals-alignment-54855858/) | 2:36 | [↓](#13-strategic-goals-alignment) |
| 14 | [Section Recap](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/section-recap-54855859/) | 0:37 | [↓](#14-section-recap) |

## 1. Refresher - What is Context Mapping

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/refresher-what-is-context-mapping-54855845/) · 2:53

### 总结

上下文映射(context mapping)是一种战略性的领域驱动设计(DDD)技术,用于以可视化的方式呈现系统内各个限界上下文(bounded context)之间的关系与集成点。
通过定义这些交互,团队可以厘清系统架构,在设计阶段尽早识别潜在的集成问题,并让解决方案不同部分的开发工作保持一致。

### 核心概念

* **Context Maps(上下文映射图)**:对限界上下文之间的关系与集成的可视化呈现。
* **Integration Points(集成点)**:系统不同部分发生交互的、被清晰定义的区域。
* **Architectural Clarity(架构清晰度)**:借助映射图来理解系统的高层结构和依赖关系。
* **Team Alignment(团队对齐)**:确保所有利益相关者都理解各个上下文之间的技术关系和组织关系。
* **Context Map Patterns(上下文映射模式)**:用于定义特定类型集成的标准化模型。

### 课程笔记

上下文映射是一种战略性的 DDD 工具,用于可视化限界上下文之间的相互依赖和通信路径。
虽然限界上下文为领域模型定义了逻辑边界,但它们必须相互集成,才能支撑端到端的业务流程。
上下文映射图就是这些集成的正式文档。

实施上下文映射有几项好处。
它通过明确定义不同组件如何交互,厘清整体的系统架构。
这种清晰度有助于在设计过程中尽早识别潜在的集成问题,从而避免在实现阶段犯下代价高昂的错误。
此外,上下文映射图通过提供对集成点以及团队之间组织关系的共同理解,促进团队对齐,无论这些团队是在系统的同一部分还是不同部分上工作。

为了精确地定义这些交互,实践者会使用上下文映射模式。
这些模式提供了一种描述两个上下文之间集成性质的正式语言。
这些定义的一个主要资源是 DDD Crew 在 GitHub 上的 "Context Mapping" 仓库。
这个仓库是模式定义、团队关系和可视化表示的事实来源,并且包含在实践中实施上下文映射的各种工具和速查表。
在本节中,所讨论的团队关系和模式都将与该仓库中确立的定义保持一致。

## 2. Team Relationships

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/team-relationships-54855846/) · 3:57

### 总结

本课探讨领域驱动设计上下文映射中的三种基本团队关系:Mutually Dependent(相互依赖)、Upstream/Downstream(上游/下游)和 Free(自由)。
它详细说明了这些关系如何界定团队之间的协调要求、发布周期和依赖关系,同时强调在真实环境中映射限界上下文时需要保持组织层面的务实。

### 核心概念

- **Mutually Dependent(相互依赖)**:两个团队必须紧密协调才能交付一个可用系统的关系。
- **Upstream/Downstream(上游/下游)**:一种有方向的关系,一个团队(上游)的行为会影响另一个团队(下游),但反过来不一定如此。
- **Free(自由)**:限界上下文之间完全独立、不存在任何技术或组织联系的状态。
- **Organizational Pragmatism(组织务实)**:让上下文映射图与现有组织结构和实际约束保持一致的必要性。

### 课程笔记

#### 相互依赖关系

在相互依赖的关系中,两个团队或限界上下文被绑定在一起,以至于任何一方没有另一方都无法成功或完整运作。
在上下文映射图中,这种关系用一条连接两个上下文的粗线来表示。

如果两个团队必须把各自的软件制品作为一个整体来交付,它们就被视为相互依赖。
这通常要求在功能开发期间进行高度协调,以确保产出属于同一个发布周期。
这些团队的数据、功能和能力之间通常存在紧密的相互联系。
由于一个团队无法独立于另一个团队发布其制品,它们的命运是交织在一起的。

#### 上游与下游关系

上游/下游关系最好借助河流的比喻来理解。
上游团队做出的变更会流向下游团队。

- **Upstream (U)(上游)**:这个团队是独立的。他们可以发布新功能和新体验,而不会被下游服务阻塞。上游团队的成功并不依赖于下游团队的命运。
- **Downstream (D)(下游)**:这个团队依赖于上游团队。上游采取的行动会对下游团队的工作产生重大影响。因此,下游团队可能会被阻塞,常常不得不等待上游提供特定的功能或数据,然后才能实现自己的需求。

```text
  Upstream (U)
      |
      |  变更向下流动
      v
  Downstream (D)
```

#### 自由关系

当两个团队或限界上下文彼此之间没有任何依赖时,就存在 "Free"(自由)关系。
在这种情况下,一个团队的成功或失败对另一个团队没有影响。
它们之间没有组织上或技术上的联系,使两个团队都能完全隔离地工作和发布功能。

#### 组织层面的考量与务实

上下文映射并不是一项纯粹的技术活动;它深受软件背后的人和团队的影响。
在定义限界上下文时,保持务实至关重要。
在许多组织中,改变现有的团队结构是不现实或不可能的。

因此,上下文映射的决策必须考虑组织层面的关注点。
团队当前的组织方式往往会决定上下文之间的边界和关系。
有效的上下文映射需要在理想的技术架构与组织的社会和结构约束这一现实之间取得平衡。

## 3. Context Map Patterns

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/context-map-patterns-54855847/) · 10:44

上下文映射模式对限界上下文之间的关系和集成点进行分类,定义领域模型和团队如何交互。

### 核心概念

- **Open Host Service (OHS)(开放主机服务)**:通过一个稳定、定义良好的接口(API)对外暴露服务的上下文。
- **Conformist(遵奉者)**:严格遵循上游领域模型以简化集成的下游上下文。
- **Anti-Corruption Layer (ACL)(防腐层)**:保护下游模型免受上游变更影响的转换层。
- **Shared Kernel(共享内核)**:两个上下文共用的领域模型子集,需要高度协调。
- **Partnership(合作关系)**:相互依赖的上下文之间成败与共的关系。
- **Customer-Supplier(客户-供应商)**:上游(Supplier,供应商)优先考虑下游(Customer,客户)需求的关系。
- **Published Language(发布语言)**:用于不同系统之间无缝通信的标准化数据格式(例如 iCalendar)。
- **Separate Ways(各行其道)**:对彼此之间没有显著关系或集成的上下文的一种标记。
- **Big Ball of Mud(大泥球)**:一个混乱、建模糟糕的系统,应当加以隔离,以防止它的设计泄漏到其他上下文中。

### 课程笔记

上下文映射模式为不同限界上下文之间如何相互关联提供了词汇和结构化的定义。
这些模式帮助架构师和开发者理解一个内聚的系统所需的依赖关系和通信协议。

**Open Host Service (OHS)**
开放主机服务是一个通过稳定、定义良好的接口对外暴露其功能的限界上下文。
这本质上是一个正式的 API,其中提供方充当上游,消费方充当下游。
通过提供一个稳定的接口,上游上下文允许多个消费方进行集成,而无需为每个消费方定制转换。

**Conformist**
在遵奉者关系中,下游上下文选择严格遵循上游上下文的领域模型。
当下游团队对上游模型没有影响力,但又希望消除转换的复杂性时,常常会使用这种模式。
例如,如果 Session Reservation 上下文使用的是 Gym Management 上下文所定义的完全相同的 Room 模型,那么它就是在遵奉。
虽然这简化了集成并确保了共享的通用语言,但它让下游上下文高度依赖于上游的设计决策。

**Anti-Corruption Layer (ACL)**
防腐层是由下游上下文实现的一个转换组件,用于保护其内部领域模型。
当下游消费来自上游上下文的数据时,ACL 会把这些数据转换为下游的内部表示。
这保护下游免受上游模型变更所造成的 "corruption"(腐化),使下游无论外部如何变化都能维持自己理想的领域模型。

```text
  Upstream context            Downstream context
+------------------+       +-----+------------------------+
| upstream model   |------>| ACL | internal domain model  |
+------------------+       +-----+------------------------+
                          (转换为下游的内部表示)
```

**Shared Kernel**
当两个限界上下文共用其领域模型的某个特定子集时,它们就是在使用共享内核。
这会在两个上下文之间引入显著的耦合,因为对共享代码或数据结构的任何变更都需要双方团队的同意。
由于这种开销,建议让共享内核尽可能小。

**Partnership**
在合作关系中,两个上下文是相互依赖的。
一个上下文的开发和成功与另一个直接绑定。
这种关系需要高度协调,因为一个上下文中的变更必须与另一个同步,以避免系统范围的故障。

**Customer-Supplier**
这种模式定义了一种上游(Supplier,供应商)和下游(Customer,客户)关系,其中上游团队有动力去满足下游团队的需求。
在这种安排中,上游团队会在其规划和优先级排序过程中考虑下游的需求。

**Published Language**
发布语言是一种用于上下文之间通信的、标准化且广为人知的数据格式。
例子包括 iCalendar 或 vCard 这样的行业标准。
这使得内部模型完全不同的上下文能够无缝通信,方法是把各自的内部数据转换为共同的发布语言。

**Separate Ways**
当两个限界上下文之间几乎没有关系时,就使用这种模式。
各个团队可以自由地独立开发自己的模型,它们之间也没有技术集成。
当这些领域真正彼此正交时,这是理想的状态。

**Big Ball of Mud**
大泥球代表系统中建模糟糕、缺乏清晰边界,或者已经变成一团混合模型的那一部分。
当一个定义良好的限界上下文必须与这样的系统交互时,应当把后者标记为大泥球。
开发者必须确保这种糟糕的模型质量不会传播到其他上下文中,通常的做法是在边界处使用防腐层。

## 4. Context Mapping In Action - Setup & Context

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/context-mapping-in-action-setup-context-54855848/) · 3:36

### 总结

本课通过定义 User Management、Gym Management、Session Reservation 和 Billing 这几个限界上下文的具体职责,为上下文映射奠定基础。
它提倡一种面向流程的上下文映射方法,即针对具体的业务流程分别创建映射图,而不是为整个系统绘制一张单一、复杂的图。
通过识别核心职责以及一条主要的端到端流程(从创建档案到预约课程),本课提供了一个分析上下文之间上游与下游关系的框架。

### 核心概念

- **Process-Specific Context Maps(特定流程的上下文映射图)**:避免使用单一的 "God" 上下文映射图,转而使用为特定业务流程建模的较小映射图,以保持清晰和可扩展。
- **Bounded Context Responsibilities(限界上下文职责)**:定义 User Management、Gym Management、Session Reservation 和 Billing 的具体职责。
- **Dynamic Relationships(动态关系)**:认识到同样两个上下文之间的上游和下游角色可能会随具体的集成场景而转换。
- **End-to-End Flow Modeling(端到端流程建模)**:为从创建档案、搭建基础设施到安排课程和预约这一整个生命周期建模。

### 课程笔记

上下文映射不应产出一张代表系统中所有集成的单一、庞大的图。
这种 "God" 上下文映射图难以维护,缺乏可扩展性,而且常常无法体现双向关系的微妙之处,即一个上下文在某个场景中是上游,而在另一个场景中是下游。
相反,上下文映射图应当为回答具体问题或为离散的业务流程建模而创建。

#### 限界上下文的职责

系统被划分为四个主要的限界上下文,每个都有一组明确的职责:

1. **User Management**:负责身份与访问相关的关注点,包括用户注册、身份验证和档案管理。它负责创建特定角色,例如 Admin、Participant 和 Trainer 档案。
2. **Gym Management**:管理该领域的物理和逻辑基础设施。它的职责包括订阅管理、健身房实体管理和房间分配。
3. **Session Reservation**:管理健身房活动在时间和事务方面的内容。它负责创建课程(session)、预约名额和查询日程。
4. **Billing**:封装系统中所有与财务和支付相关的逻辑。

#### 集成过程

为了在实践中演示上下文映射模式,下面这个端到端流程被用来界定各个上下文之间的关系:

1. **Admin Profile Creation**:用户在 User Management 上下文中建立一个管理员身份。
2. **Subscription Management**:管理员在 Gym Management 上下文中创建一个订阅。
3. **Gym Creation**:管理员在 Gym Management 上下文中定义一个健身房实体。
4. **Room Creation**:管理员在该健身房实体中定义具体的房间。
5. **Session Scheduling**:管理员在 Session Reservation 上下文中创建一个课程。
6. **Participant Profile Creation**:用户在 User Management 上下文中建立一个参与者身份。
7. **Session Reservation**:参与者通过 Session Reservation 上下文在一个已安排的课程中预约一个名额。

```text
User Management      Gym Management       Session Reservation
---------------      --------------       -------------------
1. Admin Profile
                     2. Subscription
                     3. Gym
                     4. Room
                                          5. Session
6. Participant
   Profile
                                          7. Reserve a spot
```

## 5. Creating an Admin Profile

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/creating-an-admin-profile-54855849/) · 8:58

### 总结

本课探讨跨多个限界上下文创建管理员档案的架构过程,重点关注 User Management 与 Gym Management 之间的交互。
它详细说明了领域模型如何分布以维持各自特定的职责(例如档案管理和订阅跟踪),同时强制执行诸如每个用户只能有一个管理员这样的不变量。
本课评估了多种上下文映射模式(包括 Shared Kernel 和 Conformist),最后得出结论:防腐层(ACL)是解耦系统不同部分中 'Admin' 各自不同表示的最有效方式。

### 核心概念

- **Bounded Context Responsibility(限界上下文职责)**:User Management 处理档案生命周期和身份,而 Gym Management 处理订阅和运营数据。
- **Domain Invariants(领域不变量)**:系统必须强制保证单个用户最多拥有一个管理员档案、一个参与者档案和一个教练档案。
- **Upstream/Downstream Dynamics(上游/下游动态)**:User Management 充当档案数据的上游提供方,而 Gym Management 是下游消费方。
- **Context Mapping Patterns(上下文映射模式)**:评估 Shared Kernel(共享定义)、Conformist(直接采用上游模型)和 Anti-Corruption Layer(转换层)。
- **Point of Truth(事实来源)**:Gym Management 上下文是判断管理员是否拥有有效订阅的权威来源。

### 课程笔记

#### User Management 中的领域模型

User Management 上下文主要负责档案管理。
在这个上下文中,领域模型包括 `User` 实体以及多种档案表示:`Admin`、`Participant` 和 `Trainer`。
当用户创建管理员档案时,客户端与 User Management 上下文交互来操作这些模型。

这种交互有两个目的:
1. **Logical Division(逻辑划分)**:档案管理是 User Management 领域的核心职责。
2. **Invariant Enforcement(不变量的强制执行)**:`User` 领域模型充当守门人,确保用户不会创建重复的档案(例如确保每个用户最多只有一个管理员档案)。对 `User` 状态的更新和 `Admin` 档案的创建,在这个上下文中构成一次原子变更。

#### 跨上下文集成

在 User Management 中创建管理员档案会对 Gym Management 上下文产生下游影响。
虽然 User Management 拥有档案的存在,但 Gym Management 是订阅的事实来源。
因此,当创建管理员档案时,也必须在 Gym Management 上下文中实例化一个对应的 `Admin` 领域模型,以跟踪订阅状态。

在这种关系中,User Management 是上游(**Upstream**)上下文,Gym Management 是下游(**Downstream**)上下文。
上游中的变更会流向下游,以触发必要的副作用。

```text
User Management (U)              Gym Management (D)
+--------------------+          +----------------------+
| User               |          | Admin                |
| Admin              |--------->|  (跟踪订阅状态)      |
| Participant        |          |                      |
| Trainer            |          |                      |
+--------------------+          +----------------------+
```

#### 选择上下文映射模式

对于 User Management 中的 `Admin` 模型与 Gym Management 中的 `Admin` 模型之间的关系,有几种建模方式:

- **Shared Kernel**:两个上下文可以共用同一个 `Admin` 定义。不过,这需要高度协调,并会造成紧耦合。通常最好让共享内核保持很小,以避免跨团队的依赖。
- **Conformist**:Gym Management 上下文可以直接使用 User Management 所定义的完全相同的领域模型。当上游拥有大部分数据时(例如使用 GitHub 的仓库模型这样的第三方 API),这种做法很常见。如果下游对该模型没有独特的需求,这种方式是高效的。
- **Anti-Corruption Layer (ACL)**:当这些模型尽管名称相同却代表不同的概念时,这是首选的方法。在 Gym Management 中,`Admin` 模型需要订阅相关的数据,而 User Management 上下文不应该知道这些数据。

通过实现防腐层,Gym Management 上下文保护了它的领域完整性。
ACL 接收来自 User Management 上下文的数据,并将其转换为适合 Gym Management 的表示。
这确保了松耦合,使 Gym Management 中的 `Admin` 模型能够独立演进,以支持订阅和房间管理逻辑。

## 6. Creating a Subscription

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/creating-a-subscription-54855850/) · 2:21

### 总结

在领域驱动设计架构中,创建订阅的过程涉及定义 Gym Management 与 Billing 这两个限界上下文之间的关系。
本课详细说明在这两个上下文之间建立的 Customer-Supplier 关系,其中 Billing 上下文充当支付服务的上游提供方,而 Gym Management 上下文充当依赖这些服务来完成订阅逻辑的下游消费方。

### 核心概念

* Customer-Supplier relationship(客户-供应商关系):一种上下文映射模式,其中一个上下文(供应商)向另一个上下文(客户)提供服务。
* Upstream/Downstream(上游/下游):依赖的方向;上游上下文是独立的,而下游上下文依赖于上游。
* Context Integration(上下文集成):Gym Management 与 Billing 之间针对金融交易的具体交互。

### 课程笔记

管理订阅是 Gym Management 限界上下文的主要职责。
创建订阅时,系统必须与 Billing 上下文交互,以向管理员收取订阅费用。
这种交互在两个上下文之间形成了直接的依赖。

这种关系被归类为 Customer-Supplier 关系。
在这个映射中,Billing 上下文充当供应商,而 Gym Management 上下文充当客户。

为了确定这种关系的方向,我们要识别上游和下游组件。
Billing 上下文是上游,因为它提供服务(向管理员收费),并且在功能上独立于 Gym Management 上下文。
Gym Management 上下文是下游,因为它依赖 Billing 上下文来完成自己的工作流。
在任何 Customer-Supplier 关系中,供应商都是上游,客户都是下游。

```text
Billing (Supplier, U) ------> Gym Management (Customer, D)
```

在设计的这个阶段,订阅领域模型包含在 Gym Management 上下文中。
对于这个特定场景,目前没有其他限界上下文需要了解订阅,也没有其他限界上下文维护相关的领域模型。

## 7. Creating a Gym

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/creating-a-gym-54855852/) · 2:35

### 总结

本课审视在 Gym Management 限界上下文中创建 Gym 所涉及的架构决策,重点在于识别健身房创建约束的事实来源,以及评估跨上下文的依赖。
它讨论了 Subscription 与 Gym 之间的关系、Gym 模型与 Session Reservation 上下文的相关性,以及集成事件在跨上下文边界保持一致性方面的作用。

### 核心概念

- **Source of Truth(事实来源)**:确定由哪个领域模型(Admin 还是 Subscription)负责强制执行诸如健身房数量上限这样的业务不变量。
- **Context Boundaries(上下文边界)**:评估一个上下文(Gym Management)中的领域模型是否需要在另一个上下文(Session Reservation)中有一个表示。
- **Integration Events(集成事件)**:跨限界上下文传达状态变更、以确保最终一致性的机制。
- **Deferred Decision Making(推迟决策)**:认识到何时在单个上下文中敲定具体的模型职责还为时过早。

### 课程笔记

Gym 实体是 **Gym Management** 限界上下文的核心组件。
一个关键的设计考虑是确定由哪个领域模型充当校验健身房创建的事实来源。
由于订阅通常会限制用户可以管理的健身房数量,强制执行这一约束的职责从逻辑上可以落在 `Admin` 或 `Subscription` 模型上。
由于这两个实体都位于 Gym Management 上下文中,在开发的这个阶段,由哪个模型来处理添加健身房的决定是灵活的。

此外,还必须评估创建健身房对其他限界上下文(例如 **Session Reservation**)的影响。
Session Reservation 上下文专注于管理课程、房间可用性、参与者预约和教练分配。
目前,Gym 本身不需要在 Session Reservation 上下文中有表示,因为房间和课程模型已经为预约逻辑提供了足够的细节。
这种隔离确保 Session Reservation 上下文始终专注于它自己的子领域,而不会背负来自 Gym Management 上下文的不必要开销。

在领域驱动设计中,当一个限界上下文中的变更影响到另一个时,这种交互通过集成事件(**Integration Events**)来管理。
如果 Session Reservation 上下文最终需要知道健身房的创建,Gym Management 上下文就会发布一个集成事件。
这个事件会在 Session Reservation 上下文中触发必要的状态变更,以保持整个系统的一致。
不过就目前而言,创建健身房被视为 Gym Management 上下文的内部关注点。

## 8. Creating a Room

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/creating-a-room-54855853/) · 2:32

### 总结

本课探讨在 Gym Management 上下文中创建房间时领域逻辑的战略性放置,以及这些信息如何传播到 Session Reservation 上下文。
它着重说明了决定不变量(例如基于订阅等级的房间数量上限)应当放在哪里的决策过程,并强调使用防腐层(ACL)来保护下游模型免受上游变更的影响。

### 核心概念

*   **Invariant Placement(不变量的放置)**:确定应由 Gym、Subscription 还是 Admin 实体来强制执行创建房间数量上限的业务规则。
*   **Context Integration(上下文集成)**:在资源创建过程中 Gym Management(上游)与 Session Reservation(下游)之间的关系。
*   **Anti-Corruption Layer (ACL)(防腐层)**:通过把上游数据转换为符合下游特定需求的本地表示,来保护下游领域模型。
*   **Open Host Service (OHS)(开放主机服务)**:一种常见的上游模式,经常与下游的 ACL 搭配使用,以提供一个稳定的集成点。

### 课程笔记

#### 确定事实来源 (Point of Truth)

创建房间时,系统必须强制执行特定的不变量,例如确保房间数量不超过健身房订阅所规定的上限。
在领域模型中,这段逻辑有几个合理的放置位置:

1.  **The Gym Entity(Gym 实体)**:实现一个类似 `gym.AddRoom()` 的方法,让 Gym 负责校验新增一个房间是否符合当前订阅。
2.  **The Subscription Entity(Subscription 实体)**:房间可以被看作属于某个订阅的资源。在这种模型中,Subscription 领域模型持有该不变量,并根据订阅类型决定是否可以添加新资源。
3.  **The Admin Entity(Admin 实体)**:Admin 可以充当协调者,在检查订阅类型以确定该操作是否被允许之后再创建房间。

#### 跨上下文集成

一旦在 Gym Management 上下文中创建了房间,就必须通知 Session Reservation 上下文,因为它需要知道这个房间才能安排课程。
在这种关系中,Gym Management 上下文充当上游(**Upstream**),Session Reservation 上下文充当下游(**Downstream**)。

#### 分歧的领域模型与 ACL

"Room" 的表示在不同的限界上下文中在逻辑上是不同的。
Gym Management 关注的是房间的存在及其管理细节,而 Session Reservation 上下文把房间看作课程和特定器材的容器。

由于数据需求和行为不同,Session Reservation 上下文不应直接使用上游的房间模型。
相反,它会实现一个防腐层(**Anti-Corruption Layer (ACL)**)。
这一层接收来自 Gym Management 上下文的通知,并把数据转换为由 Session Reservation 上下文拥有和控制的内部表示。
这保护下游领域,使其不必在上游模型每次演进时都被迫随之变更。

```text
Gym Management (U)            Session Reservation (D)
+--------------+  通知   +-----+---------------------------+
| Room         |-------->| ACL | Room                      |
| (管理细节)   |         |     | (课程和特定器材的容器)    |
+--------------+         +-----+---------------------------+
```

#### 常见的架构模式

上下文映射中一种常见的组合是在上游使用开放主机服务(**Open Host Service (OHS)**),在下游使用防腐层(**Anti-Corruption Layer (ACL)**)。
例如,一个 Billing Management 上下文可以提供一个开放主机服务(一个稳定的 API),各个下游上下文通过各自的 ACL 来消费它,以维护自己的领域完整性。

## 9. Creating a Session

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/creating-a-session-54855854/) · 3:09

### 总结

在 Session Reservation 上下文中,Room 领域模型充当安排课程的权威事实来源。
它负责强制执行业务不变量,例如防止时间段重叠,以及根据管理员的订阅等级限制每天的课程总数。
通过把这些规则集中在 Room 聚合中,系统确保了原子的状态转换并保持一致性,而不需要其他限界上下文去跟踪细粒度的排期数据。

### 核心概念

- **Room as Source of Truth(Room 作为事实来源)**:Room 领域模型管理内部日历并校验所有排期请求。
- **Invariant Enforcement(不变量的强制执行)**:领域模型必须确保系统从一个有效状态转移到另一个有效状态,例如检查时间段的可用性和每日课程上限。
- **Cross-Context Synchronization(跨上下文同步)**:使用集成事件把变更从 Subscription 上下文传达到 Session Reservation 上下文,以更新房间的约束。
- **Atomic State Changes(原子状态变更)**:安排一个课程和更新房间的内部日程,在领域模型中作为单个原子操作发生。

### 课程笔记

创建课程的过程主要由 **Session Reservation Context** 管理。
在这个上下文中,`Room` 领域模型充当排期逻辑的事实来源。
当有请求要在某个特定时间段安排课程时,`Room` 模型会检查它的内部日程,以确认该时间段没有被其他课程占用。
如果该时间段空闲,`Room` 就更新其内部状态,把新课程包含进来。
这次更新构成了转换系统状态所需的原子变更。

#### 强制执行业务不变量

领域模型的一项关键职责是强制执行不变量。
在这个场景中,一个关键约束是单个房间每天允许的最大课程数,它由用户的订阅计划决定(例如免费等级每天最多四个课程)。

为了有效地强制执行这个不变量,`Room` 必须知道自己具体的每日上限。
在领域建模中有几种方式可以处理这一点,但所选择的方法是让 `Room` 聚合存储允许的最大课程数。
这些信息通过集成事件在各个限界上下文之间同步:

1.  **Room Creation(创建房间)**:当房间最初被创建时,一个集成事件会把该房间的每日课程上限告知 Session Reservation 上下文。
2.  **Subscription Upgrades(订阅升级)**:如果管理员升级了订阅,Subscription 上下文会发出一个集成事件。Session Reservation 上下文监听这个事件,并更新与该管理员关联的所有房间的每日上限。

```text
Room Creation         --(integration event)-->  Session Reservation
                                                设置该 Room 的每日上限

Subscription Upgrade  --(integration event)-->  Session Reservation
                                                更新该管理员所有 Room
                                                的每日上限
```

#### 领域模型的职责

选择由哪个领域模型强制执行某个不变量,会显著影响系统的架构。
如果由 Subscription 领域模型负责每日课程上限,它就需要维护关于每个房间中安排的每个课程的详细信息。
通过把这段逻辑放在 `Room` 模型中,`Room` 始终是其自身日程的唯一事实来源。
这种设计确保创建课程时不需要通知任何其他限界上下文,从而保持清晰的边界,并减少上下文之间不必要的耦合。

## 10. Creating a Participant Profile

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/creating-a-participant-profile-54855855/) · 1:21

### 总结

本课解释创建参与者档案时 User Management 与 Session Reservation 这两个限界上下文之间的集成。
它详细说明了用户作为参与者的角色如何跨上下文边界来表示,并建立起一种上游/下游关系,其中 Session Reservation 上下文使用防腐层把参与者档案转换为它自己的领域模型,这个模型包含日程和课程管理。

### 核心概念

- **Dual Representation(双重表示)**:参与者同时存在于 User Management 和 Session Reservation 两个上下文中,具有不同的职责和数据结构。
- **Upstream/Downstream Relationship(上游/下游关系)**:User Management 上下文充当上游提供方,而 Session Reservation 上下文是下游消费方。
- **Anti-Corruption Layer (ACL)(防腐层)**:在 Session Reservation 上下文中实现一个 ACL,把它的领域模型与上游的用户档案结构隔离开。
- **Context-Specific Models(特定于上下文的模型)**:在 User Management 中,参与者是一个简单的档案或角色;在 Session Reservation 中,它是一个管理日程和预约的复杂实体。

### 课程笔记

要创建参与者档案,系统必须在 User Management 与 Session Reservation 这两个限界上下文之间进行协调。
这个过程从更新 User Management 上下文中的 User 领域模型开始,以表明某个特定用户现在拥有一个参与者档案。

参与者在两个上下文中都有表示,但领域模型会根据各自本地的需求而有显著差异:

1.  **User Management Context**:参与者被视为可以关联到用户账户的可用档案或角色之一。它是一种相对简单的表示,关注身份和档案的归属。
2.  **Session Reservation Context**:参与者是一个核心领域实体。在这个上下文中,参与者负责管理日程、跟踪即将到来的课程,并执行在这些课程中预约名额所需的逻辑。

这种交互建立了一种上下文映射,其中 User Management 上下文是上游,Session Reservation 上下文是下游。
由于 Session Reservation 上下文中对参与者的定义比 User Management 中定义的简单角色丰富得多、专门得多,因此需要一个防腐层(ACL)。

这个 ACL 位于 Session Reservation 上下文中。
它用于把上游的用户档案数据转换为下游专门的参与者模型。
通过使用 ACL,Session Reservation 上下文确保其内部逻辑(例如日程管理和预约规则)与 User Management 上下文的结构保持解耦。

## 11. Reserving a Spot in a Session

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/reserving-a-spot-in-a-session-54855856/) · 1:04

### 总结

本课详细说明在课程中预约名额的实现逻辑,重点关注 Session Reservation 上下文。
它确立 Session 作为容量管理的权威事实来源(同时跟踪最大参与人数和当前参与人数),并厘清预约逻辑与计费之间的边界,而计费仍然在当前 MVP 范围之外。

### 核心概念

- **Session Reservation Context**:负责管理参与者预订的主要限界上下文。
- **Point of Truth(事实来源)**:Session 实体,它维护校验预约请求所需的状态。
- **Capacity Management(容量管理)**:将当前参与人数与最大容量进行比较以确定是否还有空位的逻辑。
- **MVP Scope Boundaries(MVP 范围边界)**:由于采用人工、线下当面的支付流程,Billing 上下文被排除在数字化预约流程之外。

### 课程笔记

#### 上下文边界与 MVP 范围

在当前的系统设计中,Billing 上下文被排除在自动化预约流程之外。
虽然计费是整体业务中至关重要的一部分,但最小可行产品(MVP)假定参与者在健身房前台当面为课程付费。
因此,虽然预约与计费之间的关系在更广泛的领域中是存在的,但它不会在这个特定流程的上下文映射图中体现。

#### 以 Session 作为事实来源

要在课程中预约名额,系统只与 Session Reservation 上下文交互。
**Session** 聚合是判断参与者能否成功预约名额的权威事实来源。
为此,Session 维护两项关键状态:
1.  **Maximum number of participants(最大参与人数)**:该课程容量的上限。
2.  **Current number of participants(当前参与人数)**:已确认参加该课程的人数。

当有预约请求时,Session 会评估这些值,以确定是否还有剩余容量。

#### 内部预订

在 Session Reservation 上下文中,可能会创建一个 **Reservation** 实体来跟踪这次事务。
这个实体是该上下文内部的;没有任何外部上下文需要看到预约的细节,因为在流程的这个阶段,主要结果(Session 更新后的状态)是对更大范围系统唯一相关的信息。

## 12. Context Mapping In Action - Conclusion

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/context-mapping-in-action-conclusion-54855857/) · 1:11

### 总结

本课通过讨论限界上下文之间已定义关系在实际实现上的影响,为上下文映射练习作结。
它着重说明系统之间(例如 User Management、Gym Management 和 Session Reservation 之间)的信息流如何决定架构需求,包括消息代理(message broker)的集成以及上游与下游组件之间通信的方向。

### 核心概念

- **Stakeholder Alignment(利益相关者对齐)**:上下文映射图作为一种可视化工具,让技术团队和非技术团队在整体系统流程和过程上达成一致。
- **Implementation Implications(实现上的影响)**:已定义的映射图决定了上下文之间如何交互,具体来说是识别出哪些上下文必须观察其他上下文中的变更。
- **Asynchronous Communication(异步通信)**:利用消息代理促进解耦的限界上下文之间的通信。
- **Communication Directionality(通信方向)**:根据上下文映射图识别哪些上下文充当生产者(发送消息),哪些充当消费者(监听变更)。

### 课程笔记

上下文映射是领域驱动设计战略阶段中的关键一步,它直接为系统架构和实现策略提供依据。
通过可视化限界上下文之间的关系,团队可以识别数据如何流动,以及哪些系统必须对特定的领域事件做出反应。

在这个示例场景中,**User Management** 上下文是用户相关数据的主要来源。
发生在这个上下文中的变更必须被 **Gym Management** 和 **Session Reservation** 两个上下文观察到,以确保系统范围的一致性。
这些观察的具体实现取决于所选的技术栈和系统架构。

如果架构使用消息代理进行上下文之间的通信,实现细节如下:
- **User Management**:充当生产者,与消息代理交互来发布事件。在这个特定流程中,它只发送消息,不需要监听来自其他上下文的传入消息。
- **Gym Management**:充当消费者,在消息代理上监听由 User Management 上下文发布的特定变更事件。

```text
User Management            message broker            Gym Management
  (producer)    --publish-->  [ events ]  --listen-->   (consumer)
```

这个战略映射阶段为过渡到 DDD 的战术阶段提供了必要的洞察,战术阶段关注聚合设计和内部领域逻辑。

## 13. Strategic Goals Alignment

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/strategic-goals-alignment-54855858/) · 2:36

### 总结

本课强调战略阶段至关重要的最后一步:确保所提出的系统设计与总体业务目标和优先级保持一致。
由于软件开发是一项资源密集型的投资,设计必须反复对照业务目标和预算进行验证,以确保它交付预期的价值。
这个对齐过程常常需要在利益相关者之间做出艰难的权衡和妥协,从而为后续的技术实现打下坚实的基础。

### 核心概念

* **Strategic Alignment(战略对齐)**:确保系统架构支撑业务目标。
* **Iterative Validation(迭代验证)**:持续地将设计决策与业务目标进行权衡。
* **Stakeholder Compromise(利益相关者妥协)**:在相互竞争的需求和资源约束之间取得平衡。
* **Foundational Design(基础设计)**:在技术执行之前建立对系统的高层视图。

### 课程笔记

从战略阶段过渡到技术阶段,需要一次正式的停顿,以验证所提出的解决方案与战略业务目标保持一致。
这种对齐不是一次性的事件,而是一个迭代的过程。
随着系统设计的演进,它必须持续地对照业务目标和预算约束进行衡量。

一个在技术上优雅的系统设计,只有在实现了其背后的业务目的时才算成功。
为了做到这一点,架构师和利益相关者应当从设计的细枝末节中抽身出来,结合业务路线图从整体上审视系统。
这种高层视角常常会揭示出:那些孤立来看似乎至关重要的功能,在以主要业务驱动因素来衡量时并没有那么重要。

鉴于软件开发的高昂成本和时间要求,对齐过程涉及在各种妥协之间周旋。
并非每个利益相关者的需求都能得到同等的优先级。
这一阶段的目标是就一个起点达成共识,这个起点能为软件解决方案提供正确的基础。
这一阶段通过做出界定系统发展轨迹所必需的艰难决定,为战略映射画上句号。

## 14. Section Recap

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/section-recap-54855859/) · 0:37

### 总结

本课通过总结上下文映射的原则,为领域驱动设计的战略阶段作结。
它强调可视化限界上下文之间关系的重要性、通过特定的映射模式理解团队动态的重要性,以及在过渡到战术阶段之前确保技术架构与总体业务目标保持一致的重要性。

### 核心概念

- **Context Mapping(上下文映射)**:一种用于可视化不同限界上下文之间关系和边界的工具。
- **Team Relationships(团队关系)**:理解组织结构和团队互动如何影响系统设计。
- **Context Map Patterns(上下文映射模式)**:定义两个上下文如何交互的特定原型关系(例如 Partnership、Shared Kernel、Customer-Supplier)。
- **Strategic Alignment(战略对齐)**:验证系统设计支撑业务目标的必要性。

### 课程笔记

上下文映射在领域驱动设计的战略阶段中充当一座关键的桥梁。
它提供了一种可视化表示,展示系统中各个限界上下文彼此之间如何关联。
上下文映射图中的每个模式都定义了两个限界上下文之间关系的具体性质,同时兼顾技术集成和组织层面的团队动态。

这个过程包括分析系统中的具体场景,以确定哪些模式(例如 Upstream/Downstream、Anticorruption Layers 或 Open Host Services)最适合给定的约束。
除了技术实现之外,上下文映射还需要一次高层次的审视,以确保最终的系统架构与业务的战略目标保持一致。
这种对齐确保所划定的边界真正解决了预期要解决的业务问题。

随着战略边界和关系的确立,关注点从高层架构转向战术阶段,在战术阶段中会定义每个限界上下文的内部实现细节。
