using DomeGym.Section4.Domain.Common;
using DomeGym.Section4.Domain.RoomAggregate;
using ErrorOr;

namespace DomeGym.Section4.Domain.GymAggregate;

public class Gym(int maxRooms, Guid subscriptionId, Guid? id = null) : AggregateRoot(id ?? Guid.NewGuid())
{
    private readonly int _maxRooms = maxRooms;
    private readonly List<Guid> _roomIds = [];

    public Guid SubscriptionId { get; } = subscriptionId;

    public ErrorOr<Success> AddRoom(Room room)
    {
        if (_roomIds.Contains(room.Id))
        {
            return Error.Conflict(description: "Room already exists in gym");
        }

        if (_roomIds.Count >= _maxRooms)
        {
            return GymErrors.CannotHaveMoreRoomsThanSubscriptionAllows;
        }

        _roomIds.Add(room.Id);
        return Result.Success;
    }
}
