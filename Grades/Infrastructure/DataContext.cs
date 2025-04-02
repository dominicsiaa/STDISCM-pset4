using Microsoft.EntityFrameworkCore;
using Grades.Model;

namespace Grades.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Grade> Grades { get; set; }
    }
}
