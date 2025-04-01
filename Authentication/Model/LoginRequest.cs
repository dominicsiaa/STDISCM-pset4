using System.ComponentModel.DataAnnotations;

namespace Authentication.Model
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
