using DomeGym.Section5.Application.Common.Interfaces;
using DomeGym.Section5.Domain.AdminAggregate;
using DomeGym.Section5.Domain.SubscriptionAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Subscriptions.Commands.CreateSubscription;

public class CreateSubscriptionCommandHandler(IAdminsRepository adminsRepository)
    : IRequestHandler<CreateSubscriptionCommand, ErrorOr<Subscription>>
{
    public async Task<ErrorOr<Subscription>> Handle(CreateSubscriptionCommand command, CancellationToken cancellationToken)
    {
        Admin? admin = await adminsRepository.GetByIdAsync(command.AdminId, cancellationToken);

        if (admin is null)
        {
            return Error.NotFound(description: "Admin not found");
        }

        var subscription = new Subscription(command.SubscriptionType, command.AdminId);

        // Raises SubscriptionSetEvent; SubscriptionSetEventHandler persists the subscription
        // once the response has been sent (eventual consistency).
        ErrorOr<Success> setSubscriptionResult = admin.SetSubscription(subscription);

        if (setSubscriptionResult.IsError)
        {
            return setSubscriptionResult.Errors;
        }

        await adminsRepository.UpdateAsync(admin, cancellationToken);

        return subscription;
    }
}
