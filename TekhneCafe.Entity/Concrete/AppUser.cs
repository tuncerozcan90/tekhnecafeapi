using TekhneCafe.Core.Entities.Concrete;

namespace TekhneCafe.Entity.Concrete
{
    public class AppUser : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Department { get; set; }
        public string Phone { get; set; }
        public string? InternalPhone { get; set; }
        public string? ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public float Wallet { get; set; }
        public Guid AppRoleId { get; set; }
        public virtual AppRole AppRole { get; set; }
        public virtual ICollection<TransactionHistory> TransactionHistories { get; set; }
    }
}

