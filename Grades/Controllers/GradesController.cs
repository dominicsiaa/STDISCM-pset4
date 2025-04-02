using Microsoft.AspNetCore.Mvc;
using Grades.Model;
using Grades.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace Grades.Controllers
{
    [Route("api/[controller]")]
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
        [HttpGet]
        public ActionResult<IEnumerable<Grade>> GetAll()
        {
            var grades = _dataAccess.GetGrades();
            return Ok(grades);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddGrade(GradeRequest gradeRequest)
        {
            _logger.LogInformation("AddGrade method called");
            if (gradeRequest == null)
            {
                return BadRequest("Grade request is null.");
            }

            var success = _dataAccess.InsertGrade(gradeRequest);
            if (success)
            {
                return Ok();
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "Unable to add grade.");
        }
    }
}
