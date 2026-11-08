using DomeGym.Section4.Domain.Common.ValueObjects;
using ErrorOr;

namespace DomeGym.Section4.Domain.Common.Entities;

public class Schedule(Dictionary<DateOnly, List<TimeRange>>? calendar = null, Guid? id = null)
    : Entity(id ?? Guid.NewGuid())
{
    private readonly Dictionary<DateOnly, List<TimeRange>> _calendar = calendar ?? [];

    public static Schedule Empty() => new();

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
