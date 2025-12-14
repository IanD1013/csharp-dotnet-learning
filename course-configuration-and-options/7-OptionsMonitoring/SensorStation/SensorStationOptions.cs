using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace _7_OptionsMonitoring.SensorStation;

[OptionsValidator]
public sealed partial class SensorStationOptions : IValidateOptions<SensorStationOptions>
{
    public const string SensorStationOptionsSectionName = "SensorOptions";

    [Range(type: typeof(TimeSpan), minimum: "00:00:00", maximum: "23:59:59",
        ErrorMessage = "Time must be between 00:00:00 and 23:59:59")]
    [RegularExpression(pattern: @"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]$",
        ErrorMessage = "Time must be in the format HH:mm:ss and between 00:00:00 and 23:59:59")]
    public TimeSpan PollingInterval { get; set; }
    
    [Required(ErrorMessage = "A mapping of station names to thresholds is required")]
    public Dictionary<string, ThresholdOptions>? Sensors { get; set; } = default!;

    public override string ToString()
    {
        var builder = new StringBuilder();

        builder.Append(
            $"Polling interval: {PollingInterval}");

        builder.AppendLine();

        if (Sensors is { Count: > 0 })
        {
            builder.AppendLine("Sensors:");
        }

        foreach (var (name, thresholds) in Sensors ?? [])
        {
            builder.Append($"  \"{name}\" — Threshold range: ({thresholds.Low:F2}°F - {thresholds.High:F2}°F)");

            builder.AppendLine();
        }

        return builder.ToString();
    }
}