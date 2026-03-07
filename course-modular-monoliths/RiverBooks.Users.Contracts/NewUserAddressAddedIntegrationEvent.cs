using RiverBooks.ShardKernel;

namespace RiverBooks.Users.Contracts;

public record NewUserAddressAddedIntegrationEvent(UserAddressDetails Details) : IntegrationEventBase;