using Classify.Common;
using Classify.Model;
using Newtonsoft.Json;

namespace Classify.Services
{
    public class RateProfService
    {
        private readonly APIService _apiService;

        public RateProfService(APIService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<User>> GetAllProfessors()
        {
            var response = await _apiService.GetAsync(MicroserviceNames.AuthenticationAPI.GetName(), "users");
            if (!response.IsSuccessStatusCode) return new();

            var allUsers = JsonConvert.DeserializeObject<List<User>>(await response.Content.ReadAsStringAsync());
            return allUsers?.Where(u => u.Role == "Teacher").ToList() ?? new();
        }

        public async Task<HttpResponseMessage> SubmitRating(RateProf rating)
        {
            return await _apiService.PostAsync(MicroserviceNames.RateProfAPI.GetName(), "ratings/submit", rating);
        }

        public async Task<List<RateProf>> GetMyRatings(int studentId)
        {
            var response = await _apiService.GetAsync(MicroserviceNames.RateProfAPI.GetName(), $"ratings/student/{studentId}");
            if (!response.IsSuccessStatusCode) return new();

            return JsonConvert.DeserializeObject<List<RateProf>>(await response.Content.ReadAsStringAsync()) ?? new();
        }

    }
}

