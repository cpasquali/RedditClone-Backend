namespace TP_FINAL_LABO_BACKEND.Models.Comment.Dto
{
    public class CreateCommentDto
    {
        public int UserId { get; set; }
        public int IdPost { get; set; }
        public string Content { get; set; } = null!;
    }
}
