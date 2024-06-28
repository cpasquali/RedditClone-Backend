using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TP_FINAL_LABO_BACKEND.Models.User;
using TP_FINAL_LABO_BACKEND.Models.Comment;
using TP_FINAL_LABO_BACKEND.Models.Comment.Dto;

namespace TP_FINAL_LABO_BACKEND.Models.Post
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPost { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User.User User { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedPost { get; set; } = DateTime.Now;
        public DateTime? UpdatedPost { get; set; }
        public DateTime? DeletedPost { get; set; }
        public ICollection<TP_FINAL_LABO_BACKEND.Models.Comment.Comment> Comments { get; set; }
    }
}