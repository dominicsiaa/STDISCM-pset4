﻿using Classify.Common;
using Classify.Model;
using Newtonsoft.Json;

namespace Classify.Services
{
    public class GradeService
    {
        private readonly APIService _apiService;
        public GradeService(APIService apiService)
        {
            _apiService = apiService;
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
        public async Task<HttpResponseMessage> AddGrade(Grade grade)
        {
            var response = await _apiService.PostAsync(MicroserviceNames.GradesAPI.GetName(), "grades/add", grade);
            return response;
        }
    }
}
