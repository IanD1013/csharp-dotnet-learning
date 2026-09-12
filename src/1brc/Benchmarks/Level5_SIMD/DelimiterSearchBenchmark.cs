using BenchmarkDotNet.Attributes;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace Benchmarks.Level5_SIMD;

/// <summary>
/// Benchmark: the scalar delimiter scan Level 4 used against the vectorized FindByteFast of
/// "Introduction to SIMD" and "Finalizing the Approach", plus Span.IndexOf for reference
///
/// Tests: walking a 1 MB block of measurement lines, finding every ';' and every newline
/// Purpose: put a number on the claim that "a hand-written pointer loop cannot beat SIMD"
///
/// StationNames picks how far apart the delimiters sit, which is the whole variable: a vector
/// step only pays off once the scalar loop would have taken more steps than the vector costs to
/// set up.
///
/// The chapter states the gap but never benchmarks the delimiter search itself, so this class is
/// this repo's own.
/// </summary>
[BenchmarkCategory("Level05", "SIMD")]
public class DelimiterSearchBenchmark
{
    private const int BufferSize = 1024 * 1024;
    private const byte Semicolon = (byte)';';
    private const byte Newline = (byte)'\n';

    private static readonly string[] ShortNames = ["Abha", "Yaren", "Lhasa", "Perth"];
    private static readonly string[] MixedNames =
        ["Abha", "Hamburg", "Ouagadougou", "Petropavlovsk-Kamchatsky", "Bulawayo", "Yaren"];
    private static readonly string[] LongNames =
        ["Petropavlovsk-Kamchatsky", "Ho Chi Minh City", "Dar es Salaam", "Nizhny Novgorod"];

    /// <summary>Which station-name set fills the buffer: short, mixed, or long.</summary>
    [Params("short", "mixed", "long")]
    public string StationNames { get; set; } = "mixed";

    private byte[] _buffer = [];

    [GlobalSetup]
    public void Setup()
    {
        var names = StationNames switch
        {
            "short" => ShortNames,
            "long" => LongNames,
            _ => MixedNames
        };

        var builder = new StringBuilder(BufferSize + 64);
        var index = 0;

        while (builder.Length < BufferSize)
        {
            var temperature = (((index * 7919) % 700) - 350) / 10.0;

            builder.Append(names[index % names.Length])
                   .Append(';')
                   .Append(temperature.ToString("F1", CultureInfo.InvariantCulture))
                   .Append('\n');

            index++;
        }

        _buffer = Encoding.UTF8.GetBytes(builder.ToString());
    }

    [Benchmark(Baseline = true)]
    public unsafe long ScalarScan()
    {
        fixed (byte* basePtr = _buffer)
        {
            long lines = 0;
            long pos = 0;
            long end = _buffer.Length;

            while (pos < end)
            {
                var semicolon = FindByteScalar(basePtr, pos, end, Semicolon);
                if (semicolon >= end)
                    break;

                var newline = FindByteScalar(basePtr, semicolon + 1, end, Newline);
                if (newline >= end)
                    break;

                lines++;
                pos = newline + 1;
            }

            return lines;
        }
    }

    [Benchmark]
    public unsafe long SimdScan()
    {
        // Hoisted out of the loop exactly as the lesson hoists them out of the per-thread loop:
        // rebuilding the target vector on every lookup costs more than the lookup saves.
        var semicolonVec = Vector256.Create(Semicolon);
        var newlineVec = Vector256.Create(Newline);

        fixed (byte* basePtr = _buffer)
        {
            long lines = 0;
            long pos = 0;
            long end = _buffer.Length;

            while (pos < end)
            {
                var semicolon = FindByteFast(basePtr, pos, end, semicolonVec, Semicolon);
                if (semicolon >= end)
                    break;

                var newline = FindByteFast(basePtr, semicolon + 1, end, newlineVec, Newline);
                if (newline >= end)
                    break;

                lines++;
                pos = newline + 1;
            }

            return lines;
        }
    }

    [Benchmark]
    public long SpanIndexOfScan()
    {
        var span = _buffer.AsSpan();
        long lines = 0;
        var pos = 0;

        while (pos < span.Length)
        {
            var semicolon = span[pos..].IndexOf(Semicolon);
            if (semicolon < 0)
                break;

            semicolon += pos;

            var newline = span[(semicolon + 1)..].IndexOf(Newline);
            if (newline < 0)
                break;

            newline += semicolon + 1;

            lines++;
            pos = newline + 1;
        }

        return lines;
    }

    /// <summary>
    /// The byte-at-a-time search Level 4 used, and the fallback tail of FindByteFast.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static unsafe long FindByteScalar(byte* basePtr, long start, long end, byte target)
    {
        var pos = start;

        while (pos < end)
        {
            if (basePtr[pos] == target)
                return pos;

            pos++;
        }

        return end;
    }

    /// <summary>
    /// The chapter's vectorized search, identical to the one in Level5_SIMD/Program.cs. The AVX2
    /// block is the course's; the Vector128 block keeps it vectorized on hardware without AVX2,
    /// which is what this machine is.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static unsafe long FindByteFast(byte* basePtr,
                                            long start,
                                            long end,
                                            Vector256<byte> targetVec,
                                            byte target)
    {
        var pos = start;

        if (Avx2.IsSupported)
        {
            while (pos + 32 <= end)
            {
                var data = Avx.LoadVector256(basePtr + pos);
                var cmp = Avx2.CompareEqual(data, targetVec);
                var mask = (uint)Avx2.MoveMask(cmp);

                if (mask != 0)
                    return pos + BitOperations.TrailingZeroCount(mask);

                pos += 32;
            }
        }
        else if (Vector128.IsHardwareAccelerated)
        {
            var targetVec128 = targetVec.GetLower();

            while (pos + 16 <= end)
            {
                var data = Vector128.Load(basePtr + pos);
                var mask = Vector128.Equals(data, targetVec128).ExtractMostSignificantBits();

                if (mask != 0)
                    return pos + BitOperations.TrailingZeroCount(mask);

                pos += 16;
            }
        }

        while (pos < end)
        {
            if (basePtr[pos] == target)
                return pos;

            pos++;
        }

        return end;
    }
}
