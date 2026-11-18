using DomeGym.Section5.Application.Common.Interfaces;
using DomeGym.Section5.Domain.GymAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Gyms.Queries.ListGyms;

public class ListGymsQueryHandler(ISubscriptionsRepository subscriptionsRepository, IGymsRepository gymsRepository)
    : IRequestHandler<ListGymsQuery, ErrorOr<List<Gym>>>
{
    public async Task<ErrorOr<List<Gym>>> Handle(ListGymsQuery query, CancellationToken cancellationToken)
    {
        if (!await subscriptionsRepository.ExistsAsync(query.SubscriptionId, cancellationToken))
        {
            return Error.NotFound(description: "Subscription not found");
        }

        return await gymsRepository.ListSubscriptionGymsAsync(query.SubscriptionId, cancellationToken);
    }
}
