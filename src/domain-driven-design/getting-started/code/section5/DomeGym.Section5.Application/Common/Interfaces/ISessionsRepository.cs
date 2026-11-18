using DomeGym.Section5.Domain.SessionAggregate;

namespace DomeGym.Section5.Application.Common.Interfaces;

public interface ISessionsRepository
{
    Task<Session?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<List<Session>> ListByIdsAsync(
        IReadOnlyList<Guid> sessionIds,
        DateTime? startDateTime = null,
        DateTime? endDateTime = null,
        IReadOnlyList<SessionCategory>? categories = null,
        CancellationToken cancellationToken = default);

    Task<List<Session>> ListByGymIdAsync(
        Guid gymId,
        DateTime? startDateTime = null,
        DateTime? endDateTime = null,
        IReadOnlyList<SessionCategory>? categories = null,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(Session session, CancellationToken cancellationToken = default);
}
