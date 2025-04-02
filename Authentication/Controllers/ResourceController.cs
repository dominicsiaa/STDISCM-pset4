using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// DONT MIND THIS, WILL BE DELETED, JUST FOR TESTING

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        [Route("verify")]

        public ActionResult Verify()
        {
            return Ok("You are authorized");
        }
    }
}
