using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainTicketApp.Migrations
{
    /// <inheritdoc />
    public partial class TrainSeatUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvailableSeatsFriday",
                table: "Trains",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AvailableSeatsMonday",
                table: "Trains",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AvailableSeatsSaturday",
                table: "Trains",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AvailableSeatsSunday",
                table: "Trains",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AvailableSeatsThursday",
                table: "Trains",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AvailableSeatsTuesday",
                table: "Trains",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AvailableSeatsWednesday",
                table: "Trains",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableSeatsFriday",
                table: "Trains");

            migrationBuilder.DropColumn(
                name: "AvailableSeatsMonday",
                table: "Trains");

            migrationBuilder.DropColumn(
                name: "AvailableSeatsSaturday",
                table: "Trains");

            migrationBuilder.DropColumn(
                name: "AvailableSeatsSunday",
                table: "Trains");

            migrationBuilder.DropColumn(
                name: "AvailableSeatsThursday",
                table: "Trains");

            migrationBuilder.DropColumn(
                name: "AvailableSeatsTuesday",
                table: "Trains");

            migrationBuilder.DropColumn(
                name: "AvailableSeatsWednesday",
                table: "Trains");
        }
    }
}
