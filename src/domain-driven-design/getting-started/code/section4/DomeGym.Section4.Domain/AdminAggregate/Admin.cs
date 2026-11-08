using DomeGym.Section4.Domain.Common;

namespace DomeGym.Section4.Domain.AdminAggregate;

public class Admin(Guid userId, Guid subscriptionId, Guid? id = null) : AggregateRoot(id ?? Guid.NewGuid())
{
    public Guid UserId { get; } = userId;

    public Guid SubscriptionId { get; } = subscriptionId;
}
