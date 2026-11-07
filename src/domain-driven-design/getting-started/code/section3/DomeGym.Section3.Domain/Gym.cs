using ErrorOr;

namespace DomeGym.Section3.Domain;

public class Gym(int maxRooms, Guid subscriptionId, Guid? id = null)
{
    private readonly int _maxRooms = maxRooms;
    private readonly List<Guid> _roomIds = [];

    public Guid Id { get; } = id ?? Guid.NewGuid();

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
