using DomeGym.Section5.Domain.Common;
using DomeGym.Section5.Domain.Common.Entities;
using DomeGym.Section5.Domain.SessionAggregate;
using ErrorOr;

namespace DomeGym.Section5.Domain.RoomAggregate;

public class Room : AggregateRoot
{
    private readonly int _maxDailySessions;
    private readonly Schedule _schedule = Schedule.Empty();
    private readonly Dictionary<DateOnly, List<Guid>> _sessionIdsByDate = [];

    public Room(string name, int maxDailySessions, Guid gymId, Schedule? schedule = null, Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        Name = name;
        _maxDailySessions = maxDailySessions;
        GymId = gymId;
        _schedule = schedule ?? Schedule.Empty();
    }

    // Used by EF Core when materializing the aggregate.
    private Room()
    {
    }

    public string Name { get; } = null!;

    public Guid GymId { get; }

    public IReadOnlyList<Guid> SessionIds => [.. _sessionIdsByDate.Values.SelectMany(sessionIds => sessionIds)];

    public ErrorOr<Success> ScheduleSession(Session session)
    {
        if (HasSession(session.Id))
        {
            return Error.Conflict(description: "Session already exists in room");
        }

        if (_sessionIdsByDate.TryGetValue(session.Date, out List<Guid>? dailySessionIds)
            && dailySessionIds.Count >= _maxDailySessions)
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

        if (dailySessionIds is null)
        {
            _sessionIdsByDate[session.Date] = [session.Id];
        }
        else
        {
            dailySessionIds.Add(session.Id);
        }

        return Result.Success;
    }

    public bool HasSession(Guid sessionId) =>
        _sessionIdsByDate.Values.Any(sessionIds => sessionIds.Contains(sessionId));
}
