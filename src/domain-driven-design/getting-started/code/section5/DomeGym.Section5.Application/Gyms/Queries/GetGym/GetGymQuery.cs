using DomeGym.Section5.Domain.GymAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Gyms.Queries.GetGym;

public record GetGymQuery(Guid SubscriptionId, Guid GymId) : IRequest<ErrorOr<Gym>>;
