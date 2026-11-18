using DomeGym.Section5.Domain.SubscriptionAggregate;

namespace DomeGym.Section5.Application.Common.Interfaces;

public interface ISubscriptionsRepository
{
    Task AddSubscriptionAsync(Subscription subscription, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Subscription?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<List<Subscription>> ListAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(Subscription subscription, CancellationToken cancellationToken = default);
}
