using DomeGym.Section5.Application.Common.Interfaces;
using DomeGym.Section5.Domain.GymAggregate;
using DomeGym.Section5.Domain.SubscriptionAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Gyms.Commands.CreateGym;

public class CreateGymCommandHandler(ISubscriptionsRepository subscriptionsRepository)
    : IRequestHandler<CreateGymCommand, ErrorOr<Gym>>
{
    public async Task<ErrorOr<Gym>> Handle(CreateGymCommand command, CancellationToken cancellationToken)
    {
        Subscription? subscription = await subscriptionsRepository.GetByIdAsync(command.SubscriptionId, cancellationToken);

        if (subscription is null)
        {
            return Error.NotFound(description: "Subscription not found");
        }

        var gym = new Gym(
            name: command.Name,
            maxRooms: subscription.GetMaxRooms(),
            subscriptionId: subscription.Id);

        ErrorOr<Success> addGymResult = subscription.AddGym(gym);

        if (addGymResult.IsError)
        {
            return addGymResult.Errors;
        }

        await subscriptionsRepository.UpdateAsync(subscription, cancellationToken);

        return gym;
    }
}
