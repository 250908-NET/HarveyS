using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project1.Api.Migrations
{
    /// <inheritdoc />
    public partial class Modelchanges1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Planets_Stars_starId",
                table: "Planets");

            migrationBuilder.DropIndex(
                name: "IX_Planets_starId",
                table: "Planets");

            migrationBuilder.DropColumn(
                name: "starId",
                table: "Planets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "starId",
                table: "Planets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Planets_starId",
                table: "Planets",
                column: "starId");

            migrationBuilder.AddForeignKey(
                name: "FK_Planets_Stars_starId",
                table: "Planets",
                column: "starId",
                principalTable: "Stars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
