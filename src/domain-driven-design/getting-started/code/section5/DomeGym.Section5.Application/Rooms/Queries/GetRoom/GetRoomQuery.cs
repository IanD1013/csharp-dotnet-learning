using DomeGym.Section5.Domain.RoomAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Rooms.Queries.GetRoom;

public record GetRoomQuery(Guid GymId, Guid RoomId) : IRequest<ErrorOr<Room>>;
