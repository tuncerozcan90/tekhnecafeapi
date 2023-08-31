using TekhneCafe.Core.Entities.Concrete;

namespace TekhneCafe.Entity.Concrete
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<Image>? Images { get; set; }
        public virtual ICollection<ProductAttribute>? ProductAttributes { get; set; }
        public virtual ICollection<Category>? Categories { get; set; }
    }

}
