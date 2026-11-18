using System.Text.Json.Serialization;

namespace DomeGym.Section5.Contracts.Profiles;

[JsonConverter(typeof(JsonStringEnumConverter<ProfileType>))]
public enum ProfileType
{
    Admin,
    Trainer,
    Participant,
}
