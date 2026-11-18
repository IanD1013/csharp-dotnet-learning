using DomeGym.Section5.Application.Rooms.Commands.CreateRoom;
using DomeGym.Section5.Application.Rooms.Commands.DeleteRoom;
using DomeGym.Section5.Application.Rooms.Queries.GetRoom;
using DomeGym.Section5.Application.Rooms.Queries.ListRooms;
using DomeGym.Section5.Contracts.Rooms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DomeGym.Section5.Api.Controllers;

[Route("gyms/{gymId:guid}/rooms")]
public class RoomsController(ISender sender) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateRoom(
        CreateRoomRequest request,
        Guid gymId,
        CancellationToken cancellationToken)
    {
        var command = new CreateRoomCommand(gymId, request.Name);

        var createRoomResult = await sender.Send(command, cancellationToken);

        return createRoomResult.Match(
            room => CreatedAtAction(
                nameof(GetRoom),
                new { gymId, roomId = room.Id },
                new RoomResponse(room.Id, room.Name)),
            Problem);
    }

    [HttpGet]
    public async Task<IActionResult> ListRooms(Guid gymId, CancellationToken cancellationToken)
    {
        var query = new ListRoomsQuery(gymId);

        var listRoomsResult = await sender.Send(query, cancellationToken);

        return listRoomsResult.Match(
            rooms => Ok(rooms.ConvertAll(room => new RoomResponse(room.Id, room.Name))),
            Problem);
    }

    [HttpGet("{roomId:guid}")]
    public async Task<IActionResult> GetRoom(Guid gymId, Guid roomId, CancellationToken cancellationToken)
    {
        var query = new GetRoomQuery(gymId, roomId);

        var getRoomResult = await sender.Send(query, cancellationToken);

        return getRoomResult.Match(
            room => Ok(new RoomResponse(room.Id, room.Name)),
            Problem);
    }

    [HttpDelete("{roomId:guid}")]
    public async Task<IActionResult> DeleteRoom(Guid gymId, Guid roomId, CancellationToken cancellationToken)
    {
        var command = new DeleteRoomCommand(gymId, roomId);

        var deleteRoomResult = await sender.Send(command, cancellationToken);

        return deleteRoomResult.Match(_ => NoContent(), Problem);
    }
}
