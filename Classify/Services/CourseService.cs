using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Classify.Services
{
    public class CourseService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CourseService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Course>> GetAllCourses()
        {
            return await _httpClient.GetFromJsonAsync<List<Course>>("courses");
        }
        public async Task<HttpResponseMessage> EnrollStudent(EnrollStudentRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("courses/enroll", request);
            return response.EnsureSuccessStatusCode();
        }

        public async Task<HttpResponseMessage> AddCourse(Course course)
        {
            var response = await _httpClient.PostAsJsonAsync("courses/add", course);
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

        public Course()
        {
            StudentIds = new List<int>();
        }

        public bool isEnrolled(int studentId)
        {
            return StudentIds.Contains(studentId);
        }
    }

    public class EnrollStudentRequest
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
    }
}
