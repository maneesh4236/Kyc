using KYC_apllication_2.Data;
using KYC_apllication_2.DTOs;
using KYC_apllication_2.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KYC_apllication_2.Services
{
    public class KycDetailsService : IKycDetailsService
    {
        private readonly KYCContext _context;

        public KycDetailsService(KYCContext context)
        {
            _context = context;
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

        public async Task<List<UserKycDetails>> GetAllUserKycDetailsAsync()
        {
            return await _context.UserKycDetails.ToListAsync();
        }

        public async Task<UserKycDetails> GetByUserIdAsync(int id)
        {
            return await _context.UserKycDetails.FirstOrDefaultAsync(ukd => ukd.UserId == id);
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
            // existingKycDetails.KycStatus = userKycDetailsDto.KYCKycStatus; // Assuming KycStatus is part of the DTO

            _context.UserKycDetails.Update(existingKycDetails);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateKycStatusAsync(int userId, string kycStatus)
        {
            // Retrieve the KYC details based on UserId
            var kycDetails = await _context.UserKycDetails
                .SingleOrDefaultAsync(k => k.UserId == userId);

            if (kycDetails == null)
            {
                // Return false if no record is found
                return false;
            }

            // Update the KYC status
            kycDetails.KycStatus = kycStatus;

            // Mark the entity as modified
            _context.UserKycDetails.Update(kycDetails);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return true to indicate success
            return true;
        }

        public async Task<bool> DeleteKycDetailsAsync(int userId)
        {
            var kycDetails = await _context.UserKycDetails.FirstOrDefaultAsync(ukd => ukd.UserId == userId);
            if (kycDetails == null)
            {
                return false; // User not found
            }

            _context.UserKycDetails.Remove(kycDetails);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
