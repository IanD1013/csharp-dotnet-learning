namespace Shared;

public static class GlobalConstants
{
    // The course pins these to a fixed drive (V:\Dometrain\1BRC\Files). Here they resolve under
    // the temp folder so a 13 GB measurements file never lands in the working tree, which also
    // means they are static readonly rather than const.
    public static readonly string FilesDirectory = Path.Combine(Path.GetTempPath(), "1brc", "Files");
    public static readonly string FilePath = Path.Combine(FilesDirectory, "measurements.txt");

    public const int ExpectedStationCount = 413;
    //public const int ExpectedStationCount = 10_000;
}


public static class ResultLogger
{
    private const string ResultFileName = "results.log";

    /// <summary>
    /// Appends the 1BRC result to a log file in the solution directory.
    /// </summary>
    /// <param name="projectName">Name of the project (e.g., "Level01_Naive")</param>
    /// <param name="output">The 1BRC formatted output string</param>
    /// <param name="elapsed">Time elapsed for processing</param>
    /// <param name="rowCount">Number of rows processed</param>
    /// <param name="stationCount">Number of unique stations found</param>
    public static void SaveResult(
        string projectName,
        string output,
        TimeSpan elapsed,
        long rowCount,
        int stationCount)
    {
        try
        {
            var solutionDirectory = GlobalConstants.FilesDirectory;
            var filePath = Path.Combine(solutionDirectory, ResultFileName);

            // Collect memory statistics
            var workingSetMB = Environment.WorkingSet / 1024 / 1024;
            var gcMemoryMB = GC.GetTotalMemory(false) / 1024 / 1024;
            var gen0Collections = GC.CollectionCount(0);
            var gen1Collections = GC.CollectionCount(1);
            var gen2Collections = GC.CollectionCount(2);

            // Calculate throughput
            var throughputMBps = rowCount > 0 ? (rowCount * 25.0 / 1024 / 1024) / elapsed.TotalSeconds : 0; // Assuming ~25 bytes per row
            var rowsPerSecond = rowCount / elapsed.TotalSeconds;

            var logEntry = $"""
                ================================================================================
                [{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {projectName}
                ================================================================================
                Performance:
                  Rows:               {rowCount:N0}
                  Stations:           {stationCount}
                  Elapsed:            {elapsed}
                  Throughput:         {rowsPerSecond:N0} rows/sec ({throughputMBps:F2} MB/sec)

                Memory:
                  Working Set:        {workingSetMB:N0} MB
                  GC Memory:          {gcMemoryMB:N0} MB
                  Gen0 Collections:   {gen0Collections}
                  Gen1 Collections:   {gen1Collections}
                  Gen2 Collections:   {gen2Collections}

                Processor:
                  CPU Cores:          {Environment.ProcessorCount}
                --------------------------------------------------------------------------------
                {output}



                """;

            File.AppendAllText(filePath, logEntry, System.Text.Encoding.UTF8);
            Console.WriteLine($"\n📁 Results saved: {filePath}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"\n⚠️ Results could not be saved: {ex.Message}");
        }
    }

    /// <summary>
    /// Formats results to 1BRC output format.
    /// </summary>
    public static string FormatOutput<T>(IEnumerable<KeyValuePair<string, T>> sortedResults) =>
        FormatOutput(sortedResults, static value => value?.ToString() ?? string.Empty);

    /// <summary>
    /// Formats results with custom formatter.
    /// </summary>
    public static string FormatOutput<T>(
        IEnumerable<KeyValuePair<string, T>> sortedResults,
        Func<T, string> formatter)
    {
        ArgumentNullException.ThrowIfNull(sortedResults);
        ArgumentNullException.ThrowIfNull(formatter);

        return "{" +
               string.Join(", ", sortedResults.Select(kvp => $"{kvp.Key}={formatter(kvp.Value)}")) +
               "}";
    }
}


public class StationStats
{
    public double Min { get; set; }
    public double Max { get; set; }
    public double Sum { get; set; }

    public int Count { get; set; }

    public double Mean => Count > 0 ? Sum / Count : 0;

    public void Update(double temp)
    {
        if (temp < Min)
            Min = temp;

        if (temp > Max)
            Max = temp;

        Sum += temp;
        Count++;
    }

    public override string ToString() => $"{Min:F1}/{Mean:F1}/{Max:F1}";
}
