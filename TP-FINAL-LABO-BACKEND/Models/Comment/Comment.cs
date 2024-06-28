using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TP_FINAL_LABO_BACKEND.Models.Comment
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdComment { get; set; }

        [ForeignKey("UserId")]
        public User.User User { get; set; } = null!;
        public int UserId { get; set; }
        [ForeignKey("IdPost")]
        public Post.Post Post { get; set; } = null!;
        public int IdPost { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedComment { get; set; } = DateTime.Now;
        public DateTime? UpdatedComment { get; set; }
        public DateTime? DeletedComment { get; set; }
    }
}
