using TekhneCafe.Core.Entities.Concrete;

namespace TekhneCafe.Entity.Concrete
{
    public class CartLine : BaseEntity
    {
        public Guid CartId { get; set; }
        public virtual ICollection<CartLineProduct> CartLineProducts { get; set; }
    }

}
