using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class Migration7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdType",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "IdType",
                table: "Postes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdType",
                table: "Users",
                column: "IdType");

            migrationBuilder.CreateIndex(
                name: "IX_Postes_IdType",
                table: "Postes",
                column: "IdType");

            migrationBuilder.AddForeignKey(
                name: "FK_Postes_Types_IdType",
                table: "Postes",
                column: "IdType",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Types_IdType",
                table: "Users",
                column: "IdType",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Postes_Types_IdType",
                table: "Postes");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Types_IdType",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_IdType",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Postes_IdType",
                table: "Postes");

            migrationBuilder.DropColumn(
                name: "IdType",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "IdType",
                table: "Postes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
