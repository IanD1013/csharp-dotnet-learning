# Warm up

> 课程:[Deep Dive: Domain-Driven Design](https://dometrain.com/course/deep-dive-domain-driven-design-ddd/) · 第 2 章
> 共 8 课 · 约 34:11
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Designing Systems Following Domain-Driven Design](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/designing-systems-following-domain-driven-design-54855607/) | 8:08 | [↓](#1-designing-systems-following-domain-driven-design) |
| 2 | [Key Strategic and Tactical Practices](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/key-strategic-and-tactical-practices-54855608/) | 3:44 | [↓](#2-key-strategic-and-tactical-practices) |
| 3 | [Domain Discovery](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/domain-discovery-54855610/) | 2:32 | [↓](#3-domain-discovery) |
| 4 | [Domain Discovery Techniques](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/domain-discovery-techniques-54855612/) | 8:11 | [↓](#4-domain-discovery-techniques) |
| 5 | [Event Storming Sneak Peek](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/event-storming-sneak-peek-54855613/) | 2:27 | [↓](#5-event-storming-sneak-peek) |
| 6 | [Documenting Ubiquitous Language](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/documenting-ubiquitous-language-54855614/) | 4:16 | [↓](#6-documenting-ubiquitous-language) |
| 7 | [Documenting Invariants](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/documenting-invariants-54855615/) | 3:25 | [↓](#7-documenting-invariants) |
| 8 | [Section Recap](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/section-recap-54855616/) | 1:28 | [↓](#8-section-recap) |

## 1. Designing Systems Following Domain-Driven Design

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/designing-systems-following-domain-driven-design-54855607/) · 8:08

### 总结

本课为从 "Point Zero"(即对领域一无所知的状态)过渡到可供实现的结构化系统设计确立了一条路线图。
它区分了战略阶段和战术阶段:前者关注子领域(subdomain)和限界上下文(bounded context),后者处理聚合(aggregate)和领域逻辑。
本课以一个健身房排课系统作为主要示例,解释如何超越单纯实现预先定义好的不变量(invariant),借助上下文映射图(context map)和流程建模等产物去发现领域的结构。

### 核心概念

- **Point Zero**:项目的初始阶段,此时领域、子领域和模型都完全未知。
- **战略阶段(Strategic Phase)**:高层设计过程,用于识别子领域、限界上下文以及它们之间的关系。
- **战术阶段(Tactical Phase)**:实现层面的设计,在这一阶段定义聚合、实体和值对象,以强制执行业务不变量。
- **通用语言(Ubiquitous Language)**:由领域专家和开发者共同建立的一种共享且严谨的语言,用来描述领域。
- **不变量(Invariants)**:在特定边界内必须始终保持为真的业务规则。
- **领域发现(Domain Discovery)**:借助 EventStorming 和上下文映射等产物探索领域、界定其边界与逻辑的过程。

### 课程笔记

使用领域驱动设计(DDD)构建应用的过程,是一段从 "Point Zero" 走到完整实现系统的旅程。
Point Zero 代表发现阶段的起点,此时尚未识别出任何子领域或领域模型。
目标是先经过战略阶段(定义宏观架构),再进入战术阶段,在那里实现具体的代码结构。

#### 战略阶段

战略阶段关注系统的高层设计。
它包括识别一个大型问题空间中的各个子领域,并定义限界上下文来封装它们。
这一阶段的关键活动包括:

- **领域发现**:使用 Big Picture EventStorming 等协作工具来描绘领域及其事件。
- **上下文映射**:创建上下文映射图,直观展示不同的限界上下文之间如何相互关联,以及数据如何在它们之间流动。
- **流程建模**:定义在系统边界内发生的工作流和交互。

#### 战术阶段

一旦确立了战略边界,战术阶段就会聚焦于每个限界上下文的内部结构。
正是在这里,通用语言被固化为领域模型。
首要目标是创建封装领域逻辑并强制执行业务不变量的聚合。
虽然基础的 DDD 实现可能从贫血模型和 TDD 起步,但深入学习需要理解这些模型是如何从战略发现过程中推导出来的。

```text
Point Zero
    |
    v
战略阶段 (Strategic Phase)
  - 领域发现 (Big Picture EventStorming)
  - 上下文映射 (context maps)
  - 流程建模
  => subdomains, bounded contexts
    |
    v
战术阶段 (Tactical Phase)
  => aggregates: 封装领域逻辑, 强制执行 invariants
```

#### 案例研究:健身房课程排期

本课程把这些原则应用到一个健身房预约系统上。
该领域包含若干关键组成部分和规则:

- **实体**:Participants、Admins、Trainers、Gyms、Rooms 和 Sessions。
- **排期不变量**:系统必须确保任何参与者、教练或房间都不会被重复预订。这意味着对于任何共享资源,课程的开始和结束时间都不能重叠。
- **实现**:在战术阶段,这些不变量在领域模型(聚合)内部得到强制执行,以确保系统保持一致的状态。这确保了领域的逻辑完全包含在领域模型内部,而不会泄漏到应用服务中。

遵循这种结构化的方法,开发者可以把一个复杂、未知的领域分解为可实现的软件组件,并让这些组件准确反映业务需求。

## 2. Key Strategic and Tactical Practices

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/key-strategic-and-tactical-practices-54855608/) · 3:44

### 总结

本课概述了用于为领域建模的战略实践和战术实践,并强调 DDD 是一个迭代的、非线性的过程。
它指出了四个关键里程碑(Big Picture Event Storming、Process Modeling Event Storming、Context Mapping 和 Aggregate Design),并解释了每一个如何帮助识别不变量、通用语言、子领域、限界上下文和聚合。

### 核心概念

* **Big Picture Event Storming**:一项战略活动,用于对领域获得宏观理解并识别子领域。
* **Process Modeling Event Storming**:更细粒度地审视特定工作流,以识别聚合和业务规则。
* **Context Mapping**:定义不同限界上下文之间的关系和边界。
* **Aggregate Design**:对领域逻辑和一致性边界的战术实现。
* **迭代式精炼(Iterative Refinement)**:根据战术实现过程中获得的洞见,回头修订战略决策的做法。

### 课程笔记

从最初的领域发现到最终实现的旅程,包含若干关键里程碑。
虽然这些实践是按顺序介绍的,但实际的工作流是循环的;在战术设计中获得的洞见,常常迫使我们回头修订战略边界。

#### Big Picture Event Storming

整个过程从 Big Picture Event Storming 开始。
这是一项高层次的协作练习,旨在描绘出整个领域。
它提供了对不变量、通用语言、子领域以及潜在限界上下文的第一批重要洞见。

#### Process Modeling Event Storming

在宏观全景之后,焦点转向 Process Modeling。
这包括选取之前识别出的某个特定的关键流程,并对其进行详细建模。
这一阶段加深了对通用语言和不变量的理解,但它的主要贡献在于识别出底层的聚合,即在数据变更时被当作单一单元对待的领域对象集群。

#### Context Mapping

Context Mapping 用于定义不同的限界上下文之间如何交互。
这项实践为事件风暴中识别出的边界提供了一次现实检验。
它迫使我们更深入地审视每个上下文中的领域模型,以及它们与聚合之间的关系。
在这一阶段,随着上下文之间的技术依赖和组织依赖变得清晰,重新思考早先的建模决策是很常见的。

#### Aggregate Design

最后一个里程碑是对聚合本身的详细设计。
这一战术阶段关注领域对象的内部结构和行为。
虽然这是一项底层活动,但它为战略设计提供了必不可少的反馈。
设计一个聚合可能会揭示某个限界上下文过大,或者通用语言不一致,从而促使我们回到更早的阶段去精炼模型。

#### DDD 的迭代本质

DDD 不是一个线性过程。
它是一段来回往复的旅程:发现引出建模,建模又引出进一步的发现。
目标是达到这样一个状态:实现能够准确反映领域的复杂性和业务需求。
这一进程中的下一步是 Domain Discovery,也就是对问题空间进行初步探索的起点。

```text
Big Picture Event Storming
        |
        v
Process Modeling Event Storming
        |
        v
Context Mapping
        |
        v
Aggregate Design
        |
        +--> 反馈: 回到更早的阶段精炼模型 (循环, 非线性)
```

## 3. Domain Discovery

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/domain-discovery-54855610/) · 2:32

### 总结

领域发现是战略性领域驱动设计的初始阶段,专注于为一个陌生的业务领域构建一幅全面的心智地图。
通过探索一个系统(例如健身房课程排期)的全貌,团队可以识别阻碍因素、与业务目标保持一致,并确立做出高层架构决策所需的沟通标准。

### 核心概念

- 战略上下文:在做出设计选择之前先建立基础理解。
- 心智地图:把领域可视化,以发现障碍和依赖。
- 有效沟通:让利益相关者和开发者在业务目标和术语上达成一致。
- 战略决策支持:利用发现得到的知识来指导整体软件架构。

### 课程笔记

领域发现是领域驱动设计中战略阶段的奠基步骤。
当开发团队对业务领域几乎没有或完全没有先验知识时(例如一个健身房课程排期与预约系统),就会启动这一步骤。
目标是构建一幅关于领域全貌的"心智地图",识别出各种障碍、阻碍因素以及当前的业务环境。

战略阶段在很大程度上依赖于上下文。
在能够做出架构决策之前,团队必须理解领域的种种细节,以确保软件的结构与业务需求保持一致。
这一发现过程的主要动机包括:

- **领域理解**:深入了解业务的核心组成部分及其复杂性。
- **问题识别**:尽早识别潜在的技术或业务挑战。
- **有效沟通**:在业务术语和目标上达成一致,这有助于发展出通用语言。
- **业务对齐**:确保技术方向与组织的总体目标相匹配。

在这一阶段,人们经常使用可视化协作工具来记录笔记,并在探索领域的过程中保持对领域的共同理解。
这些准备工作确保后续的战略决策建立在业务的实际需求和约束之上。

## 4. Domain Discovery Techniques

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/domain-discovery-techniques-54855612/) · 8:11

### 总结

领域发现是领域驱动设计中的一个战略阶段,专注于探索问题空间,以全面理解业务全貌。
通过运用 Event Storming、Domain Storytelling 和 Brainstorming 等技术,团队可以让利益相关者和领域专家达成一致,揭示隐藏的复杂性,并建立共享的语言。
最终目标是在任何实现开始之前,产生做出明智的架构决策和战略决策所需的洞见。

### 核心概念

- **Event Storming**:一种协作建模技术,使用便利贴来描绘业务流程和事件。
- **Domain Storytelling**:一种可视化方法,通过讲述参与者如何与对象交互的故事来展示领域流程。
- **问题空间(Problem Space)**:由软件要解决的具体问题、困难和约束所界定的逻辑空间。
- **领域专家(Domain Experts)**:对业务的特定领域有深入了解的人,包括健身房老板、教练和营销专家。
- **利益相关者(Stakeholders)**:具有业务利益的人,他们的期望和目标塑造了问题空间。
- **战略对齐(Strategic Alignment)**:确保所有参与者对领域全貌拥有共同的理解和语言的过程。

### 课程笔记

#### 发现技术概览

领域发现对于理解项目全貌、并让所有参与者在一种特定的语言上达成一致至关重要。
有若干技术可以促进这种探索:

- **Event Storming**:这是用于流程建模的一种主要技术。它使用各种颜色的便利贴来代表系统的不同元素,让团队在深入技术理论之前就能直观看到一个流程的最终结果。
- **Domain Storytelling**:这种技术通过讲述具体场景来把领域可视化。例如,一个故事可能描绘一位顾客向收银员请求预订电影,并详细说明这笔交易涉及的电影、时间和数量。
- **Brainstorming Sessions**:这类会议把相关人员召集到同一个房间里进行开放讨论。目标是消除分歧、回答具体问题,并就领域的范围达成共同理解。
- **User Story Mapping 和 Event Modeling**:用于深入了解系统需求和流程的其他工具。

#### 借助专业知识

无论选择哪种技术,发现过程都依赖于各类领域专家的输入来揭示盲点。
在健身房管理系统的背景下,领域专家并不局限于软件专家;他们包括:

- **营销专家**:了解如何通过社交媒体、播客和直接推广来宣传服务的人。
- **健身房老板**:熟悉经营一家场馆的物理约束和技术约束的专家。
- **教练**:了解开展课程有哪些要求的人。
- **参与者**:能提供洞见、说明他们希望排课软件具备什么的用户。

#### 利益相关者与问题空间

利益相关者通过明确他们的期望和目标来为发现过程做出贡献。
他们的视角有助于描绘出"问题空间",即涵盖该业务挑战的种种困难和需求的逻辑区域。
理解问题空间是设计出有效解决方案的前提。

#### 分析当前状态

发现过程还包括评估当前的全貌,其中包括:
- 现有的文档。
- 任何遗留软件的当前状态。
- 竞争对手分析。
- 一般性的领域信息。

#### 战略目标

领域驱动设计的战略阶段专注于在软件架构方面做出明智的决策。
在这一阶段,产出的产物(例如 Event Storming 看板)不需要能编译。
价值在于获得的洞见以及团队之间达成的一致。
关注点始终是探索领域、理解底层问题,而不是产物本身。

## 5. Event Storming Sneak Peek

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/event-storming-sneak-peek-54855613/) · 2:27

### 总结

本课对 Event Storming 作为一种用于领域发现和流程建模的可视化工具进行了高层概述。
它介绍了如何使用按颜色区分的便利贴来代表不同的领域元素(例如事件、参与者和信息),从而对复杂流程建立共同理解,并确立通用语言。

### 核心概念

*   **Event Storming**:一种用于为复杂领域流程建模的协作可视化技术。
*   **颜色编码标记法(Color-Coded Notation)**:使用特定颜色来代表不同类型的领域信息(例如事件、参与者和数据)。
*   **领域发现(Domain Discovery)**:开发中专注于理解业务领域及其需求的阶段。
*   **不变量(Invariants)**:在领域内必须始终保持为真的业务规则。
*   **通用语言(Ubiquitous Language)**:在技术方和业务方利益相关者之间发展出的一种共享的通用语言。

### 课程笔记

Event Storming 是一种通过流程建模来可视化领域内正在发生之事的技术。
Event Storming 不依赖冗长的文本文档或零散的会议,而是使用便利贴的可视化布局来描绘复杂流程,使其更易于理解和记忆。

#### Event Storming 的可视化组成部分

建模过程使用多种颜色的便利贴来给领域中的不同元素分类:

*   **事件(蓝色和橙色)**:这些便利贴代表领域中已经发生的事情。它们用过去时书写,以表示已完成的动作或状态变化。
*   **参与者(黄色)**:这些便利贴代表在流程中执行动作的人或系统。它们通常会标上一个小人图标来表示参与者。
*   **信息(绿色)**:这些便利贴包含流程所需的特定数据或信息,例如 ID、确认码或支付详情。

#### 领域发现的目标

在领域发现阶段使用 Event Storming 的首要目的,是摆脱那些难以浏览的、密密麻麻的单页文档。
通过把领域可视化,团队可以更有效地达成两个关键成果:

1.  **确立不变量**:识别在领域的整个生命周期中都必须维持的核心业务规则和约束。
2.  **发展通用语言**:创建一套项目中每个人都使用的一致词汇,确保技术实现与业务概念完全对齐。

这一可视化产物充当了领域逻辑的一份活文档,让复杂流程变得可管理,并为随后的技术实现提供基础。

## 6. Documenting Ubiquitous Language

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/documenting-ubiquitous-language-54855614/) · 4:16

### 总结

通用语言是领域专家和开发者共同使用的共享词汇,用来确保特定领域内的每个术语都只有一个明确无歧义的定义。
本课重点讲述如何在战略阶段的早期(具体来说是在事件风暴期间)建立一套文档模式,用以记录高层术语和使用示例,这些内容之后会在各个限界上下文中演化为具体的领域模型。

### 核心概念

- **共享词汇**:所有利益相关者都使用的一种通用语言,用来消除歧义。
- **上下文含义**:术语由领域来定义;例如,在健身房的上下文中,"session"指的是瑜伽或尊巴之类的特定课程,而不是心理咨询会谈。
- **从战略到战术的演进**:通用语言起始于发现阶段的高层对齐,并在各个限界上下文中变得更加具体、更加明确。
- **文档模式(Documentation Schema)**:一种记录术语的结构化方法,包括术语的定义和实际使用示例。
- **主动观察**:在领域发现会议中、趁所有利益相关者都在场时,识别并解决命名冲突的做法。

### 课程笔记

通用语言是在整个领域中使用的共享词汇。
它确保当使用"session"这样的术语时,它只有一个经过一致同意的定义。
在健身房管理系统的上下文中,session 特指在某个特定区域进行的瑜伽、尊巴或功能性训练之类的活动,而不是这个词在心理学等领域中可能具有的含义。

建立这种语言分两个阶段进行。
最初,在战略阶段(例如事件风暴期间),重点放在高层的通用语言和不变量上。
这正是在宏观层面上就特定术语的含义达成一致的时候。
随着过程进入战术阶段,语言会变得更加具体、更加底层,最终在软件中表现为具体的领域模型。

由于通用语言特定于某个限界上下文,一个术语在一个上下文中可能是一个意思,在另一个上下文中又是另一个意思。
因此,尽早开始记录这些词汇至关重要。
这包括做出命名上的决定,例如决定参加课程的人应该叫"member"还是"participant"。

在领域发现期间,主动和被动地留意利益相关者使用的语言都很重要。
趁所有人都在同一个房间时解决冲突、敲定定义,可以避免在开发周期后期代价高昂的反复沟通。
为此,应当为通用语言建立一份草案模式。

用于记录这些术语的模式包括:具体的术语(作为标题)、一段文字定义,以及该术语在实践中如何使用的词典式示例。

```markdown
# Ubiquitous Language Draft

## User

**Definition**: An individual who interacts with the system. A user can assume roles such as Admin, Trainer, or Participant. A user has a payment method that is used to pay for the services.
**Usages**: The user created an Admin profile.
**Usages**: The user's payment method was charged.
```

[▶ 观看](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/documenting-ubiquitous-language-54855614/?t=190)

这份草案作为一份活文档,用来跟踪命名约定,并在系统设计不断精炼的过程中确保各方保持一致。
尽早建立这一模式,能为通用语言和领域不变量都打下基础。

## 7. Documenting Invariants

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/documenting-invariants-54855615/) · 3:25

### 总结

不变量是确保系统保持在有效状态的基本规则。
本课区分了高层不变量和底层不变量:前者是在战略规划期间识别出的总体规则,后者是在聚合边界内强制执行的具体规则。
在实现开始之前,以结构化的格式尽早记录这些不变量,可以为利益相关者和开发者提供一个事实来源。

### 核心概念

- **不变量(Invariants)**:为使系统处于有效状态而必须始终为真的业务规则。
- **高层不变量(High-level Invariants)**:在设定聚合边界之前、于战略发现期间识别出的总体规则。
- **底层不变量(Low-level Invariants)**:在特定聚合的边界内强制执行的技术规则。
- **不变量草案(Invariants Draft)**:一份结构化的、不需编译的文档,用于在头脑风暴和事件风暴期间记录业务规则并就其达成一致。

### 课程笔记

不变量代表了维护领域模型完整性的逻辑和约束。
在领域驱动设计中,它们在战略阶段和战术阶段都会被识别出来。

#### 高层不变量与底层不变量

不变量根据其范围和所处的设计阶段进行分类:
- **高层不变量**:它们是在战略阶段(例如事件风暴期间)发现的。它们代表系统必须满足的宽泛业务规则,但尚未被分配给某个具体的技术组件或聚合。
- **底层不变量**:它们是在战术阶段定义的,具体来说是在聚合设计期间。这些不变量是在聚合边界内实际强制执行的规则,用来确保聚合的内部一致性。

```text
Strategic phase                    Tactical phase
(event storming)                   (aggregate design)
+---------------------------+      +---------------------------+
| High-level Invariants     |      | Low-level Invariants      |
| broad business rules      | ---> | enforced within the       |
| not yet assigned to an    |      | boundary of an aggregate  |
| aggregate                 |      | (internal consistency)    |
+---------------------------+      +---------------------------+
```

#### 记录通用语言和不变量

为了促进利益相关者与开发者之间的沟通,在发现术语和规则时就把它们记录下来至关重要。
这从一份通用语言草案开始,用来定义关键实体及其角色。

```markdown
# Ubiquitous Language Draft

## User

**Definition**: An individual who interacts with the system. A user can assume roles such as Admin, Trainer, or Participant. A user has a payment method that is used to pay for the services.
**Usages**: The user created an Admin profile.
**Usages**: The user's payment method was charged.
```

[▶ 观看](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/documenting-invariants-54855615/?t=130)

随着发现过程的推进,团队会识别出具体的不变量。
这些不变量应当被记录在一份"Invariants Draft"中,使用一致的模式,包括不变量的名称和对该规则的详细描述。

```markdown
# Invariants Draft

## Session Capacity Limits

**Description**: Each session has a maximum capacity of participants. The Session Capacity Limits are determined based on the room size, the session type, and trainer choice, ensuring safe and manageable session sizes.
```

[▶ 观看](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/documenting-invariants-54855615/?t=145)

这份文档在会议和头脑风暴期间充当事实依据。
虽然这些草案不包含可执行代码,但它们确立了最终将在战术设计中实现的定义和规则。

## 8. Section Recap

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/section-recap-54855616/) · 1:28

### 总结

本课回顾了领域驱动设计的入门阶段,强调从最初对领域的模糊认识,过渡到对不变量、通用语言和限界上下文的结构化理解。
它着重指出领域发现是战略阶段的奠基步骤,借助事件风暴和领域故事讲述等技术,与领域专家和利益相关者一起精炼术语和系统需求。

### 核心概念

- **领域发现(Domain Discovery)**:探索业务全貌以理解需求和约束的初始过程。
- **战略阶段(Strategic Phase)**:聚焦于限界上下文和整体系统架构的高层设计阶段。
- **发现技术(Discovery Techniques)**:Event Storming、Domain Storytelling 和 Event Modeling 等协作方法。
- **通用语言(Ubiquitous Language)**:由开发者和领域专家共同发展出的一种共享语言,用来确保沟通一致。
- **不变量(Invariants)**:在系统的整个生命周期中都必须维持的核心业务规则和逻辑。

### 课程笔记

领域驱动设计初始阶段的首要目标,是从对领域知之甚少的状态过渡到对业务全貌的全面理解。
这包括识别具体的不变量、建立通用语言,以及定义限界上下文。

战略阶段从**领域发现**开始。
这一过程对于理解当前全貌、并确保后续的战略决策建立在业务的实际需求之上至关重要。
在做出任何战略决策之前,有必要先理解领域的范围以及现有系统的状态。

有若干技术可用于领域发现,包括:
- **Event Storming**:一种通过事件来探索复杂业务领域的协作工作坊形式。
- **Domain Storytelling**:一种把业务流程以及参与者在系统内如何交互可视化的技术。
- **Event Modeling**:一种通过关注信息流和随时间推移的状态变化来设计系统的方法。

有效的领域发现依赖于领域专家和利益相关者的积极参与。
通过运用他们的专业知识并分析当前全貌,团队可以为不变量和通用语言建立一套模式。
这些定义并非一成不变;它们会在设计过程的各个阶段中不断精炼,以确保技术实现准确反映业务需求。
