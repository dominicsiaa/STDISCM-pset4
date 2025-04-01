using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Classify.Services
{
    public class CourseService
    {
        private readonly HttpClient _httpClient;
        public CourseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Course>> GetAvailableCourses()
        {
            return await _httpClient.GetFromJsonAsync<List<Course>>("courses");
        }
        public async Task<HttpResponseMessage> EnrollStudent(EnrollStudentRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("courses/enroll", request);
            return response.EnsureSuccessStatusCode();
        }
    }

    public class Course
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int Units { get; set; }
        public int Capacity { get; set; }
        public int InstructorId { get; set; }
        public List<int> StudentIds { get; set; }
    }

    public class EnrollStudentRequest
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
    }
}
