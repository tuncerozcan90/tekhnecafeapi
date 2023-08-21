﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.DataAccess.EntityTypeConfigurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUser");
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.LdapId).IsRequired();

            builder.Property(_ => _.FullName).HasMaxLength(100).IsRequired();

            builder.Property(_ => _.Username).HasMaxLength(100).IsRequired();
            builder.HasIndex(_ => _.Username).IsUnique();

            builder.Property(_ => _.Email).IsRequired();
            builder.HasIndex(_ => _.Email).IsUnique();

            builder.Property(_ => _.Department).HasMaxLength(100);

            builder.Property(_ => _.Phone).HasMaxLength(20);
            builder.Property(_ => _.InternalPhone).HasMaxLength(20);

            builder.Property(_ => _.ImagePath).HasMaxLength(300);

            builder.Property(_ => _.CreatedDate).HasDefaultValue(DateTime.Now);

            //builder.Property(_ => _.Wallet).HasDefaultValue(0.0);

            builder.HasMany(_ => _.TransactionHistories)
                   .WithOne(_ => _.AppUser)
                   .HasForeignKey(_ => _.AppUserId);
        }
    }
}
