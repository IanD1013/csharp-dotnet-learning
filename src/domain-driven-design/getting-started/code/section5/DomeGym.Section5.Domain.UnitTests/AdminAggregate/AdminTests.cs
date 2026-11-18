using DomeGym.Section5.Domain.AdminAggregate;
using DomeGym.Section5.Domain.AdminAggregate.Events;
using DomeGym.Section5.Domain.Common;
using DomeGym.Section5.Domain.SubscriptionAggregate;
using DomeGym.Section5.Domain.UnitTests.TestUtils.Admins;
using DomeGym.Section5.Domain.UnitTests.TestUtils.Subscriptions;
using ErrorOr;

namespace DomeGym.Section5.Domain.UnitTests.AdminAggregate;

public class AdminTests
{
    [Fact]
    public void SetSubscription_WhenAdminHasNoSubscription_ShouldRaiseSubscriptionSetEvent()
    {
        // Arrange
        Admin admin = AdminFactory.CreateAdmin();
        Subscription subscription = SubscriptionFactory.CreateSubscription(adminId: admin.Id);

        // Act
        ErrorOr<Success> setSubscriptionResult = admin.SetSubscription(subscription);

        // Assert
        Assert.False(setSubscriptionResult.IsError);
        Assert.Equal(subscription.Id, admin.SubscriptionId);

        IDomainEvent domainEvent = Assert.Single(admin.PopDomainEvents());
        SubscriptionSetEvent subscriptionSetEvent = Assert.IsType<SubscriptionSetEvent>(domainEvent);
        Assert.Same(admin, subscriptionSetEvent.Admin);
        Assert.Same(subscription, subscriptionSetEvent.Subscription);

        Assert.Empty(admin.PopDomainEvents());
    }

    [Fact]
    public void SetSubscription_WhenAdminAlreadyHasSubscription_ShouldFailWithoutRaisingEvent()
    {
        // Arrange
        Admin admin = AdminFactory.CreateAdmin(subscriptionId: Guid.NewGuid());
        Subscription subscription = SubscriptionFactory.CreateSubscription(adminId: admin.Id);

        // Act
        ErrorOr<Success> setSubscriptionResult = admin.SetSubscription(subscription);

        // Assert
        Assert.True(setSubscriptionResult.IsError);
        Assert.Equal(ErrorType.Conflict, setSubscriptionResult.FirstError.Type);
        Assert.Empty(admin.PopDomainEvents());
    }
}
