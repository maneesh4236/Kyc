using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KYC_apllication_2.Migrations
{
    /// <inheritdoc />
    public partial class AddUserProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserKycDetails_Users_UserId",
                table: "UserKycDetails");

            migrationBuilder.DropIndex(
                name: "IX_UserKycDetails_UserId",
                table: "UserKycDetails");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
    }
}
