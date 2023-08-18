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
    public class OrderProductAttributeConfiguration : IEntityTypeConfiguration<OrderProductAttributeConfiguration>
    {
        public void Configure(EntityTypeBuilder<OrderProductAttributeConfiguration> builder)
        {
            throw new NotImplementedException();
        }
    }
}
