using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class Migration_17 : Migration
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
                name: "FK_UtilisateurEtablissements_Utilisateurs_IdUtilisateur",
                table: "UtilisateurEtablissements");

            migrationBuilder.DropForeignKey(
                name: "FK_Utilisateurs_Types_IdType",
                table: "Utilisateurs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Utilisateurs",
                table: "Utilisateurs");

            migrationBuilder.DropIndex(
                name: "IX_Utilisateurs_Email",
                table: "Utilisateurs");

            migrationBuilder.DropIndex(
                name: "IX_Utilisateurs_IdType",
                table: "Utilisateurs");

            migrationBuilder.DropIndex(
                name: "IX_Utilisateurs_Telephone",
                table: "Utilisateurs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Types",
                table: "Types");

            migrationBuilder.DropIndex(
                name: "IX_Types_Libelle",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "MotDePasse",
                table: "Utilisateurs");

            migrationBuilder.DropColumn(
                name: "Telephone",
                table: "Utilisateurs");

            migrationBuilder.DropColumn(
                name: "Libelle",
                table: "Types");

            migrationBuilder.RenameTable(
                name: "Utilisateurs",
                newName: "Utilisateur");

            migrationBuilder.RenameTable(
                name: "Types",
                newName: "TypeE");

            migrationBuilder.RenameColumn(
                name: "IdType",
                table: "Utilisateur",
                newName: "AccessFailedCount");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Utilisateur",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Utilisateur",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Utilisateur",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Utilisateur",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Utilisateur",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Utilisateur",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Utilisateur",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Utilisateur",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Utilisateur",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Utilisateur",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Utilisateur",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Utilisateur",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Utilisateur",
                type: "nvarchar(max)",
                nullable: true);

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
                name: "PK_Utilisateur",
                table: "Utilisateur",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeE",
                table: "TypeE",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Postes_TypeE_IdType",
                table: "Postes",
                column: "IdType",
                principalTable: "TypeE",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Salles_TypeE_IdType",
                table: "Salles",
                column: "IdType",
                principalTable: "TypeE",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UtilisateurEtablissements_Utilisateur_IdUtilisateur",
                table: "UtilisateurEtablissements",
                column: "IdUtilisateur",
                principalTable: "Utilisateur",
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
                name: "FK_UtilisateurEtablissements_Utilisateur_IdUtilisateur",
                table: "UtilisateurEtablissements");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Utilisateur",
                table: "Utilisateur");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeE",
                table: "TypeE");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Utilisateur");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Utilisateur");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Utilisateur");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Utilisateur");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Utilisateur");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Utilisateur");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Utilisateur");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Utilisateur");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Utilisateur");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Utilisateur");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Utilisateur");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Utilisateur");

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
                name: "Utilisateur",
                newName: "Utilisateurs");

            migrationBuilder.RenameTable(
                name: "TypeE",
                newName: "Types");

            migrationBuilder.RenameColumn(
                name: "AccessFailedCount",
                table: "Utilisateurs",
                newName: "IdType");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Utilisateurs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotDePasse",
                table: "Utilisateurs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telephone",
                table: "Utilisateurs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Libelle",
                table: "Types",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Utilisateurs",
                table: "Utilisateurs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Types",
                table: "Types",
                column: "Id");

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

            migrationBuilder.CreateIndex(
                name: "IX_Types_Libelle",
                table: "Types",
                column: "Libelle",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Postes_Types_IdType",
                table: "Postes",
                column: "IdType",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Salles_Types_IdType",
                table: "Salles",
                column: "IdType",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UtilisateurEtablissements_Utilisateurs_IdUtilisateur",
                table: "UtilisateurEtablissements",
                column: "IdUtilisateur",
                principalTable: "Utilisateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Utilisateurs_Types_IdType",
                table: "Utilisateurs",
                column: "IdType",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
