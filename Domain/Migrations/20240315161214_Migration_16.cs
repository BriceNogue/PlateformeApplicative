using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class Migration_16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UtilisateurEtablissements_Etablissements_EtablissementId",
                table: "UtilisateurEtablissements");

            migrationBuilder.DropForeignKey(
                name: "FK_UtilisateurEtablissements_Utilisateurs_UtilisateurId",
                table: "UtilisateurEtablissements");

            migrationBuilder.DropIndex(
                name: "IX_UtilisateurEtablissements_EtablissementId",
                table: "UtilisateurEtablissements");

            migrationBuilder.DropIndex(
                name: "IX_UtilisateurEtablissements_UtilisateurId",
                table: "UtilisateurEtablissements");

            migrationBuilder.DropColumn(
                name: "EtablissementId",
                table: "UtilisateurEtablissements");

            migrationBuilder.DropColumn(
                name: "UtilisateurId",
                table: "UtilisateurEtablissements");

            migrationBuilder.AddColumn<bool>(
                name: "Statut",
                table: "UtilisateurEtablissements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_UtilisateurEtablissements_IdUtilisateur",
                table: "UtilisateurEtablissements",
                column: "IdUtilisateur");

            migrationBuilder.AddForeignKey(
                name: "FK_UtilisateurEtablissements_Etablissements_IdUtilisateur",
                table: "UtilisateurEtablissements",
                column: "IdUtilisateur",
                principalTable: "Etablissements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UtilisateurEtablissements_Utilisateurs_IdUtilisateur",
                table: "UtilisateurEtablissements",
                column: "IdUtilisateur",
                principalTable: "Utilisateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UtilisateurEtablissements_Etablissements_IdUtilisateur",
                table: "UtilisateurEtablissements");

            migrationBuilder.DropForeignKey(
                name: "FK_UtilisateurEtablissements_Utilisateurs_IdUtilisateur",
                table: "UtilisateurEtablissements");

            migrationBuilder.DropIndex(
                name: "IX_UtilisateurEtablissements_IdUtilisateur",
                table: "UtilisateurEtablissements");

            migrationBuilder.DropColumn(
                name: "Statut",
                table: "UtilisateurEtablissements");

            migrationBuilder.AddColumn<int>(
                name: "EtablissementId",
                table: "UtilisateurEtablissements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UtilisateurId",
                table: "UtilisateurEtablissements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UtilisateurEtablissements_EtablissementId",
                table: "UtilisateurEtablissements",
                column: "EtablissementId");

            migrationBuilder.CreateIndex(
                name: "IX_UtilisateurEtablissements_UtilisateurId",
                table: "UtilisateurEtablissements",
                column: "UtilisateurId");

            migrationBuilder.AddForeignKey(
                name: "FK_UtilisateurEtablissements_Etablissements_EtablissementId",
                table: "UtilisateurEtablissements",
                column: "EtablissementId",
                principalTable: "Etablissements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UtilisateurEtablissements_Utilisateurs_UtilisateurId",
                table: "UtilisateurEtablissements",
                column: "UtilisateurId",
                principalTable: "Utilisateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
