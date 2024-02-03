using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class Migration_9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdEtablissement",
                table: "Salles",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Salles_IdEtablissement",
                table: "Salles",
                column: "IdEtablissement");

            migrationBuilder.AddForeignKey(
                name: "FK_Salles_Etablissements_IdEtablissement",
                table: "Salles",
                column: "IdEtablissement",
                principalTable: "Etablissements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salles_Etablissements_IdEtablissement",
                table: "Salles");

            migrationBuilder.DropIndex(
                name: "IX_Salles_IdEtablissement",
                table: "Salles");

            migrationBuilder.AlterColumn<string>(
                name: "IdEtablissement",
                table: "Salles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
