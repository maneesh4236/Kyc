﻿using System.ComponentModel.DataAnnotations;

namespace KYC_apllication_2.Entity
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }

       // [Required]
       // public string Email { get; set; }   

        // Navigation property for UserKycDetails
      //  public UserKycDetails UserKycDetails { get; set; }
    }
}
