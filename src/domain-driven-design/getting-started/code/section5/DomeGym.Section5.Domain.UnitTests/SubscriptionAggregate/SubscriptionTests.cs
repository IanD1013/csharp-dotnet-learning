using DomeGym.Section5.Domain.SubscriptionAggregate;
using DomeGym.Section5.Domain.UnitTests.TestUtils.Gyms;
using DomeGym.Section5.Domain.UnitTests.TestUtils.Subscriptions;
using ErrorOr;

namespace DomeGym.Section5.Domain.UnitTests.SubscriptionAggregate;

public class SubscriptionTests
{
    [Fact]
    public void AddGym_WhenMoreThanSubscriptionAllows_ShouldFail()
    {
        // Arrange
        Subscription subscription = SubscriptionFactory.CreateSubscription();

        var gyms = Enumerable.Range(0, subscription.GetMaxGyms() + 1)
            .Select(_ => GymFactory.CreateGym(id: Guid.NewGuid()))
            .ToList();

        // Act
        List<ErrorOr<Success>> addGymResults = gyms.ConvertAll(subscription.AddGym);

        // Assert
        Assert.All(addGymResults[..^1], result => Assert.False(result.IsError));

        ErrorOr<Success> lastAddGymResult = addGymResults[^1];
        Assert.True(lastAddGymResult.IsError);
        Assert.Equal(SubscriptionErrors.CannotHaveMoreGymsThanSubscriptionAllows, lastAddGymResult.FirstError);
    }
}
