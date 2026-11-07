# Exploring a Complex Domain

> 课程:[Getting Started: Domain-Driven Design](https://dometrain.com/course/getting-started-domain-driven-design-ddd/) · 第 3 章
> 共 10 课 · 约 60:42
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [The domain we’ll be exploring](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-domain-well-be-exploring-54123323/) | 6:06 | [↓](#1-the-domain-well-be-exploring) |
| 2 | [The domain’s Ubiquitous Language](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-domains-ubiquitous-language-54123324/) | 4:30 | [↓](#2-the-domains-ubiquitous-language) |
| 3 | [Ubiquitous Language as a tool for detecting Domain Objects](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/ubiquitous-language-as-a-tool-for-detecting-domain-objects-54123325/) | 3:41 | [↓](#3-ubiquitous-language-as-a-tool-for-detecting-domain-objects) |
| 4 | [The domain’s invariants](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-domains-invariants-54123326/) | 3:00 | [↓](#4-the-domains-invariants) |
| 5 | [Enforcing invariants](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/enforcing-invariants-54123327/) | 2:44 | [↓](#5-enforcing-invariants) |
| 6 | [Creating the domain objects](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/creating-the-domain-objects-54123328/) | 5:20 | [↓](#6-creating-the-domain-objects) |
| 7 | [Invariants as Unit Tests](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/invariants-as-unit-tests-54123329/) | 10:41 | [↓](#7-invariants-as-unit-tests) |
| 8 | [Enforcing Invariants that rely on dependencies](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/enforcing-invariants-that-rely-on-dependencies-54123330/) | 12:22 | [↓](#8-enforcing-invariants-that-rely-on-dependencies) |
| 9 | [The Result Pattern](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-result-pattern-54123331/) | 10:08 | [↓](#9-the-result-pattern) |
| 10 | [Section Recap](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/section-recap-54123332/) | 2:10 | [↓](#10-section-recap) |

## 1. The domain we’ll be exploring

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-domain-well-be-exploring-54123323/) · 6:06

### 总结

Dome Gym 是一个训练课(session)排期与预约系统,用于管理健身房设施、房间和训练课。
这个领域包含复杂的排期约束,用来防止房间、教练和参与者的训练课时间重叠,同时还有一个分级订阅模型,根据用户的套餐限制健身房数量、房间数量和每日训练课数量。

### 核心概念

- **Session Management(训练课管理)**:为具体的健身活动(例如 Yoga、Zumba、Functional Training)排期和预约。
- **Scheduling Invariants(排期不变量)**:防止房间、参与者和教练出现时间重叠的逻辑。
- **Subscription Tiers(订阅层级)**:约束健身房基础设施规模的业务规则(Free、Starter、Pro)。
- **Ubiquitous Language(通用语言)**:干系人和开发者用来描述领域的共享词汇。

### 课程笔记

#### 系统概览

Dome Gym 是一个平台,健身房老板(Admin)在上面管理设施,参与者在上面预约训练课。
一个健身房由一个或多个房间组成,每个房间承办各种训练课。
训练课由其类型(例如有氧或无氧)、强度等级、时长、指派的教练以及举办它的具体房间来定义。

参与者与系统交互的方式是:通过日历界面浏览可用的训练课,查看训练课详情(包括剩余名额和教练信息),并管理自己的预约。

#### 排期约束与不变量

系统必须强制执行若干排期规则,以维护领域的完整性:

*   **Room Schedule(房间日程)**:单个房间不能承办两个时间重叠的训练课。
    不过,同一健身房内的不同房间可以同时进行多个训练课。
*   **Participant Schedule(参与者日程)**:参与者被禁止在开始和结束时间重叠的两个训练课中预留名额。
*   **Trainer Schedule(教练日程)**:一名教练不能被指派到同一时间进行的多个训练课。

#### 订阅模型

系统为健身房老板(Admin)实现了一个分级订阅模型,用来控制其运营规模。
这些层级引入了领域逻辑必须强制执行的具体限制:

| 层级 | 月费 | 健身房上限 | 房间上限 | 每日训练课上限 |
| :--- | :--- | :--- | :--- | :--- |
| **Free** | $0 | 1 | 1 | 4 |
| **Starter** | $20 | 1 | 3 | 不限 |
| **Pro** | $25 | 3 | 不限 | 不限 |

在 **Pro** 层级下,超出最初三个之外的健身房可以按增量费用添加(例如每个健身房每月 $10)。
如果管理员试图超出其当前层级的限制(例如在 Free 套餐上安排当天第五个训练课),系统必须阻止该操作并提示升级。

#### 通用语言

有效的领域驱动设计需要一套清晰、共享的词汇。
诸如 **Participant**、**Admin**、**Gym**、**Room** 和 **Session** 这样的术语在 Dome Gym 上下文中有特定的含义。
建立这套通用语言,是为领域逻辑建模、并确保软件准确反映业务需求的前提。

## 2. The domain’s Ubiquitous Language

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-domains-ubiquitous-language-54123324/) · 4:30

### 总结

通用语言(Ubiquitous Language)是领域驱动设计的一根基础支柱,它由开发者和领域专家在特定限界上下文内共享的一套通用、严谨的语言构成。
通过建立一致的术语集合,团队确保软件的实现、文档和口头沟通与业务领域完全对齐,从而减少歧义和翻译错误。

### 核心概念

- **Shared Language(共享语言)**:团队中每个人都使用的通用词汇,包括开发者、产品经理和领域专家(例如会计师或顾问)。
- **Bounded Context(限界上下文)**:通用语言特定于单个限界上下文;同一个术语在更大系统的不同部分可能有不同含义。
- **Living Documentation(活文档)**:这套语言应当记录在一份共享文档中,例如代码库里的一个 markdown 文件,并随着对领域理解的演进而更新。
- **Consistency(一致性)**:在对话、需求以及代码本身中使用相同的术语,从而消除翻译的需要。

### 课程笔记

通用语言是这样一种实践:构建一套由开发团队所有成员共同使用的语言,以确保关于领域的沟通清晰而一致。
这套语言不只是给开发者用的;它必须与领域专家共享,也就是那些对业务领域拥有深入知识的人,例如产品经理或专业顾问。

#### 上下文边界

通用语言的一个关键方面是它的范围。
它存在于一个特定的 **Bounded Context(限界上下文)** 之内。
由于大型系统中经常存在重叠的术语,一个词在一个上下文里可能是一个意思,在另一个上下文里则可能是完全不同的意思。
通过在特定边界内定义这套语言,团队避免了术语被重载所带来的混乱。

#### 实现与文档

通用语言应当被视为事实来源。
它可以维护成一页纸的摘要、仓库中一个持续更新的 Markdown 文件,或者仅仅是一套共享的语言。
目标是确保当有人询问某个具体动作时(例如用户如何与训练课交互),答案能在已定义的语言中找到(例如"他们预留一个名额")。

#### 示例:健身房管理领域

以下定义为健身房管理系统建立了通用语言:

##### 参与者与训练课流程

- 一个 **User** 可以创建一个 **Participant Profile**。
- 一个 **Participant** 可以在一个 **Session** 中 **Reserve**(预留)一个名额。
- 一个 **Session** 在一个 **Room** 中举行。
- 一个 **Session** 由单独一名 **Trainer** 带领,并有一个规定的 **Participants** 最大人数。
- 一个 **Gym** 可以包含多个 **Rooms**。

##### 管理与订阅流程

- 一个 **User** 可以创建一个 **Admin Profile**。
- 一个 **Admin** 可以拥有一个 **Active Subscription**(当前正在使用的订阅)。
- 一个 **Subscription** 可以管理多个 **Gyms**(例如,一个 "Pro" 订阅可能最多允许三个健身房)。
- **Active Subscriptions** 按类型分为:**Free**、**Starter** 或 **Pro**。

##### 教练流程

- 一个 **User** 可以创建一个 **Trainer Profile**。
- 一名 **Trainer** 可以跨不同的 **Gyms** 和 **Subscriptions** 教授 **Sessions**。
- 系统必须确保 **Trainer** 的日程在不同房间、健身房或订阅之间不发生冲突。

通过显式定义这些术语和关系,领域逻辑变得更加直观,并为将要在领域层中实现的对象和行为提供了一张清晰的路线图。

## 3. Ubiquitous Language as a tool for detecting Domain Objects

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/ubiquitous-language-as-a-tool-for-detecting-domain-objects-54123325/) · 3:41

### 总结

本课演示如何分析通用语言,以识别领域对象、它们之间的关系,以及定义系统行为的具体动词。
通过保持术语一致(例如在资料管理上选择 'create' 而不是 'open'),开发者确保实现准确反映业务领域,并成为代码库的唯一事实来源。

### 核心概念

- **Noun Extraction(名词提取)**:从通用语言中识别出 User、Gym、Session 和 Subscription 这样的领域对象。
- **Verb Mapping(动词映射)**:使用领域特定的动词(例如 'Reserve'、'Teach'、'Create')为系统方法命名。
- **Terminology Consistency(术语一致性)**:解决语言上的不一致(例如 'create' 与 'open'),以确保代码与业务语言一致。
- **Domain State(领域状态)**:识别业务逻辑所需的特定状态,例如 'Active' 的 Subscription。
- **Structural Constraints(结构约束)**:定义诸如"一个 Session 有一名 Trainer 和一个 Participants 最大人数"这样的关系。

### 课程笔记

定义通用语言的过程是识别软件系统中将会存在的领域对象和行为的首要工具。
通过标出领域描述中的特定名词和动词,开发者可以提取出类、属性和方法的名称。

#### 识别领域对象和方法

在健身房管理系统的上下文中,通用语言识别出若干关键实体:
- **User**:可以承担不同角色的基础参与者。
- **Profiles**:具体的角色,包括 **Participant**、**Admin** 和 **Trainer**。
- **Gym**:包含多个房间的实体场所。
- **Room**:健身房内举行训练课的空间。
- **Session**:一个已排期的活动,有指定的教练和参与者最大容量。
- **Subscription**:与 Admin 关联的实体,具有特定的状态(例如 **Active**)和类型(例如 **Free**、**Starter** 和 **Pro**)。

#### 强制术语一致性

这项分析的一个关键方面,是在实现开始之前解决不一致之处。
例如,如果这套语言描述用户"创建"参与者资料,却"开通"管理员资料,团队就必须决定使用单一的术语。
选择 "Create" 可以确保最终代码使用 `CreateAdminProfile`,而不是 `OpenAdminProfile` 或 `AddAdminProfile`。
这种一致性让通用语言成为代码库的唯一事实来源。

#### 定义领域动作

通用语言中的动词直接映射到领域逻辑和方法名:
- **Reserving a spot(预留名额)**:当参与者加入一个训练课时,这个动作被定义为训练课内部的 `ReserveSpot`。
- **Teaching a session(教授训练课)**:当教练执行一个训练课时,领域术语是 `TeachSession`,而不是"开"或"带"一个训练课。

#### 结构关系与不变量

这套语言还定义了领域的结构约束和关系:
- 一个 **Session** 在一个 **Room** 内举行。
- 一个 **Session** 由单独一名 **Trainer** 带领,并有一个规定的 **Participants** 最大人数。
- 一个 **Gym** 可以包含多个 **Rooms**。
- 一个 **Admin** 管理一个 **Active Subscription**,该订阅可能涵盖多个 **Gyms**。

```text
User
 +-- Participant Profile --> Participant --ReserveSpot--> Session
 +-- Trainer Profile ------> Trainer ----TeachSession---> Session
 +-- Admin Profile --------> Admin --> Active Subscription
                                           |
                                           v
                                     Gym (multiple)
                                           |
                                           v
                                     Room (multiple)
                                           |
                                           v
                                        Session
```

## 4. The domain’s invariants

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-domains-invariants-54123326/) · 3:00

### 总结

在领域驱动设计中,不变量是为确保领域模型完整性而必须始终为真的基本业务规则和逻辑约束。
这些规则定义了实体和聚合的有效状态,例如强制执行训练课的容量限制、防止教练和参与者的排期冲突,以及确保资源的创建遵守基于订阅的限制。

### 核心概念

- **Definition of Invariants(不变量的定义)**:代表系统内必须始终被满足的条件的业务规则。
- **Capacity Constraints(容量约束)**:对一个训练课允许的参与者数量的限制。
- **Temporal Constraints(时间约束)**:防止房间、教练和参与者出现训练课重叠的规则。
- **Resource Limits(资源限制)**:基于订阅、对允许的健身房和房间数量的限制。
- **Ubiquitous Language Alignment(与通用语言对齐)**:在代码实现中直接使用业务规则中的术语(例如 "overlapping"),以保持领域的清晰性。

### 课程笔记

不变量是一个领域的基本业务规则,也就是确保系统在任何时候都保持一致且有效状态的逻辑。
在健身房管理系统中,这些不变量定义了实体如何交互以及资源如何分配的边界。

#### Session 不变量

训练课受容量和排期相关规则的约束:
- **Participant Limits(参与者上限)**:一个训练课不能超过其规定的最大参与者人数。
  每个训练课都有固定数量的名额,一旦达到该上限,系统必须阻止更多的报名。
- **Cancellation Rules(取消规则)**:为了维持业务的可持续性,如果请求发生在训练课开始时间之前不足 24 小时,预约就不能免费取消。

#### Gym 与 Room 不变量

不变量还根据订阅级别管理资源的层级结构:
- **Gym Capacity(健身房容量)**:订阅层级限制了该订阅下可以创建的健身房总数。
- **Room Capacity(房间容量)**:类似地,订阅层级限制了单个健身房内允许的房间数量。
- **Room Scheduling(房间排期)**:房间是一种实体资源,不能同时承办多个训练课。
  因此,一个房间不能有两个或更多重叠的训练课。

#### Subscription 不变量

订阅实体对更宏观的系统结构施加限制,具体来说是确保与一个订阅关联的健身房数量不超过允许的上限。

#### Trainer 与 Participant 不变量

个人日程必须没有冲突,以确保系统反映物理现实:
- **Trainer Availability(教练可用性)**:教练被限制不能教授重叠的训练课,因为他们不能同时身处两地。
- **Participant Availability(参与者可用性)**:参与者被限制不能在时间重叠的训练课中预留名额。

#### 实现与通用语言

定义不变量是识别和打磨项目通用语言的关键一步。
例如,"overlapping" 这个术语描述了一种特定的业务冲突。
在实现强制执行这些不变量的逻辑时,代码应当使用相同的术语。
通过让代码的方法名和逻辑与定义业务规则所用的语言保持一致,实现始终扎根于领域,使软件更直观、也更易于维护。

## 5. Enforcing invariants

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/enforcing-invariants-54123327/) · 2:44

### 总结

强制执行不变量是这样一种实践:通过把业务逻辑封装在领域模型自身之中,确保领域模型保持有效状态。
领域驱动设计(DDD)主张不依赖外部编排来验证规则,而是把这部分逻辑直接放进领域对象内部,也就是同时包含属性和行为的 C# 类。
这种做法确保对象不会转变为无效状态,例如健身房超出允许的房间数量,或者排出重叠的训练课。

### 核心概念

* **Domain Model(领域模型)**:代表一个领域实体的 C# 类,它同时封装了状态(属性)和逻辑(行为)。
* **Invariants(不变量)**:领域模型要保持有效就必须始终被满足的业务规则和约束。
* **Encapsulation(封装)**:把业务逻辑放在领域模型内部以保护其完整性的 DDD 原则。
* **Internal vs. External Enforcement(内部与外部强制)**:在外部编排器中验证规则,还是在领域模型自身中验证规则,二者之间的选择;DDD 倾向于后者。

### 课程笔记

在领域驱动设计中,领域模型不只是一个简单的数据容器;它是一个 C# 对象(通常是 Plain Old C# Object,即 POCO),代表业务领域内的一个特定概念,并且同时包含属性和行为。
强制执行不变量的首要目标,是确保这些模型按照业务需求始终处于有效状态。

#### 定义不变量

不变量是业务逻辑的基本规则。
例如,在一个预约系统中,一个关键的不变量可能是两个训练课不能被安排在重叠的时间。
如果系统允许这种重叠,领域模型就进入了无效状态,违反了业务规则。

#### 强制执行的策略

在设计系统时,有两种主要方式来强制执行这些规则:
1. **External Enforcement(外部强制)**:业务逻辑放在一个外层组件或服务中,由它来编排领域对象如何交互。
   在这种情形下,领域模型通常是"贫血的",意思是它们持有数据,却不控制自身的有效性。
2. **Internal Enforcement(内部强制)**:正如 DDD 所鼓励的那样,业务逻辑被移到领域模型内部。
   模型本身负责强制执行它的不变量。

#### 封装的实践

通过把逻辑封装在模型内部,对象保护了自身的完整性。
例如,如果一个健身房的订阅限制了它可以运营的房间数量,那么 Gym 领域模型就应当负责创建新房间的逻辑。
如果有人试图添加一个超出订阅上限的房间,Gym 模型就应当阻止这个操作。
这确保了不可能创建出一个违反其业务约束的 Gym 对象。

#### DDD 与测试驱动开发(TDD)

为领域建模并强制执行不变量,通常与测试驱动开发结合进行。
通过编写定义领域模型预期行为和约束的测试,开发者可以迭代地完善通用语言,并确保所有不变量都在代码中被正确地实现和强制执行。

## 6. Creating the domain objects

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/creating-the-domain-objects-54123328/) · 5:20

### 总结

本课讲解如何基于前面定义的通用语言和业务不变量,初步实现领域对象。
它演示了如何组织一个领域驱动设计(DDD)项目:为 Subscriptions、Gyms 和 Admins 这样的实体创建类,并使用私有字段和基于 ID 的引用来建模关系。
这种做法强调严格的封装和最小化的实现,把复杂性推迟到单元测试和业务逻辑真正需要它的时候。

### 核心概念

- **Project Structure(项目结构)**:把解决方案组织成一个承载核心逻辑的 Domain 项目和一个用于验证的 Tests 项目。
- **Encapsulation Strategy(封装策略)**:所有成员默认使用 `private readonly` 字段,以确保数据只在绝对必要时才被暴露。
- **ID-Based References(基于 ID 的引用)**:使用 `Guid` 标识符而不是对其他对象的直接内存引用,来建模领域对象之间的关系。
- **Ubiquitous Language Implementation(通用语言的落地)**:把业务术语直接映射为类名和内部字段,以保持与领域模型的一致。
- **Test-Driven Evolution(测试驱动的演进)**:从最少的代码开始,随着业务不变量的单元测试被实现而逐步扩展。

### 课程笔记

项目从一个标准结构开始,由两个项目组成:一个用于领域定义,另一个用于 XUnit 单元测试。

```plaintext
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "DomeGym.Domain", "DomeGym.Domain\DomeGym.Domain.csproj", "{723FA048-72FF-4F48-B701-22D3}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "DomeGym.Domain.Tests", "DomeGym.Domain.Tests\DomeGym.Domain.Tests.csproj", "{...}"
EndProject
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/creating-the-domain-objects-54123328/?t=10)

实现从为每个在通用语言和不变量阶段识别出的领域对象创建类开始。
最初的一组包括 `Subscription`、`Gym`、`Admin`、`Room`、`Session`、`Trainer` 和 `Participant`。

在定义这些对象时,首要目标是使用 ID 而不是对内存中其他对象的强引用来建立关系。
对于 `Admin` 类,我们包含创建该资料的用户的 `Guid`,以及其所管理订阅的 ID。
成员默认保持 `private readonly`,以维持严格的封装。

```csharp
namespace DomeGym.Domain;

public class Admin
{    
    private readonly Guid _userId;
    private readonly Guid _subscriptionId;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/creating-the-domain-objects-54123328/?t=85)

`Subscription` 对象包含它自己的唯一标识符,以及一个代表与之关联的健身房的 ID 集合。

```csharp
namespace DomeGym.Domain;

public class Subscription
{    
    private readonly Guid _id;
    private readonly List<Guid> _gymIds;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/creating-the-domain-objects-54123328/?t=130)

按照相同的模式,`Gym` 类持有它的 ID 和一个 `Room` ID 列表。

```csharp
namespace DomeGym.Domain;

public class Gym
{    
    private readonly Guid _id;
    private readonly List<Guid> _roomIds;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/creating-the-domain-objects-54123328/?t=145)

每个 `Room` 由一个 ID 标识,并维护一个 `Session` ID 列表。

```csharp
namespace DomeGym.Domain;

public class Room
{    
    private readonly Guid _id;
    private readonly List<Guid> _sessionIds;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/creating-the-domain-objects-54123328/?t=165)

`Session` 对象记录它自己的 ID、所指派 `Trainer` 的 ID,以及一个 `Participant` ID 列表。

```csharp
namespace DomeGym.Domain;

public class Session
{    
    private readonly Guid _id;
    private readonly Guid _trainerId;
    private readonly List<Guid> _participantIds;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/creating-the-domain-objects-54123328/?t=180)

`Trainer` 资料包括教练的 ID、创建该资料的用户的 ID,以及一个已计划的 `Session` ID 列表。

```csharp
namespace DomeGym.Domain;

public class Trainer
{    
    private readonly Guid _id;
    private readonly Guid _userId;
    private readonly List<Guid> _sessionIds;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/creating-the-domain-objects-54123328/?t=220)

最后,`Participant` 类包括它的 ID、关联的用户 ID,以及其参加的训练课列表。

```csharp
namespace DomeGym.Domain;

public class Participant
{    
    private readonly Guid _id;
    private readonly Guid _userId;
    private readonly List<Guid> _sessionIds;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/creating-the-domain-objects-54123328/?t=245)

```text
Admin                Subscription         Gym                Room
  _userId              _id                  _id                _id
  _subscriptionId -->  _gymIds[] ------->   _roomIds[] ----->  _sessionIds[]
                                                                   |
                                                                   v
Trainer              Participant          Session
  _id                  _id                  _id
  _userId              _userId              _trainerId ------> Trainer
  _sessionIds[]        _sessionIds[]        _participantIds[] -> Participant
```

这个最小化的实现为强制执行业务不变量奠定了基础。
下一步是把前面识别出的不变量转化为单元测试,由它们自然而然地推动领域逻辑以及这些模型之间交互的开发。

## 7. Invariants as Unit Tests

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/invariants-as-unit-tests-54123329/) · 10:41

本课聚焦于通过编写定义领域对象预期行为的单元测试来实现领域不变量。
领域不变量是对象要处于有效状态就必须始终保持为真的规则。
所用的主要示例是这样一条限制:一个训练课包含的参与者不能超过其允许的最大人数。

### 总结

本课演示如何用测试驱动的方式实现领域不变量。
通过为 Session 实体定义规则(具体来说,它不能超过其最大参与者容量),本课展示了如何组织单元测试、创建工厂和常量这样的支撑性基础设施,以及如何把逻辑封装在领域模型中以确保它保持有效状态。

### 核心概念

- **Domain Invariants(领域不变量)**:领域对象要被视为有效就必须在任何时候都被满足的业务规则。
- **Test-Driven Development (TDD) for Invariants(为不变量做测试驱动开发)**:在实现逻辑之前,先编写会失败的测试来定义领域约束。
- **Test Factories(测试工厂)**:像 `SessionFactory` 和 `ParticipantFactory` 这样的工具类,它们简化测试中的对象创建并提供合理的默认值。
- **Test Constants(测试常量)**:在整个测试套件中使用的集中化的值和 ID,用来保持一致性并减少样板代码。
- **Ubiquitous Language(通用语言)**:在代码中使用 `ReserveSpot` 这样的领域特定术语,以与业务领域保持一致。
- **Encapsulation(封装)**:通过把字段设为私有,并且只允许通过强制执行不变量的方法来修改,从而保护领域对象的内部状态。

### 课程笔记

实现领域不变量从识别每个领域对象的具体规则开始。
这些规则被记录为系统必须强制执行的不变量。

```markdown
# Invariants

## Session Invariants

- A session cannot contain more than the maximum number of participants
- A reservation cannot be canceled for free less than 24 hours before the session starts

## Gym Invariants

- A gym cannot have more rooms than the subscription allows

## Room Invariants

- A room cannot have more sessions than the subscription allows
- A room cannot have two or more overlapping sessions

## Subscription Invariants

- A subscription cannot have more gyms than the subscription allows
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/invariants-as-unit-tests-54123329/?t=10)

为了实现关于训练课容量的不变量,创建了一个单元测试,用来验证在没有更多名额时预约会失败。
该测试遵循 Arrange-Act-Assert 模式。

```csharp
namespace DomeGym.Domain.UnitTests;

public class SessionTests
{
    [Fact]
    public void ReserveSpot_WhenNoMoreRoom_ShouldFailReservation()
    {
        // Arrange
        var session = SessionFactory.CreateSession(maxParticipants: 1);
        var participant1 = ParticipantFactory.CreateParticipant(id: Guid.NewGuid());
        var participant2 = ParticipantFactory.CreateParticipant(id: Guid.NewGuid());

        // Act
        session.ReserveSpot(participant1);
        session.ReserveSpot(participant2);

        // Assert
        // participant 2 reservation failed
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/invariants-as-unit-tests-54123329/?t=125)

#### 测试的基础设施

为了支撑整洁的单元测试,建立了一个包含工厂和常量的 `TestUtils` 文件夹。
`TestConstants` 为教练和用户这样的实体提供稳定的 ID。

```csharp
namespace DomeGym.Domain.UnitTests.TestConstants;

public static partial class Constants
{
    public static class Trainer
    {
        public static readonly Guid Id = Guid.NewGuid();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/invariants-as-unit-tests-54123329/?t=300)

像 `SessionFactory` 这样的工厂使用这些常量来创建处于有效默认状态的领域对象,让测试只需关注与所测不变量相关的参数。

```csharp
namespace DomeGym.Domain.UnitTests.TestUtils.Sessions;

public static class SessionFactory
{
    public static Session CreateSession(
        int maxParticipants,
        Guid? id = null)
    {
        return new Session(
            maxParticipants,
            trainerId: Constants.Trainer.Id,
            id: id ?? Constants.Session.Id);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/invariants-as-unit-tests-54123329/?t=370)

类似地,使用一个 `ParticipantFactory` 来创建参与者,确保它们始终关联一个有效的 `UserId`。

```csharp
public static class ParticipantFactory
{
    public static Participant CreateParticipant(Guid? id = null, Guid? userId = null)
    {
        return new Participant(
            userId: userId ?? Constants.User.Id,
            id: id ?? Constants.Participant.Id);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/invariants-as-unit-tests-54123329/?t=430)

#### 领域模型的实现

`Session` 和 `Participant` 实体被设计为始终有效。
这是通过使用构造函数初始化所有必需字段,并把这些字段保持为私有以防止外部篡改来实现的。

```csharp
namespace DomeGym.Domain;

public class Participant
{
    private readonly Guid _id;
    private readonly Guid _userId;
    private readonly List<Guid> _sessionIds = new();

    public Participant(
        Guid userId,
        Guid? id = null)
    {
        _userId = userId;
        _id = id ?? Guid.NewGuid();
    }

    public Guid Id => _id;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/invariants-as-unit-tests-54123329/?t=445)

在 `Session` 实体中,构造函数确保提供了 `trainerId`,并在未指定 `id` 时生成一个。
`ReserveSpot` 方法通过把当前参与者数量与 `_maxParticipants` 上限进行比较来实现不变量逻辑。

```csharp
public class Session
{
    private readonly Guid _id;
    private readonly Guid _trainerId;
    private readonly List<Guid> _participantIds = new();
    private readonly int _maxParticipants;

    public Session(
        int maxParticipants,
        Guid trainerId,
        Guid? id = null)
    {
        _maxParticipants = maxParticipants;
        _trainerId = trainerId;
        _id = id ?? Guid.NewGuid();
    }

    public void ReserveSpot(Participant participant)
    {
        if (_participantIds.Count >= _maxParticipants)
        {
            throw new Exception("Cannot have more reservations than participants");
        }

        _participantIds.Add(participant.Id);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/invariants-as-unit-tests-54123329/?t=600)

#### 完成测试

最终的测试使用 `FluentAssertions` 捕获预留名额的动作,并验证当训练课已满员时它会抛出预期的异常。

```csharp
[Fact]
public void ReserveSpot_WhenNoMoreRoom_ShouldFailReservation()
{
    // Arrange
    var session = SessionFactory.CreateSession(maxParticipants: 1);
    var participant1 = ParticipantFactory.CreateParticipant(id: Guid.NewGuid());
    var participant2 = ParticipantFactory.CreateParticipant(id: Guid.NewGuid());

    // Act
    session.ReserveSpot(participant1);
    var action = () => session.ReserveSpot(participant2);

    // Assert
    action.Should().ThrowExactly<Exception>();
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/invariants-as-unit-tests-54123329/?t=640)

## 8. Enforcing Invariants that rely on dependencies

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/enforcing-invariants-that-rely-on-dependencies-54123330/) · 12:22

### 总结

本课探讨如何强制执行依赖外部状态(具体来说是当前时间)的领域不变量。
借助"距离训练课开始不足 24 小时不能取消预约"这条业务规则,本课演示了为什么把 IDateTimeProvider 这样的依赖直接传入领域方法,往往是保持封装性和可测试性的最佳做法。
本课还讲解了如何实现一个测试专用的时间提供者,以便在单元测试中验证这些与时间相关的不变量。

### 核心概念

- **Ubiquitous Language integration(融入通用语言)**:为领域方法命名(例如 `CancelReservation`),使其与领域上下文中定义的业务规则和词汇一致。
- **Dependency Injection in Domain Methods(领域方法中的依赖注入)**:把 `IDateTimeProvider` 这样的依赖直接传入实体方法而不是构造函数,以保持实体生命周期的完整性。
- **IDateTimeProvider Interface(IDateTimeProvider 接口)**:使用对时间的抽象,确保领域逻辑保持确定性和可测试性。
- **Mocking Time(模拟时间)**:实现一个 `TestDateTimeProvider` 来模拟特定的时间点,以验证基于时间的不变量。
- **Exception Limitations(异常的局限)**:认识到通用异常使得在测试中难以区分不同的不变量违规。

### 课程笔记

本课聚焦于实现以下不变量:"距离训练课开始不足 24 小时时,预约不能免费取消。"
这条规则是通用语言的核心部分,它把 "Cancel Reservation" 确立为领域词汇中的一个正式术语。

为了支持测试这个不变量,`SessionFactory` 被更新为允许创建带有特定日期和时间的训练课,确保测试可以精确控制训练课的日程。

```csharp
public static Session CreateSession(
    DateOnly? date = null,
    TimeOnly? startTime = null,
    TimeOnly? endTime = null,
    int maxParticipants = Constants.Session.MaxParticipants,
    Guid? id = null)
{
    return new Session(
        date ?? Constants.Session.Date,
        startTime ?? Constants.Session.StartTime,
        endTime ?? Constants.Session.EndTime,
        maxParticipants,
        trainerId: Constants.Trainer.Id,
        id: id ?? Constants.Session.Id);
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/enforcing-invariants-that-rely-on-dependencies-54123330/?t=115)

当一个领域不变量依赖于当前时间时,有三种主要的实现策略:

1. **DateTime as a parameter(把 DateTime 作为参数)**:方法接收一个 `DateTime` 值。
   这很简单,但完全依赖调用方提供正确的 UTC 时间。
2. **IDateTimeProvider as a parameter(把 IDateTimeProvider 作为参数)**:方法接收一个接口。
   这强制领域逻辑使用一个特定的提供者,而该提供者在测试时可以很容易地被模拟。
3. **Domain Service(领域服务)**:由一个独立的服务来编排这个动作、获取时间并与领域对象交互。
   这对复杂的编排很有用,但可能会把逻辑从实体中拉出去。

首选的做法是选项 2,因为它让领域对象能够强制执行自己的不变量,同时保持可测试性。
首先,在领域层中定义一个 `IDateTimeProvider` 接口:

```csharp
namespace DomeGym.Domain;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/enforcing-invariants-that-rely-on-dependencies-54123330/?t=385)

`Session` 实体通过检查训练课开始时间与 `IDateTimeProvider` 提供的当前时间之间的时间差来实现取消逻辑。
如果时间差小于 24 小时,操作就会失败。

```csharp
public void CancelReservation(Participant participant, IDateTimeProvider dateTimeProvider)
{
    if (IsTooCloseToSession(dateTimeProvider.UtcNow))
    {
        throw new Exception("Cannot cancel reservation too close to session");
    }

    if (!_participantIds.Remove(participant.Id))
    {
        throw new Exception("Reservation not found");
    }
}

private bool IsTooCloseToSession(DateTime utcNow)
{
    const int MinHours = 24;
    return (_date.ToDateTime(_startTime) - utcNow).TotalHours < MinHours;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/enforcing-invariants-that-rely-on-dependencies-54123330/?t=490)

为了测试这段逻辑,在测试工具中实现了一个 `TestDateTimeProvider`。
这个提供者允许测试把"当前"时间固定为一个特定的值,确保无论测试何时执行,结果都是确定的。

```csharp
namespace DomeGym.Domain.UnitTests.TestUtils.Services;

public class TestDateTimeProvider : IDateTimeProvider
{
    private readonly DateTime? _fixedDateTime;

    public TestDateTimeProvider(DateTime? fixedDateTime = null)
    {
        _fixedDateTime = fixedDateTime;
    }

    public DateTime UtcNow => _fixedDateTime ?? DateTime.UtcNow;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/enforcing-invariants-that-rely-on-dependencies-54123330/?t=535)

```text
Session.CancelReservation(participant, dateTimeProvider)
        |
        v
dateTimeProvider.UtcNow  <-- IDateTimeProvider
        |                      ^
        |                      +-- TestDateTimeProvider (_fixedDateTime)
        v
IsTooCloseToSession(utcNow)
  (_date.ToDateTime(_startTime) - utcNow).TotalHours < 24 ?
        |
   yes -+-> throw "Cannot cancel reservation too close to session"
   no  -+-> _participantIds.Remove(participant.Id)
                false -> throw "Reservation not found"
```

最后,单元测试验证在 24 小时窗口内尝试取消预约会抛出异常。
通过传入一个与训练课开始时间相同的 `cancellationDateTime`,该测试确认这个不变量被正确地强制执行了。

```csharp
[Fact]
public void CancelReservation_WhenCancellationIsTooCloseToSession_ShouldFailCancellation()
{
    // Arrange
    var session = SessionFactory.CreateSession(
        date: Constants.Session.Date,
        startTime: Constants.Session.StartTime,
        endTime: Constants.Session.EndTime);

    var participant = ParticipantFactory.CreateParticipant();
    session.ReserveSpot(participant);

    var cancellationDateTime = Constants.Session.Date.ToDateTime(Constants.Session.StartTime);

    // Act
    var action = () => session.CancelReservation(
        participant,
        new TestDateTimeProvider(fixedDateTime: cancellationDateTime));

    // Assert
    action.Should().ThrowExactly<Exception>();
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/enforcing-invariants-that-rely-on-dependencies-54123330/?t=700)

虽然这个实现正确地强制执行了不变量,但使用通用异常使得难以区分不同类型的失败(例如时间违规与预约不存在)。
这一局限促使我们引入 Result 模式,以便在领域层中进行更健壮的错误处理。

## 9. The Result Pattern

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-result-pattern-54123331/) · 10:08

Result 模式是这样一种设计方法:方法返回一个对象,该对象要么代表一个成功的值,要么代表一个或多个错误,而不是为预期内的领域逻辑失败抛出异常。
通过使用 ErrorOr 库,领域模型对其不变量的表达变得更加明确,错误处理也被集中到静态错误类中。
这个模式让使用方能够通过一种类似可区分联合的结构来检查结果状态和具体的错误码,从而提升可测试性和代码清晰度。

### 核心概念

* Discriminated Union for results(用于结果的可区分联合):一个处于两种状态之一的对象:Success 或 Failure。
* ErrorOr<T> Library(ErrorOr<T> 库):一个用于在 C# 中实现 Result 模式的 NuGet 包。
* Centralized Error Definitions(集中化的错误定义):使用像 SessionErrors 这样的静态类来存放可复用的领域错误。
* Explicit Invariant Validation(显式的不变量验证):返回具体的错误码,而不是通用异常或魔法字符串。
* Fluent Assertions:通过检查 IsError 并比较错误实例来测试结果对象。

### 课程笔记

Result 模式提供了一种函数式的错误处理方式:返回一个封装了值或错误的对象。
这避免了为业务逻辑违规抛出异常所带来的开销和控制流问题。

最初,像 ReserveSpot 这样的领域方法可能会在不变量(例如参与者上限)被违反时抛出异常:

```csharp
private bool IsTooCloseToSession(DateTime utcNow)
{
    const int MinHours = 24;

    return (_date.ToDateTime(_startTime) - utcNow).TotalHours < MinHours;
}

public void ReserveSpot(Participant participant)
{
    if (_participantIds.Count >= _maxParticipants)
    {
        throw new Exception("Cannot have more reservations than participants");
    }

    _participantIds.Add(participant.Id);
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-result-pattern-54123331/?t=25)

为了用 Result 模式重构这段代码,返回类型被改为 ErrorOr<Success>。
成功时,方法返回 Result.Success。
失败时,它返回一个 Error 对象。
使用 ErrorOr 库,你可以返回像 Error.Validation 这样的具体错误类型:

```csharp
public ErrorOr<Success> ReserveSpot(Participant participant)
{
    if (_participantIds.Count >= _maxParticipants)
    {
        return Error.Validation(description: "Cannot have more reservations than participants");
    }

    _participantIds.Add(participant.Id);

    return Result.Success;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-result-pattern-54123331/?t=155)

为了避免重复字符串,并让错误在整个系统中可被识别,错误应当包含唯一的错误码。
这使得在测试和更高层中能够进行精确的验证:

```csharp
public ErrorOr<Success> ReserveSpot(Participant participant)
{
    if (_participantIds.Count >= _maxParticipants)
    {
        return Error.Validation(
            code: "Session.CannotHaveMoreReservationsThanParticipants",
            description: "Cannot have more reservations than participants");
    }

    _participantIds.Add(participant.Id);

    return Result.Success;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-result-pattern-54123331/?t=265)

领域驱动设计中的一个最佳实践,是把这些错误集中到领域层内的一个静态类中。
这为管理所有领域不变量及其相关的错误消息提供了单一的位置:

```csharp
using ErrorOr;

namespace DomeGym.Domain;

public static class SessionErrors
{
    public readonly static Error CannotHaveMoreReservationsThanParticipants = Error.Validation(
        code: "Session.CannotHaveMoreReservationsThanParticipants",
        description: "Cannot have more reservations than participants");

    public readonly static Error CannotCancelReservationTooCloseToSession = Error.Validation(
        code: "Session.CannotCancelReservationTooCloseToSession",
        description: "Cannot cancel reservation too close to session start time");
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-result-pattern-54123331/?t=340)

随后领域逻辑被更新为返回这些预先定义的静态错误对象:

```csharp
public ErrorOr<Success> ReserveSpot(Participant participant)
{
    if (_participantIds.Count >= _maxParticipants)
    {
        return SessionErrors.CannotHaveMoreReservationsThanParticipants;
    }

    _participantIds.Add(participant.Id);

    return Result.Success;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-result-pattern-54123331/?t=355)

单元测试必须更新以适应返回类型的变化。
测试不再断言某个动作会抛出异常,而是验证 IsError 属性,并检查返回的错误是否与预期的领域错误一致:

```csharp
[Fact]
public void ReserveSpot_WhenNoMoreRoom_ShouldFailReservation()
{
    // Arrange
    var session = SessionFactory.CreateSession(maxParticipants: 1);
    var participant1 = ParticipantFactory.CreateParticipant(id: Guid.NewGuid());
    var participant2 = ParticipantFactory.CreateParticipant(id: Guid.NewGuid());

    // Act
    var reserveParticipant1Result = session.ReserveSpot(participant1);
    var reserveParticipant2Result = session.ReserveSpot(participant2);

    // Assert
    reserveParticipant1Result.IsError.Should().BeFalse();

    reserveParticipant2Result.IsError.Should().BeTrue();
    reserveParticipant2Result.FirstError.Should().Be(SessionErrors.CannotHaveMoreReservationsThanParticipants);
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-result-pattern-54123331/?t=400)

这个模式同样被应用到 CancelReservation 方法上。
如果在训练课的列表中找不到该参与者,这个方法返回 Error.NotFound;如果违反了基于时间的不变量,则返回集中定义的 CannotCancelReservationTooCloseToSession 错误:

```csharp
public ErrorOr<Success> CancelReservation(Participant participant, IDateTimeProvider dateTimeProvider)
{
    if (IsTooCloseToSession(dateTimeProvider.UtcNow))
    {
        return SessionErrors.CannotCancelReservationTooCloseToSession;
    }

    if (!_participantIds.Remove(participant.Id))
    {
        return Error.NotFound(description: "Participant not found");
    }

    return Result.Success;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-result-pattern-54123331/?t=505)

最后,取消预约的单元测试确保每一步都得到验证,包括最初的预约和随后失败的取消:

```csharp
[Fact]
public void CancelReservation_WhenCancellationIsTooCloseToSession_ShouldFailCancellation()
{
    // Arrange
    var session = SessionFactory.CreateSession(
        date: Constants.Session.Date,
        startTime: Constants.Session.StartTime,
        endTime: Constants.Session.EndTime);

    var participant = ParticipantFactory.CreateParticipant();
    var cancellationDateTime = Constants.Session.Date.ToDateTime(TimeOnly.MinValue);

    // Act
    var reserveSpotResult = session.ReserveSpot(participant);
    var cancelReservationResult = session.CancelReservation(
        participant,
        new TestDateTimeProvider(fixedDateTime: cancellationDateTime));

    // Assert
    reserveSpotResult.IsError.Should().BeFalse();

    cancelReservationResult.IsError.Should().BeTrue();
    cancelReservationResult.FirstError.Should().Be(SessionErrors.CannotCancelReservationTooCloseToSession);
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/the-result-pattern-54123331/?t=555)

## 10. Section Recap

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/section-recap-54123332/) · 2:10

### 总结

本课总结了领域驱动设计的基础阶段,重点是定义通用语言和识别业务不变量。
通过把业务规则(例如防止 observations 重叠)转化为单元测试,领域模型从简单的数据结构演进为承载大量逻辑、能保证系统处于有效状态的实体。
这种做法在过渡到正式的战术设计模式之前,先确立了核心业务逻辑。

### 核心概念

- **Ubiquitous Language(通用语言)**:主动定义一套特定于该领域的共享术语和语言,以确保干系人和开发者之间沟通清晰。
- **Business Invariants(业务不变量)**:系统要被视为处于有效状态就必须始终为真的关键规则。
- **Domain Validation(领域验证)**:通过强制执行诸如日程不重叠之类的规则,确保领域对象不能以无效状态存在的过程。
- **Test-Driven Domain Development(测试驱动的领域开发)**:使用单元测试逐步构建领域逻辑,从原始对象开始,随着每个不变量被实现而增加复杂度。

### 课程笔记

领域驱动设计的核心在于深入理解领域,并主动定义 **Ubiquitous Language(通用语言)**。
这套共享语言确保所有干系人使用相同的术语来描述系统的行为和实体,从而减少实现过程中的歧义。

一旦语言确立,重点就转向识别 **Business Rules(业务规则)** 和 **Invariants(不变量)**。
不变量是系统要保持有效状态就必须始终被满足的条件。
例如,在这个领域的上下文中,一个首要的不变量是确保 sessions 或 observations 不会重叠。
在编写任何实现代码之前,识别这些约束是必不可少的。

实现过程遵循这样一种模式:从最简形式的领域对象开始,逐步添加逻辑。
通过为每个识别出的不变量编写一个单元测试,领域模型被逐步构建起来。
这种做法确保对象不只是数据容器,而是负责维护自身的有效性。

最终的结构通常由两个主要部分组成:
1. **Domain Project(领域项目)**:包含定义业务规则的逻辑和实体。
2. **Unit Test Project(单元测试项目)**:验证领域对象遵守核心不变量。

虽然这个过程捕获了关键逻辑并确保系统行为正确,但领域模型起初可能缺乏正式的结构。
下一阶段的开发是应用 **Tactical Design(战术设计)** 模式,把这些逻辑组织成更易维护、更具可扩展性的架构。
