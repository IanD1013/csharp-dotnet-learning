using DomeGym.Section5.Application.Common.Interfaces;
using DomeGym.Section5.Application.Profiles.Common;
using DomeGym.Section5.Domain.Profiles;
using DomeGym.Section5.Domain.TrainerAggregate;
using Microsoft.EntityFrameworkCore;

namespace DomeGym.Section5.Infrastructure.Persistence.Repositories;

public class TrainersRepository(DomeGymDbContext dbContext) : ITrainersRepository
{
    public async Task AddTrainerAsync(Trainer trainer, CancellationToken cancellationToken = default)
    {
        await dbContext.Trainers.AddAsync(trainer, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<Trainer?> GetByIdAsync(Guid trainerId, CancellationToken cancellationToken = default) =>
        dbContext.Trainers.FirstOrDefaultAsync(trainer => trainer.Id == trainerId, cancellationToken);

    public async Task<Profile?> GetProfileByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        Trainer? trainer = await dbContext.Trainers
            .AsNoTracking()
            .FirstOrDefaultAsync(trainer => trainer.UserId == userId, cancellationToken);

        return trainer is null ? null : new Profile(trainer.Id, ProfileType.Trainer);
    }
}
