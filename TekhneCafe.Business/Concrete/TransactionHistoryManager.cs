using AutoMapper;
using Microsoft.AspNetCore.Http;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Business.Helpers.FilterServices;
using TekhneCafe.Business.Helpers.HeaderServices;
using TekhneCafe.Core.DTOs.Transaction;
using TekhneCafe.Core.Exceptions.TransactionHistory;
using TekhneCafe.Core.Filters.Transaction;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.Entity.Concrete;
using TekhneCafe.Entity.Enums;

namespace TekhneCafe.Business.Concrete
{
    public class TransactionHistoryManager : ITransactionHistoryService
    {
        private readonly ITransactionHistoryDal _transactionHistoryDal;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;


        public TransactionHistoryManager(ITransactionHistoryDal transactionHistoryDal, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _transactionHistoryDal = transactionHistoryDal;
            _mapper = mapper;
            _httpContext = httpContext;
        }

        public async Task DeleteTransactionHistoryAsync(string id)
        {
            TransactionHistory transactionHistory = await GetTransactionHistoryById(id);
            await _transactionHistoryDal.SafeDeleteAsync(transactionHistory);
        }

        public List<TransactionHistoryListDto> GetAllTransactionHistory(TransactionHistoryRequestFilter filters = null)
        {
            var filteredResult = new TransactionHistoryFilterService().FilterTransactionHistory(_transactionHistoryDal.GetAll(), filters);
            new HeaderService(_httpContext).AddToHeaders(filteredResult.Headers);
            return _mapper.Map<List<TransactionHistoryListDto>>(filteredResult.ResponseValue);
        }

        public async Task UpdateTransactionHistoryAsync(TransactionHistoryUpdateDto transactionHistoryUpdateDto)
        {
            TransactionHistory transactionHistory = await GetTransactionHistoryById(transactionHistoryUpdateDto.Id);
            _mapper.Map(transactionHistoryUpdateDto, transactionHistory);
            await _transactionHistoryDal.UpdateAsync(transactionHistory);
        }

        private async Task<TransactionHistory> GetTransactionHistoryById(string id)
        {
            TransactionHistory transactionHistory = await _transactionHistoryDal.GetByIdAsync(Guid.Parse(id));
            if (transactionHistory is null)
                throw new TransactionHistoryNotFoundException();

            return transactionHistory;
        }
        public async Task CreateOrderTransactionAsync(Guid userId, float amount)
        {
            TransactionHistory transaction = new TransactionHistory()
            {
                AppUserId = userId,
                Amount = amount,
                Description = "Sipariş verildi.",
                TransactionType = TransactionType.Order,
            };

            await _transactionHistoryDal.AddAsync(transaction);
        }
    }
}
