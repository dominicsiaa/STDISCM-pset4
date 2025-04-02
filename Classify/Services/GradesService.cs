using Classify.Common;
using Classify.Model;
using Newtonsoft.Json;

namespace Classify.Services
{
    public class GradesService
    {
        private readonly APIService _apiService;

        public GradesService(APIService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<Grade>> GetGrades()
        {
            var response = await _apiService.GetAsync(MicroserviceNames.GradesAPI.GetName(), "");
            if (response.IsSuccessStatusCode)
            {
                var grades = JsonConvert.DeserializeObject<List<Grade>>(await response.Content.ReadAsStringAsync());
                return grades;
            }
            else
            {
                return [];
            }
        }
    }
}
