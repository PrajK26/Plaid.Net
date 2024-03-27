using Acklann.Plaid.Management;
using Microsoft.EntityFrameworkCore;

namespace Acklann.Plaid.Demo.DataLayer
{
    public class DBContext : DbContext
    {
        public DbSet<Entity> ExchangeTokenResponse { get; set; }

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entity>()
                .HasIndex(e => new { e.ItemId, e.RequestId})
                .IsUnique();
        }
    }
}
