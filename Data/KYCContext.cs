using KYC_apllication_2.Entity;
using Microsoft.EntityFrameworkCore;

namespace KYC_apllication_2.Data
{
    public class KYCContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserKycDetails> UserKycDetails { get; set; }

        public KYCContext(DbContextOptions<KYCContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the one-to-one relationship between User and UserKycDetails
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserKycDetails)
                .WithOne(uk => uk.User)
                .HasForeignKey<UserKycDetails>(uk => uk.UserId);

            // Optional: Set up additional configurations if needed
        }
    }
}
