using KYC_apllication_2.DTOs;
using KYC_apllication_2.Entity;
using KYC_apllication_2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KYC_apllication_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(IUserService userService, ILogger<CustomerController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        // Endpoint to get all users by role
        [HttpGet("all")]
        public async Task<IActionResult> GetUsersByRole()
        {
            _logger.LogInformation("GetUsersByRole called");

            try
            {
                var customers = await _userService.GetAllUSerByRoleAsync("Customer");
                var customerDto = customers.Select(c => new UserDto
                {
                    Username = c.Username,
                    Role = c.Role
                }).ToList();

                _logger.LogInformation("Fetched {Count} customers", customerDto.Count);
                return Ok(customerDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching customers");
                return StatusCode(500, "Internal server error");
            }
        }

        // Endpoint to get a user by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            _logger.LogInformation("GetUserById called with ID: {Id}", id);

            try
            {
                var customer = await _userService.GetUserByIdAsync(id);
                if (customer == null || customer.Role != "Customer")
                {
                    _logger.LogWarning("Customer with ID: {Id} not found or not a customer", id);
                    return NotFound("Customer not found.");
                }

                var customerDto = new UserDto
                {
                    Username = customer.Username,
                    Role = customer.Role
                };
                return Ok(customerDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching customer with ID: {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        // Endpoint to update a user
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto updateCustomerDto)
        {
            _logger.LogInformation("UpdateUser called with ID: {Id}", id);

            try
            {
                var existingCustomer = await _userService.GetUserByIdAsync(id);
                if (existingCustomer == null || existingCustomer.Role != "Customer")
                {
                    _logger.LogWarning("Customer with ID: {Id} not found or not a customer", id);
                    return NotFound("Customer not found.");
                }

                existingCustomer.Username = updateCustomerDto.Username;
                var result = await _userService.UpdateUserAsync(existingCustomer);

                if (!result)
                {
                    _logger.LogError("Error occurred while updating customer with ID: {Id}", id);
                    return StatusCode(500, "An error occurred while updating the customer.");
                }

                _logger.LogInformation("Customer with ID: {Id} updated successfully", id);
                return Ok("Customer updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating customer with ID: {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
