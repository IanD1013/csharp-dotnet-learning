using OneBrc.WarmingUp.Core;

namespace OneBrc.WarmingUp.Demos.Sections;

/// <summary>
/// Lesson 2 - "Data Generator and Other Projects".
/// Generates one file with the Box-Muller generator, then hands the run to
/// <see cref="ResultLogger"/> and prints the block it appended to results.log.
/// </summary>
internal static class DataGeneratorAndOtherProjects
{
    internal static void Run(long rowCount)
    {
        Console.WriteLine("=== 2. Data Generator and Other Projects ===");
        Console.WriteLine($"  - Output file:    {GlobalConstants.FilePath}");
        Console.WriteLine($"  - Station count:  {DataGenerator.Stations.Length}");
        Console.WriteLine($"  - Expected count: {GlobalConstants.ExpectedStationCount} (the full course dataset)");
        Console.WriteLine();

        var elapsed = DataGenerator.Generate(GlobalConstants.FilePath, rowCount, reportProgress: true);
        var size = new FileInfo(GlobalConstants.FilePath).Length;

        Console.WriteLine($"  Generated {rowCount:N0} rows in {elapsed.TotalSeconds:F2}s ({DataGenerator.FormatSize(size)})");

        // Every level of the challenge reports through the same logger, which is what makes
        // naive and expert runs comparable months apart.
        ResultLogger.SaveResult(
            projectName: "Level00_DataGenerator",
            output: $"Wrote {DataGenerator.FormatSize(size)} to {GlobalConstants.FilePath}",
            elapsed: elapsed,
            rowCount: rowCount,
            stationCount: DataGenerator.Stations.Length);

        var logPath = Path.Combine(GlobalConstants.FilesDirectory, "results.log");
        Console.WriteLine();
        Console.WriteLine("  Last entry in results.log:");
        foreach (var line in File.ReadAllLines(logPath).TakeLast(22))
        {
            Console.WriteLine($"  | {line}");
        }

        Console.WriteLine();
    }
}
