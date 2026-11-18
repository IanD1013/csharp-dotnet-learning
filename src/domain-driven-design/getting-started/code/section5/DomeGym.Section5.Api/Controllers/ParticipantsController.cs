using DomeGym.Section5.Api.Controllers.Common;
using DomeGym.Section5.Application.Participants.Commands.CancelReservation;
using DomeGym.Section5.Application.Participants.Queries.ListParticipantSessions;
using DomeGym.Section5.Application.Reservations.Commands.CreateReservation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DomeGym.Section5.Api.Controllers;

[Route("participants/{participantId:guid}/sessions")]
public class ParticipantsController(ISender sender) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> ListParticipantSessions(
        Guid participantId,
        CancellationToken cancellationToken,
        DateTime? startDateTime = null,
        DateTime? endDateTime = null)
    {
        var query = new ListParticipantSessionsQuery(participantId, startDateTime, endDateTime);

        var listParticipantSessionsResult = await sender.Send(query, cancellationToken);

        return listParticipantSessionsResult.Match(
            sessions => Ok(sessions.ConvertAll(session => session.ToResponse())),
            Problem);
    }

    [HttpPost("{sessionId:guid}/reservation")]
    public async Task<IActionResult> CreateReservation(
        Guid participantId,
        Guid sessionId,
        CancellationToken cancellationToken)
    {
        var command = new CreateReservationCommand(sessionId, participantId);

        var createReservationResult = await sender.Send(command, cancellationToken);

        return createReservationResult.Match(_ => NoContent(), Problem);
    }

    [HttpDelete("{sessionId:guid}/reservation")]
    public async Task<IActionResult> CancelReservation(
        Guid participantId,
        Guid sessionId,
        CancellationToken cancellationToken)
    {
        var command = new CancelReservationCommand(participantId, sessionId);

        var cancelReservationResult = await sender.Send(command, cancellationToken);

        return cancelReservationResult.Match(_ => NoContent(), Problem);
    }
}
