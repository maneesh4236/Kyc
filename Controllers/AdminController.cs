using KYC_apllication_2.DTOs;
using KYC_apllication_2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KYC_apllication_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class KycApprovalController : ControllerBase
    {
        private readonly IKycDetailsService _kycDetailsService;
        private readonly IUserService _userService;
        private readonly ILogger<KycApprovalController> _logger;

        public KycApprovalController(IKycDetailsService kycDetailsService, IUserService userService, ILogger<KycApprovalController> logger)
        {
            _kycDetailsService = kycDetailsService;
            _userService = userService;
            _logger = logger;
        }

        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateKycStatus([FromBody] KycStatusUpdateDto kycStatusUpdateDto)
        {
            _logger.LogInformation("UpdateKycStatus called with UserId: {UserId} and KycStatus: {KycStatus}",
                kycStatusUpdateDto.UserId, kycStatusUpdateDto.KycStatus);

            // Validate KYC status
            if (kycStatusUpdateDto.KycStatus != "Approved" && kycStatusUpdateDto.KycStatus != "Rejected")
            {
                _logger.LogWarning("Invalid KYC status: {KycStatus}", kycStatusUpdateDto.KycStatus);
                return BadRequest("Invalid KYC status.");
            }

            // Call the service to update KYC status based on UserId
            var result = await _kycDetailsService.UpdateKycStatusAsync(kycStatusUpdateDto.UserId, kycStatusUpdateDto.KycStatus);
            if (!result)
            {
                _logger.LogError("KYC status update failed for UserId: {UserId}", kycStatusUpdateDto.UserId);
                return BadRequest("KYC status update failed.");
            }

            _logger.LogInformation("KYC status updated successfully for UserId: {UserId}", kycStatusUpdateDto.UserId);
            return Ok("KYC status updated successfully.");
        }

        [HttpPost("update-profile")]
        //[Authorize(Roles = "Admin")]
        // [Authorize]
        public async Task<IActionResult> UpdateProfie([FromBody] UpdateProfileDto updateProfileDto)
        {
            _logger.LogInformation("UpdateProfile called with UserId: {UserId}", updateProfileDto.UserId);

            var result = await _userService.UpdateAdminProfileAsync(updateProfileDto.UserId, updateProfileDto.Username, updateProfileDto.Password);
            if (!result)
            {
                _logger.LogError("Profile update failed for UserId: {UserId}", updateProfileDto.UserId);
                return BadRequest("Profile update failed");
            }

            _logger.LogInformation("Profile updated successfully for UserId: {UserId}", updateProfileDto.UserId);
            return Ok("Profile updated successfully");
        }
    }
}
