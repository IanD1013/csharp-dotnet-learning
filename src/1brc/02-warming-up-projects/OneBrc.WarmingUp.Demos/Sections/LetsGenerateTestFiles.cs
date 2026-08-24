using OneBrc.WarmingUp.Core;

namespace OneBrc.WarmingUp.Demos.Sections;

/// <summary>
/// Lesson 3 - "Let's Generate Test Files".
/// Walks the ladder of test files the lesson generates, so the linear growth in size and the
/// generator's throughput are visible before anyone reaches for the full 13 GB input.
/// </summary>
internal static class LetsGenerateTestFiles
{
    private static readonly long[] RowCounts = [10_000, 100_000, 1_000_000, 10_000_000];

    internal static void Run()
    {
        Console.WriteLine("=== 3. Let's Generate Test Files ===");
        Console.WriteLine($"  {"rows",12} | {"size",10} | {"elapsed",9} | throughput");
        Console.WriteLine($"  {new string('-', 12)}-+-{new string('-', 10)}-+-{new string('-', 9)}-+-----------");

        foreach (var rowCount in RowCounts)
        {
            var path = Path.Combine(GlobalConstants.FilesDirectory, $"measurements-{Label(rowCount)}.txt");
            var elapsed = DataGenerator.Generate(path, rowCount, reportProgress: false);
            var size = new FileInfo(path).Length;
            var rowsPerSecond = rowCount / elapsed.TotalSeconds;

            Console.WriteLine($"  {rowCount,12:N0} | {DataGenerator.FormatSize(size),10} | {elapsed.TotalSeconds,8:F2}s | {rowsPerSecond / 1_000_000:F2}M rows/sec");
        }

        // Extrapolating the smallest file: the full billion rows lands around 13 GB.
        var tenThousand = new FileInfo(Path.Combine(GlobalConstants.FilesDirectory, "measurements-10k.txt")).Length;
        Console.WriteLine();
        Console.WriteLine($"  Extrapolated to 1,000,000,000 rows: {DataGenerator.FormatSize(tenThousand * 100_000)}");
        Console.WriteLine();
        Console.WriteLine("  First lines of measurements-10k.txt:");
        foreach (var line in File.ReadLines(Path.Combine(GlobalConstants.FilesDirectory, "measurements-10k.txt")).Take(5))
        {
            Console.WriteLine($"  | {line}");
        }

        Console.WriteLine();
    }

    private static string Label(long rowCount) => rowCount switch
    {
        >= 1_000_000 => $"{rowCount / 1_000_000}m",
        _ => $"{rowCount / 1_000}k"
    };
}
