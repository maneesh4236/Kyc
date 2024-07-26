using KYC_apllication_2.DTOs;
using KYC_apllication_2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace onlinekyc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.RegisterUserAsync(user);
            if (!result)
                return BadRequest("User registration failed");

            return Ok("User registered successfully");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var validUser = await _userService.ValidateUserCredentialsAsync(userLoginDto.Username, userLoginDto.Password);
            if (!validUser)
                return Unauthorized("Invalid credentials");

            var token = await _userService.GenerateJwtTokenAsync(userLoginDto.Username);
            return Ok(new { token, username = userLoginDto.Username });
        }

        [HttpPost("update-profile")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAdminProfile([FromBody] AdminProfileDto adminProfileDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.UpdateAdminProfileAsync(adminProfileDto);
            if (!result)
                return BadRequest("Profile update failed");

            return Ok("Profile updated successfully");
        }

        [HttpPost("change-password")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeAdminPassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.ChangeAdminPasswordAsync(changePasswordDto);
            if (!result)
                return BadRequest("Password change failed");

            return Ok("Password changed successfully");
        }
    }
}
