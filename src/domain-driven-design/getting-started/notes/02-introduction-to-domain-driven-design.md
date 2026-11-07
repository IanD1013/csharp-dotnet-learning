# Introduction to Domain-Driven Design

> 课程:[Getting Started: Domain-Driven Design](https://dometrain.com/course/getting-started-domain-driven-design-ddd/) · 第 2 章
> 共 5 课 · 约 14:55
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [What is Domain-Driven Design?](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/what-is-domain-driven-design-54123298/) | 1:27 | [↓](#1-what-is-domain-driven-design) |
| 2 | [What is a Domain?](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/what-is-a-domain-54123299/) | 4:26 | [↓](#2-what-is-a-domain) |
| 3 | [Why you should learn and practice DDD](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/why-you-should-learn-and-practice-ddd-54123300/) | 2:19 | [↓](#3-why-you-should-learn-and-practice-ddd) |
| 4 | [The two parts of DDD](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-two-parts-of-ddd-54123301/) | 4:56 | [↓](#4-the-two-parts-of-ddd) |
| 5 | [Section Recap](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/section-recap-54123302/) | 1:47 | [↓](#5-section-recap) |

## 1. What is Domain-Driven Design?

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/what-is-domain-driven-design-54123298/) · 1:27

### 总结

领域驱动设计(Domain-Driven Design,DDD)是一种开发复杂软件系统的方法论,它让代码库和架构与底层的问题空间(即领域)保持一致。
通过在实现之前运用特定的实践来探索领域,DDD 确保最终的系统结构保持可维护,并确保代码中使用的术语反映真实的业务环境,从而随着时间推移减少摩擦与分歧。

### 核心概念

- 在系统架构中对问题空间进行建模。
- 在实现之前进行领域探索的实践。
- 让代码结构与业务需求保持一致。
- 通过减少术语分歧来实现长期可维护性。

### 课程笔记

领域驱动设计(DDD)被定义为一种开发复杂系统的方法。
从本质上说,DDD 关注的是确保系统、它的代码库以及它的架构能够准确地为软件所处的问题空间(也就是领域)建模。

这一方法论包含多种旨在促进领域探索的实践。
这些实践鼓励开发者和利益相关者在开始实现阶段之前深入理解业务需求和业务逻辑。
通过优先进行这种探索,最终得到的系统更有可能反映业务的真实需求。

DDD 的首要目标是让领域与技术系统之间达到高度的相似。
这种一致性带来若干长期的好处:

- **Improved Structure(更好的结构)**:代码库自然地遵循业务的逻辑。
- **Maintainability(可维护性)**:与其领域相映照的系统,在业务需求演进时更容易更新。
- **Consistent Terminology(一致的术语)**:DDD 把代码中使用的语言与业务专家使用的语言发生分歧的风险降到最低,使系统用起来更直观。

## 2. What is a Domain?

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/what-is-a-domain-54123299/) · 4:26

在领域驱动设计中,领域是应用程序逻辑所围绕的特定知识与活动范畴。
它代表软件想要解决的问题空间,由一组业务规则、约束和逻辑边界来界定。
理解领域需要识别核心主题领域及其各个子领域,以确保软件准确地反映它所自动化或辅助的真实世界流程。

### 核心概念

- **Domain Definition(领域的定义)**:用户应用程序所针对的主题领域,也是应用程序逻辑所围绕的知识范畴。
- **Application Logic(应用程序逻辑)**:领域的核心关注点,代表软件功能的逻辑边界。
- **Sub-domains(子领域)**:存在于一个更大、更宽泛的领域之下的、更小且更专门的知识领域。
- **Business Rules and Constraints(业务规则与约束)**:支配领域如何运作以及软件必须如何表现的具体决策(例如定义用户状态或参与情况)。

### 课程笔记

在软件开发和领域驱动设计(DDD)的语境中,领域是用户应用程序所针对的主题领域。
它是界定应用程序逻辑边界的特定活动与知识范畴。
虽然一般性的定义可能把领域描述为一个宽泛的研究领域,但 DDD 关注的是软件运作所在的那个具体的、往往很细分的领域。

领域的核心是应用程序逻辑。
这种逻辑代表了支配系统在其特定边界内如何表现的规则和流程。
例如,在一个大型组织中,一个宽泛的领域可能是 "Meeting Intelligence",也就是通过机器学习和人工智能,利用技术来改善会议体验。

领域往往是分层的,由多个子领域组成。
以 Meeting Intelligence 为例,这种层级可能是这样的:

1.  **Broad Domain(宽泛领域)**:Meeting Intelligence。
2.  **Sub-domain(子领域)**:专门在 Microsoft Teams 这类沟通平台中的 Meeting Intelligence。
3.  **Niche Sub-domain(细分子领域)**:AI 驱动的会议回顾(meeting recap)。这个具体领域专注于利用会议记录稿,为错过会议或只参加了部分会议的用户生成章节、提及和洞察。

```text
Meeting Intelligence                         (Broad Domain)
  └── Meeting Intelligence in Microsoft Teams  (Sub-domain)
        └── AI-driven meeting recaps             (Niche Sub-domain)
```

领域由它的约束和业务规则来界定。
这些不是技术实现细节,而是必须在代码中体现出来的业务决策。
例如,在一个会议回顾应用中,系统必须定义什么算作 "leaving early"(提前离开)。
阈值是五分钟、十分钟还是二十分钟,这是一个领域决策。
同样,判断收到转发邀请的用户是否应被视为参与者并收到回顾通知,也是一条属于该领域的业务规则。
这些规则决定了应用程序逻辑如何组织,以及软件如何服务它的用户。

## 3. Why you should learn and practice DDD

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/why-you-should-learn-and-practice-ddd-54123300/) · 2:19

### 总结

领域驱动设计(DDD)是一种为应对大规模软件系统的复杂性而发展出来的方法论,在这类系统中,代码常常与真实的业务领域发生偏离。
通过提供一套结构化的实践和指导原则,DDD 帮助开发者构建与其所解决的问题空间紧密映照的系统。
这种一致性降低了在业务需求与技术实现之间架设桥梁的认知负担,从而带来更易维护、更灵活的软件。

### 核心概念

* 在领域(问题空间)与系统(解决方案空间)之间架起桥梁。
* 运用一种经过验证的方法论来设计复杂系统。
* 实施循序渐进的实践、工具和指导原则。
* 通过新的概念框架培养思维上的灵活性。
* 在日常开发中逐步应用 DDD 模式。

### 课程笔记

领域驱动设计(DDD)是作为对构建和维护复杂系统所面临挑战的回应而出现的。
在许多传统的开发环境中,真实的业务领域(即问题空间)与代码库之间存在显著的鸿沟。
开发者常常发现自己被迫学习两个截然不同的模型:利益相关者所理解的业务逻辑,以及系统中实际存在的技术实现。
DDD 提供了一种弥合这道鸿沟的方法论,确保软件架构反映底层的领域。

DDD 的核心是一套实践和指导原则,它们引导开发过程,从问题空间的最初概念化一直到最终实现。
通过让系统的结构与领域保持一致,软件在长期内变得更容易维护和演进。

除了结构上的好处之外,DDD 还鼓励思维上的灵活性。
就像学习哈希表这类数据结构会扩充开发者解决问题的工具箱一样,DDD 引入了思考软件的新概念框架。
这些思想让开发者能够从不同的角度去处理问题,即使他们并没有实现一套完整的 DDD 架构。
DDD 中的许多原则都可以逐步应用,以改进现有系统中的日常开发任务。

## 4. The two parts of DDD

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-two-parts-of-ddd-54123301/) · 4:56

### 总结

领域驱动设计分为两个主要阶段:战略设计(strategic design)和战术设计(tactical design)。
战略设计关注高层次的领域探索、识别子领域以及确定业务优先级,以确保资源得到有效分配。
战术设计处理实现细节,关注在特定子领域内如何通过类、对象关系和状态校验把领域逻辑映射到代码。

### 核心概念

- Strategic Design(战略设计):高层次的架构规划与领域探索。
- Tactical Design(战术设计):低层次的实现模式与代码建模。
- Subdomains(子领域):问题空间的逻辑划分。
- Problem Space vs. Solution Space(问题空间与解决方案空间):在实现技术解决方案之前先理解领域。
- Resource Allocation(资源分配):根据业务价值决定在哪里投入开发精力。

### 课程笔记

领域驱动设计(DDD)由两个必不可少的阶段组成:战略阶段和战术阶段。
虽然许多开发者把注意力放在战术模式上,但战略设计是一个关键组成部分,它提供了构建有效系统所必需的 "big picture"(全局视图)。

#### Strategic Design

战略设计是关于探索领域、理解软件运作所在的空间。
它需要以 "1000-foot view"(千英尺视角)来审视系统,以识别子领域并定义它们之间的关系。
这一阶段的关键活动包括:

- **Identifying Subdomains(识别子领域)**:把一个庞大、复杂的领域拆分成更小的、合乎逻辑的、自包含的组成部分。
- **Defining Relationships(定义关系)**:确定不同子领域如何交互,例如识别上游和下游依赖。
- **Business Prioritization(业务优先级)**:判断系统的哪些部分是业务的核心、需要大量投入,哪些部分是已被解决的问题、可以用现成的解决方案来处理。

战略设计的目标是组织问题空间,使各个团队能够在不同的组成部分上独立工作,并把精力集中在能带来最佳投资回报的地方。

#### Tactical Design

战术设计关注实现的 "how"(如何做)。
一旦定义好战略边界,战术设计就提供在这些边界内为领域建模的工具。
这一阶段包括:

- **Class and Object Modeling(类与对象建模)**:设计类的结构以及类之间的关系。
- **State Validation(状态校验)**:实现规则和指导原则,确保领域对象按照业务规则始终保持有效状态。
- **Domain Mapping(领域映射)**:把逻辑上的领域概念转化为一个能够准确表达业务逻辑的可用代码库。

#### Course Methodology

需要注意的是,本课程的结构是为了教学上的清晰而设计的。
虽然一个真实世界的 DDD 项目可能遵循不同的时间顺序,但这些课程的编排是为了以尽可能易于理解的方式介绍各个概念。

## 5. Section Recap

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/section-recap-54123302/) · 1:47

### 总结

本课为领域驱动设计(DDD)的入门部分作结,把它定义为一种让系统设计与真实业务领域保持一致的方法论。
它着重说明了战略设计与战术设计之间的区别:战略设计关注高层次的系统架构和组件交互,而战术设计处理特定领域组件的内部建模和类结构。

### 核心概念

- **Domain-Driven Design (DDD)**:一种软件开发方法,其中系统设计与业务领域紧密映照。
- **Domain Hierarchy(领域层级)**:领域常常嵌套在其他领域之中,层级越深就越具体、越细分。
- **Strategic Design(战略设计)**:对系统的高层次 "1,000-foot view"(千英尺视角),关注逻辑组件以及它们之间的交互。
- **Tactical Design(战术设计)**:在单个逻辑组件内部进行的低层次建模,关注类的定义、边界以及内部交互。

### 课程笔记

领域驱动设计(DDD)是一种方法论,专注于设计软件系统,使其结构和逻辑与它所代表的业务领域紧密一致。
这种方法为开发者提供了有效设计复杂系统所需的思维灵活性。

#### Understanding Domains

领域是应用程序围绕其构建的知识或活动范畴。
领域往往是分层的;一个大的领域可以包含多个子领域,而这些子领域又包含更加具体的领域。
这种结构意味着,随着你深入系统,领域会变得越来越细分、越来越专门。

#### Strategic vs. Tactical Design

DDD 分为两个主要的关注领域:

1. **Strategic Design(战略设计)**:这需要以高层次的 "1,000-foot view"(千英尺视角)审视整个系统。它关注识别各个子逻辑组件,并理解它们彼此之间如何交互。战略设计是关于把系统作为一个整体来可视化,并定义架构中不同部分之间的关系。
2. **Tactical Design(战术设计)**:这需要聚焦到单个逻辑组件上,定义它的内部结构。战术设计关心的是具体的实现细节,例如定义类、确定它们如何交互,以及在特定领域内建立清晰的边界,以便为它要解决的问题建模。

通过同时掌握战略设计和战术设计,开发者可以创建既在架构上稳健、又准确反映业务需求的系统。
