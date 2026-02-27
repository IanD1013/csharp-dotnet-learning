using Ardalis.Result;

namespace RiverBooks.OrderProcessing;

internal interface IOrderAddressCache
{
    Task<Result<OrderAddress>> GetByIdAsync(Guid addressId);
}

internal record OrderAddress(Guid Id, Address Address);