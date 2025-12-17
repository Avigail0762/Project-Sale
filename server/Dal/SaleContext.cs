using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Dal
{
    public class SaleContext : DbContext
    {
        public DbSet<Donor> Donors { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }

        public SaleContext(DbContextOptions<SaleContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Donor>().Property(d => d.FirstName).HasMaxLength(50);
            modelBuilder.Entity<Donor>().Property(d => d.LastName).HasMaxLength(50);
            modelBuilder.Entity<Donor>().Property(d => d.Email).HasMaxLength(50);
            modelBuilder.Entity<Gift>().Property(g => g.Name).HasMaxLength(50);
            base.OnModelCreating(modelBuilder);
        }
    }
}
