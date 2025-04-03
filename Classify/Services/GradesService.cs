using System.ComponentModel.DataAnnotations;
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

        public async Task<HttpResponseMessage> AddGrade(Grade grade)
        {
            var response = await _apiService.PostAsync(MicroserviceNames.GradesAPI.GetName(), "grades/add", grade);
            return response;
        }

        public async Task<List<Grade>> GetStudentGrades(int instructorId)
        {
            var response = await _apiService.GetAsync(MicroserviceNames.GradesAPI.GetName(), $"grades/instructor?instructorId={instructorId}");
            if (response.IsSuccessStatusCode)
            {
                var grades = JsonConvert.DeserializeObject<List<Grade>>(await response.Content.ReadAsStringAsync());
                return grades;
            }
            else
            {
                return new List<Grade>();
            }
        }

        public async Task<List<Grade>> GetGradesOfStudent(int studentId)
        {
            var response = await _apiService.GetAsync(MicroserviceNames.GradesAPI.GetName(), $"grades/student?studentId={studentId}");
            if (response.IsSuccessStatusCode)
            {
                var grades = JsonConvert.DeserializeObject<List<Grade>>(await response.Content.ReadAsStringAsync());
                return grades;
            }
            else
            {
                return new List<Grade>();
            }
        }
    }

    public class GradeRequest
    {
        public string CourseCode { get; set; }
        public int StudentId { get; set; }
        public int InstructorId { get; set; }
        [Range(0.0, 4.0, ErrorMessage = "Must be between 0.0 to 4.0")]
        public double Gpa { get; set; }
    }
}
