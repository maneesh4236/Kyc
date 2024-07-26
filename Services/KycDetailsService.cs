using KYC_apllication_2.DTOs;
using KYC_apllication_2.Entity;
using KYC_apllication_2.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KYC_apllication_2.Services
{
    public class KycDetailsService : IKycDetailsService
    {
        private readonly IKycDetailsRepository _kycDetailsRepository;

        public KycDetailsService(IKycDetailsRepository kycDetailsRepository)
        {
            _kycDetailsRepository = kycDetailsRepository;
        }

        public async Task<bool> SubmitKycDetailsAsync(UserKycDetailsDto userKycDetailsDto)
        {
            return await _kycDetailsRepository.SubmitKycDetailsAsync(userKycDetailsDto);
        }

        public async Task<List<UserKycDetails>> GetAllUserKycDetailsAsync()
        {
            return await _kycDetailsRepository.GetAllUserKycDetails();
        }

        public async Task<UserKycDetails> GetByUserIdAsync(int id)
        {
            return await _kycDetailsRepository.GetByUserId(id);
        }

        public async Task<bool> UpdateKycDetailsAsync(int userId, UserKycDetailsDto userKycDetailsDto)
        {
            return await _kycDetailsRepository.UpdateKycDetailsAsync(userId, userKycDetailsDto);
        }
    }
}
