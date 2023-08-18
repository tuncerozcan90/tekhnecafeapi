using TekhneCafe.Entity.Concrete;
using TekhneCafe.Entity.Enums;

namespace TekhneCafe.Business.Abstract
{
    public interface ITransactionHistoryService
    {
        void SetTransactionHistoryForOrder(Order order, float amount, string description, Guid userId);
        void SetTransactionHistoryForPayment(Order order, float amount, string description, Guid userId);
        TransactionHistory GetNewTransactionHistory(float amount, TransactionType transactionType, string description, Guid userId);
        Task CreateTransactionHistoryAsync(TransactionHistory transactionHistory);
    }
}
