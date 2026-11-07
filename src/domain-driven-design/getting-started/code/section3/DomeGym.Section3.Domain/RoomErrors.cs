using ErrorOr;

namespace DomeGym.Section3.Domain;

public static class RoomErrors
{
    public static readonly Error CannotHaveMoreSessionsThanSubscriptionAllows = Error.Validation(
        code: "Room.CannotHaveMoreSessionsThanSubscriptionAllows",
        description: "A room cannot have more scheduled sessions than the subscription allows");

    public static readonly Error CannotHaveTwoOrMoreOverlappingSessions = Error.Validation(
        code: "Room.CannotHaveTwoOrMoreOverlappingSessions",
        description: "A room cannot have two or more overlapping sessions");
}
