using DomeGym.Section5.Domain.GymAggregate;

namespace DomeGym.Section5.Domain.UnitTests.TestUtils.Gyms;

public static class GymFactory
{
    public static Gym CreateGym(
        string name = Constants.Gym.Name,
        int maxRooms = Constants.Subscription.MaxRoomsFreeTier,
        Guid? id = null)
    {
        return new Gym(
            name,
            maxRooms,
            subscriptionId: Constants.Subscription.Id,
            id: id ?? Constants.Gym.Id);
    }
}
