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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TotalPrice).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(200);

            builder.Property(x => x.AppUserId).IsRequired();

            builder.HasMany(x => x.OrderHistories)
                   .WithOne(x => x.Order)
                   .HasForeignKey(x => x.OrderId);

            builder.HasMany(x => x.TransactionHistories)
                   .WithOne(x => x.Order)
                   .HasForeignKey(x => x.OrderId);

            builder.HasMany(x => x.OrderProducts)
                   .WithOne(x => x.Order)
                   .HasForeignKey(x => x.OrderId);
        }
    }
}
