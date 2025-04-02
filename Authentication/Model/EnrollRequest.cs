using System.ComponentModel.DataAnnotations;

namespace Authentication.Model
{
    public class EnrollRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string CourseCode { get; set; }
    }
}
