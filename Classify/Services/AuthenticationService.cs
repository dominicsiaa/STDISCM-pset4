using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Classify.Model;
using Classify.Common;

namespace Classify.Services
{
    public class AuthenticationService
    {
        private readonly AccessTokenService _accessTokenService;
        private readonly RefreshTokenService _refreshTokenService;
        private readonly NavigationManager _nav;
        private readonly HttpClient _client;
        private readonly ILogger<AuthenticationService> _logger;
        public AuthenticationService(AccessTokenService accessTokenService,
                                     NavigationManager nav,
                                     IHttpClientFactory client,
                                     ILogger<AuthenticationService> logger,
                                     RefreshTokenService refreshTokenService)
        {
            _accessTokenService = accessTokenService;
            _nav = nav;
            _client = client.CreateClient(MicroserviceNames.AuthenticationAPI.GetName());
            _logger = logger;
            _refreshTokenService = refreshTokenService;
        }
        public async Task<bool> SignUp(string username, string password, string role)
        {
            _logger.LogInformation($"Attempting to sign up user: {username} with role: {role}");
            if (role != "Student" && role != "Teacher")
            {
                _logger.LogWarning("Invalid role provided. Role must be either 'Student' or 'Teacher'.");
                return false;
            }

            try
            {
                var response = await _client.PostAsJsonAsync("signin", new { username, password, role });
                _logger.LogInformation($"Received response: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Sign-up successful. Navigating to login page.");
                    _nav.NavigateTo("/login");
                    return true;
                }
                else
                {
                    _logger.LogWarning($"Sign-up failed with status code: {response.StatusCode}");
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning($"Error response: {errorResponse}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred during sign-up: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> Login(string username, string password)
        {
            _logger.LogInformation($"Attempting to log in user: {username}");

            try
            {
                var response = await _client.PostAsJsonAsync("login", new { username, password });

                _logger.LogInformation($"Received response: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation($"Login successful, token received: {token.Substring(0, Math.Min(20, token.Length))}...");

                    var result = JsonConvert.DeserializeObject<AuthResponse>(token);
                    await _accessTokenService.SetToken(result.AccessToken);
                    await _refreshTokenService.SetToken(result.RefreshToken);

                    _nav.NavigateTo("/");
                    return true;
                }
                else
                {
                    _logger.LogWarning($"Login failed with status code: {response.StatusCode}");
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning($"Error response: {errorResponse}");

                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred during login: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> RefreshTokenAsync()
        {
            var refreshToken = await _refreshTokenService.GetToken();
            _client.DefaultRequestHeaders.Add("Cookie", $"refreshtoken={refreshToken}");
            var response = await _client.PostAsync("refresh", null);
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                if(!string.IsNullOrEmpty(token))
                {
                    var result = JsonConvert.DeserializeObject<AuthResponse>(token);
                    await _accessTokenService.SetToken(result.AccessToken);
                    await _refreshTokenService.SetToken(result.RefreshToken);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public async Task Logout()
        {
            _logger.LogInformation("Logging out user.");

            var refreshToken = await _refreshTokenService.GetToken();
            _client.DefaultRequestHeaders.Add("Cookie", $"refreshtoken={refreshToken}");
            var response = await _client.PostAsync("logout", null);
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Logout successful.");
                await _accessTokenService.RemoveToken();
                await _refreshTokenService.RemoveToken();
                _nav.NavigateTo("/login", forceLoad: true);
            }
            else
            {
                _logger.LogWarning($"Logout failed with status code: {response.StatusCode}");
            }
        }
    }
}
