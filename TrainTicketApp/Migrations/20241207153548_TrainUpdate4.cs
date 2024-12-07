using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainTicketApp.Migrations
{
    /// <inheritdoc />
    public partial class TrainUpdate4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReservationPrice",
                table: "Trains",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SupplementaryPrice",
                table: "Trains",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationPrice",
                table: "Trains");

            migrationBuilder.DropColumn(
                name: "SupplementaryPrice",
                table: "Trains");
        }
    }
}
