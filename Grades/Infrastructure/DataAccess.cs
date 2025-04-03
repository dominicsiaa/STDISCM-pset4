using Grades.Model;

namespace Grades.Infrastructure
{
    public class DataAccess : IDisposable
    {
        private readonly DataContext _context;

        public DataAccess(DataContext context)
        {
            _context = context;
            _context.Database.EnsureCreated(); 
        }

        public void Dispose() => _context.Dispose();

        public List<Grade> GetGrades()
        {
            return _context.Grades.ToList();
        }

        public Grade? GetGradeById(int id)
        {
            return _context.Grades.FirstOrDefault(g => g.Id == id);
        }

        public bool InsertGrade(Grade grade)
        {
            // Students can only have one grade per course
            if (_context.Grades.Any(g => 
                g.CourseCode == grade.CourseCode && 
                g.StudentId == grade.StudentId))
                return false;
            _context.Grades.Add(grade);
            _context.SaveChanges();
            return true;
        }

    }
}
