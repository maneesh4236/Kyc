using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KYC_apllication_2.Migrations
{
    /// <inheritdoc />
    public partial class CorrectMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserKycDetails_UserId",
                table: "UserKycDetails",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserKycDetails_Users_UserId",
                table: "UserKycDetails",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserKycDetails_Users_UserId",
                table: "UserKycDetails");

            migrationBuilder.DropIndex(
                name: "IX_UserKycDetails_UserId",
                table: "UserKycDetails");
        }
    }
}
