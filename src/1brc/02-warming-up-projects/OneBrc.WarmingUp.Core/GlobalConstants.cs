namespace OneBrc.WarmingUp.Core;

/// <summary>
/// The paths and counts every 1BRC level shares.
/// The course pins these to a fixed drive (V:\Dometrain\1BRC\Files); this repo keeps the
/// generated measurement files out of the working tree by putting them under the temp folder.
/// </summary>
public static class GlobalConstants
{
    public static readonly string FilesDirectory = Path.Combine(Path.GetTempPath(), "1brc", "Files");

    public static readonly string FilePath = Path.Combine(FilesDirectory, "measurements.txt");

    public const int ExpectedStationCount = 413;
    //public const int ExpectedStationCount = 10_000;
}
