using Microsoft.EntityFrameworkCore;
using MultiTenant.Services;

namespace MultiTenant.Models
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ICurrentTenantService _currentTenantService;

        public string CurrentTenantId { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentTenantService currentTenantService ) : base(options)
        {
            _currentTenantService = currentTenantService;
            CurrentTenantId = _currentTenantService.TenantId;
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Tenant> Tenants { get; set; }

        //on app startup
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>().HasQueryFilter(a => a.TenantId == CurrentTenantId);
        }


        //every time we save something 
        public override int SaveChanges()
        {
            foreach ( var entry in ChangeTracker.Entries<IMustHaveTenant>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                        entry.Entity.TenantId = CurrentTenantId;
                        break;
                }
            }
            var result = base.SaveChanges();
            return result;    
        }
    }    
}
