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
            try
            {
                var response = await _apiService.GetAsync(MicroserviceNames.AuthenticationAPI.GetName(), "users");
                if (!response.IsSuccessStatusCode)
                { 
                    throw new Exception("Failed to get users");
                }                   
                var allUsers = JsonConvert.DeserializeObject<List<User>>(await response.Content.ReadAsStringAsync());
                return allUsers?.Where(u => u.Role == "Teacher").ToList() ?? new();

            }
            catch
            { 
                throw new Exception("Auth API is unavailable");

            }
            
        }
        public async Task<HttpResponseMessage> SubmitRating(RateProf rating)
        {
            try
            {
                return await _apiService.PostAsync(MicroserviceNames.RateProfAPI.GetName(), "ratings/submit", rating);
            }
            catch (Exception ex)
            { 
                throw new Exception("RateProf API is unavailable.");
            }
        }

        public async Task<List<RateProf>> GetMyRatings(int studentId)
        {
            try
            {
                var response = await _apiService.GetAsync(MicroserviceNames.RateProfAPI.GetName(), $"ratings/student/{studentId}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to retrieve ratings.");
                }

                return JsonConvert.DeserializeObject<List<RateProf>>(await response.Content.ReadAsStringAsync()) ?? new();
            }
            catch (Exception ex)
            {
                // Optional: log ex.Message here
                throw new Exception("RateProf API is unavailable.");
            }
        }

        public async Task<List<RateProf>> GetRatingsByProfessor(int instructorId)
        {
            try
            {
                var response = await _apiService.GetAsync(MicroserviceNames.RateProfAPI.GetName(), $"ratings/professor/{instructorId}");
                if (!response.IsSuccessStatusCode) return new();

                return JsonConvert.DeserializeObject<List<RateProf>>(await response.Content.ReadAsStringAsync()) ?? new();
            }
            catch
            {
                return new(); 
            }
        }


    }
}

