using Microsoft.EntityFrameworkCore;

namespace RiverBooks.Users.Data;

internal class EfUserStreetAddressRepository : IReadOnlyUserStreetAddressRepository
{
    private readonly UsersDbContext _dbContext;

    public EfUserStreetAddressRepository(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<UserStreetAddress?> GetById(Guid userStreetAddressId)
    {
        return _dbContext.UserStreetAddresses.SingleOrDefaultAsync(a => a.Id == userStreetAddressId);
    }
}