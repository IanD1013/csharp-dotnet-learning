using DomeGym.Section5.Application.Common.Interfaces;
using DomeGym.Section5.Domain.SubscriptionAggregate;
using Microsoft.EntityFrameworkCore;

namespace DomeGym.Section5.Infrastructure.Persistence.Repositories;

public class SubscriptionsRepository(DomeGymDbContext dbContext) : ISubscriptionsRepository
{
    public async Task AddSubscriptionAsync(Subscription subscription, CancellationToken cancellationToken = default)
    {
        await dbContext.Subscriptions.AddAsync(subscription, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default) =>
        dbContext.Subscriptions.AsNoTracking().AnyAsync(subscription => subscription.Id == id, cancellationToken);

    public Task<Subscription?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        dbContext.Subscriptions.FirstOrDefaultAsync(subscription => subscription.Id == id, cancellationToken);

    public Task<List<Subscription>> ListAsync(CancellationToken cancellationToken = default) =>
        dbContext.Subscriptions.AsNoTracking().ToListAsync(cancellationToken);

    public async Task UpdateAsync(Subscription subscription, CancellationToken cancellationToken = default)
    {
        dbContext.Subscriptions.Update(subscription);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
