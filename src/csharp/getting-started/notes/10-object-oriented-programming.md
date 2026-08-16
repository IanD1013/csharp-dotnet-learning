# Object Oriented Programming

> Course: [Getting Started: C#](https://dometrain.com/course/getting-started-csharp/) · Chapter 10
> 5 lessons · ~59:56
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Intro to Object Oriented Programming](https://dometrain.com/take/course/getting-started-csharp-2732244/intro-to-object-oriented-programming-54134900/) | 7:50 | [↓](#1-intro-to-object-oriented-programming) |
| 2 | [Reference Types](https://dometrain.com/take/course/getting-started-csharp-2732244/reference-types-54134901/) | 13:21 | [↓](#2-reference-types) |
| 3 | [Fields & Properties](https://dometrain.com/take/course/getting-started-csharp-2732244/fields-properties-54134902/) | 11:47 | [↓](#3-fields--properties) |
| 4 | [Static vs Instances](https://dometrain.com/take/course/getting-started-csharp-2732244/static-vs-instances-54134903/) | 10:14 | [↓](#4-static-vs-instances) |
| 5 | [Constructors](https://dometrain.com/take/course/getting-started-csharp-2732244/constructors-54134905/) | 16:44 | [↓](#5-constructors) |

---

## 1. Intro to Object Oriented Programming

> [Watch the lesson](https://dometrain.com/take/course/getting-started-csharp-2732244/intro-to-object-oriented-programming-54134900/) · 7:50

### Summary

Object-Oriented Programming (OOP) in C# introduces a paradigm shift from procedural, step-by-step instruction sequences to modeling programs as collections of interacting virtual objects.
By using classes as blueprints, developers can bundle related properties and behaviors (methods) into cohesive units.
This lesson covers the basics of class declaration, object instantiation using the new keyword, and the distinction between reference types (objects) and value types.

### Key concepts

* Object-Oriented Programming (OOP) paradigm
* Classes as blueprints for virtual objects
* Bundling properties and behaviors (methods)
* The class keyword and access modifiers (e.g., public)
* Object instantiation using the new keyword and shorthand new()
* Dot notation for member access
* Reference types vs. value types

### Lesson notes

Object-Oriented Programming (OOP) is a programming paradigm that structures software as a collection of objects rather than just a series of sequential steps.
This approach allows for the creation of virtual concepts within code, where related properties and behaviors are bundled together.
While advanced topics such as inheritance and composition are essential for modeling complex relationships between objects, the fundamental building blocks are classes and instances.

#### Defining Classes and Creating Objects

In C#, a class serves as the blueprint for an object.
It is defined using the class keyword.
To use a class, an instance (or object) must be created using the new keyword.
C# also supports a shorthand instantiation syntax, new(), when the type is already known.

```csharp
// in C# we can create a class by using the class keyword
class OurClass
{

}

// we can create an object from a class by using the new keyword
OurClass ourObject = new OurClass();

// we can use the short-form method to create an object
OurClass ourObject2 = new();
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/intro-to-object-oriented-programming-54134900/?t=175)

#### Bundling Behavior with Methods

Classes are most useful when they contain methods and functions, which represent the behaviors associated with the object.
To make these methods accessible from outside the class, the public access modifier is used.
This allows the method to be called on a specific instance of the class.

```csharp
public class OurSecondClass
{
    public void ExampleMethod()
    {
        Console.WriteLine("Hello from our method!");
    }

    public int ExampleFunction()
    {
        return 42;
    }
}

OurSecondClass ourSecondObject = new();
ourSecondObject.ExampleMethod();
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/intro-to-object-oriented-programming-54134900/?t=220)

#### Member Access and Reference Types

Members of an object are accessed using dot notation (e.g., object.Method()).
This is the same syntax used for Console.WriteLine(), where Console is a class and WriteLine is a method.
A significant distinction in C# is that classes are reference types, whereas basic types like int, double, and bool are value types.
While custom classes typically require instantiation with new to access their members, some built-in classes like Console provide methods that can be called directly without creating a new instance.

```csharp
OurSecondClass ourSecondObject = new();
ourSecondObject.ExampleMethod();

int result = ourSecondObject.ExampleFunction();

// Console.WriteLine is a method on the Console class
Console.WriteLine("This is a method on the Console class!");

// objects are "reference types" in C#
// whereas types like int, double, and bool are "value types"
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/intro-to-object-oriented-programming-54134900/?t=400)

---

## 2. Reference Types

> [Watch the lesson](https://dometrain.com/take/course/getting-started-csharp-2732244/reference-types-54134901/) · 13:21

### Summary

This lesson explores the fundamental distinction between value types and reference types in C#.
While types like integers, booleans, and DateTimes are treated as value types that are copied when passed to methods, classes and collections are reference types that store a pointer to an object in memory.
The lesson demonstrates how reference equality differs from value equality and illustrates how passing reference types to methods allows for the modification of the original object's state, while reassigning the reference itself within a method does not affect the caller's original pointer.

### Key concepts

*   **Value Types**: Types such as `int`, `double`, `bool`, and `DateTime` store data directly and are passed by value (creating a copy).
*   **Reference Types**: Classes and collections (like `List<T>`) store a reference or pointer to the object's location in memory.
*   **Reference Equality**: By default, comparing two objects with `==` checks if they point to the same instance in memory, not if their data is identical.
*   **Parameter Passing**: Passing a reference type to a method allows the method to modify the internal state of that object. However, reassigning the parameter to a `new` instance only changes the local reference within the method scope.

### Lesson notes

In C#, it is critical to distinguish between value types and reference types.
Common types like integers, floats, doubles, booleans, and DateTimes are value types.
Objects created from classes, however, are reference types.
When you create multiple instances of a class, they exist as distinct objects in memory, even if they are of the same type.

```csharp
OurClass object1 = new OurClass(); // new reference
OurClass object2 = new OurClass(); // new reference
OurClass object3 = object1; // same reference as object1!

Console.WriteLine("object1 == object2:");
Console.WriteLine(object1 == object2); // false
Console.WriteLine("object1 == object3:");
Console.WriteLine(object1 == object3); // true
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/reference-types-54134901/?t=40)

In the example above, `object1` and `object2` are different instances, so an equality check returns `false`.
However, `object3` is assigned directly to `object1`, meaning it points to the same reference in memory; therefore, they are equal.

Collections like lists behave the same way.
Two different list instances containing the exact same values are not considered equal because they are separate objects in memory.

```csharp
// collections are very much the same!
List<int> myNumbers1 = new List<int> { 1, 2, 3 };
List<int> myNumbers2 = new List<int> { 1, 2, 3 };

Console.WriteLine("myNumbers1 == myNumbers2:");
Console.WriteLine(myNumbers1 == myNumbers2); // false
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/reference-types-54134901/?t=100)

#### Parameter Passing: Value vs Reference

When passing value types into a method, C# passes a copy of the value.
Any modifications made to the parameter inside the method do not affect the original variable outside the method.

```csharp
void ChangeValue(int value)
{
    value = 42;
}

int myValue = 1337;
Console.WriteLine("myValue before ChangeValue:");
Console.WriteLine(myValue); // 1337
ChangeValue(myValue);
Console.WriteLine("myValue after ChangeValue:");
Console.WriteLine(myValue); // 1337
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/reference-types-54134901/?t=280)

Reference types behave differently.
When a reference type is passed to a method, the method receives the reference (pointer) to the object.
This allows the method to modify the original object's contents.

```csharp
void ChangeReference(List<string> words)
{
    words.Add("from");
    words.Add("Dev");
    words.Add("Leader");
}

List<string> myWords = new List<string> { "Hello", "World" };
Console.WriteLine("myWords before ChangeReference:");
Console.WriteLine(string.Join(" ", myWords)); // Hello World
ChangeReference(myWords);
Console.WriteLine("myWords after ChangeReference:");
Console.WriteLine(string.Join(" ", myWords)); // Hello World from Dev Leader
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/reference-types-54134901/?t=415)

However, there is a nuance when reassigning the reference itself.
If you assign the parameter to a `new` object inside the method, you are only changing where the local variable points.
The original reference held by the caller remains unchanged.

```csharp
void ChangeReference(List<string> words)
{
    words = new List<string>();
    words.Add("from");
    words.Add("Dev");
    words.Add("Leader");
}

List<string> myWords = new List<string> { "Hello", "World" };
Console.WriteLine("myWords before ChangeReference:");
Console.WriteLine(string.Join(" ", myWords)); // Hello World
ChangeReference(myWords);
Console.WriteLine("myWords after ChangeReference:");
Console.WriteLine(string.Join(" ", myWords)); // Hello World
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/reference-types-54134901/?t=655)

In this final example, because `words` was reassigned to a `new List<string>()` at the start of the method, the subsequent `.Add()` calls affected the new, local list which was discarded when the method finished.
The original `myWords` list remained "Hello World".

---

## 3. Fields & Properties

> [Watch the lesson](https://dometrain.com/take/course/getting-started-csharp-2732244/fields-properties-54134902/) · 11:47

### Summary

This lesson introduces fields and properties as the primary mechanisms for managing state in C# classes.
It covers the use of access modifiers like private and public to implement encapsulation, the this keyword for instance-level scoping, and various property syntaxes including expression-bodied members and auto-implemented properties.

### Key concepts

*   **Fields**: Variables declared directly within a class to store data.
*   **Access Modifiers**: Keywords like `public` and `private` that control the visibility of class members.
*   **The `this` Keyword**: A reference to the current instance of the class, used to access instance members.
*   **Properties**: Members that provide a flexible mechanism to read, write, or compute values, often wrapping private fields.
*   **Accessors**: The `get` and `set` blocks that define property behavior.
*   **Auto-implemented Properties**: A concise syntax where the compiler creates a hidden backing field automatically.

### Lesson notes

#### Fields and Naming Conventions

Fields are variables declared at the class level.
In C#, it is a common convention to prefix private fields with an underscore (e.g., `_name`).
This helps differentiate class-level fields from local variables defined within methods.

```csharp
// here is how we declare a field in a class
class Person
{
    private string _name;
}

// we can give a field a value when we declare it
class Person2
{
    private string _name = "John";
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/fields-properties-54134902/?t=25)

#### The `this` Keyword

To access members of the current class instance, C# provides the `this` keyword.
Using `this.` explicitly indicates that you are accessing a field or method belonging to the instance, which is useful for distinguishing fields from local variables with identical names.

```csharp
class Person
{
    private string _name;

    public void SetInstanceName()
    {
        this._name = "John";
    }
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/fields-properties-54134902/?t=55)

#### Access Modifiers and Encapsulation

Access modifiers control which parts of a program can see or interact with class members.

*   **`public`**: Accessible from outside the class.
*   **`private`**: Accessible only within the scope of the class where it is defined.

It is standard practice to keep fields `private` to protect the internal state of an object.
If a field is private, attempting to access it from outside the class results in a compilation error stating the member is inaccessible due to its protection level.

```csharp
Person2 john = new Person2();
// john._name = "John"; // this will not work because _name is private
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/fields-properties-54134902/?t=175)

#### Accessing State via Methods

One way to expose private field data is through public methods.
A "getter" method can return the value of a private field to the caller.

```csharp
class Person3
{
    private string _name = "John";

    public string GetName()
    {
        return _name;
    }
}

Person3 johnWithMethod = new Person3();
Console.WriteLine(johnWithMethod.GetName());
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/fields-properties-54134902/?t=205)

#### Properties

Properties are specialized members designed for reading and writing values.
They offer several syntaxes for different use cases:

1.  **Standard Property**: Uses a `get` block to return a value.
2.  **Expression-bodied Property**: Uses the `=>` arrow syntax for a concise read-only property.
3.  **Auto-implemented Property**: Uses `{ get; }` or `{ get; set; }` without an explicit backing field. The compiler creates the field behind the scenes.
4.  **Mutable Property**: Includes both `get` and `set` accessors. The `set` accessor uses the `value` keyword to represent the incoming data.

```csharp
class Person4
{
    private string _name = "John";

    public string Name
    {
        get
        {
            return _name;
        }
    }

    public string Name2 => _name;

    public string Name3 { get; } = "John";

    public string MutableName
    {
        get { return _name; }
        set { _name = value; }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/fields-properties-54134902/?t=295)

#### Backing Fields and Mutability

When multiple properties reference the same private backing field, changing the field via one property's setter affects all properties that return that field.
In the example below, setting `MutableName` updates `_name`, which causes `Name` and `Name2` to also return the new value.
However, `Name3` remains unchanged because it is an auto-implemented property with its own independent, implicit backing field.

```csharp
Person4 johnWithProperty = new Person4();
Console.WriteLine(johnWithProperty.Name);
Console.WriteLine(johnWithProperty.Name2);
Console.WriteLine(johnWithProperty.Name3);

Console.WriteLine("Setting the name...");
johnWithProperty.MutableName = "John Doe";

Console.WriteLine(johnWithProperty.MutableName);
Console.WriteLine(johnWithProperty.Name);
Console.WriteLine(johnWithProperty.Name2);
Console.WriteLine(johnWithProperty.Name3);
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/fields-properties-54134902/?t=640)

---

## 4. Static vs Instances

> [Watch the lesson](https://dometrain.com/take/course/getting-started-csharp-2732244/static-vs-instances-54134903/) · 10:14

### Summary

This lesson explores the `static` modifier in C#, which allows developers to associate fields, properties, and methods with a type itself rather than with specific instances of that type.
It covers the constraints of static classes—which cannot be instantiated and must only contain static members—and demonstrates how non-static classes can host both instance and static members.
Through a practical example, the lesson illustrates that while instance members maintain unique state per object, static members share a single state across all instances, facilitating data sharing but requiring careful management to avoid unintended side effects.

### Key concepts

- **Static Modifier**: A keyword used to associate a member with the type itself rather than an instance.
- **Static Classes**: Classes marked as `static` cannot be instantiated using the `new` keyword and can only contain static members.
- **Instance Members**: Members (fields, properties, methods) that belong to a specific object instance and maintain unique state for that instance.
- **Access Rules**: Static methods can only access other static members. Instance methods can access both instance and static members.
- **Shared State**: Static properties and fields are shared across all instances of a class; changing a static value in one place affects all accessors of that type.

### Lesson notes

#### Static Classes and Methods

In C#, the `static` modifier allows you to associate a member with a type rather than an instance of that type.
When a class is marked as `static`, it serves as a container for members that do not require object-specific data.
A primary characteristic of a static class is that it cannot be instantiated; attempting to use the `new` keyword with a static class results in a compilation error.
Furthermore, every member within a static class—including fields, properties, and methods—must also be marked as `static`.

```csharp
// static is a modifier that makes a member belong to a type, rather than an "instance" of a type
// we can even make entire classes static.

static class MyStaticClass
{
    public static void MyStaticMethod()
    {
        Console.WriteLine("I am a static method!");
    }
}

// note that we cannot make an instance of "MyStaticClass" because it is marked static
// MyStaticClass myStaticClass = new MyStaticClass(); // this will not work!

// static classes cannot have instance members, so anything inside of a static class must also be static
MyStaticClass.MyStaticMethod();
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/static-vs-instances-54134903/?t=25)

This pattern is common in the .NET Base Class Library.
For example, `Console.WriteLine()` is a static method belonging to the static `Console` class.
You do not need to create an instance of `Console` to use its methods.

#### Static Members in Non-Static Classes

While static classes are restricted to static members, non-static (instance) classes can contain a mix of both instance and static members.
This allows a class to maintain unique data for every object while also providing shared data or utility methods at the type level.

There are specific rules for how these members interact:
1.  **Static methods** can access other static members, but they cannot access instance members. This is because static methods do not have an object reference (an instance) to work with.
2.  **Instance methods** can access both instance members and static members.

```csharp
class MyNonStaticClass
{
    public string MyInstanceProperty { get; set; } = "Nick";

    public static string MyStaticProperty { get; set; } = "Cosentino";

    public static void MyStaticMethod()
    {
        Console.WriteLine($"The static property value is: {MyStaticProperty}");

        // this will not work because MyInstanceProperty is not static!
        //Console.WriteLine($"The instance property value is: {MyInstanceProperty}");
    }

    public void MyInstanceMethod()
    {
        Console.WriteLine($"The static property value is: {MyStaticProperty}");
        Console.WriteLine($"The instance property value is: {MyInstanceProperty}");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/static-vs-instances-54134903/?t=370)

#### Shared State and Mutation

The most significant difference between instance and static properties is how they handle state.
Each instance of a class has its own copy of instance properties.
However, there is only one copy of a static property, which is shared across all instances of that class.

If you modify an instance property on one object, it does not affect other objects.
If you modify a static property, that change is immediately visible to every instance and to the type itself.

```csharp
MyNonStaticClass myNonStaticClass1 = new MyNonStaticClass();
MyNonStaticClass myNonStaticClass2 = new MyNonStaticClass();

Console.WriteLine("Before mutating properties on MyNonStaticClass...");
myNonStaticClass1.MyInstanceMethod();
myNonStaticClass2.MyInstanceMethod();
MyNonStaticClass.MyStaticMethod();

// let's mutate these things and see what happens!
myNonStaticClass1.MyInstanceProperty = "Dev";
myNonStaticClass2.MyInstanceProperty = "Leader";
MyNonStaticClass.MyStaticProperty = "Nick Cosentino";

Console.WriteLine("After mutating properties on MyNonStaticClass...");
myNonStaticClass1.MyInstanceMethod();
myNonStaticClass2.MyInstanceMethod();
MyNonStaticClass.MyStaticMethod();
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/static-vs-instances-54134903/?t=490)

In the example above, after mutation, `myNonStaticClass1` reports "Dev" and `myNonStaticClass2` reports "Leader" for their respective instance properties.
However, both instances (and the static method call) report "Nick Cosentino" for the static property, because that value is shared at the type level.

While static members are convenient for sharing information, they should be used with caution.
Because any part of the application can potentially change a static value, it can lead to bugs where state is modified unexpectedly across different parts of the system.

---

## 5. Constructors

> [Watch the lesson](https://dometrain.com/take/course/getting-started-csharp-2732244/constructors-54134905/) · 16:44

### Summary

Constructors are specialized methods in C# that execute automatically when an instance of a class is created.
They are primarily used to initialize the state of an object, such as instantiating internal collections or setting initial property values.
C# supports various constructor types, including parameterless, parameterized, static, and private constructors, and allows for constructor chaining to promote code reuse within a class.

### Key concepts

- Implicit vs. Explicit Constructors
- Constructor Syntax (no return type, matches class name)
- Parameterized Constructors
- Constructor Chaining using `: this()`
- Field and Property Initialization
- Static Constructors for type-level logic
- Private Constructors for encapsulation and internal reuse

### Lesson notes

Constructors are the logic that runs when creating a new instance of an object.
Every class has a constructor; if one is not explicitly defined, C# provides an implicit, parameterless default constructor that performs no additional actions.

An explicit constructor is defined like a method but with two key differences: it has no return type (not even `void`) and its name must exactly match the class name.

```csharp
class ImplicitConstructor
{
}

class ExplicitConstructor
{
    public ExplicitConstructor()
    {
        Console.WriteLine("ExplicitConstructor constructor called");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/constructors-54134905/?t=25)

Constructors can also accept parameters to allow data to be passed in during instantiation.

```csharp
class ConstructorWithParameter
{
    public ConstructorWithParameter(string message)
    {
        Console.WriteLine(message);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/constructors-54134905/?t=160)

#### Constructor Chaining

A class can have multiple constructors as long as their signatures differ.
You can "chain" constructors together using the `: this()` syntax, allowing one constructor to call another.
This is useful for providing default values or centralizing initialization logic.

```csharp
class MultipleConstructors
{
    public MultipleConstructors()
        : this("This is the default message!")
    {
    }

    public MultipleConstructors(string message)
    {
        Console.WriteLine(message);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/constructors-54134905/?t=220)

When calling the parameterless constructor above, it immediately chains to the parameterized version, passing the default string.
The chained constructor executes its body before the calling constructor's body.

#### Initialization Logic

The primary use case for constructors is initializing class members.
For example, if a class contains a `List<string>`, the constructor should instantiate that list.
Without this initialization, calling methods that rely on the list (like `Add` or `Print`) would result in a `NullReferenceException`.

```csharp
class OurCollectionOfWords
{
    private List<string> _strings;

    public OurCollectionOfWords()
    {
        // Initializing the list makes it safe to use later
        _strings = new List<string>();
    }

    public void Add(string word)
    {
        _strings.Add(word);
    }

    public void Print()
    {
        foreach (var word in _strings)
        {
            Console.WriteLine(word);
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/constructors-54134905/?t=490)

You can also combine initialization with configuration by passing collections directly into the constructor to populate internal fields.

```csharp
class OurCollectionOfWords2
{
    private List<string> _strings;

    public OurCollectionOfWords2(List<string> words)
    {
        _strings = new List<string>();

        foreach (var word in words)
        {
            _strings.Add(word);
        }
    }

    public void Print()
    {
        foreach (var word in _strings)
        {
            Console.WriteLine(word);
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/constructors-54134905/?t=595)

#### Static Constructors

While standard constructors run every time an instance is created, a static constructor runs only once: the first time the type is used.
Static constructors are used for type-level initialization and do not accept access modifiers or parameters.

```csharp
class StaticConstructor
{
    static StaticConstructor()
    {
        Console.WriteLine("StaticConstructor constructor called");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/constructors-54134905/?t=685)

#### Private Constructors

Private constructors restrict instantiation from outside the class.
They are often used to hide specific access patterns or to centralize logic that multiple public constructors share.

```csharp
class OurClassWithAHiddenConstructor
{
    public OurClassWithAHiddenConstructor(int value)
        : this()
    {
        Console.WriteLine($"This is the public constructor and we received value {value}.");
    }

    private OurClassWithAHiddenConstructor()
    {
        Console.WriteLine("Nobody can call this constructor directly from the outside!");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/constructors-54134905/?t=910)

When chaining constructors, the order of execution is critical: the chained constructor (the one following the `: this()` keyword) always executes completely before the body of the constructor that called it.
