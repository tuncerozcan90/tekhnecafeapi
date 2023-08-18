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
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.CreatedDate).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.Description).HasMaxLength(200); 

            builder.HasOne(x => x.AppUser)
                   .WithMany()
                   .HasForeignKey(x => x.AppUserId)
                   .OnDelete(DeleteBehavior.Restrict); 

            builder.Property(x => x.OrderStatus)
                   .IsRequired()
                   .HasColumnType("int");

            builder.HasMany(x => x.OrderProducts)
                   .WithOne(op => op.Order)
                   .HasForeignKey(op => op.OrderId);

            builder.HasMany(x => x.OrderHistories)
                   .WithOne(oh => oh.Order)
                   .HasForeignKey(oh => oh.OrderId);

            builder.HasMany(x => x.TransactionHistories)
                   .WithOne(th => th.Order)
                   .HasForeignKey(th => th.OrderId);
        }
    }
}
