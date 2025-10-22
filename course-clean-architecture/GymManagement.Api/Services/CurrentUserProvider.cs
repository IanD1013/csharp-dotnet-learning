using GymManagement.Application.Common.Interfaces;
using GymManagement.Application.Common.Models;
using Throw;

namespace GymManagement.Api.Services;

public class CurrentUserProvider : ICurrentUserProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public CurrentUser GetCurrentUser()
    {
        _httpContextAccessor.HttpContext.ThrowIfNull();

        var idClaim = _httpContextAccessor.HttpContext.User.Claims
            .First(claim => claim.Type == "id");

        var permissions = _httpContextAccessor.HttpContext.User.Claims
            .Where(claim => claim.Type == "permissions")
            .Select(claim => claim.Value)
            .ToList();

        return new CurrentUser(
            Id: Guid.Parse(idClaim.Value),
            Permissions: permissions);
    }
}