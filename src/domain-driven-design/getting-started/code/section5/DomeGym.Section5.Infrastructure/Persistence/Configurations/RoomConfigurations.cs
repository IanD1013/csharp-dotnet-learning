using DomeGym.Section5.Domain.RoomAggregate;
using DomeGym.Section5.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomeGym.Section5.Infrastructure.Persistence.Configurations;

public class RoomConfigurations : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(room => room.Id);

        builder.Property(room => room.Id)
            .ValueGeneratedNever();

        builder.Property<int>("_maxDailySessions")
            .HasColumnName("MaxDailySessions");

        builder.Property<Dictionary<DateOnly, List<Guid>>>("_sessionIdsByDate")
            .HasColumnName("SessionIdsByDate")
            .HasValueJsonConverter();

        builder.OwnsSchedule();

        builder.Property(room => room.Name);

        builder.Property(room => room.GymId);
    }
}
