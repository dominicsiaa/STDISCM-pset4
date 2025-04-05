using System.Security.Claims;
using Classify.Model;
using Classify.Security;

namespace Classify.Services
{
    public class UserService
    {
        private readonly JWTStateProvider _stateProvider;

        public UserService(JWTStateProvider stateProvider)
        {
            _stateProvider = stateProvider;
        }

        public async Task<User?> GetUserAsync()
        {
            try
            {
                var authState = await _stateProvider.GetAuthenticationStateAsync();
                var user = authState.User;

                if (user?.Identity?.IsAuthenticated == true)
                {
                    var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                    var usernameClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
                    var roleClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

                    return new User
                    {
                        Id = userIdClaim != null ? int.Parse(userIdClaim.Value) : throw new InvalidOperationException("User ID claim not found."),
                        Username = usernameClaim?.Value ?? string.Empty,
                        Role = roleClaim?.Value ?? string.Empty
                    };
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
