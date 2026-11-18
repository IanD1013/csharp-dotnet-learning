using DomeGym.Section5.Domain.SessionAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Gyms.Queries.ListSessions;

public record ListSessionsQuery(
    Guid SubscriptionId,
    Guid GymId,
    DateTime? StartDateTime = null,
    DateTime? EndDateTime = null,
    IReadOnlyList<SessionCategory>? Categories = null) : IRequest<ErrorOr<List<Session>>>;
