using TekhneCafe.Core.DTOs.CartLine;

namespace TekhneCafe.Core.DTOs.Cart
{
    public class CartAddDto
    {
        public string? Description { get; set; }
        public virtual ICollection<CartLineAddDto> CartLines { get; set; }
    }
}
