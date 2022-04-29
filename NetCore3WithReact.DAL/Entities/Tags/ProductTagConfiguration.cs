using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NetCore3WithReact.DAL.Entities.Tags
{
    public class ProductTagConfiguration : IEntityTypeConfiguration<ProductTag>
    {
        public void Configure(EntityTypeBuilder<ProductTag> builder)
        {
            builder
                .ToTable("ProductTags")
                .HasKey(builder => new { builder.ProductId, builder.TagId });

            builder
                .HasOne(productTag => productTag.Tag)
                .WithMany(tag => tag.ProductTags)
                .HasForeignKey(productTag => productTag.TagId);

            builder
                .HasOne(productTag => productTag.Product)
                .WithMany(product => product.Tags)
                .HasForeignKey(productTag => productTag.ProductId);
        }
    }
}
