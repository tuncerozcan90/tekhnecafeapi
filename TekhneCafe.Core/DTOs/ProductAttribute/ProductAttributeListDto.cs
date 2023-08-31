namespace TekhneCafe.Core.DTOs.ProductAttribute
{
    public class ProductAttributeListDto
    {
        public string Id { get; set; }
        public string AttributeId { get; set; }
        public bool IsRequired { get; set; }
        public float Price { get; set; }
        public bool IsDeleted { get; set; }
    }
}
