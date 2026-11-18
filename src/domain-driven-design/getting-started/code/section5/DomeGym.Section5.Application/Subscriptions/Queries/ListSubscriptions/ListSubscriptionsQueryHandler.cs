using DomeGym.Section5.Application.Common.Interfaces;
using DomeGym.Section5.Domain.SubscriptionAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Subscriptions.Queries.ListSubscriptions;

public class ListSubscriptionsQueryHandler(ISubscriptionsRepository subscriptionsRepository)
    : IRequestHandler<ListSubscriptionsQuery, ErrorOr<List<Subscription>>>
{
    public async Task<ErrorOr<List<Subscription>>> Handle(ListSubscriptionsQuery query, CancellationToken cancellationToken) =>
        await subscriptionsRepository.ListAsync(cancellationToken);
}
