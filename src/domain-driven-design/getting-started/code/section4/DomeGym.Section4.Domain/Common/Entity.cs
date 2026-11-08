namespace DomeGym.Section4.Domain.Common;

public abstract class Entity(Guid id)
{
    public Guid Id { get; } = id;

    public override bool Equals(object? obj) =>
        obj is Entity other && other.GetType() == GetType() && other.Id == Id;

    public override int GetHashCode() => Id.GetHashCode();
}
