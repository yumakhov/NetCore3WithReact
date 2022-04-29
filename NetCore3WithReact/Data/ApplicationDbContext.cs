using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NetCore3WithReact.DAL.DataProviders;
using NetCore3WithReact.DAL.Entities;
using NetCore3WithReact.DAL.Entities.Sales;
using NetCore3WithReact.DAL.Entities.Tags;

namespace NetCore3WithReact.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new VendorConfiguration());            
            builder.ApplyConfiguration(new TagConfiguration());            
            builder.ApplyConfiguration(new ProductTagConfiguration());            
        }
    }
}
