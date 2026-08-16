# Object-Oriented Programming

> Course: [Deep Dive: C#](https://dometrain.com/course/deep-dive-csharp/) · Chapter 3
> 10 lessons · ~2:07:07
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Inheritance - Intro](https://dometrain.com/take/course/deep-dive-csharp-2732260/inheritance-intro-54135470/) | 9:24 | [↓](#1-inheritance---intro) |
| 2 | [Inheritance - Vehicle Example](https://dometrain.com/take/course/deep-dive-csharp-2732260/inheritance-vehicle-example-54135471/) | 11:33 | [↓](#2-inheritance---vehicle-example) |
| 3 | [Interfaces](https://dometrain.com/take/course/deep-dive-csharp-2732260/interfaces-54135472/) | 13:12 | [↓](#3-interfaces) |
| 4 | [Abstract Classes](https://dometrain.com/take/course/deep-dive-csharp-2732260/abstract-classes-54135473/) | 12:30 | [↓](#4-abstract-classes) |
| 5 | [Working with Protected and Virtual](https://dometrain.com/take/course/deep-dive-csharp-2732260/working-with-protected-and-virtual-54135475/) | 12:10 | [↓](#5-working-with-protected-and-virtual) |
| 6 | [Composition](https://dometrain.com/take/course/deep-dive-csharp-2732260/composition-54135476/) | 13:36 | [↓](#6-composition) |
| 7 | [Head to Head - Inheritance](https://dometrain.com/take/course/deep-dive-csharp-2732260/head-to-head-inheritance-54135477/) | 10:11 | [↓](#7-head-to-head---inheritance) |
| 8 | [Head to Head - Composition & Summary](https://dometrain.com/take/course/deep-dive-csharp-2732260/head-to-head-composition-summary-54135478/) | 9:52 | [↓](#8-head-to-head---composition--summary) |
| 9 | [Generics](https://dometrain.com/take/course/deep-dive-csharp-2732260/generics-54135479/) | 16:49 | [↓](#9-generics) |
| 10 | [Tuples](https://dometrain.com/take/course/deep-dive-csharp-2732260/tuples-54135480/) | 17:50 | [↓](#10-tuples) |

---

## 1. Inheritance - Intro

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/inheritance-intro-54135470/) · 9:24

### Summary

Inheritance is a fundamental pillar of object-oriented programming used to model "is-a" relationships between types.
By creating a hierarchy where generic characteristics are defined in a base class and specific details are implemented in derived classes, developers can reduce code redundancy and establish clear logical structures.
While inheritance is a powerful tool for sharing functionality, it should be balanced with composition to ensure flexible software design.

### Key concepts

* **"Is-a" Relationship**: Inheritance models a relationship where a derived type is a specialized version of a base type.
* **Base Class (Parent)**: The class at the top of the hierarchy that contains common properties and methods.
* **Derived Class (Child)**: The class that inherits from the base class, gaining its characteristics and potentially adding its own.
* **Code Reuse**: Pushing common functionality up into a base class prevents duplication across multiple related classes.
* **Hierarchical Specialization**: Moving from generic definitions at the top of a hierarchy to highly specific implementations at the bottom.

### Lesson notes

Inheritance allows developers to model systems by defining tangible, hierarchical relationships.
In C#, this is expressed through the "is-a" relationship.
For example, in a biological model, an `Animal` serves as a generic base class.
More specific categories like `Mammal` or `Amphibian` inherit from `Animal`, and even more specific types like `Dog` or `Cat` inherit from `Mammal`.

Characteristics common to all mammals, such as `HairColor`, are defined in the `Mammal` class.
Because `Dog` and `Cat` inherit from `Mammal`, they automatically possess the `HairColor` property.
This prevents the need to define the same property multiple times in every specific animal class.
When an instance of a `Cat` is created, it can have its own unique value for the inherited `HairColor` property, but the definition of that property resides in the parent class.

This concept is frequently applied to mechanical systems, such as vehicles.
A base `Vehicle` class can define properties common to most vehicles, such as a `DoorCount`.
Specific types like `Automobile`, `Bike`, or `Plane` then inherit from this base.

```csharp
public class Vehicle
{
    public int DoorCount { get; init; }

    public void OpenDoors()
    {
        Console.WriteLine(
            $"{GetType().Name} opening {DoorCount} doors!");
    }
}

public class Automobile : Vehicle
{
}

public class Car : Automobile
{
}

public class Truck : Automobile
{
}

public class Van : Automobile
{
}

public class Bike : Vehicle
{
    public Bike()
    {
        DoorCount = 0;
    }
}

public class Plane : Vehicle
{
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/inheritance-intro-54135470/?t=558)

In the example above, `Car` inherits from `Automobile`, which in turn inherits from `Vehicle`.
This creates a chain where a `Car` "is an" `Automobile` and also "is a" `Vehicle`.
Consequently, the `Car` class has access to the `DoorCount` property and the `OpenDoors()` method defined in the `Vehicle` class.

When designing these hierarchies, the placement of properties is critical.
If a property like `DoorCount` is placed in the `Vehicle` base class, every derived class must account for it.
For a `Bike`, which does not have doors, the property is still inherited, but it can be initialized to zero in the constructor to reflect the specific nature of that object.
If a concept is not applicable to all branches of a hierarchy, it may be more appropriate to move that property down into a more specific sub-class (e.g., moving `WingSpan` to a `Plane` class rather than keeping it on `Vehicle`).

---

## 2. Inheritance - Vehicle Example

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/inheritance-vehicle-example-54135471/) · 11:33

### Summary

This lesson explores the implementation of class inheritance in C# through a vehicle modeling example.
It details the syntax for establishing parent-child relationships, the behavior of inherited properties and methods, and the importance of the "is-a" relationship over mere code reuse.

### Key concepts

- **Inheritance Syntax**: Using the colon (`:`) operator to establish a relationship between a base class and a derived class.
- **Single Inheritance**: C# classes can only inherit from one parent class.
- **Is-A Relationship**: Inheritance should represent a logical hierarchy where a child class is a specialized version of the parent.
- **Member Propagation**: Public properties and methods defined in a base class are automatically available to all derived classes.
- **Type Resolution**: The `GetType()` method identifies the most specific runtime type of an instance, even when called from logic defined in a base class.
- **DRY Principle Trap**: Warning against using inheritance solely to avoid code duplication without a valid hierarchical relationship.

### Lesson notes

In C#, inheritance allows for the creation of a class hierarchy where a base class serves as the root for more specialized derived classes.
In a vehicle model, the `Vehicle` class acts as the base class.
Specialized types like `Automobile`, `Bike`, and `Plane` inherit from `Vehicle`, while further specializations like `Car` and `Truck` inherit from `Automobile`.

```csharp
namespace ObjectOrientedProgramming;

public class Vehicle
{
}

public class Automobile : Vehicle
{
}

public class Car : Automobile
{
}

public class Truck : Automobile
{
}

public class Bike : Vehicle
{
}

public class Plane : Vehicle
{
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/inheritance-vehicle-example-54135471/?t=25)

The relationship is established using the colon (`:`) syntax in the class declaration.
It is critical to note that C# does not support multiple inheritance for classes; a class can only have one direct parent.
This hierarchy can be extended as needed to model specific application requirements.

```csharp
namespace ObjectOrientedProgramming;

public class Vehicle
{
}

public class Automobile : Vehicle
{
}

public class Car : Automobile
{
}

public class Truck : Automobile
{
}

public class Van : Automobile
{
}

public class Bike : Vehicle
{
}

public class Plane : Vehicle
{
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/inheritance-vehicle-example-54135471/?t=115)

When a property is added to a base class, it is inherited by all child classes.
For example, adding a `DoorCount` property to the `Vehicle` class makes it available to `Automobile`, `Car`, `Bike`, and all other descendants.

```csharp
namespace ObjectOrientedProgramming;

public class Vehicle
{
    public int DoorCount { get; init; }
}

public class Automobile : Vehicle
{
}

public class Car : Automobile
{
}

public class Truck : Automobile
{
}

public class Van : Automobile
{
}

public class Bike : Vehicle
{
}

public class Plane : Vehicle
{
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/inheritance-vehicle-example-54135471/?t=180)

Instances of these classes can then be initialized with specific values for inherited properties.
A `Car` instance representing a sedan might be initialized with four doors, while a coupe might have two.

```csharp
Car sedan = new() { DoorCount = 4 };
Car coupe = new() { DoorCount = 2 };
Truck pickupTruck = new() { DoorCount = 2 };

public class Vehicle
{
    public int DoorCount { get; init; }
}

public class Automobile : Vehicle
{
}

public class Car : Automobile
{
}

public class Truck : Automobile
{
}

public class Van : Automobile
{
}

public class Bike : Vehicle
{
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/inheritance-vehicle-example-54135471/?t=220)

In cases where a property value is constant for a specific type, such as a `Bike` always having zero doors, the value can be set within the child class's constructor.

```csharp
public class Car : Automobile
{
}

public class Truck : Automobile
{
}

public class Van : Automobile
{
}

public class Bike : Vehicle
{
    public Bike()
    {
        DoorCount = 0;
    }
}

public class Plane : Vehicle
{
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/inheritance-vehicle-example-54135471/?t=280)

Methods defined in the base class are also inherited.
A method like `OpenDoors` can use `GetType().Name` to access the name of the most specific runtime type of the instance.
Even if the method is defined in `Vehicle`, calling it on a `Car` instance will correctly identify the type as "Car".

```csharp
Car sedan = new() { DoorCount = 4 };
Car coupe = new() { DoorCount = 2 };
Truck pickupTruck = new() { DoorCount = 2 };
Bike Bike = new();

sedan.OpenDoors();
coupe.OpenDoors();
pickupTruck.OpenDoors();
Bike.OpenDoors();

public class Vehicle
{
    public int DoorCount { get; init; }

    public void OpenDoors()
    {
        Console.WriteLine(
            $"{GetType().Name} opening {DoorCount} doors!");
    }
}

public class Automobile : Vehicle
{
}

public class Car : Automobile
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/inheritance-vehicle-example-54135471/?t=355)

While inheritance is a powerful tool for code reuse, developers should be wary of the "Don't Repeat Yourself" (DRY) trap.
Pulling code into a base class simply because it is common across multiple classes can lead to bloated base classes and fragile hierarchies.
Inheritance should primarily be used when a clear "is-a" relationship exists between the types.

---

## 3. Interfaces

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/interfaces-54135472/) · 13:12

Interfaces in C# define a contract or API that specifies how to interact with an object without providing the actual implementation.
While C# classes are limited to single inheritance, interfaces allow for a form of multiple inheritance where a single class can adhere to multiple behavioral contracts.

### Key concepts

*   **Contractual Definition**: Interfaces define properties and methods that an implementing class must provide.
*   **Naming Convention**: Interface names typically begin with a capital "I" (e.g., `IHasDoors`).
*   **Implicit Visibility**: Interface members are public by default because they represent the external API of the implementing object.
*   **Multiple Implementation**: A single class can implement multiple interfaces, even though it can only inherit from one base class.
*   **Decoupling**: Programming against interfaces allows logic to remain agnostic of specific concrete implementations.

### Lesson notes

An interface is defined using the `interface` keyword.
It contains member signatures—such as properties and methods—but lacks the bodies for those methods.
Any class that implements an interface is required to implement every member defined within it; it cannot pick and choose specific members.

```csharp
// interfaces in C# are a way to define a contract or API
// things that implement the interface must
// implement ALL of the members of the interface

public interface IHasDoors
{

    int NumberOfDoors { get; }

    void OpenDoor(int doorIndex);

    void CloseDoor(int doorIndex);

    bool IsDoorOpen(int doorIndex);
}

public interface IMotorized
{

    bool IsEngineRunning { get; }


    void StartEngine();


    void StopEngine();
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/interfaces-54135472/?t=85)

When a class implements an interface, it uses the same colon syntax as class inheritance.
For example, a `Motorcycle` class might implement `IMotorized` because it has an engine, but it would not implement `IHasDoors` because motorcycles do not typically have doors.
The implementing class must provide the logic for the properties and methods defined in the interface.

```csharp
// interfaces in C# are a way to define a contract or API
// things that implement the interface must
// implement ALL of the members of the interface

public class Bicycle...

public class Motorcycle : IMotorized
{
    public bool IsEngineRunning { get; private set; }

    public void StartEngine()
    {
        if (IsEngineRunning)
        {
            return;
        }

        IsEngineRunning = true;
        Console.WriteLine("Engine started!");
    }

    public void StopEngine()
    {
        if (!IsEngineRunning)
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/interfaces-54135472/?t=280)

C# supports implicit casting from a concrete class to any interface it implements.
This is useful for passing objects into methods that require a specific behavior rather than a specific type.
However, converting from an interface back to a concrete type (downcasting) requires an explicit cast, which can result in runtime exceptions if the object is not actually the expected type.

```csharp
// interfaces in C# are a way to define a contract or API
// things that implement the interface must
// implement ALL of the members of the interface
Motorcycle motorcycle = new();
motorcycle.StartEngine();
Console.WriteLine(motorcycle.IsEngineRunning); // False

motorcycle.StopEngine();
Console.WriteLine(motorcycle.IsEngineRunning); // False

motorcycle.StopEngine();
Console.WriteLine(motorcycle.IsEngineRunning); // False

IMotorized motorized = motorcycle;
Motorcycle motorcycle2 = (Motorcycle)motorized;

public class Bicycle...



public class Motorcycle : IMotorized
{
    public bool IsEngineRunning { get; private set; }

    public void StartEngine()
    {
        if (IsEngineRunning)
        {
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/interfaces-54135472/?t=445)

One of the primary advantages of interfaces is the ability to implement multiple contracts on a single class.
A `Car` class, for instance, can implement both `IHasDoors` and `IMotorized`.
This allows the `Car` to be used by any logic that requires either of those specific behaviors.

```csharp
// interfaces in C# are a way to define a contract or API
// things that implement the interface must
// implement ALL of the members of the interface
Car coupe = new(2);
Car sedan = new(4);

public class Car :
    IHasDoors,
    IMotorized
{
    private readonly bool[] _doors;

    public Car(int numberOfDoors)
    {
        // NOTE: we're not doing any error checking here
        // or in the rest of this class...
        _doors = new bool[numberOfDoors];
    }

    public bool IsEngineRunning { get; private set; }

    public int NumberOfDoors => _doors.Length;

    public void OpenDoor(int doorIndex)
    {
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/interfaces-54135472/?t=520)

By writing methods that accept interface types as parameters, you create highly reusable code.
A method like `TestIgnition` can accept any `IMotorized` object—whether it is a `Car`, `Motorcycle`, or even a non-vehicle like a generator—and interact with it through the defined contract without needing to know its specific class type.

```csharp
// interfaces in C# are a way to define a contract or API
// things that implement the interface must
// implement ALL of the members of the interface
Car coupe = new(2);
Car sedan = new(4);

TestIgnition(coupe);
TestIgnition(sedan);

TestDoors(coupe);
TestDoors(sedan);

void TestIgnition(IMotorized motorized)
{
    motorized.StartEngine();
    Console.WriteLine($"Engine is running: {motorized.IsEngineRunning}");

    motorized.StopEngine();
    Console.WriteLine($"Engine is running: {motorized.IsEngineRunning}");
}

void TestDoors(IHasDoors hasDoors)
{
    for (int i = 0; i < hasDoors.NumberOfDoors; i++)
    {
        hasDoors.OpenDoor(i);
        Console.WriteLine($"Door {i} is open: {hasDoors.IsDoorOpen(i)}");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/interfaces-54135472/?t=670)

---

## 4. Abstract Classes

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/abstract-classes-54135473/) · 12:30

### Summary

Abstract classes in C# provide a mechanism to define base templates that cannot be instantiated directly, allowing for a mix of shared implementation logic and required method signatures.
They bridge the gap between interfaces and concrete classes, requiring derived types to implement abstract members using the override keyword while permitting the inheritance of existing functionality.
This structure supports complex hierarchies where implementation responsibilities can be fulfilled immediately or deferred to further descendants.

### Key concepts

*   **Instantiation**: Abstract classes cannot be created using the `new` keyword; they must be inherited.
*   **Abstract Methods**: Signatures defined in an abstract class without a body, forcing derived classes to provide an implementation.
*   **Concrete Logic**: Unlike interfaces, abstract classes can contain fully implemented methods that are shared across all derived types.
*   **The override Keyword**: Required in a derived class to implement an abstract method from a base class.
*   **Inheritance Hierarchies**: Abstract classes can inherit from other abstract classes or interfaces, allowing for the layering of functionality.
*   **Deferred Implementation**: An abstract class can defer the implementation of an interface or base abstract method by re-declaring it as abstract.

### Lesson notes

Abstract classes are defined using the `abstract` keyword.
They serve as a blueprint for other classes and cannot be instantiated directly.
This makes them similar to interfaces, but with the added capability of providing base functionality that inheriting classes can access and reuse.

```csharp
abstract class MyBaseClass
{
    public void Print()
    {
        Console.WriteLine("Print() in MyBaseClass");
    }

    public abstract void PrintAbstract();
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/abstract-classes-54135473/?t=25)

An abstract method, such as `PrintAbstract` in the example above, defines a signature but contains no method body.
This acts as a contract, suggesting to any inheriting class that it must provide its own implementation of that method.

When a class derives from an abstract class, it must use the `override` keyword to implement the abstract members.
If the `override` keyword is missing, the compiler will not recognize the method as the required implementation of the abstract member.
Furthermore, if a derived class fails to implement an abstract member, the code will not compile.

```csharp
class MyDerivedClass : MyBaseClass
{
    // we *MUST* implement the abstract method
    // when we derive from an abstract class!
    public override void PrintAbstract()
    {
        Console.WriteLine("PrintAbstract() in MyDerivedClass");
    }
}

// let's go run a quick example of this:
MyDerivedClass myDerivedClass = new MyDerivedClass();
myDerivedClass.Print();
myDerivedClass.PrintAbstract();
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/abstract-classes-54135473/?t=190)

Abstract classes can also inherit from other abstract classes and implement interfaces.
If an abstract class implements an interface, it can provide a concrete implementation for the interface methods, which will then be available to all classes further down the inheritance chain.

```csharp
interface IMyInterface
{
    void PrintInterface();
}

abstract class MyDerivedAbstractClass :
    MyBaseClass,
    IMyInterface
{
    public void PrintInterface()
    {
        Console.WriteLine("PrintInterface() in MyDerivedAbstractClass");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/abstract-classes-54135473/?t=520)

In some scenarios, an abstract class may not be the appropriate place to implement an interface method.
In these cases, the abstract class can forward the responsibility to its descendants by marking the interface method as `abstract` within its own definition.
This ensures that the first concrete class to inherit from it must provide the implementation.

```csharp
abstract class MyDerivedAbstractClass :
    MyBaseClass,
    IMyInterface
{
    public abstract void PrintInterface();
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/abstract-classes-54135473/?t=610)

When a concrete class finally inherits from this chain, it is responsible for overriding every abstract method defined in its ancestry.

```csharp
class MyDerivedClass2 : MyDerivedAbstractClass
{
    // note that we must implement BOTH the abstract methods!
    public override void PrintAbstract()
    {
        Console.WriteLine("PrintAbstract() in MyDerivedClass2");
    }

    public override void PrintInterface()
    {
        Console.WriteLine("PrintInterface() in MyDerivedClass2");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/abstract-classes-54135473/?t=640)

While abstract classes are a powerful tool for sharing logic, they should be used with caution.
Over-reliance on abstract classes to satisfy the "Don't Repeat Yourself" (DRY) principle can lead to rigid hierarchies and bloated base classes.
It is often beneficial to consider composition as an alternative to inheritance to keep codebases maintainable.

---

## 5. Working with Protected and Virtual

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/working-with-protected-and-virtual-54135475/) · 12:10

### Summary

The protected and virtual keywords in C# provide advanced control over inheritance and visibility within object hierarchies.
The protected access modifier restricts member access to the containing class and any derived classes, allowing for shared internal implementation details that remain hidden from external consumers.
The virtual keyword allows a base class to provide a default implementation for a method or property while granting derived classes the option to override it with specialized behavior.
This distinguishes virtual from abstract, which mandates implementation in descendants without providing a base version.

### Key concepts

*   **protected Access Modifier**: Limits visibility to the containing class and any classes that derive from it.
*   **virtual Keyword**: Defines a method or property with a base implementation that can be optionally overridden in derived classes.
*   **abstract vs. virtual**: Abstract members have no implementation and must be overridden; virtual members have an implementation and may be overridden.
*   **base Keyword**: Used to explicitly call members of the parent class, particularly to access the base implementation of an overridden method.
*   **Access Restrictions**: Virtual and abstract members cannot be private because they must be accessible to derived classes for overriding.

### Lesson notes

C# inheritance often requires sharing functionality between base and derived classes without exposing that functionality to external callers.
The `protected` access modifier serves this purpose by limiting visibility to the class hierarchy itself.
This is useful for implementation details that should be shared among descendants but hidden from the public API.

```csharp
abstract class AbstractBaseClass
{
    protected void ProtectedPrintInBaseClass()
    {
        Console.WriteLine("ProtectedPrintInBaseClass");
    }

    protected abstract void ProtectedAbstractPrint();
}

class DerivedClass : AbstractBaseClass
{
    public void PrintInDerivedClass()
    {
        Console.WriteLine("We're in the derived class!");
        base.ProtectedPrintInBaseClass();
        Console.WriteLine("We're leaving the method in the derived class!");
    }

    protected override void ProtectedAbstractPrint()
    {
        Console.WriteLine("ProtectedAbstractPrint");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/working-with-protected-and-virtual-54135475/?t=145)

In the example above, `ProtectedPrintInBaseClass` is accessible within `DerivedClass` but cannot be called by an instance of `DerivedClass` from the outside.
This allows developers to encapsulate implementation details that are only relevant to the internal workings of the hierarchy.

The `virtual` keyword provides a hybrid between standard methods and abstract methods.
While an `abstract` method forces a derived class to provide an implementation, a `virtual` method provides a base implementation that the derived class may optionally override using the `override` keyword.

```csharp
class BaseClass2
{
    protected void PrintInBaseClass()
    {
        Console.WriteLine("PrintInBaseClass");
    }

    public virtual void VirtualPrintInBaseClass()
    {
        Console.WriteLine("VirtualPrintInBaseClass");
    }
}

class DerivedClass2 : BaseClass2
{
    public void PrintInDerivedClass()
    {
        Console.WriteLine("PrintInDerivedClass");
        PrintInBaseClass();
    }

    public override void VirtualPrintInBaseClass()
    {
        Console.WriteLine("VirtualPrintInBaseClass in the derived class");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/working-with-protected-and-virtual-54135475/?t=460)

When defining `virtual` or `abstract` members, they cannot be marked as `private`.
Because these members are intended to be overridden by other classes, they must be visible to those classes.
The most restrictive access modifier allowed for a virtual member is `protected`.

When a derived class overrides a virtual method, it can still access the original base implementation using the `base` keyword.
This is essential when the derived implementation needs to extend rather than completely replace the base behavior.

```csharp
class DerivedClass2 : BaseClass2
{
    public void PrintInDerivedClass()
    {
        Console.WriteLine("PrintInDerivedClass... then base!");
        PrintInBaseClass();
    }

    public override void VirtualPrintInBaseClass()
    {
        Console.WriteLine("VirtualPrintInBaseClass in the derived class");
        Console.WriteLine("... and now we'll call the base class method!");
        base.VirtualPrintInBaseClass();
    }
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/working-with-protected-and-virtual-54135475/?t=505)

Using `base.MethodName()` explicitly directs the call to the parent class implementation.
Without the `base` prefix, calling the method name from within its own override would result in an infinite recursive loop, as the method would continuously call itself.

---

## 6. Composition

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/composition-54135476/) · 13:36

### Summary

Composition is an object-oriented programming paradigm that models an "is made of" relationship, contrasting with the "is a" relationship of inheritance.
It involves building complex objects by combining smaller, simpler objects as components.
This approach provides a tangible way to model systems, where a larger entity is physically constructed from discrete parts.
By using composition, developers can encapsulate the interactions between internal components, exposing a simplified interface to the caller while hiding the underlying complexity and specific execution order of the component behaviors.

### Key concepts

* **"Is made of" relationship**: Composition models how an object is constructed from other objects, unlike inheritance which models hierarchies.
* **Tangible Modeling**: Composition often feels more physical and practical than the conceptual nature of inheritance.
* **Encapsulation**: Composite objects hide the complexity of their internal parts, exposing only necessary high-level methods.
* **Multi-level Composition**: Components themselves can be composed of even smaller objects, allowing for deep nesting.
* **Code Efficiency**: Modern C# features like primary constructors and records can significantly reduce the boilerplate code required to implement composition.

### Lesson notes

Composition allows for the creation of complex objects by aggregating simpler ones.
While inheritance models conceptual relationships (e.g., a cat is a mammal), composition models physical or structural relationships (e.g., a computer is made of a case and a motherboard).

To model a computer using composition, individual components are first defined as classes.
These classes contain specific behaviors relevant to that part.

```csharp
public sealed class Case
{
    public void PressPowerButton()
    {
        Console.WriteLine("Power button pressed");
    }
}

public sealed class Motherboard
{
    public void Boot()
    {
        Console.WriteLine("Booting...");
    }
}

public sealed class PowerSupply
{
    public void TurnOn()
    {
        Console.WriteLine("Power supply turned on");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/composition-54135476/?t=250)

Components can also maintain state.
For example, a hard drive might store its capacity, which is then used during its operations.

```csharp
public sealed class HardDrive
{
    private readonly int _sizeInTb;

    public HardDrive(int sizeInTb)
    {
        _sizeInTb = sizeInTb;
    }

    public void ReadData()
    {
        Console.WriteLine(
            $"Reading data from hard drive with capacity of {_sizeInTb} TB.");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/composition-54135476/?t=295)

Other components like RAM and Graphics Cards follow a similar pattern, providing specialized methods like `Load()` or `Render()`.

```csharp
public sealed class Ram
{
    private readonly int _sizeInGb;

    public Ram(int sizeInGb)
    {
        _sizeInGb = sizeInGb;
    }

    public void Load()
    {
        Console.WriteLine(
            $"Loading data into RAM with capacity of {_sizeInGb} GB.");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/composition-54135476/?t=325)

```csharp
public sealed class GraphicsCard
{
    public void Render()
    {
        Console.WriteLine("Rendering graphics");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/composition-54135476/?t=340)

The composite `Computer` class brings these parts together.
In a standard implementation, the constructor requires instances of every component, which are then stored in private fields.

```csharp
public sealed class Computer
{
    private readonly Case _theCase;
    private readonly Motherboard _motherboard;
    private readonly PowerSupply _powerSupply;
    private readonly HardDrive _hardDrive;
    private readonly Ram _ram;
    private readonly GraphicsCard _graphicsCard;

    public Computer(
        Case theCase,
        Motherboard motherboard,
        PowerSupply powerSupply,
        HardDrive hardDrive,
        Ram ram,
        GraphicsCard graphicsCard)
    {
        _theCase = theCase;
        _motherboard = motherboard;
        _powerSupply = powerSupply;
        _hardDrive = hardDrive;
        _ram = ram;
        _graphicsCard = graphicsCard;
    }
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/composition-54135476/?t=380)

C# primary constructors can drastically reduce this boilerplate.
By defining parameters directly on the class header, the compiler handles the underlying storage.
Note that classes using primary constructors have different behavior than records.

```csharp
public sealed class Computer(
    Case theCase,
    Motherboard motherboard,
    PowerSupply powerSupply,
    HardDrive hardDrive,
    Ram ram,
    GraphicsCard graphicsCard)
{
    public void PowerOn()
    {
        theCase.PressPowerButton();
        powerSupply.TurnOn();
        motherboard.Boot();
        ram.Load();
        hardDrive.ReadData();
        graphicsCard.Render();
    }
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/composition-54135476/?t=415)

#### Encapsulation and Complexity

A major advantage of composition is the ability to encapsulate complex logic.
Instead of requiring a caller to know the exact sequence of operations to start a computer, the composite object provides a single `PowerOn()` method.
This hides the implementation details and the specific order of component interaction.

```csharp
public void PowerOn()
{ 
    _theCase.PressPowerButton();
    _powerSupply.TurnOn();
    _motherboard.Boot();
    _ram.Load();
    _hardDrive.ReadData();
    _graphicsCard.Render();
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/composition-54135476/?t=460)

To use the composite object, the caller instantiates the components and passes them into the `Computer` constructor.
This makes the "is made of" relationship explicit in the code.

```csharp
Computer myComputer = new Computer(
    new Case(),
    new MotherBoard(),
    new PowerSupply(),
    new HardDrive(sizeInTb: 16),
    new Ram(sizeInGb: 64),
    new GraphicsCard());
myComputer.PowerOn();
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/composition-54135476/?t=610)

While this example is simplified, composition can be applied many levels deep.
A component like a `GraphicsCard` could itself be composed of other objects like `Gpu` or `Vram`, allowing for highly detailed and modular system designs.

---

## 7. Head to Head - Inheritance

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/head-to-head-inheritance-54135477/) · 10:11

### Summary

This lesson provides a comparative analysis of inheritance and composition by modeling a vehicle hierarchy in C#.
It demonstrates how to use abstract base classes to define a common API, such as starting an engine or opening doors, while using derived classes to implement specific behaviors or constraints.
The discussion highlights the architectural challenges of deep inheritance, including code duplication and the limitations of single inheritance, and introduces constructor-based configuration as a transitional step toward a more flexible composition-based design.

### Key concepts

- Abstract base classes for defining hierarchical roots.
- Enforcing implementation requirements via abstract methods.
- Specializing behavior in derived classes through method overrides.
- Handling domain-specific constraints (e.g., throwing exceptions for non-existent doors in a two-door vehicle).
- The limitations of single inheritance and the "Fragile Base Class" problem.
- Using protected methods and constructor configuration to reduce hierarchy depth.

### Lesson notes

The implementation begins with a `DoorPosition` enum to categorize vehicle doors.
The inheritance hierarchy is established with an abstract `Vehicle` class, which serves as the root ancestor.
An abstract `Automobile` class inherits from `Vehicle` and defines the mandatory API for all derived automobile types: starting the engine and opening doors.

```csharp
public enum DoorPosition
{
    FrontDriverSide,
    FrontPassengerSide,
    RearDriverSide,
    RearPassengerSide
}

//
// Inheritance
//
public abstract class Vehicle
{
}

public abstract class Automobile : Vehicle
{
    public abstract void StartEngine();

    public abstract void OpenDoor(DoorPosition doorPosition);
}

public abstract class Car : Automobile
{
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/head-to-head-inheritance-54135477/?t=100)

A `Car` class further specializes `Automobile`.
Although `Car` is abstract, it provides a concrete implementation for `StartEngine`, which is then inherited by its subclasses, `Sedan` and `Coupe`.
This allows all car types to share a default engine behavior.

```csharp
public abstract class Car : Automobile
{
    public override void StartEngine()
    {
        Console.WriteLine("Car starting engine 1.8L engine!");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/head-to-head-inheritance-54135477/?t=115)

Subclasses like `Sedan` and `Coupe` implement the `OpenDoor` method.
In this model, a `Sedan` supports all door positions, while a `Coupe` throws an `InvalidOperationException` if a caller attempts to open a rear door.
This illustrates how inheritance can be used to enforce physical constraints of the modeled object, though it requires the caller to handle potential exceptions for invalid operations.

```csharp
public class Sedan : Car
{
    public override void OpenDoor(DoorPosition doorPosition)
    {
        Console.WriteLine($"Sedan opening {doorPosition} door!");
    }
}

public class Coupe : Car
{
    public override void OpenDoor(DoorPosition doorPosition)
    {
        if (doorPosition == DoorPosition.RearDriverSide ||
            doorPosition == DoorPosition.RearPassengerSide)
        {
            throw new InvalidOperationException("Coupes only have two doors!");
        }

        Console.WriteLine($"Coupe opening {doorPosition} door!");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/head-to-head-inheritance-54135477/?t=175)

The hierarchy extends to other vehicle types like `PickupTruck` and `Van`.
A `Van` implementation demonstrates polymorphic behavior where the same method (`OpenDoor`) behaves differently based on the input; for instance, rear doors slide open while front doors swing open.

```csharp
    public class Van : Automobile
    {
        public override void StartEngine()
        {
            Console.WriteLine("Van starting engine 3.6L engine!");
        }

        public override void OpenDoor(DoorPosition doorPosition)
        {
            if (doorPosition == DoorPosition.RearDriverSide ||
                doorPosition == DoorPosition.RearPassengerSide)
            {
                Console.WriteLine($"Van sliding open {doorPosition} door!");
            }
            else
            {
                Console.WriteLine($"Van swinging open {doorPosition} door!");
            }
        }
    }
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/head-to-head-inheritance-54135477/?t=265)

As hierarchies grow, developers often face the "Fragile Base Class" problem or the need for multiple inheritance, which C# does not support.
To avoid duplicating code or creating excessively deep hierarchies, common logic can be pushed into the base class.
A more flexible approach involves refactoring the base class to accept configuration—such as an engine type—via its constructor.
This reduces the need for specialized subclasses for every variation and serves as a bridge toward composition.

```csharp
    public abstract class Automobile : Vehicle
    {
        private readonly string _engineType;

        protected Automobile(string engineType)
        {
            _engineType = engineType;
        }

        public void StartEngine()
        {
            StartEngine(_engineType);
        }

        public abstract void OpenDoor(DoorPosition doorPosition);

        protected static void StartEngine(string engineType)
        {
            Console.WriteLine($"Starting {engineType} engine!");
        }
    }
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/head-to-head-inheritance-54135477/?t=520)

---

## 8. Head to Head - Composition & Summary

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/head-to-head-composition-summary-54135478/) · 9:52

### Summary

This lesson concludes the comparison between inheritance and composition by demonstrating how to model complex objects like vehicles using a compositional approach.
By defining interfaces for components such as engines and doors, a single vehicle class can be configured to represent various types—including sedans, coupes, and vans—without a deep class hierarchy.
The lesson highlights the trade-offs between the two paradigms, noting that while composition offers greater flexibility and reduces type explosion, it often requires more upfront configuration, which can be managed through design patterns like Factory or Builder.

### Key concepts

*   **Interface-based composition**: Using interfaces like `IEngine` and `IDoor` to allow a class to be composed of different modular behaviors.
*   **Type reduction**: Replacing a deep inheritance hierarchy (Sedan, Coupe, Van) with a single `ComposedVehicle` class that changes behavior based on its injected components.
*   **Pass-through methods**: Methods in a composed class that simply delegate work to an internal component, serving as a basic form of encapsulation.
*   **Configuration overhead**: Composition requires more verbose instantiation code compared to inheritance, where configuration is often baked into the type itself.
*   **Hybrid paradigms**: Inheritance and composition are not mutually exclusive and can be combined to leverage the strengths of both.

### Lesson notes

To implement a vehicle using composition, the system first defines an interface for the engine.
This allows the vehicle to be composed of various engine implementations, such as a fixed V8 engine or a configurable engine with variable displacement.

```csharp
public interface IEngine
{
    void Start();
}

public class V8Engine : IEngine
{
    public void Start()
    {
        Console.WriteLine("Big ol' V8 engine starting!");
    }
}

public class ConfigurableEngine : IEngine
{
    private readonly float _displacementInLiters;

    public ConfigurableEngine(float displacementInLiters)
    {
        _displacementInLiters = displacementInLiters;
    }

    public void Start()
    {
        Console.WriteLine($"Starting {_displacementInLiters}L engine!");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/head-to-head-composition-summary-54135478/?t=40)

Similarly, doors are modeled using an interface to support different opening behaviors, such as standard swinging doors or sliding doors commonly found on vans.

```csharp
public interface IDoor
{
    void Open();
}

public class StandardSwingingDoor : IDoor
{
    public void Open()
    {
        Console.WriteLine($"Swinging opening door!");
    }
}

public class SlidingDoor : IDoor
{
    public void Open()
    {
        Console.WriteLine($"Sliding opening door!");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/head-to-head-composition-summary-54135478/?t=85)

The `ComposedVehicle` class uses these interfaces to define its structure.
It contains a single engine and a collection of doors.
Using a dictionary to map a `DoorPosition` enum to an `IDoor` instance allows the class to handle any number of doors at specific locations.
The methods `StartEngine` and `OpenDoor` encapsulate the internal components; `StartEngine` acts as a pass-through method to the engine, while `OpenDoor` manages the logic of retrieving the correct door from the collection.

```csharp
public sealed class ComposedVehicle
{
    private readonly IEngine _engine;
    private readonly IReadOnlyDictionary<DoorPosition, IDoor> _doors;

    public ComposedVehicle(
        IEngine engine,
        Dictionary<DoorPosition, IDoor> doors)
    {
        _engine = engine;
        _doors = doors;
    }

    public void StartEngine()
    {
        _engine.Start();
    }

    public void OpenDoor(DoorPosition doorPosition)
    {
        if (!_doors.TryGetValue(doorPosition, out IDoor? door))
        {
            throw new InvalidOperationException(
                $"There is no door at position {doorPosition}!");
        }

        Console.WriteLine($"Opening {doorPosition} door...");
        door.Open();
    }
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/head-to-head-composition-summary-54135478/?t=130)

When comparing instantiation, inheritance provides a very concise syntax because the configuration is built into the type definition.
A `Sedan` or `Van` class already "knows" what its engine and doors are.

```csharp
// Inheritance approach: Concise instantiation
Automobile sedan = new Sedan();
Automobile coupe = new Coupe();
Automobile pickupTruck = new PickupTruck();
Automobile van = new Van();
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/head-to-head-composition-summary-54135478/?t=205)

In contrast, composition requires explicit configuration during instantiation.
While this results in more code at the call site, it provides significantly more flexibility.
For example, a four-door pickup truck can be created using the same `ComposedVehicle` class as a two-door pickup, simply by passing a different dictionary of doors.
This avoids the "type explosion" often seen in deep inheritance hierarchies.

```csharp
// Composition approach: Flexible but verbose configuration
ComposedVehicle composedVan = new(
    new ConfigurableEngine(3.6f),
    new Dictionary<DoorPosition, IDoor>
    {
        { DoorPosition.FrontDriverSide, new StandardSwingingDoor() },
        { DoorPosition.FrontPassengerSide, new StandardSwingingDoor() },
        { DoorPosition.RearDriverSide, new SlidingDoor() },
        { DoorPosition.RearPassengerSide, new SlidingDoor() }
    });
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/head-to-head-composition-summary-54135478/?t=400)

To manage the verbosity of composition, developers can use the Factory pattern or the Builder pattern to create pre-configured instances or provide a more fluid construction syntax.

Finally, inheritance and composition can coexist.
A class can inherit from a base class to reuse existing logic while also using composition to inject specific behaviors.
An example is an `EngineSwapCoupe`, which inherits the door-validation logic of a `Coupe` but uses a composed `IEngine` to allow for engine customization.

```csharp
public abstract class EngineSwapCoupe : Coupe
{
    private readonly IEngine _engine;

    protected EngineSwapCoupe(IEngine engine)
    {
        _engine = engine;
    }

    public override void StartEngine()
    {
        _engine.Start();
    }
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/head-to-head-composition-summary-54135478/?t=475)

---

## 9. Generics

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/generics-54135479/) · 16:49

Generics in C# allow for the creation of classes, interfaces, and methods that operate with type parameters, acting as placeholders for actual types.
This approach facilitates code reusability and type safety by allowing logic to be defined once and applied to various data types without duplication.
By utilizing constraints via the where clause, developers can restrict generic types to those that implement specific interfaces or possess certain characteristics, such as being a reference type or having a parameterless constructor.
This ensures that the generic implementation can safely interact with the members of the underlying types while remaining flexible across different concrete implementations.

### Key concepts

- **Type Parameters**: Defined using angle brackets (e.g., `<T>`), these act as placeholders for types to be specified at instantiation or call time.
- **Generic Interfaces and Classes**: Structures that can handle any type while maintaining type safety.
- **Generic Methods**: Methods that define their own type parameters, allowing them to be used with various types regardless of whether the containing class is generic.
- **Generic Constraints**: The `where` keyword is used to limit the types that can be passed as a type parameter (e.g., requiring a type to implement a specific interface).
- **Type Inference**: The compiler's ability to determine the type parameter automatically based on the arguments passed to a generic method.
- **Generic Collections**: Standard .NET collections like `List<T>` and `Dictionary<TKey, TValue>` rely on generics to provide type-safe storage.

### Lesson notes

#### Defining Generic Types

Generics allow the definition of behaviors that require a type without being coupled to a specific one.
A type parameter, typically denoted as `T`, is defined within angle brackets (`<T>`) immediately following the name of the interface or class.
When a class implements a generic interface, it must either remain generic itself to pass the type parameter along or specify a concrete type for the interface.

```csharp
// we can use generics to define an interface with a type parameter T
public interface IGenericInterface<T>
{
    // no methods...
}

// we can make a class that implements this interface:
public class GenericClass<T> : IGenericInterface<T>
{
    // NOTE: the class itself also needs to have a type parameter on it
    // to allow caller creating instances of this class to specify the type
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/generics-54135479/?t=55)

To instantiate a generic class, the specific type must be provided in angle brackets.
If a class is defined as generic, it cannot be instantiated without specifying the type parameter.

```csharp
GenericClass<int> myNumericInstance = new GenericClass<int>();
GenericClass<string> myStringInstance = new GenericClass<string>();
//GenericClass instanceWithoutType = new GenericClass(); // this will not compile!!
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/generics-54135479/?t=145)

Alternatively, a non-generic class can implement a generic interface by providing a concrete type during the inheritance declaration.
This stops the "generic chain," and callers of the resulting class do not need to provide a type parameter.

```csharp
ImplementationWithIntegerType instanceOfImplementationWithIntegerType = new();

// we could also make an implementation that specifies the type
public class ImplementationWithIntegerType : IGenericInterface<int>
{
    // NOTE: the class itself also
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/generics-54135479/?t=265)

#### Generic Methods

Methods can be generic independently of the class they reside in.
By adding a type parameter to the method signature, the method can accept or return values of type `T`.
Inside a truly generic method, the compiler treats `T` as a basic `object`, meaning only standard methods like `GetType()` or `ToString()` are available unless constraints are applied.

```csharp
public class ClassWithGenericMethod
{
    public void GenericMethod<T>(T value)
    {
        Console.WriteLine(
            $"The type of the value is {value.GetType().Name} " +
            $"and the value is {value}");
    }

    public T GenericFunction<T>(T value)
    {
        Console.WriteLine(
            $"The type of the value is {value.GetType().Name} " +
            $"and the value is {value}");
        return value;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/generics-54135479/?t=280)

When calling generic methods, C# often uses type inference to determine `T` based on the argument passed, though it can be specified explicitly.

```csharp
ClassWithGenericMethod instanceOfClassWithGenericMethod = new();
instanceOfClassWithGenericMethod.GenericMethod("This is a string");
instanceOfClassWithGenericMethod.GenericMethod(42);
instanceOfClassWithGenericMethod.GenericMethod(3.14);

int genericFunctionResult1 = instanceOfClassWithGenericMethod.GenericFunction(42);
string genericFunctionResult2 = instanceOfClassWithGenericMethod.GenericFunction("This is a string");
double genericFunctionResult3 = instanceOfClassWithGenericMethod.GenericFunction(3.14);
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/generics-54135479/?t=445)

#### Generic Collections

Generics are most commonly encountered in collections.
The `System.Collections.Generic` namespace contains types like `List<T>` and `Dictionary<TKey, TValue>`.
These collections use generic algorithms and data structures that are agnostic to the type of items they store, preventing the need for separate implementations for every possible type.

```csharp
List<int> numericList = new List<int>();
List<string> stringList = new List<string>();

// this is because the algorithms and data structures for many
// collections simply do not care about the type of the elements!
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/generics-54135479/?t=535)

#### Generic Constraints

Constraints allow a generic to require that a type parameter possesses certain characteristics.
This is done using the `where` clause.
Common constraints include requiring a type to be a reference type (`where T : class`), have a parameterless constructor (`where T : new()`), or implement a specific interface.

Consider an interface `IAnimal` and several records implementing it:

```csharp
public interface IAnimal
{
    double Weight { get; }
    double Height { get; }
    bool HasFur { get; }
}

public record Cat(double Weight, double Height, bool HasFur) : IAnimal;

public record Dog(double Weight, double Height) : IAnimal
{
    public bool HasFur => true;
}

public record Fish(double Weight, double Height) : IAnimal
{
    public bool HasFur => false;
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/generics-54135479/?t=730)

By applying the constraint `where T : IAnimal`, generic methods can safely access properties like `Weight`, `Height`, or `HasFur` while still operating on the specific type `T` passed in (e.g., a `List<Cat>` or `List<Dog>`).

```csharp
double CalculateWeight<T>(IEnumerable<T> animals)
    where T : IAnimal
{
    var total = animals.Sum(a => a.Weight);
    return total;
}

double CalculateHeight<T>(IEnumerable<T> animals)
    where T : IAnimal
{
    var total = animals.Sum(a => a.Height);
    return total;
}

IEnumerable<T> OnlyWithFur<T>(IEnumerable<T> animals)
    where T : IAnimal
{
    return animals.Where(a => a.HasFur);
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/generics-54135479/?t=805)

This allows the same method to process different collections of specific types that share the `IAnimal` interface, ensuring type safety and avoiding invalid conversions (such as trying to pass an `int[]` into a method requiring `IAnimal`).

```csharp
var cats = new Cat[] { whiskers, pharoah };
var dogs = new Dog[] { frank, spot };
var fish = new Fish[] { goldie };

var totalWeightCats = CalculateWeight(cats);
var totalWeightDogs = CalculateWeight(dogs);
var totalWeightFish = CalculateWeight(fish);
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/generics-54135479/?t=925)

---

## 10. Tuples

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/tuples-54135480/) · 17:50

### Summary

Tuples are lightweight data transfer objects used to group multiple values of different types without the overhead of declaring a formal class, struct, or record.
C# provides two distinct implementations: the reference-based System.Tuple, which is immutable and uses properties, and the value-based System.ValueTuple, which is mutable, uses fields, and features a concise language-integrated syntax.
Value tuples are particularly effective for returning multiple values from methods, supporting named elements for better API clarity, and allowing for deconstruction into individual variables or discards.

### Key concepts

* **System.Tuple vs. System.ValueTuple**: The former is a reference type (immutable, properties), while the latter is a value type (mutable, fields).
* **Shorthand Syntax**: Value tuples can be declared and initialized using parentheses `(value1, value2)` without the `new` keyword.
* **Named Tuples**: Elements can be assigned names in the tuple signature to improve code readability and provide a clearer API.
* **Deconstruction**: Tuples can be unpacked into individual variables or "discards" (using the underscore `_` symbol).
* **Global Aliasing**: C# 12 allows defining global aliases for tuple types using the `using` keyword.
* **Equality**: Tuple equality depends on cardinality (number of elements), element order, and type compatibility; element names are ignored during comparison.

### Lesson notes

Tuples serve as a convenient way to pass around related data points together.
In C#, there are two primary types of tuples with different memory behaviors and characteristics.

```csharp
// Tuples are a lightweight data transfer object
// that can contain multiple values of different types

// Tuples in C# come in two different flavors:
// 1. System.Tuple:
//    - reference type
//    - immutable
//    - values are properties
// 2. System.ValueTuple: a value type
//    - value type
//    - mutable
//    - values are fields

// System.Tuple
Tuple<int, string> tuple = new Tuple<int, string>(1, "one");

// System.ValueTuple
ValueTuple<int, string> valueTuple = new ValueTuple<int, string>(1, "one");
ValueTuple<int, string> valueTuple2 = new(1, "one");
ValueTuple<int, string> valueTuple3 = (1, "one");
var valueTuple4 = (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16); // ValueTuples can take an arbitrary number of elements
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/tuples-54135480/?t=40)

#### Returning Multiple Values

A common use case for tuples is returning multiple values from a single method.
Historically, this required using `out` parameters or creating a custom class/struct just for the return type.
Using `out` parameters often results in an unnatural API where values are split between the return type and the parameter list.

```csharp
int GetMinAndMaxWithoutParam(int[] numbers, out int max)
{
    if (numbers.Length == 0)
    {
        throw new ArgumentException(
            "Cannot find min and max of an empty array");
    }

    int min = numbers[0];
    max = numbers[0];

    for (int i = 1; i < numbers.Length; i++)
    {
        if (numbers[i] < min)
        {
            min = numbers[i];
        }
        if (numbers[i] > max)
        {
            max = numbers[i];
        }
    }

    return min;
}

// Usage with out parameter
int[] numbers = { 1, 2, 3, 4, 5 };
int min = GetMinAndMaxWithoutParam(numbers, out int max);
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/tuples-54135480/?t=385)

By using a `ValueTuple`, the method signature becomes cleaner, and all returned data is contained within a single object.

```csharp
(int, int) GetMinAndMaxWithTuple(int[] numbers)
{
    if (numbers.Length == 0)
    {
        throw new ArgumentException(
            "Cannot find min and max of an empty array");
    }

    int min = numbers[0];
    int max = numbers[0];

    for (int i = 1; i < numbers.Length; i++)
    {
        if (numbers[i] < min)
        {
            min = numbers[i];
        }
        if (numbers[i] > max)
        {
            max = numbers[i];
        }
    }

    return (min, max);
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/tuples-54135480/?t=415)

#### Named Tuples

To avoid the ambiguity of default names like `Item1` and `Item2`, C# allows you to provide names for the tuple elements.
This makes the API self-documenting.

```csharp
(int Min, int Max) GetMinAndMaxWithNamedTuple(int[] numbers)
{
    if (numbers.Length == 0)
    {
        throw new ArgumentException("Cannot find min and max of an empty array");
    }

    int min = numbers[0];
    int max = numbers[0];

    for (int i = 1; i < numbers.Length; i++)
    {
        if (numbers[i] < min) min = numbers[i];
        if (numbers[i] > max) max = numbers[i];
    }

    return (Min: min, Max: max);
}

var minAndMaxNamedTuple = GetMinAndMaxWithNamedTuple(numbers);
Console.WriteLine($"Min: {minAndMaxNamedTuple.Min}, Max: {minAndMaxNamedTuple.Max}");
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/tuples-54135480/?t=505)

#### Deconstruction and Discards

Tuples can be deconstructed directly into local variables.
If a specific value from the tuple is not needed, you can use a discard (`_`) to ignore it.

```csharp
// Deconstructing into explicit types
(int firstThing, string secondThing) = (1, "this is the second thing!");

// Deconstructing with a discard
(int minVal, _) = GetMinAndMaxWithNamedTuple(numbers);

// Using var for inference
var (firstThing2, secondThing2) = (1, "this is the second thing!");
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/tuples-54135480/?t=595)

#### Global Aliasing (C# 12)

C# 12 introduced the ability to alias tuple types globally.
This allows you to define a specific tuple structure once and use it throughout your project as if it were a named type.

```csharp
global using NicksColor = (byte R, byte G, byte B);

// Usage
NicksColor nicksColor = (255, 0, 0);
Console.WriteLine(nicksColor.R);
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/tuples-54135480/?t=655)

#### Equality Rules

Tuple equality is determined by the values and their order, not by the names of the elements.
For two tuples to be comparable, they must have the same cardinality (number of elements) and compatible types.

```csharp
(int, string, int) tupleA = (123, "hello", 456);
(byte, string, float) tupleD = (FirstNumber: 123, Name: "hello", SecondNumber: 456);
(int, int) tupleB = (123, 456);
(string, string) tupleG = ("hello", "world");

// Comparisons
Console.WriteLine($"tupleA == tupleD: {tupleA == tupleD}"); // true (types are compatible and values match)

// Compilation errors:
// Console.WriteLine(tupleA == tupleB); // Error: different cardinality
// Console.WriteLine(tupleB == tupleG); // Error: incompatible types (int vs string)

// Note: .Equals() is generally not used for tuple comparison; use == or !=
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/tuples-54135480/?t=880)
