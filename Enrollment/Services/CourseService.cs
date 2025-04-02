using Enrollment.Infrastructure;
using Enrollment.Model;

namespace Enrollment.Services
{
    public interface ICourseService
    {
        IEnumerable<Course> GetAllCourses();
        bool EnrollStudent(int courseId, int studentId);
        bool AddCourse(Course course);
    }
    public class CourseService : ICourseService
    {
        private readonly DataAccess _dataAccess;

        public CourseService(DataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public IEnumerable<Course> GetAllCourses()
        {
            return _dataAccess.GetAllCourses();
        }

        public bool EnrollStudent(int courseId, int studentId)
        {
            return _dataAccess.EnrollStudent(courseId, studentId);
        }

        public bool AddCourse(Course course)
        {
            return _dataAccess.AddCourse(course);
        }
    }
}
