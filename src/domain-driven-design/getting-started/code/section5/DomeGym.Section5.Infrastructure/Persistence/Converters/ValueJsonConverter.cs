using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DomeGym.Section5.Infrastructure.Persistence.Converters;

public class ValueJsonConverter<T>() : ValueConverter<T, string>(
    value => JsonSerializer.Serialize(value, JsonSerializerOptions.Default),
    json => JsonSerializer.Deserialize<T>(json, JsonSerializerOptions.Default)!);

public class ValueJsonComparer<T>() : ValueComparer<T>(
    (left, right) => JsonSerializer.Serialize(left, JsonSerializerOptions.Default)
        == JsonSerializer.Serialize(right, JsonSerializerOptions.Default),
    value => value == null ? 0 : JsonSerializer.Serialize(value, JsonSerializerOptions.Default).GetHashCode(StringComparison.Ordinal),
    value => JsonSerializer.Deserialize<T>(
        JsonSerializer.Serialize(value, JsonSerializerOptions.Default),
        JsonSerializerOptions.Default)!);
