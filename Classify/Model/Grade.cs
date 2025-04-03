using System.ComponentModel.DataAnnotations;

namespace Classify.Model
{
    public class Grade
    {
        public int Id { get; set; }
        public string CourseCode { get; set; }
        public int StudentId { get; set; }
        public int InstructorId { get; set; }
        public double Gpa { get; set; }
        public DateTime DateRecorded { get; set; } = DateTime.Now;
    }
}
