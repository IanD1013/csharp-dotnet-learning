using DomeGym.Section5.Application.Common.Interfaces;
using DomeGym.Section5.Application.Profiles.Common;
using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Profiles.Queries.ListProfiles;

public class ListProfilesQueryHandler(
    IAdminsRepository adminsRepository,
    ITrainersRepository trainersRepository,
    IParticipantsRepository participantsRepository)
    : IRequestHandler<ListProfilesQuery, ErrorOr<List<Profile>>>
{
    public async Task<ErrorOr<List<Profile>>> Handle(ListProfilesQuery query, CancellationToken cancellationToken)
    {
        Profile?[] profiles =
        [
            await adminsRepository.GetProfileByUserIdAsync(query.UserId, cancellationToken),
            await trainersRepository.GetProfileByUserIdAsync(query.UserId, cancellationToken),
            await participantsRepository.GetProfileByUserIdAsync(query.UserId, cancellationToken),
        ];

        List<Profile> existingProfiles = [.. profiles.OfType<Profile>()];

        if (existingProfiles.Count == 0)
        {
            return Error.NotFound(description: "User not found");
        }

        return existingProfiles;
    }
}
