using DomeGym.Section5.Application.Common.Interfaces;
using DomeGym.Section5.Domain.ParticipantAggregate;
using DomeGym.Section5.Domain.SessionAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Participants.Queries.ListParticipantSessions;

public class ListParticipantSessionsQueryHandler(
    IParticipantsRepository participantsRepository,
    ISessionsRepository sessionsRepository)
    : IRequestHandler<ListParticipantSessionsQuery, ErrorOr<List<Session>>>
{
    public async Task<ErrorOr<List<Session>>> Handle(ListParticipantSessionsQuery query, CancellationToken cancellationToken)
    {
        Participant? participant = await participantsRepository.GetByIdAsync(query.ParticipantId, cancellationToken);

        if (participant is null)
        {
            return Error.NotFound(description: "Participant not found");
        }

        return await sessionsRepository.ListByIdsAsync(
            participant.SessionIds,
            query.StartDateTime,
            query.EndDateTime,
            cancellationToken: cancellationToken);
    }
}
