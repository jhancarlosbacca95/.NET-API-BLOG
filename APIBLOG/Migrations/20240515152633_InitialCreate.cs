using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIBLOG.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CATEGORIA",
                columns: table => new
                {
                    IdCategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CATEGORI__A3C02A1086117A8A", x => x.IdCategoria);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Usuario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Contraseña = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__USUARIO__5B65BF978CD2A107", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "HISTORIAL_REFRESH_TOKEN",
                columns: table => new
                {
                    IdHistorialToken = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaExpiracion = table.Column<DateTime>(type: "datetime", nullable: true),
                    EsActivo = table.Column<bool>(type: "bit", nullable: true, computedColumnSql: "(case when [FechaExpiracion]<getdate() then CONVERT([bit],(0)) else CONVERT([bit],(1)) end)", stored: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__HISTORIA__03DC48A56DB735F0", x => x.IdHistorialToken);
                    table.ForeignKey(
                        name: "FK__HISTORIAL__IdUsu__6FE99F9F",
                        column: x => x.IdUsuario,
                        principalTable: "USUARIO",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "POST",
                columns: table => new
                {
                    IdPost = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Contenido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__POST__F8DCBD4D44C192C8", x => x.IdPost);
                    table.ForeignKey(
                        name: "FK__POST__IdUsuario__619B8048",
                        column: x => x.IdUsuario,
                        principalTable: "USUARIO",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "COMENTARIO",
                columns: table => new
                {
                    IdComentario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdPost = table.Column<int>(type: "int", nullable: true),
                    Contenido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__COMENTAR__DDBEFBF9A4E0BB96", x => x.IdComentario);
                    table.ForeignKey(
                        name: "FK__COMENTARI__IdPos__656C112C",
                        column: x => x.IdPost,
                        principalTable: "POST",
                        principalColumn: "IdPost");
                    table.ForeignKey(
                        name: "FK__COMENTARI__IdUsu__6477ECF3",
                        column: x => x.IdUsuario,
                        principalTable: "USUARIO",
                        principalColumn: "IdUsuario");
                });

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
                name: "IX_COMENTARIO_IdPost",
                table: "COMENTARIO",
                column: "IdPost");

            migrationBuilder.CreateIndex(
                name: "IX_COMENTARIO_IdUsuario",
                table: "COMENTARIO",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_HISTORIAL_REFRESH_TOKEN_IdUsuario",
                table: "HISTORIAL_REFRESH_TOKEN",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_POST_IdUsuario",
                table: "POST",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_POST_CATEGORIA_IdPost",
                table: "POST_CATEGORIA",
                column: "IdPost");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "COMENTARIO");

            migrationBuilder.DropTable(
                name: "HISTORIAL_REFRESH_TOKEN");

            migrationBuilder.DropTable(
                name: "POST_CATEGORIA");

            migrationBuilder.DropTable(
                name: "CATEGORIA");

            migrationBuilder.DropTable(
                name: "POST");

            migrationBuilder.DropTable(
                name: "USUARIO");
        }
    }
}
