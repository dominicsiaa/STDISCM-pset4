using Microsoft.EntityFrameworkCore;
using Grades.Model;

namespace Grades.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grade>()
                .HasIndex(g => g.Id)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
