namespace Classify.Services
{
    public class AccessTokenService
    {
        private readonly CookieService _cookieService;
        public AccessTokenService(CookieService cookieService)
        {
            _cookieService = cookieService;
        }

        public async Task SetToken(string token)
        {
            await _cookieService.Set("access_token", token, 1);
        }

        public async Task<string> GetToken()
        {
            return await _cookieService.Get("access_token");
        }

        public async Task RemoveToken()
        {
            await _cookieService.Remove("access_token");
        }
    }
}
