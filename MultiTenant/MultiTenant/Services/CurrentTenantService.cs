
using Microsoft.EntityFrameworkCore;
using MultiTenant.Models;

namespace MultiTenant.Services
{
    public class CurrentTenantService : ICurrentTenantService
    {
        private readonly TenantDbContext _tenantDbContext;

        public CurrentTenantService(TenantDbContext tenantDbContext)
        {
            _tenantDbContext = tenantDbContext;
        }

        public string? TenantId { get; set; }

        public async Task<bool> SetTenant(string tenant)
        {
            var tenantInfo = await _tenantDbContext.Tenants.Where(x => x.Id == tenant).FirstOrDefaultAsync();
            
            if (tenantInfo != null)
            {
                TenantId = tenantInfo.Id;
                return true;
            }
            else
            {
                throw new Exception("Tenant Invalid");
            }
        }
    }
}
