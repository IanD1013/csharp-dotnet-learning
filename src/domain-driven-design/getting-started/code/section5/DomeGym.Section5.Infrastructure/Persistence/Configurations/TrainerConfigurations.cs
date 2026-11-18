using DomeGym.Section5.Domain.TrainerAggregate;
using DomeGym.Section5.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomeGym.Section5.Infrastructure.Persistence.Configurations;

public class TrainerConfigurations : IEntityTypeConfiguration<Trainer>
{
    public void Configure(EntityTypeBuilder<Trainer> builder)
    {
        builder.HasKey(trainer => trainer.Id);

        builder.Property(trainer => trainer.Id)
            .ValueGeneratedNever();

        builder.Property<List<Guid>>("_sessionIds")
            .HasColumnName("SessionIds")
            .HasListOfIdsConverter();

        builder.OwnsSchedule();

        builder.Property(trainer => trainer.UserId);
    }
}
