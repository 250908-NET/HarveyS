using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project1.Api.Migrations
{
    /// <inheritdoc />
    public partial class Publiclistsforrelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlanetStar",
                columns: table => new
                {
                    PlanetsPlanetId = table.Column<int>(type: "int", nullable: false),
                    starsStarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanetStar", x => new { x.PlanetsPlanetId, x.starsStarId });
                    table.ForeignKey(
                        name: "FK_PlanetStar_Planets_PlanetsPlanetId",
                        column: x => x.PlanetsPlanetId,
                        principalTable: "Planets",
                        principalColumn: "PlanetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanetStar_Stars_starsStarId",
                        column: x => x.starsStarId,
                        principalTable: "Stars",
                        principalColumn: "StarId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanetStar_starsStarId",
                table: "PlanetStar",
                column: "starsStarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanetStar");
        }
    }
}
