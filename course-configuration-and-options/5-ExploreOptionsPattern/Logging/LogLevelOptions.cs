namespace _5_ExploreOptionsPattern.Logging;

public sealed class LogLevelOptions : Dictionary<string, LogLevel>
{
    public LogLevelOptions() : base(StringComparer.OrdinalIgnoreCase)
    {
    }
}