using DomeGym.Section4.Domain.Common;
using DomeGym.Section4.Domain.GymAggregate;
using ErrorOr;

namespace DomeGym.Section4.Domain.SubscriptionAggregate;

public class Subscription : AggregateRoot
{
    private readonly SubscriptionType _subscriptionType;
    private readonly int _maxGyms;
    private readonly List<Guid> _gymIds = [];

    public Subscription(SubscriptionType subscriptionType, Guid adminId, Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        _subscriptionType = subscriptionType;
        _maxGyms = GetMaxGyms();
        AdminId = adminId;
    }

    public Guid AdminId { get; }

    public int GetMaxGyms() => _subscriptionType.Name switch
    {
        nameof(SubscriptionType.Free) => 1,
        nameof(SubscriptionType.Starter) => 1,
        nameof(SubscriptionType.Pro) => 3,
        _ => throw new InvalidOperationException($"Unknown subscription type '{_subscriptionType.Name}'."),
    };

    public int GetMaxRooms() => _subscriptionType.Name switch
    {
        nameof(SubscriptionType.Free) => 1,
        nameof(SubscriptionType.Starter) => 3,
        nameof(SubscriptionType.Pro) => int.MaxValue,
        _ => throw new InvalidOperationException($"Unknown subscription type '{_subscriptionType.Name}'."),
    };

    public int GetMaxDailySessions() => _subscriptionType.Name switch
    {
        nameof(SubscriptionType.Free) => 4,
        nameof(SubscriptionType.Starter) => int.MaxValue,
        nameof(SubscriptionType.Pro) => int.MaxValue,
        _ => throw new InvalidOperationException($"Unknown subscription type '{_subscriptionType.Name}'."),
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
}
