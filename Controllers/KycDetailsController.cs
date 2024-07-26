using KYC_apllication_2.DTOs;
using KYC_apllication_2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace onlinekyc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KycDetailsController : ControllerBase
    {
        private readonly IKycDetailsService _kycDetailsService;

        public KycDetailsController(IKycDetailsService kycDetailsService)
        {
            _kycDetailsService = kycDetailsService;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitKycDetails([FromBody] UserKycDetailsDto userKycDetailsDto)
        {
            var result = await _kycDetailsService.SubmitKycDetailsAsync(userKycDetailsDto);
            if (!result)
            {
                return BadRequest("KYC submission failed.");
            }
            return Ok("KYC details submitted successfully.");
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> GetAllKycDetails()
        {
            return Ok(await _kycDetailsService.GetAllUserKycDetailsAsync());
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetKycByUserId(int id)
        {
            var kycDetails = await _kycDetailsService.GetByUserIdAsync(id);
            if (kycDetails == null)
            {
                return NotFound("KYC details not found.");
            }
            return Ok(kycDetails);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditKycDetails(int id, [FromBody] UserKycDetailsDto userKycDetailsDto)
        {
            var result = await _kycDetailsService.UpdateKycDetailsAsync(id, userKycDetailsDto);
            if (!result)
            {
                return BadRequest("KYC update failed.");
            }
            return Ok("KYC details updated successfully.");
        }
    }
}
