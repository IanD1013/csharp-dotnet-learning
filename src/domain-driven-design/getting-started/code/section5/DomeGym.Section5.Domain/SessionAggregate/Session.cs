using DomeGym.Section5.Domain.Common;
using DomeGym.Section5.Domain.Common.Interfaces;
using DomeGym.Section5.Domain.Common.ValueObjects;
using DomeGym.Section5.Domain.ParticipantAggregate;
using ErrorOr;

namespace DomeGym.Section5.Domain.SessionAggregate;

public class Session : AggregateRoot
{
    private const int MinHoursToCancelForFree = 24;

    private readonly List<Reservation> _reservations = [];
    private readonly List<SessionCategory> _categories = [];

    public Session(
        string name,
        string description,
        int maxParticipants,
        Guid roomId,
        Guid trainerId,
        DateOnly date,
        TimeRange time,
        IEnumerable<SessionCategory> categories,
        Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        Name = name;
        Description = description;
        MaxParticipants = maxParticipants;
        RoomId = roomId;
        TrainerId = trainerId;
        Date = date;
        Time = time;
        _categories = [.. categories];
    }

    // Used by EF Core when materializing the aggregate.
    private Session()
    {
    }

    public string Name { get; } = null!;

    public string Description { get; } = null!;

    public int MaxParticipants { get; }

    public int NumParticipants => _reservations.Count;

    public Guid RoomId { get; }

    public Guid TrainerId { get; }

    public DateOnly Date { get; }

    public TimeRange Time { get; } = null!;

    public IReadOnlyList<SessionCategory> Categories => _categories;

    public ErrorOr<Success> ReserveSpot(Participant participant)
    {
        if (_reservations.Count >= MaxParticipants)
        {
            return SessionErrors.CannotHaveMoreReservationsThanParticipants;
        }

        if (HasReservationForParticipant(participant.Id))
        {
            return Error.Conflict(description: "Participants cannot reserve twice to the same session");
        }

        _reservations.Add(new Reservation(participant.Id));
        return Result.Success;
    }

    public ErrorOr<Success> CancelReservation(Participant participant, IDateTimeProvider dateTimeProvider)
    {
        Reservation? reservation = _reservations.Find(reservation => reservation.ParticipantId == participant.Id);

        if (reservation is null)
        {
            return Error.NotFound(description: "Reservation not found");
        }

        if (IsPastSession(dateTimeProvider.UtcNow))
        {
            return SessionErrors.CannotCancelPastSession;
        }

        if (IsTooCloseToSession(dateTimeProvider.UtcNow))
        {
            return SessionErrors.CannotCancelReservationTooCloseToSession;
        }

        _reservations.Remove(reservation);
        return Result.Success;
    }

    public bool HasReservationForParticipant(Guid participantId) =>
        _reservations.Any(reservation => reservation.ParticipantId == participantId);

    private bool IsPastSession(DateTime utcNow) => Date.ToDateTime(Time.End) < utcNow;

    private bool IsTooCloseToSession(DateTime utcNow) =>
        (Date.ToDateTime(Time.Start) - utcNow).TotalHours < MinHoursToCancelForFree;
}
