using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TP_FINAL_LABO_BACKEND.Models.Auth;
using TP_FINAL_LABO_BACKEND.Models.Auth.Dto;
using TP_FINAL_LABO_BACKEND.Models.Role;
using TP_FINAL_LABO_BACKEND.Models.Role.Dto;
using TP_FINAL_LABO_BACKEND.Models.User;
using TP_FINAL_LABO_BACKEND.Models.User.Dto;
using TP_FINAL_LABO_BACKEND.Services;

namespace TP_FINAL_LABO_BACKEND.Controllers
{
    [Route("api/auth")]

    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthServices _authServices;
        private readonly UserServices _userServices;
        private readonly RoleServices _roleServices;
        private readonly IEncoderService _encoderServices;
        private readonly IMapper _mapper;

        public AuthController(AuthServices authServices, UserServices userServices, RoleServices roleServices, IEncoderService encoderServices, IMapper mapper)
        {
            _authServices = authServices;
            _userServices = userServices;
            _roleServices = roleServices;
            _encoderServices = encoderServices;
            _mapper = mapper;
        }


        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] Login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (string.IsNullOrEmpty(login.Email) && string.IsNullOrEmpty(login.Username))
                {
                    ModelState.AddModelError("Error", "Credentials are incorrect");
                    return BadRequest(ModelState);
                }

                if (string.IsNullOrEmpty(login.Password))
                {
                    ModelState.AddModelError("credentials", "Password is required");
                    return BadRequest(ModelState);
                }

                var user = await _userServices.GetOneByUsernameOrEmail(login.Username, login.Email);

                if (user == null)
                {
                    ModelState.AddModelError("Credentials", "Credentials are incorrect");
                    return BadRequest(ModelState);
                }

                var passwordMatch = _encoderServices.Verify(login.Password, user.PasswordUser);

                if (!passwordMatch)
                {
                    ModelState.AddModelError("Credentials", "Credentials are incorrect");
                    return BadRequest(ModelState);
                }

                var token = _authServices.GenerateJwtToken(user);

                return Ok(new LoginResponseDto
                {
                    Token = token,
                    User = _mapper.Map<UserLoginResponseDto>(user)
                });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }



        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<UserDto>> Register([FromBody] CreateUserDto register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = await _userServices.GetOneByUsernameOrEmail(register.Username, register.Email);

                if (user != null)
                {
                    ModelState.AddModelError("Error", "User already exists");
                    return BadRequest(ModelState);
                }

                var userCreated = await _userServices.CreateOne(register);

                var defaultRole = await _roleServices.GetByName("User");

                await _userServices.UpdateRolesById(userCreated.UserId, new List<Role> { defaultRole });

                return Created("RegisterUser", userCreated);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("roles/user/{id}")]  
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<User>> Put(int id, [FromBody] UpdateUserRoleDto updateUserRolesDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var roles = await _roleServices.GetByIds(updateUserRolesDto.RoleIds);
                var userUpdated = await _userServices.UpdateRolesById(id, roles);
                return Ok(userUpdated);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
