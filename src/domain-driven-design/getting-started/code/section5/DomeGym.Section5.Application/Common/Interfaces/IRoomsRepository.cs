using DomeGym.Section5.Domain.RoomAggregate;

namespace DomeGym.Section5.Application.Common.Interfaces;

public interface IRoomsRepository
{
    Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<List<Room>> ListByGymIdAsync(Guid gymId, CancellationToken cancellationToken = default);

    Task UpdateAsync(Room room, CancellationToken cancellationToken = default);
}
