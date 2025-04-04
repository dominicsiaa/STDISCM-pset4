using Authentication.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Authentication.Model;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize]
        [HttpGet("users")]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            return Ok(dataAccess.GetAllUsers());
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
            response.RefreshToken = token.RefreshToken.Token;

            dataAccess.DisableUserTokensByUsername(user.Username);
            dataAccess.InsertRefreshToken(token.RefreshToken, user.Id);

            return Ok(response);
        }

        [HttpPost("refresh")]
        public ActionResult<AuthResponse> RefreshToken()
        {
            AuthResponse response = new AuthResponse();

            var refreshToken = Request.Cookies["refreshtoken"];
            if (string.IsNullOrEmpty(refreshToken))
            {
                return BadRequest("Refresh token is required");
            }

            var isValid = dataAccess.IsRefreshTokenValid(refreshToken);
            if (!isValid)
            {
                return BadRequest("Refresh token is invalid");
            }

            var user = dataAccess.FindUserByToken(refreshToken);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            var token = tokenProvider.GenerateToken(user);
            response.AccessToken = token.AccessToken;
            response.RefreshToken = token.RefreshToken.Token;

            dataAccess.DisableUserToken(refreshToken);
            dataAccess.InsertRefreshToken(token.RefreshToken, user.Id);
            return Ok(response);
        }

        [HttpPost("logout")]
        public ActionResult LogOut()
        {
            var refreshToken = Request.Cookies["refreshtoken"];
            if (refreshToken != null)
            {
                dataAccess.DisableUserToken(refreshToken);
            }

            return Ok();
        }

    }
}
