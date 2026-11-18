using ErrorOr;

namespace DomeGym.Section5.Domain.SessionAggregate;

public static class SessionErrors
{
    public static readonly Error CannotHaveMoreReservationsThanParticipants = Error.Validation(
        code: "Session.CannotHaveMoreReservationsThanParticipants",
        description: "Cannot have more reservations than participants");

    public static readonly Error CannotCancelReservationTooCloseToSession = Error.Validation(
        code: "Session.CannotCancelReservationTooCloseToSession",
        description: "Cannot cancel reservation too close to session start time");

    public static readonly Error CannotCancelPastSession = Error.Validation(
        code: "Session.CannotCancelPastSession",
        description: "A participant cannot cancel a reservation for a session that has completed");
}
