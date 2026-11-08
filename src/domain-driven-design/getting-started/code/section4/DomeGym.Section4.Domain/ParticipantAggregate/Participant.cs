using DomeGym.Section4.Domain.Common;
using DomeGym.Section4.Domain.Common.Entities;
using DomeGym.Section4.Domain.SessionAggregate;
using ErrorOr;

namespace DomeGym.Section4.Domain.ParticipantAggregate;

public class Participant(Guid userId, Guid? id = null) : AggregateRoot(id ?? Guid.NewGuid())
{
    private readonly Schedule _schedule = Schedule.Empty();
    private readonly List<Guid> _sessionIds = [];

    public Guid UserId { get; } = userId;

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
}
