using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fisoterapia.Migrations
{
    /// <inheritdoc />
    public partial class AddLesionInformaAvanceReseta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lesiones",
                columns: table => new
                {
                    IdLesion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lesiones", x => x.IdLesion);
                });

            migrationBuilder.CreateTable(
                name: "InformasAvance",
                columns: table => new
                {
                    IdInforma = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdLesion = table.Column<int>(type: "int", nullable: false),
                    EvolucionLesion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EjerciciosRealizados = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comentarios = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformasAvance", x => x.IdInforma);
                    table.ForeignKey(
                        name: "FK_InformasAvance_Lesiones_IdLesion",
                        column: x => x.IdLesion,
                        principalTable: "Lesiones",
                        principalColumn: "IdLesion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InformasAvance_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalSchema: "usu",
                        principalTable: "Usuario",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resetas",
                columns: table => new
                {
                    IdReseta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdLesion = table.Column<int>(type: "int", nullable: false),
                    Evolucion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comentarios = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resetas", x => x.IdReseta);
                    table.ForeignKey(
                        name: "FK_Resetas_Lesiones_IdLesion",
                        column: x => x.IdLesion,
                        principalTable: "Lesiones",
                        principalColumn: "IdLesion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Resetas_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalSchema: "usu",
                        principalTable: "Usuario",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InformasAvance_IdLesion",
                table: "InformasAvance",
                column: "IdLesion");

            migrationBuilder.CreateIndex(
                name: "IX_InformasAvance_IdUsuario",
                table: "InformasAvance",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Resetas_IdLesion",
                table: "Resetas",
                column: "IdLesion");

            migrationBuilder.CreateIndex(
                name: "IX_Resetas_IdUsuario",
                table: "Resetas",
                column: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InformasAvance");

            migrationBuilder.DropTable(
                name: "Resetas");

            migrationBuilder.DropTable(
                name: "Lesiones");
        }
    }
}
