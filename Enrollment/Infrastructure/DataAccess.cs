using Enrollment.Model;

namespace Enrollment.Infrastructure
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
        public bool EnrollStudent(int courseId, int studentId)
        {
            var course = _context.Courses.Find(courseId);
            if (course == null)
                return false;

            course.StudentIds.Add(studentId);

            _context.SaveChanges();

            return true;
        }

        public IEnumerable<Course> GetAvailableCourses()
        {
            return _context.Courses.Where(c => c.StudentIds.Count < c.Capacity);
        }
    }
}
