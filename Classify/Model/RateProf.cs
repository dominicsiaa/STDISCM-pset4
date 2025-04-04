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
        public int Score { get; set; }
        public string Comment { get; set; } = "";

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

    }
}
