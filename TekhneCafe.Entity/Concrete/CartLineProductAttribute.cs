using TekhneCafe.Core.Entities.Concrete;

namespace TekhneCafe.Entity.Concrete
{
    public partial class CartLineProductAttribute : BaseEntity
    {
        public Guid ProductAttributeProductId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public Guid CartProductId { get; set; }
        public virtual CartLineProduct CartProduct { get; set; }
    }

}

