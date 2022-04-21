using Microsoft.EntityFrameworkCore;
using NetCore3WithReact.DAL.Models.Sales;

namespace NetCore3WithReact.DAL.EntityConfigurations
{
    public interface IApplicationDbContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<Vendor> Vendors { get; set; }
    }
}
