using System.ComponentModel.DataAnnotations;

namespace TP_FINAL_LABO_BACKEND.Models.User.Dto
{
    public class UpdateUserDto
    {
        [MaxLength(128)]
        public string? NameUser { get; set; }

        public string? Username { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        public DateTime? Birthdate { get; set; }
    }
}
