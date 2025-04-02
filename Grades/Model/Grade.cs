using System.ComponentModel.DataAnnotations;

namespace Grades.Model
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int TeacherId { get; set; }
        [Required]
        public double Score { get; set; }
        public DateTime DateRecorded { get; set; } = DateTime.Now;
    }
}
