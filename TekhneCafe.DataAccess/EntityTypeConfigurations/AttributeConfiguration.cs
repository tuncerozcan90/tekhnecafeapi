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
    public class AttributeConfiguration : IEntityTypeConfiguration<Entity.Concrete.Attribute>
    {
        public void Configure(EntityTypeBuilder<Entity.Concrete.Attribute> builder)
        {
            builder.ToTable("Attribute");
            builder.HasKey(_ => _.Id);

            
            builder.Property(_ => _.Name).HasMaxLength(100).IsRequired();

            
            builder.Property(_ => _.Price).IsRequired().HasDefaultValue(0.0);
            builder.HasCheckConstraint("Attribute_Price_NonNegative", "Price >= 0");

            
            builder.HasQueryFilter(x => !x.IsDeleted);

           
            builder.HasMany(_ => _.ProductAttributes)
                   .WithOne(pa => pa.Attribute)
                   .HasForeignKey(pa => pa.AttributeId);
        }
    }
}
