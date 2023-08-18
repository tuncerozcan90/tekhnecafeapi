using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.DataAccess.EntityTypeConfigurations
{
    public class ProductAttributeConfiguration : IEntityTypeConfiguration<ProductAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductAttribute> builder)
        {
            builder.ToTable("ProductAttribute");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.AttributeId).IsRequired();
            builder.Property(x => x.IsRequired).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();

            builder.HasOne(x => x.Product)
                   .WithMany(x => x.ProductAttributes)
                   .HasForeignKey(x => x.ProductId)
                   .OnDelete(DeleteBehavior.Cascade); 

            builder.HasOne(x => x.Attribute)
                   .WithMany()
                   .HasForeignKey(x => x.AttributeId);
        }
    }
}
