using TekhneCafe.Core.Entities.Concrete;

namespace TekhneCafe.Entity.Concrete
{
    public class Order : BaseEntity
    {
        public decimal TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsValid { get; set; }
        //public virtual Cart Cart { get; set; }
        public virtual ICollection<OrderHistory> OrderHistories { get; set; }
        public virtual ICollection<TransactionHistory> TransactionHistories { get; set; }
        public Cart Cart { get; set; }
    }
}

