using DomeGym.Section5.Application.Common.Interfaces;
using DomeGym.Section5.Domain.GymAggregate;
using DomeGym.Section5.Domain.RoomAggregate;
using DomeGym.Section5.Domain.SubscriptionAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Rooms.Commands.CreateRoom;

public class CreateRoomCommandHandler(ISubscriptionsRepository subscriptionsRepository, IGymsRepository gymsRepository)
    : IRequestHandler<CreateRoomCommand, ErrorOr<Room>>
{
    public async Task<ErrorOr<Room>> Handle(CreateRoomCommand command, CancellationToken cancellationToken)
    {
        Gym? gym = await gymsRepository.GetByIdAsync(command.GymId, cancellationToken);

        if (gym is null)
        {
            return Error.NotFound(description: "Gym not found");
        }

        Subscription? subscription = await subscriptionsRepository.GetByIdAsync(gym.SubscriptionId, cancellationToken);

        if (subscription is null)
        {
            return Error.Unexpected(description: "Subscription not found");
        }

        var room = new Room(
            name: command.RoomName,
            maxDailySessions: subscription.GetMaxDailySessions(),
            gymId: gym.Id);

        ErrorOr<Success> addRoomResult = gym.AddRoom(room);

        if (addRoomResult.IsError)
        {
            return addRoomResult.Errors;
        }

        await gymsRepository.UpdateAsync(gym, cancellationToken);

        return room;
    }
}
