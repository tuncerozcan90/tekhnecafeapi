<<<<<<< HEAD
using TekhneCafe.Core.DTOs.Transaction;
using TekhneCafe.Core.Filters.Transaction;
=======
﻿using TekhneCafe.Entity.Concrete;
using TekhneCafe.Entity.Enums;
>>>>>>> development

namespace TekhneCafe.Business.Abstract
{
    public interface ITransactionHistoryService
    {
<<<<<<< HEAD
        List<TransactionHistoryListDto> GetAllTransactionHistory(TransactionHistoryRequestFilter filters = null);
        Task UpdateTransactionHistoryAsync(TransactionHistoryUpdateDto transactionHistoryUpdateDto);
        Task DeleteTransactionHistoryAsync(string id);
        Task CreateOrderTransactionAsync(Guid userId, float amount);
=======
        void SetTransactionHistoryForOrder(Order order, float amount, string description, Guid userId);
        void SetTransactionHistoryForPayment(Order order, float amount, string description, Guid userId);
        TransactionHistory GetNewTransactionHistory(float amount, TransactionType transactionType, string description, Guid userId);
        Task CreateTransactionHistoryAsync(TransactionHistory transactionHistory);
>>>>>>> development
    }
}
