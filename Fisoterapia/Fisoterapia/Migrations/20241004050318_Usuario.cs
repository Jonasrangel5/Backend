using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fisoterapia.Migrations
{
    /// <inheritdoc />
    public partial class Usuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "usu");

            migrationBuilder.CreateTable(
                name: "TipoUsuario",
                schema: "usu",
                columns: table => new
                {
                    idTipoUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoUsuario", x => x.idTipoUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                schema: "usu",
                columns: table => new
                {
                    idUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    usuario = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    primerNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    segundoNombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    primerApellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    segundoApellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nombreCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contraena = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    idTipoUsuario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.idUsuario);
                    table.ForeignKey(
                        name: "FK_Usuario_TipoUsuario_idTipoUsuario",
                        column: x => x.idTipoUsuario,
                        principalSchema: "usu",
                        principalTable: "TipoUsuario",
                        principalColumn: "idTipoUsuario");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_idTipoUsuario",
                schema: "usu",
                table: "Usuario",
                column: "idTipoUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario",
                schema: "usu");

            migrationBuilder.DropTable(
                name: "TipoUsuario",
                schema: "usu");
        }
    }
}
