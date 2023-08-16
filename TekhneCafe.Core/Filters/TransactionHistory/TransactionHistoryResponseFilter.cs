using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekhneCafe.Core.Filters.Transaction
{
    public class TransactionHistoryResponseFilter<T> : ResponseFilter<T> where T : class, new()
    {
    }
}
