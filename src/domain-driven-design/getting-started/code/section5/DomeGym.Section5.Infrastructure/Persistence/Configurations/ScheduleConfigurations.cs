using DomeGym.Section5.Domain.Common;
using DomeGym.Section5.Domain.Common.Entities;
using DomeGym.Section5.Domain.Common.ValueObjects;
using DomeGym.Section5.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomeGym.Section5.Infrastructure.Persistence.Configurations;

internal static class ScheduleConfigurations
{
    // Participants, rooms and trainers each own a private Schedule, stored in the owner's table.
    public static void OwnsSchedule<TAggregate>(this EntityTypeBuilder<TAggregate> builder)
        where TAggregate : AggregateRoot
    {
        builder.OwnsOne<Schedule>("_schedule", scheduleBuilder =>
        {
            scheduleBuilder.Property<Dictionary<DateOnly, List<TimeRange>>>("_calendar")
                .HasColumnName("ScheduleCalendar")
                .HasValueJsonConverter();

            scheduleBuilder.Property(schedule => schedule.Id)
                .HasColumnName("ScheduleId");
        });
    }
}
