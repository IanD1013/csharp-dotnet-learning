using DomeGym.Section5.Domain.AdminAggregate.Events;
using DomeGym.Section5.Domain.Common;
using DomeGym.Section5.Domain.SubscriptionAggregate;
using ErrorOr;

namespace DomeGym.Section5.Domain.AdminAggregate;

public class Admin : AggregateRoot
{
    public Admin(Guid userId, Guid? subscriptionId = null, Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        UserId = userId;
        SubscriptionId = subscriptionId;
    }

    // Used by EF Core when materializing the aggregate.
    private Admin()
    {
    }

    public Guid UserId { get; }

    public Guid? SubscriptionId { get; private set; }

    public ErrorOr<Success> SetSubscription(Subscription subscription)
    {
        if (SubscriptionId.HasValue)
        {
            return Error.Conflict(description: "Admin already has an active subscription");
        }

        SubscriptionId = subscription.Id;

        RaiseDomainEvent(new SubscriptionSetEvent(this, subscription));

        return Result.Success;
    }
}
