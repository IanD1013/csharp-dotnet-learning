using DomeGym.Section5.Domain.SessionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomeGym.Section5.Infrastructure.Persistence.Configurations;

public class SessionConfigurations : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.HasKey(session => session.Id);

        builder.Property(session => session.Id)
            .ValueGeneratedNever();

        builder.OwnsMany<Reservation>("_reservations", reservationBuilder =>
        {
            reservationBuilder.ToTable("SessionReservations");

            reservationBuilder.WithOwner().HasForeignKey("SessionId");

            reservationBuilder.HasKey(reservation => reservation.Id);

            reservationBuilder.Property(reservation => reservation.Id)
                .ValueGeneratedNever();

            reservationBuilder.Property(reservation => reservation.ParticipantId);
        });

        builder.OwnsMany(session => session.Categories, categoryBuilder =>
        {
            categoryBuilder.ToTable("SessionCategories");

            categoryBuilder.WithOwner().HasForeignKey("SessionId");

            categoryBuilder.Property<int>("Id");
            categoryBuilder.HasKey("Id");

            categoryBuilder.Property(category => category.Name);

            categoryBuilder.Property(category => category.Value);
        });

        builder.Navigation(session => session.Categories)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.OwnsOne(session => session.Time, timeBuilder =>
        {
            // Read-only properties are only mapped when configured explicitly.
            timeBuilder.Property(time => time.Start);

            timeBuilder.Property(time => time.End);
        });

        builder.Property(session => session.Name);

        builder.Property(session => session.Description);

        builder.Property(session => session.MaxParticipants);

        builder.Property(session => session.RoomId);

        builder.Property(session => session.TrainerId);

        builder.Property(session => session.Date);
    }
}
