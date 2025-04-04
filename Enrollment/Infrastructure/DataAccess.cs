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
        public IEnumerable<Course> GetAllCourses()
        {
            return _context.Courses.ToList();
        }
        public bool EnrollStudent(int courseId, int studentId, string courseCode)
        {
            // To check if course exists and if student is already enrolled in the course
            var course = _context.Courses.Find(courseId);
            var studentEnrolled = _context.Courses
                .Where(c => c.Code == courseCode)
                .AsEnumerable()
                .SelectMany(c => c.StudentIds)
                .Any(s => s == studentId);
            if (course == null || studentEnrolled)
                return false;
            var isEnrollmentSuccessful = course.EnrollStudent(studentId);
            if (isEnrollmentSuccessful) _context.SaveChanges();
            return isEnrollmentSuccessful;
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
