using KYC_apllication_2.DTOs;
using KYC_apllication_2.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KYC_apllication_2.Services
{
    public interface IKycDetailsService
    {
        Task<bool> SubmitKycDetailsAsync(UserKycDetailsDto userKycDetailsDto);
        Task<List<UserKycDetails>> GetAllUserKycDetailsAsync();
        Task<UserKycDetails> GetByUserIdAsync(int id);
        Task<bool> UpdateKycDetailsAsync(int userId, UserKycDetailsDto userKycDetailsDto);
        Task<bool> UpdateKycStatusAsync(int id, string kycStatus);
        Task<bool> DeleteKycDetailsAsync(int userId); // New delete method
    }
}
