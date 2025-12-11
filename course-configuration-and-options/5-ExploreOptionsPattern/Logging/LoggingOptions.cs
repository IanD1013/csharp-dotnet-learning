namespace _5_ExploreOptionsPattern.Logging;

public sealed class LoggingOptions
{
    // The logging configuration section name. This is exposed by convention
    // to allow consumers of these options to bind configuration to their 
    // named-section
    public const string LoggingConfigurationSectionName = "Logging";
    
    public LogLevelOptions? LogLevel { get; set; }
}