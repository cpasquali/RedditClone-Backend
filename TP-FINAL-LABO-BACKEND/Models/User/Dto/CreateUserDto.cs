using System.ComponentModel.DataAnnotations;

namespace TP_FINAL_LABO_BACKEND.Models.User.Dto
{
    public class CreateUserDto
    {
        [Required]
        [MaxLength(128)]
        public string NameUser { get; set; } = null!;

        [Required]
        public string Username { get; set; } = null!;

        [Required]
        [MinLength(6)]
        public string PasswordUser { get; set; } = null!;

        [EmailAddress]
        public string? Email { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
