using Microsoft.EntityFrameworkCore;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.DataAccess.Concrete.EntityFramework.Context
{
    public class EfTekhneCafeContext : DbContext
    {
        public EfTekhneCafeContext(DbContextOptions options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public EfTekhneCafeContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=TEKHNECAFEDB;Trusted_Connection=True;TrustServerCertificate=True");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Order>()
              .HasOne(a => a.Cart)
              .WithOne(b => b.Order)
              .HasForeignKey<Cart>(e => e.Id);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AppRole> AppRole { get; set; }
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartLine> CartLine { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderHistory> OrderHistory { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<TransactionHistory> TransactionHistory { get; set; }
        public DbSet<TranscationType> TranscationType { get; set; }
    }
}
