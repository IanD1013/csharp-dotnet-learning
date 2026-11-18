using DomeGym.Section5.Application.Profiles.Commands.CreateProfile;
using DomeGym.Section5.Application.Profiles.Queries.GetProfile;
using DomeGym.Section5.Application.Profiles.Queries.ListProfiles;
using DomeGym.Section5.Contracts.Profiles;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DomainProfileType = DomeGym.Section5.Domain.Profiles.ProfileType;

namespace DomeGym.Section5.Api.Controllers;

[Route("users/{userId:guid}/profiles")]
public class ProfilesController(ISender sender) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateProfile(
        CreateProfileRequest request,
        Guid userId,
        CancellationToken cancellationToken)
    {
        var command = new CreateProfileCommand(ToDomain(request.ProfileType), userId);

        var createProfileResult = await sender.Send(command, cancellationToken);

        return createProfileResult.Match(
            id => CreatedAtAction(
                nameof(GetProfile),
                new { userId, profileType = request.ProfileType },
                new ProfileResponse(id, request.ProfileType)),
            Problem);
    }

    [HttpGet]
    public async Task<IActionResult> ListProfiles(Guid userId, CancellationToken cancellationToken)
    {
        var query = new ListProfilesQuery(userId);

        var listProfilesResult = await sender.Send(query, cancellationToken);

        return listProfilesResult.Match(
            profiles => Ok(profiles.ConvertAll(profile => new ProfileResponse(profile.Id, ToDto(profile.ProfileType)))),
            Problem);
    }

    [HttpGet("{profileType}")]
    public async Task<IActionResult> GetProfile(
        Guid userId,
        ProfileType profileType,
        CancellationToken cancellationToken)
    {
        var query = new GetProfileQuery(userId, ToDomain(profileType));

        var getProfileResult = await sender.Send(query, cancellationToken);

        return getProfileResult.Match(
            profile => Ok(new ProfileResponse(profile.Id, ToDto(profile.ProfileType))),
            Problem);
    }

    private static DomainProfileType ToDomain(ProfileType profileType) => profileType switch
    {
        ProfileType.Admin => DomainProfileType.Admin,
        ProfileType.Trainer => DomainProfileType.Trainer,
        ProfileType.Participant => DomainProfileType.Participant,
        _ => throw new ArgumentOutOfRangeException(nameof(profileType), profileType, "Unknown profile type."),
    };

    private static ProfileType ToDto(DomainProfileType profileType) => profileType.Name switch
    {
        nameof(DomainProfileType.Admin) => ProfileType.Admin,
        nameof(DomainProfileType.Trainer) => ProfileType.Trainer,
        nameof(DomainProfileType.Participant) => ProfileType.Participant,
        _ => throw new ArgumentOutOfRangeException(nameof(profileType), profileType.Name, "Unknown profile type."),
    };
}
