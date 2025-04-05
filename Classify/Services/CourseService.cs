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
            try
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
            catch (Exception ex)
            {
                // Handle exceptions as needed
                return new List<Course>();
            }
        }
        public async Task<HttpResponseMessage> EnrollStudent(EnrollStudentRequest request)
        {
            try
            {
                var response = await _apiService.PostAsync(MicroserviceNames.EnrollmentAPI.GetName(), "courses/enroll", request);
                return response;
            }
            catch
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        public async Task<HttpResponseMessage> AddCourse(Course course)
        {
            try
            {
                var response = await _apiService.PostAsync(MicroserviceNames.EnrollmentAPI.GetName(), "courses/add", course);
                return response;
            }
            catch
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
