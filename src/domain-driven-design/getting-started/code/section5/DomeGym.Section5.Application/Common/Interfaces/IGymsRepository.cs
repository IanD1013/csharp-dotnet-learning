using DomeGym.Section5.Domain.GymAggregate;

namespace DomeGym.Section5.Application.Common.Interfaces;

public interface IGymsRepository
{
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Gym?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<List<Gym>> ListSubscriptionGymsAsync(Guid subscriptionId, CancellationToken cancellationToken = default);

    Task UpdateAsync(Gym gym, CancellationToken cancellationToken = default);
}
