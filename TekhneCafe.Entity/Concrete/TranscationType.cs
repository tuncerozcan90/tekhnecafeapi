using TekhneCafe.Core.Entities.Concrete;

namespace TekhneCafe.Entity.Concrete
{
    public class TranscationType : BaseEntity
    {
        public string Type { get; set; }
        public virtual ICollection<TransactionHistory> TransactionHistories { get; set; }
    }
}
