using ErrorOr;

namespace DomeGym.Section3.Domain;

public class Session(
    DateOnly date,
    TimeRange time,
    int maxParticipants,
    Guid trainerId,
    Guid? id = null)
{
    private const int MinHoursToCancelForFree = 24;

    private readonly int _maxParticipants = maxParticipants;
    private readonly List<Guid> _participantIds = [];

    public Guid Id { get; } = id ?? Guid.NewGuid();

    public DateOnly Date { get; } = date;

    public TimeRange Time { get; } = time;

    public Guid TrainerId { get; } = trainerId;

    public ErrorOr<Success> ReserveSpot(Participant participant)
    {
        if (_participantIds.Count >= _maxParticipants)
        {
            return SessionErrors.CannotHaveMoreReservationsThanParticipants;
        }

        if (_participantIds.Contains(participant.Id))
        {
            return Error.Conflict(description: "Participants cannot reserve twice to the same session");
        }

        _participantIds.Add(participant.Id);
        return Result.Success;
    }

    public ErrorOr<Success> CancelReservation(Participant participant, IDateTimeProvider dateTimeProvider)
    {
        if (IsTooCloseToSession(dateTimeProvider.UtcNow))
        {
            return SessionErrors.CannotCancelReservationTooCloseToSession;
        }

        if (!_participantIds.Remove(participant.Id))
        {
            return Error.NotFound(description: "Participant not found");
        }

        return Result.Success;
    }

    private bool IsTooCloseToSession(DateTime utcNow) =>
        (Date.ToDateTime(Time.Start) - utcNow).TotalHours < MinHoursToCancelForFree;
}
