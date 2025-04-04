namespace Classify.Model
{
    public class EnrollStudentRequest
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public string CourseCode { get; set; } = string.Empty;
    }
}
