using System.ComponentModel.DataAnnotations;

namespace Grades.Model
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CourseCode { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int InstructorId { get; set; }
        [Required]
        public double Gpa { get; set; }
        public DateTime DateRecorded { get; set; } = DateTime.Now;
    }
}
