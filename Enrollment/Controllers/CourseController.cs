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

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public IEnumerable<Course> Get()
        {
            return _courseService.GetAvailableCourses();
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
    }
}
