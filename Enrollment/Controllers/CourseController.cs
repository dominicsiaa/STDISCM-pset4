using Enrollment.Model;
using Enrollment.Services;
using Microsoft.AspNetCore.Mvc;

namespace Enrollment.Controllers
{
    [ApiController]
    [Route("courses")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly ILogger<CourseController> _logger;

        public CourseController(ICourseService courseService, ILogger<CourseController> logger)
        {
            _courseService = courseService;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Course> Get()
        {
            _logger.LogInformation("Get Method Called");
            return _courseService.GetAllCourses();
        }

        [HttpPost("enroll")]
        public ActionResult EnrollStudent(EnrollStudentRequest request)
        {
            var result = _courseService.EnrollStudent(request.CourseId, request.StudentId);
            if (!result)
            {
                return BadRequest("Enrollment failed");
            }
            return Ok();
        }

        [HttpPost("add")]
        public ActionResult AddCourse(Course course)
        {
            var result = _courseService.AddCourse(course);
            if (!result)
            {
                return BadRequest("Course already exists");
            }
            return Ok();
        }
    }
}
