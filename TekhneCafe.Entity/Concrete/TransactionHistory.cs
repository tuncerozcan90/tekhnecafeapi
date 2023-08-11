using TekhneCafe.Core.Entities.Concrete;

namespace TekhneCafe.Entity.Concrete
{
    public class TransactionHistory : BaseEntity
    {
        public float Amount { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        //public Guid WalletId { get; set; }
        public Guid? OrderId { get; set; }
        public Guid TransactionTypeId { get; set; }
        public Guid AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public virtual Order? Order { get; set; }
        public virtual TranscationType TransactionType { get; set; }
    }
}
