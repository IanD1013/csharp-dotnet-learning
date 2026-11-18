using DomeGym.Section5.Application.Profiles.Common;
using DomeGym.Section5.Domain.Profiles;
using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Profiles.Queries.GetProfile;

public record GetProfileQuery(Guid UserId, ProfileType ProfileType) : IRequest<ErrorOr<Profile>>;
