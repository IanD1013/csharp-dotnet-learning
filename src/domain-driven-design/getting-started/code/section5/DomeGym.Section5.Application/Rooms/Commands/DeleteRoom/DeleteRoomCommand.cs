using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Rooms.Commands.DeleteRoom;

public record DeleteRoomCommand(Guid GymId, Guid RoomId) : IRequest<ErrorOr<Deleted>>;
