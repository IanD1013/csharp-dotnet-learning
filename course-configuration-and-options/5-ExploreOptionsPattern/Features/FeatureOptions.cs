namespace _5_ExploreOptionsPattern.Features;

public sealed class FeatureOptions
{
    public bool Enabled { get; set; }
    
    public string? Name { get; set; }
    
    public Version? Version { get; set; }
    
    public Uri? Endpoint { get; set; }   
    
    public string? ApiKey { get; set; }

    public string[] Tags { get; set; } = [];
}