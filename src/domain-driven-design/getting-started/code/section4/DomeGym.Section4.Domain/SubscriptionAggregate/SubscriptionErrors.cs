using ErrorOr;

namespace DomeGym.Section4.Domain.SubscriptionAggregate;

public static class SubscriptionErrors
{
    public static readonly Error CannotHaveMoreGymsThanSubscriptionAllows = Error.Validation(
        code: "Subscription.CannotHaveMoreGymsThanSubscriptionAllows",
        description: "A subscription cannot have more gyms than the subscription allows");
}
