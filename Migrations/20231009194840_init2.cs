using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlanAPI.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Users_TelegramId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_TelegramId",
                table: "Patients");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Patients_TelegramId",
                table: "Patients",
                column: "TelegramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Users_TelegramId",
                table: "Patients",
                column: "TelegramId",
                principalTable: "Users",
                principalColumn: "TelegramId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
