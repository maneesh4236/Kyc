using KYC_apllication_2.Data;
using KYC_apllication_2.DTOs;
using KYC_apllication_2.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KYC_apllication_2.Repositories
{
    public class KycDetailsRepository : IKycDetailsRepository
    {
        private readonly KYCContext _context;

        public KycDetailsRepository(KYCContext context)
        {
            _context = context;
        }

        public async Task<List<UserKycDetails>> GetAllUserKycDetails()
        {
            return await _context.UserKycDetails.ToListAsync();
        }

        public async Task<UserKycDetails> GetByUserId(int id)
        {
            return await _context.UserKycDetails.FirstOrDefaultAsync(ukd => ukd.UserId == id);
        }

        public async Task<bool> SubmitKycDetailsAsync(UserKycDetailsDto userKycDetailsDto)
        {
            var userKycDetails = new UserKycDetails
            {
                UserId = userKycDetailsDto.UserId,
                Name = userKycDetailsDto.Name,
                Address = userKycDetailsDto.Address,
                DOB = userKycDetailsDto.DOB,
                AadharCardNumber = userKycDetailsDto.AadharCardNumber,
                PhoneNumber = userKycDetailsDto.PhoneNumber,
                PanCardNumber = userKycDetailsDto.PanCardNumber,
                Email = userKycDetailsDto.Email,
                KycStatus = "pending"
            };

            await _context.UserKycDetails.AddAsync(userKycDetails);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateKycDetailsAsync(int userId, UserKycDetailsDto userKycDetailsDto)
        {
            var existingKycDetails = await _context.UserKycDetails.FirstOrDefaultAsync(ukd => ukd.UserId == userId);
            if (existingKycDetails == null)
            {
                return false; // User not found
            }

            // Update properties
            existingKycDetails.Name = userKycDetailsDto.Name;
            existingKycDetails.Address = userKycDetailsDto.Address;
            existingKycDetails.DOB = userKycDetailsDto.DOB;
            existingKycDetails.AadharCardNumber = userKycDetailsDto.AadharCardNumber;
            existingKycDetails.PhoneNumber = userKycDetailsDto.PhoneNumber;
            existingKycDetails.PanCardNumber = userKycDetailsDto.PanCardNumber;
            existingKycDetails.Email = userKycDetailsDto.Email;
           existingKycDetails.KycStatus = userKycDetailsDto.KYCKycStatus; // Assuming KycStatus is part of the DTO

            _context.UserKycDetails.Update(existingKycDetails);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
