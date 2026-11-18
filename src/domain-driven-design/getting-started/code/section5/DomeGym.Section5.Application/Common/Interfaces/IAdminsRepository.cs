using DomeGym.Section5.Application.Profiles.Common;
using DomeGym.Section5.Domain.AdminAggregate;

namespace DomeGym.Section5.Application.Common.Interfaces;

public interface IAdminsRepository
{
    Task AddAdminAsync(Admin admin, CancellationToken cancellationToken = default);

    Task<Admin?> GetByIdAsync(Guid adminId, CancellationToken cancellationToken = default);

    Task<Profile?> GetProfileByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task UpdateAsync(Admin admin, CancellationToken cancellationToken = default);
}
