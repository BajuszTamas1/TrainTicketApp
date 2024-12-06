using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainTicketApp.Migrations
{
    /// <inheritdoc />
    public partial class ModifyOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DayOfWeek",
                table: "Orders",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DepartureTime",
                table: "Orders",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DepartureTime",
                table: "Orders");
        }
    }
}
