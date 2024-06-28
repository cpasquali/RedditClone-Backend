using TP_FINAL_LABO_BACKEND.Models.User;
using TP_FINAL_LABO_BACKEND.Models.User.Dto;

namespace TP_FINAL_LABO_BACKEND.Models.Auth.Dto
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = null!;
        public UserLoginResponseDto User { get; set; } = null!;
    }
}
