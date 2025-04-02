using System.ComponentModel.DataAnnotations;

namespace Enrollment.Model
{
    public class EnrollStudentRequest
    {
        [Required]
        public int CourseId { get; set; }
        [Required]
        public int StudentId { get; set; }
    }
}
