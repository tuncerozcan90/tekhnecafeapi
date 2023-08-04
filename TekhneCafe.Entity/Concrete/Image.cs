using TekhneCafe.Core.Entities.Concrete;

namespace TekhneCafe.Entity.Concrete
{
    public class Image : BaseEntity
    {
        public string Path { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }

}
