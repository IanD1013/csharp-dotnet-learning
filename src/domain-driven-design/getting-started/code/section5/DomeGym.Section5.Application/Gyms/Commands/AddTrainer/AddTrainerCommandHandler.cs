using DomeGym.Section5.Application.Common.Interfaces;
using DomeGym.Section5.Domain.GymAggregate;
using DomeGym.Section5.Domain.SubscriptionAggregate;
using DomeGym.Section5.Domain.TrainerAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Gyms.Commands.AddTrainer;

public class AddTrainerCommandHandler(
    ISubscriptionsRepository subscriptionsRepository,
    IGymsRepository gymsRepository,
    ITrainersRepository trainersRepository)
    : IRequestHandler<AddTrainerCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(AddTrainerCommand command, CancellationToken cancellationToken)
    {
        Subscription? subscription = await subscriptionsRepository.GetByIdAsync(command.SubscriptionId, cancellationToken);

        if (subscription is null)
        {
            return Error.NotFound(description: "Subscription not found");
        }

        if (!subscription.HasGym(command.GymId))
        {
            return Error.NotFound(description: "Gym not found");
        }

        Gym? gym = await gymsRepository.GetByIdAsync(command.GymId, cancellationToken);

        if (gym is null)
        {
            return Error.NotFound(description: "Gym not found");
        }

        Trainer? trainer = await trainersRepository.GetByIdAsync(command.TrainerId, cancellationToken);

        if (trainer is null)
        {
            return Error.NotFound(description: "Trainer not found");
        }

        ErrorOr<Success> addTrainerResult = gym.AddTrainer(trainer);

        if (addTrainerResult.IsError)
        {
            return addTrainerResult.Errors;
        }

        await gymsRepository.UpdateAsync(gym, cancellationToken);

        return Result.Success;
    }
}
