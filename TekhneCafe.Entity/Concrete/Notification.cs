using TekhneCafe.Core.Entities.Concrete;

namespace TekhneCafe.Entity.Concrete
{
    public class Notification : BaseEntity
    {
        public string Message { get; set; }
        public Guid AppUserId { get; set; }
        public bool IsValid { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
