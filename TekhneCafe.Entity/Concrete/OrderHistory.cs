using TekhneCafe.Core.Entities.Concrete;

namespace TekhneCafe.Entity.Concrete
{
    public class OrderHistory : BaseEntity
    {
        public bool OrderStatus { get; set; }
        public string ActiveAuthorized { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
    }

}
