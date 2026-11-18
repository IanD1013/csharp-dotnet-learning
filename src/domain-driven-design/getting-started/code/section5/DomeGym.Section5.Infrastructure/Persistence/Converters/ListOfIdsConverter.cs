using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DomeGym.Section5.Infrastructure.Persistence.Converters;

public class ListOfIdsConverter() : ValueConverter<List<Guid>, string>(
    ids => string.Join(',', ids),
    value => value.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(Guid.Parse).ToList());

public class ListOfIdsComparer() : ValueComparer<List<Guid>>(
    (left, right) => left!.SequenceEqual(right!),
    ids => ids.Aggregate(0, (hashCode, id) => HashCode.Combine(hashCode, id)),
    ids => ids.ToList());
