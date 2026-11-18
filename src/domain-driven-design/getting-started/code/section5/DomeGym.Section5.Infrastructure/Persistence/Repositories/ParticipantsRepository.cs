using DomeGym.Section5.Application.Common.Interfaces;
using DomeGym.Section5.Application.Profiles.Common;
using DomeGym.Section5.Domain.ParticipantAggregate;
using DomeGym.Section5.Domain.Profiles;
using Microsoft.EntityFrameworkCore;

namespace DomeGym.Section5.Infrastructure.Persistence.Repositories;

public class ParticipantsRepository(DomeGymDbContext dbContext) : IParticipantsRepository
{
    public async Task AddParticipantAsync(Participant participant, CancellationToken cancellationToken = default)
    {
        await dbContext.Participants.AddAsync(participant, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<Participant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        dbContext.Participants.FirstOrDefaultAsync(participant => participant.Id == id, cancellationToken);

    public async Task<Profile?> GetProfileByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        Participant? participant = await dbContext.Participants
            .AsNoTracking()
            .FirstOrDefaultAsync(participant => participant.UserId == userId, cancellationToken);

        return participant is null ? null : new Profile(participant.Id, ProfileType.Participant);
    }
}
