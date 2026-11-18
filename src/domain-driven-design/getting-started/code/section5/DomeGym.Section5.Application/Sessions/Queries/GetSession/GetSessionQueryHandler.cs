using DomeGym.Section5.Application.Common.Interfaces;
using DomeGym.Section5.Domain.RoomAggregate;
using DomeGym.Section5.Domain.SessionAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Sessions.Queries.GetSession;

public class GetSessionQueryHandler(IRoomsRepository roomsRepository, ISessionsRepository sessionsRepository)
    : IRequestHandler<GetSessionQuery, ErrorOr<Session>>
{
    public async Task<ErrorOr<Session>> Handle(GetSessionQuery query, CancellationToken cancellationToken)
    {
        Room? room = await roomsRepository.GetByIdAsync(query.RoomId, cancellationToken);

        if (room is null)
        {
            return Error.NotFound(description: "Room not found");
        }

        if (!room.HasSession(query.SessionId))
        {
            return Error.NotFound(description: "Session not found");
        }

        if (await sessionsRepository.GetByIdAsync(query.SessionId, cancellationToken) is not Session session)
        {
            return Error.NotFound(description: "Session not found");
        }

        return session;
    }
}
