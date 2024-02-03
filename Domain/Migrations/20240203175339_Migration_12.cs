using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class Migration_12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Postes_IdSalle",
                table: "Postes",
                column: "IdSalle");

            migrationBuilder.AddForeignKey(
                name: "FK_Postes_Salles_IdSalle",
                table: "Postes",
                column: "IdSalle",
                principalTable: "Salles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Postes_Salles_IdSalle",
                table: "Postes");

            migrationBuilder.DropIndex(
                name: "IX_Postes_IdSalle",
                table: "Postes");
        }
    }
}
