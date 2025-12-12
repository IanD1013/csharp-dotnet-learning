using Microsoft.Extensions.Options;

namespace _6_OptionsPatternValidation.Features;

public sealed class FeatureOptions : IValidateOptions<FeatureOptions>
{
    public bool Enabled { get; set; }

    public string? Name { get; set; }

    public Version? Version { get; set; }

    public Uri? Endpoint { get; set; }

    public string? ApiKey { get; set; }

    public string[] Tags { get; set; } = [];

    public ValidateOptionsResult Validate(string? name, FeatureOptions options)
    {
        if (IsNamed(name, expectedName: "TodoApi"))
        {
            if (options.Enabled is false)
            {
                return ValidateOptionsResult.Success;
            }

            List<string> failures = [];

            if (options is { Endpoint: null })
            {
                failures.Add("TODO API requires a valid endpoint.");
            }

            if (options is { Version.Major: 0 })
            {
                failures.Add("TODO API running non-production version.");
            }

            if (failures.Count > 0)
            {
                return ValidateOptionsResult.Fail(failures);
            }
        }

        if (IsNamed(name, expectedName: "WeatherStation") && options is { Enabled: true })
        {
            return ValidateOptionsResult.Fail(
                "The weather station cannot be enabled. It hasn't been implemented yet...");
        }

        return ValidateOptionsResult.Skip;

        static bool IsNamed(string? name, string expectedName)
        {
            return string.Equals(name, expectedName, StringComparison.OrdinalIgnoreCase);
        }
    }
}