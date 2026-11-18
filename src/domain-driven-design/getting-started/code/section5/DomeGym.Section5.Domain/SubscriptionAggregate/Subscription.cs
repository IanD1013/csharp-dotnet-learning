using DomeGym.Section5.Domain.Common;
using DomeGym.Section5.Domain.GymAggregate;
using ErrorOr;

namespace DomeGym.Section5.Domain.SubscriptionAggregate;

public class Subscription : AggregateRoot
{
    private readonly int _maxGyms;
    private readonly List<Guid> _gymIds = [];

    public Subscription(SubscriptionType subscriptionType, Guid adminId, Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        SubscriptionType = subscriptionType;
        _maxGyms = GetMaxGyms();
        AdminId = adminId;
    }

    // Used by EF Core when materializing the aggregate.
    private Subscription()
    {
    }

    public SubscriptionType SubscriptionType { get; } = null!;

    public Guid AdminId { get; }

    public int GetMaxGyms() => SubscriptionType.Name switch
    {
        nameof(SubscriptionType.Free) => 1,
        nameof(SubscriptionType.Starter) => 1,
        nameof(SubscriptionType.Pro) => 3,
        _ => throw new InvalidOperationException($"Unknown subscription type '{SubscriptionType.Name}'."),
    };

    public int GetMaxRooms() => SubscriptionType.Name switch
    {
        nameof(SubscriptionType.Free) => 1,
        nameof(SubscriptionType.Starter) => 3,
        nameof(SubscriptionType.Pro) => int.MaxValue,
        _ => throw new InvalidOperationException($"Unknown subscription type '{SubscriptionType.Name}'."),
    };

    public int GetMaxDailySessions() => SubscriptionType.Name switch
    {
        nameof(SubscriptionType.Free) => 4,
        nameof(SubscriptionType.Starter) => int.MaxValue,
        nameof(SubscriptionType.Pro) => int.MaxValue,
        _ => throw new InvalidOperationException($"Unknown subscription type '{SubscriptionType.Name}'."),
    };

    public ErrorOr<Success> AddGym(Gym gym)
    {
        if (_gymIds.Contains(gym.Id))
        {
            return Error.Conflict(description: "Gym already exists");
        }

        if (_gymIds.Count >= _maxGyms)
        {
            return SubscriptionErrors.CannotHaveMoreGymsThanSubscriptionAllows;
        }

        _gymIds.Add(gym.Id);
        return Result.Success;
    }

    public bool HasGym(Guid gymId) => _gymIds.Contains(gymId);
}
