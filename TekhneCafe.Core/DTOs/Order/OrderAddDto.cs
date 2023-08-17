using TekhneCafe.Core.DTOs.OrderProduct;

namespace TekhneCafe.Core.DTOs.Order
{
    public class OrderAddDto
    {

        public string? Description { get; set; }
        public virtual ICollection<OrderProductAddDto> OrderProducts { get; set; }
    }
}
