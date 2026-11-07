namespace DomeGym.Section3.Domain.UnitTests.TestUtils.Subscriptions;

public static class SubscriptionFactory
{
    public static Subscription CreateSubscription(
        SubscriptionType? subscriptionType = null,
        Guid? adminId = null,
        Guid? id = null)
    {
        return new Subscription(
            subscriptionType: subscriptionType ?? Constants.Subscription.DefaultSubscriptionType,
            adminId: adminId ?? Constants.Admin.Id,
            id: id ?? Constants.Subscription.Id);
    }
}
