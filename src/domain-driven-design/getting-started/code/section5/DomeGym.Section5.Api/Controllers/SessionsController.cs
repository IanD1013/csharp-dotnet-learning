using DomeGym.Section5.Api.Controllers.Common;
using DomeGym.Section5.Application.Sessions.Commands.CreateSession;
using DomeGym.Section5.Application.Sessions.Queries.GetSession;
using DomeGym.Section5.Contracts.Sessions;
using DomeGym.Section5.Domain.SessionAggregate;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DomeGym.Section5.Api.Controllers;

[Route("rooms/{roomId:guid}/sessions")]
public class SessionsController(ISender sender) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateSession(
        CreateSessionRequest request,
        Guid roomId,
        CancellationToken cancellationToken)
    {
        ErrorOr<List<SessionCategory>> categoriesToDomainResult = SessionCategoryUtils.ToDomain(request.Categories);

        if (categoriesToDomainResult.IsError)
        {
            return Problem(categoriesToDomainResult.Errors);
        }

        var command = new CreateSessionCommand(
            roomId,
            request.Name,
            request.Description,
            request.MaxParticipants,
            request.StartDateTime,
            request.EndDateTime,
            request.TrainerId,
            categoriesToDomainResult.Value);

        var createSessionResult = await sender.Send(command, cancellationToken);

        return createSessionResult.Match(
            session => CreatedAtAction(
                nameof(GetSession),
                new { roomId, sessionId = session.Id },
                session.ToResponse()),
            Problem);
    }

    [HttpGet("{sessionId:guid}")]
    public async Task<IActionResult> GetSession(Guid roomId, Guid sessionId, CancellationToken cancellationToken)
    {
        var query = new GetSessionQuery(roomId, sessionId);

        var getSessionResult = await sender.Send(query, cancellationToken);

        return getSessionResult.Match(
            session => Ok(session.ToResponse()),
            Problem);
    }
}
