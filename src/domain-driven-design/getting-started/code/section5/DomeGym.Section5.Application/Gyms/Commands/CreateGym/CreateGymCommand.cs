using DomeGym.Section5.Domain.GymAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Gyms.Commands.CreateGym;

public record CreateGymCommand(string Name, Guid SubscriptionId) : IRequest<ErrorOr<Gym>>;
