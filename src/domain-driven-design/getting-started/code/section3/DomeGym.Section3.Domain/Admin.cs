namespace DomeGym.Section3.Domain;

public class Admin(Guid userId, Guid subscriptionId, Guid? id = null)
{
    public Guid Id { get; } = id ?? Guid.NewGuid();

    public Guid UserId { get; } = userId;

    public Guid SubscriptionId { get; } = subscriptionId;
}
