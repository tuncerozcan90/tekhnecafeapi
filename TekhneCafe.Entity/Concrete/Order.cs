using TekhneCafe.Core.Entities.Concrete;
using TekhneCafe.Entity.Enums;

namespace TekhneCafe.Entity.Concrete
{
    public class Order : BaseEntity
    {
        public float TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual ICollection<OrderHistory> OrderHistory { get; set; }
        public virtual ICollection<TransactionHistory> TransactionHistory { get; set; }
    }
}

