using TekhneCafe.Core.Entities.Concrete;

namespace TekhneCafe.Entity.Concrete
{
    public class Cart : BaseEntity
    {
        public string? Description { get; set; }
        public virtual ICollection<CartLine> CartLines { get; set; }
        public Order Order { get; set; }
    }

}
