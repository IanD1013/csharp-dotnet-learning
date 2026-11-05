# Tactical Design and Patterns

> 课程:[Getting Started: Domain-Driven Design](https://dometrain.com/course/getting-started-domain-driven-design-ddd/) · 第 4 章
> 共 18 课 · 约 73:28
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [What are Tactical Patterns?](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/what-are-tactical-patterns-54123352/) | 3:15 | [↓](#1-what-are-tactical-patterns) |
| 2 | [What is Tactical Design?](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/what-is-tactical-design-54123353/) | 3:33 | [↓](#2-what-is-tactical-design) |
| 3 | [Entities](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/entities-54123354/) | 3:49 | [↓](#3-entities) |
| 4 | [Implementing the Entity base class](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-the-entity-base-class-54123355/) | 2:23 | [↓](#4-implementing-the-entity-base-class) |
| 5 | [Value Objects](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/value-objects-54123356/) | 7:29 | [↓](#5-value-objects) |
| 6 | [Implementing the Value Object base class](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-the-value-object-base-class-54123357/) | 2:39 | [↓](#6-implementing-the-value-object-base-class) |
| 7 | [Aggregates](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/aggregates-54123358/) | 6:02 | [↓](#7-aggregates) |
| 8 | [Shifting the focus to Aggregates](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/shifting-the-focus-to-aggregates-54123359/) | 6:16 | [↓](#8-shifting-the-focus-to-aggregates) |
| 9 | [Implementing the Aggregate base class](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-the-aggregate-base-class-54123360/) | 1:09 | [↓](#9-implementing-the-aggregate-base-class) |
| 10 | [Structuring the Domain Layer around Aggregates](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/structuring-the-domain-layer-around-aggregates-54123361/) | 8:34 | [↓](#10-structuring-the-domain-layer-around-aggregates) |
| 11 | [Domain Services](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/domain-services-54123362/) | 5:32 | [↓](#11-domain-services) |
| 12 | [Factories](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/factories-54123363/) | 3:34 | [↓](#12-factories) |
| 13 | [Implementing a Static Factory Method](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-a-static-factory-method-54123364/) | 5:05 | [↓](#13-implementing-a-static-factory-method) |
| 14 | [Repositories](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/repositories-54123365/) | 4:40 | [↓](#14-repositories) |
| 15 | [Disconnected Domain Models](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/disconnected-domain-models-54123366/) | 1:40 | [↓](#15-disconnected-domain-models) |
| 16 | [Repositories and EF Core](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/repositories-and-ef-core-54123367/) | 3:18 | [↓](#16-repositories-and-ef-core) |
| 17 | [Implementing a Repository](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-a-repository-54123368/) | 3:14 | [↓](#17-implementing-a-repository) |
| 18 | [Section Recap](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/section-recap-54123369/) | 1:16 | [↓](#18-section-recap) |

## 1. What are Tactical Patterns?

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/what-are-tactical-patterns-54123352/) · 3:15

### 总结

战术设计提供了以反映底层问题空间的方式对领域进行建模所需的技术构建块。
通过使用实体(Entities)、值对象(Value Objects)和聚合(Aggregates)等特定模式,开发者可以创建与领域的逻辑和语言保持一致的代码结构,超越通用的面向对象构造,从而有效地捕捉复杂的业务规则。

### 核心概念

- 战术模式作为领域建模的构建块。
- 对领域建模以反映问题空间,而不仅仅是技术实现。
- DDD 模式与标准 OOP 构造之间的比较。
- 核心模式:Entities、Value Objects、Aggregates、Domain Services、Factories、Repositories 和 Domain Events。
- 补充模式:Modules、Application Services、Bounded Contexts 和 Ubiquitous Language。

### 课程笔记

战术设计是领域驱动设计的一个基本方面,它关注领域模型的实现细节。
战略设计处理高层的边界和关系,而战术设计则提供构建模型本身所需的具体构建块。

战术模式的主要目标是让开发者能够以与问题空间高度相似的方式对领域进行建模。
在标准的面向对象编程中,类、对象和方法等工具被用来表示现实世界的实体。
战术模式为 DDD 提供了一套更专门化的工具。
其中包括 Entities、Value Objects、Aggregates、Domain Services、Factories、Repositories 和 Domain Events。
使用这些模式可以把代码锚定在既定的架构概念上,从而在处理复杂领域时帮助降低认知负担。

除了这些核心构建块之外,战术设计还包括其他模式,例如 Modules、Application Services、Bounded Contexts 和 Ubiquitous Language。
Modules 用于把代码组织到逻辑命名空间中,确保代码结构反映领域的组织方式。
虽然 Bounded Contexts 和 Ubiquitous Language 等概念通常在战略语境中讨论,但它们同样是战术设计不可或缺的一部分,因为它们指导着领域模型的技术实现。

## 2. What is Tactical Design?

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/what-is-tactical-design-54123353/) · 3:33

### 总结

战术设计是领域驱动设计的技术阶段,在这一阶段,领域模型使用实体、值对象和聚合等特定构建块来构建。
它是一个迭代过程,与战略设计协同工作,把问题空间的心智模型和通用语言转化为具体的软件实现。

### 核心概念

- **Tactical Design**:基于已识别的问题空间对领域进行建模的过程。
- **Building Blocks**:用于构建领域模型的特定模式(Aggregates、Entities、Value Objects)。
- **Iterative Process**:战略设计与战术设计之间的关系是非线性的;实现常常会为高层战略提供信息并对其加以完善。
- **Problem Space vs. Solution Space**:把愿景和通用语言转化为技术表示。

### 课程笔记

战术设计是领域驱动设计中专注于基于问题空间对领域进行建模的阶段。
战略设计处理高层架构和边界,而战术设计则提供构建实际领域模型所需的工具和模式。

#### 房子类比

要理解领域探索与战术设计之间的关系,可以考虑建造一座房子的过程。
- **愿景**:在施工开始之前,脑海中已经有了这座房子的图景,包括它的外观、内部布局、家具和整体美感。
  在 DDD 中,这对应于最初的探索阶段,在这一阶段确立通用语言,并识别领域的不变量。
- **施工**:要把愿景变为现实,需要特定的材料,例如墙体、混凝土和砌块。
  在 DDD 中,战术设计就是实际的施工过程。
  所使用的"材料"是战术模式,包括聚合、实体和值对象。

战术设计的目标是确保建成的"房子"(建模后的领域)准确反映在问题空间探索期间形成的最初愿景。

```text
  问题空间探索 (The Vision)          战术设计 (The Construction)
+-----------------------------+    +------------------------------+
| ubiquitous language         |    | aggregates                   |
| invariants                  |--->| entities                     |
|                             |    | value objects                |
+-----------------------------+    +------------------------------+
```

#### Tactical vs. Strategic Design

战略设计和战术设计是领域驱动设计的两个主要组成部分。
虽然理想情况下应从战略设计开始,但这两个阶段并非严格按顺序进行。

战术设计和战略设计通过迭代过程携手合作。
当开发者开始对领域进行战术建模时,他们常常会发现在战略阶段没有考虑到的边界情况或业务规则。
这种发现需要一个反馈循环,让开发者、业务专家和产品经理重新回到领域讨论中来完善模型。
这种持续的协作确保技术实现始终忠实地反映不断演进的问题空间。

## 3. Entities

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/entities-54123354/) · 3:49

### 总结

实体是主要由其身份而非其属性来定义的领域对象。
在领域驱动设计中,参与者(participant)或订阅(subscription)这样的对象属于实体,因为它们拥有在整个生命周期中区分彼此的唯一标识符。
实体的相等性完全通过比较这些标识符来确定,确保代表同一逻辑概念的两个实例即使状态不同也被视为相同。
为了一致地实现这一点,通常会使用一个基类来重写标准的相等性和哈希方法,使其只关注唯一 ID。

### 核心概念

- **Identity**:实体的首要定义特征是唯一 ID。
- **Persistence of Identity**:即使属性随时间变化,实体仍然是同一个逻辑对象。
- **Equality Logic**:当且仅当两个实体实例的标识符匹配时,它们才被视为相等。
- **Base Class Implementation**:在基类中重写 `Equals` 和 `GetHashCode`,可以确保在整个领域中进行一致的基于身份的比较。

### 课程笔记

在领域驱动设计中,如果一个对象拥有唯一标识符,它就属于实体。
与由属性定义的值对象不同,实体由其 ID 定义。
如果一个对象拥有唯一 ID,并且在整个系统中都通过该 ID 来识别,那么它就是一个实体。

例如,考虑 `Participant` 或 `Subscription`。
当用户创建参与者档案时,他们就成为系统中一个唯一的逻辑实体,由唯一 ID 标识。

```csharp
public class Participant
{
    public int Id { get; }
    public string Nickname { get; private set; }
}

var p1 = new Participant { Id = 1, Nickname = "Amiko" };
var p2 = new Participant { Id = 1, Nickname = "Amikoy" };
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/entities-54123354/?t=40)

实体的一个关键特性是如何确定相等性。
比较两个实体实例时,唯一相关的因素是标识符。
在上面的例子中,`p1` 和 `p2` 的昵称不同('Amiko' 和 'Amikoy'),但 ID 相同。
从逻辑上讲,这两个实例指的是同一个人,这也许代表了一种竞态条件:用户试图两次更新自己的昵称。

```text
p1: Id = 1, Nickname = "Amiko"   \
                                  >-- Id 相同 --> 同一个实体
p2: Id = 1, Nickname = "Amikoy"  /
```

为了强制这种行为,实体不应使用默认的结构相等性。
相反,需要创建一个基类来重写 `Equals` 方法。
该实现首先检查 null 和类型不匹配,然后把对象转换为 `Entity` 基类并比较 ID。

```csharp
public override bool Equals(object? other)
{
    if (other is null || other.GetType() != GetType())
    {
        return false;
    }

    return ((Entity)other).Id == Id;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/entities-54123354/?t=160)

同样,必须重写 `GetHashCode` 方法,以确保具有相同 ID 的实体产生相同的哈希码,与相等性逻辑保持一致。

```csharp
public override int GetHashCode()
{
    return Id.GetHashCode();
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/entities-54123354/?t=205)

通过把这一逻辑集中在基类中,实体的战术模式就能在整个领域模型中得到一致的应用。

## 4. Implementing the Entity base class

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-the-entity-base-class-54123355/) · 2:23

本课介绍如何在领域驱动设计语境中实现一个 Entity 基类,以集中管理基于身份的相等性逻辑。
通过重写 Equals 和 GetHashCode 方法,使其只关注唯一的 Guid 标识符,该基类确保领域实体按其身份而非其瞬时状态进行比较。
这种模式简化了具体实体(例如 Participant)的创建,它们可以继承这一标准行为,同时专注于各自特定的领域逻辑。

### 核心概念

- **Identity-based Equality**:实体由唯一标识符而非其属性定义。
- **Abstract Base Class**:使用抽象的 `Entity` 类来封装所有领域实体的共享逻辑。
- **Method Overriding**:重写 `Equals` 和 `GetHashCode`,以确保在集合和比较中行为一致。
- **Encapsulation**:提供一个 protected 构造函数和一个只读的 `Id` 属性。

### 课程笔记

要开始实现战术 DDD 模式,先在 Domain 项目中创建一个 `Common` 文件夹,用来存放共享的基类。
`Entity` 基类被定义为抽象类,为所有领域实体提供基础。

```csharp
namespace DomeGym.Domain.Common;

public abstract class Entity
{

}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-the-entity-base-class-54123355/?t=30)

`Entity` 基类的主要职责是确保相等性由身份而非属性值决定。
这需要重写 `Equals` 方法。
该实现首先检查传入的对象是否为 null,或者其类型是否与当前实例不同。

```csharp
namespace DomeGym.Domain.Common;

public abstract class Entity
{
    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }

    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-the-entity-base-class-54123355/?t=40)

接下来,定义一个唯一标识符属性,通常是 `Guid`。
然后更新 `Equals` 方法,把对象转换为 `Entity` 并比较 `Id` 属性。
如果 ID 匹配,这些对象就被视为同一个实体。

```csharp
namespace DomeGym.Domain.Common;

public abstract class Entity
{
    public Guid Id { get; }

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }

        return ((Entity)obj).Id == Id;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-the-entity-base-class-54123355/?t=80)

使用一个 protected 构造函数来初始化 `Id`。
此外,由于重写了 `Equals`,也必须重写 `GetHashCode`,使其返回 `Id` 的哈希码。
这确保实体在用作字典的键或放在哈希集合中时行为正确。

```csharp
public abstract class Entity
{
    public Guid Id { get; }

    protected Entity(Guid id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }

        return ((Entity)obj).Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-the-entity-base-class-54123355/?t=100)

要使用这个基类,`Participant` 等领域实体需继承 `Entity`。
这让具体类可以专注于其特定的领域逻辑,同时继承基于身份的相等性行为。
具体类的构造函数把 ID 传给基类构造函数。

```csharp
public class Participant : Entity
{
    private readonly Guid _userId;
    private readonly List<Guid> _sessionIds = new();

    public Participant(
        Guid userId,
        Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        _userId = userId;
    }

    public ErrorOr<Success> AddToSchedule(Session session)
    {
        if (_sessionIds.Contains(session.Id))
        {
            return Error.Conflict(description: "Session already in schedule");
        }
        // ...
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-the-entity-base-class-54123355/?t=130)

## 5. Value Objects

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/value-objects-54123356/) · 7:29

值对象是一种表示领域中某个概念的对象,它完全由其属性来定义。
与通过唯一标识符跟踪的实体不同,值对象由其各个独立属性的组合来定义。
如果两个同类型的值对象拥有完全相同的属性值,它们就被视为同一个对象。

### 核心概念

- **Attribute-based Identity**:相等性由属性的值决定,而不是由唯一 ID 决定。
- **Self-Validation**:值对象负责强制执行自身的不变量,并确保自己始终处于有效状态。
- **Primitive Obsession Mitigation**:值对象取代原始的基元类型(如 double 或 string),以封装领域约束。
- **Immutability**:值对象一旦创建就不能被修改;任何更改都会导致创建一个新实例。
- **Entity Dependency**:值对象在领域中不独立存在;它们始终是某个实体的一部分。

### 课程笔记

#### Defining Value Objects

值对象表示一个特定的概念,例如 `TimeRange`。
在排程语境中,时间范围由开始时间和结束时间定义。
如果两个不同的实例都从下午 1:00 开始、到下午 3:00 结束,它们就表示同一个时间范围。

```csharp
public class TimeRange
{
    public TimeOnly Start { get; init; }
    public TimeOnly End { get; init; }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/value-objects-54123356/?t=40)

#### Primitive Obsession and Validation

基元类型偏执(primitive obsession)是指使用基元类型(如 `double`、`int` 或 `string`)来表示具有特定约束的领域概念的做法。
例如,`Location` 可以用两个 double 来表示纬度和经度。

```csharp
public class Location
{
    public double Latitude { get; init; }
    public double Longitude { get; init; }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/value-objects-54123356/?t=85)

然而,使用原始的基元类型会把验证的责任转移给父实体。
通过把它们包装在值对象中,对象本身就可以在构造期间强制要求纬度和经度落在有效范围内。
这确保了系统中使用的任何 `Location` 对象都保证是有效的。

#### Structural Equality

由于值对象关心的是值而不是身份,它们需要一种与实体不同的相等性处理方式。
这通常由一个重写 `Equals` 和 `GetHashCode` 的基类来处理。
该基类使用一个通常名为 `GetEqualityComponents` 的方法,它按既定顺序返回值对象的各个属性。

```csharp
public abstract IEnumerable<object> GetEqualityComponents();
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/value-objects-54123356/?t=220)

值对象的具体实现(例如 `TimeRange`)实现这个方法,产出(yield)其内部属性。
然后基类的 `Equals` 方法比较这些序列来确定相等性。

```csharp
public class TimeRange : ValueObject
{
    public TimeOnly Start { get; init; }
    public TimeOnly End { get; init; }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Start;
        yield return End;
    }
}

public override bool Equals(object? other)
{
    if (other is null || other.GetType() != GetType())
    {
        return false;
    }

    return ((ValueObject)other).GetEqualityComponents()
        .SequenceEqual(GetEqualityComponents());
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/value-objects-54123356/?t=280)

同样,`GetHashCode` 方法也被重写,以聚合所有相等性组件的哈希码,确保值相同的两个对象产生相同的哈希。

```csharp
public override int GetHashCode()
{
    return GetEqualityComponents()
        .Select(x => x?.GetHashCode() ?? 0)
        .Aggregate((x, y) => x ^ y);
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/value-objects-54123356/?t=340)

#### Immutability and Lifecycle

值对象是不可变的。
如果值对象需要更新,执行更新的方法不应修改现有实例的属性。
相反,它应该返回一个带有更新后值的新值对象实例。
这种不可变性提供了安全性,因为对象在经过验证之后不会被意外更改。

最后,值对象没有独立的生命周期。
在领域模型中,值对象始终作为实体的属性或组成部分而存在。

## 6. Implementing the Value Object base class

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-the-value-object-base-class-54123357/) · 2:39

### 总结

本课演示如何为值对象实现一个可复用的基类,以确保结构相等性。
通过定义一个用于获取相等性组件的抽象方法,基类可以通用地实现 Equals 和 GetHashCode,使 TimeRange 等具体值对象能够基于其内部属性而非引用来定义自身的身份。

### 核心概念

- 结构相等性与引用相等性。
- ValueObject 抽象基类。
- 用于枚举属性的 GetEqualityComponents 模式。
- 使用 SequenceEqual 进行逐组件比较。
- 使用 XOR 运算符聚合哈希码。

### 课程笔记

为了给值对象实现结构相等性,在 common 领域文件夹中创建一个基类。
该类定义了一个抽象方法 `GetEqualityComponents`,它返回 `IEnumerable<object?>`。
每个具体的值对象都必须实现这个方法,以提供定义其身份的特定属性。

```csharp
namespace DomeGym.Domain.Common;

public abstract class ValueObject
{
    public abstract IEnumerable<object?> GetEqualityComponents();
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-the-value-object-base-class-54123357/?t=15)

具体实现(例如 `TimeRange`)继承这个基类,并使用 `yield return` 实现该抽象方法,按明确定义的顺序提供其内部值。
这确保相等性由属性的值决定,而不是由对象的内存引用决定。

```csharp
public class TimeRange : ValueObject
{
    public TimeOnly Start { get; }
    public TimeOnly End { get; }

    public TimeRange(TimeOnly start, TimeOnly end)
    {
        Start = start;
        End = end;
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Start;
        yield return End;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-the-value-object-base-class-54123357/?t=25)

`ValueObject` 基类重写 `Equals` 方法,基于组件来比较实例。
它首先检查传入的对象是否为 null 或类型不同。
如果类型匹配,它把对象转换为 `ValueObject`,并使用 `SequenceEqual` 比较两个实例的 `GetEqualityComponents` 返回的组件。

```csharp
public abstract class ValueObject
{
    public abstract IEnumerable<object?> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }

        return ((ValueObject)obj)
            .GetEqualityComponents()
            .SequenceEqual(GetEqualityComponents());
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-the-value-object-base-class-54123357/?t=115)

最后,重写 `GetHashCode` 方法以确保与 `Equals` 保持一致。
它遍历相等性组件,获取各自的哈希码(如果组件为 null 则默认为零),并通过 `Aggregate` 方法使用 XOR(`^`)运算符把它们聚合起来。

```csharp
public abstract class ValueObject
{
    public abstract IEnumerable<object?> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }

        return ((ValueObject)obj)
            .GetEqualityComponents()
            .SequenceEqual(GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-the-value-object-base-class-54123357/?t=145)

## 7. Aggregates

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/aggregates-54123358/) · 6:02

### 总结

聚合是领域驱动设计中一种基础的战术模式,它表示一组领域对象(实体和值对象)的集群,这些对象必须作为单个原子单元保持一致。
聚合确保在提交任何状态变更之前,所有内部业务规则和不变量都得到满足。
每个聚合都有一个指定的聚合根(Aggregate Root),它作为交互的主要入口点,确保整个集群在单个事务中被一起加载、修改和持久化,以保证数据完整性。

### 核心概念

*   **Consistency Boundary**:聚合定义了必须严格强制执行领域不变量的边界。
*   **Atomicity**:聚合内的状态变更作为一个完整的单元发生;它不能处于部分更新或无效的状态。
*   **Aggregate Root**:作为聚合入口的特定实体,负责维护所有子对象的内部一致性。
*   **Transactional Integrity**:聚合通常在单个事务中作为一个整体从数据库中检索并持久化到数据库。
*   **Composition**:聚合可以由单个实体组成,也可以由实体和值对象构成的复杂树组成。

### 课程笔记

聚合被定义为必须始终作为一个整体保持一致的一个或多个领域对象(实体或值对象)。
在领域驱动设计中,它们充当原子单元。
当对聚合进行更改时,它必须以原子方式从一个有效状态转换到另一个有效状态,而绝不会处于不一致的中间状态。

以 `Participant` 实体为例。
它包含管理自身日程的逻辑,这涉及协调多个内部对象。
`AddToSchedule` 方法演示了聚合如何在其内部组件之间强制执行不变量:

```csharp
public ErrorOr<Success> AddToSchedule(Session session)
{
    if (_sessionIds.Contains(session.Id))
    {
        return Error.Conflict(
            description: "Session already exists in participant's schedule");
    }

    var bookTimeSlotResult = _schedule.BookTimeSlot(
        session.Date,
        session.Time);

    if (bookTimeSlotResult.IsError)
    {
        return bookTimeSlotResult.FirstError.Type == ErrorType.Conflict
            ? ParticipantErrors.CannotHaveTwoOrMoreOverlappingSessions
            : bookTimeSlotResult.Errors;
    }

    _sessionIds.Add(session.Id);
    return Result.Success;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/aggregates-54123358/?t=100)

在这个例子中,`Participant` 聚合由 `Participant` 实体、一个 `Schedule` 实体和多个 `TimeRange` 值对象组成。
为了强制执行"参与者不能有重叠的课程"这一不变量,聚合必须包含所有这些对象。
`AddToSchedule` 的调用方不需要了解内部的复杂性;他们只关心:如果方法返回成功,聚合就处于添加了该课程的有效状态。
如果它返回错误,聚合就保持在之前的有效状态。

```text
Participant 聚合 (Aggregate Root: Participant)
+-------------------------------------------+
| Participant (entity)                      |
|   +-- Schedule (entity)                   |
|         +-- TimeRange (value object)      |
|         +-- TimeRange (value object)      |
|         +-- ...                           |
+-------------------------------------------+
```

每个聚合都有一个作为根的实体。
在这里,`Participant` 就是聚合根。

```csharp
public class Participant : Entity
{
    // ..
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/aggregates-54123358/?t=280)

在持久化或检索聚合时,必须把它作为单个单元来处理。
存储 `Participant` 时,系统会在单个数据库事务中保存参与者、日程以及所有关联的时间范围。
这确保系统始终用一个有效状态替换另一个有效状态,从而维护领域模型的完整性。

## 8. Shifting the focus to Aggregates

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/shifting-the-focus-to-aggregates-54123359/) · 6:16

### 总结

把视角转向聚合,意味着把领域对象看作自给自足的集群,由它们负责维护自身的一致性和不变量。
聚合是由领域对象(实体和值对象)构成的一棵树,其中一个实体充当聚合根。
本课演示如何重构 Session 聚合,用一个 Reservation 实体取代简单的 ID 列表,以说明聚合如何管理内部状态,同时通过唯一标识符而非直接的对象引用来引用外部聚合。

### 核心概念

* **Aggregate Self-Sufficiency**:聚合是维护内部不变量的独立单元,因此可以在不影响系统其他部分的情况下对其进行更改。
* **Aggregate Root**:位于聚合树根部的主要实体。
  所有对聚合的外部访问都必须通过这个根进行。
* **Internal Composition**:聚合可以由单个实体、一个带有值对象的实体,或者由多个实体和值对象构成的复杂树组成。
* **External References**:聚合使用唯一标识符(ID)而不是直接的对象实例来引用其他聚合,以维护边界。

### 课程笔记

聚合是领域层中主要的一致性单元。
每个聚合都是自给自足的,这意味着它可以独立地维护自身的不变量。
如果多个对象必须一起保持一致,它们很可能应该属于同一个聚合。
聚合可以简单到只有单个实体,也可以复杂到是一棵包含多个实体和值对象的树。

最初,`Session` 实体使用一个简单的 GUID 列表来管理参与者:

```csharp
public class Session
{
    private readonly Guid _trainerId;

    private readonly List<Guid> _participantIds = new();

    private readonly int _maxParticipants;

    public Guid Id { get; }

    public DateOnly Date { get; }

    public TimeRange Time { get; }

    public Session(
        DateOnly date,
        TimeRange time,
        int maxParticipants,
        Guid trainerId,
        Guid? id = null)
    {
        Date = date;
        Time = time;
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/shifting-the-focus-to-aggregates-54123359/?t=85)

为了更好地捕捉领域需求(例如跟踪出勤、计费或到达时间),系统需要在 `Session` 聚合内有一个更健壮的 `Reservation` 实体。
这个实体跟踪参与者与课程之间的具体关系。

```csharp
using DomeGym.Domain.Common;

namespace DomeGym.Domain;

public class Reservation : Entity
{
    public Guid ParticipantId { get; }

    public Reservation(Guid participantId, Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        ParticipantId = participantId;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/shifting-the-focus-to-aggregates-54123359/?t=190)

定义好 `Reservation` 实体后,`Session` 聚合被更新为持有一个 `Reservation` 对象的集合,而不是原始的 `Guid` 标识符。

```csharp
namespace DomeGym.Domain;

public class Session
{
    private readonly Guid _trainerId;
    private readonly List<Reservation> _reservations = new();
    private readonly int _maxParticipants;

    public Guid Id { get; }

    public DateOnly Date { get; }

    public TimeRange Time { get; }

    public Session(
        DateOnly date,
        TimeRange time,
        int maxParticipants,
        Guid trainerId,
        Guid? id = null)
    {
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/shifting-the-focus-to-aggregates-54123359/?t=160)

这一转变需要更新管理预订的领域逻辑。
取消预订时,聚合根现在必须先找到与该参与者关联的特定 `Reservation` 实体,然后再把它从内部集合中移除。

```csharp
public ErrorOr<Success> CancelReservation(Participant participant, IDateTimeProvider dateTimeProvider)
{
    if (IsTooCloseToSession(dateTimeProvider.UtcNow))
    {
        return SessionErrors.CannotCancelReservationTooCloseToSession;
    }

    var reservation = _reservations.Find(reservation => reservation.ParticipantId == participant.Id);
    
    if (reservation is null)
    {
        return Error.NotFound(description: "Participant not found");
    }

    _reservations.Remove(reservation);

    return Result.Success;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/shifting-the-focus-to-aggregates-54123359/?t=220)

同样,`ReserveSpot` 方法也被更新,以确保在创建新的 `Reservation` 实例之前,该参与者在聚合内还没有有效的预订。

```csharp
public ErrorOr<Success> ReserveSpot(Participant participant)
{
    if (_reservations.Count >= _maxParticipants)
    {
        return SessionErrors.CannotHaveMoreReservationsThanParticipants;
    }

    if (_reservations.Any(reservation => reservation.ParticipantId == participant.Id))
    {
        return Error.Conflict(description: "Participants cannot reserve twice");
    }

    var reservation = new Reservation(participant.Id);

    _reservations.Add(reservation);

    return Result.Success;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/shifting-the-focus-to-aggregates-54123359/?t=265)

在这个结构中,`Session` 充当**聚合根**。
虽然它包含 `Reservation` 实体,但它只通过 ID 引用其他聚合(例如 `Participant`、`Room` 和 `Trainer`)。
这在领域层的不同聚合之间维持了清晰的边界。

```text
Session 聚合 (Aggregate Root)          其他聚合 (只通过 ID 引用)
+--------------------------------+
| Session                        |
|   _trainerId ------------------+-----> Trainer
|   _reservations                |
|     +-- Reservation (entity)   |
|           ParticipantId -------+-----> Participant
+--------------------------------+
```

## 9. Implementing the Aggregate base class

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-the-aggregate-base-class-54123360/) · 1:09

### 总结

AggregateRoot 基类是一种专门的抽象,用于区分作为聚合入口点的实体。
通过继承 Entity 基类,它确保根保持稳定的身份,同时提供一个专门的类型来强制执行架构边界。
这个基类便于把相关的实体和值对象分组,为将来聚合特有的功能(例如领域事件处理和一致性强制)奠定基础。

### 核心概念

- **Aggregate Root Identification**:使用特定的基类来标记那些充当一组领域对象主要接口的实体。
- **Inheritance from Entity**:确保聚合根保留实体的基本特征,包括唯一身份。
- **Architectural Enforcement**:提供一种类型安全的方式来区分根和内部实体,这有助于定义仓储边界和一致性规则。
- **Foundation for Future Logic**:为专门适用于聚合的逻辑(例如领域事件收集)确立一个存放位置。

### 课程笔记

AggregateRoot 基类的实现从领域的公共基础设施层开始,与现有的实体和值对象基类放在一起。
创建这个类的主要动机是为代表聚合根的实体提供一个清晰的标记。
这种显式标记帮助开发者识别聚合的边界,以及根实体与其所包含的实体和值对象之间的关系。

最初,这个类在 common 命名空间中被定义为一个抽象类。

```csharp
namespace DomeGym.Domain.Common;

public abstract class AggregateRoot
{
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-the-aggregate-base-class-54123360/?t=40)

由于聚合根本质上是一个实体,它必须继承 Entity 基类。
这种继承确保聚合根拥有唯一标识符。
AggregateRoot 的构造函数被定义为 protected,以确保它只能被派生类调用,它接受一个 Guid,并把它传给 Entity 基类的构造函数。

```csharp
namespace DomeGym.Domain.Common;

public abstract class AggregateRoot : Entity
{
    protected AggregateRoot(Guid id) : base(id)
    {
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-the-aggregate-base-class-54123360/?t=55)

虽然这个类起初看起来可能只是对 Entity 类的简单包装,但它是一个关键的扩展点。
在后续的开发中,这个基类将被用来实现聚合特有的功能,例如领域事件的管理和分发。

## 10. Structuring the Domain Layer around Aggregates

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/structuring-the-domain-layer-around-aggregates-54123361/) · 8:34

### 总结

本课详细介绍如何通过把领域对象分组到专门的聚合文件夹中,对领域层进行结构上的重组。
每个聚合都以一个聚合根为中心,聚合根继承一个专门的基类来管理身份和一致性。
共享组件,包括 Schedule 这样的通用实体和 TimeRange 这样的值对象,被移到一个集中的 common 目录中,以避免逻辑重复。
这一过程还包括调整命名空间以匹配基于聚合的文件夹结构,以及为时间提供器等外部依赖定义领域接口。

### 核心概念

- **Aggregate Root Base Class**:一个继承自 `Entity` 的专门基类,用于标识聚合的入口点。
- **Folder-per-Aggregate Structure**:把领域项目组织成以各个聚合命名的文件夹(例如 `GymAggregate`、`SessionAggregate`)。
- **Common Domain Objects**:把多个聚合共享的实体和值对象集中到一个 `Common` 文件夹中。
- **Domain Interfaces**:在领域层中为非领域关注点(例如时间)定义接口,由外层来实现。
- **Namespace Alignment**:更新 C# 命名空间,以反映物理文件夹结构和聚合边界。

### 课程笔记

#### The Aggregate Root Base Class

为了把聚合根与普通实体区分开来,引入了一个 `AggregateRoot` 基类。
这个类继承自 `Entity` 基类,并确保每个聚合根都有一个唯一标识符。

```csharp
namespace DomeGym.Domain.Common;

public abstract class AggregateRoot : Entity
{
    protected AggregateRoot(Guid id) : base(id)
    {
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/structuring-the-domain-layer-around-aggregates-54123361/?t=10)

#### Organizing Aggregates

通过为每个聚合创建一个文件夹来组织领域层。
每个文件夹包含聚合根、所有内部实体以及该聚合特有的错误定义。

##### Admin and Gym Aggregates

`Admin` 和 `Gym` 实体分别被移到 `AdminAggregate` 和 `GymAggregate` 文件夹中。
它们被更新为继承 `AggregateRoot`,并且其构造函数被修改为把 ID 传给基类,同时移除了本地的 `Id` 属性。

```csharp
using DomeGym.Domain.Common;
using ErrorOr;

namespace DomeGym.Domain.GymAggregate;

public class Gym : AggregateRoot
{
    private readonly Guid _subscriptionId;
    private readonly int _maxRooms;
    private readonly List<Guid> _roomIds = new();

    public Gym(
        int maxRooms,
        Guid subscriptionId,
        Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        _maxRooms = maxRooms;
        _subscriptionId = subscriptionId;
    }

    public ErrorOr<Success> AddRoom(Room room)
    {
        // ...
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/structuring-the-domain-layer-around-aggregates-54123361/?t=80)

##### Participant and Shared Entities

`Participant` 被移到 `ParticipantAggregate` 中。
在这个过程中,`Schedule` 实体被识别为参与者、房间和教练共用的共享组件。
共享实体被移到 `DomeGym.Domain.Common.Entities` 中以避免重复,不过这会在聚合之间引入耦合。

```csharp
using DomeGym.Domain.Common;
using ErrorOr;

namespace DomeGym.Domain.ParticipantAggregate;

public class Participant : AggregateRoot
{
    private readonly Schedule _schedule = Schedule.Empty();

    private readonly Guid _userId;
    private readonly List<Guid> _sessionIds = new();

    public Participant(Guid userId, Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        _userId = userId;
    }

    public ErrorOr<Success> AddToSchedule(Session session)
    {
        if (_sessionIds.Contains(session.Id))
        {
            return Error.Conflict(description: "Session already exists");
        }
        // ...
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/structuring-the-domain-layer-around-aggregates-54123361/?t=100)

```csharp
using ErrorOr;

namespace DomeGym.Domain.Common.Entities;

public class Schedule
{
    private readonly Dictionary<DateOnly, List<TimeRange>> _calendar = new();
    private readonly Guid _id;

    public Schedule(
        Dictionary<DateOnly, List<TimeRange>>? calendar = null,
        Guid? id = null)
    {
        _calendar = calendar ?? new();
        _id = id ?? Guid.NewGuid();
    }

    public static Schedule Empty()
    {
        return new Schedule(id: Guid.NewGuid());
    }

    internal bool CanBookTimeSlot(DateOnly date, TimeRange timeRange)
    {
        // ...
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/structuring-the-domain-layer-around-aggregates-54123361/?t=115)

##### Session and Reservation

`Session` 聚合同时包含 `Session` 聚合根和 `Reservation` 实体。

```csharp
using DomeGym.Domain.Common;
using ErrorOr;

namespace DomeGym.Domain.SessionAggregate;

public class Session : AggregateRoot
{
    private readonly Guid _trainerId;
    private readonly List<Reservation> _reservations = new();
    private readonly int _maxParticipants;

    public DateOnly Date { get; }
    public TimeRange Time { get; }

    public Session(
        DateOnly date,
        TimeRange time,
        int maxParticipants,
        Guid trainerId,
        Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        Date = date;
        Time = time;
        _maxParticipants = maxParticipants;
        _trainerId = trainerId;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/structuring-the-domain-layer-around-aggregates-54123361/?t=175)

```csharp
using DomeGym.Domain.Common;

namespace DomeGym.Domain.SessionAggregate;

public class Reservation : Entity
{
    public Guid ParticipantId { get; }

    public Reservation(Guid participantId, Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        ParticipantId = participantId;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/structuring-the-domain-layer-around-aggregates-54123361/?t=505)

#### Common Value Objects

`TimeRange` 这样的通用值对象被移到 `DomeGym.Domain.Common.ValueObjects` 中。
这个文件夹中的对象必须保持通用,不包含任何聚合特有的逻辑,以维持清晰的边界。

```csharp
using DomeGym.Domain.Common;
using ErrorOr;
using Throw;

namespace DomeGym.Domain.Common.ValueObjects;

public class TimeRange : ValueObject
{
    public TimeOnly Start { get; init; }
    public TimeOnly End { get; init; }

    public TimeRange(TimeOnly start, TimeOnly end)
    {
        Start = start.Throw().IfGreaterThanOrEqualTo(end);
        End = end;
    }

    public static ErrorOr<TimeRange> FromDateTimes(DateTime start, DateTime end)
    {
        if (start.Date != end.Date || start >= end)
        {
            return Error.Validation();
        }
        // ...
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/structuring-the-domain-layer-around-aggregates-54123361/?t=205)

#### Room, Subscription, and Trainer Aggregates

其余的实体也以类似方式被重构到各自的聚合文件夹中,并被更新为继承 `AggregateRoot`。

```csharp
using DomeGym.Domain.Common;
using ErrorOr;

namespace DomeGym.Domain.RoomAggregate;

public class Room : AggregateRoot
{
    private readonly List<Guid> _sessionIds = new();
    private readonly int _maxDailySessions;
    private readonly Guid _gymId;
    private readonly Schedule _schedule = Schedule.Empty();

    public Room(
        int maxDailySessions,
        Guid gymId,
        Schedule? schedule = null,
        Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        _maxDailySessions = maxDailySessions;
        _gymId = gymId;
        _schedule = schedule ?? Schedule.Empty();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/structuring-the-domain-layer-around-aggregates-54123361/?t=300)

```csharp
using DomeGym.Domain.Common;
using ErrorOr;

namespace DomeGym.Domain.SubscriptionAggregate;

public class Subscription : AggregateRoot
{
    private readonly List<Guid> _gymIds = new();
    private readonly SubscriptionType _subscriptionType;
    private readonly int _maxGyms;
    private readonly Guid _adminId;

    public Subscription(
        SubscriptionType subscriptionType,
        Guid adminId,
        Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        _subscriptionType = subscriptionType;
        _maxGyms = GetMaxGyms();
        _adminId = adminId;
    }
    // ...
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/structuring-the-domain-layer-around-aggregates-54123361/?t=325)

```csharp
using DomeGym.Domain.Common;
using ErrorOr;

namespace DomeGym.Domain.TrainerAggregate;

public class Trainer : AggregateRoot
{
    private readonly Guid _userId;
    private readonly List<Guid> _sessionIds = new();
    private readonly Schedule _schedule = Schedule.Empty();

    public Trainer(
        Guid userId,
        Schedule? schedule = null,
        Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        _userId = userId;
        _schedule = schedule ?? Schedule.Empty();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/structuring-the-domain-layer-around-aggregates-54123361/?t=340)

#### Domain Interfaces

那些在领域内使用、但由外层(例如 Application 或 Infrastructure)实现的依赖,其接口被放在 `DomeGym.Domain.Common.Interfaces` 中。

```csharp
namespace DomeGym.Domain.Common.Interfaces;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/structuring-the-domain-layer-around-aggregates-54123361/?t=415)

```text
DomeGym.Domain
+-- Common
|   +-- Entities          (Schedule)
|   +-- ValueObjects      (TimeRange)
|   +-- Interfaces        (IDateTimeProvider)
+-- AdminAggregate        (Admin)
+-- GymAggregate          (Gym)
+-- ParticipantAggregate  (Participant)
+-- RoomAggregate         (Room)
+-- SessionAggregate      (Session, Reservation)
+-- SubscriptionAggregate (Subscription)
+-- TrainerAggregate      (Trainer)
```

#### Final Refinement and Namespaces

移动文件之后,命名空间被更新以匹配新的文件夹结构。
此外,还修正了私有字段在构造函数中缺失的初始化。

```csharp
using DomeGym.Domain.Common;

namespace DomeGym.Domain.AdminAggregate;

public class Admin : AggregateRoot
{
    private readonly Guid _userId;
    private readonly Guid _subscriptionId;

    public Admin(
        Guid userId,
        Guid subscriptionId,
        Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        _userId = userId;
        _subscriptionId = subscriptionId;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/structuring-the-domain-layer-around-aggregates-54123361/?t=465)

## 11. Domain Services

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/domain-services-54123362/) · 5:32

### 总结

领域服务是一种专门的对象,用于存放那些不自然地属于单个实体或值对象的业务逻辑。
虽然领域驱动设计强调富领域模型,即把逻辑封装在领域对象内部,但领域服务为跨越多个实体或代表横切关注点的规则提供了一个必要的存放位置。
然而,频繁引入领域服务可能是一种"代码坏味道",表明某个领域概念(例如某项特定策略或某个分组实体)尚未在系统中被显式建模。

### 核心概念

- 领域服务的定义特征是包含业务逻辑,这与专注于编排的应用服务不同。
- 放置逻辑的首选层级是:Value Objects > Entities > Domain Services。
- 只有当逻辑无法自然地放进现有领域对象时,才应使用领域服务。
- 它们确保领域层始终是做出业务决策的唯一权威。
- 需要领域服务这一点,往往指向一个本应被建模为实体或值对象的缺失的领域概念。

### 课程笔记

领域服务常常与应用服务或通用的服务对象相混淆。
区别在于,领域服务包含业务逻辑。
在领域驱动设计中,首要目标是把逻辑放在领域对象之内,具体来说就是实体和值对象。
这种方式能创建一个丰富的、封装良好的领域模型。
然而,当业务规则无法自然地分配给单个领域对象时,就会使用领域服务。
其意图是确保只有领域层负责做出领域决策。

考虑一个涉及团体预订系统的场景。
某条业务规则可能规定:为多个参与者进行的团体预订,即使其中有些人存在日程冲突,只要失败的数量不超过特定阈值(例如三个),预订仍算成功。
这一逻辑不属于单个参与者或某个课程,因为它支配的是多个实体之间的关系。

```csharp
public class ReservationService
{
    public ErrorOr<Success> Reserve(
        Session session,
        List<Participant> participants)
    {
        const int MaxUnsuccessfulReservations = 3;

        var unavailableParticipantCount = participants.Count(
            participant => !participant.IsTimeSlotFree(session.Date, session.Time));

        if (unavailableParticipantCount > MaxUnsuccessfulReservations)
        {
            return ReservationServiceErrors.CannotScheduleTooManyUnavailableParticipants;
        }

        participants.ForEach(participant =>
            participant.AddToScheduleOrBacklog(session));

        return Result.Success;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/domain-services-54123362/?t=205)

在上面的实现中,`MaxUnsuccessfulReservations` 常量代表一项业务决策。
由于这一逻辑强制执行一个业务不变量,这个类被归类为领域服务。
该服务计算有多少参与者不可用,然后要么返回一个领域错误,要么继续更新这些参与者的日程。

虽然这种方式可行,但停下来想一想是否缺失了某个领域概念,往往是有益的。
与其使用服务,这一逻辑也许更适合封装在一个新的领域对象中,例如 `Group` 实体。
这个实体随后就可以管理自身的不变量,例如允许的最大失败预订数,从而使模型更具表达力,并与通用语言保持一致。

## 12. Factories

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/factories-54123363/) · 3:34

### 总结

工厂是一种战术设计模式,用于封装创建复杂对象(例如聚合)所需的逻辑。
虽然构造函数通常已经足够,但工厂提供了一种方式来处理复杂的实例化逻辑、异步需求,以及使用结果模式进行错误处理,从而减轻使用者的认知负担。

### 核心概念

* 封装复杂对象的创建。
* 降低领域中的认知负担。
* 处理异步实例化。
* 支持用于错误处理的 Result 模式。
* 静态工厂方法与独立工厂类的对比。

### 课程笔记

工厂是一种旨在协助创建复杂对象(例如聚合)的对象或方法。
例如,`SessionFactory` 可以封装实例化 `Session` 聚合所需的逻辑,确保所有必要的参数和内部状态都被正确初始化。

```csharp
public class SessionFactory
{
    public static Session Create(
        string name,
        string description,
        // ..
        Guid? id = null)
    {
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/factories-54123363/?t=10)

实现工厂的首要经验法则是看它是否简化了代码。
如果创建逻辑足够复杂,以至于把它提取到工厂中能减轻开发者的认知负担,那么使用工厂就是合适的。
如果它不能简化代码,通常更倾向于使用标准构造函数。

有两种特定场景必须使用工厂:

1. **Asynchronous Logic**:由于 C# 中的构造函数不能是异步的,如果实例化过程需要 `async` 操作,就必须使用工厂。
2. **Result Pattern**:构造函数不能返回结果类型(例如 `ErrorOr<T>`);它们只能返回对象本身或抛出异常。
   如果领域设计倾向于在实例化期间返回错误结果而不是抛出异常,就需要用工厂来返回结果对象。

与其创建一个单独的工厂类,一种常见且优雅的做法是直接在被实例化的类上使用**静态工厂方法**。
这个方法可以执行验证,并返回所创建的对象或一组错误。

```csharp
public class Session : AggregateRoot
{
    public static ErrorOr<Session> Create(
        string name,
        string description,
        // ..
        Guid? id = null)
    {
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/factories-54123363/?t=175)

通过使用 `Session.Create` 这样的静态工厂方法,验证输入并确保有效状态的责任被封装在聚合本身之内,同时为调用方提供一个表明成功或失败的清晰结果。

## 13. Implementing a Static Factory Method

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-a-static-factory-method-54123364/) · 5:05

### 总结

本课演示如何在领域值对象中实现静态工厂方法,以安全地处理复杂的验证和对象创建。
通过把验证逻辑从应用层移到领域模型中,开发者可以确保 TimeRange 这样的对象始终处于有效状态,或者在日常操作中返回描述性的验证错误,而不是抛出异常。

### 核心概念

- **Constructor Validation**:使用守卫子句防止实例化处于无效状态的对象。
- **Static Factory Methods**:为对象创建提供替代入口点,这些入口点可以封装复杂的转换和验证逻辑。
- **Functional Error Handling**:在输入数据不可信时,利用 `ErrorOr` 模式返回验证错误,而不是抛出异常。
- **Domain Invariants**:直接在领域对象内强制执行业务规则(例如,时间范围必须先开始后结束,并且发生在同一天)。

### 课程笔记

在值对象的标准实现中,构造函数负责赋值。
然而,简单的构造函数往往允许创建处于无效状态的对象,例如开始时间晚于结束时间的 `TimeRange`。

```csharp
namespace DomeGym.Domain.Common.ValueObjects;

public class TimeRange : ValueObject
{
    public TimeOnly Start { get; init; }
    public TimeOnly End { get; init; }

    public TimeRange(TimeOnly start, TimeOnly end)
    {
        Start = start;
        End = end;
    }

    public bool OverlapsWith(TimeRange other)
    {
        if (Start >= other.End) return false;
        if (other.Start >= End) return false;

        return true;
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Start;
        yield return End;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-a-static-factory-method-54123364/?t=10)

为了防止无效状态,第一种选择是在构造函数中实现守卫子句。
使用 `Throw` 这样的库,可以确保开始时间始终小于结束时间。
如果违反了这一条件,就会抛出异常。
这适用于系统已经通过模型验证阶段的场景,此时无效状态才真正属于异常情况。

```csharp
using Throw;

namespace DomeGym.Domain.Common.ValueObjects;

public class TimeRange : ValueObject
{
    public TimeOnly Start { get; init; }
    public TimeOnly End { get; init; }

    public TimeRange(TimeOnly start, TimeOnly end)
    {
        Start = start.Throw().IfGreaterThanOrEqualTo(end);
        End = end;
    }

    public bool OverlapsWith(TimeRange other)
    {
        if (Start >= other.End) return false;
        if (other.Start >= End) return false;

        return true;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-a-static-factory-method-54123364/?t=70)

有些情况下,你希望安全地创建 `TimeRange`,返回验证错误而不是抛出异常,例如在处理来自 Web API 请求或 UI 表单的原始数据时。
静态工厂方法让你可以在执行必要验证的同时,把外部类型(如 `DateTime`)转换为领域类型的逻辑集中起来。

静态工厂方法 `FromDateTimes` 封装了几条领域规则:
1. 开始日期和结束日期必须在同一天。
2. 开始时间在时间顺序上必须早于结束时间。
3. 如果有效,它负责从 `DateTime` 到 `TimeOnly` 的转换,并返回初始化好的对象。

```csharp
public class TimeRange : ValueObject
{
    public TimeOnly Start { get; init; }
    public TimeOnly End { get; init; }

    public TimeRange(TimeOnly start, TimeOnly end)
    {
        Start = start.Throw().IfGreaterThanOrEqualTo(end);
        End = end;
    }

    public static ErrorOr<TimeRange> FromDateTimes(DateTime start, DateTime end)
    {
        if (start.Date != end.Date)
        {
            return Error.Validation(description: "Start and end date times must be on the same day.");
        }

        if (start >= end)
        {
            return Error.Validation(description: "End time must be greater than the start time.");
        }

        return new TimeRange(
            start: TimeOnly.FromDateTime(start),
            end: TimeOnly.FromDateTime(end));
    }

    public bool OverlapsWith(TimeRange other)
    {
        if (Start >= other.End) return false;
        if (other.Start >= End) return false;

        return true;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-a-static-factory-method-54123364/?t=265)

```text
DateTime start, DateTime end
        |
        v
TimeRange.FromDateTimes(start, end)
        |
        +-- start.Date != end.Date --> Error.Validation (same day)
        +-- start >= end ------------> Error.Validation (end > start)
        |
        v
new TimeRange(TimeOnly.FromDateTime(start), TimeOnly.FromDateTime(end))
```

这种方式提供了一种优雅的手段,把原本会在应用程序多个层之间重复的共享逻辑集中起来,确保领域模型始终是有效性的唯一事实来源。

## 14. Repositories

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/repositories-54123365/) · 4:40

### 总结

仓储(Repository)模式提供了一种抽象,让领域层可以像操作内存中的集合一样与领域对象交互,从而有效地隐藏底层持久化机制的复杂性。
通过把仓储围绕聚合根来设计,该模式确保一致性边界得到尊重,并且领域保持持久化无关(persistence ignorant),专注于业务逻辑而不是存储细节。

### 核心概念

- **Persistence Abstraction**:仓储营造出一种内存列表的"假象",把领域逻辑与基于 SQL、NoSQL 或 API 的存储解耦。
- **Persistence Ignorance**:在对领域逻辑建模时不考虑对象最终如何存储的原则。
- **Aggregate Root Scope**:仓储应该只为聚合根定义,以维护一致性边界。
- **Service Orchestration**:应用服务和领域服务都使用仓储来获取、操作和持久化聚合。

### 课程笔记

仓储是一种战术模式,充当领域与持久化存储之间的抽象层。
它营造出一种假象:我们只是在操作领域对象,就好像它们就放在内存中的一个列表里,而实际的实现则处理 SQL 数据库、文档数据库或外部 HTTP 调用的复杂性。
这种抽象让使用仓储的代码可以对底层的持久化逻辑一无所知。

下面的接口展示了 `Session` 聚合的一个仓储:

```csharp
public interface ISessionsRepository
{
    Task AddSessionAsync(Session session);
    Task<Session?> GetByIdAsync(Guid id);
    Task<List<Session>> ListByIdsAsync(IReadOnlyList<Guid> sessionIds, DateTime? startDateTime = null);
    Task<List<Session>> ListByGymIdAsync(Guid gymId, DateTime? startDateTime = null);
    Task UpdateAsync(Session session);
    Task<List<Session>> ListByRoomIdAsync(Guid roomId);
    Task RemoveRangeAsync(List<Session> sessions);
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/repositories-54123365/?t=40)

#### Persistence Ignorance

使用仓储模式的一个核心动机是持久化无关性。
这意味着在对领域建模和编写业务逻辑时,我们不应关心对象是如何存储的。
我们根据领域逻辑的需要来定义接口及其所需的方法。
基础设施和持久化逻辑的技术细节只在之后才处理,从而让领域层保持整洁并专注于业务规则。

#### Usage in Services

仓储主要由服务使用,包括应用服务和领域服务。
领域服务使用仓储从数据库获取聚合,以执行复杂的领域特定操作。
应用服务使用仓储来编排领域对象,如下面的例子所示,其中创建了一个健身房并将其与一个订阅关联:

```csharp
public async Task<ErrorOr<Gym>> Handle(CreateGymCommand command, CancellationToken cancellationToken)
{
    var subscription = await _subscriptionsRepository.GetByIdAsync(command.SubscriptionId);

    if (subscription is null)
    {
        return Error.NotFound(description: "Subscription not found");
    }

    var gym = new Gym(command.Name, subscription.GetMaxRooms(), subscription.Id);

    var addGymResult = subscription.AddGym(gym);

    if (addGymResult.IsError)
    {
        return addGymResult.Errors;
    }

    await _subscriptionsRepository.UpdateAsync(subscription);

    return gym;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/repositories-54123365/?t=235)

#### Repositories and Aggregates

仓储必须只与聚合根打交道。
由于聚合充当一致性边界,它们必须作为单个单元从数据库读取和写入数据库。
这确保聚合在被检索时处于有效状态,并在被持久化回存储时仍保持有效状态。
通过把仓储限制在聚合根上,我们在整个应用程序中强制执行这些一致性边界,并确保聚合的内部对象永远不会被单独操作。

## 15. Disconnected Domain Models

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/disconnected-domain-models-54123366/) · 1:40

断开连接的领域模型(disconnected domain model)是一种模式,在这种模式中,仓储被直接传入聚合中的某个方法,用来获取外部数据。
这种做法不同于传入简单的值提供器(例如 `IDateTimeProvider`),因为它涉及从领域实体内部直接与数据持久化层交互。

### 核心概念

*   **Definition**:把仓储传给聚合的方法,使聚合能够获取自己的依赖。
*   **Lazy Loading**:这种模式是一种延迟加载,只在特定方法执行时才检索数据。
*   **Design Smell**:在聚合内部使用仓储通常表明该逻辑应属于领域服务,或者聚合边界的建模不正确。

### 课程笔记

在领域驱动设计中,"断开连接的领域模型"指的是这样一种做法:把仓储接口传入聚合的某个方法,从而为聚合提供获取其相关数据的手段。
这通常用于避免预先加载大量可能并非每个操作都需要的数据。

例如,`Session` 聚合可能有一个与教练交互的方法。
该方法不是让 `Session` 持有对教练的完整引用,也不是要求传入教练,而是直接接受仓储本身:

```csharp
public class Session : AggregateRoot
{
    public void HighFiveTrainer(
        ITrainersRepository trainersRepository)
    {
        var trainer = trainersRepository.GetById(_trainerId);
        trainer.HighFive("✋");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/disconnected-domain-models-54123366/?t=10)

这种实现是一种延迟加载;只有在调用 `HighFiveTrainer` 方法时,才会从数据库获取 `trainer`。
虽然这种模式能用,但它常常是领域模型需要改进的信号。

当聚合需要仓储来执行某个操作时,这往往表明该逻辑并不真正属于这个聚合。
相反,这一逻辑可能更适合放在领域服务中,由领域服务来协调获取所需的实体以及它们之间的交互。
或者,这可能表明聚合边界没有被正确定义,因为聚合为了履行自身职责而伸手到了自身边界之外。

## 16. Repositories and EF Core

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/repositories-and-ef-core-54123367/) · 3:18

### 总结

本课讨论关于把仓储模式与 Entity Framework Core(EF Core)结合使用的架构争论。
虽然 EF Core 的 DbContext 和 DbSet 已经提供了类似仓储的抽象,但在领域驱动设计中,通常更倾向于实现自定义的仓储接口,以维持"内存集合"的假象。
这种方式封装了复杂的持久化逻辑(例如多步查询和 SQL 优化),防止持久化特定的细节泄漏到领域层中。

### 核心概念

- **Redundancy Debate**:EF Core 的 DbContext 已经实现了仓储模式,这让一些人认为额外的仓储层是多余的。
- **In-Memory Illusion**:DDD 中仓储的首要目标是让持久化层表现得像一个内存中的对象集合。
- **Encapsulation of Complexity**:自定义的仓储方法可以把复杂的数据检索逻辑(例如连接多个表或应用特定过滤条件)隐藏在一个单一、整洁的接口之后。
- **Separation of Concerns**:使用专门的接口可以防止领域逻辑与 EF Core 特定的语法和 LINQ-to-SQL 转换紧密耦合。

### 课程笔记

开发社区中对于在使用 Entity Framework Core 时是否有必要使用仓储模式存在大量讨论。
由于 DbContext 已经充当了仓储和工作单元,再用另一个接口把它包装起来,可能显得是一层不必要的抽象。
在直接使用 EF Core 的方式中,人们只需在应用服务中操作 DbSet 并调用 SaveChangesAsync 即可。

然而,在具有复杂领域逻辑的应用程序中,自定义的仓储接口通常是更好的默认选择。
这个接口强化了领域驱动设计的原则:持久化层应该感觉就像一个内存中的集合。
与编写明确为 SQL 转换而设计的代码相比,通过 AddSessionAsync 或 GetByIdAsync 这样的方法进行交互,更容易对领域进行推理。

```csharp
public interface ISessionsRepository
{
    Task AddSessionAsync(Session session);

    Task<Session?> GetByIdAsync(Guid id);

    Task<List<Session>> ListByIdsAsync(IReadOnlyList<Guid> sessionIds, DateTime? startDateTime = null, DateTime? endDateTime = null);

    Task<List<Session>> ListByGymIdAsync(Guid gymId, DateTime? startDateTime = null, DateTime? endDateTime = null);

    Task UpdateAsync(Session session);

    Task<List<Session>> ListByRoomIdAsync(Guid roomId);

    Task RemoveRangeAsync(List<Session> sessions);
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/repositories-and-ef-core-54123367/?t=10)

在处理复杂查询时,这种抽象的价值就变得清晰起来。
例如,ListByGymIdAsync 这样的方法可能需要几个步骤:获取与某个健身房关联的所有房间,然后获取这些房间内的所有课程,最后按日期范围过滤这些课程。
通过把这一逻辑封装在仓储实现中,应用程序的其余部分就不需要知道底层的 SQL 优化或关系型数据检索的复杂性。

```text
ListByGymIdAsync(gymId, startDateTime, endDateTime)
  1. gymId    --> 该健身房关联的所有 rooms
  2. rooms    --> 这些 rooms 内的所有 sessions
  3. sessions --> 按日期范围过滤
```

虽然可以直接使用 EF Core,但在复杂的 DDD 项目中,定义自定义的仓储接口能为领域提供更清晰的边界,并简化与持久化层的交互。

## 17. Implementing a Repository

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-a-repository-54123368/) · 3:14

### 总结

仓储模式是一种战术性的领域驱动设计模式,用于抽象领域聚合的持久化和检索。
虽然仓储通常在领域层中定义,但本课探讨"断开连接的领域模型"方式:如果仓储接口没有被领域实体或领域服务直接使用,就把它们放在应用层中。
这一策略让领域层严格专注于业务逻辑,并让接口尽可能靠近其实际使用者,例如应用服务。

### 核心概念

- **Repository Pattern**:一种在领域层与数据映射层之间进行协调的抽象,其行为就像一个内存中的领域对象集合。
- **Interface Placement**:在最常使用仓储接口的那一层(Domain 还是 Application)中定义它们的做法。
- **Disconnected Domain Model**:一种设计,其中领域聚合不持有对仓储的引用,也不直接使用仓储,而是依靠应用服务来处理持久化。
- **Project Organization**:在专门的 `Common/Interfaces` 目录中组织公共接口,以保持架构清晰。

### 课程笔记

要实现仓储模式,首先要定义一个接口,来表示持久化某个聚合的契约。
在这个例子中,`ISessionsRepository` 最初创建在领域项目的 `Common/Interfaces` 下。
这个接口定义了用于添加和更新 `Session` 聚合的异步方法。

```csharp
using DomeGym.Domain.SessionAggregate;

namespace DomeGym.Domain.Common.Interfaces;

public interface ISessionsRepository
{
    Task AddSessionAsync(Session session);
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-a-repository-54123368/?t=40)

随着对仓储的需求增长,`UpdateSessionAsync` 等更多方法被添加到接口中。
把它们定义为接口,能让领域层对底层数据库实现保持无关,并通过模拟(mocking)让单元测试更容易进行。

```csharp
using DomeGym.Domain.SessionAggregate;

namespace DomeGym.Domain.Common.Interfaces;

public interface ISessionsRepository
{
    Task AddSessionAsync(Session session);
    Task UpdateSessionAsync(Session session);
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-a-repository-54123368/?t=55)

#### Architectural Placement of Repositories

DDD 中一个常见的架构决策是:仓储接口应属于领域层还是应用层。
如果领域对象(实体或领域服务)不需要使用仓储,该模型就被视为"断开连接的领域模型"。
在这种情况下,通常更倾向于把仓储接口放在应用层中,因为它主要会在那里被应用服务使用。

如果将来的某个需求要求在领域层中使用仓储,可以把接口移回领域项目。
为了便于这种分离,创建了一个新的应用项目,并把领域项目添加为引用。

```shell
dotnet new classlib -o DomeGym.Application
dotnet add DomeGym.Application reference DomeGym.Domain
dotnet sln add **/**.csproj
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-a-repository-54123368/?t=145)

应用项目建立之后,`ISessionsRepository` 接口被迁移到应用层。
命名空间也被更新,以反映它在 `DomeGym.Application.Common.Interfaces` 中的新位置。

```csharp
using DomeGym.Domain.SessionAggregate;

namespace DomeGym.Application.Common.Interfaces;

public interface ISessionsRepository
{
    Task AddSessionAsync(Session session);
    Task UpdateSessionAsync(Session session);
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/implementing-a-repository-54123368/?t=175)

## 18. Section Recap

> [观看本课](https://dometrain.com/take/course/getting-started-domain-driven-design-ddd-2731785/section-recap-54123369/) · 1:16

### 总结

本课为战术设计这一部分作结,总结了实体、值对象和聚合等模式。
它介绍了领域事件,作为用于聚合间通信的最后一个战术模式,并强调在进入大量涉及基础设施的部分之前熟悉代码库的重要性。

### 核心概念

- **Tactical DDD Patterns**:Entities、Value Objects、Aggregates、Repositories 和 Factories。
- **Domain Events**:在聚合之间传达状态变更以维持一致性的机制。
- **Codebase Familiarity**:在引入镜头外编写的基础设施代码之前,先理解当前的领域逻辑。
- **Inter-aggregate Communication**:使用事件来解耦聚合,同时允许它们对彼此的变化做出反应。

### 课程笔记

对战术领域驱动设计模式的探索已接近完成。
现在的实现已经包含实体、值对象、聚合和仓储。
这些模式构成了领域层的核心,确保业务逻辑被封装,并在已定义的边界内维持一致性。

#### Domain Events

要探讨的最后一个战术模式是**领域事件**。
在一个聚合中的变更必须触发另一个聚合中的某个操作的场景中,领域事件是必不可少的。
第一个聚合不是直接调用另一个聚合(那样会造成紧耦合,并可能违反聚合边界),而是发布一个事件。
系统的其他部分随后可以订阅这个事件并执行必要的更新,从而促成整个领域的最终一致性。

```text
Aggregate A --(发布 domain event)--> 订阅者 --(执行更新)--> Aggregate B
```

#### Codebase Review

在进入下一部分之前,建议彻底回顾一下当前的代码库。
接下来的课程会引入大量在镜头外编写的基础设施和样板代码。
这些代码支撑着领域模型,但并不是所教授的 DDD 模式的核心重点。

准备工作:
- **检查当前状态**:回顾目前为止所讲战术模式的实现,确保对领域逻辑有扎实的理解。
- **跟踪执行**:使用断点和示例请求来理解领域模型在实践中的行为。
- **理解边界**:确保实体、值对象和聚合的角色和职责都很清楚。

扎实掌握当前的实现,会让你更容易理解后续部分中领域事件和基础设施组件是如何集成进来的。
