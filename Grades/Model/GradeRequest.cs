using System.ComponentModel.DataAnnotations;

namespace Grades.Model
{
    public class GradeRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int TeacherId { get; set; }
        [Required]
        public double Score { get; set; }
    }
}
