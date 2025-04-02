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
            if (course == null) return false;
            var isEnrollmentSuccessful = course.EnrollStudent(studentId);
            if (isEnrollmentSuccessful) _context.SaveChanges();
            return isEnrollmentSuccessful;
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return _context.Courses.ToList();
        }

        public bool AddCourse(Course course)
        {
            // Instructors can only have one course at a time
            if (_context.Courses.Any(c => 
                c.Code == course.Code && 
                c.InstructorId == course.InstructorId))
                return false;
            _context.Courses.Add(course);
            _context.SaveChanges();
            return true;
        }
    }
}
