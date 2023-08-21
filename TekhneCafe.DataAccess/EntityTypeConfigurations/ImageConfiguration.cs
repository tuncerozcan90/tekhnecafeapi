using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TekhneCafe.DataAccess.EntityTypeConfigurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Entity.Concrete.Image>
    {
        public void Configure(EntityTypeBuilder<Entity.Concrete.Image> builder)
        {
            builder.ToTable("Image");
            builder.HasKey(_ => _.Id);
            
            builder.Property(_ => _.Path).HasMaxLength(300).IsRequired();
            
            builder.Property(_ => _.ProductId).IsRequired();

            builder.HasOne(_ => _.Product)
                   .WithMany(_ => _.Images)
                   .HasForeignKey(_ => _.ProductId);
        }
    }
}
