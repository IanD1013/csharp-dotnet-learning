# Growing Code Bases

> Course: [Deep Dive: C#](https://dometrain.com/course/deep-dive-csharp/) · Chapter 6
> 3 lessons · ~17:36
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Multi-project solutions](https://dometrain.com/take/course/deep-dive-csharp-2732260/multi-project-solutions-54135575/) | 6:48 | [↓](#1-multi-project-solutions) |
| 2 | [Internal access modifier](https://dometrain.com/take/course/deep-dive-csharp-2732260/internal-access-modifier-54135576/) | 5:34 | [↓](#2-internal-access-modifier) |
| 3 | [NuGet Packages](https://dometrain.com/take/course/deep-dive-csharp-2732260/nuget-packages-54135577/) | 5:14 | [↓](#3-nuget-packages) |

---

## 1. Multi-project solutions

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/multi-project-solutions-54135575/) · 6:48

As applications grow in complexity, organizing code into multiple projects within a single solution becomes essential for maintainability and reusability.
This lesson covers the practical steps of creating console applications and class libraries in Visual Studio, establishing project references, and understanding how the build system handles dependencies and access modifiers across assembly boundaries.

### Key concepts

* Code organization and reusability
* Solution vs. Project hierarchy
* Console applications as entry points
* Class libraries for shared logic
* Project references and assembly dependencies
* Access modifiers across assembly boundaries
* Build order and binary output management

### Lesson notes

The transition from simple, single-project programs to complex software applications requires a shift in how code is structured.
While single projects are sufficient for small examples with one entry point, larger systems require splitting code into multiple projects to improve maintainability and facilitate code reuse.
For instance, helper functions or custom logging implementations can be moved into a dedicated library project and shared across multiple applications.

In Visual Studio, new projects are added by right-clicking the solution and selecting the "Add New Project" menu.
A typical solution might include a console application, which serves as the program's entry point, and a class library.

```csharp
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/multi-project-solutions-54135575/?t=100)

To ensure the correct project runs during debugging, it must be designated as the "Startup Project" in the Solution Explorer.
This is indicated by the project name appearing in bold.

When creating a class library, the `public` access modifier is used to expose classes to external assemblies.
If a class is marked `public`, it becomes visible to any project that holds a reference to that assembly.

```csharp
namespace MultiProject.ClassLibrary;

public class NicksPublicClass
{
    public void SayHello()
    {
        Console.WriteLine("Hello from the class library!");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/multi-project-solutions-54135575/?t=250)

To consume code from the library, a project reference must be established.
This is done via the "Add Project Reference" dialog in the dependencies section of the consuming project.
This action modifies the project's `.csproj` file, adding a `<ProjectReference>` item that points to the library's project file.

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="EntityFramework" Version="6.4.4" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\MultiProject.ClassLibrary\MultiProject.ClassLibrary.csproj" />
  </ItemGroup>

</Project>
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/multi-project-solutions-54135575/?t=280)

Once the reference is added, the consuming project can instantiate classes and call methods defined in the library.
This allows for clean separation of concerns, where the entry point handles execution flow while the library contains the core logic.

```csharp
// See https://aka.ms/new-console-template for more information
using MultiProject.ClassLibrary;

Console.WriteLine("Hello, World!");

NicksPublicClass nicksPublicClass = new();
nicksPublicClass.SayHello();
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/multi-project-solutions-54135575/?t=325)

The .NET build system automatically manages the dependency graph.
When the top-level application is built, Visual Studio identifies the dependencies and builds them in the required order.
Finally, the binaries (DLLs) for all referenced projects are copied into the entry point's `bin` directory, ensuring all necessary components are available at runtime without extra configuration.

---

## 2. Internal access modifier

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/internal-access-modifier-54135576/) · 5:34

### Summary

The internal access modifier in C# restricts the visibility of classes and members to the assembly in which they are defined.
This is essential for encapsulating helper functions and implementation details within a class library while exposing only the necessary public API to external consumers.
While internal members are hidden from other projects by default, the InternalsVisibleTo attribute can be used within a project file to grant specific assemblies, such as unit test projects, access to these internal components without making them fully public.

### Key concepts

*   **Internal Access Modifier**: Restricts access to the current assembly (the project where the code is defined).
*   **Assembly Encapsulation**: Prevents external projects that reference a library from seeing or using implementation details not intended for public use.
*   **Internal Classes**: Classes marked as `internal` cannot be instantiated or referenced outside their home assembly.
*   **Internal Members**: Public classes can contain `internal` methods or properties that remain hidden from external consumers.
*   **InternalsVisibleTo**: An assembly-level attribute that allows a project to "friend" another specific assembly, granting it access to its internal members.

### Lesson notes

#### Defining Internal Components

When building a class library, you often need to create helper classes or methods that facilitate internal logic but should not be part of the public API.
The `internal` keyword is used to restrict access to these components so that they are only visible within the same assembly.

For example, a class can be marked as `internal`, and it can contain both public and internal methods.
However, because the class itself is internal, neither method will be accessible to an external project.

```csharp
namespace MultiProject.ClassLibrary;

internal class NicksInternalClass
{
    public void PublicMethod()
    { 
        Console.WriteLine("Hello from the public method on the internal class!");
    }

    internal void InternalMethod()
    { 
        Console.WriteLine("Hello from the internal method on the internal class!");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/internal-access-modifier-54135576/?t=55)

Alternatively, you may have a `public` class that contains `internal` members.
In this scenario, the class is visible to external projects, but the specific internal methods are hidden.

```csharp
namespace MultiProject.ClassLibrary;

public class NicksPublicClassWithInternalMembers
{
    public void PublicMethod()
    { 
        Console.WriteLine("Hello from the public method on the public class!");
    }

    internal void InternalMethod()
    { 
        Console.WriteLine("Hello from the internal method on the public class!");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/internal-access-modifier-54135576/?t=85)

#### External Visibility and Constraints

When a separate project (such as a Console Application) references the class library, IntelliSense and the compiler will only permit access to members marked as `public` within `public` classes.
If you attempt to use a class marked as `internal`, it will not appear in IntelliSense.

```csharp
// See https://aka.ms/new-console-template for more information
using MultiProject.ClassLibrary;

Console.WriteLine("Hello, World!");

NicksPublicClass nicksPublicClass = new();
nicksPublicClass.SayHello();

NicksPublicClassWithInternalMembers nicksPublicClassWithInternalMembers = new();
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/internal-access-modifier-54135576/?t=100)

Even when the class itself is public, only its public members are accessible to the external assembly.
The internal methods remain restricted to the library project.

```csharp
// See https://aka.ms/new-console-template for more information
using MultiProject.ClassLibrary;

Console.WriteLine("Hello, World!");

NicksPublicClass nicksPublicClass = new();
nicksPublicClass.SayHello();

NicksPublicClassWithInternalMembers nicksPublicClassWithInternalMembers = new();
nicksPublicClassWithInternalMembers.PublicMethod();
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/internal-access-modifier-54135576/?t=145)

#### Exposing Internals via InternalsVisibleTo

There are specific scenarios, most notably unit testing, where an external assembly needs access to internal members to verify logic.
Rather than making these members public for everyone, you can use the `InternalsVisibleTo` attribute.

This is configured in the project file (`.csproj`) of the assembly that owns the internal members.
By adding an `AssemblyAttribute`, you can specify exactly which external assembly is allowed to see the internal components.

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <AssemblyAttribute Include="System.Runtime.CompilerServices.InternalsVisibleToAttribute">
      <_Parameter1>MultiProject.Console</_Parameter1>
    </AssemblyAttribute>
  </ItemGroup>

</Project>
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/internal-access-modifier-54135576/?t=250)

Once this attribute is applied, the specified assembly (in this case, `MultiProject.Console`) can access both internal classes and internal methods as if they were public.

```csharp
// See https://aka.ms/new-console-template for more information
using MultiProject.ClassLibrary;

Console.WriteLine("Hello, World!");

NicksPublicClass nicksPublicClass = new();
nicksPublicClass.SayHello();

NicksPublicClassWithInternalMembers nicksPublicClassWithInternalMembers = new();
nicksPublicClassWithInternalMembers.InternalMethod();

NicksInternalClass nicksInternalClass = new();
nicksInternalClass.InternalMethod();
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/internal-access-modifier-54135576/?t=265)

It is important to note that the project owning the internal members must explicitly grant this access.
An external project cannot unilaterally decide to access another assembly's internal members.

---

## 3. NuGet Packages

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/nuget-packages-54135577/) · 5:14

### Summary

NuGet packages are the standard way to share and consume pre-compiled C# libraries, allowing developers to leverage existing solutions for common tasks like JSON parsing, logging, and database access.
These dependencies are managed through the NuGet Package Manager in Visual Studio or directly within the project file using PackageReference elements.
Unlike project references, which involve source code dependencies that must be compiled during the build process, NuGet packages provide pre-compiled assemblies, streamlining the development workflow and reducing build overhead.

### Key concepts

*   **NuGet Package Manager**: The integrated tool in Visual Studio used to browse, install, and update external libraries.
*   **PackageReference**: An entry in the `.csproj` file that specifies the package name and version required by the project.
*   **ORM (Object Relational Mapper)**: A library, such as Entity Framework, that allows developers to interact with databases using C# objects instead of raw SQL queries.
*   **Pre-compiled Assemblies**: NuGet packages contain code that has already been built into DLLs, meaning the consuming project does not need to recompile the library's source code.

### Lesson notes

#### Managing Dependencies

To integrate third-party code into a C# application, developers use NuGet packages.
These are managed via the "Manage NuGet Packages" menu in Visual Studio.
This interface provides a "Browse" tab to search for libraries—such as `Newtonsoft.Json` for JSON serialization or `Serilog` for logging—and an "Installed" tab to manage existing dependencies.

When a package is installed, Visual Studio updates the project file (`.csproj`) by adding an `ItemGroup` containing a `PackageReference`.
This entry explicitly defines the package name and the specific version to be used.

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="EntityFramework" Version="6.4.4" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\MultiProject.ClassLibrary\MultiProject.ClassLibrary.csproj" />
  </ItemGroup>

</Project>
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/nuget-packages-54135577/?t=55)

#### Using Package Contents

Once a package like Entity Framework is installed, its functionality is accessed through specific namespaces.
Entity Framework is an Object Relational Mapper (ORM) that abstracts database interactions (like SQLite or MySQL) into an object-oriented API.
To use the `DbContext` and `DbSet` classes provided by the package, the appropriate namespace must be imported.

In the following example, the `System.Data.Entity` namespace is required to allow the `AppContext` class to inherit from `DbContext` and manage `Product` entities.

```csharp
// See https://aka.ms/new-console-template for more information
using MultiProject.ClassLibrary;

using System.Data.Entity;

Console.WriteLine("Hello, World!");

NicksPublicClass nicksPublicClass = new();
nicksPublicClass.SayHello();

NicksPublicClassWithInternalMembers nicksPublicClassWithInternalMembers = new();
nicksPublicClassWithInternalMembers.InternalMethod();

NicksInternalClass nicksInternalClass = new();
nicksInternalClass.InternalMethod();

public class AppContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();
}

public record Product(
    int Id,
    string Name,
    string Description,
    decimal Price);
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/nuget-packages-54135577/?t=205)

If the `PackageReference` is removed or commented out in the project file, the associated namespaces and classes become unavailable, resulting in compilation errors.

#### NuGet Packages vs. Project References

There is a fundamental distinction between referencing another project in your solution and referencing a NuGet package:

1.  **Project References**: These depend on source code. When the solution is built, the referenced project must also be compiled. This is ideal for code you are actively developing alongside your application.
2.  **NuGet Packages**: These are pre-compiled assemblies. When you build your project, you are leveraging code that has already been built and published by someone else. This avoids the need to rebuild the dependency's source code every time you compile your own project.
