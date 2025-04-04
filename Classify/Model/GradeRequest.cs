using System.ComponentModel.DataAnnotations;

namespace Classify.Model
{
    public class GradeRequest
    {
        public string CourseCode { get; set; }
        public int StudentId { get; set; }
        public int InstructorId { get; set; }
        [Range(0.0, 4.0, ErrorMessage = "Must be between 0.0 to 4.0")]
        public double Gpa { get; set; }
        public int Units { get; set; }
    }
}
