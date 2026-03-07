using Microsoft.EntityFrameworkCore;
using RiverBooks.Users.Data;
using RiverBooks.Users.Domain;
using RiverBooks.Users.Interfaces;

namespace RiverBooks.Users.Infrastructure.Data;

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