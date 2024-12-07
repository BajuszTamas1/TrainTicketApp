using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainTicketApp.Migrations
{
    /// <inheritdoc />
    public partial class TrainUpdate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AllSeats",
                table: "Trains",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AvailableSeats",
                table: "Trains",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllSeats",
                table: "Trains");

            migrationBuilder.DropColumn(
                name: "AvailableSeats",
                table: "Trains");
        }
    }
}
