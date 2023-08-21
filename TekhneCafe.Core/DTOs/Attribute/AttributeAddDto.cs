using TekhneCafe.Core.DTOs.ProductAttribute;

namespace TekhneCafe.Core.DTOs.Attribute
{
    public class AttributeAddDto
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public virtual ICollection<ProductAttributeAddDto> ProductAttributes { get; set; }
    }
}
