using DomeGym.Section5.Domain.RoomAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Rooms.Queries.ListRooms;

public record ListRoomsQuery(Guid GymId) : IRequest<ErrorOr<List<Room>>>;
