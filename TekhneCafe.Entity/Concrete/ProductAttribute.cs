using TekhneCafe.Core.Entities.Concrete;

namespace TekhneCafe.Entity.Concrete
{
    public class ProductAttribute : BaseEntity
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<ProductAttributeProduct>? ProductAttributeProducts { get; set; }
    }

}
