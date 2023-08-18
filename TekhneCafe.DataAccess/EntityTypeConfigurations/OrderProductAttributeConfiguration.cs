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
    public class OrderProductAttributeConfiguration : IEntityTypeConfiguration<OrderProductAttribute>
    {
        public void Configure(EntityTypeBuilder<OrderProductAttribute> builder)
        {

            builder.ToTable("OrderProductAttribute");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductAttributeId).IsRequired();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();

            builder.HasOne(x => x.OrderProduct)
                   .WithMany(x => x.OrderProductAttributes)
                   .HasForeignKey(x => x.OrderProductId)
                   .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
