using DomeGym.Section5.Domain.Profiles;
using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Profiles.Commands.CreateProfile;

public record CreateProfileCommand(ProfileType ProfileType, Guid UserId) : IRequest<ErrorOr<Guid>>;
