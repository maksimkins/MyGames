using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyGames.Migrations
{
    /// <inheritdoc />
    public partial class NewConstrainOnUserGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserGames_UserId",
                table: "UserGames");

            migrationBuilder.CreateIndex(
                name: "IX_UserGames_UserId_GameId",
                table: "UserGames",
                columns: new[] { "UserId", "GameId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserGames_UserId_GameId",
                table: "UserGames");

            migrationBuilder.CreateIndex(
                name: "IX_UserGames_UserId",
                table: "UserGames",
                column: "UserId");
        }
    }
}
