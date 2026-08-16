# Demo project conventions

Everything new lives under `src/`. The `course-*` folders at the repo root are legacy:
they pin their own target frameworks and package versions, and `src/.editorconfig` sets
`root = true` specifically so they stay unaffected. Do not add to them.

## Layout

```
src/<course-slug>/
├── notes/
│   └── <NN>-<chapter-slug>.md
└── <NN>-<chapter-slug>/
    ├── <Course>.<Chapter>.Demos/
    └── <Course>.<Chapter>.Benchmarks/
```

`<course-slug>` is Dometrain's own slug, for example `mastering-csharp`. `<NN>` is the
chapter's position in the course, zero-padded. Keeping notes and code under the same
course folder means a chapter is one self-contained unit.

Project names are PascalCase dotted paths, for example
`MasteringCSharp.ValueVsReference.Demos`. Split demos from benchmarks: benchmarks need
Release and take minutes, demos should stay instant to run, and merging them makes both
worse.

## What the build already gives you

`src/Directory.Build.props` applies to everything under `src/`:

- `net10.0`, `latest` lang version, nullable and implicit usings on
- `AnalysisLevel` is `latest-recommended` with `EnforceCodeStyleInBuild`
- XML docs generated, `CS1591` suppressed
- `UseArtifactsOutput`, so binaries land in `src/artifacts/` instead of per-project `bin/`

Do not restate any of this in a csproj. A csproj that only sets `OutputType` and lists
packages is the goal:

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <!-- TargetFramework, Nullable and ImplicitUsings come from src/Directory.Build.props. -->
  <PropertyGroup>
    <OutputType>Exe</OutputType>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="SomePackage" />
  </ItemGroup>

</Project>
```

**Clean up after `dotnet new`.** The console template writes `TargetFramework`,
`ImplicitUsings`, and `Nullable` into every new csproj, unaware that the props file
already supplies them. Delete those three lines after scaffolding.

The values are identical today, so nothing breaks immediately, and that is exactly what
makes the duplication worth removing: a project-level property wins over
`Directory.Build.props`, so the day the repo moves to a newer target framework these
projects would silently stay behind while everything else moved. Centralized settings
only stay centralized if nobody shadows them.

## Packages

Central Package Management is on. Add packages with the CLI and let it maintain both
halves:

```bash
dotnet add <Project> package <Name>
```

That writes a versionless `PackageReference` into the csproj and a `PackageVersion` into
`src/Directory.Packages.props`. Hand-editing either half tends to desynchronize them.

Verify a package actually works on current .NET before building a demo around it.
Course recordings are often a few years old, and packages that inspect runtime internals
are the most likely to have rotted. Write the smallest program that calls the package,
run it, and only then design the demo.

## Analyzers

The build treats code style as part of the build, so a demo should compile with zero
warnings.

Analyzer warnings are sometimes the lesson. When a chapter teaches a trap and an
analyzer flags exactly that trap, suppress it narrowly with `#pragma warning
disable`/`restore` around the single line, plus a comment saying the warning is the
point. That turns the diagnostic into part of the teaching material. Reach for a
project-wide `NoWarn` only when the warning is genuinely noise.

## Demo program shape

Give a demo project a dispatcher so sections can be run alone:

```bash
dotnet run -c Release -- copy
```

Sections are static classes, one per lesson, named after the lesson they demonstrate so
the demo and the notes can be read side by side. Print explanatory lines, not bare
values: output like

```
struct: v1 = (3,4)  v2 = (4,5)  <- v1 never moved
```

is readable months later without the source open.

Benchmark projects use `BenchmarkSwitcher.FromAssembly(...).Run(args)` so a single class
can be selected with `--filter`.

## Finishing

Add every new project to the solution:

```bash
dotnet sln DotNetLearning.slnx add <path to csproj>
```

`slnx` mirrors the disk layout into solution folders automatically.

Then build and run:

```bash
dotnet build -c Release          # expect zero warnings
dotnet run -c Release            # every entry point, not just one
```

Paste the real output into the notes' `Running the demo` section, which is the only part
of the file that is not course content. Output you did not actually see is the one thing
in this workflow that is worse than no output at all.
