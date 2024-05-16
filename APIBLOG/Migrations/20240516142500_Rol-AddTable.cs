using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIBLOG.Migrations
{
    /// <inheritdoc />
    public partial class RolAddTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rol",
                table: "USUARIO");

            migrationBuilder.AddColumn<int>(
                name: "IdRol",
                table: "USUARIO",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ROL",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ROL__DDBEFBF9A4E0BB74", x => x.IdRol);
                });

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_IdRol",
                table: "USUARIO",
                column: "IdRol");

            migrationBuilder.AddForeignKey(
                name: "FK__USUARIO__IdRol__619B8048",
                table: "USUARIO",
                column: "IdRol",
                principalTable: "ROL",
                principalColumn: "IdRol",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__USUARIO__IdRol__619B8048",
                table: "USUARIO");

            migrationBuilder.DropTable(
                name: "ROL");

            migrationBuilder.DropIndex(
                name: "IX_USUARIO_IdRol",
                table: "USUARIO");

            migrationBuilder.DropColumn(
                name: "IdRol",
                table: "USUARIO");

            migrationBuilder.AddColumn<string>(
                name: "Rol",
                table: "USUARIO",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }
    }
}
