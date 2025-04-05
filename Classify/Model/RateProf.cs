using System.ComponentModel.DataAnnotations;

namespace Classify.Model
{
    public class RateProf
    {
        public int StudentId { get; set; }
        public string StudentUsername { get; set; } = "";

        public int InstructorId { get; set; }
        public string InstructorUsername { get; set; } = "";
        public string CourseCode { get; set; } = "N/A"; // Static value for now
        public string CourseTitle { get; set; } = "N/A"; // Static value for now

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Score { get; set; }

        [StringLength(200, ErrorMessage = "Comment cannot exceed 200 characters.")]
        public string Comment { get; set; } = "";

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

    }
}
