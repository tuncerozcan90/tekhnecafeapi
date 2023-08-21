using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
            builder.Property(_ => _.Message).IsRequired();

            builder.Property(_ => _.CreatedDate).HasDefaultValue(DateTime.Now).IsRequired();

            builder.Property(_ => _.DeletedDate).IsRequired(false);
        }
    }
}
