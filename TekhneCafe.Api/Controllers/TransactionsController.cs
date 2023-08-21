using Microsoft.AspNetCore.Mvc;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Core.Filters.Transaction;

namespace TekhneCafe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionHistoryService _transactionHistoryService;

        public TransactionsController(ITransactionHistoryService transactionHistoryService)
        {
            _transactionHistoryService = transactionHistoryService;
        }

        /// <summary>
        /// Get all transactions
        /// </summary>
        /// <param name="filters">filters</param>
        /// <returns>Returns all the transactions with the given filters</returns>
        [HttpGet("transactions")]
        public IActionResult Transactions([FromQuery] TransactionHistoryRequestFilter filters)
        {
            var transactions = _transactionHistoryService.GetAllTransactionHistories(filters);
            return Ok(transactions);
        }

        /// <summary>
        /// Get order transactions
        /// </summary>
        /// <param name="filters">filters</param>
        /// <returns>Returns order transactions with the given filters</returns>
        [HttpGet("ordertransactions")]
        public IActionResult OrderTransactions([FromQuery] TransactionHistoryRequestFilter filters)
        {
            var transactions = _transactionHistoryService.GetOrderTransactionHistory(filters);
            return Ok(transactions);
        }
    }
}
