using KYC_apllication_2.DTOs;
using KYC_apllication_2.Repositories;
using System.Threading.Tasks;

namespace KYC_apllication_2.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> RegisterUserAsync(UserRegisterDto user)
        {
            // Ensure that user.Email is not null or empty
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                throw new ArgumentException("Email cannot be null or empty");
            }

            return await _userRepository.RegisterUserAsync(user);
        }


        public async Task<bool> ValidateUserCredentialsAsync(string username, string password)
        {
            return await _userRepository.ValidateUserCredentialsAsync(username, password);
        }

        public async Task<string> GenerateJwtTokenAsync(string username)
        {
            return await _userRepository.GenerateJwtTokenAsync(username);
        }

        public async Task<bool> UpdateAdminProfileAsync(AdminProfileDto adminProfileDto)
        {
            return await _userRepository.UpdateAdminProfileAsync(adminProfileDto);
        }

        public async Task<bool> ChangeAdminPasswordAsync(ChangePasswordDto changePasswordDto)
        {
            return await _userRepository.ChangeAdminPasswordAsync(changePasswordDto);
        }
    }
}
