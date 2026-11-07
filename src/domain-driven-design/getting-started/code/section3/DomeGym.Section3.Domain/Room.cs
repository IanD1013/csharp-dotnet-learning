using ErrorOr;

namespace DomeGym.Section3.Domain;

public class Room(int maxDailySessions, Guid gymId, Schedule? schedule = null, Guid? id = null)
{
    private readonly int _maxDailySessions = maxDailySessions;
    private readonly Schedule _schedule = schedule ?? Schedule.Empty();
    private readonly List<Guid> _sessionIds = [];

    public Guid Id { get; } = id ?? Guid.NewGuid();

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
