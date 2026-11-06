# Tactical Phase - Aggregate Design

> 课程:[Deep Dive: Domain-Driven Design](https://dometrain.com/course/deep-dive-domain-driven-design-ddd/) · 第 9 章
> 共 11 课 · 约 15:48
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Enforce Invariants](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/enforce-invariants-54855902/) | 0:46 | [↓](#1-enforce-invariants) |
| 2 | [Reference Aggregates By Id](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/reference-aggregates-by-id-54855903/) | 0:19 | [↓](#2-reference-aggregates-by-id) |
| 3 | [Update Aggregate via Aggregate Root](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/update-aggregate-via-aggregate-root-54855904/) | 0:14 | [↓](#3-update-aggregate-via-aggregate-root) |
| 4 | [Prefer Smaller Aggregates](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/prefer-smaller-aggregates-54855905/) | 1:44 | [↓](#4-prefer-smaller-aggregates) |
| 5 | [One Aggregate Per Transaction](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/one-aggregate-per-transaction-54855906/) | 1:59 | [↓](#5-one-aggregate-per-transaction) |
| 6 | [Use Eventual Consistency Outside the Boundary](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/use-eventual-consistency-outside-the-boundary-54855907/) | 2:56 | [↓](#6-use-eventual-consistency-outside-the-boundary) |
| 7 | [Maintain Aggregate Scope](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/maintain-aggregate-scope-54855908/) | 0:30 | [↓](#7-maintain-aggregate-scope) |
| 8 | [Aggregate Root Identity Uniqueness](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/aggregate-root-identity-uniqueness-54855909/) | 0:18 | [↓](#8-aggregate-root-identity-uniqueness) |
| 9 | [Private > Public](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/private-public-54855910/) | 0:36 | [↓](#9-private--public) |
| 10 | [Assess and Refactor](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/assess-and-refactor-54855911/) | 0:20 | [↓](#10-assess-and-refactor) |
| 11 | [Aggregate Design Steps](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/aggregate-design-steps-54855912/) | 6:06 | [↓](#11-aggregate-design-steps) |

## 1. Enforce Invariants

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/enforce-invariants-54855902/) · 0:46

### 总结

本课介绍聚合设计的基本规则,重点在于聚合根(aggregate root)的首要职责:强制执行不变量(invariant)。
它解释了聚合根必须维护其边界内所有领域对象的一致性和业务规则,以确保聚合始终处于有效状态。

### 核心概念

- **Aggregate Design Rules(聚合设计规则)**:一组用于指导聚合的创建与结构的原则。
- **Aggregate Root Responsibility(聚合根的职责)**:根实体是整个聚合的主要入口,也是整个聚合的权威。
- **Invariants(不变量)**:对于聚合内的数据必须始终成立的业务规则或一致性约束。
- **Consistency Boundary(一致性边界)**:聚合界定了强制执行不变量的范围。

### 课程笔记

聚合设计受一组规则支配,这些规则旨在确保领域模型的完整性。
第一条也是最关键的一条规则是:聚合负有强制执行不变量的职责。

更具体地说,聚合根是负责在构成聚合的所有领域对象上强制执行所有不变量的那个实体。
由于聚合根是聚合中唯一允许外部对象持有引用的成员,它充当着守门人的角色。
任何修改聚合状态的操作都必须经过根,从而让根能够校验所请求的变更没有违反任何业务规则。
这种集中式的强制执行确保聚合总是从一个有效状态转换到另一个有效状态,从而维持领域模型的一致性。

## 2. Reference Aggregates By Id

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/reference-aggregates-by-id-54855903/) · 0:19

### 总结

本课确立了这样一条规则:聚合只能通过 ID 引用其他聚合。
通过避免把一个聚合嵌套或包裹在另一个聚合之中,开发者可以维持清晰的一致性边界,并避免加载庞大、相互关联的对象图所带来的技术债。

### 核心概念

- 聚合必须只通过 ID 引用其他聚合。
- 避免把一个聚合包含或包裹在另一个聚合之中。
- 在不同聚合之间维持彼此独立的一致性边界。

### 课程笔记

在领域驱动设计中,聚合设计的一条核心原则是:聚合应当只通过其他聚合的唯一标识符(ID)来引用它们。
你不应该让一个聚合被包含在另一个聚合之中。

如果一个聚合需要知道另一个聚合,或需要与另一个聚合维持某种关系,它不应该包裹那个对象,也不应该把它作为成员包含进来。
相反,它应该存储那个聚合的 ID。
这种做法确保聚合保持解耦,并确保一致性边界被严格界定。
通过 ID 引用可以防止系统试图把多个聚合当作单个工作单元来管理,这简化了持久化逻辑,并通过避免加载不必要的对象图提升了系统性能。

## 3. Update Aggregate via Aggregate Root

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/update-aggregate-via-aggregate-root-54855904/) · 0:14

### 总结

在领域驱动设计中,聚合根是对其边界内的实体和值对象进行所有修改的唯一入口。
通过让所有变更都经由根进行,系统确保业务不变量得到一致的强制执行,并确保聚合的内部状态保持有效且被封装。

### 核心概念

- **Aggregate Root as Gateway(聚合根作为网关)**:与聚合内部成员的所有交互都必须经过根实体。
- **Encapsulation(封装)**:内部实体和值对象对外部的直接操作是隐藏的。
- **Invariant Enforcement(不变量的强制执行)**:聚合根负责在任何状态变更最终确定之前,确保所有业务规则都得到满足。
- **Consistency Boundary(一致性边界)**:聚合界定了在事务结束时数据必须保持一致的范围。

### 课程笔记

对聚合的所有修改都必须通过它的聚合根来执行。
这条原则规定,对构成聚合的领域对象的任何变更,无论它们是子实体还是值对象,都必须由根实体来居中处理。

通过把逻辑集中在聚合根内部,领域模型维持了一个清晰的边界。
外部使用者被禁止深入聚合内部直接操作它的内部状态。
这种封装让聚合根能够根据聚合的业务规则校验所有传入的请求,确保整个单元在每次操作之后都保持一致的状态。
如果需要对一个嵌套实体进行变更,聚合根会提供一个公共方法来促成这项变更,并在内部执行任何必要的编排或校验。

## 4. Prefer Smaller Aggregates

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/prefer-smaller-aggregates-54855905/) · 1:44

### 总结

以最小的体量来设计聚合是领域驱动设计的一条核心原则,因为更小的聚合能把并发冲突降到最低并优化系统性能。
通过限制聚合的范围,开发者降低了状态转换期间出现竞态条件的可能性,并减少了与数据传输、内存消耗和数据库 I/O 相关的开销。

### 核心概念

- **Reduced Concurrency Collisions(减少并发冲突)**:更小的聚合缩小了出现竞态条件的面。
- **Performance Optimization(性能优化)**:更低的延迟和更小的内存占用。
- **Resource Efficiency(资源效率)**:把通过网络传输的数据量和垃圾回收器的压力降到最低。
- **Invariant Enforcement(不变量的强制执行)**:在维持一致性的同时,不必承担庞大对象图的开销。

### 课程笔记

在领域驱动设计中,聚合的大小对系统的可靠性和性能都有显著影响。
聚合设计的一条主要规则是:更小的聚合总是比更大的聚合更可取。
这种偏好由两个主要因素驱动:并发管理和资源效率。

#### 并发与竞态条件

聚合负责强制执行业务不变量。
当一个聚合很大时,它往往会参与更多的业务流程和系统流转。
这种更多的参与提高了出现竞态条件的概率。
例如,如果一个流程试图把聚合从一个状态转换到另一个状态,而第二个流程同时试图进行另一种转换,那么第二个流程可能会失败,因为聚合的状态已被第一次变更修改了。
更小的聚合把逻辑和数据隔离开来,确保对领域某一部分的变更不会不必要地阻塞无关部分的变更,也不会与之冲突。

#### 资源效率：内存、延迟与成本

聚合的物理大小会影响应用程序的技术性能。
大型聚合需要序列化并通过网络传输更多的数据,从而导致更高的延迟和更高的基础设施成本。
此外,为了执行一次很小的更新而把一个庞大的聚合加载到内存中,会给应用程序的内存和垃圾回收器带来不必要的压力。
通过让聚合保持小巧,系统只加载一次事务所需的特定数据,从而带来更快的执行速度和更高效的资源利用。

## 5. One Aggregate Per Transaction

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/one-aggregate-per-transaction-54855906/) · 1:59

### 总结

本课探讨领域驱动设计中 "one aggregate per transaction"(每个事务一个聚合)这条基本规则。
它强调一个聚合应当封装独立做出业务决策所需的全部数据,确保状态转换能够以原子方式持久化,而不需要跨多个聚合的事务。

### 核心概念

* **Transactional Consistency(事务一致性)**:聚合界定了系统内原子更新的边界。
* **Business Decision Autonomy(业务决策自主性)**:一个聚合必须拥有校验其自身状态变更并强制执行业务规则所需的全部信息。
* **Atomic State Transitions(原子状态转换)**:把一个聚合从一个状态移动到另一个状态的过程应当在单个事务中完成。
* **Modeling Indicators(建模信号)**:需要在单个事务中更新多个聚合,往往表明聚合边界定义得不正确,应当重新考虑。

### 课程笔记

"one aggregate per transaction" 这条规则是战术领域驱动设计的基石。
它规定任何业务操作都只应在单个数据库事务中修改单个聚合实例。
这种做法确保聚合始终是一致性和封装的主要单元。

当应用程序处理一个请求时,它通常会把一个聚合从数据库取到内存中。
此时,聚合负责基于其内部状态做出业务决策。
例如,一个 `Room` 聚合可能会通过检查排期冲突或容量限制,来评估是否可以在某个特定时间添加一个新的 session。
类似地,一个 `Participant` 聚合可能会决定是否允许某个用户在一个 session 中预留一个名额。

一旦聚合处理完逻辑,并从其初始状态转换到一个新状态,这一变更就必须持久化回数据库。
这次转换应当以原子方式执行。
如果出现两个不同的聚合必须一起更新才能维持系统完整性的情况,这就是一个强烈的建模缺陷信号。
在这种情况下,应当重新评估设计,以确定这些实体是否应该属于同一个更大的聚合,由它封装共享的业务逻辑并确保原子更新。

## 6. Use Eventual Consistency Outside the Boundary

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/use-eventual-consistency-outside-the-boundary-54855907/) · 2:56

### 总结

在领域驱动设计中,聚合界定了即时的、事务性一致性的边界。
当一个业务操作需要跨多个聚合进行状态变更时,这些变更应当通过最终一致性(eventual consistency)来处理,而不是扩大事务范围。
这通常是通过引发领域事件来实现的,这些事件会触发处理程序去更新次要的聚合。
虽然这引入了出现竞态条件的可能性,但它确保了系统的可扩展性并维持了清晰的边界。
如果即时一致性是不可妥协的,这表明所涉及的实体可能属于同一个聚合,或者需要一个不同的领域对象来强制执行不变量。

### 核心概念

- **Transactional Boundaries(事务边界)**:单个事务只应修改一个聚合实例,以确保性能和可靠性。
- **Eventual Consistency(最终一致性)**:随时间推移而非同时更新相关聚合的过程。
- **Domain Events(领域事件)**:用于通知同一个限界上下文中其他部分某个聚合状态已发生变化的内部信号。
- **Integration Events(集成事件)**:用于跨不同限界上下文传达状态变化的信号。
- **Boundary Validation(边界校验)**:如果最终一致性带来的延迟不可接受,可能需要重新评估或合并聚合边界。

### 课程笔记

聚合设计的基本规则是:一个事务只应更新一个聚合。
当一个业务流程涉及多个聚合时,系统必须依靠最终一致性来跨边界维持状态。

设想一个涉及 `Session` 聚合和 `Participant` 聚合的场景。
当一个参与者试图预留一个名额时,应用程序会从数据库加载 `Session` 聚合。
`Session` 校验其内部不变量(例如检查是否还有足够的空位),并执行 `ReserveSpot` 方法。
一旦 `Session` 被保存回数据库,这次预留就被正式记录下来。

然而,`Participant` 聚合可能也需要更新其内部状态,例如在该 session 的时长内把它的日程标记为忙碌。
由于 `Session` 和 `Participant` 是两个独立的聚合,对参与者日程的更新发生在最初的事务之外。

这一交互的工作流程遵循以下步骤:
1. `Session` 聚合被更新并保存到数据库。
2. 这次更新触发一个 **Domain Event**(领域事件)。
3. 一个专门的事件处理程序监听这个事件。
4. 该处理程序发起一个新事务来更新 `Participant` 聚合。

```text
Transaction 1                         Transaction 2
+--------------------------+          +------------------------------+
| load Session             |          | event handler                |
| validate invariants      |          |   update Participant         |
| ReserveSpot              |          |   (mark schedule as busy)    |
| save Session             |          |   save Participant           |
+------------+-------------+          +---------------^--------------+
             |                                        |
             +------------> Domain Event -------------+
```

这种做法接受了出现竞态条件的风险。
例如,第二个事务可能会失败或被延迟。
这是一种有意为之的架构折中。
如果业务无法容忍 session 与参与者之间的延迟或暂时不一致的风险,那么这两个实体很可能应该属于同一个聚合。
或者,可能需要一个单独的领域对象来在它们之间强制执行不变量。

这条关于边界的原则延伸到了限界上下文(Bounded Context)层面。
领域事件管理单个限界上下文内各聚合之间的一致性,而当一个限界上下文中的变化必须影响另一个限界上下文时,则使用 **Integration Events**(集成事件)。
模式保持不变:一个变化在某个边界内发生,引发一个事件,外部边界对该事件做出反应以更新它自己的状态。

## 7. Maintain Aggregate Scope

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/maintain-aggregate-scope-54855908/) · 0:30

### 总结

本课关注维持聚合范围的原则,即确保聚合内的所有对象在概念上和逻辑上都与聚合根相关。
它着重说明了聚合设计中内聚性的重要性,并提供了一条用于识别聚合边界何时定义错误的经验法则。

### 核心概念

* **Conceptual Relationship(概念关系)**:聚合中包含的每个对象都必须与聚合根有清晰的逻辑联系。
* **Aggregate Cohesiveness(聚合内聚性)**:当聚合的所有成员作为一个单一的一致性单元属于同一整体时,就维持了高内聚性。
* **Design Validation(设计校验)**:如果一个领域对象与它被加入的聚合在逻辑上感觉脱节,那么聚合边界很可能与领域模型没有对齐。

### 课程笔记

维持聚合的范围对于创建一个干净且可维护的领域模型至关重要。
聚合设计的首要规则是:所有内部对象,无论是实体还是值对象,都必须在概念上与聚合根相关。

内聚性是这里的指导原则。
当一个聚合是内聚的,它的成员自然地属于同一整体。
如果你发现自己正在把一个领域对象加入某个聚合,却意识到两者之间没有任何逻辑上或功能上的关系,这就强烈表明该聚合的设计存在缺陷。
在这种情况下,这个对象很可能属于另一个聚合,或者需要重新定义当前聚合的边界,以更好地反映领域的需求。

## 8. Aggregate Root Identity Uniqueness

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/aggregate-root-identity-uniqueness-54855909/) · 0:18

### 总结

本课探讨领域驱动设计中标识的范围,区分了聚合根所需的全局唯一性与内部实体所需的局部唯一性。
它强调,虽然聚合根必须能在整个系统中被识别以便于外部引用,但它所包含的实体只需要在该特定聚合的边界内唯一即可。

### 核心概念

- **Global Identity(全局标识)**:聚合根必须拥有一个在整个系统中唯一的标识符,以便进行外部查找和引用。
- **Local Identity(局部标识)**:聚合边界内的实体只需要拥有相对于其父聚合根唯一的标识。
- **Boundary Encapsulation(边界封装)**:由于外部对象只通过聚合根与内部实体交互,这些内部实体不需要全局唯一性。

### 课程笔记

在领域驱动设计中,标识符的范围由对象在聚合层级中的位置决定。
聚合根是通往一组领域对象的主要网关;因此,它必须拥有全局标识。
这种全局标识确保该聚合能够在整个系统中被唯一识别、从仓储(repository)中取出,或被其他聚合引用。

相比之下,内部实体(聚合中包含的非根实体)不需要一个系统范围内唯一的标识符。
它的标识只需要在其父聚合根的上下文中唯一即可。
由于封装原则规定外部组件不应持有对内部实体的直接引用,而应通过根来访问它们,因此局部唯一性就足以让根管理其内部的实体集合并维持一致性。

## 9. Private > Public

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/private-public-54855910/) · 0:36

### 总结

本课强调聚合设计中的封装原则,主张限制对内部状态和行为的访问。
通过尽量缩小公共暴露面,具体来说就是省去不必要的 getter、setter 以及对嵌套实体的公共访问,聚合得以对其不变量保持严格的控制,并防止外部组件绕过它的业务逻辑。

### 核心概念

- **Encapsulation of state(状态封装)**:只有在外部使用确实必需时,才通过 getter 暴露数据。
- **Controlled modification(受控修改)**:避免公共 setter,以确保所有状态变更都通过强制执行不变量的显式领域方法进行。
- **Hiding internal entities(隐藏内部实体)**:如果嵌套实体的行为只应由聚合根来编排,就应当把它们保持为 private 或 internal。
- **Information Hiding(信息隐藏)**:缩小聚合的公共 API,以把耦合降到最低并保护内部一致性。

### 课程笔记

"Private > Public" 原则是领域驱动设计中聚合设计的一条核心信条,关注对聚合内部结构的严格封装。
一个聚合应当只暴露其使用者绝对需要的数据和行为。

数据的暴露应当谨慎处理。
如果一个属性不需要被外部组件读取,它就不应该有公共 getter。
同样,为了保护聚合状态的完整性,属性不应该有公共 setter。
相反,状态转换应当通过有意义的领域方法来执行,这些方法会强制执行业务规则和不变量。
这可以防止外部对象以可能导致不一致或无效状态的方式操作聚合的内部状态。

这条原则同样适用于聚合内的嵌套实体。
即使一个嵌套实体拥有自己的行为,如果聚合根被设计为这段逻辑的唯一入口,那么这些行为就不应该对外部世界可见。
通过把嵌套实体保持为 private 或限制它们的可见性,聚合根有效地隐藏了内部复杂性,并确保所有交互都经由它自己的公共接口来居中处理。
这确保聚合根始终是其边界内所有业务逻辑的守门人。

## 10. Assess and Refactor

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/assess-and-refactor-54855911/) · 0:20

### 总结

聚合设计是一个迭代的过程,需要持续评估以确保与业务领域保持一致。
随着需求和业务规则随时间演进,开发者必须准备好重构聚合的边界和定义,以准确反映系统的当前状态。

### 核心概念

* **Iterative Design(迭代式设计)**:聚合边界并非固定不变,应当随着领域模型的成熟而重新审视。
* **Requirement Evolution(需求演进)**:业务逻辑或外部需求的变化使得重构现有聚合成为必要。
* **Domain Alignment(领域对齐)**:定期评估确保聚合的定义始终忠实地反映业务的当前状态。

### 课程笔记

聚合设计是一个持续进行的过程,而不是一次性的活动。
虽然一个项目可能以一组特定的聚合边界起步,但这些定义必须定期对照业务的当前状态进行评估。
随着需求演进和业务规则变化,最初的设计可能不再是最优或准确的。

重构聚合是领域驱动设计中开发生命周期的一个标准组成部分。
当领域逻辑发生转变时,应当更新聚合的定义和边界,以确保它们继续封装正确的不变量并维持一致性。
这种持续的对齐确保软件保持可维护,并准确反映它所服务的领域的复杂性。

## 11. Aggregate Design Steps

> [观看本课](https://dometrain.com/take/course/deep-dive-domain-driven-design-ddd-2760775/aggregate-design-steps-54855912/) · 6:06

### 总结

设计聚合需要一种系统化的四步方法,以定义清晰的边界,并让领域对象的集群保持小巧、易于管理。
通过一开始把每个实体都当作独立的聚合根,并且只在原子一致性或即时的业务需求要求时才合并它们,开发者可以在保护领域不变量的同时确保高性能和可扩展性。

### 核心概念

* Entity Identification(识别实体):找出一个限界上下文中的所有实体。
* Relationship Mapping(映射关系):在不考虑持久化问题的前提下理解实体之间如何关联(例如一对多)。
* Smallest Possible Aggregates(尽可能小的聚合):从每个实体都是其自身聚合根这一假设出发。
* Merge Constraints(合并约束):只为满足业务不变量,或在最终一致性不可接受时,才合并聚合。
* Effective Aggregate Design:源自 Vaughn Vernon 关于尽量缩小聚合规模的著作中的原则。

### 课程笔记

聚合设计的过程是一个实用的框架,用于把实体和值对象转化为遵循领域驱动设计规则的、定义良好的聚合。
这种方法深受 Vaughn Vernon 的 "Effective Aggregate Design" 系列中原则的影响,该系列主张尽量缩小聚合的规模,以提升系统的可靠性和性能。

**Step 1: Identify the Entities**
第一步是识别某个特定限界上下文中的所有实体。
由于每个实体都有可能充当聚合根,把它们梳理出来就为确定最终的聚合结构奠定了基础。

**Step 2: Map Relationships**
一旦识别出实体,就必须映射它们之间的关系。
例如,在一个培训领域中,一个 session 可能与一个 trainer 是一对一关系,而一个 trainer 与 session 是一对多关系。
在这个阶段,关键是要忽略数据库持久化,完全专注于领域对象之间如何交互。
这确保聚合只包含在逻辑上和功能上相关的成员。

**Step 3: Mark All Entities as Aggregate Roots**
为了确保聚合尽可能小,最初的设计应当把每个实体都当作它自己的聚合根。
这会在限界上下文中得到尽可能小的边界,而这正是可扩展性和减少事务争用的理想状态。

**Step 4: Merge Aggregates**
最后一步是只在特定约束要求时才合并这些小聚合。
合并有两个主要原因:

1. **Invariants(不变量):** 如果一条业务规则(不变量)不在同一个事务中以原子方式更新两个实体就无法强制执行,那么这些实体必须合并成单个聚合。
2. **Eventual Consistency(最终一致性):** 虽然聚合之间的通信理想情况下应当是最终一致的(使用领域事件和异步处理程序),但某些技术或用户体验方面的需求可能无法容忍这种延迟。如果一个实体中的变更必须立即反映到另一个实体中,而不能有事件处理程序带来的延迟,那么这两者就应当合并成单个聚合,以便进行原子更新。

```text
Step 1               Step 2               Step 3                Step 4
Identify the   -->   Map           -->    Mark all entities -->  Merge aggregates
Entities             Relationships        as Aggregate Roots     (Invariants /
                                                                 Eventual Consistency)
```
