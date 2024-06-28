using System.ComponentModel.DataAnnotations;

namespace TP_FINAL_LABO_BACKEND.Models.Post.Dto
{
    public class CreatePostDto
    {
        [Required(ErrorMessage = "The Title field is required.")]
        [MaxLength(100, ErrorMessage = "The Title field cannot exceed 100 characters.")]
        public string Title { get; set; } = null!;

        [MaxLength(255, ErrorMessage = "The Content field cannot exceed 255 characters.")]
        public string Content { get; set; } = null!;

        [Required(ErrorMessage = "The AuthorId field is required.")]
        public int UserId { get; set; }
    }
}
