using DomeGym.Section5.Domain.Common;
using DomeGym.Section5.Domain.RoomAggregate;
using DomeGym.Section5.Domain.TrainerAggregate;
using ErrorOr;

namespace DomeGym.Section5.Domain.GymAggregate;

public class Gym : AggregateRoot
{
    private readonly int _maxRooms;
    private readonly List<Guid> _roomIds = [];
    private readonly List<Guid> _trainerIds = [];

    public Gym(string name, int maxRooms, Guid subscriptionId, Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        Name = name;
        _maxRooms = maxRooms;
        SubscriptionId = subscriptionId;
    }

    // Used by EF Core when materializing the aggregate.
    private Gym()
    {
    }

    public string Name { get; } = null!;

    public Guid SubscriptionId { get; }

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

    public bool HasRoom(Guid roomId) => _roomIds.Contains(roomId);

    public ErrorOr<Success> RemoveRoom(Room room)
    {
        if (!_roomIds.Remove(room.Id))
        {
            return Error.NotFound(description: "Room not found");
        }

        return Result.Success;
    }

    public ErrorOr<Success> AddTrainer(Trainer trainer)
    {
        if (_trainerIds.Contains(trainer.Id))
        {
            return Error.Conflict(description: "Trainer already assigned to gym");
        }

        _trainerIds.Add(trainer.Id);
        return Result.Success;
    }

    public bool HasTrainer(Guid trainerId) => _trainerIds.Contains(trainerId);
}
