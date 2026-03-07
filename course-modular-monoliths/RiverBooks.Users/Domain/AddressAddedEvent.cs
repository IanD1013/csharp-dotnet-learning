using RiverBooks.ShardKernel;

namespace RiverBooks.Users.Domain;

internal sealed class AddressAddedEvent : DomainEventBase
{
    public UserStreetAddress NewAddress { get; }

    public AddressAddedEvent(UserStreetAddress newAddress)
    {
        NewAddress = newAddress;
    }
}