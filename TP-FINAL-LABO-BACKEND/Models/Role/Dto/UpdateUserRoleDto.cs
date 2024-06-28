using System.ComponentModel.DataAnnotations;

namespace TP_FINAL_LABO_BACKEND.Models.Role.Dto
{
    public class UpdateUserRoleDto
    {
        [Required]
        public List<int> RoleIds { get; set; } = null!;
    }
}
