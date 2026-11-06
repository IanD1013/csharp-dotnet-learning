# Big Picture Event Storming

> 课程:[Deep Dive: Domain-Driven Design](https://dometrain.com/course/deep-dive-domain-driven-design-ddd/) · 第 3 章
> 共 13 课 · 约 1:14:00
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [What is Event Storming?](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/what-is-event-storming-54855676/) | 3:07 | [↓](#1-what-is-event-storming) |
| 2 | [Event Storming Legend](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/event-storming-legend-54855677/) | 7:00 | [↓](#2-event-storming-legend) |
| 3 | [Types of Event Storming](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/types-of-event-storming-54855679/) | 2:53 | [↓](#3-types-of-event-storming) |
| 4 | [Big Picture Event Storming](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/big-picture-event-storming-54855680/) | 5:29 | [↓](#4-big-picture-event-storming) |
| 5 | [Gym Session Scheduling System Domain Overview](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/gym-session-scheduling-system-domain-overview-54855681/) | 5:22 | [↓](#5-gym-session-scheduling-system-domain-overview) |
| 6 | [Big Picture Event Storming In Action - Setup](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/big-picture-event-storming-in-action-setup-54855682/) | 5:18 | [↓](#6-big-picture-event-storming-in-action---setup) |
| 7 | [Round 1 - Chaotic Exploration](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/round-1-chaotic-exploration-54855683/) | 13:24 | [↓](#7-round-1---chaotic-exploration) |
| 8 | [Round 2 - Enforce the Timeline](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/round-2-enforce-the-timeline-54855685/) | 15:59 | [↓](#8-round-2---enforce-the-timeline) |
| 9 | [Round 3 - People & Systems](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/round-3-people-systems-54855686/) | 5:10 | [↓](#9-round-3---people--systems) |
| 10 | [Round 4 - Walkthrough & Reverse Narrative](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/round-4-walkthrough-reverse-narrative-54855687/) | 5:22 | [↓](#10-round-4---walkthrough--reverse-narrative) |
| 11 | [Round 5 - Arrow Voting](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/round-5-arrow-voting-54855688/) | 1:18 | [↓](#11-round-5---arrow-voting) |
| 12 | [Big Picture Event Storming Goals](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/big-picture-event-storming-goals-54855689/) | 1:51 | [↓](#12-big-picture-event-storming-goals) |
| 13 | [Section Recap](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/section-recap-54855690/) | 1:47 | [↓](#13-section-recap) |

## 1. What is Event Storming?

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/what-is-event-storming-54855676/) · 3:07

### 总结

Event Storming 是一种协作式、基于工作坊的技术,它使用一套标准化的彩色便利贴来可视化并梳理复杂的领域流程。
它是让不同利益相关者在共同愿景和共同语言上达成一致的有力工具,通过把所有相关方召集到同一个房间里集思广益、化解领域中的复杂问题,克服了异步沟通的局限。

### 核心概念

- **Workshop-based domain mapping(基于工作坊的领域梳理)**:一种线下或线上的聚会,利益相关者在其中把流程可视化。
- **Stakeholder Alignment(利益相关者对齐)**:把各方召集到一起,建立共同的语言和愿景。
- **Synchronous Collaboration(同步协作)**:通过实时头脑风暴来化解分歧并继续推进。
- **Standardized Notation(标准化记法)**:一套由九种便利贴颜色组成的体系,用于表示不同的领域元素。
- **Process Visualization(流程可视化)**:产出一个无需大量讲解就能看懂的"全局"成果物。

### 课程笔记

Event Storming 是一种用便利贴梳理领域中特定部分的技术。
它是一种基于工作坊的方法,参与该领域的人聚在一起把流程可视化。
虽然 Event Storming 有多种类型(每种类型涉及不同的人员和特定的便利贴类型),但其总体目标始终是把领域内部正在发生的事情可视化。

在大型组织中或在复杂产品上工作时,这种方法尤其有价值。
在这类环境中,许多利益相关者和相关方必须协作,才能拼出领域的完整图景。
传统的异步方式,例如电子邮件、电话或共享的一页纸文档,往往无法让所有人达成共识。
Event Storming 通过把所有相关人员召集到一个房间里来解决这个问题:大家一起头脑风暴,找出达成一致或存在分歧的地方,并在同一个目标和同一种语言上对齐。

这套方法使用九种不同颜色的便利贴。
虽然颜色本身是任意的,但遵循 Event Storming 文献中确立的标准是有益的。
采用一致的颜色规范,能让任何熟悉这一实践的人在看到流程建模成果物时立刻理解其核心思想和流程,而无需有人手动解释每种颜色代表什么。

## 2. Event Storming Legend

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/event-storming-legend-54855677/) · 7:00

### 总结

Event Storming 是一种协作建模技术,它使用一套标准化的、按颜色编码的便利贴来梳理业务流程和领域逻辑。
本课定义了 Event Storming 图例的核心元素,包括 Actor、Command、Domain Event、System、Aggregate、Read Model、Policy、Hotspot 和 Opportunity,并演示这些组件如何相互作用来描述系统的行为和业务需求。

### 核心概念

*   **Actor(参与者)**:发起某个动作的用户或角色(例如 Admin、Participant、Trainer)。
*   **Command(命令)**:由参与者发起的某个具体动作、意图或决定,用现在时表达。
*   **Domain Event(领域事件)**:领域内发生的、具有业务意义的事情,用过去时表达。
*   **Read Model(读模型)**:用户做出决定或系统执行命令所需的数据。
*   **System(系统)**:执行命令的逻辑组件、微服务或外部服务。
*   **Aggregate(聚合)**:作为数据变更的单一单元来对待的一组领域对象,用于确保状态一致性。
*   **Policy(策略)**:一种响应式规则,通常描述为"每当 X 发生时,就做 Y"。
*   **Hotspot(热点)**:一张红色便利贴,表示存在分歧、不一致,或需要今后解决的悬而未决的问题。
*   **Opportunity(机会)**:一张绿色便利贴,代表潜在的功能或业务改进,通常从某个热点中产生。

### 课程笔记

#### 参与者 (Actors) 与读模型 (Read Models)

建模过程从 **Actor** 开始,它代表用户与系统交互时所扮演的特定角色,例如 Admin、Participant 或 Trainer。
如果某个事件的发起者不明确,通常可以先找出是谁或什么触发了这一变化。
在某些情况下,事件并不是由人类参与者触发的,而是基于时间的(例如一个定时任务)。

在参与者执行某个动作之前,他们会与一个 **Read Model** 交互。
它代表执行某项特定任务所需的信息。
例如,为了执行 "Create Gym" 命令,读模型可能会提供健身房名称和房间数量等字段。
它还包括用户在屏幕上看到的、为其下一个决定提供依据的数据。

#### 命令 (Commands) 与系统 (Systems)

**Command** 是由参与者发起的一个动作。
它代表一个意图或一个决定,并且始终用现在时来定义(例如 "Create Gym" 或 "Update Gym")。
命令在某个 **System** 上被调用,这个系统可以是某个特定的微服务、应用程序的某个逻辑部分,甚至是外部的第三方服务(例如电子邮件服务商或另一个部门的系统)。

#### 领域事件 (Domain Events) 与聚合 (Aggregates)

**Domain Event** 或许是 Event Storming 中最关键的元素。
它代表系统中已经发生的、领域专家感兴趣的一个事实。
事件始终用过去时书写(例如 "Admin Profile Created")。

状态的变化由 **Aggregates**(用浅黄色表示)来管理。
一个聚合由一个或多个领域对象组成,这些对象必须作为单一单元保持一致。
当一个命令被处理时,它通常会导致某个聚合内部的状态发生变化,进而触发一个领域事件。

#### 策略 (Policies)、热点 (Hotspots) 与机会 (Opportunities)

**Policy**(或称 "Reactor")定义了某个事件之后的自动或手动逻辑。
它遵循这样的模式:"每当 [Event] 发生时,就 [Action]。"
例如,"每当创建了一个课程(session)时,就通知教练。"
策略可以通过软件实现(例如一个自动化的邮件系统),也可以通过手动流程实现(例如由一名员工根据数据库中的新记录给教练打电话)。

在 storming 过程中,摩擦点用 **Hotspots**(红色便利贴)来标识。
它们标记出领域专家意见不一的地方、存在已知不一致的地方,或者存在一个不应打断当前讨论的悬而未决问题的地方。
与之相对,**Opportunity**(绿色便利贴)标记出一个潜在的改进领域或新功能,例如发现某个有争议的流程可以被做成一项付费高级功能。

#### 示例工作流

为了说明这些组件的流转:
1.  **Actor**:Admin
2.  **Command**:Create Session
3.  **System**:Gym System
4.  **Domain Event**:Session Created
5.  **Policy**:Session Created Policy(每当创建了一个课程时……)
6.  **Action**:Notify Trainer

```text
[Actor]      [Command]         [System]      [Domain Event]
 Admin  -->  Create Session -->  Gym System --> Session Created
                                                     |
                                                     v
                                 [Policy] Session Created Policy
                                                     |
                                                     v
                                          [Action] Notify Trainer
```

这个序列展示了参与者的意图如何在系统中流转、产生一个被记录下来的事实,并触发后续的业务逻辑。

## 3. Types of Event Storming

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/types-of-event-storming-54855679/) · 2:53

### 总结

根据范围和粒度,Event Storming 分为三种不同的类型:Big Picture、Process Modeling 和 Software Design。
Big Picture 会议使用事件、参与者和系统,让利益相关者在宽泛的系统全貌上达成一致。
Process Modeling 通过引入命令、策略和读模型,深入到具体的工作流中。
最后,Software Design 识别出聚合,以推动领域的技术实现。

### 核心概念

* **Big Picture Event Storming**:一种高层次的对齐会议,使用五种主要的便利贴类型,关注整个系统全貌。
* **Process Modeling**:一种更细粒度的会议,聚焦于单个流程,引入数据流和业务逻辑的流转。
* **Software Design**:Event Storming 中技术性最强的层级,用于识别聚合并推动软件实现。
* **Sticky Note Evolution(便利贴的演进)**:每种类型的 Event Storming 都会引入新的记法(命令、策略、读模型和聚合),以提高细节程度。

### 课程笔记

#### Big Picture Event Storming

Big Picture Event Storming 是这套方法中范围最宽的形式。
它的目的是让所有利益相关者在系统的高层行为上达成一致。
在这一阶段,关注点不是技术实现或具体的命令,而是整体流程以及潜在问题的识别。

这类会议使用五种特定的便利贴:
1. **Domain Events**:系统中发生的各种事情。
2. **Systems**:这些行为发生所在的外部或内部系统。
3. **Actors**:系统中涉及的各种个人或角色。
4. **Hotspots**:存在分歧或技术债务、需要利益相关者达成共识的地方。
5. **Opportunities**:潜在的改进领域或新功能。

在 Big Picture 会议中,范围很大,覆盖整个领域,而不是像 "create profile" 这样的具体动作。

#### Process Modeling Event Storming

Process Modeling 把范围从整个系统缩小到单个特定的流程。
这一阶段更加结构化,并遵循关于不同便利贴如何相互作用的特定规则。
除了 Big Picture 会议中使用的五种元素之外,Process Modeling 还引入了另外三种:

* **Commands**:由参与者或系统采取的具体动作。
* **Policies**:在某个事件被触发后引发特定动作的业务规则。
* **Read Models**:参与者做出决定和执行命令所需的数据模型。

Process Modeling 充当高层业务视角与技术设计之间的桥梁,展示数据如何流经某个特定的工作流。

#### Software Design Event Storming

Software Design 是最后一种、也是技术性最强的 Event Storming 类型。
它直接建立在 Process Modeling 阶段产出的成果物之上。
这一阶段新增的主要元素是 **Aggregate**。

在 Software Design 中,目标是把各种命令和事件围绕特定的聚合进行分组。
这个过程识别出领域对象的边界,并推动软件底层架构的实际设计。
通过识别这些聚合,开发者可以按照领域驱动设计的原则,把 Event Storming 会议的成果转化为具体的实现。

```text
Big Picture        Process Modeling      Software Design
-----------        ----------------      ---------------
Domain Events      (all of Big Picture)  (all of Process Modeling)
Systems            + Commands            + Aggregates
Actors             + Policies
Hotspots           + Read Models
Opportunities
```

## 4. Big Picture Event Storming

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/big-picture-event-storming-54855680/) · 5:29

### 总结

Big Picture Event Storming 是一种高层次、基于工作坊的技术,用于探索问题空间并获得对业务领域的整体理解。
通过把利益相关者、业务专家和技术团队召集到一起,这一过程借助按时间顺序梳理领域事件来促成共同理解。
这种协作方式让团队能够在进入实现或详细设计之前识别系统边界、化解冲突、突显瓶颈,并在优先级上达成一致。

### 核心概念

* **Problem Space Exploration(问题空间探索)**:专注于理解业务挑战和需求,而不是立即寻求技术解决方案。
* **Facilitation(引导)**:引导者的角色,负责管理工作坊的节奏、设备以及冲突的化解。
* **Stakeholder Alignment(利益相关者对齐)**:确保每一个对项目有发言权的人(从 CEO 到最终用户)共享同一个心智模型。
* **Domain Events(领域事件)**:用橙色便利贴表示,它们是 Event Storming 墙上的主要构建块。
* **Chronological Modeling(按时间顺序建模)**:把事件按时间线组织起来,以可视化系统的流程。
* **Boundary Identification(边界识别)**:使用泳道来分隔并行的流程,并界定不同的职责范围。
* **Hospitals**:一种标记分歧或不确定之处、留待之后解决的技术。

### 课程笔记

#### Big Picture Event Storming 的目的

Big Picture Event Storming 的特点是范围宽。
与更细粒度的 Event Storming 形式不同,这里的目标是全面地探索问题空间。
这包括从多样化的利益相关者群体(包括业务专家、最终用户和项目经理)那里收集信息,以获得对组织所面临挑战的整体认识。

#### 工作坊的准备与引导

这个过程基于工作坊,需要一名专门的引导者。
引导者负责准备线下或线上的场地,提供马克笔和橙色便利贴等设备,并引导参与者完成特定的几轮环节。
除了后勤工作之外,引导者还要管理现场的气氛、设定预期,并调解讨论中出现的冲突。

成功的会议需要"对的人"在场。
这意味着任何对结果有发言权、或对特定部门有独到见解的人都必须参与。
这种跨职能的出席确保最终得到的模型不是各自为政的,并确保各方在业务逻辑上达成一致。

#### 线下与线上的要求

在线下场景中,一面大墙是必不可少的。
一次 Big Picture 会议很容易产生数百张代表领域事件的橙色便利贴。
场地必须足够大,以便按时间顺序建模时不受物理边界的限制。
在线上环境中,数字白板工具起到同样的作用。
环境必须鼓励开放、坦诚的沟通,让各方都能放心地分享自己的观点,从而建立共同理解。

#### 战略成果

虽然直接产出是一面贴满便利贴的墙,但这次会议的战略目标包括:

* **Identifying Boundaries(识别边界)**:通过把事件按时间顺序摆放,团队可以识别出流程在哪里分叉或并行运行。泳道常被用来可视化系统不同部分之间的这些边界。
* **Defining Responsibilities(界定职责)**:模型有助于厘清哪些角色或部门对特定的结果负责。
* **Managing Conflicts(管理冲突)**:当参与者对某个流程意见不一时,会使用一个 "hospital"(墙上的一个特定区域)来暂存这个冲突,让会议可以继续进行而不失去势头。
* **Identifying Bottlenecks(识别瓶颈)**:技术专家和业务专家可以突显出流程中占用了不成比例的时间或资源的步骤。
* **Prioritization(确定优先级)**:这次会议让利益相关者能够直观地识别出系统中哪些部分对业务目标最为关键。

这个工具用途广泛;虽然它可以用于涉及 C 级高管的大规模全公司性举措,但对于一个小型开发团队来说,在实现新功能之前进行一次同样有效,可以确保每个人都理解范围和需求。

## 5. Gym Session Scheduling System Domain Overview

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/gym-session-scheduling-system-domain-overview-54855681/) · 5:22

### 总结

本课对健身房课程排期与预订系统进行领域概览,该系统被用作领域驱动设计的主要案例。
它从健身房管理员和参与者的视角确立了功能需求,涵盖健身房注册、资源管理以及课程预约的生命周期。
这一高层背景是后续 Big Picture Event Storming 会议的基础,让我们从一个概念性的创业想法走向一个结构化的领域模型。

### 核心概念

- **Multi-tenant Gym Management(多租户健身房管理)**:支持管理多个场馆或连锁店的健身房所有者。
- **Resource Allocation(资源分配)**:管理特定健身房场馆内的实体房间和教练。
- **Session Scheduling(课程排期)**:在特定的时间段和房间内创建和管理各种课程类型(例如 Yoga、Zumba、Pilates)的过程。
- **Participant Reservation Lifecycle(参与者预约生命周期)**:用户浏览课程、查看空余名额、预约名额以及取消预订的工作流。
- **Capacity Constraints(容量约束)**:跟踪剩余名额并在名额有限时通知用户的逻辑。

### 课程笔记

该系统旨在为健身房课程的排期和预订提供便利。
它服务于三种主要角色:Administrators(健身房所有者)、Trainers 和 Participants。

#### 管理员视角

管理员使用该系统来管理一个或多个健身房场馆的运营事务。
核心的管理工作流包括:
- **Gym Registration(健身房注册)**:把新的健身房场馆接入系统。一个所有者可能管理分布在不同地点的连锁健身房。
- **Resource Management(资源管理)**:通过添加房间和接纳教练来定义每个健身房的基础设施。
- **Session Management(课程管理)**:在房间内安排具体的课程。每节课程由其类型(例如功能性训练、踢拳)、指派的教练、房间位置以及课程的强度级别来定义。
- **Oversight(监督)**:利用日历界面监控多个健身房场馆中已安排的课程,并在必要时进行调整。

#### 参与者视角

参与者与系统交互来管理自己的健身日程。
他们的工作流包括:
- **Session Discovery(课程发现)**:通过仪表盘或日历视图浏览即将开始的课程。用户可以按类型或日期筛选课程。
- **Session Details(课程详情)**:查看特定课程的元数据,包括时长、教练的个人资料、具体房间以及强度级别。
- **Reservation Management(预约管理)**:在某节课程中预约一个名额,或取消已有的预约。系统会跟踪容量,在名额有限时(例如剩余名额少于五个)显示提示信息。

#### 过渡到 Event Storming

虽然可以通过高层的线框图和用户界面来可视化该系统,但 Big Picture Event Storming 会议的目标是超越这些视觉原型。
在这一阶段,该领域还缺少正式的通用语言(ubiquitous language)和明确定义的不变量(invariants)。
后续的建模过程将在任何实现开始之前探索业务逻辑、识别领域事件,并确定系统如何为其用户提供价值。

## 6. Big Picture Event Storming In Action - Setup

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/big-picture-event-storming-in-action-setup-54855682/) · 5:18

### 总结

本课介绍在健身房课程排期领域中进行 Big Picture Event Storming 会议的实际准备工作。
它概述了为消除盲点所需的多样化利益相关者群体、高强度协作和领域可视化这两个主要目标,以及用于引导会议的线上环境,包括时间维度的布局和专用便利贴的使用。

### 核心概念

- **Stakeholder Diversity(利益相关者的多样性)**:纳入从 PM 到法律顾问的各种角色,以确保对领域有全面的认识。
- **Collaboration on Steroids(加强版协作)**:集中几个小时进行对齐,以形成一张共享的心智地图。
- **Visualizing the Problem Space(可视化问题空间)**:梳理整个业务领域,以识别不一致和缺口。
- **Temporal Modeling(时间维度建模)**:在建模面上使用从左到右的时间线来表示领域事件的先后顺序。
- **Event Storming Tooling(Event Storming 工具)**:使用带有术语表、便利贴和灵感板的线上白板。

### 课程笔记

本课的领域焦点是一个健身房课程排期系统,参与者在其中预约名额,并且适用各种业务规则,例如取消时间窗口和教练替换规程。
为了有效地探索这个问题空间,我们发起了一次 Big Picture Event Storming 会议。

#### 参与者与视角

为了避免在业务领域中出现盲点,我们召集了一个多样化的利益相关者群体来提供他们各自独特的视角:
- **Product Managers and Business Strategists(产品经理和业务战略师)**:提供高层的业务方向。
- **Domain Experts(领域专家)**:了解日常细节的健身房运营专家。
- **Technical Staff(技术人员)**:开发者和架构师,用于了解实现上的约束和可能性。
- **Designers, Sales, and Marketing(设计、销售和市场)**:提供用户体验和面向市场的视角。
- **End Users(最终用户)**:真实的健身房所有者、教练和参与者。
- **Compliance and Legal Advisors(合规和法律顾问)**:处理监管方面的要求。
- **Customer Support Experts(客户支持专家)**:突显常见的痛点和边界情况。

#### 目标

这次会议追求的是"加强版协作",让利益相关者在集中的几个小时里对齐他们对领域的理解。
主要目标是把整个业务领域可视化,让团队能够退后一步,识别出当前理解中不完整或自相矛盾的地方。
这个过程会形成一张心智地图,它与领域驱动设计的战略设计阶段相契合,首先专注于探索问题空间。

#### 线上会议的准备

建模环境由几个旨在为工作坊提供便利的关键区域组成:
- **Modeling Surface(建模面)**:梳理事件的主要区域。它遵循从左到右的时间流向;放在左边的事件在时间上先于放在右边的事件发生。
- **Sticky Notes and Glossary(便利贴和术语表)**:使用一套标准化的便利贴(代表事件、命令等)。会提供一份术语表,以确保每位参与者都理解每种便利贴类型的具体含义及其背后的"故事"。
- **Inspiration Board(灵感板)**:存放参考资料的空间,例如 UI 原型或现有文档,用于引导 storming 过程并为参与者提供背景。

## 7. Round 1 - Chaotic Exploration

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/round-1-chaotic-exploration-54855683/) · 13:24

### 总结

Big Picture Event Storming 的第 1 轮是一个混乱探索阶段,来自整个业务的利益相关者(包括产品、销售、法务和工程)在这一阶段识别并记录领域事件。
主要目标是从多个视角梳理问题空间,而不必操心严格的结构或术语的一致性。
这一阶段常常会暴露出命名冲突和业务逻辑上的缺口,它们会被标记为"热点(hotspots)",留待之后解决。

### 核心概念

- **Chaotic Exploration(混乱探索)**:一个无结构的头脑风暴阶段,所有参与者同时贡献领域事件。
- **Domain Events(领域事件)**:业务流程中发生的离散事件,用过去时表达(例如 `Session Spot Reserved`)。
- **Stakeholder Perspectives(利益相关者视角)**:纳入多样化的角色(法务、销售、技术、UX),以确保覆盖整个领域。
- **Hotspots(热点)**:用于标识分歧、命名冲突或需要进一步讨论的复杂逻辑的视觉标记。
- **Ubiquitous Language Discovery(通用语言的发现)**:初步识别出相互冲突的术语(例如 'Member' 与 'Participant'),这些术语最终会被统一。

### 课程笔记

#### 第 1 轮的目标

Event Storming 第一轮的主要目标是探索问题空间。
规则有意定得很宽松,以鼓励产出大量的领域事件。
来自各个部门的参与者被邀请列出他们能想到的每一个领域事件,并把它们放到共享的建模面上。

#### 利益相关者的视角与领域事件

组织中的不同角色关注系统生命周期的不同方面:

- **Product Management(产品管理)**:关注核心的用户交互和行为。关键事件包括 `Session Spot Reserved`、`Session Started`、`Session Ended`、`Member Arrived at Gym` 和 `Member Joined Session`。
- **Customer Support(客户支持)**:关注课后体验和问题解决。事件包括 `Session Feedback Provided`、`Money Back Requested` 和 `Money Back Ticket Opened`。
- **Sales and Marketing(销售和市场)**:关注变现和增长。事件包括 `Customer Charged`、`Service Announced`、`Service Promoted` 和 `Service Launched`。
- **Legal and Compliance(法务和合规)**:关注安全和标准。事件包括 `Trainer Reported`、`Participant Reported`、`Trainer Suspended`、`Gym Inspected`、`Gym Inspection Passed` 和 `Health Waiver Signed`。
- **Engineering and Architecture(工程和架构)**:关注技术路线图和就绪程度。事件包括 `MVP Defined`、`Figma Mocks Ready`、`Front End Development Started`、`MVP Ready`、`Service Ready for Testing` 和 `Service Production Ready`。
- **Gym Owners and End Users(健身房所有者和最终用户)**:关注运营层面的设置。事件包括 `Gym Registered`、`Room Created in Gym`、`Session Created` 和 `Session Location Chosen`。

#### 识别逻辑与命名冲突

随着事件被添加到板上,冲突不可避免地会出现。
这些冲突通过特定的技术来处理:

##### Hotspots

当出现争论时(例如用户取消时是否应该总是能拿回退款),就在板上放一个"热点"。
这把该区域标记为需要做出策略决定,同时又不会让当前的头脑风暴流程停滞。
例如,可以在 `Reservation Canceled` 事件上放一个热点,用来讨论围绕退款的具体业务规则。

##### Naming Conflicts

第 1 轮常常会暴露出不同的利益相关者用不同的名字称呼同一个实体。
例如,参加课程的人可能被产品团队称为 "Member",却被法务团队称为 "Participant"。
类似地,管理健身房的人可能被称为 "Gym Owner"、"Owner" 或 "Admin"。
这些命名上的差异会被记录下来,作为在后续几轮中建立通用语言时需要解决的事项。

#### 管理容量与请求

探索过程还会揭示出复杂的工作流,例如预约管理。
像 `Reserve Spot Request Sent` 和 `Reservation Request Denied` 这样的事件突显出,在 `Session Spot Reserved` 事件能够发生之前,需要有容量检查逻辑。

到第 1 轮结束时,建模面上通常会有几十张甚至上百张便利贴。
虽然板面看起来可能很混乱,但它代表了一张反映领域广度的全面地图,其中包括重复、冲突和缺失的部分,为后续结构化的几轮提供了必要的原材料。

## 8. Round 2 - Enforce the Timeline

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/round-2-enforce-the-timeline-54855685/) · 15:59

### 总结

在 Big Picture Event Storming 的第二轮中,重点转向强制执行按时间顺序的时间线,并整理板面以提高清晰度。
这一阶段包括创建泳道来区分不同的用户角色或流程,识别存在命名不一致或业务冲突的"热点",以及发掘新的业务机会,例如变现策略。
本课强调对 Ubiquitous Language 和 Invariants 文档的迭代完善,确保技术实现细节被有意义的业务事件所取代,并确保核心业务规则得到清晰的定义。

### 核心概念

- **Timeline Enforcement(强制时间线)**:把事件从左到右按时间顺序组织起来,以确保流程合乎逻辑。
- **Swim Lanes(泳道)**:可视化的水平分隔线,用于按角色(例如 Participant、Admin、Trainer)或特定子流程对事件进行分类。
- **Hotspots(热点)**:红色便利贴,表示存在摩擦、分歧或命名歧义、需要进一步讨论的地方。
- **Opportunities(机会)**:识别潜在的业务价值领域,例如通过订阅进行变现,或自动通知之类的功能增强。
- **Ubiquitous Language(通用语言)**:在所有利益相关者之间定义并统一术语以避免混淆的过程。
- **Invariants(不变量)**:系统中必须始终成立的核心业务规则,例如课程容量或取消限制。

### 课程笔记

第 2 轮的主要目标是从一堆混乱的事件转变为一条结构化的时间线,在这条时间线上,左边的事件严格地先于右边的事件发生。
为了管理复杂性,板面被组织成若干泳道。
这些泳道为不同的角色或流程提供视觉上的分隔,例如 Participant、Admin 或 Trainer 各自不同的体验。

```text
             time (left before right) ------------------------->
            +-------------------------------------------------+
Participant |  ...events...                                   |
            +-------------------------------------------------+
Admin       |  ...events...                                   |
            +-------------------------------------------------+
Trainer     |  ...events...                                   |
            +-------------------------------------------------+
```

#### 完善通用语言 (Ubiquitous Language)

随着时间线被强制执行,命名上的不一致常常会浮现出来。
例如,'Gym Owner' 可能被重命名为 'Admin','Member' 被重命名为 'Participant',以确保整个领域中的一致性。
当团队就某个定义达成一致时,它会被记录到 Ubiquitous Language 草案中。
这份文档是领域发现阶段的中心参考资料。

```markdown
# Ubiquitous Language Draft

## User

**Definition**: An individual who interacts
with the system. A user can assume roles
such as Admin, Trainer, or Participant. A
user has a payment method that is used to
pay for the services.
**Usages**: The user created an Admin
profile.
**Usages**: The user's payment method
was charged.

## Participant
```

[▶ 观看](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/round-2-enforce-the-timeline-54855685/?t=115)

随着定义不断完善,这份文档会被扩充,纳入具体的角色及其在系统中的用法。
这确保了当开发者实现一个 'Participant profile created' 事件时,它与 'Admin profile created' 事件的结构保持一致。

```markdown
# Ubiquitous Language Draft
## Participant
**Definition**: A user who participates
in a session.
**Usages**: The participate arrived at
the gym.
**Usages**: One of the participates
reported another participate.

## Trainer

**Definition**: The instructor of a
session.
**Usages**: The Trainer was assigned to
the session.

## Admin

**Definition**: A user authorized to
create gyms, rooms and sessions.
**Usages**: The admin created a new gym.
**Usages**: The admin's payment method
was charged.
```

[▶ 观看](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/round-2-enforce-the-timeline-54855685/?t=850)

#### 识别热点与机会

热点用于标记摩擦之处,例如对预约取消应如何运作存在不确定性。
反过来,这些摩擦点往往也可以被视为机会。
例如,创建课程的流程提供了一个讨论变现的机会,比如按课程收费还是按健身房收费。
这就引出了在 Event Storming 会议中对订阅等级和市场调研的探讨。

#### 从实现事件转向业务事件

在这一轮中,至关重要的一点是移除那些从业务角度看并不改变系统状态的、特定于实现的细节。
像 'room capacity set' 或 'HTTP request sent' 这样的事件要么被移除,要么被重命名以体现业务意图。
例如,'reserve spot request sent' 被重命名为 'reserve spot requested',以聚焦于业务事件而不是技术机制。

#### 定义不变量 (Invariants)

不变量是必须始终得到维护的业务规则。
它们被记录在一份 Invariants 草案中。
一个常见的例子是预约取消时间限制,它防止用户在离课程开始太近的时候取消预约。

```markdown
# Invariants Draft

## Session Capacity Limits

**Description**: Each session has a
maximum capacity of participants. The
Session Capacity Limits are determined
based on the room size, the session
type, and trainer choice, ensuring safe
and manageable session sizes.

## Reservation Cancellation Time Limit

**Description**: A reservation cannot
be cancelled too close to the session.
**Time Limit**: 24 hours.
```

[▶ 观看](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/round-2-enforce-the-timeline-54855685/?t=925)

到第 2 轮结束时,板面应当呈现出一张清晰的系统心智地图,按功能区域(例如档案管理、课程流程以及反馈/举报)进行分类,命名冲突已得到解决,业务目标也已被识别出来。

## 9. Round 3 - People & Systems

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/round-3-people-systems-54855686/) · 5:10

### 总结

Big Picture Event Storming 的第 3 轮专注于识别发起事件的参与者以及这些事件发生所在的系统。
通过梳理管理员、参与者和教练等角色,以及外部或内部的软件系统,团队就领域的边界和职责建立起共同的愿景。
这一阶段有助于厘清业务规则、不变量,以及最终将构成系统架构的逻辑子组件。

### 核心概念

- **Actors(参与者)**:触发领域事件的个人、角色或人物画像(例如 Admin、Participant、Trainer)。
- **Systems(系统)**:事件发生所在的软件或组织实体;被定义为"任何我们可以归咎的东西"。
- **Temporal Triggers(时间触发器)**:用于里程碑或定时事件的基于时间的参与者(例如 "Service ready for testing by March")。
- **External vs. Internal Systems(外部系统与内部系统)**:区分团队自己维护的系统,与外部的或由其他部门管理的系统。
- **Logical Groupings(逻辑分组)**:识别事件如何聚集成 User Management、Session Reservations 或 Feedback and Reporting 之类的子系统。

### 课程笔记

在 Event Storming 的第三轮中,重点转向为每个领域事件定义相关的参与者和系统。
这一步对于理解业务流程中的"谁"和"在哪里"至关重要。

#### 定义参与者

参与者(actor)是发起一个最终导致某个领域事件的流程的个人、用户、角色或人物画像。
在一个典型的系统中,参与者可能包括:
- **Specific Roles(特定角色)**:Admin、Participant 或 Trainer。
- **Departments(部门)**:例如,市场部可能是负责 "Market research conducted" 事件的参与者。
- **Decision Makers(决策者)**:产品经理(PM)或项目负责人可能是 "Subscription cost determined" 等事件背后的参与者。
- **Time(时间)**:当一个事件由截止日期或特定日期触发时(例如 3 月 15 日的 "Service ready for testing"),参与者被视为一个时间里程碑。

#### 定义系统

系统被定义为任何可以为某个事件的发生而被"归咎"的实体。
这包括:
- **External Systems(外部系统)**:团队并不维护的第三方软件或服务。
- **Internal Systems(内部系统)**:由其他组织或部门管理的遗留软件或服务。
- **Logical Sub-components(逻辑子组件)**:正在设计的软件中的特定区域,例如 "Financial Analysis Software" 或 "Market Trend Identification" 工具。

在这一阶段,事件会按照它们所属的系统进行分组。
例如,与健身房课程类别和预约相关的事件被分配给 "Session Reservation System",而与用户档案相关的事件被分配给 "User Management" 或 "User Service"。

#### 识别边界与不变量

通过梳理参与者和系统,团队开始注意到领域不同部分中所使用的语言,以及维持有效状态所需的业务规则(不变量)。
这个过程揭示出系统不同部分之间的自然边界,例如:
- **Feedback and Reporting(反馈与举报)**:一个有别于目标定义的独立区域。
- **Participant Goals(参与者目标)**:与专注于用户获取的 "Leads" 区域相互独立。

这一轮会产出一个活的成果物,它为所有参与者提供共同的愿景,并作为未来实现的技术参考。
一旦参与者和系统就位,下一步就是验证时间线,以确保没有遗漏任何事件或转换。

## 10. Round 4 - Walkthrough & Reverse Narrative

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/round-4-walkthrough-reverse-narrative-54855687/) · 5:22

### 总结

Big Picture Event Storming 的第 4 轮专注于通过走查(walkthrough)和反向叙述(reverse narrative)来验证事件时间线。
通过把事件序列当作一个故事来讲述,或从某个特定结果往回追溯,参与者能够识别出缺失的领域事件、完善业务词汇,并发现隐藏的流程或技术上的不确定之处。
这项协作练习确保了对系统的整体认识,并突显出新功能、新指标或新收入来源的机会。

### 核心概念

* **Forward Walkthrough(正向走查)**:按时间顺序讲述领域事件的序列,以确保时间线合乎逻辑且完整。
* **Reverse Narrative(反向叙述)**:从某个特定的最终状态往回追溯事件,以识别出必要的前置条件和中间步骤。
* **Domain Event Discovery(领域事件发现)**:识别出在前几轮中被忽略的缺失事件,例如通知或签到流程。
* **Hotspots(热点)**:使用标记来标识不确定之处、技术问题或需要更深入讨论的点(例如 "How do we verify a user joined the room?")。
* **Business Vocabulary(业务词汇)**:完善事件名称,使用更偏向业务而非纯技术的语言。
* **Opportunity Identification(机会识别)**:在讲故事的过程中识别出潜在的功能,例如课程历史或专门的指标。

### 课程笔记

#### Walkthrough 与 Reverse Narrative 的流程

第 4 轮的目的是确保时间线讲得通,并确保事件之间没有遗漏关键流程。
这通过两种主要技术来实现:
1. **Forward Walkthrough(正向走查)**:由一名参与者走一遍事件序列,像讲故事一样描述它们。这有助于小组把用户旅程和系统的响应可视化。
2. **Reverse Narrative(反向叙述)**:小组挑选时间线末尾附近的一个事件,然后往回推,讨论必须发生了什么才能让这个特定事件成为可能。

这些技术促进共同理解,并提供对底层业务流程的整体认识。
如果发现了缺口,就立即更新时间线。

#### 完善健身课程流程

以一节健身房课程为例,走查可能从用户浏览课程开始。
这引出了一个问题:`Session Viewed` 是否应该是一个领域事件。
虽然它看起来可能无关紧要,但它提供了跟踪指标或为参与者构建"浏览历史"功能的机会。

随着叙述从预约推进到参加课程,缺失的事件常常会浮现出来。
例如,在预约名额和参加课程之间,需要有一个通知系统。
这可能会促使在板上专门为 "Notifications" 创建一个新的类别,其中包括 `Participant Notified of Upcoming Session` 之类的事件。

#### 识别热点与技术上的不确定性

当叙述推进到参与者加入课程这一步时,技术问题常常会出现。
如果加入课程的机制不明确(例如是用户扫描二维码,还是由教练手动为他们签到),就在板上放一个 **hotspot**。
这表明这一步的具体实现细节或业务规则需要进一步讨论,并防止小组在全局阶段陷入技术细节之中。

#### 反馈循环

课后的事件也会在这一轮中得到完善。
在 `Session Ended` 事件之后,系统可能需要触发一个 `Feedback Requested from Participant` 事件。
这又可能引出一个 `Feedback Sent to Trainer` 事件。
通过追踪这些交互,板面会变得更加详细,反映出领域的实际复杂度。

```text
Session Ended
     |
     v
Feedback Requested from Participant
     |
     v
Feedback Sent to Trainer
```

这一轮利用参与者的集体知识,不仅识别出数据的流转,还识别出业务机会和潜在的变现点,这些可以用特定的标记(例如一张"金钱"便利贴)标出来,以突显创造价值的区域。

## 11. Round 5 - Arrow Voting

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/round-5-arrow-voting-54855688/) · 1:18

### 总结

Big Picture Event Storming 的第 5 轮是一个可选阶段,专注于确定优先级。
通过给每位参与者两张箭头选票,让他们投在热点或机会上,小组可以直观地识别出领域中最关键的问题和重点关注的领域。
这项协作练习利用所有利益相关者的集体知识,就什么最需要立即关注达成共识。

### 核心概念

- 对热点和机会进行优先级排序。
- 箭头投票机制(每人两票)。
- 直观地识别出关键的领域问题。
- 利益相关者就项目重点达成共识。

### 课程笔记

第 5 轮是 Event Storming 过程中可选但有价值的一步,专门用于确立优先级。
一旦领域已被梳理、热点已被识别出来,团队就需要一种方法来确定哪些问题最重要。

在这一轮中,每位参与者会拿到两个箭头用作选票。
他们把这些箭头放在自己认为最重要的热点或机会上。
这个过程会在板面上形成一张直观的热力图。
例如,如果像二维码生成这样的某个特定问题获得了高度集中的箭头,就表明大多数利益相关者都把它视为系统中需要重点关注和解决的关键部分。

这项练习利用了整个小组(包括领域专家和技术利益相关者)都在场的条件,以确保优先级反映出对业务需求的整体认识。
到这一轮结束时,Event Storming 会议中产出的成果物会清楚地突显出领域中最重要的区域,为后续的设计和开发工作提供指引。

## 12. Big Picture Event Storming Goals

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/big-picture-event-storming-goals-54855689/) · 1:51

### 总结

Big Picture Event Storming 是一种协作工具,用于梳理整个领域,让利益相关者能够识别系统边界、把职责分配给特定的参与者,并突显潜在的瓶颈。
通过运用箭头投票和热点识别等技术,团队可以化解冲突,并对问题空间中最关键的区域进行优先级排序,最终为通用语言和领域不变量奠定基础。

### 核心概念

- **System Boundaries(系统边界)**:识别领域中各自独立的不同部分,以理解系统是如何划分的。
- **Actor Responsibilities(参与者职责)**:把系统的特定部分映射到角色(例如产品经理、法务团队)上,以厘清归属。
- **Conflict Resolution(冲突化解)**:使用热点来暴露并处理领域逻辑中的分歧或歧义。
- **Prioritization(确定优先级)**:采用箭头投票来确定业务最重要的关注领域。
- **Bottleneck Identification(瓶颈识别)**:发现领域流程中阻碍进展的阻塞点和低效之处。

### 课程笔记

Big Picture Event Storming 会议会产出一个可视化的成果物,让团队能够在问题空间中实现若干战略目标。
其中一个主要成果是识别出 **system boundaries**。
通过观察事件的流转,参与者可以辨别出系统的一个部分在哪里结束、另一个部分从哪里开始,从而为最终定义限界上下文(bounded contexts)提供便利。

除了边界之外,这个过程还会厘清 **responsibilities**。
通过把参与者映射到特定的事件序列上,哪些角色(例如产品经理或法务团队)对领域的不同部分负责就变得清晰了。
这种可见性有助于理解软件所需的组织层面的对齐。

**Conflicts and Hotspots** 会在会议中自然地浮现出来。
当利益相关者对某个流程或术语意见不一时,这些点会被标记为热点。
在会议中把这些问题商量清楚,可以防止在实现过程中出现技术债务和不一致。

为了管理项目的范围,**prioritization** 通过箭头投票之类的技术来处理。
这让小组能够共同决定领域中哪些区域最关键或需要立即关注。
此外,这次会议还可以作为一种诊断工具,用来识别可能妨碍领域效率的 **bottlenecks and blockers**。

最终,Big Picture Event Storming 提供了探索问题空间、建立 **ubiquitous language** 以及识别整个系统中必须维护的 **invariants** 所需的工具。
这种高层次的理解为更细粒度的 Process Modeling Event Storming 做好了铺垫,后者专注于对具有明确起点和终点的特定工作流进行建模。

## 13. Section Recap

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/section-recap-54855690/) · 1:47

### 总结

本课总结了 Big Picture Event Storming 阶段,这是一种用于探索领域问题空间的协作技术。
通过召集利益相关者来梳理事件、识别冲突(热点)并在术语上达成一致,团队可以建立起对系统时间线的共同理解,并对最关键的开发领域进行优先级排序。

### 核心概念

- **Domain Exploration(领域探索)**:Big Picture Event Storming 的主要目标是理解问题空间。
- **Timeline Mapping(时间线梳理)**:把头脑风暴得出的事件组织成一个按时间顺序排列的序列,来讲述领域的故事。
- **Hotspots and Opportunities(热点与机会)**:识别不确定、存在分歧或有潜在改进空间的地方。
- **Stakeholder Collaboration(利益相关者协作)**:确保领域专家和决策者在场,以化解冲突并定义术语。
- **Arrow Voting(箭头投票)**:一种优先级排序技术,参与者对最重要的热点或机会进行投票。
- **Loose Guidelines(宽松的指导原则)**:Event Storming 灵活且可调整,关注的是目标而不是死板的规则。

### 课程笔记

Big Picture Event Storming 从对领域不受限制的探索开始。
在最初的阶段,参与者把代表领域事件的便利贴放到建模面上,而不必操心顺序。
一旦最初的头脑风暴完成,小组就着手把这些事件组织成一条连贯的时间线。

在这个过程中,问题和分歧会自然地出现。
它们被记录为 **hotspots**(通常用倾斜放置的红色或橙色便利贴表示)。
与之相对,改进的想法被记录为 **opportunities**。
这一阶段对于商量清楚视角上的差异、并就整个项目中将要使用的一套具体的、共享的术语达成一致至关重要。

会议的成功依赖于让对的人在场,特别是那些拥有领域知识并有权做出产品决策的人,例如产品经理。
他们的意见让团队能够化解热点,并定义系统的预期行为。

为了完善模型,团队会识别出流程中涉及的 **actors**(人)和 **external systems**。
他们通过正向或反向地走查事件来填补时间线中的缺口,以确保故事完整且合乎逻辑。
这种讲故事的方式有助于识别缺失的事件或错误的顺序。

由于一次 Big Picture 会议可能会发现许多问题,因此会使用 **arrow voting**(或点投票)来集中团队的精力。
每位参与者会拿到两个箭头,用来标记他们认为最重要的热点或机会。

最终,Event Storming 提供的是宽松的指导原则,而不是严格的规则。
总体目标是在进入更详细的阶段(例如 Process Modeling Event Storming)之前,有效地探索问题空间。
