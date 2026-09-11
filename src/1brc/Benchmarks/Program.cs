using BenchmarkDotNet.Running;
using Benchmarks.Level4_SharedMemory;

// "Hash Comparison" runs its analyzer straight from Program.cs rather than through
// BenchmarkDotNet, so it sits behind an argument:
//   dotnet run -c Release -- hash-analysis [measurementCount]
if (args.Length > 0 && args[0].Equals("hash-analysis", StringComparison.OrdinalIgnoreCase))
{
    var measurementCount = args.Length > 1
        ? int.Parse(args[1], System.Globalization.CultureInfo.InvariantCulture)
        : 100_000_000;

    HashDistributionAnalyzer.AnalyzeHashPerformance(measurementCount);
    return;
}

// Benchmarks must run in Release. One switcher for every level's benchmarks:
//   dotnet run -c Release -- --filter *ThreadLocalVsSharedBenchmark*
//   dotnet run -c Release -- --list flat
BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);

/// <summary>
/// Entry point marker so BenchmarkSwitcher can find the assembly.
/// </summary>
public partial class Program;
