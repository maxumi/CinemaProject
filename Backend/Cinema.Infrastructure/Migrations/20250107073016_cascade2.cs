using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class cascade2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingSeat_Bookings_BookingId",
                table: "BookingSeat");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingSeat_Bookings_BookingId",
                table: "BookingSeat",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingSeat_Bookings_BookingId",
                table: "BookingSeat");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingSeat_Bookings_BookingId",
                table: "BookingSeat",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
