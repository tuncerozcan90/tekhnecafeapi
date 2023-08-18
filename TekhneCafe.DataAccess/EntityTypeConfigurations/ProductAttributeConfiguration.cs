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
    public class ProductAttributeConfiguration : IEntityTypeConfiguration<ProductAttributeConfiguration>
    {
        public void Configure(EntityTypeBuilder<ProductAttributeConfiguration> builder)
        {
            throw new NotImplementedException();
        }
    }
}
