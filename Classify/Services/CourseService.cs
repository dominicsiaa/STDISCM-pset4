using Classify.Common;
using Classify.Model;
using Newtonsoft.Json;

namespace Classify.Services
{
    public class CourseService
    {
        private readonly APIService _apiService;
        public CourseService(APIService apiService)
        {
            _apiService = apiService;
        }
        public async Task<List<Course>> GetAllCourses()
        {
            var response = await _apiService.GetAsync(MicroserviceNames.EnrollmentAPI.GetName(), "courses");
            if (response.IsSuccessStatusCode)
            {
                var courses = JsonConvert.DeserializeObject<List<Course>>(await response.Content.ReadAsStringAsync());
                return courses;
            }
            else
            {
                return new List<Course>();
            }
        }
        public async Task<HttpResponseMessage> EnrollStudent(EnrollStudentRequest request)
        {
            var response = await _apiService.PostAsync(MicroserviceNames.EnrollmentAPI.GetName(), "courses/enroll", request);
            return response;
        }
        public async Task<HttpResponseMessage> AddCourse(Course course)
        {
            var response = await _apiService.PostAsync(MicroserviceNames.EnrollmentAPI.GetName(), "courses/add", course);
            return response;
        }
    }
}
