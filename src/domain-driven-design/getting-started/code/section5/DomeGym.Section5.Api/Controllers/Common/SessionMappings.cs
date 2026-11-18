using DomeGym.Section5.Contracts.Sessions;
using DomeGym.Section5.Domain.SessionAggregate;

namespace DomeGym.Section5.Api.Controllers.Common;

public static class SessionMappings
{
    public static SessionResponse ToResponse(this Session session) =>
        new(
            session.Id,
            session.Name,
            session.Description,
            session.NumParticipants,
            session.MaxParticipants,
            session.Date.ToDateTime(session.Time.Start),
            session.Date.ToDateTime(session.Time.End),
            [.. session.Categories.Select(category => category.Name)]);
}
