using DomeGym.Section5.Domain.GymAggregate;
using DomeGym.Section5.Domain.RoomAggregate;
using DomeGym.Section5.Domain.UnitTests.TestUtils.Gyms;
using DomeGym.Section5.Domain.UnitTests.TestUtils.Rooms;
using ErrorOr;

namespace DomeGym.Section5.Domain.UnitTests.GymAggregate;

public class GymTests
{
    [Fact]
    public void AddRoom_WhenMoreThanSubscriptionAllows_ShouldFail()
    {
        // Arrange
        Gym gym = GymFactory.CreateGym(maxRooms: 1);
        Room room1 = RoomFactory.CreateRoom(id: Guid.NewGuid());
        Room room2 = RoomFactory.CreateRoom(id: Guid.NewGuid());

        // Act
        ErrorOr<Success> addRoom1Result = gym.AddRoom(room1);
        ErrorOr<Success> addRoom2Result = gym.AddRoom(room2);

        // Assert
        Assert.False(addRoom1Result.IsError);

        Assert.True(addRoom2Result.IsError);
        Assert.Equal(GymErrors.CannotHaveMoreRoomsThanSubscriptionAllows, addRoom2Result.FirstError);
    }
}
