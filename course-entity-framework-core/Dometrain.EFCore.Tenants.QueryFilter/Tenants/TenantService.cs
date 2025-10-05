namespace Dometrain.EFCore.Tenants.QueryFilter.Tenants;

/// <summary>
/// This could get the tenant from anywhere (token, database, ...).
/// This implementation gets it from the header for brevity.
/// </summary>
public class TenantService(IHttpContextAccessor contextAccessor)
{
   private string? _tenantId;

   public string? GetTenantId()
   {
      if (_tenantId is not null)
         return _tenantId;
      
      var header = contextAccessor.HttpContext?.Request.Headers
         .FirstOrDefault(h => h.Key.Equals("x-tenant", StringComparison.CurrentCultureIgnoreCase));
        
      if (header is { Value: { } value })
      {
         if (value.Any())
            _tenantId = value.First()!;
      }
      return _tenantId;
   }
}