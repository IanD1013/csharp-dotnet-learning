using DomeGym.Section5.Api.Controllers.Common;
using DomeGym.Section5.Application.Gyms.Commands.AddTrainer;
using DomeGym.Section5.Application.Gyms.Commands.CreateGym;
using DomeGym.Section5.Application.Gyms.Queries.GetGym;
using DomeGym.Section5.Application.Gyms.Queries.ListGyms;
using DomeGym.Section5.Application.Gyms.Queries.ListSessions;
using DomeGym.Section5.Contracts.Gyms;
using DomeGym.Section5.Domain.SessionAggregate;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DomeGym.Section5.Api.Controllers;

[Route("subscriptions/{subscriptionId:guid}/gyms")]
public class GymsController(ISender sender) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateGym(
        CreateGymRequest request,
        Guid subscriptionId,
        CancellationToken cancellationToken)
    {
        var command = new CreateGymCommand(request.Name, subscriptionId);

        var createGymResult = await sender.Send(command, cancellationToken);

        return createGymResult.Match(
            gym => CreatedAtAction(
                nameof(GetGym),
                new { subscriptionId, gymId = gym.Id },
                new GymResponse(gym.Id, gym.Name)),
            Problem);
    }

    [HttpGet]
    public async Task<IActionResult> ListGyms(Guid subscriptionId, CancellationToken cancellationToken)
    {
        var query = new ListGymsQuery(subscriptionId);

        var listGymsResult = await sender.Send(query, cancellationToken);

        return listGymsResult.Match(
            gyms => Ok(gyms.ConvertAll(gym => new GymResponse(gym.Id, gym.Name))),
            Problem);
    }

    [HttpGet("{gymId:guid}")]
    public async Task<IActionResult> GetGym(Guid subscriptionId, Guid gymId, CancellationToken cancellationToken)
    {
        var query = new GetGymQuery(subscriptionId, gymId);

        var getGymResult = await sender.Send(query, cancellationToken);

        return getGymResult.Match(
            gym => Ok(new GymResponse(gym.Id, gym.Name)),
            Problem);
    }

    [HttpPost("{gymId:guid}/trainers")]
    public async Task<IActionResult> AddTrainer(
        AddTrainerRequest request,
        Guid subscriptionId,
        Guid gymId,
        CancellationToken cancellationToken)
    {
        var command = new AddTrainerCommand(subscriptionId, gymId, request.TrainerId);

        var addTrainerResult = await sender.Send(command, cancellationToken);

        return addTrainerResult.Match(_ => NoContent(), Problem);
    }

    [HttpGet("{gymId:guid}/sessions")]
    public async Task<IActionResult> ListSessions(
        Guid subscriptionId,
        Guid gymId,
        CancellationToken cancellationToken,
        DateTime? startDateTime = null,
        DateTime? endDateTime = null,
        [FromQuery] List<string>? categories = null)
    {
        ErrorOr<List<SessionCategory>> categoriesToDomainResult = SessionCategoryUtils.ToDomain(categories);

        if (categoriesToDomainResult.IsError)
        {
            return Problem(categoriesToDomainResult.Errors);
        }

        var query = new ListSessionsQuery(
            subscriptionId,
            gymId,
            startDateTime,
            endDateTime,
            categoriesToDomainResult.Value);

        var listSessionsResult = await sender.Send(query, cancellationToken);

        return listSessionsResult.Match(
            sessions => Ok(sessions.ConvertAll(session => session.ToResponse())),
            Problem);
    }
}
