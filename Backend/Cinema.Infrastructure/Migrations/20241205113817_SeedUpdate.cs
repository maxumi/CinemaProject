using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$muZFY1XLH0Ks/ASxO6mg7uU1DyyXuRBNHKhWW/10yY.FJOZ3nRwC.");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$muZFY1XLH0Ks/ASxO6mg7uU1DyyXuRBNHKhWW/10yY.FJOZ3nRwC.");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$muZFY1XLH0Ks/ASxO6mg7uU1DyyXuRBNHKhWW/10yY.FJOZ3nRwC.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$FYySpp0OeCKgeAQlhraucOyoSSOebkFMNJKxn82x1xIfbnlU7jbK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$FYySpp0OeCKgeAQlhraucOyoSSOebkFMNJKxn82x1xIfbnlU7jbK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$FYySpp0OeCKgeAQlhraucOyoSSOebkFMNJKxn82x1xIfbnlU7jbK");
        }
    }
}
