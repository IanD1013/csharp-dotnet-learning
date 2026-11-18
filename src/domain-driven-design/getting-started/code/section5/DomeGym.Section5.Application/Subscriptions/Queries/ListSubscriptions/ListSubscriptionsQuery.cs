using DomeGym.Section5.Domain.SubscriptionAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Subscriptions.Queries.ListSubscriptions;

// No authentication yet, so every subscription is returned instead of only the caller's.
public record ListSubscriptionsQuery : IRequest<ErrorOr<List<Subscription>>>;
