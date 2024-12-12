using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$FYySpp0OeCKgeAQlhraucOyoSSOebkFMNJKxn82x1xIfbnlU7jbK.");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$FYySpp0OeCKgeAQlhraucOyoSSOebkFMNJKxn82x1xIfbnlU7jbK.");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$FYySpp0OeCKgeAQlhraucOyoSSOebkFMNJKxn82x1xIfbnlU7jbK.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$JTCywpQ32wbqj41LAOL3u.hCYrpGPtWF9WML4A.lMWpmPZCKFSS..");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$JTCywpQ32wbqj41LAOL3u.hCYrpGPtWF9WML4A.lMWpmPZCKFSS..");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$JTCywpQ32wbqj41LAOL3u.hCYrpGPtWF9WML4A.lMWpmPZCKFSS..");
        }
    }
}
