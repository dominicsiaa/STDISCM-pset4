using Grades.Model;
using Grades.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Grades.Controllers
{
    [Route("grades")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly ILogger<GradesController> _logger;
        private readonly DataAccess _dataAccess;
        public GradesController(ILogger<GradesController> logger, DataAccess dataAccess)
        {
            _logger = logger;
            _dataAccess = dataAccess;
        }

        [Authorize]
        [HttpGet("instructor")]
        public ActionResult<IEnumerable<Grade>> GetStudentGrades([FromQuery] int instructorId)
        {
            var grades = _dataAccess.GetGrades().Where(g => g.InstructorId == instructorId).ToList();
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
            var grades = _dataAccess.GetGrades().Where(g => g.StudentId == studentId).ToList();
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
            _logger.LogInformation("AddGrade method called");
            var result = _dataAccess.InsertGrade(grade);
            if (!result)
            {
                return BadRequest("Grade already exists for this course and student.");
            }
            return Ok("Grade added successfully.");
        }
    }
}
