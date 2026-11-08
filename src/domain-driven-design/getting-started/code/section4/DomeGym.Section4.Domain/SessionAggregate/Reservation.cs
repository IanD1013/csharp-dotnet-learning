using DomeGym.Section4.Domain.Common;

namespace DomeGym.Section4.Domain.SessionAggregate;

public class Reservation(Guid participantId, Guid? id = null) : Entity(id ?? Guid.NewGuid())
{
    public Guid ParticipantId { get; } = participantId;
}
