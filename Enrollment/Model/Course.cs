using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Enrollment.Model
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; } = string.Empty;
        [Required]
        public float Units { get; set; }
        [Required]
        public int Capacity { get; set; }
        [Required]
        public int InstructorId { get; set; }
        [Required]
        public List<int> StudentIds { get; set; } = new List<int>();
    }
}
