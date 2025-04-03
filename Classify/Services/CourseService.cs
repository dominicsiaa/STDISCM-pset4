using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using Classify.Common;
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

        public async Task<HttpResponseMessage> AddCourseToStudent(EnrollCourseRequest request)
        {
            var response = await _apiService.PostAsync(MicroserviceNames.AuthenticationAPI.GetName(), "enroll", request);
            return response;
        }
    }

    public class Course
    {
        public int Id { get; set; }
        public string Code { get; set; }
        [Range(0, 9, ErrorMessage = "Courses can only be 0 to 9 units")]
        public int Units { get; set; }
        [Range(1, 100, ErrorMessage = "Courses can only have 1 to 100 students")]
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

    public class EnrollCourseRequest
    {
        public string Username { get; set; }
        public string CourseCode { get; set; }
    }
}
