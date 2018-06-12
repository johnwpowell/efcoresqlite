using Microsoft.EntityFrameworkCore;
using Pwned.Core.Data.Models;

namespace Pwned.Core.Data
{
    public class PwnedDbContext : DbContext
    {
        public PwnedDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Password> Passwords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Password>()
                .HasKey(x => x.Hash);
        }
    }
}