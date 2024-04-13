using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoUniversidad.Migrations
{
    /// <inheritdoc />
    public partial class Migracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carreras",
                columns: table => new
                {
                    carrera_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    carrera_nombre = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    carrera_area = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carreras", x => x.carrera_id);
                });

            migrationBuilder.CreateTable(
                name: "Rols",
                columns: table => new
                {
                    rol_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rol_nombre = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rols", x => x.rol_id);
                });

            migrationBuilder.CreateTable(
                name: "Servicios",
                columns: table => new
                {
                    servicio_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    servicio_nombre = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    servicio_costo = table.Column<decimal>(type: "decimal(9,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicios", x => x.servicio_id);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_documentos",
                columns: table => new
                {
                    tipo_documento_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipo_documento = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_documentos", x => x.tipo_documento_id);
                });

            migrationBuilder.CreateTable(
                name: "Cuentas",
                columns: table => new
                {
                    cuenta_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cuenta_nombre = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    cuenta_clave = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    rol_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuentas", x => x.cuenta_id);
                    table.ForeignKey(
                        name: "FK_Cuentas_Rols_rol_id",
                        column: x => x.rol_id,
                        principalTable: "Rols",
                        principalColumn: "rol_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Estudiantes",
                columns: table => new
                {
                    estudiante_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    estudiante_nombre = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    estudiante_apellido = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    carrera_id = table.Column<int>(type: "int", nullable: false),
                    estudiante_indice = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Estudiantes_Carreras_carrera_id",
                        column: x => x.carrera_id,
                        principalTable: "Carreras",
                        principalColumn: "carrera_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Estudiantes_Cuentas_cuenta_id",
                        column: x => x.cuenta_id,
                        principalTable: "Cuentas",
                        principalColumn: "cuenta_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Estudiantes_Tipo_documentos_tipo_documento_id",
                        column: x => x.tipo_documento_id,
                        principalTable: "Tipo_documentos",
                        principalColumn: "tipo_documento_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profesors",
                columns: table => new
                {
                    profesor_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    profesor_nombres = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    profesor_apellidos = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    tipo_documento_id = table.Column<int>(type: "int", nullable: false),
                    profesor_documento = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    id_cuenta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesors", x => x.profesor_id);
                    table.ForeignKey(
                        name: "FK_Profesors_Cuentas_id_cuenta",
                        column: x => x.id_cuenta,
                        principalTable: "Cuentas",
                        principalColumn: "cuenta_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Profesors_Tipo_documentos_tipo_documento_id",
                        column: x => x.tipo_documento_id,
                        principalTable: "Tipo_documentos",
                        principalColumn: "tipo_documento_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seleccions",
                columns: table => new
                {
                    seleccion_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    estudiante_id = table.Column<int>(type: "int", nullable: false),
                    seleccion_trimestre = table.Column<int>(type: "int", nullable: false),
                    seleccion_estado = table.Column<int>(type: "int", nullable: false),
                    seleccion_indice = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    seleccion_creditos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seleccions", x => x.seleccion_id);
                    table.ForeignKey(
                        name: "FK_Seleccions_Estudiantes_estudiante_id",
                        column: x => x.estudiante_id,
                        principalTable: "Estudiantes",
                        principalColumn: "estudiante_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Asignaturas",
                columns: table => new
                {
                    asignatura_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    profesor_id = table.Column<int>(type: "int", nullable: false),
                    asignatura_nombre = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    asignatura_aula = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    asignatura_creditos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asignaturas", x => x.asignatura_id);
                    table.ForeignKey(
                        name: "FK_Asignaturas_Profesors_profesor_id",
                        column: x => x.profesor_id,
                        principalTable: "Profesors",
                        principalColumn: "profesor_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asignaturas_profesor_id",
                table: "Asignaturas",
                column: "profesor_id");

            migrationBuilder.CreateIndex(
                name: "IX_Carreras_carrera_nombre",
                table: "Carreras",
                column: "carrera_nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cuentas_rol_id",
                table: "Cuentas",
                column: "rol_id");

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_carrera_id",
                table: "Estudiantes",
                column: "carrera_id");

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_cuenta_id",
                table: "Estudiantes",
                column: "cuenta_id");

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_tipo_documento_id",
                table: "Estudiantes",
                column: "tipo_documento_id");

            migrationBuilder.CreateIndex(
                name: "IX_Profesors_id_cuenta",
                table: "Profesors",
                column: "id_cuenta");

            migrationBuilder.CreateIndex(
                name: "IX_Profesors_tipo_documento_id",
                table: "Profesors",
                column: "tipo_documento_id");

            migrationBuilder.CreateIndex(
                name: "IX_Seleccions_estudiante_id",
                table: "Seleccions",
                column: "estudiante_id");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_servicio_nombre",
                table: "Servicios",
                column: "servicio_nombre",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asignaturas");

            migrationBuilder.DropTable(
                name: "Seleccions");

            migrationBuilder.DropTable(
                name: "Servicios");

            migrationBuilder.DropTable(
                name: "Profesors");

            migrationBuilder.DropTable(
                name: "Estudiantes");

            migrationBuilder.DropTable(
                name: "Carreras");

            migrationBuilder.DropTable(
                name: "Cuentas");

            migrationBuilder.DropTable(
                name: "Tipo_documentos");

            migrationBuilder.DropTable(
                name: "Rols");
        }
    }
}
