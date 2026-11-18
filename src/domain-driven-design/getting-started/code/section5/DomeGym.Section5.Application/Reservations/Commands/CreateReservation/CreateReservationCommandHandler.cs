using DomeGym.Section5.Application.Common.Interfaces;
using DomeGym.Section5.Domain.ParticipantAggregate;
using DomeGym.Section5.Domain.SessionAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Reservations.Commands.CreateReservation;

public class CreateReservationCommandHandler(
    ISessionsRepository sessionsRepository,
    IParticipantsRepository participantsRepository)
    : IRequestHandler<CreateReservationCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(CreateReservationCommand command, CancellationToken cancellationToken)
    {
        Session? session = await sessionsRepository.GetByIdAsync(command.SessionId, cancellationToken);

        if (session is null)
        {
            return Error.NotFound(description: "Session not found");
        }

        if (session.HasReservationForParticipant(command.ParticipantId))
        {
            return Error.Conflict(description: "Participant already has reservation");
        }

        Participant? participant = await participantsRepository.GetByIdAsync(command.ParticipantId, cancellationToken);

        if (participant is null)
        {
            return Error.NotFound(description: "Participant not found");
        }

        if (participant.HasReservationForSession(session.Id))
        {
            return Error.Unexpected(description: "Participant not expected to have reservation to session");
        }

        if (!participant.IsTimeSlotFree(session.Date, session.Time))
        {
            return Error.Conflict(description: "Participant's calendar is not free for the entire session duration");
        }

        ErrorOr<Success> reserveSpotResult = session.ReserveSpot(participant);

        if (reserveSpotResult.IsError)
        {
            return reserveSpotResult.Errors;
        }

        await sessionsRepository.UpdateAsync(session, cancellationToken);

        return Result.Success;
    }
}
