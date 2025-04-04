using Microsoft.JSInterop;

namespace Classify.Services
{
    public class CookieService
    {
        private readonly IJSRuntime _jsRuntime;
        public CookieService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }
        public async Task<string> Get(string key)
        {
            return await _jsRuntime.InvokeAsync<string>("getCookie", key);
        }
        public async Task Remove(string key)
        {
            await _jsRuntime.InvokeAsync<string>("deleteCookie", key);
        }
        public async Task Set(string key, string value, int days)
        {
            await _jsRuntime.InvokeAsync<string>("setCookie", key, value, days);
        }
    }
}
