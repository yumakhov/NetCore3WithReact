using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NetCore3WithReact.DAL.Entities.Sales
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .ToTable("Products")
                .HasKey(p => p.Id);

            builder
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(product => product.VendorId)
                .IsRequired();

            builder
                .HasOne(product => product.Vendor)
                .WithMany(vendor => vendor.Products)
                .HasForeignKey(product => product.VendorId);
        }
    }
}
