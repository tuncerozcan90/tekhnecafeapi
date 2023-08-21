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
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notification");
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.To).HasMaxLength(100).IsRequired();
            builder.Property(_ => _.From).HasMaxLength(100).IsRequired();
            builder.Property(_ => _.Message).IsRequired().HasMaxLength(150);

            builder.Property(_ => _.CreatedDate).HasDefaultValue(DateTime.Now).IsRequired();

            builder.Property(_ => _.DeletedDate).IsRequired(false);
        }
    }
}
