using DomeGym.Section5.Application.Common.Interfaces;
using DomeGym.Section5.Domain.GymAggregate;
using DomeGym.Section5.Domain.RoomAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Rooms.Commands.DeleteRoom;

public class DeleteRoomCommandHandler(IGymsRepository gymsRepository, IRoomsRepository roomsRepository)
    : IRequestHandler<DeleteRoomCommand, ErrorOr<Deleted>>
{
    public async Task<ErrorOr<Deleted>> Handle(DeleteRoomCommand command, CancellationToken cancellationToken)
    {
        Gym? gym = await gymsRepository.GetByIdAsync(command.GymId, cancellationToken);

        if (gym is null)
        {
            return Error.NotFound(description: "Gym not found");
        }

        if (!gym.HasRoom(command.RoomId))
        {
            return Error.NotFound(description: "Room not found");
        }

        Room? room = await roomsRepository.GetByIdAsync(command.RoomId, cancellationToken);

        if (room is null)
        {
            return Error.NotFound(description: "Room not found");
        }

        ErrorOr<Success> removeRoomResult = gym.RemoveRoom(room);

        if (removeRoomResult.IsError)
        {
            return removeRoomResult.Errors;
        }

        await gymsRepository.UpdateAsync(gym, cancellationToken);

        return Result.Deleted;
    }
}
