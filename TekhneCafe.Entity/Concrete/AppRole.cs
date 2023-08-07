using TekhneCafe.Core.Entities.Concrete;

namespace TekhneCafe.Entity.Concrete
{
    public class AppRole : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<AppUser>? AppUsers { get; set; }
        public bool IsValid { get; set; } = true;
    }

}

