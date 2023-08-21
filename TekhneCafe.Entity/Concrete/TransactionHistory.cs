﻿using TekhneCafe.Core.Entities.Concrete;
using TekhneCafe.Entity.Enums;

namespace TekhneCafe.Entity.Concrete
{
    public class TransactionHistory : BaseEntity
    {
        public float Amount { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public Guid? OrderId { get; set; }
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Order? Order { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
