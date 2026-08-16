using BenchmarkDotNet.Running;

// Benchmarks must run in Release. BenchmarkSwitcher lets you pick one:
//   dotnet run -c Release -f net10.0 -- --filter *BoxingBenchmarks*
//   dotnet run -c Release -f net10.0 -- --list flat
//
// The host has to be launched with -f net10.0 even though the benchmark runs on both
// runtimes: the [ShortRunJob(RuntimeMoniker.Net48)] attribute is what builds and runs
// the .NET Framework 4.8 leg, from a host that itself runs on .NET 10.
BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);

/// <summary>
/// Entry point marker so BenchmarkSwitcher can find the assembly.
/// </summary>
public partial class Program;
