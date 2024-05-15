using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIBLOG.Migrations
{
    /// <inheritdoc />
    public partial class CategoriaRolEtiqueta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "POST_CATEGORIA");

            migrationBuilder.DropPrimaryKey(
                name: "PK__CATEGORI__A3C02A1086117A8A",
                table: "CATEGORIA");

            migrationBuilder.AddColumn<string>(
                name: "Rol",
                table: "USUARIO",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdCategoria",
                table: "POST",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "CATEGORIA",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "CATEGORIA",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK__CATEGORI__DDBEFBF9A4E0BB96",
                table: "CATEGORIA",
                column: "IdCategoria");

            migrationBuilder.CreateTable(
                name: "ETIQUETA",
                columns: table => new
                {
                    IdEtiqueta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ETIQUETA__A3C02A1086117A8A", x => x.IdEtiqueta);
                });

            migrationBuilder.CreateTable(
                name: "POST_ETIQUETA",
                columns: table => new
                {
                    IdEtiqueta = table.Column<int>(type: "int", nullable: false),
                    IdPost = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__POST_ETI__6C4DE1C4DCBE7F07", x => new { x.IdEtiqueta, x.IdPost });
                    table.ForeignKey(
                        name: "FK__POST_ETI__IdEti__68487DD7",
                        column: x => x.IdEtiqueta,
                        principalTable: "ETIQUETA",
                        principalColumn: "IdEtiqueta");
                    table.ForeignKey(
                        name: "FK__POST_ETI__IdPos__693CA210",
                        column: x => x.IdPost,
                        principalTable: "POST",
                        principalColumn: "IdPost");
                });

            migrationBuilder.CreateIndex(
                name: "IX_POST_IdCategoria",
                table: "POST",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_POST_ETIQUETA_IdPost",
                table: "POST_ETIQUETA",
                column: "IdPost");

            migrationBuilder.AddForeignKey(
                name: "FK__POST__IdCategoria__619B8050",
                table: "POST",
                column: "IdCategoria",
                principalTable: "CATEGORIA",
                principalColumn: "IdCategoria",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__POST__IdCategoria__619B8050",
                table: "POST");

            migrationBuilder.DropTable(
                name: "POST_ETIQUETA");

            migrationBuilder.DropTable(
                name: "ETIQUETA");

            migrationBuilder.DropIndex(
                name: "IX_POST_IdCategoria",
                table: "POST");

            migrationBuilder.DropPrimaryKey(
                name: "PK__CATEGORI__DDBEFBF9A4E0BB96",
                table: "CATEGORIA");

            migrationBuilder.DropColumn(
                name: "Rol",
                table: "USUARIO");

            migrationBuilder.DropColumn(
                name: "IdCategoria",
                table: "POST");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "CATEGORIA");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "CATEGORIA",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__CATEGORI__A3C02A1086117A8A",
                table: "CATEGORIA",
                column: "IdCategoria");

            migrationBuilder.CreateTable(
                name: "POST_CATEGORIA",
                columns: table => new
                {
                    IdCategoria = table.Column<int>(type: "int", nullable: false),
                    IdPost = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__POST_CAT__6C4DE1C4DCBE7F07", x => new { x.IdCategoria, x.IdPost });
                    table.ForeignKey(
                        name: "FK__POST_CATE__IdCat__68487DD7",
                        column: x => x.IdCategoria,
                        principalTable: "CATEGORIA",
                        principalColumn: "IdCategoria");
                    table.ForeignKey(
                        name: "FK__POST_CATE__IdPos__693CA210",
                        column: x => x.IdPost,
                        principalTable: "POST",
                        principalColumn: "IdPost");
                });

            migrationBuilder.CreateIndex(
                name: "IX_POST_CATEGORIA_IdPost",
                table: "POST_CATEGORIA",
                column: "IdPost");
        }
    }
}
