using DomeGym.Section5.Application.Common.Interfaces;
using DomeGym.Section5.Domain.RoomAggregate;
using DomeGym.Section5.Domain.SessionAggregate;
using Microsoft.EntityFrameworkCore;

namespace DomeGym.Section5.Infrastructure.Persistence.Repositories;

public class SessionsRepository(DomeGymDbContext dbContext) : ISessionsRepository
{
    public Task<Session?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        dbContext.Sessions.FirstOrDefaultAsync(session => session.Id == id, cancellationToken);

    public Task<List<Session>> ListByIdsAsync(
        IReadOnlyList<Guid> sessionIds,
        DateTime? startDateTime = null,
        DateTime? endDateTime = null,
        IReadOnlyList<SessionCategory>? categories = null,
        CancellationToken cancellationToken = default)
    {
        return dbContext.Sessions
            .AsNoTracking()
            .Where(session => sessionIds.Contains(session.Id))
            .WhereStartsBetween(startDateTime, endDateTime)
            .WhereOfCategory(categories)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Session>> ListByGymIdAsync(
        Guid gymId,
        DateTime? startDateTime = null,
        DateTime? endDateTime = null,
        IReadOnlyList<SessionCategory>? categories = null,
        CancellationToken cancellationToken = default)
    {
        List<Room> gymRooms = await dbContext.Rooms
            .AsNoTracking()
            .Where(room => room.GymId == gymId)
            .ToListAsync(cancellationToken);

        List<Guid> sessionIds = [.. gymRooms.SelectMany(room => room.SessionIds)];

        return await ListByIdsAsync(sessionIds, startDateTime, endDateTime, categories, cancellationToken);
    }

    public async Task UpdateAsync(Session session, CancellationToken cancellationToken = default)
    {
        dbContext.Sessions.Update(session);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}

file static class SessionQueryExtensions
{
    public static IQueryable<Session> WhereStartsBetween(this IQueryable<Session> query, DateTime? start, DateTime? end)
    {
        if (start is DateTime startDateTime)
        {
            var startDate = DateOnly.FromDateTime(startDateTime);
            var startTime = TimeOnly.FromDateTime(startDateTime);

            query = query.Where(session =>
                session.Date > startDate || (session.Date == startDate && session.Time.Start >= startTime));
        }

        if (end is DateTime endDateTime)
        {
            var endDate = DateOnly.FromDateTime(endDateTime);
            var endTime = TimeOnly.FromDateTime(endDateTime);

            query = query.Where(session =>
                session.Date < endDate || (session.Date == endDate && session.Time.Start <= endTime));
        }

        return query;
    }

    public static IQueryable<Session> WhereOfCategory(
        this IQueryable<Session> query,
        IReadOnlyList<SessionCategory>? categories)
    {
        if (categories is null || categories.Count == 0)
        {
            return query;
        }

        List<string> categoryNames = [.. categories.Select(category => category.Name)];

        return query.Where(session => session.Categories.Any(category => categoryNames.Contains(category.Name)));
    }
}
