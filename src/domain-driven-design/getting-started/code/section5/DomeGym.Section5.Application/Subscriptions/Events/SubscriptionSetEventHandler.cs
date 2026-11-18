using DomeGym.Section5.Application.Common.Interfaces;
using DomeGym.Section5.Domain.AdminAggregate.Events;
using MediatR;

namespace DomeGym.Section5.Application.Subscriptions.Events;

public class SubscriptionSetEventHandler(ISubscriptionsRepository subscriptionsRepository)
    : INotificationHandler<SubscriptionSetEvent>
{
    public async Task Handle(SubscriptionSetEvent notification, CancellationToken cancellationToken) =>
        await subscriptionsRepository.AddSubscriptionAsync(notification.Subscription, cancellationToken);
}
