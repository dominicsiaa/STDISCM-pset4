using Grades.Model;
using Grades.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Grades.Controllers
{
    [ApiController]
    [Route("grades")]
    public class GradeController : ControllerBase
    {
        private readonly IGradeService _gradeService;
        private readonly ILogger<GradeController> _logger;
        public GradeController(IGradeService gradeService, ILogger<GradeController> logger)
        {
            _gradeService = gradeService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet("instructor")]
        public ActionResult<IEnumerable<Grade>> GetStudentGrades([FromQuery] int instructorId)
        {
            var grades = _gradeService.GetStudentGrades(instructorId);
            if (grades == null || !grades.Any())
            {
                return NotFound("No grades found for the specified instructor.");
            }
            return Ok(grades);
        }

        [Authorize]
        [HttpGet("student")]
        public ActionResult<IEnumerable<Grade>> GetGradesOfStudent([FromQuery] int studentId)
        {
            var grades = _gradeService.GetGradesOfStudent(studentId);
            if (grades == null || !grades.Any())
            {
                return NotFound("No grades found for the specified student.");
            }
            return Ok(grades);
        }

        [Authorize]
        [HttpPost("add")]
        public ActionResult AddGrade(Grade grade)
        {
            var result = _gradeService.InsertGrade(grade);
            if (!result)
            {
                return BadRequest("Grade already exists for this course and student.");
            }
            return Ok("Grade added successfully.");
        }
    }
}
