using TekhneCafe.Core.DTOs.Order;

namespace TekhneCafe.Core.DTOs.Transaction
{
    public class TransactionHistoryListDto
    {
        public string Id { get; set; }
        public float Amount { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? OrderId { get; set; }
        public Guid AppUserId { get; set; }
        public OrderListDto? Order { get; set; }
        public string TransactionType { get; set; }
    }
}
