using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainTicketApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTrainDepartureTime3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainDepartureTime");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Friday",
                table: "Trains",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Monday",
                table: "Trains",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Saturday",
                table: "Trains",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Sunday",
                table: "Trains",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Thursday",
                table: "Trains",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Tuesday",
                table: "Trains",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Wednesday",
                table: "Trains",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Friday",
                table: "Trains");

            migrationBuilder.DropColumn(
                name: "Monday",
                table: "Trains");

            migrationBuilder.DropColumn(
                name: "Saturday",
                table: "Trains");

            migrationBuilder.DropColumn(
                name: "Sunday",
                table: "Trains");

            migrationBuilder.DropColumn(
                name: "Thursday",
                table: "Trains");

            migrationBuilder.DropColumn(
                name: "Tuesday",
                table: "Trains");

            migrationBuilder.DropColumn(
                name: "Wednesday",
                table: "Trains");

            migrationBuilder.CreateTable(
                name: "TrainDepartureTime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TrainId = table.Column<int>(type: "INTEGER", nullable: false),
                    DayOfWeek = table.Column<int>(type: "INTEGER", nullable: false),
                    DepartureTime = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainDepartureTime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainDepartureTime_Trains_TrainId",
                        column: x => x.TrainId,
                        principalTable: "Trains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainDepartureTime_TrainId",
                table: "TrainDepartureTime",
                column: "TrainId");
        }
    }
}
