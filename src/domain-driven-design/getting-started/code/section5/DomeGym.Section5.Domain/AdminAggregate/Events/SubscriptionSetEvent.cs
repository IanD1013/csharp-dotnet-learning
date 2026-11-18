using DomeGym.Section5.Domain.Common;
using DomeGym.Section5.Domain.SubscriptionAggregate;

namespace DomeGym.Section5.Domain.AdminAggregate.Events;

public record SubscriptionSetEvent(Admin Admin, Subscription Subscription) : IDomainEvent;
