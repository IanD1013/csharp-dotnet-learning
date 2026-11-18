using DomeGym.Section5.Application.Common.Interfaces;
using DomeGym.Section5.Domain.RoomAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Rooms.Queries.ListRooms;

public class ListRoomsQueryHandler(IGymsRepository gymsRepository, IRoomsRepository roomsRepository)
    : IRequestHandler<ListRoomsQuery, ErrorOr<List<Room>>>
{
    public async Task<ErrorOr<List<Room>>> Handle(ListRoomsQuery query, CancellationToken cancellationToken)
    {
        if (!await gymsRepository.ExistsAsync(query.GymId, cancellationToken))
        {
            return Error.NotFound(description: "Gym not found");
        }

        return await roomsRepository.ListByGymIdAsync(query.GymId, cancellationToken);
    }
}
