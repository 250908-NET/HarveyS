using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project1.Api.Migrations
{
    /// <inheritdoc />
    public partial class Moonmodelupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Moons_Planets_planetId",
                table: "Moons");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Stars",
                newName: "StarId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Planets",
                newName: "PlanetId");

            migrationBuilder.RenameColumn(
                name: "planetId",
                table: "Moons",
                newName: "PlanetId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Moons",
                newName: "MoonId");

            migrationBuilder.RenameIndex(
                name: "IX_Moons_planetId",
                table: "Moons",
                newName: "IX_Moons_PlanetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Moons_Planets_PlanetId",
                table: "Moons",
                column: "PlanetId",
                principalTable: "Planets",
                principalColumn: "PlanetId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Moons_Planets_PlanetId",
                table: "Moons");

            migrationBuilder.RenameColumn(
                name: "StarId",
                table: "Stars",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PlanetId",
                table: "Planets",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PlanetId",
                table: "Moons",
                newName: "planetId");

            migrationBuilder.RenameColumn(
                name: "MoonId",
                table: "Moons",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Moons_PlanetId",
                table: "Moons",
                newName: "IX_Moons_planetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Moons_Planets_planetId",
                table: "Moons",
                column: "planetId",
                principalTable: "Planets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
