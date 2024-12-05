using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainTicketApp.Migrations
{
    /// <inheritdoc />
    public partial class AddTrainId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainDepartureTime_Trains_TrainId",
                table: "TrainDepartureTime");

            migrationBuilder.AlterColumn<int>(
                name: "TrainId",
                table: "TrainDepartureTime",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainDepartureTime_Trains_TrainId",
                table: "TrainDepartureTime",
                column: "TrainId",
                principalTable: "Trains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainDepartureTime_Trains_TrainId",
                table: "TrainDepartureTime");

            migrationBuilder.AlterColumn<int>(
                name: "TrainId",
                table: "TrainDepartureTime",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainDepartureTime_Trains_TrainId",
                table: "TrainDepartureTime",
                column: "TrainId",
                principalTable: "Trains",
                principalColumn: "Id");
        }
    }
}
