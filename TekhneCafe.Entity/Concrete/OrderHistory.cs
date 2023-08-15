using TekhneCafe.Core.Entities.Concrete;
using TekhneCafe.Entity.Enums;

namespace TekhneCafe.Entity.Concrete
{
    public class OrderHistory : BaseEntity
    {
        public Guid ActiveAuthorizedId { get; set; }
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
