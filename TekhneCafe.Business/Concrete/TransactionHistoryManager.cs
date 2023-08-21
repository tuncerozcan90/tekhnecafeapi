using AutoMapper;
using Microsoft.AspNetCore.Http;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Business.Helpers.FilterServices;
using TekhneCafe.Business.Helpers.HeaderServices;
using TekhneCafe.Core.DTOs.Transaction;
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

        public TransactionHistory GetNewTransactionHistory(float amount, TransactionType transactionType, string description, Guid userId)
            => new TransactionHistory()
            {
                Amount = amount,
                TransactionType = transactionType,
                Description = description,
                AppUserId = userId
            };

        public void SetTransactionHistoryForOrder(Order order, float amount, string description, Guid userId)
            => order.TransactionHistories = new List<TransactionHistory>() { GetNewTransactionHistory(amount, TransactionType.Order, description, userId) };

        public async Task CreateTransactionHistoryAsync(float amount, TransactionType transactionType, string description, Guid userId)
        {
            var transactionHistory = GetNewTransactionHistory(amount, transactionType, description, userId);
            await _transactionHistoryDal.AddAsync(transactionHistory);
        }

        public List<TransactionHistoryListDto> GetAllTransactionHistories(TransactionHistoryRequestFilter filters)
        {
            var filteredResult = new TransactionHistoryFilterService().FilterTransactionHistory(_transactionHistoryDal.GetAll(), filters);
            new HeaderService(_httpContext).AddToHeaders(filteredResult.Headers);
            return _mapper.Map<List<TransactionHistoryListDto>>(filteredResult.ResponseValue);
        }

        public List<TransactionHistoryListDto> GetOrderTransactionHistory(TransactionHistoryRequestFilter filters)
        {
            var result = _transactionHistoryDal.GetOrderTransactionHistoriesIncludeAll();
            var filteredResult = new TransactionHistoryFilterService().FilterTransactionHistory(result, filters);
            new HeaderService(_httpContext).AddToHeaders(filteredResult.Headers);
            return _mapper.Map<List<TransactionHistoryListDto>>(filteredResult.ResponseValue);
        }
    }
}
