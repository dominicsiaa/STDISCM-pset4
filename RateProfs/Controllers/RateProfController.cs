using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RateProfs.Model;
using RateProfs.Services;

namespace RateProfs.Controllers
{
    [ApiController]
    [Route("ratings")]
    public class RateProfController : ControllerBase
    {
        private readonly IRateProfService _service;
        private readonly ILogger<RateProfController> _logger;

        public RateProfController(IRateProfService service, ILogger<RateProfController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [Authorize]
        [HttpGet("student/{id}")]
        public IEnumerable<RateProf> GetRatingsByStudent(int id)
        {
            return _service.GetRatingsOfStudent(id);
        }

        [Authorize]
        [HttpPost("submit")]
        public ActionResult SubmitRating(RateProf rating)
        {
            _service.SubmitRating(rating);
            return Ok();
        }
    }
}
