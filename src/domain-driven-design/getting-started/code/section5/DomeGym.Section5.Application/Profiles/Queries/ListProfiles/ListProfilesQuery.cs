using DomeGym.Section5.Application.Profiles.Common;
using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Profiles.Queries.ListProfiles;

public record ListProfilesQuery(Guid UserId) : IRequest<ErrorOr<List<Profile>>>;
