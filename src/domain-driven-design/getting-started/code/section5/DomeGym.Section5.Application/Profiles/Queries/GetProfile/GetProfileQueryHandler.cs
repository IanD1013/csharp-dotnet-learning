using DomeGym.Section5.Application.Common.Interfaces;
using DomeGym.Section5.Application.Profiles.Common;
using DomeGym.Section5.Domain.Profiles;
using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Profiles.Queries.GetProfile;

public class GetProfileQueryHandler(
    IAdminsRepository adminsRepository,
    ITrainersRepository trainersRepository,
    IParticipantsRepository participantsRepository)
    : IRequestHandler<GetProfileQuery, ErrorOr<Profile>>
{
    public async Task<ErrorOr<Profile>> Handle(GetProfileQuery query, CancellationToken cancellationToken)
    {
        Profile? profile = query.ProfileType.Name switch
        {
            nameof(ProfileType.Admin) => await adminsRepository.GetProfileByUserIdAsync(query.UserId, cancellationToken),
            nameof(ProfileType.Trainer) => await trainersRepository.GetProfileByUserIdAsync(query.UserId, cancellationToken),
            nameof(ProfileType.Participant) => await participantsRepository.GetProfileByUserIdAsync(query.UserId, cancellationToken),
            _ => throw new InvalidOperationException($"Unknown profile type '{query.ProfileType.Name}'."),
        };

        if (profile is null)
        {
            return Error.NotFound(description: "Profile not found");
        }

        return profile;
    }
}
