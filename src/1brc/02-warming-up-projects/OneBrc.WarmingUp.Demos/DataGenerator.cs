using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace OneBrc.WarmingUp.Demos;

/// <summary>
/// The synthetic measurements generator from "Data Generator and Other Projects":
/// UTF-8 without BOM, Unix line endings, one fractional digit, Box-Muller temperatures.
/// </summary>
internal static class DataGenerator
{
    private const int BufferSize = 8_000_000;
    private const double StdDev = 10.0;

    /// <summary>
    /// The course generates from all 413 official stations. Only the ones printed on screen in
    /// the lessons are reproduced here, so nothing in this file is invented data.
    /// </summary>
    internal static readonly (string Name, double MeanTemp)[] Stations =
    [
        ("Abha", 18.0),
        ("Abidjan", 26.0),
        ("Abéché", 29.4),
        ("Accra", 26.4),
        ("Addis Ababa", 16.0),
        ("Adelaide", 17.3),
        ("Aden", 29.1),
        ("Ahvaz", 25.4),
        ("Albuquerque", 14.0),
        ("Alexandra", 11.0),
        ("Alexandria", 20.0),
        ("Algiers", 18.2),
        ("Alice Springs", 21.0),
        ("Almaty", 10.0),
        ("Amsterdam", 10.2),
        ("Zürich", 9.3)
    ];

    /// <summary>
    /// Writes <paramref name="rowCount"/> measurement lines to <paramref name="outputPath"/>
    /// and returns how long the write took.
    /// </summary>
    internal static TimeSpan Generate(string outputPath, long rowCount, bool reportProgress)
    {
        var directory = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            Console.WriteLine($"  Directory created: {directory}");
        }

        // Use UTF-8 encoding WITHOUT BOM (as per 1BRC specification)
        var utf8NoBom = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
        var progressInterval = Math.Max(rowCount / 10, 1);
        var stopwatch = Stopwatch.StartNew();

        using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None, BufferSize, FileOptions.SequentialScan))
        using (var writer = new StreamWriter(fileStream, utf8NoBom, BufferSize))
        {
            // Use Unix line endings (\n) as per 1BRC specification
            writer.NewLine = "\n";

            var random = new Random();
            var lastProgressRows = 0L;

            for (var row = 0L; row < rowCount; row++)
            {
                // Randomly select a station
                var stationIndex = random.Next(Stations.Length);
                var (name, meanTemp) = Stations[stationIndex];

                // Generate temperature with Gaussian-like distribution (Box-Muller approximation)
                var u1 = random.NextDouble();
                var u2 = random.NextDouble();
                var gaussian = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);
                var temperature = meanTemp + gaussian * StdDev;

                // Clamp to reasonable range and format to 1 decimal place
                temperature = Math.Round(Math.Clamp(temperature, -99.9, 99.9), 1);

                // Write the line
                writer.Write(name);
                writer.Write(';');
                // Invariant culture, or a comma-decimal locale would silently write `12,3`.
                writer.WriteLine(temperature.ToString("0.0", CultureInfo.InvariantCulture));

                // Update progress
                var completedRows = row + 1;
                if (reportProgress && completedRows - lastProgressRows >= progressInterval)
                {
                    lastProgressRows = completedRows;
                    var percentage = (double)completedRows / rowCount * 100;
                    var elapsed = stopwatch.Elapsed.TotalSeconds;
                    var rowsPerSecond = completedRows / elapsed;
                    var estimatedTotal = rowCount / rowsPerSecond;
                    var remaining = estimatedTotal - elapsed;

                    Console.Write($"\r  Progress: {percentage,6:F2}% | {completedRows:N0} / {rowCount:N0} rows | ");
                    Console.Write($"{rowsPerSecond / 1_000_000:F2}M rows/sec | ");
                    Console.Write($"ETA: {TimeSpan.FromSeconds(remaining):hh\\:mm\\:ss}    ");
                }
            }
        }

        stopwatch.Stop();

        if (reportProgress)
        {
            Console.WriteLine();
        }

        return stopwatch.Elapsed;
    }

    /// <summary>
    /// Renders a byte count the way the lesson reads file sizes off Explorer.
    /// </summary>
    internal static string FormatSize(long bytes) => bytes switch
    {
        >= 1024L * 1024 * 1024 => $"{bytes / 1024.0 / 1024 / 1024:F2} GB",
        >= 1024 * 1024 => $"{bytes / 1024.0 / 1024:F2} MB",
        _ => $"{bytes / 1024.0:F0} KB"
    };
}
