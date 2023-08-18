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
    public class ImageConfiguration : IEntityTypeConfiguration<Entity.Concrete.Image>
    {
        public void Configure(EntityTypeBuilder<Entity.Concrete.Image> builder)
        {
            builder.ToTable("Images");
            builder.HasKey(x => x.Id);

            
            builder.Property(x => x.Path).HasMaxLength(255).IsRequired();

            
            builder.Property(x => x.ProductId).IsRequired();

            
            builder.HasOne(x => x.Product)
                   .WithMany(p => p.Images)
                   .HasForeignKey(x => x.ProductId);
        }
    }
}
