namespace DomeGym.Section4.Domain.Common;

public abstract class ValueObject
{
    public override bool Equals(object? obj) =>
        obj is ValueObject other
        && other.GetType() == GetType()
        && other.GetEqualityComponents().SequenceEqual(GetEqualityComponents());

    public override int GetHashCode()
    {
        var hashCode = new HashCode();

        foreach (object? component in GetEqualityComponents())
        {
            hashCode.Add(component);
        }

        return hashCode.ToHashCode();
    }

    protected abstract IEnumerable<object?> GetEqualityComponents();
}
