using Enrollment.Infrastructure;
using Enrollment.Model;

namespace Enrollment.Services
{
    public interface ICourseService
    {
        IEnumerable<Course> GetAvailableCourses();
        bool EnrollStudent(int courseId, int studentId);
    }
    public class CourseService : ICourseService
    {
        private readonly DataAccess _dataAccess;

        public CourseService(DataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public IEnumerable<Course> GetAvailableCourses()
        {
            return _dataAccess.GetAvailableCourses();
        }

        public bool EnrollStudent(int courseId, int studentId)
        {
            return _dataAccess.EnrollStudent(courseId, studentId);
        }
    }
}
