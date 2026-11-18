using DomeGym.Section5.Domain.Common;
using DomeGym.Section5.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace DomeGym.Section5.Infrastructure.Middleware;

public partial class EventualConsistencyMiddleware(RequestDelegate next, ILogger<EventualConsistencyMiddleware> logger)
{
    public const string DomainEventsKey = "DomainEventsKey";

    public async Task InvokeAsync(HttpContext context, IPublisher publisher, DomeGymDbContext dbContext)
    {
        // The request and every side effect triggered by its domain events share one transaction,
        // which is only committed once all events have been handled.
        IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync();

        context.Response.OnCompleted(async () =>
        {
            try
            {
                if (context.Items.TryGetValue(DomainEventsKey, out object? value)
                    && value is Queue<IDomainEvent> domainEvents)
                {
                    // Handlers may raise further events, which SaveChangesAsync appends to this queue.
                    while (domainEvents.TryDequeue(out IDomainEvent? nextEvent))
                    {
                        await publisher.Publish(nextEvent);
                    }
                }

                await transaction.CommitAsync();
            }
            catch (EventualConsistencyException exception)
            {
                LogEventualConsistencyFailure(exception, exception.EventualConsistencyError.Code);
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        });

        await next(context);
    }

    [LoggerMessage(Level = LogLevel.Error, Message = "Eventual consistency failed ({ErrorCode}); the transaction was rolled back")]
    private partial void LogEventualConsistencyFailure(Exception exception, string errorCode);
}
