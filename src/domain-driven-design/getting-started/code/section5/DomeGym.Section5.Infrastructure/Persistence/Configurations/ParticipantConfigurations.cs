using DomeGym.Section5.Domain.ParticipantAggregate;
using DomeGym.Section5.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomeGym.Section5.Infrastructure.Persistence.Configurations;

public class ParticipantConfigurations : IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        builder.HasKey(participant => participant.Id);

        builder.Property(participant => participant.Id)
            .ValueGeneratedNever();

        builder.OwnsSchedule();

        builder.Property<List<Guid>>("_sessionIds")
            .HasColumnName("SessionIds")
            .HasListOfIdsConverter();

        builder.Ignore(participant => participant.SessionIds);

        builder.Property(participant => participant.UserId);
    }
}
