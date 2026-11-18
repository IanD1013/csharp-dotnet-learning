using DomeGym.Section5.Application.Common.Interfaces;
using DomeGym.Section5.Domain.GymAggregate;
using DomeGym.Section5.Domain.RoomAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Rooms.Queries.GetRoom;

public class GetRoomQueryHandler(IGymsRepository gymsRepository, IRoomsRepository roomsRepository)
    : IRequestHandler<GetRoomQuery, ErrorOr<Room>>
{
    public async Task<ErrorOr<Room>> Handle(GetRoomQuery query, CancellationToken cancellationToken)
    {
        Gym? gym = await gymsRepository.GetByIdAsync(query.GymId, cancellationToken);

        if (gym is null)
        {
            return Error.NotFound(description: "Gym not found");
        }

        if (!gym.HasRoom(query.RoomId))
        {
            return Error.NotFound(description: "Room not found");
        }

        if (await roomsRepository.GetByIdAsync(query.RoomId, cancellationToken) is not Room room)
        {
            return Error.NotFound(description: "Room not found");
        }

        return room;
    }
}
