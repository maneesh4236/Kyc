namespace KYC_apllication_2.DTOs
{
    public class UpdateProfileDto
    {
        public int UserId { get; set; }  // Changed from string to int
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
