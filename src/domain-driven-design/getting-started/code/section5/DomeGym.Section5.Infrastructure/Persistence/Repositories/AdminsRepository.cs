using DomeGym.Section5.Application.Common.Interfaces;
using DomeGym.Section5.Application.Profiles.Common;
using DomeGym.Section5.Domain.AdminAggregate;
using DomeGym.Section5.Domain.Profiles;
using Microsoft.EntityFrameworkCore;

namespace DomeGym.Section5.Infrastructure.Persistence.Repositories;

public class AdminsRepository(DomeGymDbContext dbContext) : IAdminsRepository
{
    public async Task AddAdminAsync(Admin admin, CancellationToken cancellationToken = default)
    {
        await dbContext.Admins.AddAsync(admin, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<Admin?> GetByIdAsync(Guid adminId, CancellationToken cancellationToken = default) =>
        dbContext.Admins.FirstOrDefaultAsync(admin => admin.Id == adminId, cancellationToken);

    public async Task<Profile?> GetProfileByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        Admin? admin = await dbContext.Admins
            .AsNoTracking()
            .FirstOrDefaultAsync(admin => admin.UserId == userId, cancellationToken);

        return admin is null ? null : new Profile(admin.Id, ProfileType.Admin);
    }

    public async Task UpdateAsync(Admin admin, CancellationToken cancellationToken = default)
    {
        dbContext.Admins.Update(admin);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
