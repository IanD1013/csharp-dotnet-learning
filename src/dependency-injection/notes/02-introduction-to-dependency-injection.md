# Introduction to Dependency Injection

> Course: [From Zero to Hero: Dependency Injection in .NET with C#](https://dometrain.com/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp/) · Chapter 2
> 8 lessons · ~32:21
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [The problem with dependencies](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-problem-with-dependencies-53953083/) | 6:47 | [↓](#1-the-problem-with-dependencies) |
| 2 | [Why Dependency injection is necessary](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/why-dependency-injection-is-necessary-53953084/) | 5:13 | [↓](#2-why-dependency-injection-is-necessary) |
| 3 | [A practical example of the dependency problem](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-practical-example-of-the-dependency-problem-53953085/) | 10:25 | [↓](#3-a-practical-example-of-the-dependency-problem) |
| 4 | [A less obvious example of a Dependency Injection use-case](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-less-obvious-example-of-a-dependency-injection-use-case-53953086/) | 4:24 | [↓](#4-a-less-obvious-example-of-a-dependency-injection-use-case) |
| 5 | [Dependency injection benefits past testability](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-injection-benefits-past-testability-53953087/) | 1:11 | [↓](#5-dependency-injection-benefits-past-testability) |
| 6 | [Injecting Classes vs Abstract classes vs Interfaces](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/injecting-classes-vs-abstract-classes-vs-interfaces-53953088/) | 1:51 | [↓](#6-injecting-classes-vs-abstract-classes-vs-interfaces) |
| 7 | [So do you have to do all that manually??](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/so-do-you-have-to-do-all-that-manually-53953089/) | 1:21 | [↓](#7-so-do-you-have-to-do-all-that-manually) |
| 8 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953090/) | 1:09 | [↓](#8-section-recap) |

---

## 1. The problem with dependencies

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-problem-with-dependencies-53953083/) · 6:47

### Summary

This lesson explores the fundamental issues that arise when software components are tightly coupled to their dependencies.
By examining a car modeling exercise, it demonstrates how hard-coding concrete implementations within high-level classes leads to rigid inheritance structures and significant testing challenges.
Understanding these pitfalls is essential for recognizing why Dependency Injection is a necessary pattern for building maintainable and testable .NET applications.

### Key concepts

* **Tight Coupling**: When a high-level class is directly responsible for instantiating its own dependencies, making it difficult to swap implementations.
* **Inheritance Bloat**: The tendency to create specialized subclasses (e.g., `PetrolCar`, `DieselCar`) to handle different dependency types, leading to complex and fragile hierarchies.
* **Testability Constraints**: The inability to isolate a component for testing because it is bound to a specific, concrete implementation of its dependencies.
* **High-Level vs. Low-Level Modules**: High-level modules (like a `Car`) should not depend on low-level implementation details (like a specific `PetrolEngine`).

### Lesson notes

#### The Modeling Challenge

When modeling a system like a car in C#, it is common to classify objects by their differentiators, such as engine type (petrol, diesel, or electric).
A common but flawed approach is to use inheritance to create specific types for every variation.

For example, one might create an abstract `Car` class and then derive `PetrolCar`, `DieselCar`, and `ElectricCar`.
This creates a modeling challenge because any further differentiator leads to deeper and deeper inheritance.
An `ElectricCar` might need to be further split into a `BatteryElectricCar` or a `HydrogenElectricCar`.
This model is fragile; changes to the parent class have a significant knock-on effect throughout the hierarchy.

#### Tight Coupling and Instantiation

The core problem is not just inheritance, but how dependencies are managed within these classes.
In a tightly coupled system, the high-level class (the `Car`) is responsible for instantiating its own low-level dependencies (the engine).

```csharp
namespace TheDependencyProblem.CarExample;

public class Car
{
    private readonly PetrolEngine _carEngine = new();

    public void StartEngine()
    {
        _carEngine.Start();
    }

    //More methods
}

public class PetrolEngine
{
    public void Start()
    {
        //Battery, induction coil, spark plug, fuel
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-problem-with-dependencies-53953083/?t=275)

If you decide to create a diesel version of the car, you are forced to create a new class or modify the existing one to accommodate a different concrete engine type.

```csharp
public class PetrolCar
{
    private readonly PetrolEngine _engine = new();

    public void StartEngine()
    {
        _engine.Start();
    }
}

public class PetrolEngine
{
    public void Start()
    {
        //Battery, induction coil, spark plug, fuel
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-problem-with-dependencies-53953083/?t=285)

To support a different fuel type, you would need to define a new engine class and a corresponding car class:

```csharp
public class DieselEngine
{
    public void Start()
    {
        //Ignites based on pressure
    }
}

public class DieselCar
{
    private readonly DieselEngine _engine = new();

    public void StartEngine()
    {
        _engine.Start();
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-problem-with-dependencies-53953083/?t=295)

In this scenario, the `Car` is no longer just a car; it is specifically a `PetrolCar` or a `DieselCar` because it is tied to a concrete implementation.
Ideally, a car should just be a car that *has* an engine.

#### The Impact on Testability

Depending on concrete implementations renders a system untestable in isolation.
If you need to test the wheel-spinning system or the steering system of a car in a lab environment, you cannot do so if the car depends directly on a `PetrolEngine`.
If the engine is not yet ready or available, the car cannot start, and the wheels cannot spin.

Because the high-level module (`Car`) depends on an implementation rather than an abstraction, you cannot substitute the real engine with a "mock" or a simple wheel-spinning mechanism for testing purposes.
This violation of clean architecture principles makes it impossible to verify high-level logic without also involving all low-level dependencies.

Dependency Injection is the solution to this problem, allowing high-level modules to depend on abstractions, thereby decoupling the components and enabling full testability.

---

## 2. Why Dependency injection is necessary

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/why-dependency-injection-is-necessary-53953084/) · 5:13

### Summary

Dependency injection (DI) addresses the issue of tight coupling by ensuring that high-level components depend on abstractions rather than concrete implementations.
By utilizing interfaces, a system becomes modular and testable, allowing developers to swap implementations—such as replacing a real engine with a test double—without modifying the consuming class.
This approach decouples the creation of dependencies from their usage, facilitating cleaner architecture and easier maintenance.

### Key concepts

* **Abstractions over Implementations**: High-level modules should not depend on low-level implementation details.
* **Interface Contracts**: Using interfaces like `ICarEngine` defines a set of behaviors that any implementation must satisfy.
* **Constructor Injection**: Passing dependencies through a class constructor allows the consumer to provide any implementation that matches the required abstraction.
* **Testability**: DI enables the use of "fake" or "test" implementations, allowing code to be tested in isolation without external side effects.
* **Modularity**: Systems become more flexible as components can be swapped out with minimal impact on the rest of the codebase.

### Lesson notes

In a tightly coupled system, a high-level component like a `Car` might directly instantiate its dependencies, such as a `PetrolEngine`.
This creates a hard dependency on a specific implementation, making the system rigid and difficult to extend or test.

```csharp
namespace TheDependencyProblem.CarExample;

public class Car
{
    private readonly PetrolEngine _engine = new();

    public void StartEngine()
    {
        _engine.Start();
    }
}

public class PetrolEngine
{
    public void Start()
    {
        //Battery, induction coil, spark plug, fuel
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/why-dependency-injection-is-necessary-53953084/?t=10)

To solve this, the dependency should be inverted by introducing an abstraction.
An interface, such as `ICarEngine`, defines the contract that any engine must follow—specifically, the ability to `Start()`.
Any class that wants to serve as an engine must implement this interface.

```csharp
public void StartEngine()
    {
        _engine.Start();
    }
}

public interface ICarEngine
{
    void Start();
}

public class PetrolEngine
{
    public void Start()
    {
        //Battery, induction coil, spark plug, fuel
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/why-dependency-injection-is-necessary-53953084/?t=75)

Once the interface is defined, concrete implementations like `PetrolEngine` and `DieselEngine` can implement it.
The `Car` class is then refactored to accept an `ICarEngine` through its constructor.
This is known as constructor injection.
The `Car` no longer knows or cares which specific engine it is using, as long as the object provided adheres to the `ICarEngine` contract.

```csharp
public class Car
{
    private readonly ICarEngine _carEngine;

    public Car(ICarEngine carEngine)
    {
        _carEngine = carEngine;
    }

    public void StartEngine()
    {
        _carEngine.Start();
    }
}

public interface ICarEngine
{
    void Start();
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/why-dependency-injection-is-necessary-53953084/?t=160)

This pattern significantly improves testability.
In a testing environment, a `TestEngine` can be created that implements `ICarEngine` but does not require actual fuel or complex setup.
This allows the `Car` module to be tested in isolation from the complexities of a real engine implementation.

```csharp
void Start();

        //Keep adding
    }

    public class TestEngine : ICarEngine
    {
        public void Start()
        {
            
        }
    }

    public class PetrolEngine : ICarEngine
    {
        public void Start()
        {
            //Battery, induction coil, spark plug, fuel
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/why-dependency-injection-is-necessary-53953084/?t=250)

Finally, the consuming code (such as `Program.cs`) becomes responsible for "injecting" the specific implementation into the `Car`.
This makes the application modular, as different engine types can be swapped in at the entry point of the application without changing the internal logic of the `Car` class.

```csharp
using TheDependencyProblem.CarExample;

var dieselCar = new Car(new DieselEngine());
var petrolCar = new Car(new PetrolEngine());


var testCar = new Car(new TestEngine());
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/why-dependency-injection-is-necessary-53953084/?t=265)

---

## 3. A practical example of the dependency problem

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-practical-example-of-the-dependency-problem-53953085/) · 10:25

### Summary

This lesson demonstrates the practical consequences of tight coupling in a .NET application using a database-driven example.
It illustrates how hard-coding concrete implementations like `UserRepository` or `SqliteDbConnectionFactory` prevents unit testing and limits modularity.
By applying Dependency Injection principles—specifically Inversion of Control through constructor injection and the use of interfaces—developers can decouple business logic from infrastructure, enabling the use of mock objects and interchangeable database providers.

### Key concepts

- Tight coupling vs. Loose coupling
- Inversion of Control (IoC) via constructor injection
- Programming to an abstraction (interfaces) rather than an implementation
- Testability: Mocking dependencies to isolate business logic
- Modularity: Swapping infrastructure components without changing business logic

### Lesson notes

In a typical .NET application, business logic often depends on external infrastructure like databases.
Without Dependency Injection (DI), these dependencies are usually hard-coded, creating a "dependency problem" where components are tightly coupled.

Consider a `UserService` that directly instantiates a `UserRepository`.
This makes the service dependent on a specific implementation rather than an abstraction:

```csharp
public class UserService
{
    private readonly UserRepository _userRepository = new();

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        // Do stuff here
        var users = await _userRepository.GetAllAsync();
        // Do stuff here
        return users;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        // Do stuff here
        var user = await _userRepository.GetByIdAsync(id);
        // Do stuff here
        return user;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-practical-example-of-the-dependency-problem-53953085/?t=55)

The problem compounds as we go deeper into the stack.
The `UserRepository` itself is often tied to a specific database technology, such as SQLite, by directly instantiating a factory:

```csharp
namespace TheDependencyProblem.Data;

public class UserRepository
{
    private readonly SqliteDbConnectionFactory _connectionFactory = new();

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        using var connection = await _connectionFactory.CreateDbConnectionAsync();
        return await connection.QueryAsync<User>("select * from Users");
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        const string query = "select * from Users where Id = @Id";
        using var connection = await _connectionFactory.CreateDbConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<User>(query, new { Id = id });
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-practical-example-of-the-dependency-problem-53953085/?t=100)

This tight coupling is problematic for two main reasons.
First, it hinders **testability**; you cannot unit test the `UserService` without a live database because you cannot intercept the calls to the repository.
Second, it reduces **modularity**; switching from SQLite to another provider like MySQL would require manual changes to the repository code.

To solve this, we introduce abstractions.
We define an `IDbConnectionFactory` interface and ensure our concrete factory implements it:

```csharp
namespace TheDependencyProblem.Data;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateDbConnectionAsync();
}

public class SqliteDbConnectionFactory : IDbConnectionFactory
{
    private readonly DbConnectionOptions _connectionOptions;

    public SqliteDbConnectionFactory()
    {
        _connectionOptions = new DbConnectionOptions
        {
            ConnectionString = "Data Source=./database.db"
        };
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-practical-example-of-the-dependency-problem-53953085/?t=175)

Next, we refactor `UserRepository` to use **Constructor Injection**.
Instead of the repository creating its own factory, it accepts an `IDbConnectionFactory` through its constructor.
This is known as Inverting Control:

```csharp
public class UserRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public UserRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        using var connection = await _connectionFactory.CreateDbConnectionAsync();
        return await connection.QueryAsync<User>("select * from Users");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-practical-example-of-the-dependency-problem-53953085/?t=205)

This allows the repository to work interchangeably with different database providers.
For example, a `MySqlDbConnectionFactory` can now be used without changing the repository's internal logic:

```csharp
public class MySqlDbConnectionFactory : IDbConnectionFactory
{
    public async Task<IDbConnection> CreateDbConnectionAsync()
    {
        return new MySqlConnection();
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-practical-example-of-the-dependency-problem-53953085/?t=265)

Finally, we apply the same principle to the `UserService` by introducing an `IUserRepository` interface.
This decouples the service from the specific data access implementation:

```csharp
public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        // Do stuff here
        var users = await _userRepository.GetAllAsync();
        // Do stuff here
        return users;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-practical-example-of-the-dependency-problem-53953085/?t=430)

With these abstractions in place, the code becomes fully testable.
In a unit test project, we can provide a `FakeUserRepository` that uses an in-memory dictionary instead of a real database:

```csharp
public class FakeUserRepository : IUserRepository
{
    private readonly Dictionary<Guid, User> _users = new();

    public Task<IEnumerable<User>> GetAllAsync()
    {
        return Task.FromResult(_users.Values);
    }

    public Task<User?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-practical-example-of-the-dependency-problem-53953085/?t=505)

More realistically, developers use mocking libraries like **NSubstitute** to define behavior dynamically.
This avoids the need to manually create fake classes for every test scenario:

```csharp
public class UserRepositoryTests
{
    private readonly UserService _userService;
    private readonly IUserRepository _mockUserRepository = Substitute.For<IUserRepository>();

    public UserRepositoryTests()
    {
        _userService = new UserService(_mockUserRepository);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-practical-example-of-the-dependency-problem-53953085/?t=565)

By depending on abstractions and inverting control to the constructor, the application becomes modular, maintainable, and easily testable without requiring external dependencies like a database to be present during unit testing.

---

## 4. A less obvious example of a Dependency Injection use-case

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-less-obvious-example-of-a-dependency-injection-use-case-53953086/) · 4:24

### Summary

This lesson demonstrates how to apply the Dependency Inversion Principle to system-level dependencies that are often overlooked, specifically the system clock.
By abstracting DateTime.Now behind an interface, developers can transform non-deterministic, untestable code into a modular system where time-based logic can be verified through unit tests using mocked providers.

### Key concepts

- Identifying hidden dependencies on system resources.
- The problem of non-deterministic code in unit testing.
- Abstracting DateTime.Now using the IDateTimeProvider interface.
- Implementing a SystemDateTimeProvider for production use.
- Injecting abstractions to achieve modularity and testability.

### Lesson notes

While some use cases for Dependency Injection are immediately apparent, others involve system-level implementations that are hidden in plain sight.
A primary example is the use of the system clock via `DateTime.Now`.

Consider a `Greeter` class with a method that generates a message based on the current time:

```csharp
public class Greeter
{
    public string CreateGreetMessage()
    {
        var dateTimeNow = DateTime.Now;
        return dateTimeNow.Hour switch
        {
            >= 5 and < 12 => "Good morning",
            >= 12 and < 18 => "Good afternoon",
            _ => "Good evening"
        };
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-less-obvious-example-of-a-dependency-injection-use-case-53953086/?t=25)

In a standard execution environment, this class works as expected.
For instance, if called in the morning, it correctly returns "Good morning."

```csharp
using ...

var greeter = new Greeter();

var message = greeter.CreateGreetMessage();

Console.WriteLine(message);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-less-obvious-example-of-a-dependency-injection-use-case-53953086/?t=70)

However, this implementation has a significant flaw: it cannot be unit tested for all scenarios.
Because the class is tightly coupled to the system clock (`DateTime.Now`), a developer cannot write a test to verify the "Good afternoon" logic unless they run the test during those specific hours.
The code depends on a concrete implementation detail of the operating system rather than an abstraction.

To solve this, apply the Dependency Inversion Principle by creating an abstraction for the date and time provider.
This involves defining an interface and a concrete implementation that wraps the system clock.

```csharp
public interface IDateTimeProvider
{
    public DateTime DateTimeNow { get; }
}

public class SystemDateTimeProvider
{

}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-less-obvious-example-of-a-dependency-injection-use-case-53953086/?t=175)

The `Greeter` class is refactored to accept the `IDateTimeProvider` interface through its constructor.
This allows the class to use any implementation of the provider—whether it is the real system clock or a mocked version for testing.

```csharp
public Greeter(IDateTimeProvider dateTimeProvider)
{
    _dateTimeProvider = dateTimeProvider;
}

public string CreateGreetMessage()
{
    var dateTimeNow = DateTime.Now;
    return dateTimeNow.Hour switch
    {
        >= 5 and < 12 => "Good morning",
        >= 12 and < 18 => "Good afternoon",
        _ => "Good evening"
    };
    }
}

public interface IDateTimeProvider
{
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-less-obvious-example-of-a-dependency-injection-use-case-53953086/?t=235)

Note that to fully implement this change, the `CreateGreetMessage` method must be updated to use the injected `_dateTimeProvider.DateTimeNow` instead of the static `DateTime.Now` call.

Finally, the application can be initialized by injecting the concrete `SystemDateTimeProvider`.

```csharp
using ...


var greeter = new Greeter(new SystemDateTimeProvider());

var message = greeter.CreateGreetMessage();

Console.WriteLine(message);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-less-obvious-example-of-a-dependency-injection-use-case-53953086/?t=225)

By depending on the abstraction, the code becomes modular and fully testable, as a mock provider can now be injected to return any specific time required for a test case.

---

## 5. Dependency injection benefits past testability

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-injection-benefits-past-testability-53953087/) · 1:11

### Summary

Dependency Injection (DI) provides significant advantages beyond unit testing, including sophisticated management of dependency lifetimes and the implementation of structural design patterns like Decorators and Interceptors.
By decoupling abstractions from their implementations, DI enables cleaner, more reusable code and facilitates safer refactoring through improved testability.

### Key concepts

- **Lifetime Management**: DI containers allow for precise control over the lifecycle of dependencies (e.g., Singleton, Scoped, or Transient).
- **Structural Design Patterns**: DI facilitates the use of Decorators and Interceptors, allowing for cross-cutting concerns to be handled cleanly.
- **Code Reusability**: By depending on abstractions rather than concrete implementations, code becomes more modular and reusable.
- **Refactoring Safety**: Even if testability were the only benefit, the ability to write reliable unit tests makes code significantly safer to refactor.

### Lesson notes

While testability is often the most immediate benefit of Dependency Injection, it is not the only one.
DI serves as a foundation for several advanced architectural patterns and practices that improve the overall quality and maintainability of a codebase.

One of the primary advantages is the ability to manage the **lifetimes** of dependencies.
Instead of a class being responsible for creating and destroying its own dependencies, the DI container manages these lifecycles, ensuring that resources are shared or disposed of correctly based on the application's needs.

Furthermore, DI enables the implementation of the **Decorator** and **Interceptor** patterns.
These patterns allow developers to wrap or intercept calls to a dependency to add functionality—such as logging, caching, or validation—without modifying the original implementation.
This is made possible because the consuming class depends on an abstraction (an interface) rather than a specific concrete class.

```csharp
public Greeter(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public string CreateGreetMessage()
    {
        var dateTimeNow = _dateTimeProvider.DateTimeNow;
        return dateTimeNow.Hour switch
        {
            >= 5 and < 12 => "Good morning",
            >= 12 and < 18 => "Good afternoon",
            _ => "Good evening"
        };
    }
}

public interface IDateTimeProvider
{
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-injection-benefits-past-testability-53953087/?t=10)

Even if testability were the sole benefit, it would remain a critical reason to adopt DI.
Code that is easily testable is inherently safer.
When unit tests are comprehensive, developers can refactor logic with confidence, knowing that any regressions will be caught immediately.
DI facilitates this by allowing the injection of mock or stub implementations of interfaces like `IDateTimeProvider` during testing.

---

## 6. Injecting Classes vs Abstract classes vs Interfaces

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/injecting-classes-vs-abstract-classes-vs-interfaces-53953088/) · 1:51

### Summary

This lesson explores the technical and architectural differences between injecting concrete classes, abstract classes, and interfaces in .NET.
While dependency injection containers can technically resolve concrete types, adhering to the Dependency Inversion Principle requires depending on abstractions to ensure code remains decoupled, testable, and maintainable.
Interfaces are the recommended choice due to their full mockability and alignment with the principle of composition over inheritance, though they should be applied pragmatically based on the need for testability or swappability.

### Key concepts

*   **Dependency Inversion Principle (DIP):** High-level components should depend on abstractions, not on concrete implementations.
*   **Concrete Injection:** Injecting a specific class (e.g., `SystemDateTimeProvider`) technically works but couples the consumer to that specific implementation.
*   **Abstract Classes:** These provide a level of abstraction but are not fully mockable and enforce inheritance hierarchies.
*   **Interfaces:** The preferred abstraction in C# because they are fully mockable and support composition over inheritance.
*   **Pragmatic Abstraction:** Interfaces should be used when unit testability or swappability is required, rather than as a default for every class.

### Lesson notes

In .NET, you have the flexibility to inject concrete classes, abstract classes, or interfaces.
While the dependency injection container can handle all three, the choice significantly impacts the architecture and testability of your application.

Consider a `Greeter` class that depends on an interface:

```csharp
namespace TheDependencyProblem;

public class Greeter
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public Greeter(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public string CreateGreetMessage()
    {
        var dateTimeNow = _dateTimeProvider.DateTimeNow;
        return dateTimeNow.Hour switch
        {
            >= 5 and < 12 => "Good morning",
            >= 12 and < 18 => "Good afternoon",
            _ => "Good evening"
        };
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/injecting-classes-vs-abstract-classes-vs-interfaces-53953088/?t=10)

If you were to change the constructor to accept a concrete implementation like `SystemDateTimeProvider`, the code would still function and technically "inject" a dependency.
However, this approach violates the Dependency Inversion Principle.
By depending on a specific implementation, you lose the benefits of abstraction.

```csharp
namespace TheDependencyProblem;

public class Greeter
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public Greeter(SystemDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public string CreateGreetMessage()
    {
        var dateTimeNow = _dateTimeProvider.DateTimeNow;
        return dateTimeNow.Hour switch
        {
            >= 5 and < 12 => "Good morning",
            >= 12 and < 18 => "Good afternoon",
            _ => "Good evening"
        };
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/injecting-classes-vs-abstract-classes-vs-interfaces-53953088/?t=25)

Abstract classes can serve as abstractions to a degree.
However, interfaces are generally preferred in the C# ecosystem.
Abstract classes are not fully mockable in many testing frameworks, whereas interfaces are.
Furthermore, modern C# development tends to favor composition over inheritance; abstract classes force an inheritance hierarchy that can become rigid and less desirable over time.

```csharp
namespace TheDependencyProblem;

public class Greeter
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public Greeter(DateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public string CreateGreetMessage()
    {
        var dateTimeNow = _dateTimeProvider.DateTimeNow;
        return dateTimeNow.Hour switch
        {
            >= 5 and < 12 => "Good morning",
            >= 12 and < 18 => "Good afternoon",
            _ => "Good evening"
        };
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/injecting-classes-vs-abstract-classes-vs-interfaces-53953088/?t=70)

The final recommendation is to prefer interfaces over concrete or abstract classes because they make unit testing and swapping implementations significantly easier.
However, you do not need an interface for every single class.
The decision to create an interface should be based on whether the component needs to be unit testable or if the implementation needs to be swappable.

```csharp
namespace TheDependencyProblem;

public class Greeter
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public Greeter(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public string CreateGreetMessage()
    {
        var dateTimeNow = _dateTimeProvider.DateTimeNow;
        return dateTimeNow.Hour switch
        {
            >= 5 and < 12 => "Good morning",
            >= 12 and < 18 => "Good afternoon",
            _ => "Good evening"
        };
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/injecting-classes-vs-abstract-classes-vs-interfaces-53953088/?t=85)

---

## 7. So do you have to do all that manually??

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/so-do-you-have-to-do-all-that-manually-53953089/) · 1:21

### Summary

This lesson addresses the practicality of manual dependency injection, introducing the Dependency Injection (DI) framework (or IoC container) as the standard solution in .NET.
While manual instantiation is technically possible, .NET provides a built-in, high-performance DI framework that automates object creation and lifetime management, such as singletons and scoped services, eliminating the need for developers to manually wire up dependencies.

### Key concepts

- **DI Framework / IoC Container**: A library or tool that automates the creation and injection of dependencies.
- **Manual Injection**: The process of manually instantiating classes and passing their dependencies through constructors.
- **Service Lifetimes**: Management of how long an object lives (e.g., Singletons, Scoped, or Transient), which is handled automatically by the DI framework.
- **Built-in .NET Support**: ASP.NET Core and modern .NET include a native, high-performance DI container out of the box.

### Lesson notes

When implementing dependency injection, a common question is whether developers must manually instantiate every class and its dependencies.
In a simple scenario, you might have a class like `Greeter` that depends on an `IDateTimeProvider` to determine the appropriate greeting based on the time of day:

```csharp
namespace TheDependencyProblem;

public class Greeter
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public Greeter(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public string CreateGreetMessage()
    {
        var dateTimeNow = _dateTimeProvider.DateTimeNow;
        return dateTimeNow.Hour switch
        {
            >= 5 and < 12 => "Good morning",
            >= 12 and < 18 => "Good afternoon",
            _ => "Good evening"
        };
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/so-do-you-have-to-do-all-that-manually-53953089/?t=10)

To use this class, you would traditionally need to manually create the implementation of the dependency and pass it into the constructor every time you want to use the service:

```csharp
using ...

var greeter = new Greeter(new SystemDateTimeProvider());

var message = greeter.CreateGreetMessage();

Console.WriteLine(message);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/so-do-you-have-to-do-all-that-manually-53953089/?t=25)

While this manual approach is technically possible, it becomes unmanageable as applications grow.
Managing object lifetimes—such as ensuring a service acts as a singleton or has a narrow scope—requires significant boilerplate code and manual tracking.

To solve this, developers use a Dependency Injection (DI) framework, also known as an Inversion of Control (IoC) container.
This tool acts as a library that manages the instantiation and injection of services automatically.
Since .NET Core 1.0, Microsoft has included a built-in DI framework that is both fast and feature-rich.
This framework is the standard for modern .NET development, including ASP.NET Core.
It removes the need for manual wiring, allowing developers to focus on application logic while the container handles the complexities of dependency resolution and lifecycle management.

---

## 8. Section recap

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953090/) · 1:09

### Summary

This lesson provides a comprehensive recap of the foundational principles of Dependency Injection (DI) in .NET.
It reviews the core problems DI addresses—specifically in the context of databases, I/O, and networking—and explores its application for system-level dependencies like date and time providers.
The lesson emphasizes the benefits of DI beyond testability and introduces the built-in .NET dependency injection framework as the standard mechanism for managing dependencies across all modern versions of the platform.

### Key concepts

- Identification of the architectural problems solved by Dependency Injection.
- Common DI use cases: Databases, I/O operations, and networking.
- System-level DI: Abstracting system components like DateTime.
- Comparison of DI implementations: Concrete classes, abstract classes, and interfaces.
- The role and stability of the built-in .NET dependency injection framework.

### Lesson notes

The section established the fundamental necessity of Dependency Injection (DI) by identifying the architectural challenges it solves.
DI is particularly critical in modern software development when managing interactions with external resources such as databases, file I/O, and network services.

A less obvious but equally important use case for DI involves system-level dependencies.
For instance, using a provider for DateTime allows developers to decouple their logic from the system clock, which is essential for creating deterministic tests and managing time-sensitive logic.

While DI significantly improves testability, it offers broader architectural benefits.
When implementing DI, developers can utilize concrete classes, abstract classes, or interfaces.
Interface-based DI is the recommended approach for achieving the highest degree of decoupling and flexibility.

In the .NET ecosystem, manual implementation of DI patterns is rarely necessary.
The platform includes a built-in dependency injection framework that has been a core component since .NET Core 1.0.
This framework remains consistent across .NET 5, .NET 6, and future versions, providing a stable foundation for managing object lifecycles and dependencies.
