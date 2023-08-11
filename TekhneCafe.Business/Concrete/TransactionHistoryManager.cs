using Microsoft.EntityFrameworkCore;
using TekhneCafe.Business.Abstract;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Concrete
{
    public class TransactionHistoryManager : ITransactionHistoryService
    {
        private readonly ITransactionHistoryDal _transactionHistoryDal;

        public TransactionHistoryManager(ITransactionHistoryDal transactionHistoryDal)
        {
            _transactionHistoryDal = transactionHistoryDal;
        }

        public async Task CreateTransactionHistoryAsync(TransactionHistory transactionHistory)
        {
            await _transactionHistoryDal.AddAsync(transactionHistory);
        }

        public async Task DeleteTransactionHistoryAsync(Guid transactionHistoryId)
        {
            var transactionHistory = await _transactionHistoryDal.GetByIdAsync(transactionHistoryId);
            if (transactionHistory != null)
            {
                await _transactionHistoryDal.HardDeleteAsync(transactionHistory);
            }
        }

        public async Task<List<TransactionHistory>> GetAllTransactionHistoryAsync()
        {
            return await _transactionHistoryDal.GetAll().ToListAsync();
        }

        public async Task<TransactionHistory> GetTransactionHistoryByIdAsync(Guid transactionHistoryId)
        {
            return await _transactionHistoryDal.GetByIdAsync(transactionHistoryId);
        }

        public async Task UpdateTransactionHistoryAsync(TransactionHistory transactionHistory)
        {
            await _transactionHistoryDal.UpdateAsync(transactionHistory);
        }
    }
}
