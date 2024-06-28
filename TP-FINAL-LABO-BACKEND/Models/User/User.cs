using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TP_FINAL_LABO_BACKEND.Models.User
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

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
        public DateTime CreatedUser { get; set; } = DateTime.Now;
        public DateTime Birthdate { get; set; }
        public List<Role.Role> Roles { get; set; } = null!;
    }
}
