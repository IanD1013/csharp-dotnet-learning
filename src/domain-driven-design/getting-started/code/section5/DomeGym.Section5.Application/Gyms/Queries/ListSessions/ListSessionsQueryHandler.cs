using DomeGym.Section5.Application.Common.Interfaces;
using DomeGym.Section5.Domain.SessionAggregate;
using DomeGym.Section5.Domain.SubscriptionAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Gyms.Queries.ListSessions;

public class ListSessionsQueryHandler(
    ISubscriptionsRepository subscriptionsRepository,
    IGymsRepository gymsRepository,
    ISessionsRepository sessionsRepository)
    : IRequestHandler<ListSessionsQuery, ErrorOr<List<Session>>>
{
    public async Task<ErrorOr<List<Session>>> Handle(ListSessionsQuery query, CancellationToken cancellationToken)
    {
        Subscription? subscription = await subscriptionsRepository.GetByIdAsync(query.SubscriptionId, cancellationToken);

        if (subscription is null)
        {
            return Error.NotFound(description: "Subscription not found");
        }

        if (!subscription.HasGym(query.GymId) || !await gymsRepository.ExistsAsync(query.GymId, cancellationToken))
        {
            return Error.NotFound(description: "Gym not found");
        }

        return await sessionsRepository.ListByGymIdAsync(
            query.GymId,
            query.StartDateTime,
            query.EndDateTime,
            query.Categories,
            cancellationToken);
    }
}
