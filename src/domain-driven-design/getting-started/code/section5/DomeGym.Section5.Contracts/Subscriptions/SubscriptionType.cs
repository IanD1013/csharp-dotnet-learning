using System.Text.Json.Serialization;

namespace DomeGym.Section5.Contracts.Subscriptions;

[JsonConverter(typeof(JsonStringEnumConverter<SubscriptionType>))]
public enum SubscriptionType
{
    Free,
    Starter,
    Pro,
}
