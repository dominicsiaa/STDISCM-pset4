using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.Model
{
    public class RefreshToken
    {
        [Key]  // Token is the primary key
        [Required]
        public string Token { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime Expires { get; set; }
        public bool Enabled { get; set; }

        // Foreign Key Reference to User
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }  // Navigation Property
    }
}
