using System.Net;
using System.Net.Http.Headers;
using Grpc.Core;
using Microsoft.AspNetCore.Components;

namespace Classify.Services
{
    public class APIService
    {
        private readonly IHttpClientFactory _httpFactory;
        private readonly ILogger<APIService> _logger;
        private readonly NavigationManager _nav;
        private readonly AuthenticationService _authenticationService;
        private readonly AccessTokenService _accessTokenService;

        public APIService(IHttpClientFactory httpFactory, ILogger<APIService> logger, NavigationManager nav, AuthenticationService authenticationService, AccessTokenService accessTokenService)
        {
            _httpFactory = httpFactory;
            _logger = logger;
            _nav = nav;
            _authenticationService = authenticationService;
            _accessTokenService = accessTokenService;
        }
        private async Task<HttpClient> CreateClientAsync(string clientName)
        {
            var client = _httpFactory.CreateClient(clientName);
            var token = await _accessTokenService.GetToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }

        public async Task<HttpResponseMessage> GetAsync(string clientName, string url)
        {
            var client = await CreateClientAsync(clientName);
            var response = await client.GetAsync(url);

            if(response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _logger.LogWarning("Unauthorized request, attempting to refresh token");
                var success = await _authenticationService.RefreshTokenAsync();
                if (success)
                {
                    var newToken = await _accessTokenService.GetToken();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", newToken);
                    
                    var newResponse = await client.GetAsync(url);
                    return newResponse;
                }
                else
                {
                    _logger.LogWarning("Failed to refresh token, logging out");
                    await _authenticationService.Logout();
                }
            }

            return response;
        }

        public async Task<HttpResponseMessage> PostAsync(string clientName, string url, object data)
        {
            var client = await CreateClientAsync(clientName);
            var response = await client.PostAsJsonAsync(url, data);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _logger.LogWarning("Unauthorized request, attempting to refresh token");
                var success = await _authenticationService.RefreshTokenAsync();
                if (success)
                {
                    var newToken = await _accessTokenService.GetToken();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", newToken);

                    var newResponse = await client.PostAsJsonAsync(url, data);
                    return newResponse;
                }
                else
                {
                    _logger.LogWarning("Failed to refresh token, logging out");
                    await _authenticationService.Logout();
                }
            }
            return response;
        }
    }
}
