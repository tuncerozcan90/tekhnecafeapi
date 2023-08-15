namespace TekhneCafe.Core.DTOs.CartLineProduct
{
    public class CartLineProductListDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public Guid CartLineId { get; set; }
        //public virtual CartLine CartLine { get; set; }
        //public virtual ICollection<CartLineProductAttribute> CartLineProductAttributes { get; set; }
    }
}
