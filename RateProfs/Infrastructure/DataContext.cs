using Microsoft.EntityFrameworkCore;
using RateProfs.Model;

namespace RateProfs.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<RateProf> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RateProf>()
                .HasIndex(r => r.Id)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
