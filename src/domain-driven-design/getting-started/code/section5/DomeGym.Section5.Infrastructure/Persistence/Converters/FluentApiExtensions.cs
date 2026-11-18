using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomeGym.Section5.Infrastructure.Persistence.Converters;

public static class FluentApiExtensions
{
    // SQLite has no JSON column type, so complex values are stored as JSON text.
    public static PropertyBuilder<T> HasValueJsonConverter<T>(this PropertyBuilder<T> propertyBuilder) =>
        propertyBuilder.HasConversion(new ValueJsonConverter<T>(), new ValueJsonComparer<T>());

    public static PropertyBuilder<List<Guid>> HasListOfIdsConverter(this PropertyBuilder<List<Guid>> propertyBuilder) =>
        propertyBuilder.HasConversion(new ListOfIdsConverter(), new ListOfIdsComparer());
}
