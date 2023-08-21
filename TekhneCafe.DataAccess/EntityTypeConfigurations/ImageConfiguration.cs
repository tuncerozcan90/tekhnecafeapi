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
            builder.ToTable("Image");
            builder.HasKey(_ => _.Id);

            
            builder.Property(_ => _.Path).HasMaxLength(255).IsRequired();

            
            builder.Property(_ => _.ProductId).IsRequired();

            
            builder.HasOne(_ => _.Product)
                   .WithMany(p => p.Images)
                   .HasForeignKey(_ => _.ProductId);
        }
    }
}
