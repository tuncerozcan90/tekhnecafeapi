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
    public class TransactionHistoryConfiguration : IEntityTypeConfiguration<TransactionHistory>
    {
        public void Configure(EntityTypeBuilder<TransactionHistory> builder)
        {
            builder.ToTable("TransactionHistory");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.AppUserId).IsRequired();

            builder.HasOne(x => x.AppUser)
                   .WithMany(x => x.TransactionHistories)
                   .HasForeignKey(x => x.AppUserId)
                   .OnDelete(DeleteBehavior.Restrict); 

            builder.HasOne(x => x.Order)
                   .WithMany(x => x.TransactionHistories)
                   .HasForeignKey(x => x.OrderId)
                   .OnDelete(DeleteBehavior.Restrict); 

        }
    }
}
