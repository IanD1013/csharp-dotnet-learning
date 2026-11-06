# Process Modeling Event Storming

> 课程:[Deep Dive: Domain-Driven Design](https://dometrain.com/course/deep-dive-domain-driven-design-ddd/) · 第 4 章
> 共 8 课 · 约 34:05
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [What is Process Modeling Event Storming?](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/what-is-process-modeling-event-storming-54855707/) | 4:28 | [↓](#1-what-is-process-modeling-event-storming) |
| 2 | [Process Modeling - The Rules of the Game](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/process-modeling-the-rules-of-the-game-54855708/) | 1:29 | [↓](#2-process-modeling---the-rules-of-the-game) |
| 3 | [Process Modeling in Action - Setup](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/process-modeling-in-action-setup-54855709/) | 2:09 | [↓](#3-process-modeling-in-action---setup) |
| 4 | [Round 1 - Framing the Problem](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/round-1-framing-the-problem-54855710/) | 2:00 | [↓](#4-round-1---framing-the-problem) |
| 5 | [Round 2 - Rush to the Goal](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/round-2-rush-to-the-goal-54855711/) | 13:24 | [↓](#5-round-2---rush-to-the-goal) |
| 6 | [Round 3 - Fill in the Blanks](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/round-3-fill-in-the-blanks-54855712/) | 8:22 | [↓](#6-round-3---fill-in-the-blanks) |
| 7 | [Process Modeling Event Storming Goals](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/process-modeling-event-storming-goals-54855713/) | 1:13 | [↓](#7-process-modeling-event-storming-goals) |
| 8 | [Section Recap](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/section-recap-54855714/) | 1:00 | [↓](#8-section-recap) |

## 1. What is Process Modeling Event Storming?

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/what-is-process-modeling-event-storming-54855707/) · 4:28

### 总结

Process Modeling Event Storming(PLES)是一种聚焦的建模技术,用于细化在更宽泛的"Big Picture"会议中识别出的特定业务流程。
通过收窄范围,利益相关者和工程师协作绘制从初始命令到最终领域事件的流程,并纳入命令(command)、策略(policy)和读模型(read model),以确保对系统行为和用户体验有全面的理解。

### 核心概念

* **Process Level Event Storming(PLES):** 一种详细的建模会议,聚焦于某个特定的、小范围的流程,而不是整个领域。
* **Cross-Functional Collaboration(跨职能协作):** 需要一名主持人、技术负责人(后端和前端)以及有决策权的产品经理。
* **Command(命令):** 发起一个流程的入口点或意图。
* **Read Model(读模型):** 呈现给用户、为其下一步行动提供依据的数据或视图。
* **Policy(策略):** 连接事件与命令的业务规则或响应式逻辑。
* **Success States(成功状态):** 定义为"System Happy"(期望的技术状态)和"Customer Happy"(期望的用户结果)。

### 课程笔记

Process Modeling Event Storming,也称为 Process Level Event Storming(PLES),把设计工作从宽泛的探索推进到详细的实现逻辑。
Big Picture Event Storming 识别的是整个领域的"是什么",而 PLES 聚焦于某个特定工作流的"怎么做"。

#### 会议准备

一次成功的 PLES 会议需要一名主持人来维护规则,还需要"合适的人"在场。
这个小组必须包括技术方面的利益相关者,例如后端和前端的首席工程师,以及业务方面的利益相关者,例如产品经理。
目标是组建一个团队,能够就流程的需求和约束提出关键问题并给出权威的回答。
由于范围缩小,所需的物理或虚拟空间比 Big Picture 会议小,但环境仍然必须鼓励开放地分享观点。

#### 建模组件

PLES 为 Event Storming 的调色板引入了更多的构建块:
- **Commands(命令):** 表示用户的意图或系统的入口点(例如 "Create Participant Profile")。
- **Read Models(读模型):** 表示用户所观察到的数据视图。用户通常会先查看一个读模型,再决定触发一个命令。
- **Policies(策略):** 表示业务规则或对事件的自动化响应。

#### 建模工作流

会议的结构是从一个起始状态推进到一个期望的结果。
白板通常按照清晰的从左到右的流向来组织:

1. **Contextual Prerequisites(上下文前置条件):** 在最左侧,团队放置那些必须已经发生、当前流程才成立的事件。例如,在用户能够在某个课程中预订名额之前,"Gym Created" 或 "Session Created" 这样的事件必须已经发生。这些事件为当前状态提供了必要的上下文。
2. **The Entry Point(入口点):** 流程从一个特定的命令开始。这个命令之前通常有一个读模型,为用户提供采取行动所需的信息(例如为开通档案而输入的详细信息)。
3. **The Sequence(序列):** 团队对到达流程终点所需的命令、事件和策略的序列进行建模。这些便利贴必须按照特定的顺序放置,这个序列才算有效。
4. **The End State(结束状态):** 当系统到达"System Happy"状态(例如 "Feedback Submitted"),且用户到达"Customer Happy"状态(例如显示"Congratulations"的确认视图)时,流程结束。

```text
 左                                                                  右
+---------------------+   +------------+   +------------+   +--------------+
| 上下文前置条件      |   | Read Model |   | Commands / |   | System Happy |
|  Gym Created        |-->|     |      |-->| Events /   |-->| Customer     |
|  Session Created    |   |  Command   |   | Policies   |   |   Happy      |
+---------------------+   +------------+   +------------+   +--------------+
```

通过绘制从初始命令到最终领域事件的整个历程,团队确保在开始实现之前,所有技术需求和业务需求都已对齐。

## 2. Process Modeling - The Rules of the Game

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/process-modeling-the-rules-of-the-game-54855708/) · 1:29

### 总结

本课定义了 Event Storming 中流程建模的正式顺序和规则。
它概述了一次交互的生命周期:从参与者(actor)观察到的读模型开始,引出一个针对系统执行的命令,最终产生一个领域事件。
本课还探讨了领域事件如何触发策略,从而自动执行后续命令,或为后续的用户交互更新读模型。

### 核心概念

* **Read Model(读模型)**:展示给参与者的信息或状态,作为决策的依据。
* **Actor(参与者)**:观察读模型并发起动作的个人或实体。
* **Command(命令)**:在系统中执行某个动作的意图表达。
* **System(系统)**:处理命令并产生结果的组件或边界。
* **Domain Event(领域事件)**:对领域中发生的某件重要事情的记录。
* **Policy(策略)**:为响应特定领域事件而触发新命令的响应式逻辑。

### 课程笔记

Event Storming 中的流程建模遵循一个严格的组件序列,通常用特定颜色的便利贴来表示,以保持清晰和一致。

流程从一个 **Read Model** 开始。
它表示呈现给一个 **Actor** 的数据或状态。
在观察到这些信息后,参与者决定调用一个 **Command**。
每个命令都必须指向一个 **System**,由它来处理这个请求。

命令的成功执行会产生一个 **Domain Event**。
这个事件表示一个已经发生的状态变化。
从这里开始,流程可以向两个方向分支:

1. **Read Model Updates(读模型更新)**:领域事件导致一个或多个读模型被创建或更新。这是展示回给用户、或通过网络传输给其他系统的信息,随后可以被参与者观察到,从而再次开始这个循环。
2. **Policy Triggers(策略触发)**:领域事件可以触发一个 **Policy**。策略封装的是"每当"逻辑:每当某个特定事件发生,就应当跟随一个特定的动作。例如,如果发生了 `SessionFeedbackSubmitted` 事件,可能会触发 `NotifyTrainerPolicy`。这个策略随后调用一个新命令,例如 `NotifyTrainer`,它被发送给一个系统,从而延续流程建模的链条。

```text
 Read Model --> Actor --> Command --> System --> Domain Event
     ^                       ^                       |
     |                       |              +--------+--------+
     |                       |              |                 |
     |                       |              v                 v
     +----------------------------- Read Model 更新         Policy
                             |                                |
                             +--------------------------------+

 例如:SessionFeedbackSubmitted -> NotifyTrainerPolicy -> NotifyTrainer
```

遵循这个序列(Command、System、Domain Event,然后是 Read Model 或 Policy),团队就能以技术上的精确度描绘出复杂的业务流程。
与 big-picture event storming 类似,流程建模通常也包含多轮细化,以捕获领域行为的完整范围。

## 3. Process Modeling in Action - Setup

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/process-modeling-in-action-setup-54855709/) · 2:09

### 总结

本课为一次使用 Event Storming 的流程建模会议打下基础。
它定义了要建模的具体工作流(从用户成为参与者到提交课程反馈的历程),并确定了一次成功会议所需的关键利益相关者,包括技术负责人、产品经理、设计师和领域专家。

### 核心概念

- **Workspace Management(工作区管理)**:利用 UI 快捷键最大化建模区域,并在 big-picture 会议和特定流程会议之间切换。
- **Process Scoping(流程范围界定)**:确定业务流程的具体起点和终点,以保持聚焦。
- **Cross-functional Collaboration(跨职能协作)**:确保工程、产品、设计和领域用户都有代表在场,以捕获所有视角。
- **Initial Framing(初始框定)**:流程建模的第一阶段,在深入详细事件之前聚焦于定义问题空间。

### 课程笔记

会议从准备建模环境开始。
在工作区中,有不同的视图分别用于 big-picture event storming 和特定的流程建模。
可以用快捷键切换用户界面,为建模画布腾出更多空间,确保注意力保持在正在绘制的流程上。

本次会议选定的具体流程跟踪用户经历的三个主要阶段:
1. 成为参与者。
2. 在某个课程中预订名额。
3. 为该课程提交评价。

为了框定会议,需要设立"书挡"(bookends)来定义范围。
流程以命令 **Create participant profile** 开始,以事件 **Session feedback provided** 结束。
通过把焦点收窄到这个特定流程,团队可以忽略在更宽泛的"big picture"会议中可能出现的无关领域,例如销售或市场营销,从而更深入地探究参与者的生命周期。

有效的流程建模需要一组多元化的利益相关者,以确保模型反映现实。
这包括:
- **Engineering(工程)**:首席后端和前端开发者,提供技术背景和实现约束。
- **Product & Design(产品与设计)**:产品经理和设计师,确保流程与用户体验和业务目标保持一致。
- **Domain Participants(领域参与者)**:真正在领域中活动的人(例如参加课程的人),能够分享关于流程在实践中如何运作的第一手视角。

```text
  [Create participant profile] ----------------> (Session feedback provided)
   起点:命令                                        终点:事件
        |                                                 |
        +-- 成为参与者 --> 预订名额 --> 提交评价 ----------+
```

在定义好范围、合适的人也已到场之后,会议进入第一个实际的回合:框定问题。

## 4. Round 1 - Framing the Problem

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/round-1-framing-the-problem-54855710/) · 2:00

### 总结

Event Storming 的第 1 轮聚焦于通过确立业务流程的边界来框定问题。
这包括识别初始状态(包括前置条件和上下文事件),以及期望的结束状态或 happy path 结果,以确保所有利益相关者在深入详细建模之前对范围有共同的理解。

### 核心概念

- **Problem Framing(问题框定)**:定义某个特定业务流程的起点和终点,以确立范围。
- **Contextual Events(上下文事件)**:必须在当前流程之前已经发生、以提供必要上下文的领域事件。
- **Actor Identification(参与者识别)**:识别发起该流程的具体角色或系统组件。
- **Read Models(读模型)**:参与者为做出明智决策或执行命令所需的数据或文档。
- **Settled State(稳定状态)**:流程的成功结束,此时系统是稳定的,用户的目标也已达成。

### 课程笔记

Event Storming 第一轮的首要目标是框定问题空间。
在这个阶段,重点是战略层面的探索和获取领域知识,而不是产出技术规格或可编译的代码。
框定确保流程的边界得到清晰定义,让团队能够基于获得的知识做出明智的战略决策。

#### 确立起始上下文

为了开始框定,建模白板的左侧会填入初始上下文和最初的动作。
这包括识别主要参与者以及他们继续推进所需的信息。

- **Actor(参与者)**:发起该流程的个人或系统(例如 User)。
- **Command(命令)**:采取的初始动作,例如 "Create participant profile"。
- **Read Model(读模型)**:呈现给参与者、便于其执行命令的信息,例如 "Health Waiver"。

此外,若干上下文事件会被放在起始线的左侧,用来表示在这个特定流程开始之前系统所处的状态。
这些前置条件包括:
- **User Registered**
- **User Signed In**
- **Gym Created**
- **Room Created**
- **Session Created**

#### 定义结束状态

白板的右侧表示"happy path",即流程达到成功结束时的稳定状态。
它定义了系统和用户都实现各自目标时是什么样子。

- **Terminal Event(终止事件)**:此范围内最后一个领域事件,例如 "Session feedback provided"。
- **Outcome(结果)**:满足用户目标的最终动作或状态变化,例如系统把填写完成的反馈表发送到参与者的电子邮箱。

```text
 起始线左侧          |  左侧起点                  ...  右侧终点
---------------------+-------------------------------------------------------
 User Registered     |  Actor: User                    Session feedback
 User Signed In      |  Read Model: Health Waiver        provided
 Gym Created         |  Command: Create participant    -> 反馈表发送到
 Room Created        |    profile                         参与者邮箱
 Session Created     |
```

通过用清晰的边界标出流程的开始和结束,团队为后续的建模回合确立了一个有界的上下文。

## 5. Round 2 - Rush to the Goal

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/round-2-rush-to-the-goal-54855711/) · 13:24

### 总结

Event Storming 的第 2 轮"Rush to the Goal"聚焦于绘制业务流程从初始命令到最终领域事件的主要"happy path"。
通过优先处理主流程,并利用 middle-out 建模之类的策略(先放置关键事件,之后再填补空隙),团队可以识别出必要的系统、读模型和策略,而不会被边缘情况或复杂的分支逻辑拖慢。
这一阶段通过对系统核心功能建立直观的映射,弥合了高层业务需求与技术实现之间的差距。

### 核心概念

- **Rush to the Goal**:一个建模阶段,聚焦于从头到尾完成主要的、成功的业务流程。
- **Modeling Strategies(建模策略)**:包括正向(从开始到结束)、反向(从结束到开始)、混沌探索,以及 middle-out(连接中间的关键事件)等技巧。
- **Happy Path Focus(聚焦 Happy Path)**:专注于流程的成功执行(例如成功预订课程名额并参加课程),而忽略失败或取消。
- **Hotspots(热点)**:红色便利贴,用于标记复杂的分支或未解决的问题,留待之后讨论,而不打断当前的流程。
- **System Mapping(系统映射)**:识别负责处理特定命令的逻辑系统边界(例如 User Management、Session Reservation)。
- **Read Models and Policies(读模型与策略)**:定义返回给参与者的信息,以及由领域事件触发的自动化逻辑(策略)。

### 课程笔记

Event Storming 的第 2 轮"Rush to the Goal"旨在确立系统的主要叙事。
目标是沿着流程的"良好路径"(good flow),把初始命令和最终领域事件连接起来。

#### 建模策略

有几种策略可以促进这一过程:
- **Forward Evolution(正向演进)**:从开始按时间顺序思考整个流程。
- **Backward Mapping(反向映射)**:从最终事件开始,确定到达它之前必须发生哪些事件。
- **Mini Chaotic Exploration(小型混沌探索)**:让参与者同时放置便利贴,以找到一个合乎逻辑的起点。
- **Middle-Out**:把高层的、关键的领域事件放在时间线的中间,然后填补它们之间的空隙,把开始和结束连接起来。

在这一轮中,焦点严格保持在主流程上。
例如,在一个健身房管理系统中,流程涵盖用户预订名额、参加课程以及提供反馈。
如果讨论涉及边缘情况,例如教练取消课程或没有房间,就在白板上放一个"hotspot"(红色便利贴),表示这个分支需要之后再探索,让团队可以继续推进主路径。

#### 绘制参与者历程

流程从用户创建档案开始。
`Create Participant Profile` 命令在某个系统上被调用,例如 **User Management** 或 **Profile Management**。
这产生了 `Participant Profile Created` 领域事件。
在这个事件之后,会有一个读模型返回给用户,通常包含新参与者的 ID 和其他档案详情。

接下来,参与者(现在是一名 **Participant**)浏览可用的课程。
他们查看课程详情,并调用 `Reserve Spot in Session` 命令。
这个命令由 **Session Reservation System** 处理,产生 `Session Spot Reserved` 事件。
用户会收到一个包含课程详情和确认码的读模型。

#### 课程执行与出勤

接着流程进入课程的执行阶段。
一名 **Trainer** 在 **Session Reservation System** 上调用 `Start Session` 命令,这会触发 `Session Started` 领域事件。

为了处理向参与者出勤的过渡,可以实现一个 `Session Started Policy`。
虽然其中一个分支可能涉及通知所有参与者,但主流程聚焦于教练生成一个唯一的二维码。
参与者看到这个二维码后调用 `Join Session` 命令。
这由 **Session Reservation System** 处理,产生 `Participant Joined Session` 事件,并为参与者生成一个确认码。

#### 反馈闭环

课程结束后,教练在 **Session Reservation System** 上调用 `End Session` 命令,产生 `Session Ended` 事件。
随后,一个 `Session Ended Policy` 触发 `Request Feedback from Participant` 命令,由 **Notifications System** 处理。

这引出 `Session Feedback Requested` 事件(之前称为 `Participant Notified`)。
接着,参与者看到一个反馈表,其中包含课程详情,例如名称、描述和参与者名单。
最后,参与者在 **Feedback System** 上调用 `Submit Feedback` 命令,产生最终的领域事件:`Session Feedback Provided`。
这会生成一个确认码,并把反馈的副本发送到用户的电子邮箱。

```text
 Create Participant Profile
   -> User Management / Profile Management -> Participant Profile Created
 Reserve Spot in Session
   -> Session Reservation System -> Session Spot Reserved
 Start Session (Trainer)
   -> Session Reservation System -> Session Started
      Session Started Policy: Trainer 生成二维码
 Join Session
   -> Session Reservation System -> Participant Joined Session
 End Session (Trainer)
   -> Session Reservation System -> Session Ended
      Session Ended Policy
 Request Feedback from Participant
   -> Notifications System -> Session Feedback Requested
 Submit Feedback
   -> Feedback System -> Session Feedback Provided
```

## 6. Round 3 - Fill in the Blanks

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/round-3-fill-in-the-blanks-54855712/) · 8:22

### 总结

Event Storming 流程的第 3 轮聚焦于识别并细化尚未探索的分支、错误条件以及跨系统的交互。
通过越过主要的 happy path,团队可以发掘关键的业务逻辑,例如支付处理和取消策略,并确定不同的限界上下文(如 Notifications、Billing 和 Reservations)如何通过策略和命令进行交互。

### 核心概念

* **Unexplored Branches(未探索的分支):** 识别在最初的 big-picture storming 中遗漏的替代路径和边缘情况。
* **Negative Events(负面事件):** 对失败状态建模,例如在业务规则未满足时产生的 **Participant Join Session Rejected** 事件。
* **Policy-Driven Commands(策略驱动的命令):** 用策略来连接各个系统,例如一个取消事件触发计费系统中的退款命令。
* **Business Decision Discovery(业务决策发现):** 利用建模过程促使人们就实现细节做出决策,例如支付是预先处理还是在现场处理。
* **Cascading Effects(级联影响):** 可视化高层动作(例如删除一个健身房)如何影响房间、课程和预订等子实体。

### 课程笔记

Event Storming 的第三轮是通过深入探究业务流程中未探索的分支来填补空白。
这一阶段对于从高层概览过渡到一个能够考虑现实世界复杂性和业务规则的详细模型至关重要。

#### 探索通知与取消流程

一个首要的关注点是识别重大事件之后会发生什么。
例如,当教练正式开始一节课程时,某个策略可能规定由 **Notifications System** 通知所有已登记的参与者。
这会产生一个包含课程详情的 **Participant Notified** 事件。
这个事件可能促使某位参与者意识到自己无法参加,于是他们向 **Session Reservation System** 发出一个 **Cancel Reservation** 命令。

#### 处理负面结果

流程建模必须考虑那些不会导致成功的路径。
参与者可能尝试通过二维码加入课程,但如果该参与者没有为这节特定课程正确登记,系统就可能发出一个 **Participant Join Session Rejected** 事件。
这些负面结果通常需要在事件中捕获特定的数据,例如错误信息。

#### 优先级与复杂逻辑

由于对每一个可能的分支建模可能是一个无止境的过程,团队必须优先处理高价值或复杂的场景,例如提前取消。
当发生 **Reservation Canceled** 事件时,一个 **Reservation Canceled Policy** 会触发一个指向 **Billing System** 的 **Refund Participant** 命令,最终产生一个 **Participant Refunded** 事件,并向客户端返回一个确认。

```text
 Reservation Canceled --> Reservation Canceled Policy
                                    |
                                    v
                  Refund Participant --> Billing System
                                              |
                                              v
                                    Participant Refunded --> 确认返回客户端
```

#### 细化预订流程

建模过程常常会揭示最初的假设过于简单。
例如,预订课程可能需要一个预先配置好的支付方式。
这引入了一个新的流程:参与者向 **Billing System** 发出一个 **Set Payment Method** 命令,产生一个 **Payment Method Set** 事件。
因此,预订流程变得更加复杂:一个 **Session Spot Reservation Requested** 事件触发一个 **Session Spot Reservation Requested Policy**,该策略发出一个 **Charge Payment Method** 命令。
只有在收到 **Payment Successful** 事件之后,系统才会发出 **Session Spot Reserved** 事件。

```text
 Set Payment Method --> Billing System --> Payment Method Set

 Session Spot Reservation Requested
        |
        v
 Session Spot Reservation Requested Policy
        |
        v
 Charge Payment Method --> Payment Successful --> Session Spot Reserved
```

#### 识别不变量与级联动作

随着这些流程被可视化,一些小的业务不变量(invariant)会浮现出来,应当把它们记录下来。
此外,对高层的管理动作建模,例如管理员删除一个健身房,能让团队看到它对房间、课程和现有预订的级联影响。
这确保了与实现相关的问题得到解答,并且业务决策(例如如何处理教练取消课程或删除健身房)能在设计过程的早期做出。

## 7. Process Modeling Event Storming Goals

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/process-modeling-event-storming-goals-54855713/) · 1:13

### 总结

通过 Event Storming 进行的流程建模,是连接高层领域探索与战略设计的桥梁。
通过对特定的业务流程建模,团队可以识别不一致之处,突出系统间的依赖,并定义行为,最终为定义限界上下文和做出明智的技术选型打下基础。

### 核心概念

* 在一个定义好的框架内对业务流程建模。
* 识别流程中的不一致和重复。
* 映射系统之间的交互(例如计费系统和预订系统)。
* 为战略层面的 DDD 决策建立基础。
* 从流程建模过渡到子领域识别。

### 课程笔记

流程建模,尤其是通过 Event Storming 进行的流程建模,旨在为软件设计实现几个关键目标。
首要目标是在选定的框架内对业务流程建模,为业务如何运作提供一个可视化的、概念性的表示。

在这个过程中,模型自然会暴露出一些原本可能一直隐藏的问题。
这些问题包括工作流中的不一致,以及工作或数据上的重复。
通过把流程可视化,团队可以识别出所涉及的各个系统(例如计费系统或预订系统),并理解它们如何交互。

这项练习提供了一个战略基础。
从流程建模中获得的洞见,对于做出高层架构决策至关重要,例如定义限界上下文和选择合适的技术栈。
此外,它明确识别出系统所需的行为。

一旦领域探索和流程建模完成,向识别子领域的过渡就会变得直观得多。
在 storming 会议中捕获的事件和交互,成为定义系统各子领域边界的原材料。

## 8. Section Recap

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/section-recap-54855714/) · 1:00

### 总结

本课回顾了 Event Storming 的流程建模阶段,重点是从初始命令到产生的领域事件这一流程。
它强调使用针对系统交互和策略的特定规则,来发掘不同系统各自的职责,例如把预订逻辑和计费逻辑分开。

### 核心概念

* **Command-to-Event Flow(命令到事件的流程)**:绘制从一张蓝色命令便利贴到一张或多张橙色领域事件便利贴的历程。
* **System Invocation Rules(系统调用规则)**:命令在系统上被调用,系统随后产生领域事件。
* **Policy Integration(策略集成)**:用策略来定义在流程中触发后续动作的逻辑。
* **Responsibility Identification(职责识别)**:通过建模过程识别出不同的功能领域(例如 Reservation 与 Billing)。

### 课程笔记

Event Storming 中的流程建模是一项结构化的练习,用来可视化一个业务流程的生命周期。
首要目标是追踪从起始命令到其最终领域事件的路径。

这一方法遵循一套特定的规则,以确保一致性。
一个流程通常从一个命令开始(在视觉上用蓝色便利贴表示),这个命令针对某个系统被调用。
这次交互会生成一个领域事件(用橙色便利贴表示)。
在这两点之间,模型可以纳入策略或额外的逻辑,来处理复杂的业务需求。

通过练习这种建模技术,开发者和利益相关者可以更容易地识别出不同子系统的边界。
例如,预订系统显然管理与课程相关的职责,而计费系统处理的是一套完全不同的关注点。
这种清晰性有助于定义更大架构中的各个子领域和限界上下文。
