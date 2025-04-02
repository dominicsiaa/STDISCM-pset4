using System.ComponentModel.DataAnnotations;

namespace Authentication.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = string.Empty;
        [Required]
        public List<string> Courses { get; set; } = new List<string>();
    }
}
