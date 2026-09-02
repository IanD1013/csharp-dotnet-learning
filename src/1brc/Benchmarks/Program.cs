using BenchmarkDotNet.Running;

// Benchmarks must run in Release. One switcher for every level's benchmarks:
//   dotnet run -c Release -- --filter *ThreadLocalVsSharedBenchmark*
//   dotnet run -c Release -- --list flat
BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);

/// <summary>
/// Entry point marker so BenchmarkSwitcher can find the assembly.
/// </summary>
public partial class Program;
