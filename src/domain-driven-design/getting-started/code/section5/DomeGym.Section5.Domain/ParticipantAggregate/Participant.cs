using DomeGym.Section5.Domain.Common;
using DomeGym.Section5.Domain.Common.Entities;
using DomeGym.Section5.Domain.Common.ValueObjects;
using DomeGym.Section5.Domain.SessionAggregate;
using ErrorOr;

namespace DomeGym.Section5.Domain.ParticipantAggregate;

public class Participant : AggregateRoot
{
    private readonly Schedule _schedule = Schedule.Empty();
    private readonly List<Guid> _sessionIds = [];

    public Participant(Guid userId, Schedule? schedule = null, Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        UserId = userId;
        _schedule = schedule ?? Schedule.Empty();
    }

    // Used by EF Core when materializing the aggregate.
    private Participant()
    {
    }

    public Guid UserId { get; }

    public IReadOnlyList<Guid> SessionIds => _sessionIds;

    public ErrorOr<Success> AddToSchedule(Session session)
    {
        if (_sessionIds.Contains(session.Id))
        {
            return Error.Conflict(description: "Session already exists in participant's schedule");
        }

        ErrorOr<Success> bookTimeSlotResult = _schedule.BookTimeSlot(session.Date, session.Time);

        if (bookTimeSlotResult.IsError)
        {
            return bookTimeSlotResult.FirstError.Type == ErrorType.Conflict
                ? ParticipantErrors.CannotHaveTwoOrMoreOverlappingSessions
                : bookTimeSlotResult.Errors;
        }

        _sessionIds.Add(session.Id);
        return Result.Success;
    }

    public bool HasReservationForSession(Guid sessionId) => _sessionIds.Contains(sessionId);

    public bool IsTimeSlotFree(DateOnly date, TimeRange time) => _schedule.CanBookTimeSlot(date, time);
}
