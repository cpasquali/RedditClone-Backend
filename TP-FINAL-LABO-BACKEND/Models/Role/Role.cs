using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TP_FINAL_LABO_BACKEND.Models.Role
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRole { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string NameRole { get; set; } = null!;
    }

    public class RoleUsers
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
    }
}
