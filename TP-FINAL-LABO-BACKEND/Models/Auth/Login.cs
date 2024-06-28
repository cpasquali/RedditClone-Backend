using System.ComponentModel.DataAnnotations;

namespace TP_FINAL_LABO_BACKEND.Models.Auth
{
    public class Login
    {
        public string? Username { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string Password { get; set; } = null!;
    }
}
