namespace TekhneCafe.Core.DTOs.Transaction
{
    public class TransactionHistoryListDto
    {
        //public string Id { get; set; }
        public List<string> Products { get; set; } = new List<string>();
        public string TransactionType { get; set; }
        public float Amount { get; set; }
        public string Personel { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        ////public Guid? OrderId { get; set; }
        ////public Guid AppUserId { get; set; }
        //public OrderListDto? Order { get; set; }
        //public string TransactionType { get; set; }
    }
}
