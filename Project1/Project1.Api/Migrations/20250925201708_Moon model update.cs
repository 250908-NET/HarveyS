using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project1.Api.Migrations
{
    /// <inheritdoc />
    public partial class Moonmodelupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Moons_Stars_starId",
                table: "Moons");

            migrationBuilder.RenameColumn(
                name: "starId",
                table: "Moons",
                newName: "planetId");

            migrationBuilder.RenameIndex(
                name: "IX_Moons_starId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Moons_Planets_planetId",
                table: "Moons");

            migrationBuilder.RenameColumn(
                name: "planetId",
                table: "Moons",
                newName: "starId");

            migrationBuilder.RenameIndex(
                name: "IX_Moons_planetId",
                table: "Moons",
                newName: "IX_Moons_starId");

            migrationBuilder.AddForeignKey(
                name: "FK_Moons_Stars_starId",
                table: "Moons",
                column: "starId",
                principalTable: "Stars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
