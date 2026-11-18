using DomeGym.Section5.Application.Common.Interfaces;
using DomeGym.Section5.Domain.RoomAggregate;
using Microsoft.EntityFrameworkCore;

namespace DomeGym.Section5.Infrastructure.Persistence.Repositories;

public class RoomsRepository(DomeGymDbContext dbContext) : IRoomsRepository
{
    public Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        dbContext.Rooms.FirstOrDefaultAsync(room => room.Id == id, cancellationToken);

    public Task<List<Room>> ListByGymIdAsync(Guid gymId, CancellationToken cancellationToken = default) =>
        dbContext.Rooms
            .AsNoTracking()
            .Where(room => room.GymId == gymId)
            .ToListAsync(cancellationToken);

    public async Task UpdateAsync(Room room, CancellationToken cancellationToken = default)
    {
        dbContext.Rooms.Update(room);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
