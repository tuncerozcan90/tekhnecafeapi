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
    public class OrderHistoryConfiguration : IEntityTypeConfiguration<OrderHistory>
    {
        public void Configure(EntityTypeBuilder<OrderHistory> builder)
        {
            builder.ToTable("OrderHistory");
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.AppUserId).IsRequired();
            builder.Property(_ => _.OrderId).IsRequired();
            builder.Property(_ => _.OrderStatus).IsRequired();

            builder.HasOne(_ => _.Order)
                   .WithMany()
                   .HasForeignKey(_ => _.OrderId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(_ => _.CreatedDate).IsRequired();
        }
    }
}
