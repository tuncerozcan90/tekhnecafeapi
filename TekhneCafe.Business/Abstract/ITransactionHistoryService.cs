using TekhneCafe.Core.DTOs.Image;
using TekhneCafe.Core.DTOs.Transaction;
using TekhneCafe.Core.Filters.Image;
using TekhneCafe.Core.Filters.Transaction;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Abstract
{
    public interface ITransactionHistoryService
    {
        Task<TransactionHistoryListDto> GetTransactionHistoryByIdAsync(string id);
        List<TransactionHistoryListDto> GetAllTransactionHistory(TransactionHistoryRequestFilter filters = null);
        Task CreateTransactionHistoryAsync(TransactionHistoryAddDto transactionHistoryAddDto);
        Task UpdateTransactionHistoryAsync(TransactionHistoryUpdateDto transactionHistoryUpdateDto);
        Task DeleteTransactionHistoryAsync(string id);
    }
}
