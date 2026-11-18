using DomeGym.Section5.Application.Common.Interfaces;
using DomeGym.Section5.Domain.GymAggregate;
using DomeGym.Section5.Domain.SubscriptionAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Gyms.Queries.GetGym;

public class GetGymQueryHandler(ISubscriptionsRepository subscriptionsRepository, IGymsRepository gymsRepository)
    : IRequestHandler<GetGymQuery, ErrorOr<Gym>>
{
    public async Task<ErrorOr<Gym>> Handle(GetGymQuery query, CancellationToken cancellationToken)
    {
        Subscription? subscription = await subscriptionsRepository.GetByIdAsync(query.SubscriptionId, cancellationToken);

        if (subscription is null)
        {
            return Error.NotFound(description: "Subscription not found");
        }

        if (!subscription.HasGym(query.GymId))
        {
            return Error.NotFound(description: "Gym not found");
        }

        if (await gymsRepository.GetByIdAsync(query.GymId, cancellationToken) is not Gym gym)
        {
            return Error.NotFound(description: "Gym not found");
        }

        return gym;
    }
}
