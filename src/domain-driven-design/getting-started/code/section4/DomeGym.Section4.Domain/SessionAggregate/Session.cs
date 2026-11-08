using DomeGym.Section4.Domain.Common;
using DomeGym.Section4.Domain.Common.Interfaces;
using DomeGym.Section4.Domain.Common.ValueObjects;
using DomeGym.Section4.Domain.ParticipantAggregate;
using ErrorOr;

namespace DomeGym.Section4.Domain.SessionAggregate;

public class Session(
    DateOnly date,
    TimeRange time,
    int maxParticipants,
    Guid trainerId,
    Guid? id = null) : AggregateRoot(id ?? Guid.NewGuid())
{
    private const int MinHoursToCancelForFree = 24;

    private readonly int _maxParticipants = maxParticipants;
    private readonly List<Reservation> _reservations = [];

    public DateOnly Date { get; } = date;

    public TimeRange Time { get; } = time;

    public Guid TrainerId { get; } = trainerId;

    public ErrorOr<Success> ReserveSpot(Participant participant)
    {
        if (_reservations.Count >= _maxParticipants)
        {
            return SessionErrors.CannotHaveMoreReservationsThanParticipants;
        }

        if (_reservations.Any(reservation => reservation.ParticipantId == participant.Id))
        {
            return Error.Conflict(description: "Participants cannot reserve twice to the same session");
        }

        _reservations.Add(new Reservation(participant.Id));
        return Result.Success;
    }

    public ErrorOr<Success> CancelReservation(Participant participant, IDateTimeProvider dateTimeProvider)
    {
        if (IsTooCloseToSession(dateTimeProvider.UtcNow))
        {
            return SessionErrors.CannotCancelReservationTooCloseToSession;
        }

        Reservation? reservation = _reservations.Find(reservation => reservation.ParticipantId == participant.Id);

        if (reservation is null)
        {
            return Error.NotFound(description: "Participant not found");
        }

        _reservations.Remove(reservation);
        return Result.Success;
    }

    private bool IsTooCloseToSession(DateTime utcNow) =>
        (Date.ToDateTime(Time.Start) - utcNow).TotalHours < MinHoursToCancelForFree;
}
