using BenchmarkDotNet.Attributes;
using System.Buffers.Text;
using System.Globalization;
using System.Text;

namespace Benchmarks.Level4_SharedMemory;

/// <summary>
/// Benchmark: double.Parse via Span, Utf8Parser, and the hand-written byte parser
///
/// Tests: the four temperature shapes the dataset contains
/// Purpose: reproduce "Custom Double Parse and Benchmarks"
/// </summary>
[BenchmarkCategory("Level04", "Parsing")]
public class TemperatureParseBenchmark
{
    [Params("9.1", "32.4", "-9.5", "-12.7")]
    public string RawValue { get; set; } = string.Empty;

    private byte[] _bytes = [];

    [GlobalSetup]
    public void Setup() => _bytes = Encoding.UTF8.GetBytes(RawValue);

    [Benchmark]
    public double SpanParse()
    {
        Span<char> chars = stackalloc char[_bytes.Length];
        var count = Encoding.UTF8.GetChars(_bytes, chars);
        return double.Parse(chars[..count], CultureInfo.InvariantCulture);
    }

    [Benchmark]
    public double Utf8Parse() =>
        Utf8Parser.TryParse(_bytes, out double value, out _) ? value : double.NaN;

    [Benchmark(Baseline = true)]
    public double CustomParse() => ParseTemperature(_bytes);

    private static double ParseTemperature(ReadOnlySpan<byte> span)
    {
        if (span.Length > 0 && span[^1] == '\r')
            span = span[..^1];

        var negative = false;
        var index    = 0;

        if (span[0] == '-')
        {
            negative = true;
            index    = 1;
        }

        double result       = 0;
        var decimalFound    = false;
        var decimalPlace    = 0.1;

        while (index < span.Length)
        {
            var c = span[index++];

            if (c == '.')
            {
                decimalFound = true;
                continue;
            }

            var digit = c - '0';

            if (decimalFound)
            {
                result      += digit * decimalPlace;
                decimalPlace *= 0.1;
            }
            else
            {
                result = result * 10 + digit;
            }
        }

        return negative ? -result : result;
    }
}
