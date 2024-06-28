using System.ComponentModel.DataAnnotations;

namespace TP_FINAL_LABO_BACKEND.Models.Like
{

    public class Like
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PostId { get; set; }

        [Required]
        public int UserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
