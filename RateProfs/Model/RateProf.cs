using System.ComponentModel.DataAnnotations;

namespace RateProfs.Model
{
    public class RateProf
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }
        [Required]
        public string StudentUsername { get; set; } = "";

        [Required]
        public int InstructorId { get; set; }
        [Required]
        public string InstructorUsername { get; set; } = "";

        public string CourseCode { get; set; } = "N/A";
        public string CourseTitle { get; set; } = "N/A";

        [Range(1, 5)]
        public int Score { get; set; }

        [MaxLength(500)]
        public string Comment { get; set; } = "";

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }
}
