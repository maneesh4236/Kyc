using System.ComponentModel.DataAnnotations;

namespace KYC_apllication_2.Entity
{
    public class UserKycDetails
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        [Required]
        public string AadharCardNumber { get; set; }

        [Required]
        public string PanCardNumber { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string KycStatus { get; set; }

        // Navigation property for User
        public User User { get; set; }
    }
}
