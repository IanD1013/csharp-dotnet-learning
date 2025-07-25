using System.Text.Json.Serialization;

namespace Movies.Contracts.Responses;

// HATEOAS: hypermedia as the engine of application state
public abstract class HalResponse // hyper media api language
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<Link> Links { get; set; } = new();
}

public class Link
{
    public required string Href { get; init; }
    public required string Rel { get; init; }
    public required string Type { get; init; }
}
