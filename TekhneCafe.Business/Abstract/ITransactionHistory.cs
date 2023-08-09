using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Abstract
{
    public interface ITransactionHistoryService
    {
        Task<TransactionHistory> GetTransactionHistoryByIdAsync(Guid orderId);
        Task<List<TransactionHistory>> GetAllTransactionHistoryAsync();
        Task CreateTransactionHistoryAsync(TransactionHistory transactionHistory);
        Task UpdateTransactionHistoryAsync(TransactionHistory transactionHistory);
        Task DeleteTransactionHistoryAsync(Guid transactionHistoryId);
    }
}
