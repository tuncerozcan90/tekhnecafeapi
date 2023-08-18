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
            builder.HasKey(x => x.Id);

            builder.Property(x => x.To).HasMaxLength(100).IsRequired();
            builder.Property(x => x.From).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Message).IsRequired();

            builder.Property(x => x.CreatedDate).HasDefaultValue(DateTime.Now).IsRequired();

            builder.Property(x => x.DeletedDate).IsRequired(false);
        }
    }
}
