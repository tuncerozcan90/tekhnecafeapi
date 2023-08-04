using TekhneCafe.Core.Entities.Concrete;

namespace TekhneCafe.Entity.Concrete
{
    public class CartLine : BaseEntity
    {
        public Guid ProductId { get; set; }
        public short Quantity { get; set; }
        public float QuantityPrice { get; set; }
        public Guid CartId { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual Product Product { get; set; }
    }

}
