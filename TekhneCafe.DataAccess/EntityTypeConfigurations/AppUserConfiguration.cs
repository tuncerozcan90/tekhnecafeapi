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
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUser");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.LdapId).IsRequired();

            builder.Property(x => x.FullName).HasMaxLength(100).IsRequired();

            builder.Property(x => x.Username).HasMaxLength(100).IsRequired();
            builder.HasIndex(x => x.Username).IsUnique();

            builder.Property(x => x.Email).IsRequired();
            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.Department).HasMaxLength(100);

            builder.Property(x => x.Phone).HasMaxLength(20);
            builder.Property(x => x.InternalPhone).HasMaxLength(20);

            builder.Property(x => x.ImagePath).HasMaxLength(270);

            builder.Property(x => x.CreatedDate).HasDefaultValue(DateTime.Now);

            builder.Property(x => x.Wallet).HasDefaultValue(0.0);

            builder.HasMany(x => x.TransactionHistories)
                   .WithOne()
                   .HasForeignKey(th => th.AppUserId);
        }
    }
}
