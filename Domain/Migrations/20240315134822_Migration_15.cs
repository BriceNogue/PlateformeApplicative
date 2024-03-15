using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class Migration_15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropColumn(
                name: "Emplacement",
                table: "Salles");

            migrationBuilder.DropColumn(
                name: "Nom",
                table: "Salles");

            migrationBuilder.DropColumn(
                name: "LibellePoste",
                table: "Postes");

            migrationBuilder.RenameColumn(
                name: "ObjetConcerne",
                table: "Types",
                newName: "Objet");

            migrationBuilder.RenameColumn(
                name: "Nom",
                table: "Types",
                newName: "Libelle");

            migrationBuilder.RenameIndex(
                name: "IX_Types_Nom",
                table: "Types",
                newName: "IX_Types_Libelle");

            migrationBuilder.RenameColumn(
                name: "AdresseIp",
                table: "Postes",
                newName: "AdresseIP");

            migrationBuilder.RenameColumn(
                name: "Adresse",
                table: "Etablissements",
                newName: "LibelleRue");

            migrationBuilder.AddColumn<int>(
                name: "Numero",
                table: "Salles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Numero",
                table: "Postes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Telephone",
                table: "Etablissements",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Etablissements",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreation",
                table: "Etablissements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "NumeroRue",
                table: "Etablissements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateNaissance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MotDePasse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateInscription = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Utilisateurs_Types_IdType",
                        column: x => x.IdType,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UtilisateurEtablissements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUtilisateur = table.Column<int>(type: "int", nullable: false),
                    IdEtablissement = table.Column<int>(type: "int", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UtilisateurId = table.Column<int>(type: "int", nullable: false),
                    EtablissementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtilisateurEtablissements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UtilisateurEtablissements_Etablissements_EtablissementId",
                        column: x => x.EtablissementId,
                        principalTable: "Etablissements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UtilisateurEtablissements_Utilisateurs_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Etablissements_Email",
                table: "Etablissements",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Etablissements_Telephone",
                table: "Etablissements",
                column: "Telephone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UtilisateurEtablissements_EtablissementId",
                table: "UtilisateurEtablissements",
                column: "EtablissementId");

            migrationBuilder.CreateIndex(
                name: "IX_UtilisateurEtablissements_UtilisateurId",
                table: "UtilisateurEtablissements",
                column: "UtilisateurId");

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_Email",
                table: "Utilisateurs",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_IdType",
                table: "Utilisateurs",
                column: "IdType");

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_Telephone",
                table: "Utilisateurs",
                column: "Telephone",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UtilisateurEtablissements");

            migrationBuilder.DropTable(
                name: "Utilisateurs");

            migrationBuilder.DropIndex(
                name: "IX_Etablissements_Email",
                table: "Etablissements");

            migrationBuilder.DropIndex(
                name: "IX_Etablissements_Telephone",
                table: "Etablissements");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Salles");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Postes");

            migrationBuilder.DropColumn(
                name: "DateCreation",
                table: "Etablissements");

            migrationBuilder.DropColumn(
                name: "NumeroRue",
                table: "Etablissements");

            migrationBuilder.RenameColumn(
                name: "Objet",
                table: "Types",
                newName: "ObjetConcerne");

            migrationBuilder.RenameColumn(
                name: "Libelle",
                table: "Types",
                newName: "Nom");

            migrationBuilder.RenameIndex(
                name: "IX_Types_Libelle",
                table: "Types",
                newName: "IX_Types_Nom");

            migrationBuilder.RenameColumn(
                name: "AdresseIP",
                table: "Postes",
                newName: "AdresseIp");

            migrationBuilder.RenameColumn(
                name: "LibelleRue",
                table: "Etablissements",
                newName: "Adresse");

            migrationBuilder.AddColumn<string>(
                name: "Emplacement",
                table: "Salles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nom",
                table: "Salles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LibellePoste",
                table: "Postes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Telephone",
                table: "Etablissements",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Etablissements",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdType = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Types_IdType",
                        column: x => x.IdType,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdType",
                table: "Users",
                column: "IdType");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users",
                column: "PhoneNumber",
                unique: true);
        }
    }
}
