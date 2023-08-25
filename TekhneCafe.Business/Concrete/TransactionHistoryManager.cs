using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Business.Helpers.FilterServices;
using TekhneCafe.Business.Helpers.HeaderServices;
using TekhneCafe.Core.DTOs.Transaction;
using TekhneCafe.Core.Extensions;
using TekhneCafe.Core.Filters.Transaction;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.Entity.Concrete;
using TekhneCafe.Entity.Enums;

namespace TekhneCafe.Business.Concrete
{
    public class TransactionHistoryManager : ITransactionHistoryService
    {
        private readonly ITransactionHistoryDal _transactionHistoryDal;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IAppUserService _userService;

        public TransactionHistoryManager(ITransactionHistoryDal transactionHistoryDal, IHttpContextAccessor httpContext, IAppUserService userService)
        {
            _transactionHistoryDal = transactionHistoryDal;
            _httpContext = httpContext;
            _userService = userService;
        }

        public void SetTransactionHistoryForOrder(Order order, float amount, string description, Guid userId)
            => order.TransactionHistories = new List<TransactionHistory>() { new TransactionHistory(amount, TransactionType.Order, description, userId) };

        public async Task CreateTransactionHistoryAsync(float amount, TransactionType transactionType, string description, Guid userId)
        {
            var transactionHistory = new TransactionHistory(amount, transactionType, description, userId);
            await _transactionHistoryDal.AddAsync(transactionHistory);
        }

        public async Task<List<TransactionHistoryListDto>> GetAllTransactionHistoriesByIdAsync(TransactionHistoryRequestFilter filters, string userId)
        {
            var filteredResult = FilterTransactionHistories(filters, userId);
            return await TransactionHistoryMappings(filteredResult.ResponseValue);
        }

        public async Task<List<TransactionHistoryListDto>> GetActiveUsersTransactionHistoriesAsync(TransactionHistoryRequestFilter filters)
        {
            string activeUserId = _httpContext.HttpContext.User.ActiveUserId();
            var filteredResult = FilterTransactionHistories(filters, activeUserId);
            return await TransactionHistoryMappings(filteredResult.ResponseValue);
        }

        private TransactionHistoryResponseFilter<List<TransactionHistory>> FilterTransactionHistories(TransactionHistoryRequestFilter filters, string userId)
        {
            var query = GetTransactionHistoriesIncludeAll(userId);
            var filteredResult = new TransactionHistoryFilterService().FilterTransactionHistory(query, filters);
            new HeaderService(_httpContext).AddToHeaders(filteredResult.Headers);
            return filteredResult;
        }

        private IQueryable<TransactionHistory> GetTransactionHistoriesIncludeAll(string userId)
            => _transactionHistoryDal.GetAll(_ => _.AppUserId == Guid.Parse(userId))
                .Include(_ => _.Order)
                    .ThenInclude(_ => _.OrderProducts)
                .Include(_ => _.Order)
                    .ThenInclude(_ => _.OrderHistories);

        private async Task<List<TransactionHistoryListDto>> TransactionHistoryMappings(List<TransactionHistory> filteredResult)
        {
            List<TransactionHistoryListDto> transactionHistories = new();
            foreach (var transactionHistory in filteredResult)
            {
                var user = await _userService.GetRawUserByIdAsync(transactionHistory.AppUserId.ToString());
                transactionHistories.Add(new()
                {
                    CreatedDate = transactionHistory.CreatedDate,
                    Products = transactionHistory.Order?.OrderProducts?.Select(_ => _.Name)?.ToList(),
                    TransactionType = transactionHistory.TransactionType == TransactionType.Order ? "Sipariş" : "Ödeme",
                    Amount = transactionHistory.Amount,
                    Personel = user.FullName,
                    Description = transactionHistory.Description
                });
            }

            return transactionHistories;
        }
    }
}
