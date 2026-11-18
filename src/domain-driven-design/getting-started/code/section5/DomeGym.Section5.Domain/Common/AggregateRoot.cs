namespace DomeGym.Section5.Domain.Common;

public abstract class AggregateRoot : Entity
{
    private readonly List<IDomainEvent> _domainEvents = [];

    protected AggregateRoot(Guid id)
        : base(id)
    {
    }

    // Used by EF Core when materializing aggregates.
    protected AggregateRoot()
    {
    }

    public IReadOnlyList<IDomainEvent> PopDomainEvents()
    {
        List<IDomainEvent> copy = [.. _domainEvents];
        _domainEvents.Clear();

        return copy;
    }

    protected void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}
