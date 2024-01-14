using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReserveApp.Migrations
{
    /// <inheritdoc />
    public partial class TourModelModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Days",
                table: "Tours",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TourId",
                table: "Reservations",
                column: "TourId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Tours_TourId",
                table: "Reservations",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "TourId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Tours_TourId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_TourId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Days",
                table: "Tours");
        }
    }
}
