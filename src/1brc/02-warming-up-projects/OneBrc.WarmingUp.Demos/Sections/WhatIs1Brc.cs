using System.Text;

using OneBrc.WarmingUp.Core;

namespace OneBrc.WarmingUp.Demos.Sections;

/// <summary>
/// Lesson 1 - "What is 1BRC actually?".
/// Reads back the smallest generated file and checks it against the challenge spec:
/// UTF-8 without BOM, `Station;Temperature`, exactly one fractional digit, -99.9..99.9,
/// names of 1..100 bytes, and the `{Station=min/mean/max, ...}` output shape.
/// </summary>
internal static class WhatIs1Brc
{
    internal static void Run()
    {
        Console.WriteLine("=== 1. What is 1BRC actually? ===");

        var path = Path.Combine(GlobalConstants.FilesDirectory, "measurements-10k.txt");
        if (!File.Exists(path))
        {
            DataGenerator.Generate(path, 10_000, reportProgress: false);
        }

        var firstThreeBytes = new byte[3];
        using (var probe = File.OpenRead(path))
        {
            probe.ReadExactly(firstThreeBytes);
        }

        var hasBom = firstThreeBytes is [0xEF, 0xBB, 0xBF];
        var raw = File.ReadAllBytes(path);
        var crlfCount = 0;
        for (var i = 1; i < raw.Length; i++)
        {
            if (raw[i] == (byte)'\n' && raw[i - 1] == (byte)'\r')
            {
                crlfCount++;
            }
        }

        Console.WriteLine($"  file:             {path}");
        Console.WriteLine($"  UTF-8 BOM:        {(hasBom ? "present (spec violation)" : "absent (as the spec requires)")}");
        Console.WriteLine($"  CRLF line breaks: {crlfCount} (the spec wants \\n only)");

        // The whole file fits in memory here only because it is the 10k row one.
        // File.ReadAllLines() on the real 13 GB input is exactly what the lesson says crashes.
        var lines = File.ReadAllLines(path);
        var stations = new SortedDictionary<string, (double Min, double Sum, double Max, long Count)>(StringComparer.Ordinal);
        var oneFractionalDigit = true;
        var inRange = true;
        var maxNameBytes = 0;

        foreach (var line in lines)
        {
            var separator = line.IndexOf(';', StringComparison.Ordinal);
            var name = line[..separator];
            var value = line[(separator + 1)..];

            oneFractionalDigit &= value.Length - value.IndexOf('.') - 1 == 1;
            maxNameBytes = Math.Max(maxNameBytes, Encoding.UTF8.GetByteCount(name));

            var temperature = double.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
            inRange &= temperature is >= -99.9 and <= 99.9;

            if (stations.TryGetValue(name, out var current))
            {
                stations[name] = (Math.Min(current.Min, temperature), current.Sum + temperature, Math.Max(current.Max, temperature), current.Count + 1);
            }
            else
            {
                stations[name] = (temperature, temperature, temperature, 1);
            }
        }

        Console.WriteLine($"  rows:             {lines.Length:N0}");
        Console.WriteLine($"  unique stations:  {stations.Count} (the spec allows up to 10,000)");
        Console.WriteLine($"  longest name:     {maxNameBytes} bytes (the spec allows 1 to 100)");
        Console.WriteLine($"  one decimal:      {oneFractionalDigit}");
        Console.WriteLine($"  within -99.9..99.9: {inRange}");

        // Mean is rounded half-up to one decimal, and stations come out alphabetically.
        var formatted = stations.Select(s =>
        {
            var mean = Math.Round(s.Value.Sum / s.Value.Count, 1, MidpointRounding.AwayFromZero);
            return $"{s.Key}={s.Value.Min:0.0}/{mean:0.0}/{s.Value.Max:0.0}";
        });

        Console.WriteLine($"  output:           {{{string.Join(", ", formatted)}}}");
        Console.WriteLine();
    }
}
