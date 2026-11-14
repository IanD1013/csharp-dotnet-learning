# Generics

> 课程:[From Zero to Hero: Reflection in .NET](https://dometrain.com/course/from-zero-to-hero-reflection-in-dotnet/) · 第 8 章
> 共 3 课 · 约 22:54
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Checking For Generics](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/checking-for-generics-58131037/) | 9:59 | [↓](#1-checking-for-generics) |
| 2 | [Using Generics From Reflection](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/using-generics-from-reflection-58131038/) | 7:57 | [↓](#2-using-generics-from-reflection) |
| 3 | [Make Generic Instances](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/make-generic-instances-58131039/) | 4:58 | [↓](#3-make-generic-instances) |

## 1. Checking For Generics

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/checking-for-generics-58131037/) · 9:59

### 总结

本课探讨如何使用反射来检查 .NET 中的泛型类型和泛型方法。
内容涵盖:使用 IsGenericType 属性识别泛型类型、通过 GetGenericArguments 获取类型参数,以及使用 GetGenericParameterConstraints 检查泛型约束。
此外,本课还演示了如何在代码中使用反引号表示法和逗号语法来处理开放泛型类型,以及如何区分在类级别定义的泛型参数与在方法级别定义的泛型参数。

### 核心概念

- **开放泛型类型(Open Generic Types)**:使用 `typeof(T<,,>)` 语法表示没有具体类型实参的泛型类型。
- **IsGenericType**:`Type` 类上的一个属性,用于识别某个类型是否为泛型。
- **GetGenericArguments**:一个方法,用于获取表示泛型类型参数的 `Type` 对象。
- **泛型约束(Generic Constraints)**:使用 `GetGenericParameterConstraints` 发现施加在类型参数上的要求(例如接口)。
- **方法级泛型(Method-Level Generics)**:检查专门定义在方法上、而不是定义在所属类上的泛型参数。
- **反引号表示法(Backtick Notation)**:理解 .NET 在内部如何表示泛型类型(例如 ``TypeName`3``)。

### 课程笔记

泛型提供了一种强大的方式来创建与类型无关的实现,例如集合,无论使用哪种具体类型,其中的逻辑都相同。
在使用反射时,泛型类型需要特殊处理,因为一个泛型定义可以代表许多具体的构造类型。

为了演示如何检查这些结构,请看下面这组接口和一个泛型类。
类 `GenericTypeA<T1, T2, T3>` 包含多种约束,以及同时使用类级别和方法级别泛型参数的方法。

```csharp
public sealed class SomeImplementation(string input) : ISomeInterface
{

    public override string ToString() => input;
}
public sealed class SomeOtherImplementation(int input) : ISomeOtherInterface
{
    public override string ToString() => input.ToString();
}

public interface ISomeInterface
{
}

public interface ISomeOtherInterface
{
}

public class GenericTypeA<T1, T2, T3>
    where T2 : ISomeInterface
{
    public void Method1(T1 value1, T2 value2)
    {
        Console.WriteLine($"Method1<{typeof(T1).Name}, {typeof(T2).Name}>({value1}, {value2})");
    }

    public void Method2<TMethodParameter>(TMethodParameter parameter)
        where TMethodParameter : ISomeOtherInterface
{
    Console.WriteLine($"Method<{typeof(TMethodParameter).Name}>({parameter})");
}
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/checking-for-generics-58131037/?t=130)

#### 检查泛型类型定义

要在编译时引用一个泛型类型定义(即"开放"泛型类型)而不提供具体的类型实参,可以将 `typeof` 运算符与逗号配合使用。
逗号的数量等于类型参数的数量减一。
例如,`typeof(GenericTypeA<,,>)` 指的是一个具有三个泛型参数的类的基础定义。

使用反射,可以通过检查 `IsGenericType` 属性来扫描程序集中的所有泛型类型。
一旦识别出某个泛型类型,`GetGenericArguments()` 会返回其类型参数,而 `GetGenericParameterConstraints()` 会揭示施加在这些参数上的任何约束。

```csharp
using System.Reflection;

Console.WriteLine();

// to refer to a generic type without the type parameter:
var genericTypeA = typeof(GenericTypeA<,,>);

var genericTypes = Assembly
    .GetExecutingAssembly()
    .GetTypes()
    .Where(t => t.IsGenericType)
    .ToArray();

Console.WriteLine("The generic types in this assembly are:");
foreach (var genericType in genericTypes)
{
    Console.WriteLine(genericType.Name);

    Console.WriteLine(" Type Parameters:");
    var genericTypeParameters = genericType.GetGenericArguments();
    foreach (var genericTypeParameter in genericTypeParameters)
    {
        Console.WriteLine($"    {genericTypeParameter.Name}");

        var constraints = genericTypeParameter.GetGenericParameterConstraints();
        foreach (var constraint in constraints)
        {
            Console.WriteLine($"        * Must Be: {constraint.Name}");
        }
    }

    Console.WriteLine(" Members:");
    foreach (var member in genericType.GetMembers())
    {
        Console.WriteLine($"    {member.Name}");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/checking-for-generics-58131037/?t=310)

#### 检查泛型方法及其参数

反射同样允许检查方法级别的泛型参数。
一个方法可能使用其父类定义的类型参数(例如 `Method1` 使用 `T1` 和 `T2`),也可能定义自己的泛型参数(例如 `Method2<TMethodParameter>`)。

```text
GenericTypeA<T1, T2, T3>            (where T2 : ISomeInterface)
 |
 +-- Method1(T1 value1, T2 value2)  -> 使用类级别的 T1、T2
 |
 +-- Method2<TMethodParameter>(TMethodParameter parameter)
       -> 定义自己的 TMethodParameter
          (where TMethodParameter : ISomeOtherInterface)
```

在检查方法参数时,参数类型上的 `IsGenericParameter` 属性会指示它是否是一个泛型占位符。
即使约束是在类级别定义的,在方法级别检查该参数时,反射也能呈现出这个约束。

```csharp
foreach (var genericType in genericTypes)
{
    Console.WriteLine(genericType.Name);

    Console.WriteLine("  Type Parameters:");
    var genericTypeParameters = genericType.GetGenericArguments();
    foreach (var genericTypeParameter in genericTypeParameters)
    {
        Console.WriteLine($"    {genericTypeParameter.Name}");

        var constraints = genericTypeParameter.GetGenericParameterConstraints();
        foreach (var constraint in constraints)
        {
            Console.WriteLine($"      * Must Be: {constraint.Name}");
        }
    }

    Console.WriteLine("  Members:");
    foreach (var member in genericType.GetMembers())
    {
        Console.WriteLine($"    {member.Name}");

        if (member is MethodInfo method)
        {
            var methodParameters = method
                .GetParameters();
            foreach (var methodParameter in methodParameters)
            {
                Console.WriteLine(
                    $"      " +
                    $"{methodParameter.Name}: " +
                    $"{methodParameter.ParameterType.Name}");

                if (methodParameter.ParameterType.IsGenericParameter)
                {
                    var constraints = methodParameter
                        .ParameterType
                        .GetGenericParameterConstraints();
                    foreach (var constraint in constraints)
                    {
                        Console.WriteLine($"        * Must Be: {constraint.Name}");
                    }
                }
            }
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/checking-for-generics-58131037/?t=415)

#### 理解输出结果

应用程序运行时,输出会显示泛型类型的内部元数据名称。
反引号后跟一个数字(例如 ``GenericTypeA`3``)表示该泛型类型的元数(arity,即类型参数的数量)。
输出还确认了在类级别和方法级别定义的约束都被正确识别。

```text
The generic types in this assembly are:
GenericTypeA`3
  Type Parameters:
    T1
    T2
      * Must Be: ISomeInterface
    T3
  Members:
    Method1
      value1: T1
      value2: T2
        * Must Be: ISomeInterface
    Method2
      parameter: TMethodParameter
        * Must Be: ISomeOtherInterface
    GetType
    ToString
    Equals
      obj: Object
    GetHashCode
    .ctor
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/checking-for-generics-58131037/?t=475)

## 2. Using Generics From Reflection

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/using-generics-from-reflection-58131038/) · 7:57

### 总结

本课演示如何使用反射与泛型类型和泛型方法进行交互,重点在于编译时类型发现与运行时类型发现之间的区别。
内容涵盖:如何借助基于元数的命名约定(例如 ``Type`3``)通过程序集扫描获取泛型类型定义、如何实例化泛型类,以及在运行时调用泛型方法之前必须先使用 `MakeGenericMethod` 提供具体类型实参这一基本要求。

### 核心概念

- **泛型类型发现(Generic Type Discovery)**:使用 `typeof(GenericTypeA<,,>)` 进行编译时引用,或使用程序集扫描进行运行时发现。
- **元数命名约定(Arity Naming Convention)**:在元数据中使用反引号表示法识别泛型类型(例如 ``GenericTypeA`3`` 表示三个类型参数)。
- **IsGenericMethod**:筛选方法,以识别那些定义了自己泛型参数的方法。
- **后期绑定的限制(Late-Bound Constraints)**:理解为什么在类型参数尚未绑定时直接调用泛型方法会失败。
- **MakeGenericMethod**:通过提供具体类型,将泛型方法定义转换为一个具体的、可调用的方法的机制。

### 课程笔记

#### 获取泛型类型定义

获取泛型类型定义的引用主要有两种方式。
如果类型在编译时已知,可以使用 `typeof` 运算符,并用空的逗号占位符表示类型参数。
如果类型在编译时未知,可以使用程序集扫描按名称查找该类型。
在元数据中,泛型类型的命名方式是反引号后跟泛型参数的数量(即元数)。

```csharp
using System.Reflection;

Console.WriteLine();

// to refer to a generic type without the type parameter:
var genericTypeA = typeof(GenericTypeA<,,>);
//var genericTypeA = Assembly
//    .GetExecutingAssembly() // Discovery via scanning
//    .GetTypes()
//    .Where(t => t.IsGenericType)
//    .SingleOrDefault(t => t.Name == "GenericTypeA`3");

var genericMethods = genericTypeA
    .GetMethods()
    .Where(m => m.IsGenericMethod)
    .ToArray();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/using-generics-from-reflection-58131038/?t=40)

#### 使用泛型实例和泛型方法

在使用泛型类时,类本身可能有泛型参数,而它的方法也可能定义各自独立的泛型参数。

请看下面的类结构:

```csharp
public sealed class SomeOtherImplementation(int input) : ISomeOtherInterface
{
    public override string ToString() => input.ToString();
}

public interface ISomeInterface
{
}

public interface ISomeOtherInterface
{
}

public class GenericTypeA<T1, T2, T3>
    where T2 : ISomeInterface
{
    public void Method1(T1 value1, T2 value2)
    {
        Console.WriteLine($"Method1<{typeof(T1).Name}, {typeof(T2).Name}>({value1}, {value2})");
    }

    public void Method2<TMethodParameter>(TMethodParameter parameter)
        where TMethodParameter : ISomeOtherInterface
    {
        Console.WriteLine($"Method2<{typeof(TMethodParameter).Name}>({parameter})");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/using-generics-from-reflection-58131038/?t=400)

```csharp
public sealed class SomeImplementation(string input) : ISomeInterface
{

    public override string ToString() => input;
}
public sealed class SomeOtherImplementation(int input) : ISomeOtherInterface
{
    public override string ToString() => input.ToString();
}

public interface ISomeInterface
{
}

public interface ISomeOtherInterface
{
}

public class GenericTypeA<T1, T2, T3>
    where T2 : ISomeInterface
{
    public void Method1(T1 value1, T2 value2)
    {
        Console.WriteLine($"Method1<{typeof(T1).Name}, {typeof(T2).Name}>({value1}, {value2})");
    }

    public void Method2<TMethodParameter>(TMethodParameter parameter)
        where TMethodParameter : ISomeOtherInterface
    {
        Console.WriteLine($"Method<{typeof(TMethodParameter).Name}>({parameter})");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/checking-for-generics-58131037/?t=130)

在这个示例中,`Method1` 使用的是在类级别定义的类型参数(`T1`、`T2`)。
一旦创建了 `GenericTypeA<int, SomeImplementation, string>` 的实例,这些类型就被固定下来了。
反射可以直接调用 `Method1`,因为这些类型已经从实例中得知。

#### MakeGenericMethod 的必要性

`Method2` 则不同,因为它定义了自己的泛型参数 `TMethodParameter`。
即使你已经通过 `GetMethod("Method2")` 获得了该方法的引用,也无法直接调用它。
尝试这样做会导致一个异常:"Late bound operation cannot be performed on types or methods for which contains generic parameters is true."

要调用泛型方法,必须先在 `MethodInfo` 对象上调用 `MakeGenericMethod`,并传入用于该方法泛型参数的具体类型。
这会创建一个特定的 `MethodInfo`,其中的泛型参数被绑定到实际类型上。

```csharp
GenericTypeA<int, SomeImplementation, string> instance = new();

// Method1 is invokable directly because its types are bound to the instance
var method1 = instance.GetType().GetMethod("Method1");
method1.Invoke(instance, [42, new SomeImplementation("Some input!")]);

// Method2 requires MakeGenericMethod because it has its own generic parameters
var method2 = instance.GetType().GetMethod("Method2");

// This would fail: 
// method2.Invoke(instance, [new SomeOtherImplementation(123)]);

// Correct approach:
method2 = method2.MakeGenericMethod(typeof(SomeOtherImplementation));
method2.Invoke(instance, [new SomeOtherImplementation(42)]);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/using-generics-from-reflection-58131038/?t=445)

```text
GetMethod("Method2")
  -> Method2<TMethodParameter>          Invoke(...) -> 异常
       |
       | MakeGenericMethod(typeof(SomeOtherImplementation))
       v
  -> Method2<SomeOtherImplementation>   Invoke(...) -> 成功
```

通过调用 `MakeGenericMethod`,运行时会在允许调用之前,验证所提供的类型(`SomeOtherImplementation`)是否满足泛型参数上定义的任何约束(在本例中为 `ISomeOtherInterface`)。

## 3. Make Generic Instances

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/make-generic-instances-58131039/) · 4:58

### 总结

在运行时创建泛型类型的实例,需要把开放泛型类型定义转换为封闭泛型类型。
普通类型可以直接通过反射实例化,但开放泛型缺少其参数所需的类型信息,导致实例化尝试失败。
通过使用 MakeGenericType 方法,开发者可以在遵守任何已定义类型约束的前提下提供所需的类型实参,从而得到一个可构造的类型,然后用 Activator.CreateInstance 对其实例化。

### 核心概念

- **开放泛型类型与封闭泛型类型(Open vs. Closed Generic Types)**:开放泛型(例如 `GenericTypeA<,,>`)在用具体类型实参封闭之前无法被实例化。
- **Type.MakeGenericType**:用于将类型实参应用到开放泛型类型定义上的方法。
- **Activator.CreateInstance**:创建对象实例的标准工具,在处理泛型时需要一个封闭类型。
- **运行时约束验证(Runtime Constraint Validation)**:反射会在封闭类型的过程中于运行时强制执行泛型类型约束(例如 `where T : ISomeInterface`)。
- **扫描程序集查找泛型(Assembly Scanning for Generics)**:使用反引号表示法(例如 ``GenericTypeA`3``)来指定泛型类型的元数,从而识别泛型类型。

### 课程笔记

在使用反射时,有些场景下你需要实例化一个泛型类型,但在编译时并没有对具体类型实参的引用。
你可以通过程序集扫描查找开放泛型类型的元数据名称来定位它,该名称包含一个反引号,后跟泛型参数的数量(例如 ``GenericTypeA`3``)。

但是,你不能把开放泛型类型直接传给 `Activator.CreateInstance`。
这样做会导致运行时异常,因为运行时不知道该为泛型参数使用哪些类型。

```csharp
using System.Reflection;

var genericTypeA = Assembly
    .GetExecutingAssembly()
    .GetTypes()
    .Where(t => t.IsGenericType)
    .SingleOrDefault(t => t.Name == "GenericTypeA`3");

// This will throw an exception because genericTypeA is an open generic type
var instance = Activator.CreateInstance(genericTypeA);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/make-generic-instances-58131039/?t=130)

产生的异常说明,当 `ContainsGenericParameters` 为 true 时,无法创建泛型类型的实例。
要解决这个问题,必须使用 `MakeGenericType` 方法把开放泛型类型转换为封闭泛型类型。
该方法接受一个 `Type` 对象数组,这些对象与类的泛型参数一一对应。

```csharp
genericTypeA = genericTypeA.MakeGenericType(
    typeof(string),
    typeof(SomeImplementation),
    typeof(int));
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/make-generic-instances-58131039/?t=160)

需要注意的是,`MakeGenericType` 会强制执行泛型类上定义的任何类型约束。
如果某个类型参数带有约束(例如要求实现某个接口),而你提供的类型不满足该约束,该方法会在运行时抛出异常。
你无法使用反射绕过类型安全。

```csharp
// This will throw an exception if SomeOtherImplementation does not meet the T2 constraint
genericTypeA = genericTypeA.MakeGenericType(
    typeof(string),
    typeof(SomeOtherImplementation),
    typeof(int));
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/make-generic-instances-58131039/?t=220)

一旦成功创建了封闭泛型类型,就可以使用 `Activator.CreateInstance` 来创建对象。
得到的实例随后可以被强制转换为具体的封闭类型(如果此时该类型已知),或者通过反射访问以调用其成员。

```csharp
using System.Reflection;

var genericTypeA = Assembly
    .GetExecutingAssembly()
    .GetTypes()
    .Where(t => t.IsGenericType)
    .SingleOrDefault(t => t.Name == "GenericTypeA`3");

genericTypeA = genericTypeA.MakeGenericType(
    typeof(string),
    typeof(SomeImplementation),
    typeof(int));

var instance = Activator.CreateInstance(genericTypeA);
var specificInstance = (GenericTypeA<string, SomeImplementation, int>)instance;

specificInstance.Method1(
    "Hello, World!",
    new SomeImplementation("From Dev Leader"));
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/make-generic-instances-58131039/?t=235)

```text
GenericTypeA`3  (open, GenericTypeA<,,>)
   |
   | MakeGenericType(typeof(string), typeof(SomeImplementation), typeof(int))
   v
GenericTypeA<string, SomeImplementation, int>  (closed)
   |
   | Activator.CreateInstance(genericTypeA)
   v
instance -> (GenericTypeA<string, SomeImplementation, int>)instance
         -> specificInstance.Method1(...)
```
