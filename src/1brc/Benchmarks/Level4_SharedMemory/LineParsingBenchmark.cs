using BenchmarkDotNet.Attributes;
using System.Globalization;

namespace Benchmarks.Level4_SharedMemory;

/// <summary>
/// Benchmark: separator search and temperature parsing, scalar vs SIMD
///
/// Tests: string.IndexOf, Span.IndexOf and a manual pointer scan, plus the two parse paths
/// Purpose: reproduce the table "Let's Start Coding" opens with, where the SIMD-backed
///          Span.IndexOf overtakes the hand-written pointer loop as the line gets longer
/// </summary>
[SimpleJob(warmupCount: 1, iterationCount: 3)]
[BenchmarkCategory("Level04", "Parsing")]
public class LineParsingBenchmark
{
    [Params("A;0.0", "Ouagadougou;32.4", "Petropavlovsk-Kamchatsky;-12.7")]
    public string Line { get; set; } = string.Empty;

    [Benchmark(Baseline = true)]
    public int StringIndexOf() => Line.IndexOf(';', StringComparison.Ordinal);

    [Benchmark]
    public int SpanIndexOf() => Line.AsSpan().IndexOf(';');

    [Benchmark]
    public unsafe int PointerScan()
    {
        fixed (char* ptr = Line)
        {
            for (var i = 0; i < Line.Length; i++)
            {
                if (ptr[i] == ';')
                    return i;
            }
        }

        return -1;
    }

    [Benchmark]
    public double SpanParse()
    {
        var span = Line.AsSpan();
        var separator = span.IndexOf(';');
        return double.Parse(span[(separator + 1)..], CultureInfo.InvariantCulture);
    }

    [Benchmark]
    public unsafe double PointerParse()
    {
        fixed (char* ptr = Line)
        {
            var separator = -1;
            for (var i = 0; i < Line.Length; i++)
            {
                if (ptr[i] == ';')
                {
                    separator = i;
                    break;
                }
            }

            return double.Parse(Line.AsSpan(separator + 1), CultureInfo.InvariantCulture);
        }
    }
}
