using TekhneCafe.Core.Entities.Concrete;

namespace TekhneCafe.Entity.Concrete
{
    public class ProductAttributeProduct : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Guid ProductAttributeId { get; set; }
        public bool IsRequired { get; set; }
        public bool IsDeleted { get; set; } = true;
        public virtual Product Product { get; set; } = null!;
        public virtual ProductAttribute ProductAttribute { get; set; } = null!;
    }

}
