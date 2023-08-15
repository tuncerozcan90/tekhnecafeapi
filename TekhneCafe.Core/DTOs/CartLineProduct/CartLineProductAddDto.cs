using TekhneCafe.Core.DTOs.CartLineProductAttribute;

namespace TekhneCafe.Core.DTOs.CartLineProduct
{
    public class CartLineProductAddDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        //public Guid CartLineId { get; set; }
        //public virtual CartLineAddDto CartLine { get; set; }
        public virtual ICollection<CartLineProductAttributeAddDto> CartLineProductAttributes { get; set; }
    }
}
