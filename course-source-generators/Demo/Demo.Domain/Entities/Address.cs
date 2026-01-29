using Demo.Generator;

namespace Demo.Domain.Entities;

[ToJsonSerializer]
public partial class Address
{
    public required string AddressLine1 { get; init; }
    public required string AddressLine2 { get; init; }
    public required string City { get; init; }
    public required string State { get; init; }
    public required string PostalCode { get; init; }
}