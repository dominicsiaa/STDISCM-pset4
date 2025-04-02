using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Classify.Services
{
    public class RefreshTokenService
    {
        private readonly ProtectedLocalStorage _protectedLocalStorage;
        public RefreshTokenService(ProtectedLocalStorage protectedLocalStorage)
        {
            _protectedLocalStorage = protectedLocalStorage;
        }

        public async Task SetToken(string token)
        {
            await _protectedLocalStorage.SetAsync("refresh_token", token);
        }

        public async Task<string> GetToken()
        {
            var result = await _protectedLocalStorage.GetAsync<string>("refresh_token");
            if(result.Success)
            {
                return result.Value;
            }

            return string.Empty;
        }

        public async Task RemoveToken()
        {
            await _protectedLocalStorage.DeleteAsync("refresh_token");
        }
    }
}
