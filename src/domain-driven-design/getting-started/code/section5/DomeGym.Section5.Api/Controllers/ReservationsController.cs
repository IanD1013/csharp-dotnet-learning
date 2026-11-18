using DomeGym.Section5.Application.Reservations.Commands.CreateReservation;
using DomeGym.Section5.Contracts.Reservations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DomeGym.Section5.Api.Controllers;

[Route("sessions/{sessionId:guid}/reservations")]
public class ReservationsController(ISender sender) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateReservation(
        CreateReservationRequest request,
        Guid sessionId,
        CancellationToken cancellationToken)
    {
        var command = new CreateReservationCommand(sessionId, request.ParticipantId);

        var createReservationResult = await sender.Send(command, cancellationToken);

        return createReservationResult.Match(_ => NoContent(), Problem);
    }
}
