# Mastering the Modern C# Stack

> Course: [Mastering: C#](https://dometrain.com/course/mastering-csharp/) · Chapter 2
> 15 lessons · ~23 minutes
> Source: Dometrain. Every section links back to the lesson it came from.
> No companion project: this chapter is about build configuration rather than runnable code. The results in [Verified on this machine](#verified-on-this-machine) were measured with throwaway projects and are reproducible from the snippets here.

---

## The mental model

Most teams believe one thing that is not true: that the target framework decides which C# you may write.

It does not.
They are **two independent axes**, and almost everything in this chapter follows from separating them.

| | Target Framework (TFM) | C# Language Version |
| --- | --- | --- |
| **Set by** | `<TargetFramework>` / `<TargetFrameworks>` | `<LangVersion>` |
| **Decides** | which BCL types exist, and which runtime executes the app | which syntax the compiler will accept |
| **Supplied by** | the runtime you install | the .NET SDK you install |
| **Recorded in metadata** | **yes** - ILSpy shows it | **no** - it leaves no trace |
| **Default** | none, you must choose | derived from the TFM, but only as a default |

The TFM sets a *default* language version, and a default is all it is.
Set `<LangVersion>` explicitly and a .NET Framework 4.8 application compiles C# 14.

The reason this works is that the language is mostly a **compile-time** construct.
The course's own breakdown of a modern C# release:

| Category | Share | Needs | Examples |
| --- | ---: | --- | --- |
| Runtime-independent | ~66% | nothing at all | file-local types, checked operators |
| Type-dependent | ~25% | a named type to exist somewhere | `init`, `required`, `System.Index`, `System.Range` |
| Runtime-dependent | ~10% | actual CLR changes | `ref` fields, static abstract interface members |

That middle band is the interesting one.
The compiler resolves those features by **name and namespace only** - it never asks which assembly the type came from.
So you can declare the missing type yourself, in your own project, and the feature switches on.
That single fact is what the middle of this chapter is about.

**Bottom line:** roughly 90% of modern C# works on .NET Framework 4.8, and the 25% that needs help needs three small files or one NuGet package.

---

## Lesson index

| # | Lesson | Length | Covered in |
| --- | --- | --- | --- |
| 1 | [Overview](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958819/) | 1:00 | [The mental model](#the-mental-model) |
| 2 | [Working on legacy projects?](https://dometrain.com/take/course/mastering-csharp-3256129/working-on-legacy-projects-69958820/) | 0:55 | [1.1](#11-why-legacy-is-not-a-reason-to-write-old-c) |
| 3 | [The compilation pipeline](https://dometrain.com/take/course/mastering-csharp-3256129/the-compilation-pipeline-69958821/) | 1:03 | [1.2](#12-what-the-build-actually-produces) |
| 4 | [Multi-targeting](https://dometrain.com/take/course/mastering-csharp-3256129/multi-targeting-69958822/) | 0:50 | [1.3](#13-multi-targeting) |
| 5 | [Adding multi-targeting](https://dometrain.com/take/course/mastering-csharp-3256129/adding-multi-targeting-69958824/) | 1:03 | [1.3](#13-multi-targeting) |
| 6 | [What is target framework moniker (TFM)?](https://dometrain.com/take/course/mastering-csharp-3256129/what-is-target-framework-moniker-tfm-69958823/) | 1:20 | [1.4](#14-what-a-tfm-actually-decides) |
| 7 | [C# Language version vs. Target framework](https://dometrain.com/take/course/mastering-csharp-3256129/csharp-language-version-vs-target-framework-69958825/) | 2:48 | [2.1](#21-the-two-axes-pulled-apart) |
| 8 | [Using init-only and required properties in .NET Framework](https://dometrain.com/take/course/mastering-csharp-3256129/using-init-only-and-required-properties-in-dotnet-framework-69958826/) | 1:40 | [2.2](#22-the-compiler-is-duck-typed) |
| 9 | [Compiler language specific types](https://dometrain.com/take/course/mastering-csharp-3256129/compiler-language-specific-types-69958827/) | 1:32 | [2.3](#23-which-features-need-which-types) |
| 10 | [PolySharp: source-only polyfills for C# Language](https://dometrain.com/take/course/mastering-csharp-3256129/polysharp-source-only-polyfills-for-csharp-language-69958828/) | 0:54 | [2.4](#24-polysharp-doing-it-automatically) |
| 11 | [Using PolySharp for init-only and required properties](https://dometrain.com/take/course/mastering-csharp-3256129/using-polysharp-for-init-only-and-required-properties-69958829/) | 0:53 | [2.4](#24-polysharp-doing-it-automatically) |
| 12 | [Choosing the right C# Language version](https://dometrain.com/take/course/mastering-csharp-3256129/choosing-the-right-csharp-language-version-69958830/) | 0:43 | [3.1](#31-the-four-values-of-langversion) |
| 13 | ['latest' vs. specific C# Language version](https://dometrain.com/take/course/mastering-csharp-3256129/latest-vs-specific-csharp-language-version-69958831/) | 3:27 | [3.2](#32-what-latest-costs-you) |
| 14 | [Using Directory.Build.props](https://dometrain.com/take/course/mastering-csharp-3256129/using-directory-build-props-69958832/) | 3:30 | [Part 4](#part-4--centralizing-the-decision) |
| 15 | [Conclusion](https://dometrain.com/take/course/mastering-csharp-3256129/conclusion-69958833/) | 1:17 | [The misconception](#the-misconception-this-chapter-exists-to-kill) |

Every lesson in this chapter has a document; nothing was skipped and there are no gaps to watch separately.

---

## Part 1 · The build, and what a TFM decides

### 1.1 Why "legacy" is not a reason to write old C#

> [Working on legacy projects?](https://dometrain.com/take/course/mastering-csharp-3256129/working-on-legacy-projects-69958820/)

Roughly **30% of .NET applications still target .NET Framework**, so this is not a museum topic.

Two claims that are easy to run together but should be kept apart:

- Moving to modern .NET buys **3x to 5x** raw performance. That is a *runtime* benefit and it requires migration.
- Moving to modern C# buys expressiveness and safety. That is a *compiler* benefit and it requires one line of XML.

You can take the second without the first, today, on a codebase nobody will let you migrate.

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>

    <TargetFramework>net9.0</TargetFramework>
    <LangVersion>latest</LangVersion>
  </PropertyGroup>

</Project>
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/working-on-legacy-projects-69958820/?t=51)

The course is deliberately **target-framework agnostic** for this reason.
The exception it flags for later: LINQ performance genuinely differs between .NET Framework and modern .NET, and no language setting closes that gap.

### 1.2 What the build actually produces

> [The compilation pipeline](https://dometrain.com/take/course/mastering-csharp-3256129/the-compilation-pipeline-69958821/)

Source plus a declarative SDK-style project file goes into `csc` (Roslyn, shipped with the SDK), and IL comes out.

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
  </PropertyGroup>
</Project>
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/the-compilation-pipeline-69958821/?t=10)

The detail worth keeping: on modern .NET, `<OutputType>Exe</OutputType>` does **not** mean your code lands in the `.exe`.

- **Modern .NET**: the code is in `YourApp.dll`. The `.exe` beside it is a thin platform-specific launcher shim.
- **.NET Framework**: the `.exe` genuinely *is* the assembly.

To check which of the two you are looking at without opening ILSpy, `Assembly.GetExecutingAssembly().Location` reports the real file extension at runtime.

### 1.3 Multi-targeting

> [Multi-targeting](https://dometrain.com/take/course/mastering-csharp-3256129/multi-targeting-69958822/) · [Adding multi-targeting](https://dometrain.com/take/course/mastering-csharp-3256129/adding-multi-targeting-69958824/)

Change the singular element to the plural one and give it a semicolon-separated list.
The compiler then runs **once per TFM** and writes each result to its own subfolder.

```csharp
using System;

namespace SimpleExecutable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Hello, DomeTrain! TFM: {AppContext.TargetFrameworkName}");
        }
    }
}
```

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFrameworks>net48;net10.0</TargetFrameworks>
  </PropertyGroup>
</Project>
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/multi-targeting-69958822/?t=10)

```text
1>------ Build started: Project: SimpleExecutable, Configuration: Debug Any CPU ------
1> SimpleExecutable -> ...\bin\Debug\net10.0\SimpleExecutable.dll
1> SimpleExecutable -> ...\bin\Debug\net48\SimpleExecutable.exe
========== Build: 1 succeeded, 0 failed, 0 up-to-date, 0 skipped ==========
```

Note the two output *kinds* in that log - a `.dll` for net10.0 and an `.exe` for net48 - which is [1.2](#12-what-the-build-actually-produces) showing up in the build output.

Visual Studio gets a TFM drop-down in the toolbar for choosing which one to debug.
From the CLI the same choice is `-f`:

```bash
dotnet run -f net48
dotnet run -f net10.0
```

> **Aside, learned the hard way.**
> `TargetFramework` (singular) **always wins** over `TargetFrameworks` (plural) when both are set.
> Because this repo's `src/Directory.Build.props` pins `net10.0`, a project that merely adds `TargetFrameworks` silently builds one TFM and looks like multi-targeting is broken.
> The fix is to clear the inherited value first: `<TargetFramework></TargetFramework>`.

> **Aside: you do not need Visual Studio to build `net48`.**
> The .NET Framework targeting packs normally arrive with VS, and this machine has none.
> The [`Microsoft.NETFramework.ReferenceAssemblies`](https://www.nuget.org/packages/Microsoft.NETFramework.ReferenceAssemblies) NuGet package supplies the reference assemblies instead, so `net48` builds from the SDK alone.
> Running the result still needs the .NET Framework runtime, which Windows already has.

### 1.4 What a TFM actually decides

> [What is target framework moniker (TFM)?](https://dometrain.com/take/course/mastering-csharp-3256129/what-is-target-framework-moniker-tfm-69958823/)

A TFM such as `net48`, `net10.0` or `netstandard2.0` does three jobs:

1. **Compilation target** - the platform your code is compiled against.
2. **Type availability** - which BCL types exist. This is the one that bites.
3. **Runtime selection** - which runtime executes the app.

`netstandard` is the exception to the third: it defines an **API surface**, not a runtime.
Targeting `netstandard2.0` means the compiler guarantees every type you touch exists on every runtime that implements that standard, which is how one library serves both .NET Framework and .NET Core.

The asymmetry to remember, and the hinge for the rest of the chapter:

> The **TFM is written into assembly metadata** and ILSpy will show it.
> The **C# language version is not recorded anywhere**.

Which is exactly why the language version can be whatever you like: nothing downstream can tell.

---

## Part 2 · Decoupling the language from the runtime

### 2.1 The two axes pulled apart

> [C# Language version vs. Target framework](https://dometrain.com/take/course/mastering-csharp-3256129/csharp-language-version-vs-target-framework-69958825/)

Left alone, the compiler picks a language version from the TFM:

| Target framework | Default C# |
| --- | --- |
| .NET Framework / .NET Standard 2.0 | 7.3 |
| .NET 5 | 9 |
| .NET 6 | 10 |
| .NET 7 | 11 |
| .NET 8 | 12 |

Override it and the pairing dissolves:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <LangVersion>latest</LangVersion>

    <TargetFramework>net48</TargetFramework>
  </PropertyGroup>
</Project>
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/csharp-language-version-vs-target-framework-69958825/?t=10)

Microsoft's documentation states the strict pairings, and the lesson's rebuttal is a good one: **Microsoft's own tooling does not follow them.**
NuGet, MSBuild and Roslyn all use modern language features while still targeting .NET Framework, and the pattern has been shown publicly by Scott Hanselman and Stephen Toub.

The documented pairing is a *support statement*, not a technical limit.

### 2.2 The compiler is duck-typed

> [Using init-only and required properties in .NET Framework](https://dometrain.com/take/course/mastering-csharp-3256129/using-init-only-and-required-properties-in-dotnet-framework-69958826/)

This is the pivotal lesson of the chapter.

Start with the target code on `net48`:

```csharp
using System;

namespace UsingInitOnly
{
    public class Point
    {
        public required int X { get; init; }
        public required int Y { get; init; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var p = new Point{X = 1, Y = 2};
            Console.WriteLine($"X: {p.X}, Y: {p.Y}");
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/using-init-only-and-required-properties-in-dotnet-framework-69958826/?t=10)

Two failures happen in order, and it matters that they are different failures.

1. **Wrong language version.** `net48` defaults to C# 7.3, so `init` and `required` are not even syntax yet. Fixed by `<LangVersion>` (9.0+ for `init`, 11.0+ for `required`, or just `latest`).
2. **Missing types.** Now the syntax parses, but the compiler wants types that .NET Framework 4.8 has never heard of.

On this machine, failure 2 reads exactly like this:

```text
error CS0518: Predefined type 'System.Runtime.CompilerServices.IsExternalInit' is not defined or imported
error CS0656: Missing compiler required member 'System.Runtime.CompilerServices.RequiredMemberAttribute..ctor'
error CS0656: Missing compiler required member 'System.Runtime.CompilerServices.CompilerFeatureRequiredAttribute..ctor'
```

And here is the whole trick: **the compiler does not care where those types come from.**
It looks them up by name and namespace. Declare them yourself and the feature turns on.

```csharp
namespace System.Runtime.CompilerServices
{
   internal class IsExternalInit { }

   [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
   internal sealed class RequiredMemberAttribute : Attribute { }

   [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
   internal sealed class CompilerFeatureRequiredAttribute : Attribute
   {
       public CompilerFeatureRequiredAttribute(string featureName)
       {
           FeatureName = featureName;
       }

       public string FeatureName { get; }
       public bool IsOptional { get; init; }
   }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/using-init-only-and-required-properties-in-dotnet-framework-69958826/?t=55)

Three small types, and a 2019 runtime compiles C# 14.

Two things worth noticing in that snippet:

- `IsExternalInit` is a **pure marker**. An empty body is the entire implementation; only its existence is checked.
- `CompilerFeatureRequiredAttribute` uses `init` **itself**, which only compiles because `IsExternalInit` is declared two lines above. The polyfill bootstraps itself.

`CompilerFeatureRequiredAttribute` is the one people delete as noise, and it is the one that matters most for correctness.
It is what makes an *older* compiler refuse the assembly rather than quietly ignore `required` and let a caller skip initialization.

> **Aside: Roslyn was already doing this behind your back.**
> Enumerating every `System.Runtime.CompilerServices` type in the finished net48 assembly turns up **six**, not three.
> `NullableAttribute`, `NullableContextAttribute` and `RefSafetyRulesAttribute` were embedded by the compiler itself, unprompted, because net48 has no versions of them either.
> Polyfilling is not a PolySharp invention or even a community workaround - it is a mechanism Roslyn already relies on. PolySharp just aims it at features *you* chose.

### 2.3 Which features need which types

> [Compiler language specific types](https://dometrain.com/take/course/mastering-csharp-3256129/compiler-language-specific-types-69958827/)

The type-dependent band, by version:

| C# | Feature | Needs |
| --- | --- | --- |
| 12 | collection expression custom builders | `CollectionBuilderAttribute` |
| 11 | `required` members, list patterns | `RequiredMemberAttribute`, `System.Index`, `System.Range` |
| 10 | async method builders, interpolated string handlers, caller argument expressions | various attributes |
| 9 | `SkipLocalsInit`, module initializers | `SkipLocalsInitAttribute`, `ModuleInitializerAttribute` |
| 8 | nullable reference types, ranges, async streams | nullable attributes, `System.Index`/`System.Range` |

Some are purely for tooling rather than semantics:

- `StringSyntaxAttribute` drives syntax highlighting inside string literals.
- `StackTraceHiddenAttribute` hides helper frames from stack traces.

**C# 13 and C# 14 added nothing to this list.**
The mechanism has stopped growing, which makes a polyfill set a fairly stable thing to depend on.

The lesson closes on the real objection to doing this by hand: it is fine for one project and one TFM, and it becomes a maintenance problem across several, since each TFM needs a different subset and duplicate definitions collide.

### 2.4 PolySharp: doing it automatically

> [PolySharp: source-only polyfills](https://dometrain.com/take/course/mastering-csharp-3256129/polysharp-source-only-polyfills-for-csharp-language-69958828/) · [Using PolySharp for init-only and required properties](https://dometrain.com/take/course/mastering-csharp-3256129/using-polysharp-for-init-only-and-required-properties-69958829/)

PolySharp is a **source generator**: it injects the support types at compile time, as `internal`, and ships nothing at runtime.

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net48</TargetFramework>
    <LangVersion>latest</LangVersion>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="PolySharp" Version="1.15.0">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
  </ItemGroup>

</Project>
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/polysharp-source-only-polyfills-for-csharp-language-69958828/?t=0)

Two attributes in there carry all the weight:

- **`PrivateAssets=all`** makes it compile-only. It does not flow to consumers of your library and no `PolySharp.dll` is deployed.
- **`internal` generated types** are what stop two PolySharp-using libraries in the same application from colliding on duplicate type names.

The measured contrast with doing it by hand is larger than expected: **3 types written manually, 34 generated by PolySharp** - the full set for the TFM, including `System.Diagnostics.CodeAnalysis` nullability attributes nobody would think to write.

> **Aside:** this repo resolves PolySharp **1.16.0**, not the lesson's 1.15.0, and it needed no configuration change.
> Central Package Management also means the version lives in `src/Directory.Packages.props` and the `PackageReference` carries no `Version` attribute.

---

## Part 3 · Choosing a language version

### 3.1 The four values of `LangVersion`

> [Choosing the right C# Language version](https://dometrain.com/take/course/mastering-csharp-3256129/choosing-the-right-csharp-language-version-69958830/)

| Value | Meaning | Use when |
| --- | --- | --- |
| `preview` | compiler-experimental, unfinalized features | trying out what is coming |
| `latest` | the highest version **the installed SDK** supports | you pin the SDK too |
| `latestMajor` | the latest major version | largely historical, from the C# 7.1/7.2/7.3 era |
| `14`, `13`, ... | exactly this version | you want the build frozen |

The catch with `latest` is that it is **not a version at all** - it is a query against the build machine.
The same source can compile as C# 13 on a CI agent and C# 14 on a laptop.

The mitigation is `global.json`, which pins the SDK and therefore pins what `latest` resolves to:

```json
{
  "sdk": {
    "version": "10.0.106"
  }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/choosing-the-right-csharp-language-version-69958830/?t=36)

Official documentation recommends against `latest` in production, and build reliability is the entire reason.

### 3.2 What `latest` costs you

> ['latest' vs. specific C# Language version](https://dometrain.com/take/course/mastering-csharp-3256129/latest-vs-specific-csharp-language-version-69958831/)

The lesson makes the risk concrete with code that is valid C# 13 and broken C# 14.

```csharp
using System.Linq;
using System;

namespace LatestCSharpIssues
{
    public sealed class Person
    {
        private string _name = "";

        public string DisplayName
        {
            get
            {
                // In C# 13, 'field' is a valid identifier for a local variable.
                string field = _name.Trim();
                return field.Length == 0 ? "John Doe" : field;
            }
        }

        public static string LastPart(string[] parts) =>
            // In C# 13, this resolves to Enumerable.Reverse.
            parts.Reverse().First();
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/latest-vs-specific-csharp-language-version-69958831/?t=25)

Bump the SDK and two independent things break:

1. **`field` became a contextual keyword.** In C# 14 it binds to the compiler-synthesized backing field inside a property accessor, so a local named `field` collides with it.
2. **Overload resolution moved.** C# 14 added first-class span conversions, which makes `MemoryExtensions.Reverse<T>(Span<T>)` applicable to a `string[]` receiver. It mutates in place and returns `void`, so `.First()` has nothing to chain onto.

Both reproduce here, but **not in the way the lesson implies** - see [Verified on this machine](#verified-on-this-machine).

The trade-off, stated fairly:

| | `latest` | pinned version |
| --- | --- | --- |
| **Upside** | no version drift between projects; upgrade the whole repo by bumping `global.json` | builds are reproducible; upgrades happen when you decide |
| **Downside** | an SDK bump can break compiling code | every new release means editing every csproj |

Both columns end in the same place, which is Part 4: whichever you pick, make the decision **once**.

---

## Part 4 · Centralizing the decision

> [Using Directory.Build.props](https://dometrain.com/take/course/mastering-csharp-3256129/using-directory-build-props-69958832/)

The lesson opens by contrasting a legacy csproj - GUIDs, explicit imports, per-configuration `PropertyGroup`s, and a dependency graph MSBuild cannot see through - with the SDK-style equivalent.

But SDK-style projects have their own failure mode at scale: three projects repeating the same four properties and the same package reference.

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net48</TargetFramework>
    <LangVersion>latest</LangVersion>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="PolySharp" Version="1.15.0">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
  </ItemGroup>
</Project>
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/using-directory-build-props-69958832/?t=70)

Lift the shared part into `Directory.Build.props`, which MSBuild imports automatically for every project beneath it:

```xml
<Project>
  <PropertyGroup>
    <TargetFramework>net48</TargetFramework>
    <LangVersion>latest</LangVersion>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="PolySharp" Version="1.15.0">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
  </ItemGroup>
</Project>
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/using-directory-build-props-69958832/?t=130)

And the projects shrink to what is genuinely theirs:

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\ProjectA\ProjectA.csproj" />
    <ProjectReference Include="..\ProjectB\ProjectB.csproj" />
  </ItemGroup>

</Project>
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/using-directory-build-props-69958832/?t=120)

The system is hierarchical, like `.editorconfig`: a file in a nested folder overrides one further up, closest wins.

> **Aside: "like `.editorconfig`" is where this gets dangerous, because it is only half true.**
> `.editorconfig` files **merge** up the tree unless one says `root = true`.
> `Directory.Build.props` files do **not**. MSBuild stops at the **first** one it finds and never looks higher.
>
> So dropping a nested props file into a subfolder of this repo **shadows** `src/Directory.Build.props` outright - silently losing `net10.0`, nullable, implicit usings and every analyzer setting.
> Chaining is manual and this is the incantation:
>
> ```xml
> <Import Project="$([MSBuild]::GetPathOfFileAbove('Directory.Build.props', '$(MSBuildThisFileDirectory)../'))" />
> ```
>
> There is a second-order surprise too: `UseArtifactsOutput` resolves output relative to the folder holding `Directory.Build.props`, so adding a nested file also relocates that subtree's binaries out of `src/artifacts/`.
> Pin `ArtifactsPath` back explicitly if you care where they land.

---

## Verified on this machine

Windows 11 x64, .NET SDK 10.0.400 (C# 14), runtimes 3.1 / 8.0 / 10.0 installed, no .NET Framework targeting pack.

Everything below was actually built and run before being written down.
No project is kept for it, because each result needs only a two-line csproj and a handful of statements; the recipes are given so any of it can be redone in a scratch folder.

One prerequisite for every `net48` experiment here, since this machine has no targeting pack:

```xml
<PackageReference Include="Microsoft.NETFramework.ReferenceAssemblies" Version="1.0.3" PrivateAssets="all" />
```

### The two axes, side by side

Compiling one source file against `<TargetFrameworks>net48;net10.0</TargetFrameworks>` and having it report on itself:

| | `net48` | `net10.0` |
| --- | --- | --- |
| `AppContext.TargetFrameworkName` | `.NETFramework,Version=v4.8` | `.NETCoreApp,Version=v10.0` |
| `FrameworkDescription` | .NET Framework 4.8.9337.0 | .NET 10.0.11 |
| Assembly on disk | `.exe` (the assembly itself) | `.dll` (plus a launcher shim) |
| Symbols defined | `NETFRAMEWORK`, `NET48` | `NET10_0`, `NETCOREAPP`, `NET5_0_OR_GREATER` |
| `Enumerable.Reverse` overloads | `IEnumerable<T>` only | `IEnumerable<T>` **and** `T[]` |
| `System.MemoryExtensions` | absent | present |
| Support types Roslyn embedded | `RefSafetyRulesAttribute` | none |

Same source, same compiler, same C# version.
Everything in that table is the TFM axis alone.

### Polyfilling, by hand and by package

A `net48` executable with the `Point` type from [2.2](#22-the-compiler-is-duck-typed), the three hand-written support types, and a loop printing every `System.Runtime.CompilerServices` type it can find in itself:

| Type found in the assembly | Where it came from |
| --- | --- |
| `IsExternalInit` | written by hand |
| `RequiredMemberAttribute` | written by hand |
| `CompilerFeatureRequiredAttribute` | written by hand |
| `NullableAttribute` | **Roslyn, unprompted** |
| `NullableContextAttribute` | **Roslyn, unprompted** |
| `RefSafetyRulesAttribute` | **Roslyn, unprompted** |

Six, not three, which is the evidence behind the aside in [2.2](#22-the-compiler-is-duck-typed).

Reflecting over the compiled `Point` also confirms where `required` ended up: both `X` and `Y` carry `RequiredMemberAttribute`, and so does the type itself.
`required` is enforced entirely at compile time; what survives into the assembly is metadata telling *other* compilers to keep enforcing it.

Swapping the hand-written file for `<PackageReference Include="PolySharp" PrivateAssets="all" />`, same `Point`, same TFM:

- **34** support types generated, spanning `System.Runtime.CompilerServices` **and** `System.Diagnostics.CodeAnalysis`.
- All of them `internal`, which is what stops two PolySharp-using libraries colliding in one application.
- Zero DLLs deployed next to the executable, confirming `PrivateAssets=all` did its job.

3 by hand versus 34 generated is the honest summary of the trade: hand-written is fine for one project on one TFM, and stops scaling immediately after that.

### The breaking changes, and one that did not reproduce

Putting each of the lesson's two samples in its own file, targeting `net8.0;net10.0`, and driving `LangVersion` from the command line:

```bash
dotnet build -c Release -p:LangVersion=13     # the "before" state
dotnet build -c Release -p:LangVersion=14     # the "after" state
dotnet build -c Release -p:LangVersion=14 -f net8.0
dotnet build -c Release -p:LangVersion=14 -f net10.0
```

Isolating them matters: put both samples in one file and the two errors mask each other.

| Break | C# 13 | C# 14 / `net8.0` | C# 14 / `net10.0` |
| --- | --- | --- | --- |
| `field` as a local in an accessor | builds | **CS9273** (+ 2x CS9258) | **CS9273** (+ 2x CS9258) |
| `parts.Reverse().First()` | builds | **CS0023** | **builds** |

The first behaves exactly as the lesson says.
The exact text is worth knowing because it names its own fix:

```text
error CS9273: In language version 14.0, 'field' is a keyword within a property accessor.
              Rename the variable or use the identifier '@field' instead.
warning CS9258: In language version 14.0, the 'field' keyword binds to a synthesized backing field
                for the property. ... use 'this.field' or '@field' instead.
```

> **The second one did not reproduce, and the reason is the more interesting result of this chapter.**
>
> On `net10.0` the lesson's `Reverse()` code compiles and runs perfectly under C# 14.
> Not because the span conversion went away, but because **.NET 10 added an array-specific `Enumerable.Reverse<T>(T[])` overload** that wins the lookup and still returns `IEnumerable<T>`.
> Reflection confirms it: `net48` and `net8.0` see one `Reverse`, `net10.0` sees two.
>
> Drop to `net8.0` with the same compiler and the same `LangVersion=14`, and the break returns immediately as `CS0023: Operator '.' cannot be applied to operand of type 'void'`.
>
> So this break is **not** a pure language-version effect the way `field` is.
> It needs C# 14 **and** a pre-.NET 10 BCL, and the BCL team shipped a shim to defuse it.
> The lesson's warning is still correct in substance - an SDK upgrade broke compiling code - but on current .NET the second example is a language-plus-library interaction, and the fix arrived in the library.
>
> That is a neater illustration of this chapter's thesis than the original example: the two axes are independent, and here they had to line up *both* ways before anything broke.

The migration that works everywhere, on any version:

```csharp
string @field = _name.Trim();          // forces the identifier reading
Enumerable.Reverse(parts).First();     // states the receiver type explicitly
parts.AsEnumerable().Reverse().First();
```

---

## The misconception this chapter exists to kill

> [Conclusion](https://dometrain.com/take/course/mastering-csharp-3256129/conclusion-69958833/)

**"We are on .NET Framework, so we are stuck on C# 7.3."**

Wrong in three separate ways:

1. The TFM only sets a **default** language version. `<LangVersion>` overrides it, and nothing records the result.
2. About **90%** of modern C# needs no runtime support at all. Two-thirds needs literally nothing; another quarter needs a type to exist, which you can supply yourself.
3. The remaining ~10% - `ref` fields, static abstract interface members - genuinely needs the runtime. That is the real limit, and it is small.

The corollary misconception, aimed the other way: **"`latest` is the safe modern default."**
`latest` is a query against whichever SDK the build machine happens to have.
It is only safe when paired with a pinned `global.json`, and even then an intentional SDK bump can break compiling code.

Both decisions belong in `Directory.Build.props`, made once.

---

## Self-test

1. A colleague says a `net48` project cannot use `required` properties. Which two separate things have to be fixed, and in which order?
2. `IsExternalInit` has an empty body. Why is that sufficient?
3. Why does `CompilerFeatureRequiredAttribute` matter for correctness, given `required` is enforced at compile time anyway?
4. Your nested `Directory.Build.props` compiles, but the project has lost `Nullable` and the analyzer settings from the parent file. What happened, and what is the fix?
5. A project sets `<TargetFrameworks>net48;net10.0</TargetFrameworks>` but only ever produces `net10.0` output. What would you check first?
6. The `field` break fires on every TFM, but the `Reverse()` break only fires below .NET 10. What does that difference tell you about where each change lives?
7. Under what circumstances is `<LangVersion>latest</LangVersion>` a reproducible build setting?

<details>
<summary>Answer key</summary>

1. First the **language version** (`<LangVersion>` to 11.0+, or `latest`) so the syntax parses at all; only then the **missing types**, because the CS0518/CS0656 errors cannot appear until the code gets past parsing. They are two distinct failures and fixing the second without the first does nothing.
2. Because the compiler checks only that a type with that **name in that namespace** exists. `IsExternalInit` is a pure marker - it is never instantiated and never called, so there is nothing to implement.
3. It is what makes an **older compiler refuse** an assembly built with a feature it does not understand. Without it, a C# 10 compiler consuming your library would silently ignore `required` and let callers construct objects with uninitialized members.
4. MSBuild stops at the **first** `Directory.Build.props` it finds walking up and never looks higher, so the nested file **shadowed** the parent rather than adding to it. (`.editorconfig` merges; this does not.) Fix by importing the parent explicitly with `$([MSBuild]::GetPathOfFileAbove(...))`.
5. Whether a `Directory.Build.props` up the tree sets the **singular** `TargetFramework`, which always wins over `TargetFrameworks`. Clear it with `<TargetFramework></TargetFramework>`.
6. `field` is a **language** change - the compiler's parsing rules changed, so it applies wherever C# 14 is used. `Reverse()` is a **language plus library** interaction: C# 14 made a span overload applicable, and whether it wins depends on what else the BCL offers. .NET 10 added `Enumerable.Reverse<T>(T[])`, which takes the call back.
7. Only when the SDK is pinned in `global.json`, because `latest` resolves against the installed SDK rather than naming a version. Even then it is reproducible rather than *stable*: bumping the SDK intentionally still changes the language version everywhere at once.

</details>

---

## Threads into later chapters

| Deferred here | Picked up in |
| --- | --- |
| Why LINQ performs differently on .NET Framework vs. modern .NET | Mastering LINQ Performance |
| Vectorization and spans as the reason for that gap | Mastering LINQ Performance |
| `required` and `init` as tools for immutability and value semantics | Mastering Records |
| Classes, structs, records and tuples as distinct data types | Value Types vs. Reference Types |
| Writing polyfills as extension methods across TFMs | Mastering Extension Everything |
