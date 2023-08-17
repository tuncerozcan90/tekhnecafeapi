using TekhneCafe.Core.DTOs.Image;
using TekhneCafe.Core.DTOs.Transaction;
using TekhneCafe.Core.Filters.Image;
using TekhneCafe.Core.Filters.Transaction;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Abstract
{
    public interface ITransactionHistoryService
    {
        List<TransactionHistoryListDto> GetAllTransactionHistory(TransactionHistoryRequestFilter filters = null);
        Task UpdateTransactionHistoryAsync(TransactionHistoryUpdateDto transactionHistoryUpdateDto);
        Task DeleteTransactionHistoryAsync(string id);
        Task CreateOrderTransactionAsync(Guid userId, float amount);
    }
}
