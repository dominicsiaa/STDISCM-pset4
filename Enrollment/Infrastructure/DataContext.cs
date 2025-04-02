using Enrollment.Model;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasIndex(c => c.Id)
                .IsUnique();

            // Temporary Seed Data
            //modelBuilder.Entity<Course>().HasData(
            //    new Course { Id = 1, Code = "CCPROG1", Units = 3, Capacity = 30, InstructorId = 1, StudentIds = new List<int>() },
            //    new Course { Id = 2, Code = "THS-ST1", Units = 2, Capacity = 20, InstructorId = 2, StudentIds = new List<int>() }
            //);

            base.OnModelCreating(modelBuilder);
        }
    }
}
