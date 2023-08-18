using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekhneCafe.DataAccess.Concrete.EntityFramework.Context;

namespace TekhneCafe.DataAccess.Helpers.Transaction
{
    public interface ITransactionManagement
    {
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
