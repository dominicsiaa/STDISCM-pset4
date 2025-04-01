﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
