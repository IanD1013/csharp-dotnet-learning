using DomeGym.Section5.Domain.AdminAggregate;
using DomeGym.Section5.Domain.Common;
using DomeGym.Section5.Domain.GymAggregate;
using DomeGym.Section5.Domain.ParticipantAggregate;
using DomeGym.Section5.Domain.RoomAggregate;
using DomeGym.Section5.Domain.SessionAggregate;
using DomeGym.Section5.Domain.SubscriptionAggregate;
using DomeGym.Section5.Domain.TrainerAggregate;
using DomeGym.Section5.Infrastructure.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DomeGym.Section5.Infrastructure.Persistence;

public class DomeGymDbContext(DbContextOptions<DomeGymDbContext> options, IHttpContextAccessor httpContextAccessor)
    : DbContext(options)
{
    public DbSet<Admin> Admins => Set<Admin>();

    public DbSet<Gym> Gyms => Set<Gym>();

    public DbSet<Participant> Participants => Set<Participant>();

    public DbSet<Room> Rooms => Set<Room>();

    public DbSet<Session> Sessions => Set<Session>();

    public DbSet<Subscription> Subscriptions => Set<Subscription>();

    public DbSet<Trainer> Trainers => Set<Trainer>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        List<IDomainEvent> domainEvents =
        [
            .. ChangeTracker.Entries<AggregateRoot>().SelectMany(entry => entry.Entity.PopDomainEvents()),
        ];

        int result = await base.SaveChangesAsync(cancellationToken);

        if (domainEvents.Count > 0)
        {
            QueueDomainEvents(domainEvents);
        }

        return result;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DomeGymDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    // The events are published by EventualConsistencyMiddleware after the response has been sent.
    private void QueueDomainEvents(List<IDomainEvent> domainEvents)
    {
        HttpContext httpContext = httpContextAccessor.HttpContext
            ?? throw new InvalidOperationException("Domain events can only be raised while handling an HTTP request.");

        if (httpContext.Items[EventualConsistencyMiddleware.DomainEventsKey] is not Queue<IDomainEvent> domainEventsQueue)
        {
            domainEventsQueue = new Queue<IDomainEvent>();
            httpContext.Items[EventualConsistencyMiddleware.DomainEventsKey] = domainEventsQueue;
        }

        domainEvents.ForEach(domainEventsQueue.Enqueue);
    }
}
