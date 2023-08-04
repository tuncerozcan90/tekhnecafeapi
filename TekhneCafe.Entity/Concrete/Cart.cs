using TekhneCafe.Core.Entities.Concrete;

namespace TekhneCafe.Entity.Concrete
{
    public class Cart : BaseEntity
    {
        public string? Description { get; set; }
        public Guid AppUserId { get; set; }
        public virtual ICollection<CartLine> CartLines { get; set; }
        //public virtual Order IdNavigation { get; set; }
        public Order Order { get; set; }
    }

}
