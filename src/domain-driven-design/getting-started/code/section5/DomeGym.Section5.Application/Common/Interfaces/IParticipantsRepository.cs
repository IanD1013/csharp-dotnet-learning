using DomeGym.Section5.Application.Profiles.Common;
using DomeGym.Section5.Domain.ParticipantAggregate;

namespace DomeGym.Section5.Application.Common.Interfaces;

public interface IParticipantsRepository
{
    Task AddParticipantAsync(Participant participant, CancellationToken cancellationToken = default);

    Task<Participant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Profile?> GetProfileByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}
