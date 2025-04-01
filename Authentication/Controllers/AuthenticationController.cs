using Authentication.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Authentication.Model;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly DataAccess dataAccess;
        private readonly TokenProvider tokenProvider;
        public AuthenticationController(DataAccess dataAccess, TokenProvider tokenProvider) { 
            this.dataAccess = dataAccess;
            this.tokenProvider = tokenProvider;
        }

        [HttpPost("signin")]
        public ActionResult SignIn(SignInRequest request)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var result = dataAccess.RegisterUser(request.Username, hashedPassword, request.Role);
            if(!result)
            {
                return BadRequest("Sign in failed");
            }

            return Ok();
        }

        [HttpPost("login")]
        public ActionResult<AuthResponse> LogIn(LoginRequest request)
        {
            AuthResponse response = new AuthResponse();

            var user = dataAccess.GetUser(request.Username);
            if (user == null)
            {
                return BadRequest("Username or password is incorrect");
            }

            var checkPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!checkPassword)
            {
                return BadRequest("Username or password is incorrect");
            }

            var token = tokenProvider.GenerateToken(user);
            response.AccessToken = token.AccessToken;

            return response;
        }

    }
}
