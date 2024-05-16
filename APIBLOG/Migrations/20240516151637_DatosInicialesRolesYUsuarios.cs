using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIBLOG.Migrations
{
    /// <inheritdoc />
    public partial class DatosInicialesRolesYUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ROL",
                columns: new[] { "IdRol", "Nombre" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "SuperAdmin" },
                    { 3, "Usuario" }
                });

            migrationBuilder.InsertData(
                table: "USUARIO",
                columns: new[] { "IdUsuario", "Activo", "Contraseña", "CorreoElectronico", "FechaRegistro", "IdRol", "Usuario" },
                values: new object[,]
                {
                    { new Guid("210396a8-904d-4646-b078-25fbe622ce74"), null, "SuperAdminPass123", "superadmin@example.com", new DateTime(2024, 5, 16, 10, 16, 36, 639, DateTimeKind.Local).AddTicks(3116), 2, "superadmin_user" },
                    { new Guid("3b39c206-79b2-4613-80fc-b7bbf54c585f"), null, "AdminPass123", "admin@example.com", new DateTime(2024, 5, 16, 10, 16, 36, 639, DateTimeKind.Local).AddTicks(3097), 1, "admin_user" },
                    { new Guid("f43f0bac-af76-497b-a50f-f08572d5c288"), null, "UserPass123", "usuario@example.com", new DateTime(2024, 5, 16, 10, 16, 36, 639, DateTimeKind.Local).AddTicks(3119), 3, "usuario_user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USUARIO",
                keyColumn: "IdUsuario",
                keyValue: new Guid("210396a8-904d-4646-b078-25fbe622ce74"));

            migrationBuilder.DeleteData(
                table: "USUARIO",
                keyColumn: "IdUsuario",
                keyValue: new Guid("3b39c206-79b2-4613-80fc-b7bbf54c585f"));

            migrationBuilder.DeleteData(
                table: "USUARIO",
                keyColumn: "IdUsuario",
                keyValue: new Guid("f43f0bac-af76-497b-a50f-f08572d5c288"));

            migrationBuilder.DeleteData(
                table: "ROL",
                keyColumn: "IdRol",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ROL",
                keyColumn: "IdRol",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ROL",
                keyColumn: "IdRol",
                keyValue: 3);
        }
    }
}
