using KYC_apllication_2.Entity;

public interface IUserService
{
    Task<bool> RegisterUserAsync(UserRegisterDto user);
    Task<bool> ValidateUserCredentialsAsync(string username, string password);
    Task<(string Token, int UserId, string Role)> GenerateJwtTokenAsync(string username); // Updated return type

    Task<User> GetUserByIdAsync(int id);
    Task<List<User>> GetAllUSerByRoleAsync(string role);
    Task<bool> UpdateUserAsync(User user);

    Task<bool> UpdateAdminProfileAsync(int userId, string username, string password);
    Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword);

    // Add the following methods
    Task<bool> CheckIfUserExistsAsync(string username);
    Task<bool> ResetPasswordAsync(string username, string newPassword);
}
