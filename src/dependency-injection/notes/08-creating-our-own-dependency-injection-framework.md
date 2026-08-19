# Creating our own Dependency Injection framework

> Course: [From Zero to Hero: Dependency Injection in .NET with C#](https://dometrain.com/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp/) · Chapter 8
> 5 lessons · ~35:28
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Why should we even bother?](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/why-should-we-even-bother-53953324/) | 1:25 | [↓](#1-why-should-we-even-bother) |
| 2 | [The design](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-design-53953326/) | 2:57 | [↓](#2-the-design) |
| 3 | [The implementation](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-implementation-53953327/) | 19:57 | [↓](#3-the-implementation) |
| 4 | [Extending the main implementation](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/extending-the-main-implementation-53953328/) | 10:36 | [↓](#4-extending-the-main-implementation) |
| 5 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953329/) | 0:33 | [↓](#5-section-recap) |

---

## 1. Why should we even bother?

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/why-should-we-even-bother-53953324/) · 1:25

### Summary

Building a custom Dependency Injection (DI) framework from scratch is a pedagogical exercise designed to deepen understanding of Inversion of Control (IoC) containers.
By deconstructing DI into its basic components, developers can grasp the underlying logic and architectural patterns that are often hidden behind high-level APIs.
This process not only clarifies how dependencies are managed but also introduces coding practices that can be adapted to improve general software implementation.

### Key concepts

* **Learning through deconstruction**: Gaining mastery by breaking a complex system down into its fundamental parts.
* **Internal mechanics of IoC**: Understanding how a container manages service registration and resolution behind the scenes.
* **Pattern adaptation**: Identifying logic and practices within framework design that can be applied to standard application development.
* **Core architectural understanding**: Moving beyond API usage to understand the principles of dependency management.

### Lesson notes

The objective of this section is to implement a functional Dependency Injection (DI) framework from the ground up.
This exercise is rooted in the principle that true mastery of a system is achieved by deconstructing it into its fundamental components.
By building an Inversion of Control (IoC) container from scratch, the internal mechanics that are typically abstracted away become visible and understandable.

While building a custom DI framework is an optional exercise and not strictly necessary for standard application development, there is significant benefit in understanding the underlying logic.
This deep dive provides a realistic look at the challenges and solutions involved in dependency management.
Furthermore, observing the implementation process reveals architectural patterns and best practices that can be directly adapted to enhance the quality and maintainability of general software code.

---

## 2. The design

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-design-53953326/) · 2:57

### Summary

This lesson outlines the architectural goals and API design for building a custom Dependency Injection (DI) framework in .NET.
The objective is to replicate the look and feel of the built-in .NET DI container, including familiar patterns like service collections, service providers, and standard registration methods such as AddSingleton.
By mirroring the existing Microsoft API, the custom framework remains intuitive while demonstrating the internal mechanics of service registration, lifetime management, and resolution without external dependencies.

### Key concepts

- **API Parity**: Designing the custom framework to match the `Microsoft.Extensions.DependencyInjection` API for familiarity.
- **Service Collection**: A container for registering service descriptors.
- **Service Provider**: The engine responsible for resolving and managing the lifecycle of services.
- **Service Descriptors**: Metadata describing the service type, implementation type, and lifetime.
- **Resolution Methods**: Implementing `GetRequiredService` to retrieve non-nullable service instances.
- **Testability**: Using interfaces to wrap static calls like `Console.WriteLine` for better mockability.

### Lesson notes

The project begins with a standard console application named `Consumer.ConsoleApp`.
This application serves as the testbed for the custom DI framework.

```csharp
// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-design-53953326/?t=25)

The design goal is to achieve API parity with the built-in .NET DI container.
This involves implementing a `ServiceCollection` to hold registrations and a `ServiceProvider` to resolve them.
By using the same naming conventions—such as `AddSingleton`, `AddTransient`, and `GetRequiredService`—the framework remains easy to use for developers already familiar with the standard .NET ecosystem.

```csharp
var services = new ServiceCollection();


var serviceProvider = services.BuildServiceProvider();

var service = serviceProvider.GetRequiredService();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-design-53953326/?t=55)

To demonstrate the framework, a simple logging service is defined.
Wrapping `Console.WriteLine` in an interface like `IConsoleWriter` is a standard practice to improve testability, as it allows the console output to be mocked or validated during unit testing.

```csharp
namespace Consumer.ConsoleApp;

public class ConsoleWriter : IConsoleWriter
{
    public void WriteLine(string text)
    {
        Console.WriteLine();
    }
}

public interface IConsoleWriter
{
    void WriteLine(string text);
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-design-53953326/?t=115)

The final design allows for registering services with specific lifetimes and resolving them via the service provider.
The implementation will support generic methods for registration and resolution, ensuring a type-safe developer experience.
The framework is built using only the .NET SDK, with zero external dependencies, to illustrate the core logic of dependency injection.

```csharp
using Consumer.ConsoleApp;

var services = new ServiceCollection();

services.AddSingleton<IConsoleWriter, ConsoleWriter>();

var serviceProvider = services.BuildServiceProvider();

var service = serviceProvider.GetRequiredService<IConsoleWriter>();

service.WriteLine("Hello from DI");
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-design-53953326/?t=145)

---

## 3. The implementation

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-implementation-53953327/) · 19:57

**Summary**
This lesson demonstrates the step-by-step creation of a custom Dependency Injection (DI) framework named "Vax."
It covers the implementation of a service collection for registration, a service provider for resolution, and the logic required to handle both transient and singleton lifetimes, including recursive constructor parameter resolution using reflection.

**Key concepts**
* **ServiceCollection**: A specialized list of service descriptors used to register dependencies.
* **ServiceDescriptor**: A metadata container storing the service type, implementation type, and lifetime.
* **ServiceLifetime**: An enumeration defining how instances are managed (Transient or Singleton).
* **ServiceProvider**: The core engine that resolves and provides service instances.
* **Recursive Resolution**: The process of automatically resolving a service's constructor parameters by querying the container.
* **Lazy Initialization**: Ensuring Singleton services are only instantiated when first requested to avoid registration order issues.

**Lesson notes**
The framework begins with the `ServiceCollection`, which acts as a registry for dependencies.
It inherits from `List<ServiceDescriptor>`, allowing it to store the definitions of the services that the container will eventually manage.

```csharp
namespace Vax;

public class ServiceCollection : List<ServiceDescriptor>
{

}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-implementation-53953327/?t=70)

Each entry in the collection is a `ServiceDescriptor`.
This class holds the `ServiceType` (the interface or base class), the `ImplementationType` (the concrete class to instantiate), and the `ServiceLifetime`.

```csharp
namespace Vax;

public class ServiceDescriptor
{
    public Type ServiceType { get; init; } = default!;

    public Type? ImplementationType { get;
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-implementation-53953327/?t=310)

To register services, the `ServiceCollection` provides methods like `AddSingleton` and `AddTransient`.
These methods create a new `ServiceDescriptor` and add it to the internal list.

```csharp
public ServiceCollection AddSingleton<TService, TImplementation>()
{
    var serviceDescriptor = new ServiceDescriptor
    {
        ServiceType = typeof(TService),
        ImplementationType = typeof(TImplementation)
    };
    Add(serviceDescriptor);
    return this;
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-implementation-53953327/?t=295)

Once all services are registered, the `BuildServiceProvider` method is called to create the `ServiceProvider`.
This method passes the current collection to the provider's constructor.

```csharp
public ServiceProvider BuildServiceProvider()
    {
        return new ServiceProvider(this);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-implementation-53953327/?t=385)

The `ServiceProvider` is responsible for the actual resolution of types.
It maintains two internal dictionaries: one for transient services (storing a factory function) and one for singleton services (storing a `Lazy<object>`).

```csharp
public class ServiceProvider
{
    private readonly Dictionary<Type, Func<object>> _transientTypes = new();
    private readonly Dictionary<Type, Lazy<object>> _singletonTypes = new();

    internal ServiceProvider(ServiceCollection serviceCollection)
    { 

    }

    public T? GetService<T>()
    { 
        return (T?)GetService(typeof(T));
    }

    public object? GetService(Type serviceType)
    { 
        throw new NotImplementedException();
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-implementation-53953327/?t=595)

The `GenerateServices` method iterates through the `ServiceCollection` and populates these dictionaries.
For singletons, `Activator.CreateInstance` is wrapped in a `Lazy<object>` to ensure that the service is only created when needed, which prevents issues if dependencies are registered out of order.

```csharp
private void GenerateServices(ServiceCollection serviceCollection)
{
    foreach (var serviceDescriptor in serviceCollection)
    {
        switch (serviceDescriptor.Lifetime)
        {
            case ServiceLifetime.Singleton:
                _singletonTypes[serviceDescriptor.ServiceType] = 
                    new Lazy<object>(() =>
                        Activator.CreateInstance(serviceDescriptor.ImplementationType,
                            GetConstructorParameters(serviceDescriptor))!);
                continue;
            case ServiceLifetime.Transient:
                _transientTypes[serviceDescriptor.ServiceType] =
                    () => Activator.CreateInstance(serviceDescriptor.ImplementationType,
                        GetConstructorParameters(serviceDescriptor))!;
                continue;
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-implementation-53953327/?t=880)

To handle services that have their own dependencies, the framework uses a recursive method called `GetConstructorParameters`.
This method inspects the first constructor of the implementation type, retrieves its parameters, and calls `GetService` for each parameter type to resolve the dependency tree.

```csharp
private object?[] GetConstructorParameters(ServiceDescriptor descriptor)
{
    var constructorInfo = descriptor.ImplementationType.GetConstructors().First();
    var parameters = constructorInfo.GetParameters()
        .Select(x => GetService(x.ParameterType)).ToArray();

    return null;
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-implementation-53953327/?t=805)

The `GetService` method logic first checks the singleton dictionary.
If a match is found, it returns the value from the `Lazy` wrapper.
If not, it checks the transient dictionary and invokes the factory function.

```csharp
public object? GetService(Type serviceType)
{
    var service = _singletonTypes.GetValueOrDefault(serviceType);

    if (service is not null)
    {
        return service.Value;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-implementation-53953327/?t=910)

Finally, the framework can be used to resolve complex dependency trees.
For instance, an `IdGenerator` that depends on an `IConsoleWriter` can be registered and resolved automatically.

```csharp
using Vax;

var services = new ServiceCollection();

services.AddSingleton<IConsoleWriter, ConsoleWriter>();
services.AddSingleton<IIdGenerator, IdGenerator>();

var serviceProvider = services.BuildServiceProvider();

var service1 = serviceProvider.GetService<IIdGenerator>();
var service2 = serviceProvider.GetService<IIdGenerator>();

service1.PrintId();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-implementation-53953327/?t=1130)

---

## 4. Extending the main implementation

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/extending-the-main-implementation-53953328/) · 10:36

### Summary

This lesson focuses on enhancing a custom Dependency Injection framework to achieve feature parity with the built-in .NET DI container.
Key improvements include adding generic constraints to ensure type safety during registration, supporting self-registered services, allowing the registration of pre-existing object instances, and implementing factory-based registration.
The lesson also covers the necessary modifications to the ServiceProvider to resolve these new registration types, ensuring both singleton and transient lifecycles are respected.

### Key concepts

- Generic constraints for type safety (`where TImplementation : class, TService`).
- Self-registration overloads (`AddSingleton<TService>`).
- Manual `ServiceDescriptor` registration.
- Instance-based registration for existing objects.
- Factory-based registration using `Func<ServiceProvider, TService>`.
- Updating `ServiceProvider` resolution logic to handle instances and factories.

### Lesson notes

#### Type Safety with Generic Constraints

To prevent invalid registrations where a concrete class does not implement the specified interface, generic constraints must be added to the `AddSingleton` and `AddTransient` methods.
By specifying `where TImplementation : class, TService`, the compiler ensures that the implementation type is compatible with the service type.

```csharp
namespace Vax;

public class ServiceCollection : List<ServiceDescriptor>
{
    public ServiceCollection AddSingleton<TService, TImplementation>()
        where TService : class
        where TImplementation : class, TService
    {
        var serviceDescriptor = AddServiceDescriptorWithLifetime<TService, TImplementation>(Se
        Add(serviceDescriptor);
        return this;
    }

    public ServiceCollection AddTransient<TService, TImplementation>()
        where TService : class
        where TImplementation : class, TService
    {
        var serviceDescriptor = AddServiceDescriptorWithLifetime<TService, TImplementation>(Se
        Add(serviceDescriptor);
        return this;
    }
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/extending-the-main-implementation-53953328/?t=55)

#### Self-Registration and Manual Descriptors

In many cases, a service is registered as its own implementation.
Overloads are added to support this "self-registration" pattern, simplifying the API for users who do not need to map an interface to a class.

```csharp
ollection : List<ServiceDescriptor>

lection AddSingleton<TService>()
e : class

scriptor = AddServiceDescriptorWithLifetime<TService, TService>(ServiceLifetime.Singleton);
scriptor);


lection AddTransient<TService>()
e : class

scriptor = AddServiceDescriptorWithLifetime<TService, TService>(ServiceLifetime.Singleton);
scriptor);


lection AddSingleton<TService, TImplementation>()
e : class
entation : class, TService
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/extending-the-main-implementation-53953328/?t=85)

Additionally, providing an `AddService` method allows users to manually register their own `ServiceDescriptor` instances, offering maximum flexibility.

```csharp
namespace Vax;

public class ServiceCollection : List<ServiceDescriptor>
{
    public ServiceCollection AddService(ServiceDescriptor descriptor)
    {
        Add(descriptor);
        return this;
    }

    public ServiceCollection AddSingleton<TService>()
        where TService : class
    {
        var serviceDescriptor = AddServiceDescriptorWithLifetime<TService, TService>(ServiceLi
        Add(serviceDescriptor);
        return this;
    }

    public ServiceCollection AddTransient<TService>()
        where TService : class
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/extending-the-main-implementation-53953328/?t=175)

#### Registering Existing Instances

To support scenarios where an object has already been instantiated outside the container, an `AddSingleton` overload is implemented that accepts an `object`.
This instance is stored directly in the `ServiceDescriptor`.

```csharp
public class ServiceCollection : List<ServiceDescriptor>
{
    public ServiceCollection AddService(ServiceDescriptor descriptor)
    {
        Add(descriptor);
        return this;
    }

    public ServiceCollection AddSingleton(object implementation)
    {
        var serviceType = implementation.GetType();
        var serviceDescriptor = new ServiceDescriptor
        {
            ServiceType = serviceType,
            ImplementationType = serviceType,
            Implementation = implementation,
            Lifetime = ServiceLifetime.Singleton
        };
    }
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/extending-the-main-implementation-53953328/?t=220)

#### Factory-Based Registration

Factory-based registration allows for custom instantiation logic.
The `ServiceDescriptor` is updated to include an `ImplementationFactory` property, which is a function that takes a `ServiceProvider` and returns the resolved object.

```csharp
public class ServiceDescriptor
{
    public Type ServiceType { get; init; } = default!;

    public Type ImplementationType { get; set; }

    public object? Implementation { get; set; }

    public Func<ServiceProvider, object>? ImplementationFactory { get; set; }

    public ServiceLifetime Lifetime { get; set; }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/extending-the-main-implementation-53953328/?t=430)

The `ServiceCollection` is then updated with overloads to accept these factories.

```csharp
ImplementationFactory = factory,
            Lifetime = ServiceLifetime.Singleton
        };

        Add(serviceDescriptor);
        return this;
    }

    public ServiceCollection AddSingleton<TService>(Func<ServiceProvider, TService> factory)
        where TService : class
    {
        var serviceDescriptor = new ServiceDescriptor
        {
            ServiceType = typeof(TService),
            ImplementationType = typeof(TService),
            ImplementationFactory = factory,
            Lifetime = ServiceLifetime.Singleton
        };

        Add(serviceDescriptor);
        return this;
    }
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/extending-the-main-implementation-53953328/?t=460)

#### Updating the Service Provider Logic

The `ServiceProvider` must be modified to handle these new registration types within its `GenerateServices` method.
For singletons, the provider checks if a pre-existing instance or a factory is available before defaulting to reflection-based instantiation.

```csharp
case ServiceLifetime.Singleton:
                    if (serviceDescriptor.Implementation is not null)
                    {
                        _singletonTypes[serviceDescriptor.ServiceType] =
                            new Lazy<object>(serviceDescriptor.Implementation);
                        continue;
                    }

                    if (serviceDescriptor.ImplementationFactory is not null)
                    {
                        _singletonTypes[serviceDescriptor.ServiceType] =
                            new Lazy<object>(() =>
                                serviceDescriptor.ImplementationFactory(this));
                        continue;
                    }

                    _singletonTypes[serviceDescriptor.ServiceType] =
                        new Lazy<object>(() =>
                            Activator.CreateInstance(serviceDescriptor.ImplementationType,
                                GetConstructorParameters(serviceDescriptor))!);
                    continue;
                case ServiceLifetime.Transient:
                    _transientTypes[serviceDescriptor.ServiceType] =
                        () => Activator.CreateInstance(serviceDescriptor.I
                            GetConstructorParameters(serviceDescriptor
                    continue;
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/extending-the-main-implementation-53953328/?t=490)

Similar logic is applied to transient services, ensuring that the factory is invoked every time the service is requested.

```csharp
GetConstructorParameters(serviceDescriptor))!);
                    continue;
                case ServiceLifetime.Transient:
                    if (serviceDescriptor.ImplementationFactory is not null)
                    {
                        _transientTypes[serviceDescriptor.ServiceType] =
                            () => serviceDescriptor.ImplementationFactory(this);
                        continue;
                    }

                    _transientTypes[serviceDescriptor.ServiceType] =
                        () => Activator.CreateInstance(serviceDescriptor.ImplementationType,
                            GetConstructorParameters(serviceDescriptor))!;
                    continue;
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/extending-the-main-implementation-53953328/?t=535)

---

## 5. Section recap

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953329/) · 0:33

### Summary

This lesson concludes the section on building a custom Dependency Injection (DI) framework in .NET.
It reviews the architectural approach of mimicking the standard Microsoft DI container's API surface, the implementation of core registration and resolution logic, and the extension of the framework to support features like singleton lifetimes and factory-based service registration.

### Key concepts

* Designing a custom IoC container with a familiar Microsoft-style API.
* Implementing a ServiceCollection for service registration.
* Building a ServiceProvider to handle dependency resolution.
* Supporting different registration methods, including direct instances and factory delegates.
* Managing service lifetimes such as Singletons.

### Lesson notes

The development of the custom DI framework focused on creating a functional Inversion of Control (IoC) container that mirrors the look and feel of the standard .NET implementation.
This approach ensures that developers familiar with the Microsoft DI container can easily transition to using this custom framework.

The implementation process involved establishing the core mechanics of service registration through a `ServiceCollection`.
This collection stores the definitions of how services should be created and managed.
Once registrations are complete, the `BuildServiceProvider` method is invoked to create the engine responsible for resolving these dependencies at runtime.

The framework supports various registration patterns, including the ability to register existing instances and the use of factory delegates.
Factory delegates are particularly powerful as they allow for manual control over instantiation while still leveraging the provider to resolve nested dependencies.

```csharp
using Consumer.ConsoleApp;
using Vax;

var services = new ServiceCollection();

// services.AddSingleton<IConsoleWriter, ConsoleWriter>();
// services.AddSingleton<IIdGenerator, IdGenerator>();
//

//services.AddSingleton<ConsoleWriter>();
services.AddSingleton(new ConsoleWriter());

services.AddSingleton(
    provider => new IdGenerator(provider.GetService<ConsoleWriter>()!));

var serviceProvider = services.BuildServiceProvider();

var service1 = serviceProvider.GetService<IdGenerator>();
var service2 = serviceProvider.GetService<IdGenerator>();

service1.PrintId();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953329/?t=10)

By implementing these features, the custom framework achieves feature parity with basic DI containers, allowing for singleton management and complex dependency resolution through a clean, fluent API.
