using DomeGym.Section5.Application.Common.Interfaces;
using DomeGym.Section5.Domain.Common.Interfaces;
using DomeGym.Section5.Domain.ParticipantAggregate;
using DomeGym.Section5.Domain.SessionAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Participants.Commands.CancelReservation;

public class CancelReservationCommandHandler(
    ISessionsRepository sessionsRepository,
    IParticipantsRepository participantsRepository,
    IDateTimeProvider dateTimeProvider)
    : IRequestHandler<CancelReservationCommand, ErrorOr<Deleted>>
{
    public async Task<ErrorOr<Deleted>> Handle(CancelReservationCommand command, CancellationToken cancellationToken)
    {
        Session? session = await sessionsRepository.GetByIdAsync(command.SessionId, cancellationToken);

        if (session is null)
        {
            return Error.NotFound(description: "Session not found");
        }

        if (!session.HasReservationForParticipant(command.ParticipantId))
        {
            return Error.NotFound(description: "User doesn't have a reservation for the given session");
        }

        Participant? participant = await participantsRepository.GetByIdAsync(command.ParticipantId, cancellationToken);

        if (participant is null)
        {
            return Error.NotFound(description: "Participant not found");
        }

        if (!participant.HasReservationForSession(session.Id))
        {
            return Error.Unexpected(description: "Participant expected to have reservation to session");
        }

        ErrorOr<Success> cancelReservationResult = session.CancelReservation(participant, dateTimeProvider);

        if (cancelReservationResult.IsError)
        {
            return cancelReservationResult.Errors;
        }

        await sessionsRepository.UpdateAsync(session, cancellationToken);

        return Result.Deleted;
    }
}
