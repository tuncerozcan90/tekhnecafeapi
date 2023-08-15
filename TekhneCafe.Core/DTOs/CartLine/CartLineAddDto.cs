using TekhneCafe.Core.DTOs.CartLineProduct;

namespace TekhneCafe.Core.DTOs.CartLine
{
    public class CartLineAddDto
    {
        public virtual ICollection<CartLineProductAddDto> CartLineProducts { get; set; }
    }
}
