using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventBookingApp.Migrations
{
    /// <inheritdoc />
    public partial class AddBookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Booking");

            migrationBuilder.AddColumn<DateTime>(
                name: "BookingDate",
                table: "Booking",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SeatsBooked",
                table: "Booking",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingDate",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "SeatsBooked",
                table: "Booking");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
