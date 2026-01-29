using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Demo.Generator;

namespace Demo.Domain.Entities;

[ToJsonSerializer]
public partial class Person
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new() { WriteIndented = true };

    public string EmailAddress { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;

    public string ToJson()
    {
        var builder = new StringBuilder();

        builder.AppendLine("{");
        builder.AppendLine($"  \"EmailAddress\": \"{EmailAddress}\",");
        builder.AppendLine($"  \"FirstName\": \"{FirstName}\",");
        builder.AppendLine($"  \"LastName\": \"{LastName}\",");
        builder.AppendLine($"  \"PhoneNumber\": \"{PhoneNumber}\"");
        builder.Append('}');

        return builder.ToString();
    }

    public string ToJsonViaReflection()
    {
        var builder = new StringBuilder();
        builder.AppendLine("{");

        foreach (var property in GetType()
                     .GetProperties()
                     .OrderBy(x => x.Name))
        {
            builder.AppendLine($"  \"{property.Name}\": \"{property.GetValue(this)}\",");
        }

        return builder.ToString()[..^3] + "\r\n}";
    }

    public string ToJsonViaSystemText()
    {
        return JsonSerializer.Serialize(this, JsonSerializerOptions);
    }

    public string ToJsonViaSystemTextGenerated()
    {
        return JsonSerializer.Serialize(this, PersonSerializerContext.Default.Person);
    }
}

[JsonSerializable(typeof(Person))]
[JsonSourceGenerationOptions(WriteIndented = true)]
public partial class PersonSerializerContext : JsonSerializerContext
{
}