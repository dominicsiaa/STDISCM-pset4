using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Classify.Security
{
    public class CustomOption : AuthenticationSchemeOptions
    {

    }

    public class JWTAuthenticationProvider : AuthenticationHandler<CustomOption>
    {
        public JWTAuthenticationProvider(IOptionsMonitor<CustomOption> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                var token = Request.Cookies["access_token"];
                Logger.LogInformation(token);

                if (string.IsNullOrEmpty(token))
                {
                    Logger.LogWarning("JWT Authentication failed: No token found in cookies.");
                    return AuthenticateResult.NoResult();
                }

                var readJWT = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var identity = new ClaimsIdentity(readJWT.Claims, "JWT");
                var principal = new ClaimsPrincipal(identity);

                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                Logger.LogInformation("JWT Authentication successful for user: {User}", identity.Name);

                return AuthenticateResult.Success(ticket);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "JWT Authentication failed: {Message}", e.Message);
                return AuthenticateResult.Fail("Invalid token.");
            }
        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.Redirect("/login");
            return Task.CompletedTask;
        }

        protected override Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            Response.Redirect("/access-denied");
            return Task.CompletedTask;
        }
    }
}
