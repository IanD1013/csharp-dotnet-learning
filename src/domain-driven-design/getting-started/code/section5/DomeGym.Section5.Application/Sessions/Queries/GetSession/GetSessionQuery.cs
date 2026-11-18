using DomeGym.Section5.Domain.SessionAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Sessions.Queries.GetSession;

public record GetSessionQuery(Guid RoomId, Guid SessionId) : IRequest<ErrorOr<Session>>;
