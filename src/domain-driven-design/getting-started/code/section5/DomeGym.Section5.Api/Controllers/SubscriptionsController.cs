using DomeGym.Section5.Application.Subscriptions.Commands.CreateSubscription;
using DomeGym.Section5.Application.Subscriptions.Queries.ListSubscriptions;
using DomeGym.Section5.Contracts.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DomainSubscriptionType = DomeGym.Section5.Domain.SubscriptionAggregate.SubscriptionType;

namespace DomeGym.Section5.Api.Controllers;

[Route("subscriptions")]
public class SubscriptionsController(ISender sender) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateSubscription(
        CreateSubscriptionRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateSubscriptionCommand(ToDomain(request.SubscriptionType), request.AdminId);

        var createSubscriptionResult = await sender.Send(command, cancellationToken);

        return createSubscriptionResult.Match(
            subscription => Ok(new SubscriptionResponse(subscription.Id, ToDto(subscription.SubscriptionType))),
            Problem);
    }

    [HttpGet]
    public async Task<IActionResult> ListSubscriptions(CancellationToken cancellationToken)
    {
        var query = new ListSubscriptionsQuery();

        var listSubscriptionsResult = await sender.Send(query, cancellationToken);

        return listSubscriptionsResult.Match(
            subscriptions => Ok(subscriptions.ConvertAll(subscription =>
                new SubscriptionResponse(subscription.Id, ToDto(subscription.SubscriptionType)))),
            Problem);
    }

    private static DomainSubscriptionType ToDomain(SubscriptionType subscriptionType) => subscriptionType switch
    {
        SubscriptionType.Free => DomainSubscriptionType.Free,
        SubscriptionType.Starter => DomainSubscriptionType.Starter,
        SubscriptionType.Pro => DomainSubscriptionType.Pro,
        _ => throw new ArgumentOutOfRangeException(nameof(subscriptionType), subscriptionType, "Unknown subscription type."),
    };

    private static SubscriptionType ToDto(DomainSubscriptionType subscriptionType) => subscriptionType.Name switch
    {
        nameof(DomainSubscriptionType.Free) => SubscriptionType.Free,
        nameof(DomainSubscriptionType.Starter) => SubscriptionType.Starter,
        nameof(DomainSubscriptionType.Pro) => SubscriptionType.Pro,
        _ => throw new ArgumentOutOfRangeException(nameof(subscriptionType), subscriptionType.Name, "Unknown subscription type."),
    };
}
