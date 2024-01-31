using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class Mingration6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adress",
                table: "Etablissements");

            migrationBuilder.RenameColumn(
                name: "TypeName",
                table: "Types",
                newName: "ObjetConcerne");

            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "Etablissements",
                newName: "Ville");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Etablissements",
                newName: "Telephone");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Etablissements",
                newName: "CodePostal");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Etablissements",
                newName: "Adresse");

            migrationBuilder.AddColumn<string>(
                name: "Nom",
                table: "Types",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nom",
                table: "Etablissements",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Postes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LibellePoste = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marque = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdresseIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdresseMAC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdSalle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Statut = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Salles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Emplacement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdType = table.Column<int>(type: "int", nullable: false),
                    IdEtablissement = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Salles_Types_IdType",
                        column: x => x.IdType,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Types_Nom",
                table: "Types",
                column: "Nom",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Etablissements_Nom",
                table: "Etablissements",
                column: "Nom",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Salles_IdType",
                table: "Salles",
                column: "IdType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Postes");

            migrationBuilder.DropTable(
                name: "Salles");

            migrationBuilder.DropIndex(
                name: "IX_Types_Nom",
                table: "Types");

            migrationBuilder.DropIndex(
                name: "IX_Etablissements_Nom",
                table: "Etablissements");

            migrationBuilder.DropColumn(
                name: "Nom",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "Nom",
                table: "Etablissements");

            migrationBuilder.RenameColumn(
                name: "ObjetConcerne",
                table: "Types",
                newName: "TypeName");

            migrationBuilder.RenameColumn(
                name: "Ville",
                table: "Etablissements",
                newName: "PostalCode");

            migrationBuilder.RenameColumn(
                name: "Telephone",
                table: "Etablissements",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "CodePostal",
                table: "Etablissements",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Adresse",
                table: "Etablissements",
                newName: "City");

            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "Etablissements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
