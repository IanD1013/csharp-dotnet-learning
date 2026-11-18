using DomeGym.Section5.Domain.GymAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Gyms.Queries.ListGyms;

public record ListGymsQuery(Guid SubscriptionId) : IRequest<ErrorOr<List<Gym>>>;
