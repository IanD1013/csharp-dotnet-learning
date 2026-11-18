using DomeGym.Section5.Application.Profiles.Common;
using DomeGym.Section5.Domain.TrainerAggregate;

namespace DomeGym.Section5.Application.Common.Interfaces;

public interface ITrainersRepository
{
    Task AddTrainerAsync(Trainer trainer, CancellationToken cancellationToken = default);

    Task<Trainer?> GetByIdAsync(Guid trainerId, CancellationToken cancellationToken = default);

    Task<Profile?> GetProfileByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}
