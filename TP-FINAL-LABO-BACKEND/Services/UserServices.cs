using AutoMapper;
using System.Net;
using System.Web.Http;
using TP_FINAL_LABO_BACKEND.Models.User.Dto;
using TP_FINAL_LABO_BACKEND.Models.User;
using TP_FINAL_LABO_BACKEND.Repositories;
using TP_FINAL_LABO_BACKEND.Models.Role;

namespace TP_FINAL_LABO_BACKEND.Services
{
    public class UserServices
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IEncoderService _encoderService;

        public UserServices(IMapper mapper, IUserRepository userRepository, IEncoderService encoderService)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _encoderService = encoderService;
        }

        public async Task<List<UsersDto>> GetAll()
        {
            var users = await _userRepository.GetAll();
            return _mapper.Map<List<UsersDto>>(users);
        }

        public async Task<User> GetOneByIdOrException(int id)
        {
            var user = await _userRepository.GetOne(u => u.UserId == id);
            if(user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return user;
        }

        public async Task<UserDto> GetOneById(int id)
        {
            var user = await GetOneByIdOrException(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<User> GetOneByUsernameOrEmail(string? username, string? email)
        {
            User user;

            if (email != null)
            {
                user = await _userRepository.GetOne(u => u.Email == email);
            }
            else if (username != null)
            {
                user = await _userRepository.GetOne(u => u.Username == username);
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            return user;
        }

        public async Task<UserDto> CreateOne(CreateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);

            user.PasswordUser = _encoderService.Encode(user.PasswordUser);

            await _userRepository.Add(user);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateOneById(int id, UpdateUserDto updateUserDto)
        {
            var user = await GetOneByIdOrException(id);

            var updated = _mapper.Map(updateUserDto, user);

            return _mapper.Map<UserDto>(await _userRepository.Update(updated));
        }

        public async Task DeleteOneById(int id)
        {
            var user = await GetOneByIdOrException(id);
            await _userRepository.Delete(user);
        }

        public async Task<User> UpdateRolesById(int id, List<Role> roles)
        {
            var user = await GetOneByIdOrException(id);

            user.Roles = roles;

            return await _userRepository.Update(user);
        }

        public async Task<List<Role>> GetRolesById(int id)
        {
            var user = await GetOneByIdOrException(id);

            return user.Roles.ToList();
        }

        public async Task ChangePassword(int userId, ChangePasswordDto changePasswordDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (!_encoderService.Verify(changePasswordDto.OldPassword, user.PasswordUser))
            {
                throw new Exception("Old password is incorrect");
            }

            user.PasswordUser = _encoderService.Encode(changePasswordDto.NewPassword);
            await _userRepository.UpdateAsync(user);
        }
    }
}
