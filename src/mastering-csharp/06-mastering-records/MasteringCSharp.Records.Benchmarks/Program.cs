using BenchmarkDotNet.Running;

// Benchmarks must run in Release. BenchmarkSwitcher lets you pick one:
//   dotnet run -c Release -- --filter *LocationContainsBenchmarks*
//   dotnet run -c Release -- --filter *HashSetBuildBenchmarks*
//   dotnet run -c Release -- --list flat
BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);

/// <summary>
/// Entry point marker so BenchmarkSwitcher can find the assembly.
/// </summary>
public partial class Program;
