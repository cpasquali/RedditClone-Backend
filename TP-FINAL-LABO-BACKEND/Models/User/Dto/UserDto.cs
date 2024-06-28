namespace TP_FINAL_LABO_BACKEND.Models.User.Dto
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string NameUser { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string? Email { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
