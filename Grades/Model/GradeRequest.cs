using System.ComponentModel.DataAnnotations;

namespace Grades.Model
{
    public class GradeRequest
    {
        [Required]
        public string CourseCode { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int InstructorId { get; set; }
        [Required]
        public double Gpa { get; set; }
    }
}
