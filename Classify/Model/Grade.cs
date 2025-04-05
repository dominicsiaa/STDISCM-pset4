using System.ComponentModel.DataAnnotations;

namespace Classify.Model
{
    public class Grade
    {
        public int Id { get; set; }
        public string CourseCode { get; set; }
        public int StudentId { get; set; }
        public int InstructorId { get; set; }
        [Range(0.0, 4.0, ErrorMessage = "GPA must be between 0.0 and 4.0")]
        public double Gpa { get; set; }
        public int Units { get; set; }
        public DateTime DateRecorded { get; set; } = DateTime.Now;
    }
}
