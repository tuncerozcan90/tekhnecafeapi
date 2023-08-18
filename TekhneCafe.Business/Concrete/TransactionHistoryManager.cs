using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Core.DTOs.Transaction;
using TekhneCafe.Core.Exceptions.TransactionHistory;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.Entity.Concrete;
using TekhneCafe.Entity.Enums;

namespace TekhneCafe.Business.Concrete
{
    public class TransactionHistoryManager : ITransactionHistoryService
    {
        private readonly ITransactionHistoryDal _transactionHistoryDal;
        private readonly IMapper _mapper;


        public TransactionHistoryManager(ITransactionHistoryDal transactionHistoryDal, IMapper mapper)
        {
            _transactionHistoryDal = transactionHistoryDal;
            _mapper = mapper;
        }

        public TransactionHistory GetNewTransactionHistory(float amount, TransactionType transactionType, string description, Guid userId)
            => new TransactionHistory()
            {
                Amount = amount,
                TransactionType = transactionType,
                Description = description,
                AppUserId = userId
            };
        public void SetTransactionHistoryForOrder(Order order, float amount, string description, Guid userId)
            => order.TransactionHistories = new List<TransactionHistory>()
            {
                GetNewTransactionHistory(amount, TransactionType.Order, description, userId)
            };

        //sonra bakılacak
        public void SetTransactionHistoryForPayment(Order order, float amount, string description, Guid userId)
            => order.TransactionHistories = new List<TransactionHistory>()
            {
                GetNewTransactionHistory(amount, TransactionType.Payment, description, userId)
            };

        public async Task CreateTransactionHistoryAsync(TransactionHistory transactionHistory)
        {
            await _transactionHistoryDal.AddAsync(transactionHistory);
        }

        public async Task<List<TransactionHistory>> GetAllTransactionHistoryAsync()
        {
            return await _transactionHistoryDal.GetAll().ToListAsync();
        }

        public async Task<TransactionHistory> GetTransactionHistoryByIdAsync(Guid transactionHistoryId)
        {
            return await _transactionHistoryDal.GetByIdAsync(transactionHistoryId);
        }
    }
}
