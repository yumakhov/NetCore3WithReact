using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NetCore3WithReact.DAL.Entities.Sales;
using NetCore3WithReact.DAL.Entities.Tags;
using System.Threading;
using System.Threading.Tasks;

namespace NetCore3WithReact.DAL.DataProviders
{
    public interface IApplicationDbContext
    {
        void Migrate();
        DbSet<Product> Products { get; set; }
        DbSet<Vendor> Vendors { get; set; }
        public DbSet<Tag> Tags { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityEntry Entry(object entity);
        void Dispose();
    }
}
