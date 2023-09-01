using TekhneCafe.Core.DTOs.Attribute;

namespace TekhneCafe.Core.DTOs.ProductAttribute
{
    public class ProductAttributeAddDto
    {
        public AttributeAddDto Attribute { get; set; }
        public bool IsRequired { get; set; }
        public float Price { get; set; }
    }
}
