using ErrorOr;

namespace DomeGym.Section4.Domain.GymAggregate;

public static class GymErrors
{
    public static readonly Error CannotHaveMoreRoomsThanSubscriptionAllows = Error.Validation(
        code: "Gym.CannotHaveMoreRoomsThanSubscriptionAllows",
        description: "A gym cannot have more rooms than the subscription allows");
}
