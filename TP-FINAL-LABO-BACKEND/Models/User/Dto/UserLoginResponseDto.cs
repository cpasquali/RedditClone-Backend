namespace TP_FINAL_LABO_BACKEND.Models.User.Dto
{
    public class UserLoginResponseDto
    {
        public int UserId { get; set; }
        public string NameUser { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string? Email { get; set; }
        public DateTime Birthdate { get; set; }

         public List<string> Roles { get; set; } = null!;
    }
}
