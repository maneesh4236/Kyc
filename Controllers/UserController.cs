using KYC_apllication_2.DTOs;
using KYC_apllication_2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace onlinekyc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto user)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid registration model state.");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Register method called with Username: {Username}", user.Username);

            var result = await _userService.RegisterUserAsync(user);
            if (!result)
            {
                _logger.LogWarning("User registration failed for Username: {Username}", user.Username);
                return BadRequest("User registration failed");
            }

            _logger.LogInformation("User registered successfully for Username: {Username}", user.Username);
            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid login model state.");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Login attempt for Username: {Username}", userLoginDto.Username);

            var validUser = await _userService.ValidateUserCredentialsAsync(userLoginDto.Username, userLoginDto.Password);
            if (!validUser)
            {
                _logger.LogWarning("Invalid credentials for Username: {Username}", userLoginDto.Username);
                return Unauthorized("Invalid credentials");
            }

            var (token, userId, role) = await _userService.GenerateJwtTokenAsync(userLoginDto.Username);
            if (token == null)
            {
                _logger.LogWarning("Failed to generate token for Username: {Username}", userLoginDto.Username);
                return Unauthorized("Failed to generate token");
            }

            _logger.LogInformation("Login successful for Username: {Username}", userLoginDto.Username);
            return Ok(new { token, username = userLoginDto.Username, userId, role });
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangeAdminPassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid change password model state.");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Change password request for UserId: {UserId}", changePasswordDto.UserId);

            var result = await _userService.ChangePasswordAsync(changePasswordDto.UserId, changePasswordDto.OldPassword, changePasswordDto.NewPassword);
            if (!result)
            {
                _logger.LogWarning("Password change failed for UserId: {UserId}", changePasswordDto.UserId);
                return BadRequest("Password change failed");
            }

            _logger.LogInformation("Password changed successfully for UserId: {UserId}", changePasswordDto.UserId);
            return Ok("Password changed successfully");
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid reset password model state.");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Forgot password request for Username: {Username}", resetPasswordDto.Username);

            var userExists = await _userService.CheckIfUserExistsAsync(resetPasswordDto.Username);
            if (!userExists)
            {
                _logger.LogWarning("User not found for Username: {Username}", resetPasswordDto.Username);
                return NotFound("User not found");
            }

            var result = await _userService.ResetPasswordAsync(resetPasswordDto.Username, resetPasswordDto.NewPassword);
            if (!result)
            {
                _logger.LogWarning("Password reset failed for Username: {Username}", resetPasswordDto.Username);
                return BadRequest("Password reset failed");
            }

            _logger.LogInformation("Password reset successfully for Username: {Username}", resetPasswordDto.Username);
            return Ok("Password reset successfully");
        }
    }
}
