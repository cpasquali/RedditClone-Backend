namespace TP_FINAL_LABO_BACKEND.Models.Post.Dto
{
    public class PostDto
    {
        public int IdPost { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedPost { get; set; } = DateTime.Now;
    }
}
