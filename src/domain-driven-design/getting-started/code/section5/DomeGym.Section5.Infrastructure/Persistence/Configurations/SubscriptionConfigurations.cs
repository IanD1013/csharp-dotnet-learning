using DomeGym.Section5.Domain.SubscriptionAggregate;
using DomeGym.Section5.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomeGym.Section5.Infrastructure.Persistence.Configurations;

public class SubscriptionConfigurations : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.HasKey(subscription => subscription.Id);

        builder.Property(subscription => subscription.Id)
            .ValueGeneratedNever();

        builder.Property<int>("_maxGyms")
            .HasColumnName("MaxGyms");

        builder.Property<List<Guid>>("_gymIds")
            .HasColumnName("GymIds")
            .HasListOfIdsConverter();

        builder.Property(subscription => subscription.SubscriptionType)
            .HasConversion(
                subscriptionType => subscriptionType.Value,
                value => SubscriptionType.FromValue(value));

        builder.Property(subscription => subscription.AdminId);
    }
}
