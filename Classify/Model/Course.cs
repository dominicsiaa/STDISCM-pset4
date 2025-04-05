using System.ComponentModel.DataAnnotations;

namespace Classify.Model
{
    public class Course
    {
        public int Id { get; set; }
        [StringLength(10, ErrorMessage = "Course name cannot be longer than 10 characters")]
        public string Code { get; set; }
        [Range(0, 9, ErrorMessage = "Courses can only be 0 to 9 units")]
        public int Units { get; set; }
        [Range(1, 100, ErrorMessage = "Courses can only have 1 to 100 students")]
        public int Capacity { get; set; }
        public int InstructorId { get; set; }
        public List<int> StudentIds { get; set; }

        public Course()
        {
            this.Units = 0;
            this.Capacity = 1;
            this.StudentIds = new List<int>();
        }
        public bool isEnrolled(int studentId)
        {
            return this.StudentIds.Contains(studentId);
        }
    }
}
