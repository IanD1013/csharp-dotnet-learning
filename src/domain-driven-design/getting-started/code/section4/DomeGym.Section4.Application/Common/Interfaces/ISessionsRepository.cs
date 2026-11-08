using DomeGym.Section4.Domain.SessionAggregate;

namespace DomeGym.Section4.Application.Common.Interfaces;

public interface ISessionsRepository
{
    Task AddSessionAsync(Session session, CancellationToken cancellationToken = default);

    Task UpdateSessionAsync(Session session, CancellationToken cancellationToken = default);
}
