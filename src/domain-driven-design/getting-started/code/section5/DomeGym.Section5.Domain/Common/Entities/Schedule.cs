using DomeGym.Section5.Domain.Common.ValueObjects;
using ErrorOr;

namespace DomeGym.Section5.Domain.Common.Entities;

public class Schedule : Entity
{
    private readonly Dictionary<DateOnly, List<TimeRange>> _calendar = [];

    public Schedule(Dictionary<DateOnly, List<TimeRange>>? calendar = null, Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        _calendar = calendar ?? [];
    }

    // Used by EF Core when materializing the owning aggregate.
    private Schedule()
    {
    }

    public static Schedule Empty() => new(id: Guid.NewGuid());

    internal bool CanBookTimeSlot(DateOnly date, TimeRange time)
    {
        if (!_calendar.TryGetValue(date, out List<TimeRange>? timeSlots))
        {
            return true;
        }

        return !timeSlots.Any(timeSlot => timeSlot.OverlapsWith(time));
    }

    internal ErrorOr<Success> BookTimeSlot(DateOnly date, TimeRange time)
    {
        if (!_calendar.TryGetValue(date, out List<TimeRange>? timeSlots))
        {
            _calendar[date] = [time];
            return Result.Success;
        }

        if (timeSlots.Any(timeSlot => timeSlot.OverlapsWith(time)))
        {
            return Error.Conflict();
        }

        timeSlots.Add(time);
        return Result.Success;
    }

    internal ErrorOr<Success> RemoveBooking(DateOnly date, TimeRange time)
    {
        if (!_calendar.TryGetValue(date, out List<TimeRange>? timeSlots) || !timeSlots.Remove(time))
        {
            return Error.NotFound(description: "Booking not found");
        }

        return Result.Success;
    }
}
