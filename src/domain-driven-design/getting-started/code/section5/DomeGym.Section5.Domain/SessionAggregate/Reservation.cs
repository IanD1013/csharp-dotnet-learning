using DomeGym.Section5.Domain.Common;

namespace DomeGym.Section5.Domain.SessionAggregate;

public class Reservation : Entity
{
    public Reservation(Guid participantId, Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        ParticipantId = participantId;
    }

    // Used by EF Core when materializing the owning aggregate.
    private Reservation()
    {
    }

    public Guid ParticipantId { get; }
}
