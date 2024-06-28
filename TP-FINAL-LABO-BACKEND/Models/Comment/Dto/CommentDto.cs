namespace TP_FINAL_LABO_BACKEND.Models.Comment.Dto
{
    public class CommentDto
    {
        public int IdComment { get; set; }
        public int UserId { get; set; }
        public int IdPost { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedComment { get; set; } = DateTime.Now;
    }
}
