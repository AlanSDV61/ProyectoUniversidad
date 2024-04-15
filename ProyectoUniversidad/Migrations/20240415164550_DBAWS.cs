using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoUniversidad.Migrations
{
    /// <inheritdoc />
    public partial class DBAWS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tipo_documentos",
                table: "Tipo_documentos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Servicios",
                table: "Servicios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seleccions",
                table: "Seleccions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rols",
                table: "Rols");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Profesors",
                table: "Profesors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Estudiantes",
                table: "Estudiantes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cuentas",
                table: "Cuentas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carreras",
                table: "Carreras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Asignaturas",
                table: "Asignaturas");

            migrationBuilder.RenameTable(
                name: "Tipo_documentos",
                newName: "Tipo_documento");

            migrationBuilder.RenameTable(
                name: "Servicios",
                newName: "Servicio");

            migrationBuilder.RenameTable(
                name: "Seleccions",
                newName: "Seleccion");

            migrationBuilder.RenameTable(
                name: "Rols",
                newName: "Rol");

            migrationBuilder.RenameTable(
                name: "Profesors",
                newName: "Profesor");

            migrationBuilder.RenameTable(
                name: "Estudiantes",
                newName: "Estudiante");

            migrationBuilder.RenameTable(
                name: "Cuentas",
                newName: "Cuenta");

            migrationBuilder.RenameTable(
                name: "Carreras",
                newName: "Carrera");

            migrationBuilder.RenameTable(
                name: "Asignaturas",
                newName: "Asignatura");

            migrationBuilder.RenameIndex(
                name: "IX_Servicios_servicio_nombre",
                table: "Servicio",
                newName: "IX_Servicio_servicio_nombre");

            migrationBuilder.RenameIndex(
                name: "IX_Cuentas_cuenta_nombre",
                table: "Cuenta",
                newName: "IX_Cuenta_cuenta_nombre");

            migrationBuilder.RenameIndex(
                name: "IX_Carreras_carrera_nombre",
                table: "Carrera",
                newName: "IX_Carrera_carrera_nombre");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tipo_documento",
                table: "Tipo_documento",
                column: "tipo_documento_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Servicio",
                table: "Servicio",
                column: "servicio_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seleccion",
                table: "Seleccion",
                column: "seleccion_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rol",
                table: "Rol",
                column: "rol_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profesor",
                table: "Profesor",
                column: "profesor_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Estudiante",
                table: "Estudiante",
                column: "estudiante_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cuenta",
                table: "Cuenta",
                column: "cuenta_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carrera",
                table: "Carrera",
                column: "carrera_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Asignatura",
                table: "Asignatura",
                column: "asignatura_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tipo_documento",
                table: "Tipo_documento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Servicio",
                table: "Servicio");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seleccion",
                table: "Seleccion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rol",
                table: "Rol");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Profesor",
                table: "Profesor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Estudiante",
                table: "Estudiante");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cuenta",
                table: "Cuenta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carrera",
                table: "Carrera");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Asignatura",
                table: "Asignatura");

            migrationBuilder.RenameTable(
                name: "Tipo_documento",
                newName: "Tipo_documentos");

            migrationBuilder.RenameTable(
                name: "Servicio",
                newName: "Servicios");

            migrationBuilder.RenameTable(
                name: "Seleccion",
                newName: "Seleccions");

            migrationBuilder.RenameTable(
                name: "Rol",
                newName: "Rols");

            migrationBuilder.RenameTable(
                name: "Profesor",
                newName: "Profesors");

            migrationBuilder.RenameTable(
                name: "Estudiante",
                newName: "Estudiantes");

            migrationBuilder.RenameTable(
                name: "Cuenta",
                newName: "Cuentas");

            migrationBuilder.RenameTable(
                name: "Carrera",
                newName: "Carreras");

            migrationBuilder.RenameTable(
                name: "Asignatura",
                newName: "Asignaturas");

            migrationBuilder.RenameIndex(
                name: "IX_Servicio_servicio_nombre",
                table: "Servicios",
                newName: "IX_Servicios_servicio_nombre");

            migrationBuilder.RenameIndex(
                name: "IX_Cuenta_cuenta_nombre",
                table: "Cuentas",
                newName: "IX_Cuentas_cuenta_nombre");

            migrationBuilder.RenameIndex(
                name: "IX_Carrera_carrera_nombre",
                table: "Carreras",
                newName: "IX_Carreras_carrera_nombre");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tipo_documentos",
                table: "Tipo_documentos",
                column: "tipo_documento_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Servicios",
                table: "Servicios",
                column: "servicio_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seleccions",
                table: "Seleccions",
                column: "seleccion_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rols",
                table: "Rols",
                column: "rol_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profesors",
                table: "Profesors",
                column: "profesor_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Estudiantes",
                table: "Estudiantes",
                column: "estudiante_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cuentas",
                table: "Cuentas",
                column: "cuenta_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carreras",
                table: "Carreras",
                column: "carrera_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Asignaturas",
                table: "Asignaturas",
                column: "asignatura_id");
        }
    }
}
