using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekhneCafe.Core.Filters.Order;
using TekhneCafe.Core.Filters;
using TekhneCafe.Core.ResponseHeaders;
using TekhneCafe.Entity.Concrete;
using TekhneCafe.Core.Filters.Transaction;

namespace TekhneCafe.Business.Helpers.FilterServices
{
    public class TransactionHistoryFilterService
    {
        public TransactionHistoryResponseFilter<List<TransactionHistory>> FilterTransactionHistory(IQueryable<TransactionHistory> transactionHistory, TransactionHistoryRequestFilter filters)
        {
            var filteredTransactionHistory = transactionHistory.Skip(filters.Page * filters.Size).Take(filters.Size).ToList();
            Metadata metadata = new()
            {
                CurrentPage = filters.Page,
                PageSize = filters.Size,
                TotalEntities = transactionHistory.Count(),
                TotalPages = transactionHistory.Count() / filters.Size + 1,
            };
            var header = new CustomHeaders().AddPaginationHeader(metadata);

            return new()
            {
                ResponseValue = filteredTransactionHistory,
                Headers = header
            };
        }
    }
}
