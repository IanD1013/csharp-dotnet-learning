using DomeGym.Section5.Domain.RoomAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Rooms.Commands.CreateRoom;

public record CreateRoomCommand(Guid GymId, string RoomName) : IRequest<ErrorOr<Room>>;
