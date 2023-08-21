using TekhneCafe.Core.DTOs.Transaction;
using TekhneCafe.Core.Filters.Transaction;
using TekhneCafe.Entity.Concrete;
using TekhneCafe.Entity.Enums;

namespace TekhneCafe.Business.Abstract
{
    public interface ITransactionHistoryService
    {
        void SetTransactionHistoryForOrder(Order order, float amount, string description, Guid userId);
        List<TransactionHistoryListDto> GetAllTransactionHistories(TransactionHistoryRequestFilter filters);
        TransactionHistory GetNewTransactionHistory(float amount, TransactionType transactionType, string description, Guid userId);
        Task CreateTransactionHistoryAsync(float amount, TransactionType transactionType, string description, Guid userId);
        List<TransactionHistoryListDto> GetOrderTransactionHistory(TransactionHistoryRequestFilter filters);
    }
}
