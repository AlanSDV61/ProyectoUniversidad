using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoUniversidad.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estudiantes",
                columns: table => new
                {
                    estudiante_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    estudiante_nombre = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    estudiante_apellido = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    carrera_id = table.Column<int>(type: "int", nullable: false),
                    estudiante_indice = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    estudiante_telefono = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    estudiante_correo = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    cuenta_id = table.Column<int>(type: "int", nullable: false),
                    estudiante_nacionalidad = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    estudiante_trimestre = table.Column<int>(type: "int", nullable: false),
                    tipo_documento_id = table.Column<int>(type: "int", nullable: false),
                    estudiante_documento = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    estudiante_fecha_ingreso = table.Column<DateOnly>(type: "date", nullable: false),
                    estudiante_activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiantes", x => x.estudiante_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estudiantes");
        }
    }
}
