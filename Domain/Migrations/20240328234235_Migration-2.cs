using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class Migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Postes_Types_IdType",
                table: "Postes");

            migrationBuilder.DropForeignKey(
                name: "FK_Salles_Types_IdType",
                table: "Salles");

            migrationBuilder.DropForeignKey(
                name: "FK_Utilisateur_Types_IdType",
                table: "Utilisateur");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Types",
                table: "Types");

            migrationBuilder.RenameTable(
                name: "Types",
                newName: "TypeE");

            migrationBuilder.RenameIndex(
                name: "IX_Types_Libelle",
                table: "TypeE",
                newName: "IX_TypeE_Libelle");

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "TypeE",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TypeE",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedName",
                table: "TypeE",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeE",
                table: "TypeE",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Postes_TypeE_IdType",
                table: "Postes",
                column: "IdType",
                principalTable: "TypeE",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Salles_TypeE_IdType",
                table: "Salles",
                column: "IdType",
                principalTable: "TypeE",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Utilisateur_TypeE_IdType",
                table: "Utilisateur",
                column: "IdType",
                principalTable: "TypeE",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Postes_TypeE_IdType",
                table: "Postes");

            migrationBuilder.DropForeignKey(
                name: "FK_Salles_TypeE_IdType",
                table: "Salles");

            migrationBuilder.DropForeignKey(
                name: "FK_Utilisateur_TypeE_IdType",
                table: "Utilisateur");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeE",
                table: "TypeE");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "TypeE");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TypeE");

            migrationBuilder.DropColumn(
                name: "NormalizedName",
                table: "TypeE");

            migrationBuilder.RenameTable(
                name: "TypeE",
                newName: "Types");

            migrationBuilder.RenameIndex(
                name: "IX_TypeE_Libelle",
                table: "Types",
                newName: "IX_Types_Libelle");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Types",
                table: "Types",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Postes_Types_IdType",
                table: "Postes",
                column: "IdType",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Salles_Types_IdType",
                table: "Salles",
                column: "IdType",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Utilisateur_Types_IdType",
                table: "Utilisateur",
                column: "IdType",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
