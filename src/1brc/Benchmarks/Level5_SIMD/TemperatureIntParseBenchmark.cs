using BenchmarkDotNet.Attributes;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace Benchmarks.Level5_SIMD;

/// <summary>
/// Benchmark: Level 4's double parser against the two integer parsers of
/// "Int Parser instead of Double.Parse()"
///
/// Tests: a batch of <see cref="TokenCount"/> temperature tokens covering every shape the
///        dataset contains, parsed one per reported operation
/// Purpose: reproduce the DoubleParse / IntParseBranchless / IntParseWithDivision table
///
/// Measured one value at a time the way the Level 4 parse benchmark does, IntParseBranchless
/// comes back at 0.04 ns with BenchmarkDotNet's "indistinguishable from the empty method"
/// warning: with a fixed input and no loop, the JIT folds it away. Parsing a batch of varied
/// tokens per invocation keeps the work real and still reports a per-token cost.
///
/// The lesson shows its table but not the code behind IntParseWithDivision, so that method is
/// this repo's reading of the name: a general loop that accumulates every digit and divides the
/// surplus decimals away, versus the branchless version that indexes the known byte positions.
/// </summary>
[BenchmarkCategory("Level05", "Parsing")]
public class TemperatureIntParseBenchmark
{
    private const int TokenCount = 4096;

    private byte[] _tokens = [];
    private int[] _offsets = [];
    private int[] _lengths = [];

    [GlobalSetup]
    public void Setup()
    {
        var builder = new StringBuilder();
        _offsets = new int[TokenCount];
        _lengths = new int[TokenCount];

        // -35.0 .. 34.9 in the dataset's own format: one decimal digit, optional minus.
        for (var i = 0; i < TokenCount; i++)
        {
            var text = (((i * 7919) % 700) - 350) / 10.0;
            var token = text.ToString("F1", CultureInfo.InvariantCulture);

            _offsets[i] = builder.Length;
            _lengths[i] = token.Length;
            builder.Append(token);
        }

        _tokens = Encoding.UTF8.GetBytes(builder.ToString());
    }

    [Benchmark(Baseline = true, OperationsPerInvoke = TokenCount)]
    public double DoubleParse()
    {
        double total = 0;

        for (var i = 0; i < TokenCount; i++)
            total += ParseTemperatureDouble(_tokens.AsSpan(_offsets[i], _lengths[i]));

        return total;
    }

    [Benchmark(OperationsPerInvoke = TokenCount)]
    public unsafe long IntParseBranchless()
    {
        long total = 0;

        fixed (byte* basePtr = _tokens)
        {
            for (var i = 0; i < TokenCount; i++)
                total += ParseTemperatureBranchless(basePtr + _offsets[i], _lengths[i]);
        }

        return total;
    }

    [Benchmark(OperationsPerInvoke = TokenCount)]
    public unsafe long IntParseWithDivision()
    {
        long total = 0;

        fixed (byte* basePtr = _tokens)
        {
            for (var i = 0; i < TokenCount; i++)
                total += ParseTemperatureWithDivision(basePtr + _offsets[i], _lengths[i]);
        }

        return total;
    }

    /// <summary>
    /// Level 4's parser, from "Coding the Custom Double Parse": a loop over the bytes with a
    /// floating-point multiply per decimal digit.
    /// </summary>
    private static double ParseTemperatureDouble(ReadOnlySpan<byte> span)
    {
        if (span.Length > 0 && span[^1] == '\r')
            span = span[..^1];

        var negative = false;
        var index = 0;

        if (span[0] == '-')
        {
            negative = true;
            index = 1;
        }

        double result = 0;
        var decimalFound = false;
        var decimalPlace = 0.1;

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
                result += digit * decimalPlace;
                decimalPlace *= 0.1;
            }
            else
            {
                result = (result * 10) + digit;
            }
        }

        return negative ? -result : result;
    }

    /// <summary>
    /// Level 5's parser: no loop, integer arithmetic only, result scaled by 10.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static unsafe int ParseTemperatureBranchless(byte* ptr, int len)
    {
        var sign = 1;

        if (ptr[0] == '-')
        {
            sign = -1;
            ptr++;
            len--;
        }

        int value;

        if (len == 3)
        {
            // "D.D" -> D*10 + D (e.g. "9.1" -> 91)
            value = ((ptr[0] - '0') * 10) + (ptr[2] - '0');
        }
        else
        {
            // "DD.D" -> D*100 + D*10 + D (e.g. "32.4" -> 324)
            value = ((ptr[0] - '0') * 100)
                  + ((ptr[1] - '0') * 10)
                  + (ptr[3] - '0');
        }

        return sign * value;
    }

    /// <summary>
    /// Integer arithmetic too, but general-purpose: it does not assume how many digits sit on
    /// either side of the point, so it keeps the loop and scales back with a division.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static unsafe int ParseTemperatureWithDivision(byte* ptr, int len)
    {
        var sign = 1;
        var i = 0;

        if (ptr[0] == '-')
        {
            sign = -1;
            i = 1;
        }

        var value = 0;
        var fractionDigits = 0;
        var pointSeen = false;

        for (; i < len; i++)
        {
            var c = ptr[i];

            if (c == '.')
            {
                pointSeen = true;
                continue;
            }

            value = (value * 10) + (c - '0');

            if (pointSeen)
                fractionDigits++;
        }

        // The result is scaled by 10, so every decimal past the first gets divided away.
        for (var extra = 1; extra < fractionDigits; extra++)
            value /= 10;

        return sign * value;
    }
}
