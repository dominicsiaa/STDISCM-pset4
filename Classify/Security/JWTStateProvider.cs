using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Components.Authorization;
using Classify.Services;

namespace Classify.Security
{
    public class JWTStateProvider : AuthenticationStateProvider
    {
        private readonly AccessTokenService _accessTokenService;
        public JWTStateProvider(AccessTokenService accessTokenService)
        {
            _accessTokenService = accessTokenService;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var token = await _accessTokenService.GetToken();
                if (string.IsNullOrWhiteSpace(token))
                {
                    return await MarkAsUnauthorized();
                }

                var readJWT = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var identity = new ClaimsIdentity(readJWT.Claims, "JWT");
                var principal = new ClaimsPrincipal(identity);

                return await Task.FromResult(new AuthenticationState(principal));
            }
            catch (Exception e)
            {
                return await MarkAsUnauthorized();
            }
        }
        private async Task<AuthenticationState> MarkAsUnauthorized()
        {
            try
            {
                var state = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                NotifyAuthenticationStateChanged(Task.FromResult(state));

                return state;
            }
            catch
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }
    }
}
