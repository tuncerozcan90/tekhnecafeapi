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
    public class TransactionHistoryConfiguration : IEntityTypeConfiguration<TransactionHistoryConfiguration>
    {
        public void Configure(EntityTypeBuilder<TransactionHistoryConfiguration> builder)
        {
            throw new NotImplementedException();
        }
    }
}
