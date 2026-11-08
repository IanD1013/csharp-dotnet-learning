using DomeGym.Section4.Domain.GymAggregate;

namespace DomeGym.Section4.Domain.UnitTests.TestUtils.Gyms;

public static class GymFactory
{
    public static Gym CreateGym(
        int maxRooms = Constants.Subscription.MaxRoomsFreeTier,
        Guid? id = null)
    {
        return new Gym(
            maxRooms,
            subscriptionId: Constants.Subscription.Id,
            id: id ?? Constants.Gym.Id);
    }
}
