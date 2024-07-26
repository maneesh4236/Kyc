using KYC_apllication_2.DTOs;
using System.Threading.Tasks;

namespace KYC_apllication_2.Services
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(UserRegisterDto user);
        Task<bool> ValidateUserCredentialsAsync(string username, string password);
        Task<string> GenerateJwtTokenAsync(string username);
        Task<bool> UpdateAdminProfileAsync(AdminProfileDto adminProfileDto);
        Task<bool> ChangeAdminPasswordAsync(ChangePasswordDto changePasswordDto);
    }
}
