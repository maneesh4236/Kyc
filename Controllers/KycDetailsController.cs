using KYC_apllication_2.DTOs;
using KYC_apllication_2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace onlinekyc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KycDetailsController : ControllerBase
    {
        private readonly IKycDetailsService _kycDetailsService;
        private readonly ILogger<KycDetailsController> _logger;

        public KycDetailsController(IKycDetailsService kycDetailsService, ILogger<KycDetailsController> logger)
        {
            _kycDetailsService = kycDetailsService;
            _logger = logger;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitKycDetails([FromBody] UserKycDetailsDto userKycDetailsDto)
        {
            _logger.LogInformation("SubmitKycDetails method called for UserId: {UserId}", userKycDetailsDto.UserId);

            var result = await _kycDetailsService.SubmitKycDetailsAsync(userKycDetailsDto);
            if (!result)
            {
                _logger.LogWarning("KYC submission failed for UserId: {UserId}", userKycDetailsDto.UserId);
                return BadRequest("KYC submission failed.");
            }

            _logger.LogInformation("KYC details submitted successfully for UserId: {UserId}", userKycDetailsDto.UserId);
            return Ok("KYC details submitted successfully.");
        }

        [HttpGet("all")]
        // [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> GetAllKycDetails()
        {
            _logger.LogInformation("GetAllKycDetails method called.");

            var kycDetailsList = await _kycDetailsService.GetAllUserKycDetailsAsync();
            _logger.LogInformation("Fetched all KYC details.");

            return Ok(kycDetailsList);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetKycByUserId(int id)
        {
            _logger.LogInformation("GetKycByUserId method called for UserId: {UserId}", id);

            var kycDetails = await _kycDetailsService.GetByUserIdAsync(id);
            if (kycDetails == null)
            {
                _logger.LogWarning("KYC details not found for UserId: {UserId}", id);
                return NotFound("KYC details not found.");
            }

            _logger.LogInformation("Fetched KYC details for UserId: {UserId}", id);
            return Ok(kycDetails);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditKycDetails(int id, [FromBody] UserKycDetailsDto userKycDetailsDto)
        {
            _logger.LogInformation("EditKycDetails method called for UserId: {UserId}", id);

            var result = await _kycDetailsService.UpdateKycDetailsAsync(id, userKycDetailsDto);
            if (!result)
            {
                _logger.LogWarning("KYC update failed for UserId: {UserId}", id);
                return BadRequest("KYC update failed.");
            }

            _logger.LogInformation("KYC details updated successfully for UserId: {UserId}", id);
            return Ok("KYC details updated successfully.");
        }

        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> DeleteKycDetails(int userId)
        {
            _logger.LogInformation("DeleteKycDetails method called for UserId: {UserId}", userId);

            var result = await _kycDetailsService.DeleteKycDetailsAsync(userId);
            if (!result)
            {
                _logger.LogWarning("KYC details deletion failed for UserId: {UserId}", userId);
                return NotFound(new { Message = "KYC details not found for the given user ID." });
            }

            _logger.LogInformation("KYC details deleted successfully for UserId: {UserId}", userId);
            return NoContent();
        }
    }
}
