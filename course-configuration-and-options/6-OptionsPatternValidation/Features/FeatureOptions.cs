using System.ComponentModel.DataAnnotations;

namespace _6_OptionsPatternValidation.Features;

public sealed class FeatureOptions
{
    public bool Enabled { get; set; }

    [MaxLength(length: 100,
        ErrorMessage = "The name cannot be longer than 100 characters.")]
    public string? Name { get; set; }

    public Version? Version { get; set; }

    public Uri? Endpoint { get; set; }

    public string? ApiKey { get; set; }

    [DeniedValues(values: ["out-of-date"])]
    public string[] Tags { get; set; } = [];
}