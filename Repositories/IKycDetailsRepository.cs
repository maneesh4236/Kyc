using KYC_apllication_2.DTOs;
using KYC_apllication_2.Entity;

namespace KYC_apllication_2.Repositories
{
    public interface IKycDetailsRepository
    {
        Task<bool> SubmitKycDetailsAsync(UserKycDetailsDto userKycDetailsDto);
        Task<List<UserKycDetails>> GetAllUserKycDetails();

        Task<UserKycDetails> GetByUserId(int Id);
        Task<bool> UpdateKycDetailsAsync(int userId, UserKycDetailsDto userKycDetailsDto);
    }
}

