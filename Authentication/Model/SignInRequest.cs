using System.ComponentModel.DataAnnotations;

namespace Authentication.Model
{
    public class SignInRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }

    }
}
