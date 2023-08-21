using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TekhneCafe.DataAccess.EntityTypeConfigurations
{
    public class AttributeConfiguration : IEntityTypeConfiguration<Entity.Concrete.Attribute>
    {
        public void Configure(EntityTypeBuilder<Entity.Concrete.Attribute> builder)
        {
            builder.ToTable("Attribute");
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Name).HasMaxLength(100).IsRequired();

            builder.Property(_ => _.Price).IsRequired().HasDefaultValue(0.0);
            builder.HasCheckConstraint("Attribute_Price_NonNegative", "Price >= 0");

            builder.HasMany(_ => _.ProductAttributes)
                   .WithOne(_ => _.Attribute)
                   .HasForeignKey(_ => _.AttributeId);
        }
    }
}
