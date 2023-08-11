using TekhneCafe.Core.Entities.Concrete;

namespace TekhneCafe.Entity.Concrete
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsValid { get; set; }
        public Guid CartLineId { get; set; }
        public virtual ICollection<CartLine> CartLines { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }

}
