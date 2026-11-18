using DomeGym.Section5.Application.Common.Interfaces;
using DomeGym.Section5.Domain.GymAggregate;
using Microsoft.EntityFrameworkCore;

namespace DomeGym.Section5.Infrastructure.Persistence.Repositories;

public class GymsRepository(DomeGymDbContext dbContext) : IGymsRepository
{
    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default) =>
        dbContext.Gyms.AsNoTracking().AnyAsync(gym => gym.Id == id, cancellationToken);

    public Task<Gym?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        dbContext.Gyms.FirstOrDefaultAsync(gym => gym.Id == id, cancellationToken);

    public Task<List<Gym>> ListSubscriptionGymsAsync(Guid subscriptionId, CancellationToken cancellationToken = default) =>
        dbContext.Gyms
            .AsNoTracking()
            .Where(gym => gym.SubscriptionId == subscriptionId)
            .ToListAsync(cancellationToken);

    public async Task UpdateAsync(Gym gym, CancellationToken cancellationToken = default)
    {
        dbContext.Gyms.Update(gym);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
