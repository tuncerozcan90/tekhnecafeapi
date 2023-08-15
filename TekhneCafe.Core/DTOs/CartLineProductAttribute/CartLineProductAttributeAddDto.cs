namespace TekhneCafe.Core.DTOs.CartLineProductAttribute
{
    public class CartLineProductAttributeAddDto
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        //public Guid CartProductId { get; set; }
        //public virtual CartLineProduct CartProduct { get; set; }
    }
}
