using DomeGym.Section4.Domain.Common;
using DomeGym.Section4.Domain.Common.Entities;
using DomeGym.Section4.Domain.SessionAggregate;
using ErrorOr;

namespace DomeGym.Section4.Domain.RoomAggregate;

public class Room(int maxDailySessions, Guid gymId, Schedule? schedule = null, Guid? id = null)
    : AggregateRoot(id ?? Guid.NewGuid())
{
    private readonly int _maxDailySessions = maxDailySessions;
    private readonly Schedule _schedule = schedule ?? Schedule.Empty();
    private readonly List<Guid> _sessionIds = [];

    public Guid GymId { get; } = gymId;

    public ErrorOr<Success> ScheduleSession(Session session)
    {
        if (_sessionIds.Contains(session.Id))
        {
            return Error.Conflict(description: "Session already exists in room");
        }

        if (_sessionIds.Count >= _maxDailySessions)
        {
            return RoomErrors.CannotHaveMoreSessionsThanSubscriptionAllows;
        }

        ErrorOr<Success> bookTimeSlotResult = _schedule.BookTimeSlot(session.Date, session.Time);

        if (bookTimeSlotResult.IsError)
        {
            return bookTimeSlotResult.FirstError.Type == ErrorType.Conflict
                ? RoomErrors.CannotHaveTwoOrMoreOverlappingSessions
                : bookTimeSlotResult.Errors;
        }

        _sessionIds.Add(session.Id);
        return Result.Success;
    }
}
